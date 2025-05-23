using System;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Tree
{
    [TestFixture]
    public class TreeNodeExtensionTests
    {
        [Test]
        public void ExpandAll_ShouldSetIsExpandedTrueForAllNodes()
        {
            // Arrange
            var root = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            var grandChild = new TreeNodeObservable();
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            root.ExpandAll();

            // Assert
            ClassicAssert.IsTrue(root.IsExpanded);
            ClassicAssert.IsTrue(child1.IsExpanded);
            ClassicAssert.IsTrue(child2.IsExpanded);
            ClassicAssert.IsTrue(grandChild.IsExpanded);
        }

        [Test]
        public void CollapseAll_ShouldSetIsExpandedFalseForAllNodes()
        {
            // Arrange
            var root = new TreeNodeObservable { IsExpanded = true };
            var child1 = new TreeNodeObservable { IsExpanded = true };
            var child2 = new TreeNodeObservable { IsExpanded = true };
            var grandChild = new TreeNodeObservable { IsExpanded = true };
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            root.CollapseAll();

            // Assert
            ClassicAssert.IsFalse(root.IsExpanded);
            ClassicAssert.IsFalse(child1.IsExpanded);
            ClassicAssert.IsFalse(child2.IsExpanded);
            ClassicAssert.IsFalse(grandChild.IsExpanded);
        }

        [Test]
        public void CheckAll_ShouldSetIsCheckedTrueForAllNodes()
        {
            // Arrange
            var root = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            var grandChild = new TreeNodeObservable();
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            root.CheckAll();

            // Assert
            ClassicAssert.AreEqual(true, root.IsChecked);
            ClassicAssert.AreEqual(true, child1.IsChecked);
            ClassicAssert.AreEqual(true, child2.IsChecked);
            ClassicAssert.AreEqual(true, grandChild.IsChecked);
        }

        [Test]
        public void UncheckAll_ShouldSetIsCheckedFalseForAllNodes()
        {
            // Arrange
            var root = new TreeNodeObservable { IsChecked = true };
            var child1 = new TreeNodeObservable { IsChecked = true };
            var child2 = new TreeNodeObservable { IsChecked = true };
            var grandChild = new TreeNodeObservable { IsChecked = true };
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            root.UncheckAll();

            // Assert
            ClassicAssert.AreEqual(false, root.IsChecked);
            ClassicAssert.AreEqual(false, child1.IsChecked);
            ClassicAssert.AreEqual(false, child2.IsChecked);
            ClassicAssert.AreEqual(false, grandChild.IsChecked);
        }

        [Test]
        public void ToFlatListDescendants_ShouldReturnAllDescendants()
        {
            // Arrange
            var root = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            var grandChild = new TreeNodeObservable();
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            var flatList = root.ToFlatListDescendants<TreeNodeObservable>();

            // Assert
            ClassicAssert.AreEqual(3, flatList.Count);
            ClassicAssert.Contains(child1, flatList);
            ClassicAssert.Contains(child2, flatList);
            ClassicAssert.Contains(grandChild, flatList);
        }

        [Test]
        public void FindDescendants_ShouldFindMatchingNode()
        {
            // Arrange
            var root = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child1 = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child2 = new TreeNodeObservable { Id = Guid.NewGuid() };
            var grandChild = new TreeNodeObservable { Id = Guid.NewGuid() };
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            var targetId = grandChild.Id;

            // Act
            var foundNode = root.FindDescendants(n => n.Id == targetId);

            // Assert
            ClassicAssert.AreEqual(grandChild, foundNode);
        }

        [Test]
        public void FindAncestors_ShouldFindMatchingNode()
        {
            // Arrange
            var root = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child1 = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child2 = new TreeNodeObservable { Id = Guid.NewGuid() };
            var grandChild = new TreeNodeObservable { Id = Guid.NewGuid() };
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            var targetId = root.Id;

            // Act
            var foundNode = grandChild.FindAncestors(n => n.Id == targetId);

            // Assert
            ClassicAssert.AreEqual(root, foundNode);
        }

        [Test]
        public void UpdateOrderDescendants_ShouldSetOrderForAllNodes()
        {
            // Arrange
            var root = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            var grandChild = new TreeNodeObservable();
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);
            child1.ChildNodes.Add(grandChild);

            // Act
            root.UpdateOrderDescendants();

            // Assert
            ClassicAssert.AreEqual(0, child1.Order);
            ClassicAssert.AreEqual(1, child2.Order);
            ClassicAssert.AreEqual(0, grandChild.Order);
        }
    }
}