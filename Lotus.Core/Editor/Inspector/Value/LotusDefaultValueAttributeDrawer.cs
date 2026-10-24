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
    /// Редактор атрибута LotusDefaultValueAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusDefaultValueAttribute), true)]
    public class LotusDefaultValueAttributeDrawer : PropertyDrawer
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
            var attr = attribute as LotusDefaultValueAttribute;

            EditorGUI.BeginProperty(position, label, property);
            {
                position.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
                EditorGUI.PropertyField(position, property, label, property.isExpanded);

                var rectButton = position;
                rectButton.x = position.xMax;
                rectButton.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
                rectButton.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

                if (GUI.Button(rectButton, "D", EditorStyles.miniButton))
                {
                    var value = CPropertyDesc.GetValue(attr.DefaultValue, attr.MemberName, attr.MemberType,
                        property.serializedObject.targetObject);
                    if (value != null)
                    {
                        property.SetValueDirect(value);
                    }
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
