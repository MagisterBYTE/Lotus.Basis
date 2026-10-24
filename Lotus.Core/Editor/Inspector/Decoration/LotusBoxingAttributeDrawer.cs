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
    /// Редактор атрибута LotusBoxingAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusBoxingAttribute), true)]
    public class LotusBoxingAttributeDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Получение высоты редактируемого свойства элемента
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        /// <returns>Высота свойства элемента</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }

        /// <summary>
        /// Рисование сериализуемого свойства
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusBoxingAttribute;

            EditorGUI.BeginProperty(position, label, property);
            {
                var indent = EditorGUI.IndentedRect(position);
                if (attr.OffsetLeft < 0)
                {
                    indent.xMin += attr.OffsetLeft;
                }
                if (attr.OffsetRight < 0)
                {
                    indent.xMax -= attr.OffsetRight;
                }
                if (attr.OffsetTop < 0)
                {
                    indent.yMin += attr.OffsetTop;
                }
                if (attr.OffsetBottom < 0)
                {
                    indent.yMax -= attr.OffsetBottom;
                }

                GUI.Box(indent, GUIContent.none, XEditorStyles.FindStyle(attr.BackgroundStyleName) ?? GUIStyle.none);

                // Центрируем для красоты
                position.y += (Mathf.Abs(attr.OffsetBottom) + Mathf.Abs(attr.OffsetTop)) / 2;
                position.x += 2;
                position.width -= 4;

                EditorGUI.PropertyField(position, property, label, property.isExpanded);
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
