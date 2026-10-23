
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusDefaultValueAttributeDrawer.cs
*		Редактор атрибута LotusDefaultValueAttribute.
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
/// Редактор атрибута LotusDefaultValueAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusDefaultValueAttribute), true)]
public class LotusDefaultValueAttributeDrawer : PropertyDrawer
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
		return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
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
		var attr = this.attribute as LotusDefaultValueAttribute;

		position.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
		EditorGUI.PropertyField(position, property, label, property.isExpanded);

		Rect rect_button = position;
		rect_button.x = position.xMax;
		rect_button.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
		rect_button.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

		if (GUI.Button(rect_button, "D", EditorStyles.miniButton))
		{
			var value = CPropertyDesc.GetValue(attr.DefaultValue, attr.MemberName, attr.MemberType, 
				property.serializedObject.targetObject);
			if (value != null)
			{
				property.SetValueDirect(value);
			}
		}
	}
	#endregion
}
