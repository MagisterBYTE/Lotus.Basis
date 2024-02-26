using System;

namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Коллекция для отображения элементов файловой системы.
    /// </summary>
    public class CollectionViewModelFileSystem : CollectionViewModelHierarchy<ILotusViewModelHierarchy,
        ILotusFileSystemEntity>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CollectionViewModelFileSystem()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        public CollectionViewModelFileSystem(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        /// <param name="source">Источник данных.</param>
        public CollectionViewModelFileSystem(string name, ILotusFileSystemEntity source)
            : base(name, source)
        {
        }
        #endregion

        #region ILotusCollectionViewModelHierarchy methods
        /// <summary>
        /// Создание конкретной ViewModel для указанной модели.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parent">Родительский элемент ViewModel.</param>
        /// <returns>ViewModel.</returns>
        public override ILotusViewModelHierarchy CreateViewModelHierarchy(object model, ILotusViewModelHierarchy? parent)
        {
            if (model is CFileSystemFile file)
            {
                return new ViewModelFileSystemFile(file, parent);
            }

            if (model is CFileSystemDirectory directory)
            {
                return new ViewModelDirectorySystemFile(directory, parent);
            }

            throw new NotImplementedException("Model must be type <CFileSystemFile> or <CFileSystemDirectory>");
        }
        #endregion
    }
    /**@}*/
}