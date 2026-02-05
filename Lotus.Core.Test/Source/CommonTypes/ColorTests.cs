using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.CommonTypes
{
    /// <summary>
    /// Тесты для <see cref="TColor"/>.
    /// </summary>
    [TestFixture]
    public class ColorTests
    {
        /// <summary>
        /// Тест метода Approximately - исправленный метод сравнивает правильные компоненты.
        /// </summary>
        [Test]
        public void Approximately_WithSimilarColors_ReturnsTrue()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(101, 151, 201, 255);

            ClassicAssert.IsTrue(TColor.Approximately(color1, color2, 1));
        }

        /// <summary>
        /// Тест метода Approximately - возвращает false при различии больше epsilon.
        /// </summary>
        [Test]
        public void Approximately_WithDifferentColors_ReturnsFalse()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(110, 160, 210, 255);

            ClassicAssert.IsFalse(TColor.Approximately(color1, color2, 1));
        }

        /// <summary>
        /// Тест метода Approximately - сравнивает R с R, G с G, B с B (исправление).
        /// </summary>
        [Test]
        public void Approximately_ComparesCorrectComponents()
        {
            var color1 = new TColor(100, 200, 50, 255);
            var color2 = new TColor(100, 201, 50, 255);

            // R и B одинаковые, G отличается на 1 - должно быть true с epsilon=1
            ClassicAssert.IsTrue(TColor.Approximately(color1, color2, 1));

            var color3 = new TColor(100, 200, 50, 255);
            var color4 = new TColor(101, 200, 50, 255);

            // G и B одинаковые, R отличается на 1 - должно быть true с epsilon=1
            ClassicAssert.IsTrue(TColor.Approximately(color3, color4, 1));
        }

        /// <summary>
        /// Тест оператора сложения.
        /// </summary>
        [Test]
        public void Operator_Addition_AddsColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(50, 75, 25, 255);

            var result = color1 + color2;

            ClassicAssert.AreEqual(150, result.R);
            ClassicAssert.AreEqual(225, result.G);
            ClassicAssert.AreEqual(225, result.B);
        }

        /// <summary>
        /// Тест оператора вычитания.
        /// </summary>
        [Test]
        public void Operator_Subtraction_SubtractsColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(50, 75, 25, 255);

            var result = color1 - color2;

            ClassicAssert.AreEqual(50, result.R);
            ClassicAssert.AreEqual(75, result.G);
            ClassicAssert.AreEqual(175, result.B);
        }

        /// <summary>
        /// Тест оператора умножения (модуляция).
        /// </summary>
        [Test]
        public void Operator_Multiplication_ModulatesColors()
        {
            var color1 = new TColor(200, 150, 100, 255);
            var color2 = new TColor(128, 128, 128, 255);

            var result = color1 * color2;

            // Модуляция: (200 * 128) / 255 ≈ 100
            ClassicAssert.IsTrue(Math.Abs(result.R - 100) <= 1);
        }

        /// <summary>
        /// Тест оператора умножения на float.
        /// </summary>
        [Test]
        public void Operator_MultiplicationWithFloat_ScalesColor()
        {
            var color = new TColor(100, 150, 200, 255);
            var result = color * 0.5f;

            ClassicAssert.AreEqual(50, result.R);
            ClassicAssert.AreEqual(75, result.G);
            ClassicAssert.AreEqual(100, result.B);
        }

        /// <summary>
        /// Тест оператора равенства.
        /// </summary>
        [Test]
        public void Operator_Equality_ReturnsTrueForEqualColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(100, 150, 200, 255);

            ClassicAssert.IsTrue(color1 == color2);
        }

        /// <summary>
        /// Тест оператора неравенства.
        /// </summary>
        [Test]
        public void Operator_Inequality_ReturnsTrueForDifferentColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(101, 150, 200, 255);

            ClassicAssert.IsTrue(color1 != color2);
        }

        /// <summary>
        /// Тест конвертации из формата BGRA.
        /// </summary>
        [Test]
        public void FromBGRA_ConvertsCorrectly()
        {
            // BGRA: B=0x12, G=0x34, R=0x56, A=0x78
            var color = TColor.FromBGRA(0x12345678);

            ClassicAssert.AreEqual(0x56, color.R);
            ClassicAssert.AreEqual(0x34, color.G);
            ClassicAssert.AreEqual(0x12, color.B);
            ClassicAssert.AreEqual(0x78, color.A);
        }

        /// <summary>
        /// Тест конвертации из формата ABGR.
        /// </summary>
        [Test]
        public void FromABGR_ConvertsCorrectly()
        {
            // ABGR: A=0x12, B=0x34, G=0x56, R=0x78
            var color = TColor.FromABGR(0x12345678);

            ClassicAssert.AreEqual(0x78, color.R);
            ClassicAssert.AreEqual(0x56, color.G);
            ClassicAssert.AreEqual(0x34, color.B);
            ClassicAssert.AreEqual(0x12, color.A);
        }

        /// <summary>
        /// Тест конвертации из формата RGBA.
        /// </summary>
        [Test]
        public void FromRGBA_ConvertsCorrectly()
        {
            // RGBA: R=0x12, G=0x34, B=0x56, A=0x78
            var color = TColor.FromRGBA(0x12345678);

            ClassicAssert.AreEqual(0x12, color.R);
            ClassicAssert.AreEqual(0x34, color.G);
            ClassicAssert.AreEqual(0x56, color.B);
            ClassicAssert.AreEqual(0x78, color.A);
        }

        /// <summary>
        /// Тест десериализации из строки.
        /// </summary>
        [Test]
        public void DeserializeFromString_ParsesCorrectly()
        {
            var color = TColor.DeserializeFromString("100,150,200,255");

            ClassicAssert.AreEqual(100, color.R);
            ClassicAssert.AreEqual(150, color.G);
            ClassicAssert.AreEqual(200, color.B);
            ClassicAssert.AreEqual(255, color.A);
        }

        /// <summary>
        /// Тест сериализации в строку.
        /// </summary>
        [Test]
        public void SerializeToString_FormatsCorrectly()
        {
            var color = new TColor(100, 150, 200, 255);
            var result = color.SerializeToString();

            ClassicAssert.AreEqual("100,150,200,255", result);
        }
    }
}
