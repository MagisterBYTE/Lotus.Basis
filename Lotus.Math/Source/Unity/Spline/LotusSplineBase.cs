#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

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
    /// Базовый компонент определяющий основное параметры сплайна
    /// </summary>
    /// <remarks>
    /// Реализация компонента определяющего основное параметры сплайна и его базовую функциональность
    /// </remarks>
    [Serializable]
    public abstract class LotusSplineBase : MonoBehaviour, ILotusSpline3D
    {
        #region Static fields
        /// <summary>
        /// Квадрат минимальной длины отрезка для отображения сплайна
        /// </summary>
        /// <remarks>
        /// Если в результате растеризации получится отрезок меньшей длины(квадрата длины) он
        /// не будет принимается в расчет и отображаться
        /// </remarks>
        public static int MinimalSqrLine = 2;
        #endregion

        #region Fields
        // Основные параметры
        [SerializeField]
        [FormerlySerializedAs("mControlPoints")]
        internal Vector3Df[] _controlPoints;
        [SerializeField]
        [FormerlySerializedAs("mSegmentsSpline")]
        internal int _segmentsSpline;
        [SerializeField]
        [FormerlySerializedAs("mSnapPlane")]
        internal TDimensionPlaneSelect _snapPlane;
        [SerializeField]
        [FormerlySerializedAs("mDrawingPoints")]
        internal List<Vector3Df> _drawingPoints;
        [SerializeField]
        [FormerlySerializedAs("mLength")]
        internal float _length;
        [SerializeField]
        [FormerlySerializedAs("mSegmentsPath")]
        internal List<TMoveSegment> _segmentsPath;
        [NonSerialized]
        internal int _currentSegmentPath;
        [NonSerialized]
        internal Vector3Df _currentPosition;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        [FormerlySerializedAs("mSizeControlHandle")]
        internal float _sizeControlHandle = 10;
        [SerializeField]
        [FormerlySerializedAs("mColorControlStart")]
        internal Color _colorControlStart = Color.cyan;
        [SerializeField]
        [FormerlySerializedAs("mColorControl")]
        internal Color _colorControl = Color.red;
        [SerializeField]
        [FormerlySerializedAs("mColorControlEnd")]
        internal Color _colorControlEnd = Color.magenta;
        [SerializeField]
        [FormerlySerializedAs("mColorSpline")]
        internal Color _colorSpline = Color.green;
        [SerializeField]
        [FormerlySerializedAs("mColorSplineAlt")]
        internal Color _colorSplineAlt = Color.green;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Начальная точка
        /// </summary>
        public Vector3Df StartPoint
        {
            get { return _controlPoints[0]; }
            set { _controlPoints[0] = value; }
        }

        /// <summary>
        /// Конечная точка
        /// </summary>
        public Vector3Df EndPoint
        {
            get { return _controlPoints[_controlPoints.Length - 1]; }
            set { _controlPoints[_controlPoints.Length - 1] = value; }
        }

        /// <summary>
        /// Количество сегментов сплайна
        /// </summary>
        /// <remarks>
        /// Данный параметр используется при растеризации сплайна
        /// </remarks>
        public int SegmentsSpline
        {
            get { return _segmentsSpline; }
            set
            {
                if (_segmentsSpline != value)
                {
                    _segmentsSpline = value;
                    OnUpdateSpline();
                }
            }
        }

        /// <summary>
        /// Список сегментов пути
        /// </summary>
        public List<TMoveSegment> SegmentsPath
        {
            get { return _segmentsPath; }
            set { _segmentsPath = value; }
        }

        /// <summary>
        /// Плоскость для ограничение перемещения при модификации точки сплайна
        /// </summary>
        public TDimensionPlaneSelect SnapPlane
        {
            get { return _snapPlane; }
            set { _snapPlane = value; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Размер ручки для редактирования сплайна
        /// </summary>
        public float SizeControlHandle
        {
            get { return _sizeControlHandle; }
            set { _sizeControlHandle = value; }
        }

        /// <summary>
        /// Цвет начальной точки сплайна
        /// </summary>
        public Color ColorControlStart
        {
            get { return _colorControlStart; }
            set { _colorControlStart = value; }
        }

        /// <summary>
        /// Цвет контрольных точек сплайна
        /// </summary>
        public Color ColorControl
        {
            get { return _colorControl; }
            set { _colorControl = value; }
        }

        /// <summary>
        /// Цвет конечной точки сплайна
        /// </summary>
        public Color ColorControlEnd
        {
            get { return _colorControlEnd; }
            set { _colorControlEnd = value; }
        }

        /// <summary>
        /// Цвет сплайна
        /// </summary>
        public Color ColorSpline
        {
            get { return _colorSpline; }
            set { _colorSpline = value; }
        }

        /// <summary>
        /// Альтернативный цвет сплайна
        /// </summary>
        public Color ColorSplineAlt
        {
            get { return _colorSplineAlt; }
            set { _colorSplineAlt = value; }
        }
#endif

        /// <summary>
        /// Количество контрольных точек сплайна
        /// </summary>
        public int CountPoints
        {
            get { return _controlPoints.Length; }
        }

        /// <summary>
        /// Длина сплайна
        /// </summary>
        public float Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Контрольные точки сплайна
        /// </summary>
        public IList<Vector3Df> ControlPoints
        {
            get { return _controlPoints; }
        }

        /// <summary>
        /// Список точек сплайна для рисования
        /// </summary>
        public IList<Vector3Df> DrawingPoints
        {
            get { return _drawingPoints; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
            OnResetSpline();
        }


        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            OnInitSpline();
        }


        /// <summary>
        /// Рисования сплайна в режиме редактора
        /// </summary>
        public void OnDrawGizmosSpline()
        {
#if UNITY_EDITOR
            if (_drawingPoints == null)
            {
                return;
            }

            for (var i = 1; i < _drawingPoints.Count; i++)
            {
                Vector3 from = transform.TransformPoint(_drawingPoints[i - 1]);
                Vector3 to = transform.TransformPoint(_drawingPoints[i]);
                Gizmos.color = i % 2 == 0 ? _colorSpline : _colorSplineAlt;
                Gizmos.DrawLine(from, to);
            }
#endif
        }


        /// <summary>
        /// Рисования контрольной точки в режиме редактора
        /// </summary>
        /// <param name="index">Индекс контрольной точки</param>
        public void OnDrawGizmosControlPoint(int index)
        {
#if UNITY_EDITOR
            Vector3 point = GetControlPointWorld(index);
            if (index == 0)
            {
                Gizmos.color = _colorControlStart;
            }
            else if (index == _controlPoints.Length - 1)
            {
                Gizmos.color = _colorControlEnd;
            }
            else
            {
                Gizmos.color = _colorControl;
            }

            Gizmos.DrawSphere(point, _sizeControlHandle / 100f * HandleUtility.GetHandleSize(point));
#endif
        }


        /// <summary>
        /// Рисования сплайна в режиме игры (только в редакторе)
        /// </summary>
        public void OnDrawEditorSpline()
        {
#if UNITY_EDITOR
            if (_drawingPoints == null)
            {
                return;
            }

            for (var i = 1; i < _drawingPoints.Count; i++)
            {
                Vector3 from = transform.TransformPoint(_drawingPoints[i - 1]);
                Vector3 to = transform.TransformPoint(_drawingPoints[i]);
                Debug.DrawLine(from, to, i % 2 == 0 ? _colorSpline : _colorSplineAlt);
            }
#endif
        }
        #endregion

        #region ILotusSpline3D methods
        /// <summary>
        /// Вычисление точки на сплайне
        /// </summary>
        /// <remarks>
        /// Это основной метод для реализации
        /// </remarks>
        /// <param name="time">Положение точки от 0 до 1</param>
        /// <returns>Позиция точки на сплайне</returns>
        public abstract Vector3Df CalculatePoint(float time);


        /// <summary>
        /// Вычисление первой производной точки на сплайне
        /// </summary>
        /// <remarks>
        /// Первая производная показывает скорость изменения функции в данной точки.
        /// Физическим смысл производной - скорость в данной точке
        /// </remarks>
        /// <param name="time">Положение точки от 0 до 1</param>
        /// <returns>Скорость точки на сплайне</returns>
        public abstract Vector3Df CalculateFirstDerivative(float time);


        /// <summary>
        /// Растеризация сплайна - вычисление точек отрезков для рисования сплайна
        /// </summary>
        /// <remarks>
        /// Качество(степень) растеризации зависит от свойства <see cref="SegmentsSpline"/>
        /// </remarks>
        public virtual void ComputeDrawingPoints()
        {
            _drawingPoints.Clear();
            var prev = _controlPoints[0];
            _drawingPoints.Add(prev);
            for (var i = 1; i < _segmentsSpline; i++)
            {
                var time = (float)i / _segmentsSpline;
                var point = CalculatePoint(time);

                // Добавляем если длина больше
                if ((point - prev).SqrLength > MinimalSqrLine)
                {
                    _drawingPoints.Add(point);
                    prev = point;
                }
            }

            _drawingPoints.Add(EndPoint);

            // Проверяем еще раз
            CheckCorrectEndPoint();
        }


        /// <summary>
        /// Вычисление длины сплайна
        /// </summary>
        /// <remarks>
        /// Реализация по умолчанию вычисляет длину сплайна по сумме отрезков растеризации
        /// </remarks>
        public virtual void ComputeLengthSpline()
        {
            _length = 0;
            _segmentsPath.Clear();
            for (var i = 1; i < _drawingPoints.Count; i++)
            {
                var length = (_drawingPoints[i] - _drawingPoints[i - 1]).Length;
                _length += length;
                var segment = new TMoveSegment(length, _length);
                _segmentsPath.Add(segment);
            }
        }


        /// <summary>
        /// Вычисление выпуклого четырехугольника, заданного опорными точками внутри которого находится сплайн
        /// </summary>
        /// <remarks>
        /// Реализация по умолчанию вычисляет прямоугольник на основе контрольных точек
        /// </remarks>
        /// <returns>Прямоугольник</returns>
        public virtual Rect GetArea()
        {
            float xMin = StartPoint.X, xMax = EndPoint.X, yMin = StartPoint.Y, yMax = EndPoint.Y;

            for (var i = 0; i < _controlPoints.Length; i++)
            {
                var point = _controlPoints[i];
                if (point.X < xMin) xMin = point.X;
                if (point.X > xMax) xMax = point.X;
                if (point.Y < yMin) yMin = point.Y;
                if (point.Y > yMax) yMax = point.Y;
            }

            return new Rect
            {
                x = xMin,
                width = xMax - xMin,
                y = yMin,
                height = yMax - yMin
            };
        }
        #endregion

        #region IMove methods
        /// <summary>
        /// Получение позиции на сплайне
        /// </summary>
        /// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
        /// <returns>Позиция на сплайне</returns>
        public Vector3Df GetMovePosition(float position)
        {
            // Ограничения
            if (position >= Length)
            {
                return transform.TransformPoint(EndPoint).ToVector3Df();
            }
            if (position <= 0.0f)
            {
                return transform.TransformPoint(StartPoint).ToVector3Df();
            }

            // Находим в какой отрезок попадает эта позиция
            _currentSegmentPath = 0;
            for (var i = 0; i < _segmentsPath.Count; i++)
            {
                // Если позиция меньше значит она попала в отрезок
                if (position < _segmentsPath[i].Summa)
                {
                    _currentSegmentPath = i;
                    break;
                }
            }

            // Перемещение относительно данного сегмента
            float delta;
            if (_currentSegmentPath == 0)
            {
                delta = position / _segmentsPath[_currentSegmentPath].Length;
            }
            else
            {
                delta = (position - _segmentsPath[_currentSegmentPath - 1].Summa) / _segmentsPath[_currentSegmentPath].Length;
            }

            // Интерполируем позицию
            _currentPosition = transform.TransformPoint(Vector3Df.Lerp(_drawingPoints[_currentSegmentPath],
                _drawingPoints[_currentSegmentPath + 1], delta)).ToVector3Df();

            return _currentPosition;
        }


        /// <summary>
        /// Получение скорости на сплайне
        /// </summary>
        /// <remarks>
        /// Это коэффициент изменения скорости на сплайне применяется для регулировки скорости
        /// </remarks>
        /// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
        /// <returns>Скорость на сплайне</returns>
        public virtual float GetMoveVelocity(float position)
        {
            return 1.0f;
        }


        /// <summary>
        /// Получение направления на сплайне
        /// </summary>
        /// <param name="position">Положение точки на сплайне от 0 до длины сплайна</param>
        /// <returns>Направление на сплайне</returns>
        public Vector3Df GetMoveDirection(float position)
        {
            var index = _currentSegmentPath;
            if (_currentSegmentPath != _segmentsPath.Count - 1)
            {
                index = _currentSegmentPath + 1;
            }

            return transform.TransformPoint(_drawingPoints[index + 1]).ToVector3Df();
        }


        /// <summary>
        /// Получение положения на сплайне
        /// </summary>
        /// <param name="position">Позиция на сплайне</param>
        /// <param name="epsilon">Погрешность при сравнении</param>
        /// <returns>Положение точки от 0 до 1</returns>
        public virtual float GetMoveTime(Vector3Df position, float epsilon = 0.01f)
        {
            // Просматриваем все отрезки и находим нужное положение
            for (var i = 1; i < _drawingPoints.Count; i++)
            {
                var p1 = _drawingPoints[i] - _drawingPoints[i - 1];
                var p2 = position - _drawingPoints[i - 1];

                var angle = Vector3Df.Dot(p2, p1) / Vector3Df.Dot(p2, p1);
                if (angle + epsilon > 0 && angle - epsilon < 1)
                {
                    // TODO: Найти точное положение на всей кривой
                    return angle;
                }
            }

            return -1;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Переустановка параметров сплайна
        /// </summary>
        protected virtual void OnResetSpline()
        {
            _drawingPoints = new List<Vector3Df>(10);
            _drawingPoints.Add(Vector3Df.Zero);

            _segmentsPath = new List<TMoveSegment>();
            _segmentsSpline = 10;
        }


        /// <summary>
        /// Первичная инициализация параметров сплайна
        /// </summary>
        protected virtual void OnInitSpline()
        {
            if (_drawingPoints == null)
            {
                _drawingPoints = new List<Vector3Df>(10);
                _drawingPoints.Add(Vector3Df.Zero);
            }

            if (_segmentsPath == null)
            {
                _segmentsPath = new List<TMoveSegment>();
            }

            if (_segmentsSpline < 1)
            {
                _segmentsSpline = 10;
            }
        }


        /// <summary>
        /// Обновление параметров сплайна
        /// </summary>
        /// <remarks>
        /// Реализация по умолчанию обновляет список точек для рисования и длину сплайна
        /// </remarks>
        protected virtual void OnUpdateSpline()
        {
            ComputeDrawingPoints();
            ComputeLengthSpline();
        }


        /// <summary>
        /// Проверка и корректировка расположения первой точки на сплайне.
        /// Применяется когда надо скорректировать отображения сплайна при его замкнутости
        /// </summary>
        protected virtual void CheckCorrectStartPoint()
        {
            _drawingPoints[_drawingPoints.Count - 1] = StartPoint;
            if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
            {
                _drawingPoints.RemoveAt(_drawingPoints.Count - 1);
                _drawingPoints[_drawingPoints.Count - 1] = StartPoint;
            }
        }


        /// <summary>
        /// Проверка и корректировка расположения последней точки на сплайне
        /// </summary>
        protected virtual void CheckCorrectEndPoint()
        {
            _drawingPoints[_drawingPoints.Count - 1] = EndPoint;
            if ((_drawingPoints[_drawingPoints.Count - 1] - _drawingPoints[_drawingPoints.Count - 2]).SqrLength < MinimalSqrLine)
            {
                _drawingPoints.RemoveAt(_drawingPoints.Count - 1);
                _drawingPoints[_drawingPoints.Count - 1] = EndPoint;
            }
        }


        /// <summary>
        /// Применение точек сплайна к компоненту LineRenderer
        /// </summary>
        protected void ApplyDrawingPointsToLineRenderer()
        {
            if (!TryGetComponent(out LineRenderer lineRenderer) || _drawingPoints == null)
            {
                return;
            }

            var positions = new Vector3[_drawingPoints.Count];
            for (var i = 0; i < _drawingPoints.Count; i++)
            {
                positions[i] = transform.TransformPoint(_drawingPoints[i]);
            }

            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
        #endregion

        #region Control point methods
        /// <summary>
        /// Получение контрольной точки сплайна по индексу в локальных координатах
        /// </summary>
        /// <param name="index">Позиция(индекс) точки</param>
        /// <returns>Контрольная точка сплайна в локальных координатах</returns>
        public virtual Vector3Df GetControlPoint(int index)
        {
            return _controlPoints[index];
        }


        /// <summary>
        /// Получение контрольной точки сплайна по индексу в мировых координатах
        /// </summary>
        /// <param name="index">Позиция(индекс) точки</param>
        /// <returns>Контрольная точка сплайна в мировых координатах</returns>
        public virtual Vector3Df GetControlPointWorld(int index)
        {
            return transform.TransformPoint(_controlPoints[index]).ToVector3Df();
        }


        /// <summary>
        /// Установка контрольной точки сплайна по индексу в локальных координатах
        /// </summary>
        /// <param name="index">Позиция(индекс) точки</param>
        /// <param name="point">Контрольная точка сплайна в локальных координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void SetControlPoint(int index, Vector3Df point, bool updateSpline = false)
        {
            _controlPoints[index] = point;
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
        public virtual void SetControlPointWorld(int index, Vector3Df point, bool updateSpline = false)
        {
            _controlPoints[index] = transform.InverseTransformPoint(point).ToVector3Df();
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Добавление контрольной точки к сплайну в локальных координатах
        /// </summary>
        /// <param name="point">Контрольная точка сплайна в локальных координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void AddControlPoint(Vector3Df point, bool updateSpline = false)
        {
            Array.Resize(ref _controlPoints, _controlPoints.Length + 1);
            _controlPoints[_controlPoints.Length - 1] = point;
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Добавление контрольной точки к сплайну в мировых координатах
        /// </summary>
        /// <param name="point">Точка сплайна в мировых координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void AddControlPointWorld(Vector3Df point, bool updateSpline = false)
        {
            Array.Resize(ref _controlPoints, _controlPoints.Length + 1);
            _controlPoints[_controlPoints.Length - 1] = transform.InverseTransformPoint(point).ToVector3Df();
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Вставка контрольной точки в сплайн в локальных координатах в указанной позиции
        /// </summary>
        /// <param name="index">Позиция(индекс) вставки точки</param>
        /// <param name="point">Контрольная точка сплайна в локальных координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void InsertControlPoint(int index, Vector3Df point, bool updateSpline = false)
        {
            _controlPoints = XArrayHelper.InsertAt(_controlPoints, point, index);
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Вставка контрольной точки в сплайн в мировых координатах в указанной позиции
        /// </summary>
        /// <param name="index">Позиция(индекс) вставки точки</param>
        /// <param name="point">Точка сплайна в мировых координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void InsertControlPointWorld(int index, Vector3Df point, bool updateSpline = false)
        {
            point = transform.InverseTransformPoint(point).ToVector3Df();
            _controlPoints = XArrayHelper.InsertAt(_controlPoints, point, index);
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Удаление контрольной точки
        /// </summary>
        /// <param name="index">Позиция(индекс) удаляемой точки</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void RemoveControlPoint(int index, bool updateSpline = false)
        {
            _controlPoints = XArrayHelper.RemoveAt(_controlPoints, index);
            if (updateSpline)
            {
                OnUpdateSpline();
            }
        }


        /// <summary>
        /// Удаление контрольной точки в локальных координатах
        /// </summary>
        /// <param name="point">Контрольная точка сплайна в локальных координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void RemoveControlPoint(Vector3Df point, bool updateSpline = false)
        {
            for (var i = 0; i < _controlPoints.Length; i++)
            {
                if (_controlPoints[i].Approximately(in point))
                {
                    _controlPoints = XArrayHelper.RemoveAt(_controlPoints, i);
                    if (updateSpline)
                    {
                        OnUpdateSpline();
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// Удаление контрольной точки в мировых координатах
        /// </summary>
        /// <param name="point">Контрольная точка сплайна в мировых координатах</param>
        /// <param name="updateSpline">Статус обновления сплайна</param>
        public virtual void RemoveControlPointWorld(Vector3Df point, bool updateSpline = false)
        {
            point = transform.InverseTransformPoint(point).ToVector3Df();
            for (var i = 0; i < _controlPoints.Length; i++)
            {
                if (_controlPoints[i].Approximately(in point))
                {
                    _controlPoints = XArrayHelper.RemoveAt(_controlPoints, i);
                    if (updateSpline)
                    {
                        OnUpdateSpline();
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// Удаление всех контрольных точек
        /// </summary>
        public virtual void ClearControlPoints()
        {
            Array.Clear(_controlPoints, 0, _controlPoints.Length);
        }
        #endregion
    }
    /**@}*/
}
#endif
