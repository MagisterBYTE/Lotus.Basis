using System.Runtime.InteropServices;

namespace Lotus.Core
{
    /** \addtogroup CoreProtection
	*@{*/
    /// <summary>
    /// Структура-оболочка для защиты вещественного числа.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TProtectionSingle
    {
        #region Const
        /// <summary>
        /// Маска для шифрования/декодирование.
        /// </summary>
        public const uint XOR_MASK = 0XAAAAAAAA;
        #endregion

        #region Fields
        [FieldOffset(0)]
        private float _encryptValue;

        [FieldOffset(0)]
        private uint _convertValue;
        #endregion

        #region Properties
        /// <summary>
        /// Зашифрованное значение.
        /// </summary>
        public float EncryptedValue
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
        /// Неявное преобразование в обычное вещественное число.
        /// </summary>
        /// <param name="value">Структура-оболочка для защиты вещественного числа.</param>
        /// <returns>Целое число.</returns>
        public static implicit operator float(TProtectionSingle value)
        {
            value._convertValue ^= XOR_MASK;
            var original = value._encryptValue;
            value._convertValue ^= XOR_MASK;
            return original;
        }

        /// <summary>
        /// Неявное преобразование в объект типа структуры-оболочки для защиты вещественного числа.
        /// </summary>
        /// <param name="value">Вещественное число.</param>
        /// <returns>Структура-оболочка для защиты вещественного числа.</returns>
        public static implicit operator TProtectionSingle(float value)
        {
            var protection = new TProtectionSingle();
            protection._encryptValue = value;
            protection._convertValue ^= XOR_MASK;
            return protection;
        }
        #endregion
    }
    /**@}*/
}