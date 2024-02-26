namespace Lotus.Core
{
    /** \addtogroup CoreInterfaces
	*@{*/
    /// <summary>
    /// Базовый интерфейс реализующий понятие владельца.
    /// </summary>
    /// <remarks>
    /// Владелец обладает каким-либо объектом и поддерживает основные операции по обновлению связей, 
    /// присоединением и отсоединением зависимого объекта
    /// </remarks>
    public interface ILotusOwnerObject
    {
        /// <summary>
        /// Присоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="add">Статус добавления в коллекцию.</param>
        void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add);

        /// <summary>
        /// Отсоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="remove">Статус удаления из коллекции.</param>
        void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove);

        /// <summary>
        /// Обновление связей для зависимых объектов.
        /// </summary>
        void UpdateOwnedObjects();

        /// <summary>
        /// Информирование данного объекта о начале изменения данных указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого будут меняться.</param>
        /// <param name="dataName">Имя данных.</param>
        /// <returns>Статус разрешения/согласования изменения данных.</returns>
        bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object? data, string dataName);

        /// <summary>
        /// Информирование данного объекта об окончании изменении данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого изменились.</param>
        /// <param name="dataName">Имя данных.</param>
        void OnNotifyUpdated(ILotusOwnedObject ownedObject, object? data, string dataName);
    }

    /// <summary>
    /// Базовый класс реализующий понятие владельца.
    /// </summary>
    /// <remarks>
    /// Владелец обладает каким-либо объектом и поддерживает основные операции по обновлению связей, 
    /// присоединением и отсоединением зависимого объекта
    /// </remarks>
    public class COwnerObject : PropertyChangedBase, ILotusOwnerObject
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public COwnerObject()
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Присоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="add">Статус добавления в коллекцию.</param>
        public virtual void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add)
        {

        }

        /// <summary>
        /// Отсоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="remove">Статус удаления из коллекции.</param>
        public virtual void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove)
        {

        }

        /// <summary>
        /// Обновление связей для зависимых объектов.
        /// </summary>
        public virtual void UpdateOwnedObjects()
        {

        }

        /// <summary>
        /// Информирование данного объекта о начале изменения данных указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого будут меняться.</param>
        /// <param name="dataName">Имя данных.</param>
        /// <returns>Статус разрешения/согласования изменения данных.</returns>
        public virtual bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object? data, string dataName)
        {
            return true;
        }

        /// <summary>
        /// Информирование данного объекта об окончании изменении данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого изменились.</param>
        /// <param name="dataName">Имя данных.</param>
        public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, object? data, string dataName)
        {

        }
        #endregion
    }

    /// <summary>
    /// Интерфейс объекта которым владеют.
    /// </summary>
    public interface ILotusOwnedObject
    {
        /// <summary>
        /// Владелец объекта.
        /// </summary>
        ILotusOwnerObject? IOwner { get; set; }
    }

    /// <summary>
    /// Базовый класс реализующий объект которым владеют.
    /// </summary>
    public class COwnedObject : PropertyChangedBase, ILotusOwnedObject
    {
        #region Fields
        protected internal ILotusOwnerObject? _owner;
        #endregion

        #region Properties
        /// <summary>
        /// Владелец объекта.
        /// </summary>
        public ILotusOwnerObject? IOwner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                RaiseOwnerObjectChanged();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public COwnedObject()
        {

        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение владельца объекта.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseOwnerObjectChanged()
        {
        }
        #endregion
    }

    /// <summary>
    /// Интерфейс объекта которым владеет определенный тип владельца.
    /// </summary>
    /// <typeparam name="TOwner">Тип владельца.</typeparam>
    public interface ILotusOwnedObject<TOwner> : ILotusOwnedObject where TOwner : ILotusOwnerObject
    {
        /// <summary>
        /// Владелец объекта.
        /// </summary>
        TOwner? Owner { get; set; }
    }

    /// <summary>
    /// Шаблон объекта которым владеет определенный тип владельца.
    /// </summary>
    /// <typeparam name="TOwner">Тип владельца.</typeparam>
    public class OwnedObject<TOwner> : PropertyChangedBase, ILotusOwnedObject<TOwner>
        where TOwner : ILotusOwnerObject
    {
        #region Fields
        protected internal TOwner? _owner;
        #endregion

        #region Properties
        /// <summary>
        /// Владелец объекта.
        /// </summary>
        public ILotusOwnerObject? IOwner
        {
            get { return _owner; }
            set
            {
                _owner = value == null ? default : (TOwner)value;
                RaiseOwnerObjectChanged();
            }
        }

        /// <summary>
        /// Владелец объекта.
        /// </summary>
        public TOwner? Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                RaiseOwnerObjectChanged();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public OwnedObject()
        {

        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение владельца объекта.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseOwnerObjectChanged()
        {
        }
        #endregion
    }
    /**@}*/
}