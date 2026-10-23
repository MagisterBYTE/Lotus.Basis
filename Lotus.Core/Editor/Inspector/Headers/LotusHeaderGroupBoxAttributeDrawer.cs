
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusHeaderGroupBoxAttributeDrawer.cs
*		Редактор атрибута HeaderGroupBox.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;

------------
/// <summary>
/// Редактор атрибута HeaderGroupBox
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusHeaderGroupBoxAttribute), true)]
public class LotusHeaderGroupBoxAttributeDrawer : DecoratorDrawer
{
	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Получение высоты декоративного элемента
	/// </summary>
	/// <returns>Высота декоративного элемента</returns>
	--------
	public override Single GetHeight()
	{
		return (base.GetHeight() + (EditorStyles.helpBox.border.top +
			EditorStyles.helpBox.border.bottom));
	}

	--------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	--------
	public override void OnGUI(Rect position)
	{
		var group_attribute = attribute as LotusHeaderGroupBoxAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;

		if (group_attribute.TextColor == TColor.Zero)
		{
			group_attribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
		}

		// Новые параметры
		style.normal.textColor = group_attribute.TextColor;
		style.alignment = XEnum.ToEnum<TextAnchor>(group_attribute.TextAlignment);

		// Рисуем
		position.x += (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		position.width -= (group_attribute.Indent * XInspectorViewParams.OFFSET_INDENT);
		position.height -= EditorGUIUtility.standardVerticalSpacing;
		GUI.Box(position, "", EditorStyles.helpBox);
		if (group_attribute.TextAlignment == "UpperLeft" ||
			group_attribute.TextAlignment == "MiddleLeft" ||
			group_attribute.TextAlignment == "LowerLeft")
		{
			position.x += EditorGUIUtility.standardVerticalSpacing * 2;
		}
		GUI.Label(position, group_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
	}
	#endregion
}
