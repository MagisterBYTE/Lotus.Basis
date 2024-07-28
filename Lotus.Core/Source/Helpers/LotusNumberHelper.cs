using System;
using System.Globalization;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с числовыми типами.
    /// </summary>
    public static class XNumberHelper
    {
        #region Format numbers
        /// <summary>
        /// Денежный формат.
        /// </summary>
        public const string Monetary = "{0:c}";
        #endregion

        #region Int32 
        /// <summary>
        /// Проверка на установленный флаг.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flag">Проверяемый флаг.</param>
        /// <returns>Статус установки флага.</returns>
        public static bool IsFlagSet(int value, int flag)
        {
            return (value & flag) != 0;
        }

        /// <summary>
        /// Установка флага.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Новое значение.</returns>
        public static int SetFlag(int value, int flags)
        {
            value |= flags;
            return value;
        }

        /// <summary>
        /// Очистка флага.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Новое значение.</returns>
        public static int ClearFlag(int value, int flags)
        {
            value &= ~flags;
            return value;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextInt(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            const int max = 11;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }

                if (number.Length > max)
                {
                    break;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static int ParseInt(string text, int defaultValue = 0)
        {
            text = ParseableTextInt(text);

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;

            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseInt(string text, out int result)
        {
            text = ParseableTextInt(text);

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Int64
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextLong(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            const int max = 19;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }

                if(number.Length > max)
                {
                    break;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static long ParseLong(string text, long defaultValue = 0)
        {
            text = ParseableTextLong(text);

            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseLong(string text, out long result)
        {
            text = ParseableTextLong(text);

            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Single 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextSingle(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static float ParseSingle(string text, float defaultValue = 0)
        {
            text = ParseableTextSingle(text);

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseSingle(string text, out float result)
        {
            text = ParseableTextSingle(text);

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Double 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextDouble(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static double ParseDouble(string text, double defaultValue = 0)
        {
            text = ParseableTextDouble(text);

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseDouble(string text, out double result)
        {
            text = ParseableTextDouble(text);

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Decimal 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextDeciminal(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ParseDecimal(string text, decimal defaultValue = 0)
        {
            text = ParseableTextDeciminal(text);

            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста, представленного как отображение валюты, в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ParseCurrency(string text, decimal defaultValue = 0)
        {
            text = ParseableTextDeciminal(text);

            if (decimal.TryParse(text, NumberStyles.Currency, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }
        #endregion
    }
    /**@}*/
}