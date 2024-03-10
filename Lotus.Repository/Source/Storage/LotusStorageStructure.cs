using System.Linq;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Определение интерфейса для реализации структуры хранилища данных.
    /// </summary>
    public interface ILotusStorageStructure
    {
        /// <summary>
        /// Получить провайдер указанной сущности.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Провайдер указанной сущности или null если указанная сущность не найдена.</returns>
        IQueryable<TEntity>? GetEntities<TEntity>() where TEntity : class, new();
    }
    /**@}*/
}