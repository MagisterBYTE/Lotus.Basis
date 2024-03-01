using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mapster;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryPageInfo
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для работы с IQueryable.
    /// </summary>
    public static class XQueryableExtensionsPageInfo
    {
        #region Const
        /// <summary>
        /// Максимальный размер страницы.
        /// </summary>
        private const int MaxPageSize = 9999;
        #endregion

        /// <summary>
        /// Получение постраничного запроса данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <returns>Запрос.</returns>
        public static IQueryable<TEntity> Paging<TEntity>(this IOrderedQueryable<TEntity> query, int pageNumber,
                int pageSize)
        {
            query = query ?? throw new ArgumentNullException(nameof(query));

            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Value should be equal or more 0");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Value should be equal or more 0");
            }

            if (pageSize == 0 || pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            return query.Skip(pageNumber * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="request">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TResponse>> ToResponsePageAsync<TEntity, TResponse>(
            this IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
        {
            if (request.PageInfo is null)
            {
                return await query.ToResponsePageAsync<TEntity, TResponse>(token);
            }
            else
            {
                return await query.ToResponsePageAsync<TEntity, TResponse>(request.PageInfo.PageNumber,
                    request.PageInfo.PageSize, token);
            }
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="request">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TEntity>> ToResponsePageAsync<TEntity>(
            this IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
        {
            if (request.PageInfo is null)
            {
                return await query.ToResponsePageAsync(token);
            }
            else
            {
                return await query.ToResponsePageAsync(request.PageInfo.PageNumber,
                    request.PageInfo.PageSize, token);
            }
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TResponse>> ToResponsePageAsync<TEntity, TResponse>(
            this IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
        {
            var totalCount = await query.CountAsync(token);
            var data = await query.Paging(page, pageSize)
                .ProjectToType<TResponse>()
                .ToArrayAsync(token);

            var pageInfo = new PageInfoResponse()
            {
                PageNumber = page,
                PageSize = pageSize,
                CurrentPageSize = data.Length,
                TotalCount = totalCount,
            };

            var paging = new ResponsePage<TResponse>
            {
                PageInfo = pageInfo,
                Payload = data
            };

            return paging;
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TEntity>> ToResponsePageAsync<TEntity>(
            this IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
        {
            var totalCount = await query.CountAsync(token);
            var data = await query.Paging(page, pageSize)
                .ToArrayAsync(token);

            var pageInfo = new PageInfoResponse()
            {
                PageNumber = page,
                PageSize = pageSize,
                CurrentPageSize = data.Length,
                TotalCount = totalCount,
            };

            var paging = new ResponsePage<TEntity>
            {
                PageInfo = pageInfo,
                Payload = data
            };

            return paging;
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TResponse>> ToResponsePageAsync<TEntity, TResponse>(
            this IOrderedQueryable<TEntity> query, CancellationToken token = default)
        {
            var totalCount = await query.CountAsync(token);
            var data = await query.ProjectToType<TResponse>().ToArrayAsync(token);

            var pageInfo = new PageInfoResponse()
            {
                PageNumber = 0,
                PageSize = totalCount,
                CurrentPageSize = data.Length,
                TotalCount = totalCount,
            };

            var paging = new ResponsePage<TResponse>
            {
                PageInfo = pageInfo,
                Payload = data
            };

            return paging;
        }

        /// <summary>
        /// Получение постраничных данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Ответ.</returns>
        public static async Task<ResponsePage<TEntity>> ToResponsePageAsync<TEntity>(
            this IOrderedQueryable<TEntity> query, CancellationToken token = default)
        {
            var totalCount = await query.CountAsync(token);
            var data = await query.ToArrayAsync(token);

            var pageInfo = new PageInfoResponse()
            {
                PageNumber = 0,
                PageSize = totalCount,
                CurrentPageSize = data.Length,
                TotalCount = totalCount,
            };

            var paging = new ResponsePage<TEntity>
            {
                PageInfo = pageInfo,
                Payload = data
            };

            return paging;
        }
    }
    /**@}*/
}