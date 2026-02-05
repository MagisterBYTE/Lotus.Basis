using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Utilities
{
    /// <summary>
    /// Тесты для критических участков Utilities.
    /// </summary>
    [TestFixture]
    public class UtilitiesCriticalTests
    {
        /// <summary>
        /// Тест XValueSet - установка значения структуры.
        /// </summary>
        [Test]
        public void XValueSet_SetStruct_ShouldReturnTrueWhenValueChanges()
        {
            // Arrange
            int currentValue = 10;
            int newValue = 20;

            // Act
            var result = XValueSet.SetStruct(ref currentValue, newValue);

            // Assert
            ClassicAssert.IsTrue(result, "Should return true when value changes");
            ClassicAssert.AreEqual(20, currentValue, "Current value should be updated");
        }

        /// <summary>
        /// Тест XValueSet - установка значения структуры с тем же значением.
        /// </summary>
        [Test]
        public void XValueSet_SetStruct_WithSameValue_ShouldReturnFalse()
        {
            // Arrange
            int currentValue = 10;
            int newValue = 10;

            // Act
            var result = XValueSet.SetStruct(ref currentValue, newValue);

            // Assert
            ClassicAssert.IsFalse(result, "Should return false when value doesn't change");
            ClassicAssert.AreEqual(10, currentValue, "Current value should remain unchanged");
        }

        /// <summary>
        /// Тест XValueSet - установка значения класса.
        /// </summary>
        [Test]
        public void XValueSet_SetClass_ShouldReturnTrueWhenValueChanges()
        {
            // Arrange
            string? currentValue = "Old";
            string newValue = "New";

            // Act
            var result = XValueSet.SetClass(ref currentValue, newValue);

            // Assert
            ClassicAssert.IsTrue(result, "Should return true when value changes");
            ClassicAssert.AreEqual("New", currentValue, "Current value should be updated");
        }

        /// <summary>
        /// Тест XValueSet - установка значения класса с null.
        /// </summary>
        [Test]
        public void XValueSet_SetClass_WithNull_ShouldWorkCorrectly()
        {
            // Arrange
            string? currentValue = "Old";
            string? newValue = null;

            // Act
            var result = XValueSet.SetClass(ref currentValue, newValue);

            // Assert
            ClassicAssert.IsTrue(result, "Should return true when setting to null");
            ClassicAssert.IsNull(currentValue, "Current value should be null");
        }

        /// <summary>
        /// Тест XDisposer - базовое освобождение ресурсов.
        /// </summary>
        [Test]
        public void XDisposer_SafeDispose_ShouldWorkCorrectly()
        {
            // Arrange
            var disposed = false;
            var disposable = new TestDisposable(() => disposed = true);
            TestDisposable? resource = disposable;

            // Act
            XDisposer.SafeDispose(ref resource);

            // Assert
            ClassicAssert.IsTrue(disposed, "Disposable should be disposed");
            ClassicAssert.IsNull(resource, "Resource should be set to null");
        }

        /// <summary>
        /// Тест XDisposer - освобождение null объекта не должно вызывать исключение.
        /// </summary>
        [Test]
        public void XDisposer_SafeDisposeNull_ShouldNotThrow()
        {
            // Arrange
            TestDisposable? resource = null;

            // Act & Assert
            ClassicAssert.DoesNotThrow(() => XDisposer.SafeDispose(ref resource), "Disposing null should not throw");
            ClassicAssert.IsNull(resource, "Resource should remain null");
        }

        /// <summary>
        /// Тест XPacked - базовые операции упаковки/распаковки int.
        /// </summary>
        [Test]
        public void XPacked_PackInteger_ShouldWorkCorrectly()
        {
            // Arrange
            int pack = 0;
            int bitStart = 0;
            int bitCount = 8;
            int originalValue = 123;

            // Act
            XPacked.PackInteger(ref pack, bitStart, bitCount, originalValue);
            var unpacked = XPacked.UnpackInteger(pack, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked, "Unpacked value should match original");
        }

        /// <summary>
        /// Тест XPacked - упаковка/распаковка bool.
        /// </summary>
        [Test]
        public void XPacked_PackBoolean_ShouldWorkCorrectly()
        {
            // Arrange
            int pack = 0;
            int bitStart = 0;
            bool originalValue = true;

            // Act
            XPacked.PackBoolean(ref pack, bitStart, originalValue);
            var unpacked = XPacked.UnpackBoolean(pack, bitStart);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked, "Unpacked value should match original");
        }

        /// <summary>
        /// Тест XPacked - упаковка/распаковка long.
        /// </summary>
        [Test]
        public void XPacked_PackLong_ShouldWorkCorrectly()
        {
            // Arrange
            long pack = 0;
            int bitStart = 0;
            int bitCount = 32;
            long originalValue = 123456789L;

            // Act
            XPacked.PackLong(ref pack, bitStart, bitCount, originalValue);
            var unpacked = XPacked.UnpackLong(pack, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked, "Unpacked value should match original");
        }

        /// <summary>
        /// Служебный класс для тестирования IDisposable.
        /// </summary>
        private class TestDisposable : IDisposable
        {
            private readonly Action _onDispose;

            public TestDisposable(Action onDispose)
            {
                _onDispose = onDispose;
            }

            public void Dispose()
            {
                _onDispose?.Invoke();
            }
        }
    }
}
