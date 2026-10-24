#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор для рисования параметров универсального типа данных
    /// </summary>
    [CustomPropertyDrawer(typeof(CVariant))]
    public class LotusVariantDrawer : PropertyDrawer
    {
        #region Constants
        /// <summary>
        /// Контент кнопки выбора типа значения
        /// </summary>
        private static readonly GUIContent ContentDrop = new GUIContent(XStringHelper.TriangleDown);
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
            var variant = property.GetValue<CVariant>();
            if (variant != null)
            {
                return GetHeightVariant(variant);
            }

            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var variant = property.GetValue<CVariant>();
            if (variant == null)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                if (label != null)
                {
                    XEditorInspector.PrepareContent(property);
                    if (DrawFixedVariant(position, variant, XEditorInspector.Content, true, true))
                    {
                        property.Save();
                    }
                }
                else
                {
                    if (DrawFixedVariant(position, variant, null, true, true))
                    {
                        property.Save();
                    }
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Получение высоты необходимой для рисования универсального типа данных
        /// </summary>
        /// <param name="variant">Универсальный тип данных</param>
        /// <returns>Высота</returns>
        public static float GetHeightVariant(CVariant variant)
        {
            var height = XInspectorViewParams.CONTROL_HEIGHT;
            if (variant.ValueType == TValueType.Vector2D ||
                variant.ValueType == TValueType.Vector3D ||
                variant.ValueType == TValueType.Vector4D)
            {
                if (!EditorGUIUtility.wideMode)
                {
                    height = (XInspectorViewParams.CONTROL_HEIGHT * 2) + XInspectorViewParams.SPACE;
                }
            }

            return height;
        }

        /// <summary>
        /// Рисование параметров универсального типа данных
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="variant">Универсальный тип данных</param>
        /// <param name="label">Надпись</param>
        /// <param name="viewButton">Статус отображения кнопки смены типа</param>
        /// <param name="colorSelect">Статус цветового выделения определённых типов</param>
        /// <returns>Статус изменения значения</returns>
        public static bool DrawFixedVariant(Rect position, CVariant variant, GUIContent label,
            bool viewButton, bool colorSelect)
        {
            var rectLabel = position;
            var rectField = position;
            var rectButton = position;

            // Вычисляем размеры областей для вывода данных
            if (viewButton)
            {
                rectLabel.width = EditorGUIUtility.labelWidth;

                rectField.x = rectLabel.xMax;
                rectField.width = position.width - EditorGUIUtility.labelWidth - XInspectorViewParams.BUTTON_MINI_WIDTH - 1;

                rectButton.x = rectField.xMax + 1;
                rectButton.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            }
            else
            {
                rectLabel.width = EditorGUIUtility.labelWidth;

                rectField.x = rectLabel.xMax;
                rectField.width = position.width - EditorGUIUtility.labelWidth - 1;
            }

            var styleLabel = EditorStyles.label;
            var changedValue = false;

            switch (variant.ValueType)
            {
                case TValueType.Void:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.LabelField(rectField, "Void");
                    }
                    break;
                case TValueType.Boolean:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.BooleanValue = EditorGUI.Toggle(rectField, variant.BooleanValue);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Integer:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            if (colorSelect)
                            {
                                GUI.backgroundColor = Color.green;
                            }
                            variant.IntegerValue = EditorGUI.IntField(rectField, variant.IntegerValue);
                            if (colorSelect)
                            {
                                GUI.backgroundColor = Color.white;
                            }
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Enum:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        if (variant.EnumValue != null)
                        {
                            EditorGUI.BeginChangeCheck();
                            {
                                variant.EnumValue = EditorGUI.EnumPopup(rectField, variant.EnumValue, EditorStyles.popup);
                            }
                            if (EditorGUI.EndChangeCheck())
                            {
                                changedValue = true;
                            }
                        }
                        else
                        {
                            EditorGUI.LabelField(rectField, "Enum - not supported in editor");
                        }
                    }
                    break;
                case TValueType.Float:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            if (colorSelect)
                            {
                                GUI.backgroundColor = Color.yellow;
                            }
                            variant.FloatValue = EditorGUI.FloatField(rectField, variant.FloatValue);
                            if (colorSelect)
                            {
                                GUI.backgroundColor = Color.white;
                            }
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.DateTime:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.LabelField(rectField, "DateTime - not supported in editor");
                    }
                    break;
                case TValueType.String:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.StringValue = EditorGUI.TextField(rectField, variant.StringValue);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Color:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.ColorValue = EditorGUI.ColorField(rectField, variant.ColorValue);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Vector2D:
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            if (label != null)
                            {
                                variant.Vector2DValue = EditorGUI.Vector2Field(position, label, variant.Vector2DValue);
                            }
                            else
                            {
                                variant.Vector2DValue = EditorGUI.Vector2Field(rectField, GUIContent.none, variant.Vector2DValue);
                            }
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Vector3D:
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            if (label != null)
                            {
                                variant.Vector3DValue = EditorGUI.Vector3Field(position, label, variant.Vector3DValue);
                            }
                            else
                            {
                                variant.Vector3DValue = EditorGUI.Vector3Field(rectField, GUIContent.none, variant.Vector3DValue);
                            }
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Vector4D:
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            if (label != null)
                            {
                                variant.Vector4DValue = EditorGUI.Vector4Field(position, label, variant.Vector4DValue);
                            }
                            else
                            {
                                variant.Vector4DValue = EditorGUI.Vector4Field(rectField, GUIContent.none, variant.Vector4DValue);
                            }
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.SysObject:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.LabelField(rectField, "SysObject - not supported in editor");
                    }
                    break;
                case TValueType.GameObject:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.GameObjectValue = (GameObject)EditorGUI.ObjectField(rectField, variant.GameObjectValue,
                                typeof(GameObject), true);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Texture2D:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.Texture2DValue = (Texture2D)EditorGUI.ObjectField(rectField, variant.Texture2DValue,
                                typeof(Texture2D), false);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Sprite:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.SpriteValue = (Sprite)EditorGUI.ObjectField(rectField, variant.SpriteValue,
                                typeof(Sprite), false);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.Model:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.ModelValue = (GameObject)EditorGUI.ObjectField(rectField, variant.ModelValue,
                                typeof(GameObject), false);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                case TValueType.TextAsset:
                    {
                        if (label != null)
                        {
                            EditorGUI.PrefixLabel(rectLabel, label, styleLabel);
                        }
                        EditorGUI.BeginChangeCheck();
                        {
                            variant.TextAssetValue = (TextAsset)EditorGUI.ObjectField(rectField, variant.TextAssetValue,
                                typeof(TextAsset), false);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            changedValue = true;
                        }
                    }
                    break;
                default:
                    break;
            }

            if (viewButton)
            {
                if (GUI.Button(rectButton, ContentDrop, EditorStyles.miniButtonRight))
                {
                    var menu = new GenericMenu();
                    var names = Enum.GetNames(typeof(TValueType));
                    for (var i = 0; i < names.Length; i++)
                    {
                        menu.AddItem(new GUIContent(names[i]), false, (object typeName) =>
                        {
                            if (Enum.TryParse<TValueType>(typeName.ToString(), out var valueType))
                            {
                                variant.ValueType = valueType;
                            }
                        },
                        names[i]);
                    }

                    menu.ShowAsContext();
                }
            }

            return changedValue;
        }

        /// <summary>
        /// Рисование параметров универсального типа данных
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="variant">Универсальный тип данных</param>
        /// <param name="viewButton">Статус отображения кнопки смены типа</param>
        /// <param name="colorSelect">Статус цветового выделения определённых типов</param>
        /// <returns>Статус изменения значения</returns>
        public static bool DrawLayoutVariant(string name, CVariant variant, bool viewButton = false,
            bool colorSelect = false)
        {
            var height = GetHeightVariant(variant);
            var changedValue = false;
            var position = EditorGUILayout.BeginHorizontal(GUILayout.Height(height));
            {
                EditorGUILayout.PrefixLabel("");

                XEditorInspector.Content.text = name;
                if (Event.current.type != EventType.Layout)
                {
                    changedValue = DrawFixedVariant(position, variant, XEditorInspector.Content, viewButton, colorSelect);
                }
            }
            EditorGUILayout.EndHorizontal();
            return changedValue;
        }
        #endregion
    }
    /**@}*/
}
#endif
