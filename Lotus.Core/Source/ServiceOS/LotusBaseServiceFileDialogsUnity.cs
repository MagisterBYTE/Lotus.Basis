#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /** \addtogroup CoreServiceOS
    *@{*/
    /// <summary>
    /// Реализации диалогов открытия/сохранения файлов и директории для Unity
    /// </summary>
    public class FileDialogsUnity : ILotusFileDialogs
    {
        /// <summary>
        /// Показ диалога для открытия файла
        /// </summary>
        /// <param name="title">Заголовок диалога</param>
        /// <param name="directory">Директория для открытия файла</param>
        /// <param name="extension">Расширение файла без точки или список расширений или null</param>
        /// <returns>Полное имя существующего файла или null</returns>
        public string Open(string title, string directory, string extension)
        {
#if UNITY_EDITOR
            return (UnityEditor.EditorUtility.OpenFilePanel(title, directory, extension));
#else
            return("");
#endif

        }

        /// <summary>
        /// Показ диалога для сохранения файла
        /// </summary>
        /// <param name="title">Заголовок диалога</param>
        /// <param name="directory">Директория для сохранения файла</param>
        /// <param name="defaultName">Имя файла по умолчанию</param>
        /// <param name="extension">Расширение файла без точки</param>
        /// <returns>Полное имя файла или null</returns>
        public string Save(string title, string directory, string defaultName, string extension)
        {
#if UNITY_EDITOR
            return (UnityEditor.EditorUtility.SaveFilePanel(title, directory, defaultName, extension));
#else
            return("");
#endif
        }
    }

    /**@}*/
}
#endif