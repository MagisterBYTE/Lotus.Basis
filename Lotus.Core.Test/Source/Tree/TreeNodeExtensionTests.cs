using System;


namespace Lotus.Core.Tree
{
    public class TreeNodeExtensionTests
    {
        [Fact]
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
            Assert.True(root.IsExpanded);
            Assert.True(child1.IsExpanded);
            Assert.True(child2.IsExpanded);
            Assert.True(grandChild.IsExpanded);
        }

        [Fact]
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
            Assert.False(root.IsExpanded);
            Assert.False(child1.IsExpanded);
            Assert.False(child2.IsExpanded);
            Assert.False(grandChild.IsExpanded);
        }

        [Fact]
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
            Assert.Equal(true, root.IsChecked);
            Assert.Equal(true, child1.IsChecked);
            Assert.Equal(true, child2.IsChecked);
            Assert.Equal(true, grandChild.IsChecked);
        }

        [Fact]
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
            Assert.Equal(false, root.IsChecked);
            Assert.Equal(false, child1.IsChecked);
            Assert.Equal(false, child2.IsChecked);
            Assert.Equal(false, grandChild.IsChecked);
        }

        [Fact]
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
            Assert.Equal(3, flatList.Count);
            Assert.Contains(child1, flatList);
            Assert.Contains(child2, flatList);
            Assert.Contains(grandChild, flatList);
        }

        [Fact]
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
            Assert.Equal(grandChild, foundNode);
        }

        [Fact]
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
            Assert.Equal(root, foundNode);
        }

        [Fact]
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
            Assert.Equal(0, child1.Order);
            Assert.Equal(1, child2.Order);
            Assert.Equal(0, grandChild.Order);
        }
    }
}