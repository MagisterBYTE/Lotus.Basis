using System;
using System.Globalization;
using System.Linq;
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
        #region Const
        const NumberStyles RealNumberStyles = NumberStyles.AllowExponent
                                              | NumberStyles.AllowDecimalPoint
                                              | NumberStyles.AllowLeadingSign
                                              | NumberStyles.Float
                                              | NumberStyles.AllowThousands;
        #endregion

        #region Int32
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="max">Максимальное кол-во символов для анализа.</param>
        /// <returns>Текст.</returns>
        public static string ParsableTextInteger(string text, int max)
        {
            var number = new StringBuilder(text.Length);

            var addMinus = false;
            var hasNumber = false;
            var enFormat = ((text.Contains(',') && text.Contains('.')) || text.Contains('.'));
            var ruFormat = (text.Contains(',') && text.Contains('.') == false && text.Count(x => x == ',') == 1);
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && addMinus == false)
                {
                    number.Append(c);
                    addMinus = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    hasNumber = true;
                    number.Append(c);
                }

                // Не анализируем после точки
                if (c == '.' && hasNumber && enFormat)
                {
                    break;
                }

                // Не анализируем после запятой
                if (c == ',' && hasNumber && ruFormat)
                {
                    break;
                }

                if (number.Length > max)
                {
                    break;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование объекта в целочисленное значение.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Целочисленное значение.</returns>
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
            return ParsableTextInteger(text, 11);
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Целочисленное значение.</returns>
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
        /// <returns>Целочисленное значение.</returns>
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
            return ParsableTextInteger(text, 19);
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Целочисленное значение.</returns>
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
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Целочисленное значение.</returns>
        public static ulong ParseUlong(string text, ulong defaultValue = 0)
        {
            text = ParsableTextLong(text);

            if (ulong.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
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
        public static string ParseableTextReal(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            var add_e = -1;
            var enFormat = ((text.Contains(',') && text.Contains('.')) || text.Contains('.'));
            var ruFormat = (text.Contains(',') && text.Contains('.') == false && text.Count(x => x == ',') == 1);
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1))
                {
                    // Добавляем перед E
                    if (i == add_e + 1)
                    {
                        number.Append(c);
                        continue;
                    }
                    else
                    {
                        if (add_minus == false)
                        {
                            number.Append(c);
                            add_minus = true;
                            continue;
                        }
                    }
                }

                if (((c == ',' && ruFormat) && (i != text.Length - 1) && add_dot == false) ||
                    ((c == '.' && enFormat) && (i != text.Length - 1) && add_dot == false))
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if ((c is >= '0' and <= '9'))
                {
                    number.Append(c);
                    continue;
                }

                if ((c == 'E' || c == 'e'))
                {
                    number.Append(c);
                    add_e = i;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование объекта в вещественное значение одинарной точности.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Вещественное значение одинарной точности.</returns>
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
            return ParseableTextReal(text);
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Вещественное значение одинарной точности.</returns>
        public static float ParseSingle(string text, float defaultValue = 0)
        {
            text = ParsableTextSingle(text);

            if (float.TryParse(text, RealNumberStyles, CultureInfo.InvariantCulture, out var resultValue))
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

            if (float.TryParse(text, RealNumberStyles, CultureInfo.InvariantCulture, out result))
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
        /// <returns>Вещественное значение двойной точности.</returns>
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
            return ParseableTextReal(text);
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Вещественное значение двойной точности.</returns>
        public static double ParseDouble(string text, double defaultValue = 0)
        {
            text = ParsableTextDouble(text);

            if (double.TryParse(text, RealNumberStyles, CultureInfo.InvariantCulture, out var resultValue))
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

            if (double.TryParse(text, RealNumberStyles, CultureInfo.InvariantCulture, out result))
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
        /// <returns>Вещественное значение.</returns>
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
            return ParseableTextReal(text);
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Вещественное значение.</returns>
        public static decimal ParseDecimal(string text, decimal defaultValue = 0)
        {
            text = ParsableTextDecimal(text);

            if (decimal.TryParse(text, RealNumberStyles, CultureInfo.InvariantCulture, out var resultValue))
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
        /// <returns>Вещественное значение.</returns>
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