﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сериализации
// Подраздел: Атрибуты подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationAttributeMemberAsReference.cs
*		Атрибут для определения сериализации свойства/поля как ссылки.
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
		//! \addtogroup CoreSerialization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут для определения сериализации свойства/поля как ссылки
		/// </summary>
		/// <remarks>
		/// Если поле/свойство имеет тип класса, т.е ссылочного типа иногда требуется не сохранять его данные, а лишь
		/// сохранить ссылку на этот объект.
		/// При этом объем сохраняемых данные должен быть достаточным чтобы после загрузки всех данных 
		/// мы смогли их восстановить и связать
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
		public sealed class LotusSerializeMemberAsReferenceAttribute : Attribute
		{
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================