
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusViewerZoomContentWindow.cs
*		Редактор для просмотра, масштабирования и панорамирование внутреннего контента.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Editor;

------------
/// <summary>
/// Редактор для просмотра, масштабирования и панорамирование внутреннего контента
/// </summary>
------------
public class LotusViewerZoomContentWindow : LotusUtilityBaseWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать окно для просмотра, масштабирования и панорамирование внутреннего контента
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathUtility + "Viewer Zoom Content", false, XCoreSettingsEditor.MenuOrderUtility + 15)]
	public static void ShowViewerZoomContent()
	{
		var window = GetWindow<LotusViewerZoomContentWindow>();
		window.titleContent.text = "Viewer Zoom Content";

		window.OnConstruct();
		window.Show();
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Основные данные
	#endregion

	#region =============================================== ОБШИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Конструирование окна
	/// </summary>
	--------
	public void OnConstruct()
	{
		mIsManager = false;
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Рисование основной панели
	/// </summary>
	--------
	public override void DrawContent()
	{
		GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
	}
	#endregion
}
