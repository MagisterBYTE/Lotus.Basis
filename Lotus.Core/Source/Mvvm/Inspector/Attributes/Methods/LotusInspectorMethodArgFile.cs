using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
    *@{*/
    /// <summary>
    /// Атрибут для вызова метода с возможностью указания для аргумента имени файла.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
    public sealed class LotusMethodArgFileAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusMethodArgFileAttribute : Attribute
#endif
    {
        #region Fields
        internal string _extension;
        internal string _defaultName;
        internal string _defaultPath;
        internal bool _isOpenFile;
        #endregion

        #region Properties
        /// <summary>
        /// Расширение файла.
        /// </summary>
        /// <remarks>
        /// Расширение файла задается без точки.
        /// </remarks>
        public string Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        /// <summary>
        /// Имя файла по умолчанию.
        /// </summary>
        /// <remarks>
        /// Стандартное значение - File.
        /// </remarks>
        public string DefaultName
        {
            get { return _defaultName; }
            set { _defaultName = value; }
        }

        /// <summary>
        /// Путь файла по умолчанию.
        /// </summary>
        /// <remarks>
        /// Стандартное значение - Assets/.
        /// </remarks>
        public string DefaultPath
        {
            get { return _defaultPath; }
            set { _defaultPath = value; }
        }

        /// <summary>
        /// Статус открытия файла (Показывается диалог для открытия файла).
        /// </summary>
        /// <remarks>
        /// В случае отрицательного значения будет показываться диалог закрытия файлов.
        /// </remarks>
        public bool IsOpenFile
        {
            get { return _isOpenFile; }
            set { _isOpenFile = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusMethodArgFileAttribute()
        {
            _defaultName = "File";
#if UNITY_2017_1_OR_NEWER
		    _defaultPath = XCoreSettings.ASSETS_PATH;
#else
            _defaultPath = "";
#endif

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="extension">Расширение файла.</param>
        /// <param name="defaultName">Имя файла по умолчанию.</param>
        /// <param name="defaultPath">Путь файла по умолчанию.</param>
        public LotusMethodArgFileAttribute(string extension, string defaultName = "File",
            string defaultPath =
#if UNITY_2017_1_OR_NEWER
		    XCoreSettings.ASSETS_PATH)
#else
            "")
#endif
        {
            _extension = extension;
            _defaultPath = defaultPath;
        }
        #endregion
    }
    /**@}*/
}