using System.Runtime.InteropServices;

namespace Lotus.Core
{
    /** \addtogroup CoreProtection
	*@{*/
    /// <summary>
    /// Структура-оболочка для защиты целого числа (64 бит).
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TProtectionLong
    {
        #region Const
        /// <summary>
        /// Маска для шифрования/декодирование.
        /// </summary>
        public const ulong XOR_MASK = 0XAAAAAAAAAAAAAAAA;
        #endregion

        #region Fields
        [FieldOffset(0)]
        private long _encryptValue;

        [FieldOffset(0)]
        private ulong _convertValue;
        #endregion

        #region Properties
        /// <summary>
        /// Зашифрованное значение.
        /// </summary>
        public long EncryptedValue
        {
            get
            {
                // Обходное решение для конструктора структуры по умолчанию
                if (_convertValue == 0 && _encryptValue == 0)
                {
                    _convertValue = XOR_MASK;
                }

                return _encryptValue;
            }
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в обычное целое число.
        /// </summary>
        /// <param name="value">Структура-оболочка для защиты целого числа.</param>
        /// <returns>Целое число.</returns>
        public static implicit operator long(TProtectionLong value)
        {
            value._convertValue ^= XOR_MASK;
            var original = value._encryptValue;
            value._convertValue ^= XOR_MASK;
            return original;
        }

        /// <summary>
        /// Неявное преобразование в объект типа cтруктуры-оболочки для защиты целого числа.
        /// </summary>
        /// <param name="value">Целое число.</param>
        /// <returns>Структура-оболочка для защиты целого числа.</returns>
        public static implicit operator TProtectionLong(long value)
        {
            var protection = new TProtectionLong();
            protection._encryptValue = value;
            protection._convertValue ^= XOR_MASK;
            return protection;
        }
        #endregion
    }
    /**@}*/
}