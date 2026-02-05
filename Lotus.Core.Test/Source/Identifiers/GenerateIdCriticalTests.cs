using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Identifiers
{
    /// <summary>
    /// Тесты для критических участков GenerateId (исправление опечатки elapsed_millisecond).
    /// </summary>
    [TestFixture]
    public class GenerateIdCriticalTests
    {
        /// <summary>
        /// Тест GenerateNext - исправленная опечатка (было elapsed_millsecond).
        /// </summary>
        [Test]
        public void GenerateNext_ReturnsCorrectMillisecondValue()
        {
            // Arrange
            var beforeCall = DateTime.UtcNow;

            // Act
            var result = XGenerateId.GenerateNext();
            var afterCall = DateTime.UtcNow;

            // Assert - проверяем что значение корректно вычисляется
            var expectedMin = (int)((beforeCall - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / 100000);
            var expectedMax = (int)((afterCall - new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks / 100000);

            ClassicAssert.GreaterOrEqual(result, expectedMin, "Result should be >= minimum expected value");
            ClassicAssert.LessOrEqual(result, expectedMax, "Result should be <= maximum expected value");
        }

        /// <summary>
        /// Тест GenerateNext - последовательные вызовы возвращают возрастающие значения.
        /// </summary>
        [Test]
        public void GenerateNext_SequentialCalls_ReturnIncreasingValues()
        {
            // Arrange & Act
            var id1 = XGenerateId.GenerateNext();
            System.Threading.Thread.Sleep(1); // Небольшая задержка для гарантии разных значений
            var id2 = XGenerateId.GenerateNext();

            // Assert
            ClassicAssert.GreaterOrEqual(id2, id1, "Second call should return value >= first call");
        }

        /// <summary>
        /// Тест Generate - корректная работа с elapsed_millisecond (исправленная опечатка).
        /// </summary>
        [Test]
        public void Generate_UsesCorrectMillisecondCalculation()
        {
            // Arrange
            var testObject = new object();
            var beforeGeneration = DateTime.UtcNow;

            // Act
            var id = XGenerateId.Generate(testObject);
            var afterGeneration = DateTime.UtcNow;

            // Assert - проверяем что дата распаковывается корректно
            var unpackedDate = XGenerateId.UnpackIdToDateTime(id);

            ClassicAssert.GreaterOrEqual(unpackedDate, beforeGeneration.AddMilliseconds(-10), 
                "Unpacked date should be close to generation time");
            ClassicAssert.LessOrEqual(unpackedDate, afterGeneration.AddMilliseconds(10), 
                "Unpacked date should be close to generation time");
        }

        /// <summary>
        /// Тест Generate - корректная работа с null объектом.
        /// </summary>
        [Test]
        public void Generate_WithNull_ReturnsNegativeOne()
        {
            // Arrange
            object? nullObject = null;

            // Act
            var result = XGenerateId.Generate(nullObject!);

            // Assert
            ClassicAssert.AreEqual(-1, result, "Null object should return -1");
        }

        /// <summary>
        /// Тест UnpackIdToDateTime - корректная распаковка даты из идентификатора.
        /// </summary>
        [Test]
        public void UnpackIdToDateTime_UnpacksCorrectDateTime()
        {
            // Arrange
            var testObject = new object();
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedDate = XGenerateId.UnpackIdToDateTime(id);

            // Assert
            ClassicAssert.GreaterOrEqual(unpackedDate, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                "Unpacked date should be >= start date");
            ClassicAssert.LessOrEqual(unpackedDate, DateTime.UtcNow.AddMinutes(1),
                "Unpacked date should be <= current time");
        }

        /// <summary>
        /// Тест UnpackIdToHashCode - корректная распаковка хеш-кода.
        /// </summary>
        [Test]
        public void UnpackIdToHashCode_UnpacksCorrectHashCode()
        {
            // Arrange
            var testObject = new object();
            var expectedHash = testObject.GetHashCode() / 16 * 16; // Округление до кратного 16
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedHash = XGenerateId.UnpackIdToHashCode(id);

            // Assert
            ClassicAssert.AreEqual(expectedHash, unpackedHash, 
                "Unpacked hash code should match original (rounded to multiple of 16)");
        }
    }
}
