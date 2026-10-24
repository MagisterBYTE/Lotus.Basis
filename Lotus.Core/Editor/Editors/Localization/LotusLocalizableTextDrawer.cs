#if UNITY_EDITOR
using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор для рисования параметров строки поддерживающей локализацию
    /// </summary>
    [CustomPropertyDrawer(typeof(TLocalizableText))]
    public class LotusLocalizableTextDrawer : PropertyDrawer
    {
        #region Constants
        /// <summary>
        /// Контент для отображения ключа локализации
        /// </summary>
        private static readonly GUIContent ContentId = new GUIContent("ID");

        /// <summary>
        /// Контент кнопки раскрытия
        /// </summary>
        private static readonly GUIContent ContentOpen = new GUIContent("+");

        /// <summary>
        /// Контент кнопки сворачивания
        /// </summary>
        private static readonly GUIContent ContentClose = new GUIContent("-");
        #endregion

        #region Unity events
        /// <summary>
        /// Получение высоты свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        /// <returns>Высота свойства элемента</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.LoadEditorBool("Expanded", false))
            {
                return XInspectorViewParams.CONTROL_HEIGHT_SPACE * 2;
            }

            return XInspectorViewParams.CONTROL_HEIGHT_SPACE;
        }

        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(position, label, property);
            {
                // Определяем высоту для отображения свойств
                position.height = XInspectorViewParams.CONTROL_HEIGHT;

                // Область для вывода текста
                var rectText = position;
                rectText.width = position.width - XInspectorViewParams.BUTTON_MINI_WIDTH;

                var propertyText = property.FindPropertyRelative(nameof(TLocalizableText.Text));
                var propertyId = property.FindPropertyRelative(nameof(TLocalizableText.IDKeyLocalize));

                // Выделяем цветом для быстрой идентификации
                var oldBackground = GUI.backgroundColor;
                if (propertyId.intValue == -1)
                {
                    // Не участвует в подсистеме локализации
                    GUI.backgroundColor = Color.red;
                }
                else if (propertyId.intValue != 0)
                {
                    // Есть ключ локализации
                    GUI.backgroundColor = Color.green;
                }

                propertyText.stringValue = EditorGUI.TextField(rectText, label, propertyText.stringValue);
                GUI.backgroundColor = oldBackground;

                // Область для вывода кнопки
                var rectButton = position;
                rectButton.x = rectText.xMax;
                rectButton.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

                // Кнопка для раскрытия кода
                var expanded = property.LoadEditorBool("Expanded", false);
                if (GUI.Button(rectButton, expanded ? ContentClose : ContentOpen, EditorStyles.miniButtonRight))
                {
                    expanded = !expanded;
                    property.SaveBoolEditor("Expanded", expanded);
                }

                if (expanded)
                {
                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;

                    EditorGUI.indentLevel++;
                    {
                        // Область для вывода ключа локализации
                        var rectId = position;
                        rectId.width = position.width - 54;
                        propertyId.intValue = EditorGUI.IntField(rectId, ContentId, propertyId.intValue);

                        // Область для вывода кнопки
                        var rectButtonGet = position;
                        rectButtonGet.x = rectId.xMax + 2;
                        rectButtonGet.width = 52;

                        // Кнопка для получения кода
                        if (GUI.Button(rectButtonGet, "Get", EditorStyles.miniButtonRight))
                        {
                            propertyId.intValue = propertyText.stringValue.GetHashCode();
                            property.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.EndProperty();

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                property.Save();
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
