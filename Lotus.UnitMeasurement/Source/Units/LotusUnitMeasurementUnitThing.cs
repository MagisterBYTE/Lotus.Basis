using System.ComponentModel.DataAnnotations;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Единица измерения вещей.
    /// </summary>
    public enum TUnitThing
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        [Display(Name = "неоп")]
        Undefined = 0,

        /// <summary>
        /// Штука.
        /// </summary>
        [Display(Name = "шт.")]
        Thing = 1,

        /// <summary>
        /// Комплект.
        /// </summary>
        [Display(Name = "комп.")]
        Kit,
    }
    /**@}*/
}