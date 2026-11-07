using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Core
{
    /** \addtogroup CoreStorage
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
        /// Получить провайдер данных указанной сущности с включением навигаций.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="include">Конфигурация Include (например <c>q => q.Include(u => u.Role)</c>).</param>
        /// <returns>Провайдер данных.</returns>
        IQueryable<TEntity> Query<TEntity>(Action<ILotusIncludeBuilder<TEntity>> include) where TEntity : class;

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
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Список сущностей.</returns>
        IList<TEntity?> GetByIds<TEntity, TKey>(IEnumerable<TKey> ids)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="ids">Список идентификаторов.</param>
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
        /// Добавляем или обновляет сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Созданная или обновленная сущность.</returns>
        TEntity AddOrUpdate<TEntity, TKey>(TEntity entity)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>;

        /// <summary>
        /// Добавляем или обновляет сущность.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Созданная или обновленная сущность.</returns>
        ValueTask<TEntity> AddOrUpdateAsync<TEntity, TKey>(TEntity entity, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>;

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

        /// <summary>
        /// Синхронизирует коллекцию связанных объектов. Добавляет новые объекты (есть в новой коллекции и отсутствует в старой) 
        /// и удаляет старые (есть в старой коллекции и отсутствует в новой).
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип идентификатора.</typeparam>
        /// <param name="entities">Список сущностей.</param>
        void SyncRelatedCollections<TEntity, TKey>(IEnumerable<TEntity> entities)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>;

        #region IQueryable
        /// <summary>
        /// Получение единственного элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Единственный элемент или null.</returns>
        Task<TEntity?> SingleOrDefaultAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение единственного элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="predicate">Предикат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Единственный элемент или null.</returns>
        Task<TEntity?> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение единственного элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Единственный элемент или null.</returns>
        Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение единственного элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="predicate">Предикат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Единственный элемент или null.</returns>
        Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение первого элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Первый элемент или null.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение первого элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="predicate">Предикат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Первый элемент или null.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение первого элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Первый элемент или null.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;

        /// <summary>
        /// Получение первого элемента по запросу.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="predicate">Предикат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Первый элемент или null.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class;
        #endregion

        #region Paging
        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="request">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
            where TEntity : class
            where TResponse : class;

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="request">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
            where TEntity : class
            where TResponse : class;

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, CancellationToken token = default)
            where TEntity : class
            where TResponse : class;

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, CancellationToken token = default)
            where TEntity : class;
        #endregion
    }
    /**@}*/
}