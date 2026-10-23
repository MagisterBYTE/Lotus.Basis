
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusUtilityBaseWindow.cs
*		Базовая панель(окно) утилиты для представления панели редактирования платформы Lotus.
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
/// Базовая панель(окно) утилиты для представления панели редактирования платформы Lotus
/// </summary>
/// <remarks>
/// Используется в качестве базовой панели(окна) для остальных панелей(окон).
/// Поддерживает различные режимы и струтуру отображения.
/// Автоматически может загружать описание структурной части панели из текстового файла
/// </remarks>
------------
public class LotusUtilityBaseWindow : EditorWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать окно базового редактора Lotus
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathUtility + "Base Window Utility", false, XCoreSettingsEditor.MenuOrderUtility + 1)]
	public static void ShowUtilityBase()
	{
		var window = GetWindow<LotusUtilityBaseWindow>();
		window.titleContent.text = "Base Window Utility";

		window.Show();
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Служебные данные для рисования
	protected LotusEditorSplitView mSplitViewMain = new LotusEditorSplitView(true, 0.25f);
	protected LotusEditorSplitView mSplitViewExplore = new LotusEditorSplitView(false, 0.8f);
	protected LotusEditorSplitView mSplitViewContent = new LotusEditorSplitView(false, 0.9f);

	// Статус отображения отдельных панелей
	protected Boolean mIsToolbar = true;
	protected Boolean mIsOutput = true;
	protected Boolean mIsExplore = true;
	protected Boolean mIsManager = true;

	// Выбранная вкладка
	protected Int32 mSelectedTab;
	protected String[] mTabs = new String[1];
	protected String[] mDesc = new String[1];
	#endregion

	#region =============================================== СВОЙСТВА ==================================================
	/// <summary>
	/// Статус отображения панели инструментов
	/// </summary>
	public Boolean IsToolbar
	{
		get { return (mIsToolbar); }
		set { mIsToolbar = value; }
	}

	/// <summary>
	/// Статус отображения панели вывода
	/// </summary>
	public Boolean IsOutput
	{
		get { return (mIsOutput); }
		set { mIsOutput = value; }
	}

	/// <summary>
	/// Статус отображения панели обозревателя
	/// </summary>
	public Boolean IsExplore
	{
		get { return (mIsExplore); }
		set { mIsExplore = value; }
	}

	/// <summary>
	/// Статус отображения панели управления
	/// </summary>
	public Boolean IsManager
	{
		get { return (mIsManager); }
		set { mIsManager = value; }
	}
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Рисование UnityGUI
	/// </summary>
	--------
	public virtual void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
			{
				if (mIsToolbar)
				{
					if (mTabs != null && mTabs.Length > 1)
					{
						GUILayout.Space(2.0f);
						mSelectedTab = GUILayout.Toolbar(mSelectedTab, mTabs, GUILayout.ExpandWidth(true),
							GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
					}
					else
					{

					}
				}

				GUILayout.FlexibleSpace();

				GUILayout.Space(2.0f);
				mIsToolbar = GUILayout.Toggle(mIsToolbar, "Toolbar", EditorStyles.miniButtonLeft, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

				//GUILayout.Space(2.0f);
				mIsOutput = GUILayout.Toggle(mIsOutput, "Output", EditorStyles.miniButtonMid, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

				//GUILayout.Space(2.0f);
				mIsExplore = GUILayout.Toggle(mIsExplore, "Explore", EditorStyles.miniButtonMid, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

				//GUILayout.Space(2.0f);
				mIsManager = GUILayout.Toggle(mIsManager, "Manager", EditorStyles.miniButtonRight, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
			}
			EditorGUILayout.EndHorizontal();


			if (mIsExplore)
			{
				mSplitViewMain.BeginSplitView();
				{
					if (mIsManager)
					{
						mSplitViewExplore.BeginSplitView();
						{
							DrawExplore();
							mSplitViewExplore.Split(0, 0);
							DrawManager();
						}
						mSplitViewExplore.EndSplitView();
					}
					else
					{
						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(4.0f);
						EditorGUILayout.BeginVertical();
						DrawExplore();
						EditorGUILayout.EndVertical();
						GUILayout.Space(6.0f);
						EditorGUILayout.EndHorizontal();
					}

					mSplitViewMain.Split(-2, 0);
					if (mIsOutput)
					{
						mSplitViewContent.BeginSplitView();
						{
							DrawContent();
							mSplitViewContent.Split(0, 22);
							DrawOutput();
						}
						mSplitViewContent.EndSplitView();
					}
					else
					{
						GUILayout.BeginVertical();
						DrawContent();
						GUILayout.EndVertical();
					}

				}
				mSplitViewMain.EndSplitView();
			}
			else
			{
				if (mIsOutput)
				{
					mSplitViewContent.BeginSplitView();
					{
						DrawContent();
						mSplitViewContent.Split(0, 22);
						DrawOutput();
					}
					mSplitViewContent.EndSplitView();
				}
				else
				{
					DrawContent();
				}
			}
		}
		EditorGUILayout.EndVertical();
		Repaint();


		BeginWindows();
		{
			DrawWindows();
		}
		EndWindows();
	}
	#endregion

	#region =============================================== ОБЩИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Добавление вкладок
	/// </summary>
	/// <param name="tabs">Набор вкладок</param>
	--------
	public void AddTabs(params String[] tabs)
	{
		if(tabs != null && tabs.Length > 0)
		{
			mTabs = new String[tabs.Length];
			for (var i = 0; i < tabs.Length; i++)
			{
				mTabs[i] = tabs[i];
			}
		}
	}
	
	--------
	/// <summary>
	/// Загрузка информации по вкладкам
	/// </summary>
	--------
	public void LoadTabAndDesc()
	{
		var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
		path = path.RemoveExtension();
		path += "Desc.txt";
		List <KeyValuePair<String, String>> data = XString.ConvertLinesToGroupLines(System.IO.File.ReadAllLines(path), XString.SeparatorFileData);

		if (data != null && data.Count > 0)
		{
			mTabs = new String[data.Count];
			mDesc = new String[data.Count];
			for (var i = 0; i < data.Count; i++)
			{
				mTabs[i] = data[i].Key.Trim('#');
				mDesc[i] = data[i].Value;
			}
		}
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Рисование основной панели
	/// </summary>
	--------
	public virtual void DrawContent()
	{
		GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
	}

	--------
	/// <summary>
	/// Рисование панели обозревателя
	/// </summary>
	--------
	public virtual void DrawExplore()
	{
		GUILayout.Box("RectExplore", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
	}

	--------
	/// <summary>
	/// Рисование панели управления
	/// </summary>
	--------
	public virtual void DrawManager()
	{
		GUILayout.Box("RectManager", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
	}

	--------
	/// <summary>
	/// Рисование панели вывода информации
	/// </summary>
	--------
	public virtual void DrawOutput()
	{
		if (mDesc.Length > 1 && mSelectedTab != -1 && mSelectedTab < mDesc.Length)
		{
			if (mDesc[mSelectedTab].IsExists())
			{
				var font_size = EditorStyles.helpBox.fontSize;
				EditorStyles.helpBox.fontSize = 12;
				EditorGUILayout.HelpBox(mDesc[mSelectedTab], MessageType.Info);
				EditorStyles.helpBox.fontSize = font_size;
			}
		}
		else
		{
			GUILayout.Box("RectOutput", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
		}
	}

	--------
	/// <summary>
	/// Рисование окон
	/// </summary>
	--------
	public virtual void DrawWindows()
	{

	}
	#endregion
}
