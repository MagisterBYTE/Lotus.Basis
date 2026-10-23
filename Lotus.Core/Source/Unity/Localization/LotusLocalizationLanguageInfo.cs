#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
using UnityEngine.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityLocalization
    *@{*/
    /// <summary>
    /// Структура для предоставления языка локализации
    /// </summary>
    [Serializable]
    public class CLanguageInfo
    {
        #region Constants
        /// <summary>
        /// Имена основных языков
        /// </summary>
        public static readonly string[] LanguageNames = new string[]
        {
            "English",
            "German",
            "French",
            "Italian",
            "Dutch",
            "Greek",
            "Polish",
            "Portuguese",
            "Spanish",
            "Russian",
            "Turkish",
            "Chinese",
        };

        /// <summary>
        /// Аббревиатуры основных языков
        /// </summary>
        public static readonly string[] LanguageAbbrs = new string[]
        {
            "en",
            "de",
            "fr",
            "it",
            "nl",
            "el",
            "pl",
            "pt",
            "es",
            "ru",
            "tr",
            "zh",
        };
        #endregion

        #region Fields
        // Основные параметры
        [SerializeField]
        [FormerlySerializedAs("mName")]
        internal string _name;
        [SerializeField]
        [FormerlySerializedAs("mAbbreviation")]
        internal string _abbreviation;
        [SerializeField]
        [FormerlySerializedAs("mFileData")]
        internal TextAsset _fileData;
        #endregion

        #region Properties
        /// <summary>
        /// Имя языка
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Аббревиатура языка
        /// </summary>
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }

        /// <summary>
        /// Файл с данными
        /// </summary>
        public TextAsset FileData
        {
            get { return _fileData; }
            set { _fileData = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CLanguageInfo()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="name">Имя языка</param>
        /// <param name="abbreviation">Аббревиатура языка</param>
        public CLanguageInfo(string name, string abbreviation)
        {
            _name = name;
            _abbreviation = abbreviation;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению
        /// </summary>
        /// <returns>Текстовое представление</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion
    }

    /**@}*/
}
#endif
