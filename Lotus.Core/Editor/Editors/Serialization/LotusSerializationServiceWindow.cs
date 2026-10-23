
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSerializationServiceWindow.cs
*		Редактор основного сервиса для сериализации данных в редакторе Unity.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор основного сервиса для сериализации данных в редакторе Unity
/// </summary>
------------
public class LotusSerializationServiceWindow : EditorWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать окно базового редактора Lotus
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathSerialization + "Service", false, XCoreSettingsEditor.MenuOrderSerialization + 1)]
	public static void ShowServiceWindow()
	{
		var window = GetWindow<LotusSerializationServiceWindow>();
		window.titleContent.text = "Serialization Service";

		window.Show();
	}

	--------
	/// <summary>
	/// Сохранение выбранного игрового объекта в файл
	/// </summary>
	--------
	[MenuItem(XEditorSettings.MenuPlace + "Save/SaveSelectedGameObject", false, XEditorSettings.MenuOrderLast + 2)]
	public static void SaveSelectedGameObject()
	{
		if(Selection.activeGameObject != null)
		{
			// Создаем при необходимости директорию
			if (!Directory.Exists(XEditorSettings.AutoSavePath)) Directory.CreateDirectory(XEditorSettings.AutoSavePath);

			// Имя файла для сохранения
			var file_name = Selection.activeGameObject.name + "_" + XDateTime.GetStrDateTimeShort();

			// Путь
			var path = Path.Combine(XEditorSettings.AutoSavePath, file_name);

			// Сохраняем
			//XSerializationDispatcherUnity.SaveToXml(path, Selection.activeGameObject, true);
		}
	}

	--------
	/// <summary>
	/// Сохранение всей сцены в файл
	/// </summary>
	--------
	[MenuItem(XEditorSettings.MenuPlace + "Save/SaveScene", false, XEditorSettings.MenuOrderLast + 3)]
	public static void SaveScene()
	{
		// Создаем при необходимости директорию
		if (!Directory.Exists(XEditorSettings.AutoSavePath)) Directory.CreateDirectory(XEditorSettings.AutoSavePath);

		Scene current_scene = SceneManager.GetActiveScene();

		// Имя файла для сохранения
		var file_name = (current_scene.name.IsExists() ? current_scene.name : "Noname") + "_" + XDateTime.GetStrDateTimeShort();

		// Путь
		var path = Path.Combine(XEditorSettings.AutoSavePath, file_name);

		// Сохраняем
		//XSerializationDispatcherUnity.SaveToXml(path, current_scene.GetRootGameObjects(), true);
	}

	--------
	/// <summary>
	/// Загрузка игровых объектов из файла
	/// </summary>
	--------
	[MenuItem(XEditorSettings.MenuPlace + "Load/LoadFromFile", false, XEditorSettings.MenuOrderLast + 4)]
	public static void LoadFromFile()
	{
		//String file_name = XFileDialog.Open("Открыть файл для загрузки", XEditorSettings.AutoSavePath, 
		//	//XSerializationDispatcherUnity.DEFAULT_EXT.Remove(0, 1));
		//if (file_name.IsExists())
		//{
		//	//XSerializationDispatcherUnity.LoadFromXml(file_name);
		//}
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private String mDescriptionService = "Основной сервис для сериализации данных";
	private Vector2 mScrollPos;
	private String mFileNameDesignXML;
	private String mFileNameGameXML;
	private TextAsset mFileAssetGameXML;
	private TextAsset mFileAssetDesignXML;
	private Boolean mExpandedEditorSave;
	private TSerializationEditorPolicy mEditorPolicy;
	private TSerializationEditorVolume mEditorVolume;
	private Boolean mIsSerializableChild;
	private Boolean mExpandedGameSave;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Рисование UnityGUI
	/// </summary>
	--------
	public void OnGUI()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.HelpBox(mDescriptionService, MessageType.Info);

		GUILayout.Space(4.0f);
		mEditorPolicy = XEditorInspector.PropertyEnum("Policy", mEditorPolicy);

		GUILayout.Space(4.0f);
		mEditorVolume = XEditorInspector.PropertyEnum("Volume", mEditorVolume);

		if (mEditorVolume == TSerializationEditorVolume.Game)
		{
			GUILayout.Space(2.0f);
			XEditorInspector.DrawGroup("Not supported in this release", XEditorStyles.ColorRed, TextAnchor.MiddleCenter);
		}

		if (mEditorVolume == TSerializationEditorVolume.Selected)
		{
			GUILayout.Space(2.0f);
			mIsSerializableChild = XEditorInspector.PropertyBoolean("IsSerializableChild", mIsSerializableChild);

			GUILayout.Space(2.0f);
			GUILayout.Label("Selected Object:", EditorStyles.boldLabel);

			var h = Selection.gameObjects.Length * XInspectorViewParams.CONTROL_HEIGHT_SPACE;
			if (h < XInspectorViewParams.CONTROL_HEIGHT_SPACE) h = XInspectorViewParams.CONTROL_HEIGHT_SPACE * 2;
			if (h > XInspectorViewParams.CONTROL_HEIGHT_SPACE * 4) h = XInspectorViewParams.CONTROL_HEIGHT_SPACE * 4;
			mScrollPos = GUILayout.BeginScrollView(mScrollPos, GUILayout.Height(h));
			{
				for (var i = 0; i < Selection.gameObjects.Length; i++)
				{
					GUILayout.Label(Selection.gameObjects[i].name, EditorStyles.whiteLabel);
				}
			}
			GUILayout.EndScrollView();
		}

		GUILayout.Space(4.0f);
		EditorGUILayout.BeginHorizontal();
		{
			mFileNameDesignXML = XEditorInspector.PropertyString("Name XML", mFileNameDesignXML);
			if (GUILayout.Button("Save"))
			{
				switch (mEditorVolume)
				{
					case TSerializationEditorVolume.Scene:
						{
							Scene current_scene = SceneManager.GetActiveScene();
							//XSerializationDispatcherUnity.SaveToXml(mFileNameDesignXML, current_scene.GetRootGameObjects(), true);
						}
						break;
					case TSerializationEditorVolume.Selected:
						{
							//XSerializationDispatcherUnity.SaveToXml(mFileNameDesignXML, Selection.activeGameObject, mIsSerializableChild);
						}
						break;
					case TSerializationEditorVolume.Game:
						break;
					default:
						break;
				}
			}
			if (GUILayout.Button("Auto Save"))
			{
				SaveScene();
			}
		}
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(4.0f);
		EditorGUILayout.BeginHorizontal();
		{
			mFileAssetDesignXML = XEditorInspector.PropertyResource("Asset XML", mFileAssetDesignXML);
			if (GUILayout.Button("Load"))
			{
				//XSerializationDispatcherUnity.LoadFromXml(mFileAssetDesignXML);
			}
			if (GUILayout.Button("Auto Load", XEditorStyles.BUTTON_DROP_DOWN_1))
			{
				var files = Directory.GetFiles(XEditorSettings.AutoSavePath);
				var menu = new GenericMenu();
				for (var i = 0; i < files.Length; i++)
				{
					//if (Path.GetExtension(files[i]) == XSerializationDispatcherUnity.DEFAULT_EXT)
					//{
					//	menu.AddItem(new GUIContent(Path.GetFileNameWithoutExtension(files[i])), false, (System.Object file_name) =>
					//	{
					//		XSerializationDispatcherUnity.LoadFromXml(file_name.ToString());
					//	}, files[i]);
					//}
				}

				menu.ShowAsContext();
			}
		}
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(2.0f);
	}
	#endregion
}
