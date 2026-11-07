using System;

namespace Lotus.Core.Message
{
    /// <summary>
    /// Тесты для критических участков Message (исправление отсутствующего присваивания _id).
    /// </summary>
    public class MessageCriticalTests
    {
        /// <summary>
        /// Тест CMessageArgs конструктор - исправленное присваивание _id = id.
        /// </summary>
        [Fact]
        public void CMessageArgs_Constructor_WithNameIdSender_SetsIdCorrectly()
        {
            // Arrange
            var testId = 12345;
            var testName = "TestMessage";
            var testSender = new object();

            // Act - используем конструктор где был исправлен баг (добавлено _id = id)
            var messageArgs = new CMessageArgs(testName, testId, testSender, false);

            // Assert - проверяем что _id был корректно присвоен
            Assert.Equal(testId, messageArgs.Id);
            Assert.Equal(testName, messageArgs.Name);
            Assert.Equal(testSender, messageArgs.Sender);
        }

        /// <summary>
        /// Тест CMessageArgs конструктор - все конструкторы корректно устанавливают Id.
        /// </summary>
        [Fact]
        public void CMessageArgs_Constructors_SetIdCorrectly()
        {
            // Test constructor with id only
            var message1 = new CMessageArgs(100, false);
            Assert.Equal(100, message1.Id);

            // Test constructor with name and id
            var message2 = new CMessageArgs("Test", 200, false);
            Assert.Equal(200, message2.Id);

            // Test constructor with name, id, and sender (критический - был исправлен)
            var message3 = new CMessageArgs("Test", 300, new object(), false);
            Assert.Equal(300, message3.Id);

            // Test constructor with id and sender
            var message4 = new CMessageArgs(400, new object(), false);
            Assert.Equal(400, message4.Id);
        }

        /// <summary>
        /// Тест CMessageArgs - корректная работа с пулом объектов.
        /// </summary>
        [Fact]
        public void CMessageArgs_WithPoolObject_ResetsCorrectly()
        {
            // Arrange
            var message = new CMessageArgs("Test", 123, new object(), true);
            Assert.Equal(123, message.Id);
            Assert.Equal("Test", message.Name);
            Assert.NotNull(message.Sender);

            // Act
            message.OnPoolRelease();

            // Assert - проверяем что поля сброшены
            Assert.Equal(0, message.Id);
            Assert.Equal("", message.Name);
            Assert.Null(message.Sender);
        }

        /// <summary>
        /// Тест CMessageArgs - корректное преобразование в строку.
        /// </summary>
        [Fact]
        public void CMessageArgs_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var message = new CMessageArgs("TestMessage", 123, new object(), false);
            message.Data = "TestData";

            // Act
            var result = message.ToString();

            // Assert
            Assert.True(result.Contains("TestMessage"), "ToString should contain message name");
            Assert.True(result.Contains("TestData"), "ToString should contain message data");
        }
    }
}
