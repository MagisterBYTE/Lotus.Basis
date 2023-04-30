//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема файловой системы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusFileSystemFile.cs
*		Элемент файловой системы представляющий собой файл.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.IO;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreFileSystem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент файловой системы представляющий собой файл
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CFileSystemFile : CNameable, ILotusOwnedObject, ILotusFileSystemEntity, ILotusViewItemOwner
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			/// <summary>
			/// Описание свойств
			/// </summary>
			public readonly static CPropertyDesc[] FileSystemFilePropertiesDesc = new CPropertyDesc[]
			{
				// Идентификация
				CPropertyDesc.OverrideDisplayNameAndDescription<CFileSystemFile>(nameof(Name), "Имя", "Имя файла"),
			};
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal ILotusOwnerObject mOwner;
			protected internal FileInfo mInfo;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Родительский объект владелей
			/// </summary>
			public ILotusOwnerObject IOwner
			{
				get { return (mOwner); }
				set { }
			}

			/// <summary>
			/// Наименование файла
			/// </summary>
			public override String Name
			{
				get { return (mName); }
				set
				{
					try
					{
						if(mInfo != null)
						{
							var new_file_path = XFilePath.GetPathForRenameFile(mInfo.FullName, value);
							File.Move(mInfo.FullName, new_file_path);
							mName = value;
							NotifyPropertyChanged(PropertyArgsName);
							RaiseNameChanged();
						}
						else
						{
							mName = value;
							NotifyPropertyChanged(PropertyArgsName);
							RaiseNameChanged();
						}
					}
					catch (Exception exc)
					{
						XLogger.LogException(exc);
					}
				}
			}

			/// <summary>
			/// Полное имя(полный путь) элемента файловой системы
			/// </summary>
			public String FullName 
			{
				get 
				{
					if(mInfo != null)
					{
						return (mInfo.FullName);
					}
					else
					{
						return (mName);
					}
				}
			}

			/// <summary>
			/// Информация о файле
			/// </summary>
			public FileInfo Info
			{
				get { return (mInfo); }
				set { mInfo = value; }
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportEditInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public String InspectorTypeName
			{
				get { return ("ФАЙЛ"); }
			}

			/// <summary>
			/// Отображаемое имя объекта в инспекторе свойств
			/// </summary>
			public String InspectorObjectName
			{
				get
				{
					if (mInfo != null)
					{
						return (mInfo.Name);
					}
					else
					{
						return ("");
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusViewItemOwner ==============================
			/// <summary>
			/// Элемент отображения
			/// </summary>
			public ILotusViewItem OwnerViewItem { get; set; }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="file_info">Данные о файле</param>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemFile(FileInfo file_info)
				: base(file_info.Name)
			{
				mInfo = file_info;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusFileSystemEntity =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количества дочерних узлов
			/// </summary>
			/// <returns>Количество дочерних узлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetCountChildrenNode()
			{
				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дочернего узла по индексу
			/// </summary>
			/// <param name="index">Индекс дочернего узла</param>
			/// <returns>Дочерней узел</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetChildrenNode(Int32 index)
			{
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка объекта на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Объект удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean CheckOne(Predicate<ILotusFileSystemEntity> match)
			{
				return (match(this));
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSupportEditInspector =========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить массив описателей свойств объекта
			/// </summary>
			/// <returns>Массив описателей</returns>
			//---------------------------------------------------------------------------------------------------------
			public CPropertyDesc[] GetPropertiesDesc()
			{
				return (FileSystemFilePropertiesDesc);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переименовать файл
			/// </summary>
			/// <param name="new_file_name">Новое имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rename(String new_file_name)
			{
				//if(mInfo != null)
				//{
				//	String new_path = XEditorAssetDatabase.RenameAssetFromFullPath(mInfo.FullName, new_file_name);
				//	mInfo = new FileInfo(new_path);
				//	mName = mInfo.Name;
				//}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Модификация имени файла путем удаления его определённой части
			/// </summary>
			/// <param name="search_option">Опции поиска</param>
			/// <param name="check">Проверяемая строка</param>
			//---------------------------------------------------------------------------------------------------------
			public void ModifyNameOfRemove(TStringSearchOption search_option, String check)
			{
				if (mInfo != null)
				{
					var file_name = mInfo.Name.RemoveExtension();
					switch (search_option)
					{
						case TStringSearchOption.Start:
							{
								var index = file_name.IndexOf(check);
								if (index > -1)
								{
#if UNITY_EDITOR
									file_name = file_name.Remove(index, check.Length);
                                    String new_path = XEditorAssetDatabase.RenameAssetFromFullPath(mInfo.FullName, file_name);
                                    mInfo = new FileInfo(new_path);
                                    mName = mInfo.Name;
#else

#endif
								}
							}
							break;
						case TStringSearchOption.End:
							{
								var index = file_name.LastIndexOf(check);
								if (index > -1)
								{
#if UNITY_EDITOR
									file_name = file_name.Remove(index, check.Length);
                                    String new_path = XEditorAssetDatabase.RenameAssetFromFullPath(mInfo.FullName, file_name);
                                    mInfo = new FileInfo(new_path);
                                    mName = mInfo.Name;
#else

#endif
								}
							}
							break;
						case TStringSearchOption.Contains:
							break;
						case TStringSearchOption.Equal:
							break;
						default:
							break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Модификация имени файла путем замены его определённой части
			/// </summary>
			/// <param name="search_option">Опции поиска</param>
			/// <param name="source">Искомая строка</param>
			/// <param name="target">Целевая строка</param>
			//---------------------------------------------------------------------------------------------------------
			public void ModifyNameOfReplace(TStringSearchOption search_option, String source, String target)
			{
				if (mInfo != null)
				{
					var file_name = mInfo.Name.RemoveExtension();
					switch (search_option)
					{
						case TStringSearchOption.Start:
							{
								var index = file_name.IndexOf(source);
								if (index > -1)
								{
#if UNITY_EDITOR
									file_name = file_name.Replace(source, target);
                                    String new_path = XEditorAssetDatabase.RenameAssetFromFullPath(mInfo.FullName, file_name);
                                    mInfo = new FileInfo(new_path);
                                    mName = mInfo.Name;
#else

#endif
								}
							}
							break;
						case TStringSearchOption.End:
							{
							}
							break;
					}
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================