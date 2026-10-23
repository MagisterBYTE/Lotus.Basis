
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusTreeViewControl.cs
*		Элемент для отображения дерева для редактора Unity.
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
		/// Элемент для отображения дерева для редактора Unity
		/// </summary>

		public class CTreeViewControl : TreeViewControlBase<CCollectionViewHierarchyObject, 
			CViewItemHierarchyObject, System.Object>
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
				: base(new CCollectionViewHierarchyObject())
			{
			}

			
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data">Данные корневого узла</param>
			
			public CTreeViewControl(System.Object data)
				: base(new CCollectionViewHierarchyObject(), data)
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
					mRoot.Filter =(System.Object data)=>
					{
						var file_system_entity = data as ILotusFileSystemEntity;
						return (file_system_entity.Name.Contains(mSearchString));
					};

					mRoot.ResetSource();
					mRoot.Expanded();
				}
			}
			#endregion
		}

		/**@}*/

	}
}
