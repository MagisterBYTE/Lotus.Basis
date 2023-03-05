//=====================================================================================================================
// Проект: Модуль алгоритмов
// Раздел: Общая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAlgorithmExtension.cs
*		Методы расширения функциональности базовых классов и структурных типов применительно к алгоритмам.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Algorithm
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup AlgorithmCommon Общая подсистема
		//! Общая подсистема содержит общие данные по алгоритмам
		//! \ingroup Algorithm
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для расширения функциональности базовых классов и структурных типов применительно к алгоритмам
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XAlgorithmExtension
		{
			#region ======================================= FloodVisit4 ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм заливки прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Flood_fill
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit4<TType>(this TType[,] massive, Vector2Di start, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType> comparer = null)
			{
				FloodVisit4(massive, start.X, start.Y, visitorDelegate, comparer);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм заливки прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Flood_fill
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="start_x">Координата начальной точки по X</param>
			/// <param name="start_y">Координата начальной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit4<TType>(this TType[,] massive, Int32 start_x, Int32 start_y, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType> comparer = null)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				Int32 length_x = massive.GetLength(0);
				Int32 length_y = massive.GetLength(1);

				if (start_x < 0 || start_x >= length_x) throw new ArgumentOutOfRangeException("startX");
				if (start_y < 0 || start_y >= length_y) throw new ArgumentOutOfRangeException("startY");

				if (comparer == null)
				{
					comparer = EqualityComparer<TType>.Default;
				}

				Boolean[,] processed = new Boolean[length_x, length_y];
				TType value = massive[start_x, start_y];

				var queue = new Queue<Vector2Di>();
				queue.Enqueue(new Vector2Di(start_x, start_y));
				processed[start_x, start_y] = true;

				Action<Int32, Int32> process = (x, y) =>
				{
					if (!processed[x, y])
					{
						if (comparer.Equals(massive[x, y], value))
						{
							queue.Enqueue(new Vector2Di(x, y));
						}
						processed[x, y] = true;
					}
				};

				while (queue.Count > 0)
				{
					Vector2Di cell = queue.Dequeue();

					if (cell.X > 0)
					{
						process(cell.X - 1, cell.Y);
					}
					if (cell.X + 1 < length_x)
					{
						process(cell.X + 1, cell.Y);
					}
					if (cell.Y > 0)
					{
						process(cell.X, cell.Y - 1);
					}
					if (cell.Y + 1 < length_y)
					{
						process(cell.X, cell.Y + 1);
					}

					visitorDelegate(cell.X, cell.Y);
				}
			}
			#endregion

			#region ======================================= FloodVisit8 ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм заливки прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Flood_fill
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="start">Начальная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit8<TType>(this TType[,] massive, Vector2Di start, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType> comparer = null)
			{
				FloodVisit4(massive, start.X, start.Y, visitorDelegate, comparer);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм заливки прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Flood_fill
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="start_x">Координата начальной точки по X</param>
			/// <param name="start_y">Координата начальной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit8<TType>(this TType[,] massive, Int32 start_x, Int32 start_y, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType> comparer = null)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				Int32 length_x = massive.GetLength(0);
				Int32 length_y = massive.GetLength(1);

				if (start_x < 0 || start_x >= length_x) throw new ArgumentOutOfRangeException("startX");
				if (start_y < 0 || start_y >= length_y) throw new ArgumentOutOfRangeException("startY");

				if (comparer == null)
				{
					comparer = EqualityComparer<TType>.Default;
				}

				Boolean[,] processed = new Boolean[length_x, length_y];
				TType value = massive[start_x, start_y];

				var queue = new Queue<Vector2Di>();
				queue.Enqueue(new Vector2Di(start_x, start_y));
				processed[start_x, start_y] = true;

				Action<Int32, Int32> process = (x, y) =>
				{
					if (!processed[x, y])
					{
						if (comparer.Equals(massive[x, y], value))
						{
							queue.Enqueue(new Vector2Di(x, y));
						}
						processed[x, y] = true;
					}
				};

				while (queue.Count > 0)
				{
					Vector2Di cell = queue.Dequeue();

					Boolean xGreaterThanZero = cell.X > 0;
					Boolean xLessThanWidth = cell.X + 1 < length_x;

					Boolean yGreaterThanZero = cell.Y > 0;
					Boolean yLessThanHeight = cell.Y + 1 < length_y;

					if (yGreaterThanZero)
					{
						if (xGreaterThanZero) process(cell.X - 1, cell.Y - 1);

						process(cell.X, cell.Y - 1);

						if (xLessThanWidth) process(cell.X + 1, cell.Y - 1);
					}

					if (xGreaterThanZero) process(cell.X - 1, cell.Y);
					if (xLessThanWidth) process(cell.X + 1, cell.Y);

					if (yLessThanHeight)
					{
						if (xGreaterThanZero) process(cell.X - 1, cell.Y + 1);

						process(cell.X, cell.Y + 1);

						if (xLessThanWidth) process(cell.X + 1, cell.Y + 1);
					}

					visitorDelegate(cell.X, cell.Y);
				}
			}
			#endregion

			#region ======================================= Visit4 ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Von_Neumann_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="center">Центральная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit4<TType>(this TType[,] massive, Vector2Di center, Action<Int32, Int32> visitorDelegate)
			{
				Visit4(massive, center.X, center.Y, visitorDelegate);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Von_Neumann_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="x">Координата центральной точки по X</param>
			/// <param name="y">Координата центральной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit4<TType>(this TType[,] massive, Int32 x, Int32 y, Action<Int32, Int32> visitorDelegate)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				if (x > 0)
				{
					visitorDelegate(x - 1, y);
				}
				if (x + 1 < massive.GetLength(0))
				{
					visitorDelegate(x + 1, y);
				}
				if (y > 0)
				{
					visitorDelegate(x, y - 1);
				}
				if (y + 1 < massive.GetLength(1))
				{
					visitorDelegate(x, y + 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Von_Neumann_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="center">Центральная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit4Unbounded<TType>(this TType[,] massive, Vector2Di center, Action<Int32, Int32> visitorDelegate)
			{
				Visit4Unbounded(massive, center.X, center.Y, visitorDelegate);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением только по вертикали и горизонтали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Von_Neumann_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="x">Координата центральной точки по X</param>
			/// <param name="y">Координата центральной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit4Unbounded<TType>(this TType[,] massive, Int32 x, Int32 y, Action<Int32, Int32> visitorDelegate)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				visitorDelegate(x - 1, y);
				visitorDelegate(x + 1, y);
				visitorDelegate(x, y - 1);
				visitorDelegate(x, y + 1);
			}
			#endregion

			#region ======================================= Visit8 ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Moore_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="center">Центральная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit8<TType>(this TType[,] massive, Vector2Di center, Action<Int32, Int32> visitorDelegate)
			{
				Visit8(massive, center.X, center.Y, visitorDelegate);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Moore_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="x">Координата центральной точки по X</param>
			/// <param name="y">Координата центральной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit8<TType>(this TType[,] massive, Int32 x, Int32 y, Action<Int32, Int32> visitorDelegate)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				Boolean xGreaterThanZero = x > 0;
				Boolean xLessThanWidth = x + 1 < massive.GetLength(0);

				Boolean yGreaterThanZero = y > 0;
				Boolean yLessThanHeight = y + 1 < massive.GetLength(1);

				if (yGreaterThanZero)
				{
					if (xGreaterThanZero) visitorDelegate(x - 1, y - 1);

					visitorDelegate(x, y - 1);

					if (xLessThanWidth) visitorDelegate(x + 1, y - 1);
				}

				if (xGreaterThanZero) visitorDelegate(x - 1, y);
				if (xLessThanWidth) visitorDelegate(x + 1, y);

				if (yLessThanHeight)
				{
					if (xGreaterThanZero) visitorDelegate(x - 1, y + 1);

					visitorDelegate(x, y + 1);

					if (xLessThanWidth) visitorDelegate(x + 1, y + 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Moore_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="center">Центральная точка</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit8Unbounded<TType>(this TType[,] massive, Vector2Di center, Action<Int32, Int32> visitorDelegate)
			{
				Visit8Unbounded(massive, center.X, center.Y, visitorDelegate);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Алгоритм посещения прямоугольной области с распространением по вертикали, горизонтали и диагонали
			/// </summary>
			/// <remarks>
			/// https://en.wikipedia.org/wiki/Moore_neighborhood
			/// </remarks>
			/// <typeparam name="TType">Тип элемента массива</typeparam>
			/// <param name="massive">Массив</param>
			/// <param name="x">Координата центральной точки по X</param>
			/// <param name="y">Координата центральной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Visit8Unbounded<TType>(this TType[,] massive, Int32 x, Int32 y, Action<Int32, Int32> visitorDelegate)
			{
				if (massive == null) throw new ArgumentNullException(nameof(Array));
				if (visitorDelegate == null) throw new ArgumentNullException("visit");

				visitorDelegate(x - 1, y - 1);
				visitorDelegate(x, y - 1);
				visitorDelegate(x + 1, y - 1);

				visitorDelegate(x - 1, y);
				visitorDelegate(x + 1, y);

				visitorDelegate(x - 1, y + 1);
				visitorDelegate(x, y + 1);
				visitorDelegate(x + 1, y + 1);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================