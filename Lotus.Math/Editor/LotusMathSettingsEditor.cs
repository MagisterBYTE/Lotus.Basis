#if UNITY_EDITOR
namespace Lotus.Editor
{
    /**
     * \defgroup MathUnityEditor Подсистема расширения функциональности редактора Unity математического модуля
     * \ingroup Math
     * \brief Редакторы инспектора и инструменты Scene View для математического модуля.
     * @{
     */
    /// <summary>
    /// Настройки модуля математической системы применительно к режиму разработки и редактору Unity
    /// </summary>
    public static class XMathSettingsEditor
    {
        /// <summary>
        /// Путь в размещении меню редактора модуля математической системы (для упорядочивания)
        /// </summary>
        public const string MenuPath = XEditorSettings.MenuPlace + "Math/";

        /// <summary>
        /// Последовательность в размещении меню редактора модуля математической системы (для упорядочивания)
        /// </summary>
        public const int MenuOrderMath = XEditorSettings.MenuOrderMath;

        /// <summary>
        /// Последовательность в размещении меню редактора подсистемы сплайнов (для упорядочивания)
        /// </summary>
        public const int MenuOrderSpline = XEditorSettings.MenuOrderMath + 50;

        /// <summary>
        /// Последовательность в размещении меню редактора математического модуля (для упорядочивания в конце)
        /// </summary>
        public const int MenuOrderLast = XEditorSettings.MenuOrderMath + 250;
    }
    /**@}*/
}
#endif
