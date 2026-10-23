#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе со шрифтами
    /// </summary>
    public static class XFont
    {
        #region Constants
        /// <summary>
        /// Имя стандартного шрифта (Unity 2019.3+ / Unity 6)
        /// </summary>
        public const string DEFAULT_NAME = "LegacyRuntime";

        /// <summary>
        /// Путь для загрузки стандартного шрифта (замена устаревшего Arial.ttf)
        /// </summary>
        public const string DEFAULT_PATH = "LegacyRuntime.ttf";
        #endregion

        #region Fields
        private static Font _default;
        #endregion

        #region Properties
        /// <summary>
        /// Стандартный шрифт
        /// </summary>
        public static Font Default
        {
            get
            {
                if (_default == null)
                {
                    LoadDefault();
                }

                return _default;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Загрузка шрифтов по умолчанию
        /// </summary>
        internal static void LoadDefault()
        {
            _default = Resources.GetBuiltinResource<Font>(DEFAULT_PATH);
        }
        #endregion
    }

    /**@}*/
}
#endif
