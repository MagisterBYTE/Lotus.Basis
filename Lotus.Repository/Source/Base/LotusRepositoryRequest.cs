﻿//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryRequest.cs
*		Определение интерфейса и моделей для запроса данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using Lotus.Core;
using System;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryBase
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый интерфейс запроса данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusRequest
		{
			/// <summary>
			/// Информация о странице
			/// </summary>
			ILotusPageInfoRequest? PageInfo { get; set; }

			/// <summary>
			/// Параметры сортировки данных
			/// </summary>
			ILotusSortProperty[]? Sorting { get; set; }

			/// <summary>
			/// Параметры фильтрации данных
			/// </summary>
			ILotusFilterProperty[]? Filtering { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для запроса данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class Request
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Параметры запрашиваемой страницы
			/// </summary>
			public CPageInfoRequest? PageInfo { get; set; } 

			/// <summary>
			/// Параметры сортировки данных
			/// </summary>
			public CSortProperty[]? Sorting { get; set; }

			/// <summary>
			/// Параметры фильтрации данных
			/// </summary>
			public CFilterProperty[]? Filtering { get; set; }
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка целочисленных значений указанного свойства
			/// </summary>
			/// <param name="propertName">Свойство</param>
			/// <returns>Список целочисленных значений и функция фильтрации</returns>
			//---------------------------------------------------------------------------------------------------------
			public (Int32[], TFilterFunction) GetIdsByInteger(String propertName)
			{
				var filterProperty = Filtering?.FirstOrDefault(x=> x.PropertyName == propertName);
				if(filterProperty != null && filterProperty.Values != null && filterProperty.Values.Length > 0)
				{
					var ids = new Int32[filterProperty.Values.Length];
					for (var i = 0; i < filterProperty.Values.Length; i++)
					{
						ids[i] = XNumbers.ParseInt(filterProperty.Values[i]);
					}
					return (ids, filterProperty.Function);
				}

				return (Array.Empty<Int32>(), TFilterFunction.Equals);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================