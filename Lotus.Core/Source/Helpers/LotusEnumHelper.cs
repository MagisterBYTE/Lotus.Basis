//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Вспомогательная подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusEnumHelper.cs
*		Работа с типом перечисления.
*		Реализация дополнительных методов для работы с типом перечисления.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreHelpers
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий дополнительные методы для работы с типом <see cref="Enum"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XEnum
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка описания элементов перечисления
			/// </summary>
			/// <remarks>
			/// Описание элемента перечисления отсутствует то используется его имя
			/// </remarks>
			/// <param name="enum_type">Тип перечисления</param>
			/// <returns>Список описания элементов перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<String> GetDescriptions(Type enum_type)
			{
				List<String> values = new List<String>();
				foreach (FieldInfo fi in enum_type.GetFields())
				{
					DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение описания либо имени указанного перечисления
			/// </summary>
			/// <param name="enum_type">Тип перечисления</param>
			/// <param name="enum_value">Экземпляр перечисления</param>
			/// <returns>Описание либо имя перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetDescriptionOrName(Type enum_type, Enum enum_value)
			{
				FieldInfo fi = enum_type.GetField(Enum.GetName(enum_type, enum_value));
				DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
				if (dna != null)
				{
					return (dna.Description);
				}
				else
				{
					return (enum_value.ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение аббревиатуры либо имени указанного перечисления
			/// </summary>
			/// <param name="enum_type">Тип перечисления</param>
			/// <param name="enum_value">Экземпляр перечисления</param>
			/// <returns>Аббревиатура либо имя перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAbbreviationOrName(Type enum_type, Enum enum_value)
			{
				FieldInfo fi = enum_type.GetField(Enum.GetName(enum_type, enum_value));
				LotusAbbreviationAttribute abbr = (LotusAbbreviationAttribute)Attribute.GetCustomAttribute(fi, typeof(LotusAbbreviationAttribute));
				if (abbr != null)
				{
					return (abbr.Name);
				}
				else
				{
					return (enum_value.ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация описания или имени перечисления в объект перечисления
			/// </summary>
			/// <param name="enum_type">Тип перечисления</param>
			/// <param name="value">Описание либо имя перечисления</param>
			/// <returns>Экземпляр перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum ConvertFromDescriptionOrName(Type enum_type, String value)
			{
				foreach (FieldInfo fi in enum_type.GetFields())
				{
					DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

					if ((dna != null) && (value == dna.Description))
					{
						return ((Enum)Enum.Parse(enum_type, fi.Name));
					}
				}

				return ((Enum)Enum.Parse(enum_type, value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация аббревиатуры или имени перечисления в объект перечисления
			/// </summary>
			/// <param name="enum_type">Тип перечисления</param>
			/// <param name="value">Описание либо имя перечисления</param>
			/// <returns>Экземпляр перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum ConvertFromAbbreviationOrName(Type enum_type, String value)
			{
				foreach (FieldInfo fi in enum_type.GetFields())
				{
					LotusAbbreviationAttribute abbr = (LotusAbbreviationAttribute)Attribute.GetCustomAttribute(fi, typeof(LotusAbbreviationAttribute));

					if ((abbr != null) && (value == abbr.Name))
					{
						return ((Enum)Enum.Parse(enum_type, fi.Name));
					}
				}

				return ((Enum)Enum.Parse(enum_type, value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект указанного типа перечисления строкового значения
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="value">Значение</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Объект перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ToEnum<TEnum>(String value, TEnum default_value = default(TEnum)) where TEnum : Enum
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new ArgumentException(String.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
				}

				try
				{
					TEnum result = (TEnum)Enum.Parse(typeof(TEnum), value, true);
					return result;
				}
				catch
				{
					return default_value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект указанного типа перечисления целочисленного значения
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="value">Значение</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Объект перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ToEnum<TEnum>(Int32 value, TEnum default_value = default(TEnum)) where TEnum : Enum
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new ArgumentException(String.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
				}

				try
				{
					return (TEnum)System.Enum.ToObject(typeof(TEnum), value);
				}
				catch
				{
					return default_value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект указанного типа перечисления обобщенного значения
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="value">Значение</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Объект перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ToEnum<TEnum>(Object value, TEnum default_value) where TEnum : Enum
			{
				return (ToEnum(Convert.ToString(value), default_value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект перечисления обобщенного значения
			/// </summary>
			/// <param name="enum_type">Тип перечисления</param>
			/// <param name="value">Значение</param>
			/// <returns>Объект перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum ToEnumOfType(Type enum_type, System.Object value)
			{
				if (value == null)
				{
					return (Enum.ToObject(enum_type, 0) as Enum);
				}
				else
				{
					if (value is Int32)
					{
						return (Enum.ToObject(enum_type, Convert.ToInt32(value)) as Enum);
					}
					else
					{
						return (Enum.Parse(enum_type, Convert.ToString(value), true) as Enum);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Попытка преобразования в объект указанного типа перечисления обобщенного значения
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="value">Значение</param>
			/// <param name="result">Объект перечисления</param>
			/// <returns>Статус успешности преобразования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean TryToEnum<TEnum>(Object value, out TEnum result) where TEnum : Enum
			{
				if (!typeof(TEnum).IsEnum)
				{
					throw new ArgumentException(String.Format("Type <{0}> must be an enumerated type", typeof(TEnum).Name));
				}

				try
				{
					if (value == null)
					{
						result = ((TEnum)Enum.ToObject(typeof(TEnum), 0));
					}
					else
					{
						if (value is Int32)
						{
							result = (TEnum)Enum.ToObject(typeof(TEnum), Convert.ToInt32(value));
						}
						else
						{
							result = (TEnum)Enum.Parse(typeof(TEnum), Convert.ToString(value), true);
						}
					}
					return (true);
				}
				catch
				{
					result = default(TEnum);
					return (false);
				}
			}
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Конвертер для <see cref="Enum"/>, преобразовывающий Enum к строке с учетом атрибута <see cref="DescriptionAttribute"/> 
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class EnumToStringConverter<TEnum> : TypeConverter where TEnum : Enum
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public EnumToStringConverter()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение возможности конвертации в определённый тип
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="dest_type">Целевой тип</param>
			/// <returns>Статус возможности</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean CanConvertTo(ITypeDescriptorContext context, Type dest_type)
			{
				return (dest_type == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение возможности конвертации из определённого типа
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="source_type">Тип источник</param>
			/// <returns>Статус возможности</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type source_type)
			{
				return (source_type == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация в определённый тип
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="culture">Культура</param>
			/// <param name="value">Значение</param>
			/// <param name="dest_type">Целевой тип</param>
			/// <returns>Значение целевого типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type dest_type)
			{
				Type type_enum = typeof(TEnum);
				return (XEnum.GetDescriptionOrName(type_enum, (Enum)value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация из определённого типа
			/// </summary>
			/// <param name="context">Контекстная информация</param>
			/// <param name="culture">Культура</param>
			/// <param name="value">Значение целевого типа</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
			{
				Type type_enum = typeof(TEnum);
				return (XEnum.ConvertFromDescriptionOrName(type_enum, value.ToString()));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================