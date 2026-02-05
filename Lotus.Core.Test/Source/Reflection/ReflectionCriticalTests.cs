using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Reflection
{
    /// <summary>
    /// Тесты для критических участков Reflection (исправление ArgList3[3] -> ArgList3[2]).
    /// </summary>
    [TestFixture]
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
        [Test]
        public void InvokeMethod_WithThreeArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act - используем метод с исправленным индексом ArgList3[2] вместо ArgList3[3]
            var result = reflectedType.InvokeMethod("InstanceMethod", instance, 2, 3, 4);

            // Assert - проверяем что метод вызван корректно (2 * 3 * 4 = 24)
            ClassicAssert.IsNotNull(result, "Result should not be null");
            ClassicAssert.AreEqual(24, result, "Method should be invoked with correct arguments");
        }

        /// <summary>
        /// Тест InvokeMethod с тремя аргументами для статического метода.
        /// </summary>
        [Test]
        public void InvokeMethod_StaticMethodWithThreeArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act
            var result = reflectedType.InvokeMethod("StaticMethod", null, 10, 20, 30);

            // Assert - проверяем что метод вызван корректно (10 + 20 + 30 = 60)
            ClassicAssert.IsNotNull(result, "Result should not be null");
            ClassicAssert.AreEqual(60, result, "Static method should be invoked with correct arguments");
        }

        /// <summary>
        /// Тест InvokeMethod с тремя строковыми аргументами.
        /// </summary>
        [Test]
        public void InvokeMethod_WithThreeStringArguments_UsesCorrectIndex()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act
            var result = reflectedType.InvokeMethod("MethodWithStrings", instance, "A", "B", "C");

            // Assert
            ClassicAssert.IsNotNull(result, "Result should not be null");
            ClassicAssert.AreEqual("A-B-C", result, "Method should concatenate strings correctly");
        }

        /// <summary>
        /// Тест InvokeMethod с тремя аргументами - проверка что все аргументы передаются корректно.
        /// </summary>
        [Test]
        public void InvokeMethod_WithThreeArguments_PassesAllArgumentsCorrectly()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act - проверяем что все три аргумента передаются в правильном порядке
            var result1 = reflectedType.InvokeMethod("InstanceMethod", instance, 1, 2, 3);
            var result2 = reflectedType.InvokeMethod("InstanceMethod", instance, 5, 6, 7);

            // Assert
            ClassicAssert.AreEqual(6, result1, "First call: 1 * 2 * 3 = 6");
            ClassicAssert.AreEqual(210, result2, "Second call: 5 * 6 * 7 = 210");
        }

        /// <summary>
        /// Тест InvokeMethod с несуществующим методом.
        /// </summary>
        [Test]
        public void InvokeMethod_WithNonExistentMethod_ReturnsNull()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));
            var instance = new TestClass();

            // Act
            var result = reflectedType.InvokeMethod("NonExistentMethod", instance, 1, 2, 3);

            // Assert
            ClassicAssert.IsNull(result, "Non-existent method should return null");
        }

        /// <summary>
        /// Тест GetField - корректная работа с исправленными именами переменных (fieldInfo вместо field_info).
        /// </summary>
        [Test]
        public void GetField_WithValidFieldName_ReturnsFieldInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var fieldInfo = reflectedType.GetField("FieldValue");

            // Assert
            ClassicAssert.IsNotNull(fieldInfo, "FieldInfo should not be null");
            ClassicAssert.AreEqual("FieldValue", fieldInfo.Name, "Field name should match");
        }

        /// <summary>
        /// Тест GetProperty - корректная работа с исправленными именами переменных (propertyInfo вместо property_info).
        /// </summary>
        [Test]
        public void GetProperty_WithValidPropertyName_ReturnsPropertyInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var propertyInfo = reflectedType.GetProperty("Value");

            // Assert
            ClassicAssert.IsNotNull(propertyInfo, "PropertyInfo should not be null");
            ClassicAssert.AreEqual("Value", propertyInfo.Name, "Property name should match");
        }

        /// <summary>
        /// Тест GetMethod - корректная работа с исправленными именами переменных (methodInfo вместо method_info).
        /// </summary>
        [Test]
        public void GetMethod_WithValidMethodName_ReturnsMethodInfo()
        {
            // Arrange
            var reflectedType = new CReflectedType(typeof(TestClass));

            // Act - используем метод с исправленным именем переменной
            var methodInfo = reflectedType.GetMethod("InstanceMethod");

            // Assert
            ClassicAssert.IsNotNull(methodInfo, "MethodInfo should not be null");
            ClassicAssert.AreEqual("InstanceMethod", methodInfo.Name, "Method name should match");
        }
    }
}
