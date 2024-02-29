using System;

namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения форматирование значения числовой величины.
    /// </summary>
    /// <remarks>
    /// Применяется стандартное форматирование строки, значение передается в качестве первого аргумента.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusNumberFormatAttribute : Attribute
    {
        #region Fields
        internal readonly string _formatValue;
        #endregion

        #region Properties
        /// <summary>
        /// Формат отображения значения числовой величины.
        /// </summary>
        public string FormatValue
        {
            get { return _formatValue; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="formatValue">Формат отображения значения числовой величины.</param>
        public LotusNumberFormatAttribute(string formatValue)
        {
            _formatValue = formatValue;
        }
        #endregion
    }
    /**@}*/
}