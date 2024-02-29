using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshVolume
	*@{*/
    /// <summary>
    /// Объемный трёхмерный примитив - сфера.
    /// </summary>
    [Serializable]
    public class MeshPrimitiveSphere3Df : Mesh3Df
    {
        #region Fields
        internal Vector3Df _pivot;
        internal float _radius;
        internal int _numberVerticalSegment = 18;
        internal int _numberHorizontalSegment = 18;
        internal bool _isHalf;
        #endregion

        #region Properties
        /// <summary>
        /// Радиус сферы.
        /// </summary>
        public float Radius
        {
            get
            {
                return _radius;
            }

            set
            {
                _radius = value;
                CreateSphere();
            }
        }

        /// <summary>
        /// Количество вертикальных сегментов.
        /// </summary>
        public int NumberVerticalSegment
        {
            get
            {
                return _numberVerticalSegment;
            }

            set
            {
                _numberVerticalSegment = value;
                CreateSphere();
            }
        }

        /// <summary>
        /// Количество горизонтальных сегментов.
        /// </summary>
        public int NumberHorizontalSegment
        {
            get
            {
                return _numberHorizontalSegment;
            }

            set
            {
                _numberHorizontalSegment = value;
                CreateSphere();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPrimitiveSphere3Df()
            : base()
        {
            _name = "Sphere3D";
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="pivot">Опорная точка сферы(центр сферы).</param>
        /// <param name="radius">Радиус сферы.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации сферы.</param>
        /// <param name="numberVerticalSegment">Количество вертикальных сегментов.</param>
        /// <param name="numberHorizontalSegment">Количество горизонтальных сегментов.</param>
        public MeshPrimitiveSphere3Df(Vector3Df pivot, float radius, float startAngle, int numberVerticalSegment,
                 int numberHorizontalSegment) : base()
        {
            _name = "Sphere3D";
            Create(pivot, radius, startAngle, numberVerticalSegment, numberHorizontalSegment);
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Создание сферы основе внутренних данных.
        /// </summary>
        protected void CreateSphere()
        {
            // Method intentionally left empty.
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Создание сферы.
        /// </summary>
        /// <param name="pivot">Опорная точка сферы(центр сферы).</param>
        /// <param name="radius">Радиус сферы.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации сферы.</param>
        /// <param name="numberVerticalSegment">Количество вертикальных сегментов.</param>
        /// <param name="numberHorizontalSegment">Количество горизонтальных сегментов.</param>
        public void Create(Vector3Df pivot, float radius, float startAngle, int numberVerticalSegment,
                 int numberHorizontalSegment)
        {
            // Сохраняем данные
            _numberVerticalSegment = numberVerticalSegment;
            _numberHorizontalSegment = numberHorizontalSegment;

            // Количество строк - есть количество горизонтальных сегментов
            var row_count = numberHorizontalSegment;

            // Количество столбов - есть количество вертикальных сегментов
            var column_count = numberVerticalSegment;

            _vertices.Clear();

            // Дельта углов для плоскостей
            var horizont_delta = 360.0f / column_count;
            var vertical_delta = 180.0f / row_count;

            for (var ir = 0; ir < row_count + 1; ir++)
            {
                for (var ic = 0; ic < column_count; ic++)
                {
                    // Широта в градусах - угол в вертикальной плоскости в пределах [0, 180]
                    var latitude_degree = ir * vertical_delta;

                    // Долгота в градусах - угол в горизонтальной плоскости между в пределах [0, 359]
                    var longitude_degree = ic * horizont_delta;

                    var positon = Vector3Df.FromSpherical(radius, latitude_degree, longitude_degree);
                    var uv = XGeometry3D.GetMapUVFromSpherical(latitude_degree, longitude_degree);

                    _vertices.AddVertex(positon + pivot, positon.Normalized, uv);
                }
            }

            // Заполняем треугольники
            _triangles.Clear();
            _triangles.AddTriangleRegularGrid(0, column_count - 1, row_count, true);
        }

        /// <summary>
        /// Вычисление нормалей для сферы.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для сферы.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public override void ComputeUVMap(int channel = 0)
        {
            var index = 0;
            for (var ir = 0; ir <= _numberHorizontalSegment; ir++)
            {
                for (var ic = 0; ic <= _numberVerticalSegment; ic++)
                {
                    var u = ic / (float)_numberVerticalSegment;
                    var v = ir / (float)_numberHorizontalSegment;

                    _vertices.Vertices[index].UV = new Vector2Df(v, u);
                    index++;
                }
            }
        }
        #endregion
    }
    /**@}*/
}