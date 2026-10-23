
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusButtonAttributeDrawer.cs
*		Редактор атрибута LotusButtonAttribute.
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
/// Редактор атрибута LotusButtonAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusButtonAttribute), true)]
public class LotusButtonAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusButtonAttribute;

		Rect field_rect = position;
		field_rect.width -= XInspectorViewParams.BUTTON_MINI_WIDTH + 2;

		Rect button_rect = position;
		button_rect.x = field_rect.xMax + 1;
		button_rect.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

		EditorGUI.PropertyField(field_rect, property, label, property.isExpanded);

		if (GUI.Button(button_rect, attr.Label))
		{
			if (attr.InputArgument)
			{
				property.Invoke(attr.MethodName, property.GetValue<System.Object>());
			}
			else
			{
				property.Invoke(attr.MethodName);
			}
		}
	}
	#endregion
}
