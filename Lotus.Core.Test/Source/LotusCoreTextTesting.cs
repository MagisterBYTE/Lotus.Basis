#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс для тестирования методов подсистемы текстовых данных базового ядра.
    /// </summary>
    public static class XCoreTextTesting
    {
        /// <summary>
        /// Тестирование методов подсистемы текстовых данных.
        /// </summary>
        [Test]
        public static void TestText()
        {
            CTextLine textLine = "1111";
            textLine += "222_" + "222";

            ClassicAssert.AreEqual(textLine.RawString, "1111222_222");

            textLine.CharFirst = 'w';
            textLine.CharLast = 'w';

            ClassicAssert.AreEqual(textLine.RawString, "w111222_22w");

            textLine.SetLength(4);
            ClassicAssert.AreEqual(textLine.RawString, "w111");

            textLine = "w111222_22w";
            textLine.SetLength(14);
            ClassicAssert.AreEqual(textLine.RawString, "w111222_22wwww");

            textLine = "===";
            textLine.SetLengthAndLastChar(8, ']');
            ClassicAssert.AreEqual(textLine.RawString, "=======]");

            ClassicAssert.AreEqual(textLine.Indent, 0);
            textLine = "===";
            textLine.Indent = 2;
            ClassicAssert.AreEqual(textLine.Indent, 2);
            ClassicAssert.AreEqual(textLine.RawString, "\t\t===");

            textLine.Indent = 1;
            ClassicAssert.AreEqual(textLine.Indent, 1);
            ClassicAssert.AreEqual(textLine.RawString, "\t===");

            textLine.Indent = 4;
            ClassicAssert.AreEqual(textLine.Indent, 4);
            ClassicAssert.AreEqual(textLine.RawString, "\t\t\t\t===");

            textLine.CharFirst = '4';
            ClassicAssert.AreEqual(textLine.Indent, 0);
            ClassicAssert.AreEqual(textLine.RawString, "4\t\t\t===");

            textLine.CharFirst = XChar.Tab;
            textLine.CharSecond = '1';
            ClassicAssert.AreEqual(textLine.Indent, 1);
            ClassicAssert.AreEqual(textLine.RawString, "\t1\t\t===");

            textLine = "12345";
            ClassicAssert.AreEqual(textLine.RawString == "12345", true);
        }
    }
}