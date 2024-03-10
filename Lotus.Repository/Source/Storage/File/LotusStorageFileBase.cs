using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;
using Lotus.Core.Serialization;

using Newtonsoft.Json;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Определение базового хранилища в отдельном файле.
    /// </summary>
    public class StorageFileBase : PropertyChangedBase, ILotusStorage
    {
        #region Const
        /// <summary>
        /// Расширение для формата Json
        /// </summary>
        public const string JsonExtension = ".json";

        /// <summary>
        /// Расширение для бинарного формата
        /// </summary>
        public const string BytesExtension = ".bytes";
        #endregion

        #region Fields
        protected internal string _fileName;
        protected internal bool _needSaved;
        protected internal JsonSerializerSettings _serializerSettings;
        #endregion

        #region Properties
        /// <summary>
        /// Данные связвания хранилища с источником.
        /// </summary>
        public string ConnectingData
        {
            get { return _fileName; }
            set 
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус указывающий на то что хранилище необходимо сохранить.
        /// </summary>
        public bool NeedSaved
        {
            get { return _needSaved; }
            set
            {
                _needSaved = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Структура данных файла.
        /// </summary>
        public virtual ILotusStorageStructure IStructure
        {
            get { return null!; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public StorageFileBase()
        {
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.Formatting = Formatting.Indented;
            _serializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Default;
            _serializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            _serializerSettings.Converters = [ColorConverter.Instance];
        }
        #endregion

        #region ILotusStorage methods
        /// <inheritdoc/>
        public virtual ILotusRepository<TEntity, TKey>? GetRepository<TEntity, TKey>()
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
        {
            var fileStructure = (IStructure as ILotusFileStorageStructure)!;
            var list = fileStructure.GetEntitiesList<TEntity>();
            if (list != null)
            {
                return new RepositoryFileList<TEntity, TKey>(list, this);
            }

            return null;
        }

        /// <summary>
        /// Сохранить все изменения в хранилище.
        /// </summary>
        /// <returns>Количество сохранённых изменений.</returns>
        public virtual int SaveChanges()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                return 0;
            }

            if(_fileName.EndsWith(JsonExtension, true, null))
            {
                var raw = JsonConvert.SerializeObject(IStructure, _serializerSettings);
                
                File.WriteAllText(_fileName, raw);

                return 1;
            }
            else
            {
                if (_fileName.EndsWith(BytesExtension, true, null))
                {
                    if(IStructure is ILotusSerializeToBinary serializeToBinary)
                    {
                        using var fileStream = new FileStream(_fileName, FileMode.Create);
                        using var binaryWriter = new BinaryWriter(fileStream);
                        serializeToBinary.WriteToBinary(binaryWriter);

                        return 1;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Сохранить все изменения в хранилище.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Количество сохранённых изменений.</returns>
        public virtual async ValueTask<int> SaveChangesAsync(CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                return await ValueTask.FromResult(0);
            }

            if (_fileName.EndsWith(XFileExtension.JSON, true, null))
            {
                var raw = JsonConvert.SerializeObject(IStructure, _serializerSettings);

                await System.IO.File.WriteAllTextAsync(_fileName, raw);

                return 1;
            }

            return 0;
        }
        #endregion
    }
    /**@}*/
}