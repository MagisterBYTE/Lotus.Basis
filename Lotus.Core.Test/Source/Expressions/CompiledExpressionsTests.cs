using System;
using System.Linq.Expressions;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Expressions
{
    [TestFixture]
    public class CompiledExpressionsTests
    {
        [Test]
        public void AsFunc_ShouldCompileAndCacheExpression()
        {
            // Arrange
            Expression<Func<int, int>> expression = x => x * 2;

            // Act
            var func1 = XCompiledExpressions<int, int>.AsFunc(expression);
            var func2 = XCompiledExpressions<int, int>.AsFunc(expression);

            // Assert
            ClassicAssert.AreEqual(4, func1(2));
            ClassicAssert.AreEqual(6, func1(3));
            ClassicAssert.AreSame(func1, func2, "Should return cached delegate");
        }
    }
}