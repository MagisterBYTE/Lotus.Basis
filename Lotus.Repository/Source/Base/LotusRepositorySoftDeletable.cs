using System;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Интерфейс для определения мягкого удаления.
    /// </summary>
    public interface ILotusRepositorySoftDeletable
    {
        /// <summary>
        /// Дата удаления сущности.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }
    /**@}*/
}