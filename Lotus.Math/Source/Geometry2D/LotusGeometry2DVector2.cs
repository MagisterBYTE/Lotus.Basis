using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /// <summary>
    /// Двухмерный вектор.
    /// </summary>
    /// <remarks>
    /// Реализация двухмерного вектора, представляющего собой базовую математическую сущность в двухмерном пространстве.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2D : IEquatable<Vector2D>, IComparable<Vector2D>
    {
        #region Const
        /// <summary>
        /// Единичный вектор.
        /// </summary>
        public static readonly Vector2D One = new(1, 1);

        /// <summary>
        /// Вектор "право".
        /// </summary>
        public static readonly Vector2D Right = new(1, 0);

        /// <summary>
        /// Вектор "влево".
        /// </summary>
        public static readonly Vector2D Left = new(-1, 0);

        /// <summary>
        /// Вектор "вверх".
        /// </summary>
        public static readonly Vector2D Up = new(0, 1);

        /// <summary>
        /// Вектор "вниз".
        /// </summary>
        public static readonly Vector2D Down = new(0, -1);

        /// <summary>
        /// Нулевой вектор.
        /// </summary>
        public static readonly Vector2D Zero = new(0, 0);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вектора.
        /// </summary>
        public static string ToStringFormat = "X = {0:0.00}; Y = {1:0.00}";

        /// <summary>
        /// Текстовый формат отображения только значений параметров вектора.
        /// </summary>
        public static string ToStringFormatValue = "{0:0.00}; {1:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Косинус угла между векторами.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <returns>Косинус угла.</returns>
        public static double Cos(in Vector2D from, in Vector2D to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y);
            var ll = from.Length * to.Length;
            return dot / ll;
        }

        /// <summary>
        /// Угол между двумя векторами (в градусах).
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечные вектор.</param>
        /// <returns>Угол в градусах.</returns>
        public static double Angle(in Vector2D from, in Vector2D to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y);
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
        public static double Distance(in Vector2D a, in Vector2D b)
        {
            var x = b.X - a.X;
            var y = b.Y - a.Y;

            return Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static double Dot(in Vector2D a, in Vector2D b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        /// <summary>
        /// Линейная интерполяция векторов.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный вектор.</returns>
        public static Vector2D Lerp(in Vector2D from, in Vector2D to, double time)
        {
            Vector2D vector;
            vector.X = from.X + ((to.X - from.X) * time);
            vector.Y = from.Y + ((to.Y - from.Y) * time);
            return vector;
        }

        /// <summary>
        /// Негативное значение для вектора.
        /// </summary>
        /// <param name="value">Исходный вектор.</param>
        /// <returns>Негативный вектор.</returns>
        public static Vector2D Negate(in Vector2D value)
        {
            return new Vector2D(-value.X, -value.Y);
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Max(in Vector2D a, in Vector2D b, out Vector2D result)
        {
            result.X = a.X > b.X ? a.X : b.X;
            result.Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2D Max(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X > b.X ? a.X : b.X, a.Y > b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Min(in Vector2D a, in Vector2D b, out Vector2D result)
        {
            result.X = a.X < b.X ? a.X : b.X;
            result.Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2D Min(in Vector2D a, in Vector2D b)
        {
            return new Vector2D(a.X < b.X ? a.X : b.X, a.Y < b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Отражение вектора относительно нормали.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <param name="normal">Вектор нормали.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Reflect(in Vector2D vector, in Vector2D normal, out Vector2D result)
        {
            var dot = (vector.X * normal.X) + (vector.Y * normal.Y);

            result.X = vector.X - (2.0 * dot * normal.X);
            result.Y = vector.Y - (2.0 * dot * normal.Y);
        }

        /// <summary>
        /// Отражение вектора относительно нормали.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <param name="normal">Вектор нормали.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2D Reflect(in Vector2D vector, in Vector2D normal)
        {
            Reflect(in vector, in normal, out var result);
            return result;
        }

        /// <summary>
        /// Аппроксимация равенства значений векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="epsilon">Погрешность.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(in Vector2D a, in Vector2D b, float epsilon = 0.001f)
        {
            if (Math.Abs(a.X - b.X) < epsilon && Math.Abs(a.Y - b.Y) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Десереализация двухмерного вектора из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2D DeserializeFromString(string data)
        {
            var vector = new Vector2D();
            var vector_data = data.Split(';');
            vector.X = XMath.ParseDouble(vector_data[0]);
            vector.Y = XMath.ParseDouble(vector_data[1]);
            return vector;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Компонента X.
        /// </summary>
        public double X;

        /// <summary>
        /// Компонента Y.
        /// </summary>
        public double Y;
        #endregion

        #region Properties
        /// <summary>
        /// Квадрат длины вектора.
        /// </summary>
        public readonly double SqrLength
        {
            get { return (X * X) + (Y * Y); }
        }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public readonly double Length
        {
            get { return Math.Sqrt((X * X) + (Y * Y)); }
        }

        /// <summary>
        /// Нормализованный вектор.
        /// </summary>
        public readonly Vector2D Normalized
        {
            get
            {
                var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y));
                return new Vector2D(X * inv_lentgh, Y * inv_lentgh);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector2D(Vector2D source)
        {
            X = source.X;
            Y = source.Y;
        }
#if USE_WINDOWS
		/// <summary>
		/// Конструктор инициализирует вектор указанной точкой WPF.
		/// </summary>
		/// <param name="source">Точка WPF.</param>
		public Vector2D(System.Windows.Point source)
		{
			X = source.X;
			Y = source.Y;
		}

		/// <summary>
		/// Конструктор инициализирует вектор разностью точек WPF.
		/// </summary>
		/// <param name="start">Начальная точка WPF.</param>
		/// <param name="end">Конечная точка WPF.</param>
		public Vector2D(System.Windows.Point start, System.Windows.Point end)
		{
			X = end.X - start.X;
			Y = end.Y - start.Y;
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Конструктор инициализирует вектор указанным вектором SharpDX.
		/// </summary>
		/// <param name="source">Вектор SharpDX.</param>
		public Vector2D(global::SharpDX.Vector2 source)
		{
			X = source.X;
			Y = source.Y;
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
            if (obj is Vector2D vector)
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
        public readonly bool Equals(Vector2D other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение векторов для упорядочивания.
        /// </summary>
        /// <param name="other">Вектор.</param>
        /// <returns>Статус сравнения векторов.</returns>
        public readonly int CompareTo(Vector2D other)
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
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода вектора.
        /// </summary>
        /// <returns>Хеш-код вектора.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, X, Y);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToString(string format)
        {
            return "X = " + X.ToString(format) + "; Y = " + Y.ToString(format);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToStringValue()
        {
            return string.Format(ToStringFormatValue, X, Y);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения компонентов вектора.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToStringValue(string format)
        {
            return string.Format(ToStringFormatValue.Replace("0.00", format), X, Y);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Сумма векторов.</returns>
        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Вычитание векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2D operator *(Vector2D vector, double scalar)
        {
            return new Vector2D(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2D operator /(Vector2D vector, double scalar)
        {
            scalar = 1 / scalar;
            return new Vector2D(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Умножение вектора на вектор. Скалярное произведение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static double operator *(Vector2D left, Vector2D right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2D operator *(Vector2D vector, Matrix3Dx2f matrix)
        {
            return new Vector2D((vector.X * matrix.M11) + (vector.Y * matrix.M21),
                (vector.X * matrix.M12) + (vector.Y * matrix.M22));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2D operator *(Vector2D vector, Matrix4Dx4 matrix)
        {
            return new Vector2D((vector.X * matrix.M11) + (vector.Y * matrix.M21) + matrix.M41,
                (vector.X * matrix.M12) + (vector.Y * matrix.M22) + matrix.M42);
        }

        /// <summary>
        /// Сравнение векторов на равенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public static bool operator ==(Vector2D left, Vector2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Сравнение векторов на неравенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус неравенства векторов.</returns>
        public static bool operator !=(Vector2D left, Vector2D right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Vector2D left, Vector2D right)
        {
            return left.X < right.X || (left.X == right.X && left.Y < right.Y);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Vector2D left, Vector2D right)
        {
            return left.X > right.X || (left.X == right.X && left.Y > right.Y);
        }

        /// <summary>
        /// Обратный вектор.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <returns>Обратный вектор.</returns>
        public static Vector2D operator -(Vector2D vector)
        {
            return new Vector2D(-vector.X, -vector.Y);
        }
        #endregion

        #region Operators conversion
        /// <summary>
        /// Неявное преобразование в объект типа Vector2Df.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Vector2Df.</returns>
        public static implicit operator Vector2Df(Vector2D vector)
        {
            return new Vector2Df((float)vector.X, (float)vector.Y);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа UnityEngine.Vector2.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>UnityEngine.Vector2.</returns>
		public static implicit operator UnityEngine.Vector2(Vector2D vector)
		{
			return new UnityEngine.Vector2((Single)vector.X, (Single)vector.Y);
		}
#endif

#if USE_WINDOWS
		/// <summary>
		/// Неявное преобразование в объект типа точки WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка WPF.</returns>
		public unsafe static implicit operator System.Windows.Point(Vector2D vector)
		{
			return (*(System.Windows.Point*)&vector);
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор WPF.</returns>
		public unsafe static implicit operator System.Windows.Vector(Vector2D vector)
		{
			return (*(System.Windows.Vector*)&vector);
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		public static implicit operator global::SharpDX.Vector2(Vector2D vector)
		{
			return (new global::SharpDX.Vector2((Single)vector.X, (Single)vector.Y));
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		public static implicit operator global::SharpDX.Mathematics.Interop.RawVector2(Vector2D vector)
		{
			return (new global::SharpDX.Mathematics.Interop.RawVector2((Single)vector.X, (Single)vector.Y));
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
                    default:
                        return Y;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    default:
                        Y = value;
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
            var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y));
            X *= inv_lentgh;
            Y *= inv_lentgh;
        }

        /// <summary>
        /// Вычисление расстояние до вектора.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Расстояние до вектора.</returns>
        public readonly double Distance(in Vector2D vector)
        {
            var x = vector.X - X;
            var y = vector.Y - Y;

            return Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Вычисление скалярного произведения векторов.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение векторов.</returns>
        public readonly double Dot(in Vector2D vector)
        {
            return (X * vector.X) + (Y * vector.Y);
        }

        /// <summary>
        /// Установка компонентов вектора из наибольших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMaximize(in Vector2D a, in Vector2D b)
        {
            X = a.X > b.X ? a.X : b.X;
            Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Установка компонентов вектора из наименьших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMinimize(in Vector2D a, in Vector2D b)
        {
            X = a.X < b.X ? a.X : b.X;
            Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix4Dx4 matrix)
        {
            this = new Vector2D((X * matrix.M11) + (Y * matrix.M21) + matrix.M41,
                                (X * matrix.M12) + (Y * matrix.M22) + matrix.M42);
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(in Matrix4Dx4 matrix)
        {
            this = new Vector2D((X * matrix.M11) + (Y * matrix.M21),
                                (X * matrix.M12) + (Y * matrix.M22));
        }

        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного против часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2D PerpToCCW()
        {
            return new Vector2D(-Y, X);
        }

        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного по часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2D PerpToCW()
        {
            return new Vector2D(Y, -X);
        }

        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного против часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2D UnitPerpToCCW()
        {
            return new Vector2D(-Y, X) / Length;
        }

        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного по часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2D UnitPerpToCW()
        {
            return new Vector2D(Y, -X) / Length;
        }

        /// <summary>
        /// Возвращение скалярного произведения с перпендикулярным вектором.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение с перпендикулярным вектором.</returns>
        public readonly double DotPerp(in Vector2D vector)
        {
            return (X * vector.Y) - (Y * vector.X);
        }

        /// <summary>
        /// Сериализация вектора в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1}", X, Y);
        }
        #endregion

        #region Convert methods
        /// <summary>
        /// Преобразование в вектор нулевой X компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2D ToVector2X()
        {
            return new Vector2D(X, 0);
        }

        /// <summary>
        /// Преобразование в вектор нулевой Y компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2D ToVector2Y()
        {
            return new Vector2D(0, Y);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости XY.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3D ToVector3XY()
        {
            return new Vector3D(X, Y, 0);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости YZ.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3D ToVector3XZ()
        {
            return new Vector3D(X, 0, Y);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости YZ.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3D ToVector3YZ()
        {
            return new Vector3D(0, X, Y);
        }
        #endregion
    }

    /// <summary>
    /// Двухмерный вектор.
    /// </summary>
    /// <remarks>
    /// Реализация двухмерного вектора, представляющего собой базовую математическую сущность в двухмерном пространстве.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2Df : IEquatable<Vector2Df>, IComparable<Vector2Df>
    {
        #region Const
        /// <summary>
        /// Единичный вектор.
        /// </summary>
        public static readonly Vector2Df One = new(1, 1);

        /// <summary>
        /// Вектор "право".
        /// </summary>
        public static readonly Vector2Df Right = new(1, 0);

        /// <summary>
        /// Вектор "влево".
        /// </summary>
        public static readonly Vector2Df Left = new(-1, 0);

        /// <summary>
        /// Вектор "вверх".
        /// </summary>
        public static readonly Vector2Df Up = new(0, 1);

        /// <summary>
        /// Вектор "вниз".
        /// </summary>
        public static readonly Vector2Df Down = new(0, -1);

        /// <summary>
        /// Нулевой вектор.
        /// </summary>
        public static readonly Vector2Df Zero = new(0, 0);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вектора.
        /// </summary>
        public static string ToStringFormat = "X = {0:0.00}; Y = {1:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Косинус угла между векторами.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <returns>Косинус угла.</returns>
        public static float Cos(in Vector2Df from, in Vector2Df to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y);
            var ll = from.Length * to.Length;
            return dot / ll;
        }

        /// <summary>
        /// Угол между двумя векторами (в градусах).
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечные вектор.</param>
        /// <returns>Угол в градусах.</returns>
        public static float Angle(in Vector2Df from, in Vector2Df to)
        {
            var dot = (from.X * to.X) + (from.Y * to.Y);
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
        public static float Distance(in Vector2Df a, in Vector2Df b)
        {
            var x = b.X - a.X;
            var y = b.Y - a.Y;

            return (float)Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float Dot(in Vector2Df a, in Vector2Df b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        /// <summary>
        /// Возвращение скалярного произведения с перпендикулярным вектором.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скалярное произведение с перпендикулярным вектором.</returns>
        public static float DotPerp(in Vector2Df a, in Vector2Df b)
        {
            return (a.X * b.Y) - (a.Y * b.X);
        }

        /// <summary>
        /// Линейная интерполяция векторов.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный вектор.</returns>
        public static Vector2Df Lerp(in Vector2Df from, in Vector2Df to, float time)
        {
            Vector2Df vector;
            vector.X = from.X + ((to.X - from.X) * time);
            vector.Y = from.Y + ((to.Y - from.Y) * time);
            return vector;
        }

        /// <summary>
        /// Негативное значение для вектора.
        /// </summary>
        /// <param name="value">Исходный вектор.</param>
        /// <returns>Негативный вектор.</returns>
        public static Vector2Df Negate(in Vector2Df value)
        {
            return new Vector2Df(-value.X, -value.Y);
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Max(in Vector2Df a, in Vector2Df b, out Vector2Df result)
        {
            result.X = a.X > b.X ? a.X : b.X;
            result.Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2Df Max(in Vector2Df a, in Vector2Df b)
        {
            return new Vector2Df(a.X > b.X ? a.X : b.X, a.Y > b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Min(in Vector2Df a, in Vector2Df b, out Vector2Df result)
        {
            result.X = a.X < b.X ? a.X : b.X;
            result.Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2Df Min(in Vector2Df a, in Vector2Df b)
        {
            return new Vector2Df(a.X < b.X ? a.X : b.X, a.Y < b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Отражение вектора относительно нормали.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <param name="normal">Вектор нормали.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Reflect(in Vector2Df vector, in Vector2Df normal, out Vector2Df result)
        {
            var dot = (vector.X * normal.X) + (vector.Y * normal.Y);

            result.X = vector.X - (2.0f * dot * normal.X);
            result.Y = vector.Y - (2.0f * dot * normal.Y);
        }

        /// <summary>
        /// Отражение вектора относительно нормали.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <param name="normal">Вектор нормали.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2Df Reflect(in Vector2Df vector, in Vector2Df normal)
        {
            Reflect(in vector, in normal, out var result);
            return result;
        }

        /// <summary>
        /// Аппроксимация равенства значений векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="epsilon">Погрешность.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(in Vector2Df a, in Vector2Df b, float epsilon = 0.001f)
        {
            if (Math.Abs(a.X - b.X) < epsilon && Math.Abs(a.Y - b.Y) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Десереализация двухмерного вектора из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Df DeserializeFromString(string data)
        {
            var vector = new Vector2Df();
            var vector_data = data.Split(';');
            vector.X = XMath.ParseSingle(vector_data[0]);
            vector.Y = XMath.ParseSingle(vector_data[1]);
            return vector;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Компонента X.
        /// </summary>
        public float X;

        /// <summary>
        /// Компонента Y.
        /// </summary>
        public float Y;
        #endregion

        #region Properties
        /// <summary>
        /// Квадрат длины вектора.
        /// </summary>
        public readonly float SqrLength
        {
            get { return (X * X) + (Y * Y); }
        }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public readonly float Length
        {
            get { return (float)Math.Sqrt((X * X) + (Y * Y)); }
        }

        /// <summary>
        /// Нормализованный вектор.
        /// </summary>
        public readonly Vector2Df Normalized
        {
            get
            {
                var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y));
                return new Vector2Df(X * inv_lentgh, Y * inv_lentgh);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        public Vector2Df(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector2Df(Vector2Df source)
        {
            X = source.X;
            Y = source.Y;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector2Df(Vector2D source)
        {
            X = (float)source.X;
            Y = (float)source.Y;
        }

#if USE_WINDOWS
		/// <summary>
		/// Конструктор инициализирует вектор указанной точкой WPF.
		/// </summary>
		/// <param name="source">Точка WPF.</param>
		public Vector2Df(System.Windows.Vector source)
		{
			X = (Single)source.X;
			Y = (Single)source.Y;
		}

		/// <summary>
		/// Конструктор инициализирует вектор указанной точкой WPF.
		/// </summary>
		/// <param name="source">Точка WPF.</param>
		public Vector2Df(System.Windows.Point source)
		{
			X = (Single)source.X;
			Y = (Single)source.Y;
		}

		/// <summary>
		/// Конструктор инициализирует вектор разностью точек WPF.
		/// </summary>
		/// <param name="start">Начальная точка WPF.</param>
		/// <param name="end">Конечная точка WPF.</param>
		public Vector2Df(System.Windows.Point start, System.Windows.Point end)
		{
			X = (Single)(end.X - start.X);
			Y = (Single)(end.Y - start.Y);
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Конструктор инициализирует вектор указанным вектором SharpDX.
		/// </summary>
		/// <param name="source">Вектор SharpDX.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Df(global::SharpDX.Vector2 source)
		{
			X = source.X;
			Y = source.Y;
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
            if (obj is Vector2Df vector)
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
        public readonly bool Equals(Vector2Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение векторов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый вектор.</param>
        /// <returns>Статус сравнения векторов.</returns>
        public readonly int CompareTo(Vector2Df other)
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
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода вектора.
        /// </summary>
        /// <returns>Хеш-код вектора.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, X, Y);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToString(string format)
        {
            return "X = " + X.ToString(format) + "; Y = " + Y.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Сумма векторов.</returns>
        public static Vector2Df operator +(Vector2Df left, Vector2Df right)
        {
            return new Vector2Df(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Вычитание векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector2Df operator -(Vector2Df left, Vector2Df right)
        {
            return new Vector2Df(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2Df operator *(Vector2Df vector, float scalar)
        {
            return new Vector2Df(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="scalar">Скаляр.</param>
        /// <param name="vector">Вектор.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2Df operator *(float scalar, Vector2Df vector)
        {
            return new Vector2Df(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2Df operator /(Vector2Df vector, float scalar)
        {
            scalar = 1 / scalar;
            return new Vector2Df(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Умножение вектора на вектор. Скалярное произведение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float operator *(Vector2Df left, Vector2Df right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2Df operator *(Vector2Df vector, Matrix3Dx2f matrix)
        {
            return new Vector2Df(((vector.X * matrix.M11) + (vector.Y * matrix.M21)),
                ((vector.X * matrix.M12) + (vector.Y * matrix.M22)));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2Df operator *(Vector2Df vector, Matrix4Dx4 matrix)
        {
            return new Vector2Df((float)((vector.X * matrix.M11) + (vector.Y * matrix.M21) + matrix.M41),
                (float)((vector.X * matrix.M12) + (vector.Y * matrix.M22) + matrix.M42));
        }

        /// <summary>
        /// Сравнение векторов на равенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public static bool operator ==(Vector2Df left, Vector2Df right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Сравнение векторов на неравенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус неравенства векторов.</returns>
        public static bool operator !=(Vector2Df left, Vector2Df right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Vector2Df left, Vector2Df right)
        {
            return left.X < right.X || (left.X == right.X && left.Y < right.Y);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Vector2Df left, Vector2Df right)
        {
            return left.X > right.X || (left.X == right.X && left.Y > right.Y);
        }

        /// <summary>
        /// Обратный вектор.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <returns>Обратный вектор.</returns>
        public static Vector2Df operator -(Vector2Df vector)
        {
            return new Vector2Df(-vector.X, -vector.Y);
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа <see cref="UnityEngine.Vector2"/>.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Объект <see cref="UnityEngine.Vector2"/>.</returns>
		public static implicit operator UnityEngine.Vector2(Vector2Df vector)
		{
			return new UnityEngine.Vector2(vector.X, vector.Y);
		}
#endif

#if USE_WINDOWS
		/// <summary>
		/// Неявное преобразование в объект типа точки WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка WPF.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Windows.Point(Vector2Df vector)
		{
			return (new System.Windows.Point(vector.X, vector.Y));
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор WPF.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Windows.Vector(Vector2Df vector)
		{
			return (new System.Windows.Vector(vector.X, vector.Y));
		}
#endif
#if USE_GDI
		/// <summary>
		/// Неявное преобразование в объект типа точки System.Drawing.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка System.Drawing.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Drawing.Point(Vector2Df vector)
		{
			return (new System.Drawing.Point((Int32)vector.X, (Int32)vector.Y));
		}

		/// <summary>
		/// Неявное преобразование в объект типа точки System.Drawing.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка System.Drawing.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Drawing.PointF(Vector2Df vector)
		{
			return (new System.Drawing.PointF(vector.X, vector.Y));
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static implicit operator SharpDX.Vector2(Vector2Df vector)
		{
			return (*(SharpDX.Vector2*)&vector);
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static implicit operator SharpDX.Mathematics.Interop.RawVector2(Vector2Df vector)
		{
			return (*(SharpDX.Mathematics.Interop.RawVector2*)&vector);
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static implicit operator Vector2Df(SharpDX.Vector2 vector)
		{
			return (*(Vector2Df*)&vector);
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора SharpDX.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор SharpDX.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static implicit operator Vector2Df(SharpDX.Mathematics.Interop.RawVector2 vector)
		{
			return (*(Vector2Df*)&vector);
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
                    default:
                        return Y;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    default:
                        Y = value;
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
            var inv_lentgh = XMath.InvSqrt((X * X) + (Y * Y));
            X *= inv_lentgh;
            Y *= inv_lentgh;
        }

        /// <summary>
        /// Смещение вектора.
        /// </summary>
        /// <param name="x">Смещение по X.</param>
        /// <param name="y">Смещение по Y.</param>
        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Вычисление расстояние до вектора.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Расстояние до вектора.</returns>
        public readonly float Distance(in Vector2Df vector)
        {
            var x = vector.X - X;
            var y = vector.Y - Y;

            return (float)Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Вычисление скалярного произведения векторов.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение векторов.</returns>
        public readonly float Dot(in Vector2Df vector)
        {
            return (X * vector.X) + (Y * vector.Y);
        }

        /// <summary>
        /// Установка компонентов вектора из наибольших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMaximize(in Vector2Df a, in Vector2Df b)
        {
            X = a.X > b.X ? a.X : b.X;
            Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Установка компонентов вектора из наименьших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMinimize(in Vector2Df a, in Vector2Df b)
        {
            X = a.X < b.X ? a.X : b.X;
            Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix3Dx2f matrix)
        {
            this = new Vector2Df((X * matrix.M11) + (Y * matrix.M21) + matrix.M31,
                                 (X * matrix.M12) + (Y * matrix.M22) + matrix.M31);
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix4Dx4 matrix)
        {
            this = new Vector2Df((float)((X * matrix.M11) + (Y * matrix.M21) + matrix.M41),
                                 (float)((X * matrix.M12) + (Y * matrix.M22) + matrix.M42));
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(in Matrix3Dx2f matrix)
        {
            this = new Vector2Df((X * matrix.M11) + (Y * matrix.M21),
                                 (X * matrix.M12) + (Y * matrix.M22));
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(in Matrix4Dx4 matrix)
        {
            this = new Vector2Df((float)((X * matrix.M11) + (Y * matrix.M21)),
                                 (float)((X * matrix.M12) + (Y * matrix.M22)));
        }

        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного против часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2Df PerpToCCW()
        {
            return new Vector2Df(-Y, X);
        }

        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного по часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2Df PerpToCW()
        {
            return new Vector2Df(Y, -X);
        }

        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного против часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2Df UnitPerpToCCW()
        {
            return new Vector2Df(-Y, X) / Length;
        }

        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного по часовой стрелки.
        /// </summary>
        /// <returns>Перпендикулярный вектор.</returns>
        public readonly Vector2Df UnitPerpToCW()
        {
            return new Vector2Df(Y, -X) / Length;
        }

        /// <summary>
        /// Возвращение скалярного произведения с перпендикулярным вектором.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение с перпендикулярным вектором.</returns>
        public readonly float DotPerp(in Vector2Df vector)
        {
            // x*V.y - y*V.x.
            return (X * vector.Y) - (Y * vector.X);
        }

        /// <summary>
        /// Аппроксимация равенства значений векторов.
        /// </summary>
        /// <param name="other">Вектор.</param>
        /// <param name="epsilon">Погрешность.</param>
        /// <returns>Статус равенства значений векторов.</returns>
        public readonly bool Approximately(in Vector2Df other, float epsilon = 0.01f)
        {
            if (Math.Abs(X - other.X) < epsilon &&
                Math.Abs(Y - other.Y) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Сериализация вектора в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1}", X, Y);
        }
        #endregion

        #region Conver methods
        /// <summary>
        /// Преобразование в вектор нулевой X компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2Df ToVector2X()
        {
            return new Vector2Df(X, 0);
        }

        /// <summary>
        /// Преобразование в вектор нулевой Y компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2Df ToVector2Y()
        {
            return new Vector2Df(0, Y);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости XY.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3Df ToVector3XY()
        {
            return new Vector3Df(X, Y, 0);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости YZ.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3Df ToVector3XZ()
        {
            return new Vector3Df(X, 0, Y);
        }

        /// <summary>
        /// Преобразование в трехмерный вектор плоскости YZ.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector3Df ToVector3YZ()
        {
            return new Vector3Df(0, X, Y);
        }
        #endregion
    }

    /// <summary>
    /// Двухмерный вектор.
    /// </summary>
    /// <remarks>
    /// Реализация двухмерного вектора, представляющего собой базовую математическую сущность в двухмерном пространстве.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2Di : IEquatable<Vector2Di>, IComparable<Vector2Di>
    {
        #region Const
        /// <summary>
        /// Единичный вектор.
        /// </summary>
        public static readonly Vector2Di One = new(1, 1);

        /// <summary>
        /// Вектор "право".
        /// </summary>
        public static readonly Vector2Di Right = new(1, 0);

        /// <summary>
        /// Вектор "влево".
        /// </summary>
        public static readonly Vector2Di Left = new(-1, 0);

        /// <summary>
        /// Вектор "вверх".
        /// </summary>
        public static readonly Vector2Di Up = new(0, 1);

        /// <summary>
        /// Вектор "вниз".
        /// </summary>
        public static readonly Vector2Di Down = new(0, -1);

        /// <summary>
        /// Нулевой вектор.
        /// </summary>
        public static readonly Vector2Di Zero = new(0, 0);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров вектора.
        /// </summary>
        public static string ToStringFormat = "X = {0}; Y = {1}";
        #endregion

        #region Static methods
        /// <summary>
        /// Косинус угла между векторами.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <returns>Косинус угла.</returns>
        public static float Cos(in Vector2Di from, in Vector2Di to)
        {
            float dot = (from.X * to.X) + (from.Y * to.Y);
            var ll = from.Length * to.Length;
            return dot / ll;
        }

        /// <summary>
        /// Угол между двумя векторами (в градусах).
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечные вектор.</param>
        /// <returns>Угол в градусах.</returns>
        public static float Angle(in Vector2Di from, in Vector2Di to)
        {
            float dot = (from.X * to.X) + (from.Y * to.Y);
            var ll = from.Length * to.Length;
            var csv = dot / ll;
            return (int)(Math.Acos(csv) * XMath.RadianToDegree_D);
        }

        /// <summary>
        /// Расстояние между двумя векторами.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Расстояние между двумя векторами.</returns>
        public static float Distance(in Vector2Di a, in Vector2Di b)
        {
            float x = b.X - a.X;
            float y = b.Y - a.Y;

            return (float)Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float Dot(in Vector2Di a, in Vector2Di b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        /// <summary>
        /// Линейная интерполяция векторов.
        /// </summary>
        /// <param name="from">Начальный вектор.</param>
        /// <param name="to">Конечный вектор.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный вектор.</returns>
        public static Vector2Di Lerp(in Vector2Di from, in Vector2Di to, float time)
        {
            Vector2Di vector;
            vector.X = (int)(from.X + ((to.X - from.X) * time));
            vector.Y = (int)(from.Y + ((to.Y - from.Y) * time));
            return vector;
        }

        /// <summary>
        /// Негативное значение для вектора.
        /// </summary>
        /// <param name="value">Исходный вектор.</param>
        /// <returns>Негативный вектор.</returns>
        public static Vector2Di Negate(in Vector2Di value)
        {
            return new Vector2Di(-value.X, -value.Y);
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Max(in Vector2Di a, in Vector2Di b, out Vector2Di result)
        {
            result.X = a.X > b.X ? a.X : b.X;
            result.Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Максимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2Di Max(in Vector2Di a, in Vector2Di b)
        {
            return new Vector2Di(a.X > b.X ? a.X : b.X, a.Y > b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <param name="result">Результирующий вектор.</param>
        public static void Min(in Vector2Di a, in Vector2Di b, out Vector2Di result)
        {
            result.X = a.X < b.X ? a.X : b.X;
            result.Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Минимальное значение из компонентов векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        /// <returns>Результирующий вектор.</returns>
        public static Vector2Di Min(in Vector2Di a, in Vector2Di b)
        {
            return new Vector2Di(a.X < b.X ? a.X : b.X, a.Y < b.Y ? a.Y : b.Y);
        }

        /// <summary>
        /// Десереализация двухмерного вектора из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Di DeserializeFromString(string data)
        {
            var vector = new Vector2Di();
            var vector_data = data.Split(';');
            vector.X = int.Parse(vector_data[0]);
            vector.Y = int.Parse(vector_data[1]);
            return vector;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Компонента X.
        /// </summary>
        public int X;

        /// <summary>
        /// Компонента Y.
        /// </summary>
        public int Y;
        #endregion

        #region Properties
        /// <summary>
        /// Квадрат длины вектора.
        /// </summary>
        public readonly float SqrLength
        {
            get { return (X * X) + (Y * Y); }
        }

        /// <summary>
        /// Длина вектора.
        /// </summary>
        public readonly float Length
        {
            get { return (float)Math.Sqrt((X * X) + (Y * Y)); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        public Vector2Di(int x, int y)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Конструктор инициализирует вектор указанными параметрами.
        /// </summary>
        /// <param name="x">X - координата.</param>
        /// <param name="y">Y - координата.</param>
        public Vector2Di(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="sourceVector">Вектор.</param>
        public Vector2Di(Vector2Di sourceVector)
        {
            X = sourceVector.X;
            Y = sourceVector.Y;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Конструктор инициализирует вектор указанным вектором.
		/// </summary>
		/// <param name="unity_vector">Вектор.</param>
		public Vector2Di(UnityEngine.Vector2 unity_vector)
		{
			X = (Int32)unity_vector.x;
			Y = (Int32)unity_vector.y;
		}
#endif

        /// <summary>
        /// Конструктор инициализирует вектор указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Vector2Di(Vector2D source)
        {
            X = (int)source.X;
            Y = (int)source.Y;
        }

#if USE_WINDOWS
		/// <summary>
		/// Конструктор инициализирует вектор указанной точкой WPF.
		/// </summary>
		/// <param name="source">Точка WPF.</param>
		public Vector2Di(System.Windows.Vector source)
		{
			X = (Int32)source.X;
			Y = (Int32)source.Y;
		}

		/// <summary>
		/// Конструктор инициализирует вектор указанной точкой WPF.
		/// </summary>
		/// <param name="source">Точка WPF.</param>
		public Vector2Di(System.Windows.Point source)
		{
			X = (Int32)source.X;
			Y = (Int32)source.Y;
		}

		/// <summary>
		/// Конструктор инициализирует вектор разностью точек WPF.
		/// </summary>
		/// <param name="start">Начальная точка WPF.</param>
		/// <param name="end">Конечная точка WPF.</param>
		public Vector2Di(System.Windows.Point start, System.Windows.Point end)
		{
			X = (Int32)(end.X - start.X);
			Y = (Int32)(end.Y - start.Y);
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Конструктор инициализирует вектор указанным вектором SharpDX.
		/// </summary>
		/// <param name="source">Вектор SharpDX.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Di(global::SharpDX.Vector2 source)
		{
			X = (Int32)source.X;
			Y = (Int32)source.Y;
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
            if (obj is Vector2Di vector)
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
        public readonly bool Equals(Vector2Di other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение векторов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый вектор.</param>
        /// <returns>Статус сравнения векторов.</returns>
        public readonly int CompareTo(Vector2Di other)
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
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода вектора.
        /// </summary>
        /// <returns>Хеш-код вектора.</returns>
        public override readonly int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, X, Y);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление вектора с указанием значений координат.</returns>
        public readonly string ToString(string format)
        {
            return "X = " + X.ToString(format) + "; Y = " + Y.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Сумма векторов.</returns>
        public static Vector2Di operator +(Vector2Di left, Vector2Di right)
        {
            return new Vector2Di(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Вычитание векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Разность векторов.</returns>
        public static Vector2Di operator -(Vector2Di left, Vector2Di right)
        {
            return new Vector2Di(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Умножение вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2Di operator *(Vector2Di vector, float scalar)
        {
            return new Vector2Di(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Деление вектора на скаляр.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный вектор.</returns>
        public static Vector2Di operator /(Vector2Di vector, float scalar)
        {
            scalar = 1 / scalar;
            return new Vector2Di(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Умножение вектора на вектор. Скалярное произведение векторов.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Скаляр.</returns>
        public static float operator *(Vector2Di left, Vector2Di right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2Di operator *(Vector2Di vector, Matrix3Dx2f matrix)
        {
            return new Vector2Di((int)((vector.X * matrix.M11) + (vector.Y * matrix.M21)),
                (int)((vector.X * matrix.M12) + (vector.Y * matrix.M22)));
        }

        /// <summary>
        /// Умножение вектора на матрицу трансформации.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <param name="matrix">Матрица трансформации.</param>
        /// <returns>Трансформированный вектор.</returns>
        public static Vector2Di operator *(Vector2Di vector, Matrix4Dx4 matrix)
        {
            return new Vector2Di((int)((vector.X * matrix.M11) + (vector.Y * matrix.M21) + matrix.M41),
                (int)((vector.X * matrix.M12) + (vector.Y * matrix.M22) + matrix.M42));
        }

        /// <summary>
        /// Сравнение векторов на равенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус равенства векторов.</returns>
        public static bool operator ==(Vector2Di left, Vector2Di right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        /// Сравнение векторов на неравенство.
        /// </summary>
        /// <param name="left">Первый вектор.</param>
        /// <param name="right">Второй вектор.</param>
        /// <returns>Статус неравенства векторов.</returns>
        public static bool operator !=(Vector2Di left, Vector2Di right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Vector2Di left, Vector2Di right)
        {
            return left.X < right.X || (left.X == right.X && left.Y < right.Y);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений векторов.
        /// </summary>
        /// <param name="left">Левый вектор.</param>
        /// <param name="right">Правый вектор.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Vector2Di left, Vector2Di right)
        {
            return left.X > right.X || (left.X == right.X && left.Y > right.Y);
        }

        /// <summary>
        /// Обратный вектор.
        /// </summary>
        /// <param name="vector">Исходный вектор.</param>
        /// <returns>Обратный вектор.</returns>
        public static Vector2Di operator -(Vector2Di vector)
        {
            return new Vector2Di(-vector.X, -vector.Y);
        }
        #endregion

        #region Operators conversion
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа UnityEngine.Vector2.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>UnityEngine.Vector2.</returns>
		public static implicit operator UnityEngine.Vector2(Vector2Di vector)
		{
			return new UnityEngine.Vector2(vector.X, vector.Y);
		}
#endif
#if USE_WINDOWS
		/// <summary>
		/// Неявное преобразование в объект типа точки WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка WPF.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Windows.Point(Vector2Di vector)
		{
			return (new System.Windows.Point(vector.X, vector.Y));
		}

		/// <summary>
		/// Неявное преобразование в объект типа вектора WPF.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Вектор WPF.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Windows.Vector(Vector2Di vector)
		{
			return (new System.Windows.Vector(vector.X, vector.Y));
		}
#endif
#if USE_GDI
		/// <summary>
		/// Неявное преобразование в объект типа точки System.Drawing.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка System.Drawing.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Drawing.Point(Vector2Di vector)
		{
			return (new System.Drawing.Point((Int32)vector.X, (Int32)vector.Y));
		}

		/// <summary>
		/// Неявное преобразование в объект типа точки System.Drawing.
		/// </summary>
		/// <param name="vector">Вектор.</param>
		/// <returns>Точка System.Drawing.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator System.Drawing.PointF(Vector2Di vector)
		{
			return (new System.Drawing.PointF(vector.X, vector.Y));
		}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов вектора на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента вектора.</returns>
        public int this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    default:
                        return Y;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    default:
                        Y = value;
                        break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Смещение вектора.
        /// </summary>
        /// <param name="x">Смещение по X.</param>
        /// <param name="y">Смещение по Y.</param>
        public void Offset(int x, int y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Смещение вектора.
        /// </summary>
        /// <param name="x">Смещение по X.</param>
        /// <param name="y">Смещение по Y.</param>
        public void Offset(float x, float y)
        {
            X += (int)x;
            Y += (int)y;
        }

        /// <summary>
        /// Вычисление расстояние до вектора.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Расстояние до вектора.</returns>
        public readonly float Distance(in Vector2Di vector)
        {
            float x = vector.X - X;
            float y = vector.Y - Y;

            return (int)Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Вычисление скалярного произведения векторов.
        /// </summary>
        /// <param name="vector">Вектор.</param>
        /// <returns>Скалярное произведение векторов.</returns>
        public readonly float Dot(in Vector2Di vector)
        {
            return (X * vector.X) + (Y * vector.Y);
        }

        /// <summary>
        /// Установка компонентов вектора из наибольших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMaximize(in Vector2Di a, in Vector2Di b)
        {
            X = a.X > b.X ? a.X : b.X;
            Y = a.Y > b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Установка компонентов вектора из наименьших компонентов двух векторов.
        /// </summary>
        /// <param name="a">Первый вектор.</param>
        /// <param name="b">Второй вектор.</param>
        public void SetMinimize(in Vector2Di a, in Vector2Di b)
        {
            X = a.X < b.X ? a.X : b.X;
            Y = a.Y < b.Y ? a.Y : b.Y;
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix3Dx2f matrix)
        {
            this = new Vector2Di((X * matrix.M11) + (Y * matrix.M21) + matrix.M31,
                                 (X * matrix.M12) + (Y * matrix.M22) + matrix.M31);
        }

        /// <summary>
        /// Трансформация вектора как точки.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsPoint(in Matrix4Dx4 matrix)
        {
            this = new Vector2Di((int)((X * matrix.M11) + (Y * matrix.M21) + matrix.M41),
                                 (int)((X * matrix.M12) + (Y * matrix.M22) + matrix.M42));
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(in Matrix3Dx2f matrix)
        {
            this = new Vector2Di((X * matrix.M11) + (Y * matrix.M21),
                                 (X * matrix.M12) + (Y * matrix.M22));
        }

        /// <summary>
        /// Трансформация вектора как вектора.
        /// </summary>
        /// <param name="matrix">Матрица трансформации.</param>
        public void TransformAsVector(in Matrix4Dx4 matrix)
        {
            this = new Vector2Di((int)((X * matrix.M11) + (Y * matrix.M21)),
                                 (int)((X * matrix.M12) + (Y * matrix.M22)));
        }

        /// <summary>
        /// Сериализация вектора в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1}", X, Y);
        }
        #endregion

        #region Convert methods
        /// <summary>
        /// Преобразование в вектор нулевой X компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2Di ToVector2X()
        {
            return new Vector2Di(X, 0);
        }

        /// <summary>
        /// Преобразование в вектор нулевой Y компонентой.
        /// </summary>
        /// <returns>Вектор.</returns>
        public readonly Vector2Di ToVector2Y()
        {
            return new Vector2Di(0, Y);
        }
        #endregion
    }
    /**@}*/
}