using System;

using Lotus.Maths;

namespace Lotus.Object3D
{
    /**
     * \defgroup Object3DMeshVolume Объемные трехмерные примитивы
     * \ingroup Object3DMesh
     * \brief Объемные трехмерные примитивы - это примитивы которые образуют объемную замкнутую поверхность.
     * @{
     */
    /// <summary>
    /// Объемный трёхмерный примитив - цилиндр.
    /// </summary>
    [Serializable]
    public class MeshPrimitiveCylinder3Df : Mesh3Df
    {
        #region Fields
        protected internal float _radius;
        protected internal float _height;
        protected internal int _numberVerticalSegment = 18;
        protected internal int _numberHorizontalSegment = 18;
        #endregion

        #region Properties
        /// <summary>
        /// Радиус цилиндра.
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
                CreateCylinder();
            }
        }

        /// <summary>
        /// Высота цилиндра.
        /// </summary>
        public float Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
                CreateCylinder();
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
                CreateCylinder();
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
                CreateCylinder();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public MeshPrimitiveCylinder3Df()
            : base()
        {
            _name = "Cylinder3D";
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Создание цилиндра основе внутренних данных.
        /// </summary>
        protected virtual void CreateCylinder()
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Создание цилиндра с основанием в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <param name="pivot">Опорная точка (центр ниженого основания).</param>
        /// <param name="radius">Радиус цилиндра.</param>
        /// <param name="height">Высота цилиндра.</param>
        /// <param name="startAngle">Начальный угол(в градусах) генерации сферы.</param>
        /// <param name="numberVerticalSegment">Количество вертикальных сегментов.</param>
        /// <param name="numberHorizontalSegment">Количество горизонтальных сегментов.</param>
        public void CreateCylinderXZ(Vector3Df pivot, float radius, float height,
                float startAngle, int numberVerticalSegment, int numberHorizontalSegment)
        {
            var up_cap = new CMeshPlanarEllipse3Df();
            up_cap.CreateEllipseXZ(pivot + (Vector3Df.Up * height), radius, radius, startAngle, numberVerticalSegment);

            var down_cap = new CMeshPlanarEllipse3Df();
            down_cap.CreateEllipseXZ(pivot, radius, radius, startAngle, numberVerticalSegment);
            down_cap.RotateFromX(180, true);

            var surface = new MeshPlanarGrid3Df();
            var points = XGeometry3D.GeneratePointsOnCircleXZ(radius, numberVerticalSegment, startAngle);
            //points.Reverse();

            for (var i = 0; i < points.Count; i++)
            {
                points[i] += pivot;
            }

            surface.CreateFromPointListXZ(points, numberHorizontalSegment, height / numberHorizontalSegment, true);

            //surface.MinimizeToCylinder(radius);

            Add(up_cap);
            Add(down_cap);
            Add(surface);
        }

        /// <summary>
        /// Вычисление нормалей для цилиндра.
        /// </summary>
        /// <remarks>
        /// Нормаль вычисления путем векторного произведения по часовой стрелки.
        /// </remarks>
        public override void ComputeNormals()
        {
            // Method intentionally left empty.
        }

        /// <summary>
        /// Вычисление текстурных координат (развертки) для цилиндра.
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