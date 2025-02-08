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
    }
    /**@}*/
}