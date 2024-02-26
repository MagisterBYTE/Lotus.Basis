using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения логической группы элементов инспектора свойств.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusGroupingAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusGroupingAttribute : Attribute
#endif
    {
        #region Fields
        internal string _groupName;
        internal TColor _headerColor;
        internal TColor _background;
        internal string _backgroundStyleName;
        #endregion

        #region Properties
        /// <summary>
        /// Имя группы.
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        /// <summary>
        /// Цвет надписи.
        /// </summary>
        public TColor HeaderColor
        {
            get { return _headerColor; }
            set
            {
                _headerColor = value;
            }
        }

        /// <summary>
        /// Фоновый цвет.
        /// </summary>
        public TColor Background
        {
            get { return _background; }
            set
            {
                _background = value;
            }
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
        /// <param name="groupName">Имя группы.</param>
        public LotusGroupingAttribute(string groupName)
        {
            _groupName = groupName;
            _background = XColors.White;
            _headerColor = XColors.White;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="groupName">Имя группы.</param>
        /// <param name="headerColorBgra">Цвет надписи в формате BGRA.</param>
        public LotusGroupingAttribute(string groupName, uint headerColorBgra)
        {
            _groupName = groupName;
            _background = XColors.White;
            _headerColor = TColor.FromBGRA(headerColorBgra);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="groupName">Имя группы.</param>
        /// <param name="headerColorBgra">Цвет надписи в формате BGRA.</param>
        /// <param name="backgroundBgra">Фоновый цвет в формате BGRA.</param>
        public LotusGroupingAttribute(string groupName, uint headerColorBgra, uint backgroundBgra)
        {
            _groupName = groupName;
            _background = TColor.FromBGRA(backgroundBgra);
            _headerColor = TColor.FromBGRA(headerColorBgra);
        }
        #endregion
    }
    /**@}*/
}