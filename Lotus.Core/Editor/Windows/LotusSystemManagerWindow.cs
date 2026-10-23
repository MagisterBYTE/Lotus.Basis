
// Проект: Модуль базового ядра Unity
// Раздел: Window утилиты Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSystemManagerWindow.cs
*		Редактор для управления основными системными параметрами и настройки системы Lotus.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;
using Lotus.Editor;

------------
/// <summary>
/// Редактор для управления основными системными параметрами и настройки системы Lotus
/// </summary>
------------
public class LotusSystemManagerWindow : LotusUtilityBaseWindow
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Показать окно редактора для управления основными системными параметрами и настройки системы Lotus
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathUtility + "System Parameters", false, XCoreSettingsEditor.MenuOrderUtility + 5)]
	public static void ShowSystemManager()
	{
		var window = GetWindow<LotusSystemManagerWindow>();
		window.titleContent.text = "System Manager";

		window.OnConstruct();
		window.Show();
	}

	--------
	/// <summary>
	/// Удалить потерянные компоненты с выбранного игрового объекта
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPath + "Remove Missing GameObject", false, XCoreSettingsEditor.MenuOrderUtility + 1000)]
	private static void FindAndRemoveMissingInSelected()
	{
		var deep_selection = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);
		var comp_count = 0;
		var go_count = 0;
		foreach (var o in deep_selection)
		{
			if (o is GameObject go)
			{
				var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
				if (count > 0)
				{
					// Edit: use undo record object, since undo destroy wont work with missing
					Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
					GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
					comp_count += count;
					go_count++;
				}
			}
		}
		Debug.Log($"Found and removed {comp_count} missing scripts from {go_count} GameObjects");
	}

	--------
	/// <summary>
	/// Удалить потерянные компоненты со всех объектов сцены
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPath + "Remove Missing All", false, XCoreSettingsEditor.MenuOrderUtility + 1001)]
	private static void FindAndRemoveMissingInAll()
	{
		var deep_selection = EditorUtility.CollectDeepHierarchy(UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects());
		var comp_count = 0;
		var go_count = 0;
		foreach (var o in deep_selection)
		{
			if (o is GameObject go)
			{
				var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
				if (count > 0)
				{
					// Edit: use undo record object, since undo destroy wont work with missing
					Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
					GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
					comp_count += count;
					go_count++;
				}
			}
		}

		Debug.Log($"Found and removed {comp_count} missing scripts from {go_count} GameObjects");
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Установка порядка исполнения скриптов
	private Int32 mExecutionOrderStart;
	private String mExecutionOrderInfo;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Активация окна
	/// </summary>
	--------
	public void OnEnable()
	{
		
	}
	#endregion

	#region =============================================== ОБШИЕ МЕТОДЫ ==============================================
	--------
	/// <summary>
	/// Конструирование окна
	/// </summary>
	--------
	public void OnConstruct()
	{
		LoadTabAndDesc();
		IsExplore = false;
	}

	--------
	/// <summary>
	/// Установка правильного порядка исполнения скриптов
	/// </summary>
	--------
	public void SetExecutionOrder()
	{
		// Iterate through all scripts (Might be a better way to do this?)
		foreach (MonoScript mono_script in MonoImporter.GetAllRuntimeMonoScripts())
		{
			Type type = mono_script.GetClass();
			if (type != null)
			{
				LotusExecutionOrderAttribute attribute = type.GetAttribute<LotusExecutionOrderAttribute>();
				if (attribute != null)
				{
					var execution_order = attribute.ExecutionOrder;
					MonoImporter.SetExecutionOrder(mono_script, execution_order + mExecutionOrderStart);
				}
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
	public override void DrawContent()
	{
		switch (mSelectedTab)
		{
			case 0:
				{
					DrawExecutionOrder();
				}
				break;
			case 1:
				{
					DrawSettings();
				}
				break;
			case 2:
				{
					DrawExport();
				}
				break;
			default:
				break;
		}
	}

	--------
	/// <summary>
	/// Рисование параметров порядка исполнения скриптов
	/// </summary>
	--------
	public void DrawExecutionOrder()
	{
		GUILayout.Space(4);
		mExecutionOrderStart = XEditorInspector.PropertyInt("Start Offset", mExecutionOrderStart);

		GUILayout.Space(4);
		if (GUILayout.Button("SetExecutionOrder"))
		{
			SetExecutionOrder();
		}

		mExecutionOrderInfo = "Скрипты системы Lotus исполняются в порядке от: " + mExecutionOrderStart.ToString() +
			" до " + (5000 + mExecutionOrderStart).ToString();

		GUILayout.Space(4);
		GUILayout.Label(mExecutionOrderInfo, EditorStyles.boldLabel);
	}

	--------
	/// <summary>
	/// Рисование настроек
	/// </summary>
	--------
	public void DrawSettings()
	{
		GUILayout.Space(4);
		GUILayout.Label("Все настройки Lotus расположены в файле LotusUnityEditorSettings", EditorStyles.boldLabel);
	}

	--------
	/// <summary>
	/// Рисование параметров экспорта
	/// </summary>
	--------
	public void DrawExport()
	{
		//String path_to_export = "C:/WORK/Release/";
		//String exc = ".unitypackage";
		//String example = ".Example";

		//Version basis_version = new Version(0, 0, 0, 1);
		//String path_example_basis = "Assets/LotusExample/Basis/";

		//String core_source = XCoreEditorSettings.SourcePath.RemoveLastOccurrence("/");
		//String core_name = "Lotus.Core";
		//String core_file = core_name + " - " + basis_version.ToString() + exc;
		//String core_example_source = path_example_basis + "Core";
		//String core_example_name = core_name + example;
		//String core_example_file = core_example_name + " - " + basis_version.ToString() + exc;

		//GUILayout.Space(4);
		//GUILayout.Label("Модуль Basis", EditorStyles.largeLabel);
		//GUILayout.Space(4);
		//if (GUILayout.Button("Экспорт модуля Core", GUILayout.Height(30)))
		//{
		//	AssetDatabase.ExportPackage(core_source, path_to_export + core_file, ExportPackageOptions.Recurse);
		//	AssetDatabase.ExportPackage(core_example_source, path_to_export + core_example_file, ExportPackageOptions.Recurse);
		//}

		//String math_source = XMathEditorSettings.SourcePath.RemoveLastOccurrence("/");
		//String math_name = "Lotus.Math";
		//String math_file = math_name + " - " + basis_version.ToString() + exc;
		//String math_example_source = path_example_basis + "Math";
		//String math_example_name = math_name + example;
		//String math_example_file = math_example_name + " - " + basis_version.ToString() + exc;

		//GUILayout.Space(4);
		//if (GUILayout.Button("Экспорт модуля Math", GUILayout.Height(30)))
		//{
		//	AssetDatabase.ExportPackage(math_source, path_to_export + math_file, ExportPackageOptions.Recurse);
		//	AssetDatabase.ExportPackage(math_example_source, path_to_export + math_example_file, ExportPackageOptions.Recurse);
		//}


		//String algo_source = XAlgorithmEditorSettings.SourcePath.RemoveLastOccurrence("/");
		//String algo_name = "Lotus.Algorithm";
		//String algo_file = algo_name + " - " + basis_version.ToString() + exc;
		//String algo_example_source = path_example_basis + "Algorithm";
		//String algo_example_name = algo_name + example;
		//String algo_example_file = algo_example_name + " - " + basis_version.ToString() + exc;

		//GUILayout.Space(4);
		//if (GUILayout.Button("Экспорт модуля Algorithm", GUILayout.Height(30)))
		//{
		//	AssetDatabase.ExportPackage(algo_source, path_to_export + algo_file, ExportPackageOptions.Recurse);
		//	AssetDatabase.ExportPackage(algo_example_source, path_to_export + algo_example_file, ExportPackageOptions.Recurse);
		//}

		//String common_source = XCommonEditorSettings.SourcePath.RemoveLastOccurrence("/");
		//String common_name = "Lotus.Common";
		//String common_file = common_name + " - " + basis_version.ToString() + exc;
		//String common_example_source = path_example_basis + "Common";
		//String common_example_name = common_name + example;
		//String common_example_file = common_example_name + " - " + basis_version.ToString() + exc;

		//GUILayout.Space(4);
		//if (GUILayout.Button("Экспорт модуля Common", GUILayout.Height(30)))
		//{
		//	AssetDatabase.ExportPackage(common_source, path_to_export + common_file, ExportPackageOptions.Recurse);
		//	AssetDatabase.ExportPackage(common_example_source, path_to_export + common_example_file, ExportPackageOptions.Recurse);
		//}
	}
	#endregion
}
