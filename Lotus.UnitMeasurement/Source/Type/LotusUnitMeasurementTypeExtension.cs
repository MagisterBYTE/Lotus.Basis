//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Типы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementTypeExtension.cs
*		Статический класс реализующий методы расширений для типа TMeasurementType.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace UnitMeasurement
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup UnitMeasurement
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для типа <see cref="TMeasurementType"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMeasurementTypeExtension
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразования типа измерения из указанной строки
			/// </summary>
			/// <param name="value">Строка</param>
			/// <returns>Тип измерения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementType Parse(String value)
			{
				return ((TMeasurementType)Enum.Parse(typeof(TMeasurementType), value));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа соответствующей единицы измерения для указанного типа измерения
			/// </summary>
			/// <param name="measurement_type">Тип измерения</param>
			/// <returns>Тип единицы измерения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetUnitType(this TMeasurementType measurement_type)
			{
				Type type = default;
				switch (measurement_type)
				{
					case TMeasurementType.Undefined:
						{

						}
						break;
					case TMeasurementType.Thing:
						{
							type = typeof(TUnitThing);
						}
						break;
					case TMeasurementType.Length:
						{
							type = typeof(TUnitLength);
						}
						break;
					case TMeasurementType.Area:
						{
							type = typeof(TUnitArea);
						}
						break;
					default:
						break;
				}

				return (type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения единицы измерения по умолчанию для указанного типа измерения
			/// </summary>
			/// <param name="measurement_type">Тип измерения</param>
			/// <returns>Единица измерения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum GetUnitValueDefault(this TMeasurementType measurement_type)
			{
				Enum value = default;
				switch (measurement_type)
				{
					case TMeasurementType.Undefined:
						{
							value = TUnitThing.Undefined;
						}
						break;
					case TMeasurementType.Thing:
						{
							value = TUnitThing.Thing;
						}
						break;
					case TMeasurementType.Length:
						{
							value = TUnitLength.Meter;
						}
						break;
					case TMeasurementType.Area:
						{
							value = TUnitArea.SquareMeter;
						}
						break;
					default:
						break;
				}

				return (value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения единицы измерения для указанного типа измерения и указанной строки единицы измерения
			/// </summary>
			/// <param name="measurement_type">Тип измерения</param>
			/// <param name="unit_value">Строка единицы измерения</param>
			/// <returns>Единица измерения</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Enum GetUnitValueFromString(this TMeasurementType measurement_type, String unit_value)
			{
				Enum value = default;
				switch (measurement_type)
				{
					case TMeasurementType.Undefined:
						{
							value = TUnitThing.Undefined;
						}
						break;
					case TMeasurementType.Thing:
						{
							value = (Enum)Enum.Parse(typeof(TUnitThing), unit_value);
						}
						break;
					case TMeasurementType.Length:
						{
							value = (Enum)Enum.Parse(typeof(TUnitLength), unit_value);
						}
						break;
					case TMeasurementType.Area:
						{
							value = (Enum)Enum.Parse(typeof(TUnitArea), unit_value);
						}
						break;
					default:
						break;
				}

				return (value);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================