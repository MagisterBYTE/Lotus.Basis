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
		private static ITreeNode? ByFilter(ITreeNode? parent, ITreeNode check,
			Predicate<ITreeNode?> filter, Converter<ITreeNode, ITreeNode> transform)
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
		public static ITreeNode? ByFilter(ITreeNode rootNode, Predicate<ITreeNode?> filter,
			Converter<ITreeNode, ITreeNode> transform)
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
		public static void ByFlatList(ITreeNode rootNode, IEnumerable<ITreeNode> list,
			Converter<ITreeNode, ITreeNode>? transform = null, Func<ITreeNode, Guid?>? keySelectorParentId = null)
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
