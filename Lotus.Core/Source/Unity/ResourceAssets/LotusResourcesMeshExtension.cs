#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений ресурса Mesh
    /// </summary>
    public static class XMeshExtension
    {
        #region Main methods
        /// <summary>
        /// Смещение вершин меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="offset">Вектор смещения</param>
        public static void Move(this Mesh @this, Vector3 offset)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var vertices = @this.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] += offset;
            }

            @this.vertices = vertices;
        }

        /// <summary>
        /// Врашение вершин меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="rotation">Кватернион вращения</param>
        public static void Rotate(this Mesh @this, Quaternion rotation)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var vertices = @this.vertices;
            var normals = @this.normals;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = rotation * vertices[i];
                normals[i] = rotation * normals[i];
            }

            @this.vertices = vertices;
            @this.normals = normals;
        }

        /// <summary>
        /// Масштабирование вершин меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="scale">Масштаб</param>
        public static void Scale(this Mesh @this, float scale)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var vertices = @this.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= scale;
            }

            @this.vertices = vertices;
        }

        /// <summary>
        /// Масштабирование вершин меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="scale">Масштаб</param>
        public static void Scale(this Mesh @this, Vector3 scale)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var vertices = @this.vertices;
            var normals = @this.normals;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector3.Scale(vertices[i], scale);
                normals[i] = Vector3.Scale(normals[i], scale).normalized;
            }

            @this.vertices = vertices;
            @this.normals = normals;
        }

        /// <summary>
        /// Раскраска меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="color">Цвет</param>
        public static void Paint(this Mesh @this, Color color)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var colors = new Color[@this.vertexCount];
            for (var i = 0; i < @this.vertexCount; i++)
            {
                colors[i] = color;
            }

            @this.colors = colors;
        }

        /// <summary>
        /// Разворачивание грани меша в обратную сторону (относительно текущей нормали)
        /// </summary>
        /// <param name="this">Меш</param>
        public static void FlipFaces(this Mesh @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            @this.FlipTriangles();
            @this.FlipNormals();
        }

        /// <summary>
        /// Обратить порядок вершин
        /// </summary>
        /// <param name="this">Меш</param>
        public static void FlipTriangles(this Mesh @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            for (var i = 0; i < @this.subMeshCount; i++)
            {
                var triangles = @this.GetTriangles(i);
                for (var j = 0; j < triangles.Length; j += 3)
                {
                    triangles.Swap(j, j + 1);
                }

                @this.SetTriangles(triangles, i);
            }
        }

        /// <summary>
        /// Обратить нормали
        /// </summary>
        /// <param name="this">Меш</param>
        public static void FlipNormals(this Mesh @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var normals = @this.normals;
            for (var i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }

            @this.normals = normals;
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по горизонтали
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="channel">Канал</param>
        public static void FlipUVHorizontally(this Mesh @this, int channel = 0)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var list = new List<Vector2>();
            @this.GetUVs(channel, list);
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = new Vector2(1 - list[i].x, list[i].y);
            }

            @this.SetUVs(channel, list);
        }

        /// <summary>
        /// Обратить развёртку текстурных координат по вертикали
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="channel">Канал</param>
        public static void FlipUVVertically(this Mesh @this, int channel = 0)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var list = new List<Vector2>();
            @this.GetUVs(channel, list);
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = new Vector2(list[i].x, 1 - list[i].y);
            }

            @this.SetUVs(channel, list);
        }

        /// <summary>
        /// Вздутие меша из центра
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="radius">Радиус сферы</param>
        /// <param name="center">Центр сферы</param>
        public static void Spherify(this Mesh @this, float radius, Vector3 center = default)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var vertices = @this.vertices;
            var normals = @this.normals;
            for (var i = 0; i < vertices.Length; i++)
            {
                normals[i] = (vertices[i] - center).normalized;
                vertices[i] = normals[i] * radius;
            }

            @this.vertices = vertices;
            @this.normals = normals;
        }

        /// <summary>
        /// Получение координат центра(опорной точки) меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <returns>Координаты центра(опорной точки) меша</returns>
        public static Vector3 GetPivot(this Mesh @this)
        {
            if (@this == null)
            {
                return Vector3.zero;
            }

            @this.RecalculateBounds();
            return @this.bounds.center;
        }

        /// <summary>
        /// Смещение координат центра(опорной точки) меша
        /// </summary>
        /// <param name="this">Меш</param>
        /// <param name="offset">Вектор смещения</param>
        public static void OffsetPivot(this Mesh @this, Vector3 offset)
        {
            if (@this == null)
            {
                return;
            }

            var vertices = @this.vertices;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= offset;
            }

            @this.vertices = vertices;
        }

        /// <summary>
        /// Вычисление тангентов меша
        /// </summary>
        /// <remarks>
        /// В Unity 6 делегирует встроенному <see cref="Mesh.RecalculateTangents"/>.
        /// </remarks>
        /// <param name="this">Меш</param>
        public static void CalculateMeshTangents(this Mesh @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            @this.RecalculateTangents();
        }
        #endregion
    }

    /**@}*/
}
#endif
