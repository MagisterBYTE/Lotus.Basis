//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Единицы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementUnitLength.cs
*		Определение основных единиц измерения длины.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		/// Дескриптор для описания единицы измерения длины
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUnitDescriptorLength : CUnitDescriptor<TUnitLength>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorLength()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unit">Единица измерения</param>
			/// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorLength(TUnitLength unit, Double сoeffToBase)
				: base(unit, сoeffToBase)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unit">Единица измерения</param>
			/// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения</param>
			/// <param name="internationalAbbv">Международная аббревиатура</param>
			/// <param name="rusAbbv">Русская аббревиатура</param>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorLength(TUnitLength unit, Double сoeffToBase, String internationalAbbv, String rusAbbv)
				: base(unit, сoeffToBase, internationalAbbv, rusAbbv)
			{
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для предоставления дескрипторов единицы измерения длины
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XUnitLength
		{
			/// <summary>
			/// Словарь дескрипторов единицы измерения длины
			/// </summary>
			public readonly static Dictionary<TUnitLength, CUnitDescriptorLength> Descriptors =
				new Dictionary<TUnitLength, CUnitDescriptorLength>()
			{
				{ TUnitLength.Undefined, new CUnitDescriptorLength() },
				{ TUnitLength.Meter, new CUnitDescriptorLength(TUnitLength.Meter, 1, "m", "м") },
				{ TUnitLength.Nanometer, new CUnitDescriptorLength(TUnitLength.Nanometer, 1e-9, "nm", "нм") },
				{ TUnitLength.Micrometer, new CUnitDescriptorLength(TUnitLength.Micrometer, 1e-6, "mk", "мкм") },
				{ TUnitLength.Millimeter, new CUnitDescriptorLength(TUnitLength.Millimeter, 1e-3, "mm", "мм") },
				{ TUnitLength.Centimeter, new CUnitDescriptorLength(TUnitLength.Centimeter, 1e-2, "cm", "см")},
				{ TUnitLength.Inch, new CUnitDescriptorLength(TUnitLength.Inch, 0.0254, "in", "дюйм")},
				{ TUnitLength.Decimeter, new CUnitDescriptorLength(TUnitLength.Decimeter, 1e-1, "dm", "дм")},
				{ TUnitLength.Foot, new CUnitDescriptorLength(TUnitLength.Foot, 0.3048, "ft", "фут")},
				{ TUnitLength.Yard, new CUnitDescriptorLength(TUnitLength.Yard, 0.9144, "yr", "ярд")},
				{ TUnitLength.Hectometre, new CUnitDescriptorLength(TUnitLength.Hectometre, 1e2, "gm", "гм")},
				{ TUnitLength.Kilometer, new CUnitDescriptorLength(TUnitLength.Kilometer, 1e3, "km", "км")},
			};
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Единица измерения длины
		/// </summary>
		/// <remarks>
		/// Значение 1 соответствуют единицы измерения по умолчанию коротая принята в СИ
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TUnitLength
		{
			/// <summary>
			/// Не определена
			/// </summary>
			Undefined = 0,

			/// <summary>
			/// Метр
			/// </summary>
			Meter = 1,

			/// <summary>
			/// Нанометр
			/// </summary>
			Nanometer,

			/// <summary>
			/// Микрометр
			/// </summary>
			Micrometer,

			/// <summary>
			/// Миллиметр
			/// </summary>
			Millimeter,

			/// <summary>
			/// Сантиметр
			/// </summary>
			Centimeter,

			/// <summary>
			/// Дюйм
			/// </summary>
			Inch,

			/// <summary>
			/// Дециметр
			/// </summary>
			Decimeter,

			/// <summary>
			/// Фут
			/// </summary>
			Foot,

			/// <summary>
			/// Ярд
			/// </summary>
			Yard,

			/// <summary>
			/// Гектометр
			/// </summary>
			Hectometre,

			/// <summary>
			/// Километр
			/// </summary>
			Kilometer,
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================