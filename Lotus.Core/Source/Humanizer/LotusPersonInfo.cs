namespace Lotus.Core
{
    /**
     * \defgroup CoreHumanizer Подсистема гуманизации текста
     * \ingroup Core
     * \brief Вспомогательная подсистема для преобразования некоторых данных в человеческий формат.
     * @{
     */
    /// <summary>
    /// Интерфейс для определения персональных данных пользователя.
    /// </summary>
    public interface ILotusPersonInfo
    {
        #region Properties
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string? Patronymic { get; set; }
        #endregion
    }
    /**@}*/
}