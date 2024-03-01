using System;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Определение базовой сущности репозитория с поддержкой мягкого удаления.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа идентификатора.</typeparam>
    public abstract class SoftDeletableEntity<TKey> : RepositoryEntityBase<TKey>, ILotusRepositorySoftDeletable
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Дата удаления сущности.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }

    /// <summary>
    /// Определение базовой сущности репозитория с поддержкой мягкого удаления.
    /// </summary>
    public abstract class SoftDeletableEntity : SoftDeletableEntity<Guid>
    {
    }
    /**@}*/
}