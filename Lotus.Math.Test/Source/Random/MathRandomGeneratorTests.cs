using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathRandomGeneratorTests
    {
        private const float EpsilonF = 0.00001f;
        private const int TestIterations = 1000;

        #region RandomStandard Tests
        [Test]
        public void RandomStandard_Constructor_WithSeed_InitializesCorrectly()
        {
            var rng = new RandomStandard(12345);
            ClassicAssert.IsNotNull(rng);
        }

        [Test]
        public void RandomStandard_NextSingle_ReturnsValueInRange()
        {
            var rng = new RandomStandard(12345);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle();
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.Less(value, 1.0f);
            }
        }

        [Test]
        public void RandomStandard_NextSingle_WithMax_ReturnsValueInRange()
        {
            var rng = new RandomStandard(12345);
            var max = 10.0f;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle(max);
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.LessOrEqual(value, max);
            }
        }

        [Test]
        public void RandomStandard_NextSingle_WithMinMax_ReturnsValueInRange()
        {
            var rng = new RandomStandard(12345);
            var min = 5.0f;
            var max = 15.0f;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle(min, max);
                ClassicAssert.GreaterOrEqual(value, min);
                ClassicAssert.LessOrEqual(value, max);
            }
        }

        [Test]
        public void RandomStandard_NextInteger_ReturnsValue()
        {
            var rng = new RandomStandard(12345);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger();
                ClassicAssert.GreaterOrEqual(value, 0u);
            }
        }

        [Test]
        public void RandomStandard_NextInteger_WithMax_ReturnsValueInRange()
        {
            var rng = new RandomStandard(12345);
            uint max = 100;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger(max);
                ClassicAssert.GreaterOrEqual(value, 0u);
                ClassicAssert.Less(value, max);
            }
        }

        [Test]
        public void RandomStandard_NextInteger_WithMinMax_ReturnsValueInRange()
        {
            var rng = new RandomStandard(12345);
            uint min = 10;
            uint max = 100;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger(min, max);
                ClassicAssert.GreaterOrEqual(value, min);
                ClassicAssert.Less(value, max);
            }
        }
        #endregion

        #region RandomMersenneTwister Tests
        [Test]
        public void RandomMersenneTwister_Constructor_WithSeed_InitializesCorrectly()
        {
            var rng = new RandomMersenneTwister(12345u);
            ClassicAssert.IsNotNull(rng);
        }

        [Test]
        public void RandomMersenneTwister_NextSingle_ReturnsValueInRange()
        {
            var rng = new RandomMersenneTwister(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle();
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.LessOrEqual(value, 1.0f);
            }
        }

        [Test]
        public void RandomMersenneTwister_NextSingle_WithMax_ReturnsValueInRange()
        {
            var rng = new RandomMersenneTwister(12345u);
            var max = 10.0f;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle(max);
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.LessOrEqual(value, max);
            }
        }

        [Test]
        public void RandomMersenneTwister_NextInteger_ReturnsValue()
        {
            var rng = new RandomMersenneTwister(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger();
                ClassicAssert.GreaterOrEqual(value, 0u);
            }
        }

        [Test]
        public void RandomMersenneTwister_WithSameSeed_ProducesSameSequence()
        {
            var rng1 = new RandomMersenneTwister(12345u);
            var rng2 = new RandomMersenneTwister(12345u);
            for (int i = 0; i < 100; i++)
            {
                ClassicAssert.AreEqual(rng1.NextInteger(), rng2.NextInteger());
            }
        }
        #endregion

        #region RandomXorShift Tests
        [Test]
        public void RandomXorShift_Constructor_WithSeed_InitializesCorrectly()
        {
            var rng = new RandomXorShift(12345u);
            ClassicAssert.IsNotNull(rng);
        }

        [Test]
        public void RandomXorShift_NextSingle_ReturnsValueInRange()
        {
            var rng = new RandomXorShift(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle();
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.Less(value, 1.0f);
            }
        }

        [Test]
        public void RandomXorShift_NextSingle_WithMax_ReturnsValueInRange()
        {
            var rng = new RandomXorShift(12345u);
            var max = 10.0f;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle(max);
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.LessOrEqual(value, max);
            }
        }

        [Test]
        public void RandomXorShift_NextInteger_ReturnsValue()
        {
            var rng = new RandomXorShift(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger();
                ClassicAssert.GreaterOrEqual(value, 0u);
            }
        }

        [Test]
        public void RandomXorShift_WithSameSeed_ProducesSameSequence()
        {
            var rng1 = new RandomXorShift(12345u);
            var rng2 = new RandomXorShift(12345u);
            for (int i = 0; i < 100; i++)
            {
                ClassicAssert.AreEqual(rng1.NextInteger(), rng2.NextInteger());
            }
        }
        #endregion

        #region RandomShift Tests
        [Test]
        public void RandomShift_Constructor_WithSeed_InitializesCorrectly()
        {
            var rng = new RandomShift(12345u);
            ClassicAssert.IsNotNull(rng);
        }

        [Test]
        public void RandomShift_NextSingle_ReturnsValueInRange()
        {
            var rng = new RandomShift(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle();
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.Less(value, 1.0f);
            }
        }

        [Test]
        public void RandomShift_NextSingle_WithMax_ReturnsValueInRange()
        {
            var rng = new RandomShift(12345u);
            var max = 10.0f;
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextSingle(max);
                ClassicAssert.GreaterOrEqual(value, 0.0f);
                ClassicAssert.LessOrEqual(value, max);
            }
        }

        [Test]
        public void RandomShift_NextInteger_ReturnsValue()
        {
            var rng = new RandomShift(12345u);
            for (int i = 0; i < TestIterations; i++)
            {
                var value = rng.NextInteger();
                ClassicAssert.GreaterOrEqual(value, 0u);
            }
        }

        [Test]
        public void RandomShift_WithSameSeed_ProducesSameSequence()
        {
            var rng1 = new RandomShift(12345u);
            var rng2 = new RandomShift(12345u);
            for (int i = 0; i < 100; i++)
            {
                ClassicAssert.AreEqual(rng1.NextInteger(), rng2.NextInteger());
            }
        }
        #endregion

        #region Distribution Tests
        [Test]
        public void RandomStandard_Distribution_IsUniform()
        {
            var rng = new RandomStandard(12345);
            var buckets = new int[10];
            for (int i = 0; i < 10000; i++)
            {
                var value = rng.NextSingle();
                var bucket = (int)(value * 10);
                if (bucket >= 0 && bucket < 10)
                    buckets[bucket]++;
            }
            // Проверяем, что все корзины заполнены (не пустые)
            foreach (var count in buckets)
            {
                ClassicAssert.Greater(count, 0, "Каждая корзина должна содержать значения");
            }
        }

        [Test]
        public void RandomXorShift_Distribution_IsUniform()
        {
            var rng = new RandomXorShift(12345u);
            var buckets = new int[10];
            for (int i = 0; i < 10000; i++)
            {
                var value = rng.NextSingle();
                var bucket = (int)(value * 10);
                if (bucket >= 0 && bucket < 10)
                    buckets[bucket]++;
            }
            foreach (var count in buckets)
            {
                ClassicAssert.Greater(count, 0, "Каждая корзина должна содержать значения");
            }
        }
        #endregion
    }
}
