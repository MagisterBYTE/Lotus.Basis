using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Реализация репозитория <see cref="ILotusRepository{TEntity, TKey}"/> через простой список <see cref="List{T}"/>.
    /// </summary>
    public class RepositoryFileList<TEntity, TKey> : ILotusRepository<TEntity, TKey>
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
    {
        protected internal List<TEntity> _list;
        protected internal StorageFileBase? _fileStorage;

        /// <inheritdoc/>
        public bool SaveEachOperation { get; set; }

        /// <summary>
        /// Конструктор для работы только с списком без сохранения в файл.
        /// </summary>
        /// <param name="list">Список сущностей.</param>
        public RepositoryFileList(List<TEntity> list)
        {
            SaveEachOperation = true;
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        /// <summary>
        /// Конструктор для работы со списком и сохранением в файловое хранилище.
        /// </summary>
        /// <param name="list">Список сущностей.</param>
        /// <param name="fileStorage">Файловое хранилище.</param>
        public RepositoryFileList(List<TEntity> list, StorageFileBase fileStorage)
        {
            SaveEachOperation = true;
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));
        }

        /// <summary>
        /// Установка списка сущностей.
        /// </summary>
        /// <param name="list">Список сущностей.</param>
        public void SetList(List<TEntity> list)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        private StorageFileBase GetFileStorage()
        {
            if (_fileStorage == null)
            {
                throw new InvalidOperationException("File storage is required for this operation. Use the constructor that accepts StorageFileBase.");
            }

            return _fileStorage;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> Query()
        {
            return _list.AsQueryable();
        }

        /// <inheritdoc/>
        public TEntity? FirstOrDefault(Func<TEntity?, bool>? predicate)
        {
            if (predicate == null)
            {
                if (_list.Count == 0)
                {
                    return default;
                }

                return _list[0];
            }
            else
            {
#pragma warning disable S3267
                foreach (var entity in _list)
                {
                    if (predicate(entity))
                    {
                        return entity;
                    }
                }
            }
#pragma warning restore

            return default;
        }

        /// <inheritdoc/>
        public TEntity? GetById(TKey id)
        {
            return _list.Find(x => x.Equals(id));
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken token = default)
        {
            var result = _list.Find(x => x.Equals(id));
            return await ValueTask.FromResult(result);
        }

        /// <inheritdoc/>
        public IList<TEntity?> GetByIds(IEnumerable<TKey> ids)
        {
            var result = new List<TEntity?>();
            foreach (var id in ids)
            {
                var entity = _list.Find(x => x.Equals(id));
                if (entity is not null)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public async ValueTask<IList<TEntity?>> GetByIdsAsync(IEnumerable<TKey> ids, CancellationToken token = default)
        {
            var result = new List<TEntity?>();
            foreach (var id in ids)
            {
                var entity = _list.Find((entity) => { return entity.Id.Equals(id); });
                if (entity is not null)
                {
                    result.Add(entity);
                }
            }

            return await ValueTask.FromResult(result);
        }

        /// <inheritdoc/>
        public TEntity? GetByName(string name)
        {
            return _list.Find((entity) =>
            {
                if (entity is ILotusNameable nameable)
                {
                    return nameable.Name == name;
                }
                return false;
            });
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity?> GetByNameAsync(string name, CancellationToken token = default)
        {
            var result = _list.Find((entity) =>
            {
                if (entity is ILotusNameable nameable)
                {
                    return nameable.Name == name;
                }
                return false;
            });

            return await ValueTask.FromResult(result);
        }

        /// <inheritdoc/>
        public TEntity GetOrAdd(TKey id)
        {
            var result = _list.Find((entity) => { return entity.Id.Equals(id); });
            if (result == null)
            {
                result = new TEntity
                {
                    Id = id
                };

                _list.Add(result);

                if (SaveEachOperation)
                {
                    GetFileStorage().SaveChanges();
                }
                else
                {
                    GetFileStorage().NeedSaved = true;
                }

                return result;
            }
            else
            {
                return result;
            }
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity> GetOrAddAsync(TKey id, CancellationToken token = default)
        {
            var result = _list.Find((entity) => { return entity.Id.Equals(id); });
            if (result == null)
            {
                result = new TEntity
                {
                    Id = id
                };

                _list.Add(result);

                if (SaveEachOperation)
                {
                    await GetFileStorage().SaveChangesAsync(token);
                }
                else
                {
                    GetFileStorage().NeedSaved = true;
                }

                return await ValueTask.FromResult(result);
            }
            else
            {
                return await ValueTask.FromResult(result);
            }
        }

        /// <inheritdoc/>
        public TEntity Add(TEntity entity)
        {
            _list.Add(entity);

            if (SaveEachOperation)
            {
                GetFileStorage().SaveChanges();
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }

            return entity;
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
        {
            _list.Add(entity);

            if (SaveEachOperation)
            {
                await GetFileStorage().SaveChangesAsync(token);
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }

            return await ValueTask.FromResult(entity);
        }

        /// <inheritdoc/>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _list.AddRange(entities);

            if (SaveEachOperation)
            {
                GetFileStorage().SaveChanges();
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            _list.AddRange(entities);

            if (SaveEachOperation)
            {
                await GetFileStorage().SaveChangesAsync(token);
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }

            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            return entity;
        }

        /// <inheritdoc/>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            _list.Remove(entity);

            if (SaveEachOperation)
            {
                GetFileStorage().SaveChanges();
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }
        }

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _list.Remove(entity);
            }

            if (SaveEachOperation)
            {
                GetFileStorage().SaveChanges();
            }
            else
            {
                GetFileStorage().NeedSaved = true;
            }
        }

        /// <inheritdoc/>
        public void RemoveId(TKey id)
        {
            var entity = _list.Find(x => x.Equals(id));
            if (entity is not null)
            {
                _list.Remove(entity);

                if (SaveEachOperation)
                {
                    GetFileStorage().SaveChanges();
                }
                else
                {
                    GetFileStorage().NeedSaved = true;
                }
            }
        }

        /// <inheritdoc/>
        public void RemoveIdsRange(IEnumerable<TKey> ids)
        {
            var isRemoving = false;
            foreach (var id in ids)
            {
                var entity = _list.Find(x => x.Equals(id));
                if (entity is not null)
                {
                    _list.Remove(entity);
                    isRemoving = true;
                }
            }

            if (isRemoving)
            {
                if (SaveEachOperation)
                {
                    GetFileStorage().SaveChanges();
                }
                else
                {
                    GetFileStorage().NeedSaved = true;
                }
            }
        }

        /// <inheritdoc/>
        public void Flush()
        {
            var storage = GetFileStorage();
            storage.SaveChanges();
            storage.NeedSaved = false;
        }

        /// <inheritdoc/>
        public async Task FlushAsync(CancellationToken token = default)
        {
            var storage = GetFileStorage();
            await storage.SaveChangesAsync(token);
            storage.NeedSaved = false;
        }
    }
    /**@}*/
}