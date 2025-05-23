using System;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Identifiers
{
    [TestFixture]
    public class GenerateIdTests
    {
        [Test]
        public void Identifier()
        {
            var test = "TestIdentifier";

            var uid = XGenerateId.Generate(test);

            var date = XGenerateId.UnpackIdToDateTime(uid);

            var oh = test.GetHashCode() / 16 * 16;
            var rh = XGenerateId.UnpackIdToHashCode(uid);

            ClassicAssert.AreEqual(rh, oh);
        }

        [Test]
        public void Generate_WithNullObject_ShouldReturnNegativeOne()
        {
            // Arrange
            object nullObject = null;

            // Act
            var result = XGenerateId.Generate(nullObject);

            // Assert
            ClassicAssert.AreEqual(-1, result);
        }

        [Test]
        public void Generate_WithNonNullObject_ShouldReturnPositiveId()
        {
            // Arrange
            var testObject = new object();

            // Act
            var result = XGenerateId.Generate(testObject);

            // Assert
            ClassicAssert.Greater(result, 0);
        }

        [Test]
        public void Generate_ShouldProduceDifferentIdsForDifferentObjects()
        {
            // Arrange
            var obj1 = new object();
            var obj2 = new object();

            // Act
            var id1 = XGenerateId.Generate(obj1);
            var id2 = XGenerateId.Generate(obj2);

            // Assert
            ClassicAssert.AreNotEqual(id1, id2);
        }

        [Test]
        public void GenerateNext_ShouldReturnIncreasingValues()
        {
            // Arrange & Act
            var id1 = XGenerateId.GenerateNext();
            var id2 = XGenerateId.GenerateNext();

            // Assert
            ClassicAssert.GreaterOrEqual(id2, id1);
        }

        [Test]
        public void UnpackIdToHashCode_ShouldReturnOriginalHashCodeDividedBy16()
        {
            // Arrange
            var testObject = new object();
            var expectedHash = testObject.GetHashCode() / 16;
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedHash = XGenerateId.UnpackIdToHashCode(id);

            // Assert
            ClassicAssert.AreEqual(expectedHash, unpackedHash / 16);
        }

        [Test]
        public void UnpackIdToDateTime_ShouldReturnRecentDateTime()
        {
            // Arrange
            var testObject = new object();

            var beforeGeneration = DateTime.UtcNow;
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedDate = XGenerateId.UnpackIdToDateTime(id);
            var afterGeneration = DateTime.UtcNow;

            // Assert
            ClassicAssert.GreaterOrEqual(unpackedDate.Second, beforeGeneration.Second);
            ClassicAssert.LessOrEqual(unpackedDate.Second, afterGeneration.Second);
        }

        [Test]
        public void UnpackIdToDateTime_ShouldReturnDateAfterStartDate()
        {
            // Arrange
            var testObject = new object();
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedDate = XGenerateId.UnpackIdToDateTime(id);

            // Assert
            ClassicAssert.GreaterOrEqual(unpackedDate, new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}