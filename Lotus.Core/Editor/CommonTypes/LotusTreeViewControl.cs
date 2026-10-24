#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Элемент для отображения дерева для редактора Unity
    /// </summary>
    public class CTreeViewControl : TreeViewControlBase<CollectionViewObject,
        ViewModelObject, object>
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CTreeViewControl()
            : base(new CollectionViewObject())
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="data">Данные корневого узла</param>
        public CTreeViewControl(object data)
            : base(new CollectionViewObject(), data)
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
                _root.Filter = (object data) =>
                {
                    if (data is ILotusFileSystemEntity fileSystemEntity && fileSystemEntity.Name != null)
                    {
                        return fileSystemEntity.Name.Contains(_searchString);
                    }

                    return false;
                };

                _root.ResetSource();
                _root.Expanded();
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
