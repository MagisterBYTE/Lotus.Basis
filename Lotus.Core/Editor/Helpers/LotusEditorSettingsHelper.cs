
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusEditorSettingsHelper.cs
*		Настройки касающиеся в целом редактора Unity.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;

namespace Lotus
{
	namespace Editor
	{
        ----
        /** 
         * \defgroup CoreUnityEditor Подсистема расширения функциональности редактора Unity
         * \ingroup Core
         * \brief Подсистема поддержки редактора Unity обеспечивает расширение функциональности редактора Unity и его 
			служебных классов, является базой для построения собственных редакторов и иных вспомогательных инструментов.
         * @{
         */

		/// <summary>
		/// Статический класс для определения настроек касающиеся в целом редактора Unity
		/// </summary>
		/// <remarks>
		/// Определяет данные для местоположения директории Lotus и размещения и упорядочивания элементов меню Lotus
		/// </remarks>

		public static class XEditorSettings
		{
			#region ======================================= СТАНДАРТНЫЕ ПУТИ ==========================================
			/// <summary>
			/// Базовая директория проекта
			/// </summary>
			public const String ASSETS_PATH = "Assets/";

			/// <summary>
			/// Основной путь к директории Lotus
			/// </summary>
			/// <remarks>
			/// По умолчанию директория располагается в корне проекта. Если нужно перенести в другую директорию, 
			/// здесь нужно прописать путь. Например "Assets/Plugins/"
			/// </remarks>
			public const String MainPath = "Assets/";

			/// <summary>
			/// Директория для автосохранения
			/// </summary>
			/// <remarks>
			/// Это директория для автосохранения и автозагрузки данных подсистемы сериализации
			/// </remarks>
			public const String AutoSavePath = "Assets/AutoSave";
			#endregion

			#region ======================================= БАЗОВЫЕ ПУТИ К ИСХОДНОМУ КОДУ =============================
			/// <summary>
			/// Относительный путь директории исходного кода платформы Lotus
			/// </summary>
			public const String SourcePath = "Runtime/";

			/// <summary>
			/// Относительный путь директории исходного кода модуля Core
			/// </summary>
			public const String SourceCorePath = MainPath + "Lotus.Core/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода модуля Math
			/// </summary>
			public const String SourceMathPath = MainPath + "Lotus.Math/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода модуля Abstractions
			/// </summary>
			public const String SourceAbstractionsPath = MainPath + "Lotus.Abstractions/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода модуля Algorithm
			/// </summary>
			public const String SourceAlgorithmPath = MainPath + "Lotus.Algorithm/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода набора Graphics2D
			/// </summary>
			public const String SourceGraphics2DPath = MainPath + "Lotus.Graphics2D/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода модуля Object3D
			/// </summary>
			public const String SourceObject3DPath = MainPath + "Lotus.Object3D/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода набора VisualEffects
			/// </summary>
			public const String SourceGraphics3DPath = MainPath + "Lotus.VisualEffects/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода набора Environment
			/// </summary>
			public const String SourceEnvironmentPath = MainPath + "Lotus.Environment/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода набора Person
			/// </summary>
			public const String SourceCharacterPath = MainPath + "Lotus.Character/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода набора Transport
			/// </summary>
			public const String SourceTransportPath = MainPath + "Lotus.Transport/" + SourcePath;

			/// <summary>
			/// Относительный путь директории исходного кода модуля Service
			/// </summary>
			public const String SourceServicePath = MainPath + "Lotus.Service/" + SourcePath;
			#endregion

			#region ======================================= БАЗОВЫЕ ПУТИ К РЕСУРСАМ ===================================
			/// <summary>
			/// Базовая директория для загрузки ресурсов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesPath = MainPath + "Lotus/Resources/";

			/// <summary>
			/// Директория для загрузки данных в режиме редактора
			/// </summary>
			public const String ResourcesDataPath = ResourcesPath + "Data/";

			/// <summary>
			/// Директория для загрузки аудиоресурсов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesAudioPath = ResourcesPath + "Audio/";

			/// <summary>
			/// Директория для загрузки шрифтов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesFontsPath = ResourcesPath + "Fonts/";

			/// <summary>
			/// Директория для загрузки моделей Lotus в режиме редактора
			/// </summary>
			public const String ResourcesModelsPath = ResourcesPath + "Models/";

			/// <summary>
			/// Директория для загрузки шейдеров Lotus в режиме редактора
			/// </summary>
			public const String ResourcesShadersPath = ResourcesPath + "Shaders/";

			/// <summary>
			/// Директория для загрузки спрайтов анимации Lotus в режиме редактора
			/// </summary>
			public const String ResourcesSpriteSheetsPath = ResourcesPath + "SpriteSheets/";

			/// <summary>
			/// Директория для загрузки текстур Lotus в режиме редактора
			/// </summary>
			public const String ResourcesTexturesPath = ResourcesPath + "Textures/";

			/// <summary>
			/// Директория для загрузки ресурсов пользовательского интерфейса Lotus в режиме редактора
			/// </summary>
			public const String ResourcesUIPath = ResourcesPath + "UI/";
			#endregion

			#region ======================================= РАЗМЕЩЕНИЕ МЕНЮ ===========================================
			/// <summary>
			/// Размещение основного меню
			/// </summary>
			/// <remarks>
			/// По умолчанию меню располагается в корне. Если его нужно перенесите в подменю, 
			/// здесь нужно прописать путь. Например "GameObject/Lotus"
			/// </remarks>
			public const String MenuPlace = "Lotus/";

			/// <summary>
			/// Последовательность в размещении меню редактора последних элементов 
			/// </summary>
			public const Int32 MenuOrderLast = 100000;
			#endregion

			#region ======================================= РАЗМЕЩЕНИЕ МЕНЮ МОДУЛЕЙ ===================================
			/// <summary>
			/// Последовательность в размещении меню редактора базового модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderCore = 0;

			/// <summary>
			/// Последовательность в размещении меню редактора математического модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderMath = 1000;

			/// <summary>
			/// Последовательность в размещении меню редактора модуля абстракций (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderAbstractions = 2000;

			/// <summary>
			/// Последовательность в размещении меню редактора модуля алгоритмов (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderAlgorithm = 3000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы с 2D контентом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderGraphics2D = 4000;

			/// <summary>
			/// Последовательность в размещении меню редактора модуля для работы с 3D контентом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderObject3D = 5000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы с визуальными эффектами (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderVisualEffects = 6000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для разработки внешней среды (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderEnvironment = 7000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы c персонажем (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderCharacter = 8000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы c транспортом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderTransport = 9000;

			/// <summary>
			/// Последовательность в размещении меню редактора служебного модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderService = 10000;
			#endregion
		}

		/**@}*/

	}
}
