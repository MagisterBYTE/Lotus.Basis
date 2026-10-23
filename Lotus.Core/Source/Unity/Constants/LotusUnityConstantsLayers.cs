#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityConstants
    *@{*/
    /// <summary>
    /// Статический класс содержащий константы используемых слоев расположения объектов Unity
    /// </summary>
    public static class XLayer
    {
        #region Name constants
        /// <summary>
        /// Слой по умолчанию
        /// </summary>
        public const string DEFAULT = "Default";

        /// <summary>
        /// Слой для прозрачных объектов
        /// </summary>
        public const string TRANSPARENT_FX = "TransparentFX";

        /// <summary>
        /// Слой для объектов которые игнорируют коллизию
        /// </summary>
        public const string IGNORE_RAYCAST = "Ignore Raycast";

        /// <summary>
        /// Слой для объектов которые представляют собой воду
        /// </summary>
        public const string WATER = "Water";

        /// <summary>
        /// Слой для объектов пользовательского интерфейса
        /// </summary>
        public const string UI = "UI";
        #endregion

        #region Id constants
        /// <summary>
        /// Слой по умолчанию
        /// </summary>
        public static readonly int DefaultId = LayerMask.NameToLayer(DEFAULT);

        /// <summary>
        /// Слой для прозрачных объектов
        /// </summary>
        public static readonly int TransparentFxId = LayerMask.NameToLayer(TRANSPARENT_FX);

        /// <summary>
        /// Слой для объектов которые игнорируют коллизию
        /// </summary>
        public static readonly int IgnoreRaycastId = LayerMask.NameToLayer(IGNORE_RAYCAST);

        /// <summary>
        /// Слой для объектов которые представляют собой воду
        /// </summary>
        public static readonly int WaterId = LayerMask.NameToLayer(WATER);

        /// <summary>
        /// Слой для объектов пользовательского интерфейса
        /// </summary>
        public static readonly int UiId = LayerMask.NameToLayer(UI);
        #endregion

        #region Mask constants
        /// <summary>
        /// Маска для слоя по умолчанию
        /// </summary>
        public static readonly int MaskDefault = 1 << DefaultId;

        /// <summary>
        /// Маска для слоя прозрачных объектов
        /// </summary>
        public static readonly int MaskTransparentFx = 1 << TransparentFxId;

        /// <summary>
        /// Маска для слоя объектов которые игнорируют коллизию
        /// </summary>
        public static readonly int MaskIgnoreRaycast = 1 << IgnoreRaycastId;

        /// <summary>
        /// Маска для слоя объектов которые представляют собой воду
        /// </summary>
        public static readonly int MaskWater = 1 << WaterId;

        /// <summary>
        /// Маска для слоя объектов пользовательского интерфейса
        /// </summary>
        public static readonly int MaskUi = 1 << UiId;
        #endregion
    }
    /**@}*/
}
#endif
