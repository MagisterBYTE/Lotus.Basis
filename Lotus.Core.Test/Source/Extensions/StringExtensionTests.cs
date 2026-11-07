using System;
using System.Collections.Generic;


namespace Lotus.Core.Extensions
{
    public class StringExtensionTests
    {
        #region Check Methods Tests
        [Fact]
        public void Common()
        {
            var testBeetwen = "Use the Assert[2222] class[00000] to";
            Assert.Equal(testBeetwen.RemoveAllBetweenSymbol('[', ']'),
                "Use the Assert[] class[] to");

            testBeetwen = "Use the Assert[2222] class";
            Assert.Equal(testBeetwen.RemoveAllBetweenSymbol('[', ']'),
                "Use the Assert[] class");

            var testBeetwenAll = "Use the Assert[2222] class";
            Assert.Equal(testBeetwenAll.RemoveAllBetweenSymbolWithSymbols('[', ']'),
                "Use the Assert class");

            testBeetwenAll = "Use the Assert[2222] class[00000] to";
            Assert.Equal(testBeetwenAll.RemoveAllBetweenSymbolWithSymbols('[', ']'),
                "Use the Assert class to");



            var eqal11 = "привет";
            var eqal12 = "Привет";

            var eqal21 = "привет";
            var eqal22 = "Привет";

            Assert.Equal(eqal21.Equal(eqal11), true);
            Assert.Equal(eqal21.Equal(eqal12), false);

            Assert.Equal(eqal22.EqualIgnoreCase(eqal11), true);
            Assert.Equal(eqal22.EqualIgnoreCase(eqal12), true);


            var test = "Use the Assert class to test conditions class.";

            test = test.RemoveFirstMatch("566");

            test = test.RemoveFirstMatch("class");
            Assert.Equal(test, "Use the Assert  to test conditions class.");


            test = "Use the Assert class to test conditions class.";
            test = test.RemoveLastMatch("class");
            Assert.Equal(test, "Use the Assert class to test conditions .");

            test = "Use the Assert class to test conditions class.xtx";
            test = test.RemoveExtension();
            Assert.Equal(test, "Use the Assert class to test conditions class");

            test = "Проверяемая строка хороша";
            test = test.RemoveFromSearchOption("хороша", TStringSearchOption.End);
            Assert.Equal(test, "Проверяемая строка ");

            test = "dfsfsd[778]sdfsd[090]";
            var nf = test.ExtractNumber();
            Assert.Equal(nf, 778);

            test = "dfsfsd[778]sdfsd[090]";
            var nl = test.ExtractNumberLast();
            Assert.Equal(nl, 90);

            test = "/// <param name=\"begin\">String begin.</param>";
            var token = test.ExtractString(">", "<");
            Assert.Equal(token, "String begin.");

            test = "222.3333";
            var before = test.SubstringTo(".", false);
            Assert.Equal(before, "222");

            var after = test.SubstringFrom(".", false);
            Assert.Equal(after, "3333");

            before = test.SubstringTo(".", true);
            Assert.Equal(before, "222.");

            after = test.SubstringFrom(".", true);
            Assert.Equal(after, ".3333");


            //
            // МЕТОДЫ РАБОТЫ СО СЛОВАМИ
            //
            test = "yield null to skip";
            test = test.ToWordUpper();
            Assert.Equal(test, "Yield null to skip");
        }


        [Fact]
        public void IsNull_ShouldReturnTrueForNullString()
        {
            // Arrange
            string nullString = null;

            // Act & Assert
            Assert.True(nullString.IsNull());
        }

        [Fact]
        public void IsNull_ShouldReturnFalseForNonNullString()
        {
            // Arrange
            var testString = "test";

            // Act & Assert
            Assert.False(testString.IsNull());
        }

        [Fact]
        public void IsExists_ShouldReturnTrueForNonEmptyString()
        {
            // Arrange
            var testString = "test";

            // Act & Assert
            Assert.True(testString.IsExists());
        }

        [Fact]
        public void IsExists_ShouldReturnFalseForEmptyString()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act & Assert
            Assert.False(emptyString.IsExists());
        }

        [Fact]
        public void IsLatinSymbols_ShouldReturnTrueForLatinString()
        {
            // Arrange
            var latinString = "Hello";

            // Act & Assert
            Assert.True(latinString.IsLatinSymbols());
        }

        [Fact]
        public void IsLatinSymbols_ShouldReturnFalseForNonLatinString()
        {
            // Arrange
            var cyrillicString = "Привет";

            // Act & Assert
            Assert.False(cyrillicString.IsLatinSymbols());
        }

        [Fact]
        public void IsCyrillicSymbols_ShouldReturnTrueForCyrillicString()
        {
            // Arrange
            var cyrillicString = "Привет";

            // Act & Assert
            Assert.True(cyrillicString.IsCyrillicSymbols());
        }

        [Fact]
        public void IsCyrillicSymbols_ShouldReturnFalseForNonCyrillicString()
        {
            // Arrange
            var latinString = "Hello";

            // Act & Assert
            Assert.False(latinString.IsCyrillicSymbols());
        }

        [Fact]
        public void IsLetterSymbols_ShouldReturnTrueForStringWithLetters()
        {
            // Arrange
            var letterString = "Hello123";

            // Act & Assert
            Assert.True(letterString.IsLetterSymbols());
        }

        [Fact]
        public void IsLetterSymbols_ShouldReturnFalseForStringWithoutLetters()
        {
            // Arrange
            var numberString = "12345";

            // Act & Assert
            Assert.False(numberString.IsLetterSymbols());
        }

        [Fact]
        public void IsDotOrCommaSymbols_ShouldReturnTrueForStringWithDotOrComma()
        {
            // Arrange
            var dotString = "123.45";
            var commaString = "123,45";

            // Act & Assert
            Assert.True(dotString.IsDotOrCommaSymbols());
            Assert.True(commaString.IsDotOrCommaSymbols());
        }

        [Fact]
        public void IsDotOrCommaSymbols_ShouldReturnFalseForStringWithoutDotOrComma()
        {
            // Arrange
            var testString = "12345";

            // Act & Assert
            Assert.False(testString.IsDotOrCommaSymbols());
        }

        #endregion

        #region Equal Methods Tests

        [Fact]
        public void Equal_ShouldReturnTrueForSameStringsCaseSensitive()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "Hello";

            // Act & Assert
            Assert.True(str1.Equal(str2));
        }

        [Fact]
        public void Equal_ShouldReturnFalseForDifferentCaseStrings()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "hello";

            // Act & Assert
            Assert.False(str1.Equal(str2));
        }

        [Fact]
        public void EqualIgnoreCase_ShouldReturnTrueForSameStringsIgnoreCase()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "hello";

            // Act & Assert
            Assert.True(str1.EqualIgnoreCase(str2));
        }

        [Fact]
        public void EqualIgnoreCase_ShouldReturnFalseForDifferentStrings()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "World";

            // Act & Assert
            Assert.False(str1.EqualIgnoreCase(str2));
        }

        #endregion

        #region Convert Methods Tests

        [Fact]
        public void ToFloat_ShouldConvertValidStringToFloat()
        {
            // Arrange
            var numberString = "123.45";

            // Act
            var result = numberString.ToFloat();

            // Assert
            Assert.Equal(123.45f, result, 0.001f);
        }

        [Fact]
        public void ToFloatUnchecked_ShouldConvertValidStringToFloat()
        {
            // Arrange
            var numberString = "123.45";

            // Act
            var result = numberString.ToFloatUnchecked();

            // Assert
            Assert.Equal(123.45f, result, 0.001f);
        }

        #endregion

        #region Transform Methods Tests

        [Fact]
        public void GetVerticalCopy_ShouldInsertNewLinesBetweenCharacters()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.GetVerticalCopy();

            // Assert
            Assert.Equal("H\ne\nl\nl\no", result);
        }

        [Fact]
        public void GetReverseCopy_ShouldReverseString()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.GetReverseCopy();

            // Assert
            Assert.Equal("olleH", result);
        }

        [Fact]
        public void InsertSymbols_ShouldInsertSymbolsAtSpecifiedPosition()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.InsertSymbols('!', 3, 2);

            // Assert
            Assert.Equal("Hel!!lo", result);
        }

        [Fact]
        public void SetLength_ShouldTruncateLongerString()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.SetLength(3, ' ');

            // Assert
            Assert.Equal("Hel", result);
        }

        [Fact]
        public void SetLength_ShouldPadShorterString()
        {
            // Arrange
            var testString = "Hi";

            // Act
            var result = testString.SetLength(5, '*');

            // Assert
            Assert.Equal("Hi***", result);
        }

        #endregion

        #region Calc Methods Tests

        [Fact]
        public void GetCountSymbol_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var count = testString.GetCountSymbol('l');

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetCountNewLine_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Line1\nLine2\nLine3";

            // Act
            var count = testString.GetCountNewLine();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetCountTab_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Column1\tColumn2\tColumn3";

            // Act
            var count = testString.GetCountTab();

            // Assert
            Assert.Equal(2, count);
        }

        #endregion

        #region Find Methods Tests

        [Fact]
        public void Contains_WithComparison_ShouldFindStringCaseInsensitive()
        {
            // Arrange
            var testString = "Hello World";

            // Act & Assert
            Assert.True(testString.Contains("world", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void IndexOf_WithList_ShouldFindFirstOccurrence()
        {
            // Arrange
            var testString = "Hello World";
            var searchList = new List<string> { "Foo", "World", "Bar" };

            // Act
            var index = testString.IndexOf(searchList);

            // Assert
            Assert.Equal(6, index);
        }

        #endregion

        #region Remove Methods Tests

        [Fact]
        public void RemoveTo_ShouldRemoveCharactersUpToMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveTo("World");

            // Assert
            Assert.Equal("World", result);
        }

        [Fact]
        public void RemoveToWith_ShouldRemoveCharactersIncludingMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveToWith("Hello ");

            // Assert
            Assert.Equal("World", result);
        }

        [Fact]
        public void RemoveFrom_ShouldRemoveCharactersAfterMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveFrom("Hello");

            // Assert
            Assert.Equal("Hello", result);
        }

        #endregion

        #region Extract Methods Tests

        [Fact]
        public void SubstringTo_ShouldReturnSubstringUpToMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.SubstringTo("World", false);

            // Assert
            Assert.Equal("Hello ", result);
        }

        [Fact]
        public void ExtractNumber_ShouldExtractFirstNumber()
        {
            // Arrange
            var testString = "Price: 123 dollars";

            // Act
            var number = testString.ExtractNumber();

            // Assert
            Assert.Equal(123, number);
        }

        [Fact]
        public void ExtractString_ShouldExtractBetweenDelimiters()
        {
            // Arrange
            var testString = "Name: [John] Doe";

            // Act
            var result = testString.ExtractString("[", "]");

            // Assert
            Assert.Equal("John", result);
        }

        #endregion

        #region Word Methods Tests

        [Fact]
        public void ToWordUpper_ShouldCapitalizeFirstLetter()
        {
            // Arrange
            var testString = "hello";

            // Act
            var result = testString.ToWordUpper();

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ToWordLower_ShouldLowercaseFirstLetter()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.ToWordLower();

            // Assert
            Assert.Equal("hello", result);
        }

        [Fact]
        public void ToTitleCase_ShouldConvertUnderscoresToCamelCase()
        {
            // Arrange
            var testString = "HELLO_WORLD";

            // Act
            var result = testString.ToTitleCase();

            // Assert
            Assert.Equal("HelloWorld", result);
        }

        [Fact]
        public void ToConstCase_ShouldConvertCamelCaseToUnderscores()
        {
            // Arrange
            var testString = "HelloWorld";

            // Act
            var result = testString.ToConstCase();

            // Assert
            Assert.Equal("HELLO_WORLD", result);
        }

        #endregion
    }
}
