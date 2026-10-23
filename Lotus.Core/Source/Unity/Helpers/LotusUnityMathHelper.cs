#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityHelpers
    *@{*/
    /// <summary>
    /// Статический класс для расширения функциональности типа Vector2
    /// </summary>
    public static class XUnityVector2
    {
        #region Main methods
        /// <summary>
        /// Десереализация двухмерного вектора из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Двухмерный вектор</returns>
        public static Vector2 DeserializeFromString(string data)
        {
            Vector2 vector;
            var vectorData = data.Split(';');
            vector.x = XNumberConverter.ParseSingle(vectorData[0]);
            vector.y = XNumberConverter.ParseSingle(vectorData[1]);
            return vector;
        }

        /// <summary>
        /// Преобразование в объект вектора из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Вектор</returns>
        public static Vector2 ToVector(object value)
        {
            if (value is Vector2)
            {
                return ((Vector2)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Vector2Int vector)
                    {
                        return (new Vector2(vector.x, vector.y));
                    }
                }
            }

            return (Vector2.zero);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа Vector2Int
    /// </summary>
    public static class XUnityVector2Int
    {
        #region Main methods
        /// <summary>
        /// Десереализация двухмерного вектора из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Двухмерный вектор</returns>
        public static Vector2Int DeserializeFromString(string data)
        {
            Vector2Int vector = Vector2Int.zero;
            var vectorData = data.Split(';');
            vector.x = XNumberConverter.ParseInt(vectorData[0]);
            vector.y = XNumberConverter.ParseInt(vectorData[1]);
            return vector;
        }

        /// <summary>
        /// Преобразование в объект вектора из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Вектор</returns>
        public static Vector2Int ToVector(object value)
        {
            if (value is Vector2Int)
            {
                return ((Vector2Int)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Vector2 vector)
                    {
                        return (vector.ToInt());
                    }
                }
            }

            return (Vector2Int.zero);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа Vector3
    /// </summary>
    public static class XUnityVector3
    {
        #region Main methods
        /// <summary>
        /// Десереализация трехмерного вектора из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Трехмерный вектор</returns>
        public static Vector3 DeserializeFromString(string data)
        {
            Vector3 vector;
            var vectorData = data.Split(';');
            vector.x = XNumberConverter.ParseSingle(vectorData[0]);
            vector.y = XNumberConverter.ParseSingle(vectorData[1]);
            vector.z = XNumberConverter.ParseSingle(vectorData[2]);
            return vector;
        }

        /// <summary>
        /// Преобразование в объект вектора из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Вектор</returns>
        public static Vector3 ToVector(object value)
        {
            if (value is Vector3)
            {
                return ((Vector3)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Vector3Int vector)
                    {
                        return (new Vector3(vector.x, vector.y, vector.z));
                    }
                }
            }

            return (Vector3.zero);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа Vector3Int
    /// </summary>
    public static class XUnityVector3Int
    {
        #region Main methods
        /// <summary>
        /// Десереализация трехмерного вектора из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Трехмерный вектор</returns>
        public static Vector3Int DeserializeFromString(string data)
        {
            Vector3Int vector = Vector3Int.zero;
            var vectorData = data.Split(';');
            vector.x = XNumberConverter.ParseInt(vectorData[0]);
            vector.y = XNumberConverter.ParseInt(vectorData[1]);
            vector.z = XNumberConverter.ParseInt(vectorData[2]);
            return vector;
        }

        /// <summary>
        /// Преобразование в объект вектора из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Вектор</returns>
        public static Vector3Int ToVector(object value)
        {
            if (value is Vector3Int)
            {
                return ((Vector3Int)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Vector3 vector)
                    {
                        return (vector.ToInt());
                    }
                }
            }

            return (Vector3Int.zero);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа Vector4
    /// </summary>
    public static class XUnityVector4
    {
        #region Main methods
        /// <summary>
        /// Десереализация четырехмерного вектора из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Четырехмерный вектор</returns>
        public static Vector4 DeserializeFromString(string data)
        {
            Vector4 vector;
            var vectorData = data.Split(';');
            vector.x = XNumberConverter.ParseSingle(vectorData[0]);
            vector.y = XNumberConverter.ParseSingle(vectorData[1]);
            vector.z = XNumberConverter.ParseSingle(vectorData[2]);
            vector.w = XNumberConverter.ParseSingle(vectorData[3]);
            return vector;
        }

        /// <summary>
        /// Преобразование в объект вектора из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Вектор</returns>
        public static Vector4 ToVector(object value)
        {
            if (value is Vector4)
            {
                return ((Vector4)value);
            }
            else
            {
                if (value is string v)
                {
                    return (DeserializeFromString(v));
                }
                else
                {
                    if (value is Vector3 vector)
                    {
                        return (new Vector4(vector.x, vector.y, vector.z));
                    }
                }
            }

            return (Vector4.zero);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа Quaternion
    /// </summary>
    public static class XUnityQuaternion
    {
        #region Main methods
        /// <summary>
        /// Десереализация кватерниона из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Кватернион</returns>
        public static Quaternion DeserializeFromString(string data)
        {
            Quaternion quaternion;
            var vectorData = data.Split(';');
            quaternion.x = XNumberConverter.ParseSingle(vectorData[0]);
            quaternion.y = XNumberConverter.ParseSingle(vectorData[1]);
            quaternion.z = XNumberConverter.ParseSingle(vectorData[2]);
            quaternion.w = XNumberConverter.ParseSingle(vectorData[3]);
            return quaternion;
        }

        /// <summary>
        /// Преобразование в объект кватерниона из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Кватернион</returns>
        public static Quaternion ToQuaternion(object value)
        {
            if (value is Quaternion)
            {
                return ((Quaternion)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Vector4 vector)
                    {
                        return (new Quaternion(vector.x, vector.y, vector.z, vector.w));
                    }
                }
            }

            return (Quaternion.identity);
        }
        #endregion
    }

    /**@}*/
}
#endif
