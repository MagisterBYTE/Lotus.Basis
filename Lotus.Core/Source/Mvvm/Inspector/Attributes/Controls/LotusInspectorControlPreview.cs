using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для возможности предпросмотра объекта.
    /// </summary>
    /// <remarks>
    /// Только в режиме разработке.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusPreviewAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusPreviewAttribute : Attribute
#endif
    {
        #region Fields
        internal float _previewHeight;
        #endregion

        #region Properties
        /// <summary>
        /// Высота области предпросмотра.
        /// </summary>
        public float PreviewHeight
        {
            get { return _previewHeight; }
            set { _previewHeight = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusPreviewAttribute()
        {
            _previewHeight = 200;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="previewHeight">Высота области предпросмотра.</param>
        public LotusPreviewAttribute(float previewHeight)
        {
            _previewHeight = previewHeight;
        }
        #endregion
    }
    /**@}*/
}