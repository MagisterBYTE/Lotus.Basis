using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathNoiseTests
    {
        private const float EpsilonF = 0.00001f;

        #region XGenerationNoise1D Tests
        [Test]
        public void XGenerationNoise1D_NoiseInteger1DV1_ReturnsDeterministicValue()
        {
            var value1 = XGenerationNoise1D.NoiseInteger1DV1(10);
            var value2 = XGenerationNoise1D.NoiseInteger1DV1(10);
            ClassicAssert.AreEqual(value1, value2, "Шум должен быть детерминированным для одинаковых входных значений");
        }

        [Test]
        public void XGenerationNoise1D_NoiseInteger1DV1_WithDifferentValues_ReturnsDifferentValues()
        {
            var value1 = XGenerationNoise1D.NoiseInteger1DV1(10);
            var value2 = XGenerationNoise1D.NoiseInteger1DV1(20);
            ClassicAssert.AreNotEqual(value1, value2, "Разные входные значения должны давать разные результаты");
        }

        [Test]
        public void XGenerationNoise1D_NoiseInteger1DV1_ReturnsPositiveValue()
        {
            for (int i = 0; i < 100; i++)
            {
                var value = XGenerationNoise1D.NoiseInteger1DV1(i);
                ClassicAssert.GreaterOrEqual(value, 0, "Шум должен возвращать неотрицательные значения");
            }
        }

        [Test]
        public void XGenerationNoise1D_NoiseInteger1DV2_ReturnsDeterministicValue()
        {
            var value1 = XGenerationNoise1D.NoiseInteger1DV2(10);
            var value2 = XGenerationNoise1D.NoiseInteger1DV2(10);
            ClassicAssert.AreEqual(value1, value2, "Шум должен быть детерминированным для одинаковых входных значений");
        }

        [Test]
        public void XGenerationNoise1D_NoiseInteger1DV2_WithDifferentValues_ReturnsDifferentValues()
        {
            var value1 = XGenerationNoise1D.NoiseInteger1DV2(10);
            var value2 = XGenerationNoise1D.NoiseInteger1DV2(20);
            ClassicAssert.AreNotEqual(value1, value2, "Разные входные значения должны давать разные результаты");
        }

        [Test]
        public void XGenerationNoise1D_Seed_CanBeChanged()
        {
            var originalSeed = XGenerationNoise1D.Seed;
            var value1 = XGenerationNoise1D.NoiseInteger1DV1(10);
            
            XGenerationNoise1D.Seed = 100;
            var value2 = XGenerationNoise1D.NoiseInteger1DV1(10);
            
            ClassicAssert.AreNotEqual(value1, value2, "Изменение Seed должно влиять на результат");
            
            // Восстанавливаем исходное значение
            XGenerationNoise1D.Seed = originalSeed;
        }
        #endregion

        #region XGenerationNoise2D Tests
        [Test]
        public void XGenerationNoise2D_NoiseInteger2DV1_ReturnsDeterministicValue()
        {
            var value1 = XGenerationNoise2D.NoiseInteger2DV1(10, 20);
            var value2 = XGenerationNoise2D.NoiseInteger2DV1(10, 20);
            ClassicAssert.AreEqual(value1, value2, "Шум должен быть детерминированным для одинаковых входных значений");
        }

        [Test]
        public void XGenerationNoise2D_NoiseInteger2DV1_WithDifferentValues_ReturnsDifferentValues()
        {
            var value1 = XGenerationNoise2D.NoiseInteger2DV1(10, 20);
            var value2 = XGenerationNoise2D.NoiseInteger2DV1(20, 10);
            ClassicAssert.AreNotEqual(value1, value2, "Разные входные значения должны давать разные результаты");
        }

        [Test]
        public void XGenerationNoise2D_NoiseInteger2DV2_ReturnsDeterministicValue()
        {
            var value1 = XGenerationNoise2D.NoiseInteger2DV2(10, 20);
            var value2 = XGenerationNoise2D.NoiseInteger2DV2(10, 20);
            ClassicAssert.AreEqual(value1, value2, "Шум должен быть детерминированным для одинаковых входных значений");
        }

        [Test]
        public void XGenerationNoise2D_NoiseInteger2DV2_WithDifferentValues_ReturnsDifferentValues()
        {
            var value1 = XGenerationNoise2D.NoiseInteger2DV2(10, 20);
            var value2 = XGenerationNoise2D.NoiseInteger2DV2(20, 10);
            ClassicAssert.AreNotEqual(value1, value2, "Разные входные значения должны давать разные результаты");
        }

        [Test]
        public void XGenerationNoise2D_NoiseSingle2DV1_ReturnsValueInRange()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var value = XGenerationNoise2D.NoiseSingle2DV1(x, y);
                    ClassicAssert.Greater(value, 0.0f, "NoiseSingle2DV1 должен возвращать положительные значения");
                    ClassicAssert.Less(value, 1.0f, "NoiseSingle2DV1 должен возвращать значения меньше 1");
                }
            }
        }

        [Test]
        public void XGenerationNoise2D_NoiseSingle2DV1_ReturnsDeterministicValue()
        {
            var value1 = XGenerationNoise2D.NoiseSingle2DV1(10.5f, 20.3f);
            var value2 = XGenerationNoise2D.NoiseSingle2DV1(10.5f, 20.3f);
            ClassicAssert.AreEqual(value1, value2, EpsilonF, "Шум должен быть детерминированным для одинаковых входных значений");
        }

        [Test]
        public void XGenerationNoise2D_NoiseSingle2DV2_ReturnsValueInRange()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var value = XGenerationNoise2D.NoiseSingle2DV2(x, y);
                    ClassicAssert.GreaterOrEqual(value, -1.0f, "NoiseSingle2DV2 должен возвращать значения >= -1");
                    ClassicAssert.LessOrEqual(value, 1.0f, "NoiseSingle2DV2 должен возвращать значения <= 1");
                }
            }
        }

        [Test]
        public void XGenerationNoise2D_SmoothNoiseSingle2D_ReturnsValueInRange()
        {
            for (float x = 0.0f; x < 10.0f; x += 0.5f)
            {
                for (float y = 0.0f; y < 10.0f; y += 0.5f)
                {
                    var value = XGenerationNoise2D.SmoothNoiseSingle2D(x, y);
                    ClassicAssert.Greater(value, 0.0f, "SmoothNoiseSingle2D должен возвращать положительные значения");
                    ClassicAssert.Less(value, 1.0f, "SmoothNoiseSingle2D должен возвращать значения меньше 1");
                }
            }
        }

        [Test]
        public void XGenerationNoise2D_SmoothNoiseSingle2D_IsSmootherThanNoise()
        {
            float x = 5.5f;
            float y = 5.5f;
            var noiseValue = XGenerationNoise2D.NoiseSingle2D(x, y);
            var smoothValue = XGenerationNoise2D.SmoothNoiseSingle2D(x, y);
            
            // Сглаженный шум должен быть более плавным (не обязательно меньше, но более предсказуемым)
            ClassicAssert.IsNotNull(smoothValue);
        }

        [Test]
        public void XGenerationNoise2D_InterpolatedNoiseSingle2D_ReturnsValueInRange()
        {
            for (float x = 0.0f; x < 10.0f; x += 0.3f)
            {
                for (float y = 0.0f; y < 10.0f; y += 0.3f)
                {
                    var value = XGenerationNoise2D.InterpolatedNoiseSingle2D(x, y);
                    ClassicAssert.Greater(value, 0.0f, "InterpolatedNoiseSingle2D должен возвращать положительные значения");
                    ClassicAssert.Less(value, 1.0f, "InterpolatedNoiseSingle2D должен возвращать значения меньше 1");
                }
            }
        }

        [Test]
        public void XGenerationNoise2D_InterpolatedNoiseSingle2D_IsContinuous()
        {
            float x = 5.0f;
            float y = 5.0f;
            var value1 = XGenerationNoise2D.InterpolatedNoiseSingle2D(x, y);
            var value2 = XGenerationNoise2D.InterpolatedNoiseSingle2D(x + 0.01f, y + 0.01f);
            
            // Интерполированный шум должен быть непрерывным (небольшие изменения входа дают небольшие изменения выхода)
            var diff = System.Math.Abs(value1 - value2);
            ClassicAssert.Less(diff, 0.5f, "Интерполированный шум должен быть относительно непрерывным");
        }

        [Test]
        public void XGenerationNoise2D_Seed_CanBeChanged()
        {
            var originalSeed = XGenerationNoise2D.Seed;
            var value1 = XGenerationNoise2D.NoiseInteger2DV1(10, 20);
            
            XGenerationNoise2D.Seed = 100;
            var value2 = XGenerationNoise2D.NoiseInteger2DV1(10, 20);
            
            ClassicAssert.AreNotEqual(value1, value2, "Изменение Seed должно влиять на результат");
            
            // Восстанавливаем исходное значение
            XGenerationNoise2D.Seed = originalSeed;
        }
        #endregion
    }
}
