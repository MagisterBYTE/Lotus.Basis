using System;
using System.IO;

namespace Lotus.Core.Logger
{
    /// <summary>
    /// Тесты для критических участков Logger (исправление имен переменных).
    /// </summary>
    public class LoggerCriticalTests : IDisposable
    {
        public void Dispose()
        {
            XLogger.Messages.Clear();
        }

        /// <summary>
        /// Тест SaveToText - корректная работа с исправленными именами переменных (fileStream, streamWriter).
        /// </summary>
        [Fact]
        public void SaveToText_WithMessages_SavesToFileCorrectly()
        {
            // Arrange
            var testMessage1 = new LogMessage("Test message 1", TLogType.Info);
            var testMessage2 = new LogMessage("Test message 2", TLogType.Warning);
            XLogger.Messages.Add(testMessage1);
            XLogger.Messages.Add(testMessage2);

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            try
            {
                // Act - используем метод с исправленными именами переменных
                XLogger.SaveToText(tempFile);

                // Assert - проверяем что файл создан и содержит сообщения
                Assert.True(File.Exists(tempFile), "File should be created");

                var content = File.ReadAllText(tempFile);
                Assert.True(content.Contains("Test message 1"), "File should contain first message");
                Assert.True(content.Contains("Test message 2"), "File should contain second message");
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        /// <summary>
        /// Тест SaveToText - корректная обработка пустого списка сообщений.
        /// </summary>
        [Fact]
        public void SaveToText_WithEmptyMessages_CreatesEmptyFile()
        {
            // Arrange
            XLogger.Messages.Clear();
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            try
            {
                // Act
                XLogger.SaveToText(tempFile);

                // Assert
                Assert.True(File.Exists(tempFile), "File should be created even with empty messages");
                var content = File.ReadAllText(tempFile);
                Assert.Empty(content);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        /// <summary>
        /// Тест SaveToText - корректное закрытие потоков (исправленные имена переменных).
        /// </summary>
        [Fact]
        public void SaveToText_ClosesStreamsCorrectly()
        {
            // Arrange
            var testMessage = new LogMessage("Test message", TLogType.Info);
            XLogger.Messages.Add(testMessage);

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            try
            {
                // Act
                XLogger.SaveToText(tempFile);

                // Assert - проверяем что файл доступен для чтения (потоки закрыты)
                // Если потоки не закрыты, это может вызвать проблемы
                using (var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
                {
                    Assert.True(fileStream.CanRead, "File should be readable after SaveToText");
                }
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
    }
}
