using System;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmTile
	*@{*/
    /// <summary>
    /// Интерфейс для визуализации поля.
    /// </summary>
    public interface ILotusFieldVisual
    {
        #region Properties
        /// <summary>
        /// Поле.
        /// </summary>
        ILotusField Field { get; set; }

        /// <summary>
        /// Ширина ячейки поля для визуализации.
        /// </summary>
        float CellWidth { get; set; }

        /// <summary>
        /// Высота ячейки поля для визуализации.
        /// </summary>
        float CellHeight { get; set; }

        /// <summary>
        /// Расстояние между ячейками по X поля для визуализации.
        /// </summary>
        float CellSpaceX { get; set; }

        /// <summary>
        /// Расстояние между ячейками по Y поля для визуализации.
        /// </summary>
        float CellSpaceY { get; set; }

        /// <summary>
        /// Событие нажатие на ячейку поля.
        /// </summary>
        Action<ILotusFieldCell> OnCellDown { get; set; }

        /// <summary>
        /// Событие отпускание ячейки поля.
        /// </summary>
        Action<ILotusFieldCell> OnCellUp { get; set; }

        /// <summary>
        /// Событие щелчок по ячейки поля.
        /// </summary>
        Action<ILotusFieldCell> OnCellClick { get; set; }
        #endregion
    }
    /**@}*/
}