#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
        [Test]
        public static void TestExtensionReflection()
        {
            // Проверка типа на поддержку интерфейса
            ClassicAssert.AreEqual(typeof(A).IsSupportInterface<IA>(), true);
            ClassicAssert.AreEqual(typeof(A).IsSupportInterface<IB>(), false);

            ClassicAssert.AreEqual(typeof(B).IsSupportInterface<IA>(), true);
            ClassicAssert.AreEqual(typeof(B).IsSupportInterface<IB>(), true);

            // Проверка типа на базовый класс
            ClassicAssert.AreEqual(typeof(B).IsSubclassOf(typeof(object)), true);
            ClassicAssert.AreEqual(typeof(B).IsSubclassOf(typeof(A)), true);
            ClassicAssert.AreEqual(typeof(B).IsSubclassOf(typeof(B)), false);

            // Проверка на равенство
            ClassicAssert.AreEqual(typeof(B).IsAssignableFrom(typeof(A)), false);
            ClassicAssert.AreEqual(typeof(B).IsAssignableFrom(typeof(B)), true);
            ClassicAssert.AreEqual(typeof(B).IsAssignableFrom(typeof(C)), true);
        }
    }
}