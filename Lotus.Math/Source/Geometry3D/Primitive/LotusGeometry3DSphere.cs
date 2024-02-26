using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Структура для представления сферы в трехметрном пространстве.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Sphere3Df : IEquatable<Sphere3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров сферы.
        /// </summary>
        public static string ToStringFormat = "Center = {0:0.00}, {1:0.00}; Radius = {3:0.00}";
        #endregion

        #region Fields
        /// <summary>
        /// Центр сферы.
        /// </summary>
        public Vector3Df Center;

        /// <summary>
        /// Радиус сферы.
        /// </summary>
        public float Radius;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует сферу указанными параметрами.
        /// </summary>
        /// <param name="position">Центр сферы.</param>
        /// <param name="radius">Радиус сферы.</param>
        public Sphere3Df(Vector3Df position, float radius)
        {
            Center = position;
            Radius = radius;
        }

        /// <summary>
        /// Конструктор инициализирует сфера указанной окружностью.
        /// </summary>
        /// <param name="source">Сфера.</param>
        public Sphere3Df(Sphere3Df source)
        {
            Center = source.Center;
            Radius = source.Radius;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует сфера указанными параметрами.
		/// </summary>
		/// <param name="center">Центр сферы.</param>
		/// <param name="radius">Радиус сферы.</param>
		public Sphere3Df(UnityEngine.Vector3 center, Single radius)
		{
			Center = new Vector3Df(center.x, center.y, center.z);
			Radius = radius;
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
            if (obj is Sphere3Df sphere)
            {
                return Equals(sphere);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства сфер по значению.
        /// </summary>
        /// <param name="other">Сравниваемая сфера.</param>
        /// <returns>Статус равенства сфер.</returns>
        public readonly bool Equals(Sphere3Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Получение хеш-кода сферы.
        /// </summary>
        /// <returns>Хеш-код сферы.</returns>
        public override readonly int GetHashCode()
        {
            return Center.GetHashCode() ^ Radius.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление сферы с указанием значений.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Center.X, Center.Y, Radius);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление сферы с указанием значений.</returns>
        public readonly string ToString(string format)
        {
            return "Center = " + Center.ToString(format) + "; Radius = " + Radius.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение сфер на равенство.
        /// </summary>
        /// <param name="left">Первая сфера.</param>
        /// <param name="right">Вторая сфера.</param>
        /// <returns>Статус равенства сфер.</returns>
        public static bool operator ==(Sphere3Df left, Sphere3Df right)
        {
            return left.Center == right.Center && left.Radius == right.Radius;
        }

        /// <summary>
        /// Сравнение сфер на неравенство.
        /// </summary>
        /// <param name="left">Первая сфера.</param>
        /// <param name="right">Вторая сфера.</param>
        /// <returns>Статус неравенства сфер.</returns>
        public static bool operator !=(Sphere3Df left, Sphere3Df right)
        {
            return left.Center != right.Center || left.Radius != right.Radius;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка на попадание точки в область сферы.
        /// </summary>
        /// <param name="point">Проверяемая точка.</param>
        /// <returns>Статус попадания.</returns>
        public readonly bool Contains(in Vector3Df point)
        {
            var d = Vector3Df.Distance(in Center, in point);
            return Math.Abs(d - Radius) < XGeometry3D.Eplsilon_f;
        }
        #endregion
    }
    /**@}*/
}