using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
    *@{*/
    /// <summary>
    /// Режим вызова метода.
    /// </summary>
    /// <remarks>
    /// Применяется в основном в Unity.
    /// </remarks>
    public enum TMethodCallMode
    {
        /// <summary>
        /// Метод можно вызвать в любое время.
        /// </summary>
        Always,

        /// <summary>
        /// Метод можно вызвать только в режиме редактора.
        /// </summary>
        OnlyEditor,

        /// <summary>
        /// Метод можно вызвать только в режиме игры.
        /// </summary>
        OnlyPlay
    }

    /// <summary>
    /// Атрибут для определения возможности вызова метода объекта через инспектор свойств.
    /// </summary>
    /// <remarks>
    /// Поддерживается до двух аргументов метода.
    /// Аргументы должны быть сопоставимы с типом <see cref="CVariant"/>.
    /// Отображение вызываемых методов в инспекторе свойств происходит после отображения всех данных
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
    public sealed class LotusMethodCallAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusMethodCallAttribute : Attribute
#endif
    {
        #region Fields
        internal string _displayName;
        internal bool _isSignature;
        internal TMethodCallMode _mode;
        internal int _drawOrder;
        #endregion

        #region Properties
        /// <summary>
        /// Удобочитаемое имя метода.
        /// </summary>
        /// <remarks>
        /// Если пустое значение то используется имя метода.
        /// </remarks>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        /// <summary>
        /// Статус отображения сигнатуры метода вместе с его имением.
        /// </summary>
        public bool IsSignature
        {
            get { return _isSignature; }
            set { _isSignature = value; }
        }

        /// <summary>
        /// Режим вызова метода.
        /// </summary>
        public TMethodCallMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        /// <summary>
        /// Порядок рисования метода.
        /// </summary>
        public int DrawOrder
        {
            get { return _drawOrder; }
            set { _drawOrder = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusMethodCallAttribute()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="humanName">Удобочитаемое имя метода.</param>
        /// <param name="buttonMode">Режим вызова метода.</param>
        public LotusMethodCallAttribute(string humanName, TMethodCallMode buttonMode = TMethodCallMode.Always)
        {
            _displayName = humanName;
            _mode = buttonMode;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="humanName">Удобочитаемое имя метода.</param>
        /// <param name="drawOrder">Порядок отрисовки кнопки.</param>
        /// <param name="buttonMode">Режим вызова метода.</param>
        public LotusMethodCallAttribute(string humanName, int drawOrder, TMethodCallMode buttonMode = TMethodCallMode.Always)
        {
            _displayName = humanName;
            _drawOrder = drawOrder;
            _mode = buttonMode;
        }
        #endregion
    }
    /**@}*/
}