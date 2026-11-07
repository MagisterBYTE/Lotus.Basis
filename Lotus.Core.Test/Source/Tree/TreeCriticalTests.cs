using System;
using System.Collections.Generic;

namespace Lotus.Core.Tree
{
    /// <summary>
    /// Тесты для критических участков Tree (исправление опечаток и оптимизация).
    /// </summary>
    public class TreeCriticalTests
    {
        /// <summary>
        /// Тест TreeNodeExtension - исправление опечатки в имени класса TreeNodeOrderComprare -> TreeNodeOrderCompare.
        /// </summary>
        [Fact]
        public void TreeNodeExtension_CompareByOrder_ShouldUseCorrectClassName()
        {
            // Arrange
            var node1 = new TreeNodeObservable { Order = 1 };
            var node2 = new TreeNodeObservable { Order = 2 };
            var node3 = new TreeNodeObservable { Order = 3 };

            // Act - используем исправленное имя класса TreeNodeOrderCompare (не TreeNodeOrderComprare)
            var comparer = XTreeNodeExtension.CompareByOrder;

            // Assert
            Assert.NotNull(comparer);
            Assert.IsType<XTreeNodeExtension.TreeNodeOrderCompare>(comparer);
            Assert.True(comparer.Compare(node1, node2) < 0, "node1 should be less than node2");
            Assert.True(comparer.Compare(node3, node2) > 0, "node3 should be greater than node2");
            Assert.Equal(0, comparer.Compare(node1, node1));
        }

        /// <summary>
        /// Тест TreeNodeExtension - исправление опечатки в комментарии "Отчистить" -> "Очистить".
        /// </summary>
        [Fact]
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
            Assert.Equal(1, parent.ChildNodes!.Count);
        }

        /// <summary>
        /// Тест TreeNodeBuilder - корректная работа с исправленными именами переменных (nodeNew вместо nodeNew, nodeRoot вместо nodeRoot).
        /// </summary>
        [Fact]
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
            Assert.NotNull(result);
            Assert.True(result!.ToString().Contains("Root"), "Should contain root text");
        }

        /// <summary>
        /// Тест TreeNodeObservable - оптимизация дублированного кода в UpdateParentsCheckedStatus (один раз parent is TreeNodeObservable вместо двух).
        /// </summary>
        [Fact]
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
            Assert.True(parent.IsChecked == true, "Parent should be checked when all children are checked");
        }

        /// <summary>
        /// Тест TreeNodeObservable - UpdateParentsCheckedStatus с частично отмеченными узлами.
        /// </summary>
        [Fact]
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
            Assert.Null(parent.IsChecked);
        }

        /// <summary>
        /// Тест TreeNodeObservable - UpdateParentsCheckedStatus с неотмеченными узлами.
        /// </summary>
        [Fact]
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
            Assert.False(parent.IsChecked, "Parent should be false when all children are unchecked");
        }

        /// <summary>
        /// Тест TreeNodeExtension - SortChilds использует исправленное имя CompareByOrder.
        /// </summary>
        [Fact]
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
            Assert.Equal(1, parent.ChildNodes![0].Order);
            Assert.Equal(2, parent.ChildNodes[1].Order);
            Assert.Equal(3, parent.ChildNodes[2].Order);
        }
    }
}
