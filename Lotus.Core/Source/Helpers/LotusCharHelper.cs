namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе с символами.
    /// </summary>
    public static class XCharHelper
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