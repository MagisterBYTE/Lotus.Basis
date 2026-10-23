#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace Lotus.Core
{
    
    /** \addtogroup CoreUnityExtension
    *@{*/
    
    /// <summary>
    /// Статический класс реализующий методы расширения для сериализации базовых классов и структурных типов Unity в бинарный поток
    /// </summary>
    /// <remarks>
    /// Реализация методов расширений потоков чтения и записи бинарных данных для сериализации базовых классов 
    /// и структурных типов Unity
    /// </remarks>
    
    public static class XBinaryStreamUnityExtension
    {
        #region Write methods
        
        /// <summary>
        /// Запись данных двухмерного вектора
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Двухмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
        }

        
        /// <summary>
        /// Запись данных двухмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Двухмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, ref Vector2 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
        }

        
        /// <summary>
        /// Запись данных списка двухмерных векторов
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vectors">Список двухмерных векторов</param>
        
        public static void Write(this BinaryWriter writer, IList<Vector2> vectors)
        {
            // Проверка против нулевых значений
            if (vectors == null || vectors.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(vectors.Count);

                // Записываем данные по порядку
                for (var i = 0; i < vectors.Count; i++)
                {
                    writer.Write(vectors[i].x);
                    writer.Write(vectors[i].y);
                }
            }
        }

        
        /// <summary>
        /// Запись данных трехмерного вектора
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Трехмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
        }

        
        /// <summary>
        /// Запись данных трехмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Трехмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, ref Vector3 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
        }

        
        /// <summary>
        /// Запись данных списка трехмерных векторов
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vectors">Список трехмерных векторов</param>
        
        public static void Write(this BinaryWriter writer, IList<Vector3> vectors)
        {
            // Проверка против нулевых значений
            if (vectors == null || vectors.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(vectors.Count);

                // Записываем данные по порядку
                for (var i = 0; i < vectors.Count; i++)
                {
                    writer.Write(vectors[i].x);
                    writer.Write(vectors[i].y);
                    writer.Write(vectors[i].z);
                }
            }
        }

        
        /// <summary>
        /// Запись данных четырехмерного вектора
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Четырехмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, Vector4 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
            writer.Write(vector.w);
        }

        
        /// <summary>
        /// Запись данных четырехмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vector">Четырехмерный вектор</param>
        
        public static void Write(this BinaryWriter writer, ref Vector4 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
            writer.Write(vector.w);
        }

        
        /// <summary>
        /// Запись данных списка четырехмерных векторов
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="vectors">Список четырехмерных векторов</param>
        
        public static void Write(this BinaryWriter writer, IList<Vector4> vectors)
        {
            // Проверка против нулевых значений
            if (vectors == null || vectors.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(vectors.Count);

                // Записываем данные по порядку
                for (var i = 0; i < vectors.Count; i++)
                {
                    writer.Write(vectors[i].x);
                    writer.Write(vectors[i].y);
                    writer.Write(vectors[i].z);
                    writer.Write(vectors[i].w);
                }
            }
        }

        
        /// <summary>
        /// Запись данных кватерниона
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="quaternion">Кватернион</param>
        
        public static void Write(this BinaryWriter writer, Quaternion quaternion)
        {
            writer.Write(quaternion.x);
            writer.Write(quaternion.y);
            writer.Write(quaternion.z);
            writer.Write(quaternion.w);
        }

        
        /// <summary>
        /// Запись данных кватерниона, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="quaternion">Кватернион</param>
        
        public static void Write(this BinaryWriter writer, ref Quaternion quaternion)
        {
            writer.Write(quaternion.x);
            writer.Write(quaternion.y);
            writer.Write(quaternion.z);
            writer.Write(quaternion.w);
        }

        
        /// <summary>
        /// Запись данных списка кватернионов
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="quaternions">Список кватернионов</param>
        
        public static void Write(this BinaryWriter writer, IList<Quaternion> quaternions)
        {
            // Проверка против нулевых значений
            if (quaternions == null || quaternions.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(quaternions.Count);

                // Записываем данные по порядку
                for (var i = 0; i < quaternions.Count; i++)
                {
                    writer.Write(quaternions[i].x);
                    writer.Write(quaternions[i].y);
                    writer.Write(quaternions[i].z);
                    writer.Write(quaternions[i].w);
                }
            }
        }

        
        /// <summary>
        /// Запись данных четырехмерной матрицы
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="matrix">Четырехмерная матрица</param>
        
        public static void Write(this BinaryWriter writer, Matrix4x4 matrix)
        {
            for (var i = 0; i < 16; i++)
            {
                writer.Write(matrix[i]);
            }
        }

        
        /// <summary>
        /// Запись данных четырехмерной матрицы, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="matrix">Четырехмерная матрица</param>
        
        public static void Write(this BinaryWriter writer, ref Matrix4x4 matrix)
        {
            for (var i = 0; i < 16; i++)
            {
                writer.Write(matrix[i]);
            }
        }

        
        /// <summary>
        /// Запись данных списка четырехмерных матриц
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="matrices">Список четырехмерных матриц</param>
        
        public static void Write(this BinaryWriter writer, IList<Matrix4x4> matrices)
        {
            // Проверка против нулевых значений
            if (matrices == null || matrices.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(matrices.Count);

                // Записываем данные по порядку
                for (var i = 0; i < matrices.Count; i++)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        writer.Write(matrices[i][j]);
                    }
                }
            }
        }

        
        /// <summary>
        /// Запись данных плоскости
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="plane">Плоскость</param>
        
        public static void Write(this BinaryWriter writer, Plane plane)
        {
            writer.Write(plane.normal.x);
            writer.Write(plane.normal.y);
            writer.Write(plane.normal.z);
            writer.Write(plane.distance);
        }

        
        /// <summary>
        /// Запись данных плоскости, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="plane">Плоскость</param>
        
        public static void Write(this BinaryWriter writer, ref Plane plane)
        {
            writer.Write(plane.normal.x);
            writer.Write(plane.normal.y);
            writer.Write(plane.normal.z);
            writer.Write(plane.distance);
        }

        
        /// <summary>
        /// Запись данных списка плоскостей
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="planes">Список плоскостей</param>
        
        public static void Write(this BinaryWriter writer, IList<Plane> planes)
        {
            // Проверка против нулевых значений
            if (planes == null || planes.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(planes.Count);

                // Записываем данные по порядку
                for (var i = 0; i < planes.Count; i++)
                {
                    writer.Write(planes[i].normal.x);
                    writer.Write(planes[i].normal.y);
                    writer.Write(planes[i].normal.z);
                    writer.Write(planes[i].distance);
                }
            }
        }

        
        /// <summary>
        /// Запись данных трехмерного луча
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="ray">Трехмерный луч</param>
        
        public static void Write(this BinaryWriter writer, Ray ray)
        {
            writer.Write(ray.origin.x);
            writer.Write(ray.origin.y);
            writer.Write(ray.origin.z);
            writer.Write(ray.direction.x);
            writer.Write(ray.direction.y);
            writer.Write(ray.direction.z);
        }

        
        /// <summary>
        /// Запись данных трехмерного луча, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="ray">Трехмерный луч</param>
        
        public static void Write(this BinaryWriter writer, ref Ray ray)
        {
            writer.Write(ray.origin.x);
            writer.Write(ray.origin.y);
            writer.Write(ray.origin.z);
            writer.Write(ray.direction.x);
            writer.Write(ray.direction.y);
            writer.Write(ray.direction.z);
        }

        
        /// <summary>
        /// Запись данных списка трехмерных лучей
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="rays">Список трехмерных лучей</param>
        
        public static void Write(this BinaryWriter writer, IList<Ray> rays)
        {
            // Проверка против нулевых значений
            if (rays == null || rays.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(rays.Count);

                // Записываем данные по порядку
                for (var i = 0; i < rays.Count; i++)
                {
                    writer.Write(rays[i].origin.x);
                    writer.Write(rays[i].origin.y);
                    writer.Write(rays[i].origin.z);
                    writer.Write(rays[i].direction.x);
                    writer.Write(rays[i].direction.y);
                    writer.Write(rays[i].direction.z);
                }
            }
        }

        
        /// <summary>
        /// Запись данных ограничивающего объема
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="bounds"></param>
        
        public static void Write(this BinaryWriter writer, Bounds bounds)
        {
            writer.Write(bounds.center.x);
            writer.Write(bounds.center.y);
            writer.Write(bounds.center.z);
            writer.Write(bounds.size.x);
            writer.Write(bounds.size.y);
            writer.Write(bounds.size.z);
        }

        
        /// <summary>
        /// Запись данных ограничивающего объема, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="bounds">Ограничивающий объем</param>
        
        public static void Write(this BinaryWriter writer, ref Bounds bounds)
        {
            writer.Write(bounds.center.x);
            writer.Write(bounds.center.y);
            writer.Write(bounds.center.z);
            writer.Write(bounds.size.x);
            writer.Write(bounds.size.y);
            writer.Write(bounds.size.z);
        }

        
        /// <summary>
        /// Запись данных списка ограничивающих объемов
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="bounds">Список ограничивающих объемов</param>
        
        public static void Write(this BinaryWriter writer, IList<Bounds> bounds)
        {
            // Проверка против нулевых значений
            if (bounds == null || bounds.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(bounds.Count);

                // Записываем данные по порядку
                for (var i = 0; i < bounds.Count; i++)
                {
                    writer.Write(bounds[i].center.x);
                    writer.Write(bounds[i].center.y);
                    writer.Write(bounds[i].center.z);
                    writer.Write(bounds[i].size.x);
                    writer.Write(bounds[i].size.y);
                    writer.Write(bounds[i].size.z);
                }
            }
        }

        
        /// <summary>
        /// Запись данных цветового значения
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="color">Цветовое значение</param>
        
        public static void Write(this BinaryWriter writer, Color color)
        {
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
            writer.Write(color.a);
        }

        
        /// <summary>
        /// Запись данных цветового значения, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="color">Цветовое значение</param>
        
        public static void Write(this BinaryWriter writer, ref Color color)
        {
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
            writer.Write(color.a);
        }

        
        /// <summary>
        /// Запись данных списка цветовых значений
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="colors">Список цветовых значений</param>
        
        public static void Write(this BinaryWriter writer, IList<Color> colors)
        {
            // Проверка против нулевых значений
            if (colors == null || colors.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(colors.Count);

                // Записываем данные по порядку
                for (var i = 0; i < colors.Count; i++)
                {
                    writer.Write(colors[i].r);
                    writer.Write(colors[i].g);
                    writer.Write(colors[i].b);
                    writer.Write(colors[i].a);
                }
            }
        }

        
        /// <summary>
        /// Запись данных 32-битного цветового значения
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="color">32-битное цветовое значение</param>
        
        public static void Write(this BinaryWriter writer, Color32 color)
        {
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
            writer.Write(color.a);
        }

        
        /// <summary>
        /// Запись данных 32-битного цветового значения, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="color">32-битное цветовое значение</param>
        
        public static void Write(this BinaryWriter writer, ref Color32 color)
        {
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
            writer.Write(color.a);
        }

        
        /// <summary>
        /// Запись данных списка 32-битных цветовых значений
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="colors">Список 32-битных цветовых значений</param>
        
        public static void Write(this BinaryWriter writer, IList<Color32> colors)
        {
            // Проверка против нулевых значений
            if (colors == null || colors.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(colors.Count);

                // Записываем данные по порядку
                for (var i = 0; i < colors.Count; i++)
                {
                    writer.Write(colors[i].r);
                    writer.Write(colors[i].g);
                    writer.Write(colors[i].b);
                    writer.Write(colors[i].a);
                }
            }
        }

        
        /// <summary>
        /// Запись данных прямоугольника
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="rect">Прямоугольник</param>
        
        public static void Write(this BinaryWriter writer, Rect rect)
        {
            writer.Write(rect.x);
            writer.Write(rect.y);
            writer.Write(rect.width);
            writer.Write(rect.height);
        }

        
        /// <summary>
        /// Запись данных прямоугольника, оптимизированная версия
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="rect">Прямоугольник</param>
        
        public static void Write(this BinaryWriter writer, ref Rect rect)
        {
            writer.Write(rect.x);
            writer.Write(rect.y);
            writer.Write(rect.width);
            writer.Write(rect.height);
        }

        
        /// <summary>
        /// Запись данных списка прямоугольников
        /// </summary>
        /// <param name="writer">Бинарный поток открытый для записи</param>
        /// <param name="rects">Список прямоугольников</param>
        
        public static void Write(this BinaryWriter writer, IList<Rect> rects)
        {
            // Проверка против нулевых значений
            if (rects == null || rects.Count == 0)
            {
                writer.Write(XBinaryStreamExtension.ZERO_DATA);
            }
            else
            {
                // Записываем длину
                writer.Write(rects.Count);

                // Записываем данные по порядку
                for (var i = 0; i < rects.Count; i++)
                {
                    writer.Write(rects[i].x);
                    writer.Write(rects[i].y);
                    writer.Write(rects[i].width);
                    writer.Write(rects[i].height);
                }
            }
        }
        #endregion

        #region Read methods
        
        /// <summary>
        /// Чтение данных двухмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="vector">Двухмерный вектор</param>
        
        public static void Read(this BinaryReader reader, ref Vector2 vector)
        {
            vector.x = reader.ReadSingle();
            vector.y = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных двухмерного вектора.
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Двухмерный вектор</returns>
        
        public static Vector2 ReadUnityVector2D(this BinaryReader reader)
        {
            var vector = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            return vector;
        }

        
        /// <summary>
        /// Чтение данных массива двухмерных векторов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив двухмерных векторов</returns>
        
        public static Vector2[] ReadUnityVectors2D(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var vectors = new Vector2[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    vectors[i].x = reader.ReadSingle();
                    vectors[i].y = reader.ReadSingle();
                }

                return vectors;
            }
        }

        
        /// <summary>
        /// Чтение данных трехмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="vector">Трехмерный вектор</param>
        
        public static void Read(this BinaryReader reader, ref Vector3 vector)
        {
            vector.x = reader.ReadSingle();
            vector.y = reader.ReadSingle();
            vector.z = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных трехмерного вектора
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Трехмерный вектор</returns>
        
        public static Vector3 ReadUnityVector3D(this BinaryReader reader)
        {
            var vector = new Vector3(reader.ReadSingle(),
                                         reader.ReadSingle(),
                                         reader.ReadSingle());
            return vector;
        }

        
        /// <summary>
        /// Чтение данных массива трехмерных векторов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив трехмерных векторов</returns>
        
        public static Vector3[] ReadUnityVectors3D(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var vectors = new Vector3[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    vectors[i].x = reader.ReadSingle();
                    vectors[i].y = reader.ReadSingle();
                    vectors[i].z = reader.ReadSingle();
                }

                return vectors;
            }
        }

        
        /// <summary>
        /// Чтение данных четырехмерного вектора, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="vector">Четырехмерный вектор</param>
        
        public static void Read(this BinaryReader reader, ref Vector4 vector)
        {
            vector.x = reader.ReadSingle();
            vector.y = reader.ReadSingle();
            vector.z = reader.ReadSingle();
            vector.w = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных четырехмерного вектора
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Четырехмерный вектор</returns>
        
        public static Vector4 ReadUnityVector4D(this BinaryReader reader)
        {
            var vector = new Vector4(reader.ReadSingle(),
                                         reader.ReadSingle(),
                                         reader.ReadSingle(),
                                         reader.ReadSingle());
            return vector;
        }

        
        /// <summary>
        /// Чтение данных массива четырехмерных векторов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив четырехмерных векторов</returns>
        
        public static Vector4[] ReadUnityVectors4D(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var vectors = new Vector4[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    vectors[i].x = reader.ReadSingle();
                    vectors[i].y = reader.ReadSingle();
                    vectors[i].z = reader.ReadSingle();
                    vectors[i].w = reader.ReadSingle();
                }

                return vectors;
            }
        }

        
        /// <summary>
        /// Чтение данных кватерниона, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="quaternion">Кватернион</param>
        
        public static void Read(this BinaryReader reader, ref Quaternion quaternion)
        {
            quaternion.x = reader.ReadSingle();
            quaternion.y = reader.ReadSingle();
            quaternion.z = reader.ReadSingle();
            quaternion.w = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных кватерниона
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Кватернион</returns>
        
        public static Quaternion ReadUnityQuaternion(this BinaryReader reader)
        {
            var quaternion = new Quaternion(reader.ReadSingle(),
                                                   reader.ReadSingle(),
                                                   reader.ReadSingle(),
                                                   reader.ReadSingle());
            return quaternion;
        }

        
        /// <summary>
        /// Чтение данных массива кватернионов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив кватернионов</returns>
        
        public static Quaternion[] ReadUnityQuaternions(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var quaternions = new Quaternion[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    quaternions[i].x = reader.ReadSingle();
                    quaternions[i].y = reader.ReadSingle();
                    quaternions[i].z = reader.ReadSingle();
                    quaternions[i].w = reader.ReadSingle();
                }

                return quaternions;
            }
        }

        
        /// <summary>
        /// Чтение данных четырехмерной матрицы, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="matrix">Четырехмерная матрица</param>
        
        public static void Read(this BinaryReader reader, ref Matrix4x4 matrix)
        {
            for (var i = 0; i < 16; i++)
            {
                matrix[i] = reader.ReadSingle();
            }
        }

        
        /// <summary>
        /// Чтение данных четырехмерной матрицы
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Четырехмерная матрица</returns>
        
        public static Matrix4x4 ReadUnityMatrix4D(this BinaryReader reader)
        {
            var matrix = new Matrix4x4();
            for (var i = 0; i < 16; i++)
            {
                matrix[i] = reader.ReadSingle();
            }
            return matrix;
        }

        
        /// <summary>
        /// Чтение данных массива четырехмерных матриц
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив четырехмерных матриц</returns>
        
        public static Matrix4x4[] ReadUnityMatrices4D(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var matrices = new Matrix4x4[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        matrices[i][j] = reader.ReadSingle();
                    }
                }

                return matrices;
            }
        }


        /// <summary>
        /// Чтение данных массива четырехмерных матриц
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив четырехмерных матриц</returns>
        [Obsolete("Опечатка в имени. Используйте ReadUnityMatrices4D")]
        public static Matrix4x4[] ReadUnityMatrixes4D(this BinaryReader reader)
        {
            return reader.ReadUnityMatrices4D();
        }

        
        /// <summary>
        /// Чтение данных плоскости, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="plane">Плоскость</param>
        
        public static void Read(this BinaryReader reader, ref Plane plane)
        {
            plane.normal = reader.ReadUnityVector3D();
            plane.distance = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных плоскости
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Плоскость</returns>
        
        public static Plane ReadUnityPlane(this BinaryReader reader)
        {
            var plane = new Plane(reader.ReadUnityVector3D(), reader.ReadSingle());
            return plane;
        }

        
        /// <summary>
        /// Чтение данных массива плоскостей
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив плоскостей</returns>
        
        public static Plane[] ReadUnityPlanes(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var planes = new Plane[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    planes[i].normal = reader.ReadUnityVector3D();
                    planes[i].distance = reader.ReadSingle();
                }

                return planes;
            }
        }

        
        /// <summary>
        /// Чтение данных трехмерного луча, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="ray">Трехмерный луч</param>
        
        public static void Read(this BinaryReader reader, ref Ray ray)
        {
            ray.origin = reader.ReadUnityVector3D();
            ray.direction = reader.ReadUnityVector3D();
        }

        
        /// <summary>
        /// Чтение данных трехмерного луча
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Трехмерный луч</returns>
        
        public static Ray ReadUnityRay3D(this BinaryReader reader)
        {
            var ray = new Ray(reader.ReadUnityVector3D(), reader.ReadUnityVector3D());
            return ray;
        }

        
        /// <summary>
        /// Чтение данных массива трехмерных лучей
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив трехмерных лучей</returns>
        
        public static Ray[] ReadUnityRays3D(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var rays = new Ray[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    rays[i].origin = reader.ReadUnityVector3D();
                    rays[i].direction = reader.ReadUnityVector3D();
                }

                return rays;
            }
        }

        
        /// <summary>
        /// Чтение данных ограничивающего объема, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="bounds">Ограничивающий объем</param>
        
        public static void Read(this BinaryReader reader, ref Bounds bounds)
        {
            bounds.center = reader.ReadUnityVector3D();
            bounds.size = reader.ReadUnityVector3D();
        }

        
        /// <summary>
        /// Чтение данных ограничивающего объема
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Ограничивающий объем</returns>
        
        public static Bounds ReadUnityBounds(this BinaryReader reader)
        {
            var bounds = new Bounds(reader.ReadUnityVector3D(), reader.ReadUnityVector3D());
            return bounds;
        }

        
        /// <summary>
        /// Чтение данных массива ограничивающих объемов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив ограничивающих объемов</returns>
        
        public static Bounds[] ReadUnityBoundsArray(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var items = new Bounds[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    items[i].center = reader.ReadUnityVector3D();
                    items[i].size = reader.ReadUnityVector3D();
                }

                return items;
            }
        }


        /// <summary>
        /// Чтение данных массива ограничивающих объемов
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив ограничивающих объемов</returns>
        [Obsolete("Некорректное имя. Используйте ReadUnityBoundsArray")]
        public static Bounds[] ReadUnityBoundses(this BinaryReader reader)
        {
            return reader.ReadUnityBoundsArray();
        }

        
        /// <summary>
        /// Чтение данных цветового значения, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="color">Цветовое значение</param>
        
        public static void Read(this BinaryReader reader, ref Color color)
        {
            color.r = reader.ReadSingle();
            color.g = reader.ReadSingle();
            color.b = reader.ReadSingle();
            color.a = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных цветового значения
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color ReadUnityColor(this BinaryReader reader)
        {
            var color = new Color(reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle(),
                                    reader.ReadSingle());
            return color;
        }

        
        /// <summary>
        /// Чтение данных массива цветовых значений
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив цветовых значений</returns>
        
        public static Color[] ReadUnityColors(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var сolors = new Color[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    сolors[i].r = reader.ReadSingle();
                    сolors[i].g = reader.ReadSingle();
                    сolors[i].b = reader.ReadSingle();
                    сolors[i].a = reader.ReadSingle();
                }

                return сolors;
            }
        }

        
        /// <summary>
        /// Чтение данных 32-битного цветового значения, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="color">32-битное цветовое значение</param>
        
        public static void Read(this BinaryReader reader, ref Color32 color)
        {
            color.r = reader.ReadByte();
            color.g = reader.ReadByte();
            color.b = reader.ReadByte();
            color.a = reader.ReadByte();
        }

        
        /// <summary>
        /// Чтение данных 32-битного цветового значения
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>32-битное цветовое значение</returns>
        
        public static Color32 ReadUnityColor32(this BinaryReader reader)
        {
            var color = new Color32(reader.ReadByte(),
                                        reader.ReadByte(),
                                        reader.ReadByte(),
                                        reader.ReadByte());
            return color;
        }

        
        /// <summary>
        /// Чтение данных массива 32-битных цветовых значений
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив 32-битных цветовых значений</returns>
        
        public static Color32[] ReadUnityColor32s(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var сolors = new Color32[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    сolors[i].r = reader.ReadByte();
                    сolors[i].g = reader.ReadByte();
                    сolors[i].b = reader.ReadByte();
                    сolors[i].a = reader.ReadByte();
                }

                return сolors;
            }
        }

        
        /// <summary>
        /// Чтение данных прямоугольника, оптимизированная версия
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <param name="rect">Прямоугольник</param>
        
        public static void Read(this BinaryReader reader, ref Rect rect)
        {
            rect.x = reader.ReadSingle();
            rect.y = reader.ReadSingle();
            rect.width = reader.ReadSingle();
            rect.height = reader.ReadSingle();
        }

        
        /// <summary>
        /// Чтение данных прямоугольника
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Прямоугольник</returns>
        
        public static Rect ReadUnityRect(this BinaryReader reader)
        {
            var rect = new Rect(reader.ReadSingle(),
                                 reader.ReadSingle(),
                                 reader.ReadSingle(),
                                 reader.ReadSingle());
            return rect;
        }

        
        /// <summary>
        /// Чтение данных массива прямоугольников
        /// </summary>
        /// <param name="reader">Бинарный поток открытый для чтения</param>
        /// <returns>Массив прямоугольников</returns>
        
        public static Rect[] ReadUnityRects(this BinaryReader reader)
        {
            // Чтение количество элементов массива
            var count = reader.ReadInt32();

            // Проверка нулевых данных
            if (count == XBinaryStreamExtension.ZERO_DATA)
            {
                return null;
            }
            else
            {
                // Создаем массив
                var rects = new Rect[count];

                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    rects[i].x = reader.ReadSingle();
                    rects[i].y = reader.ReadSingle();
                    rects[i].width = reader.ReadSingle();
                    rects[i].height = reader.ReadSingle();
                }

                return rects;
            }
        }
        #endregion
    }
    
    /**@}*/
}
#endif
