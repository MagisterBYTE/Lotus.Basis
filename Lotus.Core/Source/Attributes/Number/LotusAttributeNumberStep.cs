using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
    *@{*/
    /// <summary>
    /// Атрибут для определения шага приращения значения.
    /// </summary>
    /// <remarks>
    /// В зависимости от способа задания значение распространяется либо на весь тип, либо к каждому экземпляру.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusStepValueAttribute : Attribute
    {
        #region Fields
        internal readonly object _stepValue;
        internal readonly string _memberName;
        internal readonly TInspectorMemberType _memberType;
        internal string _styleButtonLeftName;
        internal string _styleButtonRightName;
#if UNITY_EDITOR
	internal UnityEngine.GUIStyle _styleButtonLeft;
	internal UnityEngine.GUIStyle _styleButtonRight;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Шаг приращения.
        /// </summary>
        public object StepValue
        {
            get { return _stepValue; }
        }

        /// <summary>
        /// Имя члена объекта представляющий шаг приращения значения.
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

        /// <summary>
        /// Имя визуального стиля для кнопки слева.
        /// </summary>
        public string StyleButtonLeftName
        {
            get { return _styleButtonLeftName; }
            set { _styleButtonLeftName = value; }
        }

        /// <summary>
        /// Имя визуального стиля для кнопки справа.
        /// </summary>
        public string StyleButtonRightName
        {
            get { return _styleButtonRightName; }
            set { _styleButtonRightName = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="stepValue">Шаг приращения.</param>
        public LotusStepValueAttribute(object stepValue)
        {
            _stepValue = stepValue;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="memberName">Имя члена объекта представляющий шаг приращения значения.</param>
        /// <param name="memberType">Тип члена объекта.</param>
        public LotusStepValueAttribute(string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _memberName = memberName;
            _memberType = memberType;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="type">Тип представляющий шаг приращения значения.</param>
        /// <param name="memberName">Имя члена типа представляющий шаг приращения значения.</param>
        /// <param name="memberType">Тип члена типа.</param>
        public LotusStepValueAttribute(Type type, string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
        {
            _stepValue = type;
            _memberName = memberName;
            _memberType = memberType;
        }
        #endregion
    }
    /**@}*/
}