using System;
using System.Globalization;
using System.Text;

namespace Lotus.Core
{
    /**
	 * \defgroup CoreConverters Подсистема конвертации данных
	 * \ingroup Core
	 * \brief Подсистема конвертации и преобразования данных обеспечивает единый механизм и точку входа для 
		преобразования объекта в нужный тип. 
	 * \details Преобразование происходит на основе детальной информации об объекте который надо преобразовать в нужный тип, 
		поддерживается конвертация в том числе из строки или путём преобразования из объектов смежного типа.
	 * @{
	 */
    /// <summary>
    /// Статический класс реализующий конвертацию в числовые типы.
    /// </summary>
    public static class XNumberConverter
    {
        #region Int32 
        /// <summary>
        /// Преобразование объекта в целочисленное значение.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static int ToInt(object value, int defaultValue = 0)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return intValue;
            if (value is long longValue) return (int)longValue;
            if (value is float floatValue) return (int)floatValue;
            if (value is double doubleValue) return (int)doubleValue;
            if (value is string stringValue) return ParseInt(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextInt(string text)
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
            text = ParsableTextInt(text);

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
            text = ParsableTextInt(text);

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Int64 
        /// <summary>
        /// Преобразование объекта в целочисленное значение.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static long ToLong(object value, long defaultValue = 0)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return intValue;
            if (value is long longValue) return longValue;
            if (value is float floatValue) return (long)floatValue;
            if (value is double doubleValue) return (long)doubleValue;
            if (value is string stringValue) return ParseLong(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextLong(string text)
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
        public static long ParseLong(string text, long defaultValue = 0)
        {
            text = ParsableTextLong(text);

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
            text = ParsableTextLong(text);

            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Single 
        /// <summary>
        /// Преобразование объекта в вещественное значение одинарной точности.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static float ToSingle(object value, float defaultValue = 0)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return (float)intValue;
            if (value is long longValue) return (float)longValue;
            if (value is float floatValue) return floatValue;
            if (value is double doubleValue) return (float)doubleValue;
            if (value is string stringValue) return ParseSingle(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextSingle(string text)
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
            text = ParsableTextSingle(text);

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
            text = ParsableTextSingle(text);

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Double 
        /// <summary>
        /// Преобразование объекта в вещественное значение двойной точности.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static double ToDouble(object value, double defaultValue = 0)
        {
            if (value == null) return defaultValue;
            if (value is int intValue) return (double)intValue;
            if (value is long longValue) return (double)longValue;
            if (value is float floatValue) return (double)floatValue;
            if (value is double doubleValue) return doubleValue;
            if (value is string stringValue) return ParseDouble(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextDouble(string text)
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
            text = ParsableTextDouble(text);

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
            text = ParsableTextDouble(text);

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Decimal 
        /// <summary>
        /// Преобразование объекта в вещественное значение.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ToDecimal(object value, decimal defaultValue = 0)
        {
            if (value == null) return defaultValue;
            if (value is decimal decimalValue) return decimalValue;
            if (value is int intValue) return Convert.ToDecimal(intValue);
            if (value is long longValue) return Convert.ToDecimal(longValue);
            if (value is float floatValue) return Convert.ToDecimal(floatValue);
            if (value is double doubleValue) return Convert.ToDecimal(doubleValue);
            if (value is string stringValue) return ParseDecimal(stringValue, defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextDecimal(string text)
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
            text = ParsableTextDecimal(text);

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
            text = ParsableTextDecimal(text);

            if (decimal.TryParse(text, NumberStyles.Currency, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }
        #endregion

        #region ToNumber 
        /// <summary>
        /// Преобразование вещественного значения двойной точности в числовой тип указанного типа.
        /// </summary>
        /// <param name="targetType">Целевой числовой тип.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Числовое значение.</returns>
        public static object ToNumber(Type targetType, double value)
        {
            var type_name = targetType.Name;
            switch (type_name)
            {
                case nameof(Byte):
                    {
                        return Convert.ToByte(value);
                    }
                case nameof(SByte):
                    {
                        return Convert.ToSByte(value);
                    }
                case nameof(Char):
                    {
                        return Convert.ToChar(value);
                    }
                case nameof(Int16):
                    {
                        return Convert.ToInt16(value);
                    }
                case nameof(UInt16):
                    {
                        return Convert.ToUInt16(value);
                    }
                case nameof(Int32):
                    {
                        return Convert.ToInt32(value);
                    }
                case nameof(UInt32):
                    {
                        return Convert.ToUInt32(value);
                    }
                case nameof(Int64):
                    {
                        return Convert.ToInt64(value);
                    }
                case nameof(UInt64):
                    {
                        return Convert.ToUInt64(value);
                    }
                case nameof(Single):
                    {
                        return Convert.ToSingle(value);
                    }
                case nameof(Decimal):
                    {
                        return Convert.ToDecimal(value);
                    }
            }

            return value;
        }

        /// <summary>
        /// Преобразование десятичного числа с плавающей запятой в числовой тип указанного типа.
        /// </summary>
        /// <param name="targetType">Целевой числовой тип.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Числовое значение.</returns>
        public static object ToNumber(Type targetType, decimal value)
        {
            var type_name = targetType.Name;
            switch (type_name)
            {
                case nameof(Byte):
                    {
                        return Convert.ToByte(value);
                    }
                case nameof(SByte):
                    {
                        return Convert.ToSByte(value);
                    }
                case nameof(Char):
                    {
                        return Convert.ToChar(value);
                    }
                case nameof(Int16):
                    {
                        return Convert.ToInt16(value);
                    }
                case nameof(UInt16):
                    {
                        return Convert.ToUInt16(value);
                    }
                case nameof(Int32):
                    {
                        return Convert.ToInt32(value);
                    }
                case nameof(UInt32):
                    {
                        return Convert.ToUInt32(value);
                    }
                case nameof(Int64):
                    {
                        return Convert.ToInt64(value);
                    }
                case nameof(UInt64):
                    {
                        return Convert.ToUInt64(value);
                    }
                case nameof(Single):
                    {
                        return Convert.ToSingle(value);
                    }
                case nameof(Double):
                    {
                        return Convert.ToDouble(value);
                    }
            }

            return value;
        }
        #endregion
    }
    /**@}*/
}