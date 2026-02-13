using System;
using System.Collections.Generic;
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
        /// Имя/путь свойства/поля по которому осуществляется фильтрация.
        /// </summary>
        string PropertyPath { get; set; }

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
        /// Имя/путь свойства/поля по которому осуществляется фильтрация.
        /// </summary>
        public string PropertyPath { get; set; } = default!;

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
        #region Properties
        /// <summary>
        /// Имя/путь свойства/поля по которому осуществляется фильтрация.
        /// </summary>
        public string PropertyPath { get; set; } = default!;

        /// <summary>
        /// Функция для фильтрации.
        /// </summary>
        public TFilterFunction Function { get; set; }

        /// <summary>
        /// Статус типа свойства Nullable.
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// Учитывать регистр при фильтрации строк.
        /// </summary>
        public bool? IsSensitiveCase { get; set; }

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
            return $"Name = {PropertyPath}";
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка на валидность фильтра по свойству.
        /// </summary>
        /// <returns>Статус проверки.</returns>
        public bool IsValid()
        {
            if (Function == TFilterFunction.IncludeAny
                || Function == TFilterFunction.IncludeAll
                || Function == TFilterFunction.IncludeEquals
                || Function == TFilterFunction.IncludeNone
                || Function == TFilterFunction.Between)
            {
                if (Values == null || Values.Length == 0)
                {
                    return false;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(Value) 
                    && IsNullable == false 
                    && (Function != TFilterFunction.Empty && Function != TFilterFunction.NotEmpty))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Получить константу дерева выражения для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <param name="index">Индекс искомого значения. -1 значения по умолчанию.</param>
        /// <returns>Константа выражения.</returns>
        public ConstantExpression GetConstantExpression(Type propertyType, int index = -1)
        {
            string value;
            if (index == -1)
            {
                value = Value ?? throw new InvalidOperationException("Value is required for this filter.");
            }
            else
            {
                if (Values == null || index < 0 || index >= Values.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range for Values.");
                }
                value = Values[index];
            }

            var constant = GetConstantExpression(propertyType, value);
            return constant ?? throw new InvalidOperationException($"Unable to create constant expression for value and type {propertyType}.");
        }

        /// <summary>
        /// Получить константу массива дерева выражения для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <returns>Константа массива выражения.</returns>
        public NewArrayExpression GetArrayExpression(Type propertyType)
        {
            if (Values == null || Values.Length == 0)
            {
                throw new InvalidOperationException("Values are required for this filter.");
            }

            var constants = new List<Expression>();

            foreach (var value in Values)
            {
                var constant = GetConstantExpression(propertyType, value);
                if (constant != null)
                {
                    constants.Add(constant);
                }
            }

            var massive = Expression.NewArrayInit(propertyType, constants);
            return massive;
        }

        /// <summary>
        /// Получить выражение [o => Values.Contains(p)] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <param name="propertyExpression">Выражение для объекта p.</param>
        /// <returns>Выражение вызова метода.</returns>
        public MethodCallExpression GetContainsInExpression(Type propertyType, MemberExpression propertyExpression)
        {
            var containsMethod = XReflection.GetEnumerableContainsMethod(propertyType);

            var constantIds = GetArrayExpression(propertyType);

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            return containsCall;
        }

        /// <summary>
        /// Получить выражение [o => !(Values.Contains(p))] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства p.</param>
        /// <param name="propertyExpression">Выражение для объекта p.</param>
        /// <returns>Выражение.</returns>
        public Expression GetNotContainsInExpression(Type propertyType, MemberExpression propertyExpression)
        {
            var containsMethod = XReflection.GetEnumerableContainsMethod(propertyType);

            var constantIds = GetArrayExpression(propertyType);

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            var containsNot = Expression.Not(containsCall);

            return containsNot;
        }

        /// <summary>
        /// Получить лямбду выражения [o => ids.Contains(p.Id)] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства p.</param>
        /// <param name="propertyNameId">Имя свойства Id</param>
        /// <returns>Лямбда выражения.</returns>
        public LambdaExpression GetContainsInPropertyExpression(Type propertyType, string propertyNameId = "Id")
        {
            var parameterExpression = Expression.Parameter(propertyType, "o");

            var propertyExpression = Expression.Property(parameterExpression, propertyNameId);

            var propertyInfoId = (propertyExpression.Member as PropertyInfo)!;

            var containsMethod = XReflection.GetEnumerableContainsMethod(propertyInfoId.PropertyType);

            var constantIds = GetArrayExpression(propertyInfoId.PropertyType);

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            var lambda = Expression.Lambda(containsCall, parameterExpression);

            return lambda;
        }

        /// <summary>
        /// Получить лямбду выражения [o => !(ids.Contains(p.Id))] для искомого значения.
        /// </summary>
        /// <param name="propertyType">Тип свойства p.</param>
        /// <param name="propertyNameId">Имя свойства Id</param>
        /// <returns>Лямбда выражения.</returns>
        public LambdaExpression GetNotContainsInPropertyExpression(Type propertyType, string propertyNameId = "Id")
        {
            var parameterExpression = Expression.Parameter(propertyType, "o");

            var propertyExpression = Expression.Property(parameterExpression, propertyNameId);

            var propertyInfoId = (propertyExpression.Member as PropertyInfo)!;

            var containsMethod = XReflection.GetEnumerableContainsMethod(propertyInfoId.PropertyType);

            var constantIds = GetArrayExpression(propertyInfoId.PropertyType);

            var containsCall = Expression.Call(null, containsMethod, constantIds, propertyExpression);

            var containsNot = Expression.Not(containsCall);

            var lambda = Expression.Lambda(containsNot, parameterExpression);

            return lambda;
        }

        /// <summary>
        /// Получить константу дерева выражения указанного значения в виде строки.
        /// </summary>
        /// <param name="propertyType">Тип свойства.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Константа выражения.</returns>
        private static ConstantExpression? GetConstantExpression(Type propertyType, string value)
        {
            ConstantExpression? constantExpression = null;

            // Не реализована полная совместимость c Nullable

            if (propertyType.IsEnum)
            {
                constantExpression = Expression.Constant(Enum.ToObject(propertyType, Convert.ToInt32(value)), propertyType);
                return constantExpression;
            }

            var typeCode = Type.GetTypeCode(propertyType);
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    constantExpression = Expression.Constant(XBooleanConverter.Parse(value), propertyType);
                    break;
                case TypeCode.Byte:
                    constantExpression = Expression.Constant((byte)XNumberConverter.ParseInt(value), propertyType);
                    break;
                case TypeCode.Int16:
                    constantExpression = Expression.Constant((short)XNumberConverter.ParseInt(value), propertyType);
                    break;
                case TypeCode.Int32:
                    constantExpression = Expression.Constant(XNumberConverter.ParseInt(value), propertyType);
                    break;
                case TypeCode.Int64:
                    constantExpression = Expression.Constant(XNumberConverter.ParseLong(value), propertyType);
                    break;
                case TypeCode.Single:
                    constantExpression = Expression.Constant(XNumberConverter.ParseSingle(value), propertyType);
                    break;
                case TypeCode.Double:
                    constantExpression = Expression.Constant(XNumberConverter.ParseDouble(value), propertyType);
                    break;
                case TypeCode.Decimal:
                    constantExpression = Expression.Constant(XNumberConverter.ParseDecimal(value), propertyType);
                    break;
                case TypeCode.String:
                    constantExpression = Expression.Constant(value, propertyType);
                    break;
                case TypeCode.DateTime:
                    constantExpression = Expression.Constant(XDateTimeConverter.Parse(value).ToUniversalTime(), propertyType);
                    break;
            }

            if (constantExpression is not null) return constantExpression;

            // Специфичные типы
            if (Guid.TryParse(value, out var guid))
            {
                constantExpression = Expression.Constant(guid, propertyType);
                return constantExpression;
            }

            return constantExpression;
        }
        #endregion
    }
    /**@}*/
}