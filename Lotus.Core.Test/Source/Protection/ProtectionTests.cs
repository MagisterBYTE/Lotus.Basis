#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
        [Test]
        public static void Encrypted_Decrypted()
        {
            TProtectionInt protectInd = 6566;
            var encryptedValue = protectInd.EncryptedValue;
            int decryptedValue = protectInd;
            ClassicAssert.AreEqual(6566, decryptedValue);
        }
    }
}