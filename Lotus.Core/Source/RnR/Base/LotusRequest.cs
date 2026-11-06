using System.Linq;

namespace Lotus.Core
{
    /**
     * \defgroup CoreRequestAndResponse Подсистема запроса и ответа
     * \ingroup Core
     * \brief Подсистема запроса и ответа определяет однотипный унифицированный набор типов для 
        получения фильтрованных/сортированных/сгруппированных данных с разделением на страницы и 
        формирования соответствующих запросов.
     * @{
     */
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
    public class Request : ILotusRequest
    {
        #region Properties
        /// <summary>
        /// Параметры запрашиваемой страницы.
        /// </summary>
        public PageInfoRequest? PageInfo { get; set; }

        /// <summary>
        /// Параметры запрашиваемой страницы.
        /// </summary>
        ILotusPageInfoRequest? ILotusRequest.PageInfo
        {
            get => PageInfo;
            set => PageInfo = (PageInfoRequest?)value;
        }

        /// <summary>
        /// Параметры сортировки данных.
        /// </summary>
        public SortByProperty[]? Sorting { get; set; }

        /// <summary>
        /// Параметры сортировки данных.
        /// </summary>
        ILotusSortProperty[]? ILotusRequest.Sorting
        {
            get => Sorting;
            // Каст массива (вызовет ошибку, если передан массив другого типа)
            set => Sorting = value?.Cast<SortByProperty>().ToArray();
        }

        /// <summary>
        /// Параметры фильтрации данных.
        /// </summary>
        public FilterByProperty[]? Filtering { get; set; }

        /// <summary>
        /// Параметры фильтрации данных.
        /// </summary>
        ILotusFilterProperty[]? ILotusRequest.Filtering
        {
            get => Filtering;
            set => Filtering = value?.Cast<FilterByProperty>().ToArray();
        }
        #endregion
    }
    /**@}*/
}