using System;

namespace Lotus.Core
{
    /** \addtogroup CoreEntityDb
    *@{*/
    /// <summary>
    /// Интерфейс для определения мягкого удаления сущности.
    /// </summary>
    public interface ILotusEntityDbSoftDeletable
    {
        /// <summary>
        /// Дата удаления сущности.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }

    /// <summary>
    /// Шаблонный класс для сущностей с поддержкой мягкого удаления.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа идентификатора.</typeparam>
    public class EntityDbSoftDeletable<TKey> : EntityDb<TKey>, ILotusEntityDbSoftDeletable
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Дата удаления сущности.
        /// </summary>
        public DateTime? Deleted { get; set; }
    }
    /**@}*/
}