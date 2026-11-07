using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreStorage
    *@{*/
    /// <summary>
    /// Файловая реализация <see cref="ILotusDataStorage"/>: рабочий набор в памяти,
    /// сброс на диск при <see cref="SaveChanges"/> / <see cref="SaveChangesAsync"/>.
    /// </summary>
    public class DataStorageFile : DataStorageInMemory
    {
        #region Const
        /// <summary>
        /// Расширение для формата Json.
        /// </summary>
        public const string JsonExtension = ".json";

        /// <summary>
        /// Расширение для бинарного формата.
        /// </summary>
        public const string BytesExtension = ".bytes";
        #endregion

        #region Fields
        protected internal JsonSerializerOptions _serializerOptions;
        protected internal Type _structureType;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="structure">Структура хранилища.</param>
        /// <param name="filePath">Путь к файлу.</param>
        public DataStorageFile(StorageStructureInMemory structure, string? filePath = null)
            : base(structure)
        {
            // Регистрируем List<> свойства типизированной structure для CRUD и JSON roundtrip.
            structure.RegisterFromProperties();
            _structureType = structure.GetType();
            _serializerOptions = CreateDefaultSerializerOptions();
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                ConnectingData = filePath;
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        public DataStorageFile(string? filePath = null)
            : this(new StorageStructureInMemory(), filePath)
        {
        }
        #endregion

        #region Options
        /// <summary>
        /// Опции JSON-сериализации.
        /// </summary>
        public JsonSerializerOptions SerializerOptions => _serializerOptions;

        /// <summary>
        /// Создать опции сериализации по умолчанию.
        /// </summary>
        protected static JsonSerializerOptions CreateDefaultSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
                Converters = { ColorConverter.Instance },
            };
        }
        #endregion

        #region Load
        /// <summary>
        /// Загрузить структуру из файла, указанного в <see cref="DataStorageInMemory.ConnectingData"/>.
        /// </summary>
        public virtual void Load()
        {
            if (string.IsNullOrWhiteSpace(ConnectingData) || !File.Exists(ConnectingData))
            {
                return;
            }

            if (ConnectingData.EndsWith(JsonExtension, StringComparison.OrdinalIgnoreCase))
            {
                LoadFromJson(ConnectingData);
                FixupRelationships();
                return;
            }

            if (ConnectingData.EndsWith(BytesExtension, StringComparison.OrdinalIgnoreCase))
            {
                LoadFromBinary(ConnectingData);
                FixupRelationships();
            }
        }

        /// <summary>
        /// Асинхронно загрузить структуру из файла.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        public virtual async Task LoadAsync(CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(ConnectingData) || !File.Exists(ConnectingData))
            {
                return;
            }

            if (ConnectingData.EndsWith(JsonExtension, StringComparison.OrdinalIgnoreCase))
            {
                await LoadFromJsonAsync(ConnectingData, token);
                FixupRelationships();
                return;
            }

            if (ConnectingData.EndsWith(BytesExtension, StringComparison.OrdinalIgnoreCase))
            {
                token.ThrowIfCancellationRequested();
                LoadFromBinary(ConnectingData);
                FixupRelationships();
            }
        }

        /// <summary>
        /// Загрузить структуру из JSON-файла.
        /// </summary>
        protected virtual void LoadFromJson(string filePath)
        {
            var raw = File.ReadAllText(filePath);
            ApplyLoadedStructure(JsonSerializer.Deserialize(raw, _structureType, _serializerOptions));
        }

        /// <summary>
        /// Асинхронно загрузить структуру из JSON-файла.
        /// </summary>
        protected virtual async Task LoadFromJsonAsync(string filePath, CancellationToken token)
        {
            await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            var loaded = await JsonSerializer.DeserializeAsync(stream, _structureType, _serializerOptions, token);
            ApplyLoadedStructure(loaded);
        }

        /// <summary>
        /// Загрузить структуру из бинарного файла.
        /// </summary>
        protected virtual void LoadFromBinary(string filePath)
        {
            if (_structure is not ILotusSerializeToBinary serializeToBinary)
            {
                throw new InvalidOperationException(
                    $"Structure type '{_structureType.Name}' must implement {nameof(ILotusSerializeToBinary)} for binary load.");
            }

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var binaryReader = new BinaryReader(fileStream);
            serializeToBinary.ReadFromBinary(binaryReader);
            NeedSaved = false;
            _pendingChanges = 0;
        }

        /// <summary>
        /// Применить загруженную структуру к текущему экземпляру.
        /// </summary>
        protected virtual void ApplyLoadedStructure(object? loaded)
        {
            if (loaded is null)
            {
                return;
            }

            if (loaded is not StorageStructureInMemory loadedStructure)
            {
                throw new InvalidOperationException(
                    $"Deserialized type '{loaded.GetType().Name}' is not {nameof(StorageStructureInMemory)}.");
            }

            if (ReferenceEquals(loadedStructure, _structure))
            {
                NeedSaved = false;
                _pendingChanges = 0;
                return;
            }

            // После JSON-десериализации List<> лежат в свойствах — регистрируем их в словарь.
            loadedStructure.RegisterFromProperties();

            // Копируем данные в уже используемый экземпляр structure (сохраняем ссылки списков у потребителей).
            _structure.CopyListsFrom(loadedStructure);

            // Для типизированных structure дополнительно копируем значения List<> свойств.
            CopyListProperties(loadedStructure, _structure);

            NeedSaved = false;
            _pendingChanges = 0;
        }

        /// <summary>
        /// Скопировать public List&lt;T&gt; свойства между структурами одного типа.
        /// </summary>
        protected static void CopyListProperties(StorageStructureInMemory source, StorageStructureInMemory target)
        {
            if (source.GetType() != target.GetType())
            {
                return;
            }

            var properties = source.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                if (!property.CanRead || !property.CanWrite)
                {
                    continue;
                }

                var propertyType = property.PropertyType;
                if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(System.Collections.Generic.List<>))
                {
                    continue;
                }

                var sourceValue = property.GetValue(source);
                var targetValue = property.GetValue(target);
                if (sourceValue is null)
                {
                    continue;
                }

                if (targetValue is null)
                {
                    property.SetValue(target, sourceValue);
                    continue;
                }

                if (targetValue is System.Collections.IList targetList && sourceValue is System.Collections.IList sourceList)
                {
                    targetList.Clear();
                    foreach (var item in sourceList)
                    {
                        targetList.Add(item);
                    }
                }
            }
        }
        #endregion

        #region Save
        /// <inheritdoc/>
        public override int SaveChanges()
        {
            if (string.IsNullOrWhiteSpace(ConnectingData))
            {
                return base.SaveChanges();
            }

            if (ConnectingData.EndsWith(JsonExtension, StringComparison.OrdinalIgnoreCase))
            {
                SaveToJson(ConnectingData);
                return base.SaveChanges();
            }

            if (ConnectingData.EndsWith(BytesExtension, StringComparison.OrdinalIgnoreCase))
            {
                SaveToBinary(ConnectingData);
                return base.SaveChanges();
            }

            return 0;
        }

        /// <inheritdoc/>
        public override async ValueTask<int> SaveChangesAsync(CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(ConnectingData))
            {
                return await base.SaveChangesAsync(token);
            }

            if (ConnectingData.EndsWith(JsonExtension, StringComparison.OrdinalIgnoreCase))
            {
                await SaveToJsonAsync(ConnectingData, token);
                return await base.SaveChangesAsync(token);
            }

            if (ConnectingData.EndsWith(BytesExtension, StringComparison.OrdinalIgnoreCase))
            {
                token.ThrowIfCancellationRequested();
                SaveToBinary(ConnectingData);
                return await base.SaveChangesAsync(token);
            }

            return 0;
        }

        /// <summary>
        /// Сохранить структуру в JSON-файл атомарно.
        /// </summary>
        protected virtual void SaveToJson(string filePath)
        {
            EnsureDirectoryExists(filePath);
            var tempPath = filePath + ".tmp";
            var raw = JsonSerializer.Serialize(_structure, _structureType, _serializerOptions);
            File.WriteAllText(tempPath, raw);
            ReplaceFile(tempPath, filePath);
        }

        /// <summary>
        /// Асинхронно сохранить структуру в JSON-файл атомарно.
        /// </summary>
        protected virtual async Task SaveToJsonAsync(string filePath, CancellationToken token)
        {
            EnsureDirectoryExists(filePath);
            var tempPath = filePath + ".tmp";
            await using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await JsonSerializer.SerializeAsync(stream, _structure, _structureType, _serializerOptions, token);
            }

            ReplaceFile(tempPath, filePath);
        }

        /// <summary>
        /// Сохранить структуру в бинарный файл атомарно.
        /// </summary>
        protected virtual void SaveToBinary(string filePath)
        {
            if (_structure is not ILotusSerializeToBinary serializeToBinary)
            {
                throw new InvalidOperationException(
                    $"Structure type '{_structureType.Name}' must implement {nameof(ILotusSerializeToBinary)} for binary save.");
            }

            EnsureDirectoryExists(filePath);
            var tempPath = filePath + ".tmp";
            using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                serializeToBinary.WriteToBinary(binaryWriter);
            }

            ReplaceFile(tempPath, filePath);
        }

        /// <summary>
        /// Создать каталог для файла при необходимости.
        /// </summary>
        protected static void EnsureDirectoryExists(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Атомарно заменить целевой файл временным.
        /// </summary>
        protected static void ReplaceFile(string tempPath, string targetPath)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            File.Move(tempPath, targetPath);
        }
        #endregion
    }
    /**@}*/
}
