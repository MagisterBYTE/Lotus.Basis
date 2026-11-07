using System;

namespace Lotus.Core.CommonTypes
{
    /// <summary>
    /// Тесты для <see cref="TColor"/>.
    /// </summary>
    public class ColorTests
    {
        /// <summary>
        /// Тест метода Approximately - исправленный метод сравнивает правильные компоненты.
        /// </summary>
        [Fact]
        public void Approximately_WithSimilarColors_ReturnsTrue()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(101, 151, 201, 255);

            Assert.True(TColor.Approximately(color1, color2, 1));
        }

        /// <summary>
        /// Тест метода Approximately - возвращает false при различии больше epsilon.
        /// </summary>
        [Fact]
        public void Approximately_WithDifferentColors_ReturnsFalse()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(110, 160, 210, 255);

            Assert.False(TColor.Approximately(color1, color2, 1));
        }

        /// <summary>
        /// Тест метода Approximately - сравнивает R с R, G с G, B с B (исправление).
        /// </summary>
        [Fact]
        public void Approximately_ComparesCorrectComponents()
        {
            var color1 = new TColor(100, 200, 50, 255);
            var color2 = new TColor(100, 201, 50, 255);

            // R и B одинаковые, G отличается на 1 - должно быть true с epsilon=1
            Assert.True(TColor.Approximately(color1, color2, 1));

            var color3 = new TColor(100, 200, 50, 255);
            var color4 = new TColor(101, 200, 50, 255);

            // G и B одинаковые, R отличается на 1 - должно быть true с epsilon=1
            Assert.True(TColor.Approximately(color3, color4, 1));
        }

        /// <summary>
        /// Тест оператора сложения.
        /// </summary>
        [Fact]
        public void Operator_Addition_AddsColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(50, 75, 25, 255);

            var result = color1 + color2;

            Assert.Equal(150, result.R);
            Assert.Equal(225, result.G);
            Assert.Equal(225, result.B);
        }

        /// <summary>
        /// Тест оператора вычитания.
        /// </summary>
        [Fact]
        public void Operator_Subtraction_SubtractsColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(50, 75, 25, 255);

            var result = color1 - color2;

            Assert.Equal(50, result.R);
            Assert.Equal(75, result.G);
            Assert.Equal(175, result.B);
        }

        /// <summary>
        /// Тест оператора умножения (модуляция).
        /// </summary>
        [Fact]
        public void Operator_Multiplication_ModulatesColors()
        {
            var color1 = new TColor(200, 150, 100, 255);
            var color2 = new TColor(128, 128, 128, 255);

            var result = color1 * color2;

            // Модуляция: (200 * 128) / 255 ≈ 100
            Assert.True(Math.Abs(result.R - 100) <= 1);
        }

        /// <summary>
        /// Тест оператора умножения на float.
        /// </summary>
        [Fact]
        public void Operator_MultiplicationWithFloat_ScalesColor()
        {
            var color = new TColor(100, 150, 200, 255);
            var result = color * 0.5f;

            Assert.Equal(50, result.R);
            Assert.Equal(75, result.G);
            Assert.Equal(100, result.B);
        }

        /// <summary>
        /// Тест оператора равенства.
        /// </summary>
        [Fact]
        public void Operator_Equality_ReturnsTrueForEqualColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(100, 150, 200, 255);

            Assert.True(color1 == color2);
        }

        /// <summary>
        /// Тест оператора неравенства.
        /// </summary>
        [Fact]
        public void Operator_Inequality_ReturnsTrueForDifferentColors()
        {
            var color1 = new TColor(100, 150, 200, 255);
            var color2 = new TColor(101, 150, 200, 255);

            Assert.True(color1 != color2);
        }

        /// <summary>
        /// Тест конвертации из формата BGRA.
        /// </summary>
        [Fact]
        public void FromBGRA_ConvertsCorrectly()
        {
            // BGRA: B=0x12, G=0x34, R=0x56, A=0x78
            var color = TColor.FromBGRA(0x12345678);

            Assert.Equal(0x56, color.R);
            Assert.Equal(0x34, color.G);
            Assert.Equal(0x12, color.B);
            Assert.Equal(0x78, color.A);
        }

        /// <summary>
        /// Тест конвертации из формата ABGR.
        /// </summary>
        [Fact]
        public void FromABGR_ConvertsCorrectly()
        {
            // ABGR: A=0x12, B=0x34, G=0x56, R=0x78
            var color = TColor.FromABGR(0x12345678);

            Assert.Equal(0x78, color.R);
            Assert.Equal(0x56, color.G);
            Assert.Equal(0x34, color.B);
            Assert.Equal(0x12, color.A);
        }

        /// <summary>
        /// Тест конвертации из формата RGBA.
        /// </summary>
        [Fact]
        public void FromRGBA_ConvertsCorrectly()
        {
            // RGBA: R=0x12, G=0x34, B=0x56, A=0x78
            var color = TColor.FromRGBA(0x12345678);

            Assert.Equal(0x12, color.R);
            Assert.Equal(0x34, color.G);
            Assert.Equal(0x56, color.B);
            Assert.Equal(0x78, color.A);
        }

        /// <summary>
        /// Тест десериализации из строки.
        /// </summary>
        [Fact]
        public void DeserializeFromString_ParsesCorrectly()
        {
            var color = TColor.DeserializeFromString("100,150,200,255");

            Assert.Equal(100, color.R);
            Assert.Equal(150, color.G);
            Assert.Equal(200, color.B);
            Assert.Equal(255, color.A);
        }

        /// <summary>
        /// Тест сериализации в строку.
        /// </summary>
        [Fact]
        public void SerializeToString_FormatsCorrectly()
        {
            var color = new TColor(100, 150, 200, 255);
            var result = color.SerializeToString();

            Assert.Equal("100,150,200,255", result);
        }
    }
}
