//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема конвертации данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusConverterBase.cs
*		Общие типы и структуры данных для конвертации типов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreConverters Подсистема конвертации данных
		//! Подсистема конвертации и преобразования данных обеспечивает единый механизм и точку входа для 
		//! преобразования объекта в нужный тип.
		//!
		//! Преобразование происходит на основе детальной информации об объекте который надо преобразовать в нужный тип, 
		//! поддерживается конвертация в том числе из строки или путём преобразования из объектов смежного типа.
		//!
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий конвертацию в базовые типы
		/// </summary>
		/// <remarks>
		/// Класс является прежде всего аккумулятором всех методов преобразования которые реализуют другие классы
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XConverter
		{
			#region ======================================= ToBoolean =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в логическое значение
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ToBoolean(String text)
			{
				return (XBoolean.Parse(text));
			}
			#endregion

			#region ======================================= ToInt =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в целочисленное значение
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ToInt(String text, Int32 default_value = 0)
			{
				return (XNumbers.ParseInt(text, default_value));
			}
			#endregion

			#region ======================================= ToSingle ==================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в вещественное значение одинарной точности
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ToSingle(String text, Single default_value = 0)
			{
				return (XNumbers.ParseSingle(text, default_value));
			}
			#endregion

			#region ======================================= ToDouble ==================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в вещественное значение двойной точности
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ToDouble(String text, Double default_value = 0)
			{
				return (XNumbers.ParseDouble(text, default_value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование форматированного текста в вещественное значение двойной точности
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ToDoubleFormat(String text, Double default_value = 0)
			{
				return (XNumbers.ParseDoubleFormat(text, default_value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование форматированного текста в вещественное значение двойной точности
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="result">Значение</param>
			/// <returns>Статус успешности преобразования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ToDoubleFormat(String text, out Double result)
			{
				return (XNumbers.ParseDoubleFormat(text, out result));
			}
			#endregion

			#region ======================================= ToDecimal =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в десятичное число с плавающей запятой
			/// </summary>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal ParseDecimal(String text, Decimal default_value = 0)
			{
				return (XNumbers.ParseDecimal(text, default_value));
			}
			#endregion

			#region ======================================= ToNumber ==================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование вещественного значения двойной точности в числовой тип указанного типа
			/// </summary>
			/// <param name="target_type">Целевой числовой тип</param>
			/// <param name="value">Значение</param>
			/// <returns>Числовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ToNumber(Type target_type, Double value)
			{
				String type_name = target_type.Name;
				switch (type_name)
				{
					case nameof(Byte):
						{
							return (Convert.ToByte(value));
						}
					case nameof(SByte):
						{
							return (Convert.ToSByte(value));
						}
					case nameof(Char):
						{
							return (Convert.ToChar(value));
						}
					case nameof(Int16):
						{
							return (Convert.ToInt16(value));
						}
					case nameof(UInt16):
						{
							return (Convert.ToUInt16(value));
						}
					case nameof(Int32):
						{
							return (Convert.ToInt32(value));
						}
					case nameof(UInt32):
						{
							return (Convert.ToUInt32(value));
						}
					case nameof(Int64):
						{
							return (Convert.ToInt64(value));
						}
					case nameof(UInt64):
						{
							return (Convert.ToUInt64(value));
						}
					case nameof(Single):
						{
							return (Convert.ToSingle(value));
						}
					case nameof(Decimal):
						{
							return (Convert.ToDecimal(value));
						}
				}

				return (value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование десятичного числа с плавающей запятой в числовой тип указанного типа
			/// </summary>
			/// <param name="target_type">Целевой числовой тип</param>
			/// <param name="value">Значение</param>
			/// <returns>Числовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ToNumber(Type target_type, Decimal value)
			{
				String type_name = target_type.Name;
				switch (type_name)
				{
					case nameof(Byte):
						{
							return (Convert.ToByte(value));
						}
					case nameof(SByte):
						{
							return (Convert.ToSByte(value));
						}
					case nameof(Char):
						{
							return (Convert.ToChar(value));
						}
					case nameof(Int16):
						{
							return (Convert.ToInt16(value));
						}
					case nameof(UInt16):
						{
							return (Convert.ToUInt16(value));
						}
					case nameof(Int32):
						{
							return (Convert.ToInt32(value));
						}
					case nameof(UInt32):
						{
							return (Convert.ToUInt32(value));
						}
					case nameof(Int64):
						{
							return (Convert.ToInt64(value));
						}
					case nameof(UInt64):
						{
							return (Convert.ToUInt64(value));
						}
					case nameof(Single):
						{
							return (Convert.ToSingle(value));
						}
					case nameof(Double):
						{
							return (Convert.ToDouble(value));
						}
				}

				return (value);
			}
			#endregion

			#region ======================================= ToEnum ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект указанного типа перечисления строкового значения
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="text">Текстовое значение</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Объект перечисления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ToEnum<TEnum>(String text, TEnum default_value = default(TEnum)) where TEnum : Enum
			{
				return (XEnum.ToEnum<TEnum>(text, default_value));
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
				return (XEnum.ToEnum<TEnum>(value, default_value));
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
				return (ToEnum<TEnum>(Convert.ToString(value), default_value));
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
				return (XEnum.ToEnumOfType(enum_type, value));
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
				return (XEnum.TryToEnum<TEnum>(value, out result));
			}
			#endregion

			#region ======================================= ToPrimitive ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование объекта к примитивному типу по указанному коду типа
			/// </summary>
			/// <remarks>
			/// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление
			/// </remarks>
			/// <param name="type_code">Код типа</param>
			/// <param name="value">Значение</param>
			/// <returns>Значение примитивного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TPrimitive ToPrimitive<TPrimitive>(TypeCode type_code, System.Object value)
			{
				return (default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста к примитивному типу по указанному коду типа
			/// </summary>
			/// <remarks>
			/// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление
			/// </remarks>
			/// <param name="type_code">Код типа</param>
			/// <param name="text">Текстовое значение</param>
			/// <returns>Значение примитивного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TPrimitive ToPrimitive<TPrimitive>(TypeCode type_code, String text)
			{
				System.Object result = default;

				switch (type_code)
				{
					case TypeCode.Empty:
						{
						}
						break;
					case TypeCode.Object:
						{
						}
						break;
					case TypeCode.DBNull:
						{
						}
						break;
					case TypeCode.Boolean:
						{
							result = XBoolean.Parse(text);
						}
						break;
					case TypeCode.Char:
						{
							result = text[0];
						}
						break;
					case TypeCode.SByte:
						{
							result = (SByte)XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.Byte:
						{
							result = (Byte)XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.Int16:
						{
							result = (Int16)XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.UInt16:
						{
							result = (UInt16)XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.Int32:
						{
							result = XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.UInt32:
						{
							result = (UInt32)XNumbers.ParseInt(text);
						}
						break;
					case TypeCode.Int64:
						{
							result = XNumbers.ParseLong(text);
						}
						break;
					case TypeCode.UInt64:
						{
							result = (UInt64)XNumbers.ParseLong(text);
						}
						break;
					case TypeCode.Single:
						{
							result = XNumbers.ParseSingle(text);
						}
						break;
					case TypeCode.Double:
						{
							result = XNumbers.ParseDouble(text);
						}
						break;
					case TypeCode.Decimal:
						{
							result = XNumbers.ParseDecimal(text);
						}
						break;
					case TypeCode.DateTime:
						{
							result = DateTime.Parse(text);
						}
						break;
					case TypeCode.String:
						{
							result = text;
						}
						break;
					default:
						break;
				}

				return ((TPrimitive)result);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================