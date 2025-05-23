using System;
using System.Collections.Generic;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Tree
{
    [TestFixture]
    public class TreeNodeObservableTests
    {
        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var node = new TreeNodeObservable();

            // Assert
            ClassicAssert.AreEqual(Guid.Empty, node.Id);
            ClassicAssert.IsEmpty(node.TextNode);
            ClassicAssert.IsFalse(node.IsSelected);
            ClassicAssert.IsFalse(node.IsExpanded);
            ClassicAssert.AreEqual(false, node.IsChecked);
            ClassicAssert.AreEqual(-1, node.Order);
            ClassicAssert.IsNull(node.IParentTreeNode);
            ClassicAssert.IsNotNull(node.IChildNodes);
            ClassicAssert.IsEmpty(node.IChildNodes);
            ClassicAssert.IsNotNull(node.Attributes);
            ClassicAssert.IsEmpty(node.Attributes);
        }

        [Test]
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
            ClassicAssert.Contains(nameof(TreeNodeObservable.TextNode), changedProperties);
            ClassicAssert.Contains(nameof(TreeNodeObservable.IsSelected), changedProperties);
            ClassicAssert.Contains(nameof(TreeNodeObservable.IsExpanded), changedProperties);
            ClassicAssert.Contains(nameof(TreeNodeObservable.IsChecked), changedProperties);
            ClassicAssert.Contains(nameof(TreeNodeObservable.Order), changedProperties);
        }

        [Test]
        public void CheckOne_ShouldReturnTrueWhenPredicateMatches()
        {
            // Arrange
            var node = new TreeNodeObservable { Id = Guid.NewGuid() };
            var child = new TreeNodeObservable { Id = Guid.NewGuid() };
            node.ChildNodes.Add(child);

            // Act & Assert
            ClassicAssert.IsTrue(node.CheckOne(n => n?.Id == node.Id));
            ClassicAssert.IsTrue(node.CheckOne(n => n?.Id == child.Id));
            ClassicAssert.IsFalse(node.CheckOne(n => n?.Id == Guid.NewGuid()));
        }

        [Test]
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
            ClassicAssert.AreEqual(true, parent.IsChecked);
        }

        [Test]
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
            ClassicAssert.AreEqual(true, child1.IsChecked);
            ClassicAssert.AreEqual(true, child2.IsChecked);
        }

        [Test]
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
            ClassicAssert.AreEqual(value, result);
        }
    }
}