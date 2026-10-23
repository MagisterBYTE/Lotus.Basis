
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusTreeViewControl.cs
*		Элемент для отображения дерева файловой системы для редактора Unity.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
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
				get { return mSelectedNode?.DataContext as CFileSystemDirectory; }
			}

			/// <summary>
			/// Текущий выбранный узел отображения как файл
			/// </summary>
			public CFileSystemFile SelectedFile
			{
				get { return mSelectedNode?.DataContext as CFileSystemFile; }
			}
			#endregion

			#region Constructors
			
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			
			public CTreeViewFileSystem()
				: base(new CCollectionViewHierarchyFileSystem())
			{
			}

			
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data">Данные корневого узла</param>
			
			public CTreeViewFileSystem(ILotusFileSystemEntity data)
				: base(new CCollectionViewHierarchyFileSystem(), data)
			{
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Фильтрация узлов
			/// </summary>
			
			protected override void OnFiltered()
			{
				if (String.IsNullOrEmpty(mSearchString) && mRootData != null)
				{
					mRoot.IsFiltered = false;
					mRoot.ResetSource();
				}
				else
				{
					mRoot.IsFiltered = true;
					mRoot.Filter =(ILotusFileSystemEntity data)=>
					{
						return data.Name.Contains(mSearchString);
					};

					mRoot.ResetSource();
					mRoot.Expanded();
				}
			}

			
			/// <summary>
			/// Удаление текущего выбранного узла
			/// </summary>
			
			public override void Delete()
			{
				var deleted = mSelectedNode;
				mSelectedNode = null;

				if(deleted.Model is ILotusFileSystemEntity fileSystemEntity)
				{
					fileSystemEntity.Delete();
				}

				deleted.IParent.DetachOwnedObject(deleted, true);
			}
			#endregion
		}

		/**@}*/

	}
}
