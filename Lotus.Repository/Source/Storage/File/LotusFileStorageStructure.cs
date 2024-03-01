using System.Collections.Generic;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryStorage
    *@{*/
    /// <summary>
    /// Структура файла хранилища.
    /// </summary>
    public interface ILotusFileStorageStructure
    {
        /// <summary>
        /// Получить список сущностей указанного типа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Список сущностей.</returns>
        List<TEntity> GetEntities<TEntity>();
    }
    /**@}*/
}