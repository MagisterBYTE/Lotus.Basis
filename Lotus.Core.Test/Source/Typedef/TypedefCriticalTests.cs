using System;

namespace Lotus.Core.Typedef
{
    /// <summary>
    /// Тесты для критических участков Typedef (исправление логической ошибки в Equals).
    /// </summary>
    public class TypedefCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private class TestTypedefObject : TypedefObject<TestEnum>
        {
            public TestTypedefObject(TestEnum value)
            {
                Type = value;
            }
        }

        /// <summary>
        /// Тест TypedefObject - исправление логической ошибки в Equals (x == null должен возвращать y is null, а не true).
        /// </summary>
        [Fact]
        public void TypedefObject_Equals_WhenXIsNull_ShouldReturnYIsNull()
        {
            // Arrange
            TypedefObject<TestEnum>? x = null;
            var y = new TestTypedefObject(TestEnum.Value1);
            var comparer = new TestTypedefObject(TestEnum.Value1);

            // Act - используем метод с исправленной логикой (return y is null вместо return true)
            var result1 = comparer.Equals(x, y);
            var result2 = comparer.Equals(x, null);

            // Assert
            Assert.False(result1, "When x is null and y is not null, should return false (not true)");
            Assert.True(result2, "When both x and y are null, should return true");
        }

        /// <summary>
        /// Тест TypedefObject - Equals корректно работает когда оба объекта не null.
        /// </summary>
        [Fact]
        public void TypedefObject_Equals_WhenBothNotNull_ShouldCompareTypes()
        {
            // Arrange
            var obj1 = new TestTypedefObject(TestEnum.Value1);
            var obj2 = new TestTypedefObject(TestEnum.Value1);
            var obj3 = new TestTypedefObject(TestEnum.Value2);
            var comparer = new TestTypedefObject(TestEnum.Value1);

            // Act
            var result1 = comparer.Equals(obj1, obj2);
            var result2 = comparer.Equals(obj1, obj3);

            // Assert
            Assert.True(result1, "Objects with same Type should be equal");
            Assert.False(result2, "Objects with different Type should not be equal");
        }

        /// <summary>
        /// Тест TypedefObject - Equals корректно работает когда y is null.
        /// </summary>
        [Fact]
        public void TypedefObject_Equals_WhenYIsNull_ShouldReturnFalse()
        {
            // Arrange
            var x = new TestTypedefObject(TestEnum.Value1);
            TypedefObject<TestEnum>? y = null;
            var comparer = new TestTypedefObject(TestEnum.Value1);

            // Act
            var result = comparer.Equals(x, y);

            // Assert
            Assert.False(result, "When y is null and x is not null, should return false");
        }

        /// <summary>
        /// Тест TypedefObject - Equals корректно работает для прямого вызова.
        /// </summary>
        [Fact]
        public void TypedefObject_Equals_DirectCall_ShouldWorkCorrectly()
        {
            // Arrange
            var obj1 = new TestTypedefObject(TestEnum.Value1);
            var obj2 = new TestTypedefObject(TestEnum.Value1);
            var obj3 = new TestTypedefObject(TestEnum.Value2);

            // Act
            var result1 = obj1.Equals(obj2);
            var result2 = obj1.Equals(obj3);
            var result3 = obj1.Equals((TypedefObject<TestEnum>?)null);

            // Assert
            Assert.True(result1, "Objects with same Type should be equal");
            Assert.False(result2, "Objects with different Type should not be equal");
            Assert.False(result3, "Object should not be equal to null");
        }

        /// <summary>
        /// Тест TypedefObject - Equals с enum корректно работает.
        /// </summary>
        [Fact]
        public void TypedefObject_Equals_WithEnum_ShouldWorkCorrectly()
        {
            // Arrange
            var obj = new TestTypedefObject(TestEnum.Value1);

            // Act
            var result1 = obj.Equals(TestEnum.Value1);
            var result2 = obj.Equals(TestEnum.Value2);
            var result3 = obj.Equals((TestEnum?)null);

            // Assert
            Assert.True(result1, "Object should be equal to matching enum value");
            Assert.False(result2, "Object should not be equal to different enum value");
            Assert.False(result3, "Object should not be equal to null enum");
        }
    }
}
