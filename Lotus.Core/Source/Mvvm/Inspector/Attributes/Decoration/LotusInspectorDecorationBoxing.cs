using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения фонового бокса элемента инспектора свойств.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusBoxingAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusBoxingAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly int _offsetLeft;
        internal readonly int _offsetTop;
        internal readonly int _offsetRight;
        internal readonly int _offsetBottom;
        internal string _backgroundStyleName;
        #endregion

        #region Properties
        /// <summary>
        /// Смещение слева.
        /// </summary>
        public int OffsetLeft
        {
            get { return _offsetLeft; }
        }

        /// <summary>
        /// Смещение сверху.
        /// </summary>
        public int OffsetTop
        {
            get { return _offsetTop; }
        }

        /// <summary>
        /// Смещение справа.
        /// </summary>
        public int OffsetRight
        {
            get { return _offsetRight; }
        }

        /// <summary>
        /// Смещение снизу.
        /// </summary>
        public int OffsetBottom
        {
            get { return _offsetBottom; }
        }

        /// <summary>
        /// Имя визуального стиля для фонового поля.
        /// </summary>
        /// <remarks>
        /// При необходимости может использоваться и для элементов.
        /// </remarks>
        public string BackgroundStyleName
        {
            get { return _backgroundStyleName; }
            set { _backgroundStyleName = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="offsetLeft">Красная компонента цвета.</param>
        /// <param name="offsetTop">Зеленая компонента цвета.</param>
        /// <param name="offsetRight">Синяя компонента цвета.</param>
        /// <param name="offsetBottom">Альфа компонента цвета.</param>
        public LotusBoxingAttribute(int offsetLeft, int offsetTop = 0, int offsetRight = 0,
                int offsetBottom = 0)
        {
            _offsetLeft = offsetLeft;
            _offsetTop = offsetTop;
            _offsetRight = offsetRight;
            _offsetBottom = offsetBottom;
        }
        #endregion
    }
    /**@}*/
}