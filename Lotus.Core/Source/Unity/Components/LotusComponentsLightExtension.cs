#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента Light
    /// </summary>
    public static class XLightExtension
    {
        #region Main methods
        /// <summary>
        /// Добавление слоя видимости освещаемых источником света
        /// </summary>
        /// <param name="this">Источник света</param>
        /// <param name="layer">Флаг слоя</param>
        public static void LayerAdd(this Light @this, int layer)
        {
            @this.cullingMask |= (1 << layer);
        }


        /// <summary>
        /// Удаления слоя видимости освещаемых источником света
        /// </summary>
        /// <param name="this">Источник света</param>
        /// <param name="layer">Флаг слоя</param>
        public static void LayerRemove(this Light @this, int layer)
        {
            @this.cullingMask &= ~(1 << layer);
        }
        #endregion
    }
    /**@}*/
}
#endif
