using System.Collections.Generic;

namespace Lotus.Maths
{
    /**
     * \defgroup MathGeometry3D Подсистема 3D геометрии
     * \ingroup Math
     * \brief Подсистема 3D геометрии реализует работу с геометрическими данными в 3D пространстве.
     * \details Сюда входит математические структуры данных для работы в 3D пространстве, алгоритмы поиска и нахождения
        ближайших точек проекции, пересечения и вычисления дистанции для основных геометрических тел/примитивов.
     * @{
     */
    /// <summary>
    /// Статический класс реализующий вспомогательные методы для работы с 3D пространством.
    /// </summary>
    /// <remarks>
    /// <para>
    /// При генерации и осуществлении поворотов на отдельных плоскостях используется система координат связанная с Unity.
    /// Вид на плоскость выбирается таким образом чтобы соответствующие координатные оси увеличивались 
    /// снизу на вверх и слева на право.
    /// </para>
    /// <para>
    /// Также концепция вида используется и при работе с трехмерной сеткой.
    /// </para>
    /// </remarks>
    public static class XGeometry3D
    {
        #region Fields
        /// <summary>
        /// Точность вещественного числа используемого при операция поиска/пересечения геометрических примитивов.
        /// </summary>
        /// <remarks>
        /// Это не константа, её можно регулировать для обеспечения нужной точности вычислений.
        /// </remarks>
        public static float Epsilon_f = 0.001f;

        /// <summary>
        /// Точность вещественного числа используемого при операция поиска/пересечения геометрических примитивов.
        /// </summary>
        /// <remarks>
        /// Это не константа, её можно регулировать для обеспечения нужной точности вычислений.
        /// </remarks>
        public static float Epsilon_d = 0.00f;
        #endregion

        #region Main methods
        /// <summary>
        /// Проекция угла между двумя векторами на выбранную ось.
        /// </summary>
        /// <param name="dirA">Вектор А.</param>
        /// <param name="dirB">Вектор B.</param>
        /// <param name="axis">Ось проекции.</param>
        /// <returns>Угол в градусах.</returns>
        public static double AngleAroundAxis(in Vector3D dirA, in Vector3D dirB, in Vector3D axis)
        {
            return 0;
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Проекция угла между двумя векторами на выбранную ось.
		/// </summary>
		/// <param name="dirA">Вектор А.</param>
		/// <param name="dirB">Вектор B.</param>
		/// <param name="axis">Ось проекции.</param>
		/// <returns>Угол в градусах.</returns>
		public static Single AngleAroundAxis(UnityEngine.Vector3 dirA, UnityEngine.Vector3 dirB, in UnityEngine.Vector3 axis)
		{
			// TODO Project A and B onto the plane orthogonal target axis
			dirA = dirA - UnityEngine.Vector3.Project(dirA, axis);
			dirB = dirB - UnityEngine.Vector3.Project(dirB, axis);

			// Find (positive) angle between A and B
			var angle = UnityEngine.Vector3.Angle(dirA, dirB);

			// Return angle multiplied with 1 or -1
			return angle * (UnityEngine.Vector3.Dot(axis, UnityEngine.Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
		}

		/// <summary>
		/// Вращение единичного вектора на окружности в плоскости XZ (Top - вид сверху).
		/// </summary>
		/// <remarks>
		/// Вращение осуществляется против часовой стрелки
		/// При проекции:
		/// Горизонтальной ось становиться координата X
		/// Вертикальной осью становиться координата Z
		/// </remarks>
		/// <param name="radius">Радиус окружности.</param>
		/// <param name="angle">Угол в градусах.</param>
		/// <returns>Вектор.</returns>
		public static UnityEngine.Vector3 RotationVectorXZ(Single radius, Single angle)
		{
			Single angleInRadians = angle * XMath.DegreeToRadian_F;
			var x = radius * XMath.Cos(angleInRadians);
			var y = radius * XMath.Sin(angleInRadians);
			var result = new UnityEngine.Vector3(x, 0, y);

			return (result);
		}

		/// <summary>
		/// Вращение единичного вектора на окружности в плоскости ZY (Right - вид справа).
		/// </summary>
		/// <remarks>
		/// Вращение осуществляется против часовой стрелки
		/// При проекции:
		/// Горизонтальной ось становиться координата Z
		/// Вертикальной осью становиться координата Y
		/// </remarks>
		/// <param name="radius">Радиус окружности.</param>
		/// <param name="angle">Угол в градусах.</param>
		/// <returns>Вектор.</returns>
		public static UnityEngine.Vector3 RotationVectorZY(Single radius, Single angle)
		{
			Single angleInRadians = angle * XMath.DegreeToRadian_F;
			var x = radius * XMath.Cos(angleInRadians);
			var y = radius * XMath.Sin(angleInRadians);
			var result = new UnityEngine.Vector3(0, y, x);

			return (result);
		}

		/// <summary>
		/// Вращение единичного вектора на окружности в плоскости XY (Back - вид с сзади).
		/// </summary>
		/// <remarks>
		/// Вращение осуществляется против часовой стрелки
		/// При проекции:
		/// Горизонтальной ось становиться координата X
		/// Вертикальной осью становиться координата Y
		/// </remarks>
		/// <param name="radius">Радиус окружности.</param>
		/// <param name="angle">Угол в градусах.</param>
		/// <returns>Вектор.</returns>
		public static UnityEngine.Vector3 RotationVectorXY(Single radius, Single angle)
		{
			Single angleInRadians = angle * XMath.DegreeToRadian_F;
			var x = radius * XMath.Cos(angleInRadians);
			var y = radius * XMath.Sin(angleInRadians);

			var result = new UnityEngine.Vector3(x, y, 0);

			return (result);
		}
#endif
        #endregion

        #region Generating points on circle 
        /// <summary>
        /// Получение точки на окружности в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <remarks>
        /// Вращение осуществляется против часовой стрелки
        /// При проекции:
        /// Горизонтальной ось становиться координата X
        /// Вертикальной осью становиться координата Z
        /// </remarks>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Точка.</returns>
        public static Vector3Df GetPointOnCircleXZ(float radius, float angle)
        {
            var angleInRadians = angle * XMath.DegreeToRadian_F;
            var x = radius * XMath.Cos(angleInRadians);
            var y = radius * XMath.Sin(angleInRadians);

            var result = new Vector3Df(x, 0, y);

            return result;
        }

        /// <summary>
        /// Получение точки на окружности в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <remarks>
        /// Вращение осуществляется против часовой стрелки
        /// При проекции:
        /// Горизонтальной ось становиться координата Z
        /// Вертикальной осью становиться координата Y
        /// </remarks>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Точка.</returns>
        public static Vector3Df GetPointOnCircleZY(float radius, float angle)
        {
            var angleInRadians = angle * XMath.DegreeToRadian_F;
            var x = radius * XMath.Cos(angleInRadians);
            var y = radius * XMath.Sin(angleInRadians);
            var result = new Vector3Df(0, y, x);

            return result;
        }

        /// <summary>
        /// Получение точки на окружности в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <remarks>
        /// Вращение осуществляется против часовой стрелки
        /// При проекции:
        /// Горизонтальной ось становиться координата X
        /// Вертикальной осью становиться координата Y
        /// </remarks>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Точка.</returns>
        public static Vector3Df GetPointOnCircleXY(float radius, float angle)
        {
            var angleInRadians = angle * XMath.DegreeToRadian_F;
            var x = radius * XMath.Cos(angleInRadians);
            var y = radius * XMath.Sin(angleInRadians);

            var result = new Vector3Df(x, y, 0);

            return result;
        }

        /// <summary>
        /// Получение списка точек на окружности в плоскости XZ (Top - вид сверху).
        /// </summary>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="segments">Количество сегментов окружности.</param>
        /// <param name="startAngle">Начальный угол (в градусах) для генерации точек.</param>
        /// <returns>Список точек.</returns>
        public static List<Vector3Df> GeneratePointsOnCircleXZ(float radius, int segments, float startAngle = 0)
        {
            var segmentAngle = 360f / segments;
            var currentAngle = startAngle;
            var ring = new List<Vector3Df>(segments);
            for (var i = 0; i < segments; i++)
            {
                ring.Add(GetPointOnCircleXZ(radius, currentAngle));
                currentAngle += segmentAngle;
            }
            return ring;
        }

        /// <summary>
        /// Получение списка точек на окружности в плоскости ZY (Right - вид справа).
        /// </summary>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="segments">Количество сегментов окружности.</param>
        /// <param name="startAngle">Начальный угол (в градусах) для генерации точек.</param>
        /// <returns>Список точек.</returns>
        public static List<Vector3Df> GeneratePointsOnCircleZY(float radius, int segments, float startAngle = 0)
        {
            var segmentAngle = 360f / segments;
            var currentAngle = startAngle;
            var ring = new List<Vector3Df>(segments);
            for (var i = 0; i < segments; i++)
            {
                ring.Add(GetPointOnCircleZY(radius, currentAngle));
                currentAngle += segmentAngle;
            }
            return ring;
        }

        /// <summary>
        /// Получение списка точек на окружности в плоскости XY (Back - вид с сзади).
        /// </summary>
        /// <param name="radius">Радиус окружности.</param>
        /// <param name="segments">Количество сегментов окружности.</param>
        /// <param name="startAngle">Начальный угол (в градусах) для генерации точек.</param>
        /// <returns>Список точек.</returns>
        public static List<Vector3Df> GeneratePointsOnCircleXY(float radius, int segments, float startAngle = 0)
        {
            var segmentAngle = 360f / segments;
            var currentAngle = startAngle;
            var ring = new List<Vector3Df>(segments);
            for (var i = 0; i < segments; i++)
            {
                ring.Add(GetPointOnCircleXY(radius, currentAngle));
                currentAngle += segmentAngle;
            }
            return ring;
        }
        #endregion

        #region Generating points on sphere  
        /// <summary>
        /// Получение точки на сфере в географической системе координат.
        /// </summary>
        /// <para>
        /// Экватор расположен в плоскости XZ, высота по координате Y
        /// </para>
        /// <param name="radius">Радиус сферы.</param>
        /// <param name="horizontalAngle">Горизонтальный угол в градусах в пределах [0, 359].</param>
        /// <param name="verticalAngle">Вертикальный угол в градусах в пределах[-90, 90].</param>
        /// <returns>Сгенерированная точка.</returns>
        public static Vector3Df GetPointOnSphere(float radius, float horizontalAngle, float verticalAngle)
        {
            return Vector3Df.FromSpherical(radius, horizontalAngle, verticalAngle);
        }

        /// <summary>
        /// Получение точки на сфероиде в географической системе координат.
        /// </summary>
        /// <param name="radius">Радиус сфероида.</param>
        /// <param name="height">Высота сфероида.</param>
        /// <param name="horizontalAngle">Горизонтальный угол в градусах [0, 360].</param>
        /// <param name="verticalAngle">Вертикальный угол в градусах [-90, 90].</param>
        /// <returns>Сгенерированная точка.</returns>
        public static Vector3Df PointOnSpheroid(float radius, float height, float horizontalAngle, float verticalAngle)
        {
            var horizontalRadians = horizontalAngle * XMath.DegreeToRadian_F;
            var verticalRadians = verticalAngle * XMath.DegreeToRadian_F;
            var cosVertical = XMath.Cos(verticalRadians);

            return new Vector3Df(
                x: radius * XMath.Sin(horizontalRadians) * cosVertical,
                y: height * XMath.Sin(verticalRadians),
                z: radius * XMath.Cos(horizontalRadians) * cosVertical);
        }

        /// <summary>
        /// Получение точки на каплевидной поверхности в географической системе координат.
        /// </summary>
        /// <param name="radius">Радиус.</param>
        /// <param name="height">Высота.</param>
        /// <param name="horizontalAngle">Горизонтальный угол в градусах [0, 360].</param>
        /// <param name="verticalAngle">Вертикальный угол в градусах [-90, 90].</param>
        /// <returns>Сгенерированная точка.</returns>
        public static Vector3Df PointOnTeardrop(float radius, float height, float horizontalAngle, float verticalAngle)
        {
            var horizontalRadians = horizontalAngle * XMath.DegreeToRadian_F;
            var verticalRadians = verticalAngle * XMath.DegreeToRadian_F;
            var sinVertical = XMath.Sin(verticalRadians);
            var teardrop = (1 - sinVertical) * XMath.Cos(verticalRadians) / 2;

            return new Vector3Df(
                x: radius * XMath.Sin(horizontalRadians) * teardrop,
                y: height * sinVertical,
                z: radius * XMath.Cos(horizontalRadians) * teardrop);
        }
        #endregion

        #region Generating texture coordinates 
        /// <summary>
        /// Получение координат текстурной развертки на окружности.
        /// </summary>
        /// <param name="angle">Угол в градусах.</param>
        /// <returns>Текстурные координаты.</returns>
        public static Vector2Df GetMapUVOnCircle(float angle)
        {
            var angleInRadians = angle * XMath.DegreeToRadian_F;
            return new Vector2Df((0.5f * XMath.Sin(angleInRadians)) + 0.5f,
                (0.5f * XMath.Cos(angleInRadians)) + 0.5f);
        }

        /// <summary>
        /// Получение списка координат текстурной развертки на окружности.
        /// </summary>
        /// <param name="segments">Количество сегментов окружности.</param>
        /// <param name="startAngle">Начальный угол (в градусах) для генерации точек.</param>
        /// <returns>Список текстурных координат.</returns>
        public static List<Vector2Df> GetMapUVsOnCircle(int segments, float startAngle = 0)
        {
            var segmentAngle = 360f / segments;
            var currentAngle = startAngle;
            var ring = new List<Vector2Df>(segments);
            for (var i = 0; i < segments; i++)
            {
                ring.Add(GetMapUVOnCircle(currentAngle));
                currentAngle += segmentAngle;
            }
            return ring;
        }

        /// <summary>
        /// Получение координат текстурной развертки из сферических координат.
        /// </summary>
        /// <param name="theta">Угол между осью Y и отрезком, соединяющим начало координат и точку в пределах [0, 180].</param>
        /// <param name="phi">Угол между осью Z и проекцией отрезка, соединяющего начало координат с точкой P, на плоскость XZ в пределах [0, 359].</param>
        /// <returns>Текстурные координаты.</returns>
        public static Vector2Df GetMapUVFromSpherical(float theta, float phi)
        {
            var u = phi / 360;
            var v = theta / 180;
            return new Vector2Df(u, v);
        }
        #endregion
    }
    /**@}*/
}