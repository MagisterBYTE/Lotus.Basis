using System;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс для тестирования базовой подсистемы модуля базового ядра.
    /// </summary>
    public static class XCoreBaseTesting
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

        /// <summary>
        /// Тестирование методов <see cref="XPacked"/>.
        /// </summary>
        [Test]
        public static void TestPackedInteger()
        {
            //
            // Запись 28 бит с начала
            //
            var uid = 3153600;

            var pack_0 = 0;
            XPacked.PackInteger(ref pack_0, 0, 28, uid);

            var un_pack_0 = XPacked.UnpackInteger(pack_0, 0, 28);

            ClassicAssert.AreEqual(un_pack_0, uid);

            //
            // Запись 15 бит с 4 бита
            //
            var pack_4 = 0;
            uid = 25000;
            XPacked.PackInteger(ref pack_4, 4, 15, uid);

            var un_pack_4 = XPacked.UnpackInteger(pack_4, 4, 15);

            ClassicAssert.AreEqual(un_pack_4, uid);

            //
            // Запись 27 бит с 4 бита
            //
            var pack_27 = 0;
            uid = 25000022;
            XPacked.PackInteger(ref pack_27, 4, 27, uid);

            var un_pack_27 = XPacked.UnpackInteger(pack_27, 4, 27);

            ClassicAssert.AreEqual(un_pack_27, uid);


            //
            // Запись 4 бит с 20 бита
            //
            var pack_4_20 = 0;
            uid = 12;
            XPacked.PackInteger(ref pack_4_20, 4, 27, uid);

            var un_pack_4_20 = XPacked.UnpackInteger(pack_4_20, 4, 27);

            ClassicAssert.AreEqual(un_pack_4_20, uid);
        }

        /// <summary>
        /// Тестирование методов <see cref="XPacked"/>.
        /// </summary>
        [Test]
        public static void TestPackedLong()
        {
            //
            // Запись 28 бит с начала
            //
            long uid = 3153600;

            long pack_0 = 0;
            XPacked.PackLong(ref pack_0, 0, 28, uid);

            var un_pack_0 = XPacked.UnpackLong(pack_0, 0, 28);

            ClassicAssert.AreEqual(un_pack_0, uid);

            //
            // Запись 15 бит с 4 бита
            //
            long pack_4 = 0;
            uid = 25000;
            XPacked.PackLong(ref pack_4, 4, 15, uid);

            var un_pack_4 = XPacked.UnpackLong(pack_4, 4, 15);

            ClassicAssert.AreEqual(un_pack_4, uid);

            //
            // Запись 27 бит с 4 бита
            //
            long pack_27 = 0;
            uid = 25000022;
            XPacked.PackLong(ref pack_27, 4, 27, uid);

            var un_pack_27 = XPacked.UnpackLong(pack_27, 4, 27);

            ClassicAssert.AreEqual(un_pack_27, uid);


            //
            // Запись 4 бит с 20 бита
            //
            long pack_4_20 = 0;
            uid = 12;
            XPacked.PackLong(ref pack_4_20, 20, 4, uid);

            var un_pack_4_20 = XPacked.UnpackLong(pack_4_20, 20, 4);

            ClassicAssert.AreEqual(un_pack_4_20, uid);

            //
            // Запись 24 бит с 0 бита
            //
            long pack_24_0 = 0;
            uid = 16777213;
            XPacked.PackLong(ref pack_24_0, 0, 24, uid);

            var un_pack_24_0 = XPacked.UnpackLong(pack_24_0, 0, 24);

            ClassicAssert.AreEqual(un_pack_24_0, uid);

            //
            // Запись 16 бит с 40 бита
            //
            long pack_16_40 = 0;
            uid = 1677;
            XPacked.PackLong(ref pack_16_40, 40, 16, uid);

            var un_pack_16_40 = XPacked.UnpackLong(pack_16_40, 40, 16);

            ClassicAssert.AreEqual(un_pack_16_40, uid);

            //
            // Запись 24 бит с 40 бита
            //
            long pack_24_40 = 0;
            uid = 16770044;
            XPacked.PackLong(ref pack_24_40, 40, 24, uid);

            var un_pack_24_40 = XPacked.UnpackLong(pack_24_40, 40, 24);

            ClassicAssert.AreEqual(un_pack_24_40, uid);

            //
            // Запись 40 бит с 24 бита
            //
            long pack_40_24 = 0;
            uid = 31536000000L;
            XPacked.PackLong(ref pack_40_24, 24, 40, uid);

            var un_pack_40_24 = XPacked.UnpackLong(pack_40_24, 24, 40);

            ClassicAssert.AreEqual(un_pack_40_24, uid);

            //
            // Упаковка
            //
            var hash_code = 3231545;
            var date_time = 31536000000L;

            long pack_uid = 0;
            XPacked.PackLong(ref pack_uid, 0, 24, hash_code);
            XPacked.PackLong(ref pack_uid, 24, 40, date_time);

            var un_hash_code = (int)XPacked.UnpackLong(pack_uid, 0, 24);
            ClassicAssert.AreEqual(un_hash_code, hash_code);

            var un_date_time = XPacked.UnpackLong(pack_uid, 24, 40);
            ClassicAssert.AreEqual(un_date_time, date_time);
        }

        /// <summary>
        /// Тестирование методов <see cref="XGenerateId"/>.
        /// </summary>
        [Test]
        public static void TestIdentifier()
        {
            var test = "TestIdentifier";

            var uid = XGenerateId.Generate(test);

            var date = XGenerateId.UnpackIdToDateTime(uid);

            var oh = test.GetHashCode() / 16 * 16;
            var rh = XGenerateId.UnpackIdToHashCode(uid);

            ClassicAssert.AreEqual(rh, oh);

        }
    }
}