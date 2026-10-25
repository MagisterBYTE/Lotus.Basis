#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Maths
{
    /** \addtogroup MathSpline
    *@{*/
    /// <summary>
    /// Компонент представляющий кубическую кривую Безье
    /// </summary>
    /// <remarks>
    /// Реализация компонента представляющего собой кубическую кривую Безье
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Math/Spline/BezierCubic Spline")]
    public class LotusSplineBezierCubic : LotusSplineBase
    {
        #region Properties
        /// <summary>
        /// Первая управляющая точка
        /// </summary>
        public Vector3Df HandlePoint1
        {
            get { return _controlPoints[1]; }
            set { _controlPoints[1] = value; }
        }

        /// <summary>
        /// Вторая управляющая точка
        /// </summary>
        public Vector3Df HandlePoint2
        {
            get { return _controlPoints[2]; }
            set { _controlPoints[2] = value; }
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
            // Рисуем опорные и управляющие точки
            OnDrawGizmosControlPoint(0);
            OnDrawGizmosHandlePoint(1);
            OnDrawGizmosHandlePoint(2);
            OnDrawGizmosControlPoint(3);

            // Рисуем соединение
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GetControlPointWorld(0), GetControlPointWorld(1));
            Gizmos.DrawLine(GetControlPointWorld(2), GetControlPointWorld(3));

            // Кривая
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
            return BezierCubic3D.CalculatePoint(time,
                in _controlPoints[0],
                in _controlPoints[1],
                in _controlPoints[2],
                in _controlPoints[3]);
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
            return BezierCubic3D.CalculateFirstDerivative(time,
                in _controlPoints[0],
                in _controlPoints[1],
                in _controlPoints[2],
                in _controlPoints[3]);
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
        #endregion

        #region Control point methods
        /// <summary>
        /// Вычисление точки на кривой Безье представленной с помощью четырех контрольных точек
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой</param>
        /// <returns>Позиция точки на кривой Безье</returns>
        public Vector3Df CalculatePointWorld(float time)
        {
            return transform.TransformPoint(CalculatePoint(time)).ToVector3Df();
        }


        /// <summary>
        /// Вычисление точки на кривой Безье представленной с помощью четырех контрольных точек
        /// </summary>
        /// <param name="time">Положение точки от 0 до 1, где 0 соответствует крайней "левой" точки, 1 соответствует крайне
        /// "правой" конечной точки кривой</param>
        /// <returns>Позиция точки на кривой Безье</returns>
        [Obsolete("Используйте CalculatePointWorld")]
        public Vector3Df CalculatePointToWorld(float time)
        {
            return CalculatePointWorld(time);
        }


        /// <summary>
        /// Создание кубической кривой Безье проходящий через заданные точки на равномерно заданном времени
        /// </summary>
        /// <param name="start">Начальная точка</param>
        /// <param name="point1">Первая точка</param>
        /// <param name="point2">Вторая точка</param>
        /// <param name="end">Конечная точка</param>
        public void CreateFromPassesPoint(Vector3Df start, Vector3Df point1, Vector3Df point2, Vector3Df end)
        {
            _controlPoints[0] = start;
            _controlPoints[1].X = ((-5 * start.X) + (18 * point1.X) - (9 * point2.X) + (2 * end.X)) / 6;
            _controlPoints[1].Y = ((-5 * start.Y) + (18 * point1.Y) - (9 * point2.Y) + (2 * end.Y)) / 6;
            _controlPoints[2].X = ((2 * start.X) - (9 * point1.X) + (18 * point2.X) - (5 * end.X)) / 6;
            _controlPoints[2].Y = ((2 * start.Y) - (9 * point1.Y) + (18 * point2.Y) - (5 * end.Y)) / 6;
            _controlPoints[3] = end;
        }


        /// <summary>
        /// Проверка на управляющую точку
        /// </summary>
        /// <param name="index">Позиция(индекс) контрольной точки</param>
        /// <returns>Статус управляющей точки</returns>
        public bool IsHandlePoint(int index)
        {
            return index == 1 || index == 2;
        }
        #endregion
    }
    /**@}*/
}
#endif
