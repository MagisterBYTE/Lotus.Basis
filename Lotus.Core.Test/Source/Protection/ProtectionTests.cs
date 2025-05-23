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
            TProtectionInt protect_ind = 6566;
            var encrypted_value = protect_ind.EncryptedValue;
            int decrypted_value = protect_ind;
            ClassicAssert.AreEqual(6566, decrypted_value);
        }
    }
}