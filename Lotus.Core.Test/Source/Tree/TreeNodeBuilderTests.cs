using System;
using System.Collections.Generic;
using System.Linq;


namespace Lotus.Core.Tree
{
    public class TreeNodeBuilderTests
    {
        [Fact]
        public void ByFilter_ShouldBuildTreeBasedOnFilter()
        {
            // Arrange
            var root = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child1 = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child2 = new TreeNodeObservable { Id = Guid.NewGuid() };
            root.ChildNodes.Add(child1);
            root.ChildNodes.Add(child2);

            Predicate<ILotusTreeNode?> filter = n => n?.Id == root.Id || n?.Id == child1.Id;
            Converter<ILotusTreeNode, ILotusTreeNode> transform = n => new TreeNodeObservable { Id = n.Id };

            // Act
            var result = TreeNodeBuilder.ByFilter(root, filter, transform);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(root.Id, result.Id);
            Assert.Equal(1, result.IChildNodes?.Count());
            Assert.Equal(child1.Id, result.IChildNodes?.First().Id);
        }

        [Fact]
        public void ByFlatList_ShouldBuildHierarchyFromFlatList()
        {
            // Arrange
            var root = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child1 = new TreeNodeObservable { Id = Guid.NewGuid(), IParentTreeNode = root };
            var child2 = new TreeNodeObservable { Id = Guid.NewGuid(), IParentTreeNode = root };
            var grandChild = new TreeNodeObservable { Id = Guid.NewGuid(), IParentTreeNode = child1 };

            var flatList = new List<TreeNodeObservable> { root, child1, child2, grandChild };

            // Act
            TreeNodeBuilder.ByFlatList(root, flatList);

            // Assert
            Assert.Equal(2, root.IChildNodes?.Count());
            Assert.Equal(1, child1.IChildNodes?.Count());
            Assert.Equal(grandChild, child1.IChildNodes?.First());
        }
    }
}