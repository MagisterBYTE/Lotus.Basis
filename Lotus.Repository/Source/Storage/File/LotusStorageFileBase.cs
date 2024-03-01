using System;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Определение базового хранилища в отдельном файле.
    /// </summary>
    public class StorageFileBase : ILotusStorage
    {
        #region Fields
        protected internal ILotusFileStorageStructure _fileStorageStructure;
        protected internal string _fileName;
        #endregion

        #region Properties
        /// <summary>
        /// Данные связвания хранилища с источником.
        /// </summary>
        public string ConnectingData
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// Структура данных файла.
        /// </summary>
        public ILotusFileStorageStructure Structure
        {
            get { return _fileStorageStructure; }
            set { _fileStorageStructure = value; }
        }
        #endregion

        #region Constructors
        public StorageFileBase()
        {

        }
        #endregion

        #region ILotusStorage methods
        /// <inheritdoc/>
        public virtual ILotusRepository<TEntity, TKey>? GetRepository<TEntity, TKey>()
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
        {
            var list = _fileStorageStructure.GetEntities<TEntity>();
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
            return 0;
        }

        /// <summary>
        /// Сохранить все изменения в хранилище.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Количество сохранённых изменений.</returns>
        public virtual async ValueTask<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await ValueTask.FromResult(0);
        }
        #endregion
    }
    /**@}*/
}