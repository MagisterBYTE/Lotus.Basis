#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений ресурса GUISkin
    /// </summary>
    public static class XGUISkinExtension
    {
        #region Main methods
        /// <summary>
        /// Добавление произвольного стиля в GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="style">Добавляемый стиль</param>
        public static void AddCustomStyle(this GUISkin @this, GUIStyle style)
        {
            GUIStyle[] styles = @this.customStyles;
            Array.Resize(ref styles, @this.customStyles.Length + 1);
            styles[styles.Length - 1] = style;
            @this.customStyles = styles;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }

        /// <summary>
        /// Добавление произвольного стиля в GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="styleName">Имя стиля</param>
        public static void AddCustomStyle(this GUISkin @this, string styleName)
        {
            var style = new GUIStyle(GUIStyle.none)
            {
                name = styleName
            };

            GUIStyle[] styles = @this.customStyles;
            Array.Resize(ref styles, @this.customStyles.Length + 1);
            styles[styles.Length - 1] = style;
            @this.customStyles = styles;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }

        /// <summary>
        /// Удаление произвольного стиля из GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="style">Удаляемый стиль</param>
        public static void RemoveCustomStyle(this GUISkin @this, GUIStyle style)
        {
            GUIStyle[] styles = @this.customStyles;
            var index = Array.IndexOf(styles, style);
            if (index != -1)
            {
                var listStyle = new List<GUIStyle>(styles);
                listStyle.Remove(style);
                styles = listStyle.ToArray();
            }

            @this.customStyles = styles;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }

        /// <summary>
        /// Дубликат произвольного стиля в GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="style">Дублируемый стиль</param>
        public static void DuplicateCustomStyle(this GUISkin @this, GUIStyle style)
        {
            GUIStyle[] styles = @this.customStyles;
            var index = Array.IndexOf(styles, style);
            if (index != -1)
            {
                var listStyle = new List<GUIStyle>(styles);

                var duplicate = new GUIStyle(style)
                {
                    name = style.name + "(Duplicate)"
                };
                listStyle.Insert(index, duplicate);

                styles = listStyle.ToArray();
                @this.customStyles = styles;
            }
            else
            {
                var duplicate = new GUIStyle(style)
                {
                    name = style.name + "(Duplicate)"
                };
                @this.AddCustomStyle(duplicate);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }

        /// <summary>
        /// Перемещение вверх по списку произвольного стиля в GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="style">Перемещаемый стиль</param>
        public static void MoveCustomStyleUp(this GUISkin @this, GUIStyle style)
        {
            GUIStyle[] styles = @this.customStyles;
            var index = Array.IndexOf(styles, style);
            if (index != -1)
            {
                var listStyle = new List<GUIStyle>(styles);
                listStyle.MoveElementUp(index);
                styles = listStyle.ToArray();
            }

            @this.customStyles = styles;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }

        /// <summary>
        /// Перемещение вниз по списку произвольного стиля в GUI скин
        /// </summary>
        /// <param name="this">GUI скин</param>
        /// <param name="style">Перемещаемый стиль</param>
        public static void MoveCustomStyleDown(this GUISkin @this, GUIStyle style)
        {
            GUIStyle[] styles = @this.customStyles;
            var index = Array.IndexOf(styles, style);
            if (index != -1)
            {
                var listStyle = new List<GUIStyle>(styles);
                listStyle.MoveElementDown(index);
                styles = listStyle.ToArray();
            }

            @this.customStyles = styles;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(@this);
#endif
        }
        #endregion
    }

    /**@}*/
}
#endif
