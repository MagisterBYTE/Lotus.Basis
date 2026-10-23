
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusHeaderSectionAttributeDrawer.cs
*		Редактор атрибута HeaderSection.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор атрибута HeaderSection
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusHeaderSectionAttribute), true)]
public class LotusHeaderSectionAttributeDrawer : DecoratorDrawer
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
		return (base.GetHeight() + (EditorGUIUtility.standardVerticalSpacing * 2));
	}

	--------
	/// <summary>
	/// Рисование параметров декоративного элемента
	/// </summary>
	/// <param name="position">Прямоугольник для отображения</param>
	--------
	public override void OnGUI(Rect position)
	{
		var section_attribute = attribute as LotusHeaderSectionAttribute;

		GUIStyle style = EditorStyles.boldLabel;

		// Старые параметры
		Color old_color = style.normal.textColor;
		TextAnchor old_anchor = style.alignment;
		style.fontSize++;

		if (section_attribute.TextColor == TColor.Zero)
		{
			section_attribute.TextColor = new TColor(XEditorStyles.ColorHeaderSection);
		}

		// Новые параметры
		style.normal.textColor = section_attribute.TextColor;
		style.alignment = XEnum.ToEnum<TextAnchor>(section_attribute.TextAlignment);

		// Рисуем
		GUI.Label(position, section_attribute.Name, style);

		// Восстанавливаем старые параметры
		style.normal.textColor = old_color;
		style.alignment = old_anchor;
		style.fontSize--;
	}
	#endregion
}
