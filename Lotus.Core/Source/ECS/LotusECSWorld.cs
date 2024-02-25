using System;
using System.Collections.Generic;

namespace Lotus.Core
{
    /** \addtogroup CoreECS
	*@{*/
    /// <summary>
    /// Конфигурация начальных настроек мира.
    /// </summary>
    public class CEcsWorldConfigs
    {
        public int EntityCachSize = 1024;
        public int ComponentCachSize = 1024;
        public int PoolsCachSize = 128;
        public int EntityTypesCachSize = 256;
    }

    /// <summary>
    /// Мир содержащий все взаимосвязанные сущности, компоненты и фильтры.
    /// </summary>
    public class CEcsWorld
    {
        #region Fields
        // Сущности
        protected internal int _countEntity;
        protected internal int _maxCountEntity;
        protected internal int _currentIdEntity = 1;
        protected internal TEcsEntity[] _denseEntities;
        protected internal TEcsEntity _dummyEntity = new TEcsEntity(-1);
        protected internal int[] _sparseEntities;
        protected internal int[] _removedEntities;
        protected internal int _countRemovedEntity;

        // Компоненты
        protected internal Dictionary<Type, ILotusEcsComponentData> _componentsData;

        // Фильтры
        protected internal ListArray<CEcsFilterComponent> _filterComponents;
        #endregion

        #region Properties
        /// <summary>
        /// Количество сущностей.
        /// </summary>
        public int CountEntity
        {
            get { return _countEntity; }
        }

        /// <summary>
        /// Массив сущностей.
        /// </summary>
        public TEcsEntity[] Entities
        {
            get { return _denseEntities; }
        }

        /// <summary>
        /// Словарь компонентов и связанных сущностей.
        /// </summary>
        public Dictionary<Type, ILotusEcsComponentData> ComponentsData
        {
            get { return _componentsData; }
        }

        /// <summary>
        /// Фильтры компонентов.
        /// </summary>
        public ListArray<CEcsFilterComponent> FilterComponents
        {
            get { return _filterComponents; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        /// <param name="configs">Конфигурация начальных настроек мира.</param>
        public CEcsWorld(CEcsWorldConfigs? configs = null)
        {
            _maxCountEntity = configs is null ? 512 : configs.EntityCachSize;
            _denseEntities = new TEcsEntity[_maxCountEntity];
            _sparseEntities = new int[_maxCountEntity];
            _removedEntities = new int[_maxCountEntity];

            _componentsData = new Dictionary<Type, ILotusEcsComponentData>(configs is null ? 512 : configs.ComponentCachSize);

            _filterComponents = new ListArray<CEcsFilterComponent>(24);
        }
        #endregion

        #region РАБОТЫ methods
        /// <summary>
        /// Создание новой сущности.
        /// </summary>
        /// <returns>Ссылка на новую сущность.</returns>
        public ref TEcsEntity NewEntity()
        {
            int current_id;

            // Смотрим есть у нас освобожденный идентификатор
            if (_countRemovedEntity > 0)
            {
                _countRemovedEntity--;
                current_id = _removedEntities[_countRemovedEntity];
            }
            else
            {
                current_id = _currentIdEntity;
                _currentIdEntity++;
            }

            if (current_id >= _maxCountEntity)
            {
                _maxCountEntity = Math.Max(current_id + 1, _maxCountEntity << 1);
                Array.Resize(ref _denseEntities, _maxCountEntity);
                Array.Resize(ref _sparseEntities, _maxCountEntity);
                Array.Resize(ref _removedEntities, _maxCountEntity);
            }

            if (_countEntity >= _maxCountEntity)
            {
                _maxCountEntity = _maxCountEntity << 1;
                Array.Resize(ref _denseEntities, _maxCountEntity);
                Array.Resize(ref _sparseEntities, _maxCountEntity);
                Array.Resize(ref _removedEntities, _maxCountEntity);
            }

            var current_count = _countEntity;
            _sparseEntities[current_id] = current_count;
            _denseEntities[current_count] = new TEcsEntity(current_id);
            _countEntity++;

            return ref _denseEntities[current_count];
        }

        /// <summary>
        /// Проверка на существование сущности с указным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Статус существование сущности.</returns>
        public bool ContainsEntity(int id)
        {
            return _sparseEntities[id] < _countEntity && _denseEntities[_sparseEntities[id]].Id == id;
        }

        /// <summary>
        /// Получение сущности по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Ссылка на существующую сущность.</returns>
        public ref TEcsEntity GetEntity(int id)
        {
            if (_sparseEntities[id] < _countEntity && _denseEntities[_sparseEntities[id]].Id == id)
            {
                return ref _denseEntities[_sparseEntities[id]];
            }
            else
            {
                return ref _dummyEntity;
            }
        }

        /// <summary>
        /// Удаление сущности по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        public void RemoveEntity(int id)
        {
            _removedEntities[_countRemovedEntity] = id;
            _countRemovedEntity++;

            _denseEntities[_sparseEntities[id]] = _denseEntities[_countEntity - 1];
            _sparseEntities[_denseEntities[_countEntity - 1].Id] = _sparseEntities[id];
            _countEntity--;
        }

        /// <summary>
        /// Получить список идентификатор сущностей для данного типа компонента.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <returns>Список идентификатор сущностей.</returns>
        public int[]? GetEntities<TComponent>() where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                return component_data.GetEntities();
            }

            return null;
        }
        #endregion

        #region РАБОТЫ methods
        /// <summary>
        /// Добавление к указанной сущности компонента указанного типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <returns>Ссылка на добавленный компонент.</returns>
        public ref TComponent AddComponent<TComponent>(int entityId) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;

                if (component_data_exist.HasEntity(entityId) == false)
                {
                    UpdateFilterComponentsForAdd(component_type);

                    ref TComponent value = ref component_data_exist.AddEntity(entityId);
                    return ref value;
                }
                else
                {
                    throw new Exception($"Component <{typeof(TComponent).Name}> already attached to entity <{entityId}>");
                }
            }
            else
            {
                var component_data_new = new CEcsComponentData<TComponent>();
                component_data_new.World = this;
                _componentsData.Add(component_type, component_data_new);
                ref TComponent value = ref component_data_new.AddEntity(entityId);
                return ref value;
            }
        }

        /// <summary>
        /// Получение от указанной сущности компонента указанного типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <returns>Ссылка на существующий/добавленный компонент.</returns>
        public ref TComponent GetOrAddComponent<TComponent>(int entityId) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;

                if (component_data_exist.HasEntity(entityId) == false)
                {
                    UpdateFilterComponentsForAdd(component_type);

                    ref TComponent value = ref component_data_exist.AddEntity(entityId);
                    return ref value;
                }
                else
                {
                    ref TComponent value = ref component_data_exist.GetValue(entityId);
                    return ref value;
                }
            }
            else
            {
                var component_data_new = new CEcsComponentData<TComponent>();
                _componentsData.Add(component_type, component_data_new);
                ref TComponent value = ref component_data_new.AddEntity(entityId);
                return ref value;
            }
        }

        /// <summary>
        /// Проверка на наличие компонента определённого типа у указанной сущности.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <returns>Статус наличия компонента.</returns>
        public bool HasComponent<TComponent>(int entityId) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                return component_data.HasEntity(entityId);
            }

            return false;
        }

        /// <summary>
        /// Получение от указанной сущности компонента указанного типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <returns>Ссылка на существующий компонент.</returns>
        public ref TComponent GetComponent<TComponent>(int entityId) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;
                ref TComponent value = ref component_data_exist.GetValue(entityId);
                return ref value;
            }

            throw new Exception($"Component <{typeof(TComponent).Name}> not attached to entity <{entityId}>");
        }

        /// <summary>
        /// Обновление значение компонента у указанной сущности.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        /// <param name="value">Компонент.</param>
        public void UpdateComponent<TComponent>(int entityId, in TComponent value) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;
                component_data_exist.SetValue(entityId, in value);
            }
        }

        /// <summary>
        /// Удаление у указанной сущности компонента определённого типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <param name="entityId">Идентификатор сущности.</param>
        public void RemoveComponent<TComponent>(int entityId) where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;
                component_data_exist.RemoveEntity(entityId);
            }
        }

        /// <summary>
        /// Получение массива компонентов указанного типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <returns>Массив компонентов.</returns>
        public TComponent[]? GetComponents<TComponent>() where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;
                return component_data_exist.Components._items;
            }

            return null;
        }

        /// <summary>
        /// Получение взаимосвязанного списка сущностей и компонента определённого типа.
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента.</typeparam>
        /// <returns>Список сущностей и компонента определённого типа.</returns>
        public CEcsComponentData<TComponent>? GetComponentData<TComponent>() where TComponent : struct
        {
            var component_type = typeof(TComponent);
            ILotusEcsComponentData? component_data;
            if (_componentsData.TryGetValue(component_type, out component_data))
            {
                var component_data_exist = (component_data as CEcsComponentData<TComponent>)!;
                return component_data_exist;
            }

            return null;
        }
        #endregion

        #region РАБОТЫ methods
        /// <summary>
        /// Создание пустого фильтра компонентов.
        /// </summary>
        /// <returns>Фильтр компонентов.</returns>
        public CEcsFilterComponent CreateFilterComponent()
        {
            var filter_сomponent = new CEcsFilterComponent();
            filter_сomponent.World = this;
            _filterComponents.Add(filter_сomponent);
            return filter_сomponent;
        }

        /// <summary>
        /// Обновление списка фильтров компонентов для случая добавление нового компонента.
        /// </summary>
        /// <param name="componentType">Тип компонента.</param>
        public void UpdateFilterComponentsForAdd(Type componentType)
        {
            for (var i = 0; i < _filterComponents.Count; i++)
            {
                if (_filterComponents[i].IncludedComponents.Contains(componentType))
                {
                    _filterComponents[i].UpdateFilter();
                }
            }
        }
        #endregion

    }
    /**@}*/
}