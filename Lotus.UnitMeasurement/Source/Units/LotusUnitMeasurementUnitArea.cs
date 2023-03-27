//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Единицы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementUnitArea.cs
*		Определение основных единиц измерения площади.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
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
		//! \addtogroup UnitMeasurement
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Дескриптор для описания единицы измерения площади
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUnitDescriptorArea : CUnitDescriptor<TUnitArea>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными параметрами
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorArea()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="unit">Единица измерения</param>
			/// <param name="сoeffToBase">Коэффициент для преобразования в базовую единицу измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUnitDescriptorArea(TUnitArea unit, Double сoeffToBase)
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
			public CUnitDescriptorArea(TUnitArea unit, Double сoeffToBase, String internationalAbbv, String rusAbbv)
				: base(unit, сoeffToBase, internationalAbbv, rusAbbv)
			{
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для предоставления дескрипторов единицы измерения площади
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XUnitArea
		{
			/// <summary>
			/// Словарь дескрипторов единицы измерения площади
			/// </summary>
			public readonly static Dictionary<TUnitArea, CUnitDescriptorArea> Descriptors =
				new Dictionary<TUnitArea, CUnitDescriptorArea>()
			{
				{ TUnitArea.Undefined, new CUnitDescriptorArea() },
				{ TUnitArea.SquareMeter, new CUnitDescriptorArea(TUnitArea.SquareMeter, 1, "m2", "м2") },
				{ TUnitArea.SquareNanometer, new CUnitDescriptorArea(TUnitArea.SquareNanometer, 1e-18, "nm2", "нм2") },
				{ TUnitArea.SquareMicrometer, new CUnitDescriptorArea(TUnitArea.SquareMicrometer, 1e-12, "mkm2", "мкм2") },
				{ TUnitArea.SquareMillimeter, new CUnitDescriptorArea(TUnitArea.SquareMillimeter, 1e-6, "mm2", "мм2") },
				{ TUnitArea.SquareCentimeter, new CUnitDescriptorArea(TUnitArea.SquareCentimeter, 1e-4, "cm2", "см2")},
				{ TUnitArea.SquareInch, new CUnitDescriptorArea(TUnitArea.SquareInch, 0.0254 * 0.0254, "in2", "дюйм2")},
				{ TUnitArea.SquareDecimeter, new CUnitDescriptorArea(TUnitArea.SquareDecimeter, 1e-2, "dm2", "дм2")},
				{ TUnitArea.SquareFoot, new CUnitDescriptorArea(TUnitArea.SquareFoot, 0.3048 * 0.3048, "ft2", "фут2")},
				{ TUnitArea.SquareYard, new CUnitDescriptorArea(TUnitArea.SquareYard, 0.9144 * 0.9144, "yr2", "ярд2")},
				{ TUnitArea.Hectare, new CUnitDescriptorArea(TUnitArea.Hectare, 1e4, "ha", "га")},
				{ TUnitArea.SquareKilometer, new CUnitDescriptorArea(TUnitArea.SquareKilometer, 1e6, "km2", "км2")},
				{ TUnitArea.Acre, new CUnitDescriptorArea(TUnitArea.Acre, 4046.8564224, "ac", "акр")},
			};
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Единица измерения площади
		/// </summary>
		/// <remarks>
		/// Значение 1 соответствуют единицы измерения по умолчанию коротая принята в СИ
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TUnitArea
		{
			/// <summary>
			/// Не определена
			/// </summary>
			Undefined = 0,

			/// <summary>
			/// Квадратный метр
			/// </summary>
			SquareMeter = 1,

			/// <summary>
			/// Квадратный нанометр
			/// </summary>
			SquareNanometer,

			/// <summary>
			/// Квадратный микрометр
			/// </summary>
			SquareMicrometer,

			/// <summary>
			/// Квадратный миллиметр
			/// </summary>
			SquareMillimeter,

			/// <summary>
			/// Квадратный сантиметр
			/// </summary>
			SquareCentimeter,

			/// <summary>
			/// Квадратный дюйм
			/// </summary>
			SquareInch,

			/// <summary>
			/// Квадратный дециметр
			/// </summary>
			SquareDecimeter,

			/// <summary>
			/// Квадратный фут
			/// </summary>
			SquareFoot,

			/// <summary>
			/// Квадратный ярд
			/// </summary>
			SquareYard,

			/// <summary>
			/// Гектар
			/// </summary>
			Hectare,

			/// <summary>
			/// Квадратный километр
			/// </summary>
			SquareKilometer,

			/// <summary>
			/// Акр
			/// </summary>
			Acre,
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================