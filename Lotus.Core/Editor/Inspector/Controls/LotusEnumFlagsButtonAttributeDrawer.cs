
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusEnumFlagsButtonAttributeDrawer.cs
*		Редактор атрибута LotusEnumFlagsButtonAttribute.
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
/// Редактор атрибута LotusEnumFlagsButtonAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusEnumFlagsButtonAttribute), true)]
public class LotusEnumFlagsButtonAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusEnumFlagsButtonAttribute;

		property.serializedObject.Update();

		var buttons_int_value = 0;
		var enum_length = property.enumNames.Length;
		var button_pressed = new Boolean[enum_length];
		var button_width = (position.width - EditorGUIUtility.labelWidth) / enum_length;

		var indent = EditorGUI.IndentedRect(position);
		var rect_label = new Rect(indent.x, indent.y, EditorGUIUtility.labelWidth, indent.height);
        EditorGUI.LabelField(rect_label, label);

        EditorGUI.BeginChangeCheck();
		{
			for (var i = 0; i < enum_length; i++)
			{
				// Check if the button is/was pressed 
				if ((property.intValue & (1 << i)) == 1 << i)
				{
					button_pressed[i] = true;
				}

                var rect_button = new Rect(indent.x + EditorGUIUtility.labelWidth +
					(button_width * i), indent.y, button_width, indent.height);

                GUIStyle style = EditorStyles.miniButtonMid;
				if (i == 0)
				{
					style = EditorStyles.miniButtonLeft;
				}
				else
				{
					if (i == enum_length - 1)
					{
						style = EditorStyles.miniButtonRight;
					}
				}

				button_pressed[i] = GUI.Toggle(rect_button, button_pressed[i],
					property.enumNames[i], style);

				if (button_pressed[i])
				{
					buttons_int_value += 1 << i;
				}
			}
		}

		if (EditorGUI.EndChangeCheck())
		{
			property.intValue = buttons_int_value;
			property.serializedObject.ApplyModifiedProperties();
		}
	}
	#endregion
}
