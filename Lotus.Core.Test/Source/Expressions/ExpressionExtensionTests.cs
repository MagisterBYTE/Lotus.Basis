using System;
using System.Linq.Expressions;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Expressions
{
    [TestFixture]
    public class ExpressionExtensionTests
    {
        [Test]
        public void AsFunc_ExtensionMethod_ShouldWorkSameAsStaticMethod()
        {
            // Arrange
            Expression<Func<int, int>> expression = x => x + 1;

            // Act
            var func = expression.AsFunc();

            // Assert
            ClassicAssert.AreEqual(2, func(1));
        }

        [Test]
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
            ClassicAssert.AreEqual(2, func(testDate)); // "15".Length == 2
        }

        [Test]
        public void GetPropertyExpression_WithSingleProperty_ShouldReturnCorrectExpression()
        {
            // Arrange
            var parameter = Expression.Parameter(typeof(string), "s");

            // Act
            var expr = parameter.GetPropertyExpression("Length");

            // Assert
            ClassicAssert.AreEqual("s.Length", expr.ToString());
        }

        [Test]
        public void GetPropertyExpression_WithNestedProperties_ShouldReturnCorrectExpression()
        {
            // Arrange
            var parameter = Expression.Parameter(typeof(DateTime), "d");

            // Act
            var expr1 = parameter.GetPropertyExpression("Date.Year");
            var expr2 = parameter.GetPropertyExpression(new[] { "Date", "Year" });

            // Assert
            ClassicAssert.AreEqual("d.Date.Year", expr1.ToString());
            ClassicAssert.AreEqual(expr1.ToString(), expr2.ToString());
        }
    }
}