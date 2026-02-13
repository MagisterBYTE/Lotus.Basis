using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryFilter
	*@{*/
    /// <summary>
    /// Статический класс содержащий фильтры для запросов в виде деревьев выражений.
    /// </summary>
    public static class XFilterExpression
    {
        /// <summary>
        /// Получить выражение фильтра по данным фильтрации по свойству.
        /// </summary>
        /// <typeparam name="TItem">Тип объекта.</typeparam>
        /// <param name="filterProperty">Данные для фильтрации по свойству.</param>
        /// <returns>Выражение фильтра.</returns>
        public static Expression<Func<TItem, bool>> GetFilter<TItem>(FilterByProperty filterProperty)
        {
            var parameterExpression = Expression.Parameter(typeof(TItem), "p");

            var propertyExpression = parameterExpression.GetPropertyExpression(filterProperty.PropertyPath);
            var propertyInfo = (propertyExpression.Member as PropertyInfo)!;
            var propertyType = propertyInfo.PropertyType;

            Expression? body = null;

            switch (filterProperty.Function)
            {
                case TFilterFunction.Equals:
                    {
                        body = Expression.Equal(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.NotEqual:
                    {
                        body = Expression.NotEqual(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.LessThan:
                    {
                        body = Expression.LessThan(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.LessThanOrEqual:
                    {
                        body = Expression.LessThanOrEqual(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.GreaterThan:
                    {
                        body = Expression.GreaterThan(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.GreaterThanOrEqual:
                    {
                        body = Expression.GreaterThanOrEqual(propertyExpression, filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.Between:
                    {
                        var first = Expression.GreaterThan(propertyExpression, filterProperty.GetConstantExpression(propertyType, 0));
                        var second = Expression.LessThan(propertyExpression, filterProperty.GetConstantExpression(propertyType, 1));
                        body = Expression.And(first, second);
                    }
                    break;
                case TFilterFunction.Contains:
                    {
                        if (filterProperty.IsSensitiveCase.GetValueOrDefault())
                        {
                            body = Expression.Call(propertyExpression, XReflection.StringContainsMethod,
                                filterProperty.GetConstantExpression(propertyType));
                        }
                        else
                        {
                            goto case TFilterFunction.Like;
                        }
                    }
                    break;
                case TFilterFunction.StartsWith:
                    {
                        body = Expression.Call(propertyExpression, XReflection.StringStartsWithMethod,
                            filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.EndsWith:
                    {
                        body = Expression.Call(propertyExpression, XReflection.StringEndsWithMethod,
                            filterProperty.GetConstantExpression(propertyType));
                    }
                    break;
                case TFilterFunction.NotEmpty:
                    {
                        if (propertyType == typeof(string))
                        {
                            var notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null));
                            var notEmpty = Expression.NotEqual(propertyExpression, Expression.Constant(string.Empty));
                            body = Expression.And(notNull, notEmpty);
                        }
                        else
                        {
                            body = Expression.NotEqual(propertyExpression, Expression.Constant(null));
                        }
                    }
                    break;
                case TFilterFunction.Empty:
                    {
                        if (propertyType == typeof(string))
                        {
                            var notNull = Expression.Equal(propertyExpression, Expression.Constant(null));
                            var notEmpty = Expression.Equal(propertyExpression, Expression.Constant(string.Empty));
                            body = Expression.Or(notNull, notEmpty);
                        }
                        else
                        {
                            body = Expression.Equal(propertyExpression, Expression.Constant(null));
                        }
                    }
                    break;
                case TFilterFunction.Like:
                    {
                        var valueExpression = Expression.Constant(".*" + (filterProperty.Value ?? string.Empty) + ".*");
                        var regexOptionExpression = Expression.Constant(RegexOptions.IgnoreCase);
                        body = Expression.Call(XReflection.RegexIsMatchMethod, propertyExpression, valueExpression, regexOptionExpression);
                    }
                    break;
                case TFilterFunction.IncludeAny:
                case TFilterFunction.IncludeAll:
                    {
                        if (propertyType.IsPrimitiveOrNullableType())
                        {
                            body = filterProperty.GetContainsInExpression(propertyType, propertyExpression);
                        }
                        else
                        {
                            var propertyTypeItem = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType()!;

                            var lambdaExpression = filterProperty.GetContainsInPropertyExpression(propertyTypeItem);

                            var anyMethod = XReflection.GetEnumerableAnyMethod(propertyTypeItem);

                            body = Expression.Call(null, anyMethod, propertyExpression, lambdaExpression);
                        }
                    }
                    break;
                case TFilterFunction.IncludeEquals:
                    {
                        if (propertyType.IsPrimitiveOrNullableType())
                        {
                            body = filterProperty.GetContainsInExpression(propertyType, propertyExpression);
                        }
                        else
                        {
                            var propertyTypeItem = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType()!;

                            var lambdaExpression = filterProperty.GetContainsInPropertyExpression(propertyTypeItem);

                            var anyMethod = XReflection.GetEnumerableAllMethod(propertyTypeItem);

                            body = Expression.Call(null, anyMethod, propertyExpression, lambdaExpression);
                        }
                    }
                    break;
                case TFilterFunction.IncludeNone:
                    {
                        if (propertyType.IsPrimitiveOrNullableType())
                        {
                            body = filterProperty.GetNotContainsInExpression(propertyType, propertyExpression);
                        }
                        else
                        {
                            var propertyTypeItem = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType()!;

                            var lambdaExpression = filterProperty.GetNotContainsInPropertyExpression(propertyTypeItem);

                            var anyMethod = XReflection.GetEnumerableAllMethod(propertyTypeItem);

                            body = Expression.Call(null, anyMethod, propertyExpression, lambdaExpression);
                        }
                    }
                    break;
                default:
                    body = Expression.Constant(false);
                    break;
            }

            // Получаем итоговую лямбду
            var result = Expression.Lambda<Func<TItem, bool>>(body!, parameterExpression);

            return result;
        }
    }
    /**@}*/
}