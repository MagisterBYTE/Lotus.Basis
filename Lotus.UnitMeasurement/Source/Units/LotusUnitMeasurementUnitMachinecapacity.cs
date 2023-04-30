﻿//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Единицы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementUnitMachinecapacity.cs
*		Определение единиц для измерения машиноемкости.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel.DataAnnotations;
//=====================================================================================================================
namespace Lotus
{
	namespace UnitMeasurement
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup UnitMeasurement
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Единица измерения машиноемкости
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TUnitMachinecapacity
		{
			/// <summary>
			/// Не определена
			/// </summary>
			[Display(Name = "неоп")]
			Undefined = 0,

			/// <summary>
			/// Машино-часы
			/// </summary>
			[Display(Name = "маш/час")]
			Machine_hours = 1,
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================