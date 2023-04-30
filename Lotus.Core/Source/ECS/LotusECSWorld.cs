//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ECS
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusECSWorld.cs
*		Определение мира, содержащего все взаимосвязанные сущности, компоненты и фильтры.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreECS
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конфигурация начальных настроек мира
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CEcsWorldConfigs
		{
			public Int32 EntityCachSize = 1024;
			public Int32 ComponentCachSize = 1024;
			public Int32 PoolsCachSize = 128;
			public Int32 EntityTypesCachSize = 256;
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Мир содержащий все взаимосвязанные сущности, компоненты и фильтры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CEcsWorld
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Сущности
			protected internal Int32 mCountEntity;
			protected internal Int32 mMaxCountEntity;
			protected internal Int32 mCurrentIdEntity = 1;
			protected internal TEcsEntity[] mDenseEntities;
			protected internal TEcsEntity mDummyEntity = new TEcsEntity(-1);
			protected internal Int32[] mSparseEntities;
			protected internal Int32[] mRemovedEntities;
			protected internal Int32 mCountRemovedEntity;

			// Компоненты
			protected internal Dictionary<Type, ILotusEcsComponentData> mComponentsData;

			// Фильтры
			protected internal ListArray<CEcsFilterComponent> mFilterComponents;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Количество сущностей
			/// </summary>
			public Int32 CountEntity
			{
				get { return (mCountEntity); }
			}

			/// <summary>
			/// Массив сущностей
			/// </summary>
			public TEcsEntity[] Entities
			{
				get { return (mDenseEntities); }
			}

			/// <summary>
			/// Словарь компонентов и связанных сущностей
			/// </summary>
			public Dictionary<Type, ILotusEcsComponentData> ComponentsData
			{
				get { return (mComponentsData); }
			}

			/// <summary>
			/// Фильтры компонентов
			/// </summary>
			public ListArray<CEcsFilterComponent> FilterComponents
			{
				get { return (mFilterComponents); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			/// <param name="configs">Конфигурация начальных настроек мира</param>
			//---------------------------------------------------------------------------------------------------------
			public CEcsWorld(CEcsWorldConfigs configs = null)
			{
				mMaxCountEntity = configs is null ? 512 : configs.EntityCachSize;
				mDenseEntities = new TEcsEntity[mMaxCountEntity];
				mSparseEntities = new Int32[mMaxCountEntity];
				mRemovedEntities = new Int32[mMaxCountEntity];

				mComponentsData = new Dictionary<Type, ILotusEcsComponentData>(configs is null ? 512 : configs.ComponentCachSize);

				mFilterComponents = new ListArray<CEcsFilterComponent>(24);
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С СУЩНОСТЯМИ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание новой сущности
			/// </summary>
			/// <returns>Ссылка на новую сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			public ref TEcsEntity NewEntity()
			{
				Int32 current_id;

				// Смотрим есть у нас освобожденный идентификатор
				if (mCountRemovedEntity > 0)
				{
					mCountRemovedEntity--;
					current_id = mRemovedEntities[mCountRemovedEntity];
				}
				else
				{
					current_id = mCurrentIdEntity;
					mCurrentIdEntity++;
				}

				if (current_id >= mMaxCountEntity)
				{
					mMaxCountEntity = Math.Max(current_id + 1, mMaxCountEntity << 1);
					Array.Resize(ref mDenseEntities, mMaxCountEntity);
					Array.Resize(ref mSparseEntities, mMaxCountEntity);
					Array.Resize(ref mRemovedEntities, mMaxCountEntity);
				}

				if (mCountEntity >= mMaxCountEntity)
				{
					mMaxCountEntity = mMaxCountEntity << 1;
					Array.Resize(ref mDenseEntities, mMaxCountEntity);
					Array.Resize(ref mSparseEntities, mMaxCountEntity);
					Array.Resize(ref mRemovedEntities, mMaxCountEntity);
				}

				var current_count = mCountEntity;
				mSparseEntities[current_id] = current_count;
				mDenseEntities[current_count] = new TEcsEntity(current_id);
				mCountEntity++;

				return ref mDenseEntities[current_count];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование сущности с указным идентификатором
			/// </summary>
			/// <param name="id">Идентификатор сущности</param>
			/// <returns>Статус существование сущности</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsEntity(Int32 id)
			{
				return (mSparseEntities[id] < mCountEntity && mDenseEntities[mSparseEntities[id]].Id == id);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение сущности по ее идентификатору
			/// </summary>
			/// <param name="id">Идентификатор сущности</param>
			/// <returns>Ссылка на существующую сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			public ref TEcsEntity GetEntity(Int32 id)
			{
				if(mSparseEntities[id] < mCountEntity && mDenseEntities[mSparseEntities[id]].Id == id)
				{
					return ref mDenseEntities[mSparseEntities[id]];
				}
				else
				{
					return ref mDummyEntity;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление сущности по ее идентификатору
			/// </summary>
			/// <param name="id">Идентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveEntity(Int32 id)
			{
				mRemovedEntities[mCountRemovedEntity] = id;
				mCountRemovedEntity++;

				mDenseEntities[mSparseEntities[id]] = mDenseEntities[mCountEntity - 1];
				mSparseEntities[mDenseEntities[mCountEntity - 1].Id] = mSparseEntities[id];
				mCountEntity--;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить список идентификатор сущностей для данного типа компонента
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Список идентификатор сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32[] GetEntities<TComponent>() where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					return (component_data.GetEntities());
				}

				return (null);
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С КОМПОНЕНТАМИ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление к указанной сущности компонента указанного типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			/// <returns>Ссылка на добавленный компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public ref TComponent AddComponent<TComponent>(Int32 entity_id) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;

					if (component_data_exist.HasEntity(entity_id) == false)
					{
						UpdateFilterComponentsForAdd(component_type);

						ref TComponent value = ref component_data_exist.AddEntity(entity_id);
						return ref value;
					}
					else
					{
						throw new Exception($"Component <{typeof(TComponent).Name}> already attached to entity <{entity_id}>");
					}
				}
				else
				{
					var component_data_new = new CEcsComponentData<TComponent>();
					component_data_new.World = this;
					mComponentsData.Add(component_type, component_data_new);
					ref TComponent value = ref component_data_new.AddEntity(entity_id);
					return ref value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение от указанной сущности компонента указанного типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			/// <returns>Ссылка на существующий/добавленный компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public ref TComponent GetOrAddComponent<TComponent>(Int32 entity_id) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;

					if (component_data_exist.HasEntity(entity_id) == false)
					{
						UpdateFilterComponentsForAdd(component_type);

						ref TComponent value = ref component_data_exist.AddEntity(entity_id);
						return ref value;
					}
					else
					{
						ref TComponent value = ref component_data_exist.GetValue(entity_id);
						return ref value;
					}
				}
				else
				{
					var component_data_new = new CEcsComponentData<TComponent>();
					mComponentsData.Add(component_type, component_data_new);
					ref TComponent value = ref component_data_new.AddEntity(entity_id);
					return ref value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие компонента определённого типа у указанной сущности
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			/// <returns>Статус наличия компонента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean HasComponent<TComponent>(Int32 entity_id) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					return (component_data.HasEntity(entity_id));
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение от указанной сущности компонента указанного типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			/// <returns>Ссылка на существующий компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public ref TComponent GetComponent<TComponent>(Int32 entity_id) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;
					ref TComponent value = ref component_data_exist.GetValue(entity_id);
					return ref value;
				}
				
				throw new Exception($"Component <{typeof(TComponent).Name}> not attached to entity <{entity_id}>");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значение компонента у указанной сущности
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			/// <param name="value">Компонент</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateComponent<TComponent>(Int32 entity_id, in TComponent value) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;
					component_data_exist.SetValue(entity_id, in value);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление у указанной сущности компонента определённого типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="entity_id">Идентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveComponent<TComponent>(Int32 entity_id) where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;
					component_data_exist.RemoveEntity(entity_id);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение массива компонентов указанного типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Массив компонентов</returns>
			//---------------------------------------------------------------------------------------------------------
			public TComponent[] GetComponents<TComponent>() where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;
					return component_data_exist.Components.mItems;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение взаимосвязанного списка сущностей и компонента определённого типа
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Список сущностей и компонента определённого типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public CEcsComponentData<TComponent> GetComponentData<TComponent>() where TComponent : struct
			{
				var component_type = typeof(TComponent);
				ILotusEcsComponentData component_data;
				if (mComponentsData.TryGetValue(component_type, out component_data))
				{
					var component_data_exist = component_data as CEcsComponentData<TComponent>;
					return component_data_exist;
				}

				return null;
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С ФИЛЬТРАМИ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание пустого фильтра компонентов
			/// </summary>
			/// <returns>Фильтр компонентов</returns>
			//---------------------------------------------------------------------------------------------------------
			public CEcsFilterComponent CreateFilterComponent()
			{
				var filter_сomponent = new CEcsFilterComponent();
				filter_сomponent.World = this;
				mFilterComponents.Add(filter_сomponent);
				return (filter_сomponent);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление списка фильтров компонентов для случая добавление нового компонента
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateFilterComponentsForAdd(Type component_type)
			{
				for (var i = 0; i < mFilterComponents.Count; i++)
				{
					if (mFilterComponents[i].IncludedComponents.Contains(component_type))
					{
						mFilterComponents[i].UpdateFilter();
					}
				}
			}
			#endregion

		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================