namespace Lotus.Core.Inspector
{
    /**
     * \defgroup CoreInspector Подсистема поддержки инспектора свойств
     * \ingroup Core
     * \brief Подсистема поддержки инспектора свойств обеспечивает расширенное описание и управление свойствами/полями объекта.
     * \details Инспектор свойств (или инспектор объектов) представляет собой элемент управления, который позволяет управлять
		объектом посредством изменения его свойств (и не только свойств).

		При этом этот элемент управления используется как в режиме разработки приложения, так и может использоваться
		в готовом приложении.
		
		Данная подсистема прежде всего направлена на расширение возможностей инспектора свойств Unity и инспектора свойств Lotus.
		Поддержка стандартного инспектора свойств IDE при разработке обычных приложений не предусмотрена.
     * @{
     */
    /// <summary>
    /// Базовый интерфейса для реализации пользовательского инспектора свойств.
    /// </summary>
    public interface ILotusPropertyInspector
    {
        /// <summary>
        /// Выбранный объект.
        /// </summary>
        object SelectedObject { get; set; }
    }
    /**@}*/
}