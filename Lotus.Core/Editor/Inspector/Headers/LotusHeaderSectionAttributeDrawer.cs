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
    /// Редактор атрибута LotusHeaderSectionAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusHeaderSectionAttribute), true)]
    public class LotusHeaderSectionAttributeDrawer : DecoratorDrawer
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
            var sectionAttribute = attribute as LotusHeaderSectionAttribute;
            var style = EditorStyles.boldLabel;

            // Старые параметры
            var oldColor = style.normal.textColor;
            var oldAnchor = style.alignment;
            style.fontSize++;

            if (sectionAttribute.TextColor == XColors.Zero)
            {
                sectionAttribute.TextColor = new TColor(XEditorStyles.ColorHeaderSection);
            }

            // Новые параметры
            style.normal.textColor = sectionAttribute.TextColor;
            style.alignment = XEnumConverter.ToEnum<TextAnchor>(sectionAttribute.TextAlignment);

            // Рисуем
            GUI.Label(position, sectionAttribute.Name, style);

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
            style.fontSize--;
        }
        #endregion
    }
    /**@}*/
}
#endif
