using System;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Expressions
{
    /// <summary>
    /// Тесты для критических участков PredicateBuilder (исправление VisitParameter).
    /// </summary>
    [TestFixture]
    public class PredicateBuilderCriticalTests
    {
        /// <summary>
        /// Тест VisitParameter - корректная замена параметров (критический баг был исправлен).
        /// </summary>
        [Test]
        public void VisitParameter_ReplacesParametersCorrectly()
        {
            // Arrange - создаем два выражения с разными параметрами
            Expression<Func<int, bool>> expr1 = x => x > 5;
            Expression<Func<int, bool>> expr2 = y => y < 10;

            // Act - комбинируем их через And
            // Внутри используется VisitParameter для замены параметров
            var combined = expr1.And(expr2);
            var func = combined.Compile();

            // Assert - проверяем что параметры корректно заменены и выражение работает
            ClassicAssert.IsTrue(func(7), "Value 7 should satisfy both conditions (x > 5 and y < 10)");
            ClassicAssert.IsFalse(func(3), "Value 3 should not satisfy x > 5");
            ClassicAssert.IsFalse(func(11), "Value 11 should not satisfy y < 10");
        }

        /// <summary>
        /// Тест VisitParameter - корректная замена параметров в сложных выражениях.
        /// </summary>
        [Test]
        public void VisitParameter_WithComplexExpressions_ReplacesParametersCorrectly()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x * 2 > 10;
            Expression<Func<int, bool>> expr2 = y => y + 5 < 20;

            // Act
            var combined = expr1.And(expr2);
            var func = combined.Compile();

            // Assert
            ClassicAssert.IsTrue(func(8), "Value 8 should satisfy both conditions");
            ClassicAssert.IsFalse(func(3), "Value 3 should not satisfy x * 2 > 10");
            ClassicAssert.IsFalse(func(20), "Value 20 should not satisfy y + 5 < 20");
        }

        /// <summary>
        /// Тест VisitParameter - корректная замена параметров в Or выражении.
        /// </summary>
        [Test]
        public void VisitParameter_WithOrExpression_ReplacesParametersCorrectly()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x < 5;
            Expression<Func<int, bool>> expr2 = y => y > 15;

            // Act
            var combined = expr1.Or(expr2);
            var func = combined.Compile();

            // Assert
            ClassicAssert.IsTrue(func(3), "Value 3 should satisfy x < 5");
            ClassicAssert.IsTrue(func(20), "Value 20 should satisfy y > 15");
            ClassicAssert.IsFalse(func(10), "Value 10 should not satisfy either condition");
        }

        /// <summary>
        /// Тест VisitParameter - корректная обработка когда параметр не найден в map.
        /// </summary>
        [Test]
        public void VisitParameter_WithUnmappedParameter_ReturnsBaseResult()
        {
            // Arrange - создаем выражение с параметром, который не будет в map
            Expression<Func<int, bool>> expr = x => x > 5;

            // Act - применяем Not, который должен корректно обработать параметр
            var negated = expr.Not();
            var func = negated.Compile();

            // Assert - проверяем что отрицание работает корректно
            ClassicAssert.IsTrue(func(3), "Value 3 should satisfy not(x > 5)");
            ClassicAssert.IsFalse(func(7), "Value 7 should not satisfy not(x > 5)");
        }
    }
}
