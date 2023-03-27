﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusObfuscator.cs
*		Простой обфускатор данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreProtection Подсистема защиты
		//! Подсистема защиты представляет собой подсистему работы с криптографическим средствами, работу с
		//! утверждениями, а несложные механизмы защиты (шифрования/декодирование) для примитивных типов данных,
		//! где используются простые методы - манипулирование и операции с битами и циклические сдвиги.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для обфускации примитивных данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XObfuscator
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Маска для шифрования/декодирование
			/// </summary>
			public const Byte XOR_MASK = 0x53;
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Закодировать строку
			/// </summary>
			/// <param name="original">Оригинальная строка</param>
			/// <returns>Закодированная строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String Encode(String original)
			{
				Byte[] data = Encoding.UTF8.GetBytes(original);
				for (Int32 i = 0; i < data.Length; i++)
				{
					data[i] = (Byte)(data[i] ^ XOR_MASK);
				}
				return Convert.ToBase64String(data);
			}
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Раскодировать строку
			/// </summary>
			/// <param name="decode">Закодированная строка</param>
			/// <returns>Оригинальная строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String Decode(String decode)
			{
				Byte[] data = Convert.FromBase64String(decode);
				for (Int32 i = 0; i < data.Length; i++)
				{
					data[i] = (Byte)(data[i] ^ XOR_MASK);
				}
				return Encoding.UTF8.GetString(data);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================