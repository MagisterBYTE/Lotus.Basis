namespace Lotus.Core
{
    /** \addtogroup CoreMessage
	*@{*/
    /// <summary>
    ///  Определение интерфейса издателя сообщений.
    /// </summary>
    public interface ILotusPublisher : ILotusNameable
    {
        /// <summary>
        /// Регистрация подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        void RegisterMessageHandler(ILotusMessageHandler messageHandler);

        /// <summary>
        /// Отмена регистрации подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        void UnRegisterMessageHandler(ILotusMessageHandler messageHandler);
    }

    /// <summary>
    /// Издателя сообщения.
    /// </summary>
    /// <remarks>
    /// Реализация издателя сообщений который хранит все обработчики сообщений и обеспечивает
    /// посылку сообщений.
    /// Методы издателя нужно вызывать вручную в соответствующих местах
    /// </remarks>
    public class CPublisher : ILotusPublisher
    {
        #region Static methods
        /// <summary>
        /// Конструирование объекта базового класса для определения аргумента сообщения.
        /// </summary>
        /// <returns>Объект.</returns>
        public static CMessageArgs ConstructorMessageArgs()
        {
            return new CMessageArgs(true);
        }
        #endregion

        #region Fields
        protected internal string _name;
        protected internal PoolManager<CMessageArgs> _messageArgsPools;
        protected internal ListArray<ILotusMessageHandler> _messageHandlers;
        protected internal QueueArray<CMessageArgs> _queueMessages;
        #endregion

        #region Properties
        /// <summary>
        /// Имя издателя.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Пул объектов типа оболочки для хранения аргументов сообщения.
        /// </summary>
        public PoolManager<CMessageArgs> MessageArgsPools
        {
            get { return _messageArgsPools; }
        }

        /// <summary>
        /// Список обработчиков сообщений.
        /// </summary>
        public ListArray<ILotusMessageHandler> MessageHandlers
        {
            get { return _messageHandlers; }
        }

        /// <summary>
        /// Очередь сообщений.
        /// </summary>
        public QueueArray<CMessageArgs> QueueMessages
        {
            get { return _queueMessages; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CPublisher()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя издателя.</param>
        public CPublisher(string name)
        {
            _name = name;
            _messageArgsPools = new PoolManager<CMessageArgs>(100, ConstructorMessageArgs);
            _messageHandlers = new ListArray<ILotusMessageHandler>(10);
            _queueMessages = new QueueArray<CMessageArgs>(100);
        }
        #endregion

        #region ILotusPublisher methods
        /// <summary>
        /// Регистрация подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        public virtual void RegisterMessageHandler(ILotusMessageHandler messageHandler)
        {
            if (_messageHandlers.Contains(messageHandler) == false)
            {
                _messageHandlers.Add(in messageHandler);
            }
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        public virtual void UnRegisterMessageHandler(ILotusMessageHandler messageHandler)
        {
            _messageHandlers.Remove(in messageHandler);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Обработка сообщений.
        /// </summary>
        public void OnUpdate()
        {
            // Если у нас есть обработчики и сообщения
            if (_messageHandlers.Count > 0 && _queueMessages.Count > 0)
            {
                // Перебираем все сообщения
                while (_queueMessages.Count != 0)
                {
                    // Выталкиваем сообщения
                    CMessageArgs message = _queueMessages.Dequeue()!;

                    for (var i = 0; i < _messageHandlers.Count; i++)
                    {
                        var code = _messageHandlers[i].OnMessageHandler(message);

                        // Сообщение почему-то обработано с отрицательным результатом 
                        if (code == XMessageHandlerResultCode.NEGATIVE_RESULT)
                        {
#if UNITY_2017_1_OR_NEWER
							UnityEngine.Debug.LogWarning(message.ToString());
#else
                            XLogger.LogWarning(message.ToString());
#endif
                        }
                    }

                    // Если объект был из пула
                    if (message.IsPoolObject)
                    {
                        _messageArgsPools.Release(message);
                    }
                }
            }
        }
        #endregion

        #region Send methods
        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="message">Аргументы сообщения.</param>
        public void SendMessage(CMessageArgs message)
        {
            _queueMessages.Enqueue(message);
        }

        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="data">Данные сообщения.</param>
        /// <param name="sender">Источник сообщения.</param>
        public void SendMessage(string name, object data, object sender)
        {
            CMessageArgs message = _messageArgsPools.Take();
            message.Name = name;
            message.Data = data;
            message.Sender = sender;
            _queueMessages.Enqueue(message);
        }

        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="data">Данные сообщения.</param>
        /// <param name="sender">Источник сообщения.</param>
        public void SendMessage(int id, object data, object sender)
        {
            CMessageArgs message = _messageArgsPools.Take();
            message.Id = id;
            message.Data = data;
            message.Sender = sender;
            _queueMessages.Enqueue(message);
        }
        #endregion
    }
    /**@}*/
}