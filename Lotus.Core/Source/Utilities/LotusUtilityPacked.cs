﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема утилит
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUtilityPacked.cs
*		Упаковка/распаковка данных в битовом формате.
*		Реализация упаковки/распаковки данных в битовом формате упаковки применяется при кэшированных данных, там где
*	на важна скорость и не имеет особого смыла полноценно хранить второй экземпляр данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreUtilities
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для упаковки/распаковки данных в битовом формате
		/// </summary>
		/// <remarks>
		/// Применяется для упаковки кэшированных данных.
		/// Следует применять там где на важна скорость и не имеет особого смыла полноценно хранить второй экземпляр данных
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XPacked
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Int32"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bitStart">Стартовый бит с которого записываются данные</param>
			/// <param name="bitCount">Количество бит для записи</param>
			/// <param name="value">Значение для упаковки (будет записано только указанное количество бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackInteger(ref Int32 pack, Int32 bitStart, Int32 bitCount, Int32 value)
			{
				var mask = (1 << bitCount) - 1;
				pack = (pack & ~(mask << bitStart)) | ((value & mask) << bitStart);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Int64"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bitStart">Стартовый бит с которого записываются данные</param>
			/// <param name="bitCount">Количество бит для записи</param>
			/// <param name="value">Значение для упаковки (будет записано только указанное количество бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackLong(ref Int64 pack, Int32 bitStart, Int32 bitCount, Int64 value)
			{
				var mask = (1L << bitCount) - 1L;
				pack = (pack & ~(mask << bitStart)) | ((value & mask) << bitStart);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Упаковка значения типа <see cref="System.Boolean"/> в битовое поле
			/// </summary>
			/// <param name="pack">Переменная куда будут упаковываться данные</param>
			/// <param name="bitStart">Стартовый бит с которого записываются данные</param>
			/// <param name="value">Значение для упаковки (будет записан только 1 бит)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PackBoolean(ref Int32 pack, Int32 bitStart, Boolean value)
			{
				var mask = (1 << 1) - 1;
				if (value)
				{
					pack = (pack & ~(mask << bitStart)) | ((1 & mask) << bitStart);
				}
				else
				{
					pack = (pack & ~(mask << bitStart)) | ((0 & mask) << bitStart);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Int32"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bitStart">Стартовый бит с которого начинается распаковка</param>
			/// <param name="bitCount">Количество бит для чтения</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 UnpackInteger(Int32 pack, Int32 bitStart, Int32 bitCount)
			{
				var mask = (1 << bitCount) - 1;
				return (pack >> bitStart) & mask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Int64"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bitStart">Стартовый бит с которого начинается распаковка</param>
			/// <param name="bitCount">Количество бит для чтения</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 UnpackLong(Int64 pack, Int32 bitStart, Int32 bitCount)
			{
				var mask = (1L << bitCount) - 1L;
				return (pack >> bitStart) & mask;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Распаковка значения типа <see cref="System.Boolean"/> из битового поля
			/// </summary>
			/// <param name="pack">Упакованные данные</param>
			/// <param name="bitStart">Стартовый бит с которого начинается распаковка</param>
			/// <returns>Распакованное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean UnpackBoolean(Int32 pack, Int32 bitStart)
			{
				var mask = (1 << 1) - 1;
				var data = (pack >> bitStart) & mask;
				if (data == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================