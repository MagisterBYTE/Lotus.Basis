using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Четырехмерный вектор.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4D : IEquatable<Vector4D>, IComparable<Vector4D>
    {
        #region Const
        /// <summary>
        /// Единичный вектор.
        /// </summary>
        public static readonly Vector4D One = new(1, 1, 1);

        /// <summary>
        /// Вектор "право".
        /// </summary>
        public static readonly Vector4D Right = new(1, 0, 0);

        /// <summary>
        /// Вектор "вверх".
        /// </summary>
        public static readonly Vector4D Up = new(0, 1, 0);

        /// <summary>
        /// Вектор "вперед".
        /// </summary>
        public static readonly Vector4D Forward = new(0, 0, 1);

        /// <summary>
        /// Нулевой вектор.
        /// </summary>
        public static readonly Vector4D Zero = new(0, 0, 0);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вектора.
        /// </summary>
        public static string ToStringFormat = "X = {0:0.00}; Y = {1:0.00}; Z = {2:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Косинус угла между векторами.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <returns>Косинус угла.</returns>
        public static double Cos(in Vector4D from, in Vector4D to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y) + (from.Z * to.Z);
            var ll = from.Length * to.Length;
            return dot / ll;
        }

        /// <summary>
        /// Угол между двумя векторами (в градусах).
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечные вектор.</param>
        /// <returns>Угол в градусах.</returns>
        public static double Angle(in Vector4D from, in Vector4D to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y) + (from.Z * to.Z);
            var ll = from.Length * to.Length;
            var csv = dot / ll;
            return Math.Acos(csv) * XMath.RadianToDegree_D;
        }

        /// <summary>
        /// Расстояние между двумя векторами.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Расстояние между двумя векторами.</returns>
        public static double Distance(in Vector4D a, in Vector4D b)
        {
            var x = b.X - a.X;
            var y = b.Y - a.Y;
            var z = b.Z - a.Z;

            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static double Dot(in Vector4D a, in Vector4D b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        /// <summary>
        /// Линейная интерполяция векторов.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный вектор.</returns>
        public static Vector4D Lerp(in Vector4D from, in Vector4D to, double time)
        {
            Vector4D vector;
            vector.X = from.X + ((to.X - from.X) * time);
            vector.Y = from.Y + ((to.Y - from.Y) * time);
            vector.Z = from.Z + ((to.Z - from.Z) * time);
            vector.W = from.W + ((to.W - from.W) * time);
            return vector;
        }

        /// <summary>
        /// Десереализация четырехмерного вектора из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Четырехмерный вектор.</returns>
        public static Vector4D DeserializeFromString(string data)
        {
            var vector = new Vector4D();
            var vector_data = data.Split(';');
            vector.X = XMath.ParseDouble(vector_data[0]);
            vector.Y = XMath.ParseDouble(vector_data[1]);
            vector.Z = XMath.ParseDouble(vector_data[2]);
            vector.W = XMath.ParseDouble(vector_data[3]);
            return vector;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Координата X.
        /// </summary>
        public double X;

        /// <summary>
        /// Координата Y.
        /// </summary>
        public double Y;

        /// <summary>
        /// Координата Z.
        /// </summary>
        public double Z;

        /// <summary>
        /// Координата W.
        /// </summary>
        public double W;
        #endregion

        #region Properties
        /// <summary>
        /// Квадрат длины вектора.
        /// </summary>
        public readonly double SqrLength
        {
            get { return (X * X) + (Y * Y) + (Z * Z); }
        }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public readonly double Length
        {
            get { return Math.Sqrt((X * X) + (Y * Y) + (Z * Z)); }
        }

        /// <summary>
        /// Нормализованный вектор.
        /// </summary>
        public readonly Vector4D Normalized
        {
            get
            {
                var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
                return new Vector4D(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        /// <param name="z">Z - координата.</param>
        /// <param name="w">W - координата.</param>
        public Vector4D(double x, double y, double z, double w = 1.0)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector4D(Vector4D source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is Vector4D vector)
            {
                return Equals(vector);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства векторов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public readonly bool Equals(Vector4D other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение векторов для упорядочивания.
        /// </summary>
        /// <param name="other">Вектор.</param>
        /// <returns>Статус сравнения векторов.</returns>
        public readonly int CompareTo(Vector4D other)
        {
            if (X > other.X)
            {
                return 1;
            }
            else
            {
                if (X == other.X && Y > other.Y)
                {
                    return 1;
                }
                else
                {
                    if (X == other.X && Y == other.Y && Z > other.Z)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода вектора.
        /// </summary>
        /// <returns>Хеш-код вектора.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения компонентов вектора.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToString(string format)
        {
            return string.Format(ToStringFormat.Replace("0.00", format), X, Y, Z);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, X, Y, Z);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Сумма векторов.</returns>
        public static Vector4D operator +(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Вычитание векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector4D operator -(Vector4D left, Vector4D right)
        {
            return new Vector4D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector4D operator *(Vector4D vector, double scalar)
        {
            return new Vector4D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector4D operator /(Vector4D vector, double scalar)
        {
            scalar = 1 / scalar;
            return new Vector4D(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        /// <summary>
        /// Умножение вектора на вектор. Скалярное произведение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static double operator *(Vector4D left, Vector4D right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }

        /// <summary>
        /// Умножение вектора на вектор. Векторное произведение векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Вектор.</returns>
        public static Vector4D operator ^(Vector4D left, Vector4D right)
        {
            return new Vector4D((left.Y * right.Z) - (left.Z * right.Y),
                (left.Z * right.X) - (left.X * right.Z),
                (left.X * right.Y) - (left.Y * right.X));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector4D operator *(Vector4D vector, Matrix3Dx3 matrix)
        {
            return new Vector4D((vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31),
                (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32),
                (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector4D operator *(Vector4D vector, Matrix4Dx4 matrix)
        {
            return new Vector4D((vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31) + matrix.M41,
                (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32) + matrix.M42,
                (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33) + matrix.M43);
        }

        /// <summary>
        /// Сравнение векторов на равенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public static bool operator ==(Vector4D left, Vector4D right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        /// <summary>
        /// Сравнение векторов на неравенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус не равенства векторов.</returns>
        public static bool operator !=(Vector4D left, Vector4D right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Vector4D left, Vector4D right)
        {
            return left.X < right.X || (left.X == right.X && left.Y < right.Y) ||
                   (left.X == right.X && left.Y == right.Y && left.Z < right.Z);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Vector4D left, Vector4D right)
        {
            return left.X > right.X || (left.X == right.X && left.Y > right.Y) ||
                   (left.X == right.X && left.Y == right.Y && left.Z > right.Z);
        }

        /// <summary>
        /// Обратный вектор.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <returns>Обратный вектор.</returns>
        public static Vector4D operator -(Vector4D vector)
        {
            return new Vector4D(-vector.X, -vector.Y, -vector.Z);
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Vector3"/>.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Объект <see cref="UnityEngine.Vector3"/>.</returns>
		public static implicit operator UnityEngine.Vector3(Vector4D vector)
		{
			return new UnityEngine.Vector3((Single)vector.X, (Single)vector.Y, (Single)vector.Z);
		}

		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Vector4"/>.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Объект <see cref="UnityEngine.Vector4"/>.</returns>
		public static implicit operator UnityEngine.Vector4(Vector4D vector)
		{
			return new UnityEngine.Vector4((Single)vector.X, (Single)vector.Y, 
				(Single)vector.Z, (Single)vector.W);
		}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов вектора на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента вектора.</returns>
        public double this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        return W;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        W = value;
                        break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        public void Normalize()
        {
            var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
            X *= inv_lentgh;
            Y *= inv_lentgh;
            Z *= inv_lentgh;
        }

        /// <summary>
        /// Вычисление расстояние до вектора.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Расстояние до вектора.</returns>
        public readonly double Distance(in Vector4D vector)
        {
            var x = vector.X - X;
            var y = vector.Y - Y;
            var z = vector.Z - Z;

            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Вычисление скалярного произведения векторов.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение векторов.</returns>
        public readonly double Dot(in Vector4D vector)
        {
            return (X * vector.X) + (Y * vector.Y) + (Z * vector.Z);
        }

        /// <summary>
        /// Установка компонентов вектора из наибольших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMaximize(in Vector4D a, in Vector4D b)
        {
            X = a.X > b.X ? a.X : b.X;
            Y = a.Y > b.Y ? a.Y : b.Y;
            Z = a.Z > b.Z ? a.Z : b.Z;
        }

        /// <summary>
        /// Установка компонентов вектора из наименьших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMinimize(in Vector4D a, in Vector4D b)
        {
            X = a.X < b.X ? a.X : b.X;
            Y = a.Y < b.Y ? a.Y : b.Y;
            Z = a.Z < b.Z ? a.Z : b.Z;
        }

        /// <summary>
        /// Векторное произведение c нормализацией результата.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        public void CrossNormalize(in Vector4D left, in Vector4D right)
        {
            X = (left.Y * right.Z) - (left.Z * right.Y);
            Y = (left.Z * right.X) - (left.X * right.Z);
            Z = (left.X * right.Y) - (left.Y * right.X);
            var inv_length = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
            X *= inv_length;
            Y *= inv_length;
            Z *= inv_length;
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(Matrix4Dx4 matrix)
        {
            this = new Vector4D((X * matrix.M11) + (Y * matrix.M21) + (Z * matrix.M31) + matrix.M41,
                                (X * matrix.M12) + (Y * matrix.M22) + (Z * matrix.M32) + matrix.M42,
                                (X * matrix.M13) + (Y * matrix.M23) + (Z * matrix.M33) + matrix.M43);
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(Matrix4Dx4 matrix)
        {
            this = new Vector4D((X * matrix.M11) + (Y * matrix.M21) + (Z * matrix.M31),
                                (X * matrix.M12) + (Y * matrix.M22) + (Z * matrix.M32),
                                (X * matrix.M13) + (Y * matrix.M23) + (Z * matrix.M33));
        }

        /// <summary>
        /// Сериализация вектора в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1};{2};{3}", X, Y, Z, W);
        }
        #endregion
    }

    /// <summary>
    /// Четырехмерный вектор.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4Df : IEquatable<Vector4Df>, IComparable<Vector4Df>
    {
        #region Const
        /// <summary>
        /// Единичный вектор.
        /// </summary>
        public static readonly Vector4Df One = new(1, 1, 1);

        /// <summary>
        /// Вектор "право".
        /// </summary>
        public static readonly Vector4Df Right = new(1, 0, 0);

        /// <summary>
        /// Вектор "вверх".
        /// </summary>
        public static readonly Vector4Df Up = new(0, 1, 0);

        /// <summary>
        /// Вектор "вперед".
        /// </summary>
        public static readonly Vector4Df Forward = new(0, 0, 1);

        /// <summary>
        /// Нулевой вектор.
        /// </summary>
        public static readonly Vector4Df Zero = new(0, 0, 0);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вектора.
        /// </summary>
        public static string ToStringFormat = "X = {0:0.00}; Y = {1:0.00}; Z = {2:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Косинус угла между векторами.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <returns>Косинус угла.</returns>
        public static float Cos(in Vector4Df from, in Vector4Df to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y) + (from.Z * to.Z);
            var ll = from.Length * to.Length;
            return dot / ll;
        }

        /// <summary>
        /// Угол между двумя векторами (в градусах).
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечные вектор.</param>
        /// <returns>Угол в градусах.</returns>
        public static float Angle(in Vector4Df from, in Vector4Df to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y) + (from.Z * to.Z);
            var ll = from.Length * to.Length;
            var csv = dot / ll;
            return (float)(Math.Acos(csv) * XMath.RadianToDegree_D);
        }

        /// <summary>
        /// Расстояние между двумя векторами.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Расстояние между двумя векторами.</returns>
        public static float Distance(in Vector4Df a, in Vector4Df b)
        {
            var x = b.X - a.X;
            var y = b.Y - a.Y;
            var z = b.Z - a.Z;

            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float Dot(Vector4Df a, Vector4Df b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        /// <summary>
        /// Линейная интерполяция векторов.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный вектор.</returns>
        public static Vector4Df Lerp(in Vector4Df from, in Vector4Df to, float time)
        {
            Vector4Df vector;
            vector.X = from.X + ((to.X - from.X) * time);
            vector.Y = from.Y + ((to.Y - from.Y) * time);
            vector.Z = from.Z + ((to.Z - from.Z) * time);
            vector.W = from.W + ((to.W - from.W) * time);
            return vector;
        }

        /// <summary>
        /// Десереализация четырехмерного вектора из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Четырехмерный вектор.</returns>
        public static Vector4Df DeserializeFromString(string data)
        {
            var vector = new Vector4Df();
            var vector_data = data.Split(';');
            vector.X = XMath.ParseSingle(vector_data[0]);
            vector.Y = XMath.ParseSingle(vector_data[1]);
            vector.Z = XMath.ParseSingle(vector_data[2]);
            vector.W = XMath.ParseSingle(vector_data[3]);
            return vector;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Координата X.
        /// </summary>
        public float X;

        /// <summary>
        /// Координата Y.
        /// </summary>
        public float Y;

        /// <summary>
        /// Координата Z.
        /// </summary>
        public float Z;

        /// <summary>
        /// Координата W.
        /// </summary>
        public float W;
        #endregion

        #region Properties
        /// <summary>
        /// Квадрат длины вектора.
        /// </summary>
        public readonly float SqrLength
        {
            get { return (X * X) + (Y * Y) + (Z * Z); }
        }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public readonly float Length
        {
            get { return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z)); }
        }

        /// <summary>
        /// Нормализованный вектор.
        /// </summary>
        public readonly Vector4Df Normalized
        {
            get
            {
                var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
                return new Vector4Df(X * inv_lentgh, Y * inv_lentgh, Z * inv_lentgh);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        /// <param name="z">Z - координата.</param>
        /// <param name="w">W - координата.</param>
        public Vector4Df(float x, float y, float z, float w = 1.0f)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector4Df(Vector4Df source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is Vector4Df vector)
            {
                return Equals(vector);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства векторов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public readonly bool Equals(Vector4Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение векторов для упорядочивания.
        /// </summary>
        /// <param name="other">Вектор.</param>
        /// <returns>Статус сравнения векторов.</returns>
        public readonly int CompareTo(Vector4Df other)
        {
            if (X > other.X)
            {
                return 1;
            }
            else
            {
                if (X == other.X && Y > other.Y)
                {
                    return 1;
                }
                else
                {
                    if (X == other.X && Y == other.Y && Z > other.Z)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода вектора.
        /// </summary>
        /// <returns>Хеш-код вектора.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения компонентов вектора.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToString(string format)
        {
            return string.Format(ToStringFormat.Replace("0.00", format), X, Y, Z);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, X, Y, Z);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Сумма векторов.</returns>
        public static Vector4Df operator +(Vector4Df left, Vector4Df right)
        {
            return new Vector4Df(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Вычитание векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector4Df operator -(Vector4Df left, Vector4Df right)
        {
            return new Vector4Df(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector4Df operator *(Vector4Df vector, float scalar)
        {
            return new Vector4Df(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector4Df operator /(Vector4Df vector, float scalar)
        {
            scalar = 1 / scalar;
            return new Vector4Df(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        /// <summary>
        /// Умножение вектора на вектор. Скалярное произведение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float operator *(Vector4Df left, Vector4Df right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }

        /// <summary>
        /// Умножение вектора на вектор. Векторное произведение векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Вектор.</returns>
        public static Vector4Df operator ^(Vector4Df left, Vector4Df right)
        {
            return new Vector4Df((left.Y * right.Z) - (left.Z * right.Y),
                (left.Z * right.X) - (left.X * right.Z),
                (left.X * right.Y) - (left.Y * right.X));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector4Df operator *(Vector4Df vector, Matrix3Dx3f matrix)
        {
            return new Vector4Df((vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31),
                                 (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32),
                                 (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector4Df operator *(Vector4Df vector, Matrix4Dx4f matrix)
        {
            return new Vector4Df((vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31) + matrix.M41,
                                 (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32) + matrix.M42,
                                 (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33) + matrix.M43);
        }

        /// <summary>
        /// Сравнение векторов на равенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public static bool operator ==(Vector4Df left, Vector4Df right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        /// <summary>
        /// Сравнение векторов на неравенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус не равенства векторов.</returns>
        public static bool operator !=(Vector4Df left, Vector4Df right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Vector4Df left, Vector4Df right)
        {
            return left.X < right.X || (left.X == right.X && left.Y < right.Y) ||
                   (left.X == right.X && left.Y == right.Y && left.Z < right.Z);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Vector4Df left, Vector4Df right)
        {
            return left.X > right.X || (left.X == right.X && left.Y > right.Y) ||
                   (left.X == right.X && left.Y == right.Y && left.Z > right.Z);
        }

        /// <summary>
        /// Обратный вектор.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <returns>Обратный вектор.</returns>
        public static Vector4Df operator -(Vector4Df vector)
        {
            return new Vector4Df(-vector.X, -vector.Y, -vector.Z);
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Vector3"/>.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Объект <see cref="UnityEngine.Vector3"/>.</returns>
		public static implicit operator UnityEngine.Vector3(Vector4Df vector)
		{
			return new UnityEngine.Vector3((Single)vector.X, (Single)vector.Y, (Single)vector.Z);
		}

		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Vector4"/>.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Объект <see cref="UnityEngine.Vector4"/>.</returns>
		public static implicit operator UnityEngine.Vector4(Vector4Df vector)
		{
			return new UnityEngine.Vector4((Single)vector.X, (Single)vector.Y, 
				(Single)vector.Z, (Single)vector.W);
		}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов вектора на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента вектора.</returns>
        public float this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        return W;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        W = value;
                        break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Нормализация вектора.
        /// </summary>
        public void Normalize()
        {
            var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
            X *= inv_lentgh;
            Y *= inv_lentgh;
            Z *= inv_lentgh;
        }

        /// <summary>
        /// Вычисление расстояние до вектора.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Расстояние до вектора.</returns>
        public readonly float Distance(in Vector4Df vector)
        {
            var x = vector.X - X;
            var y = vector.Y - Y;
            var z = vector.Z - Z;

            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Вычисление скалярного произведения векторов.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение векторов.</returns>
        public readonly float Dot(in Vector4Df vector)
        {
            return (X * vector.X) + (Y * vector.Y) + (Z * vector.Z);
        }

        /// <summary>
        /// Установка компонентов вектора из наибольших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMaximize(in Vector4Df a, in Vector4Df b)
        {
            X = a.X > b.X ? a.X : b.X;
            Y = a.Y > b.Y ? a.Y : b.Y;
            Z = a.Z > b.Z ? a.Z : b.Z;
        }

        /// <summary>
        /// Установка компонентов вектора из наименьших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMinimize(in Vector4Df a, in Vector4Df b)
        {
            X = a.X < b.X ? a.X : b.X;
            Y = a.Y < b.Y ? a.Y : b.Y;
            Z = a.Z < b.Z ? a.Z : b.Z;
        }

        /// <summary>
        /// Векторное произведение c нормализацией результата.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        public void CrossNormalize(in Vector4Df left, in Vector4Df right)
        {
            X = (left.Y * right.Z) - (left.Z * right.Y);
            Y = (left.Z * right.X) - (left.X * right.Z);
            Z = (left.X * right.Y) - (left.Y * right.X);
            var inv_length = XMath.InvSqrt((X * X) + (Y * Y) + (Z * Z));
            X *= inv_length;
            Y *= inv_length;
            Z *= inv_length;
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix4Dx4f matrix)
        {
            this = new Vector4Df((X * matrix.M11) + (Y * matrix.M21) + (Z * matrix.M31) + matrix.M41,
                                 (X * matrix.M12) + (Y * matrix.M22) + (Z * matrix.M32) + matrix.M42,
                                 (X * matrix.M13) + (Y * matrix.M23) + (Z * matrix.M33) + matrix.M43);
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(Matrix4Dx4f matrix)
        {
            this = new Vector4Df((X * matrix.M11) + (Y * matrix.M21) + (Z * matrix.M31),
                                 (X * matrix.M12) + (Y * matrix.M22) + (Z * matrix.M32),
                                 (X * matrix.M13) + (Y * matrix.M23) + (Z * matrix.M33));
        }

        /// <summary>
        /// Сериализация вектора в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1};{2};{3}", X, Y, Z, W);
        }
        #endregion
    }
    /**@}*/
}