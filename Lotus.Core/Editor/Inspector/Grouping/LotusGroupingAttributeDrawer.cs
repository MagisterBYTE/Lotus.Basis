
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusGroupingAttributeDrawer.cs
*		Редактор атрибута LotusGroupingAttribute.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор атрибута LotusGroupingAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusGroupingAttribute), true)]
public class LotusGroupingAttributeDrawer : PropertyDrawer
{
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
		var attr = this.attribute as LotusGroupingAttribute;

		var opened = property.LoadEditorBool("Grouping");

		if (opened)
		{

			var total_height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);

			var other_property = property.serializedObject.GetIterator();
			var next = other_property.NextVisible(true);
			if (next)
			{
				do
				{
					if (other_property.IsScript()) continue;

					if (other_property.propertyPath.IsExists() && other_property.propertyPath != property.propertyPath)
					{
						LotusInGroupAttribute in_group_attribute = XEditorCachedData.GetAttribute<LotusInGroupAttribute>(other_property);
						if (in_group_attribute != null && in_group_attribute.GroupName == attr.GroupName)
						{
							total_height += EditorGUI.GetPropertyHeight(other_property, label, other_property.isExpanded);
							total_height += EditorGUIUtility.standardVerticalSpacing;
						}
					}
				}
				while (other_property.NextVisible(false));
			}


			return total_height + XInspectorViewParams.HEADER_HEIGHT + EditorGUIUtility.standardVerticalSpacing;
		}
		else
        {
			return XInspectorViewParams.HEADER_HEIGHT;
		}
	}

	--------
	/// <summary>
	/// Рисование сериализуемого свойства
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	--------
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var attr = this.attribute as LotusGroupingAttribute;

		var opened = property.LoadEditorBool("Grouping");

		// Общий фоновый бока
		GUI.backgroundColor = attr.Background;
		GUI.Box(position, GUIContent.none, 
			XEditorStyles.FindStyle(attr.BackgroundStyleName) ?? EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene).box);
		GUI.backgroundColor = Color.white;

		// Смещаем по ширине
		position.xMin += 1;
		position.xMax -= 1;

		Rect rect_header = position;
		rect_header.height = XInspectorViewParams.HEADER_HEIGHT;

		GUI.color = attr.HeaderColor;
		GUI.Label(rect_header, attr.GroupName, EditorStyles.boldLabel);
		GUI.color = Color.white;

		Rect rect_button = position;
		rect_button.x = position.xMax - XInspectorViewParams.BUTTON_PREVIEW_WIDTH;
		rect_button.width = XInspectorViewParams.BUTTON_PREVIEW_WIDTH;
		rect_button.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

		if (GUI.Button(rect_button, opened ? XString.TriangleDown : XString.TriangleLeft, XEditorStyles.ButtonPreviewStyle))
		{
			opened = !opened;
			property.SaveBoolEditor("Grouping", opened);
		}

		if (opened)
		{
			position.y += XInspectorViewParams.HEADER_HEIGHT;
			position.height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);

			EditorGUI.PropertyField(position, property, label, property.isExpanded);
			var other_property = property.serializedObject.GetIterator();
			var next = other_property.NextVisible(true);
			if (next)
			{
				do
				{
					if (other_property.IsScript()) continue;

					if (other_property.propertyPath.IsExists() && other_property.propertyPath != property.propertyPath)
					{
						LotusInGroupAttribute in_group_attribute = XEditorCachedData.GetAttribute<LotusInGroupAttribute>(other_property);
						if (in_group_attribute != null && in_group_attribute.GroupName == attr.GroupName)
						{
							position.y += position.height;
							position.y += EditorGUIUtility.standardVerticalSpacing;

							position.height = EditorGUI.GetPropertyHeight(other_property, other_property.isExpanded);
							EditorGUI.PropertyField(position, other_property, other_property.isExpanded);
						}
					}
				}
				while (other_property.NextVisible(false));
			}
		}
	}
	#endregion
}
