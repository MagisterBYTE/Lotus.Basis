using System;
using System.Globalization;

using Newtonsoft.Json.Linq;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с типом <see cref="DateTime"/>.
    /// </summary>
    public static class XDateTimeHelper
    {
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в тип дата-время.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="formatDate">Формат даты-времени.</param>
        /// <returns>Текст или null если сконвертировать невозможно.</returns>
        public static string? ParseableText(string text, string formatDate)
        {
            string result = null;
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
                                    ParseMunute(text),
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
            var value = XNumberHelper.ParseInt(text);
            value = Math.Min(23, value);
            return value;
        }

        /// <summary>
        /// Преобразование текста в минуту.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Значение минуты в пределах от 0 до 59.</returns>
        public static int ParseMunute(string text)
        {
            var value = XNumberHelper.ParseInt(text);
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
            var value = XNumberHelper.ParseInt(text);
            value = Math.Min(59, value);
            return value;
        }
    }
}
