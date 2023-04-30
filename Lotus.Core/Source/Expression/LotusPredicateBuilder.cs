//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема деревьев выражений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusPredicateBuilder.cs
*		Определитель логических операторов деревьев выражений.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreExpression
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определитель логических операторов деревьев выражений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPredicateBuilder
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Логическое И для деревьев выражений
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static Expression<Func<T, Boolean>> And<T>(
				this Expression<Func<T, Boolean>> first,
				Expression<Func<T, Boolean>> second)
			{
				return first.Compose<Func<T, Boolean>>((LambdaExpression)second,
					new Func<Expression, Expression, Expression>(Expression.AndAlso));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Логическое ИЛИ для деревьев выражений
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static Expression<Func<T, Boolean>> Or<T>(
				this Expression<Func<T, Boolean>> first,
				Expression<Func<T, Boolean>> second)
			{
				return first.Compose<Func<T, Boolean>>((LambdaExpression)second,
					new Func<Expression, Expression, Expression>(Expression.OrElse));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Логическое ОТРИЦАНИЕ для деревьев выражений
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static Expression<Func<T, Boolean>> Not<T>(
				this Expression<Func<T, Boolean>> expression)
			{
				return Expression.Lambda<Func<T, Boolean>>((Expression)Expression.Not(expression.Body),
					(IEnumerable<ParameterExpression>)expression.Parameters);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Связывание деревьев выражений в одно
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static Expression<T> Compose<T>(
				this LambdaExpression first,
				LambdaExpression second,
				Func<Expression, Expression, Expression> merge)
			{
				Expression expression = XPredicateBuilder.ParameterRebinder.ReplaceParameters(first.Parameters.Select(
					(f, i) => new
					{
						f = f,
						s = second.Parameters[i]
					}).ToDictionary(p => p.s, p => p.f), second.Body);

				return Expression.Lambda<T>(merge(first.Body, expression),
					(IEnumerable<ParameterExpression>)first.Parameters);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработчик параметров
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private sealed class ParameterRebinder : ExpressionVisitor
			{
				private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

				private ParameterRebinder(
					Dictionary<ParameterExpression, ParameterExpression> map)
				{
					_map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Заменить параметр
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				public static Expression ReplaceParameters(
					Dictionary<ParameterExpression, ParameterExpression> map,
					Expression exp)
				{
					return new ParameterRebinder(map).Visit(exp);
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Зайти в параметр с помощью ExpressionVisitor
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				protected override Expression VisitParameter(ParameterExpression node)
				{
					ParameterExpression parameterExpression;
					if (_map.TryGetValue(node, out parameterExpression))
						node = parameterExpression;
					return base.VisitParameter(node);
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
