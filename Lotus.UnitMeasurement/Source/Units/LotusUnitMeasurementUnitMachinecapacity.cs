using System.ComponentModel.DataAnnotations;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Единица измерения машиноемкости.
    /// </summary>
    public enum TUnitMachinecapacity
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        [Display(Name = "неоп")]
        Undefined = 0,

        /// <summary>
        /// Машино-часы.
        /// </summary>
        [Display(Name = "маш/час")]
        MachineHours = 1,
    }
    /**@}*/
}