using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Плоскость в 3D пространстве.
    /// </summary>
    /// <remarks>
    /// Реализация плоскости в 3D пространстве.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Plane3D
    {
        #region Fields 
        /// <summary>
        /// Нормаль плоскости.
        /// </summary>
        public Vector3D Normal;

        /// <summary>
        /// Расстояние до плоскости.
        /// </summary>
        public double Distance;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует плоскость по заданным компонентам.
        /// </summary>
        /// <param name="a">X - нормаль.</param>
        /// <param name="b">Y - нормаль.</param>
        /// <param name="c">Z - нормаль.</param>
        /// <param name="d">Расстояние до плоскости.</param>
        public Plane3D(double a, double b, double c, double d)
        {
            Normal.X = a;
            Normal.Y = b;
            Normal.Z = c;
            Distance = d;
        }

        /// <summary>
        /// Конструктор инициализирует плоскости через точку принадлежащей плоскости и вектора нормали.
        /// </summary>
        /// <param name="point">Координаты точки принадлежащей плоскости.</param>
        /// <param name="normal">Вектор нормали к плоскости.</param>
        public Plane3D(in Vector3D point, in Vector3D normal)
        {
            Normal.X = normal.X;
            Normal.Y = normal.Y;
            Normal.Z = normal.Z;
            Distance = -(normal * point);
        }

        /// <summary>
        /// Конструктор инициализирует плоскости через 3 точки принадлежащие плоскости.
        /// </summary>
        /// <param name="p1">Точка принадлежащая плоскости.</param>
        /// <param name="p2">Точка принадлежащая плоскости.</param>
        /// <param name="p3">Точка принадлежащая плоскости.</param>
        public Plane3D(in Vector3D p1, in Vector3D p2, in Vector3D p3)
        {
            Vector3D v1 = p2 - p1;
            Vector3D v2 = p3 - p1;
            Vector3D normal = v1 ^ v2;
            Normal = normal.Normalized;
            Distance = -(Normal * p1);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Вычисление расстояния до точки.
        /// </summary>
        /// <param name="point">Точка в пространстве.</param>
        /// <returns>Расстояние до точки.</returns>
        public readonly double GetDistanceToPoint(in Vector3D point)
        {
            return (Normal.X * point.X) + (Normal.Y * point.Y) + (Normal.Z * point.Z) + Distance;
        }

        /// <summary>
        /// Вычисление взаимного расположение вектора и плоскости - простое скалярное произведение.
        /// </summary>
        /// <param name="vector">Проверяемый вектор.</param>
        /// <returns>
        /// Если n * p = 0, то вектор p ортогонален плоскости
        /// Если n * p больше 0, то вектор p находится перед плоскостью в положительном полупространстве плоскости
        /// Если n * p меньше 0, то вектор p находится за плоскостью в отрицательном полупространстве плоскости
        /// </returns>
        public readonly double ComputeVector(in Vector3D vector)
        {
            return (Normal.X * vector.X) + (Normal.Y * vector.Y) + (Normal.Z * vector.Z);
        }

        /// <summary>
        /// Вычисление взаимного расположение точки и плоскости.
        /// </summary>
        /// <param name="point">Проверяемая точка.</param>
        /// <returns>
        /// Если n * p + d = 0, то точка p принадлежит плоскости
        /// Если n * p + d больше 0, то точка p находится перед плоскостью в положительном полупространстве плоскости
        /// Если n * p + d меньше 0, то точка p находится за плоскостью в отрицательном полупространстве плоскости
        /// </returns>
        public readonly double ComputePoint(in Vector3D point)
        {
            return (Normal.X * point.X) + (Normal.Y * point.Y) + (Normal.Z * point.Z) + Distance;
        }

        /// <summary>
        /// Нормализация плоскости.
        /// </summary>
        public void Normalize()
        {
            var inv_length = XMath.InvSqrt((Normal.X * Normal.X) + (Normal.Y * Normal.Y) + (Normal.Z * Normal.Z));
            Normal.X *= inv_length;
            Normal.Y *= inv_length;
            Normal.Z *= inv_length;
            Distance *= inv_length;
        }
        #endregion
    }

    /// <summary>
    /// Плоскость в 3D пространстве.
    /// </summary>
    /// <remarks>
    /// Реализация плоскости в 3D пространстве.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Plane3Df : IEquatable<Plane3Df>, IFormattable
    {
        #region Fields
        /// <summary>
        /// Нормаль плоскости.
        /// </summary>
        public Vector3Df Normal;

        /// <summary>
        /// Расстояние до плоскости.
        /// </summary>
        public float Distance;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует плоскость по заданным компонентам.
        /// </summary>
        /// <param name="a">X - нормаль.</param>
        /// <param name="b">Y - нормаль.</param>
        /// <param name="c">Z - нормаль.</param>
        /// <param name="d">Расстояние до плоскости.</param>
        public Plane3Df(float a, float b, float c, float d)
        {
            Normal.X = a;
            Normal.Y = b;
            Normal.Z = c;
            Distance = d;
        }

        /// <summary>
        /// Конструктор инициализирует плоскости через точку принадлежащей плоскости и вектора нормали.
        /// </summary>
        /// <param name="point">Координаты точки принадлежащей плоскости.</param>
        /// <param name="normal">Вектор нормали к плоскости.</param>
        public Plane3Df(in Vector3Df point, in Vector3Df normal)
        {
            Normal.X = normal.X;
            Normal.Y = normal.Y;
            Normal.Z = normal.Z;
            Distance = -(normal * point);
        }

        /// <summary>
        /// Конструктор инициализирует плоскости через 3 точки принадлежащие плоскости.
        /// </summary>
        /// <param name="p1">Точка принадлежащая плоскости.</param>
        /// <param name="p2">Точка принадлежащая плоскости.</param>
        /// <param name="p3">Точка принадлежащая плоскости.</param>
        public Plane3Df(in Vector3Df p1, in Vector3Df p2, in Vector3Df p3)
        {
            Vector3Df v1 = p2 - p1;
            Vector3Df v2 = p3 - p1;
            Vector3Df normal = v1 ^ v2;
            Normal = normal.Normalized;
            Distance = -(Normal * p1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plane3Df"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the A, B, C, and D components of the plane. This must be an array with four elements.</param>
        public Plane3Df(float[] values)
        {
            Normal.X = values[0];
            Normal.Y = values[1];
            Normal.Z = values[2];
            Distance = values[3];
        }
        #endregion

        #region System methods
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>
        /// </returns>
        public override readonly bool Equals(object? obj)
        {
            if (!(obj is Plane3Df))
            {
                return false;
            }

            var plane = (Plane3Df)obj;
            return Equals(in plane);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Plane3Df"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Plane3Df"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Plane3Df"/> is equal to this instance; otherwise, <c>false</c>
        /// </returns>
        public readonly bool Equals(Plane3Df other)
        {
            return Equals(in other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Plane3Df"/> is equal to this instance.
        /// </summary>
        /// <param name="value">The <see cref="Plane3Df"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Plane3Df"/> is equal to this instance; otherwise, <c>false</c>
        /// </returns>
        public readonly bool Equals(in Plane3Df value)
        {
            return Normal == value.Normal && Distance == value.Distance;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table
        /// </returns>
        public override readonly int GetHashCode()
        {
            unchecked
            {
                return (Normal.GetHashCode() * 397) ^ Distance.GetHashCode();
            }
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>
        /// Текстовое представление плоскости с указание значений компонентов
        /// </returns>
        public override readonly string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "A:{0} B:{1} C:{2} D:{3}",
                Normal.X, Normal.Y, Normal.Z, Distance);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения значения компонента.</param>
        /// <returns>
        /// Текстовое представление плоскости с указание значений компонентов
        /// </returns>
        public readonly string ToString(string format)
        {
            return string.Format(CultureInfo.CurrentCulture, "A:{0} B:{1} C:{2} D:{3}", Normal.X.ToString(format,
                CultureInfo.CurrentCulture), Normal.Y.ToString(format, CultureInfo.CurrentCulture),
                Normal.Z.ToString(format, CultureInfo.CurrentCulture), Distance.ToString(format, CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="formatProvider">Интерфейс провайдера формата значения компонента.</param>
        /// <returns>
        /// Текстовое представление плоскости с указание значений компонентов
        /// </returns>
        public readonly string ToString(IFormatProvider? formatProvider)
        {
            return string.Format(formatProvider, "A:{0} B:{1} C:{2} D:{3}", Normal.X, Normal.Y, Normal.Z, Distance);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения значения компонента.</param>
        /// <param name="formatProvider">Интерфейс провайдера формата значения компонента.</param>
        /// <returns>
        /// Текстовое представление плоскости с указание значений компонентов
        /// </returns>
        public readonly string ToString(string? format, IFormatProvider? formatProvider)
        {
            return string.Format(formatProvider, "A:{0} B:{1} C:{2} D:{3}", Normal.X.ToString(format, formatProvider),
                Normal.Y.ToString(format, formatProvider), Normal.Z.ToString(format, formatProvider),
                Distance.ToString(format, formatProvider));
        }
        #endregion

        #region Operators conversion
        #endregion

        #region Indexer
        /// <summary>
        /// Gets or sets the component at the specified index.
        /// </summary>
        /// <value>The value of the A, B, C, or D component, depending on the index</value>
        /// <param name="index">The index of the component to access. Use 0 for the A component, 
        /// 1 for the B component, 2 for the C component, and 3 for the D component</param>
        /// <returns>The value of the component at the specified index.</returns>
        public float this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0: return Normal.X;
                    case 1: return Normal.Y;
                    case 2: return Normal.Z;
                    case 3: return Distance;
                    default: return 0;
                }
            }

            set
            {
                switch (index)
                {
                    case 0: Normal.X = value; break;
                    case 1: Normal.Y = value; break;
                    case 2: Normal.Z = value; break;
                    case 3: Distance = value; break;
                    default: break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Вычисление расстояния до точки.
        /// </summary>
        /// <param name="point">Точка в пространстве.</param>
        /// <returns>Расстояние до точки.</returns>
        public readonly float GetDistanceToPoint(in Vector3Df point)
        {
            return (Normal.X * point.X) + (Normal.Y * point.Y) + (Normal.Z * point.Z) + Distance;
        }

        /// <summary>
        /// Вычисление взаимного расположение вектора и плоскости - простое скалярное произведение.
        /// </summary>
        /// <param name="vector">Проверяемый вектор.</param>
        /// <returns>
        /// Если n * p = 0, то вектор p ортогонален плоскости
        /// Если n * p больше 0, то вектор p находится перед плоскостью в положительном полупространстве плоскости
        /// Если n * p меньше 0, то вектор p находится за плоскостью в отрицательном полупространстве плоскости
        /// </returns>
        public readonly float ComputeVector(in Vector3Df vector)
        {
            return (Normal.X * vector.X) + (Normal.Y * vector.Y) + (Normal.Z * vector.Z);
        }

        /// <summary>
        /// Вычисление взаимного расположение точки и плоскости.
        /// </summary>
        /// <param name="point">Проверяемая точка.</param>
        /// <returns>
        /// Если n * p + d = 0, то точка p принадлежит плоскости
        /// Если n * p + d больше 0, то точка p находится перед плоскостью в положительном полупространстве плоскости
        /// Если n * p + d меньше 0, то точка p находится за плоскостью в отрицательном полупространстве плоскости
        /// </returns>
        public readonly float ComputePoint(in Vector3Df point)
        {
            return (Normal.X * point.X) + (Normal.Y * point.Y) + (Normal.Z * point.Z) + Distance;
        }

        /// <summary>
        /// Нормализация плоскости.
        /// </summary>
        public void Normalize()
        {
            var inv_length = XMath.InvSqrt((Normal.X * Normal.X) + (Normal.Y * Normal.Y) + (Normal.Z * Normal.Z));
            Normal.X *= inv_length;
            Normal.Y *= inv_length;
            Normal.Z *= inv_length;
            Distance *= inv_length;
        }
        #endregion
    }
    /**@}*/
}