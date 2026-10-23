
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusEditorSplitView.cs
*		Класс реализующий возможности изменять размер панелей в окне редакторов.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;

------------
/// <summary>
/// Класс реализующий возможности изменять размер панелей в окне редакторов
/// </summary>
------------
public class LotusEditorSplitView
{
	#region =============================================== ДАННЫЕ ====================================================
	protected Boolean mIsHorizontal;
	protected Single mSplitNormalizedPosition;
	protected Boolean mIsResize;
	protected Vector2 mScrollPosition;
	protected Rect mAvailableRect;
	protected Boolean mIsClosedLayout;
	#endregion

	#region =============================================== КОНСТРУКТОРЫ ==============================================
	--------
	/// <summary>
	/// Конструктор инициализирует объект класса указанными параметрами
	/// </summary>
	/// <param name="is_horizontal">Статус горизонтального расположения</param>
	/// <param name="split_normalized_position">Нормализованная позиция разделителя</param>
	--------
	public LotusEditorSplitView(Boolean is_horizontal, Single split_normalized_position = 0.5f)
	{
		mSplitNormalizedPosition = split_normalized_position;
		mIsHorizontal = is_horizontal;
	}
	#endregion

	#region =============================================== ОБЩИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Начало отображения изменяемой панели
	/// </summary>
	--------
	public void BeginSplitView()
	{
		Rect temp_rect;

		if (mIsHorizontal)
		{
			temp_rect = EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		}
		else
		{
			temp_rect = EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
		}

		if (temp_rect.width > 0.0f)
		{
			mAvailableRect = temp_rect;
		}
		if (mIsHorizontal)
		{
			mScrollPosition = GUILayout.BeginScrollView(mScrollPosition,
				GUILayout.Width(mAvailableRect.width * mSplitNormalizedPosition));
		}
		else
		{
			mScrollPosition = GUILayout.BeginScrollView(mScrollPosition,
				GUILayout.Height(mAvailableRect.height * mSplitNormalizedPosition));
		}

		mIsClosedLayout = false;
	}

	--------
	/// <summary>
	/// Отображение разделителя
	/// </summary>
	/// <param name="offset_x">Дополнительное смещение разделителя по X</param>
	/// <param name="offset_y">Дополнительное смещение разделителя по Y</param>
	--------
	public void Split(Single offset_x, Single offset_y)
	{
		GUILayout.EndScrollView();
		GUILayout.Space(2.0f);
		ResizeSplitFirstView(offset_x, offset_y);
		mIsClosedLayout = true;
	}

	--------
	/// <summary>
	/// Окончание отображения изменяемой панели
	/// </summary>
	--------
	public void EndSplitView()
	{
		if(mIsClosedLayout == false)
		{
			mIsClosedLayout = true;
			GUILayout.EndScrollView();
		}

		if (mIsHorizontal)
		{
			EditorGUILayout.EndHorizontal();
		}
		else
		{
			EditorGUILayout.EndVertical();
		}
	}

	--------
	/// <summary>
	/// Изменение размера панели
	/// </summary>
	/// <param name="offset_x">Дополнительное смещение разделителя по X</param>
	/// <param name="offset_y">Дополнительное смещение разделителя по Y</param>
	--------
	private void ResizeSplitFirstView(Single offset_x, Single offset_y)
	{
		Rect resize_handle_rect;

		if (mIsHorizontal)
		{
			resize_handle_rect = new Rect((mAvailableRect.width * mSplitNormalizedPosition) + offset_x, mAvailableRect.y,
				4f, mAvailableRect.height);
		}
		else
		{
			resize_handle_rect = new Rect(mAvailableRect.x, (mAvailableRect.height * mSplitNormalizedPosition) + offset_y,
				mAvailableRect.width, 4f);
		}

		GUI.DrawTexture(resize_handle_rect, EditorGUIUtility.isProSkin ? XTexture2D.WhiteAlpha25 
			: XTexture2D.BlackAlpha25);

		if (mIsHorizontal)
		{
			EditorGUIUtility.AddCursorRect(resize_handle_rect, MouseCursor.ResizeHorizontal);
		}
		else
		{
			EditorGUIUtility.AddCursorRect(resize_handle_rect, MouseCursor.ResizeVertical);
		}

		if (Event.current.type == EventType.MouseDown && resize_handle_rect.Contains(Event.current.mousePosition))
		{
			mIsResize = true;
		}
		if (mIsResize)
		{
			if (mIsHorizontal)
			{
				mSplitNormalizedPosition = (Event.current.mousePosition.x - offset_x) / (mAvailableRect.width);
			}
			else
			{
				mSplitNormalizedPosition = (Event.current.mousePosition.y - offset_y) / (mAvailableRect.height);
			}
		}
		if (Event.current.type == EventType.MouseUp)
		{
			mIsResize = false;
		}
	}
	#endregion
}
