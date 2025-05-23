using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Tree
{
    [TestFixture]
    public class TreeNodeBuilderTests
    {
        [Test]
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
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(root.Id, result.Id);
            ClassicAssert.AreEqual(1, result.IChildNodes?.Count());
            ClassicAssert.AreEqual(child1.Id, result.IChildNodes?.First().Id);
        }

        [Test]
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
            ClassicAssert.AreEqual(2, root.IChildNodes?.Count());
            ClassicAssert.AreEqual(1, child1.IChildNodes?.Count());
            ClassicAssert.AreEqual(grandChild, child1.IChildNodes?.First());
        }
    }
}