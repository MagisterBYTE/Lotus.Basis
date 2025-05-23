using NUnit.Framework;
using NUnit.Framework.Legacy;

using System;
using System.Linq.Expressions;

namespace Lotus.Core.Expressions
{
    [TestFixture]
    public class SpecificationTests
    {
        private class TestItem
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void IsSatisfiedBy_ShouldEvaluateExpression()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);
            var item1 = new TestItem { Value = 6 };
            var item2 = new TestItem { Value = 4 };

            // Act & Assert
            ClassicAssert.IsTrue(spec.IsSatisfiedBy(item1));
            ClassicAssert.IsFalse(spec.IsSatisfiedBy(item2));
        }

        [Test]
        public void AndOperator_ShouldCombineSpecifications()
        {
            // Arrange
            var spec1 = new Specification<TestItem>(x => x.Value > 5);
            var spec2 = new Specification<TestItem>(x => x.Name == "test");
            var item1 = new TestItem { Value = 6, Name = "test" };
            var item2 = new TestItem { Value = 6, Name = "other" };

            // Act
            var combined = spec1 & spec2;

            // Assert
            ClassicAssert.IsTrue(combined.IsSatisfiedBy(item1));
            ClassicAssert.IsFalse(combined.IsSatisfiedBy(item2));
        }

        [Test]
        public void OrOperator_ShouldCombineSpecifications()
        {
            // Arrange
            var spec1 = new Specification<TestItem>(x => x.Value > 5);
            var spec2 = new Specification<TestItem>(x => x.Name == "test");
            var item1 = new TestItem { Value = 6, Name = "other" };
            var item2 = new TestItem { Value = 4, Name = "test" };
            var item3 = new TestItem { Value = 4, Name = "other" };

            // Act
            var combined = spec1 | spec2;

            // Assert
            ClassicAssert.IsTrue(combined.IsSatisfiedBy(item1));
            ClassicAssert.IsTrue(combined.IsSatisfiedBy(item2));
            ClassicAssert.IsFalse(combined.IsSatisfiedBy(item3));
        }

        [Test]
        public void NotOperator_ShouldNegateSpecification()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);
            var item1 = new TestItem { Value = 6 };
            var item2 = new TestItem { Value = 4 };

            // Act
            var negated = !spec;

            // Assert
            ClassicAssert.IsFalse(negated.IsSatisfiedBy(item1));
            ClassicAssert.IsTrue(negated.IsSatisfiedBy(item2));
        }

        [Test]
        public void ImplicitConversion_ToExpression_ShouldWork()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);

            // Act
            Expression<Func<TestItem, bool>> expr = spec;

            // Assert
            ClassicAssert.IsNotNull(expr);
            ClassicAssert.IsInstanceOf<Expression<Func<TestItem, bool>>>(expr);
        }

        [Test]
        public void ImplicitConversion_FromExpression_ShouldWork()
        {
            // Arrange
            Expression<Func<TestItem, bool>> expr = x => x.Value > 5;

            // Act
            Specification<TestItem> spec = expr;

            // Assert
            ClassicAssert.IsNotNull(spec);
            ClassicAssert.IsTrue(spec.IsSatisfiedBy(new TestItem { Value = 6 }));
        }

        [Test]
        public void From_ShouldCreateSpecificationForParentType()
        {
            // Arrange
            var spec = new Specification<string>(s => s.Length > 5);
            Expression<Func<TestItem, string>> mapFrom = item => item.Name;

            // Act
            var parentSpec = spec.From(mapFrom);
            var item1 = new TestItem { Name = "longname" };
            var item2 = new TestItem { Name = "short" };

            // Assert
            ClassicAssert.IsTrue(parentSpec.IsSatisfiedBy(item1));
            ClassicAssert.IsFalse(parentSpec.IsSatisfiedBy(item2));
        }
    }
}