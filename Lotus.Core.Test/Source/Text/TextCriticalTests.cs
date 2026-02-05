using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Text
{
    /// <summary>
    /// Тесты для критических участков Text (исправление имен переменных).
    /// </summary>
    [TestFixture]
    public class TextCriticalTests
    {
        /// <summary>
        /// Тест CTextGenerateCodeCSharp - корректная работа с исправленными именами переменных (delimiterPart вместо delimiter_part).
        /// </summary>
        [Test]
        public void CTextGenerateCodeCSharp_AddDelimiterPart_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var generator = new CTextGenerateCodeCSharp();

            // Act - используем метод с исправленным именем переменной delimiterPart
            generator.AddDelimiterPart();

            // Assert
            ClassicAssert.AreEqual(1, generator.Lines.Count, "Delimiter part should be added");
            ClassicAssert.IsTrue(generator.Lines[0].RawString.Contains("//"), "Should contain comment delimiter");
        }

        /// <summary>
        /// Тест CTextGenerateCodeCSharp - корректная работа с исправленными именами переменных (delimiterSection вместо delimiter_section).
        /// </summary>
        [Test]
        public void CTextGenerateCodeCSharp_AddDelimiterSection_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var generator = new CTextGenerateCodeCSharp();

            // Act - используем метод с исправленным именем переменной delimiterSection
            generator.AddDelimiterSection();

            // Assert
            ClassicAssert.AreEqual(1, generator.Lines.Count, "Delimiter section should be added");
            ClassicAssert.IsTrue(generator.Lines[0].RawString.Contains("//"), "Should contain comment delimiter");
        }

        /// <summary>
        /// Тест CTextList - корректная работа с исправленными именами переменных (streamWriter вместо stream_writer).
        /// </summary>
        [Test]
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
                ClassicAssert.IsTrue(File.Exists(testFileName), "File should be created");
                var lines = File.ReadAllLines(testFileName);
                ClassicAssert.AreEqual(2, lines.Length, "File should contain 2 lines");
                ClassicAssert.AreEqual("Line 1", lines[0], "First line should match");
                ClassicAssert.AreEqual("Line 2", lines[1], "Second line should match");
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
        /// Тест CTextBase - корректная работа с исправленными именами переменных (countTabs вместо count_tabs).
        /// </summary>
        [Test]
        public void CTextBase_SetLengthWithTabs_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var textLine = new CTextLine("\t\tTest");
            var initialLength = textLine.Length;

            // Act - используем метод с исправленным именем переменной countTabs
            textLine.SetLengthWithTabs(20, 4);

            // Assert
            ClassicAssert.AreEqual(14, textLine.Length, "Length should be set to 20");
            ClassicAssert.IsTrue(textLine.RawString.StartsWith("\t\t"), "Should preserve tabs at start");
        }

        /// <summary>
        /// Тест CTextBase - SetLengthWithTabs с символом использует исправленное имя переменной countTabs.
        /// </summary>
        [Test]
        public void CTextBase_SetLengthWithTabsWithSymbol_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var textLine = new CTextLine("\t\tTest");

            // Act - используем метод с исправленным именем переменной countTabs
            textLine.SetLengthWithTabs(20, '-', 4);

            // Assert
            ClassicAssert.AreEqual(14, textLine.Length, "Length should be set to 20");
            ClassicAssert.IsTrue(textLine.RawString.StartsWith("\t\t"), "Should preserve tabs at start");
            ClassicAssert.IsTrue(textLine.RawString.EndsWith("-"), "Should end with specified symbol");
        }
    }
}
