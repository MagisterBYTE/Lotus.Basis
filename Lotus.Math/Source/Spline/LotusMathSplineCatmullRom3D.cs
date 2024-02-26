using System;

namespace Lotus.Maths
{
    /** \addtogroup MathSpline
	*@{*/
    /// <summary>
    /// Алгоритм Catmull-Rom сплайна для трехмерного пространства.
    /// </summary>
    /// <remarks>
    /// Реализация алгоритма кубического Catmull-Rom интерполяционного сплайна для трехмерного пространства.
    /// </remarks>
    [Serializable]
    public class CCatmullRomSpline3D : SplineBase3D
    {
        #region Static methods
        /// <summary>
        /// Вычисление точки на сплайне Catmull-Rom представленной с помощью четырех контрольных точек.
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой.</param>
        /// <param name="p0">Первая контрольная точка.</param>
        /// <param name="p1">Вторая контрольная точка.</param>
        /// <param name="p2">Третья контрольная точка.</param>
        /// <param name="p3">Четвертая контрольная точка.</param>
        /// <returns>Позиция точки на сплайне Catmull-Rom.</returns>
        public static Vector3Df CalculatePoint(float time, in Vector3Df p0, in Vector3Df p1, in Vector3Df p2, in Vector3Df p3)
        {
            //The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
            var a = 2f * p1;
            var b = p2 - p0;
            var c = (2f * p0) - (5f * p1) + (4f * p2) - p3;
            var d = -p0 + (3f * p1) - (3f * p2) + p3;

            //The cubic polynomial: a + b * t + c * t^2 + d * t^3
            var point = 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));

            return point;
        }

        /// <summary>
        /// Вычисление первой производной точки на сплайне Catmull-Rom представленной с помощью четырех контрольных точек.
        /// </summary>
        /// <remarks>
        /// Первая производная показывает скорость изменения функции в данной точки.
        /// Физическим смысл производной - скорость в данной точке.
        /// </remarks>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой.</param>
        /// <param name="p0">Первая контрольная точка.</param>
        /// <param name="p1">Вторая контрольная точка.</param>
        /// <param name="p2">Третья контрольная точка.</param>
        /// <param name="p3">Четвертая контрольная точка.</param>
        /// <returns>Первая производная  на сплайне Catmull-Rom.</returns>
        public static Vector3Df CalculateFirstDerivative(float time, in Vector3Df p0, in Vector3Df p1, in Vector3Df p2, in Vector3Df p3)
        {
            var a = ((p1 - p2) * 1.5f) + ((p3 - p0) * 0.5f);
            var b = (p2 * 2.0f) - (p1 * 2.5f) - (p3 * 0.5f) + p0;
            var c = (p2 - p0) * 0.5f;
            return (3 * a * time) + (2 * b * time) + c;
        }
        #endregion

        #region Fields
        // Основные параметры
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
#endif
        internal bool _isClosed;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус замкнутости сплайна.
        /// </summary>
        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (_isClosed != value)
                {
                    _isClosed = value;
                    OnUpdateSpline();
                }
            }
        }

        /// <summary>
        /// Количество кривых в пути.
        /// </summary>
        public int CurveCount
        {
            get { return _controlPoints.Length - 1; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CCatmullRomSpline3D()
            : base(4)
        {
            _controlPoints[0] = Vector3Df.Zero;
            _controlPoints[1] = Vector3Df.Zero;
            _controlPoints[2] = Vector3Df.Zero;
            _controlPoints[3] = Vector3Df.Zero;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="count">Количество контрольных точек.</param>
        public CCatmullRomSpline3D(int count)
            : base(count)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="endPoint">Конечная точка.</param>
        public CCatmullRomSpline3D(Vector3Df startPoint, Vector3Df endPoint)
            : base(4)
        {
            _controlPoints[0] = startPoint;
            _controlPoints[1] = (startPoint + endPoint) / 3;
            _controlPoints[2] = (startPoint + endPoint) / 3 * 2;
            _controlPoints[3] = endPoint;
        }
        #endregion

        #region ILotusSpline3D methods
        /// <summary>
        /// Вычисление точки на сплайне.
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1.</param>
        /// <returns>Позиция точки на сплайне.</returns>
        public override Vector3Df CalculatePoint(float time)
        {
            int index_curve;
            if (time >= 1f)
            {
                time = 1f;
                index_curve = _controlPoints.Length - 1;
            }
            else
            {
                time = XMath.Clamp01(time) * CurveCount;
                index_curve = (int)time;
                time -= index_curve;
                index_curve *= 1;
            }

            if (_isClosed)
            {
                // Получаем индексы точек
                var ip_0 = ClampPosition(index_curve - 1);
                var ip_1 = ClampPosition(index_curve);
                var ip_2 = ClampPosition(index_curve + 1);
                var ip_3 = ClampPosition(index_curve + 2);

                var point = CalculatePoint(time,
                    in _controlPoints[ip_0],
                    in _controlPoints[ip_1],
                    in _controlPoints[ip_2],
                    in _controlPoints[ip_3]);

                return point;
            }
            else
            {
                // Получаем индексы точек
                var ip_0 = index_curve - 1 < 0 ? 0 : index_curve - 1;
                var ip_1 = index_curve;
                var ip_2 = index_curve + 1 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : index_curve + 1;
                var ip_3 = index_curve + 2 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : index_curve + 2;

                var point = CalculatePoint(time,
                    in _controlPoints[ip_0],
                    in _controlPoints[ip_1],
                    in _controlPoints[ip_2],
                    in _controlPoints[ip_3]);

                return point;
            }
        }

        /// <summary>
        /// Растеризация сплайна - вычисление точек отрезков для рисования сплайна.
        /// </summary>
        /// <remarks>
        /// Качество(степень) растеризации зависит от свойства <see cref="SplineBase2D.SegmentsSpline"/>.
        /// </remarks>
        public override void ComputeDrawingPoints()
        {
            _drawingPoints.Clear();

            if (_isClosed)
            {
                for (var ip = 0; ip < _controlPoints.Length; ip++)
                {
                    // Получаем индексы точек
                    var ip_0 = ClampPosition(ip - 1);
                    var ip_1 = ClampPosition(ip);
                    var ip_2 = ClampPosition(ip + 1);
                    var ip_3 = ClampPosition(ip + 2);

                    var prev = CalculatePoint(0, ip_0, ip_1, ip_2, ip_3);
                    _drawingPoints.Add(prev);
                    for (var i = 1; i < _segmentsSpline; i++)
                    {
                        var time = (float)i / _segmentsSpline;
                        var point = CalculatePoint(time, ip_0, ip_1, ip_2, ip_3);

                        // Добавляем если длина больше 1,4
                        if ((point - prev).SqrLength > 2)
                        {
                            _drawingPoints.Add(point);
                            prev = point;
                        }
                    }
                }

                CheckCorrectStartPoint();
            }
            else
            {
                for (var ip = 0; ip < _controlPoints.Length - 1; ip++)
                {
                    // Получаем индексы точек
                    var ip_0 = ip - 1 < 0 ? 0 : ip - 1;
                    var ip_1 = ip;
                    var ip_2 = ip + 1 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : ip + 1;
                    var ip_3 = ip + 2 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : ip + 2;

                    var prev = CalculatePoint(0, ip_0, ip_1, ip_2, ip_3);
                    _drawingPoints.Add(prev);
                    for (var i = 1; i < _segmentsSpline; i++)
                    {
                        var time = (float)i / _segmentsSpline;
                        var point = CalculatePoint(time, ip_0, ip_1, ip_2, ip_3);

                        // Добавляем если длина больше 1,4
                        if ((point - prev).SqrLength > 2)
                        {
                            _drawingPoints.Add(point);
                            prev = point;
                        }
                    }
                }

                CheckCorrectStartPoint();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Ограничение точек для организации замкнутой кривой.
        /// </summary>
        /// <param name="pos">Позиция.</param>
        /// <returns>Скорректированная позиция.</returns>
        protected int ClampPosition(int pos)
        {
            if (pos < 0)
            {
                pos = _controlPoints.Length - 1;
            }

            if (pos > _controlPoints.Length)
            {
                pos = 1;
            }
            else if (pos > _controlPoints.Length - 1)
            {
                pos = 0;
            }

            return pos;
        }

        /// <summary>
        /// Вычисление точки на сплайне CatmullRom представленной с помощью четырех контрольных точек.
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой</param>
        /// <param name="indexP0">Индекс первой точки.</param>
        /// <param name="indexP1">Индекс второй точки.</param>
        /// <param name="indexP2">Индекс третьей точки.</param>
        /// <param name="indexP3">Индекс четвертой точки.</param>
        /// <returns>Позиция точки на сплайне CatmullRom.</returns>
        public Vector3Df CalculatePoint(float time, int indexP0, int indexP1, int indexP2, int indexP3)
        {
            var a = 2f * _controlPoints[indexP1];
            var b = _controlPoints[indexP2] - _controlPoints[indexP0];
            var c = (2f * _controlPoints[indexP0]) - (5f * _controlPoints[indexP1]) + (4f * _controlPoints[indexP2]) - _controlPoints[indexP3];
            var d = -_controlPoints[indexP0] + (3f * _controlPoints[indexP1]) - (3f * _controlPoints[indexP2]) + _controlPoints[indexP3];

            //The cubic polynomial: a + b * t + c * t^2 + d * t^3
            var pos = 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));

            return pos;
        }
        #endregion
    }
    /**@}*/
}