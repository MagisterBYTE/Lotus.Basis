//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusPageInfoTesting.cs
*		Тестирование методов пангинации.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Repository;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования методов пангинации
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPageInfoTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="XPageInfoHelpers"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Fact]
			public static void TestPageInfoHelpers()
			{
				var pages20_100 = XPageInfoHelpers.ToPageInfo(20, 100);

				Assert.Single(pages20_100);
				Assert.Equal(20, pages20_100[0].PageSize);
				Assert.Equal(0, pages20_100[0].PageNumber);

				var pages41_10 = XPageInfoHelpers.ToPageInfo(41, 10);

				Assert.Equal(5, pages41_10.Length);

				Assert.Equal(0, pages41_10[0].PageNumber);
				Assert.Equal(10, pages41_10[0].PageSize);

				Assert.Equal(10, pages41_10[1].PageNumber);
				Assert.Equal(10, pages41_10[1].PageSize);

				Assert.Equal(20, pages41_10[2].PageNumber);
				Assert.Equal(10, pages41_10[2].PageSize);

				Assert.Equal(30, pages41_10[3].PageNumber);
				Assert.Equal(10, pages41_10[3].PageSize);

				Assert.Equal(40, pages41_10[4].PageNumber);
				Assert.Equal(1, pages41_10[4].PageSize);

				var pages501_9 = XPageInfoHelpers.ToPageInfo(501, 9);

				Assert.Equal(56, pages501_9.Length);

				Assert.Equal(0, pages501_9[0].PageNumber);
				Assert.Equal(9, pages501_9[0].PageSize);

				Assert.Equal(9, pages501_9[1].PageNumber);
				Assert.Equal(9, pages501_9[1].PageSize);

				Assert.Equal(495, pages501_9[55].PageNumber);
				Assert.Equal(6, pages501_9[55].PageSize);
			}
		}
	}
}
//=====================================================================================================================