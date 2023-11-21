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
			protected internal Int32 _columnCount;
			protected internal Int32 _rowCount;
			protected internal Single _columnWidth;
			protected internal Single _rowHeight;
			protected internal Boolean _isClosedColumn;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Количество столбцов
			/// </summary>
			public Int32 ColumnCount
			{
				get
				{
					return _columnCount;
				}

				set
				{
					_columnCount = value;
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
					return _rowCount;
				}

				set
				{
					_rowCount = value;
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
					return _columnWidth;
				}

				set
				{
					_columnWidth = value;
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
					return _rowHeight;
				}

				set
				{
					_rowHeight = value;
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
					return _isClosedColumn;
				}

				set
				{
					_isClosedColumn = value;
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
				_name = "RegularGrid3D";
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
				Vector3Df pivot = _vertices[0].Position;

				// Считаем необходимое количество вершин 
				//Int32 count = ((_columnCount + 1) * (_rowCount + 1));

				// Заполняем вершины
				_vertices.Clear();
				for (var ir = 0; ir < _rowCount + 1; ir++)
				{
					for (var ic = 0; ic < _columnCount + 1; ic++)
					{
						Vector3Df next_point = pivot + GetPlaneVector(ic * _columnWidth, ir * _rowHeight);
						_vertices.AddVertex(next_point);
					}
				}

				// Заполняем треугольники
				_triangles.Clear();
				_triangles.AddTriangleRegularGrid(0, _columnCount, _rowCount, _isClosedColumn);

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
				_planeType = Maths.TDimensionPlane.XZ;
				_rowCount = rowCount;
				_columnCount = columnCount;
				_columnWidth = columnWidth;
				_rowHeight = rowHeight;
				_isClosedColumn = isClosedColumn;

				_vertices.Clear();
				_vertices.AddVertex(pivot);

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
				_planeType = Maths.TDimensionPlane.ZY;
				_rowCount = rowCount;
				_columnCount = columnCount;
				_columnWidth = columnWidth;
				_rowHeight = rowHeight;
				_isClosedColumn = isClosedColumn;

				_vertices.Clear();
				_vertices.AddVertex(pivot);

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
				_planeType = Maths.TDimensionPlane.XY;
				_rowCount = rowCount;
				_columnCount = columnCount;
				_columnWidth = columnWidth;
				_rowHeight = rowHeight;
				_isClosedColumn = isClosedColumn;

				_vertices.Clear();
				_vertices.AddVertex(pivot);

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
				_planeType = Maths.TDimensionPlane.XZ;

				_columnCount = pointList.Count - 1;
				_rowCount = rowCount;
				_rowHeight = rowHeight;
				_isClosedColumn = isClosedColumn;

				// Считаем необходимое количество вершин 
				// Int32 count = ((_columnCount + 1) * (_rowCount + 1));

				// Заполняем вершины
				_vertices.Clear();
				for (var ir = 0; ir < _rowCount + 1; ir++)
				{
					for (var ic = 0; ic < _columnCount + 1; ic++)
					{
						Vector3Df next_point = pointList[ic] + GetPerpendicularVector() * rowHeight * ir;
						_vertices.AddVertex(next_point);
					}
				}

				// Заполняем треугольники
				_triangles.Clear();
				_triangles.AddTriangleRegularGrid(0, _columnCount, _rowCount, _isClosedColumn);

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
				var radius = _columnCount * _columnWidth / XMath.PI2_F;
				var horizont_delta = 360.0f / _columnCount;

				var index = 0;
				for (var ir = 0; ir < _rowCount + 1; ir++)
				{
					for (var ic = 0; ic < _columnCount + 1; ic++)
					{
						var angle_in_radians = 360 - ic * horizont_delta;

						var x = radius * XMath.Cos(angle_in_radians);
						var y = radius * XMath.Sin(angle_in_radians);

						_vertices.Vertices[index].Position = GetPlaneVector(x, y, _vertices.Vertices[index].Position);
						_vertices.Vertices[index].Normal = _vertices.Vertices[index].Position.Normalized;
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
				//Single horizont_delta = 360.0f / _columnCount;
				//Single vertical_delta = 180.0f / _rowCount;

				var index = 0;
				for (var ir = 0; ir < _rowCount + 1; ir++)
				{
					for (var ic = 0; ic < _columnCount + 1; ic++)
					{
						// Single angle_horizontal = 360 - ic * horizont_delta;
						// Single angle_vertical = ir * vertical_delta - 90;

						//_vertices.Vertices[index].Position = XGeneration3D.PointOnSphere(radius, angle_horizontal, angle_vertical);
						_vertices.Vertices[index].Normal = _vertices.Vertices[index].Position.Normalized;
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
				for (var ir = 0; ir < _rowCount; ir++)
				{
					for (var ic = 0; ic < _columnCount; ic++)
					{
						var iv0 = ic + (_columnCount + 1) * ir;
						var iv1 = iv0 + 1;
						var iv2 = iv0 + _columnCount + 1;
						var iv3 = iv2 + 1;

						Vector3Df down = _vertices.Vertices[iv2].Position - _vertices.Vertices[iv0].Position;
						Vector3Df right = _vertices.Vertices[iv1].Position - _vertices.Vertices[iv0].Position;

						Vector3Df normal = Vector3Df.Cross(in down, in right).Normalized;

						_vertices.Vertices[iv0].Normal = normal;
						_vertices.Vertices[iv1].Normal = normal;
						_vertices.Vertices[iv2].Normal = normal;
						_vertices.Vertices[iv3].Normal = normal;
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
				for (var ir = 0; ir <= _rowCount; ir++)
				{
					for (var ic = 0; ic <= _columnCount; ic++)
					{
						var u = ic / (Single)_columnCount;
						var v = ir / (Single)_rowCount;

						_vertices.Vertices[index].UV = new Vector2Df(u, v);
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