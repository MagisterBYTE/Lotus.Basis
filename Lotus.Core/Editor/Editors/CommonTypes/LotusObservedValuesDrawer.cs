
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusObservedValuesDrawer.cs
*		Редакторы для рисования параметров типов объекты которых информируют об изменении своего значения.
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
/// Редактор для рисования свойства типа BoolObserved
/// </summary>
------------
[CustomPropertyDrawer(typeof(BoolObserved))]
public class LotusBoolObservedDrawer : PropertyDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
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
		property.serializedObject.Update();

		SerializedProperty value_property = property.FindPropertyRelative(nameof(BoolObserved.mValue));
		if (value_property != null)
		{
			XEditorInspector.PrepareContent(property);
			EditorGUI.BeginChangeCheck();
			{
				EditorGUI.PropertyField(position, value_property, XEditorInspector.Content);
			}
			if (EditorGUI.EndChangeCheck())
			{
				property.Invoke(nameof(SingleObserved.ChangedValue));
			}
		}

		property.serializedObject.ApplyModifiedProperties();
	}
	#endregion
}

------------
/// <summary>
/// Редактор для рисования свойства типа IntObserved
/// </summary>
------------
[CustomPropertyDrawer(typeof(IntObserved))]
public class LotusIntObservedDrawer : PropertyDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
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
		property.serializedObject.Update();

		SerializedProperty value_property = property.FindPropertyRelative(nameof(IntObserved.mValue));
		if (value_property != null)
		{
			XEditorInspector.PrepareContent(property);
			EditorGUI.BeginChangeCheck();
			{
				EditorGUI.PropertyField(position, value_property, XEditorInspector.Content);
			}
			if (EditorGUI.EndChangeCheck())
			{
				property.Invoke(nameof(SingleObserved.ChangedValue));
			}
		}

		property.serializedObject.ApplyModifiedProperties();
	}
	#endregion
}

------------
/// <summary>
/// Редактор для рисования свойства типа SingleObserved
/// </summary>
------------
[CustomPropertyDrawer(typeof(SingleObserved))]
public class LotusSingleObservedDrawer : PropertyDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
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
		property.serializedObject.Update();

		SerializedProperty value_property = property.FindPropertyRelative(nameof(SingleObserved.mValue));
		if(value_property != null)
		{
			XEditorInspector.PrepareContent(property);
			EditorGUI.BeginChangeCheck();
			{
				EditorGUI.PropertyField(position, value_property, XEditorInspector.Content);
			}
			if (EditorGUI.EndChangeCheck())
			{
				property.Invoke(nameof(SingleObserved.ChangedValue));
			}
		}

		property.serializedObject.ApplyModifiedProperties();
	}
	#endregion
}

------------
/// <summary>
/// Редактор для рисования свойства типа StringObserved
/// </summary>
------------
[CustomPropertyDrawer(typeof(StringObserved))]
public class LotusStringObservedDrawer : PropertyDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
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
		property.serializedObject.Update();

		SerializedProperty value_property = property.FindPropertyRelative(nameof(StringObserved.mValue));
		if (value_property != null)
		{
			XEditorInspector.PrepareContent(property);
			EditorGUI.BeginChangeCheck();
			{
				EditorGUI.PropertyField(position, value_property, XEditorInspector.Content);
			}
			if (EditorGUI.EndChangeCheck())
			{
				property.Invoke(nameof(SingleObserved.ChangedValue));
			}
		}

		property.serializedObject.ApplyModifiedProperties();
	}
	#endregion
}
