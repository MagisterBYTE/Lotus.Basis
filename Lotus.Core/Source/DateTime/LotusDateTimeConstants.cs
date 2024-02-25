namespace Lotus.Core
{
    /** \addtogroup CoreDateTime
    *@{*/
    /// <summary>
    /// Статический класс для форматов даты.
    /// </summary>
    public static class XDateFormats
    {
        /// <summary>
        /// Формат даты по умолчанию.
        /// </summary>
        public const string Default = "dd.MM.yyyy";

        /// <summary>
        /// Формат даты по GraphQL.
        /// </summary>
        public const string History = "MM/dd/yyyy";
    }

    /// <summary>
    /// Статический класс для форматов времени.
    /// </summary>
    public static class XTimeFormats
    {
        /// <summary>
        /// Формат времени по умолчанию.
        /// </summary>
        public const string Default = "HH:mm";
    }
    /**@}*/
}