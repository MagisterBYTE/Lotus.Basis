using System;

using Lotus.Core;
using Lotus.Maths;

namespace Lotus.Object3D
{
    /** \addtogroup Object3DMeshCommon
	*@{*/
    /// <summary>
    /// Ребро трёхмерной полигональной сетки(меша).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Ребро представляет собой соединение двух вершин. Ребро может принадлежать как треугольникам с общими вершинами,
    /// так и треугольникам с разными вершинами, но имеющим одинаковую позицию.
    /// </para>
    /// <para>
    /// Для плоских трехмерных тел ребро также может принадлежать только одному треугольнику.
    /// </para>
    /// </remarks>
    [Serializable]
    public struct Edge3Df : IComparable<Edge3Df>, IEquatable<Edge3Df>
    {
        #region Static fields
        /// <summary>
        /// Текстовый формат отображения параметров ребра.
        /// </summary>
        public static string ToStringFormat = "{0}, {1}";
        #endregion

        #region Fields
        /// <summary>
        /// Индекс первой вершины первого треугольника.
        /// </summary>
        public int IndexVertex10;

        /// <summary>
        /// Индекс второй вершины первого треугольника.
        /// </summary>
        public int IndexVertex11;

        /// <summary>
        /// Индекс первой вершины второго треугольника.
        /// </summary>
        public int IndexVertex20;

        /// <summary>
        /// Индекс второй вершины второго треугольника.
        /// </summary>
        public int IndexVertex21;

        /// <summary>
        /// Индекс первого треугольника которому принадлежит ребро.
        /// </summary>
        public int IndexTriangle1;

        /// <summary>
        /// Индекс второго треугольника которому принадлежит ребро.
        /// </summary>
        public int IndexTriangle2;
        #endregion

        #region Properties
        /// <summary>
        /// Тип структурного элемента меша.
        /// </summary>
        public readonly TMeshElement MeshElement { get { return TMeshElement.Edge; } }

        /// <summary>
        /// Статус простого ребра.
        /// </summary>
        /// <remarks>
        /// Простое ребро принадлежит двум треугольника и при этом индексы вершин ссылаются на одни и те же вершины.
        /// </remarks>
        public readonly bool IsSimple
        {
            get
            {
                return IndexTriangle1 != -1 && IndexTriangle2 != -1 && IsEqualsIndex();
            }
        }

        /// <summary>
        /// Статус общего ребра.
        /// </summary>
        /// <remarks>
        /// Обще ребро принадлежит двум треугольника и при этом индексы вершин ссылаются на разные вершины.
        /// </remarks>
        public readonly bool IsCommon
        {
            get
            {
                return IndexTriangle1 != -1 && IndexTriangle2 != -1 && !IsEqualsIndex();
            }
        }

        /// <summary>
        /// Статус внешнего ребра.
        /// </summary>
        /// <remarks>
        /// Только для плоских трехмерных тел ребро также может принадлежать только одному треугольнику.
        /// </remarks>
        public readonly bool IsOuter
        {
            get
            {
                return IndexTriangle2 == -1;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="iv1">Индекс первой вершины.</param>
        /// <param name="iv2">Индекс второй вершины.</param>
        /// <param name="indexTriangle1">Индекс первого треугольника которому принадлежит ребро.</param>
        /// <param name="indexTriangle2">Индекс второго треугольника которому принадлежит ребро.</param>
        public Edge3Df(int iv1, int iv2, int indexTriangle1 = -1, int indexTriangle2 = -1)
        {
            IndexVertex10 = iv1;
            IndexVertex11 = iv2;
            IndexVertex20 = -1;
            IndexVertex21 = -1;
            IndexTriangle1 = indexTriangle1;
            IndexTriangle2 = indexTriangle2;
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
            if (obj is Edge3Df edge)
            {
                return Equals(edge);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства ребер.
        /// </summary>
        /// <remarks>
        /// Ребра равны если индексы вершины на которые они ссылаются равны.
        /// </remarks>
        /// <param name="other">Сравниваемое ребро.</param>
        /// <returns>Статус равенства ребер.</returns>
        public readonly bool Equals(Edge3Df other)
        {
            return (IndexVertex10 == other.IndexVertex10 && IndexVertex11 == other.IndexVertex11) ||
                (IndexVertex11 == other.IndexVertex10 && IndexVertex10 == other.IndexVertex11);
        }

        /// <summary>
        /// Сравнение ребер для упорядочивания.
        /// </summary>
        /// <param name="other">Ребро.</param>
        /// <returns>Статус сравнения ребер.</returns>
        public readonly int CompareTo(Edge3Df other)
        {
            if (IndexVertex10 > other.IndexVertex10)
            {
                return 1;
            }
            else
            {
                if (IndexVertex10 < other.IndexVertex10)
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
        /// Получение хеш-кода ребра.
        /// </summary>
        /// <returns>Хеш-код ребра.</returns>
        public override readonly int GetHashCode()
        {
            return IndexVertex10.GetHashCode() ^ IndexVertex11.GetHashCode();
        }

        /// <summary>
        /// Получение хеш-кода указанной ребра.
        /// </summary>
        /// <param name="obj">Ребро.</param>
        /// <returns>Хеш-код ребра.</returns>
        public readonly int GetHashCode(Edge3Df obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Индексы вершин ребра.</returns>
        public override readonly string ToString()
        {
            return string.Format(ToStringFormat, IndexVertex10, IndexVertex11);
        }
        #endregion

        #region Equals methods
        /// <summary>
        /// Проверка на равенство индексов.
        /// </summary>
        /// <remarks>
        /// Так как индексы вершин в треугольники могут иметь разный порядок формирования, то мы должны проверить
        /// в двух комбинациях
        /// </remarks>
        /// <returns>Статус равенства индексов.</returns>
        public readonly bool IsEqualsIndex()
        {
            // 1) Вариант
            if ((IndexVertex10 == IndexVertex20 && IndexVertex11 == IndexVertex21) ||
                (IndexVertex11 == IndexVertex20 && IndexVertex10 == IndexVertex21))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка данного ребра что позиция вершин по его индексам совпадет(равна) указанными позициями.
        /// </summary>
        /// <remarks>
        /// То есть сравниваются позиция вершины по индексу ребра и переданная позиция.
        /// </remarks>
        /// <param name="vertices">Список вершин.</param>
        /// <param name="p1">Первая позиция.</param>
        /// <param name="p2">Вторая позиция.</param>
        /// <returns>Статус совпадения (равенства).</returns>
        public readonly bool CheckFromPosition(ListVertex3D vertices, in Vector3Df p1, in Vector3Df p2)
        {
            // Проверяем первую пару
            if (IndexVertex10 != -1 && IndexVertex11 != -1)
            {
                var pe1 = vertices.Vertices[IndexVertex10].Position;
                var pe2 = vertices.Vertices[IndexVertex11].Position;

                if (Vector3Df.Approximately(in pe1, in p1) &&
                    Vector3Df.Approximately(in pe2, in p2))
                {
                    // Совпадает первая пара
                    return true;
                }
                else
                {
                    if (Vector3Df.Approximately(in pe1, in p2) &&
                        Vector3Df.Approximately(in pe2, in p1))
                    {
                        // Совпадает первая пара (обратный порядок)
                        return true;
                    }
                }
            }

            // Проверяем вторую пару
            if (IndexVertex20 != -1 && IndexVertex21 != -1)
            {
                var pe1 = vertices.Vertices[IndexVertex20].Position;
                var pe2 = vertices.Vertices[IndexVertex21].Position;

                if (Vector3Df.Approximately(in pe1, in p1) &&
                    Vector3Df.Approximately(in pe2, in p2))
                {
                    // Совпадает вторая пара
                    return true;
                }
                else
                {
                    if (Vector3Df.Approximately(in pe1, in p2) &&
                        Vector3Df.Approximately(in pe2, in p1))
                    {
                        // Совпадает вторая пара (обратный порядок)
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion
    }

    /// <summary>
    /// Вспомогательный класс реализующий список ребер.
    /// </summary>
    [Serializable]
    public class ListEdge3D : ListArray<Edge3Df>, ILotusMeshOperaiton
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
        public TMeshElement MeshElement { get { return TMeshElement.Edge; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public ListEdge3D()
        {
            Vertices = new ListVertex3D();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="vertices">Список вершин.</param>
        public ListEdge3D(ListVertex3D vertices)
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
                Vertices.Vertices[_arrayOfItems[i].IndexVertex10].Position += offset;
                Vertices.Vertices[_arrayOfItems[i].IndexVertex11].Position += offset;
                if (_arrayOfItems[i].IndexTriangle2 != -1 && !_arrayOfItems[i].IsEqualsIndex())
                {
                    Vertices.Vertices[_arrayOfItems[i].IndexVertex20].Position += offset;
                    Vertices.Vertices[_arrayOfItems[i].IndexVertex21].Position += offset;
                }
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
        /// Поиск ребер меша по индексам вершин которые ему принадлежат.
        /// </summary>
        /// <param name="iv1">Индекс первой вершины.</param>
        /// <param name="iv2">Индекс второй вершины.</param>
        /// <returns>Индекс ребра или -1 если такого ребра не оказалось.</returns>
        public int FindEdgeOfVertex(int iv1, int iv2)
        {
            var p1 = Vertices[iv1].Position;
            var p2 = Vertices[iv2].Position;

            for (var i = 0; i < _count; i++)
            {
                if (_arrayOfItems[i].CheckFromPosition(Vertices, in p1, in p2))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Получение списка внешних ребер.
        /// </summary>
        /// <remarks>
        /// Применяется в отношение плоских фигур для определения их контура.
        /// </remarks>
        /// <returns>Cписок внешних ребер.</returns>
        public ListEdge3D GetOuterEdge()
        {
            var outer_edges = new ListEdge3D();

            for (var i = 0; i < _count; i++)
            {
                if (_arrayOfItems[i].IndexTriangle2 == -1)
                {
                    outer_edges.Add(_arrayOfItems[i]);
                }
            }

            return outer_edges;
        }
        #endregion
    }
    /**@}*/
}