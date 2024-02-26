namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Класс реализующий ViewModel для директории файловой системы.
    /// </summary>
    public class ViewModelDirectorySystemFile : ViewModelHierarchy<ILotusFileSystemEntity>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parentItem">Родительский узел.</param>
        public ViewModelDirectorySystemFile(ILotusFileSystemEntity model, ILotusViewModelHierarchy? parentItem)
            : base(model, parentItem)
        {
            SetContextMenu();
            IsNotify = true;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка контекстного меню.
        /// </summary>
        public virtual void SetContextMenu()
        {
            _contextMenuUI = new CUIContextMenu();
            _contextMenuUI.ViewModel = this;
            _contextMenuUI.AddItem(CUIContextMenu.Remove.Duplicate());
        }

        /// <summary>
        /// Построение дочерней иерархии согласно модели.
        /// </summary>
        public override void BuildFromModel()
        {
            Clear();
            CollectionViewModelFileSystem.BuildFromParent(this, _owner!);
        }
        #endregion
    }
    /**@}*/
}