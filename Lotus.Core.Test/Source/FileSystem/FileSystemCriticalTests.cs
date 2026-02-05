using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.FileSystem
{
    /// <summary>
    /// Тесты для критических участков FileSystem (исправление RecursiveFileSystemInfo).
    /// </summary>
    [TestFixture]
    public class FileSystemCriticalTests
    {
        /// <summary>
        /// Тест RecursiveFileSystemInfo - корректное имя метода (было исправлено с RecursiveFileSysteInfo).
        /// </summary>
        [Test]
        public void RecursiveFileSystemInfo_WithValidPath_WorksCorrectly()
        {
            // Arrange - создаем временную директорию
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var tempDir = new DirectoryInfo(tempPath);
            tempDir.Create();

            try
            {
                // Создаем поддиректорию и файл
                var subDir = tempDir.CreateSubdirectory("SubDir");
                var testFile = new FileInfo(Path.Combine(tempPath, "test.txt"));
                File.WriteAllText(testFile.FullName, "test");

                // Act - используем исправленный метод RecursiveFileSystemInfo
                var directory = new CFileSystemDirectory(tempDir);
                directory.RecursiveFileSystemInfo();

                // Assert - проверяем что метод выполнился без ошибок
                ClassicAssert.IsTrue(directory.Entities.Count > 0, "Directory should contain entities");
            }
            finally
            {
                // Cleanup
                if (tempDir.Exists)
                {
                    tempDir.Delete(true);
                }
            }
        }

        /// <summary>
        /// Тест RecursiveFileSystemInfo - корректная обработка пустой директории.
        /// </summary>
        [Test]
        public void RecursiveFileSystemInfo_WithEmptyDirectory_WorksCorrectly()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var tempDir = new DirectoryInfo(tempPath);
            tempDir.Create();

            try
            {
                // Act
                var directory = new CFileSystemDirectory(tempDir);
                directory.RecursiveFileSystemInfo();

                // Assert
                ClassicAssert.IsNotNull(directory.Entities);
                // Пустая директория может содержать 0 элементов или только саму себя
            }
            finally
            {
                if (tempDir.Exists)
                {
                    tempDir.Delete(true);
                }
            }
        }

        /// <summary>
        /// Тест RecursiveFileSystemInfo - корректная обработка несуществующей директории.
        /// </summary>
        [Test]
        public void RecursiveFileSystemInfo_WithNonExistentDirectory_HandlesGracefully()
        {
            // Arrange
            var nonExistentPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var nonExistentDir = new DirectoryInfo(nonExistentPath);

            // Act & Assert - должно обработать без исключения или выбросить ожидаемое исключение
            var directory = new CFileSystemDirectory(nonExistentDir);
            
            // Метод может выбросить исключение для несуществующей директории, что нормально
            ClassicAssert.Throws<DirectoryNotFoundException>(() =>
            {
                directory.RecursiveFileSystemInfo();
            });
        }

        /// <summary>
        /// Тест RecursiveFileSystemInfoTwoLevel - корректное имя метода.
        /// </summary>
        [Test]
        public void RecursiveFileSystemInfoTwoLevel_WithValidPath_WorksCorrectly()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var tempDir = new DirectoryInfo(tempPath);
            tempDir.Create();

            try
            {
                // Создаем структуру директорий
                var subDir1 = tempDir.CreateSubdirectory("Level1");
                var subDir2 = subDir1.CreateSubdirectory("Level2");
                File.WriteAllText(Path.Combine(subDir2.FullName, "test.txt"), "test");

                // Act
                var directory = new CFileSystemDirectory(tempDir);
                directory.RecursiveFileSystemInfoTwoLevel();

                // Assert
                ClassicAssert.IsTrue(directory.Entities.Count > 0, "Directory should contain entities");
            }
            finally
            {
                if (tempDir.Exists)
                {
                    tempDir.Delete(true);
                }
            }
        }
    }
}
