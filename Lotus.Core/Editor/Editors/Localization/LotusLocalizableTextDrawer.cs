
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusLocalizableTextDrawer.cs
*		Редактор для рисования параметров строки поддерживающей локализацию.
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
/// Редактор для рисования параметров строки поддерживающей локализацию
/// </summary>
------------
[CustomPropertyDrawer(typeof(TLocalizableText))]
public class LotusLocalizableTextDrawer : PropertyDrawer
{
	#region =============================================== ДАННЫЕ ====================================================
	protected static GUIContent mContentID = new GUIContent("ID");
	protected static GUIContent mContentOpen = new GUIContent("+");
	protected static GUIContent mContentClose = new GUIContent("-");
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Получение высоты свойства
	/// </summary>
	/// <param name="property">Сериализируемое свойство</param>
	/// <param name="label">Надпись</param>
	/// <returns>Высота свойства элемента</returns>
	--------
	public override Single GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		if (property.LoadEditorBool("Expanded", false))
		{
			return (XInspectorViewParams.CONTROL_HEIGHT_SPACE * 2);
		}
		else
		{
			return (XInspectorViewParams.CONTROL_HEIGHT_SPACE);
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
		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginProperty(position, label, property);
		{
			property.serializedObject.Update();

			// Определяем высоту для отображения свойств
			position.height = XInspectorViewParams.CONTROL_HEIGHT;

			// Область для вывода текста
			Rect rect_text = position;
			rect_text.width = position.width - XInspectorViewParams.BUTTON_MINI_WIDTH;

			// Получаем свойство
			SerializedProperty sp_text = property.FindPropertyRelative(nameof(TLocalizableText.Text));
			SerializedProperty sp_id = property.FindPropertyRelative(nameof(TLocalizableText.IDKeyLocalize));

			//
			// Выделяем цветом для быстрой идентификации
			//
			// Если не участвует в подсистеме локализации выделяем фон красным цветом
			Color old = GUI.backgroundColor;
			if (sp_id.intValue == -1)
			{
				GUI.backgroundColor = Color.red;
			}
			else
			{
				// Если есть ключ локализации обозначим фон зеленым
				if (sp_id.intValue != 0)
				{
					GUI.backgroundColor = Color.green;
				}
				else
				{
				}
			}

			sp_text.stringValue = EditorGUI.TextField(rect_text, label, sp_text.stringValue);
			GUI.backgroundColor = old;

			// Область для вывода кнопки
			Rect rect_button = position;
			rect_button.x = rect_text.xMax;
			rect_button.width = XInspectorViewParams.BUTTON_MINI_WIDTH;

			// Кнопка для раскрытия кода
			var expanded = property.LoadEditorBool("Expanded", false);
			if (GUI.Button(rect_button, expanded ? mContentClose : mContentOpen, EditorStyles.miniButtonRight))
			{
				expanded = !expanded;
				property.SaveBoolEditor("Expanded", expanded);
			}

			if (expanded)
			{
				position.y += (XInspectorViewParams.CONTROL_HEIGHT_SPACE);

				EditorGUI.indentLevel++;
				{
					// Область для вывода ключа локализации
					Rect rect_id = position;
					rect_id.width = position.width - 54;
					sp_id.intValue = EditorGUI.IntField(rect_id, mContentID, sp_id.intValue);

					// Область для вывода кнопки
					Rect rect_button_get = position;
					rect_button_get.x = rect_id.xMax + 2;
					rect_button_get.width = 52;

					// Кнопка для получение кода
					if (GUI.Button(rect_button_get, "Get", EditorStyles.miniButtonRight))
					{
						// Нам нужно заменить объект
						//property.Invoke(nameof(TLocalizableText.GetIDFromText));
						sp_id.intValue = sp_text.stringValue.GetHashCode();
						property.serializedObject.ApplyModifiedProperties();
					}

				}
				EditorGUI.indentLevel--;
			}
		}
		EditorGUI.EndProperty();
		if(EditorGUI.EndChangeCheck())
		{
			property.serializedObject.ApplyModifiedProperties();
			property.Save();
		}
	}
	#endregion
}
