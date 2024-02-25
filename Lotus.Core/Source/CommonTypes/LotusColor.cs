using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Lotus.Core
{
	/** 
	 * \defgroup CoreCommonTypes Подсистема общих типов
	 * \ingroup Core
	 * \brief Подсистема общих типов реализует общие кроссплатформенные типы данных.
	 * @{
	 */
	/// <summary>
	/// Класс для представления цвета.
	/// </summary>
	/// <remarks>
	/// Применяется для кроссплатформенной реализации концепции цвета.
	/// </remarks>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	public struct TColor : IComparable<TColor>, IEquatable<TColor>, ICloneable,
		IAdditionOperators<TColor, TColor, TColor>,
		ISubtractionOperators<TColor, TColor, TColor>,
        IMultiplyOperators<TColor, TColor, TColor>,
        IMultiplyOperators<TColor, float, TColor>,
        IMultiplyOperators<TColor, double, TColor>
    {
		#region Const
		/// <summary>
		/// Текстовый формат отображения компонентов цвета.
		/// </summary>
		private const string ToStringFormat = "A:{0} R:{1} G:{2} B:{3}";
		#endregion

		#region Static methods
		/// <summary>
		/// Преобразование к допустимому значению компоненты цвета.
		/// </summary>
		/// <param name="component">Значение.</param>
		/// <returns>Допустимое значение компоненты цвета.</returns>
		private static byte ToByte(double component)
		{
			var value = (int)(component * 255.0);
			return ToByte(value);
		}

		/// <summary>
		/// Преобразование к допустимому значению компоненты цвета.
		/// </summary>
		/// <param name="component">Значение.</param>
		/// <returns>Допустимое значение компоненты цвета.</returns>
		private static byte ToByte(float component)
		{
			var value = (int)(component * 255.0f);
			return ToByte(value);
		}

		/// <summary>
		/// Преобразование к допустимому значению компоненты цвета.
		/// </summary>
		/// <param name="value">Значение.</param>
		/// <returns>Допустимое значение компоненты цвета.</returns>
		public static byte ToByte(int value)
		{
			return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
		}

		/// <summary>
		/// Сложение цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Add(in TColor a, in TColor b, out TColor result)
		{
			result.A = (byte)(a.A + b.A);
			result.R = (byte)(a.R + b.R);
			result.G = (byte)(a.G + b.G);
			result.B = (byte)(a.B + b.B);
		}

		/// <summary>
		/// Сложение цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor Add(TColor a, TColor b)
		{
			return new TColor(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
		}

		/// <summary>
		/// Вычитание цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Subtract(in TColor a, in TColor b, out TColor result)
		{
			result.A = (byte)(a.A - b.A);
			result.R = (byte)(a.R - b.R);
			result.G = (byte)(a.G - b.G);
			result.B = (byte)(a.B - b.B);
		}

		/// <summary>
		/// Вычитание цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor Subtract(TColor a, TColor b)
		{
			return new TColor(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
		}

		/// <summary>
		/// Модуляция цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Modulate(in TColor a, in TColor b, out TColor result)
		{
			result.A = (byte)(a.A * b.A / 255.0f);
			result.R = (byte)(a.R * b.R / 255.0f);
			result.G = (byte)(a.G * b.G / 255.0f);
			result.B = (byte)(a.B * b.B / 255.0f);
		}

		/// <summary>
		/// Модуляция цвета.
		/// </summary>
		/// <param name="a">Первый цвет.</param>
		/// <param name="b">Второй цвет.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor Modulate(TColor a, TColor b)
		{
			return new TColor(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
		}

		/// <summary>
		/// Масштабирование компонентов цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="scale">Коэффициент масштаба.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Scale(in TColor value, float scale, out TColor result)
		{
			result.A = (byte)(value.A * scale);
			result.R = (byte)(value.R * scale);
			result.G = (byte)(value.G * scale);
			result.B = (byte)(value.B * scale);
		}

		/// <summary>
		/// Масштабирование компонентов цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="scale">Коэффициент масштаба.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor Scale(TColor value, float scale)
		{
			return new TColor((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
		}

		/// <summary>
		/// Инвертированный цвет.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Negate(in TColor value, out TColor result)
		{
			result.A = (byte)(255 - value.A);
			result.R = (byte)(255 - value.R);
			result.G = (byte)(255 - value.G);
			result.B = (byte)(255 - value.B);
		}

		/// <summary>
		/// Инвертированный цвет.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor Negate(TColor value)
		{
			return new TColor(255 - value.R, 255 - value.G, 255 - value.B, 255 - value.A);
		}

		/// <summary>
		/// Ограничение цвета в пределах указанного диапазона.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="min">Минимальное значение.</param>
		/// <param name="max">Максимальное значение.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void Clamp(in TColor value, in TColor min, in TColor max, out TColor result)
		{
			var alpha = value.A;
			alpha = alpha > max.A ? max.A : alpha;
			alpha = alpha < min.A ? min.A : alpha;

			var red = value.R;
			red = red > max.R ? max.R : red;
			red = red < min.R ? min.R : red;

			var green = value.G;
			green = green > max.G ? max.G : green;
			green = green < min.G ? min.G : green;

			var blue = value.B;
			blue = blue > max.B ? max.B : blue;
			blue = blue < min.B ? min.B : blue;

			result = new TColor(red, green, blue, alpha);
		}

		/// <summary>
		/// Формирование цвет из упакованного формата BGRA целого числа.
		/// </summary>
		/// <param name="color">Значение цвета в BGRA формате целого числа.</param>
		/// <returns>Цвет.</returns>
		public static TColor FromBGRA(int color)
		{
			return new TColor((byte)((color >> 16) & 255), (byte)((color >> 8) & 255), (byte)(color & 255), (byte)((color >> 24) & 255));
		}

		/// <summary>
		/// Формирование цвет из упакованного формата BGRA целого числа.
		/// </summary>
		/// <param name="color">Значение цвета в BGRA формате целого числа.</param>
		/// <returns>Цвет.</returns>
		public static TColor FromBGRA(uint color)
		{
			return new TColor((byte)((color >> 16) & 255), (byte)((color >> 8) & 255), (byte)(color & 255), (byte)((color >> 24) & 255));
		}

		/// <summary>
		/// Формирование цвет из упакованного формата ABGR целого числа.
		/// </summary>
		/// <param name="color">Значение цвета в ABGR формате целого числа.</param>
		/// <returns>Цвет.</returns>
		public static TColor FromABGR(int color)
		{
			return new TColor((byte)(color >> 24), (byte)(color >> 16), (byte)(color >> 8), (byte)color);
		}

		/// <summary>
		/// Формирование цвет из упакованного формата RGBA целого числа.
		/// </summary>
		/// <param name="color">Значение цвета в RGBA формате целого числа.</param>
		/// <returns>Цвет.</returns>
		public static TColor FromRGBA(int color)
		{
			return new TColor(color);
		}

		/// <summary>
		/// Регулировка контрастности цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="contrast">Коэффициент контраста.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void AdjustContrast(in TColor value, float contrast, out TColor result)
		{
			result.A = value.A;
			result.R = ToByte(0.5f + (contrast * ((value.R / 255.0f) - 0.5f)));
			result.G = ToByte(0.5f + (contrast * ((value.G / 255.0f) - 0.5f)));
			result.B = ToByte(0.5f + (contrast * ((value.B / 255.0f) - 0.5f)));
		}

		/// <summary>
		/// Регулировка контрастности цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="contrast">Коэффициент контраста.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor AdjustContrast(TColor value, float contrast)
		{
			return new TColor(
				ToByte(0.5f + (contrast * ((value.R / 255.0f) - 0.5f))),
				ToByte(0.5f + (contrast * ((value.G / 255.0f) - 0.5f))),
				ToByte(0.5f + (contrast * ((value.B / 255.0f) - 0.5f))),
				value.A);
		}

		/// <summary>
		/// Регулировка насыщенности цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="saturation">Коэффициент насыщенности.</param>
		/// <param name="result">Результирующий цвет.</param>
		public static void AdjustSaturation(in TColor value, float saturation, out TColor result)
		{
			var grey = (value.R / 255.0f * 0.2125f) + (value.G / 255.0f * 0.7154f) + (value.B / 255.0f * 0.0721f);

			result.A = value.A;
			result.R = ToByte(grey + (saturation * ((value.R / 255.0f) - grey)));
			result.G = ToByte(grey + (saturation * ((value.G / 255.0f) - grey)));
			result.B = ToByte(grey + (saturation * ((value.B / 255.0f) - grey)));
		}

		/// <summary>
		/// Регулировка насыщенности цвета.
		/// </summary>
		/// <param name="value">Цвет.</param>
		/// <param name="saturation">Коэффициент насыщенности.</param>
		/// <returns>Результирующий цвет.</returns>
		public static TColor AdjustSaturation(TColor value, float saturation)
		{
			var grey = (value.R / 255.0f * 0.2125f) + (value.G / 255.0f * 0.7154f) + (value.B / 255.0f * 0.0721f);

			return new TColor(
				ToByte(grey + (saturation * ((value.R / 255.0f) - grey))),
				ToByte(grey + (saturation * ((value.G / 255.0f) - grey))),
				ToByte(grey + (saturation * ((value.B / 255.0f) - grey))),
				value.A);
		}

		/// <summary>
		/// Аппроксимация равенства значений цвета.
		/// </summary>
		/// <param name="a">Первое значение.</param>
		/// <param name="b">Второе значение.</param>
		/// <param name="epsilon">Погрешность.</param>
		/// <returns>Статус равенства значений цвета.</returns>
		public static bool Approximately(in TColor a, in TColor b, int epsilon = 1)
		{
			return Math.Abs(a.R - b.G) <= epsilon &&
				   Math.Abs(a.G - b.G) <= epsilon &&
				   Math.Abs(a.B - b.B) <= epsilon;
		}

		/// <summary>
		/// Десереализация цвета из строки.
		/// </summary>
		/// <param name="data">Строка данных.</param>
		/// <returns>Цвет.</returns>
		public static TColor DeserializeFromString(string data)
		{
			var color = new TColor();
			var color_data = data.Split(',');
			color.R = byte.Parse(color_data[0]);
			color.G = byte.Parse(color_data[1]);
			color.B = byte.Parse(color_data[2]);
			color.A = byte.Parse(color_data[3]);
			return color;
		}
		#endregion

		#region Fields
		/// <summary>
		/// Красная компонента цвета.
		/// </summary>
		public byte R;

		/// <summary>
		/// Зеленая компонента цвета.
		/// </summary>
		public byte G;

		/// <summary>
		/// Синяя компонента цвета.
		/// </summary>
		public byte B;

		/// <summary>
		/// Альфа компонента цвета.
		/// </summary>
		public byte A;
		#endregion

		#region Properties
		/// <summary>
		/// Красная компонента цвета.
		/// </summary>
		public float RedComponent
		{
			readonly get { return R / 255.0f; }
			set { R = ToByte(value); }
		}

		/// <summary>
		/// Зеленая компонента цвета.
		/// </summary>
		public float GreenComponent
		{
			readonly get { return G / 255.0f; }
			set { G = ToByte(value); }
		}

		/// <summary>
		/// Синяя компонента цвета.
		/// </summary>
		public float BlueComponent
		{
			readonly get { return B / 255.0f; }
			set { B = ToByte(value); }
		}

		/// <summary>
		/// Альфа компонента цвета.
		/// </summary>
		public float AlphaComponent
		{
			readonly get { return A / 255.0f; }
			set { A = ToByte(value); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="value">Компонент цвета.</param>
		public TColor(byte value)
		{
			A = R = G = B = value;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="red">Красная компонента цвета.</param>
		/// <param name="green">Зеленая компонента цвета.</param>
		/// <param name="blue">Синяя компонента цвета.</param>
		/// <param name="alpha">Альфа компонента цвета.</param>
		public TColor(byte red, byte green, byte blue, byte alpha = 255)
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="red">Красная компонента цвета.</param>
		/// <param name="green">Зеленая компонента цвета.</param>
		/// <param name="blue">Синяя компонента цвета.</param>
		/// <param name="alpha">Альфа компонента цвета.</param>
		public TColor(float red, float green, float blue, float alpha = 1.0f)
		{
			R = ToByte(red);
			G = ToByte(green);
			B = ToByte(blue);
			A = ToByte(alpha);
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="rgba">Значение цвета в RGBA формате целого числа.</param>
		public TColor(int rgba)
		{
			A = (byte)((rgba >> 24) & 255);
			B = (byte)((rgba >> 16) & 255);
			G = (byte)((rgba >> 8) & 255);
			R = (byte)(rgba & 255);
		}
#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами.
			/// </summary>
			/// <param name="color">Цвет UnityEngine.</param>
			public TColor(UnityEngine.Color color)
			{
				R = (Byte)(color.r * 255);
				G = (Byte)(color.g * 255);
				B = (Byte)(color.b * 255);
				A = (Byte)(color.a * 255);
			}

			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами.
			/// </summary>
			/// <param name="color">Цвет UnityEngine.</param>
			public TColor(UnityEngine.Color32 color)
			{
				R = color.r;
				G = color.g;
				B = color.b;
				A = color.a;
			}
#endif

#if USE_WINDOWS
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами.
			/// </summary>
			/// <param name="color">Цвет WPF.</param>
			public TColor(System.Windows.Media.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
#endif
#if USE_GDI
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами.
			/// </summary>
			/// <param name="color">Цвет WPF.</param>
			public TColor(System.Drawing.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
#endif
#if USE_SHARPDX
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами.
			/// </summary>
			/// <param name="color">Цвет SharpDX.</param>
			public TColor(SharpDX.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
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
			if (obj is TColor color)
			{
				return Equals(color);
			}
			return base.Equals(obj);
		}

		/// <summary>
		/// Проверка равенства цветов по значению.
		/// </summary>
		/// <param name="other">Сравниваемый цвет.</param>
		/// <returns>Статус равенства цветов.</returns>
#if NET45 || UNITY_2017_1_OR_NEWER
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public readonly bool Equals(TColor other)
		{
			return R == other.R && G == other.G && B == other.B && A == other.A;
		}

		/// <summary>
		/// Сравнение цветов для упорядочивания.
		/// </summary>
		/// <param name="other">Сравниваемый цвет.</param>
		/// <returns>Статус сравнения цветов.</returns>
		public readonly int CompareTo(TColor other)
		{
			if (R > other.R)
			{
				return 1;
			}
			else
			{
				if (R == other.R && G > other.G)
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
		/// Получение хеш-кода цвета.
		/// </summary>
		/// <returns>Хеш-код цвета.</returns>
		public override readonly int GetHashCode()
		{
			unchecked
			{
				var hash_code = R.GetHashCode();
				hash_code = (hash_code * 397) ^ G.GetHashCode();
				hash_code = (hash_code * 397) ^ B.GetHashCode();
				hash_code = (hash_code * 397) ^ A.GetHashCode();
				return hash_code;
			}
		}

		/// <summary>
		/// Полное копирование цвета.
		/// </summary>
		/// <returns>Копия цвета.</returns>
		public readonly object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// Преобразование к текстовому представлению.
		/// </summary>
		/// <returns>Текстовое представление цвета с указанием значений компонентов.</returns>
		public override readonly string ToString()
		{
			return string.Format(ToStringFormat, A, R, G, B);
		}
		#endregion

		#region Operators
		/// <summary>
		/// Сравнение объектов на равенство.
		/// </summary>
		/// <param name="left">Первый объект.</param>
		/// <param name="right">Второй объект.</param>
		/// <returns>Статус равенства.</returns>
		public static bool operator ==(TColor left, TColor right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Сравнение объектов на неравенство.
		/// </summary>
		/// <param name="left">Первый объект.</param>
		/// <param name="right">Второй объект.</param>
		/// <returns>Статус неравенство.</returns>
		public static bool operator !=(TColor left, TColor right)
		{
			return !(left == right);
		}

		public static TColor operator +(TColor left, TColor right)
		{
			return TColor.Add(left, right);
		}

		public static TColor operator -(TColor left, TColor right)
		{
			return TColor.Subtract(left, right);
		}

        public static TColor operator *(TColor left, TColor right)
        {
            return TColor.Modulate(left, right);
        }

        public static TColor operator *(TColor left, float right)
        {
            return TColor.Scale(left, right);
        }

        public static TColor operator *(TColor left, double right)
        {
            return TColor.Scale(left, (float)right);
        }
        #endregion

        #region Operators conversion 
#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="UnityEngine.Color32">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="UnityEngine.Color32">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator UnityEngine.Color32(TColor color)
			{
				return new UnityEngine.Color32(color.R, color.G, color.B, color.A);
			}

			/// <summary>
			/// Неявное преобразование в объект типа <see cref="UnityEngine.Color">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="UnityEngine.Color">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator UnityEngine.Color(TColor color)
			{
				return new UnityEngine.Color((color.R / 255.0f), (color.G / 255.0f), 
					(color.B / 255.0f), (color.A / 255.0f));
			}
#endif
#if USE_WINDOWS
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Windows.Media.Color">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="System.Windows.Media.Color">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator System.Windows.Media.Color(TColor color)
			{
				return (System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
			}
#endif
#if USE_GDI
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Drawing.Color">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="System.Drawing.Color">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator System.Drawing.Color(TColor color)
			{
				return (System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
			}
#endif
#if USE_SHARPDX
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.Color">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="SharpDX.Color">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe static implicit operator SharpDX.Color(TColor color)
			{
				return (*(SharpDX.Color*)&color);
			}

			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.Color">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="SharpDX.Color">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator SharpDX.Color4(TColor color)
			{
				return (new SharpDX.Color4(color.RedComponent, color.GreenComponent, color.BlueComponent, color.AlphaComponent));
			}

			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.RawColorBGRA">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="SharpDX.RawColorBGRA">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator global::SharpDX.Mathematics.Interop.RawColorBGRA(TColor value)
			{
				return (new SharpDX.Mathematics.Interop.RawColorBGRA(value.B, value.G, value.R, value.A));
			}

			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.RawColor4">.
			/// </summary>
			/// <param name="color">Цвет.</param>
			/// <returns>Объект <see cref="SharpDX.RawColor4">.</returns>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator SharpDX.Mathematics.Interop.RawColor4(TColor color)
			{
				return (new SharpDX.Mathematics.Interop.RawColor4(color.RedComponent, color.GreenComponent,
					color.BlueComponent, color.AlphaComponent));
			}
#endif
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация компонентов цвета на основе индекса.
        /// </summary>
        /// <param name="index">Индекс компонента.</param>
        /// <returns>Компонента цвета.</returns>
        public byte this[int index]
		{
			readonly get
			{
				switch (index)
				{
					case 0: return R;
					case 1: return G;
					case 2: return B;
					default: return A;
				}
			}

			set
			{
				switch (index)
				{
					case 0: R = value; break;
					case 1: G = value; break;
					case 2: B = value; break;
					default: A = value; break;
				}
			}
		}
		#endregion

		#region Main methods
		/// <summary>
		/// Получение яркости цвета в модели(HSB).
		/// </summary>
		/// <returns>Яркость цвета в модели(HSB).</returns>
		public readonly float GetBrightness()
		{
			var r = R / 255.0f;
			var g = G / 255.0f;
			var b = B / 255.0f;

			float max, min;

			max = r; min = r;

			if (g > max) max = g;
			if (b > max) max = b;

			if (g < min) min = g;
			if (b < min) min = b;

			return (max + min) / 2;
		}

		/// <summary>
		/// Получение оттенка цвета в модели(HSB).
		/// </summary>
		/// <returns>Оттенок цвета в модели(HSB).</returns>
		public readonly float GetHue()
		{
			if (R == G && G == B)
			{
				return 0; // 0 makes as good an UNDEFINED value as any
			}

			var r = R / 255.0f;
			var g = G / 255.0f;
			var b = B / 255.0f;

			float max, min;
			float delta;
			var hue = 0.0f;

			max = r; min = r;

			if (g > max) max = g;
			if (b > max) max = b;

			if (g < min) min = g;
			if (b < min) min = b;

			delta = max - min;

			if (r == max)
			{
				hue = (g - b) / delta;
			}
			else if (g == max)
			{
				hue = 2 + ((b - r) / delta);
			}
			else if (b == max)
			{
				hue = 4 + ((r - g) / delta);
			}
			hue *= 60;

			if (hue < 0.0f)
			{
				hue += 360.0f;
			}

			return hue;
		}

		/// <summary>
		/// Получение насыщенности цвета в модели(HSB).
		/// </summary>
		/// <returns>Насыщенность цвета в модели(HSB).</returns>
		public readonly float GetSaturation()
		{
			var r = R / 255.0f;
			var g = G / 255.0f;
			var b = B / 255.0f;

			float max, min;
			float l, s = 0;

			max = r; min = r;

			if (g > max) max = g;
			if (b > max) max = b;

			if (g < min) min = g;
			if (b < min) min = b;

			// if max == min, then there is no color and
			// the saturation is zero.
			//
			if (max != min)
			{
				l = (max + min) / 2;

				if (l <= .5)
				{
					s = (max - min) / (max + min);
				}
				else
				{
					s = (max - min) / (2 - max - min);
				}
			}
			return s;
		}

		/// <summary>
		/// Сериализация цвета в строку.
		/// </summary>
		/// <returns>Строка данных.</returns>
		public readonly string SerializeToString()
		{
			return string.Format("{0},{1},{2},{3}", R, G, B, A);
		}

		/// <summary>
		/// Преобразование к цвету в формате RGBA.
		/// </summary>
		/// <returns>Цвет в формате RGBA.</returns>
		public readonly int ToRGBA()
		{
			return (R << 24) | (G << 16) | (B << 8) | A;
		}

		/// <summary>
		/// Преобразование к текстовому представлению в шестнадцатеричном формате в порядке RGBA.
		/// </summary>
		/// <returns>Текстовое представление цвета.</returns>
		public readonly string ToStringHEX()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", R, G, B, A);
		}
		#endregion
	}

	/// <summary>
	/// Статический класс определяющий константы цвета. 
	/// </summary>
	public static class XColors
	{
		/// <summary>
		/// Нулевой цвет.
		/// </summary>
		public static readonly TColor Zero = TColor.FromBGRA(0x00000000);

		/// <summary>
		/// Прозрачный цвет.
		/// </summary>
		public static readonly TColor Transparent = TColor.FromBGRA(0x00000000);

		/// <summary>
		/// Цвет - AliceBlue.
		/// </summary>
		public static readonly TColor AliceBlue = TColor.FromBGRA(0xFFF0F8FF);

		/// <summary>
		/// Цвет - AntiqueWhite.
		/// </summary>
		public static readonly TColor AntiqueWhite = TColor.FromBGRA(0xFFFAEBD7);

		/// <summary>
		/// Цвет - Aqua.
		/// </summary>
		public static readonly TColor Aqua = TColor.FromBGRA(0xFF00FFFF);

		/// <summary>
		/// Цвет - Aquamarine.
		/// </summary>
		public static readonly TColor Aquamarine = TColor.FromBGRA(0xFF7FFFD4);

		/// <summary>
		/// Цвет - Azure.
		/// </summary>
		public static readonly TColor Azure = TColor.FromBGRA(0xFFF0FFFF);

		/// <summary>
		/// Цвет - Beige.
		/// </summary>
		public static readonly TColor Beige = TColor.FromBGRA(0xFFF5F5DC);

		/// <summary>
		/// Цвет - Bisque.
		/// </summary>
		public static readonly TColor Bisque = TColor.FromBGRA(0xFFFFE4C4);

		/// <summary>
		/// Цвет - Black.
		/// </summary>
		public static readonly TColor Black = TColor.FromBGRA(0xFF000000);

		/// <summary>
		/// Цвет - BlanchedAlmond.
		/// </summary>
		public static readonly TColor BlanchedAlmond = TColor.FromBGRA(0xFFFFEBCD);

		/// <summary>
		/// Цвет - Blue.
		/// </summary>
		public static readonly TColor Blue = TColor.FromBGRA(0xFF0000FF);

		/// <summary>
		/// Цвет - BlueViolet.
		/// </summary>
		public static readonly TColor BlueViolet = TColor.FromBGRA(0xFF8A2BE2);

		/// <summary>
		/// Цвет - Brown.
		/// </summary>
		public static readonly TColor Brown = TColor.FromBGRA(0xFFA52A2A);

		/// <summary>
		/// Цвет - BurlyWood.
		/// </summary>
		public static readonly TColor BurlyWood = TColor.FromBGRA(0xFFDEB887);

		/// <summary>
		/// Цвет - CadetBlue.
		/// </summary>
		public static readonly TColor CadetBlue = TColor.FromBGRA(0xFF5F9EA0);

		/// <summary>
		/// Цвет - Chartreuse.
		/// </summary>
		public static readonly TColor Chartreuse = TColor.FromBGRA(0xFF7FFF00);

		/// <summary>
		/// Цвет - Chocolate.
		/// </summary>
		public static readonly TColor Chocolate = TColor.FromBGRA(0xFFD2691E);

		/// <summary>
		/// Цвет - Coral.
		/// </summary>
		public static readonly TColor Coral = TColor.FromBGRA(0xFFFF7F50);

		/// <summary>
		/// Цвет - CornflowerBlue.
		/// </summary>
		public static readonly TColor CornflowerBlue = TColor.FromBGRA(0xFF6495ED);

		/// <summary>
		/// Цвет - Cornsilk.
		/// </summary>
		public static readonly TColor Cornsilk = TColor.FromBGRA(0xFFFFF8DC);

		/// <summary>
		/// Цвет - Crimson.
		/// </summary>
		public static readonly TColor Crimson = TColor.FromBGRA(0xFFDC143C);

		/// <summary>
		/// Цвет - Cyan.
		/// </summary>
		public static readonly TColor Cyan = TColor.FromBGRA(0xFF00FFFF);

		/// <summary>
		/// Цвет - DarkBlue.
		/// </summary>
		public static readonly TColor DarkBlue = TColor.FromBGRA(0xFF00008B);

		/// <summary>
		/// Цвет - DarkCyan.
		/// </summary>
		public static readonly TColor DarkCyan = TColor.FromBGRA(0xFF008B8B);

		/// <summary>
		/// Цвет - DarkGoldenrod.
		/// </summary>
		public static readonly TColor DarkGoldenrod = TColor.FromBGRA(0xFFB8860B);

		/// <summary>
		/// Цвет - DarkGray.
		/// </summary>
		public static readonly TColor DarkGray = TColor.FromBGRA(0xFFA9A9A9);

		/// <summary>
		/// Цвет - DarkGreen.
		/// </summary>
		public static readonly TColor DarkGreen = TColor.FromBGRA(0xFF006400);

		/// <summary>
		/// Цвет - DarkKhaki.
		/// </summary>
		public static readonly TColor DarkKhaki = TColor.FromBGRA(0xFFBDB76B);

		/// <summary>
		/// Цвет - DarkMagenta.
		/// </summary>
		public static readonly TColor DarkMagenta = TColor.FromBGRA(0xFF8B008B);

		/// <summary>
		/// Цвет - DarkOliveGreen.
		/// </summary>
		public static readonly TColor DarkOliveGreen = TColor.FromBGRA(0xFF556B2F);

		/// <summary>
		/// Цвет - DarkOrange.
		/// </summary>
		public static readonly TColor DarkOrange = TColor.FromBGRA(0xFFFF8C00);

		/// <summary>
		/// Цвет - DarkOrchid.
		/// </summary>
		public static readonly TColor DarkOrchid = TColor.FromBGRA(0xFF9932CC);

		/// <summary>
		/// Цвет - DarkRed.
		/// </summary>
		public static readonly TColor DarkRed = TColor.FromBGRA(0xFF8B0000);

		/// <summary>
		/// Цвет - DarkSalmon.
		/// </summary>
		public static readonly TColor DarkSalmon = TColor.FromBGRA(0xFFE9967A);

		/// <summary>
		/// Цвет - DarkSeaGreen.
		/// </summary>
		public static readonly TColor DarkSeaGreen = TColor.FromBGRA(0xFF8FBC8B);

		/// <summary>
		/// Цвет - DarkSlateBlue.
		/// </summary>
		public static readonly TColor DarkSlateBlue = TColor.FromBGRA(0xFF483D8B);

		/// <summary>
		/// Цвет - DarkSlateGray.
		/// </summary>
		public static readonly TColor DarkSlateGray = TColor.FromBGRA(0xFF2F4F4F);

		/// <summary>
		/// Цвет - DarkTurquoise.
		/// </summary>
		public static readonly TColor DarkTurquoise = TColor.FromBGRA(0xFF00CED1);

		/// <summary>
		/// Цвет - DarkViolet.
		/// </summary>
		public static readonly TColor DarkViolet = TColor.FromBGRA(0xFF9400D3);

		/// <summary>
		/// Цвет - DeepPink.
		/// </summary>
		public static readonly TColor DeepPink = TColor.FromBGRA(0xFFFF1493);

		/// <summary>
		/// Цвет - DeepSkyBlue.
		/// </summary>
		public static readonly TColor DeepSkyBlue = TColor.FromBGRA(0xFF00BFFF);

		/// <summary>
		/// Цвет - DimGray.
		/// </summary>
		public static readonly TColor DimGray = TColor.FromBGRA(0xFF696969);

		/// <summary>
		/// Цвет - DodgerBlue.
		/// </summary>
		public static readonly TColor DodgerBlue = TColor.FromBGRA(0xFF1E90FF);

		/// <summary>
		/// Цвет - Firebrick.
		/// </summary>
		public static readonly TColor Firebrick = TColor.FromBGRA(0xFFB22222);

		/// <summary>
		/// Цвет - FloralWhite.
		/// </summary>
		public static readonly TColor FloralWhite = TColor.FromBGRA(0xFFFFFAF0);

		/// <summary>
		/// Цвет - ForestGreen.
		/// </summary>
		public static readonly TColor ForestGreen = TColor.FromBGRA(0xFF228B22);

		/// <summary>
		/// Цвет - Fuchsia.
		/// </summary>
		public static readonly TColor Fuchsia = TColor.FromBGRA(0xFFFF00FF);

		/// <summary>
		/// Цвет - Gainsboro.
		/// </summary>
		public static readonly TColor Gainsboro = TColor.FromBGRA(0xFFDCDCDC);

		/// <summary>
		/// Цвет - GhostWhite.
		/// </summary>
		public static readonly TColor GhostWhite = TColor.FromBGRA(0xFFF8F8FF);

		/// <summary>
		/// Цвет - Gold.
		/// </summary>
		public static readonly TColor Gold = TColor.FromBGRA(0xFFFFD700);

		/// <summary>
		/// Цвет - Goldenrod.
		/// </summary>
		public static readonly TColor Goldenrod = TColor.FromBGRA(0xFFDAA520);

		/// <summary>
		/// Цвет - Gray.
		/// </summary>
		public static readonly TColor Gray = TColor.FromBGRA(0xFF808080);

		/// <summary>
		/// Цвет - Green.
		/// </summary>
		public static readonly TColor Green = TColor.FromBGRA(0xFF008000);

		/// <summary>
		/// Цвет - GreenYellow.
		/// </summary>
		public static readonly TColor GreenYellow = TColor.FromBGRA(0xFFADFF2F);

		/// <summary>
		/// Цвет - Honeydew.
		/// </summary>
		public static readonly TColor Honeydew = TColor.FromBGRA(0xFFF0FFF0);

		/// <summary>
		/// Цвет - HotPink.
		/// </summary>
		public static readonly TColor HotPink = TColor.FromBGRA(0xFFFF69B4);

		/// <summary>
		/// Цвет - IndianRed.
		/// </summary>
		public static readonly TColor IndianRed = TColor.FromBGRA(0xFFCD5C5C);

		/// <summary>
		/// Цвет - Indigo.
		/// </summary>
		public static readonly TColor Indigo = TColor.FromBGRA(0xFF4B0082);

		/// <summary>
		/// Цвет - Ivory.
		/// </summary>
		public static readonly TColor Ivory = TColor.FromBGRA(0xFFFFFFF0);

		/// <summary>
		/// Цвет - Khaki.
		/// </summary>
		public static readonly TColor Khaki = TColor.FromBGRA(0xFFF0E68C);

		/// <summary>
		/// Цвет - Lavender.
		/// </summary>
		public static readonly TColor Lavender = TColor.FromBGRA(0xFFE6E6FA);

		/// <summary>
		/// Цвет - LavenderBlush.
		/// </summary>
		public static readonly TColor LavenderBlush = TColor.FromBGRA(0xFFFFF0F5);

		/// <summary>
		/// Цвет - LawnGreen.
		/// </summary>
		public static readonly TColor LawnGreen = TColor.FromBGRA(0xFF7CFC00);

		/// <summary>
		/// Цвет - LemonChiffon.
		/// </summary>
		public static readonly TColor LemonChiffon = TColor.FromBGRA(0xFFFFFACD);

		/// <summary>
		/// Цвет - LightBlue.
		/// </summary>
		public static readonly TColor LightBlue = TColor.FromBGRA(0xFFADD8E6);

		/// <summary>
		/// Цвет - LightCoral.
		/// </summary>
		public static readonly TColor LightCoral = TColor.FromBGRA(0xFFF08080);

		/// <summary>
		/// Цвет - LightCyan.
		/// </summary>
		public static readonly TColor LightCyan = TColor.FromBGRA(0xFFE0FFFF);

		/// <summary>
		/// Цвет - LightGoldenrodYellow.
		/// </summary>
		public static readonly TColor LightGoldenrodYellow = TColor.FromBGRA(0xFFFAFAD2);

		/// <summary>
		/// Цвет - LightGray.
		/// </summary>
		public static readonly TColor LightGray = TColor.FromBGRA(0xFFD3D3D3);

		/// <summary>
		/// Цвет - LightGreen.
		/// </summary>
		public static readonly TColor LightGreen = TColor.FromBGRA(0xFF90EE90);

		/// <summary>
		/// Цвет - LightPink.
		/// </summary>
		public static readonly TColor LightPink = TColor.FromBGRA(0xFFFFB6C1);

		/// <summary>
		/// Цвет - LightSalmon.
		/// </summary>
		public static readonly TColor LightSalmon = TColor.FromBGRA(0xFFFFA07A);

		/// <summary>
		/// Цвет - LightSeaGreen.
		/// </summary>
		public static readonly TColor LightSeaGreen = TColor.FromBGRA(0xFF20B2AA);

		/// <summary>
		/// Цвет - LightSkyBlue.
		/// </summary>
		public static readonly TColor LightSkyBlue = TColor.FromBGRA(0xFF87CEFA);

		/// <summary>
		/// Цвет - LightSlateGray.
		/// </summary>
		public static readonly TColor LightSlateGray = TColor.FromBGRA(0xFF778899);

		/// <summary>
		/// Цвет - LightSteelBlue.
		/// </summary>
		public static readonly TColor LightSteelBlue = TColor.FromBGRA(0xFFB0C4DE);

		/// <summary>
		/// Цвет - LightYellow.
		/// </summary>
		public static readonly TColor LightYellow = TColor.FromBGRA(0xFFFFFFE0);

		/// <summary>
		/// Цвет - Lime.
		/// </summary>
		public static readonly TColor Lime = TColor.FromBGRA(0xFF00FF00);

		/// <summary>
		/// Цвет - LimeGreen.
		/// </summary>
		public static readonly TColor LimeGreen = TColor.FromBGRA(0xFF32CD32);

		/// <summary>
		/// Цвет - Linen.
		/// </summary>
		public static readonly TColor Linen = TColor.FromBGRA(0xFFFAF0E6);

		/// <summary>
		/// Цвет - Magenta.
		/// </summary>
		public static readonly TColor Magenta = TColor.FromBGRA(0xFFFF00FF);

		/// <summary>
		/// Цвет - Maroon.
		/// </summary>
		public static readonly TColor Maroon = TColor.FromBGRA(0xFF800000);

		/// <summary>
		/// Цвет - MediumAquamarine.
		/// </summary>
		public static readonly TColor MediumAquamarine = TColor.FromBGRA(0xFF66CDAA);

		/// <summary>
		/// Цвет - MediumBlue.
		/// </summary>
		public static readonly TColor MediumBlue = TColor.FromBGRA(0xFF0000CD);

		/// <summary>
		/// Цвет - MediumOrchid.
		/// </summary>
		public static readonly TColor MediumOrchid = TColor.FromBGRA(0xFFBA55D3);

		/// <summary>
		/// Цвет - MediumPurple.
		/// </summary>
		public static readonly TColor MediumPurple = TColor.FromBGRA(0xFF9370DB);

		/// <summary>
		/// Цвет - MediumSeaGreen.
		/// </summary>
		public static readonly TColor MediumSeaGreen = TColor.FromBGRA(0xFF3CB371);

		/// <summary>
		/// Цвет - MediumSlateBlue.
		/// </summary>
		public static readonly TColor MediumSlateBlue = TColor.FromBGRA(0xFF7B68EE);

		/// <summary>
		/// Цвет - MediumSpringGreen.
		/// </summary>
		public static readonly TColor MediumSpringGreen = TColor.FromBGRA(0xFF00FA9A);

		/// <summary>
		/// Цвет - MediumTurquoise.
		/// </summary>
		public static readonly TColor MediumTurquoise = TColor.FromBGRA(0xFF48D1CC);

		/// <summary>
		/// Цвет - MediumVioletRed.
		/// </summary>
		public static readonly TColor MediumVioletRed = TColor.FromBGRA(0xFFC71585);

		/// <summary>
		/// Цвет - MidnightBlue.
		/// </summary>
		public static readonly TColor MidnightBlue = TColor.FromBGRA(0xFF191970);

		/// <summary>
		/// Цвет - MintCream.
		/// </summary>
		public static readonly TColor MintCream = TColor.FromBGRA(0xFFF5FFFA);

		/// <summary>
		/// Цвет - MistyRose.
		/// </summary>
		public static readonly TColor MistyRose = TColor.FromBGRA(0xFFFFE4E1);

		/// <summary>
		/// Цвет - Moccasin.
		/// </summary>
		public static readonly TColor Moccasin = TColor.FromBGRA(0xFFFFE4B5);

		/// <summary>
		/// Цвет - NavajoWhite.
		/// </summary>
		public static readonly TColor NavajoWhite = TColor.FromBGRA(0xFFFFDEAD);

		/// <summary>
		/// Цвет - Navy.
		/// </summary>
		public static readonly TColor Navy = TColor.FromBGRA(0xFF000080);

		/// <summary>
		/// Цвет - OldLace.
		/// </summary>
		public static readonly TColor OldLace = TColor.FromBGRA(0xFFFDF5E6);

		/// <summary>
		/// Цвет - Olive.
		/// </summary>
		public static readonly TColor Olive = TColor.FromBGRA(0xFF808000);

		/// <summary>
		/// Цвет - OliveDrab.
		/// </summary>
		public static readonly TColor OliveDrab = TColor.FromBGRA(0xFF6B8E23);

		/// <summary>
		/// Цвет - Orange.
		/// </summary>
		public static readonly TColor Orange = TColor.FromBGRA(0xFFFFA500);

		/// <summary>
		/// Цвет - OrangeRed.
		/// </summary>
		public static readonly TColor OrangeRed = TColor.FromBGRA(0xFFFF4500);

		/// <summary>
		/// Цвет - Orchid.
		/// </summary>
		public static readonly TColor Orchid = TColor.FromBGRA(0xFFDA70D6);

		/// <summary>
		/// Цвет - PaleGoldenrod.
		/// </summary>
		public static readonly TColor PaleGoldenrod = TColor.FromBGRA(0xFFEEE8AA);

		/// <summary>
		/// Цвет - PaleGreen.
		/// </summary>
		public static readonly TColor PaleGreen = TColor.FromBGRA(0xFF98FB98);

		/// <summary>
		/// Цвет - PaleTurquoise.
		/// </summary>
		public static readonly TColor PaleTurquoise = TColor.FromBGRA(0xFFAFEEEE);

		/// <summary>
		/// Цвет - PaleVioletRed.
		/// </summary>
		public static readonly TColor PaleVioletRed = TColor.FromBGRA(0xFFDB7093);

		/// <summary>
		/// Цвет - PapayaWhip.
		/// </summary>
		public static readonly TColor PapayaWhip = TColor.FromBGRA(0xFFFFEFD5);

		/// <summary>
		/// Цвет - PeachPuff.
		/// </summary>
		public static readonly TColor PeachPuff = TColor.FromBGRA(0xFFFFDAB9);

		/// <summary>
		/// Цвет - Peru.
		/// </summary>
		public static readonly TColor Peru = TColor.FromBGRA(0xFFCD853F);

		/// <summary>
		/// Цвет - Pink.
		/// </summary>
		public static readonly TColor Pink = TColor.FromBGRA(0xFFFFC0CB);

		/// <summary>
		/// Цвет - Plum.
		/// </summary>
		public static readonly TColor Plum = TColor.FromBGRA(0xFFDDA0DD);

		/// <summary>
		/// Цвет - PowderBlue.
		/// </summary>
		public static readonly TColor PowderBlue = TColor.FromBGRA(0xFFB0E0E6);

		/// <summary>
		/// Цвет - Purple.
		/// </summary>
		public static readonly TColor Purple = TColor.FromBGRA(0xFF800080);

		/// <summary>
		/// Цвет - Red.
		/// </summary>
		public static readonly TColor Red = TColor.FromBGRA(0xFFFF0000);

		/// <summary>
		/// Цвет - RosyBrown.
		/// </summary>
		public static readonly TColor RosyBrown = TColor.FromBGRA(0xFFBC8F8F);

		/// <summary>
		/// Цвет - RoyalBlue.
		/// </summary>
		public static readonly TColor RoyalBlue = TColor.FromBGRA(0xFF4169E1);

		/// <summary>
		/// Цвет - SaddleBrown.
		/// </summary>
		public static readonly TColor SaddleBrown = TColor.FromBGRA(0xFF8B4513);

		/// <summary>
		/// Цвет - Salmon.
		/// </summary>
		public static readonly TColor Salmon = TColor.FromBGRA(0xFFFA8072);

		/// <summary>
		/// Цвет - SandyBrown.
		/// </summary>
		public static readonly TColor SandyBrown = TColor.FromBGRA(0xFFF4A460);

		/// <summary>
		/// Цвет - SeaGreen.
		/// </summary>
		public static readonly TColor SeaGreen = TColor.FromBGRA(0xFF2E8B57);

		/// <summary>
		/// Цвет - SeaShell.
		/// </summary>
		public static readonly TColor SeaShell = TColor.FromBGRA(0xFFFFF5EE);

		/// <summary>
		/// Цвет - Sienna.
		/// </summary>
		public static readonly TColor Sienna = TColor.FromBGRA(0xFFA0522D);

		/// <summary>
		/// Цвет - Silver.
		/// </summary>
		public static readonly TColor Silver = TColor.FromBGRA(0xFFC0C0C0);

		/// <summary>
		/// Цвет - SkyBlue.
		/// </summary>
		public static readonly TColor SkyBlue = TColor.FromBGRA(0xFF87CEEB);

		/// <summary>
		/// Цвет - SlateBlue.
		/// </summary>
		public static readonly TColor SlateBlue = TColor.FromBGRA(0xFF6A5ACD);

		/// <summary>
		/// Цвет - SlateGray.
		/// </summary>
		public static readonly TColor SlateGray = TColor.FromBGRA(0xFF708090);

		/// <summary>
		/// Цвет - Snow.
		/// </summary>
		public static readonly TColor Snow = TColor.FromBGRA(0xFFFFFAFA);

		/// <summary>
		/// Цвет - SpringGreen.
		/// </summary>
		public static readonly TColor SpringGreen = TColor.FromBGRA(0xFF00FF7F);

		/// <summary>
		/// Цвет - SteelBlue.
		/// </summary>
		public static readonly TColor SteelBlue = TColor.FromBGRA(0xFF4682B4);

		/// <summary>
		/// Цвет - Tan.
		/// </summary>
		public static readonly TColor Tan = TColor.FromBGRA(0xFFD2B48C);

		/// <summary>
		/// Цвет - Teal.
		/// </summary>
		public static readonly TColor Teal = TColor.FromBGRA(0xFF008080);

		/// <summary>
		/// Цвет - Thistle.
		/// </summary>
		public static readonly TColor Thistle = TColor.FromBGRA(0xFFD8BFD8);

		/// <summary>
		/// Цвет - Tomato.
		/// </summary>
		public static readonly TColor Tomato = TColor.FromBGRA(0xFFFF6347);

		/// <summary>
		/// Цвет - Turquoise.
		/// </summary>
		public static readonly TColor Turquoise = TColor.FromBGRA(0xFF40E0D0);

		/// <summary>
		/// Цвет - Violet.
		/// </summary>
		public static readonly TColor Violet = TColor.FromBGRA(0xFFEE82EE);

		/// <summary>
		/// Цвет - Wheat.
		/// </summary>
		public static readonly TColor Wheat = TColor.FromBGRA(0xFFF5DEB3);

		/// <summary>
		/// Цвет - White.
		/// </summary>
		public static readonly TColor White = TColor.FromBGRA(0xFFFFFFFF);

		/// <summary>
		/// Цвет - WhiteSmoke.
		/// </summary>
		public static readonly TColor WhiteSmoke = TColor.FromBGRA(0xFFF5F5F5);

		/// <summary>
		/// Цвет - Yellow.
		/// </summary>
		public static readonly TColor Yellow = TColor.FromBGRA(0xFFFFFF00);

		/// <summary>
		/// Цвет - YellowGreen.
		/// </summary>
		public static readonly TColor YellowGreen = TColor.FromBGRA(0xFF9ACD32);
	}

	/// <summary>
	/// Статический класс определяющий константы цвета в формате BGRA.
	/// </summary>
	public static class XColorBGRA
	{
		/// <summary>
		/// Нулевой цвет.
		/// </summary>
		public const uint Zero = 0x00000000;

		/// <summary>
		/// Прозрачный цвет.
		/// </summary>
		public const uint Transparent = 0x00000000;

		/// <summary>
		/// Цвет - AliceBlue.
		/// </summary>
		public const uint AliceBlue = 0xFFF0F8FF;

		/// <summary>
		/// Цвет - AntiqueWhite.
		/// </summary>
		public const uint AntiqueWhite = 0xFFFAEBD7;

		/// <summary>
		/// Цвет - Aqua.
		/// </summary>
		public const uint Aqua = 0xFF00FFFF;

		/// <summary>
		/// Цвет - Aquamarine.
		/// </summary>
		public const uint Aquamarine = 0xFF7FFFD4;

		/// <summary>
		/// Цвет - Azure.
		/// </summary>
		public const uint Azure = 0xFFF0FFFF;

		/// <summary>
		/// Цвет - Beige.
		/// </summary>
		public const uint Beige = 0xFFF5F5DC;

		/// <summary>
		/// Цвет - Bisque.
		/// </summary>
		public const uint Bisque = 0xFFFFE4C4;

		/// <summary>
		/// Цвет - Black.
		/// </summary>
		public const uint Black = 0xFF000000;

		/// <summary>
		/// Цвет - BlanchedAlmond.
		/// </summary>
		public const uint BlanchedAlmond = 0xFFFFEBCD;

		/// <summary>
		/// Цвет - Blue.
		/// </summary>
		public const uint Blue = 0xFF0000FF;

		/// <summary>
		/// Цвет - BlueViolet.
		/// </summary>
		public const uint BlueViolet = 0xFF8A2BE2;

		/// <summary>
		/// Цвет - Brown.
		/// </summary>
		public const uint Brown = 0xFFA52A2A;

		/// <summary>
		/// Цвет - BurlyWood.
		/// </summary>
		public const uint BurlyWood = 0xFFDEB887;

		/// <summary>
		/// Цвет - CadetBlue.
		/// </summary>
		public const uint CadetBlue = 0xFF5F9EA0;

		/// <summary>
		/// Цвет - Chartreuse.
		/// </summary>
		public const uint Chartreuse = 0xFF7FFF00;

		/// <summary>
		/// Цвет - Chocolate.
		/// </summary>
		public const uint Chocolate = 0xFFD2691E;

		/// <summary>
		/// Цвет - Coral.
		/// </summary>
		public const uint Coral = 0xFFFF7F50;

		/// <summary>
		/// Цвет - CornflowerBlue.
		/// </summary>
		public const uint CornflowerBlue = 0xFF6495ED;

		/// <summary>
		/// Цвет - Cornsilk.
		/// </summary>
		public const uint Cornsilk = 0xFFFFF8DC;

		/// <summary>
		/// Цвет - Crimson.
		/// </summary>
		public const uint Crimson = 0xFFDC143C;

		/// <summary>
		/// Цвет - Cyan.
		/// </summary>
		public const uint Cyan = 0xFF00FFFF;

		/// <summary>
		/// Цвет - DarkBlue.
		/// </summary>
		public const uint DarkBlue = 0xFF00008B;

		/// <summary>
		/// Цвет - DarkCyan.
		/// </summary>
		public const uint DarkCyan = 0xFF008B8B;

		/// <summary>
		/// Цвет - DarkGoldenrod.
		/// </summary>
		public const uint DarkGoldenrod = 0xFFB8860B;

		/// <summary>
		/// Цвет - DarkGray.
		/// </summary>
		public const uint DarkGray = 0xFFA9A9A9;

		/// <summary>
		/// Цвет - DarkGreen.
		/// </summary>
		public const uint DarkGreen = 0xFF006400;

		/// <summary>
		/// Цвет - DarkKhaki.
		/// </summary>
		public const uint DarkKhaki = 0xFFBDB76B;

		/// <summary>
		/// Цвет - DarkMagenta.
		/// </summary>
		public const uint DarkMagenta = 0xFF8B008B;

		/// <summary>
		/// Цвет - DarkOliveGreen.
		/// </summary>
		public const uint DarkOliveGreen = 0xFF556B2F;

		/// <summary>
		/// Цвет - DarkOrange.
		/// </summary>
		public const uint DarkOrange = 0xFFFF8C00;

		/// <summary>
		/// Цвет - DarkOrchid.
		/// </summary>
		public const uint DarkOrchid = 0xFF9932CC;

		/// <summary>
		/// Цвет - DarkRed.
		/// </summary>
		public const uint DarkRed = 0xFF8B0000;

		/// <summary>
		/// Цвет - DarkSalmon.
		/// </summary>
		public const uint DarkSalmon = 0xFFE9967A;

		/// <summary>
		/// Цвет - DarkSeaGreen.
		/// </summary>
		public const uint DarkSeaGreen = 0xFF8FBC8B;

		/// <summary>
		/// Цвет - DarkSlateBlue.
		/// </summary>
		public const uint DarkSlateBlue = 0xFF483D8B;

		/// <summary>
		/// Цвет - DarkSlateGray.
		/// </summary>
		public const uint DarkSlateGray = 0xFF2F4F4F;

		/// <summary>
		/// Цвет - DarkTurquoise.
		/// </summary>
		public const uint DarkTurquoise = 0xFF00CED1;

		/// <summary>
		/// Цвет - DarkViolet.
		/// </summary>
		public const uint DarkViolet = 0xFF9400D3;

		/// <summary>
		/// Цвет - DeepPink.
		/// </summary>
		public const uint DeepPink = 0xFFFF1493;

		/// <summary>
		/// Цвет - DeepSkyBlue.
		/// </summary>
		public const uint DeepSkyBlue = 0xFF00BFFF;

		/// <summary>
		/// Цвет - DimGray.
		/// </summary>
		public const uint DimGray = 0xFF696969;

		/// <summary>
		/// Цвет - DodgerBlue.
		/// </summary>
		public const uint DodgerBlue = 0xFF1E90FF;

		/// <summary>
		/// Цвет - Firebrick.
		/// </summary>
		public const uint Firebrick = 0xFFB22222;

		/// <summary>
		/// Цвет - FloralWhite.
		/// </summary>
		public const uint FloralWhite = 0xFFFFFAF0;

		/// <summary>
		/// Цвет - ForestGreen.
		/// </summary>
		public const uint ForestGreen = 0xFF228B22;

		/// <summary>
		/// Цвет - Fuchsia.
		/// </summary>
		public const uint Fuchsia = 0xFFFF00FF;

		/// <summary>
		/// Цвет - Gainsboro.
		/// </summary>
		public const uint Gainsboro = 0xFFDCDCDC;

		/// <summary>
		/// Цвет - GhostWhite.
		/// </summary>
		public const uint GhostWhite = 0xFFF8F8FF;

		/// <summary>
		/// Цвет - Gold.
		/// </summary>
		public const uint Gold = 0xFFFFD700;

		/// <summary>
		/// Цвет - Goldenrod.
		/// </summary>
		public const uint Goldenrod = 0xFFDAA520;

		/// <summary>
		/// Цвет - Gray.
		/// </summary>
		public const uint Gray = 0xFF808080;

		/// <summary>
		/// Цвет - Green.
		/// </summary>
		public const uint Green = 0xFF008000;

		/// <summary>
		/// Цвет - GreenYellow.
		/// </summary>
		public const uint GreenYellow = 0xFFADFF2F;

		/// <summary>
		/// Цвет - Honeydew.
		/// </summary>
		public const uint Honeydew = 0xFFF0FFF0;

		/// <summary>
		/// Цвет - HotPink.
		/// </summary>
		public const uint HotPink = 0xFFFF69B4;

		/// <summary>
		/// Цвет - IndianRed.
		/// </summary>
		public const uint IndianRed = 0xFFCD5C5C;

		/// <summary>
		/// Цвет - Indigo.
		/// </summary>
		public const uint Indigo = 0xFF4B0082;

		/// <summary>
		/// Цвет - Ivory.
		/// </summary>
		public const uint Ivory = 0xFFFFFFF0;

		/// <summary>
		/// Цвет - Khaki.
		/// </summary>
		public const uint Khaki = 0xFFF0E68C;

		/// <summary>
		/// Цвет - Lavender.
		/// </summary>
		public const uint Lavender = 0xFFE6E6FA;

		/// <summary>
		/// Цвет - LavenderBlush.
		/// </summary>
		public const uint LavenderBlush = 0xFFFFF0F5;

		/// <summary>
		/// Цвет - LawnGreen.
		/// </summary>
		public const uint LawnGreen = 0xFF7CFC00;

		/// <summary>
		/// Цвет - LemonChiffon.
		/// </summary>
		public const uint LemonChiffon = 0xFFFFFACD;

		/// <summary>
		/// Цвет - LightBlue.
		/// </summary>
		public const uint LightBlue = 0xFFADD8E6;

		/// <summary>
		/// Цвет - LightCoral.
		/// </summary>
		public const uint LightCoral = 0xFFF08080;

		/// <summary>
		/// Цвет - LightCyan.
		/// </summary>
		public const uint LightCyan = 0xFFE0FFFF;

		/// <summary>
		/// Цвет - LightGoldenrodYellow.
		/// </summary>
		public const uint LightGoldenrodYellow = 0xFFFAFAD2;

		/// <summary>
		/// Цвет - LightGray.
		/// </summary>
		public const uint LightGray = 0xFFD3D3D3;

		/// <summary>
		/// Цвет - LightGreen.
		/// </summary>
		public const uint LightGreen = 0xFF90EE90;

		/// <summary>
		/// Цвет - LightPink.
		/// </summary>
		public const uint LightPink = 0xFFFFB6C1;

		/// <summary>
		/// Цвет - LightSalmon.
		/// </summary>
		public const uint LightSalmon = 0xFFFFA07A;

		/// <summary>
		/// Цвет - LightSeaGreen.
		/// </summary>
		public const uint LightSeaGreen = 0xFF20B2AA;

		/// <summary>
		/// Цвет - LightSkyBlue.
		/// </summary>
		public const uint LightSkyBlue = 0xFF87CEFA;

		/// <summary>
		/// Цвет - LightSlateGray.
		/// </summary>
		public const uint LightSlateGray = 0xFF778899;

		/// <summary>
		/// Цвет - LightSteelBlue.
		/// </summary>
		public const uint LightSteelBlue = 0xFFB0C4DE;

		/// <summary>
		/// Цвет - LightYellow.
		/// </summary>
		public const uint LightYellow = 0xFFFFFFE0;

		/// <summary>
		/// Цвет - Lime.
		/// </summary>
		public const uint Lime = 0xFF00FF00;

		/// <summary>
		/// Цвет - LimeGreen.
		/// </summary>
		public const uint LimeGreen = 0xFF32CD32;

		/// <summary>
		/// Цвет - Linen.
		/// </summary>
		public const uint Linen = 0xFFFAF0E6;

		/// <summary>
		/// Цвет - Magenta.
		/// </summary>
		public const uint Magenta = 0xFFFF00FF;

		/// <summary>
		/// Цвет - Maroon.
		/// </summary>
		public const uint Maroon = 0xFF800000;

		/// <summary>
		/// Цвет - MediumAquamarine.
		/// </summary>
		public const uint MediumAquamarine = 0xFF66CDAA;

		/// <summary>
		/// Цвет - MediumBlue.
		/// </summary>
		public const uint MediumBlue = 0xFF0000CD;

		/// <summary>
		/// Цвет - MediumOrchid.
		/// </summary>
		public const uint MediumOrchid = 0xFFBA55D3;

		/// <summary>
		/// Цвет - MediumPurple.
		/// </summary>
		public const uint MediumPurple = 0xFF9370DB;

		/// <summary>
		/// Цвет - MediumSeaGreen.
		/// </summary>
		public const uint MediumSeaGreen = 0xFF3CB371;

		/// <summary>
		/// Цвет - MediumSlateBlue.
		/// </summary>
		public const uint MediumSlateBlue = 0xFF7B68EE;

		/// <summary>
		/// Цвет - MediumSpringGreen.
		/// </summary>
		public const uint MediumSpringGreen = 0xFF00FA9A;

		/// <summary>
		/// Цвет - MediumTurquoise.
		/// </summary>
		public const uint MediumTurquoise = 0xFF48D1CC;

		/// <summary>
		/// Цвет - MediumVioletRed.
		/// </summary>
		public const uint MediumVioletRed = 0xFFC71585;

		/// <summary>
		/// Цвет - MidnightBlue.
		/// </summary>
		public const uint MidnightBlue = 0xFF191970;

		/// <summary>
		/// Цвет - MintCream.
		/// </summary>
		public const uint MintCream = 0xFFF5FFFA;

		/// <summary>
		/// Цвет - MistyRose.
		/// </summary>
		public const uint MistyRose = 0xFFFFE4E1;

		/// <summary>
		/// Цвет - Moccasin.
		/// </summary>
		public const uint Moccasin = 0xFFFFE4B5;

		/// <summary>
		/// Цвет - NavajoWhite.
		/// </summary>
		public const uint NavajoWhite = 0xFFFFDEAD;

		/// <summary>
		/// Цвет - Navy.
		/// </summary>
		public const uint Navy = 0xFF000080;

		/// <summary>
		/// Цвет - OldLace.
		/// </summary>
		public const uint OldLace = 0xFFFDF5E6;

		/// <summary>
		/// Цвет - Olive.
		/// </summary>
		public const uint Olive = 0xFF808000;

		/// <summary>
		/// Цвет - OliveDrab.
		/// </summary>
		public const uint OliveDrab = 0xFF6B8E23;

		/// <summary>
		/// Цвет - Orange.
		/// </summary>
		public const uint Orange = 0xFFFFA500;

		/// <summary>
		/// Цвет - OrangeRed.
		/// </summary>
		public const uint OrangeRed = 0xFFFF4500;

		/// <summary>
		/// Цвет - Orchid.
		/// </summary>
		public const uint Orchid = 0xFFDA70D6;

		/// <summary>
		/// Цвет - PaleGoldenrod.
		/// </summary>
		public const uint PaleGoldenrod = 0xFFEEE8AA;

		/// <summary>
		/// Цвет - PaleGreen.
		/// </summary>
		public const uint PaleGreen = 0xFF98FB98;

		/// <summary>
		/// Цвет - PaleTurquoise.
		/// </summary>
		public const uint PaleTurquoise = 0xFFAFEEEE;

		/// <summary>
		/// Цвет - PaleVioletRed.
		/// </summary>
		public const uint PaleVioletRed = 0xFFDB7093;

		/// <summary>
		/// Цвет - PapayaWhip.
		/// </summary>
		public const uint PapayaWhip = 0xFFFFEFD5;

		/// <summary>
		/// Цвет - PeachPuff.
		/// </summary>
		public const uint PeachPuff = 0xFFFFDAB9;

		/// <summary>
		/// Цвет - Peru.
		/// </summary>
		public const uint Peru = 0xFFCD853F;

		/// <summary>
		/// Цвет - Pink.
		/// </summary>
		public const uint Pink = 0xFFFFC0CB;

		/// <summary>
		/// Цвет - Plum.
		/// </summary>
		public const uint Plum = 0xFFDDA0DD;

		/// <summary>
		/// Цвет - PowderBlue.
		/// </summary>
		public const uint PowderBlue = 0xFFB0E0E6;

		/// <summary>
		/// Цвет - Purple.
		/// </summary>
		public const uint Purple = 0xFF800080;

		/// <summary>
		/// Цвет - Red.
		/// </summary>
		public const uint Red = 0xFFFF0000;

		/// <summary>
		/// Цвет - RosyBrown.
		/// </summary>
		public const uint RosyBrown = 0xFFBC8F8F;

		/// <summary>
		/// Цвет - RoyalBlue.
		/// </summary>
		public const uint RoyalBlue = 0xFF4169E1;

		/// <summary>
		/// Цвет - SaddleBrown.
		/// </summary>
		public const uint SaddleBrown = 0xFF8B4513;

		/// <summary>
		/// Цвет - Salmon.
		/// </summary>
		public const uint Salmon = 0xFFFA8072;

		/// <summary>
		/// Цвет - SandyBrown.
		/// </summary>
		public const uint SandyBrown = 0xFFF4A460;

		/// <summary>
		/// Цвет - SeaGreen.
		/// </summary>
		public const uint SeaGreen = 0xFF2E8B57;

		/// <summary>
		/// Цвет - SeaShell.
		/// </summary>
		public const uint SeaShell = 0xFFFFF5EE;

		/// <summary>
		/// Цвет - Sienna.
		/// </summary>
		public const uint Sienna = 0xFFA0522D;

		/// <summary>
		/// Цвет - Silver.
		/// </summary>
		public const uint Silver = 0xFFC0C0C0;

		/// <summary>
		/// Цвет - SkyBlue.
		/// </summary>
		public const uint SkyBlue = 0xFF87CEEB;

		/// <summary>
		/// Цвет - SlateBlue.
		/// </summary>
		public const uint SlateBlue = 0xFF6A5ACD;

		/// <summary>
		/// Цвет - SlateGray.
		/// </summary>
		public const uint SlateGray = 0xFF708090;

		/// <summary>
		/// Цвет - Snow.
		/// </summary>
		public const uint Snow = 0xFFFFFAFA;

		/// <summary>
		/// Цвет - SpringGreen.
		/// </summary>
		public const uint SpringGreen = 0xFF00FF7F;

		/// <summary>
		/// Цвет - SteelBlue.
		/// </summary>
		public const uint SteelBlue = 0xFF4682B4;

		/// <summary>
		/// Цвет - Tan.
		/// </summary>
		public const uint Tan = 0xFFD2B48C;

		/// <summary>
		/// Цвет - Teal.
		/// </summary>
		public const uint Teal = 0xFF008080;

		/// <summary>
		/// Цвет - Thistle.
		/// </summary>
		public const uint Thistle = 0xFFD8BFD8;

		/// <summary>
		/// Цвет - Tomato.
		/// </summary>
		public const uint Tomato = 0xFFFF6347;

		/// <summary>
		/// Цвет - Turquoise.
		/// </summary>
		public const uint Turquoise = 0xFF40E0D0;

		/// <summary>
		/// Цвет - Violet.
		/// </summary>
		public const uint Violet = 0xFFEE82EE;

		/// <summary>
		/// Цвет - Wheat.
		/// </summary>
		public const uint Wheat = 0xFFF5DEB3;

		/// <summary>
		/// Цвет - White.
		/// </summary>
		public const uint White = 0xFFFFFFFF;

		/// <summary>
		/// Цвет - WhiteSmoke.
		/// </summary>
		public const uint WhiteSmoke = 0xFFF5F5F5;

		/// <summary>
		/// Цвет - Yellow.
		/// </summary>
		public const uint Yellow = 0xFFFFFF00;

		/// <summary>
		/// Цвет - YellowGreen.
		/// </summary>
		public const uint YellowGreen = 0xFF9ACD32;
	}
	/**@}*/
}