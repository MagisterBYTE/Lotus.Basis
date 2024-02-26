namespace Lotus.Core
{
    /**
     * \defgroup CoreMessage Подсистема сообщений
     * \ingroup Core
     * \brief Централизованная подсистема сообщений. Подсистема сообщений предназначена для уведомления всех подписчиков
        о произошедшем событии посредством соответствующих издателей.
     * \details
        ## Возможности/особенности
        1. Возможность отправить сообщение любым подписчикам
        2. Легкость реализации обработчика сообщений
        3. Минимальные накладные расходы, высокая скорость работы
        4. Низкая связность и зависимость подсистемы от других подсистем
        5. Полностью интегрирована в систему Lotus в качестве основной
    
        ## Описание
        Подсистема сообщений очень простая и основана на паттерне подписчик/издатель. Отправка сообщений происходит
        через издателя, и также возможно через статический метод центрального диспетчера сообщений
        (который создает при необходимости издателя по умолчанию) \ref Lotus.Core.CPublisher.
        Для обработки сообщения нужно реализовать соответствующий интерфейс \ref Lotus.Core.ILotusMessageHandler. 
        Компонент с реализаций интерфейса нужно зарегистрировать в либо диспетчере, либо в соответствующем издателе.
        Сообщения поступают в очередь и последовательно обрабатываются. Сообщения можно послать подписчикам издателя
        или через диспетчер всем подписчикам. Сообщения также можно послать из консоли(для Unity).
        Многие подсистемы реализует интерфейс сообщений для централизованного и унифицированного управления.

        ## Использование
        1. Реализовать интерфейс \ref Lotus.Core.ILotusMessageHandler
        2. Зарегистрировать компонент в конкретном издателе \ref Lotus.Core.CPublisher
        3. Теперь можно посылать команды через издателя \ref Lotus.Core.CPublisher
        4. Методы диспетчер или издателя нужно использовать в ручную(непосредственно вызывать методы в нужных местах)
     * @{
     */
    /// <summary>
    /// Базовый класс для определения аргумента сообщения.
    /// </summary>
    /// <remarks>
    /// Фактические данный класс реализует полноценное хранение данных о произошедшем событии, источники событии.
    /// Чтобы исключить накладные расходы связанные с фрагментацией памяти малыми объектами, объекты классы управляются
    /// через пул.
    /// Таким образом фактически происходит циркуляция сообщений в системе без существенных накладных затрат
    /// </remarks>
    public class CMessageArgs : ILotusPoolObject, ILotusIdentifierId<int>
    {
        #region Fields
        // Основные параметры
        protected internal int _id;
        protected internal string _name;
        protected internal object? _data;
        protected internal object? _sender;
        protected internal bool _isPoolObject;
        #endregion

        #region Properties
        /// <summary>
        /// Уникальный идентификатор сообщения.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Наименование сообщения.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Данные сообщения.
        /// </summary>
        public object? Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Источник сообщения.
        /// </summary>
        public object? Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Источник сообщения - как пользовательский скрипт.
		/// </summary>
		public UnityEngine.MonoBehaviour SenderBehaviour
		{
			get { return _sender as UnityEngine.MonoBehaviour; }
			set { _sender = value; }
		}
#endif

        /// <summary>
        /// Статус объекта из пула.
        /// </summary>
        public bool IsPoolObject
        {
            get { return _isPoolObject; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CMessageArgs()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(string name, bool isPool = false)
        {
            _name = name;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(int id, bool isPool = false)
        {
            _id = id;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(string name, int id, bool isPool = false)
        {
            _name = name;
            _id = id;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="sender">Компонент - источник события.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(string name, object sender, bool isPool = false)
        {
            _name = name;
            _sender = sender;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="sender">Источник события.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(int id, object sender, bool isPool = false)
        {
            _id = id;
            _sender = sender;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="sender">Компонент - источник события.</param>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(string name, int id, object sender, bool isPool = false)
        {
            _name = name;
            _sender = sender;
            _isPoolObject = isPool;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="isPool">Статус размещения объекта в пуле.</param>
        public CMessageArgs(bool isPool)
        {
            _isPoolObject = isPool;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление сообщения.</returns>
        public override string ToString()
        {
            return $"Name <{_name}> | Value[{_data?.ToString()}]";
        }
        #endregion

        #region ILotusPoolObject methods
        /// <summary>
        /// Псевдо-конструктор.
        /// </summary>
        /// <remarks>
        /// Вызывается диспетчером пула в момент взятия объекта из пула.
        /// </remarks>
        public void OnPoolTake()
        {

        }

        /// <summary>
        /// Псевдо-деструктор.
        /// </summary>
        /// <remarks>
        /// Вызывается диспетчером пула в момент попадания объекта в пул.
        /// </remarks>
        public void OnPoolRelease()
        {
            _data = null;
            _sender = null;
            _name = "";
            _id = 0;
        }
        #endregion
    }
    /**@}*/
}