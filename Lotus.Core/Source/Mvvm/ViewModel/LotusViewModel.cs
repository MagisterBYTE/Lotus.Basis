using System.ComponentModel;

namespace Lotus.Core
{
    /** \addtogroup CoreViewModel
	*@{*/
    /// <summary>
    /// Интерфейса ViewModel.
    /// </summary>
    /// <remarks>
    /// ViewModel представляет собой концепцию промежуточного элемента, который хранит ссылку на модель
    /// дополнительно предоставляет основные параметры просмотра и управления
    /// </remarks>
    public interface ILotusViewModel : ILotusNameable, ILotusOwnedObject
    {
        #region Properties
        /// <summary>
        /// Модель.
        /// </summary>
        /// <remarks>
        /// Ссылка на данные которые связаны с данным элементом ViewModel.
        /// </remarks>
        object Model { get; }

        /// <summary>
        /// Выбор элемента ViewModel.
        /// </summary>
        /// <remarks>
        /// Подразумевает выбор элемента пользователем для просмотра свойств.
        /// По умолчанию может быть активировано только для одного элемента списка
        /// </remarks>
        bool IsSelected { get; set; }

        /// <summary>
        /// Доступность элемента ViewModel.
        /// </summary>
        /// <remarks>
        /// Подразумевается некая логическая доступность элемента.
        /// Активировано может быть для нескольких элементов списка
        /// </remarks>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Выбор элемента ViewModel.
        /// </summary>
        /// <remarks>
        /// Подразумевает выбор элемента пользователем для каких-либо действий.
        /// Активировано может быть для нескольких элементов списка
        /// </remarks>
        bool? IsChecked { get; set; }

        /// <summary>
        /// Отображение элемента ViewModel.
        /// </summary>
        /// <remarks>
        /// Подразумевает отображение элемента в отдельном контексте
        /// По умолчанию может быть активировано только для одного элемента списка
        /// </remarks>
        bool IsPresented { get; set; }

        /// <summary>
        /// Статус элемента ViewModel находящегося в режиме редактирования.
        /// </summary>
        /// <remarks>
        /// Реализация зависит от конкретной платформы.
        /// </remarks>
        bool IsEditMode { get; set; }

        /// <summary>
        /// Элемент пользовательского интерфейса который отображает данную ViewModel.
        /// </summary>
        object UIElement { get; set; }

        /// <summary>
        /// Контекстное меню элемента ViewModel.
        /// </summary>
        CUIContextMenu UIContextMenu { get; }

        /// <summary>
        /// Возможность перемещать ViewModel в элементе пользовательского интерфейса.
        /// </summary>
        bool UIDraggableStatus { get; }
        #endregion

        #region Methods 
        /// <summary>
        /// Открытие контекстного меню.
        /// </summary>
        /// <param name="contextMenu">Контекстное меню.</param>
        void OpenContextMenu(object contextMenu);
        #endregion
    }

    /// <summary>
    /// Интерфейса ViewModel с конкретным типом модели.
    /// </summary>
    /// <typeparam name="TModel">Тип модели.</typeparam>
    public interface ILotusViewModel<out TModel> : ILotusViewModel
    {
        #region Properties
        /// <summary>
        /// Модель.
        /// </summary>
        /// <remarks>
        /// Ссылка на данные которые связаны с данным элементом ViewModel.
        /// </remarks>
        new TModel Model { get; }
        #endregion
    }

    /// <summary>
    /// Шаблон определяющий минимальный ViewModel, который реализует основные параметры просмотра и управления.
    /// </summary>
    /// <typeparam name="TModel">Тип модели.</typeparam>
    public class ViewModel<TModel> : CNameable, ILotusViewModel<TModel>
        where TModel : class
    {
        #region Const
        /// <summary>
        /// Статус реализации модели интерейса <see cref="ILotusNameable"/>.
        /// </summary>
        public static readonly bool IsSupportNameable = typeof(TModel).IsSupportInterface<ILotusNameable>();

        /// <summary>
        /// Статус реализации модели интерейса <see cref="ILotusIdentifierInt"/>.
        /// </summary>
        public static readonly bool IsSupportIdentifierInt = typeof(TModel).IsSupportInterface<ILotusIdentifierInt>();

        /// <summary>
        /// Статус реализации модели интерейса <see cref="ILotusIdentifierLong"/>.
        /// </summary>
        public static readonly bool IsSupportIdentifierLong = typeof(TModel).IsSupportInterface<ILotusIdentifierLong>();

        /// <summary>
        /// Статус реализации модели интерейса <see cref="ILotusModelSelected"/>.
        /// </summary>
        public static readonly bool IsSupportModelSelected = typeof(TModel).IsSupportInterface<ILotusModelSelected>();

        /// <summary>
        /// Статус реализации модели интерейса <see cref="ILotusModelEnabled"/>.
        /// </summary>
        public static readonly bool IsSupportModelEnabled = typeof(TModel).IsSupportInterface<ILotusModelEnabled>();
        #endregion

        #region Static fields
        //
        // Константы для информирования об изменении свойств
        //
        protected static readonly PropertyChangedEventArgs PropertyArgsIsSelected = new PropertyChangedEventArgs(nameof(IsSelected));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsEnabled = new PropertyChangedEventArgs(nameof(IsEnabled));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsChecked = new PropertyChangedEventArgs(nameof(IsChecked));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsPresented = new PropertyChangedEventArgs(nameof(IsPresented));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsEditMode = new PropertyChangedEventArgs(nameof(IsEditMode));
        #endregion

        #region Fields
        // Основные параметры
        protected internal ILotusOwnerObject? _owner;
        protected internal TModel _model;
        protected internal bool _isEnabled;
        protected internal bool _isSelected;
        protected internal bool? _isChecked;
        protected internal bool _isPresented;
        protected internal bool _isEditMode;

        // Элементы интерфейса
        protected internal object _elementUI;
        protected internal CUIContextMenu _contextMenuUI;
        #endregion

        #region Properties
        /// <summary>
        /// Владелец объекта.
        /// </summary>
        public ILotusOwnerObject? IOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// Модель.
        /// </summary>
        /// <remarks>
        /// Ссылка на данные которые связаны с данным элементом ViewModel.
        /// </remarks>
        object ILotusViewModel.Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Модель.
        /// </summary>
        /// <remarks>
        /// Ссылка на данные которые связаны с данным элементом ViewModel.
        /// </remarks>
        public TModel Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Выбор элемента ViewModel.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    if (_model is ILotusModelSelected view_selected)
                    {
                        if (view_selected.CanModelSelected(this))
                        {
                            _isSelected = value;
                            view_selected.SetModelSelected(this, value);
                            OnPropertyChanged(PropertyArgsIsSelected);
                            RaiseIsSelectedChanged();
                        }
                    }
                    else
                    {
                        _isSelected = value;
                        OnPropertyChanged(PropertyArgsIsSelected);
                        RaiseIsSelectedChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Доступность элемента ViewModel.
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    if (_model is ILotusModelEnabled view_enabled)
                    {
                        _isEnabled = value;
                        view_enabled.SetModelEnabled(this, value);
                        OnPropertyChanged(PropertyArgsIsEnabled);
                        RaiseIsEnabledChanged();
                    }
                    else
                    {
                        _isEnabled = value;
                        OnPropertyChanged(PropertyArgsIsEnabled);
                        RaiseIsEnabledChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Выбор элемента элемента ViewModel.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(PropertyArgsIsChecked);
                    RaiseIsCheckedChanged();
                }
            }
        }

        /// <summary>
        /// Отображение элемента ViewModel в отдельном контексте.
        /// </summary>
        public bool IsPresented
        {
            get { return _isPresented; }
            set
            {
                if (_isPresented != value)
                {
                    if (_model is ILotusModelPresented view_presented)
                    {
                        _isPresented = value;
                        view_presented.SetModelPresented(this, value);
                        OnPropertyChanged(PropertyArgsIsPresented);
                        RaiseIsPresentedChanged();
                    }
                    else
                    {
                        _isPresented = value;
                        OnPropertyChanged(PropertyArgsIsPresented);
                        RaiseIsPresentedChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Статус элемента ViewModel находящегося в режиме редактирования.
        /// </summary>
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnPropertyChanged(PropertyArgsIsEditMode);
                }
            }
        }

        /// <summary>
        /// Элемент пользовательского интерфейса который отображает данную ViewModel.
        /// </summary>
        public object UIElement
        {
            get { return _elementUI; }
            set
            {
                _elementUI = value;
            }
        }

        /// <summary>
        /// Контекстное меню элемента ViewModel.
        /// </summary>
        public CUIContextMenu UIContextMenu
        {
            get { return _contextMenuUI; }
        }

        /// <summary>
        /// Возможность перемещать ViewModel в элементе пользовательского интерфейса.
        /// </summary>
        public virtual bool UIDraggableStatus
        {
            get { return false; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="name">Служебное наименование ViewModel.</param>
        public ViewModel(TModel model, string name = "")
            : base(name)
        {
            _model = model;
            SetDataContext();
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Служебное наименование ViewModel.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Изменение выбора элемента ViewModel.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsSelectedChanged()
        {
            if (_owner != null)
            {
                _owner.OnNotifyUpdated(this, IsSelected, nameof(IsSelected));
            }
        }

        /// <summary>
        /// Изменение доступности элемента ViewModel.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsEnabledChanged()
        {
            if (_owner != null)
            {
                _owner.OnNotifyUpdated(this, IsEnabled, nameof(IsEnabled));
            }
        }

        /// <summary>
        /// Изменение выбора элемента ViewModel.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsCheckedChanged()
        {
            if (_owner != null)
            {
                _owner.OnNotifyUpdated(this, IsChecked, nameof(IsChecked));
            }
        }

        /// <summary>
        /// Изменение статуса отображения ViewModel.
        /// Метод автоматически вызывается после установки соответствующего свойства.
        /// </summary>
        protected virtual void RaiseIsPresentedChanged()
        {
            if (IOwner != null)
            {
                IOwner.OnNotifyUpdated(this, IsPresented, nameof(IsPresented));
            }
        }
        #endregion

        #region ILotusViewModel methods
        /// <summary>
        /// Открытие контекстного меню.
        /// </summary>
        /// <param name="contextMenu">Контекстное меню.</param>
        public virtual void OpenContextMenu(object contextMenu)
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка данных.
        /// </summary>
        public virtual void SetDataContext()
        {
            if (string.IsNullOrEmpty(_name))
            {
                if (_model is ILotusNameable nameable)
                {
                    _name = nameable.Name;
                }
                else
                {
                    _name = _model.ToString()!;
                }
            }

            if (_model is ILotusViewModelOwner view_item_owner)
            {
                view_item_owner.OwnerViewModel = this;
            }
        }

        /// <summary>
        /// Очистка данных.
        /// </summary>
        public virtual void UnsetDataContext()
        {
        }
        #endregion
    }
    /**@}*/
}