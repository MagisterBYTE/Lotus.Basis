#if UNITY_EDITOR
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusEnumFlagsButtonAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusEnumFlagsButtonAttribute), true)]
    public class LotusEnumFlagsButtonAttributeDrawer : PropertyDrawer
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
                var buttonsIntValue = 0;
                var enumLength = property.enumNames.Length;
                var buttonPressed = new bool[enumLength];
                var buttonWidth = (position.width - EditorGUIUtility.labelWidth) / enumLength;

                var indent = EditorGUI.IndentedRect(position);
                var rectLabel = new Rect(indent.x, indent.y, EditorGUIUtility.labelWidth, indent.height);
                EditorGUI.LabelField(rectLabel, label);

                EditorGUI.BeginChangeCheck();
                {
                    for (var i = 0; i < enumLength; i++)
                    {
                        if ((property.intValue & (1 << i)) == 1 << i)
                        {
                            buttonPressed[i] = true;
                        }

                        var rectButton = new Rect(indent.x + EditorGUIUtility.labelWidth + (buttonWidth * i),
                            indent.y, buttonWidth, indent.height);

                        var style = EditorStyles.miniButtonMid;
                        if (i == 0)
                        {
                            style = EditorStyles.miniButtonLeft;
                        }
                        else if (i == enumLength - 1)
                        {
                            style = EditorStyles.miniButtonRight;
                        }

                        buttonPressed[i] = GUI.Toggle(rectButton, buttonPressed[i],
                            property.enumNames[i], style);

                        if (buttonPressed[i])
                        {
                            buttonsIntValue += 1 << i;
                        }
                    }
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = buttonsIntValue;
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
