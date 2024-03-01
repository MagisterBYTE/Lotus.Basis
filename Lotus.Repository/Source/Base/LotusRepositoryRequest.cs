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

        #region Main methods
        /// <summary>
        /// Получение списка целочисленных значений указанного свойства.
        /// </summary>
        /// <param name="propertName">Свойство.</param>
        /// <returns>Список целочисленных значений и функция фильтрации.</returns>
        public (int[], TFilterFunction) GetIdsByInteger(string propertName)
        {
#pragma warning disable S6602 // "Find" method should be used instead of the "FirstOrDefault" extension
            var filterProperty = Filtering?.FirstOrDefault(x => x.PropertyName == propertName);
#pragma warning restore S6602 // "Find" method should be used instead of the "FirstOrDefault" extension
            if (filterProperty != null && filterProperty.Values != null && filterProperty.Values.Length > 0)
            {
                var ids = new int[filterProperty.Values.Length];
                for (var i = 0; i < filterProperty.Values.Length; i++)
                {
                    ids[i] = XNumbers.ParseInt(filterProperty.Values[i]);
                }
                return (ids, filterProperty.Function);
            }

            return (Array.Empty<int>(), TFilterFunction.Equals);
        }
        #endregion
    }
    /**@}*/
}