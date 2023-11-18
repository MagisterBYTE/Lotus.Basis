//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Подсистема тайлов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmTileFieldCell.cs
*		Определение концепции ячейки поля.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup AlgorithmTile
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс для определения ячейки поля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusFieldCell
		{
			/// <summary>
			/// Владелец - поле которому принадлежат ячейка
			/// </summary>
			ILotusField OwnerField { get; set; }

			/// <summary>
			/// Слой для расположения ячейки
			/// </summary>
			Int32 CellLayer { get; set; }

			/// <summary>
			/// Координата ячейки по X
			/// </summary>
			Int32 CellCoordinateX { get; set; }

			/// <summary>
			/// Координата ячейки по Y
			/// </summary>
			Int32 CellCoordinateY { get; set; }

			/// <summary>
			/// Наличие границы у ячейки поля слева
			/// </summary>
			Boolean IsCellBorderLeft { get; set; }

			/// <summary>
			/// Наличие границы у ячейки поля справа
			/// </summary>
			Boolean IsCellBorderRight { get; set; }

			/// <summary>
			/// Наличие границы у ячейки поля сверху
			/// </summary>
			Boolean IsCellBorderUp { get; set; }

			/// <summary>
			/// Наличие границы у ячейки поля снизу
			/// </summary>
			Boolean IsCellBorderDown { get; set; }

			/// <summary>
			/// Статус ячейки
			/// </summary>
			Int32 CellStatus { get; set; }

			/// <summary>
			/// Смежная ячейка расположенная слева
			/// </summary>
			ILotusFieldCell CellLeft { get; }

			/// <summary>
			/// Смежная ячейка расположенная справа
			/// </summary>
			ILotusFieldCell CellRight { get; }

			/// <summary>
			/// Смежная ячейка расположенная сверху
			/// </summary>
			ILotusFieldCell CellTop { get; }

			/// <summary>
			/// Смежная ячейка расположенная снизу
			/// </summary>
			ILotusFieldCell CellBottom { get; }

			/// <summary>
			/// Элемент визуализации для отображения ячейки поля
			/// </summary>
			System.Object VisualElement { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс реализующий функциональность ячейки поля
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CFieldCellBase : ILotusFieldCell
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal ILotusField _ownerField;
			protected internal Int32 _cellLayer;
			protected internal Int32 _cellCoordinateX;
			protected internal Int32 _cellCoordinateY;
			protected internal Boolean _isCellBorderLeft;
			protected internal Boolean _isCellBorderRight;
			protected internal Boolean _isCellBorderUp;
			protected internal Boolean _isCellBorderDown;
			protected internal Int32 _cellStatus;
			protected internal ILotusFieldCell _cellLeft;
			protected internal ILotusFieldCell _cellRight;
			protected internal ILotusFieldCell _cellTop;
			protected internal ILotusFieldCell _cellBottom;
			protected internal System.Object _visualElement;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Владелец - поле которому принадлежат ячейка
			/// </summary>
			public ILotusField OwnerField 
			{ 
				get { return _ownerField; }
				set { _ownerField = value; } 
			}

			/// <summary>
			/// Слой для расположения ячейки
			/// </summary>
			public Int32 CellLayer
			{
				get { return _cellLayer; }
				set { _cellLayer = value; }
			}

			/// <summary>
			/// Координата ячейки по X
			/// </summary>
			public Int32 CellCoordinateX
			{
				get { return _cellCoordinateX; }
				set { _cellCoordinateX = value; }
			}

			/// <summary>
			/// Координата ячейки по Y
			/// </summary>
			public Int32 CellCoordinateY
			{
				get { return _cellCoordinateY; }
				set { _cellCoordinateY = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля слева
			/// </summary>
			public Boolean IsCellBorderLeft
			{
				get { return _isCellBorderLeft; }
				set { _isCellBorderLeft = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля справа
			/// </summary>
			public Boolean IsCellBorderRight
			{
				get { return _isCellBorderRight; }
				set { _isCellBorderRight = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля сверху
			/// </summary>
			public Boolean IsCellBorderUp
			{
				get { return _isCellBorderUp; }
				set { _isCellBorderUp = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля снизу
			/// </summary>
			public Boolean IsCellBorderDown
			{
				get { return _isCellBorderDown; }
				set { _isCellBorderDown = value; }
			}

			/// <summary>
			/// Статус ячейки
			/// </summary>
			public Int32 CellStatus
			{
				get { return _cellStatus; }
				set { _cellStatus = value; }
			}

			/// <summary>
			/// Смежная ячейка расположенная слева
			/// </summary>
			public ILotusFieldCell CellLeft
			{
				get { return _cellLeft; }
			}

			/// <summary>
			/// Смежная ячейка расположенная справа
			/// </summary>
			public ILotusFieldCell CellRight
			{
				get { return _cellRight; }
			}

			/// <summary>
			/// Смежная ячейка расположенная сверху
			/// </summary>
			public ILotusFieldCell CellTop
			{
				get { return _cellTop; }
			}

			/// <summary>
			/// Смежная ячейка расположенная снизу
			/// </summary>
			public ILotusFieldCell CellBottom
			{
				get { return _cellBottom; }
			}

			/// <summary>
			/// Элемент визуализации для отображения ячейки поля
			/// </summary>
			public System.Object VisualElement
			{
				get { return _visualElement; }
				set { _visualElement = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CFieldCellBase()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="cellCoordinateX">Координата ячейки по X</param>
			/// <param name="cellCoordinateY">Координата ячейки по Y</param>
			/// <param name="ownerField">Владелец - поле которому принадлежат ячейка</param>
			//---------------------------------------------------------------------------------------------------------
			public CFieldCellBase(Int32 cellCoordinateX, Int32 cellCoordinateY, ILotusField ownerField)
			{
				_cellCoordinateX = cellCoordinateX;
				_cellCoordinateY = cellCoordinateY;
				_ownerField = ownerField;
				//mCellLeft = _ownerField.GetCell(_cellCoordinateX - 1, _cellCoordinateY);
				//mCellRight = _ownerField.GetCell(_cellCoordinateX + 1, _cellCoordinateY);
				//mCellTop = _ownerField.GetCell(_cellCoordinateX, _cellCoordinateY + 1);
				//mCellBottom = _ownerField.GetCell(_cellCoordinateX, _cellCoordinateY - 1);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================

			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================