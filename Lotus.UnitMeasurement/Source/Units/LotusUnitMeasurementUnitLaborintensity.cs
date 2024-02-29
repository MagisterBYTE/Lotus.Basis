using System.ComponentModel.DataAnnotations;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Единица измерения трудоемкости.
    /// </summary>
    public enum TUnitLaborintensity
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        [Display(Name = "неоп")]
        Undefined = 0,

        /// <summary>
        /// Человеко-часы.
        /// </summary>
        [Display(Name = "чел/час")]
        ManHours = 1,
    }
    /**@}*/
}