//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема хранения в бинарном файле
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryBinaryFile.cs
*		Класс представляющий собой репозиторий для хранения сущностей в бинарном файле.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup RepositoryBinaryFile Подсистема хранения в бинарном файле
         * \ingroup Repository
         * \brief Реализация репозитория для хранения сущностей в бинарном файле.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс представляющий собой репозиторий для хранения сущностей в бинарном файле
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public abstract class RepositoryBinaryFile : ILotusRepository
		{
			#region ======================================= ДАННЫЕ ====================================================
			private string _fileName;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			protected RepositoryBinaryFile(String fileName)
			{
				_fileName = fileName;
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			/// <summary>
			/// Получение сущностей указанного типа
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <returns>Список указанного типа</returns>
			protected abstract IList<TEntity> GetEntities<TEntity>() where TEntity : class;
			#endregion

			#region ======================================= МЕТОДЫ ILotusRepository ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить провайдер данных указанной сущности
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <returns>Провайдер данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public IQueryable<TEntity> Query<TEntity>() where TEntity : class
			{
				return GetEntities<TEntity>().AsQueryable();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущности в хранилище по идентификатору.
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <typeparam name="TKey">Тип идентификатора</typeparam>
			/// <param name="id">Идентификатор сущности</param>
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TEntity? GetById<TEntity, TKey>(TKey id) where TEntity : class
															where TKey : struct, IEquatable<TKey>
			{
				return null;
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
				return await ValueTask.FromResult<TEntity?>(null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущностей в БД или в хранилище по идентификаторам.
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <typeparam name="TKey">Тип идентификатора</typeparam>
			/// <param name="ids">Список идентифиакторов</param>
			/// <returns>Список сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public IList<TEntity?> GetByIds<TEntity, TKey>(IEnumerable<TKey> ids)
				where TEntity : class, ILotusIdentifierIdTemplate<TKey>
				where TKey : struct, IEquatable<TKey>
			{
				return GetEntities<TEntity>().Where(x => ids.Contains(x.Id)).ToArray();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущностей в БД или в хранилище по идентификаторам.
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <typeparam name="TKey">Тип идентификатора</typeparam>
			/// <param name="ids">Список идентифиакторов</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Список сущностей</returns>
			//---------------------------------------------------------------------------------------------------------
			public async ValueTask<IList<TEntity?>> GetByIdsAsync<TEntity, TKey>(IEnumerable<TKey> ids, CancellationToken token = default)
				where TEntity : class, ILotusIdentifierIdTemplate<TKey>
				where TKey : struct, IEquatable<TKey>
			{
				return await ValueTask.FromResult(GetEntities<TEntity>().Where(x => ids.Contains(x.Id)).ToArray());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущности в БД или в хранилище по ее имени.
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="name">Имя сущности</param>
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TEntity? GetByName<TEntity>(String name) where TEntity : class, ILotusNameable
			{
				var entity = GetEntities<TEntity>().FirstOrDefault(x => x.Name == name);
				return entity;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Производит поиск сущности в БД или в хранилище по ее имени.
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="name">Имя сущности</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public async ValueTask<TEntity?> GetByNameAsync<TEntity>(String name, CancellationToken token = default)
				where TEntity : class, ILotusNameable
			{
				var entity = await ValueTask.FromResult(GetEntities<TEntity>().FirstOrDefault(x => x.Name == name));
				return entity;
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
			/// <returns>Найденная сущность или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TEntity GetOrAdd<TEntity, TKey>(TKey id) where TEntity : class, ILotusIdentifierIdTemplate<TKey>, new()
															where TKey : struct, IEquatable<TKey>
			{
				var entitiesCurrent = GetEntities<TEntity>();
				var entity = entitiesCurrent.FirstOrDefault(x => x.Id.Equals(id));
				if (entity == null)
				{
					entity = new TEntity
					{
						Id = id
					};

					entitiesCurrent.Add(entity);
				}

				return entity;
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
				var entitiesCurrent = GetEntities<TEntity>();
				var entity = entitiesCurrent.FirstOrDefault(x => x.Id.Equals(id));
				if (entity == null)
				{
					entity = new TEntity
					{
						Id = id
					};

					entitiesCurrent.Add(entity);
				}

				return await ValueTask.FromResult(entity);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить сущность
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entity">Сущность</param>
			/// <returns>Добавленная сущность</returns>
			//---------------------------------------------------------------------------------------------------------
			public TEntity Add<TEntity>(TEntity entity) where TEntity : class
			{
				var entitiesCurrent = GetEntities<TEntity>();
				entitiesCurrent.Add(entity);
				return entity;
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
				var entitiesCurrent = GetEntities<TEntity>();
				entitiesCurrent.Add(entity);
				return await ValueTask.FromResult(entity);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить список сущностей
			/// </summary>
			/// <typeparam name="TEntity">Тип сущности</typeparam>
			/// <param name="entities">Список сущностей</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
			{
				var entitiesCurrent = GetEntities<TEntity>();
				foreach (var entity in entities)
				{
					entitiesCurrent.Add(entity);
				}
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
				var entitiesCurrent = GetEntities<TEntity>();
				foreach (var entity in entities)
				{
					entitiesCurrent.Add(entity);
				}

				await Task.CompletedTask;
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
				var entitiesCurrent = GetEntities<TEntity>();
				return entitiesCurrent.FirstOrDefault(entity);
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
				var entitiesCurrent = GetEntities<TEntity>();
				entitiesCurrent.Remove(entity);
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
				var entitiesCurrent = GetEntities<TEntity>();
				foreach(var entity in entities)
				{
					entitiesCurrent.Remove(entity);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранить в хранилище все изменения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Flush()
			{
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
				await Task.CompletedTask;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================