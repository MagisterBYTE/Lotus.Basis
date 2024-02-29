using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения максимального значения величины.
    /// </summary>
    /// <remarks>
    /// В зависимости от способа задания значение распространяется либо на весь тип, либо к каждому экземпляру.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusMaxValueAttribute : Attribute
    {
        #region Fields
        internal readonly object _maxValue;
        internal readonly string _memberName;
        internal readonly TInspectorMemberType _memberType;
        #endregion

        #region Properties
        /// <summary>
        /// Максимальное значение величины.
        /// </summary>
        public object MaxValue
        {
            get { return _maxValue; }
        }

        /// <summary>
        /// Имя члена объекта содержащие максимальное значение.
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
        /// <param name="maxValue">Максимальное значение величины.</param>
        public LotusMaxValueAttribute(object maxValue)
        {
            _maxValue = maxValue;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="memberName">Имя члена объекта содержащие максимальное значение.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        public LotusMaxValueAttribute(string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _memberName = memberName;
            _memberType = memberType;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="type">Тип содержащие максимальное значение.</param>
        /// <param name="memberName">Имя члена типа содержащие максимальное значение.</param>
        /// <param name="memberType">Тип члена типа.</param>
        public LotusMaxValueAttribute(Type type, string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _maxValue = type;
            _memberName = memberName;
            _memberType = memberType;
        }
        #endregion
    }
    /**@}*/
}