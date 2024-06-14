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
    /// Базовый интерфейс для фильтрации по свойству/полю объекта.
    /// </summary>
    public interface ILotusFilterProperty
    {
        /// <summary>
        /// Имя свойства/поля по которому осуществляется фильтрация.
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// Функция для фильтрации.
        /// </summary>
        TFilterFunction Function { get; set; }
    }

    /// <summary>
    /// Класс для фильтрации по свойству/полю объекта поддерживающего интерфейс <see cref="IComparable"/>.
    /// </summary>
    /// <typeparam name="TPropertyType">Тип свойства.</typeparam>
    public class Filter<TPropertyType> : ILotusFilterProperty where TPropertyType : IComparable<TPropertyType>
    {
        /// <summary>
        /// Свойство/поле по которому идет фильтрация.
        /// </summary>
        public string PropertyName { get; set; } = default!;

        /// <summary>
        /// Функция для фильтрации.
        /// </summary>
        public TFilterFunction Function { get; set; }

        /// <summary>
        /// Значение для фильтрации.
        /// </summary>
        public TPropertyType? Value { get; set; }
    }

    /// <summary>
    /// Класс для фильтрации по свойству/полю объекта строкового типа.
    /// </summary>
    public class FilterString : Filter<string>
    {
    }

    /// <summary>
    /// Универсальный класс для фильтрации по свойству/полю объекта.
    /// </summary>
    public class FilterByProperty : ILotusFilterProperty
    {
        #region Const
        /// <summary>
        /// Суффикс, сигнализирующий о том, что свойство относиться к массиву идентификаторов.
        /// </summary>
        public const string SuffixIds = "Ids";
        #endregion

        #region Properties
        /// <summary>
        /// Имя свойства/поля по которому осуществляется фильтрация.
        /// </summary>
        public string PropertyName { get; set; } = default!;

        /// <summary>
        /// Функция для фильтрации.
        /// </summary>
        public TFilterFunction Function { get; set; }

        /// <summary>
        /// Тип свойства.
        /// </summary>
        public TEntityPropertyType PropertyType { get; set; }

        /// <summary>
        /// Статус типа свойства - массив.
        /// </summary>
        public bool? IsArray { get; set; }

        /// <summary>
        /// Учитывать регистр при фильтрации строк.
        /// </summary>
        public bool? IsSensativeCase { get; set; }

        /// <summary>
        /// Значение.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Массив значений.
        /// </summary>
        public string[]? Values { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public FilterByProperty()
        {
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление.</returns>
        public override string ToString()
        {
            return $"Name = {PropertyName}";
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получить информацию о свойстве фильтрации.
        /// </summary>
        /// <typeparam name="TItem">Тпи объекта.</typeparam>
        /// <returns>Информация о свойстве.</returns>
        public PropertyInfo GetPropertyInfo<TItem>()
        {
            if (PropertyName.Contains(SuffixIds))
            {
                // Удаляем суффикс
                var correctName = PropertyName.RemoveLastOccurrence(SuffixIds);

                // Добавляем окончание множественности
                correctName += "s";

                var propertyInfo = typeof(TItem).GetProperties().First(property => property.Name == correctName);
                return propertyInfo;
            }
            else
            {
                var propertyInfo = typeof(TItem).GetProperties().First(property => property.Name == PropertyName);
                return propertyInfo;
            }
        }

        /// <summary>
        /// Получить константу дерева выражения для искомого значения.
        /// </summary>
        /// <param name="index">Индекс искомого значения. -1 значения по умолчанию.</param>
        /// <returns>Константа выражения.</returns>
        public ConstantExpression GetConstantExpression(int index = -1)
        {
            var value = index == -1 ? Value! : Values![index];

            ConstantExpression? constantExpression = null;
            switch (PropertyType)
            {
                case TEntityPropertyType.Boolean:
                    {
                        constantExpression = Expression.Constant(XBooleanHelper.Parse(value));
                    }
                    break;
                case TEntityPropertyType.Integer:
                    {
                        constantExpression = Expression.Constant(XNumberHelper.ParseInt(value));
                    }
                    break;
                case TEntityPropertyType.Enum:
                    {
                        constantExpression = Expression.Constant(XNumberHelper.ParseInt(value));
                    }
                    break;
                case TEntityPropertyType.Float:
                    {
                        constantExpression = Expression.Constant(XNumberHelper.ParseSingle(value));
                    }
                    break;
                case TEntityPropertyType.DateTime:
                    {
                        constantExpression = Expression.Constant(XDateTimeHelper.Parse(value));
                    }
                    break;
                case TEntityPropertyType.String:
                    {
                        constantExpression = Expression.Constant(value);
                    }
                    break;
            }

            return constantExpression!;
        }

        /// <summary>
        /// Получить константу массива дерева выражения для искомого значения.
        /// </summary>
        /// <returns>Константа массива выражения.</returns>
        public NewArrayExpression GetArrayExpression()
        {
            var constants = new List<Expression>();

            if (PropertyType == TEntityPropertyType.Integer)
            {
                var values = Values!.ToIntArray();
                foreach (var value in values)
                {
                    var constant = Expression.Constant(value);
                    constants.Add(constant);
                }

                var massive = Expression.NewArrayInit(typeof(int), constants);
                return massive;
            }
            else
            {
                foreach (var value in Values!)
                {
                    var constant = Expression.Constant(value);
                    constants.Add(constant);
                }

                var massive = Expression.NewArrayInit(typeof(string), constants);
                return massive;
            }
        }

        /// <summary>
        /// Получить лямбду выражения [o => ids.Contains(p.Id)] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства p.</param>
        /// <returns>Лямбда выражения.</returns>
        public LambdaExpression GetContainsInPropertyExpression(Type propertyType)
        {
            var parameter = Expression.Parameter(propertyType, "o");

            var property = Expression.Property(parameter, $"Id");

            var containsMethod = XExpressionFilters.GetEnumerableContainsMethod(PropertyType);

            var constantIds = GetArrayExpression();

            var containsCall = Expression.Call(null, containsMethod, constantIds, property);

            var lambda = Expression.Lambda(containsCall, parameter);

            return lambda;
        }

        /// <summary>
        /// Получить лямбду выражения [o => !(ids.Contains(p.Id))] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства p.</param>
        /// <returns>Лямбда выражения.</returns>
        public LambdaExpression GetNotContainsInPropertyExpression(Type propertyType)
        {
            var parameter = Expression.Parameter(propertyType, "o");

            var property = Expression.Property(parameter, $"Id");

            var containsMethod = XExpressionFilters.GetEnumerableContainsMethod(PropertyType);

            var constantIds = GetArrayExpression();

            var containsCall = Expression.Call(null, containsMethod, constantIds, property);

            var containsNot = Expression.Not(containsCall);

            var lambda = Expression.Lambda(containsNot, parameter);

            return lambda;
        }

        /// <summary>
        /// Получить выражение [o => ids.Contains(p)] для искомого значения.
        /// </summary>
        /// <param name="propertyExpression">Выражение для объекта p.</param>
        /// <returns>Выражение.</returns>
        public Expression GetContainsInObjectExpression(MemberExpression propertyExpression)
        {
            var containsMethod = XExpressionFilters.GetEnumerableContainsMethod(PropertyType);

            var constantIds = GetArrayExpression();

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            return containsCall;
        }

        /// <summary>
        /// Получить выражение [o => !(ids.Contains(p))] для искомого значения.
        /// </summary>
        /// <param name="propertyExpression">Выражение для объекта p.</param>
        /// <returns>Выражение.</returns>
        public Expression GetNotContainsInObjectExpression(MemberExpression propertyExpression)
        {
            var containsMethod = XExpressionFilters.GetEnumerableContainsMethod(PropertyType);

            var constantIds = GetArrayExpression();

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            var containsNot = Expression.Not(containsCall);

            return containsNot;
        }
        #endregion
    }
    /**@}*/
}