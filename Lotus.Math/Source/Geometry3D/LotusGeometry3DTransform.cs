using System;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry3D
	*@{*/
    /// <summary>
    /// Трансформация трехмерного объекта.
    /// </summary>
    /// <remarks>
    /// Реализация типа реализующего комплексную трансформацию, удобное и эффективное управления параметрами
    /// трансформации трехмерного объекта
    /// </remarks>
    [Serializable]
    public struct Transform3Df
    {
        #region Fields
        internal Vector3Df _pivot;
        internal Vector3Df _offset;
        internal Quaternion3Df _rotation;
        internal Vector3Df _forward;
        internal Vector3Df _right;
        internal Vector3Df _up;
        internal bool _updateOrt;
        #endregion

        #region Properties
        /// <summary>
        /// Опорная точка трехмерного объекта.
        /// </summary>
        public Vector3Df Pivot
        {
            readonly get { return _pivot; }
            set
            {
                _pivot = value;
            }
        }

        /// <summary>
        /// Смещение трехмерного объекта.
        /// </summary>
        public Vector3Df Offset
        {
            readonly get { return _offset; }
            set
            {
                _offset = value;
            }
        }

        /// <summary>
        /// Вращение трехмерного объекта.
        /// </summary>
        public Quaternion3Df Rotation
        {
            readonly get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }

        /// <summary>
        /// Вектор "вперед".
        /// </summary>
        public Vector3Df Forward
        {
            readonly get { return _forward; }
            set
            {
                _forward = value;
            }
        }

        /// <summary>
        /// Вектор "вправо".
        /// </summary>
        public Vector3Df Right
        {
            readonly get { return _right; }
            set
            {
                _right = value;
            }
        }

        /// <summary>
        /// Вектор вверх.
        /// </summary>
        public Vector3Df Up
        {
            readonly get { return _up; }
            set
            {
                _up = value;
            }
        }

        /// <summary>
        /// Матрица трансформации объекта.
        /// </summary>
        public readonly Matrix4Dx4 MatrixTransform
        {
            get
            {
                var transform = new Matrix4Dx4();

                var pos = Pivot;
                var x = -Vector3Df.Dot(in _right, in pos);
                var y = -Vector3Df.Dot(in _up, in pos);
                var z = -Vector3Df.Dot(in _forward, in pos);

                transform.M11 = _right.X;
                transform.M12 = _up.X;
                transform.M13 = _forward.X;
                transform.M14 = 0.0;

                transform.M21 = _right.Y;
                transform.M22 = _up.Y;
                transform.M23 = _forward.Y;
                transform.M24 = 0.0;

                transform.M31 = _right.Z;
                transform.M32 = _up.Z;
                transform.M33 = _forward.Z;
                transform.M34 = 0.0;

                transform.M41 = x;
                transform.M42 = y;
                transform.M43 = z;
                transform.M44 = 1.0;

                return transform;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует трансформацию указанными параметрами.
        /// </summary>
        /// <param name="pivot">Опорная точка объекта.</param>
        public Transform3Df(Vector3Df pivot)
        {
            _pivot = pivot;
            _offset = Vector3Df.Zero;
            _rotation = Quaternion3Df.Identity;
            _forward = Vector3Df.Forward;
            _right = Vector3Df.Right;
            _up = Vector3Df.Up;
            _updateOrt = false;
        }
        #endregion

        #region Transform methods
        /// <summary>
        /// Установка локального вращения объекта вокруг собственной оси.
        /// </summary>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол вращения, задается в градусах.</param>
        public void SetRotate(in Vector3Df axis, float angle)
        {
            // Локальное вращение
            _rotation = new Quaternion3Df(axis, angle);

            // Трансформируем орты
            _up = _rotation.TransformVector(Vector3Df.Up);
            _forward = _rotation.TransformVector(Vector3Df.Forward);
            _right = _rotation.TransformVector(Vector3Df.Right);
            _updateOrt = true;
        }

        /// <summary>
        /// Локальное вращение объекта вокруг собственной оси.
        /// </summary>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол приращения, задается в градусах.</param>
        public readonly void Rotate(in Vector3Df axis, float angle)
        {
            //// Локальное вращение
            //mAngleAxisPivot += angle;
            //_rotation = new Quaternion3D(axis, mAngleAxisPivot);

            //// Трансформируем орты
            //_up = _rotation.TransformVector(Vector3Df.Up);
            //_forward = _rotation.TransformVector(Vector3Df.Forward);
            //_right = _rotation.TransformVector(Vector3Df.Right);
            //_updateOrt = true;
        }

        /// <summary>
        /// Установка вращения объекта вокруг оси и определенного центра.
        /// </summary>
        /// <param name="center">Центр вращения.</param>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол вращения, задается в градусах.</param>
        public readonly void SetRotateAround(in Vector3Df center, in Vector3Df axis, float angle)
        {
            // Глобальное вращение
            //Quaternion3D rotation = new Quaternion3D(axis, angle + 180);
            //mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
            //mPositionGlobal = center;
            //mAngleAxisGlobal = angle;
        }

        /// <summary>
        /// Установка вращения объекта вокруг оси и определенного центра.
        /// </summary>
        /// <param name="center">Центр вращения.</param>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол вращения, задается в градусах.</param>
        /// <param name="parent">Родительская трансформация.</param>
        public readonly void SetRotateAround(in Vector3Df center, in Vector3Df axis, float angle, Transform3Df parent)
        {
            // Глобальное вращение
            //Vector3Df axis_d = new Vector3Df(axis);
            //Quaternion3D rotation = new Quaternion3D(axis_d, angle + 180);
            //mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
            //mPositionGlobal = center - parent.Pivot;
            //mAngleAxisGlobal = angle;
        }

        /// <summary>
        /// Вращение объекта вокруг оси и определенного центра.
        /// </summary>
        /// <param name="center">Центр вращения.</param>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол приращения, задается в градусах.</param>
        public readonly void RotateAround(in Vector3Df center, in Vector3Df axis, float angle)
        {
            // Глобальное вращение
            //mAngleAxisGlobal += angle;
            //Quaternion3D rotation = new Quaternion3D(axis, mAngleAxisGlobal + 180);
            //mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
            //mPositionGlobal = center;
        }

        /// <summary>
        /// Вращение объекта вокруг оси и определенного центра.
        /// </summary>
        /// <param name="center">Центр вращения.</param>
        /// <param name="axis">Ось вращения.</param>
        /// <param name="angle">Угол приращения, задается в градусах.</param>
        /// <param name="parent">Родительская трансформация.</param>
        public readonly void RotateAround(in Vector3Df center, in Vector3Df axis, float angle, Transform3Df parent)
        {
            //// Глобальное вращение
            //Vector3Df axis_d = new Vector3Df(axis);
            //mAngleAxisGlobal += angle;
            //Quaternion3D rotation = new Quaternion3D(axis_d, mAngleAxisGlobal + 180);

            //// 1) Переносим позицию в локальный центр
            ////Vector3Df position = mOriginalPosition + center;
            //mPositionPivot = rotation.TransformVector(ref mOriginalPosition);
            //mPositionGlobal = center;
        }

        /// <summary>
        /// Установка направления взгляда объекта на определенную точку.
        /// </summary>
        /// <param name="point">Точка.</param>
        /// <param name="up">Вектор вверх.</param>
        public void LookAt(Vector3Df point, Vector3Df up)
        {
            var direction = point - Pivot;
            direction.Normalize();
            _rotation.SetLookRotation(in direction, in up);

            // Трансформируем орты
            _forward = direction;
            _up = up;
            _right = Vector3Df.Cross(in _up, in _forward);
            _updateOrt = true;
        }

        /// <summary>
        /// Обновление орт.
        /// </summary>
        public void Update()
        {
            if (_updateOrt)
            {
                _up = Vector3Df.Cross(in _forward, in _right);
                _right = Vector3Df.Cross(in _up, in _forward);


                _forward.Normalize();
                _right.Normalize();
                _up.Normalize();
                _updateOrt = false;
            }
        }
        #endregion
    }
    /**@}*/
}