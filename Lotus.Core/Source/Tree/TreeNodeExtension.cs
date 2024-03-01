
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="ITreeNode"/>.
    /// </summary>
    public static class TreeNodeExtension
    {
        /// <summary>
        /// Класс реализующий стандартный компаратор для сравнения узлов по их индексу.
        /// </summary>
        public class TreeNodeOrderComprare : IComparer
        {
            public int Compare(Object? x, Object? y)
            {
                if (x is ITreeNode xNode && y is ITreeNode yNode)
                {
                    return xNode.Order.CompareTo(yNode.Order);
                }
                return 0;
            }
        }

        /// <summary>
        /// Глобальный компаратор для сравнения узлов по их индексу.
        /// </summary>
        public static readonly TreeNodeOrderComprare ComprareByOrder = new TreeNodeOrderComprare();

        /// <summary>
        /// Рекурсивное раскрытие всех узлов.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void ExpandAll(this ITreeNode @this)
        {
            @this.IsExpanded = true;

            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                node.ExpandAll();
            }
        }

        /// <summary>
        /// Рекурсивное сворачивание всех узлов.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void CollapseAll(this ITreeNode @this)
        {
            @this.IsExpanded = false;

            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                node.CollapseAll();
            }
        }

        /// <summary>
        /// Рекурсивное выделение всех узлов флажком.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void CheckAll(this ITreeNode @this)
        {
            @this.IsChecked = true;

            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                node.CheckAll();
            }
        }

        /// <summary>
        /// Рекурсивное снятие флажка выделения.
        /// </summary>
        /// <param name="this">Текущий узел</param>
        public static void UncheckAll(this ITreeNode @this)
        {
            @this.IsChecked = false;

            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                node.UncheckAll();
            }
        }

        /// <summary>
        /// Рекурсивно обновляет <see cref="ITreeNode.IsChecked"/> всех родителей текущего узла. 
        /// Устанавливает <see cref="ITreeNode.IsChecked"/> в true, если все вложенные узлы отмечены. <br></br>
        /// Null - если отмечен хотя бы один узел. <br></br>
        /// False - если не отмечен ни один узел.<br></br>
        /// Рекурсивно проходит вверх по дереву.
        /// </summary>
        /// <param name="this">Текущий узел</param>
        public static void UpdateParentsCheck(this ITreeNode @this)
        {
            var parent = @this.IParentTreeNode;
            if (parent == null || parent == @this || parent.IChildNodes == null)
            {
                return;
            }

            if (parent.IChildNodes.Any(c => c.IsChecked != false))
            {
                if (parent.IChildNodes.All(c => c.IsChecked == true))
                {
                    parent.IsChecked = true;
                }
                else
                {
                    parent.IsChecked = null;
                }
            }
            else
            {
                parent.IsChecked = false;
            }

            parent.UpdateParentsCheck();
        }

        /// <summary>
        /// Получение индекса расположения узла дерева.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <returns>Индекс расположения или -1.</returns>
        public static int GetIndexTreeNode(this ITreeNode @this)
        {
            if (@this.IParentTreeNode != null && @this.IParentTreeNode.IChildNodes is IList listNodes)
            {
                return listNodes.IndexOf(@this);
            }

            return -1;
        }

        /// <summary>
        /// Перемещение узла дерева в новое положение.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="newIndex">Индекс расположения.</param>
        public static void MoveTreeNode(this ITreeNode @this, int newIndex)
        {
            if (newIndex == -1) return;

            if (@this.IParentTreeNode != null && @this.IParentTreeNode.IChildNodes is IList listNodes)
            {
                var currentIndex = listNodes.IndexOf(@this);
                if (currentIndex != -1)
                {
                    var item = listNodes[currentIndex];
                    listNodes.RemoveAt(currentIndex);
                    listNodes.Insert(newIndex, item);
                }
            }
        }

        /// <summary>
        /// Сортировка по свойству <see cref="ITreeNode.Order"/> только дочерних узлов.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void SortChilds(this ITreeNode @this)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                listNodes.Sort(ComprareByOrder);
            }
        }

        /// <summary>
        /// Отчисть и добавить дочерние узлы из списка.
        /// </summary>
        /// <param name="this">Текущий узел</param>
        /// <param name="list">Список.</param>
        public static void ClearAndAddTreeNodes(this ITreeNode @this, IEnumerable<ITreeNode> list)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                listNodes.Clear();
                foreach (var node in list)
                {
                    node.IParentTreeNode = @this;
                    listNodes.Add(node);
                }
            }
        }

        /// <summary>
        /// Добавить дочерние узлы из списка.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="list">Список.</param>
        public static void AddTreeNodes(this ITreeNode @this, IEnumerable<ITreeNode> list)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                foreach (var node in list)
                {
                    node.IParentTreeNode = @this;
                    listNodes.Add(node);
                }
            }
        }

        /// <summary>
        /// Удалить дочерние узлы по списку.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="list">Список.</param>
        public static void RemoveTreeNodes(this ITreeNode @this, IEnumerable<ITreeNode> list)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                foreach (var node in list)
                {
                    node.IParentTreeNode = null;
                    listNodes.Remove(node);
                }
            }
        }

        /// <summary>
        /// Добавить дочерний узел.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="node">Добавляемый узел.</param>
        public static void AddTreeNode(this ITreeNode @this, ITreeNode node)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                node.IParentTreeNode = @this;
                listNodes.Add(node);
            }
        }

        /// <summary>
        /// Вставить дочерний узел.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="node">Вставляемый узел.</param>
        /// <param name="index">Индекс вставки.</param>
        public static void InsertTreeNode(this ITreeNode @this, ITreeNode node, int index)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                node.IParentTreeNode = @this;
                listNodes.Insert(index, node);
            }
        }

        /// <summary>
        /// Удалить дочерний узел.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="node">Удаляемый узел</param>
        public static void RemoveTreeNode(this ITreeNode @this, ITreeNode node)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                listNodes.Remove(node);
                node.IParentTreeNode = null;
            }
        }

        /// <summary>
        /// Заполнить указанный список узлами.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="list">Список.</param>
        /// <param name="transform">Метод трансформатор для преобразования узлов.</param>
        private static void FillList<TTreeNode>(this ITreeNode @this, IList<TTreeNode> list,
            Converter<ITreeNode, TTreeNode>? transform = null)
            where TTreeNode : ITreeNode
        {
            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                if (transform == null)
                {
                    list.Add((TTreeNode)node);
                }
                else
                {
                    list.Add(transform(node));
                }
                node.FillList(list, transform);
            }
        }

        /// <summary>
        /// Преобразовать в линейный список все дочернии узлы и их потомки.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="transform">Метод трансформатор для преобразования узлов.</param>
        /// <returns>Линейный список узлов.</returns>
        public static List<TTreeNode> ToFlatListDescendants<TTreeNode>(this ITreeNode @this,
            Converter<ITreeNode, TTreeNode>? transform = null)
            where TTreeNode : ITreeNode
        {
            var list = new List<TTreeNode>();
            if (@this.IChildNodes == null) return list;

            foreach (var node in @this.IChildNodes)
            {
                if (transform == null)
                {
                    list.Add((TTreeNode)node);
                }
                else
                {
                    list.Add(transform(node));
                }
                node.FillList(list, transform);
            }

            return list;
        }

        /// <summary>
        /// Преобразовать в линейный список текущий узел и все дочернии узлы и их потомки.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="transform">Метод трансформатор для преобразования узлов.</param>
        /// <returns>Линейный список узлов.</returns>
        public static List<TTreeNode> ToFlatListSelfAndDescendants<TTreeNode>(this ITreeNode @this,
            Converter<ITreeNode, TTreeNode>? transform = null)
            where TTreeNode : ITreeNode
        {
            var list = new List<TTreeNode>();

            if (transform != null)
            {
                list.Add(transform(@this));
            }
            else
            {
                list.Add((TTreeNode)@this);
            }

            if (@this.IChildNodes == null) return list;

            foreach (var node in @this.IChildNodes)
            {
                if (transform == null)
                {
                    list.Add((TTreeNode)node);
                }
                else
                {
                    list.Add(transform(node));
                }
                node.FillList(list, transform);
            }

            return list;
        }

        /// <summary>
        /// Рекурсивный поиск в дочерних узлах и их потомков первого узла совпавшему по указанному предикату.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="predicate">Предикат поиска.</param>
        /// <returns>Найденный узел или null</returns>
        public static ITreeNode? FindDescendants(this ITreeNode @this, Predicate<ITreeNode> predicate)
        {
            if (predicate(@this))
            {
                return @this;
            }

            if (@this.IChildNodes == null) return null;

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (var node in @this.IChildNodes)
            {
                if (predicate(node))
                {
                    return node;
                }
            }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions

            foreach (var node in @this.IChildNodes)
            {
                var result = node.FindDescendants(predicate);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Рекурсивный поиск в предках первого узла совпавшему по указанному предикату.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="predicate">Предикат поиска.</param>
        /// <returns>Найденный узел или null.</returns>
        public static ITreeNode? FindAncestors(this ITreeNode @this, Predicate<ITreeNode> predicate)
        {
            if (predicate(@this))
            {
                return @this;
            }

            if (@this.IParentTreeNode == null) return null;

            return @this.IParentTreeNode.FindAncestors(predicate);
        }

        /// <summary>
        /// Рекурсивное посещение каждого дочернего узла и его потомков.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="callback">Посетитель узла.</param>
        public static void VisitDescendants(this ITreeNode @this, Action<ITreeNode> callback)
        {
            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                callback(node);
                VisitDescendants(node, callback);
            }
        }

        /// <summary>
        /// Рекурсивное посещение каждого предка.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        /// <param name="callback">Посетитель узла.</param>
        public static void VisitAncestors(this ITreeNode @this, Action<ITreeNode> callback)
        {
            callback(@this);

            if (@this.IParentTreeNode == null) return;

            @this.IParentTreeNode.VisitAncestors(callback);
        }

        /// <summary>
        /// Рекурсивное присваивание родителя каждому дочернему узла и его потомков.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void UpdateParentsDescendants(this ITreeNode @this)
        {
            if (@this.IChildNodes == null) return;

            foreach (var node in @this.IChildNodes)
            {
                node.IParentTreeNode = @this;
                node.UpdateParentsDescendants();
            }
        }

        /// <summary>
        /// Рекурсивное присваивание каждому дочернему узла и его потомков индекса его расположения в списке.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void UpdateOrderDescendants(this ITreeNode @this)
        {
            if (@this.IChildNodes == null) return;

            var index = 0;
            foreach (var node in @this.IChildNodes)
            {
                node.Order = index;
                index++;
            }

            foreach (var node in @this.IChildNodes)
            {
                node.UpdateOrderDescendants();
            }
        }

        /// <summary>
        /// Сортировка по свойству <see cref="ITreeNode.Order"/> дочерних узлов и их потомков.
        /// </summary>
        /// <param name="this">Текущий узел.</param>
        public static void SortDescendants(this ITreeNode @this)
        {
            if (@this.IChildNodes == null) return;

            if (@this.IChildNodes is IList listNodes)
            {
                listNodes.Sort(ComprareByOrder);
            }

            foreach (var node in @this.IChildNodes)
            {
                node.SortDescendants();
            }
        }
    }
}