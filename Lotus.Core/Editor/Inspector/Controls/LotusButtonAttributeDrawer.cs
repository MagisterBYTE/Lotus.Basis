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
    /// Редактор атрибута LotusButtonAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusButtonAttribute), true)]
    public class LotusButtonAttributeDrawer : PropertyDrawer
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
            var attr = attribute as LotusButtonAttribute;

            var fieldRect = position;
            fieldRect.width -= XInspectorViewParams.BUTTON_MINI_WIDTH + 2;

            var buttonRect = position;
            buttonRect.x = fieldRect.xMax + 1;
            buttonRect.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.PropertyField(fieldRect, property, label, property.isExpanded);

                if (GUI.Button(buttonRect, attr.Label))
                {
                    if (attr.InputArgument)
                    {
                        property.Invoke(attr.MethodName, property.GetValue<object>());
                    }
                    else
                    {
                        property.Invoke(attr.MethodName);
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
