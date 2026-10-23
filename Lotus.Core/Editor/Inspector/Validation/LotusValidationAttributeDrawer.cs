
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusValidationAttributeDrawer.cs
*		Редактор атрибута LotusValidationAttribute.
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
/// Редактор атрибута LotusValidationAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusValidationAttribute), true)]
public class LotusValidationAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusValidationAttribute;

		// Получаем статус валидации от метода
		var status = (Boolean)property.Invoke(attr.ValidationMethodName, property.GetValueDirect());

		// Если элемент не прошел валидацию то выводим сообщение
		if (status != true)
		{
			return EditorGUI.GetPropertyHeight(property, label, property.isExpanded) + 
				(XInspectorViewParams.CONTROL_HEIGHT * 2) + EditorGUIUtility.standardVerticalSpacing;
		}

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
		var attr = this.attribute as LotusValidationAttribute;

		position.height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
		EditorGUI.PropertyField(position, property, label, property.isExpanded);

		// Получаем статус валидации от метода
		var status = (Boolean)property.Invoke(attr.ValidationMethodName, property.GetValueDirect());

		// Если элемент не прошел валидацию то выводим сообщение
		if (status != true)
		{
			var indent = EditorGUI.IndentedRect(position);
			Rect rect_box = indent;
			rect_box.y += position.height + EditorGUIUtility.standardVerticalSpacing;
			rect_box.height = XInspectorViewParams.CONTROL_HEIGHT * 2;

			EditorGUI.HelpBox(rect_box, attr.Message, ConvertToMessageType(attr.MessageType));
		}
	}
	
	/// <summary>
	/// Конвертация из типа сообщения лога в тип сообщения
	/// </summary>
	/// <param name="log_type">Тип сообщения лога</param>
	/// <returns>Тип сообщения</returns>
	
	public static MessageType ConvertToMessageType(TLogType log_type)
	{
		switch (log_type)
		{
			case TLogType.Info: return MessageType.Info;
			case TLogType.Warning: return MessageType.Warning;
			case TLogType.Error: return MessageType.Error;
			case TLogType.Exception: return MessageType.Error;
			default:
				break;
		}

		return (MessageType.None);
	}
	#endregion
}
