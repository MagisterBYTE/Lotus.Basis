#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента Rigidbody
    /// </summary>
    public static class XRigidbodyExtension
    {
        #region Main methods
        /// <summary>
        /// Зафиксировать вращение/перемещение по всем осям
        /// </summary>
        /// <param name="this">Компонент физики</param>
        public static void FreezeAll(this Rigidbody @this)
        {
            @this.constraints = RigidbodyConstraints.FreezeAll;
        }


        /// <summary>
        /// Зафиксировать перемещение по всем осям
        /// </summary>
        /// <param name="this">Компонент физики</param>
        public static void FreezePosition(this Rigidbody @this)
        {
            @this.constraints = RigidbodyConstraints.FreezePosition;
        }


        /// <summary>
        /// Зафиксировать вращение по всем осям
        /// </summary>
        /// <param name="this">Компонент физики</param>
        public static void FreezeRotation(this Rigidbody @this)
        {
            @this.constraints = RigidbodyConstraints.FreezeRotation;
        }


        /// <summary>
        /// Зафиксировать вращение/перемещение по всем осям, кроме указанных
        /// </summary>
        /// <param name="this">Компонент физики</param>
        /// <param name="except">Исключение компонента фиксации</param>
        public static void FreezeAllExcept(this Rigidbody @this, RigidbodyConstraints except)
        {
            @this.constraints = RigidbodyConstraints.FreezeAll & ~except;
        }
        #endregion
    }
    /**@}*/
}
#endif
