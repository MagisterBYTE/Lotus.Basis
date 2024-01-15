//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема функций плавности
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusEasingMethods.cs
*		Реализация функция плавности, а также расширения для компонентов трансформации Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreEasing
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий основные функция плавности
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XEasing
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Коэффициент по умолчанию для функции Back
			/// </summary>
			public const Single CoefficientBack1Default = 2.70158f;

			/// <summary>
			/// Коэффициент по умолчанию для функции Back
			/// </summary>
			public const Single CoefficientBack2Default = 1.70158f;

			/// <summary>
			/// Коэффициент по умолчанию для функции Expo - основание степени
			/// </summary>
			public const Single CoefficientExpoBasisDefault = 2;

			/// <summary>
			/// Коэффициент по умолчанию для функции Expo - показатель степени
			/// </summary>
			public const Single CoefficientExpoDefault = 10;

			/// <summary>
			/// Коэффициент по умолчанию для функции Elastic - основание степени
			/// </summary>
			public const Single CoefficientElasticBasisDefault = 2;

			/// <summary>
			/// Коэффициент по умолчанию для функции Elastic - показатель степени
			/// </summary>
			public const Single CoefficientElasticDefault = 10;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Коэффициент для функции Back
			/// </summary>
			public static Single CoefficientBack1 = 2.70158f;

			/// <summary>
			/// Коэффициент для функции Back
			/// </summary>
			public static Single CoefficientBack2 = 1.70158f;

			/// <summary>
			/// Коэффициент для функции Expo - основание степени
			/// </summary>
			public static Single CoefficientExpoBasis = 2;

			/// <summary>
			/// Коэффициент для функции Expo - показатель степени
			/// </summary>
			public static Single CoefficientExpo = 10;

			/// <summary>
			/// Коэффициент для функции Elastic - основание степени
			/// </summary>
			public static Single CoefficientElasticBasis = 2;

			/// <summary>
			/// Коэффициент для функции Elastic - показатель степени
			/// </summary>
			public static Single CoefficientElastic = 10;
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easingType">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Interpolation(Double start, Double end, Double time, TEasingType easingType)
			{
				Double value = 0;
				switch (easingType)
				{
					case TEasingType.Linear:
						{
							value = Linear(start, end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(start, end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(start, end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(start, end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(start, end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(start, end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(start, end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(start, end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(start, end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(start, end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(start, end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(start, end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(start, end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(start, end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(start, end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(start, end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(start, end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(start, end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(start, end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easingType">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Interpolation(Single start, Single end, Single time, TEasingType easingType)
			{
				Single value = 0;
				switch (easingType)
				{
					case TEasingType.Linear:
						{
							value = Linear(start, end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(start, end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(start, end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(start, end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(start, end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(start, end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(start, end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(start, end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(start, end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(start, end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(start, end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(start, end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(start, end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(start, end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(start, end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(start, end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(start, end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(start, end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(start, end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 Interpolation(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time, TEasingType easing_type)
			{
				UnityEngine.Vector2 value = UnityEngine.Vector2.zero;
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							value = Linear(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(ref start, ref end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(ref start, ref end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(ref start, ref end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 Interpolation(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time, TEasingType easing_type)
			{
				UnityEngine.Vector2 value = UnityEngine.Vector2.zero;
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							value = Linear(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(ref start, ref end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(ref start, ref end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(ref start, ref end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 Interpolation(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time, TEasingType easing_type)
			{
				UnityEngine.Vector3 value = UnityEngine.Vector3.zero;
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							value = Linear(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(ref start, ref end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(ref start, ref end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(ref start, ref end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Интерполяция значения по указанной функции изменения скорости
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 Interpolation(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time, TEasingType easing_type)
			{
				UnityEngine.Vector3 value = UnityEngine.Vector3.zero;
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							value = Linear(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadIn:
						{
							value = QuadIn(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadOut:
						{
							value = QuadOut(ref start, ref end, time);
						}
						break;
					case TEasingType.QuadInOut:
						{
							value = QuadInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeIn:
						{
							value = CubeIn(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeOut:
						{
							value = CubeOut(ref start, ref end, time);
						}
						break;
					case TEasingType.CubeInOut:
						{
							value = CubeInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackIn:
						{
							value = BackIn(ref start, ref end, time);
						}
						break;
					case TEasingType.BackOut:
						{
							value = BackOut(ref start, ref end, time);
						}
						break;
					case TEasingType.BackInOut:
						{
							value = BackInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoIn:
						{
							value = ExpoIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoOut:
						{
							value = ExpoOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ExpoInOut:
						{
							value = ExpoInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineIn:
						{
							value = SineIn(ref start, ref end, time);
						}
						break;
					case TEasingType.SineOut:
						{
							value = SineOut(ref start, ref end, time);
						}
						break;
					case TEasingType.SineInOut:
						{
							value = SineInOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticIn:
						{
							value = ElasticIn(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticOut:
						{
							value = ElasticOut(ref start, ref end, time);
						}
						break;
					case TEasingType.ElasticInOut:
						{
							value = ElasticInOut(ref start, ref end, time);
						}
						break;
					default:
						break;
				}

				return value;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Linear =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значения
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Linear(Double start, Double end, Double time)
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
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Linear(Single start, Single end, Single time)
			{
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значения
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 Linear(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
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
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 Linear(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
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
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 Linear(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
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
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 Linear(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				return start + ((end - start) * time);
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Quad ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double QuadIn(Double start, Double end, Double time)
			{
				return start + ((end - start) * (time * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single QuadIn(Single start, Single end, Single time)
			{
				return start + ((end - start) * (time * time));
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = time * time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = time * time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = time * time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = time * time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double QuadOut(Double start, Double end, Double time)
			{
				time = 1.0f - ((1 - time) * (1 - time));
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single QuadOut(Single start, Single end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time));
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time));

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time));

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time));

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time));

				var result =  new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double QuadInOut(Double start, Double end, Double time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single QuadInOut(Single start, Single end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				return start + ((end - start) * time);
			}
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 QuadInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Квадратическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 QuadInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				if(time < 0.5f)
				{
					time = time * 2;
					time = time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Cube ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double CubeIn(Double start, Double end, Double time)
			{
				return start + ((end - start) * (time * time * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single CubeIn(Single start, Single end, Single time)
			{
				return start + ((end - start) * (time * time * time));
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = time * time * time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = time * time * time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = time * time * time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = time * time * time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double CubeOut(Double start, Double end, Double time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single CubeOut(Single start, Single end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double CubeInOut(Double start, Double end, Double time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single CubeInOut(Single start, Single end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 CubeInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кубическая интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 CubeInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = time * time * time / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = ((1.0f - ((1 - time) * (1 - time) * (1 - time))) / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Back ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BackIn(Double start, Double end, Double time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BackIn(Single start, Single end, Single time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BackOut(Double start, Double end, Double time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BackOut(Single start, Single end, Single time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = 1.0f - time;
				time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
				time = 1.0f - time;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BackInOut(Double start, Double end, Double time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BackInOut(Single start, Single end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BackInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BackInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * (time * time * (((CoefficientBack1 + 1) * time) - CoefficientBack1));
				}
				else
				{
					time -= 2;
					time = 0.5f * ((time * time * (((CoefficientBack1 + 1) * time) + CoefficientBack1)) + 2);
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Expo ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ExpoIn(Double start, Double end, Double time)
			{
				time = Math.Pow(CoefficientExpoBasis, CoefficientExpo * ((Single)time - 1));
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ExpoIn(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#else
				time = (Single)Math.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#endif
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ExpoOut(Double start, Double end, Double time)
			{
				time = -Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ExpoOut(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;
#else
				time = (Single)(-Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1);
#endif
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ExpoInOut(Double start, Double end, Double time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * Math.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
				}
				else
				{
					time--;
					time = 0.5f * (-Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ExpoInOut(Single start, Single end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
#if UNITY_2017_1_OR_NEWER
					time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#else
					time = (Single)(0.5f * Math.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1)));
#endif

				}
				else
				{
					time--;
#if UNITY_2017_1_OR_NEWER
					time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
#else
					time = (Single)(0.5f * (-Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2));
#endif
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
				}
				else
				{
					time--;
					time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ExpoInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
				}
				else
				{
					time--;
					time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
				}
				else
				{
					time--;
					time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Экспоненциальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ExpoInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time /= .5f;
				if (time < 1)
				{
					time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
				}
				else
				{
					time--;
					time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Sine ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double SineIn(Double start, Double end, Double time)
			{
				time = -Math.Cos(Math.PI / 2 * time) + 1.0;
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SineIn(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;
#else
				time = -(Single)Math.Cos(Math.PI / 2 * time) + 1.0f;
#endif
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double SineOut(Double start, Double end, Double time)
			{
				time = Math.Sin(Math.PI / 2 * time);
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SineOut(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);
#else
				time = (Single)Math.Sin(Math.PI / 2 * time);
#endif
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double SineInOut(Double start, Double end, Double time)
			{
				time = (-Math.Cos(Math.PI * time) / 2) + 0.5;

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single SineInOut(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;
#else
				time = -(Single)Math.Cos(Math.PI * time) / 2 + 0.5f;
#endif
				return start + ((end - start) * time);
			}
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 SineInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Синусоидальная интерполяция значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 SineInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Elastic ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ElasticIn(Double start, Double end, Double time)
			{
				time = 1 - time;
				time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075) * (2 * Math.PI) / 0.3)) + 1;
				time = 1 - time;
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ElasticIn(Single start, Single end, Single time)
			{
				time = 1 - time;
#if UNITY_2017_1_OR_NEWER
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
				time = (Single)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
				time = 1 - time;
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = 1 - time;
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
				time = 1 - time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = 1 - time;
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
				time = 1 - time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = 1 - time;
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
				time = 1 - time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = 1 - time;
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
				time = 1 - time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ElasticOut(Double start, Double end, Double time)
			{
				time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f)) + 1;
				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ElasticOut(Single start, Single end, Single time)
			{
#if UNITY_2017_1_OR_NEWER
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
				time = (Single)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ElasticInOut(Double start, Double end, Double time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075) * (2 * Math.PI) / 0.3)) + 1;
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075) * (2 * Math.PI) / 0.3)) + 1;
					time = (time / 2) + 0.5;
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ElasticInOut(Single start, Single end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
#if UNITY_2017_1_OR_NEWER
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
					time = (Single)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
#if UNITY_2017_1_OR_NEWER
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
					time = (Single)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
					time = (time / 2) + 0.5f;
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (time / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 ElasticInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (time / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (time / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Колебательная форма интерполяции значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 ElasticInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (1 - time) / 2;
				}
				else
				{
					time = (time * 2) - 1;
					time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
					time = (time / 2) + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion

			#region ======================================= МЕТОДЫ Bounce =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BounceIn(Double start, Double end, Double time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BounceIn(Single start, Single end, Single time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				time = 1 - time;
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}
				time = 1 - time;

				return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
					start.y + ((end.y - start.y) * time),
					start.z + ((end.z - start.z) * time));
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BounceOut(Double start, Double end, Double time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BounceOut(Single start, Single end, Single time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				if (time < 1 / 2.75f)
				{
					time = 7.5625f * time * time;
				}
				else if (time < 2 / 2.75f)
				{
					time -= 1.5f / 2.75f;
					time = (7.5625f * time * time) + .75f;
				}
				else if (time < 2.5 / 2.75)
				{
					time -= 2.25f / 2.75f;
					time = (7.5625f * time * time) + .9375f;
				}
				else
				{
					time -= 2.625f / 2.75f;
					time = (7.5625f * time * time) + .984375f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));
				return result;
			}
#else

#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double BounceInOut(Double start, Double end, Double time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				return start + ((end - start) * time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single BounceInOut(Single start, Single end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				return start + ((end - start) * time);
			}

#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector2 BounceInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Форма отскока значения в начале и в конце
			/// </summary>
			/// <param name="start">Начальное значение</param>
			/// <param name="end">Конечное значение</param>
			/// <param name="time">Время от 0 до 1</param>
			/// <returns>Текущие значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static UnityEngine.Vector3 BounceInOut(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
			{
				if (time < 0.5f)
				{
					time = time * 2;
					time = 1 - time;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}
					time = 1 - time;
					time = time * 0.5f;
				}
				else
				{
					time = time * 2;
					time = time - 1;
					if (time < 1 / 2.75f)
					{
						time = 7.5625f * time * time;
					}
					else if (time < 2 / 2.75f)
					{
						time -= 1.5f / 2.75f;
						time = (7.5625f * time * time) + .75f;
					}
					else if (time < 2.5 / 2.75)
					{
						time -= 2.25f / 2.75f;
						time = (7.5625f * time * time) + .9375f;
					}
					else
					{
						time -= 2.625f / 2.75f;
						time = (7.5625f * time * time) + .984375f;
					}

					time = time * 0.5f;
					time = time + 0.5f;
				}

				var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
											start.y + ((end.y - start.y) * time),
											start.z + ((end.z - start.z) * time));

				return result;
			}
#else

#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================