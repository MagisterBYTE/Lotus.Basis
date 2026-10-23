
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusPreviewAttributeDrawer.cs
*		Редактор атрибута LotusPreviewAttribute.
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
/// Редактор атрибута LotusPreviewAttribute
/// </summary>
------------
[CustomPropertyDrawer(typeof(LotusPreviewAttribute), true)]
public class LotusPreviewAttributeDrawer : PropertyDrawer
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
		if (property.propertyType == SerializedPropertyType.ObjectReference)
		{
			Texture2D previewTexture = GetAssetPreview(property);
			if (previewTexture != null)
			{
				return EditorGUI.GetPropertyHeight(property, label, property.isExpanded) + GetAssetPreviewSize(property).y;
			}
			else
			{
				return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
			}
		}
		else
		{
			return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
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
		var attr = this.attribute as LotusPreviewAttribute;

		EditorGUI.BeginProperty(position, label, property);

		if (property.propertyType == SerializedPropertyType.ObjectReference)
		{
			var propertyRect = new Rect()
			{
				x = position.x,
				y = position.y,
				width = position.width,
				height = EditorGUIUtility.singleLineHeight
			};

			EditorGUI.PropertyField(propertyRect, property, label);

			Texture2D previewTexture = GetAssetPreview(property);
			if (previewTexture != null)
			{
				var previewRect = new Rect()
				{
					x = position.x + 2,
					y = position.y + EditorGUIUtility.singleLineHeight,
					width = position.width,
					height = GetAssetPreviewSize(property).y
				};

				GUI.Label(previewRect, previewTexture);
			}
		}
		else
		{
			//string message = property.name + " doesn't have an asset preview";
			//DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
		}

		EditorGUI.EndProperty();
	}

	--------
	/// <summary>
	/// Получение текстуры предпросмотра у сериализуемого свойства
	/// </summary>
	/// <param name="property">Сериализируемое свойство</param>
	/// <returns>Текстура или null</returns>
	--------
	private Texture2D GetAssetPreview(SerializedProperty property)
	{
		if (property.propertyType == SerializedPropertyType.ObjectReference)
		{
			if (property.objectReferenceValue != null)
			{
				Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
				return previewTexture;
			}

			return null;
		}

		return null;
	}

	--------
	/// <summary>
	/// Получение размера текстуры предпросмотра у сериализуемого свойства
	/// </summary>
	/// <param name="property">Сериализируемое свойство</param>
	/// <returns>Размер текстуры</returns>
	--------
	private Vector2 GetAssetPreviewSize(SerializedProperty property)
	{
		Texture2D previewTexture = GetAssetPreview(property);
		if (previewTexture == null)
		{
			return Vector2.zero;
		}
		else
		{
			var targetWidth = 256; // ShowAssetPreviewAttribute.DefaultWidth;
			var targetHeight = 256; // ShowAssetPreviewAttribute.DefaultHeight;

			//ShowAssetPreviewAttribute showAssetPreviewAttribute = PropertyUtility.GetAttribute<ShowAssetPreviewAttribute>(property);
			//if (showAssetPreviewAttribute != null)
			//{
			//	targetWidth = showAssetPreviewAttribute.Width;
			//	targetHeight = showAssetPreviewAttribute.Height;
			//}

			var width = Mathf.Clamp(targetWidth, 0, previewTexture.width);
			var height = Mathf.Clamp(targetHeight, 0, previewTexture.height);

			return new Vector2(width, height);
		}
	}
	#endregion
}
