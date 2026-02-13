using System;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryFilter
	*@{*/
    /// <summary>
    /// Базовый интерфейс для фильтрации данных.
    /// </summary>
    public interface ILotusFilterObject
    {
    }

    /// <summary>
    /// Универсальный класс для фильтрации данных.
    /// </summary>
    public class FilterObject : ILotusFilterObject
    {
        #region Properties
        /// <summary>
        /// Фильтры по свойствам.
        /// </summary>
        public FilterByProperty[] Filters { get; set; } = default!;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public FilterObject()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="filters">Список фильтров.</param>
        public FilterObject(params FilterByProperty[]? filters)
        {
            Filters = filters ?? Array.Empty<FilterByProperty>();
        }
        #endregion
    }
    /**@}*/
}