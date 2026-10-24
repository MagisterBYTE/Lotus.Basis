#if UNITY_EDITOR
using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор для рисования свойства типа BoolObserved
    /// </summary>
    [CustomPropertyDrawer(typeof(BoolObserved))]
    public class LotusBoolObservedDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            if (valueProperty == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                XEditorInspector.PrepareContent(property);
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUI.PropertyField(position, valueProperty, XEditorInspector.Content);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    property.Invoke(nameof(BoolObserved.ChangedValue));
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования свойства типа IntObserved
    /// </summary>
    [CustomPropertyDrawer(typeof(IntObserved))]
    public class LotusIntObservedDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            if (valueProperty == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                XEditorInspector.PrepareContent(property);
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUI.PropertyField(position, valueProperty, XEditorInspector.Content);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    property.Invoke(nameof(IntObserved.ChangedValue));
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования свойства типа SingleObserved
    /// </summary>
    [CustomPropertyDrawer(typeof(SingleObserved))]
    public class LotusSingleObservedDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            if (valueProperty == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                XEditorInspector.PrepareContent(property);
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUI.PropertyField(position, valueProperty, XEditorInspector.Content);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    property.Invoke(nameof(SingleObserved.ChangedValue));
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования свойства типа StringObserved
    /// </summary>
    [CustomPropertyDrawer(typeof(StringObserved))]
    public class LotusStringObservedDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_value");
            if (valueProperty == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                XEditorInspector.PrepareContent(property);
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUI.PropertyField(position, valueProperty, XEditorInspector.Content);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    property.serializedObject.ApplyModifiedProperties();
                    property.Invoke(nameof(StringObserved.ChangedValue));
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
