namespace Lotus.Repository
{
    /**
     * \defgroup RepositoryFilter Подсистема фильтрации
     * \ingroup Repository
     * \brief Подсистема фильтрации определяет однотипный унифицированный набор типов для фильтрации данных.
     * @{
     */
    /// <summary>
    /// Функции для фильтрации данных.
    /// </summary>
    public enum TFilterFunction
    {
        /// <summary>
        /// Равно аргументу.
        /// </summary>
        Equals,

        /// <summary>
        /// Не равно аргументу.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Меньше аргумента.
        /// </summary>
        LessThan,

        /// <summary>
        /// Меньше или равно аргумента.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// Больше аргумента.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Больше или равно аргумента.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Между первым аргументом (меньшим) и вторым аргументом (большим).
        /// </summary>
        Between,

        /// <summary>
        /// Aргумент(строка) может находиться в любом месте c учетом регистра.
        /// </summary>
        Contains,

        /// <summary>
        /// Aргумент(строка) должна находится в начала c учетом регистра.
        /// </summary>
        StartsWith,

        /// <summary>
        /// Aргумент(строка) должна находится в конце c учетом регистра.
        /// </summary>
        EndsWith,

        /// <summary>
        /// Не равно пустой строке. Аргумент пустая строка.
        /// </summary>
        NotEmpty,

        /// <summary>
        /// Любой из проверяемых элементов списка должен находиться в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> true
        /// item01 [4,2,3] -> true
        /// item01 [2,3]   -> true
        /// item01 [1,2]   -> true
        /// item01 [4,5]   -> true
        /// </remarks>
        IncludeAny,

        /// <summary>
        /// Все из проверяемых элементов списка должен находиться в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> true
        /// item01 [4,2,3] -> false
        /// item01 [2,3]   -> false
        /// item01 [1,2]   -> true
        /// item01 [4,5]   -> false
        /// </remarks>
        IncludeAll,

        /// <summary>
        /// Проверяемые элементы списка должен быть равны массиву аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> false
        /// item01 [4,2,3] -> false
        /// item01 [2,3]   -> false
        /// item01 [1,2]   -> true
        /// item01 [4,5]   -> false
        /// </remarks>
        IncludeEquals,

        /// <summary>
        /// Ни один из проверяемых элементов списка не должен находится в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> false
        /// item01 [4,2,3] -> false
        /// item01 [2,3]   -> false
        /// item01 [1,2]   -> false
        /// item01 [4,5]   -> true
        /// </remarks>
        IncludeNone,
    }
    /**@}*/
}