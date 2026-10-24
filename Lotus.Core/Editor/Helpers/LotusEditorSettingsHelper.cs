#if UNITY_EDITOR
using System;

namespace Lotus.Editor
{
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
        #region Path constants
        /// <summary>
        /// Базовая директория проекта
        /// </summary>
        public const string ASSETS_PATH = "Assets/";

        /// <summary>
        /// Основной путь к директории Lotus
        /// </summary>
        /// <remarks>
        /// По умолчанию директория располагается в корне проекта. Если нужно перенести в другую директорию,
        /// здесь нужно прописать путь. Например "Assets/Plugins/"
        /// </remarks>
        public const string MainPath = "Assets/";

        /// <summary>
        /// Директория для автосохранения
        /// </summary>
        /// <remarks>
        /// Это директория для автосохранения и автозагрузки данных подсистемы сериализации
        /// </remarks>
        public const string AutoSavePath = "Assets/AutoSave";
        #endregion

        #region Source paths
        /// <summary>
        /// Относительный путь директории исходного кода платформы Lotus
        /// </summary>
        public const string SourcePath = "Source/";

        /// <summary>
        /// Относительный путь директории исходного кода модуля Core
        /// </summary>
        public const string SourceCorePath = MainPath + "Lotus.Core/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода модуля Math
        /// </summary>
        public const string SourceMathPath = MainPath + "Lotus.Math/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода модуля Abstractions
        /// </summary>
        public const string SourceAbstractionsPath = MainPath + "Lotus.Abstractions/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода модуля Algorithm
        /// </summary>
        public const string SourceAlgorithmPath = MainPath + "Lotus.Algorithm/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода набора Graphics2D
        /// </summary>
        public const string SourceGraphics2DPath = MainPath + "Lotus.Graphics2D/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода модуля Object3D
        /// </summary>
        public const string SourceObject3DPath = MainPath + "Lotus.Object3D/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода набора VisualEffects
        /// </summary>
        public const string SourceGraphics3DPath = MainPath + "Lotus.VisualEffects/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода набора Environment
        /// </summary>
        public const string SourceEnvironmentPath = MainPath + "Lotus.Environment/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода набора Person
        /// </summary>
        public const string SourceCharacterPath = MainPath + "Lotus.Character/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода набора Transport
        /// </summary>
        public const string SourceTransportPath = MainPath + "Lotus.Transport/" + SourcePath;

        /// <summary>
        /// Относительный путь директории исходного кода модуля Service
        /// </summary>
        public const string SourceServicePath = MainPath + "Lotus.Service/" + SourcePath;
        #endregion

        #region Resource paths
        /// <summary>
        /// Базовая директория для загрузки ресурсов Lotus в режиме редактора
        /// </summary>
        public const string ResourcesPath = MainPath + "Lotus/Resources/";

        /// <summary>
        /// Директория для загрузки данных в режиме редактора
        /// </summary>
        public const string ResourcesDataPath = ResourcesPath + "Data/";

        /// <summary>
        /// Директория для загрузки аудиоресурсов Lotus в режиме редактора
        /// </summary>
        public const string ResourcesAudioPath = ResourcesPath + "Audio/";

        /// <summary>
        /// Директория для загрузки шрифтов Lotus в режиме редактора
        /// </summary>
        public const string ResourcesFontsPath = ResourcesPath + "Fonts/";

        /// <summary>
        /// Директория для загрузки моделей Lotus в режиме редактора
        /// </summary>
        public const string ResourcesModelsPath = ResourcesPath + "Models/";

        /// <summary>
        /// Директория для загрузки шейдеров Lotus в режиме редактора
        /// </summary>
        public const string ResourcesShadersPath = ResourcesPath + "Shaders/";

        /// <summary>
        /// Директория для загрузки спрайтов анимации Lotus в режиме редактора
        /// </summary>
        public const string ResourcesSpriteSheetsPath = ResourcesPath + "SpriteSheets/";

        /// <summary>
        /// Директория для загрузки текстур Lotus в режиме редактора
        /// </summary>
        public const string ResourcesTexturesPath = ResourcesPath + "Textures/";

        /// <summary>
        /// Директория для загрузки ресурсов пользовательского интерфейса Lotus в режиме редактора
        /// </summary>
        public const string ResourcesUIPath = ResourcesPath + "UI/";
        #endregion

        #region Menu placement
        /// <summary>
        /// Размещение основного меню
        /// </summary>
        /// <remarks>
        /// По умолчанию меню располагается в корне. Если его нужно перенести в подменю,
        /// здесь нужно прописать путь. Например "GameObject/Lotus"
        /// </remarks>
        public const string MenuPlace = "Lotus/";

        /// <summary>
        /// Последовательность в размещении меню редактора последних элементов
        /// </summary>
        public const int MenuOrderLast = 100000;
        #endregion

        #region Module menu order
        /// <summary>
        /// Последовательность в размещении меню редактора базового модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderCore = 0;

        /// <summary>
        /// Последовательность в размещении меню редактора математического модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderMath = 1000;

        /// <summary>
        /// Последовательность в размещении меню редактора модуля абстракций (для упорядочивания)
        /// </summary>
        public const int MenuOrderAbstractions = 2000;

        /// <summary>
        /// Последовательность в размещении меню редактора модуля алгоритмов (для упорядочивания)
        /// </summary>
        public const int MenuOrderAlgorithm = 3000;

        /// <summary>
        /// Последовательность в размещении меню редактора набора для работы с 2D контентом (для упорядочивания)
        /// </summary>
        public const int MenuOrderGraphics2D = 4000;

        /// <summary>
        /// Последовательность в размещении меню редактора модуля для работы с 3D контентом (для упорядочивания)
        /// </summary>
        public const int MenuOrderObject3D = 5000;

        /// <summary>
        /// Последовательность в размещении меню редактора набора для работы с визуальными эффектами (для упорядочивания)
        /// </summary>
        public const int MenuOrderVisualEffects = 6000;

        /// <summary>
        /// Последовательность в размещении меню редактора набора для разработки внешней среды (для упорядочивания)
        /// </summary>
        public const int MenuOrderEnvironment = 7000;

        /// <summary>
        /// Последовательность в размещении меню редактора набора для работы c персонажем (для упорядочивания)
        /// </summary>
        public const int MenuOrderCharacter = 8000;

        /// <summary>
        /// Последовательность в размещении меню редактора набора для работы c транспортом (для упорядочивания)
        /// </summary>
        public const int MenuOrderTransport = 9000;

        /// <summary>
        /// Последовательность в размещении меню редактора служебного модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderService = 10000;
        #endregion
    }

    /**@}*/
}
#endif
