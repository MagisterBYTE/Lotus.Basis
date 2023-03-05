﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема атрибутов
// Подраздел: Атрибуты дополнительного описания поля/свойства объекта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAttributeAbbreviation.cs
*		Атрибут для определения аббревиатуры.
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
		//! \defgroup CoreAttribute Подсистема атрибутов
		//! Подсистема атрибутов содержит необходимые атрибуты для расширения функциональности и добавления новых
		//! характеристик к свойствам, методам и классам.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для определения аббревиатуры
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		public sealed class LotusAbbreviationAttribute : Attribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly String mName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Аббревиатура
			/// </summary>
			public String Name
			{
				get { return mName; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Аббревиатура</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusAbbreviationAttribute(String name)
			{
				mName = name;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================