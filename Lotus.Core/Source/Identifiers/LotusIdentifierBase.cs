using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreIdentifiers Подсистема идентификаторов
     * \ingroup Core
     * \brief Подсистема идентификаторов определяет базовые понятия об идентификации и классы идентификации объектов.
     * \details Под идентификацией объекта понимается наличие у сущности определённого уникального идентификатора по 
		которому ее возможно однозначно найти или получить в определённом контексте.
     * @{
     */
    /// <summary>
    /// Базовый интерфейс обозначающих что сущность поддерживает идентификацию.
    /// </summary>
    public interface ILotusIdentifier
    {
    }

    /// <summary>
    /// Определение шаблона интерфейса для идентификации сущности через уникальный идентификатор-ключ.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public interface ILotusIdentifierId<TKey> : ILotusIdentifier where TKey : notnull, IEquatable<TKey>
    {
        /// <summary>
        /// Ключ сущности.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Определение интерфейса для объектов реализующих идентификации через глобальный идентификатор.
    /// </summary>
    public interface ILotusIdentifierGlobal : ILotusIdentifierId<Guid>
    {
    }

    /// <summary>
    /// Определение интерфейса для объектов реализующих идентификации через уникальный числовой идентификатор.
    /// </summary>
    public interface ILotusIdentifierInt : ILotusIdentifierId<int>
    {
    }

    /// <summary>
    /// Определение интерфейса для объектов реализующих идентификации через уникальный числовой идентификатор.
    /// </summary>
    public interface ILotusIdentifierLong : ILotusIdentifierId<long>
    {
    }

    /// <summary>
    /// Определение интерфейса для объектов реализующих понятие имени.
    /// </summary>
    public interface ILotusNameable
    {
        /// <summary>
        /// Имя объекта.
        /// </summary>
        string Name { get; set; }
    }
    /**@}*/
}