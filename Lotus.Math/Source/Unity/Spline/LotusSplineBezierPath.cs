#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Maths
{
    /** \addtogroup MathSpline
    *@{*/
    /// <summary>
    /// Компонент представляющий путь из кубических кривых Безье
    /// </summary>
    /// <remarks>
    /// Реализация компонента представляющего путь из кубических кривых Безье
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Math/Spline/BezierPath Spline")]
    public class LotusSplineBezierPath : LotusSplineBase
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        [FormerlySerializedAs("mIsClosed")]
        internal bool _isClosed;
        [SerializeField]
        [FormerlySerializedAs("mHandleModes")]
        internal TBezierHandleMode[] _handleModes;
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

                    if (_isClosed)
                    {
                        _handleModes[_handleModes.Length - 1] = _handleModes[0];
                        SetControlPoint(0, _controlPoints[0], true);
                    }

                    OnUpdateSpline();
                }
            }
        }

        /// <summary>
        /// Количество кривых в пути
        /// </summary>
        public int CurveCount
        {
            get { return (_controlPoints.Length - 1) / 3; }
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
        /// Рисование кривой Безье в сцене в режиме редактора
        /// </summary>
        public void OnDrawGizmos()
        {
#if UNITY_EDITOR
            for (var i = 0; i < _controlPoints.Length; i++)
            {
                if (IsHandlePoint(i))
                {
                    OnDrawGizmosHandlePoint(i);
                }
                else
                {
                    OnDrawGizmosControlPoint(i);
                }

                // Управляющий вектор к начальной точки
                if (i == 1)
                {
                    Vector3 p1 = GetControlPointWorld(1);
                    Vector3 p2 = GetControlPointWorld(0);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(p1, p2);
                    continue;
                }

                // Управляющий вектор к конечной точки
                if (i == _controlPoints.Length - 2)
                {
                    Vector3 p1 = GetControlPointWorld(_controlPoints.Length - 2);
                    Vector3 p2 = GetControlPointWorld(_controlPoints.Length - 1);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(p1, p2);
                    continue;
                }

                if (i > 1 && i < _controlPoints.Length - 2 && i % 3 == 0)
                {
                    Vector3 pc = GetControlPointWorld(i);
                    Vector3 p1 = GetControlPointWorld(i - 1);
                    Vector3 p2 = GetControlPointWorld(i + 1);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(p1, pc);
                    Gizmos.DrawLine(p2, pc);
                }
            }

            OnDrawGizmosSpline();
#endif
        }


        /// <summary>
        /// Рисования управляющей точки в режиме редактора
        /// </summary>
        /// <param name="index">Индекс контрольной точки</param>
        public void OnDrawGizmosHandlePoint(int index)
        {
#if UNITY_EDITOR
            Vector3 point = GetControlPointWorld(index);
            var size = _sizeControlHandle / 100f * HandleUtility.GetHandleSize(point);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(point, Vector3.one * size);
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
                indexCurve = _controlPoints.Length - 4;
            }
            else
            {
                time = Mathf.Clamp01(time) * CurveCount;
                indexCurve = (int)time;
                time -= indexCurve;
                indexCurve *= 3;
            }

            return BezierCubic3D.CalculatePoint(time,
                in _controlPoints[indexCurve],
                in _controlPoints[indexCurve + 1],
                in _controlPoints[indexCurve + 2],
                in _controlPoints[indexCurve + 3]);
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
                indexCurve = _controlPoints.Length - 4;
            }
            else
            {
                time = Mathf.Clamp01(time) * CurveCount;
                indexCurve = (int)time;
                time -= indexCurve;
                indexCurve *= 3;
            }

            return BezierCubic3D.CalculateFirstDerivative(time,
                in _controlPoints[indexCurve],
                in _controlPoints[indexCurve + 1],
                in _controlPoints[indexCurve + 2],
                in _controlPoints[indexCurve + 3]);
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

            for (var i = 0; i < CurveCount; i++)
            {
                var prev = CalculateCurvePoint(i, 0);
                _drawingPoints.Add(prev);
                for (var ip = 1; ip < SegmentsSpline; ip++)
                {
                    var time = (float)ip / SegmentsSpline;
                    var point = CalculateCurvePoint(i, time);

                    // Добавляем если длина больше 1,4
                    if ((point - prev).SqrLength > MinimalSqrLine)
                    {
                        _drawingPoints.Add(point);
                        prev = point;
                    }
                }
            }

            if (_isClosed)
            {
                CheckCorrectStartPoint();
            }
            else
            {
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

            _handleModes = new TBezierHandleMode[]
            {
                TBezierHandleMode.Free,
                TBezierHandleMode.Free
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
                    (Vector3Df.Forward * 100) + (Vector3Df.Right * 200),
                    Vector3Df.Right * 300
                };
            }

            _handleModes = new TBezierHandleMode[]
            {
                TBezierHandleMode.Free,
                TBezierHandleMode.Free
            };

            OnUpdateSpline();
        }


        /// <summary>
        /// Установка режима редактирования управляющей точки
        /// </summary>
        /// <param name="index">Позиция(индекс) контрольной точки</param>
        private void SetHandleMode(int index)
        {
            var modeIndex = (index + 1) / 3;
            var mode = _handleModes[modeIndex];
            if (mode == TBezierHandleMode.Free || (!_isClosed && (modeIndex == 0 || modeIndex == _handleModes.Length - 1)))
            {
                return;
            }

            var middleIndex = modeIndex * 3;
            int fixedIndex, enforcedIndex;
            if (index <= middleIndex)
            {
                fixedIndex = middleIndex - 1;
                if (fixedIndex < 0)
                {
                    fixedIndex = _controlPoints.Length - 2;
                }
                enforcedIndex = middleIndex + 1;
                if (enforcedIndex >= _controlPoints.Length)
                {
                    enforcedIndex = 1;
                }
            }
            else
            {
                fixedIndex = middleIndex + 1;
                if (fixedIndex >= _controlPoints.Length)
                {
                    fixedIndex = 1;
                }
                enforcedIndex = middleIndex - 1;
                if (enforcedIndex < 0)
                {
                    enforcedIndex = _controlPoints.Length - 2;
                }
            }

            var middle = _controlPoints[middleIndex];
            var enforcedTangent = middle - _controlPoints[fixedIndex];
            if (mode == TBezierHandleMode.Aligned)
            {
                enforcedTangent = enforcedTangent.Normalized * Vector3Df.Distance(middle, _controlPoints[enforcedIndex]);
            }

            _controlPoints[enforcedIndex] = middle + enforcedTangent;

            OnUpdateSpline();
        }
        #endregion

        #region Control point methods
        /// <summary>
        /// Вычисление точки на сплайне
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1</param>
        /// <returns>Позиция точки на сплайне</returns>
        public Vector3Df CalculatePointWorld(float time)
        {
            return transform.TransformPoint(CalculatePoint(time)).ToVector3Df();
        }


        /// <summary>
        /// Установка контрольной точки сплайна по индексу в локальных координатах
        /// </summary>
        /// <param name="index">Позиция(индекс) точки</param>
        /// <param name="point">Контрольная точка сплайна в локальных координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public override void SetControlPoint(int index, Vector3Df point, bool updateSpline = false)
        {
            if (index % 3 == 0)
            {
                var delta = point - _controlPoints[index];
                if (_isClosed)
                {
                    if (index == 0)
                    {
                        _controlPoints[1] += delta;
                        _controlPoints[_controlPoints.Length - 2] += delta;
                        _controlPoints[_controlPoints.Length - 1] = point;
                    }
                    else if (index == _controlPoints.Length - 1)
                    {
                        _controlPoints[0] = point;
                        _controlPoints[1] += delta;
                        _controlPoints[index - 1] += delta;
                    }
                    else
                    {
                        _controlPoints[index - 1] += delta;
                        _controlPoints[index + 1] += delta;
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        _controlPoints[index - 1] += delta;
                    }
                    if (index + 1 < _controlPoints.Length)
                    {
                        _controlPoints[index + 1] += delta;
                    }
                }
            }
            _controlPoints[index] = point;
            SetHandleMode(index);

            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Установка контрольной точки сплайна по индексу в мировых координатах
        /// </summary>
        /// <param name="index">Позиция(индекс) точки</param>
        /// <param name="point">Контрольная точка сплайна в мировых координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public override void SetControlPointWorld(int index, Vector3Df point, bool updateSpline = false)
        {
            point = transform.InverseTransformPoint(point).ToVector3Df();
            SetControlPoint(index, point, updateSpline);
        }
        #endregion

        #region Curve methods
        /// <summary>
        /// Добавить кривую
        /// </summary>
        public void AddCurve()
        {
            var point = _controlPoints[_controlPoints.Length - 1];
            Array.Resize(ref _controlPoints, _controlPoints.Length + 3);
            point.X += 100f;
            _controlPoints[_controlPoints.Length - 3] = point;
            point.X += 100f;
            _controlPoints[_controlPoints.Length - 2] = point;
            point.X += 100f;
            _controlPoints[_controlPoints.Length - 1] = point;

            Array.Resize(ref _handleModes, _handleModes.Length + 1);
            _handleModes[_handleModes.Length - 1] = _handleModes[_handleModes.Length - 2];
            SetHandleMode(_controlPoints.Length - 4);

            if (_isClosed)
            {
                _controlPoints[_controlPoints.Length - 1] = _controlPoints[0];
                _handleModes[_handleModes.Length - 1] = _handleModes[0];
                SetHandleMode(0);
            }

            OnUpdateSpline();
        }


        /// <summary>
        /// Удаление последней кривой
        /// </summary>
        public void RemoveCurve()
        {
            if (CurveCount > 1)
            {
                Array.Resize(ref _controlPoints, _controlPoints.Length - 3);
                Array.Resize(ref _handleModes, _handleModes.Length - 1);
                SetHandleMode(_controlPoints.Length - 2, TBezierHandleMode.Free);
                if (_isClosed)
                {
                    _controlPoints[_controlPoints.Length - 1] = _controlPoints[0];
                    _handleModes[_handleModes.Length - 1] = _handleModes[0];
                    SetHandleMode(0);
                }

                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Вычисление точки на отдельной кривой Безье в локальных координатах
        /// </summary>
        /// <param name="curveIndex">Индекс кривой</param>
        /// <param name="time">Время</param>
        /// <returns>Точка</returns>
        public Vector3Df CalculateCurvePoint(int curveIndex, float time)
        {
            var nodeIndex = curveIndex * 3;

            return BezierCubic3D.CalculatePoint(time,
                in _controlPoints[nodeIndex],
                in _controlPoints[nodeIndex + 1],
                in _controlPoints[nodeIndex + 2],
                in _controlPoints[nodeIndex + 3]);
        }


        /// <summary>
        /// Вычисление точки на отдельной кривой Безье в мировых координатах
        /// </summary>
        /// <param name="curveIndex">Индекс кривой</param>
        /// <param name="time">Время</param>
        /// <returns>Точка</returns>
        public Vector3Df CalculateCurvePointWorld(int curveIndex, float time)
        {
            var nodeIndex = curveIndex * 3;

            return transform.TransformPoint(BezierCubic3D.CalculatePoint(time,
                in _controlPoints[nodeIndex],
                in _controlPoints[nodeIndex + 1],
                in _controlPoints[nodeIndex + 2],
                in _controlPoints[nodeIndex + 3])).ToVector3Df();
        }


        /// <summary>
        /// Получение контрольной точки на отдельной кривой Безье в локальных координатах
        /// </summary>
        /// <param name="curveIndex">Индекс кривой</param>
        /// <param name="pointIndex">Индекс контрольной точки</param>
        /// <returns>Контрольная точка</returns>
        public Vector3Df GetCurveControlPoint(int curveIndex, int pointIndex)
        {
            curveIndex *= 3;
            return _controlPoints[curveIndex + pointIndex];
        }


        /// <summary>
        /// Установка позиции контрольной точки на отдельной кривой Безье в локальных координатах
        /// </summary>
        /// <param name="curveIndex">Индекс кривой</param>
        /// <param name="pointIndex">Индекс точки</param>
        /// <param name="position">Позиция контрольной точки</param>
        public void SetCurveControlPoint(int curveIndex, int pointIndex, Vector3Df position)
        {
            curveIndex *= 3;
            _controlPoints[curveIndex + pointIndex] = position;
        }
        #endregion

        #region Point methods
        /// <summary>
        /// Проверка на управляющую точку
        /// </summary>
        /// <param name="index">Позиция(индекс) контрольной точки</param>
        /// <returns>Статус управляющей точки</returns>
        public bool IsHandlePoint(int index)
        {
            return index == 1 ||
                   index == 2 ||
                   index % 3 == 1 ||
                   index % 3 == 2;
        }


        /// <summary>
        /// Получения режима редактирования управляющей точки
        /// </summary>
        /// <param name="index">Позиция(индекс) контрольной точки</param>
        /// <returns>Режим редактирования управляющей точки</returns>
        public TBezierHandleMode GetHandleMode(int index)
        {
            return _handleModes[(index + 1) / 3];
        }


        /// <summary>
        /// Установка режима редактирования управляющей точки
        /// </summary>
        /// <param name="index">Позиция(индекс) контрольной точки</param>
        /// <param name="mode">Режим редактирования управляющей точки</param>
        public void SetHandleMode(int index, TBezierHandleMode mode)
        {
            var modeIndex = (index + 1) / 3;
            _handleModes[modeIndex] = mode;

            if (_isClosed)
            {
                if (modeIndex == 0)
                {
                    _handleModes[_handleModes.Length - 1] = mode;
                }
                else if (modeIndex == _handleModes.Length - 1)
                {
                    _handleModes[0] = mode;
                }
            }

            SetHandleMode(index);
        }
        #endregion
    }
    /**@}*/
}
#endif
