namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Класс реализующий ViewModel для файла файловой системы.
    /// </summary>
    public class ViewModelFileSystemFile : ViewModelHierarchy<ILotusFileSystemEntity>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parentItem">Родительский узел.</param>
        public ViewModelFileSystemFile(ILotusFileSystemEntity model, ILotusViewModelHierarchy? parentItem)
            : base(model, parentItem)
        {
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
        #endregion
    }
    /**@}*/
}