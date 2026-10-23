#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityConstants Подсистема констант Unity
     * \ingroup Core
     * \brief Подсистема констант реализует общие константы в Unity.
     * @{
     */
    /// <summary>
    /// Статический класс содержащий константы используемых осей ввода Unity
    /// </summary>
    public static class XAxis
    {
        /// <summary>
        /// Ось - Horizontal
        /// </summary>
        public const string HORIZONTAL = "Horizontal";

        /// <summary>
        /// Ось - Vertical
        /// </summary>
        public const string VERTICAL = "Vertical";

        /// <summary>
        /// Ось - Fire1
        /// </summary>
        public const string FIRE1 = "Fire1";

        /// <summary>
        /// Ось - Fire2
        /// </summary>
        public const string FIRE2 = "Fire2";

        /// <summary>
        /// Ось - Fire3
        /// </summary>
        public const string FIRE3 = "Fire3";

        /// <summary>
        /// Ось - Jump
        /// </summary>
        public const string JUMP = "Jump";

        /// <summary>
        /// Ось - Mouse X
        /// </summary>
        public const string MOUSE_X = "Mouse X";

        /// <summary>
        /// Ось - Mouse Y
        /// </summary>
        public const string MOUSE_Y = "Mouse Y";

        /// <summary>
        /// Ось - Mouse ScrollWheel
        /// </summary>
        public const string MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";

        /// <summary>
        /// Ось - Submit
        /// </summary>
        public const string SUBMIT = "Submit";

        /// <summary>
        /// Ось - Cancel
        /// </summary>
        public const string CANCEL = "Cancel";
    }
    /**@}*/
}
#endif
