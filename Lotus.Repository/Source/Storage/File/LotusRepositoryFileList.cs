using System;
using System.Collections.Generic;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Реализация репозитория <see cref="ILotusRepository{TEntity, TKey}"/> через простой список <see cref="List{T}"/>.
    /// </summary>
    public class RepositoryFileList<TEntity, TKey> : RepositoryList<TEntity, TKey>
                where TEntity : class, ILotusIdentifierId<TKey>, new()
                where TKey : notnull, IEquatable<TKey>
    {
        protected internal StorageFileBase _fileStorage;

        public RepositoryFileList(List<TEntity> list)
            : base(list)
        {
        }

        public RepositoryFileList(List<TEntity> list, StorageFileBase fileStorage)
            : base(list)
        {
            _fileStorage = fileStorage;
        }
    }
    /**@}*/
}