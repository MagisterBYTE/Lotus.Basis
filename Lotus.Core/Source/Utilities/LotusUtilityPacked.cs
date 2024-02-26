namespace Lotus.Core
{
    /** \addtogroup CoreUtilities
	*@{*/
    /// <summary>
    /// Статический класс для упаковки/распаковки данных в битовом формате.
    /// </summary>
    /// <remarks>
    /// Применяется для упаковки кэшированных данных.
    /// Следует применять там где на важна скорость и не имеет особого смыла полноценно хранить второй экземпляр данных.
    /// </remarks>
    public static class XPacked
    {
        /// <summary>
        /// Упаковка значения типа <see cref="int"/> в битовое поле.
        /// </summary>
        /// <param name="pack">Переменная куда будут упаковываться данные.</param>
        /// <param name="bitStart">Стартовый бит с которого записываются данные.</param>
        /// <param name="bitCount">Количество бит для записи.</param>
        /// <param name="value">Значение для упаковки (будет записано только указанное количество бит).</param>
        public static void PackInteger(ref int pack, int bitStart, int bitCount, int value)
        {
            var mask = (1 << bitCount) - 1;
            pack = (pack & ~(mask << bitStart)) | ((value & mask) << bitStart);
        }

        /// <summary>
        /// Упаковка значения типа <see cref="long"/> в битовое поле.
        /// </summary>
        /// <param name="pack">Переменная куда будут упаковываться данные.</param>
        /// <param name="bitStart">Стартовый бит с которого записываются данные.</param>
        /// <param name="bitCount">Количество бит для записи.</param>
        /// <param name="value">Значение для упаковки (будет записано только указанное количество бит).</param>
        public static void PackLong(ref long pack, int bitStart, int bitCount, long value)
        {
            var mask = (1L << bitCount) - 1L;
            pack = (pack & ~(mask << bitStart)) | ((value & mask) << bitStart);
        }

        /// <summary>
        /// Упаковка значения типа <see cref="bool"/> в битовое поле.
        /// </summary>
        /// <param name="pack">Переменная куда будут упаковываться данные.</param>
        /// <param name="bitStart">Стартовый бит с которого записываются данные.</param>
        /// <param name="value">Значение для упаковки (будет записан только 1 бит).</param>
        public static void PackBoolean(ref int pack, int bitStart, bool value)
        {
            var mask = (1 << 1) - 1;
            if (value)
            {
                pack = (pack & ~(mask << bitStart)) | ((1 & mask) << bitStart);
            }
            else
            {
                pack = (pack & ~(mask << bitStart)) | ((0 & mask) << bitStart);
            }
        }

        /// <summary>
        /// Распаковка значения типа <see cref="int"/> из битового поля.
        /// </summary>
        /// <param name="pack">Упакованные данные.</param>
        /// <param name="bitStart">Стартовый бит с которого начинается распаковка.</param>
        /// <param name="bitCount">Количество бит для чтения.</param>
        /// <returns>Распакованное значение.</returns>
        public static int UnpackInteger(int pack, int bitStart, int bitCount)
        {
            var mask = (1 << bitCount) - 1;
            return (pack >> bitStart) & mask;
        }

        /// <summary>
        /// Распаковка значения типа <see cref="long"/> из битового поля.
        /// </summary>
        /// <param name="pack">Упакованные данные.</param>
        /// <param name="bitStart">Стартовый бит с которого начинается распаковка.</param>
        /// <param name="bitCount">Количество бит для чтения.</param>
        /// <returns>Распакованное значение.</returns>
        public static long UnpackLong(long pack, int bitStart, int bitCount)
        {
            var mask = (1L << bitCount) - 1L;
            return (pack >> bitStart) & mask;
        }

        /// <summary>
        /// Распаковка значения типа <see cref="bool"/> из битового поля.
        /// </summary>
        /// <param name="pack">Упакованные данные.</param>
        /// <param name="bitStart">Стартовый бит с которого начинается распаковка.</param>
        /// <returns>Распакованное значение.</returns>
        public static bool UnpackBoolean(int pack, int bitStart)
        {
            var mask = (1 << 1) - 1;
            var data = (pack >> bitStart) & mask;
            if (data == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    /**@}*/
}