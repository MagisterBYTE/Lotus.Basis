using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Core
{
    /** \addtogroup CoreInterfaces
	*@{*/
    /// <summary>
    /// Определение основного интерфейса репозитория данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public interface ILotusRepository<TEntity, TKey>
        where TEntity : class, ILotusIdentifierId<TKey>, new()
        where TKey : notnull, IEquatable<TKey>
    {
        /// <summary>
        /// Выполнять сохранение после каждой операции
        /// </summary>
        /// <remarks>
        /// По умолчанию данный режим включен 
        /// </remarks>
        bool SaveEachOperation { get; set; }

        /// <summary>
        /// Получить провайдер данных указанной сущности.
        /// </summary>
        /// <returns>Провайдер данных.</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Получить первую сущность или значение по умолчанию удовлетворяющий указанному предикату.
        /// </summary>
        /// <param name="predicate">Предикат.</param>
        /// <returns>Первая сущность или значение по умолчанию.</returns>
        TEntity? FirstOrDefault(Func<TEntity?, bool>? predicate);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        TEntity? GetById(TKey id);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken token = default);

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <param name="ids">Список идентифиакторов.</param>
        /// <returns>Список сущностей.</returns>
        IList<TEntity?> GetByIds(IEnumerable<TKey> ids);

        /// <summary>
        /// Производит поиск сущностей в БД или в хранилище по идентификаторам.
        /// </summary>
        /// <param name="ids">Список идентифиакторов.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Список сущностей.</returns>
        ValueTask<IList<TEntity?>> GetByIdsAsync(IEnumerable<TKey> ids, CancellationToken token = default);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <param name="name">Имя сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        TEntity? GetByName(string name);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по ее имени.
        /// </summary>
        /// <param name="name">Имя сущности.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или null.</returns>
        ValueTask<TEntity?> GetByNameAsync(string name, CancellationToken token = default);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// Если сущность отсутствует то она будет создана с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или созданная сущность.</returns>
        TEntity GetOrAdd(TKey id);

        /// <summary>
        /// Производит поиск сущности в БД или в хранилище по идентификатору.
        /// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
        /// Если сущность отсутствует то она будет создана с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор сущност.и.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Найденная сущность или или созданная сущность.</returns>
        ValueTask<TEntity> GetOrAddAsync(TKey id, CancellationToken token = default);

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Добавленная сущность.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Добавленная сущность.</returns>
        ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken token = default);

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <param name="entities">Список сущностей.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добавить список сущностей.
        /// </summary>
        /// <param name="entities">Список сущностей.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Задача.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default);

        /// <summary>
        /// Обновить указанную сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Обновленная сущность.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Обновить указанные сущности.
        /// </summary>
        /// <param name="entities">Список сущностей.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удалить указанную сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Удалить указанные сущности.
        /// </summary>
        /// <param name="entities">Список сущностей.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удалить указанную сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        void RemoveId(TKey id);

        /// <summary>
        /// Удалить указанные сущности по их идентификаторам.
        /// </summary>
        /// <param name="ids">Список идентификаторов сущностей.</param>
        void RemoveIdsRange(IEnumerable<TKey> ids);

        /// <summary>
        /// Сохранить в хранилище все изменения.
        /// </summary>
        void Flush();

        /// <summary>
        /// Сохранить в хранилище все изменения.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Задача.</returns>
        Task FlushAsync(CancellationToken token = default);
    }
    /**@}*/
}