#if UNITY_EDITOR
using System.Runtime.CompilerServices;

using Lotus.Editor;
// Обеспечивает дружественность для сборки редакторов
[assembly: InternalsVisibleTo("Lotus.Core.Editor")]

namespace Lotus.Algorithm
{
    /** \addtogroup Algorithm
    *@{*/
    /// <summary>
    /// Настройки модуля алгоритмов применительно к режиму разработки и редактору Unity
    /// </summary>
    public static class XAlgorithmSettingsEditor
    {
        /// <summary>
        /// Путь в размещении меню редактора модуля алгоритмов (для упорядочивания)
        /// </summary>
        public const string MenuPath = XEditorSettings.MenuPlace + "Algorithm/";

        //
        // ЭЛЕМЕНТЫ
        //
        /// <summary>
        /// Относительный путь директории исходного кода подсистемы элементов
        /// </summary>
        public const string SourceElementPath = XEditorSettings.SourceAlgorithmPath + "Element/";

        /// <summary>
        /// Путь в размещении меню редактора подсистемы элементов (для упорядочивания)
        /// </summary>
        public const string MenuElementPath = MenuPath + "Element/";

        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы элементов (для упорядочивания)
        /// </summary>
        public const int MenuOrderElement = XEditorSettings.MenuOrderAlgorithm + 10;

        //
        // ПОСЛЕДНИЕ ПУНКТЫ МЕНЮ
        //
        /// <summary>
        /// Последовательность в размещении меню редактора модуля элементов (для упорядочивания в конце)
        /// </summary>
        public const int MenuOrderLast = XEditorSettings.MenuOrderAlgorithm + 100;
    }
    /**@}*/
}
#endif