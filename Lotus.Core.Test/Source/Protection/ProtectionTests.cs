#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace Lotus.Core.Protection
{
    /// <summary>
    /// Статический класс для тестирования методов защиты модуля базового ядра.
    /// </summary>
    public static class ProtectionTests
    {
        /// <summary>
        /// Тестирование методов защиты.
        /// </summary>
        [Fact]
        public static void Encrypted_Decrypted()
        {
            TProtectionInt protectInd = 6566;
            var encryptedValue = protectInd.EncryptedValue;
            int decryptedValue = protectInd;
            Assert.Equal(6566, decryptedValue);
        }
    }
}