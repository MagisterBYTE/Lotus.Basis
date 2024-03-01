using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Lotus.Core
{
    /** \addtogroup CoreViewModel
	*@{*/
    /// <summary>
    /// Интерфейс для определения коллекции ViewModel.
    /// </summary>
    public interface ILotusCollectionViewModel : ILotusOwnerObject
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Источник данных.
        /// </summary>
        object Source { get; set; }

        /// <summary>
        /// Список элементов ViewModel.
        /// </summary>
        IList IViewModels { get; }

        /// <summary>
        /// Количество элементов ViewModel.
        /// </summary>
        int CountViewModels { get; }

        //
        // ВЫБРАННЫЙ ЭЛЕМЕНТ
        //
        /// <summary>
        /// Выбранный индекс элемента ViewModel, -1 выбора нет.
        /// </summary>
        /// <remarks>
        /// При множественном выборе индекс последнего выбранного элемента ViewModel.
        /// </remarks>
        int SelectedIndex { get; set; }

        /// <summary>
        /// Предпоследний выбранный индекс элемента ViewModel, -1 выбора нет.
        /// </summary>
        int PrevSelectedIndex { get; }

        /// <summary>
        /// Текущий выбранный элемент ViewModel.
        /// </summary>
        ILotusViewModel? ISelectedViewModel { get; set; }

        /// <summary>
        /// Текущий элемент ViewModel для отображения в отдельном контексте.
        /// </summary>
        ILotusViewModel? IPresentedViewModel { get; set; }

        //
        // ПАРАМЕТРЫ ФИЛЬТРАЦИИ
        //
        /// <summary>
        /// Статус фильтрации коллекции.
        /// </summary>
        bool IsFiltered { get; set; }

        //
        // ПАРАМЕТРЫ СОРТИРОВКИ
        //
        /// <summary>
        /// Статус сортировки коллекции.
        /// </summary>
        bool IsSorted { get; set; }

        /// <summary>
        /// Статус сортировки коллекции по возрастанию.
        /// </summary>
        bool IsAscendingSorted { get; set; }

        //
        // МНОЖЕСТВЕННЫЙ ВЫБОР
        //
        /// <summary>
        /// Возможность выбора нескольких элементов ViewModel.
        /// </summary>
        bool IsMultiSelected { get; set; }

        /// <summary>
        /// Режим выбора нескольких элементов ViewModel (первый раз выделение, второй раз снятие выделения).
        /// </summary>
        bool ModeSelectAddRemove { get; set; }

        /// <summary>
        /// При множественном выборе всегда должен быть выбран хотя бы один элемент.
        /// </summary>
        bool AlwaysSelectedItem { get; set; }

        /// <summary>
        /// Режим включения отмены выделения элемента ViewModel.
        /// </summary>
        /// <remarks>
        /// При его включение будет вызваться метод элемента <see cref="ILotusModelSelected.SetModelSelected(ILotusViewModel, bool)"/>.
        /// Это может не понадобиться если, например, режим визуального реагирования как у кнопки
        /// </remarks>
        bool IsEnabledUnselectingItem { get; set; }
        #endregion

        #region Methods 
        /// <summary>
        /// Создание конкретной ViewModel для указанной модели.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>ViewModel.</returns>
        ILotusViewModel CreateViewModel(object model);

        /// <summary>
        /// Переустановка источника данных.
        /// </summary>
        void ResetSource();

        /// <summary>
        /// Выключение выбора всех элементов ViewModel кроме исключаемого.
        /// </summary>
        /// <param name="exclude">Исключаемый элемент ViewModel.</param>
        void UnsetAllSelected(ILotusViewModel exclude);

        /// <summary>
        /// Выключение презентации всех элементов ViewModel кроме исключаемого.
        /// </summary>
        /// <param name="exclude">Исключаемый элемент ViewModel.</param>
        /// <param name="parameters">Параметры контекста исключения.</param>
        void UnsetAllPresent(ILotusViewModel exclude, CParameters parameters);
        #endregion
    }

    /// <summary>
    /// Шаблон коллекции для элементов ViewModel которая поддерживает концепцию просмотра и управления с полноценной
    /// поддержкой всех операций.
    /// </summary>
    /// <remarks>
    /// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
    /// позволяет выбирать данные и производить над ними операции.
    /// </remarks>
    /// <typeparam name="TViewModel">Тип элемента ViewModel.</typeparam>
    /// <typeparam name="TModel">Тип модели.</typeparam>
    public class CollectionViewModel<TViewModel, TModel> : ListArray<TViewModel>, ILotusNameable, ILotusCollectionViewModel
        where TViewModel : ILotusViewModel
        where TModel : class
    {
        #region Inner types
        /// <summary>
        /// Компара́тор для сортировки элементов ViewModel по возрастанию.
        /// </summary>
        protected sealed class ComparerAscending : IComparer<TViewModel>
        {
            /// <summary>
            /// Глобальный экземпляр.
            /// </summary>
            public static readonly ComparerAscending Instance = new ComparerAscending();

            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Сравнение элементов ViewModel по возрастанию.
            /// </summary>
            /// <param name="x">Первый объект.</param>
            /// <param name="y">Второй объект.</param>
            /// <returns>Статус сравнения.</returns>
            //-----------------------------------------------------------------------------------------------------
            public int Compare(TViewModel? x, TViewModel? y)
            {
                return ComprareOfAscending(x, y);
            }
        }

        /// <summary>
        /// Компара́тор для сортировки элементов ViewModel по убыванию.
        /// </summary>
        protected sealed class ComparerDescending : IComparer<TViewModel>
        {
            /// <summary>
            /// Глобальный экземпляр.
            /// </summary>
            public static readonly ComparerDescending Instance = new ComparerDescending();

            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Сравнение элементов ViewModel по убыванию.
            /// </summary>
            /// <param name="x">Первый объект.</param>
            /// <param name="y">Второй объект.</param>
            /// <returns>Статус сравнения.</returns>
            //-----------------------------------------------------------------------------------------------------
            public int Compare(TViewModel? x, TViewModel? y)
            {
                var result = ComprareOfAscending(x, y);
                if (result == 1)
                {
                    return -1;
                }
                else
                {
                    if (result == -1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        #endregion

        #region Const
        /// <summary>
        /// Статус реализации типом интерейса <see cref="ILotusNameable"/>.
        /// </summary>
        public static readonly bool IsSupportNameable = typeof(TModel).IsSupportInterface<ILotusNameable>();

        /// <summary>
        /// Статус реализации типом интерейса <see cref="ILotusIdentifierInt"/>.
        /// </summary>
        public static readonly bool IsSupportIdentifierInt = typeof(TModel).IsSupportInterface<ILotusIdentifierInt>();

        /// <summary>
        /// Статус реализации типом интерейса <see cref="ILotusModelSelected"/>.
        /// </summary>
        public static readonly bool IsSupportModelSelected = typeof(TModel).IsSupportInterface<ILotusModelSelected>();

        /// <summary>
        /// Статус реализации типом интерейса <see cref="ILotusModelEnabled"/>.
        /// </summary>
        public static readonly bool IsSupportModelEnabled = typeof(TModel).IsSupportInterface<ILotusModelEnabled>();
        #endregion

        #region Static fields
        //
        // Константы для информирования об изменении свойств
        //
        // Идентификация
        protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
        protected static readonly PropertyChangedEventArgs PropertyArgsSelectedViewModel = new PropertyChangedEventArgs(nameof(SelectedViewModel));
        protected static readonly PropertyChangedEventArgs PropertyArgsPresentedViewModel = new PropertyChangedEventArgs(nameof(PresentedViewModel));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsFiltered = new PropertyChangedEventArgs(nameof(IsFiltered));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsSorted = new PropertyChangedEventArgs(nameof(IsSorted));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsAscendingSorted = new PropertyChangedEventArgs(nameof(IsAscendingSorted));

        // Множественный выбор
        protected static readonly PropertyChangedEventArgs PropertyArgsIsMultiSelected = new PropertyChangedEventArgs(nameof(IsMultiSelected));
        protected static readonly PropertyChangedEventArgs PropertyArgsModeSelectAddRemove = new PropertyChangedEventArgs(nameof(ModeSelectAddRemove));
        protected static readonly PropertyChangedEventArgs PropertyArgsAlwaysSelectedItem = new PropertyChangedEventArgs(nameof(AlwaysSelectedItem));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsEnabledUnselectingItem = new PropertyChangedEventArgs(nameof(IsEnabledUnselectingItem));

        #endregion

        #region Static methods
        /// <summary>
        /// Сравнение элементов ViewModel по возрастанию.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус сравнения.</returns>
        public static int ComprareOfAscending(TViewModel? left, TViewModel? right)
        {
            if (left == null)
            {
                if (right == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (right == null)
                {
                    return -1;
                }
                else
                {
                    if (left.Model != null && right.Model != null)
                    {
                        if (left.Model is IComparable<TModel> left_comparable)
                        {
                            return left_comparable.CompareTo((TModel)right.Model);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        if (left is IComparable left_comparable)
                        {
                            return left_comparable.CompareTo(right);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
        #endregion

        #region Fields
        // Основные параметры
        protected internal string _name = "";
        protected internal object _source;

        // Выбранный элемент
        protected internal int _selectedIndex = -1;
        protected internal int _prevSelectedIndex = -1;
        protected internal TViewModel? _selectedViewModel;
        protected internal TViewModel? _presentedViewModel;

        // Параметры фильтрации
        protected internal bool _isFiltered;
        protected internal Predicate<TModel?> _filter;

        // Параметры сортировки
        protected internal bool _isSorted;
        protected internal bool _isAscendingSorted;

        // Множественный выбор
        protected internal bool _isMultiSelected;
        protected internal bool _modeSelectAddRemove;
        protected internal bool _alwaysSelectedItem;
        protected internal bool _isEnabledUnselectingItem;
        protected internal ListArray<TModel> selectedItems;

        // События
        protected internal Action _onCurrentItemChanged;
        protected internal Action<int> _onSelectedIndexChanged;
        protected internal Action<int> _onSelectionAddItem;
        protected internal Action<int> _onSelectionRemovedItem;
        protected internal Action<TViewModel> _onSelectedItem;
        protected internal Action<TViewModel> _onActivatedItem;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Наименование коллекции.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(PropertyArgsName);
                RaiseNameChanged();
            }
        }

        /// <summary>
        /// Источник данных.
        /// </summary>
        public object Source
        {
            get { return _source; }
            set
            {
                if (_source is INotifyCollectionChanged collection_changed)
                {
                    collection_changed.CollectionChanged -= OnCollectionChangedHandler;
                }

                _source = value;
                ResetSource();
            }
        }

        /// <summary>
        /// Список элементов ViewModel.
        /// </summary>
        public IList IViewModels
        {
            get { return this; }
        }

        /// <summary>
        /// Количество элементов ViewModel.
        /// </summary>
        public int CountViewModels
        {
            get { return _count; }
        }

        //
        // ВЫБРАННЫЙ ЭЛЕМЕНТ
        //
        /// <summary>
        /// Выбранный индекс элемента ViewModel, -1 выбора нет.
        /// </summary>
        /// <remarks>
        /// При множественном выборе индекс последнего выбранного элемента ViewModel.
        /// </remarks>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                this.SetSelectedItem(value);
            }
        }

        /// <summary>
        /// Предпоследний выбранный индекс элемента ViewModel, -1 выбора нет.
        /// </summary>
        public int PrevSelectedIndex
        {
            get { return _prevSelectedIndex; }
        }

        /// <summary>
        /// Текущий выбранный элемент ViewModel.
        /// </summary>
        public ILotusViewModel? ISelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                SelectedViewModel = value == null ? default : (TViewModel)value;
            }
        }

        /// <summary>
        /// Текущий выбранный элемент ViewModel.
        /// </summary>
        public TViewModel? SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                if (ReferenceEquals(_selectedViewModel, value) == false)
                {
                    _selectedViewModel = value;
                    NotifyPropertyChanged(PropertyArgsSelectedViewModel);
                }
            }
        }

        /// <summary>
        /// Текущий элемент ViewModel для отображения в отдельном контексте.
        /// </summary>
        public ILotusViewModel? IPresentedViewModel
        {
            get { return _presentedViewModel; }
            set
            {
                PresentedViewModel = value == null ? default : (TViewModel)value;
            }
        }

        /// <summary>
        /// Текущий элемент ViewModel для отображения в отдельном контексте.
        /// </summary>
        public TViewModel? PresentedViewModel
        {
            get
            {
                return _presentedViewModel;
            }
            set
            {
                if (ReferenceEquals(_presentedViewModel, value) == false)
                {
                    _presentedViewModel = value;
                    NotifyPropertyChanged(PropertyArgsPresentedViewModel);
                }
            }
        }

        //
        // ПАРАМЕТРЫ ФИЛЬТРАЦИИ
        //
        /// <summary>
        /// Статус фильтрации коллекции.
        /// </summary>
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set
            {
                _isFiltered = value;
                NotifyPropertyChanged(PropertyArgsIsFiltered);
                RaiseIsFilteredChanged();
            }
        }

        /// <summary>
        /// Функтор осуществляющий фильтрацию данных.
        /// </summary>
        public Predicate<TModel?> Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter == null || _filter != value)
                {
                    _filter = value;
                    if (_filter != null)
                    {
                        _isFiltered = true;
                        NotifyPropertyChanged(PropertyArgsIsFiltered);
                        ResetSource();
                    }
                    else
                    {
                        _isFiltered = false;
                        NotifyPropertyChanged(PropertyArgsIsFiltered);
                        ResetSource();
                    }
                }
            }
        }

        //
        // ПАРАМЕТРЫ СОРТИРОВКИ
        //
        /// <summary>
        /// Статус сортировки коллекции.
        /// </summary>
        public bool IsSorted
        {
            get { return _isSorted; }
            set
            {
                _isSorted = value;
                NotifyPropertyChanged(PropertyArgsIsSorted);
            }
        }

        /// <summary>
        /// Статус сортировки коллекции по возрастанию.
        /// </summary>
        public bool IsAscendingSorted
        {
            get { return _isAscendingSorted; }
            set
            {
                _isAscendingSorted = value;
                NotifyPropertyChanged(PropertyArgsIsAscendingSorted);
            }
        }

        //
        // МНОЖЕСТВЕННЫЙ ВЫБОР
        //
        /// <summary>
        /// Возможность выбора нескольких элементов ViewModel.
        /// </summary>
        public bool IsMultiSelected
        {
            get { return _isMultiSelected; }
            set
            {
                _isMultiSelected = value;
                NotifyPropertyChanged(PropertyArgsIsMultiSelected);
            }
        }

        /// <summary>
        /// Режим выбора нескольких элементов ViewModel (первый раз выделение, второй раз снятие выделения).
        /// </summary>
        public bool ModeSelectAddRemove
        {
            get { return _modeSelectAddRemove; }
            set
            {
                _modeSelectAddRemove = value;
                NotifyPropertyChanged(PropertyArgsModeSelectAddRemove);
            }
        }

        /// <summary>
        /// При множественном выборе всегда должен быть выбран хотя бы один элемент.
        /// </summary>
        public bool AlwaysSelectedItem
        {
            get { return _alwaysSelectedItem; }
            set
            {
                _alwaysSelectedItem = value;
                NotifyPropertyChanged(PropertyArgsAlwaysSelectedItem);
            }
        }

        /// <summary>
        /// Режим включения отмены выделения элемента.
        /// </summary>
        /// <remarks>
        /// При его включение будет вызваться метод элемента <see cref="ILotusModelSelected.SetModelSelected(ILotusViewModel, bool)"/>.
        /// Это может не понадобиться если, например, режим визуального реагирования как у кнопки
        /// </remarks>
        public bool IsEnabledUnselectingItem
        {
            get { return _isEnabledUnselectingItem; }
            set
            {
                _isEnabledUnselectingItem = value;
                NotifyPropertyChanged(PropertyArgsIsEnabledUnselectingItem);
            }
        }

        //
        // СОБЫТИЯ
        //
        /// <summary>
        /// Событие для нотификации об изменение текущего выбранного элемента.
        /// </summary>
        public Action OnCurrentItemChanged
        {
            get { return _onCurrentItemChanged; }
            set { _onCurrentItemChanged = value; }
        }

        /// <summary>
        /// Событие для нотификации об изменение индекса выбранного элемента. Аргумент - индекс выбранного элемента.
        /// </summary>
        public Action<int> OnSelectedIndexChanged
        {
            get { return _onSelectedIndexChanged; }
            set { _onSelectedIndexChanged = value; }
        }

        /// <summary>
        /// Событие для нотификации о добавлении элемента к списку выделенных(после добавления). Аргумент - индекс (позиция) добавляемого элемента.
        /// </summary>
        public Action<int> OnSelectionAddItem
        {
            get { return _onSelectionAddItem; }
            set { _onSelectionAddItem = value; }
        }

        /// <summary>
        /// Событие для нотификации о удалении элемента из списка выделенных(после удаления). Аргумент - индекс (позиция) удаляемого элемента.
        /// </summary>
        public Action<int> OnSelectionRemovedItem
        {
            get { return _onSelectionRemovedItem; }
            set { _onSelectionRemovedItem = value; }
        }

        /// <summary>
        /// Событие для нотификации о выборе элемента.
        /// </summary>
        /// <remarks>
        /// В основном применяется(должно применяется) для служебных целей.
        /// </remarks>
        public Action<TViewModel> OnSelectedItem
        {
            get { return _onSelectedItem; }
            set { _onSelectedItem = value; }
        }

        /// <summary>
        /// Событие для нотификации о активации элемента.
        /// </summary>
        /// <remarks>
        /// В основном применяется(должно применяется) для служебных целей.
        /// </remarks>
        public Action<TViewModel> OnActivatedItem
        {
            get { return _onActivatedItem; }
            set { _onActivatedItem = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CollectionViewModel()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        public CollectionViewModel(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        /// <param name="source">Источник данных.</param>
        public CollectionViewModel(string name, object source)
        {
            _name = name;
            _source = source;
            ResetSource();
        }
        #endregion

        #region System methods
        /// <summary>
        /// Получение копии объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public virtual object Clone()
        {
            var clone = new CollectionViewModel<TViewModel, TModel>();
            clone.Name = _name;

            for (var i = 0; i < _count; i++)
            {
                clone.Add(_arrayOfItems.Clone());
            }

            return clone;
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Имя коллекции.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion

        #region IDisposable methods
        /// <summary>
        /// Освобождение управляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение управляемых ресурсов.
        /// </summary>
        /// <param name="disposing">Статус освобождения.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Освобождаем только управляемые ресурсы
            if (disposing)
            {
                if (_source is INotifyCollectionChanged collection_changed)
                {
                    collection_changed.CollectionChanged -= OnCollectionChangedHandler;
                }
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение имени коллекции.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseNameChanged()
        {
        }

        /// <summary>
        /// Изменение статуса сортировки коллекции.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsFilteredChanged()
        {
            if (_isFiltered && _filter != null)
            {
                ResetSource();
            }
        }
        #endregion

        #region ILotusCollectionViewModel methods
        /// <summary>
        /// Создание конкретной ViewModel для указанной модели.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>ViewModel.</returns>
        public virtual ILotusViewModel CreateViewModel(object model)
        {
            throw new NotImplementedException("CreateViewModel must be implemented");
        }

        /// <summary>
        /// Выключение выбора всех элементов ViewModel кроме исключаемого.
        /// </summary>
        /// <param name="exclude">Исключаемый элемент ViewModel.</param>
        public virtual void UnsetAllSelected(ILotusViewModel exclude)
        {
            if (exclude != null)
            {
                for (var i = 0; i < _count; i++)
                {
                    if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
                    {
                        _arrayOfItems[i]!.IsSelected = false;
                    }
                }

                SelectedViewModel = (TViewModel)exclude;
            }
            else
            {
                for (var i = 0; i < _count; i++)
                {
                    _arrayOfItems[i]!.IsSelected = false;
                }

                SelectedViewModel = default;
            }
        }

        /// <summary>
        /// Выключение презентации сех элементов ViewModel кроме исключаемого.
        /// </summary>
        /// <param name="exclude">Исключаемый элемент ViewModel.</param>
        /// <param name="parameters">Параметры контекста исключения.</param>
        public virtual void UnsetAllPresent(ILotusViewModel exclude, CParameters parameters)
        {
            if (exclude != null)
            {
                if (parameters == null)
                {
                    for (var i = 0; i < _count; i++)
                    {
                        if (Object.ReferenceEquals(_arrayOfItems[i], exclude) == false)
                        {
                            _arrayOfItems[i]!.IsPresented = false;
                        }
                    }

                    PresentedViewModel = (TViewModel)exclude;
                }
                else
                {
                    var present_type = parameters.GetValueOfType<Type>();
                    if (present_type != null)
                    {

                    }
                }
            }
            else
            {
                if (parameters == null)
                {
                    // Выключаем все элемента ViewModel
                    for (var i = 0; i < _count; i++)
                    {
                        _arrayOfItems[i]!.IsPresented = false;
                    }

                    PresentedViewModel = default;
                }
                else
                {
                    var present_type = parameters.GetValueOfType<Type>();
                    if (present_type != null)
                    {

                    }
                }
            }
        }
        #endregion

        #region ILotusOwnerObject methods
        /// <summary>
        /// Присоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="add">Статус добавления в коллекцию.</param>
        public virtual void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add)
        {
            // Присоединять можем только объекты
            if (ownedObject is ILotusViewModel view_model)
            {
                // Если владелец есть
                if (ownedObject.IOwner != null)
                {
                    // И он не равен текущему
                    if (ownedObject.IOwner != this)
                    {
                        // Отсоединяем
                        ownedObject.IOwner.DetachOwnedObject(ownedObject, add);
                    }
                }

                if (add)
                {
                    view_model.IOwner = this;
                    Add(view_model);
                }
            }
        }

        /// <summary>
        /// Отсоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="remove">Статус удаления из коллекции.</param>
        public virtual void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove)
        {
            // Отсоединять можем только объекты
            if (ownedObject is ILotusViewModel view_model)
            {
                ownedObject.IOwner = null;

                if (remove)
                {
                    // Ищем его
                    var index = IndexOf(view_model);
                    if (index != -1)
                    {
                        // Удаляем
                        RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление связей для зависимых объектов.
        /// </summary>
        public virtual void UpdateOwnedObjects()
        {
            for (var i = 0; i < _count; i++)
            {
                _arrayOfItems[i]!.IOwner = this;
            }
        }

        /// <summary>
        /// Информирование данного объекта о начале изменения данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект данные которого будут меняться.</param>
        /// <param name="dataName">Имя данных.</param>
        /// <returns>Статус разрешения/согласования изменения данных.</returns>
        public virtual bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object? data, string dataName)
        {
            return true;
        }

        /// <summary>
        /// Информирование данного объекта об окончании изменении данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого изменились.</param>
        /// <param name="dataName">Имя данных.</param>
        public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, object? data, string dataName)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Инициализация коллекции посредством указанного обобщённого списка.
        /// </summary>
        /// <param name="list">Обобщенный список.</param>
        protected virtual void CreateFromList(IList list)
        {
            if (_count > 0)
            {
                Clear();
            }

            // Устанавливаем параметры
            _isReadOnly = list.IsReadOnly;
            _isFixedSize = list.IsFixedSize;

            // Смотрим статус фильта
            if (_filter != null && _isFiltered == true)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    // Если тип поддерживается
                    if (list[i] is TModel model)
                    {
                        // И проходит условия фильтрации
                        if (_filter(model))
                        {
                            var view_model = CreateViewModel(model);
                            view_model.IOwner = this;

                            if (list[i] is ILotusViewModelOwner view_item_owner)
                            {
                                view_item_owner.OwnerViewModel = view_model;
                            }

                            // Добавляем
                            Add(view_model);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < list.Count; i++)
                {
                    // Если тип поддерживается
                    if (list[i] is TModel model)
                    {
                        var view_model = CreateViewModel(model);
                        view_model.IOwner = this;

                        if (list[i] is ILotusViewModelOwner view_item_owner)
                        {
                            view_item_owner.OwnerViewModel = view_model;
                        }

                        // Добавляем
                        Add(view_model);
                    }
                }
            }
        }

        /// <summary>
        /// Переустановка источника данных.
        /// </summary>
        public virtual void ResetSource()
        {
            if (_source == null)
            {
                if (_count > 0)
                {
                    Clear();
                }

                _isReadOnly = false;
                _isFixedSize = false;
            }

            if (_source is IList list)
            {
                CreateFromList(list);
            }

            if (_source is INotifyCollectionChanged collection_changed)
            {
                collection_changed.CollectionChanged += OnCollectionChangedHandler;
            }
        }

        /// <summary>
        /// Сортировка списка по возрастанию.
        /// </summary>
        public override void SortAscending()
        {
            this.Sort(ComparerAscending.Instance);
        }

        /// <summary>
        /// Сортировка списка по убыванию.
        /// </summary>
        public override void SortDescending()
        {
            this.Sort(ComparerDescending.Instance);
        }

        /// <summary>
        /// Сброс выделения списка.
        /// </summary>
        public void Unselect()
        {
            if (_selectedIndex != -1 && _selectedIndex < Count)
            {
                if (_isMultiSelected == false && _isEnabledUnselectingItem)
                {
                    if (IsSupportModelSelected)
                    {
                        if (_arrayOfItems[_selectedIndex]?.Model is ILotusModelSelected selected_item)
                        {
                            selected_item.SetModelSelected(_arrayOfItems[_selectedIndex]!, false);
                        }
                    }
                }

                _prevSelectedIndex = _selectedIndex;
                _selectedIndex = -1;

                // Информируем о смене выбранного элемента
                if (_onSelectedIndexChanged != null) _onSelectedIndexChanged(_selectedIndex);
                if (_onCurrentItemChanged != null) _onCurrentItemChanged();

                if (_isMultiSelected && _isEnabledUnselectingItem)
                {
                    for (var i = 0; i < selectedItems.Count; i++)
                    {
                    }

                    selectedItems.Clear();
                }
            }
        }
        #endregion

        #region List methods
        /// <summary>
        /// Активация элемента списка.
        /// </summary>
        /// <param name="item">Элемент списка.</param>
        internal void ActivatedItemDirect(ILotusModelSelected item)
        {
            for (var i = 0; i < Count; i++)
            {
                // 1) Смотрим на совпадение
                if (_arrayOfItems[i]!.Model.Equals(item))
                {
                    SetSelectedItem(i);
                    if (_onActivatedItem != null)
                    {
                        _onActivatedItem(_arrayOfItems[i]!);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Установка выбранного элемента.
        /// </summary>
        /// <param name="index">Индекс выбранного элемента.</param>
        internal void SetSelectedItem(int index)
        {
            if (index > -1 && index < Count)
            {
                // Выключенный элемент выбрать нельзя
                if (IsSupportModelSelected)
                {
                    var selected_item = _arrayOfItems[index]?.Model as ILotusModelSelected;
                    if (selected_item != null && selected_item.CanModelSelected(_arrayOfItems[index]!) == false)
                    {
                        return;
                    }
                }

                var old_index = _selectedIndex;

                // Если выбран другой элемент
                if (old_index != index)
                {
                    // Сохраняем старый выбор
                    _prevSelectedIndex = _selectedIndex;
                    _selectedIndex = index;

                    // Если нет режима мульти выбора
                    if (!_isMultiSelected)
                    {
                        // Обновляем статус
                        if (IsSupportModelSelected)
                        {
                            if (_arrayOfItems[_selectedIndex]?.Model is ILotusModelSelected selected_item)
                            {
                                selected_item.SetModelSelected(_arrayOfItems[_selectedIndex]!, true);
                            }
                        }

                        // Если предыдущий элемент был выбран, то снимаем выбор
                        if (_prevSelectedIndex != -1 && _prevSelectedIndex < Count)
                        {
                            // Если нет мульти выбора
                            if (IsSupportModelSelected && _isEnabledUnselectingItem)
                            {
                                if (_arrayOfItems[_prevSelectedIndex]?.Model is ILotusModelSelected prev_selected_item)
                                {
                                    prev_selected_item.SetModelSelected(_arrayOfItems[_prevSelectedIndex]!, false);
                                }
                            }
                        }
                    }

                    // Информируем о смене выбранного элемента
                    if (_onSelectedIndexChanged != null) _onSelectedIndexChanged(_selectedIndex);
                    if (_onCurrentItemChanged != null) _onCurrentItemChanged();
                }

                // Пользователь выбрал тот же элемент  - Только если включен мультирежим 
                if (_isMultiSelected)
                {
                    // Смотрим, есть ли у нас элемент в выделенных
                    if (selectedItems.Contains(_arrayOfItems[index]!.Model))
                    {
                        // Есть
                        // Режим снятие/выделения
                        if (_modeSelectAddRemove)
                        {
                            // Только если мы можем оставлять элементе на невыбранными
                            if (_alwaysSelectedItem == false ||
                               (_alwaysSelectedItem && selectedItems.Count > 1))
                            {
                                // Убираем выделение
                                if (IsSupportModelSelected && _isEnabledUnselectingItem)
                                {
                                    if (_arrayOfItems[index]?.Model is ILotusModelSelected selected_item)
                                    {
                                        selected_item.SetModelSelected(_arrayOfItems[index]!, false);
                                    }
                                }

                                // Удаляем
                                selectedItems.Remove(_arrayOfItems[index]!.Model);

                                // Информируем - вызываем событие
                                if (_onSelectionRemovedItem != null) _onSelectionRemovedItem(index);
                            }
                            else
                            {
#if UNITY_EDITOR
								UnityEngine.Debug.Log("At least one element must be selected");
#else
                                XLogger.LogInfo("At least one element must be selected");
#endif
                            }
                        }
                    }
                    else
                    {
                        // Нету - добавляем
                        selectedItems.Add(_arrayOfItems[index]!.Model);

                        // Выделяем элемент
                        if (IsSupportModelSelected)
                        {
                            if (_arrayOfItems[index]?.Model is ILotusModelSelected selected_item)
                            {
                                selected_item.SetModelSelected(_arrayOfItems[index]!, true);
                            }
                        }

                        // Информируем - вызываем событие
                        if (_onSelectionAddItem != null) _onSelectionAddItem(index);
                    }
                }
            }
            else
            {
                if (index < 0)
                {
                    this.Unselect();
                }
            }
        }

        /// <summary>
        /// Дублирование текущего элемента и добавление элемента в список элементов.
        /// </summary>
        public virtual void DublicateSelectedItem()
        {
            if (_selectedIndex != -1)
            {
            }
        }

        /// <summary>
        /// Удаление текущего элемента из списка (удаляется объект).
        /// </summary>
        public virtual void DeleteSelectedItem()
        {
            if (_selectedIndex != -1)
            {
                RemoveAt(_selectedIndex);
                SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Перемещение текущего элемента назад.
        /// </summary>
        public virtual void MoveSelectedBackward()
        {
            // Корректируем индекс
            if (SelectedViewModel != null && _selectedIndex > 0)
            {
                MoveUp(_selectedIndex);

                // Корректируем индекс
                SetSelectedItem(_selectedIndex - 1);
            }
        }

        /// <summary>
        /// Перемещение текущего элемента вперед.
        /// </summary>
        public virtual void MoveSelectedForward()
        {
            // Корректируем индекс
            if (SelectedViewModel != null && _selectedIndex < Count - 1)
            {
                MoveDown(_selectedIndex);

                // Корректируем индекс
                SelectedIndex++;
            }
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Служебный метод.
        /// </summary>
        /// <returns>Список выделенных индексов.</returns>
        public string GetSelectedIndexes()
        {
            var result = "{" + selectedItems.Count.ToString() + "} ";
            for (var i = 0; i < selectedItems.Count; i++)
            {
                if (selectedItems[i] != null)
                {
                    result += selectedItems[i].ToString() + "; ";
                }
            }

            return result;
        }
        #endregion

        #region Event handler methods
        /// <summary>
        /// Обработчик события изменения данных источника привязки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="args">Аргументы события.</param>
        protected virtual void OnCollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var new_models = args.NewItems;
                        if (new_models != null && new_models.Count > 0)
                        {
                            for (var i = 0; i < new_models.Count; i++)
                            {
                                // Проверяем на дубликаты
                                var is_dublicate = false;
                                for (var j = 0; j < Count; j++)
                                {
                                    if (_arrayOfItems[j]!.Model == new_models[i])
                                    {
                                        is_dublicate = true;
                                        break;
                                    }
                                }

                                if (is_dublicate == false)
                                {
                                    var model = new_models[i] as TModel;
                                    var view_model = CreateViewModel(model!);
                                    view_model.IOwner = this;

                                    if (model is ILotusViewModelOwner view_item_owner)
                                    {
                                        view_item_owner.OwnerViewModel = view_model;
                                    }

                                    // Добавляем
                                    this.Add(view_model);
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    {
                        var old_index = args.OldStartingIndex;
                        var new_index = args.NewStartingIndex;
                        Move(old_index, new_index);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        var old_models = args.OldItems;
                        if (old_models != null && old_models.Count > 0)
                        {
                            for (var i = 0; i < old_models.Count; i++)
                            {
                                var model = old_models[i] as TModel;

                                // Находим элемент с данным контекстом
                                ILotusViewModel? view_model = this.Search((item) =>
                                {
                                    if (Object.ReferenceEquals(item?.Model, model))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                });

                                if (view_model != null)
                                {
                                    this.Remove(view_model);
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {

                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    {

                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
    /**@}*/
}