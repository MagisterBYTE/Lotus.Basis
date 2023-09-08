//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема EF Core
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryDataStorageContextDb.cs
*		Класс представляющий собой основной контекст БД для работы сущностями.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using Lotus.Core;
using Microsoft.EntityFrameworkCore;
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
		/**
         * \defgroup RepositoryEFCore Подсистема EF Core
         * \ingroup Repository
         * \brief Подсистема EF Core для работы с базами данных.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой основной контекст БД для работы сущностями
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class DataStorageContextDb<TContext> : ILotusDataStorage where TContext : DbContext
		{
			#region ======================================= ДАННЫЕ ====================================================
			private readonly TContext _context;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="context">Контекст БД</param>
			//---------------------------------------------------------------------------------------------------------
			public DataStorageContextDb(TContext context)
			{
				_context = context;
			}
			#endregion

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить провайдер данных указанной сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <returns>Провайдер данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public IQueryable<TEntity> Query<TEntity>() where TEntity : class
			{
				return _context.Set<TEntity>();
			}

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
			public async ValueTask<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
				where TEntity : class
				where TKey : struct, IEquatable<TKey>
			{
				return await _context.FindAsync<TEntity>(new object[] { id }, token);
			}

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
			public async ValueTask<TEntity> GetOrAddAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
				where TEntity : class, ILotusIdentifierIdTemplate<TKey>, new()
				where TKey : struct, IEquatable<TKey>
			{
				var entity = await _context.FindAsync<TEntity>(new object[] { id }, token);
				if(entity == null) 
				{
					entity = new TEntity();
					entity.Id = id;

					var entry = await _context.AddAsync(entity, token);
					return entry.Entity;
				}
				else
				{
					return entity;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Добавленная сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			public async ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken token = default)
					where TEntity : class
			{
				var entry = await _context.AddAsync(entity, token);
				return entry.Entity;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить список сущностей
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Задача</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
				where TEntity : class
			{
				await _context.AddRangeAsync(entities, token);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить указанную сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			/// <returns>Обновленная сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			public TEntity Update<TEntity>(TEntity entity)
				where TEntity : class
			{
				return _context.Update(entity).Entity;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновить указанные сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateRange<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				_context.UpdateRange(entities);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить указанную сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			//---------------------------------------------------------------------------------------------------------
			public void Remove<TEntity>(TEntity entity)
				where TEntity : class
			{
				_context.Remove(entity);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удалить указанные сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
				where TEntity : class
			{
				_context.RemoveRange(entities);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранить в хранилище все изменения
			/// </summary>
			/// <param name="token">Токен отмены</param>
			/// <returns>Задача</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task FlushAsync(CancellationToken token = default)
			{
				await _context.SaveChangesAsync(token);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================