using System;

namespace Lotus.Core
{
    /** \addtogroup CoreIdentifiers
	*@{*/
    /// <summary>
    /// Статический класс реализующий работу генерации идентификаторов и уникальных значений.
    /// </summary>
    public static class XGenerateId
    {
        #region Const
        /// <summary>
        /// 1 января 2024 года.
        /// </summary>
        private static readonly DateTime StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region ГЕНЕРАЦИИ methods
        /// <summary>
        /// Генерация уникального числового идентификатора указанного объекта.
        /// </summary>
        /// <remarks>
        /// В первые 28 бита - записывается хэш-код объекта деленный на 16
        /// В остальные 36 - количество 10 миллисекунд прошедших с 1 января 2020 года
        /// </remarks>
        /// <param name="obj">Объект.</param>
        /// <returns>Уникальный числовой идентификатор.</returns>
        public static long Generate(object obj)
        {
            if (obj == null)
            {
                return -1;
            }
            else
            {
                DateTime current_date = DateTime.UtcNow;
                var elapsed_ticks = current_date.Ticks - StartDate.Ticks;
                var elapsed_millisecond = elapsed_ticks / 100000;

                long result = 0;

                // Пакуем хеш код
                XPacked.PackLong(ref result, 0, 28, obj.GetHashCode() / 16);

                // Пакуем дату
                XPacked.PackLong(ref result, 28, 36, elapsed_millisecond);

                return result;
            }
        }

        /// <summary>
        /// Распаковка и получение хеш-кода из уникального числового идентификатора.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        /// <returns>Хеш-код.</returns>
        public static int UnpackIdToHashCode(long id)
        {
            return (int)XPacked.UnpackLong(id, 0, 28) * 16;
        }

        /// <summary>
        /// Распаковка и получение даты создания из уникального числового идентификатора.
        /// </summary>
        /// <param name="id">Уникальный числовой идентификатор.</param>
        /// <returns>Дата создания.</returns>
        public static DateTime UnpackIdToDateTime(long id)
        {
            var elapsed_millisecond = XPacked.UnpackLong(id, 28, 36);
            var elapsed_ticks = elapsed_millisecond * 100000;
            var result = new DateTime(StartDate.Ticks + elapsed_ticks, DateTimeKind.Utc);
            return result;
        }
        #endregion
    }
    /**@}*/
}