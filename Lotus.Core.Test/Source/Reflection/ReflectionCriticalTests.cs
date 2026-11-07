using System;
using System.Reflection;

namespace Lotus.Core.Reflection
{
    /// <summary>
    /// Тесты для критических участков Reflection (исправление ArgList3[3] -> ArgList3[2]).
    /// </summary>
    public class ReflectionCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private class TestClass
        {
            public int FieldValue;

            public int Value { get; set; }

            public static int StaticMethod(int a, int b, int c)
            {
                return a + b + c;
            }

            public int InstanceMethod(int a, int b, int c)
            {
                return a * b * c;
            }

            public string MethodWithStrings(string a, string b, string c)
            {
                return $"{a}-{b}-{c}";
            }
        }

        /// <summary>
        /// Тест InvokeMethod с тремя аргументами - критический баг был исправлен (ArgList3[3] -> ArgList3[2]).
        /// </summary>
        [Fact]
        public void InvokeMethod_WithThreeArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act - используем метод с исправленным индексом ArgList3[2] вместо ArgList3[3]
            var result = reflectedType.InvokeMethod("InstanceMethod", instance, 2, 3, 4);

            // Assert - проверяем что метод вызван корректно (2 * 3 * 4 = 24)
            Assert.NotNull(result);
            Assert.Equal(24, result);
        }

        /// <summary>
        /// Тест InvokeMethod с тремя аргументами для статического метода.
        /// </summary>
        [Fact]
        public void InvokeMethod_StaticMethodWithThreeArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act
            var result = reflectedType.InvokeMethod("StaticMethod", null, 10, 20, 30);

            // Assert - проверяем что метод вызван корректно (10 + 20 + 30 = 60)
            Assert.NotNull(result);
            Assert.Equal(60, result);
        }

        /// <summary>
        /// Тест InvokeMethod с тремя строковыми аргументами.
        /// </summary>
        [Fact]
        public void InvokeMethod_WithThreeStringArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act
            var result = reflectedType.InvokeMethod("MethodWithStrings", instance, "A", "B", "C");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("A-B-C", result);
        }

        /// <summary>
        /// Тест InvokeMethod с тремя аргументами - проверка что все аргументы передаются корректно.
        /// </summary>
        [Fact]
        public void InvokeMethod_WithThreeArguments_PassesAllArgumentsCorrectly()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act - проверяем что все три аргумента передаются в правильном порядке
            var result1 = reflectedType.InvokeMethod("InstanceMethod", instance, 1, 2, 3);
            var result2 = reflectedType.InvokeMethod("InstanceMethod", instance, 5, 6, 7);

            // Assert
            Assert.Equal(6, result1);
            Assert.Equal(210, result2);
        }

        /// <summary>
        /// Тест InvokeMethod с несуществующим методом.
        /// </summary>
        [Fact]
        public void InvokeMethod_WithNonExistentMethod_ReturnsNull()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act
            var result = reflectedType.InvokeMethod("NonExistentMethod", instance, 1, 2, 3);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Тест GetField - корректная работа с исправленными именами переменных (fieldInfo вместо fieldInfo).
        /// </summary>
        [Fact]
        public void GetField_WithValidFieldName_ReturnsFieldInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var fieldInfo = reflectedType.GetField("FieldValue");

            // Assert
            Assert.NotNull(fieldInfo);
            Assert.Equal("FieldValue", fieldInfo.Name);
        }

        /// <summary>
        /// Тест GetProperty - корректная работа с исправленными именами переменных (propertyInfo вместо propertyInfo).
        /// </summary>
        [Fact]
        public void GetProperty_WithValidPropertyName_ReturnsPropertyInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var propertyInfo = reflectedType.GetProperty("Value");

            // Assert
            Assert.NotNull(propertyInfo);
            Assert.Equal("Value", propertyInfo.Name);
        }

        /// <summary>
        /// Тест GetMethod - корректная работа с исправленными именами переменных (methodInfo вместо methodInfo).
        /// </summary>
        [Fact]
        public void GetMethod_WithValidMethodName_ReturnsMethodInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var methodInfo = reflectedType.GetMethod("InstanceMethod");

            // Assert
            Assert.NotNull(methodInfo);
            Assert.Equal("InstanceMethod", methodInfo.Name);
        }
    }
}
