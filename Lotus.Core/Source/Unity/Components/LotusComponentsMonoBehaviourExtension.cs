#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента MonoBehaviour
    /// </summary>
    public static class XMonoBehaviourExtension
    {
        #region Main methods
        /// <summary>
        /// Получение интерфейса указанного типа от компонента
        /// </summary>
        /// <typeparam name="TInterface">Тип интерфейса</typeparam>
        /// <param name="this">Базовый компонент пользователя</param>
        /// <returns>Интерфейс или null</returns>
        public static TInterface GetInterface<TInterface>(this MonoBehaviour @this)
        {
            if (!typeof(TInterface).IsInterface)
            {
                return default(TInterface);
            }

            if (@this is TInterface result)
            {
                return result;
            }

            return default(TInterface);
        }
        #endregion
    }
    /**@}*/
}
#endif
