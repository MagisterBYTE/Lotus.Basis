using System;
using System.Linq.Expressions;


namespace Lotus.Core.Expressions
{
    public class CompiledExpressionsTests
    {
        [Fact]
        public void AsFunc_ShouldCompileAndCacheExpression()
        {
            // Arrange
            Expression<Func<int, int>> expression = x => x * 2;

            // Act
            var func1 = XCompiledExpressions<int, int>.AsFunc(expression);
            var func2 = XCompiledExpressions<int, int>.AsFunc(expression);

            // Assert
            Assert.Equal(4, func1(2));
            Assert.Equal(6, func1(3));
            Assert.Same(func1, func2);
        }
    }
}