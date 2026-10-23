#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений ресурса TextAsset
    /// </summary>
    public static class XTextAssetExtension
    {
        #region Main methods
        /// <summary>
        /// Возвращение массива строк текстового ресурса
        /// </summary>
        /// <param name="this">Текстовый ресурс</param>
        /// <returns>Массив строк</returns>
        public static string[] GetAllLines(this TextAsset @this)
        {
            return @this.text.Split(XStringHelper.SeparatorNewCarriageLine, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }

    /**@}*/
}
#endif
