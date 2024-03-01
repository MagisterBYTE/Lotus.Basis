namespace Lotus.Core
{
    /** \addtogroup CoreObjectPool
	*@{*/
    /// <summary>
    /// Центральный диспетчер для управления менеджерами пулом объектов.
    /// </summary>
    /// <remarks>
    /// Реализация диспетчер который обеспечивает глобальный доступ к всем зарегистрированным менеджерам пулом объектов.
    /// Доступ к зарегистрированным менеджерам пулом объектов осуществляется по имени менеджера.
    /// </remarks>
    public static class XPoolDispatcher
    {
        #region Const
        /// <summary>
        /// Имя менеджера по умолчанию.
        /// </summary>
        public const string DefaultName = "Default";
        #endregion

        #region Fields
        private static ListArray<ILotusPoolManager> _poolManagers;
        #endregion

        #region Properties
        /// <summary>
        /// Список менеджеров пулов объектов.
        /// </summary>
        public static ListArray<ILotusPoolManager> PoolManagers
        {
            get
            {
                if (_poolManagers == null)
                {
                    OnInit();
                }
                return _poolManagers!;
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
        /// Первичная инициализация данных центрального диспетчера.
        /// </summary>
        public static void OnInit()
        {
            if (_poolManagers == null)
            {
                _poolManagers = new ListArray<ILotusPoolManager>();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Регистрация менеджера управления пулом объектов.
        /// </summary>
        /// <param name="manager">Менеджер управления пулом объектов.</param>
        /// <returns>Статус успешности регистрации.</returns>
        public static bool RegisterManager(ILotusPoolManager manager)
        {
            PoolManagers.Add(manager);
            return true;
        }

        /// <summary>
        /// Отмена регистрации менеджера управления пулом объектов.
        /// </summary>
        /// <param name="manager">Менеджер управления пулом объектов.</param>
        /// <returns>Статус успешности отмены регистрации.</returns>
        public static bool UnRegisterManager(ILotusPoolManager manager)
        {
            return PoolManagers.Remove(in manager);
        }
        #endregion

        #region ILotusPoolManager methods
        /// <summary>
        /// Взять готовый объект из пула.
        /// </summary>
        /// <param name="managerName">Имя менеджер.</param>
        /// <returns>Объект.</returns>
        public static TPoolObject? Take<TPoolObject>(string managerName)
        {
            var result = PoolManagers.Search(x => x!.Name == managerName);
            if (result != null)
            {
                return (TPoolObject)result.TakeObjectFromPool();
            }

            return default(TPoolObject);
        }

        /// <summary>
        /// Освободить объект и положить его назад в пул.
        /// </summary>
        /// <param name="managerName">Имя менеджер.</param>
        /// <param name="poolObject">Объект.</param>
        /// <returns>Статус успешности добавления объекта в пул.</returns>
        public static bool Release<TPoolObject>(string managerName, TPoolObject poolObject)
        {
            var result = PoolManagers.Search(x => x!.Name == managerName);
            if (result != null)
            {
                result.ReleaseObjectToPool(poolObject!);
                return true;
            }

            return false;
        }
        #endregion
    }
    /**@}*/
}