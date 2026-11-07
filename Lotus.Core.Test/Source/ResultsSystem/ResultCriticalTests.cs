using System;
using System.Net;

namespace Lotus.Core.ResultsSystem
{
    /// <summary>
    /// Тесты для критических участков ResultsSystem.
    /// </summary>
    public class ResultCriticalTests
    {
        /// <summary>
        /// Тест Result - корректная работа с успешным результатом.
        /// </summary>
        [Fact]
        public void Result_Succeed_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result.Succeed("Operation completed", 200, "Data");

            // Assert
            Assert.True(result.Succeeded, "Result should be successful");
            Assert.Equal("Operation completed", result.Message);
            Assert.Equal(200, result.Code);
            Assert.Equal("Data", result.Value);
        }

        /// <summary>
        /// Тест Result - корректная работа с неуспешным результатом.
        /// </summary>
        [Fact]
        public void Result_Failed_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result.Failed("Operation failed", 500, "Error");

            // Assert
            Assert.False(result.Succeeded, "Result should be failed");
            Assert.Equal("Operation failed", result.Message);
            Assert.Equal(500, result.Code);
            Assert.Equal("Error", result.Value);
        }

        /// <summary>
        /// Тест Result - корректная работа с HttpStatusCode.
        /// </summary>
        [Fact]
        public void Result_WithHttpStatusCode_SetsCorrectCode()
        {
            // Arrange & Act
            var result = Result.Failed(HttpStatusCode.BadRequest, "Bad request", 400, null);

            // Assert
            Assert.False(result.Succeeded, "Result should be failed");
            Assert.Equal(HttpStatusCode.BadRequest, result.HttpCode);
            Assert.Equal("Bad request", result.Message);
            Assert.Equal(400, result.Code);
        }

        /// <summary>
        /// Тест Result - корректная работа предопределенных результатов.
        /// </summary>
        [Fact]
        public void Result_PredefinedResults_WorkCorrectly()
        {
            // Assert
            Assert.True(Result.Ok.Succeeded, "Ok should be successful");
            Assert.False(Result.Error.Succeeded, "Error should be failed");
            Assert.Equal(HttpStatusCode.BadRequest, Result.BadRequest.HttpCode);
            Assert.Equal(HttpStatusCode.Conflict, Result.Conflict.HttpCode);
            Assert.Equal(HttpStatusCode.Unauthorized, Result.Unauthorized.HttpCode);
            Assert.Equal(HttpStatusCode.Forbidden, Result.Forbidden.HttpCode);
        }

        /// <summary>
        /// Тест Result&lt;TValue&gt; - корректная работа с типизированным значением.
        /// </summary>
        [Fact]
        public void ResultTValue_WithTypedValue_ReturnsCorrectResult()
        {
            // Arrange & Act
            var result = Result<int>.Succeed("Operation completed", 200, 42);

            // Assert
            Assert.True(result.Succeeded, "Result should be successful");
            Assert.Equal(42, result.Value);
            Assert.Equal("Operation completed", result.Message);
        }

        /// <summary>
        /// Тест Result - корректная работа Clone.
        /// </summary>
        [Fact]
        public void Result_Clone_CreatesCorrectCopy()
        {
            // Arrange
            var original = Result.Succeed("Test", 200, "Data");

            // Act
            var cloned = (Result)original.Clone();

            // Assert
            Assert.Equal(original.Succeeded, cloned.Succeeded);
            Assert.Equal(original.Message, cloned.Message);
            Assert.Equal(original.Code, cloned.Code);
            Assert.Equal(original.Value, cloned.Value);
        }

        /// <summary>
        /// Тест Result - корректное преобразование в строку.
        /// </summary>
        [Fact]
        public void Result_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var result = Result.Succeed("Test message", 200, null);

            // Act
            var resultString = result.ToString();

            // Assert
            Assert.True(resultString.Contains("OK: True"), "ToString should contain success status");
            Assert.True(resultString.Contains("Test message"), "ToString should contain message");
        }
    }
}
