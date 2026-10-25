#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
using UnityEngine.Serialization;

namespace Lotus.Maths
{
    /** \addtogroup MathSpline
    *@{*/
    /// <summary>
    /// Компонент представляющий сплайн CatmullRom
    /// </summary>
    /// <remarks>
    /// Реализация компонента представляющего собой сплайн CatmullRom
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Math/Spline/CatmullRom Spline")]
    public class LotusSplineCatmullRom : LotusSplineBase
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [FormerlySerializedAs("mIsClosed")]
        internal bool _isClosed;
        #endregion

        #region Properties
        /// <summary>
        /// Статус замкнутости сплайна
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
        /// Количество кривых в пути
        /// </summary>
        public int CurveCount
        {
            get { return _controlPoints.Length - 1; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Старт скрипта
        /// </summary>
        public void Start()
        {
            ApplyDrawingPointsToLineRenderer();
        }


        /// <summary>
        /// Рисование сплайна CatmullRom в сцене в режиме редактора
        /// </summary>
        public void OnDrawGizmos()
        {
#if UNITY_EDITOR
            // Рисуем опорные точки
            for (var i = 0; i < _controlPoints.Length; i++)
            {
                OnDrawGizmosControlPoint(i);
            }

            // Кривая
            OnDrawGizmosSpline();
#endif
        }
        #endregion

        #region ILotusSpline3D methods
        /// <summary>
        /// Вычисление точки на сплайне
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1</param>
        /// <returns>Позиция точки на сплайне</returns>
        public override Vector3Df CalculatePoint(float time)
        {
            int indexCurve;
            if (time >= 1f)
            {
                time = 1f;
                indexCurve = _controlPoints.Length - 1;
            }
            else
            {
                time = Mathf.Clamp01(time) * CurveCount;
                indexCurve = (int)time;
                time -= indexCurve;
            }

            if (_isClosed)
            {
                // Получаем индексы точек
                var indexP0 = ClampPosition(indexCurve - 1);
                var indexP1 = ClampPosition(indexCurve);
                var indexP2 = ClampPosition(indexCurve + 1);
                var indexP3 = ClampPosition(indexCurve + 2);

                return CCatmullRomSpline3D.CalculatePoint(time,
                    in _controlPoints[indexP0],
                    in _controlPoints[indexP1],
                    in _controlPoints[indexP2],
                    in _controlPoints[indexP3]);
            }

            // Получаем индексы точек
            var openP0 = indexCurve - 1 < 0 ? 0 : indexCurve - 1;
            var openP1 = indexCurve;
            var openP2 = indexCurve + 1 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : indexCurve + 1;
            var openP3 = indexCurve + 2 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : indexCurve + 2;

            return CCatmullRomSpline3D.CalculatePoint(time,
                in _controlPoints[openP0],
                in _controlPoints[openP1],
                in _controlPoints[openP2],
                in _controlPoints[openP3]);
        }


        /// <summary>
        /// Вычисление первой производной точки на сплайне
        /// </summary>
        /// <remarks>
        /// Первая производная показывает скорость изменения функции в данной точки.
        /// Физическим смысл производной - скорость в данной точке
        /// </remarks>
        /// <param name="time">Положение точки от 0 до 1</param>
        /// <returns>Скорость точки на сплайне</returns>
        public override Vector3Df CalculateFirstDerivative(float time)
        {
            int indexCurve;
            if (time >= 1f)
            {
                time = 1f;
                indexCurve = _controlPoints.Length - 1;
            }
            else
            {
                time = Mathf.Clamp01(time) * CurveCount;
                indexCurve = (int)time;
                time -= indexCurve;
            }

            if (_isClosed)
            {
                // Получаем индексы точек
                var indexP0 = ClampPosition(indexCurve - 1);
                var indexP1 = ClampPosition(indexCurve);
                var indexP2 = ClampPosition(indexCurve + 1);
                var indexP3 = ClampPosition(indexCurve + 2);

                return CCatmullRomSpline3D.CalculateFirstDerivative(time,
                    in _controlPoints[indexP0],
                    in _controlPoints[indexP1],
                    in _controlPoints[indexP2],
                    in _controlPoints[indexP3]);
            }

            // Получаем индексы точек
            var openP0 = indexCurve - 1 < 0 ? 0 : indexCurve - 1;
            var openP1 = indexCurve;
            var openP2 = indexCurve + 1 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : indexCurve + 1;
            var openP3 = indexCurve + 2 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : indexCurve + 2;

            return CCatmullRomSpline3D.CalculateFirstDerivative(time,
                in _controlPoints[openP0],
                in _controlPoints[openP1],
                in _controlPoints[openP2],
                in _controlPoints[openP3]);
        }


        /// <summary>
        /// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
        /// </summary>
        /// <remarks>
        /// Качество(степень) растеризации зависит от свойства <see cref="SegmentsSpline"/>
        /// </remarks>
        public override void ComputeDrawingPoints()
        {
            _drawingPoints.Clear();

            if (_isClosed)
            {
                for (var ip = 0; ip < _controlPoints.Length; ip++)
                {
                    // Получаем индексы точек
                    var indexP0 = ClampPosition(ip - 1);
                    var indexP1 = ClampPosition(ip);
                    var indexP2 = ClampPosition(ip + 1);
                    var indexP3 = ClampPosition(ip + 2);

                    var prev = CalculatePoint(0, indexP0, indexP1, indexP2, indexP3);
                    _drawingPoints.Add(prev);
                    for (var i = 1; i < _segmentsSpline; i++)
                    {
                        var time = (float)i / _segmentsSpline;
                        var point = CalculatePoint(time, indexP0, indexP1, indexP2, indexP3);

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
                    var indexP0 = ip - 1 < 0 ? 0 : ip - 1;
                    var indexP1 = ip;
                    var indexP2 = ip + 1 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : ip + 1;
                    var indexP3 = ip + 2 > _controlPoints.Length - 1 ? _controlPoints.Length - 1 : ip + 2;

                    var prev = CalculatePoint(0, indexP0, indexP1, indexP2, indexP3);
                    _drawingPoints.Add(prev);
                    for (var i = 1; i < _segmentsSpline; i++)
                    {
                        var time = (float)i / _segmentsSpline;
                        var point = CalculatePoint(time, indexP0, indexP1, indexP2, indexP3);

                        // Добавляем если длина больше 1,4
                        if ((point - prev).SqrLength > 2)
                        {
                            _drawingPoints.Add(point);
                            prev = point;
                        }
                    }
                }

                CheckCorrectEndPoint();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Переустановка параметров сплайна
        /// </summary>
        protected override void OnResetSpline()
        {
            base.OnResetSpline();

            _controlPoints = new Vector3Df[]
            {
                Vector3Df.Zero,
                (Vector3Df.Forward * 100) + (Vector3Df.Right * 100),
                (Vector3Df.Forward * 100) + (Vector3Df.Right * 200),
                Vector3Df.Right * 300
            };

            OnUpdateSpline();
        }


        /// <summary>
        /// Первичная инициализация параметров сплайна
        /// </summary>
        protected override void OnInitSpline()
        {
            base.OnInitSpline();

            if (_controlPoints == null)
            {
                _controlPoints = new Vector3Df[]
                {
                    Vector3Df.Zero,
                    (Vector3Df.Forward * 100) + (Vector3Df.Right * 100),
                    (Vector3Df.Forward * 200) + (Vector3Df.Right * 200),
                    Vector3Df.Right * 300
                };
            }

            OnUpdateSpline();
        }


        /// <summary>
        /// Ограничение точек для организации замкнутой кривой
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <returns>Скорректированная позиция</returns>
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
        #endregion

        #region Control point methods
        /// <summary>
        /// Вычисление точки на сплайне CatmullRom представленной с помощью четырех контрольных точек
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой</param>
        /// <param name="indexP0">Индекс первой точки</param>
        /// <param name="indexP1">Индекс второй точки</param>
        /// <param name="indexP2">Индекс третьей точки</param>
        /// <param name="indexP3">Индекс четвертой точки</param>
        /// <returns>Позиция точки на сплайне CatmullRom</returns>
        public Vector3Df CalculatePoint(float time, int indexP0, int indexP1, int indexP2, int indexP3)
        {
            var a = 2f * _controlPoints[indexP1];
            var b = _controlPoints[indexP2] - _controlPoints[indexP0];
            var c = (2f * _controlPoints[indexP0]) - (5f * _controlPoints[indexP1]) + (4f * _controlPoints[indexP2]) - _controlPoints[indexP3];
            var d = -_controlPoints[indexP0] + (3f * _controlPoints[indexP1]) - (3f * _controlPoints[indexP2]) + _controlPoints[indexP3];

            // The cubic polynomial: a + b * t + c * t^2 + d * t^3
            return 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));
        }
        #endregion
    }
    /**@}*/
}
#endif
