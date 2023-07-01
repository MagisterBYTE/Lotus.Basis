//=====================================================================================================================
// Проект: Модуль трехмерного объекта
// Раздел: Подсистема мешей
// Подраздел: Плоскостные трехмерные примитивы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMesh3DPlanarGrid.cs
*		Плоскостной трехмерный примитив - регулярная сетка.
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
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Maths;
//=====================================================================================================================
namespace Lotus
{
	namespace Object3D
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup Object3DMeshPlanar
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Плоскостной трехмерный примитив - регулярная сетка
		/// </summary>
		/// <remarks>
		/// <para>
		/// https://en.wikipedia.org/wiki/Triangle_strip
		/// </para>
		/// <para>
		/// Пример сетки 2х3 квадрата
		/// </para>
		/// <para>
		/// 9)------- 10)-------11)
		/// |       / |       / |
		/// |     /   |     /   |
		/// |   /     |   /     |
		/// | /       | /       |
		/// 6)--------7)--------8)
		/// |       / |       / |
		/// |     /   |     /   |
		/// |   /     |   /     |
		/// | /       | /       |
		/// 3)--------4)--------5)
		/// |       / |       / |
		/// |     /   |     /   |
		/// |   /     |   /     |
		/// | /       | /       |
		/// 0)--------1)--------2)
		/// </para>
		/// <para>
		/// Опорная точка сетки – её первая вершина (индекс вершины равен нулю)
		/// </para>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CMeshPlanarGrid3Df : CMeshPlanar3Df
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Z
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			/// <returns>Регулярная сетка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarGrid3Df CreateXZ(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth,
				Single rowHeight, Boolean isClosedColumn = false)
			{
				var planar_grid = new CMeshPlanarGrid3Df();
				planar_grid.CreateGridXZ(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
				return planar_grid;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости ZY (Right - вид справа)
			/// </summary>
			/// <remarks>
			/// Ширина по оси Z
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			/// <returns>Регулярная сетка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarGrid3Df CreateZY(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth,
				Single rowHeight, Boolean isClosedColumn = false)
			{
				var planar_grid = new CMeshPlanarGrid3Df();
				planar_grid.CreateGridZY(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
				return planar_grid;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости XY (Back - вид с сзади)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			/// <returns>Регулярная сетка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMeshPlanarGrid3Df CreateXY(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth,
				Single rowHeight, Boolean isClosedColumn = false)
			{
				var planar_grid = new CMeshPlanarGrid3Df();
				planar_grid.CreateGridXY(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
				return planar_grid;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			internal Int32 mColumnCount;
			internal Int32 mRowCount;
			internal Single mColumnWidth;
			internal Single mRowHeight;
			internal Boolean mIsClosedColumn;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Количество столбцов
			/// </summary>
			public Int32 ColumnCount
			{
				get
				{
					return mColumnCount;
				}

				set
				{
					mColumnCount = value;
					//CreateRegularGrid();
				}
			}

			/// <summary>
			/// Количество строк
			/// </summary>
			public Int32 RowCount
			{
				get
				{
					return mRowCount;
				}

				set
				{
					mRowCount = value;
					//CreateRegularGrid();
				}
			}

			/// <summary>
			/// Ширина столбца
			/// </summary>
			public Single ColumnWidth
			{
				get
				{
					return mColumnWidth;
				}

				set
				{
					mColumnWidth = value;
					//CreateRegularGrid();
				}
			}

			/// <summary>
			/// Высота строки
			/// </summary>
			public Single RowHeight
			{
				get
				{
					return mRowHeight;
				}

				set
				{
					mRowHeight = value;
					//CreateRegularGrid();
				}
			}

			/// <summary>
			/// Статус соединения последнего столбца с первым (т.е. по ширине сетки)
			/// </summary>
			public Boolean IsClosedColumn
			{
				get
				{
					return mIsClosedColumn;
				}

				set
				{
					mIsClosedColumn = value;
					//CreateRegularGrid();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CMeshPlanarGrid3Df()
				:base()
			{
				mName = "RegularGrid3D";
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки на основе внутренних данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void CreateRegularGrid()
			{
				// Сохраняем опорную точку
				Vector3Df pivot = mVertices[0].Position;

				// Считаем необходимое количество вершин 
				//Int32 count = ((mColumnCount + 1) * (mRowCount + 1));

				// Заполняем вершины
				mVertices.Clear();
				for (var ir = 0; ir < mRowCount + 1; ir++)
				{
					for (var ic = 0; ic < mColumnCount + 1; ic++)
					{
						Vector3Df next_point = pivot + GetPlaneVector(ic * mColumnWidth, ir * mRowHeight);
						mVertices.AddVertex(next_point);
					}
				}

				// Заполняем треугольники
				mTriangles.Clear();
				mTriangles.AddTriangleRegularGrid(0, mColumnCount, mRowCount, mIsClosedColumn);

				this.ComputeNormals();
				this.ComputeUVMap();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости XZ (Top - вид сверху)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Z
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateGridXZ(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth, 
				Single rowHeight, Boolean isClosedColumn = false)
			{
				mPlaneType = Maths.TDimensionPlane.XZ;
				mRowCount = rowCount;
				mColumnCount = columnCount;
				mColumnWidth = columnWidth;
				mRowHeight = rowHeight;
				mIsClosedColumn = isClosedColumn;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateRegularGrid();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости ZY (Right - вид справа)
			/// </summary>
			/// <remarks>
			/// Ширина по оси Z
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateGridZY(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth,
				Single rowHeight, Boolean isClosedColumn = false)
			{
				mPlaneType = Maths.TDimensionPlane.ZY;
				mRowCount = rowCount;
				mColumnCount = columnCount;
				mColumnWidth = columnWidth;
				mRowHeight = rowHeight;
				mIsClosedColumn = isClosedColumn;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateRegularGrid();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание регулярной сетки в плоскости XY (Back - вид с сзади)
			/// </summary>
			/// <remarks>
			/// Ширина по оси X
			/// Высота по оси Y
			/// </remarks>
			/// <param name="pivot">Опорная точка сетки (нижний-левый угол)</param>
			/// <param name="columnCount">Количество столбцов</param>
			/// <param name="rowCount">Количество строк</param>
			/// <param name="columnWidth">Ширина столбца</param>
			/// <param name="rowHeight">Высота строки</param>
			/// <param name="isClosedColumn">Статус замыкания по ширине</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateGridXY(Vector3Df pivot, Int32 columnCount, Int32 rowCount, Single columnWidth,
				Single rowHeight, Boolean isClosedColumn = false)
			{
				mPlaneType = Maths.TDimensionPlane.XY;
				mRowCount = rowCount;
				mColumnCount = columnCount;
				mColumnWidth = columnWidth;
				mRowHeight = rowHeight;
				mIsClosedColumn = isClosedColumn;

				mVertices.Clear();
				mVertices.AddVertex(pivot);

				CreateRegularGrid();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// 
			/// </summary>
			/// <param name="pointList"></param>
			/// <param name="rowCount"></param>
			/// <param name="rowHeight"></param>
			/// <param name="isClosedColumn"></param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateFromPointListXZ(IList<Vector3Df> pointList, Int32 rowCount, Single rowHeight, Boolean isClosedColumn = false)
			{
				mPlaneType = Maths.TDimensionPlane.XZ;

				mColumnCount = pointList.Count - 1;
				mRowCount = rowCount;
				mRowHeight = rowHeight;
				mIsClosedColumn = isClosedColumn;

				// Считаем необходимое количество вершин 
				// Int32 count = ((mColumnCount + 1) * (mRowCount + 1));

				// Заполняем вершины
				mVertices.Clear();
				for (var ir = 0; ir < mRowCount + 1; ir++)
				{
					for (var ic = 0; ic < mColumnCount + 1; ic++)
					{
						Vector3Df next_point = pointList[ic] + GetPerpendicularVector() * rowHeight * ir;
						mVertices.AddVertex(next_point);
					}
				}

				// Заполняем треугольники
				mTriangles.Clear();
				mTriangles.AddTriangleRegularGrid(0, mColumnCount, mRowCount, mIsClosedColumn);

				this.ComputeNormals();
				this.ComputeUVMap();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Свернуть сетку в цилиндр
			/// </summary>
			/// <remarks>
			/// Сетка сворачивается по ширине
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void MinimizeToCylinder()
			{
				var radius = mColumnCount * mColumnWidth / XMath.PI2_F;
				var horizont_delta = 360.0f / mColumnCount;

				var index = 0;
				for (var ir = 0; ir < mRowCount + 1; ir++)
				{
					for (var ic = 0; ic < mColumnCount + 1; ic++)
					{
						var angle_in_radians = 360 - ic * horizont_delta;

						var x = radius * XMath.Cos(angle_in_radians);
						var y = radius * XMath.Sin(angle_in_radians);

						mVertices.Vertices[index].Position = GetPlaneVector(x, y, mVertices.Vertices[index].Position);
						mVertices.Vertices[index].Normal = mVertices.Vertices[index].Position.Normalized;
						index++;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Свернуть сетку в сферу
			/// </summary>
			/// <param name="radius">Радиус сферы</param>
			//---------------------------------------------------------------------------------------------------------
			public void MinimizeToSphere(Single radius)
			{
				//Single horizont_delta = 360.0f / mColumnCount;
				//Single vertical_delta = 180.0f / mRowCount;

				var index = 0;
				for (var ir = 0; ir < mRowCount + 1; ir++)
				{
					for (var ic = 0; ic < mColumnCount + 1; ic++)
					{
						// Single angle_horizontal = 360 - ic * horizont_delta;
						// Single angle_vertical = ir * vertical_delta - 90;

						//mVertices.Vertices[index].Position = XGeneration3D.PointOnSphere(radius, angle_horizontal, angle_vertical);
						mVertices.Vertices[index].Normal = mVertices.Vertices[index].Position.Normalized;
						index++;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление нормалей для сетки
			/// </summary>
			/// <remarks>
			/// Нормаль вычисления путем векторного произведения по часовой стрелки
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeNormals()
			{
				for (var ir = 0; ir < mRowCount; ir++)
				{
					for (var ic = 0; ic < mColumnCount; ic++)
					{
						var iv0 = ic + (mColumnCount + 1) * ir;
						var iv1 = iv0 + 1;
						var iv2 = iv0 + mColumnCount + 1;
						var iv3 = iv2 + 1;

						Vector3Df down = mVertices.Vertices[iv2].Position - mVertices.Vertices[iv0].Position;
						Vector3Df right = mVertices.Vertices[iv1].Position - mVertices.Vertices[iv0].Position;

						Vector3Df normal = Vector3Df.Cross(in down, in right).Normalized;

						mVertices.Vertices[iv0].Normal = normal;
						mVertices.Vertices[iv1].Normal = normal;
						mVertices.Vertices[iv2].Normal = normal;
						mVertices.Vertices[iv3].Normal = normal;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление текстурных координат (развертки) для сетки
			/// </summary>
			/// <param name="channel">Канал текстурных координат</param>
			//---------------------------------------------------------------------------------------------------------
			public override void ComputeUVMap(Int32 channel = 0)
			{
				var index = 0;
				for (var ir = 0; ir <= mRowCount; ir++)
				{
					for (var ic = 0; ic <= mColumnCount; ic++)
					{
						var u = ic / (Single)mColumnCount;
						var v = ir / (Single)mRowCount;

						mVertices.Vertices[index].UV = new Vector2Df(u, v);
						index++;
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