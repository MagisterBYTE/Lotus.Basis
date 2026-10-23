
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusBackgroundAttributeDrawer.cs
*		Редактор атрибута LotusBackgroundAttribute.
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
/// Редактор атрибута LotusBackgroundAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusBackgroundAttribute), true)]
public class LotusBackgroundAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusBackgroundAttribute;

		GUI.backgroundColor = attr.Background;
		EditorGUI.PropertyField(position, property, label, property.isExpanded);
		GUI.backgroundColor = Color.white;
	}
	#endregion
}
