#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityCamera Подсистема управления камерами Unity
     * \ingroup Core
     * \brief Подсистема управления камерами реализует компоненты для типового и универсального управления камерами.
        Специализированные камеры будут реализованы в соответствующих подсистемах.
     * @{
     */
    /// <summary>
    /// Статический класс определения данных ввода для управления камерами
    /// </summary>
    public static class XCameraInput
    {
        /// <summary>
        /// Название оси для управления в горизонтальной плоскости
        /// </summary>
        public static string AxisHorizontalName = XAxis.MOUSE_X;

        /// <summary>
        /// Название оси для управления в вертикальной плоскости
        /// </summary>
        public static string AxisVerticalName = XAxis.MOUSE_Y;

        /// <summary>
        /// Название оси для управления приближением камеры
        /// </summary>
        public static string AxisOrbitName = XAxis.MOUSE_SCROLL_WHEEL;
    }
    /**@}*/
}
#endif
