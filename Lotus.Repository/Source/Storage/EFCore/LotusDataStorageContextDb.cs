using System;
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
    /// Класс представляющий собой хранилища данных для EF Core.
    /// </summary>
    public class DataStorageContextDb<TContext> : StorageBaseContextDb<TContext>, ILotusDataStorage where TContext : ContextDbStorageStructure
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public DataStorageContextDb(TContext context)
            : base(context)
        {
        }
        #endregion

        #region ILotusDataStorage methods
        /// <summary>
        /// Получить провайдер данных указанной сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Провайдер данных.</returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        public TEntity? GetById<TEntity, TKey>(TKey id) where TEntity : class
                                                        where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = id;
            return _context.Find<TEntity>(_ids);
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        public async ValueTask<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = id;
            return await _context.FindAsync<TEntity>(_ids, token);
        }

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Список сущностей.</returns>
        public IList<TEntity?> GetByIds<TEntity, TKey>(IEnumerable<TKey> ids)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>
        {
            return _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToArray();
        }

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="ids">Список идентификаторов.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Список сущностей.</returns>
        public async ValueTask<IList<TEntity?>> GetByIdsAsync<TEntity, TKey>(IEnumerable<TKey> ids, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>
        {
            return await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToArrayAsync(token);
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="name">Имя сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        public TEntity? GetByName<TEntity>(string name) where TEntity : class, ILotusNameable
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(x => x.Name == name);
            return entity;
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="name">Имя сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        public async ValueTask<TEntity?> GetByNameAsync<TEntity>(string name, CancellationToken token = default)
            where TEntity : class, ILotusNameable
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Name == name, token);
            return entity;
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// Если сущность отсутствует то она будет создана с указанным идентификатором.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        public TEntity GetOrAdd<TEntity, TKey>(TKey id) where TEntity : class, ILotusIdentifierId<TKey>, new()
                                                 where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = id;
            var entity = _context.Find<TEntity>(_ids);
            if (entity is null)
            {
                entity = new TEntity
                {
                    Id = id
                };

                var entry = _context.Add(entity);
                return entry.Entity;
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// Если сущность отсутствует то она будет создана с указанным идентификатором.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        public async ValueTask<TEntity> GetOrAddAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = id;
            var entity = await _context.FindAsync<TEntity>(_ids, token);
            if (entity is null)
            {
                entity = new TEntity
                {
                    Id = id
                };

                var entry = await _context.AddAsync(entity, token);
                return entry.Entity;
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Добавленная сущность.</returns>
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);
            var entry = _context.Add(entity);
            return entry.Entity;
        }

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Добавленная сущность.</returns>
        public async ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken token = default)
                where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);
            var entry = await _context.AddAsync(entity, token);
            return entry.Entity;
        }

        /// <summary>
        /// Добавляем или обновляет сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Созданная или обновленная сущность.</returns>
        public TEntity AddOrUpdate<TEntity, TKey>(TEntity entity)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = entity.Id;
            var existEntity = _context.Find<TEntity>(_ids);
            if (existEntity is null)
            {
                // Добавляем
                var entry = _context.Add(entity);
                return entry.Entity;
            }
            else
            {
                // Обновляем
                // Копируем значения из пришедшей сущности в найденную в базе
                _context.Entry(existEntity).CurrentValues.SetValues(entity);
                return existEntity;
            }
        }

        /// <summary>
        /// Добавляем или обновляет сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Созданная или обновленная сущность.</returns>
        public async ValueTask<TEntity> AddOrUpdateAsync<TEntity, TKey>(TEntity entity, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            _ids[0] = entity.Id;
            var existEntity = await _context.FindAsync<TEntity>(_ids, token);
            if (existEntity is null)
            {
                // Добавляем
                var entry = await _context.AddAsync(entity, token);
                return entry.Entity;
            }
            else
            {
                // Обновляем
                // Копируем значения из пришедшей сущности в найденную в базе
                _context.Entry(existEntity).CurrentValues.SetValues(entity);
                return existEntity;
            }
        }

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entities);
            _context.AddRange(entities);
        }

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Задача.</returns>
        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entities);
            await _context.AddRangeAsync(entities, token);
        }

        /// <summary>
        /// Обновить указанную сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Обновленная сущность.</returns>
        public TEntity Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);
            return _context.Update(entity).Entity;
        }

        /// <summary>
        /// Обновить указанные сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entities);
            _context.UpdateRange(entities);
        }

        /// <summary>
        /// Удалить указанную сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        public void Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Remove(entity);
        }

        /// <summary>
        /// Удалить указанные сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entities);
            _context.RemoveRange(entities);
        }

        /// <summary>
        /// Синхронизирует коллекцию связанных объектов. Добавляет новые объекты (есть в новой коллекции и отсутствует в старой) 
        /// и удаляет старые (есть в старой коллекции и отсутствует в новой).
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        public void SyncRelatedCollections<TEntity, TKey>(IEnumerable<TEntity> entities)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            // 1. Получаем ID всех входящих сущностей для быстрого поиска
            var newEntities = entities.ToList();
            var newIds = newEntities.Select(x => x.Id).ToHashSet();

            // 2. Получаем текущие сущности из БД (Local + Database)
            // ВАЖНО: Если это коллекция конкретного владельца, здесь должен быть фильтр по Foreign Key!
            var dbSet = _context.Set<TEntity>();
            var trackedEntities = dbSet.Local.ToList();

            // Если Local пуст или не полон, подтягиваем из базы те, что не в Local, но имеют те же ID или принадлежат контексту
            // Но чаще всего в Sync передают уже отфильтрованный список "старых" данных.
            // Предположим, мы работаем со всем набором этого типа:
            var oldEntities = dbSet.ToList();

            // 3. Удаляем те, которых нет в новом списке
            foreach (var oldItem in oldEntities)
            {
                if (!newIds.Contains(oldItem.Id))
                {
                    dbSet.Remove(oldItem);
                }
            }

            // 4. Добавляем новые или обновляем существующие
            foreach (var newItem in newEntities)
            {
                var existing = oldEntities.FirstOrDefault(x => x.Id.Equals(newItem.Id));

                if (existing == null)
                {
                    // Полностью новый объект
                    dbSet.Add(newItem);
                }
                else
                {
                    // Объект уже есть — обновляем его значения из пришедшей модели
                    // Это решает проблему "another instance with the same key is already being tracked"
                    _context.Entry(existing).CurrentValues.SetValues(newItem);
                }
            }
        }

        /// <summary>
        /// Сохранить в хранилище все изменения.
        /// </summary>
        public void Flush()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Сохранить в хранилище все изменения.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Задача.</returns>
        public async Task FlushAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
        #endregion
    }
    /**@}*/
}