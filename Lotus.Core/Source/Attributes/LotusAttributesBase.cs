namespace Lotus.Core
{
    /** 
	 * \defgroup CoreAttribute Подсистема атрибутов
	 * \ingroup Core
	 * \brief Подсистема атрибутов содержит необходимые атрибуты для расширения функциональности и добавления новых
		характеристик к свойствам, методам и классам.
	 * @{
	 */
    /// <summary>
    /// Тип члена объекта для атрибутов поддержки инспектора свойств.
    /// </summary>
    public enum TInspectorMemberType
    {
        /// <summary>
        /// Поле.
        /// </summary>
        Field,

        /// <summary>
        /// Свойство.
        /// </summary>
        Property,

        /// <summary>
        /// Метод.
        /// </summary>
        Method
    }
    /**@}*/
}