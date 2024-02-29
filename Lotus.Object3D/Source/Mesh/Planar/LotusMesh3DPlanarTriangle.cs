using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshPlanar
	*@{*/
    /// <summary>
    /// Базовый плоскостной трехмерный примитив - треугольник.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Топология вершин:
    /// 1)-------2)
    /// |      /
    /// |    /
    /// |  /
    /// |/
    /// 0)
    /// </para>
    /// <para>
    /// Треугольник: 0, 1, 2
    /// </para>
    /// <para>
    /// Опорная точка треугольника – первая вершина (индекс у которой вершины - 0)
    /// </para>
    /// </remarks>
    [Serializable]
    public class MeshPlanarTriangle3Df : MeshPlanar3Df
    {
        #region Static methods
        /// <summary>
        /// Создание треугольника по трем вершинам.
        /// </summary>
        /// <remarks>
        /// Задавать вершины нужно по часовой стрелки.
        /// </remarks>
        /// <param name="p1">Первая вершина треугольника.</param>
        /// <param name="p2">Вторая вершина треугольника.</param>
        /// <param name="p3">Третья вершина треугольника.</param>
        /// <returns>Треугольник.</returns>
        public static MeshPlanarTriangle3Df CreateOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3)
        {
            var mesh = new MeshPlanarTriangle3Df(p1, p2, p3);
            return mesh;
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPlanarTriangle3Df()
            : base()
        {
            _name = "Triangle3D";
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <remarks>
        /// Задавать вершины нужно по часовой стрелки.
        /// </remarks>
        /// <param name="p1">Первая вершина треугольника.</param>
        /// <param name="p2">Вторая вершина треугольника.</param>
        /// <param name="p3">Третья вершина треугольника.</param>
        public MeshPlanarTriangle3Df(Vector3Df p1, Vector3Df p2, Vector3Df p3)
            : base()
        {
            CreateTriangleOfPoint(p1, p2, p3);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Создание треугольника по трем вершинам.
        /// </summary>
        /// <remarks>
        /// Задавать вершины нужно по часовой стрелки.
        /// </remarks>
        /// <param name="p1">Первая вершина треугольника.</param>
        /// <param name="p2">Вторая вершина треугольника.</param>
        /// <param name="p3">Третья вершина треугольника.</param>
        public void CreateTriangleOfPoint(Vector3Df p1, Vector3Df p2, Vector3Df p3)
        {
            _vertices.Clear();
            _vertices.AddVertex(p1);
            _vertices.AddVertex(p2);
            _vertices.AddVertex(p3);

            _triangles.Clear();
            _triangles.AddTriangle(0, 1, 2);

            this.ComputeNormals();
            this.ComputeUVMap();
        }

        /// <summary>
        /// Обновление данных меша.
        /// </summary>
        public override void UpdateData()
        {
            ComputeNormals();
        }

        /// <summary>
        /// Вычисление нормалей для треугольника.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            var iv0 = _vertices.Count - 3;
            var iv1 = _vertices.Count - 2;
            var iv2 = _vertices.Count - 1;

            var down = _vertices.Vertices[iv1].Position - _vertices.Vertices[iv0].Position;
            var right = _vertices.Vertices[iv2].Position - _vertices.Vertices[iv0].Position;

            var normal = Vector3Df.Cross(in down, in right).Normalized;

            _vertices.Vertices[iv0].Normal = normal;
            _vertices.Vertices[iv1].Normal = normal;
            _vertices.Vertices[iv2].Normal = normal;
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для треугольника.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public override void ComputeUVMap(int channel = 0)
        {
            _vertices.Vertices[0].UV = XGeometry2D.MapUVBottomLeft;
            _vertices.Vertices[1].UV = XGeometry2D.MapUVTopLeft;
            _vertices.Vertices[2].UV = XGeometry2D.MapUVTopRight;
        }
        #endregion
    }
    /**@}*/
}