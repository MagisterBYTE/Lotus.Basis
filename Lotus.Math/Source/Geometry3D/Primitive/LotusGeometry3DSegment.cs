using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Структура сегмента(отрезка) в трехмерном пространстве.
    /// </summary>
    /// <remarks>
    /// Сегмент(отрезок) характеризуется начальной и конечной точкой.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Segment3Df : IEquatable<Segment3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров сегмента.
        /// </summary>
        public static string ToStringFormat = "Start = {0:0.00}, {1:0.00}, {2:0.00}; End = {3:0.00}, {4:0.00}, {5:0.00}";
        #endregion

        #region Fields
        /// <summary>
        /// Начало сегмента.
        /// </summary>
        public Vector3Df Start;

        /// <summary>
        /// Окончание сегмента.
        /// </summary>
        public Vector3Df End;
        #endregion

        #region Properties
        /// <summary>
        /// Центр сегмента.
        /// </summary>
        public readonly Vector3Df Location
        {
            get { return (Start + End) / 2; }
        }

        /// <summary>
        /// Направление сегмента.
        /// </summary>
        public readonly Vector3Df Direction
        {
            get { return End - Start; }
        }

        /// <summary>
        /// Единичное направление сегмента.
        /// </summary>
        public readonly Vector3Df DirectionUnit
        {
            get { return (End - Start).Normalized; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует сегмент указанными параметрами.
        /// </summary>
        /// <param name="start">Начало сегмента.</param>
        /// <param name="end">Окончание сегмента.</param>
        public Segment3Df(Vector3Df start, Vector3Df end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Конструктор инициализирует сегмент указанной линией.
        /// </summary>
        /// <param name="source">Сегмент.</param>
        public Segment3Df(Segment3Df source)
        {
            Start = source.Start;
            End = source.End;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует сегмент указанными параметрами.
		/// </summary>
		/// <param name="start">Начало сегмента.</param>
		/// <param name="end">Окончание сегмента.</param>
		public Segment3Df(UnityEngine.Vector3 start, UnityEngine.Vector3 end)
		{
			Start = new Vector3Df(start.x, start.y, start.z);
			End = new Vector3Df(end.x, end.y, end.z);
		}

		/// <summary>
		/// Конструктор инициализирует сегмент указанным лучом.
		/// </summary>
		/// <param name="ray">Луч.</param>
		/// <param name="length">Длина сегмента.</param>
		public Segment3Df(UnityEngine.Ray ray, Single length)
		{
			Start = new Vector3Df(ray.origin.x, ray.origin.y, ray.origin.z);
			End = new Vector3Df(ray.origin.x + (ray.direction.x * length), 
				ray.origin.y + (ray.direction.y * length),
				ray.origin.z + (ray.direction.z * length));
		}
#endif
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is Segment3Df segment)
            {
                return Equals(segment);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства сегментов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый сегмент.</param>
        /// <returns>Статус равенства сегментов.</returns>
        public readonly bool Equals(Segment3Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Получение хеш-кода сегмента.
        /// </summary>
        /// <returns>Хеш-код сегмента.</returns>
        public override readonly int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление сегмента с указанием значений.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Start.X, Start.Y, Start.Z, End.X, End.Y, End.Z);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление сегмента с указанием значений.</returns>
        public readonly string ToString(string format)
        {
            return "Start = " + Start.ToString(format) + "; End = " + End.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение сегментов на равенство.
        /// </summary>
        /// <param name="left">Первый сегмент.</param>
        /// <param name="right">Второй сегмент.</param>
        /// <returns>Статус равенства сегментов.</returns>
        public static bool operator ==(Segment3Df left, Segment3Df right)
        {
            return left.Start == right.Start && left.End == right.End;
        }

        /// <summary>
        /// Сравнение сегментов на неравенство.
        /// </summary>
        /// <param name="left">Первый сегмент.</param>
        /// <param name="right">Второй сегмент.</param>
        /// <returns>Статус неравенства сегментов.</returns>
        public static bool operator !=(Segment3Df left, Segment3Df right)
        {
            return left.Start != right.Start || left.End != right.End;
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Ray"/>.
		/// </summary>
		/// <param name="segment">Сегмент.</param>
		/// <returns>Объект <see cref="UnityEngine.Ray"/>.</returns>
		public static implicit operator UnityEngine.Ray(Segment3Df segment)
		{
			return new UnityEngine.Ray(segment.Start, segment.DirectionUnit);
		}
#endif
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка на попадание точки на сегмент.
        /// </summary>
        /// <param name="point">Проверяемая точка.</param>
        /// <returns>Статус попадания.</returns>
        public readonly bool Contains(in Vector3Df point)
        {
            return XIntersect3D.PointSegment(in point, in Start, in End);
        }
        #endregion
    }
    /**@}*/
}