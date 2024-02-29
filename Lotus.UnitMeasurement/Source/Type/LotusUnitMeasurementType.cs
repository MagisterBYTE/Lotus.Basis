namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Определение доступных типов измерения.
    /// </summary>
    public enum TMeasurementType
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Единица измерения вещей/предметов/абстаркций <see cref="TUnitThing"/>.
        /// </summary>
        Thing,

        /// <summary>
        /// Единица измерения длины <see cref="TUnitLength"/>.
        /// </summary>
        Length,

        /// <summary>
        /// Единица измерения площади <see cref="TUnitArea"/>.
        /// </summary>
        Area,

        /// <summary>
        /// Единица измерения объема <see cref="TUnitVolume"/>.
        /// </summary>
        Volume,

        /// <summary>
        /// Единица измерения массы <see cref="TUnitMass"/>.
        /// </summary>
        Mass,

        /// <summary>
        /// Единица измерения времени <see cref="TUnitTime"/>.
        /// </summary>
        Time,

        /// <summary>
        /// Единица измерения трудоемкости <see cref="TUnitLaborintensity"/>.
        /// </summary>
        Laborintensity,

        /// <summary>
        /// Единица измерения машиноемкости <see cref="TUnitMachinecapacity"/>.
        /// </summary>
        Machinecapacity
    }
    /**@}*/
}