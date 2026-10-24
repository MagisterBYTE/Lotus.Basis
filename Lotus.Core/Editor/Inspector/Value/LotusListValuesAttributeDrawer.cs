#if UNITY_EDITOR
using System.Collections;

using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusListValuesAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusListValuesAttribute), true)]
    public class LotusListValuesAttributeDrawer : PropertyDrawer
    {
        #region Fields
        internal SerializedProperty _currentProperty;
        #endregion

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
            _currentProperty = property;
            var attr = attribute as LotusListValuesAttribute;

            EditorGUI.BeginProperty(position, label, property);
            {
                position.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
                EditorGUI.PropertyField(position, property, label, property.isExpanded);

                var rectButton = position;
                rectButton.x = position.xMax;
                rectButton.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
                rectButton.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

                if (GUI.Button(rectButton, "L", EditorStyles.miniButton))
                {
                    var listValues = CPropertyDesc.GetValue(attr.ListValues, attr.MemberName,
                        attr.MemberType, property.serializedObject.targetObject) as IEnumerable;
                    if (listValues != null)
                    {
                        var menu = new GenericMenu();
                        foreach (var item in listValues)
                        {
                            var textValue = item.ToString();

                            if (attr.FormatMethodName.IsExists())
                            {
                                textValue = (string)property.Invoke(attr.FormatMethodName, item);
                            }

                            menu.AddItem(new GUIContent(textValue), false, OnSetValueFromMenuItem, item);
                        }

                        menu.ShowAsContext();
                    }
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установить значение поля/свойства объекта на основании выбранного пункта меню
        /// </summary>
        /// <param name="value">Значение</param>
        private void OnSetValueFromMenuItem(object value)
        {
            if (_currentProperty != null)
            {
                _currentProperty.SetValueDirect(value);
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
