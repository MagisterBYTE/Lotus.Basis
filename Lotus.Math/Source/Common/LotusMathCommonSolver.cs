namespace Lotus.Maths
{
    /** \addtogroup MathCommon
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы решений уравнений.
    /// </summary>
    public static class XMathSolverEquations
    {
        #region Solver methods
        /// <summary>
        /// Решение квадратного уравнения ax2 + bx + c = 0.
        /// </summary>
        /// <param name="a">Параметр a.</param>
        /// <param name="b">Параметр b.</param>
        /// <param name="c">Параметр c.</param>
        /// <param name="x1">Корень уравнения x1.</param>
        /// <param name="x2">Корень уравнения x2.</param>
        /// <returns>
        /// -1 - Корней нет
        /// 0 - Один корень
        /// 1 - Два корня
        /// </returns>
        public static int SolveQuadraticEquation(double a, double b, double c, out double x1, out double x2)
        {
            // Дискриминант
            var d = (b * b) - (4 * a * c);

            if (d < 0)
            {
                x1 = 0;
                x2 = 0;
                return -1;
            }
            else
            {
                if (XMath.Approximately(d, 0.0))
                {
                    x1 = -b / 2 * a;
                    x2 = x1;
                    return 0;
                }
                else
                {
                    x1 = (-b + d) / 2 * a;
                    x2 = (-b - d) / 2 * a;
                    return 1;
                }
            }
        }
        #endregion
    }
    /**@}*/
}