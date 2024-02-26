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
    public static class XEnum
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
            foreach (FieldInfo fi in enumType.GetFields())
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
            FieldInfo? fi = enumType.GetField(Enum.GetName(enumType, enumValue) ?? string.Empty);

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
            FieldInfo? fi = enumType.GetField(Enum.GetName(enumType, enumValue) ?? string.Empty);

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
            foreach (FieldInfo fi in enumType.GetFields())
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
            foreach (FieldInfo fi in enumType.GetFields())
            {
                var abbr = Attribute.GetCustomAttribute(fi, typeof(LotusAbbreviationAttribute)) as LotusAbbreviationAttribute;

                if ((abbr != null) && (value == abbr.Name))
                {
                    return (Enum)Enum.Parse(enumType, fi.Name);
                }
            }

            return (Enum)Enum.Parse(enumType, value);
        }

        /// <summary>
        /// Преобразование в объект указанного типа перечисления строкового значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Объект перечисления.</returns>
        public static TEnum ToEnum<TEnum>(string value, TEnum? defaultValue = default(TEnum)) where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
            }

            try
            {
                var result = (TEnum)Enum.Parse(typeof(TEnum), value, true);
                return result;
            }
            catch
            {
                return defaultValue!;
            }
        }

        /// <summary>
        /// Преобразование в объект указанного типа перечисления целочисленного значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Объект перечисления.</returns>
        public static TEnum ToEnum<TEnum>(int value, TEnum? defaultValue = default(TEnum)) where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
            }

            try
            {
                return (TEnum)System.Enum.ToObject(typeof(TEnum), value);
            }
            catch
            {
                return defaultValue!;
            }
        }

        /// <summary>
        /// Преобразование в объект указанного типа перечисления обобщенного значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Объект перечисления.</returns>
        public static TEnum ToEnum<TEnum>(object value, TEnum defaultValue) where TEnum : Enum
        {
            return ToEnum(Convert.ToString(value) ?? string.Empty, defaultValue);
        }

        /// <summary>
        /// Преобразование в объект перечисления обобщенного значения.
        /// </summary>
        /// <param name="enumType">Тип перечисления.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект перечисления.</returns>
        public static Enum? ToEnumOfType(Type enumType, object value)
        {
            if (value == null)
            {
                return Enum.ToObject(enumType, 0) as Enum;
            }
            else
            {
                if (value is int)
                {
                    return Enum.ToObject(enumType, Convert.ToInt32(value)) as Enum;
                }
                else
                {
                    return Enum.Parse(enumType, Convert.ToString(value) ?? string.Empty, true) as Enum;
                }
            }
        }

        /// <summary>
        /// Попытка преобразования в объект указанного типа перечисления обобщенного значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="result">Объект перечисления.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryToEnum<TEnum>(object value, out TEnum result) where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
            }

            try
            {
                if (value == null)
                {
                    result = (TEnum)Enum.ToObject(typeof(TEnum), 0);
                }
                else
                {
                    if (value is int)
                    {
                        result = (TEnum)Enum.ToObject(typeof(TEnum), Convert.ToInt32(value));
                    }
                    else
                    {
                        result = (TEnum)Enum.Parse(typeof(TEnum), Convert.ToString(value) ?? string.Empty, true);
                    }
                }
                return true;
            }
            catch
            {
                result = default(TEnum)!;
                return false;
            }
        }

        /// <summary>
        /// Преобразование из строки в массив значений перечисления указанного типа.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="valueEnum">Строка со значениями перечисления, разделёнными запятыми.</param>
        /// <returns>Массив значений перечисления или пустой массив в случае ошибки.</returns>
        public static TEnum[] ToEnums<TEnum>(string valueEnum) where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var enums = new List<TEnum>();

            if (string.IsNullOrEmpty(valueEnum))
            {
                return enums.ToArray();
            }

            var valueEnums = valueEnum.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (valueEnums.Length == 0)
            {
                return enums.ToArray();
            }

            foreach (var item in valueEnums)
            {
                var enumVal = (TEnum)Enum.Parse(enumType, item, true);
                enums.Add(enumVal);
            }

            return enums.ToArray();
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
            Type type_enum = typeof(TEnum);

            if (value == null) return string.Empty;

            return XEnum.GetDescriptionOrName(type_enum, (value as Enum)!);
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
            Type type_enum = typeof(TEnum);

            if (value == null) return string.Empty;

            return XEnum.ConvertFromDescriptionOrName(type_enum, value.ToString()!);
        }
        #endregion
    }
    /**@}*/
}