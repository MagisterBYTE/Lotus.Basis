using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry2D
	*@{*/
    /// <summary>
    /// Структура размерности в двухмерном пространстве.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Size2D : IEquatable<Size2D>, IComparable<Size2D>
    {
        #region Const
        /// <summary>
        /// Пустой размер.
        /// </summary>
        public static readonly Size2D Empty = new(0, 0);

        /// <summary>
        /// Нулевой размер.
        /// </summary>
        public static readonly Size2D Default = new(10, 10);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров размеров.
        /// </summary>
        public static string ToStringFormat = "W = {0:0.00}; H = {1:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Линейная интерполяция размеров.
        /// </summary>
        /// <param name="from">Начальный размер.</param>
        /// <param name="to">Конечный размер.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный размер.</returns>
        public static Size2D Lerp(in Size2D from, in Size2D to, double time)
        {
            Size2D size;
            size.Width = from.Width + ((to.Width - from.Width) * time);
            size.Height = from.Height + ((to.Height - from.Height) * time);
            return size;
        }

        /// <summary>
        /// Десереализация двухмерного размера из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Двухмерный размер.</returns>
        public static Size2D DeserializeFromString(string data)
        {
            var size = new Size2D();
            var size_data = data.Split(';');
            size.Width = XNumberHelper.ParseDouble(size_data[0]);
            size.Height = XNumberHelper.ParseDouble(size_data[1]);
            return size;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Ширина.
        /// </summary>
        public double Width;

        /// <summary>
        /// Высота.
        /// </summary>
        public double Height;
        #endregion

        #region Properties
        /// <summary>
        /// Статус пустого размера.
        /// </summary>
        public readonly bool IsEmpty
        {
            get { return Width == 0 && Height == 0; }
        }

        /// <summary>
        /// Площадь.
        /// </summary>
        public readonly double Area
        {
            get { return Width * Height; }
        }

        /// <summary>
        /// Диагональ.
        /// </summary>
        public readonly double Diagonal
        {
            get { return Math.Sqrt((Width * Width) + (Height * Height)); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует размер указанными параметрами.
        /// </summary>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public Size2D(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Конструктор инициализирует размер указанным размером.
        /// </summary>
        /// <param name="source">Размер.</param>
        public Size2D(Size2D source)
        {
            Width = source.Width;
            Height = source.Height;
        }

        /// <summary>
        /// Конструктор инициализирует размер указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Size2D(Vector2D source)
        {
            Width = source.X;
            Height = source.Y;
        }

#if USE_WINDOWS
		/// <summary>
		/// Конструктор инициализирует размер указанным размером WPF.
		/// </summary>
		/// <param name="source">Размер WPF.</param>
		public Size2D(System.Windows.Size source)
		{
			Width = source.Width;
			Height = source.Height;
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Конструктор инициализирует размер указанным размером SharpDX.
		/// </summary>
		/// <param name="source">Размер SharpDX.</param>
		public Size2D(global::SharpDX.Size2 source)
		{
			Width = source.Width;
			Height = source.Height;
		}

		/// <summary>
		/// Конструктор инициализирует размер указанным размером SharpDX.
		/// </summary>
		/// <param name="source">Размер SharpDX.</param>
		public Size2D(global::SharpDX.Size2F source)
		{
			Width = source.Width;
			Height = source.Height;
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
            if (obj is Size2D size)
            {
                return Equals(size);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства размеров по значению.
        /// </summary>
        /// <param name="other">Сравниваемый размер.</param>
        /// <returns>Статус равенства размеров.</returns>
        public readonly bool Equals(Size2D other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение размеров для упорядочивания.
        /// </summary>
        /// <param name="other">Размер.</param>
        /// <returns>Статус сравнения размеров.</returns>
        public readonly int CompareTo(Size2D other)
        {
            if (Width > other.Width)
            {
                return 1;
            }
            else
            {
                if (Width == other.Width && Height > other.Height)
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
        /// Получение хеш-кода размера.
        /// </summary>
        /// <returns>Хеш-код размера.</returns>
        public override readonly int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление размера с указанием значений.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Width, Height);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление размера с указанием значений.</returns>
        public readonly string ToString(string format)
        {
            return "Width = " + Width.ToString(format) + "; Height = " + Height.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение размеров.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Сумма размеров.</returns>
        public static Size2D operator +(Size2D left, Size2D right)
        {
            return new Size2D(left.Width + right.Width, left.Height + right.Height);
        }

        /// <summary>
        /// Вычитание размеров.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Разность размеров.</returns>
        public static Size2D operator -(Size2D left, Size2D right)
        {
            return new Size2D(left.Width - right.Width, left.Height - right.Height);
        }

        /// <summary>
        /// Умножение размера на скаляр.
        /// </summary>
        /// <param name="size">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный размер.</returns>
        public static Size2D operator *(Size2D size, double scalar)
        {
            return new Size2D(size.Width * scalar, size.Height * scalar);
        }

        /// <summary>
        /// Деление размера на скаляр.
        /// </summary>
        /// <param name="size">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный размер.</returns>
        public static Size2D operator /(Size2D size, double scalar)
        {
            scalar = 1 / scalar;
            return new Size2D(size.Width * scalar, size.Height * scalar);
        }

        /// <summary>
        /// Сравнение размеров на равенство.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Статус равенства размеров.</returns>
        public static bool operator ==(Size2D left, Size2D right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>
        /// Сравнение размеров на неравенство.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Статус неравенства размеров.</returns>
        public static bool operator !=(Size2D left, Size2D right)
        {
            return left.Width != right.Width || left.Height != right.Height;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений размеров.
        /// </summary>
        /// <param name="left">Левый размер.</param>
        /// <param name="right">Правый размер.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Size2D left, Size2D right)
        {
            return left.Width < right.Width || (left.Width == right.Width && left.Height < right.Height);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений размеров.
        /// </summary>
        /// <param name="left">Левый размер.</param>
        /// <param name="right">Правый размер.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Size2D left, Size2D right)
        {
            return left.Width > right.Width || (left.Width == right.Width && left.Height > right.Height);
        }

        /// <summary>
        /// Обратный размер.
        /// </summary>
        /// <param name="size">Исходный размер.</param>
        /// <returns>Обратный размер.</returns>
        public static Size2D operator -(Size2D size)
        {
            return new Size2D(-size.Width, -size.Height);
        }
        #endregion

        #region Operators conversion
        /// <summary>
        /// Неявное преобразование в вектор.
        /// </summary>
        /// <param name="size">Размер.</param>
        /// <returns>Вектор.</returns>
        public static implicit operator Vector2D(Size2D size)
        {
            return new Vector2D(size.Width, size.Height);
        }

#if USE_WINDOWS
		/// <summary>
		/// Неявное преобразование в объект типа размера WPF.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер WPF.</returns>
		public unsafe static implicit operator System.Windows.Size(Size2D size)
		{
			return (*(System.Windows.Size*)&size);
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Неявное преобразование в объект типа размера SharpDX.Size2.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер SharpDX.Size2.</returns>
		public static implicit operator global::SharpDX.Size2(Size2D size)
		{
			return (new global::SharpDX.Size2((Int32)size.Width, (Int32)size.Height));
		}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов размера на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента размера.</returns>
        public double this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return Width;
                    default:
                        return Height;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        Width = value;
                        break;
                    default:
                        Height = value;
                        break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка компонентов размера из наибольших компонентов двух размеров.
        /// </summary>
        /// <param name="a">Первый размер.</param>
        /// <param name="b">Второй размер.</param>
        public void SetMaximize(in Size2D a, in Size2D b)
        {
            Width = a.Width > b.Width ? a.Width : b.Width;
            Height = a.Height > b.Height ? a.Height : b.Height;
        }

        /// <summary>
        /// Установка компонентов размера из наименьших компонентов двух размеров.
        /// </summary>
        /// <param name="a">Первый размер.</param>
        /// <param name="b">Второй размер.</param>
        public void SetMinimize(in Size2D a, in Size2D b)
        {
            Width = a.Width < b.Width ? a.Width : b.Width;
            Height = a.Height < b.Height ? a.Height : b.Height;
        }

        /// <summary>
        /// Сериализация размера в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1}", Width, Height);
        }
        #endregion
    }

    /// <summary>
    /// Структура размерности в двухмерном пространстве.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct Size2Df : IEquatable<Size2Df>, IComparable<Size2Df>
    {
        #region Const
        /// <summary>
        /// Пустой размер.
        /// </summary>
        public static readonly Size2Df Empty = new(0, 0);

        /// <summary>
        /// Нулевой размер.
        /// </summary>
        public static readonly Size2Df Default = new(10, 10);
        #endregion

        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров размеров.
        /// </summary>
        public static string ToStringFormat = "W = {0:0.00}; H = {1:0.00}";
        #endregion

        #region Static methods
        /// <summary>
        /// Линейная интерполяция размеров.
        /// </summary>
        /// <param name="from">Начальный размер.</param>
        /// <param name="to">Конечный размер.</param>
        /// <param name="time">Время от 0 до 1.</param>
        /// <returns>Интерполированный размер.</returns>
        public static Size2Df Lerp(in Size2Df from, in Size2Df to, float time)
        {
            Size2Df size;
            size.Width = from.Width + ((to.Width - from.Width) * time);
            size.Height = from.Height + ((to.Height - from.Height) * time);
            return size;
        }

        /// <summary>
        /// Десереализация двухмерного размера из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Двухмерный размер.</returns>
        public static Size2Df DeserializeFromString(string data)
        {
            var size = new Size2Df();
            var size_data = data.Split(';');
            size.Width = XNumberHelper.ParseSingle(size_data[0]);
            size.Height = XNumberHelper.ParseSingle(size_data[1]);
            return size;
        }
        #endregion

        #region Fields
        /// <summary>
        /// Ширина.
        /// </summary>
        public float Width;

        /// <summary>
        /// Высота.
        /// </summary>
        public float Height;
        #endregion

        #region Properties
        /// <summary>
        /// Статус пустого размера.
        /// </summary>
        public readonly bool IsEmpty
        {
            get { return Width == 0 && Height == 0; }
        }

        /// <summary>
        /// Площадь.
        /// </summary>
        public readonly float Area
        {
            get { return Width * Height; }
        }

        /// <summary>
        /// Диагональ.
        /// </summary>
        public readonly float Diagonal
        {
            get { return (float)Math.Sqrt((Width * Width) + (Height * Height)); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует размер указанными параметрами.
        /// </summary>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public Size2Df(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Конструктор инициализирует размер указанным размером.
        /// </summary>
        /// <param name="source">Размер.</param>
        public Size2Df(Size2Df source)
        {
            Width = source.Width;
            Height = source.Height;
        }

        /// <summary>
        /// Конструктор инициализирует размер указанным вектором.
        /// </summary>
        /// <param name="source">Вектор.</param>
        public Size2Df(Vector2Df source)
        {
            Width = source.X;
            Height = source.Y;
        }

#if USE_WINDOWS
		/// <summary>
		/// Конструктор инициализирует размер указанным размером WPF.
		/// </summary>
		/// <param name="source">Размер WPF.</param>
		public Size2Df(System.Windows.Size source)
		{
			Width = (Single)source.Width;
			Height = (Single)source.Height;
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Конструктор инициализирует размер указанным размером SharpDX.
		/// </summary>
		/// <param name="source">Размер SharpDX.</param>
		public Size2Df(SharpDX.Size2 source)
		{
			Width = source.Width;
			Height = source.Height;
		}

		/// <summary>
		/// Конструктор инициализирует размер указанным размером SharpDX.
		/// </summary>
		/// <param name="source">Размер SharpDX.</param>
		public Size2Df(SharpDX.Size2F source)
		{
			Width = source.Width;
			Height = source.Height;
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
            if (obj is Size2Df size)
            {
                return Equals(size);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства размеров по значению.
        /// </summary>
        /// <param name="other">Сравниваемый размер.</param>
        /// <returns>Статус равенства размеров.</returns>
        public readonly bool Equals(Size2Df other)
        {
            return this == other;
        }

        /// <summary>
        /// Сравнение размеров для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый размер.</param>
        /// <returns>Статус сравнения размеров.</returns>
        public readonly int CompareTo(Size2Df other)
        {
            if (Width > other.Width)
            {
                return 1;
            }
            else
            {
                if (Width == other.Width && Height > other.Height)
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
        /// Получение хеш-кода размера.
        /// </summary>
        /// <returns>Хеш-код размера.</returns>
        public override readonly int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление размера с указанием значений.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, Width, Height);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <param name="format">Формат отображения.</param>
        /// <returns>Текстовое представление размера с указанием значений.</returns>
        public readonly string ToString(string format)
        {
            return "Width = " + Width.ToString(format) + "; Height = " + Height.ToString(format);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение размеров.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Сумма размеров.</returns>
        public static Size2Df operator +(Size2Df left, Size2Df right)
        {
            return new Size2Df(left.Width + right.Width, left.Height + right.Height);
        }

        /// <summary>
        /// Вычитание размеров.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Разность размеров.</returns>
        public static Size2Df operator -(Size2Df left, Size2Df right)
        {
            return new Size2Df(left.Width - right.Width, left.Height - right.Height);
        }

        /// <summary>
        /// Умножение размера на скаляр.
        /// </summary>
        /// <param name="size">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный размер.</returns>
        public static Size2Df operator *(Size2Df size, float scalar)
        {
            return new Size2Df(size.Width * scalar, size.Height * scalar);
        }

        /// <summary>
        /// Деление размера на скаляр.
        /// </summary>
        /// <param name="size">Вектор.</param>
        /// <param name="scalar">Скаляр.</param>
        /// <returns>Масштабированный размер.</returns>
        public static Size2Df operator /(Size2Df size, float scalar)
        {
            scalar = 1 / scalar;
            return new Size2Df(size.Width * scalar, size.Height * scalar);
        }

        /// <summary>
        /// Сравнение размеров на равенство.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Статус равенства размеров.</returns>
        public static bool operator ==(Size2Df left, Size2Df right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }

        /// <summary>
        /// Сравнение размеров на неравенство.
        /// </summary>
        /// <param name="left">Первый размер.</param>
        /// <param name="right">Второй размер.</param>
        /// <returns>Статус неравенства размеров.</returns>
        public static bool operator !=(Size2Df left, Size2Df right)
        {
            return left.Width != right.Width || left.Height != right.Height;
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений размеров.
        /// </summary>
        /// <param name="left">Левый размер.</param>
        /// <param name="right">Правый размер.</param>
        /// <returns>Статус меньше.</returns>
        public static bool operator <(Size2Df left, Size2Df right)
        {
            return left.Width < right.Width || (left.Width == right.Width && left.Height < right.Height);
        }

        /// <summary>
        /// Реализация лексикографического порядка отношений размеров.
        /// </summary>
        /// <param name="left">Левый размер.</param>
        /// <param name="right">Правый размер.</param>
        /// <returns>Статус больше.</returns>
        public static bool operator >(Size2Df left, Size2Df right)
        {
            return left.Width > right.Width || (left.Width == right.Width && left.Height > right.Height);
        }

        /// <summary>
        /// Обратный размер.
        /// </summary>
        /// <param name="size">Исходный размер.</param>
        /// <returns>Обратный размер.</returns>
        public static Size2Df operator -(Size2Df size)
        {
            return new Size2Df(-size.Width, -size.Height);
        }
        #endregion

        #region Operators conversion
        /// <summary>
        /// Неявное преобразование в вектор.
        /// </summary>
        /// <param name="size">Размер.</param>
        /// <returns>Вектор.</returns>
        public static implicit operator Vector2Df(Size2Df size)
        {
            return new Vector2Df(size.Width, size.Height);
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Неявное преобразование в объект типа UnityEngine.Vector2.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>UnityEngine.Vector2.</returns>
		public static implicit operator UnityEngine.Vector2(Size2Df size)
		{
			return new UnityEngine.Vector2(size.Width, size.Height);
		}
#endif
#if USE_WINDOWS
		/// <summary>
		/// Неявное преобразование в объект типа размера WPF.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер WPF.</returns>
		public static implicit operator System.Windows.Size(Size2Df size)
		{
			return (new System.Windows.Size(size.Width, size.Height));
		}
#endif
#if USE_GDI
		/// <summary>
		/// Неявное преобразование в объект типа размера System.Drawing.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер System.Drawing.</returns>
		public static implicit operator System.Drawing.Size(Size2Df size)
		{
			return (new System.Drawing.Size((Int32)size.Width, (Int32)size.Height));
		}

		/// <summary>
		/// Неявное преобразование в объект типа размера System.Drawing.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер System.Drawing.</returns>
		public static implicit operator System.Drawing.SizeF(Size2Df size)
		{
			return (new System.Drawing.SizeF(size.Width, size.Height));
		}
#endif
#if USE_SHARPDX
		/// <summary>
		/// Неявное преобразование в объект типа размера SharpDX.Size2.
		/// </summary>
		/// <param name="size">Размер.</param>
		/// <returns>Размер SharpDX.Size2.</returns>
		public static implicit operator SharpDX.Size2(Size2Df size)
		{
			return (new SharpDX.Size2((Int32)size.Width, (Int32)size.Height));
		}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов размера на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента размера.</returns>
        public float this[int index]
        {
            readonly get
            {
                switch (index)
                {
                    case 0:
                        return Width;
                    default:
                        return Height;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        Width = value;
                        break;
                    default:
                        Height = value;
                        break;
                }
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка компонентов размера из наибольших компонентов двух размеров.
        /// </summary>
        /// <param name="a">Первый размер.</param>
        /// <param name="b">Второй размер.</param>
        public void SetMaximize(in Size2Df a, in Size2Df b)
        {
            Width = a.Width > b.Width ? a.Width : b.Width;
            Height = a.Height > b.Height ? a.Height : b.Height;
        }

        /// <summary>
        /// Установка компонентов размера из наименьших компонентов двух размеров.
        /// </summary>
        /// <param name="a">Первый размер.</param>
        /// <param name="b">Второй размер.</param>
        public void SetMinimize(in Size2Df a, in Size2Df b)
        {
            Width = a.Width < b.Width ? a.Width : b.Width;
            Height = a.Height < b.Height ? a.Height : b.Height;
        }

        /// <summary>
        /// Сериализация размера в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return string.Format("{0};{1}", Width, Height);
        }
        #endregion
    }
    /**@}*/
}