using System;
using System.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="System.Enum"/>.
    /// </summary>
    public static class XEnumExtension
    {
        /// <summary>
        /// Проверка на установленный флаг в перечислении.
        /// </summary>
        /// <param name="value">Перечисление.</param>
        /// <param name="flag">Проверяемый флаг.</param>
        /// <returns>Статус установки флага.</returns>
        public static bool IsFlagSet(this Enum value, Enum flag)
        {
            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        /// <summary>
        /// Установка флага в перечислении.
        /// </summary>
        /// <param name="value">Перечисление.</param>
        /// <param name="flags">Флаг.</param>
        /// <param name="on">Статус установки или сброса.</param>
        /// <returns>Перечисление.</returns>
        public static Enum SetFlags(this Enum value, Enum flags, bool on)
        {
            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= ~lFlag;
            }

            var result = (Enum)Enum.ToObject(value.GetType(), lValue);
            return result;
        }

        /// <summary>
        /// Установка флага в перечислении.
        /// </summary>
        /// <param name="value">Перечисление.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Перечисление.</returns>
        public static Enum SetFlags(this Enum value, Enum flags)
        {
            return value.SetFlags(flags, true);
        }

        /// <summary>
        /// Сброс флага в перечислении.
        /// </summary>
        /// <param name="value">Перечисление.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Перечисление.</returns>
        public static Enum ClearFlags(this Enum value, Enum flags)
        {
            return value.SetFlags(flags, false);
        }

        /// <summary>
        /// Получение атрибута перечисления.
        /// </summary>
        /// <typeparam name="TType">Тип атрибута.</typeparam>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Найденный атрибут.</returns>
        public static TType? GetAttributeOfType<TType>(this Enum enumValue) where TType : Attribute
        {
            var type = enumValue.GetType();
            var member_info = type.GetMember(enumValue.ToString());
            var attributes = member_info[0].GetCustomAttributes(typeof(TType), false);
            return attributes.Length > 0 ? (TType)attributes[0] : null;
        }

        /// <summary>
        /// Получение описания либо имени указанного перечисления.
        /// </summary>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Описание либо имя перечисления.</returns>
        public static string? GetDescriptionOrName(this Enum enumValue)
        {
            Type type_enum = enumValue.GetType();
            return XEnum.GetDescriptionOrName(type_enum, enumValue);
        }

        /// <summary>
        /// Получение аббревиатуры либо имени указанного перечисления.
        /// </summary>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Аббревиатура либо имя перечисления.</returns>
        public static string? GetAbbreviationOrName(this Enum enumValue)
        {
            Type type_enum = enumValue.GetType();
            return XEnum.GetAbbreviationOrName(type_enum, enumValue);
        }

        /// <summary>
        /// Преобразование одного типа перечисления к другому типу перечисления.
        /// </summary>
        /// <typeparam name="TInEnum">Исходный тип перечисления.</typeparam>
        /// <typeparam name="TOutEnum">Целевой тип перечисления.</typeparam>
        /// <param name="enumValue">Экземпляр перечисления исходного типа.</param>
        /// <returns>Экземпляр перечисления целевого типа.</returns>
        public static TOutEnum Cast<TInEnum, TOutEnum>(this TInEnum enumValue)
            where TOutEnum : struct, Enum
            where TInEnum : struct, Enum
        {
            return Enum.GetValues<TOutEnum>().First(v => v.ToString() == enumValue.ToString());
        }

        /// <summary>
        /// Преобразование в целое значение.
        /// </summary>
        /// <param name="enumValue">Экземпляр перечисления.</param>
        /// <returns>Целое значение.</returns>
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
    /**@}*/
}