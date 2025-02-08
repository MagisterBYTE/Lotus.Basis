using System;
using System.Collections.Generic;

namespace Lotus.Core
{
    /** \addtogroup CoreConverters
    *@{*/
    /// <summary>
    /// Статический класс реализующий конвертацию в тип перечисления.
    /// </summary>
    public static class XEnumConverter
    {
        /// <summary>
        /// Преобразование в объект указанного типа перечисления строкового значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Объект перечисления.</returns>
        public static TEnum ToEnum<TEnum>(string value, TEnum defaultValue = default!) where TEnum : Enum
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
                return defaultValue;
            }
        }

        /// <summary>
        /// Преобразование в объект указанного типа перечисления целочисленного значения.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="value">Значение.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Объект перечисления.</returns>
        public static TEnum ToEnum<TEnum>(int value, TEnum defaultValue = default!) where TEnum : Enum
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
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException(string.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
            }

            try
            {
                if(value is string stringValue)
                {
                    var result = (TEnum)Enum.Parse(typeof(TEnum), stringValue, true);
                    return result;
                }
                if (value is int intValue)
                {
                    var result = (TEnum)System.Enum.ToObject(typeof(TEnum), intValue);
                    return result;
                }

                return (TEnum)System.Enum.ToObject(typeof(TEnum), value);
            }
            catch
            {
                return defaultValue!;
            }
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
                if (value is string stringValue)
                {
                    return Enum.Parse(enumType, stringValue, true) as Enum;
                }
                if (value is int intValue)
                {
                    return Enum.ToObject(enumType, intValue) as Enum;
                }

                return Enum.ToObject(enumType, value) as Enum;
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
                    if (value is string stringValue)
                    {
                        result = (TEnum)Enum.Parse(typeof(TEnum), stringValue, true);
                    }
                    else
                    {
                        if (value is int intValue)
                        {
                            result = (TEnum)Enum.ToObject(typeof(TEnum), intValue);
                        }
                        else
                        {
                            result = (TEnum)Enum.ToObject(typeof(TEnum), value);
                        }
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
        /// <param name="valueEnum">Строка со значениями перечисления, разделёнными указанным сепаратором.</param>
        /// <param name="separator">Сепаратор для разделения значений.</param>
        /// <returns>Массив значений перечисления или пустой массив в случае ошибки.</returns>
        public static TEnum[] ToSplitEnums<TEnum>(string valueEnum, string separator = ",") where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var enums = new List<TEnum>();

            if (string.IsNullOrEmpty(valueEnum))
            {
                return [.. enums];
            }

            var valueEnums = valueEnum.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (valueEnums.Length == 0)
            {
                return [.. enums];
            }

            foreach (var item in valueEnums)
            {
                var enumVal = (TEnum)Enum.Parse(enumType, item, true);
                enums.Add(enumVal);
            }

            return [.. enums];
        }
    }
    /**@}*/
}