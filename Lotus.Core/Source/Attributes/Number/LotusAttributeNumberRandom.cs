using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
    *@{*/
    /// <summary>
    /// Атрибут для определения диапазона величины при генерировании случайных значений.
    /// </summary>
    /// <remarks>
    /// В зависимости от способа задания значение распространяется либо на весь тип, либо к каждому экземпляру.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusRandomValueAttribute : Attribute
    {
        #region Fields
        internal readonly object _minValue;
        internal readonly object _maxValue;
        internal readonly string _memberName;
        internal readonly TInspectorMemberType _memberType;
        #endregion

        #region Properties
        /// <summary>
        /// Минимальное значение величины.
        /// </summary>
        public object MinValue
        {
            get { return _minValue; }
        }

        /// <summary>
        /// Максимальное значение величины.
        /// </summary>
        public object MaxValue
        {
            get { return _maxValue; }
        }

        /// <summary>
        /// Имя члена объекта представляющее случайное значение.
        /// </summary>
        public string MemberName
        {
            get { return _memberName; }
        }

        /// <summary>
        /// Тип члена объекта.
        /// </summary>
        public TInspectorMemberType MemberType
        {
            get { return _memberType; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="minValue">Минимальное значение величины.</param>
        /// <param name="maxValue">Максимальное значение величины.</param>
        public LotusRandomValueAttribute(object minValue, object maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="memberName">Имя члена объекта представляющую случайное значение.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        public LotusRandomValueAttribute(string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _memberName = memberName;
            _memberType = memberType;
        }
        #endregion
    }
    /**@}*/
}