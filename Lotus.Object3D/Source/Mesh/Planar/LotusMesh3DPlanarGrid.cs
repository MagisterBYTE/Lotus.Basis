using System;
using System.Collections.Generic;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshPlanar
	*@{*/
    /// <summary>
    /// Плоскостной трехмерный примитив - регулярная сетка.
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
    /// Опорная точка сетки – её первая вершина (индекс вершины равен нулю).
    /// </para>
    /// </remarks>
    [Serializable]
    public class MeshPlanarGrid3Df : MeshPlanar3Df
    {
        #region Static methods
        /// <summary>
        /// Создание регулярной сетки в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Z
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        /// <returns>Регулярная сетка.</returns>
        public static MeshPlanarGrid3Df CreateXZ(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
        {
            var planar_grid = new MeshPlanarGrid3Df();
            planar_grid.CreateGridXZ(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
            return planar_grid;
        }

        /// <summary>
        /// Создание регулярной сетки в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <remarks>
        /// Ширина по оси Z
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        /// <returns>Регулярная сетка.</returns>
        public static MeshPlanarGrid3Df CreateZY(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
        {
            var planar_grid = new MeshPlanarGrid3Df();
            planar_grid.CreateGridZY(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
            return planar_grid;
        }

        /// <summary>
        /// Создание регулярной сетки в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        /// <returns>Регулярная сетка.</returns>
        public static MeshPlanarGrid3Df CreateXY(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
        {
            var planar_grid = new MeshPlanarGrid3Df();
            planar_grid.CreateGridXY(pivot, columnCount, rowCount, columnWidth, rowHeight, isClosedColumn);
            return planar_grid;
        }
        #endregion

        #region Fields
        // Основные параметры
        protected internal int _columnCount;
        protected internal int _rowCount;
        protected internal float _columnWidth;
        protected internal float _rowHeight;
        protected internal bool _isClosedColumn;
        #endregion

        #region Properties
        /// <summary>
        /// Количество столбцов.
        /// </summary>
        public int ColumnCount
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
        /// Количество строк.
        /// </summary>
        public int RowCount
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
        /// Ширина столбца.
        /// </summary>
        public float ColumnWidth
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
        /// Высота строки.
        /// </summary>
        public float RowHeight
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
        /// Статус соединения последнего столбца с первым (т.е. по ширине сетки).
        /// </summary>
        public bool IsClosedColumn
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

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPlanarGrid3Df()
            : base()
        {
            _name = "RegularGrid3D";
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Создание регулярной сетки на основе внутренних данных.
        /// </summary>
        protected virtual void CreateRegularGrid()
        {
            // Сохраняем опорную точку
            var pivot = _vertices[0].Position;

            // Считаем необходимое количество вершин 
            //Int32 count = ((_columnCount + 1) * (_rowCount + 1));

            // Заполняем вершины
            _vertices.Clear();
            for (var ir = 0; ir < _rowCount + 1; ir++)
            {
                for (var ic = 0; ic < _columnCount + 1; ic++)
                {
                    var next_point = pivot + GetPlaneVector(ic * _columnWidth, ir * _rowHeight);
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

        #region Main methods
        /// <summary>
        /// Создание регулярной сетки в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Z
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        public void CreateGridXZ(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
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

        /// <summary>
        /// Создание регулярной сетки в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <remarks>
        /// Ширина по оси Z
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        public void CreateGridZY(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
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

        /// <summary>
        /// Создание регулярной сетки в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка сетки (нижний-левый угол).</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="columnWidth">Ширина столбца.</param>
        /// <param name="rowHeight">Высота строки.</param>
        /// <param name="isClosedColumn">Статус замыкания по ширине.</param>
        public void CreateGridXY(Vector3Df pivot, int columnCount, int rowCount, float columnWidth,
                float rowHeight, bool isClosedColumn = false)
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

        /// <summary>
        /// Создание регулярной сетки в плоскости XZ.
        /// </summary>
        /// <param name="pointList"></param>
        /// <param name="rowCount"></param>
        /// <param name="rowHeight"></param>
        /// <param name="isClosedColumn"></param>
        public void CreateFromPointListXZ(IList<Vector3Df> pointList, int rowCount, float rowHeight, bool isClosedColumn = false)
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
                    var next_point = pointList[ic] + (GetPerpendicularVector() * rowHeight * ir);
                    _vertices.AddVertex(next_point);
                }
            }

            // Заполняем треугольники
            _triangles.Clear();
            _triangles.AddTriangleRegularGrid(0, _columnCount, _rowCount, _isClosedColumn);

            this.ComputeNormals();
            this.ComputeUVMap();
        }

        /// <summary>
        /// Свернуть сетку в цилиндр.
        /// </summary>
        /// <remarks>
        /// Сетка сворачивается по ширине.
        /// </remarks>
        public void MinimizeToCylinder()
        {
            var radius = _columnCount * _columnWidth / XMath.PI2_F;
            var horizont_delta = 360.0f / _columnCount;

            var index = 0;
            for (var ir = 0; ir < _rowCount + 1; ir++)
            {
                for (var ic = 0; ic < _columnCount + 1; ic++)
                {
                    var angle_in_radians = 360 - (ic * horizont_delta);

                    var x = radius * XMath.Cos(angle_in_radians);
                    var y = radius * XMath.Sin(angle_in_radians);

                    _vertices.Vertices[index].Position = GetPlaneVector(x, y, _vertices.Vertices[index].Position);
                    _vertices.Vertices[index].Normal = _vertices.Vertices[index].Position.Normalized;
                    index++;
                }
            }
        }

        /// <summary>
        /// Свернуть сетку в сферу.
        /// </summary>
        /// <param name="radius">Радиус сферы.</param>
        public void MinimizeToSphere(float radius)
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

        /// <summary>
        /// Вычисление нормалей для сетки.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            for (var ir = 0; ir < _rowCount; ir++)
            {
                for (var ic = 0; ic < _columnCount; ic++)
                {
                    var iv0 = ic + ((_columnCount + 1) * ir);
                    var iv1 = iv0 + 1;
                    var iv2 = iv0 + _columnCount + 1;
                    var iv3 = iv2 + 1;

                    var down = _vertices.Vertices[iv2].Position - _vertices.Vertices[iv0].Position;
                    var right = _vertices.Vertices[iv1].Position - _vertices.Vertices[iv0].Position;

                    var normal = Vector3Df.Cross(in down, in right).Normalized;

                    _vertices.Vertices[iv0].Normal = normal;
                    _vertices.Vertices[iv1].Normal = normal;
                    _vertices.Vertices[iv2].Normal = normal;
                    _vertices.Vertices[iv3].Normal = normal;
                }
            }
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для сетки.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public override void ComputeUVMap(int channel = 0)
        {
            var index = 0;
            for (var ir = 0; ir <= _rowCount; ir++)
            {
                for (var ic = 0; ic <= _columnCount; ic++)
                {
                    var u = ic / (float)_columnCount;
                    var v = ir / (float)_rowCount;

                    _vertices.Vertices[index].UV = new Vector2Df(u, v);
                    index++;
                }
            }
        }
        #endregion
    }
    /**@}*/
}