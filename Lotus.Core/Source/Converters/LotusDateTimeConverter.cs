using System;
using System.Globalization;

namespace Lotus.Core
{
    /** \addtogroup CoreConverters
    *@{*/
    /// <summary>
    /// Статический класс реализующий конвертацию в тип даты-времени.
    /// </summary>
    public static class XDateTimeConverter
    {
        /// <summary>
        /// Преобразование объекта в значение даты-времени.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static DateTime ToDateTime(object value, DateTime defaultValue)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return FromTimestamp(intValue);
            if (value is long longValue) return FromTimestamp(longValue);
            if (value is float floatValue) return FromTimestamp(floatValue);
            if (value is double doubleValue) return FromTimestamp(doubleValue);
            if (value is string stringValue) return Parse(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в тип дата-время.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="formatDate">Формат даты-времени.</param>
        /// <returns>Текст или null если сконвертировать невозможно.</returns>
        public static string? ParsableText(string text, string formatDate)
        {
            string? result = null;
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            if (DateTime.TryParse(text, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var di))
            {
                return di.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                if (DateTime.TryParse(text, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out var dc))
                {
                    return dc.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    if (string.IsNullOrEmpty(formatDate))
                    {
                        return null;
                    }

                    switch (formatDate)
                    {
                        case "%s":
                            {
                                result = new DateTime(
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    DateTime.Now.Hour,
                                    DateTime.Now.Minute,
                                    ParseSecond(text),
                                    DateTimeKind.Unspecified).ToString(CultureInfo.CurrentCulture);
                            }
                            break;
                        case "%m":
                            {
                                result = new DateTime(
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    DateTime.Now.Hour,
                                    ParseMinute(text),
                                    0,
                                    DateTimeKind.Unspecified).ToString(CultureInfo.CurrentCulture);
                            }
                            break;
                        case "%H":
                            {
                                result = new DateTime(
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    ParseHour(text),
                                    0,
                                    0,
                                    DateTimeKind.Unspecified).ToString(CultureInfo.CurrentCulture);
                            }
                            break;
                        case "H:m:s":
                            {
                                result = new DateTime(
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    ParseHour(text),
                                    0,
                                    0,
                                    DateTimeKind.Unspecified).ToString(CultureInfo.CurrentCulture);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Преобразование текста в объект дата-время.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static DateTime Parse(string text, DateTime defaultValue = default)
        {
            if (string.IsNullOrEmpty(text))
            {
                return defaultValue;
            }

            if (DateTime.TryParse(text, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out defaultValue))
            {
                return defaultValue;
            }
            else
            {
                if (DateTime.TryParse(text, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out defaultValue))
                {
                    return defaultValue;
                }

                return defaultValue;
            }
        }

        /// <summary>
        /// Преобразование текста в объект дата-время.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParse(string text, out DateTime result)
        {
            if (string.IsNullOrEmpty(text))
            {
                result = default(DateTime);
                return false;
            }

            if (DateTime.TryParse(text, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
            {
                return true;
            }
            else
            {
                if (DateTime.TryParse(text, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out result))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Преобразование текста в час.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Значение часа в пределах от 0 до 24.</returns>
        public static int ParseHour(string text)
        {
            var value = XNumberConverter.ParseInt(text);
            value = Math.Min(23, value);
            return value;
        }

        /// <summary>
        /// Преобразование текста в минуту.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Значение минуты в пределах от 0 до 59.</returns>
        public static int ParseMinute(string text)
        {
            var value = XNumberConverter.ParseInt(text);
            value = Math.Min(59, value);
            return value;
        }

        /// <summary>
        /// Преобразование текста в секунду.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Значение секунды в пределах от 0 до 59.</returns>
        public static int ParseSecond(string text)
        {
            var value = XNumberConverter.ParseInt(text);
            value = Math.Min(59, value);
            return value;
        }

        /// <summary>
        /// Получить значение даты-времени через временную метку.
        /// </summary>
        /// <remarks>
        /// Временная метка в формате unix time (количество секунд прошедших с 1 января 1970 года в UTC).
        /// </remarks>
        /// <param name="value">Временная метка.</param>
        /// <returns>Значение даты-времени.</returns>
        public static DateTime FromTimestamp(string value)
        {
            double.TryParse(value, out var unixTimeStamp);
            var dtDateTime = DateTime.UnixEpoch;
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        /// <summary>
        /// Получить значение даты-времени через временную метку.
        /// </summary>
        /// <remarks>
        /// Временная метка в формате unix time (количество секунд прошедших с 1 января 1970 года в UTC).
        /// </remarks>
        /// <param name="value">Временная метка.</param>
        /// <returns>Значение даты-времени.</returns>
        public static DateTime FromTimestamp(double value)
        {
            var dtDateTime = DateTime.UnixEpoch;
            dtDateTime = dtDateTime.AddSeconds(value);
            return dtDateTime;
        }

        /// <summary>
        /// Преобразовать значение даты-времени во временную метку.
        /// </summary>
        /// <remarks>
        /// Временная метка в формате unix time (количество секунд прошедших с 1 января 1970 года в UTC).
        /// </remarks>
        /// <param name="value">Значение даты-времени</param>
        /// <returns>Временная метка.</returns>
        public static double ToTimestamp(DateTime value)
        {
            var result = (value.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds;
            return result;
        }
    }
    /**@}*/
}