namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Тип свойства сущности.
    /// </summary>
    /// <remarks>
    /// Тип свойства сущности может быть любым типом, однако в целях унификации фильтрации и доступа для ряда 
    /// наиболее востребованных типов есть специальные методы 
    /// </remarks>
    public enum TEntityPropertyType
    {
        /// <summary>
        /// Логический тип.
        /// </summary>
        Boolean,

        /// <summary>
        /// Целый тип.
        /// </summary>
        Integer,

        /// <summary>
        /// Вещественный тип.
        /// </summary>
        Float,

        /// <summary>
        /// Строковый тип.
        /// </summary>
        String,

        /// <summary>
        /// Перечисление.
        /// </summary>
        Enum,

        /// <summary>
        /// Тип даты-времени.
        /// </summary>
        DateTime,

        /// <summary>
        /// Глобальный идентификатор в формате UUID.
        /// </summary>
        Guid,

        /// <summary>
        /// Объект.
        /// </summary>
        Object
    }
    /**@}*/
}