namespace Lotus.Core
{
    /** \addtogroup CoreECS
	*@{*/
    /// <summary>
    /// Базовый интерфейс определения системы для реализации логики в ECS.
    /// </summary>
    public interface ILotusEcsSystem
    {
        /// <summary>
        /// Статус недоступности системы.
        /// </summary>
        bool IsDisabled { get; set; }

        /// <summary>
        /// Порядок исполнения системы.
        /// </summary>
        int OrderExecution { get; set; }
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики в момент инициализации.
    /// </summary>
    public interface IEcsLotusPreInitSystem : ILotusEcsSystem
    {
        /// <summary>
        /// Инициализация подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void PreInit(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики в момент инициализации.
    /// </summary>
    public interface IEcsLotusInitSystem : ILotusEcsSystem
    {
        /// <summary>
        /// Инициализация подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void Init(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики постоянного обновления.
    /// </summary>
    public interface IEcsLotusUpdateSystem : ILotusEcsSystem
    {
        /// <summary>
        /// Обновление подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void Update(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики постоянного обновления.
    /// </summary>
    public interface IEcsLotusLateUpdateSystem : ILotusEcsSystem
    {
        /// <summary>
        /// Обновление подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void LateUpdate(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики фиксированного обновления.
    /// </summary>
    public interface IEcsLotusFixedUpdateSystem : ILotusEcsSystem
    {
        /// <summary>
        /// Фиксированное обновление подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void FixedUpdate(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики удаления.
    /// </summary>
    public interface IEcsDestroySystem : ILotusEcsSystem
    {
        /// <summary>
        /// Удаление подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void Destroy(CEcsSystems systems);
    }

    /// <summary>
    /// Интерфейс определения подсистемы для реализации логики удаления.
    /// </summary>
    public interface IEcsPostDestroySystem : ILotusEcsSystem
    {
        /// <summary>
        /// Удаление подсистемы.
        /// </summary>
        /// <param name="systems">Контекст систем.</param>
        void PostDestroy(CEcsSystems systems);
    }

    /// <summary>
    /// Класс содержащий контекст ECS.
    /// </summary>
    /// <remarks>
    /// Определение системы в ECS которая является контейнером для основной логики для обработки отфильтрованных сущностей.
    /// </remarks>
    public class CEcsSystems
    {
        #region Fields
        // Список миров
        protected internal ListArray<CEcsWorld> _worlds;

        // Подсистемы логики
        protected internal ListArray<IEcsLotusPreInitSystem> _preInitSystems;
        protected internal ListArray<IEcsLotusInitSystem> _initSystems;
        protected internal ListArray<IEcsLotusUpdateSystem> _updateSystems;
        protected internal ListArray<IEcsLotusLateUpdateSystem> _lateUpdateSystems;
        protected internal ListArray<IEcsLotusFixedUpdateSystem> _fixedUpdateSystems;
        protected internal ListArray<IEcsDestroySystem> _destroySystems;
        protected internal ListArray<IEcsPostDestroySystem> _postDestroySystems;
        #endregion

        #region Properties
        /// <summary>
        /// Список миров.
        /// </summary>
        public ListArray<CEcsWorld> Worlds
        {
            get { return _worlds; }
        }

        /// <summary>
        /// Список подсистем для реализации логики в момент инициализации.
        /// </summary>
        public ListArray<IEcsLotusPreInitSystem> PreInitSystems
        {
            get { return _preInitSystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики в момент инициализации.
        /// </summary>
        public ListArray<IEcsLotusInitSystem> InitSystems
        {
            get { return _initSystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики постоянного обновления.
        /// </summary>
        public ListArray<IEcsLotusUpdateSystem> UpdateSystems
        {
            get { return _updateSystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики постоянного обновления.
        /// </summary>
        public ListArray<IEcsLotusLateUpdateSystem> LateUpdateSystems
        {
            get { return _lateUpdateSystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики фиксированного обновления.
        /// </summary>
        public ListArray<IEcsLotusFixedUpdateSystem> FixedUpdateSystems
        {
            get { return _fixedUpdateSystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики удаления.
        /// </summary>
        public ListArray<IEcsDestroySystem> DestroySystems
        {
            get { return _destroySystems; }
        }

        /// <summary>
        /// Список подсистем для реализации логики удаления.
        /// </summary>
        public ListArray<IEcsPostDestroySystem> PostDestroySystems
        {
            get { return _postDestroySystems; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CEcsSystems()
        {
            _worlds = new ListArray<CEcsWorld>(2);

            _preInitSystems = new ListArray<IEcsLotusPreInitSystem>(24);
            _initSystems = new ListArray<IEcsLotusInitSystem>(24);
            _updateSystems = new ListArray<IEcsLotusUpdateSystem>(24);
            _lateUpdateSystems = new ListArray<IEcsLotusLateUpdateSystem>(24);
            _fixedUpdateSystems = new ListArray<IEcsLotusFixedUpdateSystem>(24);
            _destroySystems = new ListArray<IEcsDestroySystem>(24);
            _postDestroySystems = new ListArray<IEcsPostDestroySystem>(24);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Исполнения всех подсистем для реализации логики в момент инициализации.
        /// </summary>
        public void OnPreInitSystems()
        {
            for (var i = 0; i < _preInitSystems.Count; i++)
            {
                if (_preInitSystems[i].IsDisabled == false)
                {
                    _preInitSystems[i].PreInit(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики в момент инициализации.
        /// </summary>
        public void OnInitSystems()
        {
            for (var i = 0; i < _initSystems.Count; i++)
            {
                if (_initSystems[i].IsDisabled == false)
                {
                    _initSystems[i].Init(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики постоянного обновления.
        /// </summary>
        public void OnUpdateSystems()
        {
            for (var i = 0; i < _updateSystems.Count; i++)
            {
                if (_updateSystems[i].IsDisabled == false)
                {
                    _updateSystems[i].Update(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики постоянного обновления.
        /// </summary>
        public void OnLateUpdateSystems()
        {
            for (var i = 0; i < _lateUpdateSystems.Count; i++)
            {
                if (_lateUpdateSystems[i].IsDisabled == false)
                {
                    _lateUpdateSystems[i].LateUpdate(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики постоянного обновления.
        /// </summary>
        public void OnFixedUpdateSystems()
        {
            for (var i = 0; i < _fixedUpdateSystems.Count; i++)
            {
                if (_fixedUpdateSystems[i].IsDisabled == false)
                {
                    _fixedUpdateSystems[i].FixedUpdate(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики удаления.
        /// </summary>
        public void OnDestroySystems()
        {
            for (var i = 0; i < _destroySystems.Count; i++)
            {
                if (_destroySystems[i].IsDisabled == false)
                {
                    _destroySystems[i].Destroy(this);
                }
            }
        }

        /// <summary>
        /// Исполнения всех подсистем для реализации логики удаления.
        /// </summary>
        public void OnPostDestroySystems()
        {
            for (var i = 0; i < _postDestroySystems.Count; i++)
            {
                if (_postDestroySystems[i].IsDisabled == false)
                {
                    _postDestroySystems[i].PostDestroy(this);
                }
            }
        }
        #endregion
    }
    /**@}*/
}