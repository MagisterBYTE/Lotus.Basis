#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Интерфейс для реализации пользовательского ресурса
    /// </summary>
    /// <remarks>
    /// Интерфейс определяет ряд методов которые обязательно должен реализовать пользовательский ресурс
    /// для того чтоб нормально функционировать.
    /// </remarks>
    public interface ILotusResourceable
    {
        #region Main methods
        /// <summary>
        /// Первичное создание ресурса
        /// </summary>
        void Create();

        /// <summary>
        /// Первичная безопасная инициализация несериализуемых данных
        /// </summary>
        void Init();

        /// <summary>
        /// Принудительный сброс записанных данных на диск
        /// </summary>
        void Flush();
        #endregion
    }

    /**@}*/
}
#endif
