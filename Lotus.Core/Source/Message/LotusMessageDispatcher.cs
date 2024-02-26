namespace Lotus.Core
{
    /** \addtogroup CoreMessage
	*@{*/
    /// <summary>
    /// Центральный диспетчер для работы с сообщениями.
    /// </summary>
    /// <remarks>
    /// Реализация центрального диспетчера сообщений, который используется издателя по умолчанию хранит обеспечивает
    /// централизованную посылку и обработку сообщений.
    /// Методы центрального диспетчера нужно вызывать вручную в соответствующих местах
    /// </remarks>
    public static class XMessageDispatcher
    {
        #region Const
        /// <summary>
        /// Имя издателя по умолчанию.
        /// </summary>
        public const string DefaultName = "Default";
        #endregion

        #region Fields
        private static ListArray<CPublisher> _publishers;
        #endregion

        #region Properties
        /// <summary>
        /// Список всех издателей.
        /// </summary>
        public static ListArray<CPublisher> Publishers
        {
            get
            {
                if (_publishers == null)
                {
                    OnInit();
                }
                return _publishers!;
            }
        }

        /// <summary>
        /// Издатель по умолчанию.
        /// </summary>
        public static CPublisher Default
        {
            get
            {
                return Publishers[0];
            }
        }
        #endregion

        #region Dispatcher methods
        /// <summary>
        /// Перезапуск данных центрального диспетчера в режиме редактора.
        /// </summary>
        public static void OnResetEditor()
        {
        }

        /// <summary>
        /// Первичная инициализация данных центрального диспетчера для работы с сообщениями.
        /// </summary>
        public static void OnInit()
        {
            if (_publishers == null)
            {
                _publishers = new ListArray<CPublisher>();
                _publishers.Add(new CPublisher(DefaultName));
            }
        }

        /// <summary>
        /// Обработка сообщений.
        /// </summary>
        public static void OnUpdate()
        {
            for (var i = 0; i < _publishers.Count; i++)
            {
                _publishers[i].OnUpdate();
            }
        }
        #endregion

        #region ILotusMessageHandler methods
        /// <summary>
        /// Регистрация подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        public static void RegisterMessageHandler(ILotusMessageHandler messageHandler)
        {
            Default.RegisterMessageHandler(messageHandler);
        }

        /// <summary>
        /// Отмена регистрации подписки на обработку сообщений.
        /// </summary>
        /// <param name="messageHandler">Интерфейс для обработки сообщений.</param>
        public static void UnRegisterMessageHandler(ILotusMessageHandler messageHandler)
        {
            Default.UnRegisterMessageHandler(messageHandler);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="message">Аргументы сообщения.</param>
        public static void SendMessage(CMessageArgs message)
        {
            Default.SendMessage(message);
        }

        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="name">Имя сообщения.</param>
        /// <param name="data">Данные сообщения.</param>
        /// <param name="sender">Источник сообщения.</param>
        public static void SendMessage(string name, object data, object sender)
        {
            Default.SendMessage(name, data, sender);
        }

        /// <summary>
        /// Послать сообщения.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сообщения.</param>
        /// <param name="data">Данные сообщения.</param>
        /// <param name="sender">Источник сообщения.</param>
        public static void SendMessage(int id, object data, object sender)
        {
            Default.SendMessage(id, data, sender);
        }
        #endregion
    }
    /**@}*/
}