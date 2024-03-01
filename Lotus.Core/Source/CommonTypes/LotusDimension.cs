namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Тип компонента измерения пространства.
    /// </summary>
    public enum TDimensionComponent
    {
        /// <summary>
        /// Координата X.
        /// </summary>
        X = 1,

        /// <summary>
        /// Координата Y.
        /// </summary>
        Y = 2,

        /// <summary>
        /// Координата Z.
        /// </summary>
        Z = 4,

        /// <summary>
        /// Координата W.
        /// </summary>
        W = 8
    }

    /// <summary>
    /// Выбор типа компонента измерения пространства.
    /// </summary>
    public enum TDimensionComponentSelect
    {
        /// <summary>
        /// Компонента отсутствует.
        /// </summary>
        None = 0,

        /// <summary>
        /// Координата X.
        /// </summary>
        X = 1,

        /// <summary>
        /// Координата Y.
        /// </summary>
        Y = 2,

        /// <summary>
        /// Координата Z.
        /// </summary>
        Z = 4,

        /// <summary>
        /// Координата W.
        /// </summary>
        W = 8
    }

    /// <summary>
    /// Тип плоскости пространства.
    /// </summary>
    public enum TDimensionPlane
    {
        /// <summary>
        /// Плоскость XZ.
        /// </summary>
        /// <remarks>
        /// Вид сверху (Top).
        /// </remarks>
        XZ = 1,

        /// <summary>
        /// Плоскость ZY.
        /// </summary>
        /// <remarks>
        /// Вид справа (Right).
        /// </remarks>
        ZY = 2,

        /// <summary>
        /// Плоскость XY.
        /// </summary>
        /// <remarks>
        /// Вид спереди (Front).
        /// </remarks>
        XY = 4
    }

    /// <summary>
    /// Выбор типа плоскости пространства.
    /// </summary>
    public enum TDimensionPlaneSelect
    {
        /// <summary>
        /// Плоскость отсутствует.
        /// </summary>
        None = 0,

        /// <summary>
        /// Плоскость XZ.
        /// </summary>
        /// <remarks>
        /// Вид сверху (Top).
        /// </remarks>
        XZ = 1,

        /// <summary>
        /// Плоскость ZY.
        /// </summary>
        /// <remarks>
        /// Вид справа (Right).
        /// </remarks>
        ZY = 2,

        /// <summary>
        /// Плоскость XY.
        /// </summary>
        /// <remarks>
        /// Вид спереди (Front).
        /// </remarks>
        XY = 4
    }
    /**@}*/
}