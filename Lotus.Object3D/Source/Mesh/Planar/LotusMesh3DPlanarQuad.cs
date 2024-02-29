using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshPlanar
	*@{*/
    /// <summary>
    /// Плоскостной трехмерный примитив - четырехугольник.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Топология вершин:
    /// 2)------- 3)
    /// |       / |
    /// |     /   |
    /// |   /     |
    /// | /       |
    /// 0)--------1)
    /// </para>
    /// <para>
    /// Первый треугольник: 0, 2, 3
    /// Второй треугольник: 0, 3, 1
    /// </para>
    /// <para>
    /// Опорная точка четырёхугольника – его геометрический центр
    /// </para>
    /// </remarks>
    [Serializable]
    public class MeshPlanarQuad3Df : MeshPlanar3Df
    {
        #region Static methods
        /// <summary>
        /// Создание четырехугольника по четырем вершинам.
        /// </summary>
        /// <param name="p1">Первая вершина четырехугольника.</param>
        /// <param name="p2">Вторая вершина четырехугольника.</param>
        /// <param name="p3">Третья вершина четырехугольника.</param>
        /// <param name="p4">Четверта вершина четырехугольника.</param>
        /// <returns>Четырехугольник.</returns>
        public static MeshPlanarQuad3Df CreateOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
        {
            var mesh = new MeshPlanarQuad3Df(p1, p2, p3, p4);
            return mesh;
        }

        /// <summary>
        /// Создание четырехугольника в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Z
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        /// <returns>Четырехугольник.</returns>
        public static MeshPlanarQuad3Df CreateXZ(Vector3Df pivot, float width, float height)
        {
            var mesh = new MeshPlanarQuad3Df();
            mesh.CreateQuadXZ(pivot, width, height);
            return mesh;
        }

        /// <summary>
        /// Создание четырехугольника в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <remarks>
        /// Ширина по оси Z
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        /// <returns>Четырехугольник.</returns>
        public static MeshPlanarQuad3Df CreateZY(Vector3Df pivot, float width, float height)
        {
            var mesh = new MeshPlanarQuad3Df();
            mesh.CreateQuadZY(pivot, width, height);
            return mesh;
        }

        /// <summary>
        /// Создание четырехугольника в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        /// <returns>Четырехугольник.</returns>
        public static MeshPlanarQuad3Df CreateXY(Vector3Df pivot, float width, float height)
        {
            var mesh = new MeshPlanarQuad3Df();
            mesh.CreateQuadXY(pivot, width, height);
            return mesh;
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// Ширина четырехугольника.
        /// </summary>
        public float Width
        {
            get
            {
                return Vector3Df.Distance(in _vertices.Vertices[1].Position, in _vertices.Vertices[0].Position);
            }

            set
            {
                // Получаем направление
                var dir = (_vertices.Vertices[1].Position - _vertices.Vertices[0].Position).Normalized;

                // Смещаем 1 вершину от 0
                _vertices.Vertices[1].Position = _vertices.Vertices[0].Position + (dir * value);

                // Смещаем 3 вершину от 2
                _vertices.Vertices[3].Position = _vertices.Vertices[2].Position + (dir * value);

                UpdateData();

                //Centering();
            }
        }

        /// <summary>
        /// Высота четырехугольника.
        /// </summary>
        public float Height
        {
            get
            {
                return Vector3Df.Distance(in _vertices.Vertices[2].Position, in _vertices.Vertices[0].Position);
            }

            set
            {

                // Получаем направление
                var dir = (_vertices.Vertices[2].Position - _vertices.Vertices[0].Position).Normalized;

                // Смещаем 2 вершину от 0
                _vertices.Vertices[2].Position = _vertices.Vertices[0].Position + (dir * value);

                // Смещаем 3 вершину от 1
                _vertices.Vertices[3].Position = _vertices.Vertices[1].Position + (dir * value);

                UpdateData();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPlanarQuad3Df()
            : base()
        {
            _name = "Quad3D";
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="p1">Первая вершина четырехугольника.</param>
        /// <param name="p2">Вторая вершина четырехугольника.</param>
        /// <param name="p3">Третья вершина четырехугольника.</param>
        /// <param name="p4">Четверта вершина четырехугольника.</param>
        public MeshPlanarQuad3Df(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
            : base()
        {
            _name = "Quad3D";
            CreateQuadOfPoint(p1, p2, p3, p4);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Создание четырехугольника по четырем вершинам.
        /// </summary>
        /// <param name="p1">Первая вершина четырехугольника.</param>
        /// <param name="p2">Вторая вершина четырехугольника.</param>
        /// <param name="p3">Третья вершина четырехугольника.</param>
        /// <param name="p4">Четверта вершина четырехугольника.</param>
        public void CreateQuadOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3, Vector3Df p4)
        {
            _vertices.Clear();
            _vertices.AddVertex(p1);
            _vertices.AddVertex(p2);
            _vertices.AddVertex(p3);
            _vertices.AddVertex(p4);

            _triangles.Clear();
            _triangles.AddTriangleQuad();

            this.ComputeNormals();
            this.ComputeUVMap();
        }

        /// <summary>
        /// Создание четырехугольника в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Z
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        public void CreateQuadXZ(Vector3Df pivot, float width, float height)
        {
            _planeType = Maths.TDimensionPlane.XZ;

            _vertices.Clear();
            _vertices.AddVertex(pivot + new Vector3Df(-width / 2, 0, -height / 2));
            _vertices.AddVertex(pivot + new Vector3Df(width / 2, 0, -height / 2));
            _vertices.AddVertex(pivot + new Vector3Df(-width / 2, 0, height / 2));
            _vertices.AddVertex(pivot + new Vector3Df(width / 2, 0, height / 2));

            _triangles.Clear();
            _triangles.AddTriangleQuad();

            this.ComputeNormals();
            this.ComputeUVMap();

        }

        /// <summary>
        /// Создание четырехугольника в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <remarks>
        /// Ширина по оси Z
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        public void CreateQuadZY(Vector3Df pivot, float width, float height)
        {
            _planeType = Maths.TDimensionPlane.ZY;

            _vertices.Clear();
            _vertices.AddVertex(pivot + new Vector3Df(0, -height / 2, -width / 2));
            _vertices.AddVertex(pivot + new Vector3Df(0, -height / 2, width / 2));
            _vertices.AddVertex(pivot + new Vector3Df(0, height / 2, -width / 2));
            _vertices.AddVertex(pivot + new Vector3Df(0, height / 2, width / 2));

            _triangles.Clear();
            _triangles.AddTriangleQuad();

            this.ComputeNormals();
            this.ComputeUVMap();

        }

        /// <summary>
        /// Создание четырехугольника в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <remarks>
        /// Ширина по оси X
        /// Высота по оси Y
        /// </remarks>
        /// <param name="pivot">Опорная точка четырехугольника.</param>
        /// <param name="width">Ширина четырехугольника.</param>
        /// <param name="height">Высота четырехугольника.</param>
        public void CreateQuadXY(Vector3Df pivot, float width, float height)
        {
            _planeType = Maths.TDimensionPlane.XY;

            _vertices.Clear();
            _vertices.AddVertex(pivot + new Vector3Df(-width / 2, -height / 2, 0));
            _vertices.AddVertex(pivot + new Vector3Df(width / 2, -height / 2, 0));
            _vertices.AddVertex(pivot + new Vector3Df(-width / 2, height / 2, 0));
            _vertices.AddVertex(pivot + new Vector3Df(width / 2, height / 2, 0));

            _triangles.Clear();
            _triangles.AddTriangleQuad();

            this.ComputeNormals();
            this.ComputeUVMap();

        }

        /// <summary>
        /// Вычисление нормалей для четырехугольника.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            var down = _vertices.Vertices[2].Position - _vertices.Vertices[0].Position;
            var right = _vertices.Vertices[1].Position - _vertices.Vertices[0].Position;

            var normal = Vector3Df.Cross(in down, in right).Normalized;

            _vertices.Vertices[0].Normal = normal;
            _vertices.Vertices[1].Normal = normal;
            _vertices.Vertices[2].Normal = normal;
            _vertices.Vertices[3].Normal = normal;
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для четырехугольника.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public override void ComputeUVMap(int channel = 0)
        {
            _vertices.Vertices[0].UV = XGeometry2D.MapUVBottomLeft;
            _vertices.Vertices[1].UV = XGeometry2D.MapUVBottomRight;
            _vertices.Vertices[2].UV = XGeometry2D.MapUVTopLeft;
            _vertices.Vertices[3].UV = XGeometry2D.MapUVTopRight;
        }
        #endregion
    }
    /**@}*/
}