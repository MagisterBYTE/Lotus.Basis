using System.Collections.Generic;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Дескриптор для описания единицы измерения объема.
    /// </summary>
    public class CUnitDescriptorVolume : CUnitDescriptor<TUnitVolume>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами.
        /// </summary>
        public CUnitDescriptorVolume()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="unit">Единица измерения.</param>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        public CUnitDescriptorVolume(TUnitVolume unit, double сoeffToBase)
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
        public CUnitDescriptorVolume(TUnitVolume unit, double сoeffToBase, string internationalAbbv, string rusAbbv)
            : base(unit, сoeffToBase, internationalAbbv, rusAbbv)
        {
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для предоставления дескрипторов единицы измерения объема.
    /// </summary>
    public static class XUnitVolume
    {
        /// <summary>
        /// Словарь дескрипторов единицы измерения объема.
        /// </summary>
        public readonly static Dictionary<TUnitVolume, CUnitDescriptorVolume> Descriptors = new()
        {
            { TUnitVolume.Undefined, new CUnitDescriptorVolume() },
            { TUnitVolume.CubicMeter, new CUnitDescriptorVolume(TUnitVolume.CubicMeter, 1, "m3", "м3") },
            { TUnitVolume.CubicNanometer, new CUnitDescriptorVolume(TUnitVolume.CubicNanometer, 1e-27, "nm3", "нм3") },
            { TUnitVolume.CubicMicrometer, new CUnitDescriptorVolume(TUnitVolume.CubicMicrometer, 1e-18, "mk3", "мкм3") },
            { TUnitVolume.CubicMillimeter, new CUnitDescriptorVolume(TUnitVolume.CubicMillimeter, 1e-9, "mm3", "мм3") },
            { TUnitVolume.CubicCentimeter, new CUnitDescriptorVolume(TUnitVolume.CubicCentimeter, 1e-6, "cm3", "см3")},
            { TUnitVolume.CubicInch, new CUnitDescriptorVolume(TUnitVolume.CubicInch, 0.0254 * 0.0254 * 0.0254, "in3", "дюйм3")},
            { TUnitVolume.CubicDecimeter, new CUnitDescriptorVolume(TUnitVolume.CubicDecimeter, 1e-4, "dm3", "дм3")},
            { TUnitVolume.CubicFoot, new CUnitDescriptorVolume(TUnitVolume.CubicFoot, 0.3048 * 0.3048 * 0.3048, "ft3", "фут3")},
            { TUnitVolume.CubicYard, new CUnitDescriptorVolume(TUnitVolume.CubicYard, 0.9144 * 0.9144 * 0.9144, "yr3", "ярд3")},
            { TUnitVolume.CubicHectare, new CUnitDescriptorVolume(TUnitVolume.CubicHectare, 1e6, "ha3", "га3")},
            { TUnitVolume.CubicKilometer, new CUnitDescriptorVolume(TUnitVolume.CubicKilometer, 1e9, "km3", "км3")},
        };
    }

    /// <summary>
    /// Единица измерения объема.
    /// </summary>
    /// <remarks>
    /// Значение 1 соответствуют единицы измерения по умолчанию коротая принята в СИ.
    /// </remarks>
    public enum TUnitVolume
    {
        /// <summary>
        /// Не определена.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Кубический метр.
        /// </summary>
        CubicMeter = 1,

        /// <summary>
        /// Кубический нанометр.
        /// </summary>
        CubicNanometer,

        /// <summary>
        /// Кубический микрометр.
        /// </summary>
        CubicMicrometer,

        /// <summary>
        /// Кубический миллиметр.
        /// </summary>
        CubicMillimeter,

        /// <summary>
        /// Кубический сантиметр.
        /// </summary>
        CubicCentimeter,

        /// <summary>
        /// Кубический дюйм.
        /// </summary>
        CubicInch,

        /// <summary>
        /// Кубический дециметр.
        /// </summary>
        CubicDecimeter,

        /// <summary>
        /// Кубический фут.
        /// </summary>
        CubicFoot,

        /// <summary>
        /// Кубический ярд.
        /// </summary>
        CubicYard,

        /// <summary>
        /// Кубический гектар.
        /// </summary>
        CubicHectare,

        /// <summary>
        /// Кубический километр.
        /// </summary>
        CubicKilometer,
    }
    /**@}*/
}