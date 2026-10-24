using System;
using System.Runtime.CompilerServices;

// Обеспечивает дружественность для сборки редакторов
[assembly: InternalsVisibleToAttribute("Assembly-CSharp-Editor")]
[assembly: InternalsVisibleToAttribute("Lotus.Core.Editor")]
namespace Lotus.Core
{
    /** \addtogroup Core
    *@{*/
    /// <summary>
    /// Статический класс для определения настроек модуля базового ядра
    /// </summary>
    public static class XCoreSettings
    {
        /// <summary>
        /// Базовая директория проекта
        /// </summary>
        public const string ASSETS_PATH = "Assets/";

        /// <summary>
        /// Директория для автосохранения
        /// </summary>
        /// <remarks>
        /// Это директория для автосохранения и автозагрузки данных подсистемы сериализации
        /// </remarks>
        public const string AutoSavePath = "Assets/AutoSave";

        /// <summary>
        /// Директория для загрузки данных
        /// </summary>
        public const string ResourcesData = "Data/";

        /// <summary>
        /// Директория для загрузки шейдеров
        /// </summary>
        public const string ResourcesShaders = "Shaders/Debug/";
    }
    /**@}*/
}