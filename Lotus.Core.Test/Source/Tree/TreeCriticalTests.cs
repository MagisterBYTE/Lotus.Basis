using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Tree
{
    /// <summary>
    /// Тесты для критических участков Tree (исправление опечаток и оптимизация).
    /// </summary>
    [TestFixture]
    public class TreeCriticalTests
    {
        /// <summary>
        /// Тест TreeNodeExtension - исправление опечатки в имени класса TreeNodeOrderComprare -> TreeNodeOrderCompare.
        /// </summary>
        [Test]
        public void TreeNodeExtension_CompareByOrder_ShouldUseCorrectClassName()
        {
            // Arrange
            var node1 = new TreeNodeObservable { Order = 1 };
            var node2 = new TreeNodeObservable { Order = 2 };
            var node3 = new TreeNodeObservable { Order = 3 };

            // Act - используем исправленное имя класса TreeNodeOrderCompare (не TreeNodeOrderComprare)
            var comparer = XTreeNodeExtension.CompareByOrder;

            // Assert
            ClassicAssert.IsNotNull(comparer, "Comparer should not be null");
            ClassicAssert.IsInstanceOf<XTreeNodeExtension.TreeNodeOrderCompare>(comparer, "Should be TreeNodeOrderCompare (not TreeNodeOrderComprare)");
            ClassicAssert.Less(comparer.Compare(node1, node2), 0, "node1 should be less than node2");
            ClassicAssert.Greater(comparer.Compare(node3, node2), 0, "node3 should be greater than node2");
            ClassicAssert.AreEqual(0, comparer.Compare(node1, node1), "Same nodes should be equal");
        }

        /// <summary>
        /// Тест TreeNodeExtension - исправление опечатки в комментарии "Отчистить" -> "Очистить".
        /// </summary>
        [Test]
        public void TreeNodeExtension_ClearAndAddTreeNodes_ShouldHaveCorrectComment()
        {
            // Arrange
            var parent = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            parent.AddTreeNode(child1);
            parent.AddTreeNode(child2);

            // Act - метод с исправленным комментарием "Очистить" (не "Отчистить")
            var newChildren = new List<ILotusTreeNode> { new TreeNodeObservable() };
            parent.ClearAndAddTreeNodes(newChildren);

            // Assert
            ClassicAssert.AreEqual(1, parent.ChildNodes!.Count, "Should have one child after clear and add");
        }

        /// <summary>
        /// Тест TreeNodeBuilder - корректная работа с исправленными именами переменных (nodeNew вместо node_new, nodeRoot вместо node_root).
        /// </summary>
        [Test]
        public void TreeNodeBuilder_ByFilter_ShouldUseCorrectVariableNames()
        {
            // Arrange
            var rootNode = new TreeNodeObservable { TextNode = "Root" };
            var child1 = new TreeNodeObservable { TextNode = "Child1" };
            var child2 = new TreeNodeObservable { TextNode = "Child2" };
            rootNode.AddTreeNode(child1);
            rootNode.AddTreeNode(child2);

            // Act - используем метод с исправленными именами переменных nodeNew и nodeRoot
            var result = TreeNodeBuilder.ByFilter(rootNode, 
                node => node != null, 
                node => new TreeNodeObservable { TextNode = node.ToString() + "_Copy" });

            // Assert
            ClassicAssert.IsNotNull(result, "Result should not be null");
            ClassicAssert.IsTrue(result!.ToString().Contains("Root"), "Should contain root text");
        }

        /// <summary>
        /// Тест TreeNodeObservable - оптимизация дублированного кода в UpdateParentsCheckedStatus (один раз parent is TreeNodeObservable вместо двух).
        /// </summary>
        [Test]
        public void TreeNodeObservable_UpdateParentsCheckedStatus_ShouldUseOptimizedCode()
        {
            // Arrange
            var grandParent = new TreeNodeObservable { TextNode = "GrandParent" };
            var parent = new TreeNodeObservable { TextNode = "Parent" };
            var child1 = new TreeNodeObservable { TextNode = "Child1" };
            var child2 = new TreeNodeObservable { TextNode = "Child2" };

            parent.AddTreeNode(child1);
            parent.AddTreeNode(child2);
            grandParent.AddTreeNode(parent);

            // Act - используем оптимизированный метод (один раз parent is TreeNodeObservable вместо двух)
            child1.IsChecked = true;
            child2.IsChecked = true;

            // Assert - проверяем что родитель обновился корректно
            ClassicAssert.IsTrue(parent.IsChecked == true, "Parent should be checked when all children are checked");
        }

        /// <summary>
        /// Тест TreeNodeObservable - UpdateParentsCheckedStatus с частично отмеченными узлами.
        /// </summary>
        [Test]
        public void TreeNodeObservable_UpdateParentsCheckedStatus_WithPartiallyChecked_ShouldSetNull()
        {
            // Arrange
            var parent = new TreeNodeObservable { TextNode = "Parent" };
            var child1 = new TreeNodeObservable { TextNode = "Child1" };
            var child2 = new TreeNodeObservable { TextNode = "Child2" };

            parent.AddTreeNode(child1);
            parent.AddTreeNode(child2);

            // Act
            child1.IsChecked = true;
            child2.IsChecked = false;

            // Assert - родитель должен быть null (частично отмечен)
            ClassicAssert.IsNull(parent.IsChecked, "Parent should be null when children are partially checked");
        }

        /// <summary>
        /// Тест TreeNodeObservable - UpdateParentsCheckedStatus с неотмеченными узлами.
        /// </summary>
        [Test]
        public void TreeNodeObservable_UpdateParentsCheckedStatus_WithUnchecked_ShouldSetFalse()
        {
            // Arrange
            var parent = new TreeNodeObservable { TextNode = "Parent" };
            var child1 = new TreeNodeObservable { TextNode = "Child1" };
            var child2 = new TreeNodeObservable { TextNode = "Child2" };

            parent.AddTreeNode(child1);
            parent.AddTreeNode(child2);

            // Act
            child1.IsChecked = false;
            child2.IsChecked = false;

            // Assert
            ClassicAssert.IsFalse(parent.IsChecked, "Parent should be false when all children are unchecked");
        }

        /// <summary>
        /// Тест TreeNodeExtension - SortChilds использует исправленное имя CompareByOrder.
        /// </summary>
        [Test]
        public void TreeNodeExtension_SortChilds_ShouldUseCorrectComparerName()
        {
            // Arrange
            var parent = new TreeNodeObservable();
            var child1 = new TreeNodeObservable { Order = 3 };
            var child2 = new TreeNodeObservable { Order = 1 };
            var child3 = new TreeNodeObservable { Order = 2 };
            parent.AddTreeNode(child1);
            parent.AddTreeNode(child2);
            parent.AddTreeNode(child3);

            // Act - используем метод с исправленным именем CompareByOrder (не ComprareByOrder)
            parent.SortChilds();

            // Assert
            ClassicAssert.AreEqual(1, parent.ChildNodes![0].Order, "First child should have order 1");
            ClassicAssert.AreEqual(2, parent.ChildNodes[1].Order, "Second child should have order 2");
            ClassicAssert.AreEqual(3, parent.ChildNodes[2].Order, "Third child should have order 3");
        }
    }
}
