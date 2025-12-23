namespace Lotus.Core
{
    /** \addtogroup CoreHumanizer
    *@{*/
    /// <summary>
    /// Форматы имени для метода GetFormattedName.
    /// </summary>
    public enum TPersonNameFormat
    {
        /// <summary>
        /// Краткий формат: "Фамилия И. О."
        /// </summary>
        Short,

        /// <summary>
        /// Полный формат: "Фамилия Имя Отчество"
        /// </summary>
        Full,

        /// <summary>
        /// Отображаемое имя: "Имя Фамилия"
        /// </summary>
        Display,

        /// <summary>
        /// Инициалы: "ИИ"
        /// </summary>
        Initials
    }
    /**@}*/
}