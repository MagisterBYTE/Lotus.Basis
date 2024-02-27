using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry2D
	*@{*/
    /// <summary>
    /// Структура линии в двухмерном пространстве.
    /// </summary>
    /// <remarks>
    /// Линия представляют собой точку и направление, при этом направление рассматривается в обе стороны.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Line2Df : IEquatable<Line2Df>, IComparable<Line2Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров линии.
        /// </summary>
        public static string ToStringFormat = "Pos = {0:0.00}, {1:0.00}; Dir = {2:0.00}, {3:0.00}";

        /// <summary>
        /// Горизонтальная ось.
        /// </summary>
        public readonly static Line2Df XAxis = new(Vector2Df.Zero, Vector2Df.Right);

        /// <summary>
        /// Вертикальная ось.
        /// </summary>
        public readonly static Line2Df YAxis = new(Vector2Df.Zero, Vector2Df.Up);
        #endregion

        #region Fields
        /// <summary>
        /// Позиция линии.
        /// </summary>
        public Vector2Df Position;

        /// <summary>
        /// Направление линии.
        /// </summary>
        public Vector2Df Direction;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует линию указанными параметрами.
        /// </summary>
        /// <param name="pos">Позиция линии.</param>
        /// <param name="dir">Направление линии.</param>
        public Line2Df(Vector2Df pos, Vector2Df dir)
        {
            Position = pos;
            Direction = dir;
        }

        /// <summary>
        /// Конструктор инициализирует линию указанной линией.
        /// </summary>
        /// <param name="source">Линия.</param>
        public Line2Df(Line2Df source)
        {
            Position = source.Position;
            Direction = source.Direction;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует линию указанными параметрами.
		/// </summary>
		/// <param name="pos">Позиция линии.</param>
		/// <param name="dir">Направление линии.</param>
		public Line2Df(UnityEngine.Vector2 pos, UnityEngine.Vector2 dir)
		{
			Position = new Vector2Df(pos.x, pos.y);
			Direction = new Vector2Df(dir.x, dir.y);
		}

		/// <summary>
		/// Конструктор инициализирует линию указанным лучом.
		/// </summary>
		/// <param name="ray">Луч.</param>
		public Line2Df(UnityEngine.Ray2D ray)
		{
			Position = new Vector2Df(ray.origin.x, ray.origin.y);
			Direction = new Vector2Df(ray.direction.x, ray.direction.y);
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
            if (obj is Line2Df line)
            {
                return Equals(line);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства линий по значению.
        /// </summary>
        /// <param name="other">Сравниваемая линия.</param>
        /// <returns>Статус равенства линий.</returns>
        public readonly bool Equals(Line2Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение линий для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый линия.</param>
        /// <returns>Статус сравнения линий.</returns>
        public readonly int CompareTo(Line2Df other)
        {
            if (Position > other.Position)
            {
                return 1;
            }
            else
            {
                if (Position == other.Position && Direction > other.Direction)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода линии.
        /// </summary>
        /// <returns>Хеш-код линии.</returns>
        public override readonly int GetHashCode()
        {
            return Position.GetHashCode() ^ Direction.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление линии с указанием значений.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Position.X, Position.Y, Direction.X, Direction.Y);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление линии с указанием значений.</returns>
        public readonly string ToString(string format)
        {
            return "Pos = " + Position.ToString(format) + "; Dir = " + Direction.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение линий на равенство.
        /// </summary>
        /// <param name="left">Первая линия.</param>
        /// <param name="right">Вторая линия.</param>
        /// <returns>Статус равенства линий.</returns>
        public static bool operator ==(Line2Df left, Line2Df right)
        {
            return left.Position == right.Position && left.Direction == right.Direction;
        }

        /// <summary>
        /// Сравнение линий на неравенство.
        /// </summary>
        /// <param name="left">Первая линия.</param>
        /// <param name="right">Вторая линия.</param>
        /// <returns>Статус неравенства линий.</returns>
        public static bool operator !=(Line2Df left, Line2Df right)
        {
            return left.Position != right.Position || left.Direction != right.Direction;
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Ray2D"/>.
		/// </summary>
		/// <param name="line">Линия.</param>
		/// <returns>Объект <see cref="UnityEngine.Ray2D"/>.</returns>
		public static implicit operator UnityEngine.Ray2D(Line2Df line)
		{
			return new UnityEngine.Ray2D(line.Position, line.Direction);
		}
#endif
        #endregion

        #region Main methods
        /// <summary>
        /// Получение точки на линии.
        /// </summary>
        /// <param name="position">Позиция точки от начала линии.</param>
        /// <returns>Точка на линии.</returns>
        public readonly Vector2Df GetPoint(float position)
        {
            return Position + (Direction * position);
        }

        /// <summary>
        /// Установка параметров линии.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="endPoint">Конечная точка.</param>
        public void SetFromPoint(in Vector2Df startPoint, in Vector2Df endPoint)
        {
            Position = startPoint;
            Direction = (endPoint - startPoint).Normalized;
        }
        #endregion
    }
    /**@}*/
}