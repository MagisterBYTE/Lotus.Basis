using System;
using System.Net;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.ResultsSystem
{
    /// <summary>
    /// Тесты для критических участков ResultsSystem.
    /// </summary>
    [TestFixture]
    public class ResultCriticalTests
    {
        /// <summary>
        /// Тест Result - корректная работа с успешным результатом.
        /// </summary>
        [Test]
        public void Result_Succeed_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result.Succeed("Operation completed", 200, "Data");

            // Assert
            ClassicAssert.IsTrue(result.Succeeded, "Result should be successful");
            ClassicAssert.AreEqual("Operation completed", result.Message, "Message should match");
            ClassicAssert.AreEqual(200, result.Code, "Code should match");
            ClassicAssert.AreEqual("Data", result.Value, "Value should match");
        }

        /// <summary>
        /// Тест Result - корректная работа с неуспешным результатом.
        /// </summary>
        [Test]
        public void Result_Failed_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result.Failed("Operation failed", 500, "Error");

            // Assert
            ClassicAssert.IsFalse(result.Succeeded, "Result should be failed");
            ClassicAssert.AreEqual("Operation failed", result.Message, "Message should match");
            ClassicAssert.AreEqual(500, result.Code, "Code should match");
            ClassicAssert.AreEqual("Error", result.Value, "Value should match");
        }

        /// <summary>
        /// Тест Result - корректная работа с HttpStatusCode.
        /// </summary>
        [Test]
        public void Result_WithHttpStatusCode_SetsCorrectCode()
        {
            // Arrange & Act
            var result = Result.Failed(HttpStatusCode.BadRequest, "Bad request", 400, null);

            // Assert
            ClassicAssert.IsFalse(result.Succeeded, "Result should be failed");
            ClassicAssert.AreEqual(HttpStatusCode.BadRequest, result.HttpCode, "HttpCode should match");
            ClassicAssert.AreEqual("Bad request", result.Message, "Message should match");
            ClassicAssert.AreEqual(400, result.Code, "Code should match");
        }

        /// <summary>
        /// Тест Result - корректная работа предопределенных результатов.
        /// </summary>
        [Test]
        public void Result_PredefinedResults_WorkCorrectly()
        {
            // Assert
            ClassicAssert.IsTrue(Result.Ok.Succeeded, "Ok should be successful");
            ClassicAssert.IsFalse(Result.Error.Succeeded, "Error should be failed");
            ClassicAssert.AreEqual(HttpStatusCode.BadRequest, Result.BadRequest.HttpCode, "BadRequest should have correct HttpCode");
            ClassicAssert.AreEqual(HttpStatusCode.Conflict, Result.Conflict.HttpCode, "Conflict should have correct HttpCode");
            ClassicAssert.AreEqual(HttpStatusCode.Unauthorized, Result.Unauthorized.HttpCode, "Unauthorized should have correct HttpCode");
            ClassicAssert.AreEqual(HttpStatusCode.Forbidden, Result.Forbidden.HttpCode, "Forbidden should have correct HttpCode");
        }

        /// <summary>
        /// Тест Result&lt;TValue&gt; - корректная работа с типизированным значением.
        /// </summary>
        [Test]
        public void ResultTValue_WithTypedValue_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result<int>.Succeed("Operation completed", 200, 42);

            // Assert
            ClassicAssert.IsTrue(result.Succeeded, "Result should be successful");
            ClassicAssert.AreEqual(42, result.Value, "Typed value should match");
            ClassicAssert.AreEqual("Operation completed", result.Message, "Message should match");
        }

        /// <summary>
        /// Тест Result - корректная работа Clone.
        /// </summary>
        [Test]
        public void Result_Clone_CreatesCorrectCopy()
        {
            // Arrange
            var original = Result.Succeed("Test", 200, "Data");

            // Act
            var cloned = (Result)original.Clone();

            // Assert
            ClassicAssert.AreEqual(original.Succeeded, cloned.Succeeded, "Succeeded should match");
            ClassicAssert.AreEqual(original.Message, cloned.Message, "Message should match");
            ClassicAssert.AreEqual(original.Code, cloned.Code, "Code should match");
            ClassicAssert.AreEqual(original.Value, cloned.Value, "Value should match");
        }

        /// <summary>
        /// Тест Result - корректное преобразование в строку.
        /// </summary>
        [Test]
        public void Result_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var result = Result.Succeed("Test message", 200, null);

            // Act
            var resultString = result.ToString();

            // Assert
            ClassicAssert.IsTrue(resultString.Contains("OK: True"), "ToString should contain success status");
            ClassicAssert.IsTrue(resultString.Contains("Test message"), "ToString should contain message");
        }
    }
}
