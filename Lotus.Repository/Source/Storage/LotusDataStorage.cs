using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Основной интерфейс совмещения концепции хранения данных и универсального доступа для управлениями сущностями хранилища.
    /// </summary>
    public interface ILotusDataStorage : ILotusStorage
    {
        /// <summary>
        /// Получить провайдер данных указанной сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Провайдер данных.</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        TEntity? GetById<TEntity, TKey>(TKey id) where TEntity : class
                                                 where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        ValueTask<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="ids">Список идентифиакторов.</param>
        /// <returns>Список сущностей.</returns>
        IList<TEntity?> GetByIds<TEntity, TKey>(IEnumerable<TKey> ids)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="ids">Список идентифиакторов.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Список сущностей.</returns>
        ValueTask<IList<TEntity?>> GetByIdsAsync<TEntity, TKey>(IEnumerable<TKey> ids, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="name">Имя сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        TEntity? GetByName<TEntity>(string name) where TEntity : class, ILotusNameable;

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="name">Имя сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        ValueTask<TEntity?> GetByNameAsync<TEntity>(string name, CancellationToken token = default)
            where TEntity : class, ILotusNameable;

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// Если сущность отсутствует то она будет создана с указанным идентификатором.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        TEntity GetOrAdd<TEntity, TKey>(TKey id) where TEntity : class, ILotusIdentifierId<TKey>, new()
                                                 where TKey : struct, IEquatable<TKey>;

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
        ValueTask<TEntity> GetOrAddAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Добавленная сущность.</returns>
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Добавленная сущность.</returns>
        ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken token = default)
                where TEntity : class;

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Задача.</returns>
        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// Обновить указанную сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Обновленная сущность.</returns>
        TEntity Update<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Обновить указанные сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary>
        /// Удалить указанную сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entity">Сущность.</param>
        void Remove<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Удалить указанные сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
    }
    /**@}*/
}