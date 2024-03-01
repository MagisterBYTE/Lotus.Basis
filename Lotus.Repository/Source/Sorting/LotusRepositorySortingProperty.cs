namespace Lotus.Repository
{
    /**
     * \defgroup RepositorySorting Подсистема сортировки
     * \ingroup Repository
     * \brief Подсистема сортировки определяет однотипный унифицированный набор типов для сортировки данных.
     * @{
     */
    /// <summary>
    /// Интерфейс для определения сортировки по одному свойству/полю объекта.
    /// </summary>
    public interface ILotusSortProperty
    {
        /// <summary>
        /// Имя свойства/поля по которому идет сортировка.
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// Статус сортировки по убыванию.
        /// </summary>
        /// <remarks>
        /// По умолчанию, сортировка всегда идет по возрастанию.
        /// </remarks>
        bool? IsDesc { get; set; }
    }

    /// <summary>
    /// Класс для определения сортировки по одному свойству/полю объекта.
    /// </summary>
    public class SortByProperty : ILotusSortProperty
    {
        /// <summary>
        /// Имя свойства/поля по которому идет сортировка.
        /// </summary>
        public string PropertyName { get; set; } = default!;

        /// <summary>
        /// Статус сортировки по убыванию.
        /// </summary>
        /// <remarks>
        /// По умолчанию, сортировка всегда идет по возрастанию.
        /// </remarks>
        public bool? IsDesc { get; set; }
    }
    /**@}*/
}