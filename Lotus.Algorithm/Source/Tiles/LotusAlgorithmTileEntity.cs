namespace Lotus.Algorithm
{
    /** \addtogroup AlgorithmTile
	*@{*/
    /// <summary>
    /// Интерфейс для определения базового тайла.
    /// </summary>
    public interface ILotusTile
    {
        /// <summary>
        /// Владелец - фигура.
        /// </summary>
        ILotusShapeTile OwnerShape { get; set; }

        /// <summary>
        /// Координаты тайла по X.
        /// </summary>
        int TileCoordinateX { get; set; }

        /// <summary>
        /// Координаты тайла по Y.
        /// </summary>
        int TileCoordinateY { get; set; }
    }
    /**@}*/
}