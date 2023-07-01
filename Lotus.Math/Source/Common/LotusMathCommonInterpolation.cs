//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathCommonInterpolation.cs
*		Основные методы интерполяции данных.
*		Реализация основных методов интерполяции данных для нахождения промежуточных значений величины по имеющемуся
*	дискретному набору известных значений.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathCommon
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы интерполяции данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMathInterpolation
		{
			#region ======================================= МЕТОДЫ ИНТЕРПОЛЯЦИИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значения
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Lerp(Double start, Double end, Double time)
			{
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значения
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Lerp(Single start, Single end, Single time)
			{
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значения
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Byte Lerp(Byte start, Byte end, Single time)
			{
				return (Byte)(start + ((end - start) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кривая интерполяции по Эрмиту
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Hermite_interpolation
			/// </remarks>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Hermite(Double start, Double end, Double time)
			{
				return start + ((start - end) * (time * time * (3.0 - (2.0 * time))));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кривая интерполяции по Эрмиту
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Hermite_interpolation
			/// </remarks>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Hermite(Single start, Single end, Single time)
			{
				return start + ((start - end) * (time * time * (3.0f - (2.0f * time))));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция, этот метод интерполирует, ослабляя итоговое значение
			/// когда значение будет близко к границам
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Sinerp(Double start, Double end, Double time)
			{
				return Lerp(start, end, Math.Sin(time * XMath.PI_2_D));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция, этот метод интерполирует, ослабляя итоговое значение
			/// когда значение будет близко к границам
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Sinerp(Single start, Single end, Single time)
			{
				return Lerp(start, end, (Single)Math.Sin(time * XMath.PI_2_D));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция (cos), этот метод интерполирует, ослабляя итоговое значение
			/// когда значение будет близко к границам
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Coserp(Double start, Double end, Double time)
			{
				return Lerp(start, end, 1.0 - Math.Cos(time * XMath.PI_2_D));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция (cos), этот метод интерполирует, ослабляя итоговое значение
			/// когда значение будет близко к границам
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Coserp(Single start, Single end, Single time)
			{
				return Lerp(start, end, 1.0f - (Single)Math.Cos(time * XMath.PI_2_D));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs smooth (cubic Hermite) interpolation between 0 and 1
			/// </summary>
			/// <remarks>
			/// See https://en.wikipedia.org/wiki/Smoothstep
			/// </remarks>
			/// <param name="amount">Значение в границах от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SmoothStep(Single amount)
			{
				return amount <= 0 ? 0
					: amount >= 1 ? 1
					: amount * amount * (3 - (2 * amount));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a smooth(er) interpolation between 0 and 1 with 1st and 2nd order derivatives of zero at endpoints
			/// </summary>
			/// <remarks>
			/// See https://en.wikipedia.org/wiki/Smoothstep
			/// </remarks>
			/// <param name="amount">Значение в границах от 0 до 1</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SmootherStep(Single amount)
			{
				return amount <= 0 ? 0
					: amount >= 1 ? 1
					: amount * amount * amount * ((amount * ((amount * 6) - 15)) + 10);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Smoothstep - скалярная функция интерполяции, обычно используемая в компьютерной графике.
			/// Метод интерполирует итоговое значение между двумя входными значениями, основанными на третьем,
			/// который должен быть между первыми двумя. Возвращенное значение находится между 0 и 1
			/// </summary>
			/// <param name="start">Минимальное значение</param>
			/// <param name="end">Максимальное значение</param>
			/// <param name="value">Значение</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double SmoothStep(Double start,Double end, Double value)
			{
				value = XMath.Clamp(value, start, end);
				var v1 = (value - start) / (end - start);
				var v2 = (value - start) / (end - start);
				return (-2.0 * v1 * v1 * v1) + (3.0 * v2 * v2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Smoothstep - скалярная функция интерполяции, обычно используемая в компьютерной графике.
			/// Метод интерполирует итоговое значение между двумя входными значениями, основанными на третьем,
			/// который должен быть между первыми двумя. Возвращенное значение находится между 0 и 1
			/// </summary>
			/// <param name="start">Минимальное значение</param>
			/// <param name="end">Максимальное значение</param>
			/// <param name="value">Значение</param>
			/// <returns>Интерполированное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SmoothStep(Single start, Single end, Single value)
			{
				value = XMath.Clamp(value, start, end);
				var v1 = (value - start) / (end - start);
				var v2 = (value - start) / (end - start);
				return (-2.0f * v1 * v1 * v1) + (3.0f * v2 * v2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Гауссова функция
			/// </summary>
			///<remarks>
			/// Смотри http://en.wikipedia.org/wiki/Gaussian_function#Two-dimensional_Gaussian_function
			///</remarks>
			/// <param name="amplitude">Curve amplitude</param>
			/// <param name="x">Position X</param>
			/// <param name="y">Position Y</param>
			/// <param name="centerX">Center X</param>
			/// <param name="centerY">Center Y</param>
			/// <param name="sigmaX">Curve sigma X</param>
			/// <param name="sigmaY">Curve sigma Y</param>
			/// <returns>The result of Gaussian function</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Gauss(Double amplitude, Double x, Double y, Double centerX, Double centerY, 
				Double sigmaX, Double sigmaY)
			{
				var cx = x - centerX;
				var cy = y - centerY;

				var component_x = cx * cx / (2 * sigmaX * sigmaX);
				var component_y = cy * cy / (2 * sigmaY * sigmaY);

				return amplitude * Math.Exp(-(component_x + component_y));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Гауссова функция
			/// </summary>
			///<remarks>
			/// Смотри http://en.wikipedia.org/wiki/Gaussian_function#Two-dimensional_Gaussian_function
			///</remarks>
			/// <param name="amplitude">Curve amplitude</param>
			/// <param name="x">Position X</param>
			/// <param name="y">Position Y</param>
			/// <param name="centerX">Center X</param>
			/// <param name="centerY">Center Y</param>
			/// <param name="sigmaX">Curve sigma X</param>
			/// <param name="sigmaY">Curve sigma Y</param>
			/// <returns>The result of Gaussian function</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Gauss(Single amplitude, Single x, Single y, Single centerX, Single centerY,
				Single sigmaX, Single sigmaY)
			{
				var cx = x - centerX;
				var cy = y - centerY;

				var component_x = cx * cx / (2 * sigmaX * sigmaX);
				var component_y = cy * cy / (2 * sigmaY * sigmaY);

				return amplitude * (Single)Math.Exp(-(component_x + component_y));
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================