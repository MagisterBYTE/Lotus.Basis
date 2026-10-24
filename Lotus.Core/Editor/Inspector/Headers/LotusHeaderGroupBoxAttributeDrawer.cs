#if UNITY_EDITOR
using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusHeaderGroupBoxAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusHeaderGroupBoxAttribute), true)]
    public class LotusHeaderGroupBoxAttributeDrawer : DecoratorDrawer
    {
        #region Unity events
        /// <summary>
        /// Получение высоты декоративного элемента
        /// </summary>
        /// <returns>Высота декоративного элемента</returns>
        public override float GetHeight()
        {
            return base.GetHeight() + (EditorStyles.helpBox.border.top + EditorStyles.helpBox.border.bottom);
        }

        /// <summary>
        /// Рисование параметров декоративного элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        public override void OnGUI(Rect position)
        {
            var groupAttribute = attribute as LotusHeaderGroupBoxAttribute;
            var style = EditorStyles.boldLabel;

            // Старые параметры
            var oldColor = style.normal.textColor;
            var oldAnchor = style.alignment;

            if (groupAttribute.TextColor == XColors.Zero)
            {
                groupAttribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
            }

            // Новые параметры
            style.normal.textColor = groupAttribute.TextColor;
            style.alignment = XEnumConverter.ToEnum<TextAnchor>(groupAttribute.TextAlignment);

            // Рисуем
            position.x += groupAttribute.Indent * XInspectorViewParams.OFFSET_INDENT;
            position.width -= groupAttribute.Indent * XInspectorViewParams.OFFSET_INDENT;
            position.height -= EditorGUIUtility.standardVerticalSpacing;
            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            if (groupAttribute.TextAlignment == "UpperLeft" ||
                groupAttribute.TextAlignment == "MiddleLeft" ||
                groupAttribute.TextAlignment == "LowerLeft")
            {
                position.x += EditorGUIUtility.standardVerticalSpacing * 2;
            }

            GUI.Label(position, groupAttribute.Name, style);

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
        }
        #endregion
    }
    /**@}*/
}
#endif
