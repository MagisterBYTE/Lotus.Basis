﻿//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryEntity.cs
*		Определение базовой сущности репозитория.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup RepositoryBase Базовая подсистема
         * \ingroup Repository
         * \brief Базовая подсистема определяет основные сущности репозитория.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определение базовой сущности репозитория
		/// </summary>
		/// <typeparam name="TKey">Тип ключа(идентификатора)</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusEntity<TKey> : ILotusIdentifierId<TKey> where TKey : struct, IEquatable<TKey> 
		{
			/// <summary>
			/// Дата создания сущности
			/// </summary>
			DateTime Created { get; set; }

			/// <summary>
			/// Дата последней модификации сущности
			/// </summary>
			DateTime Modified { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение базовой сущности репозитория
		/// </summary>
		/// <typeparam name="TKey">Тип ключа(идентификатора)</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public abstract class EntityBase<TKey> : ILotusEntity<TKey> where TKey : struct, IEquatable<TKey>
		{
			/// <summary>
			/// Идентификатор объекта
			/// </summary>
			public TKey Id { get; set; }

			/// <summary>
			/// Дата создания сущности
			/// </summary>
			public DateTime Created { get; set; }

			/// <summary>
			/// Дата последней модификации сущности
			/// </summary>
			public DateTime Modified { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение базовой сущности репозитория с поддержкой глобального уникального идентификатора
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public abstract class CEntityBase : EntityBase<Guid>
		{
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================