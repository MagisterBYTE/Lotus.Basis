//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DMatrix3x3.cs
*		Трехмерная матрицы размерностью 3x3.
*		Реализация трехмерной матрицы для реализации всевозможных трансформаций объекта в трехмерном пространстве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup MathGeometry3D
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Трехмерная матрицы размерностью 3x3
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Matrix3Dx3 : IEquatable<Matrix3Dx3>, IComparable<Matrix3Dx3>, ICloneable
			
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичная матрица
			/// </summary>
			public static readonly Matrix3Dx3 Identity = new Matrix3Dx3(1, 0, 0, 0, 1, 0, 0, 0, 1);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление определителя трехмерной матрицы
			/// | a1, a2, a3 |
			/// | b1, b2, b3 |
			/// | c1, c2, c3 |
			/// </summary>
			/// <param name="a1">The value row 1 column 1 of the matrix</param>
			/// <param name="a2">The value row 1 column 2 of the matrix</param>
			/// <param name="a3">The value row 1 column 3 of the matrix</param>
			/// <param name="b1">The value row 2 column 1 of the matrix</param>
			/// <param name="b2">The value row 2 column 2 of the matrix</param>
			/// <param name="b3">The value row 2 column 3 of the matrix</param>
			/// <param name="c1">The value row 3 column 1 of the matrix</param>
			/// <param name="c2">The value row 3 column 2 of the matrix</param>
			/// <param name="c3">The value row 3 column 3 of the matrix</param>
			/// <returns>Определитель трехмерной матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double Determinat(Double a1, Double a2, Double a3,
											Double b1, Double b2, Double b3,
											Double c1, Double c2, Double c3)
			{
				return (a1 * Matrix2Dx2.Determinat(b2, b3, c2, c3)) -
				       (b1 * Matrix2Dx2.Determinat(a2, a3, c2, c3)) +
				       (c1 * Matrix2Dx2.Determinat(a2, a3, b2, b3));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="axis">Ось, вектор должен быть единичным</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetRotation(Double angle, in Vector3D axis, out Matrix3Dx3 result)
			{
				var ct = Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = Math.Sin(angle * XMath.DegreeToRadian_D);

				var xx = axis.X * axis.X;
				var yy = axis.Y * axis.Y;
				var zz = axis.Z * axis.Z;
				var xy = axis.X * axis.Y;
				var xz = axis.X * axis.Z;
				var yz = axis.Y * axis.Z;

				result.M11 = xx + (ct * (1 - xx));
				result.M21 = xy + (ct * -xy) + (st * -axis.Z);
				result.M31 = xz + (ct * -xz) + (st * axis.Y);

				result.M12 = xy + (ct * -xy) + (st * axis.Z);
				result.M22 = yy + (ct * (1 - yy));
				result.M32 = yz + (ct * -yz) + (st * -axis.X);

				result.M13 = xz + (ct * -xz) + (st * -axis.Y);
				result.M23 = yz + (ct * -yz) + (st * axis.X);
				result.M33 = zz + (ct * (1 - zz));
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Компонент с позицией 11
			/// </summary>
			public Double M11;
			/// <summary>
			/// Компонент с позицией 12
			/// </summary>
			public Double M12;
			/// <summary>
			/// Компонент с позицией 13
			/// </summary>
			public Double M13;
			/// <summary>
			/// Компонент с позицией 21
			/// </summary>
			public Double M21;
			/// <summary>
			/// Компонент с позицией 22
			/// </summary>
			public Double M22;
			/// <summary>
			/// Компонент с позицией 23
			/// </summary>
			public Double M23;
			/// <summary>
			/// Компонент с позицией 31
			/// </summary>
			public Double M31;
			/// <summary>
			/// Компонент с позицией 32
			/// </summary>
			public Double M32;
			/// <summary>
			/// Компонент с позицией 33
			/// </summary>
			public Double M33;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу соответствующим компонентами
			/// </summary>
			/// <param name="m11">Компонент с позицией 11</param>
			/// <param name="m12">Компонент с позицией 12</param>
			/// <param name="m13">Компонент с позицией 13</param>
			/// <param name="m21">Компонент с позицией 21</param>
			/// <param name="m22">Компонент с позицией 22</param>
			/// <param name="m23">Компонент с позицией 23</param>
			/// <param name="m31">Компонент с позицией 31</param>
			/// <param name="m32">Компонент с позицией 32</param>
			/// <param name="m33">Компонент с позицией 33</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx3(Double m11, Double m12, Double m13,
							Double m21, Double m22, Double m23,
							Double m31, Double m32, Double m33)
			{
				M11 = m11; M12 = m12; M13 = m13;
				M21 = m21; M22 = m22; M23 = m23;
				M31 = m31; M32 = m32; M33 = m33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу указанной матрицей
			/// </summary>
			/// <param name="source">Матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx3(Matrix3Dx3 source)
			{
				M11 = source.M11; M12 = source.M12; M13 = source.M13;
				M21 = source.M21; M22 = source.M22; M23 = source.M23;
				M31 = source.M31; M32 = source.M32; M33 = source.M33;

			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (typeof(Matrix3Dx3) == obj.GetType())
					{
						var matrix = (Matrix3Dx3)obj;
						return Equals(matrix);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства матриц по значению
			/// </summary>
			/// <param name="other">Сравниваемая матрица</param>
			/// <returns>Статус равенства матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(Matrix3Dx3 other)
			{
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение матриц для упорядочивания
			/// </summary>
			/// <param name="other">Матрица</param>
			/// <returns>Статус сравнения матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(Matrix3Dx3 other)
			{
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода матрицы
			/// </summary>
			/// <returns>Хеш-код матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return M11.GetHashCode() ^ M22.GetHashCode() ^ M33.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование матрицы
			/// </summary>
			/// <returns>Копия матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление матрицы с указанием значений компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return "M11 = " + M11.ToString("F3") + "; M22 = " + M22.ToString("F3") +
				       "; M33 = " + M33.ToString("F3");
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Трехмерная матрицы размерностью 3x3
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Matrix3Dx3f : IEquatable<Matrix3Dx3f>, IFormattable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// The size of the <see cref="Matrix3Dx3f"/> type, in bytes
			/// </summary>
			public static readonly Int32 SizeInBytes = Marshal.SizeOf(typeof(Matrix3Dx3f));

			/// <summary>
			/// A <see cref="Matrix3Dx3f"/> with all of its components set to zero
			/// </summary>
			public static readonly Matrix3Dx3f Zero = new Matrix3Dx3f();

			/// <summary>
			/// The identity <see cref="Matrix3Dx3f"/>
			/// </summary>
			public static readonly Matrix3Dx3f Identity = new Matrix3Dx3f() { M11 = 1.0f, M22 = 1.0f, M33 = 1.0f };
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление определителя трехмерной матрицы
			/// | a1, a2, a3 |
			/// | b1, b2, b3 |
			/// | c1, c2, c3 |
			/// </summary>
			/// <param name="a1">The value row 1 column 1 of the matrix</param>
			/// <param name="a2">The value row 1 column 2 of the matrix</param>
			/// <param name="a3">The value row 1 column 3 of the matrix</param>
			/// <param name="b1">The value row 2 column 1 of the matrix</param>
			/// <param name="b2">The value row 2 column 2 of the matrix</param>
			/// <param name="b3">The value row 2 column 3 of the matrix</param>
			/// <param name="c1">The value row 3 column 1 of the matrix</param>
			/// <param name="c2">The value row 3 column 2 of the matrix</param>
			/// <param name="c3">The value row 3 column 3 of the matrix</param>
			/// <returns>Определитель трехмерной матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single Determinat(Single a1, Single a2, Single a3,
											Single b1, Single b2, Single b3,
											Single c1, Single c2, Single c3)
			{
				return (a1 * Matrix2Dx2f.Determinat(b2, b3, c2, c3)) -
					   (b1 * Matrix2Dx2f.Determinat(a2, a3, c2, c3)) +
					   (c1 * Matrix2Dx2f.Determinat(a2, a3, b2, b3));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="axis">Ось, вектор должен быть единичным</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetRotation(Single angle, in Vector3Df axis, out Matrix3Dx3 result)
			{
				var ct = (Single)Math.Cos(angle * XMath.DegreeToRadian_F);
				var st = (Single)Math.Sin(angle * XMath.DegreeToRadian_F);

				var xx = axis.X * axis.X;
				var yy = axis.Y * axis.Y;
				var zz = axis.Z * axis.Z;
				var xy = axis.X * axis.Y;
				var xz = axis.X * axis.Z;
				var yz = axis.Y * axis.Z;

				result.M11 = xx + (ct * (1 - xx));
				result.M21 = xy + (ct * -xy) + (st * -axis.Z);
				result.M31 = xz + (ct * -xz) + (st * axis.Y);

				result.M12 = xy + (ct * -xy) + (st * axis.Z);
				result.M22 = yy + (ct * (1 - yy));
				result.M32 = yz + (ct * -yz) + (st * -axis.X);

				result.M13 = xz + (ct * -xz) + (st * -axis.Y);
				result.M23 = yz + (ct * -yz) + (st * axis.X);
				result.M33 = zz + (ct * (1 - zz));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="Matrix3Dx3f"/> are equal
			/// </summary>
			/// <param name="a">The first matrix</param>
			/// <param name="b">The second matrix</param>
			/// <returns>Статус равенства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Equals(in Matrix3Dx3f a, in Matrix3Dx3f b)
			{
#if UNITY_2017_1_OR_NEWER
				return UnityEngine.Mathf.Approximately(a.M11, b.M11) &&
				       UnityEngine.Mathf.Approximately(a.M12, b.M12) &&
				       UnityEngine.Mathf.Approximately(a.M13, b.M13) &&

				       UnityEngine.Mathf.Approximately(a.M21, b.M21) &&
				       UnityEngine.Mathf.Approximately(a.M22, b.M22) &&
				       UnityEngine.Mathf.Approximately(a.M23, b.M23) &&

				       UnityEngine.Mathf.Approximately(a.M31, b.M31) &&
				       UnityEngine.Mathf.Approximately(a.M32, b.M32) &&
				       UnityEngine.Mathf.Approximately(a.M33, b.M33);
#else
				return XMath.Approximately(a.M11, b.M11) &&
						XMath.Approximately(a.M12, b.M12) &&
						XMath.Approximately(a.M13, b.M13) &&

						XMath.Approximately(a.M21, b.M21) &&
						XMath.Approximately(a.M22, b.M22) &&
						XMath.Approximately(a.M23, b.M23) &&

						XMath.Approximately(a.M31, b.M31) &&
						XMath.Approximately(a.M32, b.M32) &&
						XMath.Approximately(a.M33, b.M33);
#endif

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the sum of two matrices
			/// </summary>
			/// <param name="left">The first matrix to add</param>
			/// <param name="right">The second matrix to add</param>
			/// <param name="result">When the method completes, contains the sum of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Add(in Matrix3Dx3f left, in Matrix3Dx3f right, out Matrix3Dx3f result)
			{
				result.M11 = left.M11 + right.M11;
				result.M12 = left.M12 + right.M12;
				result.M13 = left.M13 + right.M13;
				result.M21 = left.M21 + right.M21;
				result.M22 = left.M22 + right.M22;
				result.M23 = left.M23 + right.M23;
				result.M31 = left.M31 + right.M31;
				result.M32 = left.M32 + right.M32;
				result.M33 = left.M33 + right.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the sum of two matrices
			/// </summary>
			/// <param name="left">The first matrix to add</param>
			/// <param name="right">The second matrix to add</param>
			/// <returns>The sum of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Add(in Matrix3Dx3f left, in Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Add(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the difference between two matrices
			/// </summary>
			/// <param name="left">The first matrix to subtract</param>
			/// <param name="right">The second matrix to subtract</param>
			/// <param name="result">When the method completes, contains the difference between the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Subtract(in Matrix3Dx3f left, in Matrix3Dx3f right, out Matrix3Dx3f result)
			{
				result.M11 = left.M11 - right.M11;
				result.M12 = left.M12 - right.M12;
				result.M13 = left.M13 - right.M13;
				result.M21 = left.M21 - right.M21;
				result.M22 = left.M22 - right.M22;
				result.M23 = left.M23 - right.M23;
				result.M31 = left.M31 - right.M31;
				result.M32 = left.M32 - right.M32;
				result.M33 = left.M33 - right.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the difference between two matrices
			/// </summary>
			/// <param name="left">The first matrix to subtract</param>
			/// <param name="right">The second matrix to subtract</param>
			/// <returns>The difference between the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Subtract(in Matrix3Dx3f left, in Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Subtract(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by the given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <param name="result">When the method completes, contains the scaled matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Multiply(in Matrix3Dx3f left, Single right, out Matrix3Dx3f result)
			{
				result.M11 = left.M11 * right;
				result.M12 = left.M12 * right;
				result.M13 = left.M13 * right;
				result.M21 = left.M21 * right;
				result.M22 = left.M22 * right;
				result.M23 = left.M23 * right;
				result.M31 = left.M31 * right;
				result.M32 = left.M32 * right;
				result.M33 = left.M33 * right;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by the given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Multiply(in Matrix3Dx3f left, Single right)
			{
				Matrix3Dx3f result;
				Multiply(in left, right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the product of two matrices
			/// </summary>
			/// <param name="left">The first matrix to multiply</param>
			/// <param name="right">The second matrix to multiply</param>
			/// <param name="result">The product of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Multiply(in Matrix3Dx3f left, in Matrix3Dx3f right, out Matrix3Dx3f result)
			{
				var temp = new Matrix3Dx3f();
				temp.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31);
				temp.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32);
				temp.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33);
				temp.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31);
				temp.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32);
				temp.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33);
				temp.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31);
				temp.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32);
				temp.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33);
				result = temp;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the product of two matrices
			/// </summary>
			/// <param name="left">The first matrix to multiply</param>
			/// <param name="right">The second matrix to multiply</param>
			/// <returns>The product of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Multiply(in Matrix3Dx3f left, in Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Multiply(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by the given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <param name="result">When the method completes, contains the scaled matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Divide(in Matrix3Dx3f left, Single right, out Matrix3Dx3f result)
			{
				var inv = 1.0f / right;

				result.M11 = left.M11 * inv;
				result.M12 = left.M12 * inv;
				result.M13 = left.M13 * inv;
				result.M21 = left.M21 * inv;
				result.M22 = left.M22 * inv;
				result.M23 = left.M23 * inv;
				result.M31 = left.M31 * inv;
				result.M32 = left.M32 * inv;
				result.M33 = left.M33 * inv;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by the given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Divide(in Matrix3Dx3f left, Single right)
			{
				Matrix3Dx3f result;
				Divide(in left, right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the quotient of two matrices
			/// </summary>
			/// <param name="left">The first matrix to divide</param>
			/// <param name="right">The second matrix to divide</param>
			/// <param name="result">When the method completes, contains the quotient of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Divide(in Matrix3Dx3f left, in Matrix3Dx3f right, out Matrix3Dx3f result)
			{
				result.M11 = left.M11 / right.M11;
				result.M12 = left.M12 / right.M12;
				result.M13 = left.M13 / right.M13;
				result.M21 = left.M21 / right.M21;
				result.M22 = left.M22 / right.M22;
				result.M23 = left.M23 / right.M23;
				result.M31 = left.M31 / right.M31;
				result.M32 = left.M32 / right.M32;
				result.M33 = left.M33 / right.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the quotient of two matrices
			/// </summary>
			/// <param name="left">The first matrix to divide</param>
			/// <param name="right">The second matrix to divide</param>
			/// <returns>The quotient of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Divide(in Matrix3Dx3f left, in Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Divide(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs the exponential operation on a matrix.
			/// </summary>
			/// <param name="value">The matrix to perform the operation on.</param>
			/// <param name="exponent">The exponent to raise the matrix to.</param>
			/// <param name="result">When the method completes, contains the exponential Matrix3Dx3f.</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Exponent(in Matrix3Dx3f value, Int32 exponent, out Matrix3Dx3f result)
			{
				//Source: http://rosettacode.org
				//Reference: http://rosettacode.org/wiki/Matrix3Dx3f-exponentiation_operator

					if (exponent == 0)
				{
					result = Matrix3Dx3f.Identity;
					return;
				}

				if (exponent == 1)
				{
					result = value;
					return;
				}

				Matrix3Dx3f identity = Matrix3Dx3f.Identity;
				Matrix3Dx3f temp = value;

				while (true)
				{
					if ((exponent & 1) != 0)
						identity = identity * temp;

					exponent /= 2;

					if (exponent > 0)
						temp *= temp;
					else
						break;
				}

				result = identity;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs the exponential operation on a matrix
			/// </summary>
			/// <param name="value">The matrix to perform the operation on</param>
			/// <param name="exponent">The exponent to raise the matrix to</param>
			/// <returns>The exponential Matrix3Dx3f</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Exponent(in Matrix3Dx3f value, Int32 exponent)
			{
				Matrix3Dx3f result;
				Exponent(in value, exponent, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to be negated</param>
			/// <param name="result">When the method completes, contains the negated matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Negate(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				result.M11 = -value.M11;
				result.M12 = -value.M12;
				result.M13 = -value.M13;
				result.M21 = -value.M21;
				result.M22 = -value.M22;
				result.M23 = -value.M23;
				result.M31 = -value.M31;
				result.M32 = -value.M32;
				result.M33 = -value.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to be negated</param>
			/// <returns>The negated matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Negate(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				Negate(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a linear interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/></param>
			/// <param name="result">When the method completes, contains the linear interpolation of the two matrices</param>
			/// <remarks>
			/// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static void Lerp(in Matrix3Dx3f start, in Matrix3Dx3f end, Single amount, out Matrix3Dx3f result)
			{
				result.M11 = XMathInterpolation.Lerp(start.M11, end.M11, amount);
				result.M12 = XMathInterpolation.Lerp(start.M12, end.M12, amount);
				result.M13 = XMathInterpolation.Lerp(start.M13, end.M13, amount);
				result.M21 = XMathInterpolation.Lerp(start.M21, end.M21, amount);
				result.M22 = XMathInterpolation.Lerp(start.M22, end.M22, amount);
				result.M23 = XMathInterpolation.Lerp(start.M23, end.M23, amount);
				result.M31 = XMathInterpolation.Lerp(start.M31, end.M31, amount);
				result.M32 = XMathInterpolation.Lerp(start.M32, end.M32, amount);
				result.M33 = XMathInterpolation.Lerp(start.M33, end.M33, amount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a linear interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight of <paramref name="end"/></param>
			/// <returns>The linear interpolation of the two matrices</returns>
			/// <remarks>
			/// Passing <paramref name="time"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Lerp(in Matrix3Dx3f start, in Matrix3Dx3f end, Single time)
			{
				Matrix3Dx3f result;
				Lerp(in start, in end, time, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a cubic interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight of <paramref name="end"/></param>
			/// <param name="result">When the method completes, contains the cubic interpolation of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SmoothStep(in Matrix3Dx3f start, in Matrix3Dx3f end, Single time, out Matrix3Dx3f result)
			{
				time = XMathInterpolation.SmoothStep(time);
				Lerp(in start, in end, time, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a cubic interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight of <paramref name="end"/></param>
			/// <returns>The cubic interpolation of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f SmoothStep(in Matrix3Dx3f start, in Matrix3Dx3f end, Single time)
			{
				Matrix3Dx3f result;
				SmoothStep(in start, in end, time, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the transpose of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose transpose is to be calculated</param>
			/// <param name="result">When the method completes, contains the transpose of the specified matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Transpose(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				var temp = new Matrix3Dx3f();
				temp.M11 = value.M11;
				temp.M12 = value.M21;
				temp.M13 = value.M31;
				temp.M21 = value.M12;
				temp.M22 = value.M22;
				temp.M23 = value.M32;
				temp.M31 = value.M13;
				temp.M32 = value.M23;
				temp.M33 = value.M33;

				result = temp;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the transpose of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose transpose is to be calculated</param>
			/// <param name="result">When the method completes, contains the transpose of the specified matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void TransposeByRef(in Matrix3Dx3f value, ref Matrix3Dx3f result)
			{
				result.M11 = value.M11;
				result.M12 = value.M21;
				result.M13 = value.M31;
				result.M21 = value.M12;
				result.M22 = value.M22;
				result.M23 = value.M32;
				result.M31 = value.M13;
				result.M32 = value.M23;
				result.M33 = value.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the transpose of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose transpose is to be calculated</param>
			/// <returns>The transpose of the specified matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Transpose(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				Transpose(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose inverse is to be calculated</param>
			/// <param name="result">When the method completes, contains the inverse of the specified matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Invert(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				var d11 = (value.M22 * value.M33) + (value.M23 * -value.M32);
				var d12 = (value.M21 * value.M33) + (value.M23 * -value.M31);
				var d13 = (value.M21 * value.M32) + (value.M22 * -value.M31);

				var det = (value.M11 * d11) - (value.M12 * d12) + (value.M13 * d13);
				if (Math.Abs(det) == 0.0f)
				{
					result = Matrix3Dx3f.Zero;
					return;
				}

				det = 1f / det;

				var d21 = (value.M12 * value.M33) + (value.M13 * -value.M32);
				var d22 = (value.M11 * value.M33) + (value.M13 * -value.M31);
				var d23 = (value.M11 * value.M32) + (value.M12 * -value.M31);

				var d31 = (value.M12 * value.M23) - (value.M13 * value.M22);
				var d32 = (value.M11 * value.M23) - (value.M13 * value.M21);
				var d33 = (value.M11 * value.M22) - (value.M12 * value.M21);

				result.M11 = +d11 * det; result.M12 = -d21 * det; result.M13 = +d31 * det;
				result.M21 = -d12 * det; result.M22 = +d22 * det; result.M23 = -d32 * det;
				result.M31 = +d13 * det; result.M32 = -d23 * det; result.M33 = +d33 * det;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose inverse is to be calculated</param>
			/// <returns>The inverse of the specified matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Invert(in Matrix3Dx3f value)
			{
				value.Invert();
				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthogonalizes the specified matrix
			/// </summary>
			/// <param name="value">The matrix to orthogonalize</param>
			/// <param name="result">When the method completes, contains the orthogonalized Matrix3Dx3f.</param>
			/// <remarks>
			/// <para>Orthogonalization is the process of making all rows orthogonal to each other. This
			/// means that any given row in the matrix will be orthogonal to any other given row in the
			/// Matrix3Dx3f.</para>
			/// <para>Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.</para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output</para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static void Orthogonalize(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				//Uses the modified Gram-Schmidt process.
				//q1 = m1
				//q2 = m2 - ((q1 ^ m2) / (q1 ^ q1)) * q1
				//q3 = m3 - ((q1 ^ m3) / (q1 ^ q1)) * q1 - ((q2 ^ m3) / (q2 ^ q2)) * q2

				//By separating the above algorithm into multiple lines, we actually increase accuracy.
				result = value;

				result.Row2 = result.Row2 - (Vector3Df.Dot(result.Row1, result.Row2) / Vector3Df.Dot(result.Row1, result.Row1) * result.Row1);

				result.Row3 = result.Row3 - (Vector3Df.Dot(result.Row1, result.Row3) / Vector3Df.Dot(result.Row1, result.Row1) * result.Row1);
				result.Row3 = result.Row3 - (Vector3Df.Dot(result.Row2, result.Row3) / Vector3Df.Dot(result.Row2, result.Row2) * result.Row2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthogonalizes the specified matrix
			/// </summary>
			/// <param name="value">The matrix to orthogonalize</param>
			/// <returns>The orthogonalized Matrix3Dx3f</returns>
			/// <remarks>
			/// <para>
			/// Orthogonalization is the process of making all rows orthogonal to each other. This
			/// means that any given row in the matrix will be orthogonal to any other given row in the
			/// Matrix3Dx3f.
			/// </para>
			/// <para>
			/// Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.
			/// </para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output
			/// </para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Orthogonalize(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				Orthogonalize(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthonormalizes the specified matrix
			/// </summary>
			/// <param name="value">The matrix to orthonormalize</param>
			/// <param name="result">When the method completes, contains the orthonormalized Matrix3Dx3f</param>
			/// <remarks>
			/// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
			/// other and making all rows and columns of unit length. This means that any given row will
			/// be orthogonal to any other given row and any given column will be orthogonal to any other
			/// given column. Any given row will not be orthogonal to any given column. Every row and every
			/// column will be of unit length.</para>
			/// <para>Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.</para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output</para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static void Orthonormalize(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				//Uses the modified Gram-Schmidt process.
				//Because we are making unit vectors, we can optimize the math for orthonormalization
				//and simplify the projection operation to remove the division.
				//q1 = m1 / |m1|
				//q2 = (m2 - (q1 ^ m2) * q1) / |m2 - (q1 ^ m2) * q1|
				//q3 = (m3 - (q1 ^ m3) * q1 - (q2 ^ m3) * q2) / |m3 - (q1 ^ m3) * q1 - (q2 ^ m3) * q2|

				//By separating the above algorithm into multiple lines, we actually increase accuracy.
				result = value;

				result.Row1 = Vector3Df.Normalize(result.Row1);

				result.Row2 = result.Row2 - (Vector3Df.Dot(result.Row1, result.Row2) * result.Row1);
				result.Row2 = Vector3Df.Normalize(result.Row2);

				result.Row3 = result.Row3 - (Vector3Df.Dot(result.Row1, result.Row3) * result.Row1);
				result.Row3 = result.Row3 - (Vector3Df.Dot(result.Row2, result.Row3) * result.Row2);
				result.Row3 = Vector3Df.Normalize(result.Row3);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthonormalizes the specified matrix
			/// </summary>
			/// <param name="value">The matrix to orthonormalize</param>
			/// <returns>The orthonormalized Matrix3Dx3f</returns>
			/// <remarks>
			/// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
			/// other and making all rows and columns of unit length. This means that any given row will
			/// be orthogonal to any other given row and any given column will be orthogonal to any other
			/// given column. Any given row will not be orthogonal to any given column. Every row and every
			/// column will be of unit length.</para>
			/// <para>Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.</para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output   </para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Orthonormalize(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				Orthonormalize(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into upper triangular form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into upper triangular form</param>
			/// <param name="result">When the method completes, contains the upper triangular Matrix3Dx3f</param>
			/// <remarks>
			/// If the matrix is not invertible (i.e. its determinant is zero) than the result of this
			/// method may produce Single.Nan and Single.Inf values. When the matrix represents a system
			/// of linear equations, than this often means that either no solution exists or an infinite
			/// number of solutions exist
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static void UpperTriangularForm(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				//Adapted from the row echelon code.
				result = value;
				var lead = 0;
				var rowcount = 3;
				var columncount = 3;

				for (var r = 0; r < rowcount; ++r)
				{
					if (columncount <= lead)
						return;

					var i = r;

					while (XMath.IsZero(result[i, lead]))
					{
						i++;

						if (i == rowcount)
						{
							i = r;
							lead++;

							if (lead == columncount)
								return;
						}
					}

					if (i != r)
					{
						result.ExchangeRows(i, r);
					}

					var multiplier = 1f / result[r, lead];

					for (; i < rowcount; ++i)
					{
						if (i != r)
						{
							result[i, 0] -= result[r, 0] * multiplier * result[i, lead];
							result[i, 1] -= result[r, 1] * multiplier * result[i, lead];
							result[i, 2] -= result[r, 2] * multiplier * result[i, lead];
						}
					}

					lead++;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into upper triangular form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into upper triangular form</param>
			/// <returns>The upper triangular Matrix3Dx3f</returns>
			/// <remarks>
			/// If the matrix is not invertible (i.e. its determinant is zero) than the result of this
			/// method may produce Single.Nan and Single.Inf values. When the matrix represents a system
			/// of linear equations, than this often means that either no solution exists or an infinite
			/// number of solutions exist.
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f UpperTriangularForm(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				UpperTriangularForm(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into lower triangular form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into lower triangular form</param>
			/// <param name="result">When the method completes, contains the lower triangular Matrix3Dx3f</param>
			/// <remarks>
			/// If the matrix is not invertible (i.e. its determinant is zero) than the result of this
			/// method may produce Single.Nan and Single.Inf values. When the matrix represents a system
			/// of linear equations, than this often means that either no solution exists or an infinite
			/// number of solutions exist.
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static void LowerTriangularForm(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				//Adapted from the row echelon code.
				Matrix3Dx3f temp = value;
				Matrix3Dx3f.Transpose(in temp, out result);

				var lead = 0;
				var rowcount = 3;
				var columncount = 3;

				for (var r = 0; r < rowcount; ++r)
				{
					if (columncount <= lead)
						return;

					var i = r;

					while (XMath.IsZero(result[i, lead]))
					{
						i++;

						if (i == rowcount)
						{
							i = r;
							lead++;

							if (lead == columncount)
								return;
						}
					}

					if (i != r)
					{
						result.ExchangeRows(i, r);
					}

					var multiplier = 1f / result[r, lead];

					for (; i < rowcount; ++i)
					{
						if (i != r)
						{
							result[i, 0] -= result[r, 0] * multiplier * result[i, lead];
							result[i, 1] -= result[r, 1] * multiplier * result[i, lead];
							result[i, 2] -= result[r, 2] * multiplier * result[i, lead];
						}
					}

					lead++;
				}

				Matrix3Dx3f.Transpose(in result, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into lower triangular form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into lower triangular form</param>
			/// <returns>The lower triangular Matrix3Dx3f</returns>
			/// <remarks>
			/// If the matrix is not invertible (i.e. its determinant is zero) than the result of this
			/// method may produce Single.Nan and Single.Inf values. When the matrix represents a system
			/// of linear equations, than this often means that either no solution exists or an infinite
			/// number of solutions exist.
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f LowerTriangularForm(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				LowerTriangularForm(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into row echelon form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into row echelon form</param>
			/// <param name="result">When the method completes, contains the row echelon form of the matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RowEchelonForm(in Matrix3Dx3f value, out Matrix3Dx3f result)
			{
				//Source: Wikipedia pseudo code
				//Reference: http://en.wikipedia.org/wiki/Row_echelon_form#Pseudocode

				result = value;
				var lead = 0;
				var rowcount = 3;
				var columncount = 3;

				for (var r = 0; r < rowcount; ++r)
				{
					if (columncount <= lead)
						return;

					var i = r;

					while (XMath.IsZero(result[i, lead]))
					{
						i++;

						if (i == rowcount)
						{
							i = r;
							lead++;

							if (lead == columncount)
								return;
						}
					}

					if (i != r)
					{
						result.ExchangeRows(i, r);
					}

					var multiplier = 1f / result[r, lead];
					result[r, 0] *= multiplier;
					result[r, 1] *= multiplier;
					result[r, 2] *= multiplier;

					for (; i < rowcount; ++i)
					{
						if (i != r)
						{
							result[i, 0] -= result[r, 0] * result[i, lead];
							result[i, 1] -= result[r, 1] * result[i, lead];
							result[i, 2] -= result[r, 2] * result[i, lead];
						}
					}

					lead++;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Brings the matrix into row echelon form using elementary row operations
			/// </summary>
			/// <param name="value">The matrix to put into row echelon form</param>
			/// <returns>When the method completes, contains the row echelon form of the matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RowEchelonForm(in Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				RowEchelonForm(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a left-handed spherical billboard that rotates around a specified object position
			/// </summary>
			/// <param name="objectPosition">The position of the object around which the billboard will rotate</param>
			/// <param name="cameraPosition">The position of the camera</param>
			/// <param name="cameraUpVector">The up vector of the camera</param>
			/// <param name="cameraForwardVector">The forward vector of the camera</param>
			/// <param name="result">When the method completes, contains the created billboard Matrix3Dx3f</param>
			//---------------------------------------------------------------------------------------------------------
			public static void BillboardLH(in Vector3Df objectPosition, in Vector3Df cameraPosition, in Vector3Df cameraUpVector, in Vector3Df cameraForwardVector, out Matrix3Dx3f result)
			{
				Vector3Df crossed;
				Vector3Df final;
				Vector3Df difference = cameraPosition - objectPosition;

				var lengthSq = difference.SqrLength;
				if (XMath.IsZero(lengthSq))
					difference = -cameraForwardVector;
				else
					difference *= (Single)(1.0 / Math.Sqrt(lengthSq));

				Vector3Df.Cross(in cameraUpVector, in difference, out crossed);
				crossed.Normalize();
				Vector3Df.Cross(in difference, in crossed, out final);

				result.M11 = crossed.X;
				result.M12 = crossed.Y;
				result.M13 = crossed.Z;
				result.M21 = final.X;
				result.M22 = final.Y;
				result.M23 = final.Z;
				result.M31 = difference.X;
				result.M32 = difference.Y;
				result.M33 = difference.Z;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a left-handed spherical billboard that rotates around a specified object position
			/// </summary>
			/// <param name="objectPosition">The position of the object around which the billboard will rotate</param>
			/// <param name="cameraPosition">The position of the camera</param>
			/// <param name="cameraUpVector">The up vector of the camera</param>
			/// <param name="cameraForwardVector">The forward vector of the camera</param>
			/// <returns>The created billboard Matrix3Dx3f</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f BillboardLH(in Vector3Df objectPosition, in Vector3Df cameraPosition, in Vector3Df cameraUpVector, 
				in Vector3Df cameraForwardVector)
			{
				Matrix3Dx3f result;
				BillboardLH(in objectPosition, in cameraPosition, in cameraUpVector, in cameraForwardVector, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a right-handed spherical billboard that rotates around a specified object position
			/// </summary>
			/// <param name="objectPosition">The position of the object around which the billboard will rotate</param>
			/// <param name="cameraPosition">The position of the camera</param>
			/// <param name="cameraUpVector">The up vector of the camera</param>
			/// <param name="cameraForwardVector">The forward vector of the camera</param>
			/// <param name="result">When the method completes, contains the created billboard Matrix3Dx3f</param>
			//---------------------------------------------------------------------------------------------------------
			public static void BillboardRH(in Vector3Df objectPosition, in Vector3Df cameraPosition, in Vector3Df cameraUpVector, 
				in Vector3Df cameraForwardVector, out Matrix3Dx3f result)
			{
				Vector3Df crossed;
				Vector3Df final;
				Vector3Df difference = objectPosition - cameraPosition;

				var lengthSq = difference.SqrLength;
				if (XMath.IsZero(lengthSq))
					difference = -cameraForwardVector;
				else
					difference *= (Single)(1.0 / Math.Sqrt(lengthSq));

				Vector3Df.Cross(in cameraUpVector, in difference, out crossed);
				crossed.Normalize();
				Vector3Df.Cross(in difference, in crossed, out final);

				result.M11 = crossed.X;
				result.M12 = crossed.Y;
				result.M13 = crossed.Z;
				result.M21 = final.X;
				result.M22 = final.Y;
				result.M23 = final.Z;
				result.M31 = difference.X;
				result.M32 = difference.Y;
				result.M33 = difference.Z;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a right-handed spherical billboard that rotates around a specified object position
			/// </summary>
			/// <param name="objectPosition">The position of the object around which the billboard will rotate</param>
			/// <param name="cameraPosition">The position of the camera</param>
			/// <param name="cameraUpVector">The up vector of the camera</param>
			/// <param name="cameraForwardVector">The forward vector of the camera</param>
			/// <returns>The created billboard Matrix3Dx3f</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f BillboardRH(in Vector3Df objectPosition, in Vector3Df cameraPosition, in Vector3Df cameraUpVector,
				in Vector3Df cameraForwardVector)
			{
				Matrix3Dx3f result;
				BillboardRH(in objectPosition, in cameraPosition, in cameraUpVector, in cameraForwardVector, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a left-handed, look-at matrix
			/// </summary>
			/// <param name="eye">The position of the viewer's eye</param>
			/// <param name="target">The camera look-at target</param>
			/// <param name="up">The camera's up vector</param>
			/// <param name="result">When the method completes, contains the created look-at matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LookAtLH(in Vector3Df eye, in Vector3Df target, in Vector3Df up, out Matrix3Dx3f result)
			{
				Vector3Df xaxis, yaxis, zaxis;
				Vector3Df.Subtract(in target, in eye, out zaxis); zaxis.Normalize();
				Vector3Df.Cross(in up, in zaxis, out xaxis); xaxis.Normalize();
				Vector3Df.Cross(in zaxis, in xaxis, out yaxis);

				result = Matrix3Dx3f.Identity;
				result.M11 = xaxis.X; result.M21 = xaxis.Y; result.M31 = xaxis.Z;
				result.M12 = yaxis.X; result.M22 = yaxis.Y; result.M32 = yaxis.Z;
				result.M13 = zaxis.X; result.M23 = zaxis.Y; result.M33 = zaxis.Z;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a left-handed, look-at matrix
			/// </summary>
			/// <param name="eye">The position of the viewer's eye</param>
			/// <param name="target">The camera look-at target</param>
			/// <param name="up">The camera's up vector</param>
			/// <returns>The created look-at matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f LookAtLH(Vector3Df eye, Vector3Df target, Vector3Df up)
			{
				Matrix3Dx3f result;
				LookAtLH(in eye, in target, in up, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a right-handed, look-at matrix
			/// </summary>
			/// <param name="eye">The position of the viewer's eye</param>
			/// <param name="target">The camera look-at target</param>
			/// <param name="up">The camera's up vector</param>
			/// <param name="result">When the method completes, contains the created look-at matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LookAtRH(in Vector3Df eye, in Vector3Df target, in Vector3Df up, out Matrix3Dx3f result)
			{
				Vector3Df xaxis, yaxis, zaxis;
				Vector3Df.Subtract(in eye, in target, out zaxis); zaxis.Normalize();
				Vector3Df.Cross(in up, in zaxis, out xaxis); xaxis.Normalize();
				Vector3Df.Cross(in zaxis, in xaxis, out yaxis);

				result = Matrix3Dx3f.Identity;
				result.M11 = xaxis.X; result.M21 = xaxis.Y; result.M31 = xaxis.Z;
				result.M12 = yaxis.X; result.M22 = yaxis.Y; result.M32 = yaxis.Z;
				result.M13 = zaxis.X; result.M23 = zaxis.Y; result.M33 = zaxis.Z;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a right-handed, look-at matrix
			/// </summary>
			/// <param name="eye">The position of the viewer's eye</param>
			/// <param name="target">The camera look-at target</param>
			/// <param name="up">The camera's up vector</param>
			/// <returns>The created look-at matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f LookAtRH(in Vector3Df eye, in Vector3Df target, in Vector3Df up)
			{
				Matrix3Dx3f result;
				LookAtRH(in eye, in target, in up, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis, y-axis, and y-axis
			/// </summary>
			/// <param name="scale">Scaling factor for all three axes</param>
			/// <param name="result">When the method completes, contains the created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(in Vector3Df scale, out Matrix3Dx3f result)
			{
				Scaling(scale.X, scale.Y, scale.Z, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis, y-axis, and y-axis
			/// </summary>
			/// <param name="scale">Scaling factor for all three axes</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Scaling(in Vector3Df scale)
			{
				Matrix3Dx3f result;
				Scaling(in scale, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis, y-axis, and y-axis
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <param name="z">Scaling factor that is applied along the z-axis</param>
			/// <param name="result">When the method completes, contains the created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(Single x, Single y, Single z, out Matrix3Dx3f result)
			{
				result = Matrix3Dx3f.Identity;
				result.M11 = x;
				result.M22 = y;
				result.M33 = z;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis, y-axis, and y-axis
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <param name="z">Scaling factor that is applied along the z-axis</param>
			/// <returns>The created scaling matrix.</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Scaling(Single x, Single y, Single z)
			{
				Matrix3Dx3f result;
				Scaling(x, y, z, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that uniformly scales along all three axis
			/// </summary>
			/// <param name="scale">The uniform scale that is applied along all axis</param>
			/// <param name="result">When the method completes, contains the created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(Single scale, out Matrix3Dx3f result)
			{
				result = Matrix3Dx3f.Identity;
				result.M11 = result.M22 = result.M33 = scale;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that uniformly scales along all three axis
			/// </summary>
			/// <param name="scale">The uniform scale that is applied along all axis</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f Scaling(Single scale)
			{
				Matrix3Dx3f result;
				Scaling(scale, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the x-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationX(Single angle, out Matrix3Dx3f result)
			{
				var cos = (Single)Math.Cos(angle);
				var sin = (Single)Math.Sin(angle);

				result = Matrix3Dx3f.Identity;
				result.M22 = cos;
				result.M23 = sin;
				result.M32 = -sin;
				result.M33 = cos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the x-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationX(Single angle)
			{
				Matrix3Dx3f result;
				RotationX(angle, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the y-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin.</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationY(Single angle, out Matrix3Dx3f result)
			{
				var cos = (Single)Math.Cos(angle);
				var sin = (Single)Math.Sin(angle);

				result = Matrix3Dx3f.Identity;
				result.M11 = cos;
				result.M13 = -sin;
				result.M31 = sin;
				result.M33 = cos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the y-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationY(Single angle)
			{
				Matrix3Dx3f result;
				RotationY(angle, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the z-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationZ(Single angle, out Matrix3Dx3f result)
			{
				var cos = (Single)Math.Cos(angle);
				var sin = (Single)Math.Sin(angle);

				result = Matrix3Dx3f.Identity;
				result.M11 = cos;
				result.M12 = sin;
				result.M21 = -sin;
				result.M22 = cos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around the z-axis
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationZ(Single angle)
			{
				Matrix3Dx3f result;
				RotationZ(angle, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around an arbitrary axis
			/// </summary>
			/// <param name="axis">The axis around which to rotate. This parameter is assumed to be normalized</param>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationAxis(in Vector3Df axis, Single angle, out Matrix3Dx3f result)
			{
				var x = axis.X;
				var y = axis.Y;
				var z = axis.Z;
				var cos = (Single)Math.Cos(angle);
				var sin = (Single)Math.Sin(angle);
				var xx = x * x;
				var yy = y * y;
				var zz = z * z;
				var xy = x * y;
				var xz = x * z;
				var yz = y * z;

				result = Matrix3Dx3f.Identity;
				result.M11 = xx + (cos * (1.0f - xx));
				result.M12 = xy - (cos * xy) + (sin * z);
				result.M13 = xz - (cos * xz) - (sin * y);
				result.M21 = xy - (cos * xy) - (sin * z);
				result.M22 = yy + (cos * (1.0f - yy));
				result.M23 = yz - (cos * yz) + (sin * x);
				result.M31 = xz - (cos * xz) + (sin * y);
				result.M32 = yz - (cos * yz) - (sin * x);
				result.M33 = zz + (cos * (1.0f - zz));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates around an arbitrary axis
			/// </summary>
			/// <param name="axis">The axis around which to rotate. This parameter is assumed to be normalized</param>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along
			/// the rotation axis toward the origin</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationAxis(in Vector3Df axis, Single angle)
			{
				Matrix3Dx3f result;
				RotationAxis(in axis, angle, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a rotation matrix from a quaternion
			/// </summary>
			/// <param name="rotation">The quaternion to use to build the matrix</param>
			/// <param name="result">The created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationQuaternion(in Quaternion3Df rotation, out Matrix3Dx3f result)
			{
				var xx = rotation.X * rotation.X;
				var yy = rotation.Y * rotation.Y;
				var zz = rotation.Z * rotation.Z;
				var xy = rotation.X * rotation.Y;
				var zw = rotation.Z * rotation.W;
				var zx = rotation.Z * rotation.X;
				var yw = rotation.Y * rotation.W;
				var yz = rotation.Y * rotation.Z;
				var xw = rotation.X * rotation.W;

				result = Matrix3Dx3f.Identity;
				result.M11 = 1.0f - (2.0f * (yy + zz));
				result.M12 = 2.0f * (xy + zw);
				result.M13 = 2.0f * (zx - yw);
				result.M21 = 2.0f * (xy - zw);
				result.M22 = 1.0f - (2.0f * (zz + xx));
				result.M23 = 2.0f * (yz + xw);
				result.M31 = 2.0f * (zx + yw);
				result.M32 = 2.0f * (yz - xw);
				result.M33 = 1.0f - (2.0f * (yy + xx));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a rotation matrix from a quaternion
			/// </summary>
			/// <param name="rotation">The quaternion to use to build the matrix</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationQuaternion(in Quaternion3Df rotation)
			{
				Matrix3Dx3f result;
				RotationQuaternion(in rotation, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a rotation matrix with a specified yaw, pitch, and roll
			/// </summary>
			/// <param name="yaw">Yaw around the y-axis, in radians</param>
			/// <param name="pitch">Pitch around the x-axis, in radians</param>
			/// <param name="roll">Roll around the z-axis, in radians</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationYawPitchRoll(Single yaw, Single pitch, Single roll, out Matrix3Dx3f result)
			{
				_ = new Quaternion3Df();
				Quaternion3Df quaternion;
				Quaternion3Df.RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
				RotationQuaternion(in quaternion, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a rotation matrix with a specified yaw, pitch, and roll
			/// </summary>
			/// <param name="yaw">Yaw around the y-axis, in radians</param>
			/// <param name="pitch">Pitch around the x-axis, in radians</param>
			/// <param name="roll">Roll around the z-axis, in radians</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f RotationYawPitchRoll(Single yaw, Single pitch, Single roll)
			{
				Matrix3Dx3f result;
				RotationYawPitchRoll(yaw, pitch, roll, out result);
				return result;
			}
#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Value at row 1 column 1 of the matrix
			/// </summary>
			public Single M11;

			/// <summary>
			/// Value at row 1 column 2 of the matrix
			/// </summary>
			public Single M12;

			/// <summary>
			/// Value at row 1 column 3 of the matrix
			/// </summary>
			public Single M13;

			/// <summary>
			/// Value at row 2 column 1 of the matrix
			/// </summary>
			public Single M21;

			/// <summary>
			/// Value at row 2 column 2 of the matrix
			/// </summary>
			public Single M22;

			/// <summary>
			/// Value at row 2 column 3 of the matrix
			/// </summary>
			public Single M23;

			/// <summary>
			/// Value at row 3 column 1 of the matrix
			/// </summary>
			public Single M31;

			/// <summary>
			/// Value at row 3 column 2 of the matrix
			/// </summary>
			public Single M32;

			/// <summary>
			/// Value at row 3 column 3 of the matrix
			/// </summary>
			public Single M33;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Gets or sets the first row in the matrix; that is M11, M12, M13
			/// </summary>
			public Vector3Df Row1
			{
				get { return new Vector3Df(M11, M12, M13); }
				set { M11 = value.X; M12 = value.Y; M13 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the second row in the matrix; that is M21, M22, M23
			/// </summary>
			public Vector3Df Row2
			{
				get { return new Vector3Df(M21, M22, M23); }
				set { M21 = value.X; M22 = value.Y; M23 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the third row in the matrix; that is M31, M32, M33
			/// </summary>
			public Vector3Df Row3
			{
				get { return new Vector3Df(M31, M32, M33); }
				set { M31 = value.X; M32 = value.Y; M33 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the first column in the matrix; that is M11, M21, M31
			/// </summary>
			public Vector3Df Column1
			{
				get { return new Vector3Df(M11, M21, M31); }
				set { M11 = value.X; M21 = value.Y; M31 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the second column in the matrix; that is M12, M22, M32
			/// </summary>
			public Vector3Df Column2
			{
				get { return new Vector3Df(M12, M22, M32); }
				set { M12 = value.X; M22 = value.Y; M32 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the third column in the matrix; that is M13, M23, M33
			/// </summary>
			public Vector3Df Column3
			{
				get { return new Vector3Df(M13, M23, M33); }
				set { M13 = value.X; M23 = value.Y; M33 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the scale of the matrix; that is M11, M22, and M33.
			/// </summary>
			public Vector3Df ScaleVector
			{
				get { return new Vector3Df(M11, M22, M33); }
				set { M11 = value.X; M22 = value.Y; M33 = value.Z; }
			}

			/// <summary>
			/// Gets a value indicating whether this instance is an identity Matrix3Dx3f.
			/// </summary>
			/// <value>
			/// <c>true</c> if this instance is an identity Matrix3Dx3f; otherwise, <c>false</c>.
			/// </value>
			public Boolean IsIdentity
			{
				get { return this.Equals(Identity); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx3f"/> struct
			/// </summary>
			/// <param name="value">The value that will be assigned to all components</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx3f(Single value)
			{
				M11 = M12 = M13 =
				M21 = M22 = M23 =
				M31 = M32 = M33 = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx3f"/> struct
			/// </summary>
			/// <param name="M11">The value to assign at row 1 column 1 of the matrix</param>
			/// <param name="M12">The value to assign at row 1 column 2 of the matrix</param>
			/// <param name="M13">The value to assign at row 1 column 3 of the matrix</param>
			/// <param name="M21">The value to assign at row 2 column 1 of the matrix</param>
			/// <param name="M22">The value to assign at row 2 column 2 of the matrix</param>
			/// <param name="M23">The value to assign at row 2 column 3 of the matrix</param>
			/// <param name="M31">The value to assign at row 3 column 1 of the matrix</param>
			/// <param name="M32">The value to assign at row 3 column 2 of the matrix</param>
			/// <param name="M33">The value to assign at row 3 column 3 of the matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx3f(Single M11, Single M12, Single M13,
								Single M21, Single M22, Single M23,
								Single M31, Single M32, Single M33)
			{
				this.M11 = M11; this.M12 = M12; this.M13 = M13;
				this.M21 = M21; this.M22 = M22; this.M23 = M23;
				this.M31 = M31; this.M32 = M32; this.M33 = M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx3f"/> struct
			/// </summary>
			/// <param name="values">The values to assign to the components of the matrix. This must be an array with sixteen elements</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx3f(Single[] values)
			{
				M11 = values[0];
				M12 = values[1];
				M13 = values[2];

				M21 = values[3];
				M22 = values[4];
				M23 = values[5];

				M31 = values[6];
				M32 = values[7];
				M33 = values[8];
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="System.Object"/> is equal to this instance
			/// </summary>
			/// <param name="obj">The <see cref="System.Object"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object obj)
			{
				if (!(obj is Matrix3Dx3f))
				{
					return false;
				}

				var matrix = (Matrix3Dx3f)obj;
				return Equals(in matrix);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="Matrix3Dx3f"/> is equal to this instance
			/// </summary>
			/// <param name="other">The <see cref="Matrix3Dx3f"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="Matrix3Dx3f"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(Matrix3Dx3f other)
			{
				return Equals(in other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="Matrix3Dx3f"/> is equal to this instance
			/// </summary>
			/// <param name="other">The <see cref="Matrix3Dx3f"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="Matrix3Dx3f"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(in Matrix3Dx3f other)
			{
				return XMath.Approximately(other.M11, M11) &&
					XMath.Approximately(other.M12, M12) &&
					XMath.Approximately(other.M13, M13) &&
					XMath.Approximately(other.M21, M21) &&
					XMath.Approximately(other.M22, M22) &&
					XMath.Approximately(other.M23, M23) &&
					XMath.Approximately(other.M31, M31) &&
					XMath.Approximately(other.M32, M32) &&
					XMath.Approximately(other.M33, M33);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Returns a hash code for this instance
			/// </summary>
			/// <returns>
			/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				unchecked
				{
					var hash_code = M11.GetHashCode();
					hash_code = (hash_code * 397) ^ M12.GetHashCode();
					hash_code = (hash_code * 397) ^ M13.GetHashCode();
					hash_code = (hash_code * 397) ^ M21.GetHashCode();
					hash_code = (hash_code * 397) ^ M22.GetHashCode();
					hash_code = (hash_code * 397) ^ M23.GetHashCode();
					hash_code = (hash_code * 397) ^ M31.GetHashCode();
					hash_code = (hash_code * 397) ^ M32.GetHashCode();
					hash_code = (hash_code * 397) ^ M33.GetHashCode();
					return hash_code;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return String.Format(CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2}] [M21:{3} M22:{4} M23:{5}] [M31:{6} M32:{7} M33:{8}]",
					M11, M12, M13, M21, M22, M23, M31, M32, M33);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="format">Формат отображения значения компонента</param>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public String ToString(String format)
			{
				if (format == null)
				{
					return ToString();
				}

				return String.Format(format, CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2}] [M21:{3} M22:{4} M23:{5}] [M31:{6} M32:{7} M33:{8}]",
					M11.ToString(format, CultureInfo.CurrentCulture), M12.ToString(format, CultureInfo.CurrentCulture), M13.ToString(format, CultureInfo.CurrentCulture),
					M21.ToString(format, CultureInfo.CurrentCulture), M22.ToString(format, CultureInfo.CurrentCulture), M23.ToString(format, CultureInfo.CurrentCulture),
					M31.ToString(format, CultureInfo.CurrentCulture), M32.ToString(format, CultureInfo.CurrentCulture), M33.ToString(format, CultureInfo.CurrentCulture));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="formatProvider">Интерфейс провайдера формата значения компонента</param>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//----------------------------------------------------------------------------------------------------------
			public String ToString(IFormatProvider formatProvider)
			{
				return String.Format(formatProvider, "[M11:{0} M12:{1} M13:{2}] [M21:{3} M22:{4} M23:{5}] [M31:{6} M32:{7} M33:{8}]",
					M11.ToString(formatProvider), M12.ToString(formatProvider), M13.ToString(formatProvider),
					M21.ToString(formatProvider), M22.ToString(formatProvider), M23.ToString(formatProvider),
					M31.ToString(formatProvider), M32.ToString(formatProvider), M33.ToString(formatProvider));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="format">Формат отображения значения компонента</param>
			/// <param name="formatProvider">Интерфейс провайдера формата значения компонента</param>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//----------------------------------------------------------------------------------------------------------
			public String ToString(String format, IFormatProvider formatProvider)
			{
				if (format == null)
				{
					return ToString(formatProvider);
				}

				return String.Format(format, formatProvider, "[M11:{0} M12:{1} M13:{2}] [M21:{3} M22:{4} M23:{5}] [M31:{6} M32:{7} M33:{8}]",
					M11.ToString(format, formatProvider), M12.ToString(format, formatProvider), M13.ToString(format, formatProvider),
					M21.ToString(format, formatProvider), M22.ToString(format, formatProvider), M23.ToString(format, formatProvider),
					M31.ToString(format, formatProvider), M32.ToString(format, formatProvider), M33.ToString(format, formatProvider));
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Adds two matrices
			/// </summary>
			/// <param name="left">The first matrix to add</param>
			/// <param name="right">The second matrix to add</param>
			/// <returns>The sum of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator +(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Add(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Assert a matrix (return it unchanged)
			/// </summary>
			/// <param name="value">The matrix to assert (unchanged)</param>
			/// <returns>The asserted (unchanged) Matrix3Dx3f</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator +(Matrix3Dx3f value)
			{
				return value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Subtracts two matrices
			/// </summary>
			/// <param name="left">The first matrix to subtract</param>
			/// <param name="right">The second matrix to subtract</param>
			/// <returns>The difference between the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator -(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Subtract(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to negate</param>
			/// <returns>The negated matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator -(Matrix3Dx3f value)
			{
				Matrix3Dx3f result;
				Negate(in value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by a given value
			/// </summary>
			/// <param name="right">The matrix to scale</param>
			/// <param name="left">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator *(Single left, Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Multiply(in right, left, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by a given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator *(Matrix3Dx3f left, Single right)
			{
				Matrix3Dx3f result;
				Multiply(in left, right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Multiplies two matrices
			/// </summary>
			/// <param name="left">The first matrix to multiply</param>
			/// <param name="right">The second matrix to multiply</param>
			/// <returns>The product of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator *(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Multiply(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by a given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator /(Matrix3Dx3f left, Single right)
			{
				Matrix3Dx3f result;
				Divide(in left, right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Divides two matrices
			/// </summary>
			/// <param name="left">The first matrix to divide</param>
			/// <param name="right">The second matrix to divide</param>
			/// <returns>The quotient of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx3f operator /(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				Matrix3Dx3f result;
				Divide(in left, in right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Tests for equality between two objects
			/// </summary>
			/// <param name="left">The first value to compare</param>
			/// <param name="right">The second value to compare</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c></returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				return left.Equals(in right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Tests for inequality between two objects
			/// </summary>
			/// <param name="left">The first value to compare</param>
			/// <param name="right">The second value to compare</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c></returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Matrix3Dx3f left, Matrix3Dx3f right)
			{
				return !left.Equals(in right);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа матрицы трансформации WPF
			/// </summary>
			/// <param name="matrix">Двухмерная матрица размерностью 3х2</param>
			/// <returns>Матрица трансформации WPF</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator System.Windows.Media.Media3D.Matrix3D(Matrix3Dx3f matrix)
			{
				return (new System.Windows.Media.Media3D.Matrix3D
				{
					M11 = matrix.M11,
					M12 = matrix.M12,
					M13 = matrix.M13,
					M21 = matrix.M21,
					M22 = matrix.M22,
					M23 = matrix.M23,
					OffsetX = matrix.M31,
					OffsetY = matrix.M32,
					OffsetZ = matrix.M33
				});
			}
#endif
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Gets or sets the component at the specified index
			/// </summary>
			/// <value>The value of the matrix component, depending on the index</value>
			/// <param name="index">The zero-based index of the component to access</param>
			/// <returns>The value of the component at the specified index</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single this[Int32 index]
			{
				get
				{
					switch (index)
					{
						case 0: return M11;
						case 1: return M12;
						case 2: return M13;
						case 3: return M21;
						case 4: return M22;
						case 5: return M23;
						case 6: return M31;
						case 7: return M32;
						case 8: return M33;
						default: return 0;
					}
				}

				set
				{
					switch (index)
					{
						case 0: M11 = value; break;
						case 1: M12 = value; break;
						case 2: M13 = value; break;
						case 3: M21 = value; break;
						case 4: M22 = value; break;
						case 5: M23 = value; break;
						case 6: M31 = value; break;
						case 7: M32 = value; break;
						case 8: M33 = value; break;
						default: throw new ArgumentOutOfRangeException(nameof(index), "Indices for Matrix3Dx3f run from 0 to 8, inclusive.");
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Gets or sets the component at the specified index
			/// </summary>
			/// <value>The value of the matrix component, depending on the index</value>
			/// <param name="row">The row of the matrix to access</param>
			/// <param name="column">The column of the matrix to access</param>
			/// <returns>The value of the component at the specified index</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single this[Int32 row, Int32 column]
			{
				get
				{
					return this[(row * 3) + column];
				}

				set
				{
					this[(row * 3) + column] = value;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the determinant of the matrix
			/// </summary>
			/// <returns>The determinant of the matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single Determinant()
			{
				return (M11 * M22 * M33) + (M12 * M23 * M31) + (M13 * M21 * M32) - (M13 * M22 * M31) - (M12 * M21 * M33) - (M11 * M23 * M32);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Inverts the matrix
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Invert()
			{
				Invert(in this, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Transposes the matrix
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Transpose()
			{
				Transpose(in this, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthogonalizes the specified matrix
			/// </summary>
			/// <remarks>
			/// <para>
			/// Orthogonalization is the process of making all rows orthogonal to each other. This
			/// means that any given row in the matrix will be orthogonal to any other given row in the
			/// Matrix3Dx3f
			/// </para>
			/// <para>Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.
			/// </para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output
			/// </para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void Orthogonalize()
			{
				Orthogonalize(in this, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Orthonormalizes the specified matrix
			/// </summary>
			/// <remarks>
			/// <para>Orthonormalization is the process of making all rows and columns orthogonal to each
			/// other and making all rows and columns of unit length. This means that any given row will
			/// be orthogonal to any other given row and any given column will be orthogonal to any other
			/// given column. Any given row will not be orthogonal to any given column. Every row and every
			/// column will be of unit length.</para>
			/// <para>Because this method uses the modified Gram-Schmidt process, the resulting Matrix3Dx3f
			/// tends to be numerically unstable. The numeric stability decreases according to the rows
			/// so that the first row is the most stable and the last row is the least stable.</para>
			/// <para>This operation is performed on the rows of the matrix rather than the columns.
			/// If you wish for this operation to be performed on the columns, first transpose the
			/// input and than transpose the output</para>
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void Orthonormalize()
			{
				Orthonormalize(in this, out this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Decomposes a matrix into an orthonormalized Matrix3Dx3f Q and a right triangular Matrix3Dx3f R
			/// </summary>
			/// <param name="Q">When the method completes, contains the orthonormalized Matrix3Dx3f of the decomposition</param>
			/// <param name="R">When the method completes, contains the right triangular Matrix3Dx3f of the decomposition</param>
			//---------------------------------------------------------------------------------------------------------
			public void DecomposeQR(out Matrix3Dx3f Q, out Matrix3Dx3f R)
			{
				Matrix3Dx3f temp = this;
				temp.Transpose();
				Orthonormalize(in temp, out Q);
				Q.Transpose();

				R = new Matrix3Dx3f();
				R.M11 = Vector3Df.Dot(Q.Column1, Column1);
				R.M12 = Vector3Df.Dot(Q.Column1, Column2);
				R.M13 = Vector3Df.Dot(Q.Column1, Column3);

				R.M22 = Vector3Df.Dot(Q.Column2, Column2);
				R.M23 = Vector3Df.Dot(Q.Column2, Column3);

				R.M33 = Vector3Df.Dot(Q.Column3, Column3);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Decomposes a matrix into a lower triangular Matrix3Dx3f L and an orthonormalized Matrix3Dx3f Q
			/// </summary>
			/// <param name="L">When the method completes, contains the lower triangular Matrix3Dx3f of the decomposition</param>
			/// <param name="Q">When the method completes, contains the orthonormalized Matrix3Dx3f of the decomposition</param>
			//---------------------------------------------------------------------------------------------------------
			public void DecomposeLQ(out Matrix3Dx3f L, out Matrix3Dx3f Q)
			{
				Orthonormalize(in this, out Q);

				L = new Matrix3Dx3f();
				L.M11 = Vector3Df.Dot(Q.Row1, Row1);

				L.M21 = Vector3Df.Dot(Q.Row1, Row2);
				L.M22 = Vector3Df.Dot(Q.Row2, Row2);

				L.M31 = Vector3Df.Dot(Q.Row1, Row3);
				L.M32 = Vector3Df.Dot(Q.Row2, Row3);
				L.M33 = Vector3Df.Dot(Q.Row3, Row3);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Decomposes a matrix into a scale, rotation, and translation
			/// </summary>
			/// <remarks>
			/// This method is designed to decompose an SRT transformation matrix only
			/// </remarks>
			/// <param name="scale">When the method completes, contains the scaling component of the decomposed matrix</param>
			/// <param name="rotation">When the method completes, contains the rotation component of the decomposed matrix</param>
			/// <returns>Статус успешности</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Decompose(out Vector3Df scale, out Quaternion3Df rotation)
			{
				//Source: Unknown
				//References: http://www.gamedev.net/community/forums/topic.asp?topic_id=441695

				//Scaling is the length of the rows.
				scale.X = (Single)Math.Sqrt((M11 * M11) + (M12 * M12) + (M13 * M13));
				scale.Y = (Single)Math.Sqrt((M21 * M21) + (M22 * M22) + (M23 * M23));
				scale.Z = (Single)Math.Sqrt((M31 * M31) + (M32 * M32) + (M33 * M33));

				//If any of the scaling factors are zero, than the rotation matrix can not exist.
				if (XMath.IsZero(scale.X) ||
					XMath.IsZero(scale.Y) ||
					XMath.IsZero(scale.Z))
				{
					rotation = Quaternion3Df.Identity;
					return false;
				}

				//The rotation is the left over Matrix3Dx3f after dividing out the scaling.
				var rotationMatrix3x3 = new Matrix3Dx3f();
				rotationMatrix3x3.M11 = M11 / scale.X;
				rotationMatrix3x3.M12 = M12 / scale.X;
				rotationMatrix3x3.M13 = M13 / scale.X;

				rotationMatrix3x3.M21 = M21 / scale.Y;
				rotationMatrix3x3.M22 = M22 / scale.Y;
				rotationMatrix3x3.M23 = M23 / scale.Y;

				rotationMatrix3x3.M31 = M31 / scale.Z;
				rotationMatrix3x3.M32 = M32 / scale.Z;
				rotationMatrix3x3.M33 = M33 / scale.Z;

				Quaternion3Df.RotationMatrix(in rotationMatrix3x3, out rotation);
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Decomposes a uniform scale matrix into a scale, rotation, and translation
			/// A uniform scale matrix has the same scale in every axis
			/// </summary>
			/// <remarks>
			/// This method is designed to decompose only an SRT transformation matrix that has the same scale in every axis
			/// </remarks>
			/// <param name="scale">When the method completes, contains the scaling component of the decomposed matrix</param>
			/// <param name="rotation">When the method completes, contains the rotation component of the decomposed matrix</param>
			/// <returns>Статус успешности</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean DecomposeUniformScale(out Single scale, out Quaternion3Df rotation)
			{
				//Scaling is the length of the rows. ( just take one row since this is a uniform matrix)
				scale = (Single)Math.Sqrt((M11 * M11) + (M12 * M12) + (M13 * M13));
				var inv_scale = 1f / scale;

				//If any of the scaling factors are zero, then the rotation matrix can not exist.
				if (Math.Abs(scale) < XMath.ZeroTolerance_F)
				{
					rotation = Quaternion3Df.Identity;
					return false;
				}

				//The rotation is the left over matrix after dividing out the scaling.
				var rotationmatrix = new Matrix3Dx3f();
				rotationmatrix.M11 = M11 * inv_scale;
				rotationmatrix.M12 = M12 * inv_scale;
				rotationmatrix.M13 = M13 * inv_scale;

				rotationmatrix.M21 = M21 * inv_scale;
				rotationmatrix.M22 = M22 * inv_scale;
				rotationmatrix.M23 = M23 * inv_scale;

				rotationmatrix.M31 = M31 * inv_scale;
				rotationmatrix.M32 = M32 * inv_scale;
				rotationmatrix.M33 = M33 * inv_scale;

				Quaternion3Df.RotationMatrix(in rotationmatrix, out rotation);

				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Exchanges two rows in the matrix
			/// </summary>
			/// <param name="firstRow">The first row to exchange. This is an index of the row starting at zero</param>
			/// <param name="secondRow">The second row to exchange. This is an index of the row starting at zero</param>
			//---------------------------------------------------------------------------------------------------------
			public void ExchangeRows(Int32 firstRow, Int32 secondRow)
			{
				if (firstRow == secondRow)
					return;

				var temp0 = this[secondRow, 0];
				var temp1 = this[secondRow, 1];
				var temp2 = this[secondRow, 2];

				this[secondRow, 0] = this[firstRow, 0];
				this[secondRow, 1] = this[firstRow, 1];
				this[secondRow, 2] = this[firstRow, 2];

				this[firstRow, 0] = temp0;
				this[firstRow, 1] = temp1;
				this[firstRow, 2] = temp2;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Exchanges two columns in the matrix
			/// </summary>
			/// <param name="firstColumn">The first column to exchange. This is an index of the column starting at zero</param>
			/// <param name="secondColumn">The second column to exchange. This is an index of the column starting at zero</param>
			//---------------------------------------------------------------------------------------------------------
			public void ExchangeColumns(Int32 firstColumn, Int32 secondColumn)
			{
				if (firstColumn == secondColumn)
					return;

				var temp0 = this[0, secondColumn];
				var temp1 = this[1, secondColumn];
				var temp2 = this[2, secondColumn];

				this[0, secondColumn] = this[0, firstColumn];
				this[1, secondColumn] = this[1, firstColumn];
				this[2, secondColumn] = this[2, firstColumn];

				this[0, firstColumn] = temp0;
				this[1, firstColumn] = temp1;
				this[2, firstColumn] = temp2;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates an array containing the elements of the matrix
			/// </summary>
			/// <returns>A 9-element array containing the components of the matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single[] ToArray()
			{
				return new[] { M11, M12, M13, M21, M22, M23, M31, M32, M33 };
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================