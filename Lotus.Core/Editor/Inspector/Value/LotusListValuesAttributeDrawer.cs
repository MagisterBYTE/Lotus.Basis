
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusListValuesAttributeDrawer.cs
*		Редактор атрибута LotusListValuesAttribute.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор атрибута LotusListValuesAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusListValuesAttribute), true)]
public class LotusListValuesAttributeDrawer : PropertyDrawer
{
	#region =============================================== ДАННЫЕ ====================================================
	internal SerializedProperty mCurrentProperty;
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
		mCurrentProperty = property;
		var attr = this.attribute as LotusListValuesAttribute;

		position.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
		EditorGUI.PropertyField(position, property, label, property.isExpanded);

		Rect rect_button = position;
		rect_button.x = position.xMax;
		rect_button.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
		rect_button.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

		if (GUI.Button(rect_button, "L", EditorStyles.miniButton))
		{
			var list_values = CPropertyDesc.GetValue(attr.ListValues, attr.MemberName,
				attr.MemberType, property.serializedObject.targetObject) as IEnumerable;
			if (list_values != null)
			{
				var menu = new GenericMenu();
				foreach (var item in list_values)
				{
					var text_value = item.ToString();

					if (attr.FormatMethodName.IsExists())
					{
						text_value = (String)property.Invoke(attr.FormatMethodName, item);
					}

					menu.AddItem(new GUIContent(text_value), false, OnSetValueFromMenuItem, item);
				}

				menu.ShowAsContext();
			}
		}
	}

	
	/// <summary>
	/// Установить значение поля/свойства объекта на основании выбранного пункта меню
	/// </summary>
	/// <param name="value">Значение</param>
	
	private void OnSetValueFromMenuItem(System.Object value)
	{
		if(mCurrentProperty != null)
        {
			mCurrentProperty.SetValueDirect(value);
		}
	}
	#endregion
}
