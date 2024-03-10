using System.Collections.Generic;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Определение интерфейса для структуры физического файла хранилища.
    /// </summary>
    public interface ILotusFileStorageStructure : ILotusStorageStructure
    {
        /// <summary>
        /// Получить список сущностей указанного типа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Список сущностей.</returns>
        List<TEntity> GetEntitiesList<TEntity>();
    }
    /**@}*/
}