using System;
using System.IO;

namespace Lotus.Core
{
    /** \addtogroup CoreFileSystem
	*@{*/
    /// <summary>
    /// Элемент файловой системы представляющий собой файл.
    /// </summary>
    [Serializable]
    public class CFileSystemFile : CNameable, ILotusOwnedObject, ILotusFileSystemEntity
    {
        #region Fields
        protected internal ILotusOwnerObject _owner;
        protected internal FileInfo? _info;
        #endregion

        #region Properties
        /// <summary>
        /// Родительский объект владелей.
        /// </summary>
        public ILotusOwnerObject? IOwner
        {
            get { return _owner; }
            set { }
        }

        /// <summary>
        /// Наименование файла.
        /// </summary>
        public override string Name
        {
            get { return _name; }
            set
            {
                try
                {
                    if (_info != null)
                    {
                        var new_file_path = XFilePath.GetPathForRenameFile(_info.FullName, value);
                        File.Move(_info.FullName, new_file_path);
                        _name = value;
                        OnPropertyChanged(PropertyArgsName);
                        RaiseNameChanged();

                    }
                    else
                    {
                        _name = value;
                        OnPropertyChanged(PropertyArgsName);
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
        /// Информация о файле.
        /// </summary>
        public FileInfo? Info
        {
            get { return _info; }
            set { _info = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="fileInfo">Данные о файле.</param>
        public CFileSystemFile(FileInfo fileInfo)
            : base(fileInfo.Name)
        {
            _info = fileInfo;
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
        public bool CheckOne(Predicate<ILotusFileSystemEntity> match)
        {
            return match(this);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Переименовать файл.
        /// </summary>
        /// <param name="newFileName">Новое имя файла.</param>
        public void RenameAssets(string newFileName)
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

        /// <summary>
        /// Модификация имени файла путем удаления его определённой части.
        /// </summary>
        /// <param name="searchOption">Опции поиска.</param>
        /// <param name="check">Проверяемая строка.</param>
        public void ModifyNameOfRemove(TStringSearchOption searchOption, string check)
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

        /// <summary>
        /// Модификация имени файла путем замены его определённой части.
        /// </summary>
        /// <param name="searchOption">Опции поиска.</param>
        /// <param name="source">Искомая строка.</param>
        /// <param name="target">Целевая строка.</param>
        public void ModifyNameOfReplace(TStringSearchOption searchOption, string source, string target)
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

        /// <summary>
        /// Удаление файла.
        /// </summary>
        public void Delete()
        {
            if (_info == null) return;

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
    /**@}*/
}