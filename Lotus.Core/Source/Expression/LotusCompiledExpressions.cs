using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Lotus.Core
{
	/** \addtogroup CoreExpression
	*@{*/
	/// <summary>
	/// Кэш скомпилированных в делегаты деревьев выражений.
	/// </summary>
	/// <typeparam name="TIn">Тип входного параметра.</typeparam>
	/// <typeparam name="TOut">Тип выходного параметра.</typeparam>
	public static class XCompiledExpressions<TIn, TOut>
	{
		/// <summary>
		/// Кэш.
		/// </summary>
		private static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> Cache =
			new ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>>();

		/// <summary>
		/// Скомпилировать дерево выражений или получить закэшированный делегат.
		/// </summary>
		/// <param name="expression">Выражение.</param>
		/// <returns>Функтор.</returns>
		public static Func<TIn, TOut> AsFunc(Expression<Func<TIn, TOut>> expression)
		{
			return Cache.GetOrAdd(expression, k => k.Compile());
		}
	}
	/**@}*/
}