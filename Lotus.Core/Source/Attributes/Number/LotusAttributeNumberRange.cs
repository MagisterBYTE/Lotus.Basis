using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
    *@{*/
    /// <summary>
    /// Атрибут для определения диапазона числовой величины.
    /// </summary>
    /// <remarks>
    /// В зависимости от способа задания, значение распространяется либо на весь тип, либо к каждому экземпляру.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusNumberRangeAttribute : Attribute
    {
        #region Fields
        internal readonly double _minValue;
        internal readonly double _maxValue;
        internal readonly string _memberName;
        internal readonly TInspectorMemberType _memberType;
        #endregion

        #region Properties
        /// <summary>
        /// Минимальное значение величины.
        /// </summary>
        public double MinValue
        {
            get { return _minValue; }
        }

        /// <summary>
        /// Максимальное значение величины.
        /// </summary>
        public double MaxValue
        {
            get { return _maxValue; }
        }

        /// <summary>
        /// Имя члена объекта представляющий случайное значение.
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
        public LotusNumberRangeAttribute(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="memberName">Имя члена объекта представляющий случайное значение.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        public LotusNumberRangeAttribute(string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _memberName = memberName;
            _memberType = memberType;
        }
        #endregion
    }
    /**@}*/
}