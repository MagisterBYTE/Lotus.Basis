using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения уровня смещения отображения элемента инспектора свойств.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusIndentLevelAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusIndentLevelAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly int _indentLevel;
        internal readonly bool _isAbsolute = false;
        #endregion

        #region Properties
        /// <summary>
        /// Уровень смещения.
        /// </summary>
        public int IndentLevel
        {
            get { return _indentLevel; }
        }

        /// <summary>
        /// Статус абсолютного смещения.
        /// </summary>
        public bool IsAbsolute
        {
            get { return _isAbsolute; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="indentLevel">Уровень смещения.</param>
        /// <param name="isAbsolute">Статус абсолютного смещения.</param>
        public LotusIndentLevelAttribute(int indentLevel, bool isAbsolute = true)
        {
            _indentLevel = indentLevel;
            _isAbsolute = isAbsolute;
        }
        #endregion
    }
    /**@}*/
}