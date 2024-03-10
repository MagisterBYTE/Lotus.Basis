using System;

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
    *@{*/
    /// <summary>
    /// Шаблонный класс для простых сущностей поддерживающих идентификатор.
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатор.</typeparam>
    public class IdentifierDtoId<TKey> : ILotusIdentifierId<TKey> where TKey : notnull, IEquatable<TKey>
    {
        /// <summary>
        /// Ключ сущности.
        /// </summary>
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// Базовый класс для простых сущностей реализующий идентификацию через уникальный числовой идентификатор.
    /// </summary>
    public class IdentifierDtoInt : IdentifierDtoId<int>, ILotusIdentifierInt
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierDtoInt()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public IdentifierDtoInt(int id)
        {
            Id = id;
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс для простых сущностей реализующий идентификацию через уникальный числовой идентификатор.
    /// </summary>
    public class IdentifierDtoLong : IdentifierDtoId<long>, ILotusIdentifierLong
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierDtoLong()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public IdentifierDtoLong(long id)
        {
            Id = id;
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс для простых сущностей реализующий идентификацию через глобальный идентификатор.
    /// </summary>
    public class IdentifierDtoGuid : IdentifierDtoId<Guid>, ILotusIdentifierGlobal
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierDtoGuid()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Глобальный идентификатор.</param>
        public IdentifierDtoGuid(Guid id)
        {
            Id = id;
        }
        #endregion
    }
    /**@}*/
}