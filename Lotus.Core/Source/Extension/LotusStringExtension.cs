using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Компаратор для сортировки списка файлов и директорий.
    /// </summary>
    /// <remarks>
    /// Директории располагаются внизу списка.
    /// </remarks>
    public class CFileNameComparer : Comparer<string>
    {
        #region IComparer methods
        /// <summary>
        /// Сравнение строк для упорядочивания.
        /// </summary>
        /// <param name="x">Первая строка.</param>
        /// <param name="y">Вторая строка.</param>
        /// <returns>Статус сравнения строк.</returns>
        public override int Compare(string? x, string? y)
        {
            if (x == y)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(x))
            {
                return -1;
            }
            if (string.IsNullOrEmpty(y))
            {
                return 1;
            }

            if (System.IO.Path.HasExtension(x))
            {
                if (System.IO.Path.HasExtension(y))
                {
                    if (x.Length > y.Length)
                    {
                        return 1;
                    }
                    else
                    {
                        if (x.Length < y.Length)
                        {
                            return -1;
                        }
                        else
                        {
                            return string.CompareOrdinal(x, y);
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (System.IO.Path.HasExtension(y))
                {
                    return 1;
                }
                else
                {
                    if (x.Length > y.Length)
                    {
                        return 1;
                    }
                    else
                    {
                        if (x.Length < y.Length)
                        {
                            return -1;
                        }
                        else
                        {
                            return string.CompareOrdinal(x, y);
                        }
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширений строкового типа.
    /// </summary>
    public static class XStringExtension
    {
        #region Fields
        /// <summary>
        /// Формат предоставления десятичных чисел.
        /// </summary>
        public static readonly NumberFormatInfo NumberFormatInfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        /// <summary>
        /// Регулярное выражение для символов латиницы.
        /// </summary>
        private static readonly Regex RegexLatin = new Regex(@"\p{IsBasicLatin}");

        /// <summary>
        /// Регулярное выражение для символов кириллицы.
        /// </summary>
        private static readonly Regex RegexCyrllics = new Regex(@"\p{IsCyrillic}");
        #endregion

        #region Check methods
        /// <summary>
        /// Проверка на нулевое значение строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Статус нулевого значения строки.</returns>
        public static bool IsNull(this string @this)
        {
            return @this == null;
        }

        /// <summary>
        /// Проверка на существование данных строки.
        /// </summary>
        /// <remarks>
        /// Данные существую если строка не пустая и ненулевая.
        /// </remarks>
        /// <param name="this">Строка.</param>
        /// <returns>Статус существования данных строки.</returns>
        public static bool IsExists(this string @this)
        {
            return string.IsNullOrEmpty(@this) == false;
        }

        /// <summary>
        /// Проверка на содержание в строки символов латиницы.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Статус наличия символов латиницы.</returns>
        public static bool IsLatinSymbols(this string @this)
        {
            return RegexLatin.IsMatch(@this);
        }

        /// <summary>
        /// Проверка на содержание в строки символов кириллицы.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Статус наличия символов кириллицы.</returns>
        public static bool IsCyrillicSymbols(this string @this)
        {
            return RegexCyrllics.IsMatch(@this);
        }

        /// <summary>
        /// Проверка на содержание в строки символов алфавита.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Статус наличия символов алфавита.</returns>
        public static bool IsLetterSymbols(this string @this)
        {
            for (var i = 0; i < @this.Length; i++)
            {
                if (char.IsLetter(@this[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка на содержание в строки символа запятой или точки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Статус наличия символов запятой или точки.</returns>
        public static bool IsDotOrCommaSymbols(this string @this)
        {
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == XCharHelper.Dot || @this[i] == XCharHelper.Comma)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Equal methods
        /// <summary>
        /// Проверка на равенство строк с учетом регистра.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="str">Сравниваемая строка.</param>
        /// <returns>Статус равенства строк.</returns>
        public static bool Equal(this string @this, string? str)
        {
            return string.Compare(@this, str, false) == 0;
        }

        /// <summary>
        /// Проверка на равенство строк без учета регистра.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="str">Сравниваемая строка.</param>
        /// <returns>Статус равенства строк.</returns>
        public static bool EqualIgnoreCase(this string @this, string? str)
        {
            return string.Compare(@this, str, true) == 0;
        }
        #endregion

        #region Convert methods
        /// <summary>
        /// Конвертация в вещественный тип.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Число.</returns>
        public static float ToFloat(this string @this)
        {
            return XNumberHelper.ParseInt(@this);
        }

        /// <summary>
        /// Конвертация в вещественный тип.
        /// </summary>
        /// <remarks>
        /// Более быстрая чем стандартная версия, но без проверки.
        /// </remarks>
        /// <param name="this">Строка.</param>
        /// <returns>Число.</returns>
        public static float ToFloatUnchecked(this string @this)
        {
            var ret_val1 = 0f;
            var ret_val2 = 0f;
            var sign = 1f;
            if (@this != null)
            {
                var dir = 10f;
                int i;
                var i_max = @this.Length;
                char c;
                for (i = 0; i < i_max; i++)
                {
                    c = @this[i];
                    if (c >= '0' && c <= '9')
                    {
                        ret_val1 *= dir;
                        ret_val1 += c - '0';
                    }
                    else
                    {
                        if (c == '.')
                        {
                            break;
                        }
                        else
                        {
                            if (c == '-')
                            {
                                sign = -1f;
                            }
                        }
                    }
                }
                i++;
                dir = 0.1f;
                for (; i < i_max; i++)
                {
                    c = @this[i];
                    if (c >= '0' && c <= '9')
                    {
                        ret_val2 += (c - '0') * dir;
                        dir *= 0.1f;
                    }
                }
            }
            return sign * (ret_val1 + ret_val2);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конвертация строки в цвет. Формат строки "RRGGBB".
		/// </summary>
		/// <param name="this">Строка.</param>
		/// <returns>Цвет.</returns>
		public static UnityEngine.Color ToColor24(this String @this)
		{
			try
			{
				var data = Convert.ToInt32(@this.Length > 6 ? @this.Substring(0, 6) : @this, 16);
				return new UnityEngine.Color(
					((data >> 16) & 0xff) / 255f,
					((data >> 8) & 0xff) / 255f,
					(data & 0xff) / 255f,
					1f);
			}
			catch
			{
				return UnityEngine.Color.black;
			}
		}

		/// <summary>
		/// Конвертация строки в цвет. Формат строки "RRGGBBAA".
		/// </summary>
		/// <param name="this">Строка.</param>
		/// <returns>Цвет.</returns>
		public static UnityEngine.Color ToColor32(this String @this)
		{
			try
			{
				var data = Convert.ToInt32(@this.Length > 8 ? @this.Substring(0, 8) : @this, 16);
				return new UnityEngine.Color
					(((data >> 24) & 0xff) / 255f,
					((data >> 16) & 0xff) / 255f,
					((data >> 8) & 0xff) / 255f,
					(data & 0xff) / 255f);
			}
			catch
			{
				return UnityEngine.Color.black;
			}
		}
#endif
        #endregion

        #region Transform methods
        /// <summary>
        /// Преобразование к вертикальной конфигурации строки.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string GetVerticalCopy(this string @this)
        {
            if (@this.Length > 1)
            {
                var result = new StringBuilder(@this.Length * 2);
                for (var i = 0; i < @this.Length - 1; i++)
                {
                    result.Append(@this[i]);
                    result.Append("\n");
                }

                result.Append(@this[@this.Length - 1]);
                return result.ToString();
            }
            else
            {
                return @this;
            }

        }

        /// <summary>
        /// Изменение порядка символов в строке на обратный.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string GetReverseCopy(this string @this)
        {
            var char_array = @this.ToCharArray();
            Array.Reverse(char_array);
            return new string(char_array);
        }

        /// <summary>
        /// Вставка определенного количества символов в указанную позицию строки.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <param name="symbol">Символ для вставки.</param>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="count">Количество символов.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string InsertSymbols(this string @this, char symbol, int index, int count = 1)
        {
            return @this.Insert(index, new string(symbol, count));
        }

        /// <summary>
        /// Установка длины строки.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <param name="length">Требуемая длина строки.</param>
        /// <param name="symbol">Символ для заполнения.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string SetLength(this string @this, int length, char symbol)
        {
            if (@this.Length > length)
            {
                return @this.Remove(length);
            }
            else
            {
                if (@this.Length < length)
                {
                    var count = length - @this.Length;
                    return @this.Insert(@this.Length - 1, new string(symbol, count));
                }
                else
                {
                    return @this;
                }
            }
        }

        /// <summary>
        /// Установка длины строки с учетом размера знака табулятора.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <param name="length">Требуемая длина строки.</param>
        /// <param name="symbol">Символ для заполнения.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string SetLengthTabs(this string @this, int length, char symbol)
        {
            var original = @this.Replace("\t", "[tt]");

            original = SetLength(original, length, symbol);

            original = original.Replace("[tt]", "\t");

            return original;
        }
        #endregion

        #region Calc methods
        /// <summary>
        /// Получение количества указанных символов.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <param name="symbol">Искомый символ.</param>
        /// <returns>Количество символов.</returns>
        public static int GetCountSymbol(this string @this, char symbol)
        {
            var count = 0;
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == symbol)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Получение количества символов новой линии.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <returns>Количество символов новой линии.</returns>
        public static int GetCountNewLine(this string @this)
        {
            var count = 0;
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == XCharHelper.NewLine)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Получение количества символов табуляции.
        /// </summary>
        /// <param name="this">Исходная строка.</param>
        /// <returns>Количество символов табуляции.</returns>
        public static int GetCountTab(this string @this)
        {
            var count = 0;
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == XCharHelper.Tab)
                {
                    count++;
                }
            }

            return count;
        }
        #endregion

        #region Find methods
        /// <summary>
        /// Статус вхождение строки с указанными параметрами сравнения.
        /// </summary>
        /// <remarks>
        /// Credits to JaredPar http://stackoverflow.com/questions/444798/case-insensitive-containsstring/444818#444818.
        /// </remarks>
        /// <param name="this">Строка.</param>
        /// <param name="check">Искомая строка.</param>
        /// <param name="comparer">Компаратор сравнения.</param>
        /// <returns>Статус вхождение.</returns>
        public static bool Contains(this string @this, string check, StringComparison comparer)
        {
            return @this.IndexOf(check, comparer) >= 0;
        }

        /// <summary>
        /// Поиск вхождения в строку любой строки из списка строк.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="list">Список строк.</param>
        /// <returns>Позиция вхождения в строку любой строки из списка строк или - 1.</returns>
        public static int IndexOf(this string @this, IList<string> list)
        {
            var result = -1;

            for (var i = 0; i < list.Count; i++)
            {
                result = @this.IndexOf(list[i]);
                if (result > -1)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Статус вхождение строки с указанными опциями поиска.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="check">Искомая строка.</param>
        /// <param name="searchOption">Опции поиска строки.</param>
        /// <returns>Статус вхождение.</returns>
        public static bool FindFromSearchOption(this string @this, string check, TStringSearchOption searchOption)
        {
            switch (searchOption)
            {
                case TStringSearchOption.Start:
                    {
                        return @this.StartsWith(check);
                    }
                case TStringSearchOption.End:
                    {
                        return @this.EndsWith(check);
                    }
                case TStringSearchOption.Contains:
                    {
                        return @this.IndexOf(check) > -1;
                    }
                case TStringSearchOption.Equal:
                    {
                        return string.CompareOrdinal(@this, check) == 0;
                    }
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// Статус вхождение строки с указанными опциями поиска без учета регистра.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="check">Искомая строка.</param>
        /// <param name="searchOption">Опции поиска строки.</param>
        /// <returns>Статус вхождение.</returns>
        public static bool FindFromSearchOptionIgnoreCase(this string @this, string check, TStringSearchOption searchOption)
        {
            switch (searchOption)
            {
                case TStringSearchOption.Start:
                    {
                        return @this.StartsWith(check, true, CultureInfo.CurrentCulture);
                    }
                case TStringSearchOption.End:
                    {
                        return @this.EndsWith(check, true, CultureInfo.CurrentCulture);
                    }
                case TStringSearchOption.Contains:
                    {
                        return @this.IndexOf(check, 0, StringComparison.CurrentCultureIgnoreCase) > -1;
                    }
                case TStringSearchOption.Equal:
                    {
                        return string.Compare(@this, check, true) == 0;
                    }
                default:
                    break;
            }

            return false;
        }
        #endregion

        #region Remove methods
        /// <summary>
        /// Удаление символов до первого совпадение указанной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveTo(this string @this, string what)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                return @this.Remove(0, index);
            }
            return @this;
        }

        /// <summary>
        /// Удаление символов до первого совпадение указанной строки и саму указанную строку.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveToWith(this string @this, string what)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                return @this.Remove(0, index + what.Length);
            }
            return @this;
        }

        /// <summary>
        /// Удаление символов от конца первого совпадение указанной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveFrom(this string @this, string what)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                return @this.Remove(index + what.Length);
            }
            return @this;
        }

        /// <summary>
        /// Удаление первого совпадение указанной строки из исходной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveFirstOccurrence(this string @this, string what)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                return @this.Remove(index, what.Length);
            }
            return @this;
        }

        /// <summary>
        /// Удаление последнего совпадения заданной строки из исходной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveLastOccurrence(this string @this, string what)
        {
            var index = @this.LastIndexOf(what);
            if (index > -1)
            {
                return @this.Remove(index, what.Length);
            }
            return @this;
        }

        /// <summary>
        /// Удаление расширение из исходной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveExtension(this string @this)
        {
            var index = @this.LastIndexOf(XCharHelper.Dot);
            if (index > -1)
            {
                return @this.Remove(index);
            }
            return @this;
        }

        /// <summary>
        /// Удаление из строки массив токенов.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="tokens">Массив токенов.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveTokens(this string @this, params string[] tokens)
        {
            var result = @this;
            for (var i = 0; i < tokens.Length; i++)
            {
                result = result.Replace(tokens[i], string.Empty);
            }

            return result;
        }

        /// <summary>
        /// Удаление вхождения всех символов между указанными символами.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="left">Символ слева.</param>
        /// <param name="right">Символ справа.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveAllBetweenSymbol(this string @this, char left, char right)
        {
            var builder = new StringBuilder(@this.Length);

            var is_opened = false;
            var is_pre_opened = false;
            for (var i = 0; i < @this.Length; i++)
            {
                if (is_pre_opened)
                {
                    is_opened = true;
                }

                if (@this[i] == left)
                {
                    is_pre_opened = true;
                }

                if (@this[i] == right)
                {
                    is_opened = false;
                    is_pre_opened = false;
                }

                if (is_opened == false)
                {
                    builder.Append(@this[i]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Удаление вхождения всех символов между указанными символами и самих символов.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="left">Символ слева.</param>
        /// <param name="right">Символ справа.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveAllBetweenSymbolWithSymbols(this string @this, char left, char right)
        {
            var builder = new StringBuilder(@this.Length);

            var is_opened = false;
            var is_pre_opened = false;
            for (var i = 0; i < @this.Length; i++)
            {
                if (@this[i] == left)
                {
                    is_opened = true;
                    is_pre_opened = true;
                }

                if (is_pre_opened == false)
                {
                    is_opened = false;
                }

                if (@this[i] == right)
                {
                    is_pre_opened = false;
                }

                if (is_opened == false)
                {
                    builder.Append(@this[i]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Удаления вхождение строки с указанными опциями поиска.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="check">Искомая строка.</param>
        /// <param name="searchOption">Опции поиска строки.</param>
        /// <returns>Модифицированная строка.</returns>
        public static string RemoveFromSearchOption(this string @this, string check, TStringSearchOption searchOption)
        {
            switch (searchOption)
            {
                case TStringSearchOption.Start:
                    {
                        if (@this.StartsWith(check))
                        {
                            return @this.Remove(0, check.Length);
                        }
                    }
                    break;
                case TStringSearchOption.End:
                    {
                        if (@this.EndsWith(check))
                        {
                            return @this.Remove(@this.Length - check.Length);
                        }
                    }
                    break;
                case TStringSearchOption.Contains:
                    {
                        var index = @this.IndexOf(check);
                        if (index > -1)
                        {
                            return @this.Remove(index, check.Length);
                        }
                    }
                    break;
                case TStringSearchOption.Equal:
                    {
                    }
                    break;
                default:
                    break;
            }

            return @this;
        }
        #endregion

        #region Extract methods
        /// <summary>
        /// Получение подстроки до первого вхождения указанной строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <param name="include">Включать ли заданную строку в результат.</param>
        /// <returns>Найденная подстрока или текущая строка.</returns>
        public static string SubstringTo(this string @this, string what, bool include)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                if (include)
                {
                    return @this.Substring(0, index + what.Length);
                }
                else
                {
                    return @this.Substring(0, index);
                }
            }
            return @this;
        }

        /// <summary>
        /// Получение подстроки от первого вхождения указанной строки и до конца строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="what">Заданная строка.</param>
        /// <param name="include">Включать ли заданную строку в результат.</param>
        /// <returns>Найденная подстрока или текущая строка.</returns>
        public static string SubstringFrom(this string @this, string what, bool include)
        {
            var index = @this.IndexOf(what);
            if (index > -1)
            {
                if (include)
                {
                    return @this.Substring(index);
                }
                else
                {
                    return @this.Substring(index + what.Length);
                }
            }
            return @this;
        }

        /// <summary>
        /// Извлечение из строки числа.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Найденное число.</returns>
        public static int ExtractNumber(this string @this)
        {
            var number = new StringBuilder(4);
            var find = false;
            for (var i = 0; i < @this.Length; i++)
            {
                if (char.IsDigit(@this[i]))
                {
                    find = true;
                    number.Append(@this[i]);
                }
                else
                {
                    // Если мы уже находили символ
                    if (find)
                    {
                        break;
                    }
                }
            }

            var result = XNumberHelper.ParseInt(number.ToString(), -1);
            return result;
        }

        /// <summary>
        /// Извлечение из строки числа. Поиск осуществляется от конца.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Найденное число.</returns>
        public static int ExtractNumberLast(this string @this)
        {
            var number = new StringBuilder(4);
            var find = false;
            for (var i = @this.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(@this[i]))
                {
                    find = true;
                    number.Append(@this[i]);
                }
                else
                {
                    // Если мы уже находили символ
                    if (find)
                    {
                        break;
                    }
                }
            }

            var result = XNumberHelper.ParseInt(number.ToString().GetReverseCopy(), -1);
            return result;
        }

        /// <summary>
        /// Извлечение из строки определенной части строки.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="begin">Строка начала.</param>
        /// <param name="end">Строка окончания.</param>
        /// <returns>Извлеченная строка.</returns>
        public static string ExtractString(this string @this, string begin, string end)
        {
            var pos_begin = @this.IndexOf(begin);
            if (pos_begin > -1)
            {
                var pos_end = @this.IndexOf(end, pos_begin);
                var start = pos_begin + begin.Length;
                if (pos_end > start)
                {
                    var l = pos_end - start;
                    return @this.Substring(start, l);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Извлечение из строки определенной части строки. Поиск осуществляется от конца.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="begin">Строка начала.</param>
        /// <param name="end">Строка окончания.</param>
        /// <returns>Извлеченная строка.</returns>
        public static string ExtractStringLast(this string @this, string begin, string end)
        {
            var pos_begin = @this.LastIndexOf(begin);
            var pos_end = @this.LastIndexOf(end);
            var start = pos_begin + begin.Length;
            if (pos_end > start)
            {
                var l = pos_end - start;
                return @this.Substring(start, l);
            }

            return string.Empty;
        }
        #endregion

        #region Word methods
        /// <summary>
        /// Преобразование строки по формату value => Value.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToWordUpper(this string @this)
        {
            var builder = new StringBuilder(@this);
            for (var i = 0; i < builder.Length; i++)
            {
                if (char.IsLetter(builder[i]))
                {
                    builder[i] = char.ToUpper(builder[i]);
                    break;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Преобразование строки по формату Value => value.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToWordLower(this string @this)
        {
            var builder = new StringBuilder(@this);
            for (var i = 0; i < builder.Length; i++)
            {
                if (char.IsLetter(builder[i]))
                {
                    builder[i] = char.ToLower(builder[i]);
                    break;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Преобразование строки к допустимому имени переменной.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToVarName(this string @this)
        {
            return @this.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Преобразование строки по формату MY_INT_VALUE => MyIntValue.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToTitleCase(this string @this)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < @this.Length; i++)
            {
                var current = @this[i];
                if (current == '_' && i + 1 < @this.Length)
                {
                    var next = @this[i + 1];
                    if (char.IsLower(next))
                    {
                        next = char.ToUpper(next);
                    }
                    builder.Append(next);
                    i++;
                }
                else
                {
                    builder.Append(current);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Преобразование строки по формату MyIntValue => MY_INT_VALUE.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToConstCase(this string @this)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < @this.Length; i++)
            {
                var current = @this[i];

                if (current == XCharHelper.Space) continue;

                if (char.IsUpper(current))
                {
                    if (i > 0)
                    {
                        if (char.IsLower(@this[i - 1]) || (@this[i - 1] == XCharHelper.Space))
                        {
                            builder.Append('_');
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(current);
                        }
                    }
                    else
                    {
                        builder.Append(current);
                    }
                }
                else
                {
                    builder.Append(char.ToUpper(current));
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Преобразование строки по формату "tHiS is a sTring TesT" -> "This Is A String Test".
        /// </summary>
        /// <remarks>
        /// Credits: http://extensionmethod.net/csharp/String/topropercase.
        /// </remarks>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string ToProperCase(this string @this)
        {
            var culture_info = System.Threading.Thread.CurrentThread.CurrentCulture;
            var text_info = culture_info.TextInfo;
            return text_info.ToTitleCase(@this);
        }

        /// <summary>
        /// Преобразование строки по формату "thisIsCamelCase" -> "this Is Camel Case".
        /// </summary>
        /// <remarks>
        /// Credits: http://stackoverflow.com/questions/155303/net-how-can-you-split-a-caps-delimited-String-into-an-array.
        /// </remarks>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string SplitCamelCase(this string @this)
        {
            return Regex.Replace(@this, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        /// <summary>
        /// Преобразование строки по формату "thisIsCamelCase" -> "This Is Camel Case".
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Преобразованная строка.</returns>
        public static string SplitPascalCase(this string @this)
        {
            return string.IsNullOrEmpty(@this) ? @this : @this.SplitCamelCase().ToUpper();
        }

        /// <summary>
        /// Разбивка строки на токены по словам начинающих с заглавной буквы.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Список токенов.</returns>
        public static List<string> SplitToTokensFromWord(this string @this)
        {
            var tokens = new List<string>();

            // Статус окончания
            var end = false;
            for (var i = 0; i < @this.Length; i++)
            {
                var token = new StringBuilder(10);
                var start = false;

                if (end)
                {
                    break;
                }

                for (var j = i; j < @this.Length; j++)
                {
                    // Информируем о конце
                    if (j == @this.Length - 1)
                    {
                        end = true;
                        token.Append(@this[j]);
                        break;
                    }

                    // Статус перехода к следующему токену
                    if (j != 0 && char.IsUpper(@this[j]))
                    {
                        if (start)
                        {
                            i = j - 1;
                            break;
                        }
                    }

                    token.Append(@this[j]);
                    if (start == false) start = true;
                }

                tokens.Add(token.ToString());
            }

            return tokens;
        }

        /// <summary>
        /// Объединение некоторых элементов списка токенов.
        /// </summary>
        /// <remarks>
        /// Метод находит индекс первого токена и все последующие за ним объединяет в указанный индекс,
        /// а последующие элементы списка удаляет
        /// </remarks>
        /// <param name="tokens">Список токенов.</param>
        /// <param name="list">Массив токенов которые нужно объединить в один.</param>
        public static void JoinTokens(this List<string> tokens, params string[] list)
        {
            if (list == null || list.Length < 2) return;

            // Ищем первый элемент списка
            var index = -1;
            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] == list[0])
                {
                    index = i;
                    break;
                }
            }

            // Если нашли и он не последний
            if (index != -1 && index != tokens.Count - 1)
            {
                // Рассмотрим частные случае
                if (list.Length == 2)
                {
                    var i1 = tokens.IndexOf(list[1], index);
                    if (i1 != -1)
                    {
                        tokens[index] = tokens[index] + tokens[i1];
                        tokens.RemoveAt(i1);
                    }
                }

                if (list.Length == 3)
                {
                    var i1 = tokens.IndexOf(list[1], index);
                    var i2 = tokens.IndexOf(list[2], index);
                    if (i1 != -1 && i2 != -1 && i1 == i2 - 1)
                    {
                        tokens[index] = tokens[index] + tokens[i1] + tokens[i2];
                        tokens.RemoveAt(i1);
                        tokens.RemoveAt(i1);
                    }
                }
            }
        }
        #endregion

        #region Format HTML methods
        /// <summary>
        /// Форматирование строки к жирному отображению.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <returns>Отформатированная строка.</returns>
        public static string ToDrawBold(this string @this)
        {
            return "<b>" + @this + "</b>";
        }

        /// <summary>
        /// Форматирование строки к цветному отображению.
        /// </summary>
        /// <param name="this">Строка.</param>
        /// <param name="color">Цвет строки.</param>
        /// <returns>Отформатированная строка.</returns>
        public static string ToDrawColor(this string @this, TColor color)
        {
            return "<color=#" + color.ToStringHEX() + ">" + @this + "</color>";
        }
        #endregion
    }
    /**@}*/
}