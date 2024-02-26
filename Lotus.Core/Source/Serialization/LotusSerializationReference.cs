using System.Reflection;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Вспомогательный класс хранящий данные для связывания поля/свойства типа ссылочного объекта.
    /// </summary>
    /// <remarks>
    /// При чтении ссылки на объект мы пытаемся найти объект по его сохраненным параметрам, однако может быть такая
    /// ситуация что этот объект еще не создан, и тогда мы должны повторить это процесс после загрузки всех объектов.
    /// </remarks>
    public class CSerializeReference
    {
        #region Fields
        /// <summary>
        /// Код типа объекта ссылки.
        /// </summary>
        public int CodeObject;

        /// <summary>
        /// Метаданные поля/свойства.
        /// </summary>
        public MemberInfo Member;

        /// <summary>
        /// Индекс элемента для коллекций.
        /// </summary>
        public int Index;

        /// <summary>
        /// Экземпляр объекта которому принадлежит поле/свойство.
        /// </summary>
        public object Instance;

        /// <summary>
        /// Идентификатор объекта ссылки.
        /// </summary>
        public int ID;

        /// <summary>
        /// Имя типа объекта ссылки.
        /// </summary>
        public string TypeObject;

        /// <summary>
        /// Найденный объект.
        /// </summary>
        public object Result;
        #endregion
    }
    /**@}*/
}