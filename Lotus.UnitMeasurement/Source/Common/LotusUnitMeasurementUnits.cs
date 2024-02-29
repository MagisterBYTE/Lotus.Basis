using System;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений для типов единиц измерения.
    /// </summary>
    public static class XUnitType
    {
        /// <summary>
        /// Получение соответствующего типа измерения от указанного единицы измерения.
        /// </summary>
        /// <param name="unitType">Единица измерения.</param>
        /// <returns>Тип измерения.</returns>
        public static TMeasurementType GetMeasurementType(Enum unitType)
        {
            var type = unitType.GetType();
            var measurement_type = TMeasurementType.Undefined;
            switch (type.Name)
            {
                case nameof(TUnitThing):
                    {
                        measurement_type = TMeasurementType.Thing;
                    }
                    break;
                case nameof(TUnitLength):
                    {
                        measurement_type = TMeasurementType.Length;
                    }
                    break;
                case nameof(TUnitArea):
                    {
                        measurement_type = TMeasurementType.Area;
                    }
                    break;
                default:
                    break;
            }

            return measurement_type;
        }
    }
    /**@}*/
}