
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSystemSettingsServiceEditor.cs
*		Редактор сервиса для хранения глобальных системных и пользовательских настроек системы Lotus.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.IO;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор сервиса для хранения глобальных системных и пользовательских настроек системы Lotus
/// </summary>
------------
[CustomEditor(typeof(LotusSystemSettingsService))]
public class LotusSystemSettingsServiceEditor : Editor
{
	#region =============================================== КОНСТАНТНЫЕ ДАННЫЕ ========================================
	/// <summary>
	/// Описание сервиса
	/// </summary>
	public const String DESCRIPTION_SERVICE = "Сервис для хранения глобальных системных и пользовательских настроек системы Lotus";

	/// <summary>
	/// Путь файла сервиса
	/// </summary>
	public const String SYSTEM_SETTINGS_SERVICE_PATH = XEditorSettings.ResourcesDataPath;
	#endregion

	#region =============================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
	protected static readonly GUIContent mContentDeleteSkin = new GUIContent("X", "Delete reference to skin");
	protected static readonly GUIContent mContentSetSkin = new GUIContent("S", "Set skin to current");
	protected static readonly GUIContent mContentAddSkin = new GUIContent("Add", "Add reference to skin");
	protected static readonly GUIContent mContentFind = new GUIContent("Find resources", "Find and load resources");
	#endregion

	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Создания центрального сервиса для управления визуальными стилями и скинами
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPath + "Create System Settings Service", false, XCoreSettingsEditor.MenuOrderLast + 20)]
	public static void Create()
	{
		// Создаем сервис
		var instance = ScriptableObject.CreateInstance<LotusSystemSettingsService>();
		instance.Create();

		// Создаем ресурс для хранения сервиса
		var path = SYSTEM_SETTINGS_SERVICE_PATH + LotusSystemSettingsService.SYSTEM_SETTINGS_SERVICE_NAME + ".asset";
		if (!Directory.Exists(SYSTEM_SETTINGS_SERVICE_PATH))
		{
			Directory.CreateDirectory(SYSTEM_SETTINGS_SERVICE_PATH);
		}
		AssetDatabase.CreateAsset(instance, path);

		// Обновляем в редакторе
		AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
		EditorUtility.DisplayDialog("System settings service successfully created", "Path\n" + path, "OK");
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private LotusSystemSettingsService mSystemSettingsService;
	private String mDesignScreenSizeText;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mSystemSettingsService = this.target as LotusSystemSettingsService;
	}

	--------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	--------
	public override void OnInspectorGUI()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.HelpBox(DESCRIPTION_SERVICE, MessageType.Info);

		// Параметры разработки
		DrawDesign();

		GUILayout.Space(2.0f);
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	--------
	/// <summary>
	/// Рисование параметров разработки
	/// </summary>
	--------
	public void DrawDesign()
	{
		XEditorInspector.DrawCaption("Параметры разработки");

		EditorGUI.BeginChangeCheck();

		GUILayout.Space(2.0f);
		mSystemSettingsService.DesignWidth = XEditorInspector.PropertyInt("Design Width", mSystemSettingsService.DesignWidth);

		GUILayout.Space(2.0f);
		mSystemSettingsService.DesignHeight = XEditorInspector.PropertyInt("Design Height", mSystemSettingsService.DesignHeight);

		GUILayout.BeginHorizontal();
		{
			if(GUILayout.Button("H:1600х1000"))
            {
				mSystemSettingsService.DesignWidth = 1600;
				mSystemSettingsService.DesignHeight = 1000;
				mDesignScreenSizeText = "H:1600х1000";
			}

			if (GUILayout.Button("H:1600х900"))
			{
				mSystemSettingsService.DesignWidth = 1600;
				mSystemSettingsService.DesignHeight = 900;
				mDesignScreenSizeText = "H:1600х900";
			}

			if (GUILayout.Button("V:1000х1600"))
			{
				mSystemSettingsService.DesignWidth = 1000;
				mSystemSettingsService.DesignHeight = 1600;
				mDesignScreenSizeText = "V:1000х1600";
			}

			if (GUILayout.Button("V:900х1600"))
			{
				mSystemSettingsService.DesignWidth = 900;
				mSystemSettingsService.DesignHeight = 1600;
				mDesignScreenSizeText = "V:900х1600";
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Apply to game screen"))
			{

				XEditorGameView.ApplyCustomSize(TGameViewSizeType.AspectRatio, EditorUserBuildSettings.activeBuildTarget,
					mSystemSettingsService.DesignWidth, mSystemSettingsService.DesignHeight, mDesignScreenSizeText);
			}
		}
		GUILayout.EndHorizontal();

		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.Save();
		}
	}
	#endregion
}
