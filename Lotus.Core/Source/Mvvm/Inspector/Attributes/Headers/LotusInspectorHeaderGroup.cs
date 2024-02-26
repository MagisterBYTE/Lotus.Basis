using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут декоративной отрисовки заголовка группы.
    /// </summary>
    /// <remarks>
    /// Реализация декоративной атрибута отрисовки заголовка группы c возможностью задать выравнивания и цвет текста заголовка.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusHeaderGroupAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusHeaderGroupAttribute : Attribute
#endif
    {
        #region Fields
        internal string _name;
        internal TColor _textColor;
        internal string _textAlignment = "MiddleLeft";
        internal int _indent;
        #endregion

        #region Properties
        /// <summary>
        /// Имя заголовка.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Цвет текста заголовка.
        /// </summary>
        public TColor TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        /// <summary>
        /// Выравнивание текста заголовка.
        /// </summary>
        public string TextAlignment
        {
            get { return _textAlignment; }
            set { _textAlignment = value; }
        }

        /// <summary>
        /// Уровень смещения.
        /// </summary>
        public int Indent
        {
            get { return _indent; }
            set { _indent = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusHeaderGroupAttribute()
        {
            _name = "";
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя заголовка.</param>
        public LotusHeaderGroupAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя заголовка.</param>
        /// <param name="indent">Уровень смещения.</param>
        public LotusHeaderGroupAttribute(string name, int indent)
        {
            _name = name;
            _indent = indent;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя заголовка.</param>
        /// <param name="colorBGRA">Цвет текста заголовка.</param>
        /// <param name="textAlignment">Выравнивание текста заголовка.</param>
        public LotusHeaderGroupAttribute(string name, uint colorBGRA, string textAlignment = "MiddleLeft")
        {
            _name = name;
            _textColor = TColor.FromBGRA(colorBGRA);
            _textAlignment = textAlignment;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя заголовка.</param>
        /// <param name="colorBGRA">Цвет текста заголовка.</param>
        /// <param name="ord">Порядок отображения свойства.</param>
        /// <param name="textAlignment">Выравнивание текста заголовка.</param>
        public LotusHeaderGroupAttribute(string name, uint colorBGRA, int ord, string textAlignment = "MiddleLeft")
        {
            _name = name;
            _textColor = TColor.FromBGRA(colorBGRA);
            _textAlignment = textAlignment;
#if UNITY_2017_1_OR_NEWER
			order = ord;
#endif
        }
        #endregion
    }
    /**@}*/
}