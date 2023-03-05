//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема 2D геометрии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGeometry2DMatrix3x2.cs
*		Двухмерная матрица размерностью 3х2.
*		Реализация двухмерной матрицы размерностью 3х2 для реализации базовых трансформаций в 2D плоскости.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup MathGeometry2D
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Двухмерная матрица размерностью 3х2
		/// </summary>
		/// <remarks>
		/// Реализация двухмерной матрицы размерностью 3х2 для реализации базовых трансформаций в 2D плоскости
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public struct Matrix3Dx2f : IEquatable<Matrix3Dx2f>, IFormattable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Единичная матрица
			/// </summary>
			public static readonly Matrix3Dx2f Identity = new Matrix3Dx2f(1, 0, 0, 1, 0, 0);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ  =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the sum of two matrices
			/// </summary>
			/// <param name="left">The first matrix to add</param>
			/// <param name="right">The second matrix to add</param>
			/// <param name="result">When the method completes, contains the sum of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Add(ref Matrix3Dx2f left, ref Matrix3Dx2f right, out Matrix3Dx2f result)
			{
				result.M11 = left.M11 + right.M11;
				result.M12 = left.M12 + right.M12;
				result.M21 = left.M21 + right.M21;
				result.M22 = left.M22 + right.M22;
				result.M31 = left.M31 + right.M31;
				result.M32 = left.M32 + right.M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the sum of two matrices
			/// </summary>
			/// <param name="left">The first matrix to add</param>
			/// <param name="right">The second matrix to add</param>
			/// <returns>The sum of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Add(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Add(ref left, ref right, out result);
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
			public static void Subtract(ref Matrix3Dx2f left, ref Matrix3Dx2f right, out Matrix3Dx2f result)
			{
				result.M11 = left.M11 - right.M11;
				result.M12 = left.M12 - right.M12;
				result.M21 = left.M21 - right.M21;
				result.M22 = left.M22 - right.M22;
				result.M31 = left.M31 - right.M31;
				result.M32 = left.M32 - right.M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the difference between two matrices
			/// </summary>
			/// <param name="left">The first matrix to subtract</param>
			/// <param name="right">The second matrix to subtract</param>
			/// <returns>The difference between the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Subtract(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Subtract(ref left, ref right, out result);
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
			public static void Multiply(ref Matrix3Dx2f left, Single right, out Matrix3Dx2f result)
			{
				result.M11 = left.M11 * right;
				result.M12 = left.M12 * right;
				result.M21 = left.M21 * right;
				result.M22 = left.M22 * right;
				result.M31 = left.M31 * right;
				result.M32 = left.M32 * right;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Scales a matrix by the given value
			/// </summary>
			/// <param name="left">The matrix to scale</param>
			/// <param name="right">The amount by which to scale</param>
			/// <returns>The scaled matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Multiply(Matrix3Dx2f left, Single right)
			{
				Matrix3Dx2f result;
				Multiply(ref left, right, out result);
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
			public static void Multiply(ref Matrix3Dx2f left, ref Matrix3Dx2f right, out Matrix3Dx2f result)
			{
				result = new Matrix3Dx2f();
				result.M11 = (left.M11 * right.M11) + (left.M12 * right.M21);
				result.M12 = (left.M11 * right.M12) + (left.M12 * right.M22);
				result.M21 = (left.M21 * right.M11) + (left.M22 * right.M21);
				result.M22 = (left.M21 * right.M12) + (left.M22 * right.M22);
				result.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + right.M31;
				result.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + right.M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the product of two matrices
			/// </summary>
			/// <param name="left">The first matrix to multiply</param>
			/// <param name="right">The second matrix to multiply</param>
			/// <returns>The product of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Multiply(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Multiply(ref left, ref right, out result);
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
			public static void Divide(ref Matrix3Dx2f left, Single right, out Matrix3Dx2f result)
			{
				Single inv = 1.0f / right;

				result.M11 = left.M11 * inv;
				result.M12 = left.M12 * inv;
				result.M21 = left.M21 * inv;
				result.M22 = left.M22 * inv;
				result.M31 = left.M31 * inv;
				result.M32 = left.M32 * inv;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines the quotient of two matrices
			/// </summary>
			/// <param name="left">The first matrix to divide</param>
			/// <param name="right">The second matrix to divide</param>
			/// <param name="result">When the method completes, contains the quotient of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Divide(ref Matrix3Dx2f left, ref Matrix3Dx2f right, out Matrix3Dx2f result)
			{
				result.M11 = left.M11 / right.M11;
				result.M12 = left.M12 / right.M12;
				result.M21 = left.M21 / right.M21;
				result.M22 = left.M22 / right.M22;
				result.M31 = left.M31 / right.M31;
				result.M32 = left.M32 / right.M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to be negated</param>
			/// <param name="result">When the method completes, contains the negated matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Negate(ref Matrix3Dx2f value, out Matrix3Dx2f result)
			{
				result.M11 = -value.M11;
				result.M12 = -value.M12;
				result.M21 = -value.M21;
				result.M22 = -value.M22;
				result.M31 = -value.M31;
				result.M32 = -value.M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to be negated</param>
			/// <returns>The negated matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Negate(Matrix3Dx2f value)
			{
				Matrix3Dx2f result;
				Negate(ref value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a linear interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight</param>
			/// <param name="result">When the method completes, contains the linear interpolation of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Lerp(ref Matrix3Dx2f start, ref Matrix3Dx2f end, Single time, out Matrix3Dx2f result)
			{
				result.M11 = XMathInterpolation.Lerp(start.M11, end.M11, time);
				result.M12 = XMathInterpolation.Lerp(start.M12, end.M12, time);
				result.M21 = XMathInterpolation.Lerp(start.M21, end.M21, time);
				result.M22 = XMathInterpolation.Lerp(start.M22, end.M22, time);
				result.M31 = XMathInterpolation.Lerp(start.M31, end.M31, time);
				result.M32 = XMathInterpolation.Lerp(start.M32, end.M32, time);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a linear interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight</param>
			/// <returns>The linear interpolation of the two matrices</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Lerp(Matrix3Dx2f start, Matrix3Dx2f end, Single time)
			{
				Matrix3Dx2f result;
				Lerp(ref start, ref end, time, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Performs a cubic interpolation between two matrices
			/// </summary>
			/// <param name="start">Start matrix</param>
			/// <param name="end">End matrix</param>
			/// <param name="time">Time between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
			/// <param name="result">When the method completes, contains the cubic interpolation of the two matrices</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SmoothStep(ref Matrix3Dx2f start, ref Matrix3Dx2f end, Single time, out Matrix3Dx2f result)
			{
				time = XMathInterpolation.SmoothStep(time);
				Lerp(ref start, ref end, time, out result);
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
			public static Matrix3Dx2f SmoothStep(Matrix3Dx2f start, Matrix3Dx2f end, Single time)
			{
				Matrix3Dx2f result;
				SmoothStep(ref start, ref end, time, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis and y-axis
			/// </summary>
			/// <param name="scale">Scaling factor for both axes</param>
			/// <param name="result">When the method completes, contains the created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(ref Vector2Df scale, out Matrix3Dx2f result)
			{
				Scaling(scale.X, scale.Y, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis and y-axis
			/// </summary>
			/// <param name="scale">Scaling factor for both axes</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Scaling(Vector2Df scale)
			{
				Matrix3Dx2f result;
				Scaling(ref scale, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis and y-axis
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <param name="result">When the method completes, contains the created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(Single x, Single y, out Matrix3Dx2f result)
			{
				result = Matrix3Dx2f.Identity;
				result.M11 = x;
				result.M22 = y;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that scales along the x-axis and y-axis
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Scaling(Single x, Single y)
			{
				Matrix3Dx2f result;
				Scaling(x, y, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that uniformly scales along both axes
			/// </summary>
			/// <param name="scale">The uniform scale that is applied along both axes.</param>
			/// <param name="result">When the method completes, contains the created scaling matrix.</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(Single scale, out Matrix3Dx2f result)
			{
				result = Matrix3Dx2f.Identity;
				result.M11 = result.M22 = scale;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that uniformly scales along both axes
			/// </summary>
			/// <param name="scale">The uniform scale that is applied along both axes</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Scaling(Single scale)
			{
				Matrix3Dx2f result;
				Scaling(scale, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that is scaling from a specified center
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <param name="center">The center of the scaling</param>
			/// <returns>The created scaling matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Scaling(Single x, Single y, Vector2Df center)
			{
				Matrix3Dx2f result;

				result.M11 = x; result.M12 = 0.0f;
				result.M21 = 0.0f; result.M22 = y;

				result.M31 = center.X - (x * center.X);
				result.M32 = center.Y - (y * center.Y);

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that is scaling from a specified center
			/// </summary>
			/// <param name="x">Scaling factor that is applied along the x-axis</param>
			/// <param name="y">Scaling factor that is applied along the y-axis</param>
			/// <param name="center">The center of the scaling</param>
			/// <param name="result">The created scaling matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scaling(Single x, Single y, ref Vector2Df center, out Matrix3Dx2f result)
			{
				Matrix3Dx2f local_result;

				local_result.M11 = x; local_result.M12 = 0.0f;
				local_result.M21 = 0.0f; local_result.M22 = y;

				local_result.M31 = center.X - (x * center.X);
				local_result.M32 = center.Y - (y * center.Y);

				result = local_result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Rotation(Single angle, out Matrix3Dx2f result)
			{
				Single cos = (Single)Math.Cos(angle);
				Single sin = (Single)Math.Sin(angle);

				result = Matrix3Dx2f.Identity;
				result.M11 = cos;
				result.M12 = sin;
				result.M21 = -sin;
				result.M22 = cos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Rotation(Single angle)
			{
				Matrix3Dx2f result;
				Rotation(angle, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates about a specified center
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis</param>
			/// <param name="center">The center of the rotation</param>
			/// <returns>The created rotation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Rotation(Single angle, Vector2Df center)
			{
				Matrix3Dx2f result;
				Rotation(angle, center, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a matrix that rotates about a specified center
			/// </summary>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis</param>
			/// <param name="center">The center of the rotation.</param>
			/// <param name="result">When the method completes, contains the created rotation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Rotation(Single angle, Vector2Df center, out Matrix3Dx2f result)
			{
				result = Translation(-center) * Rotation(angle) * Translation(center);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a transformation matrix
			/// </summary>
			/// <param name="x_scale">Scaling factor that is applied along the x-axis</param>
			/// <param name="y_scale">Scaling factor that is applied along the y-axis</param>
			/// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis</param>
			/// <param name="x_offset">X-coordinate offset</param>
			/// <param name="y_offset">Y-coordinate offset</param>
			/// <param name="result">When the method completes, contains the created transformation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Transformation(Single x_scale, Single y_scale, Single angle, Single x_offset, 
				Single y_offset, out Matrix3Dx2f result)
			{
				result = Scaling(x_scale, y_scale) * Rotation(angle) * Translation(x_offset, y_offset);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a transformation matrix
			/// </summary>
			/// <param name="x_scale">Scaling factor that is applied along the x-axis</param>
			/// <param name="y_scale">Scaling factor that is applied along the y-axis</param>
			/// <param name="angle">Angle of rotation in radians</param>
			/// <param name="x_offset">X-coordinate offset</param>
			/// <param name="y_offset">Y-coordinate offset</param>
			/// <returns>The created transformation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Transformation(Single x_scale, Single y_scale, Single angle, Single x_offset, Single y_offset)
			{
				Matrix3Dx2f result;
				Transformation(x_scale, y_scale, angle, x_offset, y_offset, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a translation matrix using the specified offsets
			/// </summary>
			/// <param name="value">The offset for both coordinate planes</param>
			/// <param name="result">When the method completes, contains the created translation matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Translation(ref Vector2Df value, out Matrix3Dx2f result)
			{
				Translation(value.X, value.Y, out result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a translation matrix using the specified offsets
			/// </summary>
			/// <param name="value">The offset for both coordinate planes</param>
			/// <returns>The created translation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Translation(Vector2Df value)
			{
				Matrix3Dx2f result;
				Translation(ref value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a translation matrix using the specified offsets
			/// </summary>
			/// <param name="x">X-coordinate offset</param>
			/// <param name="y">Y-coordinate offset</param>
			/// <param name="result">When the method completes, contains the created translation matrix.</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Translation(Single x, Single y, out Matrix3Dx2f result)
			{
				result = Matrix3Dx2f.Identity;
				result.M31 = x;
				result.M32 = y;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a translation matrix using the specified offsets
			/// </summary>
			/// <param name="x">X-coordinate offset</param>
			/// <param name="y">Y-coordinate offset</param>
			/// <returns>The created translation matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Translation(Single x, Single y)
			{
				Matrix3Dx2f result;
				Translation(x, y, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Transforms a vector by this matrix
			/// </summary>
			/// <param name="matrix">The matrix to use as a transformation matrix</param>
			/// <param name="point">The original vector to apply the transformation</param>
			/// <returns>The result of the transformation for the input vector</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df TransformPoint(Matrix3Dx2f matrix, Vector2Df point)
			{
				Vector2Df result;
				result.X = (point.X * matrix.M11) + (point.Y * matrix.M21) + matrix.M31;
				result.Y = (point.X * matrix.M12) + (point.Y * matrix.M22) + matrix.M32;
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Transforms a vector by this matrix
			/// </summary>
			/// <param name="matrix">The matrix to use as a transformation matrix</param>
			/// <param name="point">The original vector to apply the transformation</param>
			/// <returns>The result of the transformation for the input vector</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df TransformPoint(ref Matrix3Dx2f matrix, ref Vector2Df point)
			{
				return new Vector2Df((point.X * matrix.M11) + (point.Y * matrix.M21) + matrix.M31,
					(point.X * matrix.M12) + (point.Y * matrix.M22) + matrix.M32);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Transforms a vector by this matrix
			/// </summary>
			/// <param name="matrix">The matrix to use as a transformation matrix</param>
			/// <param name="point">The original vector to apply the transformation</param>
			/// <param name="result">The result of the transformation for the input vector</param>
			//---------------------------------------------------------------------------------------------------------
			public static void TransformPoint(ref Matrix3Dx2f matrix, ref Vector2Df point, out Vector2Df result)
			{
				Vector2Df local_result;
				local_result.X = (point.X * matrix.M11) + (point.Y * matrix.M21) + matrix.M31;
				local_result.Y = (point.X * matrix.M12) + (point.Y * matrix.M22) + matrix.M32;
				result = local_result;
			}


			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Transforms a vector by this matrix
			/// </summary>
			/// <param name="matrix">The matrix to use as a transformation matrix</param>
			/// <param name="point">The original vector to apply the transformation</param>
			/// <returns>The result of the transformation for the input vector</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df TransformVector(ref Matrix3Dx2f matrix, ref Vector2Df point)
			{
				return new Vector2Df((point.X * matrix.M11) + (point.Y * matrix.M21),
					(point.X * matrix.M12) + (point.Y * matrix.M22));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a skew matrix
			/// </summary>
			/// <param name="angle_x">Angle of skew along the X-axis in radians</param>
			/// <param name="angle_y">Angle of skew along the Y-axis in radians</param>
			/// <returns>The created skew matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Skew(Single angle_x, Single angle_y)
			{
				Matrix3Dx2f result;
				Skew(angle_x, angle_y, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates a skew matrix
			/// </summary>
			/// <param name="angle_x">Angle of skew along the X-axis in radians</param>
			/// <param name="angle_y">Angle of skew along the Y-axis in radians</param>
			/// <param name="result">When the method completes, contains the created skew matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Skew(Single angle_x, Single angle_y, out Matrix3Dx2f result)
			{
				result = Matrix3Dx2f.Identity;
				result.M12 = (Single)Math.Tan(angle_x);
				result.M21 = (Single)Math.Tan(angle_y);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose inverse is to be calculated</param>
			/// <returns>the inverse of the specified matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Invert(Matrix3Dx2f value)
			{
				Matrix3Dx2f result;
				Invert(ref value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose inverse is to be calculated</param>
			/// <returns>the inverse of the specified matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f Invert(ref Matrix3Dx2f value)
			{
				Matrix3Dx2f result;
				Invert(ref value, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of the specified matrix
			/// </summary>
			/// <param name="value">The matrix whose inverse is to be calculated</param>
			/// <param name="result">When the method completes, contains the inverse of the specified matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Invert(ref Matrix3Dx2f value, out Matrix3Dx2f result)
			{
				Single determinant = value.Determinant();

				if (XMath.IsZero(determinant))
				{
					result = Identity;
					return;
				}

				Single invdet = 1.0f / determinant;
				Single offset_x = value.M31;
				Single offset_y = value.M32;

				result = new Matrix3Dx2f(
					value.M22 * invdet,
					-value.M12 * invdet,
					-value.M21 * invdet,
					value.M11 * invdet,
					((value.M21 * offset_y) - (offset_x * value.M22)) * invdet,
					((offset_x * value.M12) - (value.M11 * offset_y)) * invdet);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Element (1,1)
			/// </summary>
			public Single M11;

			/// <summary>
			/// Element (1,2)
			/// </summary>
			public Single M12;

			/// <summary>
			/// Element (2,1)
			/// </summary>
			public Single M21;

			/// <summary>
			/// Element (2,2)
			/// </summary>
			public Single M22;

			/// <summary>
			/// Element (3,1)
			/// </summary>
			public Single M31;

			/// <summary>
			/// Element (3,2)
			/// </summary>
			public Single M32;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Gets or sets the first row in the matrix; that is M11 and M12
			/// </summary>
			public Vector2Df Row1
			{
				get { return new Vector2Df(M11, M12); }
				set { M11 = value.X; M12 = value.Y; }
			}

			/// <summary>
			/// Gets or sets the second row in the matrix; that is M21 and M22
			/// </summary>
			public Vector2Df Row2
			{
				get { return new Vector2Df(M21, M22); }
				set { M21 = value.X; M22 = value.Y; }
			}

			/// <summary>
			/// Gets or sets the third row in the matrix; that is M31 and M32
			/// </summary>
			public Vector2Df Row3
			{
				get { return new Vector2Df(M31, M32); }
				set { M31 = value.X; M32 = value.Y; }
			}

			/// <summary>
			/// Gets or sets the first column in the matrix; that is M11, M21, and M31
			/// </summary>
			public Vector3Df Column1
			{
				get { return new Vector3Df(M11, M21, M31); }
				set { M11 = value.X; M21 = value.Y; M31 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the second column in the matrix; that is M12, M22, and M32
			/// </summary>
			public Vector3Df Column2
			{
				get { return new Vector3Df(M12, M22, M32); }
				set { M12 = value.X; M22 = value.Y; M32 = value.Z; }
			}

			/// <summary>
			/// Gets or sets the translation of the matrix; that is M31 and M32
			/// </summary>
			public Vector2Df TranslationVector
			{
				get { return new Vector2Df(M31, M32); }
				set { M31 = value.X; M32 = value.Y; }
			}

			/// <summary>
			/// Gets or sets the scale of the matrix; that is M11 and M22
			/// </summary>
			public Vector2Df ScaleVector
			{
				get { return new Vector2Df(M11, M22); }
				set { M11 = value.X; M22 = value.Y; }
			}

			/// <summary>
			/// Gets a value indicating whether this instance is an identity matrix
			/// </summary>
			/// <value>
			/// <c>true</c> if this instance is an identity matrix; otherwise, <c>false</c>.
			/// </value>
			public Boolean IsIdentity
			{
				get { return this.Equals(Identity); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx2f"/> struct
			/// </summary>
			/// <param name="value">The value that will be assigned to all components</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx2f(Single value)
			{
				M11 = M12 =
				M21 = M22 =
				M31 = M32 = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx2f"/> struct
			/// </summary>
			/// <param name="M11">The value to assign at row 1 column 1 of the matrix</param>
			/// <param name="M12">The value to assign at row 1 column 2 of the matrix</param>
			/// <param name="M21">The value to assign at row 2 column 1 of the matrix</param>
			/// <param name="M22">The value to assign at row 2 column 2 of the matrix</param>
			/// <param name="M31">The value to assign at row 3 column 1 of the matrix</param>
			/// <param name="M32">The value to assign at row 3 column 2 of the matrix</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx2f(Single M11, Single M12, Single M21, Single M22, Single M31, Single M32)
			{
				this.M11 = M11; this.M12 = M12;
				this.M21 = M21; this.M22 = M22;
				this.M31 = M31; this.M32 = M32;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Initializes a new instance of the <see cref="Matrix3Dx2f"/> struct
			/// </summary>
			/// <param name="values">The values to assign to the components of the matrix. This must be an array with six elements</param>
			//---------------------------------------------------------------------------------------------------------
			public Matrix3Dx2f(Single[] values)
			{
				M11 = values[0];
				M12 = values[1];

				M21 = values[2];
				M22 = values[3];

				M31 = values[4];
				M32 = values[5];
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="System.Object"/> is equal to this instance
			/// </summary>
			/// <param name="value">The <see cref="System.Object"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object value)
			{
				if (!(value is Matrix3Dx2f))
				{
					return false;
				}

				var matrix = (Matrix3Dx2f)value;
				return Equals(ref matrix);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="Matrix3Dx2f"/> is equal to this instance
			/// </summary>
			/// <param name="other">The <see cref="Matrix3Dx2f"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="Matrix3Dx2f"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(Matrix3Dx2f other)
			{
				return Equals(ref other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Determines whether the specified <see cref="Matrix3Dx2f"/> is equal to this instance
			/// </summary>
			/// <param name="other">The <see cref="Matrix3Dx2f"/> to compare with this instance</param>
			/// <returns>
			/// <c>true</c> if the specified <see cref="Matrix3Dx2f"/> is equal to this instance; otherwise, <c>false</c>
			/// </returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(ref Matrix3Dx2f other)
			{
				return (XMath.Approximately(other.M11, M11) &&
					XMath.Approximately(other.M12, M12) &&
					XMath.Approximately(other.M21, M21) &&
					XMath.Approximately(other.M22, M22) &&
					XMath.Approximately(other.M31, M31) &&
					XMath.Approximately(other.M32, M32));
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
					hash_code = (hash_code * 397) ^ M21.GetHashCode();
					hash_code = (hash_code * 397) ^ M22.GetHashCode();
					hash_code = (hash_code * 397) ^ M31.GetHashCode();
					hash_code = (hash_code * 397) ^ M32.GetHashCode();
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
				return String.Format(CultureInfo.CurrentCulture, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]",
					M11, M12, M21, M22, M31, M32);
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

				return String.Format(format, CultureInfo.CurrentCulture, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]",
					M11.ToString(format, CultureInfo.CurrentCulture), M12.ToString(format, CultureInfo.CurrentCulture),
					M21.ToString(format, CultureInfo.CurrentCulture), M22.ToString(format, CultureInfo.CurrentCulture),
					M31.ToString(format, CultureInfo.CurrentCulture), M32.ToString(format, CultureInfo.CurrentCulture));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="format_provider">Интерфейс провайдера формата значения компонента</param>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//----------------------------------------------------------------------------------------------------------
			public String ToString(IFormatProvider format_provider)
			{
				return String.Format(format_provider, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]",
					M11.ToString(format_provider), M12.ToString(format_provider),
					M21.ToString(format_provider), M22.ToString(format_provider),
					M31.ToString(format_provider), M32.ToString(format_provider));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="format">Формат отображения значения компонента</param>
			/// <param name="format_provider">Интерфейс провайдера формата значения компонента</param>
			/// <returns>
			/// Текстовое представление матрицы с указание значений компонентов
			/// </returns>
			//----------------------------------------------------------------------------------------------------------
			public String ToString(String format, IFormatProvider format_provider)
			{
				if (format == null)
				{
					return ToString(format_provider);
				}

				return String.Format(format, format_provider, "[M11:{0} M12:{1}] [M21:{2} M22:{3}] [M31:{4} M32:{5}]",
					M11.ToString(format, format_provider), M12.ToString(format, format_provider),
					M21.ToString(format, format_provider), M22.ToString(format, format_provider),
					M31.ToString(format, format_provider), M32.ToString(format, format_provider));
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
			public static Matrix3Dx2f operator +(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Add(ref left, ref right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Assert a matrix (return it unchanged)
			/// </summary>
			/// <param name="value">The matrix to assert (unchanged)</param>
			/// <returns>The asserted (unchanged) matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f operator +(Matrix3Dx2f value)
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
			public static Matrix3Dx2f operator -(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Subtract(ref left, ref right, out result);
				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Negates a matrix
			/// </summary>
			/// <param name="value">The matrix to negate</param>
			/// <returns>The negated matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Matrix3Dx2f operator -(Matrix3Dx2f value)
			{
				Matrix3Dx2f result;
				Negate(ref value, out result);
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
			public static Matrix3Dx2f operator *(Single left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Multiply(ref right, left, out result);
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
			public static Matrix3Dx2f operator *(Matrix3Dx2f left, Single right)
			{
				Matrix3Dx2f result;
				Multiply(ref left, right, out result);
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
			public static Matrix3Dx2f operator *(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Multiply(ref left, ref right, out result);
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
			public static Matrix3Dx2f operator /(Matrix3Dx2f left, Single right)
			{
				Matrix3Dx2f result;
				Divide(ref left, right, out result);
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
			public static Matrix3Dx2f operator /(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				Matrix3Dx2f result;
				Divide(ref left, ref right, out result);
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
			public static Boolean operator ==(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				return left.Equals(ref right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Tests for inequality between two objects
			/// </summary>
			/// <param name="left">The first value to compare</param>
			/// <param name="right">The second value to compare</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c></returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(Matrix3Dx2f left, Matrix3Dx2f right)
			{
				return !left.Equals(ref right);
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
			public static implicit operator System.Windows.Media.Matrix(Matrix3Dx2f matrix)
			{
				return (new System.Windows.Media.Matrix
				{
					M11 = matrix.M11,
					M12 = matrix.M12,
					M21 = matrix.M21,
					M22 = matrix.M22,
					OffsetX = matrix.M31,
					OffsetY = matrix.M32
				});
			}
#endif
#if USE_GDI_EX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа матрицы трансформации System.Drawing
			/// </summary>
			/// <param name="matrix">Двухмерная матрица размерностью 3х2</param>
			/// <returns>Матрица трансформации System.Drawing</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator System.Drawing.Drawing2D.Matrix(Matrix3Dx2f matrix)
			{
				return (new System.Drawing.Drawing2D.Matrix(matrix.M11, matrix.M12,
					matrix.M21, matrix.M22, matrix.M31, matrix.M32));
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
						case 2: return M21;
						case 3: return M22;
						case 4: return M31;
						case 5: return M32;
						default: return 0;
					}
				}

				set
				{
					switch (index)
					{
						case 0: M11 = value; break;
						case 1: M12 = value; break;
						case 2: M21 = value; break;
						case 3: M22 = value; break;
						case 4: M31 = value; break;
						case 5: M32 = value; break;
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
					return this[(row * 2) + column];
				}

				set
				{
					this[(row * 2) + column] = value;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Creates an array containing the elements of the matrix
			/// </summary>
			/// <returns>A sixteen-element array containing the components of the matrix</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single[] ToArray()
			{
				return new[] { M11, M12, M21, M22, M31, M32 };
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the determinant of this matrix
			/// </summary>
			/// <returns>Result of the determinant</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single Determinant()
			{
				return (M11 * M22) - (M12 * M21);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Calculates the inverse of this matrix instance
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Invert()
			{
				Invert(ref this, out this);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================