using System;

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
    *@{*/
    /// <summary>
    /// Шаблонный класс для простых сущностей поддерживающих идентификатор.
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатор.</typeparam>
    public class IdentifierDtoId<TKey> : ILotusIdentifierId<TKey> where TKey : 
        notnull, IEquatable<TKey>
    {
        /// <summary>
        /// Ключ сущности.
        /// </summary>
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// Шаблонный класс для сущностей поддерживающих идентификатор.
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатор.</typeparam>
    public class IdentifierId<TKey> : PropertyChangedBase, ILotusIdentifierId<TKey> 
        where TKey : notnull, IEquatable<TKey>
    {
        #region Fields
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
		[UnityEngine.HideInInspector]
#endif
        protected internal TKey _id;
        #endregion

        #region Properties
        //
        // ИДЕНТИФИКАЦИЯ
        //
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public virtual TKey Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierId()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public IdentifierId(TKey id)
        {
            _id = id;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Значение идентификатора.</returns>
        public override string? ToString()
        {
            return Id.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий идентификацию через уникальный числовой идентификатор.
    /// </summary>
    [Serializable]
    public class CIdentifierInt : IdentifierId<int>, ILotusIdentifierInt
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CIdentifierInt()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public CIdentifierInt(int id)
            : base(id)
        {
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий идентификацию через уникальный числовой идентификатор.
    /// </summary>
    [Serializable]
    public class CIdentifierLong : IdentifierId<long>, ILotusIdentifierLong
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CIdentifierLong()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public CIdentifierLong(long id)
            : base(id)
        {
        }
        #endregion
    }
    /**@}*/
}