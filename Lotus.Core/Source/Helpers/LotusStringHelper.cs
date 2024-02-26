using System.Collections.Generic;
using System.ComponentModel;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Опции поиска в строке другой подстроки.
    /// </summary>
    [TypeConverter(typeof(EnumToStringConverter<TStringSearchOption>))]
    public enum TStringSearchOption
    {
        /// <summary>
        /// Подстрока должна находится в начала.
        /// </summary>
        [Description("В начале")]
        Start,

        /// <summary>
        /// Подстрока должна находится в конце.
        /// </summary>
        [Description("В конце")]
        End,

        /// <summary>
        /// Подстрока может находиться в любом месте.
        /// </summary>
        [Description("Содержит")]
        Contains,

        /// <summary>
        /// Подстрока должна точно совпадать.
        /// </summary>
        [Description("Равно")]
        Equal
    }

    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе со строками.
    /// </summary>
    public static class XString
    {
        #region Fields
        //
        // КОНСТАНТНЫЕ ДАННЫЕ
        //
        /// <summary>
        /// Символ разделитель новой строки.
        /// </summary>
        public const string NewLine = "\n";

        /// <summary>
        /// Символ разделитель возврата каретки.
        /// </summary>
        public const string CarriageReturn = "\r";

        /// <summary>
        /// Символ смайлика.
        /// </summary>
        public const string Smiley = "☺";

        /// <summary>
        /// Символ света.
        /// </summary>
        public const string Light = "☼";

        /// <summary>
        /// Символ стрелки вверх.
        /// </summary>
        public const string ArrowUp = "↑";

        /// <summary>
        /// Символ стрелки вниз.
        /// </summary>
        public const string ArrowDown = "↓";

        /// <summary>
        /// Символ стрелки вправо.
        /// </summary>
        public const string ArrowRight = "→";

        /// <summary>
        /// Символ стрелки влево.
        /// </summary>
        public const string ArrowLeft = "←";

        /// <summary>
        /// Символ треугольника вверх.
        /// </summary>
        public const string TriangleUp = "▲";

        /// <summary>
        /// Символ треугольника вниз.
        /// </summary>
        public const string TriangleDown = "▼";

        /// <summary>
        /// Символ треугольника вправо.
        /// </summary>
        public const string TriangleRight = "►";

        /// <summary>
        /// Символ треугольника влево.
        /// </summary>
        public const string TriangleLeft = "◄";

        /// <summary>
        /// Символ дома.
        /// </summary>
        public const string Home = "⌂";

        /// <summary>
        /// Символ кавычки слева (французские).
        /// </summary>
        public const string QuoteLeft = "«";

        /// <summary>
        /// Символ кавычки справа (французские).
        /// </summary>
        public const string QuoteRight = "»";

        /// <summary>
        /// Символ меню.
        /// </summary>
        public const string Menu = "≡";

        /// <summary>
        /// Символ квадрата.
        /// </summary>
        public const string Square = "■";

        /// <summary>
        /// Символ плюс.
        /// </summary>
        public const string Plus = "+";

        /// <summary>
        /// Символ минус.
        /// </summary>
        public const string Minus = "-";

        //
        // ДАННЫЕ РАЗДЕЛИТЕЛЕЙ
        //
        /// <summary>
        /// Разделителей для данных текстовых файлов.
        /// </summary>
        /// <remarks>
        /// Данный разделитель используется чтобы отделить некий заголовок(ключ) от следующих за ним по порядку данных.
        /// </remarks>
        public static readonly string SeparatorFileData = "##";

        /// <summary>
        /// Массив символов разделителей (новая строка).
        /// </summary>
        public static readonly string[] SeparatorNewLine = new string[] { "\n" };

        /// <summary>
        /// Массив символов разделителей (новая строка и возврат каретки).
        /// </summary>
        public static readonly string[] SeparatorNewCarriageLine = new string[] { "\n", "\r" };

        /// <summary>
        /// Массив символов разделителей (запятая и новая строка).
        /// </summary>
        public static readonly string[] SeparatorComma = new string[] { ",", "\n" };

        /// <summary>
        /// Массив символов разделителей (точка запятая).
        /// </summary>
        public static readonly string[] SeparatorDotComma = new string[] { ";" };

        /// <summary>
        /// Массив символов разделителей (квадратные скобки).
        /// </summary>
        public static readonly string[] SeparatorSquareBracket = new string[] { "[", "]" };

        /// <summary>
        /// Массив символов разделителей (нижние подчеркивание).
        /// </summary>
        public static readonly string[] SeparatorLowLine = new string[] { "_" };

        /// <summary>
        /// Массив символов разделителей (пробел).
        /// </summary>
        public static readonly string[] SeparatorSpaces = new string[] { " " };

        /// <summary>
        /// Массив символов разделителей (символ табуляции).
        /// </summary>
        public static readonly string[] SeparatorTab = new string[] { "\t" };

        /// <summary>
        /// Массив символов для разделение на предложения.
        /// </summary>
        public static readonly string[] SeparatorSentences = new string[] { ".", "!", "?" };

        /// <summary>
        /// Массив символов табуляции.
        /// </summary>
        public static readonly string[] Depths = new string[]
        {
            "",
            "\t",
            "\t\t",
            "\t\t\t",
            "\t\t\t\t",
            "\t\t\t\t\t",
            "\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t\t\t\t\t",
            "\t\t\t\t\t\t\t\t\t\t\t\t\t\t",
        };

        /// <summary>
        /// Массив символов пробела.
        /// </summary>
        public static readonly string[] Spaces = new string[]
        {
            "",
            new string(XChar.Space, 1),
            new string(XChar.Space, 2),
            new string(XChar.Space, 3),
            new string(XChar.Space, 4),
            new string(XChar.Space, 5),
            new string(XChar.Space, 6),
            new string(XChar.Space, 7),
            new string(XChar.Space, 8),
            new string(XChar.Space, 9),
            new string(XChar.Space, 10),
            new string(XChar.Space, 11),
            new string(XChar.Space, 12),
            new string(XChar.Space, 13),
            new string(XChar.Space, 14),
            new string(XChar.Space, 15)
        };

        /// <summary>
        /// Массив символов пробела c начальным символом новой строки.
        /// </summary>
        /// <remarks>
        /// При последовательном присвоение позволяет получить иерархически выглядящую строку.
        /// </remarks>
        public static readonly string[] HierarchySpaces = new string[]
        {
            "",
            new string(XChar.Space, 1).Insert(0, XString.NewLine),
            new string(XChar.Space, 2).Insert(0, XString.NewLine),
            new string(XChar.Space, 3).Insert(0, XString.NewLine),
            new string(XChar.Space, 4).Insert(0, XString.NewLine),
            new string(XChar.Space, 5).Insert(0, XString.NewLine),
            new string(XChar.Space, 6).Insert(0, XString.NewLine),
            new string(XChar.Space, 7).Insert(0, XString.NewLine),
            new string(XChar.Space, 8).Insert(0, XString.NewLine),
            new string(XChar.Space, 9).Insert(0, XString.NewLine),
            new string(XChar.Space, 10).Insert(0, XString.NewLine),
            new string(XChar.Space, 11).Insert(0, XString.NewLine),
            new string(XChar.Space, 12).Insert(0, XString.NewLine),
            new string(XChar.Space, 13).Insert(0, XString.NewLine),
            new string(XChar.Space, 14).Insert(0, XString.NewLine),
            new string(XChar.Space, 15).Insert(0, XString.NewLine)
        };
        #endregion

        #region Main methods 
        /// <summary>
        /// Конвертация массива строк в группы строк разделяемых по указанному разделителю.
        /// </summary>
        /// <remarks>
        /// Метод ищет по строкам вхождение разделителя (должен быть в начале строки) и присваивает его ключу.
        /// Дальнейшие строки, до другого вхождения разделителя, присваиваются значению
        /// </remarks>
        /// <param name="lines">Массив строк.</param>
        /// <param name="delimiters">Строка для разделения данных.</param>
        /// <returns>Список данных ключ-значение.</returns>
        public static List<KeyValuePair<string, string>> ConvertLinesToGroupLines(string[] lines, string delimiters)
        {
            var result = new List<KeyValuePair<string, string>>(30);

            if (lines != null && lines.Length > 1)
            {
                // Проходим все строки в файле
                for (var i = 0; i < lines.Length; i++)
                {
                    // Подготавливаем для анализа токен
                    var token = lines[i].Trim(XChar.NewLine, XChar.CarriageReturn, XChar.Space);

                    // Если нашли разделитель (должен быть в начале)
                    if (token.IndexOf(delimiters) == 0)
                    {
                        // В ключ записываем данные разделения
                        var key = token;

                        var current_value = "";

                        // Читаем данные (со следующей строк)
                        for (var j = i + 1; j < lines.Length; j++)
                        {
                            // Пустые пропускаем
                            if (string.IsNullOrEmpty(lines[j]))
                            {
                                continue;
                            }

                            // Если дошли до следующего разделителя то выходим
                            if (lines[j].IndexOf(delimiters) == 0)
                            {
                                // Скорректируем позицию чтения
                                i = j - 1;

                                break;
                            }

                            // Добавляем в значение
                            if (string.IsNullOrEmpty(current_value))
                            {
                                current_value = lines[j].Trim();
                            }
                            else
                            {
                                if (lines[j].Length == 1 && (lines[j][0] == XChar.NewLine || lines[j][0] == XChar.CarriageReturn))
                                {

                                }
                                else
                                {
                                    current_value += XChar.NewLine + lines[j].Trim();
                                }
                            }
                        }

                        // Конец
                        result.Add(new KeyValuePair<string, string>(key, current_value));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Конвертация массива строк в группы строк разделяемых по указанному разделителю игнорируя комментарии.
        /// </summary>
        /// <remarks>
        /// Метод ищет по строкам вхождение разделителя (должен быть в начале строки) и присваивает его ключу.
        /// Дальнейшие строки, до другого вхождения разделителя, присваиваются значению.
        /// Также данный метод игнорирует комментарии в стиле С# начинающиеся с символов //
        /// </remarks>
        /// <param name="lines">Массив строк.</param>
        /// <param name="delimiters">Строка для разделения данных.</param>
        /// <returns>Список данных ключ-значение.</returns>
        public static List<KeyValuePair<string, string>> ConvertLinesToGroupLinesIgnoringComments(string[] lines, string delimiters)
        {
            var result = new List<KeyValuePair<string, string>>(30);

            if (lines != null && lines.Length > 1)
            {
                // Проходим все строки
                for (var i = 0; i < lines.Length; i++)
                {
                    // Подготавливаем для анализа токен
                    var token = lines[i].Trim(XChar.NewLine, XChar.CarriageReturn, XChar.Space);

                    // Игнорируем комментарий 
                    if (token.IndexOf("//") == 0) continue;

                    // Если нашли разделитель (должен быть в начале)
                    if (token.IndexOf(delimiters) == 0)
                    {
                        // В ключ записываем данные разделения
                        var key = token;

                        var current_value = "";

                        // Читаем данные (со следующей строк)
                        for (var j = i + 1; j < lines.Length; j++)
                        {
                            // Игнорируем комментарий 
                            if (lines[j].IndexOf("//") == 0) continue;

                            // Пустые пропускаем
                            if (string.IsNullOrEmpty(lines[j]))
                            {
                                continue;
                            }

                            // Если дошли до следующего разделителя то выходим
                            if (lines[j].IndexOf(delimiters) > -1)
                            {
                                // Скорректируем позицию чтения
                                i = j - 1;

                                break;
                            }

                            // Добавляем в значение
                            if (string.IsNullOrEmpty(current_value))
                            {
                                current_value = lines[j].Trim();
                            }
                            else
                            {
                                if (lines[j].Length == 1 && (lines[j][0] == XChar.NewLine || lines[j][0] == XChar.CarriageReturn))
                                {

                                }
                                else
                                {
                                    current_value += XChar.NewLine + lines[j].Trim();
                                }
                            }
                        }

                        // Конец
                        result.Add(new KeyValuePair<string, string>(key, current_value));
                    }
                }
            }

            return result;
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе с символами.
    /// </summary>
    public static class XChar
    {
        //
        // КОНСТАНТНЫЕ ДАННЫЕ
        //
        /// <summary>
        /// Символ разделитель новой строки.
        /// </summary>
        public const char NewLine = '\n';

        /// <summary>
        /// Символ разделитель возврата каретки.
        /// </summary>
        public const char CarriageReturn = '\r';

        /// <summary>
        /// Символ пробела.
        /// </summary>
        public const char Space = ' ';

        /// <summary>
        /// Символ табуляции.
        /// </summary>
        public const char Tab = '\t';

        /// <summary>
        /// Символ смайлика.
        /// </summary>
        public const char Smiley = '☺';

        /// <summary>
        /// Символ света.
        /// </summary>
        public const char Light = '☼';

        /// <summary>
        /// Символ стрелки вверх.
        /// </summary>
        public const char ArrowUp = '↑';

        /// <summary>
        /// Символ стрелки вниз.
        /// </summary>
        public const char ArrowDown = '↓';

        /// <summary>
        /// Символ стрелки вправо.
        /// </summary>
        public const char ArrowRight = '→';

        /// <summary>
        /// Символ стрелки влево.
        /// </summary>
        public const char ArrowLeft = '←';

        /// <summary>
        /// Символ треугольника вверх.
        /// </summary>
        public const char TriangleUp = '▲';

        /// <summary>
        /// Символ треугольника вниз.
        /// </summary>
        public const char TriangleDown = '▼';

        /// <summary>
        /// Символ треугольника вправо.
        /// </summary>
        public const char TriangleRight = '►';

        /// <summary>
        /// Символ треугольника влево.
        /// </summary>
        public const char TriangleLeft = '◄';

        /// <summary>
        /// Символ дома.
        /// </summary>
        public const char Home = '⌂';

        /// <summary>
        /// Символ кавычки слева (французские).
        /// </summary>
        public const char QuoteLeft = '«';

        /// <summary>
        /// Символ кавычки справа (французские).
        /// </summary>
        public const char QuoteRight = '»';

        /// <summary>
        /// Символ двойных кавычек.
        /// </summary>
        public const char DoubleQuotes = '"';

        /// <summary>
        /// Символ меню.
        /// </summary>
        public const char Menu = '≡';

        /// <summary>
        /// Символ квадрата.
        /// </summary>
        public const char Square = '■';

        /// <summary>
        /// Символ плюс.
        /// </summary>
        public const char Plus = '+';

        /// <summary>
        /// Символ минус.
        /// </summary>
        public const char Minus = '-';

        /// <summary>
        /// Символ запятая.
        /// </summary>
        public const char Comma = ',';

        /// <summary>
        /// Символ точки.
        /// </summary>
        public const char Dot = '.';

        //
        // ДАННЫЕ РАЗДЕЛИТЕЛЕЙ
        //
        /// <summary>
        /// Массив символов разделителей (новая строка).
        /// </summary>
        public static readonly char[] SeparatorNewLine = new char[] { '\n' };

        /// <summary>
        /// Массив символов разделителей (новая строка и возврат каретки).
        /// </summary>
        public static readonly char[] SeparatorNewCarriageLine = new char[] { '\n', '\r' };

        /// <summary>
        /// Массив символов разделителей (запятая и новая строка).
        /// </summary>
        public static readonly char[] SeparatorComma = new char[] { ',', '\n' };

        /// <summary>
        /// Массив символов разделителей (точка запятая).
        /// </summary>
        public static readonly char[] SeparatorDotComma = new char[] { ';' };

        /// <summary>
        /// Массив символов разделителей (квадратные скобки).
        /// </summary>
        public static readonly char[] SeparatorSquareBracket = new char[] { '[', ']' };

        /// <summary>
        /// Массив символов разделителей (нижние подчеркивание).
        /// </summary>
        public static readonly char[] SeparatorLowLine = new char[] { '_' };

        /// <summary>
        /// Массив символов разделителей (пробел).
        /// </summary>
        public static readonly char[] SeparatorSpaces = new char[] { ' ' };

        /// <summary>
        /// Массив символов разделителей (символ табуляции).
        /// </summary>
        public static readonly char[] SeparatorTab = new char[] { '\t' };

        /// <summary>
        /// Массив символов для разделение на предложения.
        /// </summary>
        public static readonly char[] SeparatorSentences = new char[] { '.', '!', '?' };
    }
    /**@}*/
}