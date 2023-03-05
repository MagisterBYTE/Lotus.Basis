﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема работы с датой и временем
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDateTimeConstants.cs
*		Константные данные для работы с датой и временем.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreDateTime
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для форматов даты
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XDateFormats
		{
			/// <summary>
			/// Формат даты по умолчанию
			/// </summary>
			public const String Default = "dd.MM.yyyy";

			/// <summary>
			/// Формат даты по GraphQL
			/// </summary>
			public const String History = "MM/dd/yyyy";
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для форматов времени
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XTimeFormats
		{
			/// <summary>
			/// Формат времени по умолчанию
			/// </summary>
			public const String Default = "HH:mm";
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================