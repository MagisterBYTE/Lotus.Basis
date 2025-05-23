using System;

namespace Lotus.Core
{
    /** \addtogroup CoreConverters
    *@{*/
    /// <summary>
    /// Статический класс реализующий конвертацию в логический тип.
    /// </summary>
    public static class XBooleanConverter
    {
        /// <summary>
        /// Текстовые значение логического типа которые означает истинное значение.
        /// </summary>
        public static readonly string[] TrueValues =
        [
            "True",
            "true",
            "1",
            "on",
            "On",
            "истина",
            "Истина",
            "да",
            "Да"
        ];

        /// <summary>
        /// Преобразование объекта в логическое значение.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Логическое значение.</returns>
        public static bool ToBoolean(object value, bool defaultValue = false)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return Convert.ToBoolean(intValue);
            if (value is long longValue) return Convert.ToBoolean(longValue);
            if (value is float floatValue) return Convert.ToBoolean(floatValue);
            if (value is double doubleValue) return Convert.ToBoolean(doubleValue);
            if (value is string stringValue) return Parse(stringValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в логическое значение.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Логическое значение.</returns>
        public static bool Parse(string text)
        {
            return Array.IndexOf(TrueValues, text) > -1;
        }
    }
    /**@}*/
}