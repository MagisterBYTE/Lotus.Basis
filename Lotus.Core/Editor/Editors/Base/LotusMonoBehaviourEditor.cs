
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusMonoBehaviourEditor.cs
*		Редактор для пользовательского скрипта.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;

------------
/// <summary>
/// Редактор для пользовательского скрипта
/// </summary>
------------
[CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
[CanEditMultipleObjects]
public class LotusInspectorMonoBehaviourEditor : LotusInspectorBaseEditor
{
}
