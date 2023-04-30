//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Единицы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementUnitMass.cs
*		Определение основных единиц измерения массы.
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
		/// Дескриптор для описания единицы измерения массы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUnitDescriptorMass : CUnitDescriptor<TUnitMass>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorMass()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unit">Единица измерения</param>
			/// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorMass(TUnitMass unit, Double сoeffToBase)
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
			public CUnitDescriptorMass(TUnitMass unit, Double сoeffToBase, String internationalAbbv, String rusAbbv)
				: base(unit, сoeffToBase, internationalAbbv, rusAbbv)
			{
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для предоставления дескрипторов единицы измерения массы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XUnitMass
		{
			/// <summary>
			/// Словарь дескрипторов единицы измерения массы
			/// </summary>
			public readonly static Dictionary<TUnitMass, CUnitDescriptorMass> Descriptors = 
				new Dictionary<TUnitMass, CUnitDescriptorMass>()
			{
				{ TUnitMass.Undefined, new CUnitDescriptorMass() },
				{ TUnitMass.Kilogram, new CUnitDescriptorMass(TUnitMass.Kilogram, 1, "kg", "кг") },
				{ TUnitMass.Nanogram, new CUnitDescriptorMass(TUnitMass.Nanogram, 1e-9, "ng", "нг") },
				{ TUnitMass.Microgram, new CUnitDescriptorMass(TUnitMass.Microgram, 1e-6, "mg", "мкг") },
				{ TUnitMass.Milligram, new CUnitDescriptorMass(TUnitMass.Milligram, 1e-3, "mmg", "ммг") },
				{ TUnitMass.Centigram, new CUnitDescriptorMass(TUnitMass.Centigram, 1e-2, "сg", "cг")},
				{ TUnitMass.Carat, new CUnitDescriptorMass(TUnitMass.Carat, 0.0002, "ct", "кар")},
				{ TUnitMass.Uncia, new CUnitDescriptorMass(TUnitMass.Uncia, 0.028349523125, "uncia", "унц")},
				{ TUnitMass.Pound, new CUnitDescriptorMass(TUnitMass.Pound, 0.45359237, "pondus", "фунт")},
				{ TUnitMass.Centner, new CUnitDescriptorMass(TUnitMass.Centner, 1e2, "centum", "цент")},
				{ TUnitMass.Tonne, new CUnitDescriptorMass(TUnitMass.Tonne, 1e3, "tonne", "тн")},
			};
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Единица измерения массы
		/// </summary>
		/// <remarks>
		/// Значение 1 соответствуют единицы измерения по умолчанию коротая принята в СИ
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TUnitMass
		{
			/// <summary>
			/// Не определена
			/// </summary>
			Undefined = 0,

			/// <summary>
			/// Килограмм
			/// </summary>
			Kilogram = 1,

			/// <summary>
			/// Нанограмм
			/// </summary>
			Nanogram,

			/// <summary>
			/// Микрограмм
			/// </summary>
			Microgram,

			/// <summary>
			/// Миллиграмм
			/// </summary>
			Milligram,

			/// <summary>
			/// Сантиграмм
			/// </summary>
			Centigram,

			/// <summary>
			/// Карат
			/// </summary>
			Carat,

			/// <summary>
			/// Грамм
			/// </summary>
			Gram,

			/// <summary>
			/// Унция
			/// </summary>
			Uncia,

			/// <summary>
			/// Фунт
			/// </summary>
			Pound,

			/// <summary>
			/// Центер
			/// </summary>
			Centner,

			/// <summary>
			/// Тонна
			/// </summary>
			Tonne,
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================