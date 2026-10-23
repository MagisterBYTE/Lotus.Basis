
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusVariantDrawer.cs
*		Редактор для рисования параметров универсального типа данных.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор для рисования параметров универсального типа данных
/// </summary>
------------
[CustomPropertyDrawer(typeof(CVariant))]
public class LotusVariantDrawer : PropertyDrawer
{
	#region =============================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
	protected static readonly GUIContent mContentDrop = new GUIContent(XString.TriangleDown);
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Получение высоты редактируемого свойства элемента
	/// </summary>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	/// <returns>Высота свойства элемента</returns>
	--------
	public override Single GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		// Получаем объект
		CVariant variant = property.GetValue<CVariant>();
		if (variant != null)
		{
			return (GetHeightVariant(variant));
		}
		else
		{
			return (base.GetPropertyHeight(property, label));
		}
	}

	--------
	/// <summary>
	/// Рисование параметров элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	--------
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Получаем объект
		CVariant variant = property.GetValue<CVariant>();

		if (label != null)
		{
			// Подготавливаем контент
			XEditorInspector.PrepareContent(property);
			if(DrawFixedVariant(position, variant, XEditorInspector.Content, true, true))
			{
				property.Save();
			}
		}
		else
		{
			if(DrawFixedVariant(position, variant,null, true, true))
			{
				property.Save();
			}
		}
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Получение высоты необходимой для рисования универсального типа данных
	/// </summary>
	/// <param name="variant">Универсальный тип данных</param>
	/// <returns>Высота</returns>
	--------
	public static Single GetHeightVariant(CVariant variant)
	{
		var height = XInspectorViewParams.CONTROL_HEIGHT;
		if (variant.ValueType == TValueType.Vector2D ||
			variant.ValueType == TValueType.Vector3D ||
			variant.ValueType == TValueType.Vector4D)
		{
			if (EditorGUIUtility.wideMode == false)
			{
				height = ((XInspectorViewParams.CONTROL_HEIGHT * 2) + XInspectorViewParams.SPACE);
			}
		}

		return (height);
	}

	--------
	/// <summary>
	/// Рисование параметров универсального типа данных
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	/// <param name="variant">Универсальный тип данных</param>
	/// <param name="label">Надпись</param>
	/// <param name="view_button">Статус отображения кнопки смены типа</param>
	/// <param name="color_select">Статус цветового выделения определённых типов</param>
	/// <returns>Статус изменения значения</returns>
	--------
	public static Boolean DrawFixedVariant(Rect position, CVariant variant, GUIContent label, 
		Boolean view_button, Boolean color_select)
	{
		Rect rect_label = position, rect_field = position, rect_button = position;

		// Вычисляем размеры областей для вывода данных
		if (view_button)
		{
			rect_label.width = EditorGUIUtility.labelWidth;

			rect_field.x = rect_label.xMax;
			rect_field.width = position.width - EditorGUIUtility.labelWidth - XInspectorViewParams.BUTTON_MINI_WIDTH - 1;

			rect_button.x = rect_field.xMax + 1;
			rect_button.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
		}
		else
		{
			rect_label.width = EditorGUIUtility.labelWidth;

			rect_field.x = rect_label.xMax;
			rect_field.width = position.width - EditorGUIUtility.labelWidth - 1;
		}

		GUIStyle style_label = EditorStyles.label;
		var changed_value = false;

		switch (variant.ValueType)
		{
			case TValueType.Void:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.LabelField(rect_field, "Void");
				}
				break;
			case TValueType.Boolean:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.BooleanValue = EditorGUI.Toggle(rect_field, variant.BooleanValue);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Integer:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						if (color_select) GUI.backgroundColor = Color.green;
						variant.IntegerValue = EditorGUI.IntField(rect_field, variant.IntegerValue);
						if (color_select) GUI.backgroundColor = Color.white;
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Enum:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					if (variant.EnumValue != null)
					{
						EditorGUI.BeginChangeCheck();
						{
							variant.EnumValue = EditorGUI.EnumPopup(rect_label, variant.EnumValue, EditorStyles.popup);
						}
						if (EditorGUI.EndChangeCheck())
						{
							changed_value = true;
						}
					}
					else
					{
						EditorGUI.LabelField(rect_field, "Enum - not supported in editor");
					}
				}
				break;
			case TValueType.Float:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						if (color_select) GUI.backgroundColor = Color.yellow;
						variant.FloatValue = EditorGUI.FloatField(rect_field, variant.FloatValue);
						if (color_select) GUI.backgroundColor = Color.white;
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.DateTime:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.LabelField(rect_field, "DateTime - not supported in editor");
				}
				break;
			case TValueType.String:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.StringValue = EditorGUI.TextField(rect_field, variant.StringValue);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Color:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.ColorValue = EditorGUI.ColorField(rect_field, variant.ColorValue);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
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
							variant.Vector2DValue = EditorGUI.Vector2Field(rect_field, "", variant.Vector2DValue);
						}
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
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
							variant.Vector3DValue = EditorGUI.Vector3Field(rect_field, "", variant.Vector3DValue);
						}
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
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
							variant.Vector4DValue = EditorGUI.Vector4Field(rect_field, "", variant.Vector4DValue);
						}
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.SysObject:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.LabelField(rect_field, "SysObject - not supported in editor");
				}
				break;
			case TValueType.GameObject:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.GameObjectValue = (GameObject)EditorGUI.ObjectField(rect_field, variant.GameObjectValue,
							typeof(GameObject), true);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Texture2D:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.Texture2DValue = (Texture2D)EditorGUI.ObjectField(rect_field, variant.Texture2DValue,
							typeof(Texture2D), false);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Sprite:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.SpriteValue = (Sprite)EditorGUI.ObjectField(rect_field, variant.SpriteValue,
							typeof(Sprite), false);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.Model:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.ModelValue = (GameObject)EditorGUI.ObjectField(rect_field, variant.ModelValue,
							typeof(GameObject), false);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			case TValueType.TextAsset:
				{
					if (label != null)
					{
						EditorGUI.PrefixLabel(rect_label, label, style_label);
					}
					EditorGUI.BeginChangeCheck();
					{
						variant.TextAssetValue = (TextAsset)EditorGUI.ObjectField(rect_field, variant.ModelValue,
							typeof(TextAsset), false);
					}
					if (EditorGUI.EndChangeCheck())
					{
						changed_value = true;
					}
				}
				break;
			default:
				break;
		}

		if (view_button)
		{
			if (GUI.Button(rect_button, mContentDrop, EditorStyles.miniButtonRight))
			{
				var menu = new GenericMenu();
				var names = Enum.GetNames(typeof(TValueType));
				for (var i = 0; i < names.Length; i++)
				{
					menu.AddItem(new GUIContent(names[i]), false, (System.Object type_name) =>
					{
						variant.ValueType = (TValueType)Enum.Parse(typeof(TValueType), type_name.ToString());
					},
					names[i]);
				}

				menu.ShowAsContext();
			}
		}

		return (changed_value);
	}

	--------
	/// <summary>
	/// Рисование параметров универсального типа данных
	/// </summary>
	/// <param name="name">Имя</param>
	/// <param name="variant">Универсальный тип данных</param>
	/// <param name="view_button">Статус отображения кнопки смены типа</param>
	/// <param name="color_select">Статус цветового выделения определённых типов</param>
	/// <returns>Статус изменения значения</returns>
	--------
	public static Boolean DrawLayoutVariant(String name, CVariant variant, Boolean view_button = false, 
		Boolean color_select = false)
	{
		var height = GetHeightVariant(variant);
		var changed_value = false;
		Rect position = EditorGUILayout.BeginHorizontal(GUILayout.Height(height));
		{
			EditorGUILayout.PrefixLabel("");

			XEditorInspector.Content.text = name;
			if (Event.current.type != EventType.Layout)
			{
				changed_value = DrawFixedVariant(position, variant, XEditorInspector.Content, view_button, color_select);
			}
		}
		EditorGUILayout.EndHorizontal();
		return (changed_value);
	}
	#endregion
}
