using NUnit.Framework;
using NUnit.Framework.Legacy;

using System;
using System.Linq.Expressions;

namespace Lotus.Core.Expressions
{
    [TestFixture]
    public class PredicateBuilderTests
    {
        [Test]
        public void And_ShouldCombineExpressionsWithAndAlso()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x > 5;
            Expression<Func<int, bool>> expr2 = x => x < 10;

            // Act
            var combined = expr1.And(expr2);
            var func = combined.Compile();

            // Assert
            ClassicAssert.IsTrue(func(7));
            ClassicAssert.IsFalse(func(3));
            ClassicAssert.IsFalse(func(11));
        }

        [Test]
        public void Or_ShouldCombineExpressionsWithOrElse()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x < 5;
            Expression<Func<int, bool>> expr2 = x => x > 10;

            // Act
            var combined = expr1.Or(expr2);
            var func = combined.Compile();

            // Assert
            ClassicAssert.IsTrue(func(3));
            ClassicAssert.IsTrue(func(11));
            ClassicAssert.IsFalse(func(7));
        }

        [Test]
        public void Not_ShouldNegateExpression()
        {
            // Arrange
            Expression<Func<int, bool>> expr = x => x > 5;

            // Act
            var negated = expr.Not();
            var func = negated.Compile();

            // Assert
            ClassicAssert.IsTrue(func(3));
            ClassicAssert.IsFalse(func(7));
        }
    }

}