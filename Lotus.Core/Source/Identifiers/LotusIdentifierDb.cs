using System;
using System.ComponentModel;
#if NET6_0_OR_GREATER
using System.ComponentModel.DataAnnotations;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
    *@{*/
    /// <summary>
    /// Шаблонный класс для сущностей баз данных поддерживающих идентификатор через первичный ключ.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public class EntityDb<TKey> : ILotusIdentifierId<TKey>
        where TKey : notnull, IEquatable<TKey>
    {
        #region Properties
        /// <summary>
        /// Ключ сущности.
        /// </summary>
#if NET6_0_OR_GREATER
        [Key]
        [Required]
#endif
        public virtual TKey Id { get; set; } = default!;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public EntityDb()
#pragma warning disable CS8604 // Possible null reference argument.
            : this(default)
#pragma warning restore CS8604 // Possible null reference argument.
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Ключ сущности.</param>
        public EntityDb(TKey id)
        {
            Id = id;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string? ToString()
        {
            return Id.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Шаблонный класс для сущностей баз данных поддерживающих идентификатор через первичный ключ.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public class EntityDbNotifyProperty<TKey> : EntityDb<TKey>, INotifyPropertyChanged
        where TKey : notnull, IEquatable<TKey>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public EntityDbNotifyProperty()
#pragma warning disable CS8604 // Possible null reference argument.
            : this(default)
#pragma warning restore CS8604 // Possible null reference argument.
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Ключ сущности.</param>
        public EntityDbNotifyProperty(TKey id)
        {
            Id = id;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string? ToString()
        {
            return Id.ToString();
        }
        #endregion

        #region ДАННЫЕ INotifyPropertyChanged 
        /// <summary>
        /// Событие срабатывает ПОСЛЕ изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="args">Аргументы события.</param>
        public void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }
        #endregion
    }

    /// <summary>
    /// Шаблонный класс для сущностей баз данных поддерживающих идентификатор через первичный ключ c поддержкой имени.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public class EntityNameDb<TKey> : EntityDb<TKey>, ILotusNameable
        where TKey : notnull, IEquatable<TKey>
    {
        #region Properties
        /// <summary>
        /// Имя сущности.
        /// </summary>
#if NET6_0_OR_GREATER
        [Required]
#endif
        public virtual string Name { get; set; } = default!;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public EntityNameDb()
#pragma warning disable CS8604 // Possible null reference argument.
            : this(default)
#pragma warning restore CS8604 // Possible null reference argument.
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Ключ сущности.</param>
        public EntityNameDb(TKey id)
        {
            Id = id;
            Name = string.Empty;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string? ToString()
        {
            return $"Name: {Name} | Id: {Id}";
        }
        #endregion
    }
    /**@}*/
}