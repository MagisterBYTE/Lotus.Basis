using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут видимости(отображения поля/свойства)в зависимости от логического условия равенства.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusVisibleEqualityAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusVisibleEqualityAttribute : Attribute
#endif
    {
        #region Fields
        internal string _managingMemberName;
        internal TInspectorMemberType _memberType;
        internal bool _value;
        #endregion

        #region Properties
        /// <summary>
        /// Имя члена объекта от которого определяется видимость.
        /// </summary>
        public string ManagingMemberName
        {
            get { return _managingMemberName; }
            set { _managingMemberName = value; }
        }

        /// <summary>
        /// Тип члена объекта.
        /// </summary>
        public TInspectorMemberType MemberType
        {
            get { return _memberType; }
            set { _memberType = value; }
        }

        /// <summary>
        /// Целевое значение поля/свойства при котором существует видимость.
        /// </summary>
        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="managingMemberName">Имя члена объекта от которого определяется видимость.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        /// <param name="value">Целевое значение поля/свойства при котором существует видимость.</param>
        public LotusVisibleEqualityAttribute(string managingMemberName, TInspectorMemberType memberType, bool value)
        {
            _managingMemberName = managingMemberName;
            _memberType = memberType;
            _value = value;
        }
        #endregion
    }
    /**@}*/
}