
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusTweenDispatcherEditor.cs
*		Редактор центрального диспетчера для хранения и управления ресурсами анимации.
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
/// Редактор центрального диспетчера для хранения и управления ресурсами анимации
/// </summary>
------------
[CustomEditor(typeof(LotusTweenDispatcher))]
public class LotusTweenDispatcherEditor : Editor
{
	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	--------
	/// <summary>
	/// Создания основного сервиса для локализации текстовых данных
	/// </summary>
	--------
	[MenuItem(XCoreSettingsEditor.MenuPathTween + "Create Tween Dispatcher", false, XCoreSettingsEditor.MenuOrderTween)]
	public static void Create()
	{
#pragma warning disable 0219
		LotusTweenDispatcher tween_dispatcher = LotusTweenDispatcher.Instance;
#pragma warning restore 0219
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private LotusTweenDispatcher mDispatcher;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mDispatcher = this.target as LotusTweenDispatcher;
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

			XEditorInspector.DrawGroup("Tween settings");
			{
				GUILayout.Space(2.0f);
				mDispatcher._curveStorage = XEditorInspector.PropertyResource(nameof(LotusTweenDispatcher.CurveStorage), 
					mDispatcher._curveStorage);

				GUILayout.Space(2.0f);
				mDispatcher._spriteStorage = XEditorInspector.PropertyResource(nameof(LotusTweenDispatcher.SpriteStorage),
					mDispatcher._spriteStorage);
			}
		}
		if (EditorGUI.EndChangeCheck())
		{
			mDispatcher.SaveInEditor();
		}

		GUILayout.Space(2.0f);
	}
	#endregion
}
