using System;


namespace Lotus.Core.Identifiers
{
    public class GenerateIdTests
    {
        [Fact]
        public void Identifier()
        {
            var test = "TestIdentifier";

            var uid = XGenerateId.Generate(test);

            var date = XGenerateId.UnpackIdToDateTime(uid);

            var oh = test.GetHashCode() / 16 * 16;
            var rh = XGenerateId.UnpackIdToHashCode(uid);

            Assert.Equal(rh, oh);
        }

        [Fact]
        public void Generate_WithNullObject_ShouldReturnNegativeOne()
        {
            // Arrange
            object nullObject = null;

            // Act
            var result = XGenerateId.Generate(nullObject);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Generate_WithNonNullObject_ShouldReturnPositiveId()
        {
            // Arrange
            var testObject = new object();

            // Act
            var result = XGenerateId.Generate(testObject);

            // Assert
            Assert.True(result > 0);
        }

        [Fact]
        public void Generate_ShouldProduceDifferentIdsForDifferentObjects()
        {
            // Arrange
            var obj1 = new object();
            var obj2 = new object();

            // Act
            var id1 = XGenerateId.Generate(obj1);
            var id2 = XGenerateId.Generate(obj2);

            // Assert
            Assert.NotEqual(id1, id2);
        }

        [Fact]
        public void GenerateNext_ShouldReturnIncreasingValues()
        {
            // Arrange & Act
            var id1 = XGenerateId.GenerateNext();
            var id2 = XGenerateId.GenerateNext();

            // Assert
            Assert.True(id2 >= id1);
        }

        [Fact]
        public void UnpackIdToHashCode_ShouldReturnOriginalHashCodeDividedBy16()
        {
            // Arrange
            var testObject = new object();
            var expectedHash = testObject.GetHashCode() / 16;
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedHash = XGenerateId.UnpackIdToHashCode(id);

            // Assert
            Assert.Equal(expectedHash, unpackedHash / 16);
        }

        [Fact]
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
            Assert.True(unpackedDate.Second >= beforeGeneration.Second);
            Assert.True(unpackedDate.Second <= afterGeneration.Second);
        }

        [Fact]
        public void UnpackIdToDateTime_ShouldReturnDateAfterStartDate()
        {
            // Arrange
            var testObject = new object();
            var id = XGenerateId.Generate(testObject);

            // Act
            var unpackedDate = XGenerateId.UnpackIdToDateTime(id);

            // Assert
            Assert.True(unpackedDate >= new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}