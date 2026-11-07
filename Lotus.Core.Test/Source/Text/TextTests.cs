#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace Lotus.Core.Text
{
    /// <summary>
    /// Статический класс для тестирования методов подсистемы текстовых данных базового ядра.
    /// </summary>
    public static class TextTests
    {
        /// <summary>
        /// Тестирование методов подсистемы текстовых данных.
        /// </summary>
        [Fact]
        public static void TestText()
        {
            CTextLine textLine = "1111";
            textLine += "222_" + "222";

            Assert.Equal(textLine.RawString, "1111222_222");

            textLine.CharFirst = 'w';
            textLine.CharLast = 'w';

            Assert.Equal(textLine.RawString, "w111222_22w");

            textLine.SetLength(4);
            Assert.Equal(textLine.RawString, "w111");

            textLine = "w111222_22w";
            textLine.SetLength(14);
            Assert.Equal(textLine.RawString, "w111222_22wwww");

            textLine = "===";
            textLine.SetLengthAndLastChar(8, ']');
            Assert.Equal(textLine.RawString, "=======]");

            Assert.Equal(textLine.Indent, 0);
            textLine = "===";
            textLine.Indent = 2;
            Assert.Equal(textLine.Indent, 2);
            Assert.Equal(textLine.RawString, "\t\t===");

            textLine.Indent = 1;
            Assert.Equal(textLine.Indent, 1);
            Assert.Equal(textLine.RawString, "\t===");

            textLine.Indent = 4;
            Assert.Equal(textLine.Indent, 4);
            Assert.Equal(textLine.RawString, "\t\t\t\t===");

            textLine.CharFirst = '4';
            Assert.Equal(textLine.Indent, 0);
            Assert.Equal(textLine.RawString, "4\t\t\t===");

            textLine.CharFirst = XCharHelper.Tab;
            textLine.CharSecond = '1';
            Assert.Equal(textLine.Indent, 1);
            Assert.Equal(textLine.RawString, "\t1\t\t===");

            textLine = "12345";
            Assert.Equal(textLine.RawString == "12345", true);
        }
    }
}
