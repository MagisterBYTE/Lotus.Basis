//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 3D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry3DMatrix4x4.cs
*		Четырехмерная матрицы размерностью 4х4.
*		Реализация четырехмерной матрицы для реализации всевозможных трансформаций объекта в трехмерном пространстве.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
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
		/// Четырехмерная матрицы размерностью 4х4
		/// </summary>
		/// <remarks>
		/// Реализация четырехмерной матрицы для реализации всевозможных трансформаций объекта в трехмерном пространстве
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Matrix4Dx4 : IEquatable<Matrix4Dx4>, IComparable<Matrix4Dx4>, ICloneable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичная матрица
			/// </summary>
			public static readonly Matrix4Dx4 Identity = new Matrix4Dx4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Матричное произведение
			/// </summary>
			/// <param name="left">Левая матрица</param>
			/// <param name="right">Правая матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Multiply(in Matrix4Dx4 left, in Matrix4Dx4 right, out Matrix4Dx4 result)
			{
				result.M11 = (left.M11 * right.M11) + (left.M21 * right.M12) + (left.M31 * right.M13) + (left.M41 * right.M14);
				result.M12 = (left.M12 * right.M11) + (left.M22 * right.M12) + (left.M32 * right.M13) + (left.M42 * right.M14);
				result.M13 = (left.M13 * right.M11) + (left.M23 * right.M12) + (left.M33 * right.M13) + (left.M43 * right.M14);
				result.M14 = (left.M14 * right.M11) + (left.M24 * right.M12) + (left.M34 * right.M13) + (left.M44 * right.M14);

				result.M21 = (left.M11 * right.M21) + (left.M21 * right.M22) + (left.M31 * right.M23) + (left.M41 * right.M24);
				result.M22 = (left.M12 * right.M21) + (left.M22 * right.M22) + (left.M32 * right.M23) + (left.M42 * right.M24);
				result.M23 = (left.M13 * right.M21) + (left.M23 * right.M22) + (left.M33 * right.M23) + (left.M43 * right.M24);
				result.M24 = (left.M14 * right.M21) + (left.M24 * right.M22) + (left.M34 * right.M23) + (left.M44 * right.M24);

				result.M31 = (left.M11 * right.M31) + (left.M21 * right.M32) + (left.M31 * right.M33) + (left.M41 * right.M34);
				result.M32 = (left.M12 * right.M31) + (left.M22 * right.M32) + (left.M32 * right.M33) + (left.M42 * right.M34);
				result.M33 = (left.M13 * right.M31) + (left.M23 * right.M32) + (left.M33 * right.M33) + (left.M43 * right.M34);
				result.M34 = (left.M14 * right.M31) + (left.M24 * right.M32) + (left.M34 * right.M33) + (left.M44 * right.M34);

				result.M41 = (left.M11 * right.M41) + (left.M21 * right.M42) + (left.M31 * right.M43) + (left.M41 * right.M44);
				result.M42 = (left.M12 * right.M41) + (left.M22 * right.M42) + (left.M32 * right.M43) + (left.M42 * right.M44);
				result.M43 = (left.M13 * right.M41) + (left.M23 * right.M42) + (left.M33 * right.M43) + (left.M43 * right.M44);
				result.M44 = (left.M14 * right.M41) + (left.M24 * right.M42) + (left.M34 * right.M43) + (left.M44 * right.M44);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инверсия матрицы
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			/// <returns>Статус успешности инверсии</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Inverse(in Matrix4Dx4 matrix, out Matrix4Dx4 result)
			{
				Double det, oodet;

				result.M11 = Matrix3Dx3.Determinat(matrix.M22, matrix.M23, matrix.M24, matrix.M32, matrix.M33, matrix.M34, matrix.M42, matrix.M43, matrix.M44);
				result.M12 = -Matrix3Dx3.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M32, matrix.M33, matrix.M34, matrix.M42, matrix.M43, matrix.M44);
				result.M13 = Matrix3Dx3.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M22, matrix.M23, matrix.M24, matrix.M42, matrix.M43, matrix.M44);
				result.M14 = -Matrix3Dx3.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M22, matrix.M23, matrix.M24, matrix.M32, matrix.M33, matrix.M34);

				result.M21 = -Matrix3Dx3.Determinat(matrix.M21, matrix.M23, matrix.M24, matrix.M31, matrix.M33, matrix.M34, matrix.M41, matrix.M43, matrix.M44);
				result.M22 = Matrix3Dx3.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M31, matrix.M33, matrix.M34, matrix.M41, matrix.M43, matrix.M44);
				result.M23 = -Matrix3Dx3.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M21, matrix.M23, matrix.M24, matrix.M41, matrix.M43, matrix.M44);
				result.M24 = Matrix3Dx3.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M21, matrix.M23, matrix.M24, matrix.M31, matrix.M33, matrix.M34);

				result.M31 = Matrix3Dx3.Determinat(matrix.M21, matrix.M22, matrix.M24, matrix.M31, matrix.M32, matrix.M34, matrix.M41, matrix.M42, matrix.M44);
				result.M32 = -Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M31, matrix.M32, matrix.M34, matrix.M41, matrix.M42, matrix.M44);
				result.M33 = Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M21, matrix.M22, matrix.M24, matrix.M41, matrix.M42, matrix.M44);
				result.M34 = -Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M21, matrix.M22, matrix.M24, matrix.M31, matrix.M32, matrix.M34);

				result.M41 = -Matrix3Dx3.Determinat(matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);
				result.M42 = Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);
				result.M43 = -Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M41, matrix.M42, matrix.M43);
				result.M44 = Matrix3Dx3.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33);

				det = (matrix.M11 * result.M11) + (matrix.M21 * result.M12) + (matrix.M31 * result.M13) + (matrix.M41 * result.M14);

				if (XMath.Approximately(det, 0))
				{
					result = Matrix4Dx4.Identity;

					return false;
				}
				else
				{

					oodet = 1.0 / det;

					result.M11 *= oodet;
					result.M12 *= oodet;
					result.M13 *= oodet;
					result.M14 *= oodet;

					result.M21 *= oodet;
					result.M22 *= oodet;
					result.M23 *= oodet;
					result.M24 *= oodet;

					result.M31 *= oodet;
					result.M32 *= oodet;
					result.M33 *= oodet;
					result.M34 *= oodet;

					result.M41 *= oodet;
					result.M42 *= oodet;
					result.M43 *= oodet;
					result.M44 *= oodet;

					return true;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инверсия матрицы содержащей трансформации вращения
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void InverseRotation(in Matrix4Dx4 matrix, out Matrix4Dx4 result)
			{
				result.M11 = matrix.M11;
				result.M12 = matrix.M21;
				result.M13 = matrix.M31;
				result.M14 = matrix.M14;

				result.M21 = matrix.M12;
				result.M22 = matrix.M22;
				result.M23 = matrix.M32;
				result.M24 = matrix.M24;

				result.M31 = matrix.M13;
				result.M32 = matrix.M23;
				result.M33 = matrix.M33;
				result.M34 = matrix.M34;

				result.M41 = -((matrix.M11 * matrix.M41) + (matrix.M12 * matrix.M42) + (matrix.M13 * matrix.M43));
				result.M42 = -((matrix.M21 * matrix.M41) + (matrix.M22 * matrix.M42) + (matrix.M23 * matrix.M43));
				result.M43 = -((matrix.M31 * matrix.M41) + (matrix.M32 * matrix.M42) + (matrix.M33 * matrix.M43));
				result.M44 = matrix.M44;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Транспонирование матрицы
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Transpose(in Matrix4Dx4 matrix, out Matrix4Dx4 result)
			{
				result.M11 = matrix.M11;
				result.M21 = matrix.M12;
				result.M31 = matrix.M13;
				result.M41 = matrix.M14;

				result.M12 = matrix.M21;
				result.M22 = matrix.M22;
				result.M32 = matrix.M23;
				result.M42 = matrix.M24;

				result.M13 = matrix.M31;
				result.M23 = matrix.M32;
				result.M33 = matrix.M33;
				result.M43 = matrix.M34;

				result.M14 = matrix.M41;
				result.M24 = matrix.M42;
				result.M34 = matrix.M43;
				result.M44 = matrix.M44;
			}
			#endregion

			#region ======================================= МЕТОДЫ ТРАНСФОРМАЦИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси X
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationX(Double angle, ref Matrix4Dx4 result)
			{
				var ct = Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = 1;
				result.M21 = 0;
				result.M31 = 0;

				result.M12 = 0;
				result.M22 = ct;
				result.M32 = -st;

				result.M13 = 0;
				result.M23 = st;
				result.M33 = ct;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси Y
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationY(Double angle, ref Matrix4Dx4 result)
			{
				var ct = Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = ct;
				result.M21 = 0;
				result.M31 = st;

				result.M12 = 0;
				result.M22 = 1;
				result.M32 = 0;

				result.M13 = -st;
				result.M23 = 0;
				result.M33 = ct;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси Z
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationZ(Double angle, ref Matrix4Dx4 result)
			{
				var ct = Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = ct;
				result.M21 = -st;
				result.M31 = 0;

				result.M12 = st;
				result.M22 = ct;
				result.M32 = 0;

				result.M13 = 0;
				result.M23 = 0;
				result.M33 = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="axis">Ось, вектор должен быть единичным</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromAxis(Double angle, in Vector3D axis, ref Matrix4Dx4 result)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromQuaternion(in Quaternion3D quaternion, ref Matrix4Dx4 result)
			{
				Double wx, wy, wz, xx, yy, yz, xy, xz, zz, x2, y2, z2;

				var sc = 2.0 / quaternion.Length;

				x2 = quaternion.X * sc; y2 = quaternion.Y * sc; z2 = quaternion.Z * sc;
				xx = quaternion.X * x2; xy = quaternion.X * y2; xz = quaternion.X * z2;
				yy = quaternion.Y * y2; yz = quaternion.Y * z2; zz = quaternion.Z * z2;
				wx = quaternion.W * x2; wy = quaternion.W * y2; wz = quaternion.W * z2;

				result.M11 = 1.0 - (yy + zz);
				result.M21 = xy - wz;
				result.M31 = xz + wy;

				result.M12 = xy + wz;
				result.M22 = 1.0 - (xx + zz);
				result.M32 = yz - wx;

				result.M13 = xz - wy;
				result.M23 = yz + wx;
				result.M33 = 1.0 - (xx + yy);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="matrix">Исходная матрица трансформации</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromMatrix(in Matrix3Dx3 matrix, ref Matrix4Dx4 result)
			{
				// copy the 3x3 rotation block
				result.M11 = matrix.M11; result.M12 = matrix.M12; result.M13 = matrix.M13;
				result.M21 = matrix.M21; result.M22 = matrix.M22; result.M23 = matrix.M23;
				result.M31 = matrix.M31; result.M32 = matrix.M32; result.M33 = matrix.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы перемещения
			/// </summary>
			/// <param name="position">Позиция</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Translation(in Vector3D position, ref Matrix4Dx4 result)
			{
				result.M41 = position.X;
				result.M42 = position.Y;
				result.M43 = position.Z;
			}

			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ 3D КАМЕРЫ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление пирамиды видимости
			/// </summary>
			/// <param name="left">Левый край</param>
			/// <param name="right">Правый край</param>
			/// <param name="bottom">Верхний край</param>
			/// <param name="top">Нижний край</param>
			/// <param name="near">Ближняя плоскость отсечения</param>
			/// <param name="far">Дальняя плоскость отсечения</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PerspectiveFrustum(Double left, Double right, Double bottom, Double top, Double near,
				Double far, out Matrix4Dx4 result)
			{
				result.M11 = 2.0 * near / (right - left);
				result.M12 = 0.0;
				result.M13 = 0.0;
				result.M14 = 0.0;

				result.M21 = 0.0;
				result.M22 = 2.0 * near / (top - bottom);
				result.M23 = 0.0;
				result.M24 = 0.0;

				result.M31 = (right + left) / (right - left);
				result.M32 = (top + bottom) / (top - bottom);
				result.M33 = -(far + near) / (far - near);
				result.M34 = 1.0;

				result.M41 = 0.0;
				result.M42 = 0.0;
				result.M43 = -(2.0 * far * near) / (far - near);
				result.M44 = 0.0;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			public Double M11, M12, M13, M14;
			public Double M21, M22, M23, M24;
			public Double M31, M32, M33, M34;
			public Double M41, M42, M43, M44;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Инверсная матрица
			/// </summary>
			public readonly Matrix4Dx4 Inversed
			{
				get
				{
					Matrix4Dx4 result;

					Matrix4Dx4.Inverse(in this, out result);

					return result;
				}
			}

			/// <summary>
			/// Транспонированная матрица
			/// </summary>
			public readonly Matrix4Dx4 Transposed
			{
				get
				{
					Matrix4Dx4 result;

					Matrix4Dx4.Transpose(in this, out result);

					return result;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу соответствующим компонентами
			/// </summary>
			/// <param name="m11">Компонент с позицией 11</param>
			/// <param name="m12">Компонент с позицией 12</param>
			/// <param name="m13">Компонент с позицией 13</param>
			/// <param name="m14">Компонент с позицией 14</param>
			/// <param name="m21">Компонент с позицией 21</param>
			/// <param name="m22">Компонент с позицией 22</param>
			/// <param name="m23">Компонент с позицией 23</param>
			/// <param name="m24">Компонент с позицией 24</param>
			/// <param name="m31">Компонент с позицией 31</param>
			/// <param name="m32">Компонент с позицией 32</param>
			/// <param name="m33">Компонент с позицией 33</param>
			/// <param name="m34">Компонент с позицией 34</param>
			/// <param name="m41">Компонент с позицией 41</param>
			/// <param name="m42">Компонент с позицией 43</param>
			/// <param name="m43">Компонент с позицией 43</param>
			/// <param name="m44">Компонент с позицией 44</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix4Dx4(Double m11, Double m12, Double m13, Double m14,
							Double m21, Double m22, Double m23, Double m24,
							Double m31, Double m32, Double m33, Double m34,
							Double m41, Double m42, Double m43, Double m44)
			{
				M11 = m11; M12 = m12; M13 = m13; M14 = m14;
				M21 = m21; M22 = m22; M23 = m23; M24 = m24;
				M31 = m31; M32 = m32; M33 = m33; M34 = m34;
				M41 = m41; M42 = m42; M43 = m43; M44 = m44;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу указанной матрицей
			/// </summary>
			/// <param name="source">Матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix4Dx4(Matrix4Dx4 source)
			{
				M11 = source.M11; M12 = source.M12; M13 = source.M13; M14 = source.M14;
				M21 = source.M21; M22 = source.M22; M23 = source.M23; M24 = source.M24;
				M31 = source.M31; M32 = source.M32; M33 = source.M33; M34 = source.M34;
				M41 = source.M41; M42 = source.M42; M43 = source.M43; M44 = source.M44;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка идентичности матриц
			/// </summary>
			/// <param name="obj">Сравниваемая матрица</param>
			/// <returns>Статус идентичности матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Boolean Equals(System.Object? obj)
			{
				if (obj != null)
				{
					if (obj is Matrix4Dx4 matrix)
					{
						return Equals(matrix);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка идентичности матриц
			/// </summary>
			/// <param name="other">Сравниваемая матрица</param>
			/// <returns>Статус идентичности матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Boolean Equals(Matrix4Dx4 other)
			{
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода матрицы
			/// </summary>
			/// <returns>Хеш-код матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Int32 GetHashCode()
			{
				return M11.GetHashCode() ^ M22.GetHashCode() ^ M33.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение матриц для упорядочивания
			/// </summary>
			/// <param name="other">Матрица</param>
			/// <returns>Статус сравнения матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Int32 CompareTo(Matrix4Dx4 other)
			{
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование матрицы
			/// </summary>
			/// <returns>Копия матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление матрицы с указанием значений компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly String ToString()
			{
				return "M11 = " + M11.ToString("F3") + "; M22 = " + M22.ToString("F3") +
					   "; M33 = " + M33.ToString("F3");
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на равенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус равенства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(Matrix4Dx4 left, Matrix4Dx4 right)
			{
				return left.Equals(right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на неравенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус неравенство</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Matrix4Dx4 left, Matrix4Dx4 right)
			{
				return !(left == right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение матриц
			/// </summary>
			/// <param name="left">Первая матрица</param>
			/// <param name="right">Вторая матрица</param>
			/// <returns>Сумма матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4 operator +(Matrix4Dx4 left, Matrix4Dx4 right)
			{
				Matrix4Dx4 result;

				result.M11 = left.M11 + right.M11; result.M12 = left.M12 + right.M12; result.M13 = left.M13 + right.M13; result.M14 = left.M14 + right.M14;
				result.M21 = left.M21 + right.M21; result.M22 = left.M22 + right.M22; result.M23 = left.M23 + right.M23; result.M24 = left.M24 + right.M24;
				result.M31 = left.M31 + right.M31; result.M32 = left.M32 + right.M32; result.M33 = left.M33 + right.M33; result.M34 = left.M34 + right.M34;
				result.M41 = left.M41 + right.M41; result.M42 = left.M42 + right.M42; result.M43 = left.M43 + right.M43; result.M44 = left.M44 + right.M44;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание матриц
			/// </summary>
			/// <param name="left">Первая матрица</param>
			/// <param name="right">Вторая матрица</param>
			/// <returns>Разность матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4 operator -(Matrix4Dx4 left, Matrix4Dx4 right)
			{
				Matrix4Dx4 result;

				result.M11 = left.M11 - right.M11; result.M12 = left.M12 - right.M12; result.M13 = left.M13 - right.M13; result.M14 = left.M14 - right.M14;
				result.M21 = left.M21 - right.M21; result.M22 = left.M22 - right.M22; result.M23 = left.M23 - right.M23; result.M24 = left.M24 - right.M24;
				result.M31 = left.M31 - right.M31; result.M32 = left.M32 - right.M32; result.M33 = left.M33 - right.M33; result.M34 = left.M34 - right.M34;
				result.M41 = left.M41 - right.M41; result.M42 = left.M42 - right.M42; result.M43 = left.M43 - right.M43; result.M44 = left.M44 - right.M44;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение матрицы на скаляр
			/// </summary>
			/// <param name="matrix">Матрица</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированная матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4 operator *(Matrix4Dx4 matrix, Double scalar)
			{
				Matrix4Dx4 result;

				result.M11 = matrix.M11 * scalar; result.M12 = matrix.M12 * scalar; result.M13 = matrix.M13 * scalar; result.M14 = matrix.M14 * scalar;
				result.M21 = matrix.M21 * scalar; result.M22 = matrix.M22 * scalar; result.M23 = matrix.M23 * scalar; result.M24 = matrix.M24 * scalar;
				result.M31 = matrix.M31 * scalar; result.M32 = matrix.M32 * scalar; result.M33 = matrix.M33 * scalar; result.M34 = matrix.M34 * scalar;
				result.M41 = matrix.M41 * scalar; result.M42 = matrix.M42 * scalar; result.M43 = matrix.M43 * scalar; result.M44 = matrix.M44 * scalar;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Деление матрицы на скаляр
			/// </summary>
			/// <param name="matrix">Матрица</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированная матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4 operator /(Matrix4Dx4 matrix, Double scalar)
			{
				Matrix4Dx4 result;

				scalar = 1 / scalar;

				result.M11 = matrix.M11 * scalar; result.M12 = matrix.M12 * scalar; result.M13 = matrix.M13 * scalar; result.M14 = matrix.M14 * scalar;
				result.M21 = matrix.M21 * scalar; result.M22 = matrix.M22 * scalar; result.M23 = matrix.M23 * scalar; result.M24 = matrix.M24 * scalar;
				result.M31 = matrix.M31 * scalar; result.M32 = matrix.M32 * scalar; result.M33 = matrix.M33 * scalar; result.M34 = matrix.M34 * scalar;
				result.M41 = matrix.M41 * scalar; result.M42 = matrix.M42 * scalar; result.M43 = matrix.M43 * scalar; result.M44 = matrix.M44 * scalar;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение матрицы на матрицу
			/// </summary>
			/// <param name="left">Левая матрица</param>
			/// <param name="right">Правая матрица</param>
			/// <returns>Результирующая матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4 operator *(Matrix4Dx4 left, Matrix4Dx4 right)
			{
				Matrix4Dx4 result;

				Multiply(in left, in right, out result);

				return result;
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация строк матрицы на основе индекса
			/// </summary>
			/// <param name="index">Индекс строки матрицы</param>
			/// <returns>Строка матрицы в виде четырехмерного вектора</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector4D this[Int32 index]
			{
				readonly get
				{
					switch (index)
					{
						case 0:
							return new Vector4D(M11, M12, M13, M14);
						case 1:
							return new Vector4D(M21, M22, M23, M24);
						case 2:
							return new Vector4D(M31, M32, M33, M34);
						default:
							return new Vector4D(M41, M42, M43, M44);
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							{
								M11 = value.X;
								M12 = value.Y;
								M13 = value.Z;
								M14 = value.W;
							}
							break;
						case 1:
							{
								M21 = value.X;
								M22 = value.Y;
								M23 = value.Z;
								M24 = value.W;
							}
							break;
						case 2:
							{
								M31 = value.X;
								M32 = value.Y;
								M33 = value.Z;
								M34 = value.W;
							}
							break;
						default:
							{
								M41 = value.X;
								M42 = value.Y;
								M43 = value.Z;
								M44 = value.W;
							}
							break;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в кватернион
			/// </summary>
			/// <returns>Кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Quaternion3D ToQuaternion3D()
			{
				var tr = M11 + M22 + M33;
				if (tr > 0.0)
				{
					return new Quaternion3D(M23 - M32, M31 - M13, M12 - M21, tr + 1.0);
				}
				else
				{
					if (M11 > M22 && M11 > M33)
					{
						return new Quaternion3D(1.0 + M11 - M22 - M33, M21 + M12, M31 + M13, M23 - M32);
					}
					else
					{
						if (M22 > M33)
						{
							return new Quaternion3D(M21 + M12, 1.0 + M22 - M11 - M33, M32 + M23, M31 - M13);
						}
						else
						{
							return new Quaternion3D(M31 + M13, M32 + M23, 1.0 + M33 - M11 - M22, M12 - M21);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в кватернион
			/// </summary>
			/// <returns>Кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Quaternion3Df ToQuaternion3Df()
			{
				var tr = (Single)(M11 + M22 + M33);
				if (tr > 0.0)
				{
					return new Quaternion3Df((Single)(M23 - M32), (Single)(M31 - M13), (Single)(M12 - M21), tr + 1.0f);
				}
				else
				{
					if (M11 > M22 && M11 > M33)
					{
						return new Quaternion3Df((Single)(1.0 + M11 - M22 - M33), (Single)(M21 + M12), (Single)(M31 + M13), (Single)(M23 - M32));
					}
					else
					{
						if (M22 > M33)
						{
							return new Quaternion3Df((Single)(M21 + M12), (Single)(1.0 + M22 - M11 - M33), 
								(Single)(M32 + M23), (Single)(M31 - M13));
						}
						else
						{
							return new Quaternion3Df((Single)(M31 + M13), (Single)(M32 + M23), 
								(Single)(1.0 + M33 - M11 - M22), (Single)(M12 - M21));
						}
					}
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Четырехмерная матрицы размерностью 4х4
		/// </summary>
		/// <remarks>
		/// Реализация четырехмерной матрицы для реализации всевозможных трансформаций объекта в трехмерном пространстве
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public struct Matrix4Dx4f : IEquatable<Matrix4Dx4f>, IComparable<Matrix4Dx4f>, ICloneable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичная матрица
			/// </summary>
			public static readonly Matrix4Dx4f Identity = new Matrix4Dx4f(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Матричное произведение
			/// </summary>
			/// <param name="left">Левая матрица</param>
			/// <param name="right">Правая матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Multiply(in Matrix4Dx4f left, in Matrix4Dx4f right, out Matrix4Dx4f result)
			{
				result.M11 = (left.M11 * right.M11) + (left.M21 * right.M12) + (left.M31 * right.M13) + (left.M41 * right.M14);
				result.M12 = (left.M12 * right.M11) + (left.M22 * right.M12) + (left.M32 * right.M13) + (left.M42 * right.M14);
				result.M13 = (left.M13 * right.M11) + (left.M23 * right.M12) + (left.M33 * right.M13) + (left.M43 * right.M14);
				result.M14 = (left.M14 * right.M11) + (left.M24 * right.M12) + (left.M34 * right.M13) + (left.M44 * right.M14);

				result.M21 = (left.M11 * right.M21) + (left.M21 * right.M22) + (left.M31 * right.M23) + (left.M41 * right.M24);
				result.M22 = (left.M12 * right.M21) + (left.M22 * right.M22) + (left.M32 * right.M23) + (left.M42 * right.M24);
				result.M23 = (left.M13 * right.M21) + (left.M23 * right.M22) + (left.M33 * right.M23) + (left.M43 * right.M24);
				result.M24 = (left.M14 * right.M21) + (left.M24 * right.M22) + (left.M34 * right.M23) + (left.M44 * right.M24);

				result.M31 = (left.M11 * right.M31) + (left.M21 * right.M32) + (left.M31 * right.M33) + (left.M41 * right.M34);
				result.M32 = (left.M12 * right.M31) + (left.M22 * right.M32) + (left.M32 * right.M33) + (left.M42 * right.M34);
				result.M33 = (left.M13 * right.M31) + (left.M23 * right.M32) + (left.M33 * right.M33) + (left.M43 * right.M34);
				result.M34 = (left.M14 * right.M31) + (left.M24 * right.M32) + (left.M34 * right.M33) + (left.M44 * right.M34);

				result.M41 = (left.M11 * right.M41) + (left.M21 * right.M42) + (left.M31 * right.M43) + (left.M41 * right.M44);
				result.M42 = (left.M12 * right.M41) + (left.M22 * right.M42) + (left.M32 * right.M43) + (left.M42 * right.M44);
				result.M43 = (left.M13 * right.M41) + (left.M23 * right.M42) + (left.M33 * right.M43) + (left.M43 * right.M44);
				result.M44 = (left.M14 * right.M41) + (left.M24 * right.M42) + (left.M34 * right.M43) + (left.M44 * right.M44);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инверсия матрицы
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			/// <returns>Статус успешности инверсии</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Inverse(in Matrix4Dx4f matrix, out Matrix4Dx4f result)
			{
				Single det, oodet;

				result.M11 = Matrix3Dx3f.Determinat(matrix.M22, matrix.M23, matrix.M24, matrix.M32, matrix.M33, matrix.M34, matrix.M42, matrix.M43, matrix.M44);
				result.M12 = -Matrix3Dx3f.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M32, matrix.M33, matrix.M34, matrix.M42, matrix.M43, matrix.M44);
				result.M13 = Matrix3Dx3f.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M22, matrix.M23, matrix.M24, matrix.M42, matrix.M43, matrix.M44);
				result.M14 = -Matrix3Dx3f.Determinat(matrix.M12, matrix.M13, matrix.M14, matrix.M22, matrix.M23, matrix.M24, matrix.M32, matrix.M33, matrix.M34);

				result.M21 = -Matrix3Dx3f.Determinat(matrix.M21, matrix.M23, matrix.M24, matrix.M31, matrix.M33, matrix.M34, matrix.M41, matrix.M43, matrix.M44);
				result.M22 = Matrix3Dx3f.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M31, matrix.M33, matrix.M34, matrix.M41, matrix.M43, matrix.M44);
				result.M23 = -Matrix3Dx3f.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M21, matrix.M23, matrix.M24, matrix.M41, matrix.M43, matrix.M44);
				result.M24 = Matrix3Dx3f.Determinat(matrix.M11, matrix.M13, matrix.M14, matrix.M21, matrix.M23, matrix.M24, matrix.M31, matrix.M33, matrix.M34);

				result.M31 = Matrix3Dx3f.Determinat(matrix.M21, matrix.M22, matrix.M24, matrix.M31, matrix.M32, matrix.M34, matrix.M41, matrix.M42, matrix.M44);
				result.M32 = -Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M31, matrix.M32, matrix.M34, matrix.M41, matrix.M42, matrix.M44);
				result.M33 = Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M21, matrix.M22, matrix.M24, matrix.M41, matrix.M42, matrix.M44);
				result.M34 = -Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M14, matrix.M21, matrix.M22, matrix.M24, matrix.M31, matrix.M32, matrix.M34);

				result.M41 = -Matrix3Dx3f.Determinat(matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);
				result.M42 = Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M31, matrix.M32, matrix.M33, matrix.M41, matrix.M42, matrix.M43);
				result.M43 = -Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M41, matrix.M42, matrix.M43);
				result.M44 = Matrix3Dx3f.Determinat(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33);

				det = (matrix.M11 * result.M11) + (matrix.M21 * result.M12) + (matrix.M31 * result.M13) + (matrix.M41 * result.M14);

				if (XMath.Approximately(det, 0))
				{
					result = Matrix4Dx4f.Identity;

					return false;
				}
				else
				{

					oodet = 1.0f / det;

					result.M11 *= oodet;
					result.M12 *= oodet;
					result.M13 *= oodet;
					result.M14 *= oodet;

					result.M21 *= oodet;
					result.M22 *= oodet;
					result.M23 *= oodet;
					result.M24 *= oodet;

					result.M31 *= oodet;
					result.M32 *= oodet;
					result.M33 *= oodet;
					result.M34 *= oodet;

					result.M41 *= oodet;
					result.M42 *= oodet;
					result.M43 *= oodet;
					result.M44 *= oodet;

					return true;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инверсия матрицы содержащей трансформации вращения
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void InverseRotation(in Matrix4Dx4f matrix, out Matrix4Dx4f result)
			{
				result.M11 = matrix.M11;
				result.M12 = matrix.M21;
				result.M13 = matrix.M31;
				result.M14 = matrix.M14;

				result.M21 = matrix.M12;
				result.M22 = matrix.M22;
				result.M23 = matrix.M32;
				result.M24 = matrix.M24;

				result.M31 = matrix.M13;
				result.M32 = matrix.M23;
				result.M33 = matrix.M33;
				result.M34 = matrix.M34;

				result.M41 = -((matrix.M11 * matrix.M41) + (matrix.M12 * matrix.M42) + (matrix.M13 * matrix.M43));
				result.M42 = -((matrix.M21 * matrix.M41) + (matrix.M22 * matrix.M42) + (matrix.M23 * matrix.M43));
				result.M43 = -((matrix.M31 * matrix.M41) + (matrix.M32 * matrix.M42) + (matrix.M33 * matrix.M43));
				result.M44 = matrix.M44;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Транспонирование матрицы
			/// </summary>
			/// <param name="matrix">Исходная матрица</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Transpose(in Matrix4Dx4f matrix, out Matrix4Dx4f result)
			{
				result.M11 = matrix.M11;
				result.M21 = matrix.M12;
				result.M31 = matrix.M13;
				result.M41 = matrix.M14;

				result.M12 = matrix.M21;
				result.M22 = matrix.M22;
				result.M32 = matrix.M23;
				result.M42 = matrix.M24;

				result.M13 = matrix.M31;
				result.M23 = matrix.M32;
				result.M33 = matrix.M33;
				result.M43 = matrix.M34;

				result.M14 = matrix.M41;
				result.M24 = matrix.M42;
				result.M34 = matrix.M43;
				result.M44 = matrix.M44;
			}
			#endregion

			#region ======================================= МЕТОДЫ ТРАНСФОРМАЦИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси X
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationX(Single angle, ref Matrix4Dx4f result)
			{
				var ct = (Single)Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = (Single)Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = 1;
				result.M21 = 0;
				result.M31 = 0;

				result.M12 = 0;
				result.M22 = ct;
				result.M32 = -st;

				result.M13 = 0;
				result.M23 = st;
				result.M33 = ct;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси Y
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationY(Single angle, ref Matrix4Dx4f result)
			{
				var ct = (Single)Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = (Single)Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = ct;
				result.M21 = 0;
				result.M31 = st;

				result.M12 = 0;
				result.M22 = 1;
				result.M32 = 0;

				result.M13 = -st;
				result.M23 = 0;
				result.M33 = ct;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения вокруг оси Z
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationZ(Single angle, ref Matrix4Dx4f result)
			{
				var ct = (Single)Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = (Single)Math.Sin(angle * XMath.DegreeToRadian_D);

				result.M11 = ct;
				result.M21 = -st;
				result.M31 = 0;

				result.M12 = st;
				result.M22 = ct;
				result.M32 = 0;

				result.M13 = 0;
				result.M23 = 0;
				result.M33 = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="angle">Угол, задается в градусах</param>
			/// <param name="axis">Ось, вектор должен быть единичным</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromAxis(Single angle, in Vector3Df axis, ref Matrix4Dx4f result)
			{
				var ct = (Single)Math.Cos(angle * XMath.DegreeToRadian_D);
				var st = (Single)Math.Sin(angle * XMath.DegreeToRadian_D);

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
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="quaternion">Кватернион</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromQuaternion(in Quaternion3Df quaternion, ref Matrix4Dx4f result)
			{
				Single wx, wy, wz, xx, yy, yz, xy, xz, zz, x2, y2, z2;

				var sc = 2.0f / quaternion.Length;

				x2 = quaternion.X * sc; y2 = quaternion.Y * sc; z2 = quaternion.Z * sc;
				xx = quaternion.X * x2; xy = quaternion.X * y2; xz = quaternion.X * z2;
				yy = quaternion.Y * y2; yz = quaternion.Y * z2; zz = quaternion.Z * z2;
				wx = quaternion.W * x2; wy = quaternion.W * y2; wz = quaternion.W * z2;

				result.M11 = 1.0f - (yy + zz);
				result.M21 = xy - wz;
				result.M31 = xz + wy;

				result.M12 = xy + wz;
				result.M22 = 1.0f - (xx + zz);
				result.M32 = yz - wx;

				result.M13 = xz - wy;
				result.M23 = yz + wx;
				result.M33 = 1.0f - (xx + yy);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы вращения
			/// </summary>
			/// <param name="matrix">Исходная матрица трансформации</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationFromMatrix(in Matrix3Dx3f matrix, ref Matrix4Dx4f result)
			{
				// copy the 3x3 rotation block
				result.M11 = matrix.M11; result.M12 = matrix.M12; result.M13 = matrix.M13;
				result.M21 = matrix.M21; result.M22 = matrix.M22; result.M23 = matrix.M23;
				result.M31 = matrix.M31; result.M32 = matrix.M32; result.M33 = matrix.M33;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка матрицы перемещения
			/// </summary>
			/// <param name="position">Позиция</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Translation(in Vector3Df position, ref Matrix4Dx4f result)
			{
				result.M41 = position.X;
				result.M42 = position.Y;
				result.M43 = position.Z;
			}

			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ 3D КАМЕРЫ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление пирамиды видимости
			/// </summary>
			/// <param name="left">Левый край</param>
			/// <param name="right">Правый край</param>
			/// <param name="bottom">Верхний край</param>
			/// <param name="top">Нижний край</param>
			/// <param name="near">Ближняя плоскость отсечения</param>
			/// <param name="far">Дальняя плоскость отсечения</param>
			/// <param name="result">Результирующая матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void PerspectiveFrustum(Single left, Single right, Single bottom, Single top, Single near,
				Single far, out Matrix4Dx4f result)
			{
				result.M11 = 2.0f * near / (right - left);
				result.M12 = 0.0f;
				result.M13 = 0.0f;
				result.M14 = 0.0f;

				result.M21 = 0.0f;
				result.M22 = 2.0f * near / (top - bottom);
				result.M23 = 0.0f;
				result.M24 = 0.0f;

				result.M31 = (right + left) / (right - left);
				result.M32 = (top + bottom) / (top - bottom);
				result.M33 = -(far + near) / (far - near);
				result.M34 = 1.0f;

				result.M41 = 0.0f;
				result.M42 = 0.0f;
				result.M43 = -(2.0f * far * near) / (far - near);
				result.M44 = 0.0f;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			public Single M11, M12, M13, M14;
			public Single M21, M22, M23, M24;
			public Single M31, M32, M33, M34;
			public Single M41, M42, M43, M44;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Инверсная матрица
			/// </summary>
			public readonly Matrix4Dx4f Inversed
			{
				get
				{
					Matrix4Dx4f result;

					Matrix4Dx4f.Inverse(in this, out result);

					return result;
				}
			}

			/// <summary>
			/// Транспонированная матрица
			/// </summary>
			public readonly Matrix4Dx4f Transposed
			{
				get
				{
					Matrix4Dx4f result;

					Matrix4Dx4f.Transpose(in this, out result);

					return result;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу соответствующим компонентами
			/// </summary>
			/// <param name="m11">Компонент с позицией 11</param>
			/// <param name="m12">Компонент с позицией 12</param>
			/// <param name="m13">Компонент с позицией 13</param>
			/// <param name="m14">Компонент с позицией 14</param>
			/// <param name="m21">Компонент с позицией 21</param>
			/// <param name="m22">Компонент с позицией 22</param>
			/// <param name="m23">Компонент с позицией 23</param>
			/// <param name="m24">Компонент с позицией 24</param>
			/// <param name="m31">Компонент с позицией 31</param>
			/// <param name="m32">Компонент с позицией 32</param>
			/// <param name="m33">Компонент с позицией 33</param>
			/// <param name="m34">Компонент с позицией 34</param>
			/// <param name="m41">Компонент с позицией 41</param>
			/// <param name="m42">Компонент с позицией 43</param>
			/// <param name="m43">Компонент с позицией 43</param>
			/// <param name="m44">Компонент с позицией 44</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix4Dx4f(Single m11, Single m12, Single m13, Single m14,
							   Single m21, Single m22, Single m23, Single m24,
							   Single m31, Single m32, Single m33, Single m34,
							   Single m41, Single m42, Single m43, Single m44)
			{
				M11 = m11; M12 = m12; M13 = m13; M14 = m14;
				M21 = m21; M22 = m22; M23 = m23; M24 = m24;
				M31 = m31; M32 = m32; M33 = m33; M34 = m34;
				M41 = m41; M42 = m42; M43 = m43; M44 = m44;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует матрицу указанной матрицей
			/// </summary>
			/// <param name="source">Матрица</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix4Dx4f(Matrix4Dx4f source)
			{
				M11 = source.M11; M12 = source.M12; M13 = source.M13; M14 = source.M14;
				M21 = source.M21; M22 = source.M22; M23 = source.M23; M24 = source.M24;
				M31 = source.M31; M32 = source.M32; M33 = source.M33; M34 = source.M34;
				M41 = source.M41; M42 = source.M42; M43 = source.M43; M44 = source.M44;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка идентичности матриц
			/// </summary>
			/// <param name="obj">Сравниваемая матрица</param>
			/// <returns>Статус идентичности матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Boolean Equals(System.Object? obj)
			{
				if (obj != null)
				{
					if (obj is Matrix4Dx4f matrix)
					{
						return Equals(matrix);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка идентичности матриц
			/// </summary>
			/// <param name="other">Сравниваемая матрица</param>
			/// <returns>Статус идентичности матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Boolean Equals(Matrix4Dx4f other)
			{
				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода матрицы
			/// </summary>
			/// <returns>Хеш-код матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Int32 GetHashCode()
			{
				return M11.GetHashCode() ^ M22.GetHashCode() ^ M33.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение матриц для упорядочивания
			/// </summary>
			/// <param name="other">Матрица</param>
			/// <returns>Статус сравнения матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Int32 CompareTo(Matrix4Dx4f other)
			{
				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование матрицы
			/// </summary>
			/// <returns>Копия матрицы</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление матрицы с указанием значений компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly String ToString()
			{
				return "M11 = " + M11.ToString("F3") + "; M22 = " + M22.ToString("F3") +
					   "; M33 = " + M33.ToString("F3");
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на равенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус равенства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(Matrix4Dx4f left, Matrix4Dx4f right)
			{
				return left.Equals(right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на неравенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус неравенство</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Matrix4Dx4f left, Matrix4Dx4f right)
			{
				return !(left == right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение матриц
			/// </summary>
			/// <param name="left">Первая матрица</param>
			/// <param name="right">Вторая матрица</param>
			/// <returns>Сумма матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4f operator +(Matrix4Dx4f left, Matrix4Dx4f right)
			{
				Matrix4Dx4f result;

				result.M11 = left.M11 + right.M11; result.M12 = left.M12 + right.M12; result.M13 = left.M13 + right.M13; result.M14 = left.M14 + right.M14;
				result.M21 = left.M21 + right.M21; result.M22 = left.M22 + right.M22; result.M23 = left.M23 + right.M23; result.M24 = left.M24 + right.M24;
				result.M31 = left.M31 + right.M31; result.M32 = left.M32 + right.M32; result.M33 = left.M33 + right.M33; result.M34 = left.M34 + right.M34;
				result.M41 = left.M41 + right.M41; result.M42 = left.M42 + right.M42; result.M43 = left.M43 + right.M43; result.M44 = left.M44 + right.M44;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание матриц
			/// </summary>
			/// <param name="left">Первая матрица</param>
			/// <param name="right">Вторая матрица</param>
			/// <returns>Разность матриц</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4f operator -(Matrix4Dx4f left, Matrix4Dx4f right)
			{
				Matrix4Dx4f result;

				result.M11 = left.M11 - right.M11; result.M12 = left.M12 - right.M12; result.M13 = left.M13 - right.M13; result.M14 = left.M14 - right.M14;
				result.M21 = left.M21 - right.M21; result.M22 = left.M22 - right.M22; result.M23 = left.M23 - right.M23; result.M24 = left.M24 - right.M24;
				result.M31 = left.M31 - right.M31; result.M32 = left.M32 - right.M32; result.M33 = left.M33 - right.M33; result.M34 = left.M34 - right.M34;
				result.M41 = left.M41 - right.M41; result.M42 = left.M42 - right.M42; result.M43 = left.M43 - right.M43; result.M44 = left.M44 - right.M44;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение матрицы на скаляр
			/// </summary>
			/// <param name="matrix">Матрица</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированная матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4f operator *(Matrix4Dx4f matrix, Single scalar)
			{
				Matrix4Dx4f result;

				result.M11 = matrix.M11 * scalar; result.M12 = matrix.M12 * scalar; result.M13 = matrix.M13 * scalar; result.M14 = matrix.M14 * scalar;
				result.M21 = matrix.M21 * scalar; result.M22 = matrix.M22 * scalar; result.M23 = matrix.M23 * scalar; result.M24 = matrix.M24 * scalar;
				result.M31 = matrix.M31 * scalar; result.M32 = matrix.M32 * scalar; result.M33 = matrix.M33 * scalar; result.M34 = matrix.M34 * scalar;
				result.M41 = matrix.M41 * scalar; result.M42 = matrix.M42 * scalar; result.M43 = matrix.M43 * scalar; result.M44 = matrix.M44 * scalar;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Деление матрицы на скаляр
			/// </summary>
			/// <param name="matrix">Матрица</param>
			/// <param name="scalar">Скаляр</param>
			/// <returns>Масштабированная матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4f operator /(Matrix4Dx4f matrix, Single scalar)
			{
				Matrix4Dx4f result;

				scalar = 1 / scalar;

				result.M11 = matrix.M11 * scalar; result.M12 = matrix.M12 * scalar; result.M13 = matrix.M13 * scalar; result.M14 = matrix.M14 * scalar;
				result.M21 = matrix.M21 * scalar; result.M22 = matrix.M22 * scalar; result.M23 = matrix.M23 * scalar; result.M24 = matrix.M24 * scalar;
				result.M31 = matrix.M31 * scalar; result.M32 = matrix.M32 * scalar; result.M33 = matrix.M33 * scalar; result.M34 = matrix.M34 * scalar;
				result.M41 = matrix.M41 * scalar; result.M42 = matrix.M42 * scalar; result.M43 = matrix.M43 * scalar; result.M44 = matrix.M44 * scalar;

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Умножение матрицы на матрицу
			/// </summary>
			/// <param name="left">Левая матрица</param>
			/// <param name="right">Правая матрица</param>
			/// <returns>Результирующая матрица</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix4Dx4f operator *(Matrix4Dx4f left, Matrix4Dx4f right)
			{
				Matrix4Dx4f result;

				Multiply(in left, in right, out result);

				return result;
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация строк матрицы на основе индекса
			/// </summary>
			/// <param name="index">Индекс строки матрицы</param>
			/// <returns>Строка матрицы в виде четырехмерного вектора</returns>
			//---------------------------------------------------------------------------------------------------------
			public Vector4Df this[Int32 index]
			{
				readonly get
				{
					switch (index)
					{
						case 0:
							return new Vector4Df(M11, M12, M13, M14);
						case 1:
							return new Vector4Df(M21, M22, M23, M24);
						case 2:
							return new Vector4Df(M31, M32, M33, M34);
						default:
							return new Vector4Df(M41, M42, M43, M44);
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							{
								M11 = value.X;
								M12 = value.Y;
								M13 = value.Z;
								M14 = value.W;
							}
							break;
						case 1:
							{
								M21 = value.X;
								M22 = value.Y;
								M23 = value.Z;
								M24 = value.W;
							}
							break;
						case 2:
							{
								M31 = value.X;
								M32 = value.Y;
								M33 = value.Z;
								M34 = value.W;
							}
							break;
						default:
							{
								M41 = value.X;
								M42 = value.Y;
								M43 = value.Z;
								M44 = value.W;
							}
							break;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в кватернион
			/// </summary>
			/// <returns>Кватернион</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Quaternion3Df ToQuaternion3Df()
			{
				var tr = M11 + M22 + M33;
				if (tr > 0.0)
				{
					return new Quaternion3Df(M23 - M32, M31 - M13, M12 - M21, tr + 1.0f);
				}
				else
				{
					if (M11 > M22 && M11 > M33)
					{
						return new Quaternion3Df(1.0f + M11 - M22 - M33, M21 + M12, M31 + M13, M23 - M32);
					}
					else
					{
						if (M22 > M33)
						{
							return new Quaternion3Df(M21 + M12, 1.0f + M22 - M11 - M33, M32 + M23, M31 - M13);
						}
						else
						{
							return new Quaternion3Df(M31 + M13, M32 + M23, 1.0f + M33 - M11 - M22, M12 - M21);
						}
					}
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================