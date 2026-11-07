
using System;
using System.Linq.Expressions;

namespace Lotus.Core.Expressions
{
    public class PredicateBuilderTests
    {
        [Fact]
        public void And_ShouldCombineExpressionsWithAndAlso()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x > 5;
            Expression<Func<int, bool>> expr2 = x => x < 10;

            // Act
            var combined = expr1.And(expr2);
            var func = combined.Compile();

            // Assert
            Assert.True(func(7));
            Assert.False(func(3));
            Assert.False(func(11));
        }

        [Fact]
        public void Or_ShouldCombineExpressionsWithOrElse()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x < 5;
            Expression<Func<int, bool>> expr2 = x => x > 10;

            // Act
            var combined = expr1.Or(expr2);
            var func = combined.Compile();

            // Assert
            Assert.True(func(3));
            Assert.True(func(11));
            Assert.False(func(7));
        }

        [Fact]
        public void Not_ShouldNegateExpression()
        {
            // Arrange
            Expression<Func<int, bool>> expr = x => x > 5;

            // Act
            var negated = expr.Not();
            var func = negated.Compile();

            // Assert
            Assert.True(func(3));
            Assert.False(func(7));
        }
    }

}