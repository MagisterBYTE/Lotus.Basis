#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using NUnit.Framework;

namespace Lotus.Core.Interfaces
{
    public static class InterfacesTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class TestA
        {

        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class TestB : TestA, ILotusDuplicate<TestA>
        {
            public TestA Duplicate(CParameters parameters = null)
            {
                return new TestB();
            }
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class TestC : TestB
        {
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class TestOther : ILotusDuplicate<TestA>
        {
            TestA test;

            public TestA Duplicate(CParameters parameters = null)
            {
                if (test == null)
                {
                    test = new TestA();
                }

                return test;
            }
        }

        /// <summary>
        /// Тестирование методов <see cref="ILotusDuplicate{TType}"/>.
        /// </summary>
        [Test]
        public static void TestInterfaceDuplicate()
        {
            TestA result = null;

            var testB = new TestB();
            var testC = new TestC();
            var testOther = new TestOther();

            result = testB.Duplicate();
            result = testC.Duplicate();
            result = testOther.Duplicate();
        }
    }
}