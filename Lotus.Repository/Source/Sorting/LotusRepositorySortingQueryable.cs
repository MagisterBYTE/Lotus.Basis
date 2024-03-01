using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lotus.Repository
{
    /** \addtogroup RepositorySorting
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений для работы с интерфейсом <see cref="IQueryable"/>.
    /// </summary>
    public static class XQueryableExtensionSorting
    {
        #region Const
        /// <summary>
        /// Имя метода сортировки для первого свойства по возрастанию.
        /// </summary>
        private const string OrderBy = "OrderBy";

        /// <summary>
        /// Имя метода сортировки для первого свойства по убыванию.
        /// </summary>
        private const string OrderByDescending = "OrderByDescending";

        /// <summary>
        /// Имя метода сортировки для последующего свойства по возрастанию.
        /// </summary>
        private const string ThenBy = "ThenBy";

        /// <summary>
        /// Имя метода сортировки для последующего свойства по убыванию.
        /// </summary>
        private const string ThenByDescending = "ThenByDescending";
        #endregion

        /// <summary>
        /// Сортировка данных запроса по указанному свойству.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="isDecs">Статус сортировки по убыванию.</param>
        /// <returns>Запрос.</returns>
        public static IOrderedQueryable<TEntity> OrderByProperty<TEntity>(this IQueryable<TEntity> query,
                string propertyName, bool isDecs)
        {
            // Результат p
            var param = Expression.Parameter(typeof(TEntity), "p");

            // Результат: p.sortColumn
            var prop = Expression.Property(param, propertyName);

            // Результат: p => о.sortColumn
            var exp = Expression.Lambda(prop, param);

            var method = isDecs ? OrderByDescending : OrderBy;

            var types = new[] { query.ElementType, exp.Body.Type };

            var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

            return (query.Provider.CreateQuery<TEntity>(mce) as IOrderedQueryable<TEntity>)!;
        }

        /// <summary>
        /// Сортировка данных запроса по указанным параметрам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="properties">Параметры сортировки.</param>
        /// <returns>Запрос.</returns>
        public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query,
            params SortByProperty[]? properties)
        {
            if (properties == null || properties.Length == 0)
            {
                return query;
            }
            if (properties.Length == 1)
            {
                return query.OrderByProperty(properties[0].PropertyName, properties[0].IsDesc.GetValueOrDefault());
            }

            var firstTime = true;

            // The type that represents each row in the table
            var itemType = typeof(TEntity);

            // Name the parameter passed into the lamda "x", of the type TEntity
            var parameter = Expression.Parameter(itemType, "x");

            // Loop through the sorted columns to build the expression tree
            foreach (var property in properties)
            {
                // Get the property from the TEntity, based on the key
                var prop = Expression.Property(parameter, property.PropertyName);

                // Build something like x => x.Cassette or x => x.SlotNumber
                var exp = Expression.Lambda(prop, parameter);

                // Based on the sorting direction, get the right method
                var method = string.Empty;
                if (firstTime)
                {
                    method = (property.IsDesc == false) ? OrderBy : OrderByDescending;
                    firstTime = false;
                }
                else
                {
                    method = (property.IsDesc == false) ? ThenBy : ThenByDescending;
                }

                // itemType is the type of the TEntity
                // exp.Body.Type is the type of the property. Again, for Cassette, it's
                //     a String. For SlotNumber, it's a Double.
                var types = new Type[] { itemType, exp.Body.Type };

                // Build the call expression
                // It will look something like:
                //     OrderBy*(x => x.Cassette) or Order*(x => x.SlotNumber)
                //     ThenBy*(x => x.Cassette) or ThenBy*(x => x.SlotNumber)
                var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

                // Now you can run the expression against the collection
                query = query.Provider.CreateQuery<TEntity>(mce);
            }

            return query;
        }

        /// <summary>
        /// Сортировка данных запроса по указанным параметрам.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Ключ для сортировки по умолчанию.</typeparam>
        /// <param name="query">Запрос.</param>
        /// <param name="properties">Параметры сортировки.</param>
        /// <param name="keySelector">Выражение для ключа.</param>
        /// <returns>Запрос с поддержкой сортировки.</returns>
        public static IOrderedQueryable<TEntity> Sort<TEntity, TKey>(this IQueryable<TEntity> query,
            SortByProperty[]? properties, Expression<Func<TEntity, TKey>> keySelector)
        {
            if (properties == null || properties.Length == 0)
            {
                return query.OrderBy(keySelector);
            }

            var queryOrder = (Sort(query, properties) as IOrderedQueryable<TEntity>)!;
            return queryOrder;
        }
    }
    /**@}*/
}