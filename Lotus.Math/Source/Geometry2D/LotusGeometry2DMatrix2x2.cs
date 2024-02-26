using System;
using System.Runtime.InteropServices;

namespace Lotus.Maths
{
    /** \addtogroup MathGeometry2D
	*@{*/
    /// <summary>
    /// Двухмерная матрица размерностью 2х2.
    /// </summary>
    /// <remarks>
    /// Реализация двухмерной матрицы размерностью 2х2 для реализации базовых трансформаций в 2D плоскости.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix2Dx2
    {
        #region Static methods
        /// <summary>
        /// Вычисление определителя двухмерной матрицы. Расположение компонентов:.
        /// | a1, a2 |
        /// | b1, b2 |.
        /// </summary>
        /// <param name="a1">Компонент a1.</param>
        /// <param name="a2">Компонент a2.</param>
        /// <param name="b1">Компонент b1.</param>
        /// <param name="b2">Компонент b2.</param>
        /// <returns>Определитель двухмерной матрицы.</returns>
        public static double Determinat(double a1, double a2, double b1, double b2)
        {
            return (a1 * b2) - (b1 * a2);
        }
        #endregion

        #region Fields
        /// <summary>
        /// Компонент с позицией 11.
        /// </summary>
        public double M11;

        /// <summary>
        /// Компонент с позицией 12.
        /// </summary>
        public double M12;

        /// <summary>
        /// Компонент с позицией 21.
        /// </summary>
        public double M21;

        /// <summary>
        /// Компонент с позицией 22.
        /// </summary>
        public double M22;
        #endregion
    }

    /// <summary>
    /// Двухмерная матрица размерностью 2х2.
    /// </summary>
    /// <remarks>
    /// Реализация двухмерной матрицы размерностью 2х2 для реализации базовых трансформаций в 2D плоскости.
    /// </remarks>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix2Dx2f
    {
        #region Static methods
        /// <summary>
        /// Вычисление определителя двухмерной матрицы. Расположение компонентов:.
        /// | a1, a2 |
        /// | b1, b2 |.
        /// </summary>
        /// <param name="a1">Компонент a1.</param>
        /// <param name="a2">Компонент a2.</param>
        /// <param name="b1">Компонент b1.</param>
        /// <param name="b2">Компонент b2.</param>
        /// <returns>Определитель двухмерной матрицы.</returns>
        public static float Determinat(float a1, float a2, float b1, float b2)
        {
            return (a1 * b2) - (b1 * a2);
        }
        #endregion

        #region Fields
        /// <summary>
        /// Компонент с позицией 11.
        /// </summary>
        public float M11;

        /// <summary>
        /// Компонент с позицией 12.
        /// </summary>
        public float M12;

        /// <summary>
        /// Компонент с позицией 21.
        /// </summary>
        public float M21;

        /// <summary>
        /// Компонент с позицией 22.
        /// </summary>
        public float M22;
        #endregion
    }
    /**@}*/
}