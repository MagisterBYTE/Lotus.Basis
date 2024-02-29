using System;

#nullable disable

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Дескриптор для описания единицы измерения.
    /// </summary>
    public class CUnitDescriptor
    {
        #region Properties
        /// <summary>
        /// Коэффициент для преобразования в базовую единицу измерения.
        /// </summary>
        public double CoeffToBase { get; set; }

        /// <summary>
        /// Коэффициент для преобразования в текущую единицу измерения из базовой единицы измерения.
        /// </summary>
        public double CoeffToCurrent
        {
            get { return 1 / CoeffToBase; }
        }

        /// <summary>
        /// Международное название.
        /// </summary>
        public string InternationalName { get; set; }

        /// <summary>
        /// Русское название.
        /// </summary>
        public string RusName { get; set; }

        /// <summary>
        /// Международная аббревиатура.
        /// </summary>
        public string InternationalAbbv { get; set; }

        /// <summary>
        /// Русская аббревиатура.
        /// </summary>
        public string RusAbbv { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами.
        /// </summary>
        public CUnitDescriptor()
        {
            CoeffToBase = 1.0;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        public CUnitDescriptor(double сoeffToBase)
        {
            CoeffToBase = сoeffToBase;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        /// <param name="internationalAbbv">Международная аббревиатура.</param>
        /// <param name="rusAbbv">Русская аббревиатура.</param>
        public CUnitDescriptor(double сoeffToBase, string internationalAbbv, string rusAbbv)
        {
            CoeffToBase = сoeffToBase;
            InternationalAbbv = internationalAbbv;
            RusAbbv = rusAbbv;
        }
        #endregion
    }

    /// <summary>
    /// Дескриптор для описания единицы измерения.
    /// </summary>
    /// <typeparam name="TUnit">Единица измерения.</typeparam>
    public class CUnitDescriptor<TUnit> : CUnitDescriptor where TUnit : Enum
    {
        #region Properties
        /// <summary>
        /// Единица измерения.
        /// </summary>
        public TUnit Unit { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами.
        /// </summary>
        public CUnitDescriptor()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="unit">Единица измерения.</param>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        public CUnitDescriptor(TUnit unit, double сoeffToBase)
            : base(сoeffToBase)
        {
            Unit = unit;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="unit">Единица измерения.</param>
        /// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения.</param>
        /// <param name="internationalAbbv">Международная аббревиатура.</param>
        /// <param name="rusAbbv">Русская аббревиатура.</param>
        public CUnitDescriptor(TUnit unit, double сoeffToBase, string internationalAbbv, string rusAbbv)
            : base(сoeffToBase, internationalAbbv, rusAbbv)
        {
            Unit = unit;
        }
        #endregion
    }
    /**@}*/
}