//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема фильтрации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryFilterExpression.cs
*		Статический класс содержащий фильтры для запросов в виде деревьев выражений.
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
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryFilter
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс содержащий фильтры для запросов в виде деревьев выражений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExpressionFilters
		{
			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить типизированную версию метода <see cref="Enumerable.Contains{TSource}(IEnumerable{TSource}, TSource)"/>
			/// </summary>
			/// <param name="propertyType">Тип свойства</param>
			/// <returns>Типизированная версия метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static MethodInfo GetEnumerableContainsMethod(TEntityPropertyType propertyType)
			{
				switch (propertyType)
				{
					case TEntityPropertyType.Boolean:
						break;
					case TEntityPropertyType.Integer:
						{
							return XReflection.GetEnumerableContainsMethod(typeof(Int32));
						}
					case TEntityPropertyType.Enum:
						break;
					case TEntityPropertyType.Float:
						{
							return XReflection.GetEnumerableContainsMethod(typeof(Single));
						}
					case TEntityPropertyType.DateTime:
						{
							return XReflection.GetEnumerableContainsMethod(typeof(DateTime));
						}
					case TEntityPropertyType.String:
						{
							return XReflection.GetEnumerableContainsMethod(typeof(String));
						}
				}

				return XReflection.GetEnumerableContainsMethod(typeof(System.Byte));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить типизированную версию метода <see cref="Enumerable.Any{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
			/// </summary>
			/// <param name="propertyType">Тип свойства</param>
			/// <returns>Типизированная версия метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static MethodInfo GetEnumerableAnyMethod(TEntityPropertyType propertyType)
			{
				switch (propertyType)
				{
					case TEntityPropertyType.Boolean:
						break;
					case TEntityPropertyType.Integer:
						{
							return XReflection.GetEnumerableAnyMethod(typeof(Int32));
						}
					case TEntityPropertyType.Enum:
						break;
					case TEntityPropertyType.Float:
						{
							return XReflection.GetEnumerableAnyMethod(typeof(Single));
						}
					case TEntityPropertyType.DateTime:
						{
							return XReflection.GetEnumerableAnyMethod(typeof(DateTime));
						}
					case TEntityPropertyType.String:
						{
							return XReflection.GetEnumerableAnyMethod(typeof(String));
						}
				}

				return XReflection.GetEnumerableAnyMethod(typeof(System.Byte));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить типизированную версию метода <see cref="Enumerable.All{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
			/// </summary>
			/// <param name="propertyType">Тип свойства</param>
			/// <returns>Типизированная версия метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static MethodInfo GetEnumerableAllMethod(TEntityPropertyType propertyType)
			{
				switch (propertyType)
				{
					case TEntityPropertyType.Boolean:
						break;
					case TEntityPropertyType.Integer:
						{
							return XReflection.GetEnumerableAllMethod(typeof(Int32));
						}
					case TEntityPropertyType.Enum:
						break;
					case TEntityPropertyType.Float:
						{
							return XReflection.GetEnumerableAllMethod(typeof(Single));
						}
					case TEntityPropertyType.DateTime:
						{
							return XReflection.GetEnumerableAllMethod(typeof(DateTime));
						}
					case TEntityPropertyType.String:
						{
							return XReflection.GetEnumerableAllMethod(typeof(String));
						}
				}

				return XReflection.GetEnumerableAllMethod(typeof(System.Byte));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить выражение фильтра по данным фильтрации по свойству
			/// </summary>
			/// <typeparam name="TItem">Тип объекта</typeparam>
			/// <param name="filterProperty">Данные для фильтрации по свойству</param>
			/// <returns>Выражение фильтра</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Expression<Func<TItem, Boolean>> GetFilter<TItem>(CFilterProperty filterProperty)
			{
				// Создаем параметр дерева выражений
				var parameter = Expression.Parameter(typeof(TItem), "x");

				// Получаем информацию о свойстве по которому будем фильтровать
				var propertyInfo = filterProperty.GetPropertyInfo<TItem>();

				// Создаем свойство дерева выражений
				var property = Expression.Property(parameter, propertyInfo);

				Expression body = null;

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

							var lambdaContains = filterProperty.GetContainsExpression(propertyType);

							var anyMethod = XReflection.GetEnumerableAnyMethod(propertyType);

							body = Expression.Call(null, anyMethod, property, lambdaContains);
						}
						break;
					case TFilterFunction.IncludeEquals:
						{
							var propertyType = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType();

							var lambdaContains = filterProperty.GetContainsExpression(propertyType);

							var allMethod = XReflection.GetEnumerableAllMethod(propertyType);

							body = Expression.Call(null, allMethod, property, lambdaContains);
						}
						break;
					case TFilterFunction.IncludeNone:
						{
							var propertyType = propertyInfo.PropertyType.GetClassicCollectionItemTypeOrThisType();

							var lambdaNotContains = filterProperty.GetNotContainsExpression(propertyType);

							var allMethod = XReflection.GetEnumerableAllMethod(propertyType);

							body = Expression.Call(null, allMethod, property, lambdaNotContains);
						}
						break;
					default:
						break;
				}

				// Получаем итоговую лямбду
				var result = Expression.Lambda<Func<TItem, Boolean>>(body, parameter);

				return result;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
