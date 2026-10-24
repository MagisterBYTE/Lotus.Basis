#if UNITY_EDITOR
namespace Lotus.Editor
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
        public const string MenuPath = XEditorSettings.MenuPlace + "Core/";

        //
        // Utility subsystem
        //
        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы утилит модуля базового ядра (для упорядочивания)
        /// </summary>
        public const int MenuOrderUtility = XEditorSettings.MenuOrderCore + 50;

        /// <summary>
        /// Путь в размещении меню редактора модуля подсистемы утилит модуля базового ядра (для упорядочивания)
        /// </summary>
        public const string MenuPathUtility = MenuPath + "BaseUtility/";

        //
        // Localization subsystem
        //
        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы локализации общего модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderLocalization = XEditorSettings.MenuOrderCore + 100;

        /// <summary>
        /// Путь в размещении меню редактора модуля подсистемы локализации общего модуля (для упорядочивания)
        /// </summary>
        public const string MenuPathLocalization = MenuPath + "Localization/";

        //
        // Serialization subsystem
        //
        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы сериализации общего модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderSerialization = XEditorSettings.MenuOrderCore + 150;

        /// <summary>
        /// Путь в размещении меню редактора модуля подсистемы сериализации общего модуля (для упорядочивания)
        /// </summary>
        public const string MenuPathSerialization = MenuPath + "Serialization/";

        //
        // Tween subsystem
        //
        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы анимации общего модуля (для упорядочивания)
        /// </summary>
        public const int MenuOrderTween = XEditorSettings.MenuOrderCore + 200;

        /// <summary>
        /// Путь в размещении меню редактора модуля подсистемы анимации общего модуля (для упорядочивания)
        /// </summary>
        public const string MenuPathTween = MenuPath + "Tween/";

        //
        // Last menu items
        //
        /// <summary>
        /// Последовательность в размещении меню редактора общего модуля (для упорядочивания в конце)
        /// </summary>
        public const int MenuOrderLast = XEditorSettings.MenuOrderCore + 250;
    }
    /**@}*/
}
#endif
