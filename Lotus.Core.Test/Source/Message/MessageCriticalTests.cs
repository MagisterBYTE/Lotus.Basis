using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Message
{
    /// <summary>
    /// Тесты для критических участков Message (исправление отсутствующего присваивания _id).
    /// </summary>
    [TestFixture]
    public class MessageCriticalTests
    {
        /// <summary>
        /// Тест CMessageArgs конструктор - исправленное присваивание _id = id.
        /// </summary>
        [Test]
        public void CMessageArgs_Constructor_WithNameIdSender_SetsIdCorrectly()
        {
            // Arrange
            var testId = 12345;
            var testName = "TestMessage";
            var testSender = new object();

            // Act - используем конструктор где был исправлен баг (добавлено _id = id)
            var messageArgs = new CMessageArgs(testName, testId, testSender, false);

            // Assert - проверяем что _id был корректно присвоен
            ClassicAssert.AreEqual(testId, messageArgs.Id, "Id should be set correctly");
            ClassicAssert.AreEqual(testName, messageArgs.Name, "Name should be set correctly");
            ClassicAssert.AreEqual(testSender, messageArgs.Sender, "Sender should be set correctly");
        }

        /// <summary>
        /// Тест CMessageArgs конструктор - все конструкторы корректно устанавливают Id.
        /// </summary>
        [Test]
        public void CMessageArgs_Constructors_SetIdCorrectly()
        {
            // Test constructor with id only
            var message1 = new CMessageArgs(100, false);
            ClassicAssert.AreEqual(100, message1.Id, "Constructor with id should set Id");

            // Test constructor with name and id
            var message2 = new CMessageArgs("Test", 200, false);
            ClassicAssert.AreEqual(200, message2.Id, "Constructor with name and id should set Id");

            // Test constructor with name, id, and sender (критический - был исправлен)
            var message3 = new CMessageArgs("Test", 300, new object(), false);
            ClassicAssert.AreEqual(300, message3.Id, "Constructor with name, id, and sender should set Id");

            // Test constructor with id and sender
            var message4 = new CMessageArgs(400, new object(), false);
            ClassicAssert.AreEqual(400, message4.Id, "Constructor with id and sender should set Id");
        }

        /// <summary>
        /// Тест CMessageArgs - корректная работа с пулом объектов.
        /// </summary>
        [Test]
        public void CMessageArgs_WithPoolObject_ResetsCorrectly()
        {
            // Arrange
            var message = new CMessageArgs("Test", 123, new object(), true);
            ClassicAssert.AreEqual(123, message.Id);
            ClassicAssert.AreEqual("Test", message.Name);
            ClassicAssert.IsNotNull(message.Sender);

            // Act
            message.OnPoolRelease();

            // Assert - проверяем что поля сброшены
            ClassicAssert.AreEqual(0, message.Id, "Id should be reset to 0");
            ClassicAssert.AreEqual("", message.Name, "Name should be reset to empty string");
            ClassicAssert.IsNull(message.Sender, "Sender should be reset to null");
        }

        /// <summary>
        /// Тест CMessageArgs - корректное преобразование в строку.
        /// </summary>
        [Test]
        public void CMessageArgs_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var message = new CMessageArgs("TestMessage", 123, new object(), false);
            message.Data = "TestData";

            // Act
            var result = message.ToString();

            // Assert
            ClassicAssert.IsTrue(result.Contains("TestMessage"), "ToString should contain message name");
            ClassicAssert.IsTrue(result.Contains("TestData"), "ToString should contain message data");
        }
    }
}
