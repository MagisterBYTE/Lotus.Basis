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
		public class CFileSystemFile : CNameable, ILotusOwnedObject, ILotusFileSystemEntity
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal ILotusOwnerObject _owner;
			protected internal FileInfo _info;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Родительский объект владелей
			/// </summary>
			public ILotusOwnerObject IOwner
			{
				get { return _owner; }
				set { }
			}

			/// <summary>
			/// Наименование файла
			/// </summary>
			public override String Name
			{
				get { return _name; }
				set
				{
					try
					{
						if(_info != null)
						{
							var new_file_path = XFilePath.GetPathForRenameFile(_info.FullName, value);
							File.Move(_info.FullName, new_file_path);
							_name = value;
							NotifyPropertyChanged(PropertyArgsName);
							RaiseNameChanged();

						}
						else
						{
							_name = value;
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
					if(_info != null)
					{
						return _info.FullName;
					}
					else
					{
						return _name;
					}
				}
			}

			/// <summary>
			/// Информация о файле
			/// </summary>
			public FileInfo Info
			{
				get { return _info; }
				set { _info = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="fileInfo">Данные о файле</param>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemFile(FileInfo fileInfo)
				: base(fileInfo.Name)
			{
				_info = fileInfo;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusFileSystemEntity =============================
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
				return match(this);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переименовать файл
			/// </summary>
			/// <param name="newFileName">Новое имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void RenameAssets(String newFileName)
			{
#if UNITY_EDITOR
				if (_info != null)
				{
					String new_path = XEditorAssetDatabase.RenameAssetFromFullPath(_info.FullName, newFileName);
					_info = new FileInfo(new_path);
					_name = _info.Name;
					NotifyPropertyChanged(PropertyArgsName);
					RaiseNameChanged();
				}
#else

#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Модификация имени файла путем удаления его определённой части
			/// </summary>
			/// <param name="searchOption">Опции поиска</param>
			/// <param name="check">Проверяемая строка</param>
			//---------------------------------------------------------------------------------------------------------
			public void ModifyNameOfRemove(TStringSearchOption searchOption, String check)
			{
				if (_info != null)
				{
					var file_name = _info.Name.RemoveExtension();
					switch (searchOption)
					{
						case TStringSearchOption.Start:
							{
								var index = file_name.IndexOf(check);
								if (index > -1)
								{
#if UNITY_EDITOR
									file_name = file_name.Remove(index, check.Length);
                                    var new_path = XEditorAssetDatabase.RenameAssetFromFullPath(_info.FullName, file_name);
                                    _info = new FileInfo(new_path);
                                    _name = _info.Name;
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
                                    var new_path = XEditorAssetDatabase.RenameAssetFromFullPath(_info.FullName, file_name);
                                    _info = new FileInfo(new_path);
                                    _name = _info.Name;
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
			/// <param name="searchOption">Опции поиска</param>
			/// <param name="source">Искомая строка</param>
			/// <param name="target">Целевая строка</param>
			//---------------------------------------------------------------------------------------------------------
			public void ModifyNameOfReplace(TStringSearchOption searchOption, String source, String target)
			{
				if (_info != null)
				{
					var file_name = _info.Name.RemoveExtension();
					switch (searchOption)
					{
						case TStringSearchOption.Start:
							{
								var index = file_name.IndexOf(source);
								if (index > -1)
								{
#if UNITY_EDITOR
									file_name = file_name.Replace(source, target);
                                    var new_path = XEditorAssetDatabase.RenameAssetFromFullPath(_info.FullName, file_name);
                                    _info = new FileInfo(new_path);
                                    _name = _info.Name;
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление файла
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Delete()
			{
				File.Delete(_info.FullName);

				var metaFile = _info.FullName + ".meta";
				if (File.Exists(metaFile))
				{
					File.Delete(metaFile);
				}

				_info = null;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================