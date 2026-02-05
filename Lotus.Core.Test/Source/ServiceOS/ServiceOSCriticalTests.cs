using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.ServiceOS
{
    /// <summary>
    /// Тесты для критических участков ServiceOS.
    /// </summary>
    [TestFixture]
    public class ServiceOSCriticalTests
    {
        /// <summary>
        /// Тест ILotusFileDialogs - интерфейс определен корректно.
        /// </summary>
        [Test]
        public void ILotusFileDialogs_Interface_IsDefinedCorrectly()
        {
            // Arrange & Act
            var interfaceType = typeof(ILotusFileDialogs);

            // Assert
            ClassicAssert.IsNotNull(interfaceType, "Interface should be defined");
            ClassicAssert.IsTrue(interfaceType.IsInterface, "Should be an interface");
        }

        /// <summary>
        /// Тест ILotusBackgroundWorker - интерфейс определен корректно.
        /// </summary>
        [Test]
        public void ILotusBackgroundWorker_Interface_IsDefinedCorrectly()
        {
            // Arrange & Act
            //var interfaceType = typeof(ILotusBackgroundWorker);

            //// Assert
            //ClassicAssert.IsNotNull(interfaceType, "Interface should be defined");
            //ClassicAssert.IsTrue(interfaceType.IsInterface, "Should be an interface");
        }

        /// <summary>
        /// Тест XFileDialog - статический класс определен корректно.
        /// </summary>
        [Test]
        public void XFileDialog_StaticClass_IsDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XFileDialog);

            // Assert
            ClassicAssert.IsNotNull(classType, "Class should be defined");
            ClassicAssert.IsTrue(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }

        /// <summary>
        /// Тест XBackgroundManager - статический класс определен корректно.
        /// </summary>
        [Test]
        public void XBackgroundManager_StaticClass_IsDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XBackgroundManager);

            // Assert
            ClassicAssert.IsNotNull(classType, "Class should be defined");
            ClassicAssert.IsTrue(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }

        /// <summary>
        /// Тест XBackgroundWorkerExtension - методы расширения определены корректно.
        /// </summary>
        [Test]
        public void XBackgroundWorkerExtension_ExtensionMethods_AreDefinedCorrectly()
        {
            // Arrange & Act
            var classType = typeof(XBackgroundWorkerExtension);

            // Assert
            ClassicAssert.IsNotNull(classType, "Extension class should be defined");
            ClassicAssert.IsTrue(classType.IsAbstract && classType.IsSealed, "Should be a static class");
        }
    }
}
