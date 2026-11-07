using System;
using System.Collections.Generic;


namespace Lotus.Core.Tree
{
    public class TreeNodeObservableTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var node = new TreeNodeObservable();

            // Assert
            Assert.Equal(Guid.Empty, node.Id);
            Assert.Empty(node.TextNode);
            Assert.False(node.IsSelected);
            Assert.False(node.IsExpanded);
            Assert.Equal(false, node.IsChecked);
            Assert.Equal(-1, node.Order);
            Assert.Null(node.IParentTreeNode);
            Assert.NotNull(node.IChildNodes);
            Assert.Empty(node.IChildNodes);
            Assert.NotNull(node.Attributes);
            Assert.Empty(node.Attributes);
        }

        [Fact]
        public void PropertyChanges_ShouldRaisePropertyChanged()
        {
            // Arrange
            var node = new TreeNodeObservable();
            var changedProperties = new List<string>();
            node.PropertyChanged += (s, e) => changedProperties.Add(e.PropertyName);

            // Act
            node.Id = Guid.NewGuid();
            node.TextNode = "Test";
            node.IsSelected = true;
            node.IsExpanded = true;
            node.IsChecked = true;
            node.Order = 1;

            // Assert
            Assert.Contains(nameof(TreeNodeObservable.TextNode), changedProperties);
            Assert.Contains(nameof(TreeNodeObservable.IsSelected), changedProperties);
            Assert.Contains(nameof(TreeNodeObservable.IsExpanded), changedProperties);
            Assert.Contains(nameof(TreeNodeObservable.IsChecked), changedProperties);
            Assert.Contains(nameof(TreeNodeObservable.Order), changedProperties);
        }

        [Fact]
        public void CheckOne_ShouldReturnTrueWhenPredicateMatches()
        {
            // Arrange
            var node = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child = new TreeNodeObservable { Id = Guid.NewGuid() };
            node.ChildNodes.Add(child);

            // Act & Assert
            Assert.True(node.CheckOne(n => n?.Id == node.Id));
            Assert.True(node.CheckOne(n => n?.Id == child.Id));
            Assert.False(node.CheckOne(n => n?.Id == Guid.NewGuid()));
        }

        [Fact]
        public void UpdateParentsCheckedStatus_ShouldUpdateParentCheckedState()
        {
            // Arrange
            var parent = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            parent.ChildNodes.Add(child1);
            parent.ChildNodes.Add(child2);

            // Act
            child1.IsChecked = true;
            child2.IsChecked = true;

            // Assert
            Assert.Equal(true, parent.IsChecked);
        }

        [Fact]
        public void UpdateChildsCheckedStatus_ShouldUpdateChildrenCheckedState()
        {
            // Arrange
            var parent = new TreeNodeObservable();
            var child1 = new TreeNodeObservable();
            var child2 = new TreeNodeObservable();
            parent.ChildNodes.Add(child1);
            parent.ChildNodes.Add(child2);

            // Act
            parent.IsChecked = true;

            // Assert
            Assert.Equal(true, child1.IsChecked);
            Assert.Equal(true, child2.IsChecked);
        }

        [Fact]
        public void Attributes_ShouldStoreAndRetrieveValues()
        {
            // Arrange
            var node = new TreeNodeObservable();
            const string key = "test";
            const int value = 42;

            // Act
            node.SetCustomAttributeInt(key, value);
            var result = node.GetCustomAttributeInt(key);

            // Assert
            Assert.Equal(value, result);
        }
    }
}