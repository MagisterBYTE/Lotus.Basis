namespace Lotus.Core
{
    /** \addtogroup CoreECS
	*@{*/
    /// <summary>
    /// Интерфейс для хранения взаимосвязанного списка сущностей и компонента определённого типа.
    /// </summary>
    public interface ILotusEcsComponentData
    {
        /// <summary>
        /// Текущие количество компонентов.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Проверка наличия компонента для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <returns>Статус наличия компонента.</returns>
        bool HasEntity(int entityId);

        /// <summary>
        /// Удаление компонента для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        void RemoveEntity(int entityId);

        /// <summary>
        /// Получить список идентификатор сущностей для данного компонента.
        /// </summary>
        /// <returns>Список идентификатор сущностей.</returns>
        int[] GetEntities();
    }

    /// <summary>
    /// Класс для хранения взаимосвязанного списка сущностей и компонента определённого типа.
    /// </summary>
    /// <typeparam name="TComponent">Тип компонента.</typeparam>
    public class CEcsComponentData<TComponent> : ILotusEcsComponentData where TComponent : struct
    {
        #region Fields
        protected internal CEcsWorld _world;
        protected internal SparseSet<TComponent> _components;
        #endregion

        #region СВОЙСТВА 
        /// <summary>
        /// Мир.
        /// </summary>
        public CEcsWorld World
        {
            get
            {
                return _world;
            }
            set
            {
                _world = value;
            }
        }

        /// <summary>
        /// Текущие количество компонентов.
        /// </summary>
        public int Count
        {
            get { return _components.Count; }
        }

        /// <summary>
        /// Список компонентов.
        /// </summary>
        public SparseSet<TComponent> Components
        {
            get
            {
                return _components;
            }
            set
            {
                _components = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CEcsComponentData()
        {
            _components = new SparseSet<TComponent>(24);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="configs">Конфигурация начальных настроек мира.</param>
        public CEcsComponentData(CEcsWorldConfigs? configs = null)
        {
            _components = new SparseSet<TComponent>(24);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавить компонент для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <returns>Ссылка на созданный компонент.</returns>
        public ref TComponent AddEntity(int entityId)
        {
            _components.Add(entityId, default(TComponent));

            _world.GetEntity(entityId)._componentCount++;

            return ref _components.GetValue(entityId);
        }

        /// <summary>
        /// Получит или добавить компонент для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <returns>Ссылка на компонент.</returns>
        public ref TComponent GetOrAddEntity(int entityId)
        {
            if (_components.Contains(entityId))
            {
                return ref _components.GetValue(entityId);

            }
            else
            {
                _components.Add(entityId, default(TComponent));

                _world.GetEntity(entityId)._componentCount++;

                return ref _components.GetValue(entityId);
            }
        }

        /// <summary>
        /// Проверка наличия компонента для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <returns>Статус наличия компонента.</returns>
        public bool HasEntity(int entityId)
        {
            return _components.Contains(entityId);
        }

        /// <summary>
        /// Удаление компонента для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        public void RemoveEntity(int entityId)
        {
            _world.GetEntity(entityId)._componentCount--;

            _components.Remove(entityId);
        }

        /// <summary>
        /// Получить компонент для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <returns>Ссылка на компонент.</returns>
        public ref TComponent GetValue(int entityId)
        {
            return ref _components.GetValue(entityId);
        }

        /// <summary>
        /// Установить/обновить компонент для указанной сущности.
        /// </summary>
        /// <param name="entityId">Индентификатор сущности.</param>
        /// <param name="value">Компонент.</param>
        public void SetValue(int entityId, in TComponent value)
        {
            _components.SetValue(entityId, in value);
        }

        /// <summary>
        /// Получить список идентификатор сущностей для данного компонента.
        /// </summary>
        /// <returns>Список идентификатор сущностей.</returns>
        public int[] GetEntities()
        {
            return _components.GetIndexes();
        }
        #endregion
    }
    /**@}*/
}