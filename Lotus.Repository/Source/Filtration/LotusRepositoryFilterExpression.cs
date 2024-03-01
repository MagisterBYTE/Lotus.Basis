using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryFilter
	*@{*/
    /// <summary>
    /// Статический класс содержащий фильтры для запросов в виде деревьев выражений.
    /// </summary>
    public static class XExpressionFilters
    {
        /// <summary>
        /// Получить типизированную версию метода <see cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource)"/>.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <returns>Типизированная версия метода.</returns>
        public static MethodInfo GetEnumerableContainsMethod(TEntityPropertyType propertyType)
        {
            switch (propertyType)
            {
                case TEntityPropertyType.Boolean:
                    break;
                case TEntityPropertyType.Integer:
                    {
                        return XReflection.GetEnumerableContainsMethod(typeof(int));
                    }
                case TEntityPropertyType.Float:
                    {
                        return XReflection.GetEnumerableContainsMethod(typeof(float));
                    }
                case TEntityPropertyType.String:
                    {
                        return XReflection.GetEnumerableContainsMethod(typeof(string));
                    }
                case TEntityPropertyType.Enum:
                    break;
                case TEntityPropertyType.DateTime:
                    {
                        return XReflection.GetEnumerableContainsMethod(typeof(DateTime));
                    }
                case TEntityPropertyType.Guid:
                    {
                        return XReflection.GetEnumerableContainsMethod(typeof(Guid));
                    }
            }

            return XReflection.GetEnumerableContainsMethod(typeof(byte));
        }

        /// <summary>
        /// Получить типизированную версию метода <see cref="Enumerable.Any{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <returns>Типизированная версия метода.</returns>
        public static MethodInfo GetEnumerableAnyMethod(TEntityPropertyType propertyType)
        {
            switch (propertyType)
            {
                case TEntityPropertyType.Boolean:
                    break;
                case TEntityPropertyType.Integer:
                    {
                        return XReflection.GetEnumerableAnyMethod(typeof(int));
                    }
                case TEntityPropertyType.Float:
                    {
                        return XReflection.GetEnumerableAnyMethod(typeof(float));
                    }
                case TEntityPropertyType.String:
                    {
                        return XReflection.GetEnumerableAnyMethod(typeof(string));
                    }
                case TEntityPropertyType.Enum:
                    break;
                case TEntityPropertyType.DateTime:
                    {
                        return XReflection.GetEnumerableAnyMethod(typeof(DateTime));
                    }
                case TEntityPropertyType.Guid:
                    {
                        return XReflection.GetEnumerableAnyMethod(typeof(Guid));
                    }
            }

            return XReflection.GetEnumerableAnyMethod(typeof(byte));
        }

        /// <summary>
        /// Получить типизированную версию метода <see cref="Enumerable.All{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <returns>Типизированная версия метода.</returns>
        public static MethodInfo GetEnumerableAllMethod(TEntityPropertyType propertyType)
        {
            switch (propertyType)
            {
                case TEntityPropertyType.Boolean:
                    break;
                case TEntityPropertyType.Integer:
                    {
                        return XReflection.GetEnumerableAllMethod(typeof(int));
                    }
                case TEntityPropertyType.Float:
                    {
                        return XReflection.GetEnumerableAllMethod(typeof(float));
                    }
                case TEntityPropertyType.String:
                    {
                        return XReflection.GetEnumerableAllMethod(typeof(string));
                    }
                case TEntityPropertyType.Enum:
                    break;
                case TEntityPropertyType.DateTime:
                    {
                        return XReflection.GetEnumerableAllMethod(typeof(DateTime));
                    }
                case TEntityPropertyType.Guid:
                    {
                        return XReflection.GetEnumerableAllMethod(typeof(Guid));
                    }

            }

            return XReflection.GetEnumerableAllMethod(typeof(byte));
        }

        /// <summary>
        /// Получить выражение фильтра по данным фильтрации по свойству.
        /// </summary>
        /// <typeparam name="TItem">Тип объекта.</typeparam>
        /// <param name="filterProperty">Данные для фильтрации по свойству.</param>
        /// <returns>Выражение фильтра.</returns>
        public static Expression<Func<TItem, bool>> GetFilter<TItem>(FilterByProperty filterProperty)
        {
            // Создаем параметр дерева выражений
            var parameter = Expression.Parameter(typeof(TItem), "x");

            // Получаем информацию о свойстве по которому будем фильтровать
            var propertyInfo = filterProperty.GetPropertyInfo<TItem>();

            // Создаем свойство дерева выражений
            var property = Expression.Property(parameter, propertyInfo);

            Expression? body = null;

            switch (filterProperty.Function)
            {
                case TFilterFunction.Equals:
                    {
                        body = Expression.Equal(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.NotEqual:
                    {
                        body = Expression.NotEqual(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.LessThan:
                    {
                        body = Expression.LessThan(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.LessThanOrEqual:
                    {
                        body = Expression.LessThanOrEqual(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.GreaterThan:
                    {
                        body = Expression.GreaterThan(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.GreaterThanOrEqual:
                    {
                        body = Expression.GreaterThanOrEqual(property, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.Between:
                    {
                        var first = Expression.GreaterThan(property, filterProperty.GetConstantExpression(0));
                        var second = Expression.LessThan(property, filterProperty.GetConstantExpression(1));
                        body = Expression.And(first, second);
                    }
                    break;
                case TFilterFunction.Contains:
                    {
                        body = Expression.Call(property, XReflection.StringContainsMethod, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.StartsWith:
                    {
                        body = Expression.Call(property, XReflection.StringStartsWithMethod, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.EndsWith:
                    {
                        body = Expression.Call(property, XReflection.StringEndsWithMethod, filterProperty.GetConstantExpression());
                    }
                    break;
                case TFilterFunction.NotEmpty:
                    {
                        var notNull = Expression.NotEqual(property, Expression.Constant(null));
                        var notEmpty = Expression.NotEqual(property, Expression.Constant(string.Empty));
                        body = Expression.And(notNull, notEmpty);
                    }
                    break;
                case TFilterFunction.IncludeAny:
                case TFilterFunction.IncludeAll:
                    {
                        var propertyType = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType();
                        if (propertyType != null && propertyType.IsPrimitiveType() == false)
                        {
                            var lambdaContains = filterProperty.GetContainsInPropertyExpression(propertyType);

                            var anyMethod = XReflection.GetEnumerableAnyMethod(propertyType);

                            body = Expression.Call(null, anyMethod, property, lambdaContains);
                        }
                        else
                        {
                            body = filterProperty.GetContainsInObjectExpression(property);
                        }
                    }
                    break;
                case TFilterFunction.IncludeEquals:
                    {
                        var propertyType = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType();

                        if (propertyType != null && propertyType.IsPrimitiveType() == false)
                        {
                            var lambdaContains = filterProperty.GetContainsInPropertyExpression(propertyType);

                            var allMethod = XReflection.GetEnumerableAllMethod(propertyType);

                            body = Expression.Call(null, allMethod, property, lambdaContains);
                        }
                        else
                        {
                            body = filterProperty.GetContainsInObjectExpression(property);
                        }
                    }
                    break;
                case TFilterFunction.IncludeNone:
                    {
                        var propertyType = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType();

                        if (propertyType != null && propertyType.IsPrimitiveType() == false)
                        {
                            var lambdaNotContains = filterProperty.GetNotContainsInPropertyExpression(propertyType);

                            var allMethod = XReflection.GetEnumerableAllMethod(propertyType);

                            body = Expression.Call(null, allMethod, property, lambdaNotContains);
                        }
                        else
                        {
                            body = filterProperty.GetNotContainsInObjectExpression(property);
                        }
                    }
                    break;
                default:
                    break;
            }

            // Получаем итоговую лямбду
            var result = Expression.Lambda<Func<TItem, bool>>(body!, parameter);

            return result;
        }
    }
    /**@}*/
}