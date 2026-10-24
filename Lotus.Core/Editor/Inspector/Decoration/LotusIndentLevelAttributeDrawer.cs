#if UNITY_EDITOR
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusIndentLevelAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusIndentLevelAttribute), true)]
    public class LotusIndentLevelAttributeDrawer : PropertyDrawer
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
            var attr = attribute as LotusIndentLevelAttribute;
            var savedIndent = EditorGUI.indentLevel;

            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.indentLevel = attr.IsAbsolute ? attr.IndentLevel : savedIndent + attr.IndentLevel;
                EditorGUI.PropertyField(position, property, label, property.isExpanded);
                EditorGUI.indentLevel = savedIndent;
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
