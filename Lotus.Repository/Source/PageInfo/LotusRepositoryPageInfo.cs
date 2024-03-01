namespace Lotus.Repository
{
    /**
     * \defgroup RepositoryPageInfo Подсистема постраничного разделения
     * \ingroup Repository
     * \brief Подсистема постраничного разделения определяет однотипный унифицированный набор типов для 
		постраничного разделения данных и формирования соответствующих запросов.
     * @{
     */
    /// <summary>
    /// Интерфейса для постраничного запроса данных.
    /// </summary>
    public interface ILotusPageInfoRequest
    {
        /// <summary>
        /// Номер старницы, отсчет от нуля.
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        int PageSize { get; set; }
    }

    /// <summary>
    /// Класс для постраничного запроса данных.
    /// </summary>
    public class PageInfoRequest : ILotusPageInfoRequest
    {
        /// <summary>
        /// Номер старницы, отсчет от нуля.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Интерфейс для постраничного получения данных.
    /// </summary>
    public interface ILotusPageInfoResponse
    {
        /// <summary>
        /// Номер старницы, отсчет от нуля.
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Количество данных на текущей странице.
        /// </summary>
        int CurrentPageSize { get; set; }

        /// <summary>
        /// Общие количество данных по данному запросу.
        /// </summary>
        int TotalCount { get; set; }
    }

    /// <summary>
    /// Класс для постраничного получения данных.
    /// </summary>
    public class PageInfoResponse : ILotusPageInfoResponse
    {
        /// <summary>
        /// Номер старницы, отсчет от нуля.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Количество данных на текущей странице.
        /// </summary>
        public int CurrentPageSize { get; set; }

        /// <summary>
        /// Общие количество данных по данному запросу.
        /// </summary>
        public int TotalCount { get; set; }
    }
    /**@}*/
}