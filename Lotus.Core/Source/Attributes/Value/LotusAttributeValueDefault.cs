using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения свойства/поля у которого есть значение по умолчанию.
    /// </summary>
    /// <remarks>
    /// В зависимости от способа задания, значение распространяется либо на весь тип, либо к каждому экземпляру.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusDefaultValueAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusDefaultValueAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly object _defaultValue;
        internal readonly string _memberName;
        internal readonly TInspectorMemberType _memberType;
        #endregion

        #region Properties
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        public object DefaultValue
        {
            get { return _defaultValue; }
        }

        /// <summary>
        /// Имя члена объекта содержащие значение по умолчанию.
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
        /// <param name="defaultValue">Значение по умолчанию.</param>
        public LotusDefaultValueAttribute(object defaultValue)
        {
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="memberName">Имя члена объекта содержащий значение по умолчанию.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        public LotusDefaultValueAttribute(string memberName, TInspectorMemberType memberType)
        {
            _memberName = memberName;
            _memberType = memberType;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="type">Тип представляющий шаг содержащий значение по умолчанию.</param>
        /// <param name="memberName">Имя члена типа содержащий значение по умолчанию.</param>
        /// <param name="memberType">Тип члена типа.</param>
        public LotusDefaultValueAttribute(Type type, string memberName, TInspectorMemberType memberType)
        {
            _defaultValue = type;
            _memberName = memberName;
            _memberType = memberType;
        }
        #endregion
    }
    /**@}*/
}