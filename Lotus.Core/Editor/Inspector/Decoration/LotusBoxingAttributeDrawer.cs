
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusBoxingAttributeDrawer.cs
*		Редактор атрибута LotusBoxingAttribute.
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
/// Редактор атрибута LotusBoxingAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusBoxingAttribute), true)]
public class LotusBoxingAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusBoxingAttribute;

		var indent = EditorGUI.IndentedRect(position);
		if (attr.OffsetLeft < 0) indent.xMin += attr.OffsetLeft;
		if (attr.OffsetRight < 0) indent.xMax -= attr.OffsetRight;
		if (attr.OffsetTop < 0) indent.yMin += attr.OffsetTop;
		if (attr.OffsetBottom < 0) indent.yMax -= attr.OffsetBottom;
		GUI.Box(indent, GUIContent.none, XEditorStyles.FindStyle(attr.BackgroundStyleName) ?? GUIStyle.none);

		// Центрируем для красоты
		position.y += (Mathf.Abs(attr.OffsetBottom) + Mathf.Abs(attr.OffsetTop)) / 2;
		position.x += 2;
		position.width -= 4;

		EditorGUI.PropertyField(position, property, label, property.isExpanded);
	}
	#endregion
}
