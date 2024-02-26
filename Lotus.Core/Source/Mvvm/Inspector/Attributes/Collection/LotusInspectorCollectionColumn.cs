using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения колонки отображения данных дочернего поля/свойства объекта.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class LotusColumnAttribute : Attribute
    {
        #region Fields
        internal readonly float _percent;
        #endregion

        #region Properties
        /// <summary>
        /// Процент занимаемой ширины.
        /// </summary>
        public float Percent
        {
            get { return _percent; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="percent">Процент занимаемой ширины.</param>
        public LotusColumnAttribute(float percent)
        {
            _percent = percent;
        }
        #endregion
    }
    /**@}*/
}