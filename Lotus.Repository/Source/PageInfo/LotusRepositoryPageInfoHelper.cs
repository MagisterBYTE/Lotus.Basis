﻿//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема постраничного разделения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryPageInfoHelper.cs
*		Определение типов постраничного запроса/получения данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryPageInfo
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс, реализующий вспомогательные методы для постраничного запроса данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPageInfoHelpers
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение массива постраничного запроса данных 
			/// </summary>
			/// <param name="totalSize">Общее количество данных</param>
			/// <param name="pageSize">Размер страницы</param>
			/// <returns>Массив для постраничного запроса данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPageInfoRequest[] ToPageInfo(Int32 totalSize, Int32 pageSize)
			{
				if (totalSize <= pageSize)
				{
					return new[] { new CPageInfoRequest { PageNumber = 0, PageSize = totalSize, } };
				}

				var PageSize = totalSize / pageSize;
				var residue = totalSize % pageSize;

				var CPageInfoRequests = new List<CPageInfoRequest>();

				for (var i = 0; i < PageSize; i++)
				{
					var CPageInfoRequest = new CPageInfoRequest()
					{
						PageNumber = i * pageSize,
						PageSize = pageSize,
					};
					CPageInfoRequests.Add(CPageInfoRequest);
				}

				if (residue > 0)
				{
					var CPageInfoRequest = new CPageInfoRequest()
					{
						PageNumber = PageSize * pageSize,
						PageSize = residue,
					};

					CPageInfoRequests.Add(CPageInfoRequest);
				}

				return CPageInfoRequests.ToArray();
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================