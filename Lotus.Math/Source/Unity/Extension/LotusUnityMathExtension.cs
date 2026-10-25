#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Maths
{
    /**
     * \defgroup MathUnityExtension Методы расширений Unity
     * \ingroup Math
     * \brief Методы расширения для базовых типов Unity.
     * @{
     */
    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Vector2"/>
    /// </summary>
    public static class XVector2Extension
    {
        #region Main methods
        /// <summary>
        /// Сериализация вектора в строку
        /// </summary>
        /// <param name="this">Двухмерный вектор</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Vector2 @this)
        {
            return string.Format("{0};{1}", @this.x, @this.y);
        }


        /// <summary>
        /// Аппроксимация равенства значений двухмерных векторов
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <param name="other">Сравниваемый вектор</param>
        /// <param name="epsilon">Погрешность</param>
        /// <returns>Статус равенства значений</returns>
        public static bool Approximately(this Vector2 @this, Vector2 other, float epsilon = 0.01f)
        {
            return Mathf.Abs(@this.x - other.x) < epsilon && Mathf.Abs(@this.y - other.y) < epsilon;
        }


        /// <summary>
        /// Проверка на равенство значений векторов по углу между ними
        /// </summary>
        /// <remarks>
        /// Credits: http://answers.unity3d.com/questions/131624/vector3-comparison.html#answer-131672
        /// </remarks>
        /// <param name="this">Исходный вектор</param>
        /// <param name="other">Сравниваемый вектор</param>
        /// <param name="angleError">Погрешность угла</param>
        /// <returns>Статус равенства значений</returns>
        public static bool ApproximatelyFromAngle(this Vector2 @this, Vector2 other, float angleError = 0.01f)
        {
            // If they aren't the same length, don't bother checking the rest.
            if (!Mathf.Approximately(@this.magnitude, other.magnitude))
            {
                return false;
            }

            var cosAngleError = Mathf.Cos(angleError * Mathf.Deg2Rad);

            // A value between -1 and 1 corresponding to the angle.
            // The dot product of normalized Vectors is equal to the cosine of the angle between them.
            // So the closer they are, the closer the value will be to 1. Opposite Vectors will be -1
            // and orthogonal Vectors will be 0.
            var cosAngle = Vector2.Dot(@this.normalized, other.normalized);

            // If angle is greater, that means that the angle between the two vectors is less than the error allowed.
            return cosAngle >= cosAngleError;
        }


        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного против часовой стрелки
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Перпендикулярный вектор</returns>
        [Obsolete("Используйте Vector2.Perpendicular — штатный API Unity с Unity 2018.1")]
        public static Vector2 PerpToCCW(this Vector2 @this)
        {
            return Vector2.Perpendicular(@this);
        }


        /// <summary>
        /// Возвращение перпендикулярного вектора расположенного по часовой стрелке
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Перпендикулярный вектор</returns>
        public static Vector2 PerpToCW(this Vector2 @this)
        {
            return -Vector2.Perpendicular(@this);
        }


        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного против часовой стрелки
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Перпендикулярный вектор</returns>
        [Obsolete("Используйте Vector2.Perpendicular(v).normalized")]
        public static Vector2 UnitPerpToCCW(this Vector2 @this)
        {
            return Vector2.Perpendicular(@this).normalized;
        }


        /// <summary>
        /// Возвращение единичного перпендикулярного вектора расположенного по часовой стрелке
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Перпендикулярный вектор</returns>
        public static Vector2 UnitPerpToCW(this Vector2 @this)
        {
            return (-Vector2.Perpendicular(@this)).normalized;
        }


        /// <summary>
        /// Возвращение скалярного произведения с перпендикулярным вектором
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <param name="vector">Вектор</param>
        /// <returns>Скалярное произведение с перпендикулярным вектором</returns>
        public static float DotPerp(this Vector2 @this, Vector2 vector)
        {
            return (@this.x * vector.y) - (@this.y * vector.x);
        }


        /// <summary>
        /// Возвращение скалярного произведения с перпендикулярным вектором
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <param name="vector">Вектор</param>
        /// <returns>Скалярное произведение с перпендикулярным вектором</returns>
        [Obsolete("Передача Vector2 по ref не нужна. Используйте DotPerp без ref")]
        public static float DotPerp(this Vector2 @this, ref Vector2 vector)
        {
            return DotPerp(@this, vector);
        }


        /// <summary>
        /// Преобразование в целочисленный вектор
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Целочисленный вектор</returns>
        [Obsolete("Усечение через (int) не совпадает со штатным API. Используйте Vector2Int.FloorToInt / Vector2Int.RoundToInt / Vector2Int.CeilToInt")]
        public static Vector2Int ToInt(this Vector2 @this)
        {
            return new Vector2Int((int)@this.x, (int)@this.y);
        }


        /// <summary>
        /// Преобразование в вектор <see cref="Vector2Df"/>
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Вектор Vector2Df</returns>
        public static Vector2Df ToVector2Df(this Vector2 @this)
        {
            return new Vector2Df(@this.x, @this.y);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Vector2Int"/>
    /// </summary>
    public static class XVector2IntExtension
    {
        #region Main methods
        /// <summary>
        /// Сериализация вектора в строку
        /// </summary>
        /// <param name="this">Двухмерный вектор</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Vector2Int @this)
        {
            return string.Format("{0};{1}", @this.x, @this.y);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Vector3"/>
    /// </summary>
    public static class XVector3Extension
    {
        #region Main methods
        /// <summary>
        /// Сериализация вектора в строку
        /// </summary>
        /// <param name="this">Трехмерный вектор</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Vector3 @this)
        {
            return string.Format("{0};{1};{2}", @this.x, @this.y, @this.z);
        }


        /// <summary>
        /// Аппроксимация равенства значений векторов
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <param name="other">Сравниваемый вектор</param>
        /// <param name="epsilon">Погрешность</param>
        /// <returns>Статус равенства значений</returns>
        public static bool Approximately(this Vector3 @this, Vector3 other, float epsilon = 0.01f)
        {
            return Mathf.Abs(@this.x - other.x) < epsilon
                && Mathf.Abs(@this.y - other.y) < epsilon
                && Mathf.Abs(@this.z - other.z) < epsilon;
        }


        /// <summary>
        /// Аппроксимация равенства значений векторов в плоскости XZ
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <param name="other">Сравниваемый вектор</param>
        /// <param name="epsilon">Погрешность</param>
        /// <returns>Статус равенства значений</returns>
        public static bool ApproximatelyPlaneXZ(this Vector3 @this, Vector3 other, float epsilon = 0.01f)
        {
            return Mathf.Abs(@this.x - other.x) < epsilon && Mathf.Abs(@this.z - other.z) < epsilon;
        }


        /// <summary>
        /// Проверка на равенство значений векторов по углу между ними
        /// </summary>
        /// <remarks>
        /// Credits: http://answers.unity3d.com/questions/131624/vector3-comparison.html#answer-131672
        /// </remarks>
        /// <param name="this">Исходный вектор</param>
        /// <param name="other">Сравниваемый вектор</param>
        /// <param name="angleError">Погрешность угла</param>
        /// <returns>Статус равенства значений</returns>
        public static bool ApproximatelyFromAngle(this Vector3 @this, Vector3 other, float angleError = 0.01f)
        {
            // If they aren't the same length, don't bother checking the rest.
            if (!Mathf.Approximately(@this.magnitude, other.magnitude))
            {
                return false;
            }

            var cosAngleError = Mathf.Cos(angleError * Mathf.Deg2Rad);

            // A value between -1 and 1 corresponding to the angle.
            // The dot product of normalized Vectors is equal to the cosine of the angle between them.
            // So the closer they are, the closer the value will be to 1. Opposite Vectors will be -1
            // and orthogonal Vectors will be 0.
            var cosAngle = Vector3.Dot(@this.normalized, other.normalized);

            // If angle is greater, that means that the angle between the two vectors is less than the error allowed.
            return cosAngle >= cosAngleError;
        }


        /// <summary>
        /// Преобразование в целочисленный вектор
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Целочисленный вектор</returns>
        [Obsolete("Усечение через (int) не совпадает со штатным API. Используйте Vector3Int.FloorToInt / Vector3Int.RoundToInt / Vector3Int.CeilToInt")]
        public static Vector3Int ToInt(this Vector3 @this)
        {
            return new Vector3Int((int)@this.x, (int)@this.y, (int)@this.z);
        }


        /// <summary>
        /// Преобразование в вектор <see cref="Vector3Df"/>
        /// </summary>
        /// <param name="this">Исходный вектор</param>
        /// <returns>Вектор Vector3Df</returns>
        public static Vector3Df ToVector3Df(this Vector3 @this)
        {
            return new Vector3Df(@this.x, @this.y, @this.z);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Vector3Int"/>
    /// </summary>
    public static class XVector3IntExtension
    {
        #region Main methods
        /// <summary>
        /// Сериализация вектора в строку
        /// </summary>
        /// <param name="this">Трехмерный вектор</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Vector3Int @this)
        {
            return string.Format("{0};{1};{2}", @this.x, @this.y, @this.z);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Vector4"/>
    /// </summary>
    public static class XVector4Extension
    {
        #region Main methods
        /// <summary>
        /// Сериализация вектора в строку
        /// </summary>
        /// <param name="this">Четырехмерный вектор</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Vector4 @this)
        {
            return string.Format("{0};{1};{2};{3}", @this.x, @this.y, @this.z, @this.w);
        }


        /// <summary>
        /// Конвертация в тип <see cref="UnityEngine.RectOffset"/>
        /// </summary>
        /// <param name="this">Четырехмерный вектор</param>
        /// <returns>Объект RectOffset</returns>
        public static RectOffset ToRectOffset(this Vector4 @this)
        {
            return new RectOffset((int)@this.x, (int)@this.z, (int)@this.y, (int)@this.w);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Quaternion"/>
    /// </summary>
    public static class XQuaternionExtension
    {
        #region Main methods
        /// <summary>
        /// Сериализация кватерниона в строку
        /// </summary>
        /// <param name="this">Кватернион</param>
        /// <returns>Строка данных</returns>
        public static string SerializeToString(this Quaternion @this)
        {
            return string.Format("{0};{1};{2};{3}", @this.x, @this.y, @this.z, @this.w);
        }


        /// <summary>
        /// Преобразование в кватернион <see cref="Quaternion3Df"/>
        /// </summary>
        /// <param name="this">Исходный кватернион</param>
        /// <returns>Кватернион Quaternion3Df</returns>
        public static Quaternion3Df ToQuaternion3Df(this Quaternion @this)
        {
            return new Quaternion3Df(@this.x, @this.y, @this.z, @this.w);
        }
        #endregion
    }
    /**@}*/
}
#endif
