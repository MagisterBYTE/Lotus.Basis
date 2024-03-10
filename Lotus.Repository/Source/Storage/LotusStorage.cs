using System;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;

namespace Lotus.Repository
{
    /**
     * \defgroup RepositoryStorage Хранилища данных
     * \ingroup Repository
     * \brief Хранилища данных представляет собой совокупность всех доступных репозиториев и определяет место и 
        способ хранения данных.
     * @{
     */
    /// <summary>
    /// Определение интерфейса для реализации хранилища данных (паттерн Unit of Work).
    /// </summary>
    public interface ILotusStorage
    {
        /// <summary>
        /// Данные связвания хранилища с источником.
        /// </summary>
        string ConnectingData { get; set; }

        /// <summary>
        /// Статус указывающий на то что хранилище необходимо сохранить.
        /// </summary>
        bool NeedSaved { get; set; }

        /// <summary>
        /// Структура хранилища
        /// </summary>
        ILotusStorageStructure IStructure { get; }

        /// <summary>
        /// Получить репозиторий указанной сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип ключа сущности.</typeparam>
        /// <returns>Репозиторий или null если указанная сущность не найдена.</returns>
        ILotusRepository<TEntity, TKey>? GetRepository<TEntity, TKey>()
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>;

        /// <summary>
        /// Сохранить все изменения в хранилище.
        /// </summary>
        /// <returns>Количество сохранённых изменений.</returns>
        int SaveChanges();

        /// <summary>
        /// Сохранить все изменения в хранилище.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Количество сохранённых изменений.</returns>
        ValueTask<int> SaveChangesAsync(CancellationToken token = default);
    }
    /**@}*/
}