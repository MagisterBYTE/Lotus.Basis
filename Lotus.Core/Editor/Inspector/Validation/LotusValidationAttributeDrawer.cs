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
    /// Редактор атрибута LotusValidationAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusValidationAttribute), true)]
    public class LotusValidationAttributeDrawer : PropertyDrawer
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
            var attr = attribute as LotusValidationAttribute;
            var height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);

            // Если элемент не прошел валидацию то добавляем место под сообщение
            if (!IsValid(property, attr))
            {
                height += (XInspectorViewParams.CONTROL_HEIGHT * 2) + EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }

        /// <summary>
        /// Рисование сериализуемого свойства
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusValidationAttribute;

            EditorGUI.BeginProperty(position, label, property);
            {
                position.height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
                EditorGUI.PropertyField(position, property, label, property.isExpanded);

                // Если элемент не прошел валидацию то выводим сообщение
                if (!IsValid(property, attr))
                {
                    var indent = EditorGUI.IndentedRect(position);
                    var rectBox = indent;
                    rectBox.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                    rectBox.height = XInspectorViewParams.CONTROL_HEIGHT * 2;

                    EditorGUI.HelpBox(rectBox, attr.Message, ConvertToMessageType(attr.MessageType));
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка валидации свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="attr">Атрибут валидации</param>
        /// <returns>Статус успешной валидации</returns>
        private static bool IsValid(SerializedProperty property, LotusValidationAttribute attr)
        {
            var result = property.Invoke(attr.ValidationMethodName, property.GetValueDirect());
            return result is bool status && status;
        }

        /// <summary>
        /// Конвертация из типа сообщения лога в тип сообщения
        /// </summary>
        /// <param name="logType">Тип сообщения лога</param>
        /// <returns>Тип сообщения</returns>
        public static MessageType ConvertToMessageType(TLogType logType)
        {
            switch (logType)
            {
                case TLogType.Info:
                    return MessageType.Info;
                case TLogType.Warning:
                    return MessageType.Warning;
                case TLogType.Error:
                    return MessageType.Error;
                case TLogType.Exception:
                    return MessageType.Error;
                default:
                    return MessageType.None;
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
