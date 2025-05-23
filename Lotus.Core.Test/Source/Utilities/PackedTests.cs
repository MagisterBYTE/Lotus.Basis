using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Utilities
{
    [TestFixture]
    public class PackedTests
    {
        #region Pack/Unpack Integer Tests

        [Test]
        public void PackInteger_ShouldPackValueCorrectly()
        {
            // Arrange
            int packed = 0;
            int valueToPack = 0b1010; // 10 in decimal
            int bitStart = 4;
            int bitCount = 4;

            // Act
            XPacked.PackInteger(ref packed, bitStart, bitCount, valueToPack);

            // Assert
            ClassicAssert.AreEqual(0b10100000, packed);
        }

        [Test]
        public void UnpackInteger_ShouldUnpackValueCorrectly()
        {
            // Arrange
            int packed = 0b10100000; // 160 in decimal
            int bitStart = 4;
            int bitCount = 4;

            // Act
            var unpacked = XPacked.UnpackInteger(packed, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(0b1010, unpacked); // 10 in decimal
        }

        [Test]
        public void PackAndUnpackInteger_ShouldBeReversible()
        {
            // Arrange
            int originalValue = 123;
            int packed = 0;
            int bitStart = 8;
            int bitCount = 10;

            // Act
            XPacked.PackInteger(ref packed, bitStart, bitCount, originalValue);
            var unpacked = XPacked.UnpackInteger(packed, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked);
        }

        #endregion

        #region Pack/Unpack Long Tests

        [Test]
        public void PackLong_ShouldPackValueCorrectly()
        {
            // Arrange
            long packed = 0;
            long valueToPack = 0b1010; // 10 in decimal
            int bitStart = 4;
            int bitCount = 4;

            // Act
            XPacked.PackLong(ref packed, bitStart, bitCount, valueToPack);

            // Assert
            ClassicAssert.AreEqual(0b10100000, packed);
        }

        [Test]
        public void UnpackLong_ShouldUnpackValueCorrectly()
        {
            // Arrange
            long packed = 0b10100000; // 160 in decimal
            int bitStart = 4;
            int bitCount = 4;

            // Act
            var unpacked = XPacked.UnpackLong(packed, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(0b1010, unpacked); // 10 in decimal
        }

        [Test]
        public void PackAndUnpackLong_ShouldBeReversible()
        {
            // Arrange
            long originalValue = 123456789;
            long packed = 0;
            int bitStart = 16;
            int bitCount = 32;

            // Act
            XPacked.PackLong(ref packed, bitStart, bitCount, originalValue);
            var unpacked = XPacked.UnpackLong(packed, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked);
        }

        #endregion

        #region Pack/Unpack Boolean Tests

        [Test]
        public void PackBoolean_ShouldPackTrueCorrectly()
        {
            // Arrange
            int packed = 0;
            int bitStart = 5;

            // Act
            XPacked.PackBoolean(ref packed, bitStart, true);

            // Assert
            ClassicAssert.AreEqual(0b00100000, packed);
        }

        [Test]
        public void PackBoolean_ShouldPackFalseCorrectly()
        {
            // Arrange
            int packed = 0b11111111;
            int bitStart = 3;

            // Act
            XPacked.PackBoolean(ref packed, bitStart, false);

            // Assert
            ClassicAssert.AreEqual(0b11110111, packed);
        }

        [Test]
        public void UnpackBoolean_ShouldUnpackTrueCorrectly()
        {
            // Arrange
            int packed = 0b00001000;
            int bitStart = 3;

            // Act
            var unpacked = XPacked.UnpackBoolean(packed, bitStart);

            // Assert
            ClassicAssert.IsTrue(unpacked);
        }

        [Test]
        public void UnpackBoolean_ShouldUnpackFalseCorrectly()
        {
            // Arrange
            int packed = 0b11110111;
            int bitStart = 3;

            // Act
            var unpacked = XPacked.UnpackBoolean(packed, bitStart);

            // Assert
            ClassicAssert.IsFalse(unpacked);
        }

        [Test]
        public void PackAndUnpackBoolean_ShouldBeReversible()
        {
            // Arrange
            bool originalValue = true;
            int packed = 0;
            int bitStart = 7;

            // Act
            XPacked.PackBoolean(ref packed, bitStart, originalValue);
            var unpacked = XPacked.UnpackBoolean(packed, bitStart);

            // Assert
            ClassicAssert.AreEqual(originalValue, unpacked);
        }

        #endregion

        #region Edge Cases Tests

        [Test]
        public void PackInteger_WithZeroBitCount_ShouldNotModifyPack()
        {
            // Arrange
            int packed = 0xFFFFFFF;
            int valueToPack = 0xAAAAAAA;
            int bitStart = 8;
            int bitCount = 0;

            // Act
            XPacked.PackInteger(ref packed, bitStart, bitCount, valueToPack);

            // Assert
            ClassicAssert.AreEqual(0xFFFFFFF, packed);
        }

        [Test]
        public void UnpackInteger_WithZeroBitCount_ShouldReturnZero()
        {
            // Arrange
            int packed = 0xFFFFFFF;
            int bitStart = 8;
            int bitCount = 0;

            // Act
            var unpacked = XPacked.UnpackInteger(packed, bitStart, bitCount);

            // Assert
            ClassicAssert.AreEqual(0, unpacked);
        }

        [Test]
        public void PackLong_WithBitCount64_ShouldHandleFullRange()
        {
            // Arrange
            long packed = 0;
            long valueToPack = long.MaxValue / 2;
            int bitStart = 0;
            int bitCount = 63;

            // Act
            XPacked.PackLong(ref packed, bitStart, bitCount, valueToPack);

            // Assert
            ClassicAssert.AreEqual(long.MaxValue / 2, packed);
        }

        [Test]
        public void PackedInteger()
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

        [Test]
        public void PackedLong()
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

        #endregion
    }
}