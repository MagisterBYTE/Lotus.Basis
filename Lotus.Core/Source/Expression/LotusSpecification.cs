using System;
using System.Linq.Expressions;

namespace Lotus.Core
{
	/** \addtogroup CoreExpression
	*@{*/
	/// <summary>
	/// Спецификация.
	/// </summary>
	/// <typeparam name="TItem">Тип объекта спецификации.</typeparam>
	public class Specification<TItem>
	{
		private readonly Expression<Func<TItem, bool>> _expression;
		private Func<TItem, bool> _func;

		/// <summary>
		/// Определение оператора false для работы операторов И, ИЛИ, ОТРИЦАНИЕ.
		/// </summary>
		public static bool operator false(Specification<TItem> _)
		{
			return false;
		}

		/// <summary>
		/// Определение оператора true для работы операторов И, ИЛИ, ОТРИЦАНИЕ.
		/// </summary>
		public static bool operator true(Specification<TItem> _)
		{
			return false;
		}

		/// <summary>
		/// Определение логического оператора И.
		/// </summary>
		public static Specification<TItem> operator &(Specification<TItem> spec1, Specification<TItem> spec2)
		{
			return new Specification<TItem>(spec1._expression.And(spec2._expression));
		}

		/// <summary>
		/// Определение логического оператора ИЛИ.
		/// </summary>
		public static Specification<TItem> operator |(Specification<TItem> spec1, Specification<TItem> spec2)
		{
			return new Specification<TItem>(spec1._expression.Or(spec2._expression));
		}

		/// <summary>
		/// Определение логического оператора ОТРИЦАНИЕ.
		/// </summary>
		public static Specification<TItem> operator !(Specification<TItem> spec)
		{
			return new Specification<TItem>(spec._expression.Not());
		}

            /// <summary>
            /// Неявное преобразование к дереву выражений.
            /// </summary>
            public static implicit operator Expression<Func<TItem, bool>>(Specification<TItem> spec)
            {
                return spec?._expression!;
            }

            /// <summary>
            /// Неявное преобразование к спецификации.
            /// </summary>
            public static implicit operator Specification<TItem>(Expression<Func<TItem, bool>> expression)
            {
                return new Specification<TItem>(expression);
            }

            public Specification(Expression<Func<TItem, bool>> expression)
            {
                _expression = expression ?? throw new ArgumentNullException(nameof(expression));
            }

            /// <summary>
            /// Проверка на соответствие спецификации.
            /// IMPORTANT! Это функция, а не дерево выражений.
            /// </summary>
            public bool IsSatisfiedBy(TItem obj)
            {
                return (_func ?? (_func = _expression.AsFunc()))(obj);
            }

            /// <summary>
            /// Композиция спецификаций.
            /// </summary>
            public Specification<TParent> From<TParent>(Expression<Func<TParent, TItem>> mapFrom)
            {
                return (Specification<TParent>)_expression.From(mapFrom);
            }
        }
	/**@}*/
}