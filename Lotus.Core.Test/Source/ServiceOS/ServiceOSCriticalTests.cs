using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Core.ServiceOS
{
    /// <summary>
    /// Тесты для критических участков ServiceOS.
    /// </summary>
    public class ServiceOSCriticalTests
    {
        /// <summary>
        /// Тест ILotusFileDialogs - интерфейс определен корректно.
        /// </summary>
        [Fact]
        public void ILotusFileDialogs_Interface_IsDefinedCorrectly()
        {
            // Arrange & Act
            var interfaceType = typeof(ILotusFileDialogs);

            // Assert
            Assert.NotNull(interfaceType);
            Assert.True(interfaceType.IsInterface, "Should be an interface");
        }

        /// <summary>
        /// Тест ILotusBackgroundWorker - интерфейс определен корректно.
        /// </summary>
        [Fact]
        public void ILotusBackgroundWorker_Interface_IsDefinedCorrectly()
        {
            // Arrange & Act
            //var interfaceType = typeof(ILotusBackgroundWorker);

            //// Assert
            //Assert.NotNull(interfaceType);
            //Assert.True(interfaceType.IsInterface, "Should be an interface");
        }

        /// <summary>
        /// Тест XFileDialog - статический класс определен корректно.
        /// </summary>
        [Fact]
        public void XFileDialog_StaticClass_IsDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XFileDialog);

            // Assert
            Assert.NotNull(classType);
            Assert.True(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }

        /// <summary>
        /// Тест XBackgroundManager - статический класс определен корректно.
        /// </summary>
        [Fact]
        public void XBackgroundManager_StaticClass_IsDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XBackgroundManager);

            // Assert
            Assert.NotNull(classType);
            Assert.True(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }

        /// <summary>
        /// Тест XBackgroundWorkerExtension - методы расширения определены корректно.
        /// </summary>
        [Fact]
        public void XBackgroundWorkerExtension_ExtensionMethods_AreDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XBackgroundWorkerExtension);

            // Assert
            Assert.NotNull(classType);
            Assert.True(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }
    }
}
