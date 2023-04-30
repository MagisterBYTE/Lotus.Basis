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
using System;
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
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Тип <see cref="String"/>
			/// </summary>
			private static readonly Type StringType = typeof(String);

			/// <summary>
			/// Мета информация о методе String.Contains
			/// </summary>
			private static readonly MethodInfo StringContainsMethod = StringType.GetMethods()
																.Where(method => method.Name == nameof(String.Contains))
																.Where(method => method.GetParameters().Length == 1)
																.Single(method => method.GetParameters()[0].GetType() == StringType);
			/// <summary>
			/// Мета информация о методе String.StartsWith
			/// </summary>
			private static readonly MethodInfo StringStartsWithMethod = StringType.GetMethods()
																.Where(method => method.Name == nameof(String.StartsWith))
																.Where(method => method.GetParameters().Length == 1)
																.Single(method => method.GetParameters()[0].GetType() == StringType);
			/// <summary>
			/// Мета информация о методе String.EndsWith
			/// </summary>
			private static readonly MethodInfo StringEndsWithMethod = StringType.GetMethods()
																.Where(method => method.Name == nameof(String.EndsWith))
																.Where(method => method.GetParameters().Length == 1)
																.Single(method => method.GetParameters()[0].GetType() == StringType);
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
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
				var type = Expression.Parameter(typeof(TItem));

				// Получаем информацию о свойстве по которому будем фильтровать
				var propertyInfo = filterProperty.GetPropertyInfo<TItem>();

				// Создаем свойство дерева выражений
				var property = Expression.Property(type, propertyInfo);

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
							body = Expression.Call(property, StringContainsMethod, filterProperty.GetConstantExpression());
						}
						break;
					case TFilterFunction.StartsWith:
						{
							body = Expression.Call(property, StringStartsWithMethod, filterProperty.GetConstantExpression());
						}
						break;
					case TFilterFunction.EndsWith:
						{
							body = Expression.Call(property, StringEndsWithMethod, filterProperty.GetConstantExpression());
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
						break;
					case TFilterFunction.IncludeAll:
						break;
					case TFilterFunction.IncludeNone:
						break;
					default:
						break;
				}

				// Получаем итоговую лямбду
				var result = Expression.Lambda<Func<TItem, Boolean>>(body, type);

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
