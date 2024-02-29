using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshPlanar
	*@{*/
    /// <summary>
    /// Плоскостной трехмерный примитив - эллипс.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Топология вершин:
    /// 3)---2)
    /// |   / \
    /// |  /   \
    /// | /     \
    /// |/       \
    /// 0)--------1)
    /// </para>
    /// <para>
    /// Первый треугольник: 0, 2, 1
    /// Второй треугольник: 0, 3, 2
    /// Третий треугольник: 0, 4, 3 и т.д.
    /// </para>
    /// <para>
    /// Опорная точка элиппса – его геометрический центр
    /// </para>
    /// </remarks>
    [Serializable]
    public class CMeshPlanarEllipse3Df : MeshPlanar3Df
    {
        #region Fields
        protected internal float _radiusX;
        protected internal float _radiusY;
        protected internal float _startAngle;
        protected internal int _numberSegment = 18;
        #endregion

        #region Properties
        /// <summary>
        /// Радиус эллипса по X.
        /// </summary>
        public float RadiusX
        {
            get { return _radiusX; }
            set
            {
                _radiusX = value;
                CreateEllipse();
            }
        }

        /// <summary>
        /// Радиус эллипса по Y.
        /// </summary>
        public float RadiusY
        {
            get { return _radiusY; }
            set
            {
                _radiusY = value;
                CreateEllipse();
            }
        }

        /// <summary>
        /// Начальный угол(в градусах) генерации эллипса.
        /// </summary>
        public float StartAngle
        {
            get { return _startAngle; }
            set
            {
                _startAngle = value;
                CreateEllipse();
            }
        }

        /// <summary>
        /// Количество сегментов эллипса.
        /// </summary>
        public int NumberSegment
        {
            get { return _numberSegment; }
            set
            {
                _numberSegment = value;
                CreateEllipse();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CMeshPlanarEllipse3Df()
            : base()
        {
            _name = "Ellipse3D";
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Создание эллипса на основе внутренних данных.
        /// </summary>
        protected void CreateEllipse()
        {
            // Сохраняем опорную точку
            var pivot = _vertices[0].Position;
            _vertices.Clear();

            var segment_angle = 360f / _numberSegment;
            var current_angle = _startAngle;

            var normal = GetPerpendicularVector();
            _vertices.AddVertex(pivot, normal, XGeometry2D.MapUVMiddleCenter);

            for (var i = 0; i < _numberSegment; i++)
            {
                var angle_in_radians = current_angle * XMath.DegreeToRadian_F;
                var pos = Vector3Df.Zero;
                switch (_planeType)
                {
                    case Maths.TDimensionPlane.XZ:
                        {
                            pos.X = _radiusX * XMath.Cos(angle_in_radians);
                            pos.Z = _radiusY * XMath.Sin(angle_in_radians);
                        }
                        break;
                    case Maths.TDimensionPlane.ZY:
                        {
                            pos.Z = _radiusX * XMath.Cos(angle_in_radians);
                            pos.Y = _radiusY * XMath.Sin(angle_in_radians);
                        }
                        break;
                    case Maths.TDimensionPlane.XY:
                        {
                            pos.X = _radiusX * XMath.Cos(angle_in_radians);
                            pos.Y = _radiusY * XMath.Sin(angle_in_radians);
                        }
                        break;
                    default:
                        break;
                }

                var uv = new Vector2Df((0.5f * XMath.Cos(angle_in_radians)) + 0.5f, (0.5f * XMath.Sin(angle_in_radians)) + 0.5f);

                _vertices.AddVertex(pivot + pos, normal, uv);

                current_angle += segment_angle;
            }

            _triangles.Clear();
            _triangles.AddTriangleFan(0, _numberSegment - 1, true);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Создание эллипса в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <param name="pivot">Опорная точка эллипса (его центр).</param>
        /// <param name="radiusX">Радиус эллипса по X.</param>
        /// <param name="radiusZ">Радиус эллипса по Z.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации эллипса.</param>
        /// <param name="numberSegment">Количество сегментов эллипса.</param>
        public void CreateEllipseXZ(Vector3Df pivot, float radiusX, float radiusZ, float startAngle, int numberSegment)
        {
            _planeType = Maths.TDimensionPlane.XZ;
            _radiusX = radiusX;
            _radiusY = radiusZ;
            _startAngle = startAngle;
            _numberSegment = numberSegment;

            _vertices.Clear();
            _vertices.AddVertex(pivot);

            CreateEllipse();
        }

        /// <summary>
        /// Создание эллипса в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <param name="pivot">Опорная точка эллипса (его центр).</param>
        /// <param name="radiusZ">Радиус эллипса по Z.</param>
        /// <param name="radiusY">Радиус эллипса по Y.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации эллипса.</param>
        /// <param name="numberSegment">Количество сегментов эллипса.</param>
        public void CreateEllipseZY(Vector3Df pivot, float radiusZ, float radiusY, float startAngle, int numberSegment)
        {
            _planeType = Maths.TDimensionPlane.ZY;
            _radiusX = radiusZ;
            _radiusY = radiusY;
            _startAngle = startAngle;
            _numberSegment = numberSegment;

            _vertices.Clear();
            _vertices.AddVertex(pivot);

            CreateEllipse();
        }

        /// <summary>
        /// Создание эллипса в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <param name="pivot">Опорная точка эллипса (его центр).</param>
        /// <param name="radiusX">Радиус эллипса по X.</param>
        /// <param name="radiusY">Радиус эллипса по Y.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации эллипса.</param>
        /// <param name="numberSegment">Количество сегментов эллипса.</param>
        public void CreateEllipseXY(Vector3Df pivot, float radiusX, float radiusY, float startAngle, int numberSegment)
        {
            _planeType = Maths.TDimensionPlane.XY;
            _radiusX = radiusX;
            _radiusY = radiusY;
            _startAngle = startAngle;
            _numberSegment = numberSegment;

            _vertices.Clear();
            _vertices.AddVertex(pivot);

            CreateEllipse();
        }

        /// <summary>
        /// Вычисление нормалей для эллипса.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для эллипса.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public override void ComputeUVMap(int channel = 0)
        {
            // Method intentionally left empty.
        }
        #endregion
    }
    /**@}*/
}