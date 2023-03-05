﻿//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Подсистема тайлов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmTileVisual.cs
*		Интерфейсы для визуализации тайловой подсистемы.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup AlgorithmTile
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для визуализации поля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusFieldVisual
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Поле
			/// </summary>
			ILotusField Field { get; set; }

			/// <summary>
			/// Ширина ячейки поля для визуализации
			/// </summary>
			Single CellWidth { get; set; }

			/// <summary>
			/// Высота ячейки поля для визуализации
			/// </summary>
			Single CellHeight { get; set; }

			/// <summary>
			/// Расстояние между ячейками по X поля для визуализации
			/// </summary>
			Single CellSpaceX { get; set; }

			/// <summary>
			/// Расстояние между ячейками по Y поля для визуализации
			/// </summary>
			Single CellSpaceY { get; set; }

			/// <summary>
			/// Событие нажатие на ячейку поля
			/// </summary>
			Action<ILotusFieldCell> OnCellDown { get; set; }

			/// <summary>
			/// Событие отпускание ячейки поля
			/// </summary>
			Action<ILotusFieldCell> OnCellUp { get; set; }

			/// <summary>
			/// Событие щелчок по ячейки поля
			/// </summary>
			Action<ILotusFieldCell> OnCellClick { get; set; }
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================