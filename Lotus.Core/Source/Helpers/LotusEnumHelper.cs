using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с типом <see cref="Enum"/>.
    /// </summary>
    public static class XEnumHelper
    {
        /// <summary>
        /// Получение списка описания элементов перечисления.
        /// </summary>
        /// <remarks>
        /// Описание элемента перечисления отсутствует то используется его имя.
        /// </remarks>
        /// <param name="enumType">Тип перечисления.</param>
        /// <returns>Список описания элементов перечисления.</returns>
        public static List<string> GetDescriptions(Type enumType)
        {
            var values = new List<string>();
            foreach (var fi in enumType.GetFields())
            {
                var dna = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (dna != null)
                {
                    values.Add(dna.Description);
                }
                else
                {
                    if (fi.Name != "value__")
                    {
                        values.Add(fi.Name);
                    }
                }
            }

            return values;
        }

        /// <summary>
        /// Получение описания либо имени указанного перечисления.
        /// </summary>
        /// <param name="enumType">Тип перечисления.</param>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Описание либо имя перечисления.</returns>
        public static string GetDescriptionOrName(Type enumType, Enum enumValue)
        {
            var fi = enumType.GetField(Enum.GetName(enumType, enumValue) ?? string.Empty);

            if (fi == null) return string.Empty;

            var dna = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (dna != null)
            {
                return dna.Description;
            }
            else
            {
                return enumValue.ToString();
            }
        }

        /// <summary>
        /// Получение аббревиатуры либо имени указанного перечисления.
        /// </summary>
        /// <param name="enumType">Тип перечисления.</param>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Аббревиатура либо имя перечисления.</returns>
        public static string GetAbbreviationOrName(Type enumType, Enum enumValue)
        {
            var fi = enumType.GetField(Enum.GetName(enumType, enumValue) ?? string.Empty);

            if (fi == null) return string.Empty;

            var abbr = Attribute.GetCustomAttribute(fi, typeof(LotusAbbreviationAttribute)) as LotusAbbreviationAttribute;
            if (abbr != null)
            {
                return abbr.Name;
            }
            else
            {
                return enumValue.ToString();
            }
        }

        /// <summary>
        /// Конвертация описания или имени перечисления в объект перечисления.
        /// </summary>
        /// <param name="enumType">Тип перечисления.</param>
        /// <param name="value">Описание либо имя перечисления.</param>
        /// <returns>Экземпляр перечисления.</returns>
        public static Enum ConvertFromDescriptionOrName(Type enumType, string value)
        {
            foreach (var fi in enumType.GetFields())
            {
                var dna = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if ((dna != null) && (value == dna.Description))
                {
                    return (Enum)Enum.Parse(enumType, fi.Name);
                }
            }

            return (Enum)Enum.Parse(enumType, value);
        }

        /// <summary>
        /// Конвертация аббревиатуры или имени перечисления в объект перечисления.
        /// </summary>
        /// <param name="enumType">Тип перечисления.</param>
        /// <param name="value">Описание либо имя перечисления.</param>
        /// <returns>Экземпляр перечисления.</returns>
        public static Enum ConvertFromAbbreviationOrName(Type enumType, string value)
        {
            foreach (var fi in enumType.GetFields())
            {
                var abbr = Attribute.GetCustomAttribute(fi, typeof(LotusAbbreviationAttribute)) as LotusAbbreviationAttribute;

                if ((abbr != null) && (value == abbr.Name))
                {
                    return (Enum)Enum.Parse(enumType, fi.Name);
                }
            }

            return (Enum)Enum.Parse(enumType, value);
        }

    }

    /// <summary>
    /// Конвертер для <see cref="Enum"/>, преобразовывающий Enum к строке с учетом атрибута <see cref="DescriptionAttribute"/>.
    /// </summary>
    public class EnumToStringConverter<TEnum> : TypeConverter where TEnum : Enum
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public EnumToStringConverter()
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Определение возможности конвертации в определённый тип.
        /// </summary>
        /// <param name="context">Контекстная информация.</param>
        /// <param name="destinationType">Целевой тип.</param>
        /// <returns>Статус возможности.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        {
            if (destinationType == null) return false;

            return destinationType == typeof(string);
        }

        /// <summary>
        /// Определение возможности конвертации из определённого типа.
        /// </summary>
        /// <param name="context">Контекстная информация.</param>
        /// <param name="sourceType">Тип источник.</param>
        /// <returns>Статус возможности.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type? sourceType)
        {
            if (sourceType == null) return false;

            return sourceType == typeof(string);
        }

        /// <summary>
        /// Конвертация в определённый тип.
        /// </summary>
        /// <param name="context">Контекстная информация.</param>
        /// <param name="culture">Культура.</param>
        /// <param name="value">Значение.</param>
        /// <param name="destinationType">Целевой тип.</param>
        /// <returns>Значение целевого типа.</returns>
        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType)
        {
            var type_enum = typeof(TEnum);

            if (value == null) return string.Empty;

            return XEnumHelper.GetDescriptionOrName(type_enum, (value as Enum)!);
        }

        /// <summary>
        /// Конвертация из определённого типа.
        /// </summary>
        /// <param name="context">Контекстная информация.</param>
        /// <param name="culture">Культура.</param>
        /// <param name="value">Значение целевого типа.</param>
        /// <returns>Значение.</returns>
        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
        {
            var type_enum = typeof(TEnum);

            if (value == null) return string.Empty;

            return XEnumHelper.ConvertFromDescriptionOrName(type_enum, value.ToString()!);
        }
        #endregion
    }
    /**@}*/
}