//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема деревьев выражений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSpecification.cs
*		Спецификация.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
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
		/// Спецификация
		/// </summary>
		/// <typeparam name="TItem">Тип объекта спецификации</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class Specification<TItem>
		{
			private readonly Expression<Func<TItem, Boolean>> _expression;
			private Func<TItem, Boolean> _func;

			/// <summary>
			/// Определение оператора false для работы операторов И, ИЛИ, ОТРИЦАНИЕ
			/// </summary>
			public static Boolean operator false(Specification<TItem> _)
			{
				return false;
			}

			/// <summary>
			/// Определение оператора true для работы операторов И, ИЛИ, ОТРИЦАНИЕ
			/// </summary>
			public static Boolean operator true(Specification<TItem> _)
			{
				return false;
			}

			/// <summary>
			/// Определение логического оператора И
			/// </summary>
			public static Specification<TItem> operator &(Specification<TItem> spec1, Specification<TItem> spec2)
			{
				return new Specification<TItem>(spec1._expression.And(spec2._expression));
			}

			/// <summary>
			/// Определение логического оператора ИЛИ
			/// </summary>
			public static Specification<TItem> operator |(Specification<TItem> spec1, Specification<TItem> spec2)
			{
				return new Specification<TItem>(spec1._expression.Or(spec2._expression));
			}

			/// <summary>
			/// Определение логического оператора ОТРИЦАНИЕ
			/// </summary>
			public static Specification<TItem> operator !(Specification<TItem> spec)
			{
				return new Specification<TItem>(spec._expression.Not());
			}

			/// <summary>
			/// Неявное преобразование к дереву выражений
			/// </summary>
			public static implicit operator Expression<Func<TItem, Boolean>>(Specification<TItem> spec) => spec?._expression!;

			/// <summary>
			/// Неявное преобразование к спецификации
			/// </summary>
			public static implicit operator Specification<TItem>(Expression<Func<TItem, Boolean>> expression) => new Specification<TItem>(expression);

			public Specification(Expression<Func<TItem, Boolean>> expression) => _expression = expression ?? throw new ArgumentNullException(nameof(expression));

			/// <summary>
			/// Проверка на соответствие спецификации
			/// IMPORTANT! Это функция, а не дерево выражений
			/// </summary>
			public Boolean IsSatisfiedBy(TItem obj) => (_func ?? (_func = _expression.AsFunc()))(obj);

			/// <summary>
			/// Композиция спецификаций
			/// </summary>
			public Specification<TParent> From<TParent>(Expression<Func<TParent, TItem>> mapFrom) => (Specification<TParent>)_expression.From(mapFrom);
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
