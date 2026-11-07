#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace Lotus.Core.Extensions
{
    /// <summary>
    /// Статический класс для тестирования методов расширений модуля базового ядра.
    /// </summary>
    public static class ExtensionTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public interface IA
        {
            void TestA();
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public interface IB
        {
            void TestB();
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class A : IA
        {
            public void TestA()
            {
            }
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class B : A, IB
        {
            public void TestB()
            {
            }
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class C : B
        {
            public void TestC()
            {
            }
        }

        /// <summary>
        /// Тестирование методов <see cref="XReflectionTypeExtension"/>.
        /// </summary>
        [Fact]
        public static void TestExtensionReflection()
        {
            // Проверка типа на поддержку интерфейса
            Assert.Equal(typeof(A).IsSupportInterface<IA>(), true);
            Assert.Equal(typeof(A).IsSupportInterface<IB>(), false);

            Assert.Equal(typeof(B).IsSupportInterface<IA>(), true);
            Assert.Equal(typeof(B).IsSupportInterface<IB>(), true);

            // Проверка типа на базовый класс
            Assert.Equal(typeof(B).IsSubclassOf(typeof(object)), true);
            Assert.Equal(typeof(B).IsSubclassOf(typeof(A)), true);
            Assert.Equal(typeof(B).IsSubclassOf(typeof(B)), false);

            // Проверка на равенство
            Assert.Equal(typeof(B).IsAssignableFrom(typeof(A)), false);
            Assert.Equal(typeof(B).IsAssignableFrom(typeof(B)), true);
            Assert.Equal(typeof(B).IsAssignableFrom(typeof(C)), true);
        }
    }
}