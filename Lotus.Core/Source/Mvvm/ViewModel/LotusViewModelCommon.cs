namespace Lotus.Core
{
    /**
     * \defgroup CoreViewModel Подсистема ViewModel
     * \ingroup Core
     * \brief Подсистема ViewModel является мощной, гибкой, промежуточной подсистемой между реальными
		данными(слой логики - модели данных) и элементами пользовательского интерфейса которые эти данные отображают.
     * @{
     */
    /// <summary>
    /// Определение интерфейса для объекта(модели) который может быть выбран.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Обычно логика пользовательского интерфейса должно быть отделена от основной логики приложения, 
    /// однако иногда бывает необходимость определять некоторые типовые действия в элементах пользовательского 
    /// интерфейса и правильно на них реагировать.
    /// </para>
    /// <para>
    /// Благодаря реализации данному интерфейсу объект(модель) может реагировать когда он подлежит выбору, а также корректно
    /// реагировать на этот выбор.
    /// </para>
    /// </remarks>
    public interface ILotusModelSelected
    {
        /// <summary>
        /// Установка статуса выбора объекта(модели).
        /// </summary>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <param name="selected">Статус выбора объекта.</param>
        void SetModelSelected(ILotusViewModel viewModel, bool selected);

        /// <summary>
        /// Возможность выбора объекта(модели).
        /// </summary>
        /// <remarks>
        /// Имеется виду возможность выбора объекта(модели) в данный момент.
        /// </remarks>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <returns>Возможность выбора.</returns>
        bool CanModelSelected(ILotusViewModel viewModel);
    }

    /// <summary>
    /// Определение интерфейса для объекта(модели) который может быть недоступен.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Обычно логика пользовательского интерфейса должно быть отделена от основной логики приложения, 
    /// однако иногда бывает необходимость определять некоторые типовые действия в элементах пользовательского 
    /// интерфейса и правильно на них реагировать.
    /// </para>
    /// <para>
    /// Благодаря реализации данному интерфейсу объект(модель) может реагировать когда он недоступен, а также корректно
    /// реагировать на этот статус.
    /// </para>
    /// </remarks>
    public interface ILotusModelEnabled
    {
        /// <summary>
        /// Установка статуса недоступности объекта(модели).
        /// </summary>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <param name="enabled">Статус недоступности.</param>
        void SetModelEnabled(ILotusViewModel viewModel, bool enabled);
    }

    /// <summary>
    /// Определение интерфейса для объекта(модели) который может быть представлен.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Обычно логика пользовательского интерфейса должно быть отделена от основной логики приложения, 
    /// однако иногда бывает необходимость определять некоторые типовые действия в элементах пользовательского 
    /// интерфейса и правильно на них реагировать.
    /// </para>
    /// <para>
    /// Благодаря реализации данному интерфейсу объект(модели) может реагировать когда он представлен, а также корректно
    /// реагировать на этот статус.
    /// </para>
    /// </remarks>
    public interface ILotusModelPresented
    {
        /// <summary>
        /// Установка статуса представления объекта(модели).
        /// </summary>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <param name="presented">Статус представления объекта.</param>
        void SetModelPresented(ILotusViewModel viewModel, bool presented);
    }

    /// <summary>
    /// Определение интерфейса для объекта(модели) который может быть "раскрыт".
    /// </summary>
    /// <remarks>
    /// <para>
    /// Применяется в основном для иерархические отношений, в целях оптимизации получения данных.
    /// </para>
    /// </remarks>
    public interface ILotusModelExpanded
    {
        /// <summary>
        /// Установка статуса раскрытия объекта.
        /// </summary>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <param name="expanded">Статус раскрытия объекта.</param>
        void SetModelExpanded(ILotusViewModelHierarchy viewModel, bool expanded);
    }

    /// <summary>
    /// Определение интерфейса для хранения ссылки на элемент ViewModel.
    /// </summary>
    /// <remarks>
    /// Конечно, основная логика не должна зависеть от логики пользовательского интерфейса и ViewModel. Тем не менее, 
    /// иногда, в целях упрощения или оптимизации, необходимо знать ViewModel
    /// </remarks>
    public interface ILotusViewModelOwner
    {
        /// <summary>
        /// Элемент ViewModel.
        /// </summary>
        ILotusViewModel OwnerViewModel { get; set; }
    }

    /// <summary>
    /// Определение интерфейса для посторенния ViewModel согласно модели данных.
    /// </summary>
    /// <remarks>
    /// Реализация данного интерфейса позволяет построить определённым образом или плоскую ViewModel 
    /// или иерархическую ViewModel
    /// </remarks>
    public interface ILotusViewModelBuilder
    {
        #region МЕТОДЫ 
        /// <summary>
        /// Получение количества дочерних узлов.
        /// </summary>
        /// <returns>Количество дочерних узлов.</returns>
        int GetCountChildrenNode();

        /// <summary>
        /// Получение дочернего узла по индексу.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Дочерней узел.</returns>
        object GetChildrenNode(int index);
        #endregion
    }
    /**@}*/
}