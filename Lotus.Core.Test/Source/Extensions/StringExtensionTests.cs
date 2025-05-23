using System;
using System.Collections.Generic;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        #region Check Methods Tests
        [Test]
        public void Common()
        {
            var test_beetwen = "Use the Assert[2222] class[00000] to";
            ClassicAssert.AreEqual(test_beetwen.RemoveAllBetweenSymbol('[', ']'),
                "Use the Assert[] class[] to");

            test_beetwen = "Use the Assert[2222] class";
            ClassicAssert.AreEqual(test_beetwen.RemoveAllBetweenSymbol('[', ']'),
                "Use the Assert[] class");

            var test_beetwen_all = "Use the Assert[2222] class";
            ClassicAssert.AreEqual(test_beetwen_all.RemoveAllBetweenSymbolWithSymbols('[', ']'),
                "Use the Assert class");

            test_beetwen_all = "Use the Assert[2222] class[00000] to";
            ClassicAssert.AreEqual(test_beetwen_all.RemoveAllBetweenSymbolWithSymbols('[', ']'),
                "Use the Assert class to");



            var eqal11 = "привет";
            var eqal12 = "Привет";

            var eqal21 = "привет";
            var eqal22 = "Привет";

            ClassicAssert.AreEqual(eqal21.Equal(eqal11), true);
            ClassicAssert.AreEqual(eqal21.Equal(eqal12), false);

            ClassicAssert.AreEqual(eqal22.EqualIgnoreCase(eqal11), true);
            ClassicAssert.AreEqual(eqal22.EqualIgnoreCase(eqal12), true);


            var test = "Use the Assert class to test conditions class.";

            test = test.RemoveFirstMatch("566");

            test = test.RemoveFirstMatch("class");
            ClassicAssert.AreEqual(test, "Use the Assert  to test conditions class.");


            test = "Use the Assert class to test conditions class.";
            test = test.RemoveLastMatch("class");
            ClassicAssert.AreEqual(test, "Use the Assert class to test conditions .");

            test = "Use the Assert class to test conditions class.xtx";
            test = test.RemoveExtension();
            ClassicAssert.AreEqual(test, "Use the Assert class to test conditions class");

            test = "Проверяемая строка хороша";
            test = test.RemoveFromSearchOption("хороша", TStringSearchOption.End);
            ClassicAssert.AreEqual(test, "Проверяемая строка ");

            test = "dfsfsd[778]sdfsd[090]";
            var nf = test.ExtractNumber();
            ClassicAssert.AreEqual(nf, 778);

            test = "dfsfsd[778]sdfsd[090]";
            var nl = test.ExtractNumberLast();
            ClassicAssert.AreEqual(nl, 90);

            test = "/// <param name=\"begin\">String begin.</param>";
            var token = test.ExtractString(">", "<");
            ClassicAssert.AreEqual(token, "String begin.");

            test = "222.3333";
            var before = test.SubstringTo(".", false);
            ClassicAssert.AreEqual(before, "222");

            var after = test.SubstringFrom(".", false);
            ClassicAssert.AreEqual(after, "3333");

            before = test.SubstringTo(".", true);
            ClassicAssert.AreEqual(before, "222.");

            after = test.SubstringFrom(".", true);
            ClassicAssert.AreEqual(after, ".3333");


            //
            // МЕТОДЫ РАБОТЫ СО СЛОВАМИ
            //
            test = "yield null to skip";
            test = test.ToWordUpper();
            ClassicAssert.AreEqual(test, "Yield null to skip");
        }


        [Test]
        public void IsNull_ShouldReturnTrueForNullString()
        {
            // Arrange
            string nullString = null;

            // Act & Assert
            ClassicAssert.IsTrue(nullString.IsNull());
        }

        [Test]
        public void IsNull_ShouldReturnFalseForNonNullString()
        {
            // Arrange
            var testString = "test";

            // Act & Assert
            ClassicAssert.IsFalse(testString.IsNull());
        }

        [Test]
        public void IsExists_ShouldReturnTrueForNonEmptyString()
        {
            // Arrange
            var testString = "test";

            // Act & Assert
            ClassicAssert.IsTrue(testString.IsExists());
        }

        [Test]
        public void IsExists_ShouldReturnFalseForEmptyString()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act & Assert
            ClassicAssert.IsFalse(emptyString.IsExists());
        }

        [Test]
        public void IsLatinSymbols_ShouldReturnTrueForLatinString()
        {
            // Arrange
            var latinString = "Hello";

            // Act & Assert
            ClassicAssert.IsTrue(latinString.IsLatinSymbols());
        }

        [Test]
        public void IsLatinSymbols_ShouldReturnFalseForNonLatinString()
        {
            // Arrange
            var cyrillicString = "Привет";

            // Act & Assert
            ClassicAssert.IsFalse(cyrillicString.IsLatinSymbols());
        }

        [Test]
        public void IsCyrillicSymbols_ShouldReturnTrueForCyrillicString()
        {
            // Arrange
            var cyrillicString = "Привет";

            // Act & Assert
            ClassicAssert.IsTrue(cyrillicString.IsCyrillicSymbols());
        }

        [Test]
        public void IsCyrillicSymbols_ShouldReturnFalseForNonCyrillicString()
        {
            // Arrange
            var latinString = "Hello";

            // Act & Assert
            ClassicAssert.IsFalse(latinString.IsCyrillicSymbols());
        }

        [Test]
        public void IsLetterSymbols_ShouldReturnTrueForStringWithLetters()
        {
            // Arrange
            var letterString = "Hello123";

            // Act & Assert
            ClassicAssert.IsTrue(letterString.IsLetterSymbols());
        }

        [Test]
        public void IsLetterSymbols_ShouldReturnFalseForStringWithoutLetters()
        {
            // Arrange
            var numberString = "12345";

            // Act & Assert
            ClassicAssert.IsFalse(numberString.IsLetterSymbols());
        }

        [Test]
        public void IsDotOrCommaSymbols_ShouldReturnTrueForStringWithDotOrComma()
        {
            // Arrange
            var dotString = "123.45";
            var commaString = "123,45";

            // Act & Assert
            ClassicAssert.IsTrue(dotString.IsDotOrCommaSymbols());
            ClassicAssert.IsTrue(commaString.IsDotOrCommaSymbols());
        }

        [Test]
        public void IsDotOrCommaSymbols_ShouldReturnFalseForStringWithoutDotOrComma()
        {
            // Arrange
            var testString = "12345";

            // Act & Assert
            ClassicAssert.IsFalse(testString.IsDotOrCommaSymbols());
        }

        #endregion

        #region Equal Methods Tests

        [Test]
        public void Equal_ShouldReturnTrueForSameStringsCaseSensitive()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "Hello";

            // Act & Assert
            ClassicAssert.IsTrue(str1.Equal(str2));
        }

        [Test]
        public void Equal_ShouldReturnFalseForDifferentCaseStrings()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "hello";

            // Act & Assert
            ClassicAssert.IsFalse(str1.Equal(str2));
        }

        [Test]
        public void EqualIgnoreCase_ShouldReturnTrueForSameStringsIgnoreCase()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "hello";

            // Act & Assert
            ClassicAssert.IsTrue(str1.EqualIgnoreCase(str2));
        }

        [Test]
        public void EqualIgnoreCase_ShouldReturnFalseForDifferentStrings()
        {
            // Arrange
            var str1 = "Hello";
            var str2 = "World";

            // Act & Assert
            ClassicAssert.IsFalse(str1.EqualIgnoreCase(str2));
        }

        #endregion

        #region Convert Methods Tests

        [Test]
        public void ToFloat_ShouldConvertValidStringToFloat()
        {
            // Arrange
            var numberString = "123.45";

            // Act
            var result = numberString.ToFloat();

            // Assert
            ClassicAssert.AreEqual(123.45f, result, 0.001f);
        }

        [Test]
        public void ToFloatUnchecked_ShouldConvertValidStringToFloat()
        {
            // Arrange
            var numberString = "123.45";

            // Act
            var result = numberString.ToFloatUnchecked();

            // Assert
            ClassicAssert.AreEqual(123.45f, result, 0.001f);
        }

        #endregion

        #region Transform Methods Tests

        [Test]
        public void GetVerticalCopy_ShouldInsertNewLinesBetweenCharacters()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.GetVerticalCopy();

            // Assert
            ClassicAssert.AreEqual("H\ne\nl\nl\no", result);
        }

        [Test]
        public void GetReverseCopy_ShouldReverseString()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.GetReverseCopy();

            // Assert
            ClassicAssert.AreEqual("olleH", result);
        }

        [Test]
        public void InsertSymbols_ShouldInsertSymbolsAtSpecifiedPosition()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.InsertSymbols('!', 3, 2);

            // Assert
            ClassicAssert.AreEqual("Hel!!lo", result);
        }

        [Test]
        public void SetLength_ShouldTruncateLongerString()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.SetLength(3, ' ');

            // Assert
            ClassicAssert.AreEqual("Hel", result);
        }

        [Test]
        public void SetLength_ShouldPadShorterString()
        {
            // Arrange
            var testString = "Hi";

            // Act
            var result = testString.SetLength(5, '*');

            // Assert
            ClassicAssert.AreEqual("Hi***", result);
        }

        #endregion

        #region Calc Methods Tests

        [Test]
        public void GetCountSymbol_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var count = testString.GetCountSymbol('l');

            // Assert
            ClassicAssert.AreEqual(2, count);
        }

        [Test]
        public void GetCountNewLine_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Line1\nLine2\nLine3";

            // Act
            var count = testString.GetCountNewLine();

            // Assert
            ClassicAssert.AreEqual(2, count);
        }

        [Test]
        public void GetCountTab_ShouldReturnCorrectCount()
        {
            // Arrange
            var testString = "Column1\tColumn2\tColumn3";

            // Act
            var count = testString.GetCountTab();

            // Assert
            ClassicAssert.AreEqual(2, count);
        }

        #endregion

        #region Find Methods Tests

        [Test]
        public void Contains_WithComparison_ShouldFindStringCaseInsensitive()
        {
            // Arrange
            var testString = "Hello World";

            // Act & Assert
            ClassicAssert.IsTrue(testString.Contains("world", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void IndexOf_WithList_ShouldFindFirstOccurrence()
        {
            // Arrange
            var testString = "Hello World";
            var searchList = new List<string> { "Foo", "World", "Bar" };

            // Act
            var index = testString.IndexOf(searchList);

            // Assert
            ClassicAssert.AreEqual(6, index);
        }

        #endregion

        #region Remove Methods Tests

        [Test]
        public void RemoveTo_ShouldRemoveCharactersUpToMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveTo("World");

            // Assert
            ClassicAssert.AreEqual("World", result);
        }

        [Test]
        public void RemoveToWith_ShouldRemoveCharactersIncludingMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveToWith("Hello ");

            // Assert
            ClassicAssert.AreEqual("World", result);
        }

        [Test]
        public void RemoveFrom_ShouldRemoveCharactersAfterMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.RemoveFrom("Hello");

            // Assert
            ClassicAssert.AreEqual("Hello", result);
        }

        #endregion

        #region Extract Methods Tests

        [Test]
        public void SubstringTo_ShouldReturnSubstringUpToMatch()
        {
            // Arrange
            var testString = "Hello World";

            // Act
            var result = testString.SubstringTo("World", false);

            // Assert
            ClassicAssert.AreEqual("Hello ", result);
        }

        [Test]
        public void ExtractNumber_ShouldExtractFirstNumber()
        {
            // Arrange
            var testString = "Price: 123 dollars";

            // Act
            var number = testString.ExtractNumber();

            // Assert
            ClassicAssert.AreEqual(123, number);
        }

        [Test]
        public void ExtractString_ShouldExtractBetweenDelimiters()
        {
            // Arrange
            var testString = "Name: [John] Doe";

            // Act
            var result = testString.ExtractString("[", "]");

            // Assert
            ClassicAssert.AreEqual("John", result);
        }

        #endregion

        #region Word Methods Tests

        [Test]
        public void ToWordUpper_ShouldCapitalizeFirstLetter()
        {
            // Arrange
            var testString = "hello";

            // Act
            var result = testString.ToWordUpper();

            // Assert
            ClassicAssert.AreEqual("Hello", result);
        }

        [Test]
        public void ToWordLower_ShouldLowercaseFirstLetter()
        {
            // Arrange
            var testString = "Hello";

            // Act
            var result = testString.ToWordLower();

            // Assert
            ClassicAssert.AreEqual("hello", result);
        }

        [Test]
        public void ToTitleCase_ShouldConvertUnderscoresToCamelCase()
        {
            // Arrange
            var testString = "HELLO_WORLD";

            // Act
            var result = testString.ToTitleCase();

            // Assert
            ClassicAssert.AreEqual("HelloWorld", result);
        }

        [Test]
        public void ToConstCase_ShouldConvertCamelCaseToUnderscores()
        {
            // Arrange
            var testString = "HelloWorld";

            // Act
            var result = testString.ToConstCase();

            // Assert
            ClassicAssert.AreEqual("HELLO_WORLD", result);
        }

        #endregion
    }
}