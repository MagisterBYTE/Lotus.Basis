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
    /// Редактор атрибута LotusGroupingAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusGroupingAttribute), true)]
    public class LotusGroupingAttributeDrawer : PropertyDrawer
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
            var attr = attribute as LotusGroupingAttribute;
            var opened = property.LoadEditorBool("Grouping");

            if (!opened)
            {
                return XInspectorViewParams.HEADER_HEIGHT;
            }

            var totalHeight = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
            var otherProperty = property.serializedObject.GetIterator();
            var next = otherProperty.NextVisible(true);
            if (next)
            {
                do
                {
                    if (otherProperty.IsScript())
                    {
                        continue;
                    }

                    if (otherProperty.propertyPath.IsExists() && otherProperty.propertyPath != property.propertyPath)
                    {
                        var inGroupAttribute = XEditorCachedData.GetAttribute<LotusInGroupAttribute>(otherProperty);
                        if (inGroupAttribute != null && inGroupAttribute.GroupName == attr.GroupName)
                        {
                            totalHeight += EditorGUI.GetPropertyHeight(otherProperty, label, otherProperty.isExpanded);
                            totalHeight += EditorGUIUtility.standardVerticalSpacing;
                        }
                    }
                }
                while (otherProperty.NextVisible(false));
            }

            return totalHeight + XInspectorViewParams.HEADER_HEIGHT + EditorGUIUtility.standardVerticalSpacing;
        }

        /// <summary>
        /// Рисование сериализуемого свойства
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusGroupingAttribute;
            var opened = property.LoadEditorBool("Grouping");
            var previousBackground = GUI.backgroundColor;
            var previousColor = GUI.color;

            EditorGUI.BeginProperty(position, label, property);
            {
                // Общий фоновый бокс
                GUI.backgroundColor = attr.Background;
                GUI.Box(position, GUIContent.none,
                    XEditorStyles.FindStyle(attr.BackgroundStyleName) ??
                    EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene).box);
                GUI.backgroundColor = previousBackground;

                // Смещаем по ширине
                position.xMin += 1;
                position.xMax -= 1;

                var rectHeader = position;
                rectHeader.height = XInspectorViewParams.HEADER_HEIGHT;

                GUI.color = attr.HeaderColor;
                GUI.Label(rectHeader, attr.GroupName, EditorStyles.boldLabel);
                GUI.color = previousColor;

                var rectButton = position;
                rectButton.x = position.xMax - XInspectorViewParams.BUTTON_PREVIEW_WIDTH;
                rectButton.width = XInspectorViewParams.BUTTON_PREVIEW_WIDTH;
                rectButton.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

                if (GUI.Button(rectButton, opened ? XStringHelper.TriangleDown : XStringHelper.TriangleLeft,
                    XEditorStyles.ButtonPreviewStyle))
                {
                    opened = !opened;
                    property.SaveBoolEditor("Grouping", opened);
                }

                if (opened)
                {
                    position.y += XInspectorViewParams.HEADER_HEIGHT;
                    position.height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);

                    EditorGUI.PropertyField(position, property, label, property.isExpanded);

                    var otherProperty = property.serializedObject.GetIterator();
                    var next = otherProperty.NextVisible(true);
                    if (next)
                    {
                        do
                        {
                            if (otherProperty.IsScript())
                            {
                                continue;
                            }

                            if (otherProperty.propertyPath.IsExists() &&
                                otherProperty.propertyPath != property.propertyPath)
                            {
                                var inGroupAttribute =
                                    XEditorCachedData.GetAttribute<LotusInGroupAttribute>(otherProperty);
                                if (inGroupAttribute != null && inGroupAttribute.GroupName == attr.GroupName)
                                {
                                    position.y += position.height;
                                    position.y += EditorGUIUtility.standardVerticalSpacing;

                                    position.height = EditorGUI.GetPropertyHeight(otherProperty,
                                        otherProperty.isExpanded);
                                    EditorGUI.PropertyField(position, otherProperty, otherProperty.isExpanded);
                                }
                            }
                        }
                        while (otherProperty.NextVisible(false));
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
