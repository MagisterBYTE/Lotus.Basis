using System;
using System.Linq.Expressions;


namespace Lotus.Core.Expressions
{
    public class ExpressionExtensionTests
    {
        [Fact]
        public void AsFunc_ExtensionMethod_ShouldWorkSameAsStaticMethod()
        {
            // Arrange
            Expression<Func<int, int>> expression = x => x + 1;

            // Act
            var func = expression.AsFunc();

            // Assert
            Assert.Equal(2, func(1));
        }

        [Fact]
        public void From_ShouldComposeExpressionsCorrectly()
        {
            // Arrange
            Expression<Func<string, int>> source = s => s.Length;
            Expression<Func<DateTime, string>> mapFrom = d => d.Day.ToString();

            // Act
            var composed = source.From(mapFrom);
            var func = composed.Compile();
            var testDate = new DateTime(2023, 5, 15); // Day = 15

            // Assert
            Assert.Equal(2, func(testDate)); // "15".Length == 2
        }

        [Fact]
        public void GetPropertyExpression_WithSingleProperty_ShouldReturnCorrectExpression()
        {
            // Arrange
            var parameter = Expression.Parameter(typeof(string), "s");

            // Act
            var expr = parameter.GetPropertyExpression("Length");

            // Assert
            Assert.Equal("s.Length", expr.ToString());
        }

        [Fact]
        public void GetPropertyExpression_WithNestedProperties_ShouldReturnCorrectExpression()
        {
            // Arrange
            var parameter = Expression.Parameter(typeof(DateTime), "d");

            // Act
            var expr1 = parameter.GetPropertyExpression("Date.Year");
            var expr2 = parameter.GetPropertyExpression(new[] { "Date", "Year" });

            // Assert
            Assert.Equal("d.Date.Year", expr1.ToString());
            Assert.Equal(expr1.ToString(), expr2.ToString());
        }
    }
}