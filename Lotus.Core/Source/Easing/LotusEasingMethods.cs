using System;

namespace Lotus.Core
{
    /** \addtogroup CoreEasing
	*@{*/
    /// <summary>
    /// Статический класс реализующий основные функция плавности.
    /// </summary>
    public static class XEasing
    {
        #region Const
        /// <summary>
        /// Коэффициент по умолчанию для функции Back.
        /// </summary>
        public const float CoefficientBack1Default = 2.70158f;

        /// <summary>
        /// Коэффициент по умолчанию для функции Back.
        /// </summary>
        public const float CoefficientBack2Default = 1.70158f;

        /// <summary>
        /// Коэффициент по умолчанию для функции Expo - основание степени.
        /// </summary>
        public const float CoefficientExpoBasisDefault = 2;

        /// <summary>
        /// Коэффициент по умолчанию для функции Expo - показатель степени.
        /// </summary>
        public const float CoefficientExpoDefault = 10;

        /// <summary>
        /// Коэффициент по умолчанию для функции Elastic - основание степени.
        /// </summary>
        public const float CoefficientElasticBasisDefault = 2;

        /// <summary>
        /// Коэффициент по умолчанию для функции Elastic - показатель степени.
        /// </summary>
        public const float CoefficientElasticDefault = 10;
        #endregion

        #region Fields
        /// <summary>
        /// Коэффициент для функции Back.
        /// </summary>
        public static float CoefficientBack1 = 2.70158f;

        /// <summary>
        /// Коэффициент для функции Back.
        /// </summary>
        public static float CoefficientBack2 = 1.70158f;

        /// <summary>
        /// Коэффициент для функции Expo - основание степени.
        /// </summary>
        public static float CoefficientExpoBasis = 2;

        /// <summary>
        /// Коэффициент для функции Expo - показатель степени.
        /// </summary>
        public static float CoefficientExpo = 10;

        /// <summary>
        /// Коэффициент для функции Elastic - основание степени.
        /// </summary>
        public static float CoefficientElasticBasis = 2;

        /// <summary>
        /// Коэффициент для функции Elastic - показатель степени.
        /// </summary>
        public static float CoefficientElastic = 10;
        #endregion

        #region Main methods
        /// <summary>
        /// Интерполяция значения по указанной функции изменения скорости.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <param name="easingType">Тип функции скорости.</param>
        /// <returns>Текущие значение.</returns>
        public static double Interpolation(double start, double end, double time, TEasingType easingType)
        {
            double value = 0;
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

        /// <summary>
        /// Интерполяция значения по указанной функции изменения скорости.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <param name="easingType">Тип функции скорости.</param>
        /// <returns>Текущие значение.</returns>
        public static float Interpolation(float start, float end, float time, TEasingType easingType)
        {
            float value = 0;
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
		/// <summary>
		/// Интерполяция значения по указанной функции изменения скорости.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <param name="easing_type">Тип функции скорости.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Интерполяция значения по указанной функции изменения скорости.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <param name="easing_type">Тип функции скорости.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Интерполяция значения по указанной функции изменения скорости.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <param name="easing_type">Тип функции скорости.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Интерполяция значения по указанной функции изменения скорости.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <param name="easing_type">Тип функции скорости.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Linear methods
        /// <summary>
        /// Линейная интерполяция значения.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double Linear(double start, double end, double time)
        {
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Линейная интерполяция значения.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float Linear(float start, float end, float time)
        {
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Линейная интерполяция значения.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 Linear(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			return start + ((end - start) * time);
		}

		/// <summary>
		/// Линейная интерполяция значения.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 Linear(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			return start + ((end - start) * time);
		}

		/// <summary>
		/// Линейная интерполяция значения.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 Linear(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			return start + ((end - start) * time);
		}

		/// <summary>
		/// Линейная интерполяция значения.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 Linear(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			return start + ((end - start) * time);
		}
#else

#endif
        #endregion

        #region Quad methods
        /// <summary>
        /// Квадратическая интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double QuadIn(double start, double end, double time)
        {
            return start + ((end - start) * (time * time));
        }

        /// <summary>
        /// Квадратическая интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float QuadIn(float start, float end, float time)
        {
            return start + ((end - start) * (time * time));
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Квадратическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 QuadIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = time * time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Квадратическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 QuadIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = time * time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Квадратическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 QuadIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = time * time;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Квадратическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 QuadIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			time = time * time;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}
#else

#endif
        /// <summary>
        /// Квадратическая интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double QuadOut(double start, double end, double time)
        {
            time = 1.0f - ((1 - time) * (1 - time));
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Квадратическая интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float QuadOut(float start, float end, float time)
        {
            time = 1.0f - ((1 - time) * (1 - time));
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Квадратическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 QuadOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time));

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Квадратическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 QuadOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time));

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Квадратическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 QuadOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time));

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));
			return result;
		}

		/// <summary>
		/// Квадратическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Квадратическая интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double QuadInOut(double start, double end, double time)
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

        /// <summary>
        /// Квадратическая интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float QuadInOut(float start, float end, float time)
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
		/// <summary>
		/// Квадратическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Квадратическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Квадратическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Квадратическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Cube methods
        /// <summary>
        /// Кубическая интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double CubeIn(double start, double end, double time)
        {
            return start + ((end - start) * (time * time * time));
        }

        /// <summary>
        /// Кубическая интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float CubeIn(float start, float end, float time)
        {
            return start + ((end - start) * (time * time * time));
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Кубическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 CubeIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = time * time * time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Кубическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 CubeIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = time * time * time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Кубическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 CubeIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = time * time * time;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Кубическая интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 CubeIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			time = time * time * time;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}
#else

#endif

        /// <summary>
        /// Кубическая интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double CubeOut(double start, double end, double time)
        {
            time = 1.0f - ((1 - time) * (1 - time) * (1 - time));
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Кубическая интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float CubeOut(float start, float end, float time)
        {
            time = 1.0f - ((1 - time) * (1 - time) * (1 - time));
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Кубическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 CubeOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Кубическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 CubeOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Кубическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 CubeOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = 1.0f - ((1 - time) * (1 - time) * (1 - time));

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));
			return result;
		}

		/// <summary>
		/// Кубическая интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        /// <summary>
        /// Кубическая интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double CubeInOut(double start, double end, double time)
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

        /// <summary>
        /// Кубическая интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float CubeInOut(float start, float end, float time)
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
		/// <summary>
		/// Кубическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кубическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кубическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кубическая интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Back methods
        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BackIn(double start, double end, double time)
        {
            time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BackIn(float start, float end, float time)
        {
            time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 BackIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 BackIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 BackIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 BackIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}
#else

#endif
        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BackOut(double start, double end, double time)
        {
            time = 1.0f - time;
            time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
            time = 1.0f - time;
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BackOut(float start, float end, float time)
        {
            time = 1.0f - time;
            time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
            time = 1.0f - time;
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 BackOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - time;
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
			time = 1.0f - time;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 BackOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = 1.0f - time;
			time = time * time * ((CoefficientBack1 * time) - CoefficientBack2);
			time = 1.0f - time;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BackInOut(double start, double end, double time)
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

        /// <summary>
        /// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BackInOut(float start, float end, float time)
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
		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Кратковременное изменение на противоположенное и интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Expo methods
        /// <summary>
        /// Экспоненциальная интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ExpoIn(double start, double end, double time)
        {
            time = Math.Pow(CoefficientExpoBasis, CoefficientExpo * ((float)time - 1));
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Экспоненциальная интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ExpoIn(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#else
            time = (float)Math.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#endif
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Экспоненциальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ExpoIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ExpoIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 ExpoIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 ExpoIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			time = UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}
#else

#endif
        /// <summary>
        /// Экспоненциальная интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ExpoOut(double start, double end, double time)
        {
            time = -Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Экспоненциальная интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ExpoOut(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;
#else
            time = (float)(-Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1);
#endif
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Экспоненциальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ExpoOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ExpoOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 ExpoOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = -UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 1;

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));
			return result;
		}

		/// <summary>
		/// Экспоненциальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Экспоненциальная интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ExpoInOut(double start, double end, double time)
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

        /// <summary>
        /// Экспоненциальная интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ExpoInOut(float start, float end, float time)
        {
            time /= .5f;
            if (time < 1)
            {
#if UNITY_2017_1_OR_NEWER
				time = 0.5f * UnityEngine.Mathf.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1));
#else
                time = (float)(0.5f * Math.Pow(CoefficientExpoBasis, CoefficientExpo * (time - 1)));
#endif

            }
            else
            {
                time--;
#if UNITY_2017_1_OR_NEWER
				time = 0.5f * (-UnityEngine.Mathf.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2);
#else
                time = (float)(0.5f * (-Math.Pow(CoefficientExpoBasis, -CoefficientExpo * time) + 2));
#endif
            }

            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Экспоненциальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Экспоненциальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Sine methods
        /// <summary>
        /// Синусоидальная интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double SineIn(double start, double end, double time)
        {
            time = -Math.Cos(Math.PI / 2 * time) + 1.0;
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Синусоидальная интерполяция значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float SineIn(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;
#else
            time = -(float)Math.Cos(Math.PI / 2 * time) + 1.0f;
#endif
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Синусоидальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 SineIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 SineIn(ref UnityEngine.Vector3 start, ref UnityEngine.Vector3 end, Single time)
		{
			time = -UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI / 2 * time) + 1.0f;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}
#else

#endif
        /// <summary>
        /// Синусоидальная интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double SineOut(double start, double end, double time)
        {
            time = Math.Sin(Math.PI / 2 * time);
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Синусоидальная интерполяция значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float SineOut(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);
#else
            time = (float)Math.Sin(Math.PI / 2 * time);
#endif
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Синусоидальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 SineOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = UnityEngine.Mathf.Sin(UnityEngine.Mathf.PI / 2 * time);

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));
			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Синусоидальная интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double SineInOut(double start, double end, double time)
        {
            time = (-Math.Cos(Math.PI * time) / 2) + 0.5;

            return start + ((end - start) * time);
        }

        /// <summary>
        /// Синусоидальная интерполяция значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float SineInOut(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;
#else
            time = -(float)Math.Cos(Math.PI * time) / 2 + 0.5f;
#endif
            return start + ((end - start) * time);
        }
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Синусоидальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineInOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));

			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 SineInOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));

			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 SineInOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = (-UnityEngine.Mathf.Cos(UnityEngine.Mathf.PI * time) / 2) + 0.5f;

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));

			return result;
		}

		/// <summary>
		/// Синусоидальная интерполяция значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Elastic methods
        /// <summary>
        /// Колебательная форма интерполяции значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ElasticIn(double start, double end, double time)
        {
            time = 1 - time;
            time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075) * (2 * Math.PI) / 0.3)) + 1;
            time = 1 - time;
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Колебательная форма интерполяции значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ElasticIn(float start, float end, float time)
        {
            time = 1 - time;
#if UNITY_2017_1_OR_NEWER
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
            time = (float)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
            time = 1 - time;
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Колебательная форма интерполяции значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ElasticIn(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = 1 - time;
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
			time = 1 - time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ElasticIn(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = 1 - time;
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
			time = 1 - time;

			return new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time));
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 ElasticIn(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = 1 - time;
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
			time = 1 - time;

			return new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
				start.y + ((end.y - start.y) * time),
				start.z + ((end.z - start.z) * time));
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Колебательная форма интерполяции значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ElasticOut(double start, double end, double time)
        {
            time = (Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f)) + 1;
            return start + ((end - start) * time);
        }

        /// <summary>
        /// Колебательная форма интерполяции значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ElasticOut(float start, float end, float time)
        {
#if UNITY_2017_1_OR_NEWER
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
            time = (float)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Колебательная форма интерполяции значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ElasticOut(UnityEngine.Vector2 start, UnityEngine.Vector2 end, Single time)
		{
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector2 ElasticOut(ref UnityEngine.Vector2 start, ref UnityEngine.Vector2 end, Single time)
		{
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

			var result = new UnityEngine.Vector2(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time));
			return result;
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
		public static UnityEngine.Vector3 ElasticOut(UnityEngine.Vector3 start, UnityEngine.Vector3 end, Single time)
		{
			time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;

			var result = new UnityEngine.Vector3(start.x + ((end.x - start.x) * time),
										start.y + ((end.y - start.y) * time),
										start.z + ((end.z - start.z) * time));
			return result;
		}

		/// <summary>
		/// Колебательная форма интерполяции значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Колебательная форма интерполяции значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double ElasticInOut(double start, double end, double time)
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

        /// <summary>
        /// Колебательная форма интерполяции значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float ElasticInOut(float start, float end, float time)
        {
            if (time < 0.5f)
            {
                time = time * 2;
                time = 1 - time;
#if UNITY_2017_1_OR_NEWER
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
                time = (float)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
                time = (1 - time) / 2;
            }
            else
            {
                time = (time * 2) - 1;
#if UNITY_2017_1_OR_NEWER
				time = (UnityEngine.Mathf.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * UnityEngine.Mathf.Sin((time - 0.075f) * (2 * UnityEngine.Mathf.PI) / 0.3f)) + 1;
#else
                time = (float)(Math.Pow(CoefficientElasticBasis, -CoefficientElastic * time) * Math.Sin((time - 0.075f) * (2 * Math.PI) / 0.3f) + 1);
#endif
                time = (time / 2) + 0.5f;
            }

            return start + ((end - start) * time);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Колебательная форма интерполяции значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Колебательная форма интерполяции значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Колебательная форма интерполяции значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Колебательная форма интерполяции значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

        #region Bounce methods
        /// <summary>
        /// Форма отскока значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BounceIn(double start, double end, double time)
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

        /// <summary>
        /// Форма отскока значения в начале.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BounceIn(float start, float end, float time)
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
		/// <summary>
		/// Форма отскока значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Форма отскока значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BounceOut(double start, double end, double time)
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

        /// <summary>
        /// Форма отскока значения в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BounceOut(float start, float end, float time)
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
		/// <summary>
		/// Форма отскока значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
        /// <summary>
        /// Форма отскока значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static double BounceInOut(double start, double end, double time)
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

        /// <summary>
        /// Форма отскока значения в начале и в конце.
        /// </summary>
        /// <param name="start">Начальное значение.</param>
        /// <param name="end">Конечное значение.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Текущие значение.</returns>
        public static float BounceInOut(float start, float end, float time)
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
		/// <summary>
		/// Форма отскока значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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

		/// <summary>
		/// Форма отскока значения в начале и в конце.
		/// </summary>
		/// <param name="start">Начальное значение.</param>
		/// <param name="end">Конечное значение.</param>
		/// <param name="time">Время от 0 до 1.</param>
		/// <returns>Текущие значение.</returns>
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
    /**@}*/
}