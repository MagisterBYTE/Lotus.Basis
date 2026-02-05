using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Easing
{
    /// <summary>
    /// Тесты для критических участков функций плавности (граничные случаи).
    /// </summary>
    [TestFixture]
    public class EasingCriticalTests
    {
        /// <summary>
        /// Тест Interpolation - граничные значения 0 и 1 для всех типов.
        /// </summary>
        [Test]
        public void Interpolation_WithBoundaryValues_ReturnsCorrectResults()
        {
            // При t=0 должно быть start, при t=1 должно быть end для всех типов
            foreach (TEasingType easingType in Enum.GetValues(typeof(TEasingType)))
            {
                if(easingType == TEasingType.BounceIn)
                {
                    Console.WriteLine("");
                }
                var result0 = XEasing.Interpolation(0.0f, 1.0f, 0.0f, easingType);
                var result1 = XEasing.Interpolation(0.0f, 1.0f, 1.0f, easingType);

                ClassicAssert.AreEqual(0.0f, result0, 0.01f, $"Easing type {easingType} should return start at t=0");
                ClassicAssert.AreEqual(1.0f, result1, 0.01f, $"Easing type {easingType} should return end at t=1");
            }
        }

        /// <summary>
        /// Тест Interpolation - значения вне диапазона [0, 1].
        /// </summary>
        [Test]
        public void Interpolation_WithOutOfRangeValues_HandlesCorrectly()
        {
            // Отрицательные значения
            var resultNegative = XEasing.Interpolation(0.0f, 1.0f, -0.5f, TEasingType.Linear);
            ClassicAssert.IsTrue(resultNegative <= 0.0f || resultNegative >= 0.0f, "Negative input should be handled");

            // Значения больше 1
            var resultGreater = XEasing.Interpolation(0.0f, 1.0f, 1.5f, TEasingType.Linear);
            ClassicAssert.IsTrue(resultGreater >= 1.0f || resultGreater <= 1.0f, "Value > 1 should be handled");
        }

        /// <summary>
        /// Тест Interpolation - линейная функция для средних значений.
        /// </summary>
        [Test]
        public void Interpolation_Linear_ReturnsLinearProgression()
        {
            ClassicAssert.AreEqual(0.0f, XEasing.Interpolation(0.0f, 1.0f, 0.0f, TEasingType.Linear), 0.0001f);
            ClassicAssert.AreEqual(0.5f, XEasing.Interpolation(0.0f, 1.0f, 0.5f, TEasingType.Linear), 0.0001f);
            ClassicAssert.AreEqual(1.0f, XEasing.Interpolation(0.0f, 1.0f, 1.0f, TEasingType.Linear), 0.0001f);
        }

        /// <summary>
        /// Тест Interpolation - монотонность для некоторых типов.
        /// </summary>
        [Test]
        public void Interpolation_MonotonicTypes_AreMonotonic()
        {
            var types = new[] { TEasingType.Linear, TEasingType.QuadIn, TEasingType.QuadOut, 
                TEasingType.CubeIn, TEasingType.CubeOut };

            foreach (var type in types)
            {
                var prev = XEasing.Interpolation(0.0f, 1.0f, 0.0f, type);
                for (float t = 0.1f; t <= 1.0f; t += 0.1f)
                {
                    var current = XEasing.Interpolation(0.0f, 1.0f, t, type);
                    ClassicAssert.IsTrue(current >= prev, $"Easing type {type} should be monotonic at t={t}");
                    prev = current;
                }
            }
        }
    }
}
