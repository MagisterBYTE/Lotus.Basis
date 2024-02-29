using System.Collections.Generic;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Дескриптор для описания единицы измерения времени.
    /// </summary>
    public class CUnitDescriptorTime : CUnitDescriptor<TUnitTime>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами.
        /// </summary>
        public CUnitDescriptorTime()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="unit">Единица измерения.</param>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        public CUnitDescriptorTime(TUnitTime unit, double сoeffToBase)
            : base(unit, сoeffToBase)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="unit">Единица измерения.</param>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        /// <param name="internationalAbbv">Международная аббревиатура.</param>
        /// <param name="rusAbbv">Русская аббревиатура.</param>
        public CUnitDescriptorTime(TUnitTime unit, double сoeffToBase, string internationalAbbv, string rusAbbv)
            : base(unit, сoeffToBase, internationalAbbv, rusAbbv)
        {
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для предоставления дескрипторов единицы измерения времени.
    /// </summary>
    public static class XUnitTime
    {
        /// <summary>
        /// Словарь дескрипторов единицы измерения времени.
        /// </summary>
        public readonly static Dictionary<TUnitTime, CUnitDescriptorTime> Descriptors = new()
        {
            { TUnitTime.Undefined, new CUnitDescriptorTime() },
            { TUnitTime.Second, new CUnitDescriptorTime(TUnitTime.Second, 1, "s", "с") },
            { TUnitTime.Nanosecond, new CUnitDescriptorTime(TUnitTime.Nanosecond, 1e-9, "ns", "нс") },
            { TUnitTime.Microsecond, new CUnitDescriptorTime(TUnitTime.Microsecond, 1e-6, "ms", "мкс") },
            { TUnitTime.Millisecond, new CUnitDescriptorTime(TUnitTime.Millisecond, 1e-3, "us", "ммс") },
            { TUnitTime.Minute, new CUnitDescriptorTime(TUnitTime.Minute, 60, "m", "м")},
            { TUnitTime.Hour, new CUnitDescriptorTime(TUnitTime.Hour, 60 * 60, "h", "ч")},
            { TUnitTime.Day, new CUnitDescriptorTime(TUnitTime.Day, 60 * 60 * 24, "d", "д")},
            { TUnitTime.Week, new CUnitDescriptorTime(TUnitTime.Week, 60 * 60 * 24 * 7, "w", "н")},
            { TUnitTime.Year, new CUnitDescriptorTime(TUnitTime.Year, 31557600, "y", "г")},
        };
    }

    /// <summary>
    /// Единица измерения времени.
    /// </summary>
    /// <remarks>
    /// Значение 1 соответствуют единицы измерения по умолчанию коротая принята в СИ.
    /// </remarks>
    public enum TUnitTime
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Секунда.
        /// </summary>
        Second = 1,

        /// <summary>
        /// Наносекунда.
        /// </summary>
        Nanosecond,

        /// <summary>
        /// Микросекунда.
        /// </summary>
        Microsecond,

        /// <summary>
        /// Миллисекунда.
        /// </summary>
        Millisecond,

        /// <summary>
        /// Минута.
        /// </summary>
        Minute,

        /// <summary>
        /// Час.
        /// </summary>
        Hour,

        /// <summary>
        /// День.
        /// </summary>
        Day,

        /// <summary>
        /// Неделя.
        /// </summary>
        Week,

        /// <summary>
        /// Год.
        /// </summary>
        Year,
    }
    /**@}*/
}