using System;
using System.Collections.Generic;
using System.Linq;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс для построения деревьев по различным критериям.
    /// </summary>
    public static class TreeNodeBuilder
    {
        /// <summary>
        /// Рекурсивное создание узлов по фильтру.
        /// </summary>
        /// <param name="parent">Родительский узел.</param>
        /// <param name="check">Текущий узел.</param>
        /// <param name="filter">Предикат фильтрации.</param>
        /// <param name="transform">Метод трансформатор для создания узлов.</param>
        /// <returns>Новый узел.</returns>
        private static ILotusTreeNode? ByFilter(ILotusTreeNode? parent, ILotusTreeNode check,
            Predicate<ILotusTreeNode?> filter, Converter<ILotusTreeNode, ILotusTreeNode> transform)
        {
            var node_new = transform(check);

            if (node_new == null)
            {
                return null;
            }

            if (check.CheckOne(filter))
            {
                if (parent != null)
                {
                    parent.AddTreeNode(node_new);

                }

                if (check.IChildNodes == null) return node_new;

                foreach (var node in check.IChildNodes)
                {
                    if (node != null)
                    {
                        ByFilter(node_new, node!, filter, transform);
                    }
                }
            }

            return node_new;
        }

        /// <summary>
        /// Рекурсивное создание узлов по фильтру.
        /// </summary>
        /// <param name="rootNode">Корневой узел.</param>
        /// <param name="filter">Предикат фильтрации.</param>
        /// <param name="transform">Метод трансформатор для создания узлов.</param>
        /// <returns>Новый узел.</returns>
        public static ILotusTreeNode? ByFilter(ILotusTreeNode rootNode, Predicate<ILotusTreeNode?> filter,
            Converter<ILotusTreeNode, ILotusTreeNode> transform)
        {
            var node_root = ByFilter(null, rootNode, filter, transform);
            return node_root;
        }

        /// <summary>
        /// Построить иерархию дочерних узлов по переданному линейному списку.
        /// </summary>
        /// <param name="rootNode">Корневой узел.</param>
        /// <param name="list">Список.</param>
        /// <param name="transform">Метод трансформатор для преобразования узлов.</param>
        /// <param name="keySelectorParentId">Метод для получения родительского Id.</param>
        public static void ByFlatList(ILotusTreeNode rootNode, IEnumerable<ILotusTreeNode> list,
            Converter<ILotusTreeNode, ILotusTreeNode>? transform = null, Func<ILotusTreeNode, Guid?>? keySelectorParentId = null)
        {
            var sourceList = list;

            if (transform != null)
            {
                sourceList = list.Select(x => transform(x)).ToArray();
            }

            var lookup = (keySelectorParentId == null) ? sourceList.ToLookup(c => c.IParentTreeNode?.Id)
                : sourceList.ToLookup(keySelectorParentId);

            foreach (var node in sourceList)
            {
                if (lookup.Contains(node.Id))
                {
                    node.ClearAndAddTreeNodes(lookup[node.Id]);
                }
                if (lookup.Contains(rootNode.Id))
                {
                    rootNode.ClearAndAddTreeNodes(lookup[rootNode.Id]);
                }
            }
        }
    }
}