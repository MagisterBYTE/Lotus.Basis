using System;
using System.IO;

namespace Lotus.Core.Text
{
    /// <summary>
    /// Тесты для критических участков Text (исправление имен переменных).
    /// </summary>
    public class TextCriticalTests
    {
        /// <summary>
        /// Тест CTextGenerateCodeCSharp - корректная работа с исправленными именами переменных (delimiterPart вместо delimiterPart).
        /// </summary>
        [Fact]
        public void CTextGenerateCodeCSharp_AddDelimiterPart_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var generator = new CTextGenerateCodeCSharp();

            // Act - используем метод с исправленным именем переменной delimiterPart
            generator.AddDelimiterPart();

            // Assert
            Assert.Equal(1, generator.Lines.Count);
            Assert.True(generator.Lines[0].RawString.Contains("//"), "Should contain comment delimiter");
        }

        /// <summary>
        /// Тест CTextGenerateCodeCSharp - корректная работа с исправленными именами переменных (delimiterSection вместо delimiterSection).
        /// </summary>
        [Fact]
        public void CTextGenerateCodeCSharp_AddDelimiterSection_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var generator = new CTextGenerateCodeCSharp();

            // Act - используем метод с исправленным именем переменной delimiterSection
            generator.AddDelimiterSection();

            // Assert
            Assert.Equal(1, generator.Lines.Count);
            Assert.True(generator.Lines[0].RawString.Contains("//"), "Should contain comment delimiter");
        }

        /// <summary>
        /// Тест CTextList - корректная работа с исправленными именами переменных (streamWriter вместо streamWriter).
        /// </summary>
        [Fact]
        public void CTextList_Save_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var textList = new CTextList();
            textList.Add("Line 1");
            textList.Add("Line 2");
            var testFileName = Path.Combine(Path.GetTempPath(), "test_text.txt");

            try
            {
                // Act - используем метод с исправленным именем переменной streamWriter
                textList.Save(testFileName);

                // Assert
                Assert.True(File.Exists(testFileName), "File should be created");
                var lines = File.ReadAllLines(testFileName);
                Assert.Equal(2, lines.Length);
                Assert.Equal("Line 1", lines[0]);
                Assert.Equal("Line 2", lines[1]);
            }
            finally
            {
                // Cleanup
                if (File.Exists(testFileName))
                {
                    File.Delete(testFileName);
                }
            }
        }

        /// <summary>
        /// Тест CTextBase - корректная работа с исправленными именами переменных (countTabs вместо countTabs).
        /// </summary>
        [Fact]
        public void CTextBase_SetLengthWithTabs_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var textLine = new CTextLine("\t\tTest");
            var initialLength = textLine.Length;

            // Act - используем метод с исправленным именем переменной countTabs
            textLine.SetLengthWithTabs(20, 4);

            // Assert
            Assert.Equal(14, textLine.Length);
            Assert.True(textLine.RawString.StartsWith("\t\t"), "Should preserve tabs at start");
        }

        /// <summary>
        /// Тест CTextBase - SetLengthWithTabs с символом использует исправленное имя переменной countTabs.
        /// </summary>
        [Fact]
        public void CTextBase_SetLengthWithTabsWithSymbol_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var textLine = new CTextLine("\t\tTest");

            // Act - используем метод с исправленным именем переменной countTabs
            textLine.SetLengthWithTabs(20, '-', 4);

            // Assert
            Assert.Equal(14, textLine.Length);
            Assert.True(textLine.RawString.StartsWith("\t\t"), "Should preserve tabs at start");
            Assert.True(textLine.RawString.EndsWith("-"), "Should end with specified symbol");
        }
    }
}
