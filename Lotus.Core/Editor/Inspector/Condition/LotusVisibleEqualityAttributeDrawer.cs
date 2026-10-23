
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusVisibleEqualityAttributeDrawer.cs
*		Редактор атрибута LotusVisibleEqualityAttribute.
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
/// Редактор атрибута LotusVisibleEqualityAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusVisibleEqualityAttribute), true)]
public class LotusVisibleEqualityAttributeDrawer : PropertyDrawer
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
		var attr = this.attribute as LotusVisibleEqualityAttribute;

		// Получаем значение от члена объекта от которого зависит доступность
		var enabled = property.serializedObject.GetValueFromMember<Boolean>(attr.ManagingMemberName, attr.MemberType);

		if (enabled == attr.Value)
		{
			return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
		}
		else
        {
			return 0;
        }
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
		var attr = this.attribute as LotusVisibleEqualityAttribute;

		// Получаем значение от члена объекта от которого зависит доступность
		var enabled = property.serializedObject.GetValueFromMember<Boolean>(attr.ManagingMemberName, attr.MemberType);

		// Если значения не совпадают значит элемент недоступен
		if (enabled == attr.Value)
		{
			EditorGUI.PropertyField(position, property, label, property.isExpanded);
		}
	}
	#endregion
}
