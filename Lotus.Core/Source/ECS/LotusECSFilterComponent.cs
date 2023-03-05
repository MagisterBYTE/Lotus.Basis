//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ECS
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusECSFilterComponent.cs
*		Определение фильтра компонентов в системе ECS. Под фильтром компонентов понимается архетип содержащий список сущностей
*	с набором определённых компонентов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Runtime.CompilerServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreECS
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс фильтра компонентов для хранения списка сущностей с набором определенных компонентов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusEcsFilterComponent
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить сущность к фильтру
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			void AddEntity(Int32 entity_id);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка наличия сущности в фильтре
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			/// <returns>Статус наличия сущности</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean HasEntity(Int32 entity_id);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить сущность из фильтра
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			void RemoveEntity(Int32 entity_id);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включить сущности с указанным типом компонента в фильтр
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusEcsFilterComponent Include(Type component_type);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие компонента в фильтре
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			Boolean Exsist(Type component_type);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Исключить сущности с указанным типом компонента из фильтра
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			ILotusEcsFilterComponent Exclude(Type component_type);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить сущности фильтра по текущим данным
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			void UpdateFilter();

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить список идентификатор сущностей фильтра
			/// </summary>
			/// <returns>Список идентификатор сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			Int32[] GetEntities();
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Фильтр компонентов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CEcsFilterComponent : ILotusEcsFilterComponent
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal CEcsWorld mWorld;
			protected internal SparseSet mEntities;
			protected internal ListArray<Type> mIncludedComponents;
			protected internal ListArray<Type> mExcludedComponents;
			#endregion

			#region ======================================= СВОЙСТВА ===================================================
			/// <summary>
			/// Мир
			/// </summary>
			public CEcsWorld World
			{
				get
				{
					return (mWorld);
				}
				set
				{
					mWorld = value;
				}
			}

			/// <summary>
			/// Текущие количество сущностей в фильтре
			/// </summary>
			public Int32 CountEntities
			{
				get { return (mEntities.Count); }
			}

			/// <summary>
			/// Список типов компонентов которые включены в фильтр
			/// </summary>
			public ListArray<Type> IncludedComponents
			{
				get
				{
					return (mIncludedComponents);
				}
			}

			/// <summary>
			/// Список типов компонентов которые исключены из фильтра
			/// </summary>
			public ListArray<Type> ExcludedComponents
			{
				get
				{
					return (mExcludedComponents);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CEcsFilterComponent()
			{
				mEntities = new SparseSet(16);
				mIncludedComponents = new ListArray<Type>(8);
				mExcludedComponents = new ListArray<Type>(4);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить сущность к фильтру
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void AddEntity(Int32 entity_id)
			{
				mEntities.Add(entity_id);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка наличия сущности в фильтре
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			/// <returns>Статус наличия сущности</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean HasEntity(Int32 entity_id)
			{
				return(mEntities.Contains(entity_id));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить сущность из фильтра
			/// </summary>
			/// <param name="entity_id">Индентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void RemoveEntity(Int32 entity_id)
			{
				mEntities.Remove(entity_id);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включить сущности с указанным типом компонента в фильтр
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public CEcsFilterComponent Include<TComponent>() where TComponent : struct
			{
				Include(typeof(TComponent));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включить сущности с указанным типом компонента в фильтр
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ILotusEcsFilterComponent Include(Type component_type)
			{
				mIncludedComponents.AddIfNotContains(component_type);
				UpdateFilter();
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие компонента в фильтре
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean Exsist<TComponent>() where TComponent : struct
			{
				Type component_type = typeof(TComponent);
				return (mIncludedComponents.Contains(component_type) || mExcludedComponents.Contains(component_type));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие компонента в фильтре
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Boolean Exsist(Type component_type)
			{
				return (mIncludedComponents.Contains(component_type) || mExcludedComponents.Contains(component_type));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Исключить сущности с указанным типом компонента из фильтра
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public CEcsFilterComponent Exclude<TComponent>() where TComponent : struct
			{
				Exclude(typeof(TComponent));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Исключить сущности с указанным типом компонента из фильтра
			/// </summary>
			/// <param name="component_type">Тип компонента</param>
			/// <returns>Фильтр</returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ILotusEcsFilterComponent Exclude(Type component_type)
			{
				mExcludedComponents.AddIfNotContains(component_type);
				ILotusEcsComponentData component_data;
				if (mWorld.mComponentsData.TryGetValue(component_type, out component_data))
				{
					Int32[] exclude_entities = component_data.GetEntities();
					mEntities.RemoveValues(exclude_entities);
				}

				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить сущности фильтра по текущим данным
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void UpdateFilter()
			{
				mEntities.Clear();
				if(mIncludedComponents.Count > 1)
				{
					Type first_type_filter = mIncludedComponents[0];
					ILotusEcsComponentData component_data;
					if (mWorld.mComponentsData.TryGetValue(first_type_filter, out component_data))
					{
						Int32[] entities = component_data.GetEntities();
						for (Int32 i = 0; i < component_data.Count; i++)
						{
							Int32 id = entities[i];
							Int32 find_count = 0;
							for (Int32 f = 1; f < mIncludedComponents.Count; f++)
							{
								Type type_filter = mIncludedComponents[f];
								ILotusEcsComponentData filter_data;
								if (mWorld.mComponentsData.TryGetValue(type_filter, out filter_data))
								{
									if(filter_data.HasEntity(id))
									{
										find_count++;
									}
								}
								else
								{
									break;
								}
							}

							if(find_count == mIncludedComponents.Count - 1)
							{
								AddEntity(id);
							}
						}
					}
				}

				for (Int32 i = 0; i < mExcludedComponents.Count; i++)
				{
					ILotusEcsComponentData component_data;
					if (mWorld.mComponentsData.TryGetValue(mExcludedComponents[i], out component_data))
					{
						Int32[] include_entities = component_data.GetEntities();
						mEntities.RemoveValues(include_entities);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить список идентификатор сущностей фильтра
			/// </summary>
			/// <returns>Список идентификатор сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32[] GetEntities()
			{
				return (mEntities.mDenseItems);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================