using System;

namespace Lotus.Core
{
    /** \addtogroup CoreEntityDb
    *@{*/
    /// <summary>
    /// Интерфейс для определение сущности.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа(идентификатора).</typeparam>
    public interface ILotusEntityDb<TKey> : ILotusIdentifierId<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Дата создания сущности.
        /// </summary>
        DateTime Created { get; set; }

        /// <summary>
        /// Дата последней модификации сущности.
        /// </summary>
        DateTime Modified { get; set; }
    }

    /// <summary>
    /// Шаблонный класс для сущностей поддерживающих идентификатор через первичный ключ.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа(идентификатора).</typeparam>
    public class EntityDb<TKey> : BaseEntityDb<TKey>, ILotusEntityDb<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Дата создания сущности.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Дата последней модификации сущности.
        /// </summary>
        public DateTime Modified { get; set; }
    }
    /**@}*/
}