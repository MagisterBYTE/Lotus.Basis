﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ECS
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusECSEntity.cs
*		Определение сущности в подсистеме ECS.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup CoreECS Подсистема ECS
         * \ingroup Core
         * \brief Подсистема ECS - это архитектурный шаблон представления объектов как правило игрового мира. ECS состоит из
			сущностей, к которым прикреплены компоненты содержащие данные, и системами, которые работают
			с компонентами сущностей. 
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения сущности
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusEcsEntity
		{
			/// <summary>
			/// Идентификатор сущности
			/// </summary>
			/// <remarks>
			/// Является уникальным в пределах одного мира
			/// </remarks>
			Int32 Id { get; }

			/// <summary>
			/// Статус активности сущности
			/// </summary>
			/// <remarks>
			/// Только активные сущности обрабатываются системами
			/// </remarks>
			Boolean IsEnabled { get; set; }

			/// <summary>
			/// Слой расположения сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			Byte Layer { get; set; }

			/// <summary>
			/// Тег сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			Byte Tag { get; set; }

			/// <summary>
			/// Группа, которой принадлежит сущность
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			Byte Group { get; set; }

			/// <summary>
			/// Статус маркировки сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			Byte Marked { get; set; }

			/// <summary>
			/// Количество компонентов 
			/// </summary>
			Int32 ComponentCount { get; }

			/// <summary>
			/// Статус уничтожения сущности
			/// </summary>
			Boolean IsDestroyed { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Сущность
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TEcsEntity : ILotusEcsEntity, IEquatable<TEcsEntity>, IComparable<TEcsEntity>
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Int32 _id;
			internal Boolean _isEnabled;
			internal Byte _layer;
			internal Byte _tag;
			internal Byte _group;
			internal Byte _marked;
			internal Int32 _componentCount;
			internal Boolean _isDestroyed;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Идентификатор сущности
			/// </summary>
			/// <remarks>
			/// Является уникальным в пределах одного мира
			/// </remarks>
			public readonly Int32 Id 
			{ 
				get 
				{
					return _id;
				}
			}

			/// <summary>
			/// Статус активности сущности
			/// </summary>
			/// <remarks>
			/// Только активные сущности обрабатываются системами
			/// </remarks>
			public Boolean IsEnabled
			{
				readonly get
				{
					return _isEnabled;
				}
				set
				{
					_isEnabled = value;
				}
			}

			/// <summary>
			/// Слой расположения сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			public Byte Layer
			{
				readonly get
				{
					return _layer;
				}
				set
				{
					_layer = value;
				}
			}

			/// <summary>
			/// Тег сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			public Byte Tag
			{
				readonly get
				{
					return _tag;
				}
				set
				{
					_tag = value;
				}
			}

			/// <summary>
			/// Группа, которой принадлежит сущность
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			public Byte Group
			{
				readonly get
				{
					return _group;
				}
				set
				{
					_group = value;
				}
			}

			/// <summary>
			/// Статус маркировки сущности
			/// </summary>
			/// <remarks>
			/// Является вспомогательной характеристикой сущности и используется на усмотрения пользователя
			/// </remarks>
			public Byte Marked
			{
				readonly get
				{
					return _marked;
				}
				set
				{
					_marked = value;
				}
			}

			/// <summary>
			/// Количество компонентов 
			/// </summary>
			public readonly Int32 ComponentCount
			{
				get
				{
					return _componentCount;
				}
			}

			/// <summary>
			/// Статус уничтожения сущности
			/// </summary>
			public Boolean IsDestroyed
			{
				readonly get
				{
					return _isDestroyed;
				}
				set
				{
					_isDestroyed = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует сущность указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор сущности</param>
			//---------------------------------------------------------------------------------------------------------
			public TEcsEntity(Int32 id)
			{
				_id = id;
				_isEnabled = true;
				_layer = 0;
				_tag = 0;
				_group = 0;
				_marked = 0;
				_marked = 0;
				_componentCount = 0;
				_isDestroyed = false;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (obj is TEcsEntity entity)
					{
						return Equals(entity);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства сущностей по значению
			/// </summary>
			/// <param name="other">Сущность</param>
			/// <returns>Статус равенства сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Boolean Equals(TEcsEntity other)
			{
				return _id == other.Id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение сущностей для упорядочивания
			/// </summary>
			/// <param name="other">Сущность</param>
			/// <returns>Статус сравнения сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Int32 CompareTo(TEcsEntity other)
			{
				return _id.CompareTo(other.Id);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода сущности
			/// </summary>
			/// <returns>Хеш-код сущности</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Int32 GetHashCode()
			{
				return _id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление сущности с указанием значений</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly String ToString()
			{
				return _id.ToString();
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение сущностей на равенство
			/// </summary>
			/// <param name="left">Первый сущность</param>
			/// <param name="right">Второй сущность</param>
			/// <returns>Статус равенства сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(TEcsEntity left, TEcsEntity right)
			{
				return left.Id == right.Id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение сущностей на неравенство
			/// </summary>
			/// <param name="left">Первый сущность</param>
			/// <param name="right">Второй сущность</param>
			/// <returns>Статус неравенства сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(TEcsEntity left, TEcsEntity right)
			{
				return left.Id != right.Id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Реализация лексикографического порядка отношений сущностей
			/// </summary>
			/// <param name="left">Левый сущность</param>
			/// <param name="right">Правый сущность</param>
			/// <returns>Статус меньше</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator <(TEcsEntity left, TEcsEntity right)
			{
				return left.Id < right.Id;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Реализация лексикографического порядка отношений сущностей
			/// </summary>
			/// <param name="left">Левый сущность</param>
			/// <param name="right">Правый сущность</param>
			/// <returns>Статус больше</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator >(TEcsEntity left, TEcsEntity right)
			{
				return left.Id > right.Id;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================