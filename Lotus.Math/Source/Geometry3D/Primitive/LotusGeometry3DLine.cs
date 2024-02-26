using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Структура линии в трехмерном пространстве.
    /// </summary>
    /// <remarks>
    /// Линия представляют собой точку и направление, при этом направление рассматривается в обе стороны.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Line3Df : IEquatable<Line3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров линии.
        /// </summary>
        public static string ToStringFormat = "Pos = {0:0.00}, {1:0.00}, {2:0.00}; Dir = {3:0.00}, {4:0.00}, {5:0.00}";

        /// <summary>
        /// Ось по X.
        /// </summary>
        public readonly static Line3Df XAxis = new Line3Df(Vector3Df.Zero, Vector3Df.Right);

        /// <summary>
        /// Ось по Y.
        /// </summary>
        public readonly static Line3Df YAxis = new Line3Df(Vector3Df.Zero, Vector3Df.Up);

        /// <summary>
        /// Ось по Z.
        /// </summary>
        public readonly static Line3Df ZAxis = new Line3Df(Vector3Df.Zero, Vector3Df.Forward);
        #endregion

        #region Fields
        /// <summary>
        /// Позиция линии.
        /// </summary>
        public Vector3Df Position;

        /// <summary>
        /// Направление линии.
        /// </summary>
        public Vector3Df Direction;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует линию указанными параметрами.
        /// </summary>
        /// <param name="pos">Позиция линии.</param>
        /// <param name="dir">Направление линии.</param>
        public Line3Df(Vector3Df pos, Vector3Df dir)
        {
            Position = pos;
            Direction = dir;
        }

        /// <summary>
        /// Конструктор инициализирует линию указанной линией.
        /// </summary>
        /// <param name="source">Линия.</param>
        public Line3Df(Line3Df source)
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
		public Line3Df(UnityEngine.Vector3 pos, UnityEngine.Vector3 dir)
		{
			Position = new Vector3Df(pos.x, pos.y, pos.z);
			Direction = new Vector3Df(dir.x, dir.y, dir.z);
		}

		/// <summary>
		/// Конструктор инициализирует линию указанным лучом.
		/// </summary>
		/// <param name="ray">Луч.</param>
		public Line3Df(UnityEngine.Ray ray)
		{
			Position = new Vector3Df(ray.origin.x, ray.origin.y, ray.origin.z);
			Direction = new Vector3Df(ray.direction.x, ray.direction.y, ray.direction.z);
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
            if (obj is Line3Df line)
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
        public readonly bool Equals(Line3Df other)
        {
            return this == other;
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
            return string.Format(ToStringFormat, Position.X, Position.Y, Position.Z,
                Direction.X, Direction.Y, Direction.Z);
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
        public static bool operator ==(Line3Df left, Line3Df right)
        {
            return left.Position == right.Position && left.Direction == right.Direction;
        }

        /// <summary>
        /// Сравнение линий на неравенство.
        /// </summary>
        /// <param name="left">Первая линия.</param>
        /// <param name="right">Вторая линия.</param>
        /// <returns>Статус неравенства линий.</returns>
        public static bool operator !=(Line3Df left, Line3Df right)
        {
            return left.Position != right.Position || left.Direction != right.Direction;
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Ray"/>.
		/// </summary>
		/// <param name="line">Линия.</param>
		/// <returns>Объект <see cref="UnityEngine.Ray"/>.</returns>
		public static implicit operator UnityEngine.Ray(Line3Df line)
		{
			return new UnityEngine.Ray(line.Position, line.Direction);
		}
#endif
        #endregion

        #region Main methods
        /// <summary>
        /// Получение точки на линии.
        /// </summary>
        /// <param name="position">Позиция точки от начала линии.</param>
        /// <returns>Точка на линии.</returns>
        public readonly Vector3Df GetPoint(float position)
        {
            return Position + (Direction * position);
        }

        /// <summary>
        /// Установка параметров линии.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="endPoint">Конечная точка.</param>
        public void SetFromPoint(in Vector3Df startPoint, in Vector3Df endPoint)
        {
            Position = startPoint;
            Direction = (endPoint - startPoint).Normalized;
        }
        #endregion
    }
    /**@}*/
}