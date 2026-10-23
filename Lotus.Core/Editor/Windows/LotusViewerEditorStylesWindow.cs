
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusViewerEditorStylesWindow.cs
*		Редактор для просмотра стилей редактора Unity.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор для просмотра стилей редактора Unity
/// </summary>
/// <remarks>
/// За основу взята реализация:
/// https://forum.unity.com/threads/advanced-editor-styles-viewer-free.284148/
/// </remarks>
------------
public class LotusViewerEditorStylesWindow : EditorWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать для просмотра стилей редактора Unity
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathUtility + "Viewer Editor Styles", false, XCoreSettingsEditor.MenuOrderUtility + 20)]
	public static void ShowViewerEditorStyles()
	{
		var window = GetWindow<LotusViewerEditorStylesWindow>();
		window.titleContent.text = "Viewer Editor Styles";

		window.minSize = new Vector2(335, 400);
		window.Show();
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Основные данные
	private Vector2 scrollPos;
	private Int32 countSize = 0;
	private Int32 initialCount = 0;
	private List<GUIStyle> eStyles = new List<GUIStyle>();
	private Int32 selSearchParam = 2;
	private Int32 selOption = 0;
	private Int32 selSkin = 1;

	public GenericMenu optionsMenu = new GenericMenu();
	public GenericMenu searchMenu = new GenericMenu();

	public readonly List<StylePref> stylePrefs = new List<StylePref>();
	public Int32 totalSizeofView = 0;
	public EditorSkin eSkin;
	public List<GUISkin> projectSkins = new List<GUISkin>();
	public List<GUISkin> usableSkins = new List<GUISkin>();

	public List<String> guiSkins = new List<String>();

	public String[] SearchParams = new String[]
	{
		"StartsWith",
		"EndsWith",
		"Contains"
	};

	private Int32 totalSizeOfStyles = 0;
	private String lastCopy = "";
	private String searchText = "";

	public class StylePref
	{
		public Int32 H = 0;
		public Int32 W = 0;
		public Boolean npActive = false;
		public Boolean pActive = false;
		public Boolean enabled = true;
		public Color bgColor = Color.white;
		public Color cColor = Color.white;
		public Boolean displayText = false;
		public String text = "ABC";
	}
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Активация окна
	/// </summary>
	--------
	private void OnEnable()
	{
		totalSizeOfStyles = 0;

		usableSkins.Clear();
		projectSkins.Clear();
		guiSkins.Clear();

		projectSkins = XEditorAssetDatabase.GetAssetsFromFolder<GUISkin>(XEditorSettings.ASSETS_PATH);
		usableSkins.Add(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game));
		usableSkins.Add(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector));

		foreach (GUISkin s in projectSkins)
		{
			usableSkins.Add(s);
		}

		foreach (GUISkin s in usableSkins)
		{
			guiSkins.Add(s.name);
		}

		guiSkins[1] = "InspectorSkin";

		LoadSkin(usableSkins[selSkin]);
	}

	--------
	/// <summary>
	/// Рисование UnityGUI
	/// </summary>
	--------
	private void OnGUI()
	{
		wantsMouseMove = true;
		GUI.Box(new Rect(0, 0, position.width, 17), "", EditorStyles.toolbar);
		GUI.Label(new Rect(5, 0, 500, 14), "Count : " + countSize);

		selSkin = EditorGUI.Popup(new Rect(position.width - 220, 0, 90, 14), "", selSkin, guiSkins.ToArray(), EditorStyles.toolbarDropDown);
		if (GUI.changed)
		{
			LoadSkin(usableSkins[selSkin]);
		}

		GUI.BeginGroup(new Rect(position.width - 102, 2, 86, 14));
		
		searchText = GUI.TextField(new Rect(-16, 0, 102, 14), searchText, EditorStyles.toolbarSearchField);
		GUI.EndGroup();
		selSearchParam = EditorGUI.Popup(new Rect(position.width - 118, 2, 16, 16), "", selSearchParam, SearchParams, XEditorStyles.SEARCH_FIELD_POPUP);

		if (GUI.Button(new Rect(position.width - 16, 2, 16, 14), "", XEditorStyles.SEARCH_FIELD_CANCEL))
		{
			searchText = "";
			Repaint();
		}

		if (GUI.changed)
		{
			UpdateListSize();
		}
		GUI.Box(new Rect(0, position.height - 17, position.width, 17), "", "OL Title");
		float lastY = 0;
		var count = 0;

		scrollPos = GUI.BeginScrollView(new Rect(0, 18, position.width, position.height - 35), scrollPos, new Rect(0, 0, position.width - 16, totalSizeofView + 10));
		for (var i = 0; i < initialCount; i++)
		{
			GUIStyle s = eStyles[i];
			if (CanShow(s.name))
			{
				StylePref sP = stylePrefs[i];
				float aHeight = sP.H;
				var newY = lastY + (5 * count);
				GUI.Box(new Rect(5, newY + 5, position.width - 26, aHeight + 54), "", EditorStyles.toolbar);
				var area = new Rect(5, newY + 5, position.width - 26, aHeight + 50);
				GUI.BeginGroup(area);
				GUI.Box(new Rect(0, 0, area.width - 1, 17), "", "CN Box");
				GUI.Box(new Rect(0, 18, area.width - 1, 17), "", "OL Title");

				GUI.TextField(new Rect(0, 2, area.width - 10, 16),
					i + " : " + s.name);

				if (GUI.Button(new Rect(0, 18, 60, 14), "Options", "OL Title"))
				{
					selOption = i;
					optionsMenu = new GenericMenu();
					optionsMenu.AddItem(new GUIContent("Copy"), false, CopyItem);
					optionsMenu.AddItem(new GUIContent("Display Sample Text"), stylePrefs[selOption].displayText, ShowSampleText);
					optionsMenu.AddItem(new GUIContent("Enable or Disable"), stylePrefs[selOption].enabled, ToggleDisabled);
					optionsMenu.DropDown(new Rect(0, 22, 30, 14));
				}

				GUI.color = Color.gray;
				GUI.Box(new Rect(4, 38, area.width - 8, area.height - 38), "", "GroupBox");
				GUI.color = Color.white;

				if (stylePrefs[i].displayText)
				{
					stylePrefs[i].text = GUI.TextField(new Rect(62, 20, 40, 14), stylePrefs[i].text);
					var colorGroup = new Rect(new Rect(104, 20, position.width - 125, 14));
					GUI.BeginGroup(colorGroup);
					stylePrefs[i].bgColor = EditorGUI.ColorField(new Rect(0, 0, (colorGroup.width / 2) - 5, 14),
						stylePrefs[i].bgColor);
					GUI.color = stylePrefs[i].bgColor.Inverted();
					GUI.Label(new Rect(0, 0, 120, 14), "BG Color");
					GUI.color = Color.white;

					stylePrefs[i].cColor =
						EditorGUI.ColorField(new Rect(colorGroup.width / 2, 0, (colorGroup.width / 2) - 8, 14),
							stylePrefs[i].cColor);

					GUI.color = stylePrefs[i].cColor.Inverted();
					GUI.Label(new Rect(colorGroup.width / 2, 0, 120, 14), "Text Color");
					GUI.color = Color.white;

					GUI.EndGroup();
					GUI.enabled = stylePrefs[i].enabled;
					GUI.backgroundColor = stylePrefs[i].bgColor;
					GUI.contentColor = stylePrefs[i].cColor;
					Vector2 size = s.CalcSize(new GUIContent(stylePrefs[i].text));
					stylePrefs[i].npActive = GUI.Toggle(new Rect(10, 41, (sP.W / 2) + size.x, size.y), stylePrefs[i].npActive, stylePrefs[i].text, s);

					if (EditorGUIUtility.isProSkin && selSkin == 1)
					{
						stylePrefs[i].pActive = GUI.Toggle(new Rect(30 + (sP.W / 2) + size.x, 41, (sP.W / 2) + size.x, size.y), stylePrefs[i].pActive,
							stylePrefs[i].text, s.name);
					}
					GUI.contentColor = Color.white;
					GUI.backgroundColor = Color.white;
				}
				else
				{
					GUI.enabled = stylePrefs[i].enabled;
					stylePrefs[i].npActive = GUI.Toggle(new Rect(10, 41, sP.W, sP.H), stylePrefs[i].npActive, "", s);

					if (EditorGUIUtility.isProSkin && selSkin == 1)
					{
						stylePrefs[i].pActive = GUI.Toggle(new Rect(30 + sP.W, 41, sP.W, sP.H), stylePrefs[i].pActive,
							"", s.name);
					}
				}
				GUI.enabled = true;
				GUI.EndGroup();
				lastY =
					newY - (5 * count)
					+
					(aHeight + 54);
				count++;
			}
		}
		GUI.EndScrollView();
		GUI.Label(new Rect(5, position.height - 16, position.width, 16), "Last style copied : " + lastCopy);
		Repaint();
	}
	#endregion

	#region =============================================== ОБШИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Загрузка скина
	/// </summary>
	/// <param name="skin">Скин</param>
	--------
	private void LoadSkin(GUISkin skin)
	{
		stylePrefs.Clear();
		eStyles.Clear();
		foreach (GUIStyle s in skin)
		{
			stylePrefs.Add(new StylePref());
			eStyles.Add(s);
		}
		initialCount = stylePrefs.Count;
		UpdateListSize();
	}

	--------
	/// <summary>
	/// Показать текст
	/// </summary>
	--------
	public void ShowSampleText()
	{
		stylePrefs[selOption].displayText = !stylePrefs[selOption].displayText;
	}

	--------
	/// <summary>
	/// Скопировать в буфер
	/// </summary>
	--------
	public void CopyItem()
	{
		GUIStyle s = eStyles[selOption];
		EditorGUIUtility.systemCopyBuffer = '"' + s.name + '"';
		lastCopy = '"' + s.name + '"';
		Repaint();
	}

	--------
	/// <summary>
	/// Установить статус недоступности визуального стиля
	/// </summary>
	--------
	public void ToggleDisabled()
	{
		stylePrefs[selOption].enabled = !stylePrefs[selOption].enabled;
		Repaint();
	}

	--------
	/// <summary>
	/// Обновление списка
	/// </summary>
	--------
	public void UpdateListSize()
	{
		var lastY = 0;
		var newY = 0;
		var count = 0;
		for (var i = 0; i < initialCount; i++)
		{
			GUIStyle s = eStyles[i];

			if (CanShow(s.name))
			{
				stylePrefs[i] = new StylePref();
				var height = 16;
				var width = 16;
				newY = lastY + (5 * count);
				if (s.active.background != null)
				{
					height = s.active.background.height;
					width = s.active.background.width;
				}

				if (s.hover.background != null)
				{
					height = s.hover.background.height;
					width = s.hover.background.width;
				}

				if (s.normal.background != null)
				{
					height = s.normal.background.height;
					width = s.normal.background.width;
				}

				if (height < 8 || width < 8)
				{
					height = 8;
					width = 8;
				}

				totalSizeOfStyles += height;
				stylePrefs[i].H = height;
				stylePrefs[i].W = width;
				lastY =
					newY - (5 * count)
					+
					(stylePrefs[i].H + 54);
				count++;
			}
		}
		totalSizeofView = lastY + ((count - 1) * 5);
		countSize = count;
	}

	--------
	/// <summary>
	/// Проверка на видимость объект по вхождению строки поиска
	/// </summary>
	/// <param name="search_string">Строка поиска</param>
	/// <returns>Статус видимости объекта</returns>
	--------
	private Boolean CanShow(String search_string)
	{
		var can_show = false;

		switch (selSearchParam)
		{
			case 0:
				if (search_string.ToLower().StartsWith(searchText.ToLower()))
					can_show = true;
				break;

			case 1:
				if (search_string.ToLower().EndsWith(searchText.ToLower()))
					can_show = true;
				break;

			case 2:
				if (search_string.ToLower().Contains(searchText.ToLower()))
					can_show = true;
				break;
		}

		return can_show;
	}
	#endregion
}
