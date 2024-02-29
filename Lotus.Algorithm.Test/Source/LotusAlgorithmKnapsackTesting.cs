using System;
using System.Collections.Generic;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Algorithm
{
    [TestFixture]
    public class KnapsackSolverTests
    {
        [Test]
        public void Knapsack_WithValidInput_ShouldReturnFilledKnapsackDictionary()
        {
            // Arrange
            var set = new Dictionary<string, float>
            {
                { "A", 2.5f },
                { "B", 3.0f },
                { "C", 1.5f }
            };
            var capacity = 4.0f;

            // Act
            var result = XAlgorithmKnapsack.Knapsack(set, capacity);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(3, result.Count);
            ClassicAssert.AreEqual(1, result["A"]);
            ClassicAssert.AreEqual(0, result["B"]);
            ClassicAssert.AreEqual(1, result["C"]);
        }

        [Test]
        public void Knapsack_WithEmptyInput_ShouldReturnEmptyKnapsackDictionary()
        {
            // Arrange
            var set = new Dictionary<string, float>();
            var capacity = 5.0f;

            // Act
            var result = XAlgorithmKnapsack.Knapsack(set, capacity);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(0, result.Count);
        }

        [Test]
        public void Knapsack_WithNullKnapsack_ShouldCalculateKnapsackCorrectly()
        {
            // Arrange
            var set = new Dictionary<string, float>
            {
                { "A", 2.0f },
                { "B", 2.5f }
            };
            var capacity = 3.0f;

            // Act
            var result = XAlgorithmKnapsack.Knapsack(set, capacity, null);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.AreEqual(1, result["A"]);
            ClassicAssert.AreEqual(0, result["B"]);
        }
    }
}