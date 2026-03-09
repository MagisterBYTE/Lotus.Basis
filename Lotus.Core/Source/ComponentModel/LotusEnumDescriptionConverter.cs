using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreComponentModel
    *@{*/
    /// <summary>
    /// Преобразователь типов для перечислений, который использует атрибут <see cref="DescriptionAttribute"/> 
    /// для отображения понятных пользователю имен в интерфейсе.
    /// </summary>
    public class EnumDescriptionConverter<TEnum> : EnumConverter where TEnum : Enum
    {
        #region Constructor
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EnumDescriptionConverter{TEnum}"/>.
        /// </summary>
        public EnumDescriptionConverter() 
            : base(typeof(TEnum)) { }
        #endregion

        #region Override properties
        /// <summary>
        /// Указывает, поддерживает ли данный объект стандартный набор значений, которые можно выбрать из списка.
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <returns>Всегда true.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;

        /// <summary>
        /// Указывает, является ли список стандартных значений эксклюзивным (запрет на ввод произвольного текста).
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <returns>Всегда true.</returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;
        #endregion

        #region Override methods
        /// <summary>
        /// Преобразует значение перечисления в строку (описание из атрибута или имя константы).
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <param name="value">Значение перечисления для преобразования.</param>
        /// <param name="destinationType">Тип, в который выполняется преобразование.</param>
        /// <returns>Строковое описание перечисления.</returns>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Enum enumValue)
            {
                return XEnumExtension.GetDescriptionOrName(enumValue);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Преобразует строковое описание обратно в соответствующее значение перечисления.
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <param name="culture">Сведения о культуре.</param>
        /// <param name="value">Строковое значение для преобразования.</param>
        /// <returns>Значение перечисления.</returns>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string strValue)
            {
                foreach (var field in EnumType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var desc = field.GetCustomAttribute<DescriptionAttribute>();
                    if (desc?.Description == strValue || field.Name == strValue)
                        return field.GetValue(null);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Возвращает коллекцию стандартных значений (описаний) для перечисления.
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <returns>Коллекция строк-описаний для отображения в выпадающем списке.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
        {
            var descriptions = EnumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                                       .Select(f => f.GetCustomAttribute<DescriptionAttribute>()?.Description
                                                    ?? f.Name)
                                       .ToArray();
            return new StandardValuesCollection(descriptions);
        }

        /// <summary>
        /// Определяет, может ли преобразователь конвертировать объект из заданного типа источника.
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <param name="sourceType">Тип, из которого выполняется преобразование.</param>
        /// <returns>True, если преобразование возможно.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return true;
        }

        /// <summary>
        /// Определяет, может ли преобразователь конвертировать объект в заданный тип назначения.
        /// </summary>
        /// <param name="context">Контекст дескриптора типа.</param>
        /// <param name="destinationType">Тип, в который выполняется преобразование.</param>
        /// <returns>True, если преобразование возможно.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return true;
        }
        #endregion
    }
    /**@}*/
}
