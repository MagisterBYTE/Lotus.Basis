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
    /// Редактор атрибута LotusHeaderGroupAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusHeaderGroupAttribute), true)]
    public class LotusHeaderGroupAttributeDrawer : DecoratorDrawer
    {
        #region Unity events
        /// <summary>
        /// Получение высоты декоративного элемента
        /// </summary>
        /// <returns>Высота декоративного элемента</returns>
        public override float GetHeight()
        {
            return base.GetHeight() + (EditorGUIUtility.standardVerticalSpacing * 2);
        }

        /// <summary>
        /// Рисование параметров декоративного элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        public override void OnGUI(Rect position)
        {
            var groupAttribute = attribute as LotusHeaderGroupAttribute;
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
