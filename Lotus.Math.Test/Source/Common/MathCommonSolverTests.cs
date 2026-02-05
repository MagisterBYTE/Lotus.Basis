using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathCommonSolverTests
    {
        #region SolveQuadraticEquation Tests
        [Test]
        public void SolveQuadraticEquation_WithNoRoots_ReturnsMinusOne()
        {
            // Arrange
            var a = 1.0;
            var b = 1.0;
            var c = 1.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(-1, result);
            ClassicAssert.AreEqual(0.0, x1);
            ClassicAssert.AreEqual(0.0, x2);
        }

        [Test]
        public void SolveQuadraticEquation_WithOneRoot_ReturnsZero()
        {
            // Arrange
            // x^2 - 2x + 1 = 0 -> (x-1)^2 = 0 -> x = 1
            var a = 1.0;
            var b = -2.0;
            var c = 1.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(0, result);
            ClassicAssert.AreEqual(1.0, x1, 0.001);
            ClassicAssert.AreEqual(1.0, x2, 0.001);
        }

        [Test]
        public void SolveQuadraticEquation_WithTwoRoots_ReturnsOne()
        {
            // Arrange
            // x^2 - 5x + 6 = 0 -> (x-2)(x-3) = 0 -> x = 2 or x = 3
            var a = 1.0;
            var b = -5.0;
            var c = 6.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(1, result);
            // Корни могут быть в любом порядке
            var root1 = x1;
            var root2 = x2;
            ClassicAssert.IsTrue((XMath.Approximately(root1, 2.0) && XMath.Approximately(root2, 3.0)) ||
                                (XMath.Approximately(root1, 3.0) && XMath.Approximately(root2, 2.0)));
        }

        [Test]
        public void SolveQuadraticEquation_WithSimpleEquation_ReturnsCorrectRoots()
        {
            // Arrange
            // x^2 - 4 = 0 -> x^2 = 4 -> x = 2 or x = -2
            var a = 1.0;
            var b = 0.0;
            var c = -4.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(1, result);
            var root1 = x1;
            var root2 = x2;
            ClassicAssert.IsTrue((XMath.Approximately(root1, 2.0) && XMath.Approximately(root2, -2.0)) ||
                                (XMath.Approximately(root1, -2.0) && XMath.Approximately(root2, 2.0)));
        }

        [Test]
        public void SolveQuadraticEquation_WithNegativeDiscriminant_ReturnsMinusOne()
        {
            // Arrange
            // x^2 + x + 1 = 0 -> дискриминант = 1 - 4 = -3 < 0
            var a = 1.0;
            var b = 1.0;
            var c = 1.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(-1, result);
        }

        [Test]
        public void SolveQuadraticEquation_WithZeroDiscriminant_ReturnsZero()
        {
            // Arrange
            // x^2 - 4x + 4 = 0 -> (x-2)^2 = 0 -> дискриминант = 0
            var a = 1.0;
            var b = -4.0;
            var c = 4.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(0, result);
            ClassicAssert.AreEqual(2.0, x1, 0.001);
            ClassicAssert.AreEqual(2.0, x2, 0.001);
        }

        [Test]
        public void SolveQuadraticEquation_WithNonUnitCoefficient_ReturnsCorrectRoots()
        {
            // Arrange
            // 2x^2 - 7x + 3 = 0 -> дискриминант = 49 - 24 = 25
            // x = (7 ± 5) / 4 -> x = 3 or x = 0.5
            var a = 2.0;
            var b = -7.0;
            var c = 3.0;

            // Act
            var result = XMathSolverEquations.SolveQuadraticEquation(a, b, c, out var x1, out var x2);

            // Assert
            ClassicAssert.AreEqual(1, result);
            var root1 = x1;
            var root2 = x2;
            ClassicAssert.IsTrue((XMath.Approximately(root1, 3.0) && XMath.Approximately(root2, 0.5)) ||
                                (XMath.Approximately(root1, 0.5) && XMath.Approximately(root2, 3.0)));
        }
        #endregion
    }
}
