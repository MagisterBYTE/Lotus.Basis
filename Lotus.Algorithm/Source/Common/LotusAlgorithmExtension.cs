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
// Последнее изменение от 30.04.2023
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
		/**
         * \defgroup AlgorithmCommon Подсистема интерфейсов
         * \ingroup Algorithm
         * \brief Общая подсистема содержит общие данные по алгоритмам.
         * @{
         */
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
				IEqualityComparer<TType>? comparer = null)
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
			/// <param name="startX">Координата начальной точки по X</param>
			/// <param name="startY">Координата начальной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit4<TType>(this TType[,] massive, Int32 startX, Int32 startY, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType>? comparer = null)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				var length_x = massive.GetLength(0);
				var length_y = massive.GetLength(1);

				if (startX < 0 || startX >= length_x) throw new ArgumentOutOfRangeException(nameof(startX));
				if (startY < 0 || startY >= length_y) throw new ArgumentOutOfRangeException(nameof(startY));

				if (comparer == null)
				{
					comparer = EqualityComparer<TType>.Default;
				}

				var processed = new Boolean[length_x, length_y];
				TType value = massive[startX, startY];

				var queue = new Queue<Vector2Di>();
				queue.Enqueue(new Vector2Di(startX, startY));
				processed[startX, startY] = true;

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
				IEqualityComparer<TType>? comparer = null)
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
			/// <param name="startX">Координата начальной точки по X</param>
			/// <param name="startY">Координата начальной точки по Y</param>
			/// <param name="visitorDelegate">Делегат вызываемый при посещении точки</param>
			/// <param name="comparer">Компаратор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void FloodVisit8<TType>(this TType[,] massive, Int32 startX, Int32 startY, Action<Int32, Int32> visitorDelegate,
				IEqualityComparer<TType>? comparer = null)
			{
				if (massive == null) throw new ArgumentNullException(nameof(massive));
				if (visitorDelegate == null) throw new ArgumentNullException(nameof(visitorDelegate));

				var length_x = massive.GetLength(0);
				var length_y = massive.GetLength(1);

				if (startX < 0 || startX >= length_x) throw new ArgumentOutOfRangeException(nameof(startX));
				if (startY < 0 || startY >= length_y) throw new ArgumentOutOfRangeException(nameof(startY));

				if (comparer == null)
				{
					comparer = EqualityComparer<TType>.Default;
				}

				var processed = new Boolean[length_x, length_y];
				TType value = massive[startX, startY];

				var queue = new Queue<Vector2Di>();
				queue.Enqueue(new Vector2Di(startX, startY));
				processed[startX, startY] = true;

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

					var xGreaterThanZero = cell.X > 0;
					var xLessThanWidth = cell.X + 1 < length_x;

					var yGreaterThanZero = cell.Y > 0;
					var yLessThanHeight = cell.Y + 1 < length_y;

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

				var xGreaterThanZero = x > 0;
				var xLessThanWidth = x + 1 < massive.GetLength(0);

				var yGreaterThanZero = y > 0;
				var yLessThanHeight = y + 1 < massive.GetLength(1);

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
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================