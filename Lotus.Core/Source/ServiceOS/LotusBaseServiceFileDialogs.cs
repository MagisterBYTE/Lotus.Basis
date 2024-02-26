using System;

namespace Lotus.Core
{
    /** \addtogroup CoreServiceOS
	*@{*/
    /// <summary>
    /// Интерфейс сервиса для диалогового окна открытия/сохранения файлов.
    /// </summary>
    public interface ILotusFileDialogs
    {
        /// <summary>
        /// Показ диалога для открытия файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для открытия файла.</param>
        /// <param name="extension">Расширение файла без точки или список расширений или null.</param>
        /// <returns>Полное имя существующего файла или null.</returns>
        string? Open(string title, string directory, string? extension);

        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для сохранения файла.</param>
        /// <param name="defaultName">Имя файла по умолчанию.</param>
        /// <param name="extension">Расширение файла без точки.</param>
        /// <returns>Полное имя файла или null.</returns>
        string? Save(string title, string directory, string defaultName, string? extension);
    }

    /// <summary>
    /// Статический класс реализующий диалоговые окна открытия/сохранения файлов.
    /// </summary>
    public static class XFileDialog
    {
        #region Const
        /// <summary>
        /// Файл был успешно сохранен.
        /// </summary>
        public const string FILE_SAVE_SUCCESSFULLY = "The file has been successfully saved";

        /// <summary>
        /// Файл был успешно загружен.
        /// </summary>
        public const string FILE_LOAD_SUCCESSFULLY = "The file has been successfully loaded";

        /// <summary>
        /// Фильтр для текстовых файлов.
        /// </summary>
        public const string TXT_FILTER = "Text files (*.txt)|*.txt";

        /// <summary>
        /// Фильтр для XML файлов.
        /// </summary>
        public const string XML_FILTER = "XML files (*.xml)|*.xml";

        /// <summary>
        /// Фильтр для JSON файлов.
        /// </summary>
        public const string JSON_FILTER = "JSON files (*.json)|*.json";

        /// <summary>
        /// Фильтр для файлов Lua скриптов.
        /// </summary>
        public const string LUA_FILTER = "LUA files (*.lua)|*.lua";

        /// <summary>
        /// Фильтр для стандартного расширения файлов с бинарными данными.
        /// </summary>
        public const string BIN_FILTER = "Binary files (*.bin)|*.bin";

        /// <summary>
        /// Фильтр для расширения файлов с бинарными данными для TextAsset.
        /// </summary>
        public const string BYTES_FILTER = "Binary files (*.bytes)|*.bytes";

        /// <summary>
        /// Фильтр для расширения файлов формата Wavefront.
        /// </summary>
        public const string D3_OBJ_FILTER = "Wavefront file (*.obj)|*.obj";

        /// <summary>
        /// Фильтр для расширения файлов формата COLLADA.
        /// </summary>
        public const string D3_DAE_FILTER = "COLLADA file (*.dae)|*.dae";

        /// <summary>
        /// Фильтр для расширения файлов формата Autodesk 3ds Max 3D.
        /// </summary>
        public const string D3_3DS_FILTER = " Autodesk 3ds Max 3D file (*.3ds)|*.3ds";

        /// <summary>
        /// Фильтр для расширения файлов формата Stereolithography file.
        /// </summary>
        public const string D3_STL_FILTER = "Stereolithography file (*.stl)|*.stl";
        #endregion

        #region Fields
        //
        // БАЗОВЫЕ ПУТИ
        //
#if UNITY_2017_1_OR_NEWER
#if UNITY_EDITOR
		/// <summary>
		/// Путь по умолчанию для сохранения файлов.
		/// </summary>
		public static String DefaultPath = XCoreSettings.ASSETS_PATH;
#else
		/// <summary>
		/// Путь по умолчанию для сохранения файлов.
		/// </summary>
		public static readonly String DefaultPath = UnityEngine.Application.persistentDataPath;
#endif
#else
        /// <summary>
        /// Путь по умолчанию для сохранения файлов.
        /// </summary>
        public static readonly string DefaultPath = Environment.CurrentDirectory;
#endif
        //
        // БАЗОВЫЕ РАСШИРЕНИЯ
        //
        /// <summary>
        /// Расширение файла по умолчанию.
        /// </summary>
        public static string DefaultExt = XFileExtension.XML;

        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Реализация сервиса для диалогового окна открытия/сохранения файлов.
		/// </summary>
		public static ILotusFileDialogs FileDialogs = new CFileDialogsUnity();
#else
        /// <summary>
        /// Реализация сервиса для диалогового окна открытия/сохранения файлов.
        /// </summary>
        public static ILotusFileDialogs FileDialogs;
#endif
        #endregion

        #region Open methods
        /// <summary>
        /// Показ диалога для открытия файла.
        /// </summary>
        /// <returns>Полное имя существующего файла или null.</returns>
        public static string? Open()
        {
            return FileDialogs.Open("Открыть файл", DefaultPath, DefaultExt);
        }

        /// <summary>
        /// Показ диалога для открытия файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <returns>Полное имя существующего файла или null.</returns>
        public static string? Open(string title)
        {
            return FileDialogs.Open(title, DefaultPath, DefaultExt);
        }

        /// <summary>
        /// Показ диалога для открытия файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для открытия файла.</param>
        /// <returns>Полное имя существующего файла или null.</returns>
        public static string? Open(string title, string directory)
        {
            return FileDialogs.Open(title, directory, DefaultExt);
        }

        /// <summary>
        /// Показ диалога для открытия файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для открытия файла.</param>
        /// <param name="extension">Расширение файла без точки.</param>
        /// <returns>Полное имя существующего файла или null.</returns>
        public static string? Open(string title, string directory, string? extension)
        {
            return FileDialogs.Open(title, directory, extension);
        }
        #endregion

        #region Save methods
        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <returns>Полное имя файла или null.</returns>
        public static string? Save()
        {
            return FileDialogs.Save("Сохранить файл", DefaultPath, "Новый файл", DefaultExt);
        }

        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <returns>Полное имя файла или null.</returns>
        public static string? Save(string title)
        {
            return FileDialogs.Save(title, DefaultPath, "Новый файл", DefaultExt);
        }

        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для сохранения файла.</param>
        /// <returns>Полное имя файла или null.</returns>
        public static string? Save(string title, string directory)
        {
            return FileDialogs.Save(title, directory, "Новый файл", DefaultExt);
        }

        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для сохранения файла.</param>
        /// <param name="defaultName">Имя файла по умолчанию.</param>
        /// <returns>Полное имя файла или null.</returns>
        public static string? Save(string title, string directory, string defaultName)
        {
            return FileDialogs.Save(title, directory, defaultName, DefaultExt);
        }

        /// <summary>
        /// Показ диалога для сохранения файла.
        /// </summary>
        /// <param name="title">Заголовок диалога.</param>
        /// <param name="directory">Директория для сохранения файла.</param>
        /// <param name="defaultName">Имя файла по умолчанию.</param>
        /// <param name="extension">Расширение файла без точки.</param>
        /// <returns>Полное имя файла или null.</returns>
        public static string? Save(string title, string directory, string defaultName, string? extension)
        {
            return FileDialogs.Save(title, directory, defaultName, extension);
        }
        #endregion
    }
    /**@}*/
}