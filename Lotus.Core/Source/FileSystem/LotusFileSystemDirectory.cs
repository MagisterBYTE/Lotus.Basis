//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема файловой системы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusFileSystemDirectory.cs
*		Элемент файловой системы представляющий собой директорию.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.IO;
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
		/// Элемент файловой системы представляющий собой директорию
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CFileSystemDirectory : CNameable, ILotusOwnerObject, ILotusFileSystemEntity, ILotusViewExpanded
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			public const String IMPropertyMissing = "Свойство отсутствует";
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			/// <summary>
			/// Описание свойств
			/// </summary>
			public readonly static CPropertyDesc[] FileSystemDirectoryPropertiesDesc = new CPropertyDesc[]
			{
				// Идентификация
				CPropertyDesc.OverrideDisplayNameAndDescription<CFileSystemDirectory>(nameof(Name), "Имя", "Имя директории"),
			};
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дерева элементов файловой системы по указанному пути
			/// </summary>
			/// <param name="path">Путь</param>
			/// <returns>Узел дерева представляющего собой директорию</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CFileSystemDirectory Build(String path)
			{
				var dir_info = new DirectoryInfo(path);
				var dir_model = new CFileSystemDirectory(dir_info);
				dir_model.RecursiveFileSystemInfo();
				return dir_model;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal DirectoryInfo mInfo;
			protected internal ListArray<ILotusFileSystemEntity> mEntities;
			protected internal CParameters mParameters;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наименование директории
			/// </summary>
			public override String Name
			{
				get { return mName; }
				set
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
					RaiseNameChanged();
				}
			}

			/// <summary>
			/// Список вложенных директорий и файлов
			/// </summary>
			public ListArray<ILotusFileSystemEntity> Entities
			{
				get { return mEntities; }
			}

			/// <summary>
			/// Полное имя(полный путь) элемента файловой системы
			/// </summary>
			public String FullName
			{
				get
				{
					if (mInfo != null)
					{
						return mInfo.FullName;
					}
					else
					{
						return mName;
					}
				}
			}

			/// <summary>
			/// Информация о директории
			/// </summary>
			public DirectoryInfo Info
			{
				get { return mInfo; }
				set { mInfo = value; }
			}

#if USE_WINDOWS
			/// <summary>
			/// Графическая иконка связанная с данным элементом файловой системы
			/// </summary>
			public System.Windows.Media.ImageSource IconSource { get; set; }
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Полное имя(полный путь) элемента файловой системы
			/// </summary>
			[LotusPropertyOrder(0)]
			[DisplayName("Имя элемента")]
			[Description("Имя элемента как информационной модели")]
			[Category("Информационная модель")]
			[LotusCategoryOrder(1)]
			public String Label
			{
				get
				{
					if (mParameters != null)
					{
						return mParameters.GetStringValue(nameof(Label), IMPropertyMissing);
					}
					else
					{
						return IMPropertyMissing;
					}
				}
				set
				{
					if (mParameters != null)
					{
						mParameters.UpdateStringValue(nameof(Label), value);
					}
				}
			}

			/// <summary>
			/// Полное имя(полный путь) элемента файловой системы
			/// </summary>
			[LotusPropertyOrder(1)]
			[DisplayName("Описание элемента")]
			[Description("Описание элемента как информационной модели")]
			[Category("Информационная модель")]
			public String Desc
			{
				get
				{
					if (mParameters != null)
					{
						return mParameters.GetStringValue(nameof(Desc), IMPropertyMissing);
					}
					else
					{
						return IMPropertyMissing;
					}
				}
				set
				{
					if (mParameters != null)
					{
						mParameters.UpdateStringValue(nameof(Desc), value);
					}
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusSupportEditInspector =======================
			/// <summary>
			/// Отображаемое имя типа в инспекторе свойств
			/// </summary>
			public String InspectorTypeName
			{
				get { return "ДИРЕКТОРИЯ"; }
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
						return mInfo.Name;
					}
					else
					{
						return "";
					}
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="directoryInfo">Информация о директории</param>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemDirectory(DirectoryInfo directoryInfo)
				: this(directoryInfo.Name, directoryInfo)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="fullPath">Полный путь к директории</param>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemDirectory(String fullPath)
			{
				mInfo = new DirectoryInfo(fullPath);
				mName = mInfo.Name;
				mEntities = new ListArray<ILotusFileSystemEntity>();
				mEntities.IsNotify = true;
				if(mInfo != null)
				{
					var path = Path.Combine(mInfo.FullName, "Info.json");
					if(File.Exists(path))
					{
						mParameters = new CParameters();
						mParameters.Load(path);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="displayName">Название узла</param>
			/// <param name="directoryInfo">Информация о директории</param>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemDirectory(String displayName, DirectoryInfo directoryInfo)
				: base(displayName)
			{
				mInfo = directoryInfo;
				mEntities = new ListArray<ILotusFileSystemEntity>();
				mEntities.IsNotify = true;

				if (mInfo != null)
				{
					var path = Path.Combine(mInfo.FullName, "Info.json");
					if (File.Exists(path))
					{
						mParameters = new CParameters();
						mParameters.Load(path);
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="add">Статус добавления в коллекцию</param>
			//---------------------------------------------------------------------------------------------------------
			public void AttachOwnedObject(ILotusOwnedObject owned_object, Boolean add)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отсоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="remove">Статус удаления из коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public void DetachOwnedObject(ILotusOwnedObject owned_object, Boolean remove)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей для зависимых объектов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateOwnedObjects()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта о начале изменения данных указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект, данные которого будут меняться</param>
			/// <param name="data_name">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean OnNotifyUpdating(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта об окончании изменении данных указанного объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект, данные которого изменились</param>
			/// <param name="data_name">Имя данных</param>
			//---------------------------------------------------------------------------------------------------------
			public void OnNotifyUpdated(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{

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
				return mEntities.Count;
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
				return mEntities[index];
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
				if (match(this))
				{
					return true;
				}
				else
				{
					for (var i = 0; i < mEntities.Count; i++)
					{
						if(mEntities[i].CheckOne(match))
						{
							return true;
						}
					}
				}

				return false;
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewExpanded =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка статуса раскрытия объекта
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			/// <param name="expanded">Статус раскрытия объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetViewExpanded(ILotusViewItemHierarchy view_item, Boolean expanded)
			{
				if (expanded)
				{
					for (var i = 0; i < mEntities.Count; i++)
					{
						if (mEntities[i] is CFileSystemDirectory directory)
						{
							if (directory.Entities.Count == 0)
							{
								DirectoryInfo[] dirs_info = directory.Info.GetDirectories();
								FileInfo[] files_info = directory.Info.GetFiles();
								
								// Если данные есть
								if(dirs_info.Length > 0 || files_info.Length > 0)
								{
									// Сначала директории
									for (var id = 0; id < dirs_info.Length; id++)
									{
										DirectoryInfo dir_info = dirs_info[id];
										directory.AddDirectory(dir_info);
									}

									// Теперь файлы
									for (var ifi = 0; ifi < files_info.Length; ifi++)
									{
										FileInfo file_info = files_info[ifi];
										directory.AddFile(file_info);
									}

									// Теперь визуальные модели
									view_item.BuildFromDataContext();
								}
							}
						}
					}
				}
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
				return FileSystemDirectoryPropertiesDesc;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление директории
			/// </summary>
			/// <param name="directoryInfo">Информация о директории</param>
			/// <returns>Элемент файловой системы представляющий собой директорию</returns>
			//---------------------------------------------------------------------------------------------------------
			protected CFileSystemDirectory AddDirectory(DirectoryInfo directoryInfo)
			{
				// Не создаем не нежные директории
				if (directoryInfo.Name.Contains(".git")) return null;
				if (directoryInfo.Name.Contains(".vs")) return null;

				// Создаем директорию
				var directory = new CFileSystemDirectory(directoryInfo);

				// Добавляем
				this.mEntities.Add(directory);

				return directory;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление директории
			/// </summary>
			/// <param name="fileInfo">Информация о файле</param>
			/// <returns>Элемент файловой системы представляющий собой файл</returns>
			//---------------------------------------------------------------------------------------------------------
			protected CFileSystemFile AddFile(FileInfo fileInfo)
			{
				// Не создаем не нежные файлы
				if (fileInfo.Extension == ".meta") return null;

				// Создаем файл
				var file = new CFileSystemFile(fileInfo);

				// Добавляем
				this.mEntities.Add(file);

				return file;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение файлов и дочерних директорий в текущей директории
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void GetFileSystemItems()
			{
				mEntities.Clear();

				DirectoryInfo[] dirs_info = Info.GetDirectories();
				FileInfo[] files_info = Info.GetFiles();

				// Сначала директории
				for (var i = 0; i < dirs_info.Length; i++)
				{
					DirectoryInfo dir_info = dirs_info[i];
					AddDirectory(dir_info);
				}

				// Теперь файлы
				for (var i = 0; i < files_info.Length; i++)
				{
					FileInfo file_info = files_info[i];
					AddFile(file_info);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение файлов и дочерних директорий в текущей директории и всех дочерних директориях
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void GetFileSystemItemsTwoLevel()
			{
				mEntities.Clear();

				DirectoryInfo[] dirs_info = Info.GetDirectories();
				FileInfo[] files_info = Info.GetFiles();

				// Сначала директории
				for (var i = 0; i < dirs_info.Length; i++)
				{
					DirectoryInfo dir_info = dirs_info[i];
					CFileSystemDirectory directory = AddDirectory(dir_info);
					if(directory != null)
					{
						directory.GetFileSystemItems();
					}
				}

				// Теперь файлы
				for (var i = 0; i < files_info.Length; i++)
				{
					FileInfo file_info = files_info[i];
					AddFile(file_info);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение файлов и дочерних директорий в текущей директории и всех дочерних директориях
			/// </summary>
			/// <returns>Статус получение файлов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean AddFileSystemItemsTwoLevel()
			{
				var status = false;
				for (var i = 0; i < mEntities.Count; i++)
				{
					if (mEntities[i] is CFileSystemDirectory directory)
					{
						if(directory.Entities.Count == 0)
						{
							directory.GetFileSystemItems();
							status = true;
						}
					}
				}

				return status;
			}



			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переименовать директорию
			/// </summary>
			/// <param name="newDirectoryName">Новое имя директории</param>
			//---------------------------------------------------------------------------------------------------------
			public void Rename(String newDirectoryName)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное получение данных элементов файловой системы
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RecursiveFileSystemInfo()
			{
				mEntities.Clear();
				RecursiveFileSystemInfo(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивное получение данных элементов файловой системы на 2 уровня ниже
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RecursiveFileSystemInfoTwoLevel()
			{
				DirectoryInfo[] dirs_info = Info.GetDirectories();
				FileInfo[] files_info = Info.GetFiles();

				// Сначала директории
				for (var i = 0; i < dirs_info.Length; i++)
				{
					DirectoryInfo dir_info = dirs_info[i];
					CFileSystemDirectory sub_directory = AddDirectory(dir_info);

					//// 2 уровень
					//for (Int32 i2 = 0; i2 < dirs_info.Length; i2++)
					//{
					//	//DirectoryInfo[] sub_directories_2 = dirs_info[i2].GetDirectories();
					//	//FileInfo[] files_2 = dirs_info[i2].GetFiles();

					//	//// Сначала директории
					//	//for (Int32 i2d = 0; i2d < sub_directories_2.Length; i2d++)
					//	//{
					//	//	CFileSystemDirectory sub_directory_node2 = new CFileSystemDirectory(sub_directories_2[i2d]);

					//	//	if (sub_directory_node2.Name.Contains(".git")) continue;
					//	//	if (sub_directory_node2.Name.Contains(".vs")) continue;

					//	//	sub_directory_node.Entities.Add(sub_directory_node2);
					//	//}
					//}
				}

				// Теперь файлы
				for (var i = 0; i < files_info.Length; i++)
				{
					FileInfo file_info = files_info[i];
					AddFile(file_info);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рекурсивная обработка объектов файловой системы
			/// </summary>
			/// <param name="parentDirectoryNode">Родительский узел директории</param>
			//---------------------------------------------------------------------------------------------------------
			protected void RecursiveFileSystemInfo(CFileSystemDirectory parentDirectoryNode)
			{
				DirectoryInfo[] sub_directories = parentDirectoryNode.Info.GetDirectories();
				FileInfo[] files = parentDirectoryNode.Info.GetFiles();

				// Сначала директории
				for (var i = 0; i < sub_directories.Length; i++)
				{
					var sub_directory_node = new CFileSystemDirectory(sub_directories[i]);

					if (sub_directory_node.Name.Contains(".git")) continue;
					if (sub_directory_node.Name.Contains(".vs")) continue;

					this.mEntities.Add(sub_directory_node);

					sub_directory_node.RecursiveFileSystemInfo(sub_directory_node);
				}

				// Теперь файлы
				for (var i = 0; i < files.Length; i++)
				{
					FileInfo file_info = files[i];

					if (file_info.Extension == ".meta") continue;

					var file_node = new CFileSystemFile(file_info);
					this.mEntities.Add(file_node);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существовании директории среди дочерних объектов
			/// </summary>
			/// <param name="dirInfo">Информация о директории</param>
			/// <returns>Статус существования</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ExistInChildren(DirectoryInfo dirInfo)
			{
				for (var i = 0; i < mEntities.Count; i++)
				{
					var dir_node = mEntities[i] as CFileSystemDirectory;
					if (dir_node != null)
					{
						if (dir_node.Info.Name == dirInfo.Name)
						{
							return true;
						}
					}
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существовании файла среди дочерних объектов
			/// </summary>
			/// <param name="fileInfo">Информация о файле</param>
			/// <returns>Статус существования</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ExistInChildren(FileInfo fileInfo)
			{
				for (var i = 0; i < mEntities.Count; i++)
				{
					var file_node = mEntities[i] as CFileSystemFile;
					if (file_node != null)
					{
						if (file_node.Info.Name == fileInfo.Name)
						{
							return true;
						}
					}
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и получение узла директории среди дочерних объектов
			/// </summary>
			/// <param name="dirInfo">Информация о директории</param>
			/// <returns>Узел директории</returns>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemDirectory GetDirectoryNodeFromChildren(DirectoryInfo dirInfo)
			{
				for (var i = 0; i < mEntities.Count; i++)
				{
					var dir_node = mEntities[i] as CFileSystemDirectory;
					if (dir_node != null)
					{
						if (dir_node.Info.Name == dirInfo.Name)
						{
							return dir_node;
						}
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и получение узла файла среди дочерних объектов
			/// </summary>
			/// <param name="fileInfo">Информация о файле</param>
			/// <returns>Узел файла</returns>
			//---------------------------------------------------------------------------------------------------------
			public CFileSystemFile GetFileNodeFromChildren(FileInfo fileInfo)
			{
				for (var i = 0; i < mEntities.Count; i++)
				{
					var file_node = mEntities[i] as CFileSystemFile;
					if (file_node != null)
					{
						if (file_node.Info.Name == fileInfo.Name)
						{
							return file_node;
						}
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование файлов и указанную директорию
			/// </summary>
			/// <param name="path">Имя директории</param>
			/// <param name="isDirectoryName">С учетом данной директории</param>
			//---------------------------------------------------------------------------------------------------------
			public void Copy(String path, Boolean isDirectoryName)
			{
				if(isDirectoryName)
				{
					var dest_path_dir = Path.Combine(path, Info.Name);
					if (Directory.Exists(dest_path_dir) == false)
					{
						Directory.CreateDirectory(dest_path_dir);
					}
				}

				for (var i = 0; i < mEntities.Count; i++)
				{
					var file_node = mEntities[i] as CFileSystemFile;
					if (file_node != null)
					{
						if(file_node.OwnerViewItem != null)
						{
							if (file_node.OwnerViewItem.IsChecked.HasValue && file_node.OwnerViewItem.IsChecked.Value)
							{
								if (isDirectoryName)
								{
									var dest_path = Path.Combine(path, Info.Name, file_node.Info.Name);
									File.Copy(file_node.Info.FullName, dest_path);
								}
								else
								{
									var dest_path = Path.Combine(path, file_node.Info.Name);
									File.Copy(file_node.Info.FullName, dest_path);
								}
							}
						}
						else
						{
							if (isDirectoryName)
							{
								var dest_path = Path.Combine(path, Info.Name, file_node.Info.Name);
								File.Copy(file_node.Info.FullName, dest_path);
							}
							else
							{
								var dest_path = Path.Combine(path, file_node.Info.Name);
								File.Copy(file_node.Info.FullName, dest_path);
							}
						}
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