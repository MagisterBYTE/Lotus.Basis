namespace Lotus.Core
{
    /**
     * \defgroup CoreExtension Методы расширений
     * \ingroup Core
     * \brief Общие методы расширения для базовых типов платформы NET.
     * \details В подсистеме приведены только общие методы расширения базовых типов платформы NET направленных на 
        унификацию функциональных возможностей. Специфичные методы расширения будут реализованы в соответствующих 
        подсистемах.
     * @{
     */
    /// <summary>
    /// Статический класс реализующий методы расширения с обобщенными типами.
    /// </summary>
    public static class XObjectExtension
    {
        /// <summary>
        /// Безопасная проверка на существование объекта.
        /// </summary>
        /// <remarks>
        /// Применяется в подсистеме Unity.
        /// </remarks>
        /// <typeparam name="TType">Тип объекта.</typeparam>
        /// <param name="this">Объект.</param>
        /// <returns>Статус отсутствия объекта.</returns>
        public static bool IsObjectNull<TType>(this TType @this) where TType : class
        {
            return @this is null || @this.Equals(null);
        }
    }
    /**@}*/
}