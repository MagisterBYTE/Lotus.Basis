using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryEFCore
    *@{*/
    /// <summary>
    /// Реализация репозитория <see cref="ILotusRepository{TEntity, TKey}"/> через <see cref="DbSet{T}"/>.
    /// </summary>
    public class RepositoryDbSet<TEntity, TKey> : ILotusRepository<TEntity, TKey>
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
    {
        private readonly object[] _ids = new object[1];

        protected internal DbSet<TEntity> _list;
        protected internal DbContext _context;

        public RepositoryDbSet(DbContext context)
        {
            _list = context.Set<TEntity>();
            _context = context;
        }

        /// <summary>
        /// Установка списка сущностей.
        /// </summary>
        /// <param name="list">Список сущностей.</param>
        public void SetList(DbSet<TEntity> list)
        {
            _list = list;
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
                if (_list.Any() == false)
                {
                    return default;
                }

                return _list.First();
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
            _ids[0] = id;
            return _list.Find(_ids);
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken token = default)
        {
            _ids[0] = id;
            var result = _list.FindAsync(_ids, token);
            return await result;
        }

        /// <inheritdoc/>
        public IList<TEntity?> GetByIds(IEnumerable<TKey> ids)
        {
            var result = new List<TEntity?>();
            foreach (var id in ids)
            {
                _ids[0] = id;
                var entity = _list.Find(_ids);
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
                _ids[0] = id;
                var entity = await _list.FindAsync(_ids, token);
                if (entity is not null)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public TEntity? GetByName(string name)
        {
            return _list.Cast<ILotusNameable>().FirstOrDefault((entity) => entity.Name == name) as TEntity;
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity?> GetByNameAsync(string name, CancellationToken token = default)
        {
            return await _list.Cast<ILotusNameable>().FirstOrDefaultAsync((entity) => entity.Name == name, token) as TEntity;
        }

        /// <inheritdoc/>
        public TEntity GetOrAdd(TKey id)
        {
            _ids[0] = id;
            var entity = _list.Find(_ids);
            if (entity == null)
            {
                entity = new TEntity
                {
                    Id = id
                };

                var entry = _list.Add(entity);
                return entry.Entity;
            }
            else
            {
                return entity;
            }
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity> GetOrAddAsync(TKey id, CancellationToken token = default)
        {
            _ids[0] = id;
            var entity = await _list.FindAsync(_ids, token);
            if (entity == null)
            {
                entity = new TEntity
                {
                    Id = id
                };

                var entry = await _list.AddAsync(entity, token);
                return entry.Entity;
            }
            else
            {
                return entity;
            }
        }

        /// <inheritdoc/>
        public TEntity Add(TEntity entity)
        {
            var entry = _list.Add(entity);
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
        {
            var entry = await _list.AddAsync(entity, token);
            return entry.Entity;
        }

        /// <inheritdoc/>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _list.AddRange(entities);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            await _list.AddRangeAsync(entities, token);
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            _context.ChangeTracker.Clear();
            return _list.Update(entity).Entity;
        }

        /// <inheritdoc/>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.ChangeTracker.Clear();
            _list.UpdateRange(entities);
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            _context.ChangeTracker.Clear();
            _list.Remove(entity);
        }

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.ChangeTracker.Clear();
            _list.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public void Flush()
        {
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task FlushAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
    }
    /**@}*/
}