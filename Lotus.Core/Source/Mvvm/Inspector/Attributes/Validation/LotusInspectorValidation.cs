using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут подтверждения правильности (валидации) данных.
    /// </summary>
    /// <remarks>
    /// Для Unity, в случае не прохождения валидации данных, отображается бокс с информацией.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusValidationAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusValidationAttribute : Attribute
#endif
    {
        #region Fields
        internal string _validationMethodName;
        internal string _message = "Data has not been validated";
        internal TLogType _messageType;
        #endregion

        #region Properties
        /// <summary>
        /// Имя метода который осуществляет проверку на валидацию данных.
        /// </summary>
        /// <remarks>
        /// Метод должен иметь один аргумент соответствующего типа и возвращать true, если данные проходят 
        /// валидацию и false в противном случае 
        /// </remarks>
        public string ValidationMethodName
        {
            get { return _validationMethodName; }
            set { _validationMethodName = value; }
        }

        /// <summary>
        /// Сообщение которое отображается если данные не прошли валидацию.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Тип сообщения.
        /// </summary>
        public TLogType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="validationMethod">Имя метода который осуществляет проверку на валидацию данных.</param>
        /// <param name="messageType">Тип сообщения.</param>
        public LotusValidationAttribute(string validationMethod, TLogType messageType = TLogType.Error)
        {
            _validationMethodName = validationMethod;
            _messageType = messageType;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="validationMethod">Имя метода который осуществляет проверку на валидацию данных.</param>
        /// <param name="message">Сообщение которое отображается если данные не прошли валидацию.</param>
        /// <param name="messageType">Тип сообщения.</param>
        public LotusValidationAttribute(string validationMethod, string message, TLogType messageType = TLogType.Error)
        {
            _validationMethodName = validationMethod;
            _message = message;
            _messageType = messageType;
        }
        #endregion

    }
    /**@}*/
}