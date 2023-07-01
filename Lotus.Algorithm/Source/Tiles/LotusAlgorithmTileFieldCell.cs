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
		public class CFieldCellBase : ILotusFieldCell, ILotusViewItemOwner
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal ILotusField mOwnerField;
			protected internal Int32 mCellLayer;
			protected internal Int32 mCellCoordinateX;
			protected internal Int32 mCellCoordinateY;
			protected internal Boolean mIsCellBorderLeft;
			protected internal Boolean mIsCellBorderRight;
			protected internal Boolean mIsCellBorderUp;
			protected internal Boolean mIsCellBorderDown;
			protected internal Int32 mCellStatus;
			protected internal ILotusFieldCell mCellLeft;
			protected internal ILotusFieldCell mCellRight;
			protected internal ILotusFieldCell mCellTop;
			protected internal ILotusFieldCell mCellBottom;
			protected internal System.Object mVisualElement;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Владелец - поле которому принадлежат ячейка
			/// </summary>
			public ILotusField OwnerField 
			{ 
				get { return mOwnerField; }
				set { mOwnerField = value; } 
			}

			/// <summary>
			/// Слой для расположения ячейки
			/// </summary>
			public Int32 CellLayer
			{
				get { return mCellLayer; }
				set { mCellLayer = value; }
			}

			/// <summary>
			/// Координата ячейки по X
			/// </summary>
			public Int32 CellCoordinateX
			{
				get { return mCellCoordinateX; }
				set { mCellCoordinateX = value; }
			}

			/// <summary>
			/// Координата ячейки по Y
			/// </summary>
			public Int32 CellCoordinateY
			{
				get { return mCellCoordinateY; }
				set { mCellCoordinateY = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля слева
			/// </summary>
			public Boolean IsCellBorderLeft
			{
				get { return mIsCellBorderLeft; }
				set { mIsCellBorderLeft = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля справа
			/// </summary>
			public Boolean IsCellBorderRight
			{
				get { return mIsCellBorderRight; }
				set { mIsCellBorderRight = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля сверху
			/// </summary>
			public Boolean IsCellBorderUp
			{
				get { return mIsCellBorderUp; }
				set { mIsCellBorderUp = value; }
			}

			/// <summary>
			/// Наличие границы у ячейки поля снизу
			/// </summary>
			public Boolean IsCellBorderDown
			{
				get { return mIsCellBorderDown; }
				set { mIsCellBorderDown = value; }
			}

			/// <summary>
			/// Статус ячейки
			/// </summary>
			public Int32 CellStatus
			{
				get { return mCellStatus; }
				set { mCellStatus = value; }
			}

			/// <summary>
			/// Смежная ячейка расположенная слева
			/// </summary>
			public ILotusFieldCell CellLeft
			{
				get { return mCellLeft; }
			}

			/// <summary>
			/// Смежная ячейка расположенная справа
			/// </summary>
			public ILotusFieldCell CellRight
			{
				get { return mCellRight; }
			}

			/// <summary>
			/// Смежная ячейка расположенная сверху
			/// </summary>
			public ILotusFieldCell CellTop
			{
				get { return mCellTop; }
			}

			/// <summary>
			/// Смежная ячейка расположенная снизу
			/// </summary>
			public ILotusFieldCell CellBottom
			{
				get { return mCellBottom; }
			}

			/// <summary>
			/// Элемент визуализации для отображения ячейки поля
			/// </summary>
			public System.Object VisualElement
			{
				get { return mVisualElement; }
				set { mVisualElement = value; }
			}
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Элемент отображения
			/// </summary>
			public ILotusViewItem OwnerViewItem { get; set; }
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
				mCellCoordinateX = cellCoordinateX;
				mCellCoordinateY = cellCoordinateY;
				mOwnerField = ownerField;
				//mCellLeft = mOwnerField.GetCell(mCellCoordinateX - 1, mCellCoordinateY);
				//mCellRight = mOwnerField.GetCell(mCellCoordinateX + 1, mCellCoordinateY);
				//mCellTop = mOwnerField.GetCell(mCellCoordinateX, mCellCoordinateY + 1);
				//mCellBottom = mOwnerField.GetCell(mCellCoordinateX, mCellCoordinateY - 1);
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