﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема работы с датой и временем
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDateTimePeriod.cs
*		Работа с периодом.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreDateTime
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для объектов которые поддерживают период
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusDatePeriod
		{
			/// <summary>
			/// Дата начала периода
			/// </summary>
#if UNITY_2017_1_OR_NEWER
			DateTime BeginPeriodDate { get; set; }
#else
			DateOnly BeginPeriodDate { get; set; }
#endif

			/// <summary>
			/// Дата окончания периода
			/// </summary>
#if UNITY_2017_1_OR_NEWER
			DateTime? EndPeriodDate { get; set; }
#else
			DateOnly? EndPeriodDate { get; set; }
#endif
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий дополнительные методы для работы с типом <see cref="ILotusDatePeriod"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDatePeriodExtension
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение текущей даты в текстовом формате UTC
			/// </summary>
			/// <param name="this">Объект</param>
			/// <returns>Дата в текстовом формате UTC</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetPeriodOfText(this ILotusDatePeriod @this)
			{
				if(@this.EndPeriodDate != null)
				{
					return @this.BeginPeriodDate.ToString(XDateFormats.Default) 
						+ "г. - " + @this.EndPeriodDate.GetValueOrDefault().ToString(XDateFormats.Default) + "г.";
				}
				else
				{
					return @this.BeginPeriodDate.ToString(XDateFormats.Default) + "г. - по н.в.";
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================