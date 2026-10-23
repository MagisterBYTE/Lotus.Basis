
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSystemDispatcherEditor.cs
*		Редактор основного системного диспетчера Lotus.
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
/// Редактор основного системного диспетчера Lotus
/// </summary>
------------
[CustomEditor(typeof(LotusSystemDispatcher))]
public class LotusSystemDispatcherEditor : Editor
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Добавления системного диспетчера в сцену
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPath + "Create SystemDispatcher", false, XCoreSettingsEditor.MenuOrderLast + 1)]
	public static void CreateSystemDispatcher()
	{
		LotusSystemDispatcher ld = LotusSystemDispatcher.Instance;
		ld.gameObject.layer = XLayer.DefaultId;
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private LotusSystemDispatcher mDispatcher;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mDispatcher = this.target as LotusSystemDispatcher;
	}

	--------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	--------
	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		{
			GUILayout.Space(4.0f);
			XEditorInspector.DrawGroup("Singleton settings");
			{
				GUILayout.Space(2.0f);
				mDispatcher.IsMainInstance = XEditorInspector.PropertyBoolean(nameof(mDispatcher.IsMainInstance), mDispatcher.IsMainInstance);

				GUILayout.Space(2.0f);
				EditorGUI.BeginDisabledGroup(mDispatcher.IsMainInstance);
				{
					mDispatcher.DestroyMode = XEditorInspector.PropertyEnum(nameof(mDispatcher.DestroyMode), mDispatcher.DestroyMode);
				}
				EditorGUI.EndDisabledGroup();

				GUILayout.Space(2.0f);
				mDispatcher.IsDontDestroy = XEditorInspector.PropertyBoolean(nameof(mDispatcher.IsDontDestroy), mDispatcher.IsDontDestroy);
			}

			GUILayout.Space(4.0f);
			XEditorInspector.DrawGroup("Settings 2D Core");
			{
				GUILayout.Space(2.0f);
				mDispatcher.UseIMGUI = XEditorInspector.PropertyBoolean(nameof(mDispatcher.UseIMGUI), mDispatcher.UseIMGUI);

				if(mDispatcher.UseIMGUI)
				{
					GUILayout.Space(2.0f);
					mDispatcher.CurrentSkin = XEditorInspector.PropertyResource("CurrentSkin", mDispatcher.CurrentSkin);
				}
			}

			XEditorInspector.DrawGroup("Parameters console");
			{
				GUILayout.Space(2.0f);
				mDispatcher.ConsoleVisible = XEditorInspector.PropertyBoolean(nameof(mDispatcher.ConsoleVisible), mDispatcher.ConsoleVisible);

				GUILayout.Space(2.0f);
				mDispatcher.ConsoleHeight = XEditorInspector.PropertyFloat(nameof(mDispatcher.ConsoleHeight), mDispatcher.ConsoleHeight);
			}

			XEditorInspector.DrawGroup("Parameters fps");
			{
				GUILayout.Space(2.0f);
				mDispatcher.IsFpsShow = XEditorInspector.PropertyBoolean(nameof(mDispatcher.IsFpsShow), mDispatcher.IsFpsShow);

				GUILayout.Space(2.0f);
				mDispatcher.FpsUpdateInterval = XEditorInspector.PropertyFloatSlider(nameof(mDispatcher.FpsUpdateInterval), mDispatcher.FpsUpdateInterval, 0.1f, 2.0f);

				GUILayout.Space(2.0f);
				mDispatcher.FpsPosition = XEditorInspector.PropertyRect(nameof(mDispatcher.FpsPosition), mDispatcher.FpsPosition);
			}
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.Save();
		}

		GUILayout.Space(2.0f);
	}
	#endregion
}
