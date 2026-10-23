
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusScriptableObjectEditor.cs
*		Редактор для пользовательского ресурса.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;

------------
/// <summary>
/// Редактор для пользовательского ресурса
/// </summary>
------------
[CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
[CanEditMultipleObjects]
public class LotusInspectorScriptableObjectEditor : LotusInspectorBaseEditor
{
}
