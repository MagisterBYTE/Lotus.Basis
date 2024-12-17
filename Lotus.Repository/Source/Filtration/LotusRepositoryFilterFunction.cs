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
        Equals = 0,

        /// <summary>
        /// Не равно аргументу.
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// Меньше аргумента.
        /// </summary>
        LessThan = 2,

        /// <summary>
        /// Меньше или равно аргумента.
        /// </summary>
        LessThanOrEqual = 3,

        /// <summary>
        /// Больше аргумента.
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// Больше или равно аргумента.
        /// </summary>
        GreaterThanOrEqual = 5,

        /// <summary>
        /// Между первым аргументом (меньшим) и вторым аргументом (большим).
        /// </summary>
        Between = 6,

        /// <summary>
        /// Аргумент(строка) может находиться в любом месте c учетом регистра.
        /// Аргумент(иной) значение должно присутствовать в аргументе массива.
        /// </summary>
        Contains = 7,

        /// <summary>
        /// Аргумент(строка) должна находится в начала c учетом регистра.
        /// </summary>
        StartsWith = 8,

        /// <summary>
        /// Аргумент(строка) должна находится в конце c учетом регистра.
        /// </summary>
        EndsWith = 9,

        /// <summary>
        /// Аргумент(строка) должна сравнивается с учетом оператора Like.
        /// </summary>
        Like = 10,

        /// <summary>
        /// Не равно пустой или NULL строке. Аргумент НЕ требуется.
        /// Не равно значению NULL для иных объектов.
        /// </summary>
        NotEmpty = 11,

        /// <summary>
        /// Равно пустой или NULL строке. Аргумент НЕ требуется.
        /// Равно значению NULL для иных объектов.
        /// </summary>
        Empty = 12,

        /// <summary>
        /// Любой из проверяемых элементов списка должен находиться в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> true
        /// item02 [4,2,3] -> true
        /// item03 [2,3]   -> true
        /// item04 [1,2]   -> true
        /// item05 [4,5]   -> false
        /// </remarks>
        IncludeAny = 13,

        /// <summary>
        /// Все из проверяемых элементов списка должен находиться в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> true
        /// item02 [4,2,3] -> false
        /// item03 [2,3]   -> false
        /// item04 [1,2]   -> true
        /// item05 [4,5]   -> false
        /// </remarks>
        IncludeAll = 14,

        /// <summary>
        /// Проверяемые элементы списка должен быть равны массиву аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> false
        /// item02 [4,2,3] -> false
        /// item03 [2,3]   -> false
        /// item04 [1,2]   -> true
        /// item05 [4,5]   -> false
        /// </remarks>
        IncludeEquals = 15,

        /// <summary>
        /// Ни один из проверяемых элементов списка не должен находится в массиве аргумента.
        /// </summary>
        /// <remarks>
        /// filter [1, 2]
        /// item01 [1,2,3] -> false
        /// item02 [4,2,3] -> false
        /// item03 [2,3]   -> false
        /// item04 [1,2]   -> false
        /// item05 [4,5]   -> true
        /// </remarks>
        IncludeNone = 16,
    }
    /**@}*/
}