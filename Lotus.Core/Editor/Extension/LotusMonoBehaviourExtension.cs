
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusMonoBehaviourExtension.cs
*		Методы расширений базового компонента логики MonoBehaviour.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;

namespace Lotus
{
	namespace Editor
	{

		/** \addtogroup CoreUnityEditor
		*@{*/

		/// <summary>
		/// Статический класс реализующий методы расширения базового компонента логики MonoBehaviour
		/// </summary>
		/// <remarks>
		/// Только для режима редактора Unity
		/// </remarks>

		public static class XMonoBehaviourEditorExtension
		{
			
			/// <summary>
			/// Сохранение данных компонента в режиме редактора
			/// </summary>
			/// <param name="this">Базовый компонент логики</param>
			
			public static void SaveInEditor(this UnityEngine.MonoBehaviour @this)
			{
				UnityEditor.EditorUtility.SetDirty(@this);
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
					UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(@this.gameObject.scene);
				}
			}
		}

		/**@}*/

	}
}
