namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspector
	*@{*/
    /// <summary>
    /// Интерфейс для определения просмотра общей информации объекта в инспекторе свойств.
    /// </summary>
    public interface ILotusSupportViewInspector
    {
        /// <summary>
        /// Отображаемое имя типа в инспекторе свойств.
        /// </summary>
        string InspectorTypeName { get; }

        /// <summary>
        /// Отображаемое имя объекта в инспекторе свойств.
        /// </summary>
        string InspectorObjectName { get; }
    }

    /// <summary>
    /// Интерфейс для определения расширенной поддержки редактирования объекта в инспекторе свойств.
    /// </summary>
    public interface ILotusSupportEditInspector : ILotusSupportViewInspector
    {
        /// <summary>
        /// Получить массив описателей свойств объекта.
        /// </summary>
        /// <returns>Массив описателей.</returns>
        CPropertyDesc[] GetPropertiesDesc();
    }
    /**@}*/
}