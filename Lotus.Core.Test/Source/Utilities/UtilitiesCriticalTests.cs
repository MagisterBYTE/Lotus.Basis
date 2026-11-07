using System;

namespace Lotus.Core.Utilities
{
    /// <summary>
    /// Тесты для критических участков Utilities.
    /// </summary>
    public class UtilitiesCriticalTests
    {
        /// <summary>
        /// Тест XValueSet - установка значения структуры.
        /// </summary>
        [Fact]
        public void XValueSet_SetStruct_ShouldReturnTrueWhenValueChanges()
        {
            // Arrange
            int currentValue = 10;
            int newValue = 20;

            // Act
            var result = XValueSet.SetStruct(ref currentValue, newValue);

            // Assert
            Assert.True(result, "Should return true when value changes");
            Assert.Equal(20, currentValue);
        }

        /// <summary>
        /// Тест XValueSet - установка значения структуры с тем же значением.
        /// </summary>
        [Fact]
        public void XValueSet_SetStruct_WithSameValue_ShouldReturnFalse()
        {
            // Arrange
            int currentValue = 10;
            int newValue = 10;

            // Act
            var result = XValueSet.SetStruct(ref currentValue, newValue);

            // Assert
            Assert.False(result, "Should return false when value doesn't change");
            Assert.Equal(10, currentValue);
        }

        /// <summary>
        /// Тест XValueSet - установка значения класса.
        /// </summary>
        [Fact]
        public void XValueSet_SetClass_ShouldReturnTrueWhenValueChanges()
        {
            // Arrange
            string? currentValue = "Old";
            string newValue = "New";

            // Act
            var result = XValueSet.SetClass(ref currentValue, newValue);

            // Assert
            Assert.True(result, "Should return true when value changes");
            Assert.Equal("New", currentValue);
        }

        /// <summary>
        /// Тест XValueSet - установка значения класса с null.
        /// </summary>
        [Fact]
        public void XValueSet_SetClass_WithNull_ShouldWorkCorrectly()
        {
            // Arrange
            string? currentValue = "Old";
            string? newValue = null;

            // Act
            var result = XValueSet.SetClass(ref currentValue, newValue);

            // Assert
            Assert.True(result, "Should return true when setting to null");
            Assert.Null(currentValue);
        }

        /// <summary>
        /// Тест XDisposer - базовое освобождение ресурсов.
        /// </summary>
        [Fact]
        public void XDisposer_SafeDispose_ShouldWorkCorrectly()
        {
            // Arrange
            var disposed = false;
            var disposable = new TestDisposable(() => disposed = true);
            TestDisposable? resource = disposable;

            // Act
            XDisposer.SafeDispose(ref resource);

            // Assert
            Assert.True(disposed, "Disposable should be disposed");
            Assert.Null(resource);
        }

        /// <summary>
        /// Тест XDisposer - освобождение null объекта не должно вызывать исключение.
        /// </summary>
        [Fact]
        public void XDisposer_SafeDisposeNull_ShouldNotThrow()
        {
            // Arrange
            TestDisposable? resource = null;

            // Act & Assert
            XDisposer.SafeDispose(ref resource);
            Assert.Null(resource);
        }

        /// <summary>
        /// Тест XPacked - базовые операции упаковки/распаковки int.
        /// </summary>
        [Fact]
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
            Assert.Equal(originalValue, unpacked);
        }

        /// <summary>
        /// Тест XPacked - упаковка/распаковка bool.
        /// </summary>
        [Fact]
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
            Assert.Equal(originalValue, unpacked);
        }

        /// <summary>
        /// Тест XPacked - упаковка/распаковка long.
        /// </summary>
        [Fact]
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
            Assert.Equal(originalValue, unpacked);
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
