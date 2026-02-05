using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Protection
{
    /// <summary>
    /// Тесты для критических участков Protection (проверка корректности шифрования/дешифрования).
    /// </summary>
    [TestFixture]
    public class ProtectionCriticalTests
    {
        /// <summary>
        /// Тест TProtectionInt - корректное шифрование и дешифрование.
        /// </summary>
        [Test]
        public void TProtectionInt_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = 12345;

            // Act
            var encrypted = protectedValue.EncryptedValue;
            int decrypted = protectedValue;

            // Assert
            ClassicAssert.AreNotEqual(12345, encrypted, "Encrypted value should be different from original");
            ClassicAssert.AreEqual(12345, decrypted, "Decrypted value should match original");
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с отрицательными значениями.
        /// </summary>
        [Test]
        public void TProtectionInt_WithNegativeValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = -100;

            // Act
            int decrypted = protectedValue;

            // Assert
            ClassicAssert.AreEqual(-100, decrypted, "Negative value should be preserved");
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с нулевым значением.
        /// </summary>
        [Test]
        public void TProtectionInt_WithZeroValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = 0;

            // Act
            int decrypted = protectedValue;

            // Assert
            ClassicAssert.AreEqual(0, decrypted, "Zero value should be preserved");
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с максимальным значением int.
        /// </summary>
        [Test]
        public void TProtectionInt_WithMaxIntValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = int.MaxValue;

            // Act
            int decrypted = protectedValue;

            // Assert
            ClassicAssert.AreEqual(int.MaxValue, decrypted, "Max int value should be preserved");
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с минимальным значением int.
        /// </summary>
        [Test]
        public void TProtectionInt_WithMinIntValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = int.MinValue;

            // Act
            int decrypted = protectedValue;

            // Assert
            ClassicAssert.AreEqual(int.MinValue, decrypted, "Min int value should be preserved");
        }

        /// <summary>
        /// Тест TProtectionLong - корректное шифрование и дешифрование.
        /// </summary>
        [Test]
        public void TProtectionLong_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionLong protectedValue = 123456789012345L;

            // Act
            long encrypted = protectedValue.EncryptedValue;
            long decrypted = protectedValue;

            // Assert
            ClassicAssert.AreNotEqual(123456789012345L, encrypted, "Encrypted value should be different from original");
            ClassicAssert.AreEqual(123456789012345L, decrypted, "Decrypted value should match original");
        }

        /// <summary>
        /// Тест TProtectionSingle - корректное шифрование и дешифрование.
        /// </summary>
        [Test]
        public void TProtectionSingle_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionSingle protectedValue = 123.45f;

            // Act
            float encrypted = protectedValue.EncryptedValue;
            float decrypted = protectedValue;

            // Assert
            ClassicAssert.AreNotEqual(123.45f, BitConverter.ToSingle(BitConverter.GetBytes(encrypted), 0), 
                "Encrypted value should be different from original");
            ClassicAssert.AreEqual(123.45f, decrypted, 0.01f, "Decrypted value should match original");
        }

        /// <summary>
        /// Тест TProtectionSingle - корректная работа с отрицательными значениями.
        /// </summary>
        [Test]
        public void TProtectionSingle_WithNegativeValue_WorksCorrectly()
        {
            // Arrange
            TProtectionSingle protectedValue = -123.45f;

            // Act
            float decrypted = protectedValue;

            // Assert
            ClassicAssert.AreEqual(-123.45f, decrypted, 0.01f, "Negative value should be preserved");
        }

        /// <summary>
        /// Тест TProtectionInt - множественные преобразования сохраняют значение.
        /// </summary>
        [Test]
        public void TProtectionInt_MultipleConversions_PreservesValue()
        {
            // Arrange
            TProtectionInt protectedValue = 999;

            // Act - множественные преобразования
            int value1 = protectedValue;
            int value2 = protectedValue;
            int value3 = protectedValue;

            // Assert
            ClassicAssert.AreEqual(999, value1, "First conversion should preserve value");
            ClassicAssert.AreEqual(999, value2, "Second conversion should preserve value");
            ClassicAssert.AreEqual(999, value3, "Third conversion should preserve value");
        }
    }
}
