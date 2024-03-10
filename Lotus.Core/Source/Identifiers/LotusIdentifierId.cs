using System;

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
    *@{*/
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
    public class IdentifierInt : IdentifierId<int>, ILotusIdentifierInt
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierInt()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public IdentifierInt(int id)
            : base(id)
        {
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий идентификацию через уникальный числовой идентификатор.
    /// </summary>
    [Serializable]
    public class IdentifierLong : IdentifierId<long>, ILotusIdentifierLong
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierLong()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        public IdentifierLong(long id)
            : base(id)
        {
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий идентификацию через глобальный идентификатор.
    /// </summary>
    [Serializable]
    public class IdentifierGuid : IdentifierId<Guid>, ILotusIdentifierGlobal
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public IdentifierGuid()
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Глобальный идентификатор.</param>
        public IdentifierGuid(Guid id)
            : base(id)
        {
        }
        #endregion
    }
    /**@}*/
}