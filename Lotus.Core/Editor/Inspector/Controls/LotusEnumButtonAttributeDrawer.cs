#if UNITY_EDITOR
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusEnumButtonAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusEnumButtonAttribute), true)]
    public class LotusEnumButtonAttributeDrawer : PropertyDrawer
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
            EditorGUI.BeginProperty(position, label, property);
            {
                var indent = EditorGUI.IndentedRect(position);
                var rectLabel = new Rect(indent.x, indent.y, EditorGUIUtility.labelWidth, indent.height);
                EditorGUI.LabelField(rectLabel, label);

                var rectToolbar = new Rect(rectLabel.xMax, indent.y,
                    indent.width - rectLabel.width, indent.height);

                EditorGUI.BeginChangeCheck();
                {
                    property.enumValueIndex = GUI.Toolbar(rectToolbar,
                        property.enumValueIndex, property.enumDisplayNames);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
