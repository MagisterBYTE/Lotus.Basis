using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения фонового цвета элемента инспектора свойств.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusBackgroundAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusBackgroundAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly TColor _background;
        #endregion

        #region Properties
        /// <summary>
        /// Фоновый цвет.
        /// </summary>
        public TColor Background
        {
            get { return _background; }
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
        public LotusBackgroundAttribute(byte red, byte green, byte blue, byte alpha = 255)
        {
            _background = new TColor(red, green, blue, alpha);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="colorBgra">Цвет в формате BGRA.</param>
        public LotusBackgroundAttribute(uint colorBgra)
        {
            _background = TColor.FromBGRA(colorBgra);
        }
        #endregion
    }
    /**@}*/
}