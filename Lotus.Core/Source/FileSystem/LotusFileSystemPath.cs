using System.IO;

namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы для работы с расширениями файлов.
    /// </summary>
    /// <remarks>
    /// Типовые расширения файлов приведены без точек и с точками в начале.
    /// </remarks>
    public static class XFileExtension
    {
        #region Typical file extensions 
        /// <summary>
        /// Расширение текстового файла.
        /// </summary>
        public const string TXT = "txt";

        /// <summary>
        /// Расширение текстового файла с начальной точкой.
        /// </summary>
        public const string TXT_D = ".txt";

        /// <summary>
        /// Расширение XML файла.
        /// </summary>
        public const string XML = "xml";

        /// <summary>
        /// Расширение XML файла с начальной точкой.
        /// </summary>
        public const string XML_D = ".xml";

        /// <summary>
        /// Расширение JSON файла.
        /// </summary>
        public const string JSON = "json";

        /// <summary>
        /// Расширение JSON файла с начальной точкой.
        /// </summary>
        public const string JSON_D = ".json";

        /// <summary>
        /// Расширение файла Lua скрипта.
        /// </summary>
        public const string LUA = "lua";

        /// <summary>
        /// Расширение файла Lua скрипта с начальной точкой.
        /// </summary>
        public const string LUA_D = ".lua";

        /// <summary>
        /// Стандартное расширение файла с бинарными данными.
        /// </summary>
        public const string BIN = "bin";

        /// <summary>
        /// Стандартное расширение файла с бинарными данными с начальной точкой.
        /// </summary>
        public const string BIN_D = ".bin";

        /// <summary>
        /// Расширение файла с бинарными данными для TextAsset.
        /// </summary>
        public const string BYTES = "bytes";

        /// <summary>
        /// Расширение файла с бинарными данными для TextAsset с начальной точкой.
        /// </summary>
        public const string BYTES_D = ".bytes";
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка расширения имени файла на принадлежность к текстовым данным.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsTextFileName(string fileName)
        {
            var exe = Path.GetExtension(fileName).ToLower();
            if (exe == TXT_D)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка расширения имени файла на принадлежность к бинарным данным.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsBinaryFileName(string fileName)
        {
            var exe = Path.GetExtension(fileName).ToLower();
            if (exe == BIN_D || exe == BYTES_D)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка расширения имени файла на принадлежность к формату XML.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsXmlFileName(string fileName)
        {
            var exe = Path.GetExtension(fileName).ToLower();
            if (exe == XML_D)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка расширения имени файла на принадлежность к формату JSON.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsJSONFileName(string fileName)
        {
            var exe = Path.GetExtension(fileName).ToLower();
            if (exe == JSON_D)
            {
                return true;
            }

            return false;
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы для работы с путями, имена файлов и директорий в файловой системе.
    /// </summary>
    public static class XFilePath
    {
        #region Main methods
        /// <summary>
        /// Получение имени файла доступного для загрузки.
        /// </summary>
        /// <remarks>
        /// Метод анализирует имя файла и при необходимости добавляет путь и расширение.
        /// </remarks>
        /// <param name="path">Путь.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="ext">Расширение файла.</param>
        /// <returns>Имя файла доступного для загрузки.</returns>
        public static string GetFileName(string path, string fileName, string ext)
        {
#if UNITY_2017_1_OR_NEWER
			fileName = fileName.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
            fileName = fileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

            var result = "";
            if (fileName.IndexOf(Path.DirectorySeparatorChar) > -1 || fileName.IndexOf(Path.AltDirectorySeparatorChar) > -1)
            {
#if UNITY_2017_1_OR_NEWER
				if(fileName.Contains(XCoreSettings.ASSETS_PATH))
				{
					result = fileName;
				}
				else
				{
					result = Path.Combine(path, fileName);
				}
#else
                result = fileName;
#endif
            }
            else
            {
                result = Path.Combine(path, fileName);
            }

            if (Path.HasExtension(result) == false)
            {
                if (ext[0] != XCharHelper.Dot)
                {
                    result = result + XCharHelper.Dot + ext;
                }
                else
                {
                    result = result + ext;
                }
            }

#if UNITY_2017_1_OR_NEWER
			result = result.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
            result = result.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

            return result;
        }

        /// <summary>
        /// Получение полного пути файла с новым именем.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <param name="newFileName">Новое имя файла.</param>
        /// <returns>Путь с новым именем файла.</returns>
        public static string GetPathForRenameFile(string path, string newFileName)
        {
            var dir = Path.GetDirectoryName(path);
            var exe = Path.GetExtension(path);

            var result = "";
            if (Path.HasExtension(newFileName))
            {
                result = Path.Combine(dir ?? string.Empty, newFileName);
            }
            else
            {
                result = Path.Combine(dir ?? string.Empty, newFileName + exe);
            }

#if UNITY_2017_1_OR_NEWER
			result = result.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
            result = result.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

            return result;
        }

        /// <summary>
        /// Проверка на корректность имени файла для сохранения.
        /// </summary>
        /// <remarks>
        /// Имя файла корректно, если оно не пустое, длиной меньше 240 символов и не содержит запрещающих символов.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Статус корректности имени файла.</returns>
        public static bool CheckCorrectFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (fileName.Length > 240)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
    /**@}*/
}