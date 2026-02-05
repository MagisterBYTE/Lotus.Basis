using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Parameters
{
    /// <summary>
    /// Тесты для критических участков Parameters (исправление имен переменных в SaveToJson).
    /// </summary>
    [TestFixture]
    public class ParametersCriticalTests
    {
        /// <summary>
        /// Тест SaveToJson - корректная работа с исправленными именами переменных (fileStream, streamWriter).
        /// </summary>
        [Test]
        public void SaveToJson_WithValidParameters_SavesToFileCorrectly()
        {
            // Arrange
            var parameters = new CParameters("TestParams",
                new CParameterString("Name", "TestValue"),
                new CParameterInteger("Count", 42));

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");

            try
            {
                // Act - используем метод с исправленными именами переменных
                parameters.SaveToJson(tempFile, true);

                // Assert - проверяем что файл создан и содержит данные
                ClassicAssert.IsTrue(File.Exists(tempFile), "File should be created");

                var content = File.ReadAllText(tempFile);
                ClassicAssert.IsTrue(content.Contains("TestParams"), "File should contain parameter name");
                ClassicAssert.IsTrue(content.Contains("TestValue"), "File should contain parameter value");
                ClassicAssert.IsTrue(content.Contains("42"), "File should contain integer value");
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
        /// Тест SaveToJson - корректное закрытие потоков (исправленные имена переменных).
        /// </summary>
        [Test]
        public void SaveToJson_ClosesStreamsCorrectly()
        {
            // Arrange
            var parameters = new CParameters("Test");
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");

            try
            {
                // Act
                parameters.SaveToJson(tempFile, true);

                // Assert - проверяем что файл доступен для чтения (потоки закрыты)
                using (var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
                {
                    ClassicAssert.IsTrue(fileStream.CanRead, "File should be readable after SaveToJson");
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

        /// <summary>
        /// Тест SaveToJson - корректная работа с UTF-8 кодировкой.
        /// </summary>
        [Test]
        public void SaveToJson_WithUtf8Encoding_SavesCorrectly()
        {
            // Arrange
            var parameters = new CParameters("TestParams",
                new CParameterString("Russian", "Привет"),
                new CParameterString("Chinese", "你好"));

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");

            try
            {
                // Act
                parameters.SaveToJson(tempFile, true);

                // Assert - проверяем что UTF-8 символы сохранены корректно
                var content = File.ReadAllText(tempFile, System.Text.Encoding.UTF8);
                ClassicAssert.IsTrue(content.Contains("Привет"), "File should contain Russian text");
                ClassicAssert.IsTrue(content.Contains("你好"), "File should contain Chinese text");
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
        /// Тест SaveToJson - корректная работа с вложенными параметрами.
        /// </summary>
        [Test]
        public void SaveToJson_WithNestedParameters_SavesCorrectly()
        {
            // Arrange
            var nestedParams = new CParameters("Nested",
                new CParameterString("Inner", "Value"));
            
            var parameters = new CParameters("Root",
                new CParameterString("Name", "Test"),
                new CParameterObject("Nested", nestedParams));

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");

            try
            {
                // Act
                parameters.SaveToJson(tempFile, true);

                // Assert
                ClassicAssert.IsTrue(File.Exists(tempFile), "File should be created");
                var content = File.ReadAllText(tempFile);
                ClassicAssert.IsTrue(content.Contains("Root"), "File should contain root parameter");
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
