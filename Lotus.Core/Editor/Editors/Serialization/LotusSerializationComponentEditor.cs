
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSerializationComponentEditor.cs
*		Редактор компонента для сериализации данных.
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
/// Редактор компонента для сериализации данных
/// </summary>
------------
[CustomEditor(typeof(LotusSerializationComponent))]
public class LotusSerializationComponentEditor : Editor
{
	#region =============================================== ДАННЫЕ ====================================================
	private LotusSerializationComponent mSerialization;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	--------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	--------
	public void OnEnable()
	{
		mSerialization = this.target as LotusSerializationComponent;
	}

	--------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	--------
	public override void OnInspectorGUI()
	{
		GUILayout.Space(4.0f);
		EditorGUI.BeginChangeCheck();
		{
			GUILayout.Space(2.0f);
			EditorGUILayout.BeginHorizontal();
			XEditorInspector.DrawGroup("IDSerialKey = " + mSerialization.IDKeySerial.ToString());
			if (GUILayout.Button("Generate"))
			{
				mSerialization.IDKeySerial = XGenerateId.Generate(mSerialization);
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Space(4.0f);
			mSerialization.SerializationVolume = (TSerializationVolume)XEditorInspector.PropertyFlags("SerialVolume", mSerialization.SerializationVolume);
		}
		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.Save();
		}

		GUILayout.Space(2.0f);
	}
	#endregion
}
