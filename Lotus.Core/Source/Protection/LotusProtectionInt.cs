﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusProtectionInt.cs
*		Защита целого числа.
*		Реализация механизма защиты (шифрования/декодирование) целого числа.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreProtection
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура-оболочка для защиты целого числа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[StructLayout(LayoutKind.Explicit)]
		public struct TProtectionInt
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Маска для шифрования/декодирование
			/// </summary>
			public const UInt32 XORMASK = 0XAAAAAAAA;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			[FieldOffset(0)]
			private Int32 mEncryptValue;

			[FieldOffset(0)]
			private UInt32 _convertValue;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Зашифрованное значение
			/// </summary>
			public Int32 EncryptedValue
			{
				get
				{
					// Обходное решение для конструктора структуры по умолчанию
					if (_convertValue == 0 && mEncryptValue == 0)
					{
						_convertValue = XORMASK;
					}

					return mEncryptValue;
				}
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в обычное целое число
			/// </summary>
			/// <param name="value">Структура-оболочка для защиты целого числа</param>
			/// <returns>Целое число</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Int32(TProtectionInt value)
			{
				value._convertValue ^= XORMASK;
				var original = value.mEncryptValue;
				value._convertValue ^= XORMASK;
				return original;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа структуры-оболочки для защиты целого числа
			/// </summary>
			/// <param name="value">Целое число</param>
			/// <returns>Структура-оболочка для защиты целого числа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TProtectionInt(Int32 value)
			{
				var protection = new TProtectionInt();
				protection.mEncryptValue = value;
				protection._convertValue ^= XORMASK;
				return protection;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================