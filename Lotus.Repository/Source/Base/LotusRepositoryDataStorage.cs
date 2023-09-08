﻿//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryDataStorage.cs
*		Определение основного интерфейса для работы сущностями.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using Lotus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryBase
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Основной интерфейс для работы сущностями
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusDataStorage
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить провайдер данных указанной сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <returns>Провайдер данных</returns>
			//---------------------------------------------------------------------------------------------------------
			IQueryable<TEntity> Query<TEntity>() where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущности в БД или в хранилище по идентификатору.
			/// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <typeparam name="TKey">Тип идентификатора</typeparam>
			/// <param name="id">Идентификатор сущности</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			ValueTask<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
				where TEntity : class
				where TKey : struct, IEquatable<TKey>;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущности в БД или в хранилище по идентификатору.
			/// Если сущность еще не добавлена в БД, но добавлена в хранилище вернет экземпляр.
			/// Если сущность отсутствует то она будет создана с указанным идентификатором
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <typeparam name="TKey">Тип идентификатора</typeparam>
			/// <param name="id">Идентификатор сущности</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			ValueTask<TEntity> GetOrAddAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
				where TEntity : class, ILotusIdentifierIdTemplate<TKey>, new()
				where TKey : struct, IEquatable<TKey>;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Добавленная сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken token = default)
					where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить список сущностей
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Задача</returns>
			//---------------------------------------------------------------------------------------------------------
			Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
				where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить указанную сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			/// <returns>Обновленная сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			TEntity Update<TEntity>(TEntity entity)
				where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить указанные сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			//---------------------------------------------------------------------------------------------------------
			void UpdateRange<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить указанную сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			//---------------------------------------------------------------------------------------------------------
			void Remove<TEntity>(TEntity entity)
				where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить указанные сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			//---------------------------------------------------------------------------------------------------------
			void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранить в хранилище все изменения
			/// </summary>
			/// <param name="token">Токен отмены</param>
			/// <returns>Задача</returns>
			//---------------------------------------------------------------------------------------------------------
			Task FlushAsync(CancellationToken token = default);
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================