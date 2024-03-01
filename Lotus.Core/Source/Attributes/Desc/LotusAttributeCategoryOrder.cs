using System;
namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения порядка отображения группы свойств/полей.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusCategoryOrderAttribute : Attribute
    {
        #region Fields
        internal readonly int _order;
        #endregion

        #region Properties
        /// <summary>
        /// Порядок отображения группы свойств.
        /// </summary>
        public int Order
        {
            get { return _order; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="order">Порядок отображения группы свойств.</param>
        public LotusCategoryOrderAttribute(int order)
        {
            _order = order;
        }
        #endregion
    }
    /**@}*/
}