using System;

namespace Lotus.Core.Protection
{
    /// <summary>
    /// Тесты для критических участков Protection (проверка корректности шифрования/дешифрования).
    /// </summary>
    public class ProtectionCriticalTests
    {
        /// <summary>
        /// Тест TProtectionInt - корректное шифрование и дешифрование.
        /// </summary>
        [Fact]
        public void TProtectionInt_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = 12345;

            // Act
            var encrypted = protectedValue.EncryptedValue;
            int decrypted = protectedValue;

            // Assert
            Assert.NotEqual(12345, encrypted);
            Assert.Equal(12345, decrypted);
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с отрицательными значениями.
        /// </summary>
        [Fact]
        public void TProtectionInt_WithNegativeValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = -100;

            // Act
            int decrypted = protectedValue;

            // Assert
            Assert.Equal(-100, decrypted);
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с нулевым значением.
        /// </summary>
        [Fact]
        public void TProtectionInt_WithZeroValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = 0;

            // Act
            int decrypted = protectedValue;

            // Assert
            Assert.Equal(0, decrypted);
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с максимальным значением int.
        /// </summary>
        [Fact]
        public void TProtectionInt_WithMaxIntValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = int.MaxValue;

            // Act
            int decrypted = protectedValue;

            // Assert
            Assert.Equal(int.MaxValue, decrypted);
        }

        /// <summary>
        /// Тест TProtectionInt - корректная работа с минимальным значением int.
        /// </summary>
        [Fact]
        public void TProtectionInt_WithMinIntValue_WorksCorrectly()
        {
            // Arrange
            TProtectionInt protectedValue = int.MinValue;

            // Act
            int decrypted = protectedValue;

            // Assert
            Assert.Equal(int.MinValue, decrypted);
        }

        /// <summary>
        /// Тест TProtectionLong - корректное шифрование и дешифрование.
        /// </summary>
        [Fact]
        public void TProtectionLong_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionLong protectedValue = 123456789012345L;

            // Act
            long encrypted = protectedValue.EncryptedValue;
            long decrypted = protectedValue;

            // Assert
            Assert.NotEqual(123456789012345L, encrypted);
            Assert.Equal(123456789012345L, decrypted);
        }

        /// <summary>
        /// Тест TProtectionSingle - корректное шифрование и дешифрование.
        /// </summary>
        [Fact]
        public void TProtectionSingle_EncryptionDecryption_WorksCorrectly()
        {
            // Arrange
            TProtectionSingle protectedValue = 123.45f;

            // Act
            float encrypted = protectedValue.EncryptedValue;
            float decrypted = protectedValue;

            // Assert
            Assert.NotEqual(123.45f, BitConverter.ToSingle(BitConverter.GetBytes(encrypted), 0));
            Assert.Equal(123.45f, decrypted, 0.01f);
        }

        /// <summary>
        /// Тест TProtectionSingle - корректная работа с отрицательными значениями.
        /// </summary>
        [Fact]
        public void TProtectionSingle_WithNegativeValue_WorksCorrectly()
        {
            // Arrange
            TProtectionSingle protectedValue = -123.45f;

            // Act
            float decrypted = protectedValue;

            // Assert
            Assert.Equal(-123.45f, decrypted, 0.01f);
        }

        /// <summary>
        /// Тест TProtectionInt - множественные преобразования сохраняют значение.
        /// </summary>
        [Fact]
        public void TProtectionInt_MultipleConversions_PreservesValue()
        {
            // Arrange
            TProtectionInt protectedValue = 999;

            // Act - множественные преобразования
            int value1 = protectedValue;
            int value2 = protectedValue;
            int value3 = protectedValue;

            // Assert
            Assert.Equal(999, value1);
            Assert.Equal(999, value2);
            Assert.Equal(999, value3);
        }
    }
}
