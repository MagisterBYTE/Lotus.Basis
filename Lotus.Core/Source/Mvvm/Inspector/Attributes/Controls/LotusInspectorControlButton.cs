using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут реализующий отображение кнопки рядом с полем/свойством для вызова метода.
    /// </summary>
    /// <remarks>
    /// Если метод принимает аргумент то он должен быть того же типа как и тип поля/свойства.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusButtonAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusButtonAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly string _methodName;
        internal string _label;
        internal bool _inputArgument;
        #endregion

        #region Properties
        /// <summary>
        /// Имя метода.
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
        }

        /// <summary>
        /// Надпись на кнопке.
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        /// <summary>
        /// Статус получения аргумента.
        /// </summary>
        public bool InputArgument
        {
            get { return _inputArgument; }
            set { _inputArgument = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <param name="label">Надпись на кнопке.</param>
        public LotusButtonAttribute(string methodName, string label = "D")
        {
            _methodName = methodName;
        }
        #endregion
    }
    /**@}*/
}