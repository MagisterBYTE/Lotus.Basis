
// Проект: Модуль базового ядра Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusCoreSettingsEditor.cs
*		Настройки модуля базового ядра применительно к режиму разработки и редактору Unity.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;

namespace Lotus
{
	namespace Editor
	{

		/** \addtogroup CoreUnity
		*@{*/

		/// <summary>
		/// Настройки модуля базового ядра применительно к режиму разработки и редактору Unity
		/// </summary>

		public static class XCoreSettingsEditor
		{
			/// <summary>
			/// Путь в размещении меню редактора общего модуля (для упорядочивания)
			/// </summary>
			public const String MenuPath = XEditorSettings.MenuPlace + "Core/";

			//
			// ПОДСИСТЕМА УТИЛИТ
			//
			/// <summary>
			/// Последовательность в размещении меню редактора подсистемы утилит модуля базового ядра (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderUtility = XEditorSettings.MenuOrderCore + 50;

			/// <summary>
			/// Путь в размещении меню редактора модуля подсистемы утилит модуля базового ядра  (для упорядочивания)
			/// </summary>
			public const String MenuPathUtility = MenuPath + "BaseUtility/";

			//
			// ПОДСИСТЕМА ЛОКАЛИЗАЦИИ
			//
			/// <summary>
			/// Последовательность в размещении меню редактора подсистемы локализации общего модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderLocalization = XEditorSettings.MenuOrderCore + 100;

			/// <summary>
			/// Путь в размещении меню редактора модуля подсистемы локализации общего модуля (для упорядочивания)
			/// </summary>
			public const String MenuPathLocalization = MenuPath + "Localization/";

			//
			// ПОДСИСТЕМА СЕРИАЛИЗАЦИИ
			//
			/// <summary>
			/// Последовательность в размещении меню редактора подсистемы сериализации общего модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderSerialization = XEditorSettings.MenuOrderCore + 150;

			/// <summary>
			/// Путь в размещении меню редактора модуля подсистемы сериализации общего модуля (для упорядочивания)
			/// </summary>
			public const String MenuPathSerialization = MenuPath + "Serialization/";

			//
			// ПОДСИСТЕМА АНИМАЦИИ
			//
			/// <summary>
			/// Последовательность в размещении меню редактора подсистемы анимации общего модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderTween = XEditorSettings.MenuOrderCore + 200;

			/// <summary>
			/// Путь в размещении меню редактора модуля подсистемы анимации общего модуля (для упорядочивания)
			/// </summary>
			public const String MenuPathTween = MenuPath + "Tween/";

			//
			// ПОСЛЕДНИЕ ПУНКТЫ МЕНЮ
			//
			/// <summary>
			/// Последовательность в размещении меню редактора общего модуля (для упорядочивания в конце)
			/// </summary>
			public const Int32 MenuOrderLast = XEditorSettings.MenuOrderCore + 250;

		}

		/**@}*/

	}
}
