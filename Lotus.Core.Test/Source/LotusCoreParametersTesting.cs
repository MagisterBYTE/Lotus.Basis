﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема параметрических объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreParametersTesting.cs
*		Тестирование подсистемы параметрических объектов модуля базового ядра.
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
		/// Статический класс для тестирования подсистемы параметрических объектов модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreParametersTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование подсистемы параметрических объектов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestParameters()
			{
				var parameter_inner = new CParameters(223, new CParameterString("Имя", "Эра"));

				var parameterObject = new CParameters("Программа",
					new CParameterString("Имя", "Эра"), parameter_inner);

				Assert.AreEqual(parameterObject.Value[0].Name, "Имя");
				Assert.AreEqual(parameterObject.Value[0].Value, "Эра");
			}
		}
	}
}
//=====================================================================================================================