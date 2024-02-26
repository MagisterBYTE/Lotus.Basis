using System;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
    *@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с типом <see cref="bool"/>.
    /// </summary>
    public static class XBoolean
    {
        /// <summary>
        /// Текстовые значение логического типа которые означает истинное значение.
        /// </summary>
        public static readonly string[] TrueValues = new string[]
        {
            "True",
            "true",
            "1",
            "on",
            "On",
            "истина",
            "Истина",
            "да",
            "Да"
        };

        /// <summary>
        /// Преобразование текста в логическое значение.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Значение.</returns>
        public static bool Parse(string text)
        {
            return Array.IndexOf(TrueValues, text) > -1;
        }
    }
    /**@}*/
}