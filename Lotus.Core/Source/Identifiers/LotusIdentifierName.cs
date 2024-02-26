using System;
using System.ComponentModel;

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
    *@{*/
    /// <summary>
    /// Базовый класс реализующий имя объекта.
    /// </summary>
    [Serializable]
    public class CNameable : PropertyChangedBase, ILotusNameable, IComparable<ILotusNameable>,
        IComparable<CNameable>
    {
        #region Static fields
        //
        // Константы для информирования об изменении свойств
        //
        // Идентификация
        protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
		[LotusDisplayName(nameof(Name))]
#endif
        protected internal string _name = "";
        #endregion

        #region Properties
        /// <summary>
        /// Наименование объекта.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(PropertyArgsName);
                RaiseNameChanged();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CNameable()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя объекта.</param>
        public CNameable(string name)
        {
            _name = name;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(ILotusNameable? other)
        {
            if (other == null) return 0;

            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(CNameable? other)
        {
            if (other == null) return 0;

            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение имени объекта.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseNameChanged()
        {
        }
        #endregion
    }

    /// <summary>
    /// Базовый класс реализующий имя объекта и уникальный числовой идентификатор.
    /// </summary>
    [Serializable]
    public class CNameableInt : PropertyChangedBase, ILotusNameable, ILotusIdentifierInt, IComparable<ILotusNameable>,
        IComparable<CNameableInt>
    {
        #region Static fields
        //
        // Константы для информирования об изменении свойств
        //
        // Идентификация
        protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
        protected static readonly PropertyChangedEventArgs PropertyArgsId = new PropertyChangedEventArgs(nameof(Id));
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
		[LotusDisplayName(nameof(Name))]
#endif
        protected internal string _name = "";
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
		[UnityEngine.HideInInspector]
#endif
        protected internal int _id;
        #endregion

        #region Properties
        /// <summary>
        /// Наименование объекта.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(PropertyArgsName);
                RaiseNameChanged();
            }
        }

        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(PropertyArgsId);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CNameableInt()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя объекта.</param>
        public CNameableInt(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public CNameableInt(int id)
        {
            _id = id;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(ILotusNameable? other)
        {
            if (other == null) return 0;

            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(CNameableInt? other)
        {
            if (other == null) return 0;

            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string ToString()
        {
            return $"Name: {_name} | Id: {_id}";
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение имени объекта.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseNameChanged()
        {
        }
        #endregion
    }
    /**@}*/
}