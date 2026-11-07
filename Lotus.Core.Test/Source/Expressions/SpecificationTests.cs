
using System;
using System.Linq.Expressions;

namespace Lotus.Core.Expressions
{
    public class SpecificationTests
    {
        private class TestItem
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void IsSatisfiedBy_ShouldEvaluateExpression()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);
            var item1 = new TestItem { Value = 6 };
            var item2 = new TestItem { Value = 4 };

            // Act & Assert
            Assert.True(spec.IsSatisfiedBy(item1));
            Assert.False(spec.IsSatisfiedBy(item2));
        }

        [Fact]
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
            Assert.True(combined.IsSatisfiedBy(item1));
            Assert.False(combined.IsSatisfiedBy(item2));
        }

        [Fact]
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
            Assert.True(combined.IsSatisfiedBy(item1));
            Assert.True(combined.IsSatisfiedBy(item2));
            Assert.False(combined.IsSatisfiedBy(item3));
        }

        [Fact]
        public void NotOperator_ShouldNegateSpecification()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);
            var item1 = new TestItem { Value = 6 };
            var item2 = new TestItem { Value = 4 };

            // Act
            var negated = !spec;

            // Assert
            Assert.False(negated.IsSatisfiedBy(item1));
            Assert.True(negated.IsSatisfiedBy(item2));
        }

        [Fact]
        public void ImplicitConversion_ToExpression_ShouldWork()
        {
            // Arrange
            var spec = new Specification<TestItem>(x => x.Value > 5);

            // Act
            Expression<Func<TestItem, bool>> expr = spec;

            // Assert
            Assert.NotNull(expr);
            Assert.IsAssignableFrom<Expression<Func<TestItem, bool>>>(expr);
        }

        [Fact]
        public void ImplicitConversion_FromExpression_ShouldWork()
        {
            // Arrange
            Expression<Func<TestItem, bool>> expr = x => x.Value > 5;

            // Act
            Specification<TestItem> spec = expr;

            // Assert
            Assert.NotNull(spec);
            Assert.True(spec.IsSatisfiedBy(new TestItem { Value = 6 }));
        }

        [Fact]
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
            Assert.True(parentSpec.IsSatisfiedBy(item1));
            Assert.False(parentSpec.IsSatisfiedBy(item2));
        }
    }
}