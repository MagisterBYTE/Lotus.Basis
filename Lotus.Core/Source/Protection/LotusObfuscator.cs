using System;
using System.Text;

namespace Lotus.Core
{
    /**
     * \defgroup CoreProtection Подсистема защиты
     * \ingroup Core
     * \brief Подсистема защиты представляет собой подсистему работы с криптографическим средствами, работу с
		утверждениями, а несложные механизмы защиты (шифрования/декодирование) для примитивных типов данных,
		где используются простые методы - манипулирование и операции с битами и циклические сдвиги. 
     * @{
     */
    /// <summary>
    /// Статический класс для обфускации примитивных данных.
    /// </summary>
    public static class XObfuscator
    {
        #region Const
        /// <summary>
        /// Маска для шифрования/декодирование.
        /// </summary>
        public const byte XOR_MASK = 0x53;
        #endregion

        #region МЕТОДЫ 
        /// <summary>
        /// Закодировать строку.
        /// </summary>
        /// <param name="original">Оригинальная строка.</param>
        /// <returns>Закодированная строка.</returns>
        public static string Encode(string original)
        {
            var data = Encoding.UTF8.GetBytes(original);
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ XOR_MASK);
            }
            return Convert.ToBase64String(data);
        }
        /// <summary>
        /// Раскодировать строку.
        /// </summary>
        /// <param name="decode">Закодированная строка.</param>
        /// <returns>Оригинальная строка.</returns>
        public static string Decode(string decode)
        {
            var data = Convert.FromBase64String(decode);
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ XOR_MASK);
            }
            return Encoding.UTF8.GetString(data);
        }
        #endregion
    }
    /**@}*/
}