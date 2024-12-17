using System;
using System.Linq;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Базовый интерфейс запроса данных.
    /// </summary>
    public interface ILotusRequest
    {
        /// <summary>
        /// Информация о странице.
        /// </summary>
        ILotusPageInfoRequest? PageInfo { get; set; }

        /// <summary>
        /// Параметры сортировки данных.
        /// </summary>
        ILotusSortProperty[]? Sorting { get; set; }

        /// <summary>
        /// Параметры фильтрации данных.
        /// </summary>
        ILotusFilterProperty[]? Filtering { get; set; }
    }

    /// <summary>
    /// Базовый класс для запроса данных.
    /// </summary>
    public class Request
    {
        #region Properties
        /// <summary>
        /// Параметры запрашиваемой страницы.
        /// </summary>
        public PageInfoRequest? PageInfo { get; set; }

        /// <summary>
        /// Параметры сортировки данных.
        /// </summary>
        public SortByProperty[]? Sorting { get; set; }

        /// <summary>
        /// Параметры фильтрации данных.
        /// </summary>
        public FilterByProperty[]? Filtering { get; set; }
        #endregion
    }
    /**@}*/
}