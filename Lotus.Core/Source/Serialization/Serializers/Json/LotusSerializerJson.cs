using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Cериализатор для сохранения/загрузки объектов в формат Json.
    /// </summary>
    public class CSerializerJson : CBaseSerializer
    {
        #region Const
        //
        // БАЗОВЫЕ ПУТИ
        //
#if UNITY_2017_1_OR_NEWER
#if UNITY_EDITOR
        /// <summary>
        /// Путь по умолчанию для сохранения/загрузки файлов.
        /// </summary>
        public static string DefaultPath = Lotus.Core.XCoreSettings.AutoSavePath;
#else
        /// <summary>
        /// Путь по умолчанию для сохранения/загрузки  файлов.
        /// </summary>
        public static string DefaultPath = UnityEngine.Application.persistentDataPath;
#endif
#else
        /// <summary>
        /// Путь по умолчанию для сохранения/загрузки файлов.
        /// </summary>
        public static string DefaultPath = Environment.CurrentDirectory;
#endif

        //
        // БАЗОВЫЕ РАСШИРЕНИЯ
        //
        /// <summary>
        /// Расширение файла по умолчанию.
        /// </summary>
        public static string DefaultExt = XFileExtension.JSON_D;
        #endregion

        #region Fields
        protected internal JsonSerializerOptions _options;
        #endregion

        #region Properties
        /// <summary>
        /// Опции сериализатора Json.
        /// </summary>
        public JsonSerializerOptions Options
        {
            get
            {
                return _options;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CSerializerJson()
        {
            _options = new JsonSerializerOptions();
            AddDefaultConverters(false);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public CSerializerJson(bool useTypeName)
        {
            _options = new JsonSerializerOptions();
            AddDefaultConverters(useTypeName);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сериализатора.</param>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public CSerializerJson(string name, bool useTypeName = true)
            : base(name)
        {
            _options = new JsonSerializerOptions();
            AddDefaultConverters(useTypeName);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление конвертера.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public void AddConverter(JsonConverter converter)
        {
            _options.Converters.Add(converter);
        }

        /// <summary>
        /// Добавление стандартных конвертеров.
        /// </summary>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public void AddDefaultConverters(bool useTypeName)
        {
            _options.Converters.Add(ColorConverter.Instance);
#if UNITY_2017_1_OR_NEWER
            // Примечание: Конвертеры Unity также должны быть переписаны на System.Text.Json.Serialization.JsonConverter
            // _options.Converters.Add(CUnityColor32Converter.Instance);
            // _options.Converters.Add(CUnityVector2Converter.Instance);
            // ... и т.д.
#endif

            // Настройки, аналогичные Formatting.Indented
            _options.WriteIndented = true;

            // Настройки для совместимости (опционально)
            _options.PropertyNameCaseInsensitive = true; // Игнорировать регистр имен свойств
            _options.IncludeFields = true; // Сериализировать поля (public), актуально для Unity/структур

            if (useTypeName)
            {
                // ВНИМАНИЕ: System.Text.Json не поддерживает TypeNameHandling.Auto из соображений безопасности.
                // Для реализации полиморфизма необходимо использовать атрибуты [JsonDerivedType] (в .NET 7+) 
                // или писать собственный конвертер, который сохраняет информацию о типе.
                // Аналогичная функциональность требует отдельной реализации.
            }
        }
        #endregion

        #region Save methods
        /// <summary>
        /// Сохранения объекта в файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        public override void SaveTo(string fileName, object instance, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Создаем поток для записи
            using (var stream_writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                SaveTo(stream_writer, instance, parameters);
            }
            // stream_writer.Close(); // Используем using, закрытие происходит автоматически

#if UNITY_EDITOR
            // Обновляем в редакторе
            UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
            UnityEditor.EditorUtility.DisplayDialog(XFileDialog.FILE_SAVE_SUCCESSFULLY, "Path\n" + path, "OK");
#endif
        }

        /// <summary>
        /// Сохранения объекта в строку в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        /// <returns>Строка в формате Json.</returns>
        public string SaveTo(object instance, CParameters? parameters = null)
        {
            // Добавляем себя в качестве параметра
            if (parameters == null) parameters = new CParameters("SerializerJson");
            parameters.AddObject(this.Name, this, false);

            if (instance is ILotusBeforeSave before_save)
            {
                before_save.OnBeforeSave(parameters);
            }

            var result = JsonSerializer.Serialize(instance, _options);

            if (instance is ILotusAfterSave after_save)
            {
                after_save.OnAfterSave(parameters);
            }

            return result;
        }

        /// <summary>
        /// Сохранения объекта в поток данных в формате Json.
        /// </summary>
        /// <param name="textWriter">Средство для записи в поток строковых данных.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        public void SaveTo(TextWriter textWriter, object instance, CParameters? parameters = null)
        {
            // Добавляем себя в качестве параметра
            if (parameters == null) parameters = new CParameters("SerializerJson");
            parameters.AddObject(this.Name, this, false);

            if (instance is ILotusBeforeSave before_save)
            {
                before_save.OnBeforeSave(parameters);
            }

            // JsonSerializer не умеет писать напрямую в TextWriter, только в Stream или string.
            // Поэтому получаем строку и пишем её.
            var json = JsonSerializer.Serialize(instance, _options);
            textWriter.Write(json);

            if (instance is ILotusAfterSave after_save)
            {
                after_save.OnAfterSave(parameters);
            }
        }
        #endregion

        #region Load methods
        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public override object? LoadFrom(string fileName, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var string_json = File.ReadAllText(path);

            // Читаем объект
            var result = LoadFromString(string_json, parameters);

            return result;
        }

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <typeparam name="TResultType">Тип объекта.</typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public override TResultType? LoadFrom<TResultType>(string fileName, CParameters? parameters = null) where TResultType : default
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var stringJson = File.ReadAllText(path);

            // Читаем объект
            var result = LoadFromString<TResultType>(stringJson, parameters);

            return result;
        }

        /// <summary>
        /// Загрузка объекта из строки в формате Json.
        /// </summary>
        /// <param name="stringJson">Строка с данными в формате Json.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public object? LoadFromString(string stringJson, CParameters? parameters = null)
        {
            // Читаем объект (аналог JsonConvert.DeserializeObject)
            var result = JsonSerializer.Deserialize(stringJson, typeof(object), _options);

            if (result is ILotusAfterLoad after_load)
            {
                // Добавляем себя в качестве параметра
                parameters ??= new CParameters("SerializerJson");
                parameters.AddObject(this.Name, this, false);

                after_load.OnAfterLoad(parameters);
            }

            return result;
        }

        /// <summary>
        /// Загрузка объекта из строки (обобщенный метод).
        /// </summary>
        public TResultType? LoadFromString<TResultType>(string stringJson, CParameters? parameters = null)
        {
            var result = JsonSerializer.Deserialize<TResultType>(stringJson, _options);

            if (result is ILotusAfterLoad after_load)
            {
                // Добавляем себя в качестве параметра
                if (parameters == null) parameters = new CParameters("SerializerJson");
                parameters.AddObject(this.Name, this, false);

                after_load.OnAfterLoad(parameters);
            }

            return result;
        }

        /// <summary>
        /// Загрузка объекта из потока данных.
        /// </summary>
        /// <param name="textReader">Средство для чтения из потока строковых данных.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public TResultType? LoadFrom<TResultType>(TextReader textReader, CParameters? parameters = null)
        {
            // System.Text.Json работает со строками или Stream.
            // Читаем весь контент из TextReader.
            var stringJson = textReader.ReadToEnd();
            var result = JsonSerializer.Deserialize<TResultType>(stringJson, _options);

            if (result is ILotusAfterLoad after_load)
            {
                // Добавляем себя в качестве параметра
                if (parameters == null) parameters = new CParameters("SerializerJson");
                parameters.AddObject(this.Name, this, false);

                after_load.OnAfterLoad(parameters);
            }

            return result;
        }
        #endregion

        #region Update methods
        /// <summary>
        /// Обновление объекта из файла.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public override void UpdateFrom(object instance, string fileName, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var string_json = File.ReadAllText(path);

            // Обновляем объект
            UpdateFromString(instance, string_json, parameters);
        }

        /// <summary>
        /// Обновление объекта из строки в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="stringJson">Строка с данными в формате Json.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public void UpdateFromString(object instance, string stringJson, CParameters? parameters = null)
        {
            // Добавляем себя в качестве параметра
            if (parameters == null) parameters = new CParameters("SerializerJson");
            parameters.AddObject(this.Name, this, false);

            if (instance is ILotusBeforeLoad before_load)
            {
                before_load.OnBeforeLoad(parameters);
            }

            // В System.Text.Json НЕТ встроенного метода PopulateObject (заполнение существующего объекта).
            // JsonConvert.PopulateObject заменяется созданием нового объекта и копированием свойств через рефлексию.
            // Это "заглушка", реализующая простейшее поведение.
            try
            {
                var type = instance.GetType();
                var tempInstance = JsonSerializer.Deserialize(stringJson, type, _options);

                if (tempInstance != null)
                {
                    CopyProperties(tempInstance, instance);
                }
            }
            catch
            {
                // Обработка ошибок десериализации
            }

            if (instance is ILotusAfterLoad after_load)
            {
                after_load.OnAfterLoad(parameters);
            }
        }

        /// <summary>
        /// Обновление объект из потока данных в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="textReader">Средство для чтения из потока строковых данных.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public void UpdateFrom(object instance, TextReader textReader, CParameters? parameters = null)
        {
            var stringJson = textReader.ReadToEnd();
            UpdateFromString(instance, stringJson, parameters);
        }

        /// <summary>
        /// Вспомогательный метод для копирования свойств (замена PopulateObject).
        /// </summary>
        /// <param name="source">Исходный объект.</param>
        /// <param name="destination">Целевой объект</param>
        private void CopyProperties(object source, object destination)
        {
            if (source == null || destination == null) return;

            var type = source.GetType();
            var flags = BindingFlags.Public | BindingFlags.Instance;

            foreach (var prop in type.GetProperties(flags))
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var value = prop.GetValue(source);
                    prop.SetValue(destination, value);
                }
            }

            foreach (var field in type.GetFields(flags))
            {
                var value = field.GetValue(source);
                field.SetValue(destination, value);
            }
        }
        #endregion
    }
    /**@}*/
}