using System;
using System.Collections.Generic;

using Lotus.Core;
using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshCommon
	*@{*/
    /// <summary>
    /// Треугольник трёхмерной полигональной сетки(меша).
    /// </summary>
    /// <remarks>
    /// Треугольник - это базовая грань меша которая содержит замкнутое множество трех рёбер.
    /// </remarks>
    [Serializable]
    public struct Triangle3Df : IComparable<Triangle3Df>, IEquatable<Triangle3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров треугольника.
        /// </summary>
        public static string ToStringFormat = "{0}, {1}, {2}";
        #endregion

        #region Fields
        /// <summary>
        /// Индекс первой вершины в списке вершин.
        /// </summary>
        public int IndexVertex0;

        /// <summary>
        /// Индекс второй вершины в списке вершин.
        /// </summary>
        public int IndexVertex1;

        /// <summary>
        /// Индекс третьей вершины в списке вершин.
        /// </summary>
        public int IndexVertex2;
        #endregion

        #region Properties
        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public readonly TMeshElement MeshElement { get { return TMeshElement.Triangle; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="iv1">Индекс первой вершины.</param>
        /// <param name="iv2">Индекс второй вершины.</param>
        /// <param name="iv3">Индекс третьей вершины.</param>
        public Triangle3Df(int iv1, int iv2, int iv3)
        {
            IndexVertex0 = iv1;
            IndexVertex1 = iv2;
            IndexVertex2 = iv3;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is Triangle3Df triangle)
            {
                return Equals(triangle);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства треугольников.
        /// </summary>
        /// <remarks>
        /// Треугольники равны года ссылаются на одни и те же вершины.
        /// </remarks>
        /// <param name="other">Сравниваемый треугольник.</param>
        /// <returns>Статус равенства треугольников.</returns>
        public readonly bool Equals(Triangle3Df other)
        {
            return IndexVertex0 == other.IndexVertex0 &&
                IndexVertex1 == other.IndexVertex1 &&
                IndexVertex2 == other.IndexVertex2;
        }

        /// <summary>
        /// Сравнение треугольников для упорядочивания.
        /// </summary>
        /// <param name="x">Первый треугольник.</param>
        /// <param name="y">Второй треугольник.</param>
        /// <returns>Статус сравнения треугольников.</returns>
        public readonly int Compare(Triangle3Df x, Triangle3Df y)
        {
            return x.CompareTo(y);
        }

        /// <summary>
        /// Сравнение треугольников для упорядочивания.
        /// </summary>
        /// <param name="other">Треугольник.</param>
        /// <returns>Статус сравнения треугольников.</returns>
        public readonly int CompareTo(Triangle3Df other)
        {
            if (IndexVertex0 > other.IndexVertex0)
            {
                return 1;
            }
            else
            {
                if (IndexVertex0 < other.IndexVertex0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Получение хеш-кода треугольника.
        /// </summary>
        /// <returns>Хеш-код треугольника.</returns>
        public override readonly int GetHashCode()
        {
            return IndexVertex0.GetHashCode() ^ IndexVertex1.GetHashCode() ^ IndexVertex2.GetHashCode();
        }

        /// <summary>
        /// Получение хеш-кода указанного треугольника.
        /// </summary>
        /// <param name="obj">Треугольник.</param>
        /// <returns>Хеш-код треугольника.</returns>
        public readonly int GetHashCode(Triangle3Df obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Индексы вершин треугольника.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, IndexVertex0, IndexVertex1, IndexVertex2);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Смещение индексов вершин.
        /// </summary>
        /// <remarks>
        /// Применяется при соединении/разъединение мешей.
        /// </remarks>
        /// <param name="countVertex">Количество вершин на величину которых смещаются индексы.</param>
        public void Offset(int countVertex)
        {
            IndexVertex0 += countVertex;
            IndexVertex1 += countVertex;
            IndexVertex2 += countVertex;
        }

        /// <summary>
        /// Получение треугольника со смещенными индексами вершин.
        /// </summary>
        /// <remarks>
        /// Применяется при соединении/разъединение мешей.
        /// </remarks>
        /// <param name="countVertex">Количество вершин на величину которых смещаются индексы.</param>
        /// <returns>Треугольник трехмерной сетки.</returns>
        public readonly Triangle3Df GetTriangleOffset(int countVertex)
        {
            return new Triangle3Df(IndexVertex0 + countVertex, IndexVertex1 + countVertex, IndexVertex2 + countVertex);
        }

        /// <summary>
        /// Получение ребра(стороны) треугольника.
        /// </summary>
        /// <param name="indexEdge">Индекс ребра(стороны) треугольника.</param>
        /// <param name="indexTriangle">Индекс данного треугольника.</param>
        /// <returns>Ребро(сторона) треугольника.</returns>
        public readonly Edge3Df GetEdge(int indexEdge, int indexTriangle)
        {
            switch (indexEdge)
            {
                case 0:
                    {
                        return new Edge3Df(IndexVertex0, IndexVertex1, indexTriangle);
                    }
                case 1:
                    {
                        return new Edge3Df(IndexVertex1, IndexVertex2, indexTriangle);
                    }
                default:
                    {
                        return new Edge3Df(IndexVertex2, IndexVertex0, indexTriangle);
                    }
            }
        }

        /// <summary>
        /// Обратить порядок вершин.
        /// </summary>
        public void Flip()
        {
            var temp = IndexVertex0;
            IndexVertex0 = IndexVertex1;
            IndexVertex1 = temp;
        }

        /// <summary>
        /// Получение нормали треугольника по индексу вершины.
        /// </summary>
        /// <param name="indexVertex">Индекс вершины относительно которой считается нормаль.</param>
        /// <param name="listVertex">Список вершин.</param>
        /// <returns>Нормаль.</returns>
        public readonly Vector3Df GetNormal(int indexVertex, ListVertex3D listVertex)
        {
            if (indexVertex == IndexVertex0)
            {
                var one = listVertex[IndexVertex1].Position - listVertex[IndexVertex0].Position;
                var two = listVertex[IndexVertex2].Position - listVertex[IndexVertex0].Position;
                var normal = Vector3Df.Cross(in one, in two);
                return normal;
            }
            else
            {
                if (indexVertex == IndexVertex1)
                {
                    var one = listVertex[IndexVertex2].Position - listVertex[IndexVertex1].Position;
                    var two = listVertex[IndexVertex0].Position - listVertex[IndexVertex1].Position;
                    var normal = Vector3Df.Cross(in one, in two);
                    return normal;
                }
                else
                {
                    var one = listVertex[IndexVertex0].Position - listVertex[IndexVertex2].Position;
                    var two = listVertex[IndexVertex1].Position - listVertex[IndexVertex2].Position;
                    var normal = Vector3Df.Cross(in one, in two);
                    return normal;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Вспомогательный класс реализующий список треугольников.
    /// </summary>
    [Serializable]
    public class ListTriangle3D : ListArray<Triangle3Df>, ILotusMeshOperaiton
    {
        #region Fields
        /// <summary>
        /// Вершины.
        /// </summary>
        public ListVertex3D Vertices;
        #endregion

        #region Properties
        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public TMeshElement MeshElement { get { return TMeshElement.Triangle; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public ListTriangle3D()
        {
            Vertices = new ListVertex3D();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="vertices">Список вершин.</param>
        public ListTriangle3D(ListVertex3D vertices)
        {
            Vertices = vertices;
        }
        #endregion

        #region ILotusMeshOperaiton methods
        /// <summary>
        /// Смещение вершин.
        /// </summary>
        /// <param name="offset">Вектор смещения.</param>
        public void Move(Vector3Df offset)
        {
            for (var i = 0; i < _count; i++)
            {
                Vertices.Vertices[_arrayOfItems[i].IndexVertex0].Position += offset;
                Vertices.Vertices[_arrayOfItems[i].IndexVertex1].Position += offset;
                Vertices.Vertices[_arrayOfItems[i].IndexVertex2].Position += offset;
            }
        }

        /// <summary>
        /// Врашение вершин.
        /// </summary>
        /// <param name="rotation">Кватернион вращения.</param>
        public void Rotate(Quaternion3Df rotation)
        {

        }

        /// <summary>
        /// Однородное масштабирование вершин.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(float scale)
        {

        }

        /// <summary>
        /// Масштабирование вершины.
        /// </summary>
        /// <param name="scale">Масштаб.</param>
        public void Scale(Vector3Df scale)
        {

        }

        /// <summary>
        /// Обратить нормали.
        /// </summary>
        public void FlipNormals()
        {

        }

        /// <summary>
        /// Обратить развёртку текстурных координат по горизонтали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVHorizontally(int channel = 0)
        {

        }

        /// <summary>
        /// Обратить развёртку текстурных координат по вертикали.
        /// </summary>
        /// <param name="channel">Канал текстурных координат.</param>
        public void FlipUVVertically(int channel = 0)
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Полное дублирование списка треугольников.
        /// </summary>
        /// <returns>Дубликат списка треугольников.</returns>
        public ListTriangle3D Duplicate()
        {
            var list_triangle = new ListTriangle3D(Vertices);
            for (var i = 0; i < _count; i++)
            {
                list_triangle.Add(_arrayOfItems[i]);
            }
            return list_triangle;
        }

        /// <summary>
        /// Смещение индексов вершин.
        /// </summary>
        /// <remarks>
        /// Применяется при соединении/разъединение мешей.
        /// </remarks>
        /// <param name="countVertex">Количество вершин на величину которых смещаются индексы.</param>
        public void Offset(int countVertex)
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Offset(countVertex);
            }
        }

        /// <summary>
        /// Получение списка треугольников со смещенными индексами вершин.
        /// </summary>
        /// <remarks>
        /// Применяется при соединении/разъединение мешей.
        /// </remarks>
        /// <param name="countVertex">Количество вершин на величину которых смещаются индексы.</param>
        /// <returns>Список треугольников.</returns>
        public ListTriangle3D GetTrianglesOffset(int countVertex)
        {
            var list_triangle = new ListTriangle3D(Vertices);
            for (var i = 0; i < _count; i++)
            {
                list_triangle.Add(_arrayOfItems[i].GetTriangleOffset(countVertex));
            }
            return list_triangle;
        }

        /// <summary>
        /// Получение списка треугольников имеющий хотя бы одну вершину совпадающей с указанной.
        /// </summary>
        /// <param name="indexVertex">Индекс вершины.</param>
        /// <returns>Список треугольников.</returns>
        public List<Triangle3Df> GetTrianglesOfIndexVertex(int indexVertex)
        {
            var triangles = new List<Triangle3Df>(3);
            for (var i = 0; i < Count; i++)
            {
                if (_arrayOfItems[i].IndexVertex0 == indexVertex ||
                    _arrayOfItems[i].IndexVertex1 == indexVertex ||
                    _arrayOfItems[i].IndexVertex2 == indexVertex)
                {
                    triangles.Add(_arrayOfItems[i]);
                }
            }

            return triangles;
        }

        /// <summary>
        /// Обратить порядок вершин.
        /// </summary>
        public void Flip()
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i].Flip();
            }
        }
        #endregion

        #region Triangle methods
        /// <summary>
        /// Добавление треугольника по последним(существующим) трем вершинам.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Топология вершин:
        /// 1)-------2)
        /// |      /
        /// |    /
        /// |  /
        /// |/
        /// 0)
        /// </para>
        /// <para>
        /// Треугольник: 0, 1, 2
        /// </para>
        /// </remarks>
        public void AddTriangle()
        {
            var iv1 = Vertices.Count - 3;
            var iv2 = Vertices.Count - 2;
            var iv3 = Vertices.Count - 1;
            var triangle = new Triangle3Df(iv1, iv2, iv3);
            Add(triangle);
        }

        /// <summary>
        /// Добавление треугольника.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Топология вершин:
        /// 1)-------2)
        /// |      /
        /// |    /
        /// |  /
        /// |/
        /// 0)
        /// </para>
        /// <para>
        /// Треугольник: 0, 1, 2
        /// </para>
        /// </remarks>
        /// <param name="iv1">Индекс первой вершины.</param>
        /// <param name="iv2">Индекс второй вершины.</param>
        /// <param name="iv3">Индекс третьей вершины.</param>
        public void AddTriangle(int iv1, int iv2, int iv3)
        {
            var triangle = new Triangle3Df(iv1, iv2, iv3);
            Add(triangle);
        }

        /// <summary>
        /// Добавление треугольника по последним(существующим) четырем вершинам которые образуют квадрат.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Топология квадрата:
        /// 2)------- 3)
        /// |       / |
        /// |     /   |
        /// |   /     |
        /// | /       |
        /// 0)--------1)
        /// </para>
        /// <para>
        /// Первый треугольник: 0, 2, 3
        /// Второй треугольник: 0, 3, 1
        /// </para>
        /// </remarks>
        public void AddTriangleQuad()
        {
            var iv1 = Vertices.Count - 4;
            var iv2 = Vertices.Count - 3;
            var iv3 = Vertices.Count - 2;
            var iv4 = Vertices.Count - 1;
            Add(new Triangle3Df(iv1, iv3, iv4));
            Add(new Triangle3Df(iv1, iv4, iv2));
        }

        /// <summary>
        /// Добавление треугольника четырем вершинам которые образуют квадрат.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Топология квадрата:
        /// 2)------- 3)
        /// |       / |
        /// |     /   |
        /// |   /     |
        /// | /       |
        /// 0)--------1)
        /// </para>
        /// <para>
        /// Первый треугольник: 0, 2, 3
        /// Второй треугольник: 0, 3, 1
        /// </para>
        /// </remarks>
        /// <param name="iv1">Индекс первой вершины.</param>
        /// <param name="iv2">Индекс второй вершины.</param>
        /// <param name="iv3">Индекс третьей вершины.</param>
        /// <param name="iv4">Индекс четвертой вершины.</param>
        public void AddTriangleQuad(int iv1, int iv2, int iv3, int iv4)
        {
            Add(new Triangle3Df(iv1, iv3, iv4));
            Add(new Triangle3Df(iv1, iv4, iv2));
        }

        /// <summary>
        /// Добавление списка треугольников типа последовательной регулярной сетки.
        /// </summary>
        /// <remarks>
        /// <para>
        /// https://en.wikipedia.org/wiki/Triangle_strip
        /// </para>
        /// <para>
        /// Пример сетки 2х3 квадрата
        /// </para>
        /// <para>
        /// Топология сетки:
        /// 9)------- 10)-------11)
        /// |       / |       / |
        /// |     /   |     /   |
        /// |   /     |   /     |
        /// | /       | /       |
        /// 6)--------7)--------8)
        /// |       / |       / |
        /// |     /   |     /   |
        /// |   /     |   /     |
        /// | /       | /       |
        /// 3)--------4)--------5)
        /// |       / |       / |
        /// |     /   |     /   |
        /// |   /     |   /     |
        /// | /       | /       |
        /// 0)--------1)--------2)
        /// </para>
        /// </remarks>
        /// <param name="startIndexVertex">Индекс начальной вершины.</param>
        /// <param name="columnCount">Количество столбцов.</param>
        /// <param name="rowCount">Количество строк.</param>
        /// <param name="isClosedColumn">Статус соединения последнего столбца с первым (т.е. по ширине сетки).</param>
        public void AddTriangleRegularGrid(int startIndexVertex, int columnCount, int rowCount, bool isClosedColumn = false)
        {
            for (var ir = 0; ir < rowCount; ir++)
            {
                for (var ic = 0; ic < columnCount; ic++)
                {
                    // Нижняя левая вершина
                    var left_bottom = ic + (ir * (columnCount + 1)) + startIndexVertex;

                    // Нижняя правая вершина
                    var right_bottom = left_bottom + 1 + startIndexVertex;

                    // Верхняя левая вершина
                    var left_top = ic + ((ir + 1) * (columnCount + 1)) + startIndexVertex;

                    // Верхняя правая вершина
                    var right_top = left_top + 1;

                    Add(new Triangle3Df(left_bottom, left_top, right_top));
                    Add(new Triangle3Df(left_bottom, right_top, right_bottom));

                    // Если это последний в строке квадрат и включен режим замыкания
                    if (isClosedColumn && ic == columnCount - 1)
                    {
                        // Нижняя левая вершина
                        var left_bottom_last = right_bottom;

                        // Верхняя левая вершина
                        var left_top_last = right_top;

                        // Нижняя правая вершина
                        var right_bottom_last = left_bottom_last - columnCount;

                        // Верхняя правая вершина
                        var right_top_last = left_top_last - columnCount;

                        Add(new Triangle3Df(left_bottom_last, left_top_last, right_top_last));
                        Add(new Triangle3Df(left_bottom_last, right_top_last, right_bottom_last));
                    }
                }
            }
        }

        /// <summary>
        /// Добавление списка треугольников типа веер.
        /// </summary>
        /// <remarks>
        /// <para>
        /// https://en.wikipedia.org/wiki/Triangle_fan
        /// </para>
        /// <para>
        /// Топология вершин:
        /// 3)---2)
        /// |   / \
        /// |  /   \
        /// | /     \
        /// |/       \
        /// 0)--------1)
        /// </para>
        /// </remarks>
        /// <param name="indexCenterVertex">Индекс вершины центра(общей вершины).</param>
        /// <param name="countTriangle">Количество треугольников для генерации.</param>
        /// <param name="isClosed">Статус замыкания.</param>
        public void AddTriangleFan(int indexCenterVertex, int countTriangle, bool isClosed)
        {
            if (countTriangle + 2 > Vertices.Count - indexCenterVertex)
            {
#if UNITY_2017_1_OR_NEWER

				UnityEngine.Debug.LogErrorFormat("Not enough vertices: Current: {0}, Dest: {1}",
					countTriangle + 2, Vertices.Count - indexCenterVertex);
#else
                XLogger.LogError(string.Format("Not enough vertices: Current: {0}, Dest: {1}",
                    countTriangle + 2, Vertices.Count - indexCenterVertex));
#endif
                return;
            }

            var iv0 = indexCenterVertex;
            var iv1 = iv0 + 1;
            var iv2 = iv0 + 2;

            for (var i = 0; i < countTriangle; i++)
            {
                //Add(new CTriangle3Df(iv0, iv1, iv2));
                Add(new Triangle3Df(iv0, iv2, iv1));

                iv1++;
                iv2++;
            }

            if (isClosed)
            {
                //Add(new CTriangle3Df(iv0, iv1, iv0 + 1));
                Add(new Triangle3Df(iv0, iv0 + 1, iv1));
            }
        }
        #endregion
    }
    /**@}*/
}