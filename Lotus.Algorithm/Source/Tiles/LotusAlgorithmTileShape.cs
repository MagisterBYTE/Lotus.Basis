using System.Collections.Generic;

namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmTile
	*@{*/
    /// <summary>
    /// Интерфейс фигуры состоящей из тайлов.
    /// </summary>
    public interface ILotusShapeTile
    {
        /// <summary>
        /// Владелец - поле которому принадлежат ячейки.
        /// </summary>
        ILotusField OwnerField { get; set; }

        /// <summary>
        /// Список тайлов который образуют данную фигуру.
        /// </summary>
        IList<ILotusTile> Tiles { get; set; }

        /// <summary>
        /// Текущий слой в котором расположен тайл.
        /// </summary>
        int ShapeTileLayer { get; set; }
    }
    /**@}*/
}