#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Элемент для отображения дерева файловой системы для редактора Unity
    /// </summary>
    public class CTreeViewFileSystem : TreeViewControlBase<CollectionViewModelFileSystem,
        ILotusViewModelHierarchy, ILotusFileSystemEntity>
    {
        #region Fields
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Текущий выбранный узел отображения как директория
        /// </summary>
        public CFileSystemDirectory SelectedDirectory
        {
            get { return _selectedNode?.Model as CFileSystemDirectory; }
        }

        /// <summary>
        /// Текущий выбранный узел отображения как файл
        /// </summary>
        public CFileSystemFile SelectedFile
        {
            get { return _selectedNode?.Model as CFileSystemFile; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CTreeViewFileSystem()
            : base(new CollectionViewModelFileSystem())
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="data">Данные корневого узла</param>
        public CTreeViewFileSystem(ILotusFileSystemEntity data)
            : base(new CollectionViewModelFileSystem(), data)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Фильтрация узлов
        /// </summary>
        protected override void OnFiltered()
        {
            if (string.IsNullOrEmpty(_searchString) && _rootData != null)
            {
                _root.IsFiltered = false;
                _root.ResetSource();
            }
            else
            {
                _root.IsFiltered = true;
                _root.Filter = (ILotusFileSystemEntity data) =>
                {
                    return data != null && data.Name != null && data.Name.Contains(_searchString);
                };

                _root.ResetSource();
                _root.Expanded();
            }
        }

        /// <summary>
        /// Удаление текущего выбранного узла
        /// </summary>
        public override void Delete()
        {
            var deleted = _selectedNode;
            _selectedNode = null;

            if (deleted?.Model is ILotusFileSystemEntity fileSystemEntity)
            {
                fileSystemEntity.Delete();
            }

            deleted?.IParent?.DetachOwnedObject(deleted, true);
        }
        #endregion
    }
    /**@}*/
}
#endif
