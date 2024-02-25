using System;

namespace Lotus.Core
{
    /** \addtogroup CoreDateTime
    *@{*/
    /// <summary>
    /// Интерфейс для объектов которые поддерживают период.
    /// </summary>
    public interface ILotusDatePeriod
    {
        /// <summary>
        /// Дата начала периода.
        /// </summary>
#if UNITY_2017_1_OR_NEWER
		DateTime BeginPeriodDate { get; set; }
#else
        DateOnly BeginPeriodDate { get; set; }
#endif

        /// <summary>
        /// Дата окончания периода.
        /// </summary>
#if UNITY_2017_1_OR_NEWER
		DateTime? EndPeriodDate { get; set; }
#else
        DateOnly? EndPeriodDate { get; set; }
#endif
    }

    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с типом <see cref="ILotusDatePeriod"/>.
    /// </summary>
    public static class XDatePeriodExtension
    {
        /// <summary>
        /// Получение текущей даты в текстовом формате UTC.
        /// </summary>
        /// <param name="this">Объект.</param>
        /// <returns>Дата в текстовом формате UTC.</returns>
        public static string GetPeriodOfText(this ILotusDatePeriod @this)
        {
            if (@this.EndPeriodDate != null)
            {
                return @this.BeginPeriodDate.ToString(XDateFormats.Default)
                    + "г. - " + @this.EndPeriodDate.GetValueOrDefault().ToString(XDateFormats.Default) + "г.";
            }
            else
            {
                return @this.BeginPeriodDate.ToString(XDateFormats.Default) + "г. - по н.в.";
            }
        }
    }
    /**@}*/
}