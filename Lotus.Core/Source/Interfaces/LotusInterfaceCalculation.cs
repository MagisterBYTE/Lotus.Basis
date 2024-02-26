namespace Lotus.Core
{
    /** \addtogroup CoreInterfaces
	*@{*/
    /// <summary>
    /// Определение интерфейса для объектов поддерживающих понятие неизменяемости (константности).
    /// </summary>
    public interface ILotusConstantable
    {
        /// <summary>
        /// Статус константного объекта.
        /// </summary>
        bool IsConst { get; set; }
    }

    /// <summary>
    /// Определение интерфейса для объектов которые могут не участвовать в расчетах.
    /// </summary>
    public interface ILotusNotCalculation
    {
        /// <summary>
        /// Не учитывать объект в расчетах.
        /// </summary>
        bool NotCalculation { get; set; }
    }

    /// <summary>
    /// Определение интерфейса для объектов которые поддерживают верификацию данных.
    /// </summary>
    public interface ILotusVerified
    {
        /// <summary>
        /// Статус верификации данных.
        /// </summary>
        bool IsVerified { get; set; }
    }
    /**@}*/
}