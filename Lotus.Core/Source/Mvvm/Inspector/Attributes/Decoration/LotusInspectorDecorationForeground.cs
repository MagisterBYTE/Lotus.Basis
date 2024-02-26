using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения цвета текста элемента инспектора свойств.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusForegroundAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusForegroundAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly TColor _foreground;
        #endregion

        #region Properties
        /// <summary>
        /// Цвет текста.
        /// </summary>
        public TColor Foreground
        {
            get { return _foreground; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="red">Красная компонента цвета.</param>
        /// <param name="green">Зеленая компонента цвета.</param>
        /// <param name="blue">Синяя компонента цвета.</param>
        /// <param name="alpha">Альфа компонента цвета.</param>
        public LotusForegroundAttribute(byte red, byte green, byte blue, byte alpha = 255)
        {
            _foreground = new TColor(red, green, blue, alpha);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="colorBgra">Цвет в формате BGRA.</param>
        public LotusForegroundAttribute(uint colorBgra)
        {
            _foreground = TColor.FromBGRA(colorBgra);
        }
        #endregion
    }
    /**@}*/
}