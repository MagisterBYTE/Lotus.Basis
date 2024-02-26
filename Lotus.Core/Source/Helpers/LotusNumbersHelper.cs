using System.Globalization;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с числовыми типами.
    /// </summary>
    public static class XNumbers
    {
        #region ФОРМАТЫ ОТОБРАЖЕНИЯ ЧИСЕЛ 
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
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static int ParseInt(string text, int defaultValue = 0)
        {
            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;

            }

            return defaultValue;
        }
        #endregion

        #region Int64 
        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static long ParseLong(string text, long defaultValue = 0)
        {
            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }
        #endregion

        #region Single 
        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static float ParseSingle(string text, float defaultValue = 0)
        {
            if (text.IndexOf(',') > -1)
            {
                text = text.Replace(',', XChar.Dot);
            }

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }
        #endregion

        #region Double 
        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static double ParseDouble(string text, double defaultValue = 0)
        {
            if (text.IndexOf(',') > -1)
            {
                text = text.Replace(',', XChar.Dot);
            }

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование форматированного текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static double ParseDoubleFormat(string text, double defaultValue = 0)
        {
            var number = new StringBuilder(text.Length);

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-')
                {
                    number.Append(c);
                    continue;
                }

                if (c == ',' || c == XChar.Dot)
                {
                    if (i != text.Length - 1)
                    {
                        number.Append(XChar.Dot);
                    }
                    continue;
                }

                if (c >= '0' && c <= '9')
                {
                    number.Append(c);
                }

            }

            if (double.TryParse(number.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование форматированного текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseDoubleFormat(string text, out double result)
        {
            var number = new StringBuilder(text.Length);

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-')
                {
                    number.Append(c);
                    continue;
                }

                if (c == ',' || c == XChar.Dot)
                {
                    if (i != text.Length - 1)
                    {
                        number.Append(XChar.Dot);
                    }
                    continue;
                }

                if (c >= '0' && c <= '9')
                {
                    number.Append(c);
                }

            }

            if (double.TryParse(number.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Decimal 
        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ParseDecimal(string text, decimal defaultValue = 0)
        {
            if (text.IndexOf(',') > -1)
            {
                text = text.Replace(',', XChar.Dot);
            }

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
            if (text.IndexOf(',') > -1)
            {
                text = text.Replace(',', XChar.Dot);
            }

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