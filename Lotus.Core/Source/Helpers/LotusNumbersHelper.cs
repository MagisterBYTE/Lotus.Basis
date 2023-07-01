//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Вспомогательная подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusNumbersHelper.cs
*		Работа с числовыми типами.
*		Реализация дополнительных методов для работы с числовыми типами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Globalization;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreHelpers
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий дополнительные методы для работы с числовыми типами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XNumbers
		{
			#region ======================================= ФОРМАТЫ ОТОБРАЖЕНИЯ ЧИСЕЛ =================================
			/// <summary>
			/// Денежный формат
			/// </summary>
			public const String Monetary = "{0:c}";
			#endregion

			#region ======================================= Int32 =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на установленный флаг
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="flag">Проверяемый флаг</param>
			/// <returns>Статус установки флага</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsFlagSet(Int32 value, Int32 flag)
			{
				return (value & flag) != 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка флага
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="flags">Флаг</param>
			/// <returns>Новое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 SetFlag(Int32 value, Int32 flags)
			{
				value |= flags;
				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка флага
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="flags">Флаг</param>
			/// <returns>Новое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ClearFlag(Int32 value, Int32 flags)
			{
				value &= ~flags;
				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ParseInt(String text, Int32 defaultValue = 0)
			{
				if (Int32.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}
			#endregion

			#region ======================================= Int64 =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 ParseLong(String text, Int64 defaultValue = 0)
			{
				if (Int64.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}
			#endregion

			#region ======================================= Single ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ParseSingle(String text, Single defaultValue = 0)
			{
				if (text.IndexOf(',') > -1)
				{
					text = text.Replace(',', XChar.Dot);
				}

				if (Single.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}
			#endregion

			#region ======================================= Double ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ParseDouble(String text, Double defaultValue = 0)
			{
				if (text.IndexOf(',') > -1)
				{
					text = text.Replace(',', XChar.Dot);
				}

				if (Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование форматированного текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ParseDoubleFormat(String text, Double defaultValue = 0)
			{
				var number = new StringBuilder(text.Length);

				for (var i = 0; i < text.Length; i++)
				{
					var c = text[i];

					if(c == '-')
					{
						number.Append(c);
						continue;
					}

					if (c == ',' || c == XChar.Dot)
					{
						if (i != text.Length - 1)
						{
							number.Append(XChar.Dot);
						}
						continue;
					}

					if (c >= '0' && c <= '9')
					{
						number.Append(c);
						continue;
					}

				}

				if (Double.TryParse(number.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование форматированного текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="result">Значение</param>
			/// <returns>Статус успешности преобразования</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ParseDoubleFormat(String text, out Double result)
			{
				var number = new StringBuilder(text.Length);

				for (var i = 0; i < text.Length; i++)
				{
					var c = text[i];

					if (c == '-')
					{
						number.Append(c);
						continue;
					}

					if (c == ',' || c == XChar.Dot)
					{
						if (i != text.Length - 1)
						{
							number.Append(XChar.Dot);
						}
						continue;
					}

					if (c >= '0' && c <= '9')
					{
						number.Append(c);
						continue;
					}

				}

				if (Double.TryParse(number.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
				{
					return true;
				}

				return false;
			}
			#endregion

			#region ======================================= Decimal ===================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal ParseDecimal(String text, Decimal defaultValue = 0)
			{
				if (text.IndexOf(',') > -1)
				{
					text = text.Replace(',', XChar.Dot);
				}

				if (Decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование текста, представленного как отображение валюты, в число
			/// </summary>
			/// <param name="text">Текст</param>
			/// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal ParseCurrency(String text, Decimal defaultValue = 0)
			{
				if (text.IndexOf(',') > -1)
				{
					text = text.Replace(',', XChar.Dot);
				}

				if (Decimal.TryParse(text, NumberStyles.Currency, CultureInfo.InvariantCulture, out defaultValue))
				{

				}

				return defaultValue;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================