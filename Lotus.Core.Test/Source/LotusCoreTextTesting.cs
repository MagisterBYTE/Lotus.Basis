//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема текстовых данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreTextTesting.cs
*		Тестирование методов подсистемы текстовых данных модуля базового ядра.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования методов подсистемы текстовых данных базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreTextTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов подсистемы текстовых данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestText()
			{
				CTextLine textLine = "1111";
				textLine += "222_" + "222";

				Assert.AreEqual(textLine.RawString, "1111222_222");

				textLine.CharFirst = 'w';
				textLine.CharLast = 'w';

				Assert.AreEqual(textLine.RawString, "w111222_22w");

				textLine.SetLength(4);
				Assert.AreEqual(textLine.RawString, "w111");

				textLine = "w111222_22w";
				textLine.SetLength(14);
				Assert.AreEqual(textLine.RawString, "w111222_22wwww");

				textLine = "===";
				textLine.SetLengthAndLastChar(8, ']');
				Assert.AreEqual(textLine.RawString, "=======]");

				Assert.AreEqual(textLine.Indent, 0);
				textLine = "===";
				textLine.Indent = 2;
				Assert.AreEqual(textLine.Indent, 2);
				Assert.AreEqual(textLine.RawString, "\t\t===");

				textLine.Indent = 1;
				Assert.AreEqual(textLine.Indent, 1);
				Assert.AreEqual(textLine.RawString, "\t===");

				textLine.Indent = 4;
				Assert.AreEqual(textLine.Indent, 4);
				Assert.AreEqual(textLine.RawString, "\t\t\t\t===");

				textLine.CharFirst = '4';
				Assert.AreEqual(textLine.Indent, 0);
				Assert.AreEqual(textLine.RawString, "4\t\t\t===");

				textLine.CharFirst = XChar.Tab;
				textLine.CharSecond = '1';
				Assert.AreEqual(textLine.Indent, 1);
				Assert.AreEqual(textLine.RawString, "\t1\t\t===");

				textLine = "12345";
				Assert.AreEqual(textLine.RawString == "12345", true);
			}
		}
	}
}
//=====================================================================================================================