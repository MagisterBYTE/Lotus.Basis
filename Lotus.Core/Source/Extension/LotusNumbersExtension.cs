//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusNumbersExtension.cs
*		Методы расширения числовых типов.
*		Реализация максимально обобщенных расширений направленных на работу с числовыми типами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreExtension
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений числовых классов платформы .NET
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XNumbersExtension
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Внутренний строковый буфер для быстрой конвертации
			/// </summary>
			internal static readonly StringBuilder FloatToStringBuffer = new StringBuilder(64);
			#endregion

			#region ======================================= Int32 =====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация значения в тип объема информации
			/// </summary>
			/// <param name="this">Значение</param>
			/// <returns>Объем информации с суффиксом</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ToShortString(this Int32 @this)
			{
				if (@this < 0)
				{
					@this = -@this;
				}
				if (@this >= 1000000000)
				{
#if (UNITY_2017_1_OR_NEWER)
					return String.Format("{0}mB", UnityEngine.Mathf.Floor(@this / 10000000f) / 10f);
#else
					return String.Format("{0}mB", Math.Floor(@this / 10000000f) / 10f);
#endif
				}
				if (@this >= 1000000)
				{
#if (UNITY_2017_1_OR_NEWER)
					return String.Format("{0}kB", UnityEngine.Mathf.Floor(@this / 100000f) / 10f);
#else
					return String.Format("{0}kB", Math.Floor(@this / 100000f) / 10f);
#endif
				}
				if (@this >= 1000)
				{
#if (UNITY_2017_1_OR_NEWER)
					return String.Format("{0}B", UnityEngine.Mathf.Floor(@this / 100f) / 10f);
#else
					return String.Format("{0}B", Math.Floor(@this / 100f) / 10f);
#endif
				}

				return @this.ToString();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Статус расположения значения в промежутке от начального до конечного включая границы
			/// </summary>
			/// <param name="this">Значение</param>
			/// <param name="from">Начальное значение</param>
			/// <param name="to">Конечное значение</param>
			/// <returns>Статус расположения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Between(this Int32 @this, Int32 from, Int32 to)
			{
				return (@this >= from && @this <= to);
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к 32-битному цветовому значению
			/// </summary>
			/// <param name="this">Цвет в формате RGBA</param>
			/// <returns>32-битное цветовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Color32 ToColor32(this Int32 @this)
			{
				UnityEngine.Color32 color_value = new UnityEngine.Color32();
				color_value.r = (Byte)(@this >> 24);
				color_value.g = (Byte)(@this >> 16);
				color_value.b = (Byte)(@this >> 8);
				color_value.a = (Byte)(@this >> 0);

				return color_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к цветовому значению
			/// </summary>
			/// <param name="this">Цвет в формате RGBA</param>
			/// <returns>Цветовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Color ToColor(this Int32 @this)
			{
				const Single factor = 1.0f / 255.0f;
				UnityEngine.Color color_value = new UnityEngine.Color();
				color_value.r = factor * (Byte)(@this >> 24);
				color_value.g = factor * (Byte)(@this >> 16);
				color_value.b = factor * (Byte)(@this >> 8);
				color_value.a = factor * (Byte)(@this >> 0);

				return color_value;
			}
#endif
			#endregion

			#region ======================================= UInt32 ====================================================
#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к 32-битному цветовому значению
			/// </summary>
			/// <param name="this">Цвет в формате RGBA</param>
			/// <returns>32-битное цветовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Color32 ToColor32(this UInt32 @this)
			{
				UnityEngine.Color32 color_value = new UnityEngine.Color32();
				color_value.r = (Byte)(@this >> 24);
				color_value.g = (Byte)(@this >> 16);
				color_value.b = (Byte)(@this >> 8);
				color_value.a = (Byte)(@this >> 0);

				return color_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к цветовому значению
			/// </summary>
			/// <param name="this">Цвет в формате RGBA</param>
			/// <returns>Цветовое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Color ToColor(this UInt32 @this)
			{
				const Single factor = 1.0f / 255.0f;
				UnityEngine.Color color_value = new UnityEngine.Color();
				color_value.r = factor * (Byte)(@this >> 24);
				color_value.g = factor * (Byte)(@this >> 16);
				color_value.b = factor * (Byte)(@this >> 8);
				color_value.a = factor * (Byte)(@this >> 0);

				return color_value;
			}
#endif
			#endregion

			#region ======================================= Single ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <remarks>
			/// Преобразование более быстрое чем стандартное, не поддерживается научный формат
			/// </remarks>
			/// <param name="this">Значение</param>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ToNormalizedString(this Single @this)
			{
				lock (FloatToStringBuffer)
				{
					const Int32 prec_mul = 100000;
					FloatToStringBuffer.Length = 0;
					var is_neg = @this < 0f;
					if (is_neg)
					{
						@this = -@this;
					}
					var v0 = (uint)@this;
					var diff = (@this - v0) * prec_mul;
					var v1 = (uint)diff;
					diff -= v1;
					if (diff > 0.5f)
					{
						v1++;
						if (v1 >= prec_mul)
						{
							v1 = 0;
							v0++;
						}
					}
					else
					{
						if (diff == 0.5f && (v1 == 0 || (v1 & 1) != 0))
						{
							v1++;
						}
					}
					if (v1 > 0)
					{
						var count = 5;
						while (v1 % 10 == 0)
						{
							count--;
							v1 /= 10;
						}

						do
						{
							count--;
							FloatToStringBuffer.Append((char)(v1 % 10 + '0'));
							v1 /= 10;
						}
						while (v1 > 0);
						while (count > 0)
						{
							count--;
							FloatToStringBuffer.Append('0');
						}
						FloatToStringBuffer.Append('.');
					}
					do
					{
						FloatToStringBuffer.Append((char)(v0 % 10 + '0'));
						v0 /= 10;
					}
					while (v0 > 0);
					if (is_neg)
					{
						FloatToStringBuffer.Append('-');
					}
					var i0 = 0;
					var i1 = FloatToStringBuffer.Length - 1;
					char c;
					while (i1 > i0)
					{
						c = FloatToStringBuffer[i0];
						FloatToStringBuffer[i0] = FloatToStringBuffer[i1];
						FloatToStringBuffer[i1] = c;
						i0++;
						i1--;
					}

					return FloatToStringBuffer.ToString();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Расширенное форматирование в строку
			/// </summary>
			/// <param name="this">Значение</param>
			/// <param name="format">Формат</param>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ToStringFormat(this Single @this, String format)
			{
				switch (format)
				{
					case "T":
						{
							var secs = (Int32)(@this % 60);
							var mins = (Int32)(@this / 60);
							return String.Format("{0:00}:{1:00}", mins, secs);
						}
					case "T1":
						{
							if (@this > 3600)
							{
								var hour = (Int32)(@this / 3600);
								var secs = (Int32)(@this % 60);
								var mins = (Int32)(@this / 60) - hour * 60;
								return String.Format("{0:00}:{1:00}:{2:00}", hour, mins, secs);
							}
							else
							{
								var secs = (Int32)(@this % 60);
								var mins = (Int32)(@this / 60);
								return String.Format("{0:00}:{1:00}:{2:00}", 0, mins, secs);
							}
						}
					case "P":
						{
							return (@this * 100).ToString("F0") + "%";
						}
					case "P1":
						{
							return (@this * 100).ToString("F1") + "%";
						}
					case "P2":
						{
							return (@this * 100).ToString("F2") + "%";
						}
					default:
						{
							return @this.ToString(format);
						}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Статус расположения значения в промежутке от начального до конечного включая границы
			/// </summary>
			/// <param name="this">Значение</param>
			/// <param name="from">Начальное значение</param>
			/// <param name="to">Конечное значение</param>
			/// <returns>Статус расположения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Between(this Single @this, Single from, Single to)
			{
				return (@this >= from && @this <= to);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================