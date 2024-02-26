namespace Lotus.Maths
{
    /**
     * \defgroup MathRandom Подсистема генерации значений
     * \ingroup Math
     * \brief Подсистема генерации псевдослучайных значений.
	 * \details Подсистема обеспечивает генерацию псевдослучайных значений по различным алгоритмам.
     * @{
     */
    /// <summary>
    /// Определение общего интерфейса генератора для получения псевдослучайных значений.
    /// </summary>
    public interface ILotusRandom
    {
        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 1].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        float NextSingle();

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        float NextSingle(float max);

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        float NextSingle(float min, float max);

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - 4294967295].
        /// </summary>
        /// <returns>Псевдослучайное число.</returns>
        uint NextInteger();

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [0 - max].
        /// </summary>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        uint NextInteger(uint max);

        /// <summary>
        /// Получиться следующие псевдослучайное число в диапазоне [min - max].
        /// </summary>
        /// <param name="min">Минимальное число.</param>
        /// <param name="max">Максимальное число.</param>
        /// <returns>Псевдослучайное число.</returns>
        uint NextInteger(uint min, uint max);
    }
    /**@}*/
}