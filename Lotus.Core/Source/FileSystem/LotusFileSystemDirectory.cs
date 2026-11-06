using System;
using System.IO;

namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Элемент файловой системы представляющий собой директорию.
    /// </summary>
    [Serializable]
    public class CFileSystemDirectory : Nameable, ILotusOwnerObject, ILotusFileSystemEntity,
        ILotusViewModelBuilder, ILotusModelExpanded
    {
        #region Static methods
        /// <summary>
        /// Построение дерева элементов файловой системы по указанному пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Узел дерева представляющего собой директорию.</returns>
        public static CFileSystemDirectory Build(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            var dirModel = new CFileSystemDirectory(dirInfo);
            dirModel.RecursiveFileSystemInfo();
            return dirModel;
        }
        #endregion

        #region Fields
        protected internal DirectoryInfo _info;
        protected internal ListArray<ILotusFileSystemEntity> _entities;
        protected internal CParameters _parameters;
        #endregion

        #region Properties
        /// <summary>
        /// Наименование директории.
        /// </summary>
        public override string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(PropertyArgsName);
                RaiseNameChanged();
            }
        }

        /// <summary>
        /// Список вложенных директорий и файлов.
        /// </summary>
        public ListArray<ILotusFileSystemEntity> Entities
        {
            get { return _entities; }
        }

        /// <summary>
        /// Полное имя(полный путь) элемента файловой системы.
        /// </summary>
        public string FullName
        {
            get
            {
                if (_info != null)
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
        /// Информация о директории.
        /// </summary>
        public DirectoryInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="directoryInfo">Информация о директории.</param>
        public CFileSystemDirectory(DirectoryInfo directoryInfo)
            : this(directoryInfo.Name, directoryInfo)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="fullPath">Полный путь к директории.</param>
        public CFileSystemDirectory(string fullPath)
        {
            _info = new DirectoryInfo(fullPath);
            _name = _info.Name;
            _entities = new ListArray<ILotusFileSystemEntity>
            {
                IsNotify = true
            };
            if (_info != null)
            {
                var path = Path.Combine(_info.FullName, "Info.json");
                if (File.Exists(path))
                {
                    _parameters = new CParameters();
                    _parameters.Load(path);
                }
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="displayName">Название узла.</param>
        /// <param name="directoryInfo">Информация о директории.</param>
        public CFileSystemDirectory(string displayName, DirectoryInfo directoryInfo)
            : base(displayName)
        {
            _info = directoryInfo;
            _entities = new ListArray<ILotusFileSystemEntity>
            {
                IsNotify = true
            };

            if (_info != null)
            {
                var path = Path.Combine(_info.FullName, "Info.json");
                if (File.Exists(path))
                {
                    _parameters = new CParameters();
                    _parameters.Load(path);
                }
            }
        }
        #endregion

        #region ILotusOwnerObject methods
        /// <summary>
        /// Присоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="add">Статус добавления в коллекцию.</param>
        public void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add)
        {

        }

        /// <summary>
        /// Отсоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="remove">Статус удаления из коллекции.</param>
        public void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove)
        {

        }

        /// <summary>
        /// Обновление связей для зависимых объектов.
        /// </summary>
        public void UpdateOwnedObjects()
        {

        }

        /// <summary>
        /// Информирование данного объекта о начале изменения данных указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого будут меняться.</param>
        /// <param name="dataName">Имя данных.</param>
        /// <returns>Статус разрешения/согласования изменения данных.</returns>
        public bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object? data, string dataName)
        {
            return true;
        }

        /// <summary>
        /// Информирование данного объекта об окончании изменении данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого изменились.</param>
        /// <param name="dataName">Имя данных.</param>
        public void OnNotifyUpdated(ILotusOwnedObject ownedObject, object? data, string dataName)
        {

        }
        #endregion

        #region ILotusFileSystemEntity methods
        /// <summary>
        /// Проверка объекта на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Объект удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        public bool CheckOne(Predicate<ILotusFileSystemEntity?> match)
        {
            if (match(this))
            {
                return true;
            }
            else
            {
                for (var i = 0; i < _entities.Count; i++)
                {
                    if (_entities[i].CheckOne(match))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region ILotusViewModelBuilder methods
        /// <summary>
        /// Получение количества дочерних узлов.
        /// </summary>
        /// <returns>Количество дочерних узлов.</returns>
        public int GetCountChildrenNode()
        {
            return _entities.Count;
        }

        /// <summary>
        /// Получение дочернего узла по индексу.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Дочерней узел.</returns>
        public object GetChildrenNode(int index)
        {
            return _entities[index];
        }
        #endregion

        #region ILotusModelExpanded methods
        /// <summary>
        /// Установка статуса раскрытия объекта.
        /// </summary>
        /// <param name="viewModel">Элемент ViewModel.</param>
        /// <param name="expanded">Статус раскрытия объекта.</param>
        public void SetModelExpanded(ILotusViewModelHierarchy viewModel, bool expanded)
        {
            if (expanded)
            {
                foreach (var entity in _entities)
                {
                    if (entity is CFileSystemDirectory directory)
                    {
                        directory.GetFileSystemItemsTwoLevel();
                    }
                }

                viewModel.BuildFromModel();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление директории.
        /// </summary>
        /// <param name="directoryInfo">Информация о директории.</param>
        /// <returns>Элемент файловой системы представляющий собой директорию.</returns>
        protected CFileSystemDirectory? AddDirectory(DirectoryInfo? directoryInfo)
        {
            if (directoryInfo == null) return null;

            // Не создаем ненужные директории
            if (directoryInfo.Name.Contains(".git")) return null;
            if (directoryInfo.Name.Contains(".vs")) return null;

            // Создаем директорию
            var directory = new CFileSystemDirectory(directoryInfo);

            // Добавляем
            _entities.Add(directory);

            return directory;
        }

        /// <summary>
        /// Добавление файла.
        /// </summary>
        /// <param name="fileInfo">Информация о файле.</param>
        /// <returns>Элемент файловой системы представляющий собой файл.</returns>
        protected CFileSystemFile? AddFile(FileInfo fileInfo)
        {
            // Не создаем ненужные файлы
            if (fileInfo.Extension == ".meta") return null;

            // Создаем файл
            var file = new CFileSystemFile(fileInfo);

            // Добавляем
            _entities.Add(file);

            return file;
        }

        /// <summary>
        /// Получение файлов и дочерних директорий в текущей директории.
        /// </summary>
        public void GetFileSystemItems()
        {
            _entities.Clear();

            var dirsInfo = Info.GetDirectories();
            var filesInfo = Info.GetFiles();

            // Сначала директории
            for (var i = 0; i < dirsInfo.Length; i++)
            {
                var dirInfo = dirsInfo[i];
                AddDirectory(dirInfo);
            }

            // Теперь файлы
            for (var i = 0; i < filesInfo.Length; i++)
            {
                var fileInfo = filesInfo[i];
                AddFile(fileInfo);
            }
        }

        /// <summary>
        /// Получение файлов и дочерних директорий в текущей директории и всех дочерних директориях.
        /// </summary>
        public void GetFileSystemItemsTwoLevel()
        {
            _entities.Clear();

            var dirsInfo = Info.GetDirectories();
            var filesInfo = Info.GetFiles();

            // Сначала директории
            for (var i = 0; i < dirsInfo.Length; i++)
            {
                var dirInfo = dirsInfo[i];
                var directory = AddDirectory(dirInfo);
                directory?.GetFileSystemItems();
            }

            // Теперь файлы
            for (var i = 0; i < filesInfo.Length; i++)
            {
                var fileInfo = filesInfo[i];
                AddFile(fileInfo);
            }
        }

        /// <summary>
        /// Получение файлов и дочерних директорий в текущей директории и всех дочерних директориях.
        /// </summary>
        /// <returns>Статус получение файлов.</returns>
        public bool AddFileSystemItemsTwoLevel()
        {
            var status = false;
            for (var i = 0; i < _entities.Count; i++)
            {
                if (_entities[i] is CFileSystemDirectory directory)
                {
                    if (directory.Entities.Count == 0)
                    {
                        directory.GetFileSystemItems();
                        status = true;
                    }
                }
            }

            return status;
        }

        /// <summary>
        /// Переименовать директорию.
        /// </summary>
        /// <param name="newDirectoryName">Новое имя директории.</param>
        public void Rename(string newDirectoryName)
        {

        }

        /// <summary>
        /// Рекурсивное получение данных элементов файловой системы.
        /// </summary>
        public void RecursiveFileSystemInfo()
        {
            _entities.Clear();
            RecursiveFileSystemInfo(this);
        }

        /// <summary>
        /// Рекурсивное получение данных элементов файловой системы на 2 уровня ниже.
        /// </summary>
        public void RecursiveFileSystemInfoTwoLevel()
        {
            var dirsInfo = Info.GetDirectories();
            var filesInfo = Info.GetFiles();

            // Сначала директории
            for (var i = 0; i < dirsInfo.Length; i++)
            {
                var dirInfo = dirsInfo[i];
                var subDirectory = AddDirectory(dirInfo);

                //// 2 уровень
                //for (Int32 i2 = 0; i2 < dirsInfo.Length; i2++)
                //{
                //	//DirectoryInfo[] subDirectories2 = dirsInfo[i2].GetDirectories();
                //	//FileInfo[] files2 = dirsInfo[i2].GetFiles();

                //	//// Сначала директории
                //	//for (Int32 i2d = 0; i2d < subDirectories2.Length; i2d++)
                //	//{
                //	//	CFileSystemDirectory subDirectoryNode2 = new CFileSystemDirectory(subDirectories2[i2d]);

                //	//	if (subDirectoryNode2.Name.Contains(".git")) continue;
                //	//	if (subDirectoryNode2.Name.Contains(".vs")) continue;

                //	//	subDirectoryNode.Entities.Add(subDirectoryNode2);
                //	//}
                //}
            }

            // Теперь файлы
            for (var i = 0; i < filesInfo.Length; i++)
            {
                var fileInfo = filesInfo[i];
                AddFile(fileInfo);
            }
        }

        /// <summary>
        /// Рекурсивная обработка объектов файловой системы.
        /// </summary>
        /// <param name="parentDirectoryNode">Родительский узел директории.</param>
        protected void RecursiveFileSystemInfo(CFileSystemDirectory parentDirectoryNode)
        {
            var subDirectories = parentDirectoryNode.Info.GetDirectories();
            var files = parentDirectoryNode.Info.GetFiles();

            // Сначала директории
            for (var i = 0; i < subDirectories.Length; i++)
            {
                var subDirectoryNode = new CFileSystemDirectory(subDirectories[i]);

                if (subDirectoryNode.Name.Contains(".git")) continue;
                if (subDirectoryNode.Name.Contains(".vs")) continue;

                _entities.Add(subDirectoryNode);

                subDirectoryNode.RecursiveFileSystemInfo(subDirectoryNode);
            }

            // Теперь файлы
            for (var i = 0; i < files.Length; i++)
            {
                var fileInfo = files[i];

                if (fileInfo.Extension == ".meta") continue;

                var fileNode = new CFileSystemFile(fileInfo);
                _entities.Add(fileNode);
            }
        }

        /// <summary>
        /// Проверка на существовании директории среди дочерних объектов.
        /// </summary>
        /// <param name="dirInfo">Информация о директории.</param>
        /// <returns>Статус существования.</returns>
        public bool ExistInChildren(DirectoryInfo dirInfo)
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                var dirNode = _entities[i] as CFileSystemDirectory;
                if (dirNode != null)
                {
                    if (dirNode.Info.Name == dirInfo.Name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка на существовании файла среди дочерних объектов.
        /// </summary>
        /// <param name="fileInfo">Информация о файле.</param>
        /// <returns>Статус существования.</returns>
        public bool ExistInChildren(FileInfo fileInfo)
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                var fileNode = _entities[i] as CFileSystemFile;
                if (fileNode != null && fileNode.Info != null)
                {
                    if (fileNode.Info.Name == fileInfo.Name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Поиск и получение узла директории среди дочерних объектов.
        /// </summary>
        /// <param name="dirInfo">Информация о директории.</param>
        /// <returns>Узел директории.</returns>
        public CFileSystemDirectory? GetDirectoryNodeFromChildren(DirectoryInfo dirInfo)
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                var dirNode = _entities[i] as CFileSystemDirectory;
                if (dirNode != null && dirNode.Info != null)
                {
                    if (dirNode.Info.Name == dirInfo.Name)
                    {
                        return dirNode;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск и получение узла файла среди дочерних объектов.
        /// </summary>
        /// <param name="fileInfo">Информация о файле.</param>
        /// <returns>Узел файла.</returns>
        public CFileSystemFile? GetFileNodeFromChildren(FileInfo fileInfo)
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                var fileNode = _entities[i] as CFileSystemFile;
                if (fileNode != null && fileNode.Info != null)
                {
                    if (fileNode.Info.Name == fileInfo.Name)
                    {
                        return fileNode;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Копирование файлов и указанную директорию.
        /// </summary>
        /// <param name="path">Имя директории.</param>
        /// <param name="isDirectoryName">С учетом данной директории.</param>
        public void Copy(string path, bool isDirectoryName)
        {
            if (isDirectoryName)
            {
                var destPathDir = Path.Combine(path, Info.Name);
                if (Directory.Exists(destPathDir) == false)
                {
                    Directory.CreateDirectory(destPathDir);
                }
            }

            for (var i = 0; i < _entities.Count; i++)
            {
                var fileNode = _entities[i] as CFileSystemFile;
                if (fileNode != null && fileNode.Info != null)
                {
                    if (isDirectoryName)
                    {
                        var destPath = Path.Combine(path, Info.Name, fileNode.Info.Name);
                        File.Copy(fileNode.Info.FullName, destPath);
                    }
                    else
                    {
                        var destPath = Path.Combine(path, fileNode.Info.Name);
                        File.Copy(fileNode.Info.FullName, destPath);
                    }
                }
            }
        }

        /// <summary>
        /// Вставить указанную директорию и все ее содержимое.
        /// </summary>
        /// <param name="directory">Директория.</param>
        public void PasteFrom(DirectoryInfo directory)
        {
            //Now Create all of the directories
            var sourcePath = directory.FullName;
            var targetPath = Path.Combine(_info.FullName, directory.Name);

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }

            RecursiveFileSystemInfo();
        }

        /// <summary>
        /// Удаление директории.
        /// </summary>
        public void Delete()
        {
        }
        #endregion
    }
    /**@}*/
}