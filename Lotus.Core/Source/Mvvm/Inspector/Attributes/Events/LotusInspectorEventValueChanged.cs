using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут информирующий об изменении значения поля/свойства объекта.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusEventValueChangedAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusEventValueChangedAttribute : Attribute
#endif
    {
        #region Fields
        internal string _eventMethodName;
        #endregion

        #region Properties
        /// <summary>
        /// Имя метода который будет вызван при изменении значения.
        /// </summary>
        /// <remarks>
        /// Метод должен быть без аргументов.
        /// </remarks>
        public string EventMethodName
        {
            get { return _eventMethodName; }
            set { _eventMethodName = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="eventMethodName">Имя метода который будет вызван при изменении значения.</param>
        public LotusEventValueChangedAttribute(string eventMethodName)
        {
            _eventMethodName = eventMethodName;
        }
        #endregion
    }
    /**@}*/
}