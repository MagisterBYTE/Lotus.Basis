﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDelegateExtension.cs
*		Методы расширения работы с делегатами.
*		Реализация максимально обобщенных расширений направленных на работу с делегатами и событиями.
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
		//! \addtogroup CoreExtension
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений с делегатами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDelegateExtension
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие обработчик в списке делегатов
			/// </summary>
			/// <param name="this">Делегат</param>
			/// <param name="delegat">Проверяемый делегат</param>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean HasHandler(this Action @this, Action delegat)
			{
				return @this.GetInvocationList().Contains(delegat);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================