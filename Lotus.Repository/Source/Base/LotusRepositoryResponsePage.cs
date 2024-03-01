using System.Collections.Generic;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Интерфейс для постраничного получения данных.
    /// </summary>
    /// <typeparam name="TData">Тип данных.</typeparam>
    public interface ILotusResponsePage<TData> : ILotusResponse
    {
        /// <summary>
        /// Данные.
        /// </summary>
        IReadOnlyCollection<TData> Payload { get; set; }

        /// <summary>
        /// Информация о странице.
        /// </summary>
        ILotusPageInfoResponse PageInfo { get; set; }
    }

    /// <summary>
    /// Класс для постраничного получения данных.
    /// </summary>
    /// <typeparam name="TData">Тип данных.</typeparam>
    public class ResponsePage<TData> : ILotusResponsePage<TData>
    {
        /// <summary>
        /// Результат получения данных.
        /// </summary>
        public ILotusResult? Result { get; set; }

        /// <summary>
        /// Данные.
        /// </summary>
        public IReadOnlyCollection<TData> Payload { get; set; } = default!;

        /// <summary>
        /// Информация о странице.
        /// </summary>
        public ILotusPageInfoResponse PageInfo { get; set; } = default!;
    }
    /**@}*/
}