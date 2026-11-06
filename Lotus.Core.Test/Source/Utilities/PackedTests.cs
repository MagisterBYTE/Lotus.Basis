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

            var pack0 = 0;
            XPacked.PackInteger(ref pack0, 0, 28, uid);

            var unPack0 = XPacked.UnpackInteger(pack0, 0, 28);

            ClassicAssert.AreEqual(unPack0, uid);

            //
            // Запись 15 бит с 4 бита
            //
            var pack4 = 0;
            uid = 25000;
            XPacked.PackInteger(ref pack4, 4, 15, uid);

            var unPack4 = XPacked.UnpackInteger(pack4, 4, 15);

            ClassicAssert.AreEqual(unPack4, uid);

            //
            // Запись 27 бит с 4 бита
            //
            var pack27 = 0;
            uid = 25000022;
            XPacked.PackInteger(ref pack27, 4, 27, uid);

            var unPack27 = XPacked.UnpackInteger(pack27, 4, 27);

            ClassicAssert.AreEqual(unPack27, uid);


            //
            // Запись 4 бит с 20 бита
            //
            var pack420 = 0;
            uid = 12;
            XPacked.PackInteger(ref pack420, 4, 27, uid);

            var unPack420 = XPacked.UnpackInteger(pack420, 4, 27);

            ClassicAssert.AreEqual(unPack420, uid);
        }

        [Test]
        public void PackedLong()
        {
            //
            // Запись 28 бит с начала
            //
            long uid = 3153600;

            long pack0 = 0;
            XPacked.PackLong(ref pack0, 0, 28, uid);

            var unPack0 = XPacked.UnpackLong(pack0, 0, 28);

            ClassicAssert.AreEqual(unPack0, uid);

            //
            // Запись 15 бит с 4 бита
            //
            long pack4 = 0;
            uid = 25000;
            XPacked.PackLong(ref pack4, 4, 15, uid);

            var unPack4 = XPacked.UnpackLong(pack4, 4, 15);

            ClassicAssert.AreEqual(unPack4, uid);

            //
            // Запись 27 бит с 4 бита
            //
            long pack27 = 0;
            uid = 25000022;
            XPacked.PackLong(ref pack27, 4, 27, uid);

            var unPack27 = XPacked.UnpackLong(pack27, 4, 27);

            ClassicAssert.AreEqual(unPack27, uid);


            //
            // Запись 4 бит с 20 бита
            //
            long pack420 = 0;
            uid = 12;
            XPacked.PackLong(ref pack420, 20, 4, uid);

            var unPack420 = XPacked.UnpackLong(pack420, 20, 4);

            ClassicAssert.AreEqual(unPack420, uid);

            //
            // Запись 24 бит с 0 бита
            //
            long pack240 = 0;
            uid = 16777213;
            XPacked.PackLong(ref pack240, 0, 24, uid);

            var unPack240 = XPacked.UnpackLong(pack240, 0, 24);

            ClassicAssert.AreEqual(unPack240, uid);

            //
            // Запись 16 бит с 40 бита
            //
            long pack1640 = 0;
            uid = 1677;
            XPacked.PackLong(ref pack1640, 40, 16, uid);

            var unPack1640 = XPacked.UnpackLong(pack1640, 40, 16);

            ClassicAssert.AreEqual(unPack1640, uid);

            //
            // Запись 24 бит с 40 бита
            //
            long pack2440 = 0;
            uid = 16770044;
            XPacked.PackLong(ref pack2440, 40, 24, uid);

            var unPack2440 = XPacked.UnpackLong(pack2440, 40, 24);

            ClassicAssert.AreEqual(unPack2440, uid);

            //
            // Запись 40 бит с 24 бита
            //
            long pack4024 = 0;
            uid = 31536000000L;
            XPacked.PackLong(ref pack4024, 24, 40, uid);

            var unPack4024 = XPacked.UnpackLong(pack4024, 24, 40);

            ClassicAssert.AreEqual(unPack4024, uid);

            //
            // Упаковка
            //
            var hashCode = 3231545;
            var dateTime = 31536000000L;

            long packUid = 0;
            XPacked.PackLong(ref packUid, 0, 24, hashCode);
            XPacked.PackLong(ref packUid, 24, 40, dateTime);

            var unHashCode = (int)XPacked.UnpackLong(packUid, 0, 24);
            ClassicAssert.AreEqual(unHashCode, hashCode);

            var unDateTime = XPacked.UnpackLong(packUid, 24, 40);
            ClassicAssert.AreEqual(unDateTime, dateTime);
        }

        #endregion
    }
}