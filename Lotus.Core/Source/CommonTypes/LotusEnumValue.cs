using System;
using System.Collections.Generic;
using System.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Класс обвертка для представления элемента перечисления как объекта класса.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления.</typeparam>
    public class EnumValue<TEnum> where TEnum : Enum
    {
        #region Static methods
        /// <summary>
        /// Получить список элементов перечисления с описанием из атрибута Description.
        /// </summary>
        /// <returns>Список элементов перечисления</returns>
        public static List<EnumValue<TEnum>> GetEnumValuesByDescription()
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<object>()
                       .Select(e => new EnumValue<TEnum>
                       {
                           Value = (TEnum)e,
                           Text = XEnumHelper.GetDescriptionOrName(typeof(TEnum), (Enum)e)
                       })
                       .ToList();
        }
        #endregion

        #region Fields
        /// <summary>
        /// Значение
        /// </summary>
        public TEnum Value { get; set; }

        /// <summary>
        /// Текстовое значение из соответствующего атрибута.
        /// </summary>
        public string Text { get; set; }
        #endregion

        #region System methods
        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
    /**@}*/
}