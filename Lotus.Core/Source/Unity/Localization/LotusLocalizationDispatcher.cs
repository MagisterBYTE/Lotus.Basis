#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
using UnityEngine.Serialization;

namespace Lotus.Core
{
    
    /** \addtogroup CoreUnityLocalization
    *@{*/
    
    /// <summary>
    /// Центральный диспетчер для локализации ресурсов
    /// </summary>
    
    [Serializable]
    public class LotusLocalizationDispatcher : LotusSystemSingleton<LotusLocalizationDispatcher>, ISerializationCallbackReceiver,
        ILotusSingleton, ILotusMessageHandler
    {
        #region Constants
        /// <summary>
        /// Начальный символ обрамления токена ключа
        /// </summary>
        public const char SYMBOL_KEY_START = '[';

        /// <summary>
        /// Основной символ разделителя
        /// </summary>
        public const char SYMBOL_KEY_DELIMITER = '#';

        /// <summary>
        /// Основной символ разделителя
        /// </summary>
        [Obsolete("Опечатка в имени. Используйте SYMBOL_KEY_DELIMITER")]
        public const char SYMBOL_KEY_DELIMETR = SYMBOL_KEY_DELIMITER;

        /// <summary>
        /// Конечный символ обрамления токена ключа
        /// </summary>
        public const char SYMBOL_KEY_END = ']';
        #endregion

        #region Static events
        /// <summary>
        /// Глобальное событие для нотификации о смене языка. Аргумент - новый язык
        /// </summary>
        public static event Action<CLanguageInfo> OnLanguageChanged = delegate { };
        #endregion

        #region Static methods
        
        /// <summary>
        /// Смена языка
        /// </summary>
        /// <param name="languageName">Имя языка</param>
        
        public static void ChangedLanguage(string languageName)
        {
            Instance.ChangedLang(languageName);

            // Вызываем событие
            OnLanguageChanged(null);
        }

        
        /// <summary>
        /// Смена языка
        /// </summary>
        /// <param name="languageIndex">Индекс языка в списке</param>
        
        public static void ChangedLanguage(int languageIndex)
        {
            Instance.ChangedLang(_instance._languages[languageIndex].Name);

            // Вызываем событие
            OnLanguageChanged(null);
        }

        
        /// <summary>
        /// Получение текстового ресурса по умолчанию по ключу локализации
        /// </summary>
        /// <param name="id">Ключ локализации текстового ресурса</param>
        /// <returns>Текстовый ресурс по умолчанию</returns>
        
        public static string GetTextDefaultFromID(long id)
        {
            var result = "";
            if (Instance._localizeDictionaryDefault.TryGetValue(id, out result))
            {
                return result;
            }

            return "";
        }

        
        /// <summary>
        /// Получение текстового ресурса по умолчанию по номеру (индексу)
        /// </summary>
        /// <param name="number">Последовательный номер текстового ресурса</param>
        /// <returns>Текстовый ресурс по умолчанию</returns>
        
        public static string GetTextDefaultFromNumber(int number)
        {
            return Instance._localizeListDefault[number];
        }

        
        /// <summary>
        /// Получение ключа локализации текстового ресурса по умолчанию
        /// </summary>
        /// <param name="textDefault">Текстовый ресурс по умолчанию</param>
        /// <returns>Ключ локализации текстового ресурса или ноль если его нет</returns>
        
        public static long GetIDFromTextDefault(string textDefault)
        {
            return Instance._localizeDictionaryDefault.FirstOrDefault(pair => pair.Value == textDefault).Key;
        }

        
        /// <summary>
        /// Получение последовательного номера (индекса) текстового ресурса по умолчанию
        /// </summary>
        /// <param name="textDefault">Текстовый ресурс по умолчанию</param>
        /// <returns>Последовательный номер (индекс) текстового ресурса</returns>
        
        public static int GetNumberFromTextDefault(string textDefault)
        {
            return Instance._localizeListDefault.IndexOf(textDefault);
        }

        
        /// <summary>
        /// Получение текущего текстового ресурса по ключу локализации
        /// </summary>
        /// <param name="id">Ключ локализации текстового ресурса</param>
        /// <returns>Текущий текстовый ресурс</returns>
        
        public static string GetTextCurrentFromID(long id)
        {
            string result;
            if (Instance._localizeDictionaryCurrent.TryGetValue(id, out result))
            {
                return result;
            }

            return "";
        }

        
        /// <summary>
        /// Получение текущего текстового ресурса по ключу локализации
        /// </summary>
        /// <param name="id">Ключ локализации текстового ресурса</param>
        /// <param name="result">Текущий текстовый ресурс</param>
        
        public static void GetTextCurrentFromID(long id, out string result)
        {
            Instance._localizeDictionaryCurrent.TryGetValue(id, out result);
        }

        
        /// <summary>
        /// Получение текущего текстового ресурса по номеру (индексу)
        /// </summary>
        /// <param name="number">Последовательный номер текстового ресурса</param>
        /// <returns>Текущий текстовый ресурс</returns>
        
        public static string GetTextCurrentFromNumber(int number)
        {
            return Instance._localizeListCurrent[number];
        }

        
        /// <summary>
        /// Получение текущего текстового ресурса по номеру (индексу)
        /// </summary>
        /// <param name="number">Последовательный номер текстового ресурса</param>
        /// <param name="result">Текущий текстовый ресурс</param>
        
        public static void GetTextCurrentFromNumber(int number, out string result)
        {
            result = Instance._localizeListCurrent[number];
        }

        
        /// <summary>
        /// Получение текущего текстового ресурса по текстовому ресурсу по умолчанию
        /// </summary>
        /// <param name="textDefault">Текстовый ресурс по умолчанию</param>
        /// <returns>Текущий текстовый ресурс</returns>
        
        public static string GetTextCurrentFromTextDefault(string textDefault)
        {
            return Instance.GetLocalizeResource(textDefault);
        }
        #endregion

        #region Fields
        // Системные параметры
        [SerializeField]
        [FormerlySerializedAs("mIsMainInstance")]
        internal bool _isMainInstance;
        [SerializeField]
        [FormerlySerializedAs("mDestroyMode")]
        internal TSingletonDestroyMode _destroyMode;
        [SerializeField]
        [FormerlySerializedAs("mIsDontDestroy")]
        internal bool _isDontDestroy;
        [SerializeField]
        [FormerlySerializedAs("mGroupMessage")]
        internal string _groupMessage = "Localization";

        // Основные параметры
        [SerializeField]
        [FormerlySerializedAs("mLanguages")]
        internal List<CLanguageInfo> _languages;
        [NonSerialized]
        internal int _currentIndexLanguage;
        [NonSerialized]
        internal Dictionary<long, string> _localizeDictionaryDefault;
        [NonSerialized]
        internal Dictionary<long, string> _localizeDictionaryCurrent;
        [NonSerialized]
        internal List<string> _localizeListDefault;
        [NonSerialized]
        internal List<string> _localizeListCurrent;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        [FormerlySerializedAs("mExpandedLanguages")]
        internal bool _expandedLanguages;
        [SerializeField]
        [FormerlySerializedAs("mExpandedTestTranslator")]
        internal bool _expandedTestTranslator;
        [SerializeField]
        [FormerlySerializedAs("mExpandedTranslation")]
        internal bool _expandedTranslation;
        [SerializeField]
        [FormerlySerializedAs("mListTranslate")]
        internal List<CTextTranslate> _listTranslate;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Язык текста по умолчанию
        /// </summary>
        public CLanguageInfo DefaultLanguage
        {
            get { return _languages[0]; }
        }

        /// <summary>
        /// Текущий язык
        /// </summary>
        public CLanguageInfo CurrentLanguage
        {
            get { return _languages[_currentIndexLanguage]; }
        }

        /// <summary>
        /// Индекс текущего языка
        /// </summary>
        public int CurrentLanguageIndex
        {
            get { return _currentIndexLanguage; }
        }

        /// <summary>
        /// Список поддерживаемых языков
        /// </summary>
        public List<CLanguageInfo> Languages
        {
            get { return _languages; }
        }

        /// <summary>
        /// Словарь текстовых данных языка по умолчанию
        /// </summary>
        public Dictionary<long, string> LocalizeDictionaryDefault
        {
            get { return _localizeDictionaryDefault; }
        }

        /// <summary>
        /// Словарь текстовых данных текущего языка
        /// </summary>
        public Dictionary<long, string> LocalizeDictionaryCurrent
        {
            get { return _localizeDictionaryCurrent; }
        }

        /// <summary>
        /// Список текстовых данных языка по умолчанию
        /// </summary>
        public List<string> LocalizeListDefault
        {
            get { return _localizeListDefault; }
        }

        /// <summary>
        /// Список текстовых данных текущего языка
        /// </summary>
        public List<string> LocalizeListCurrent
        {
            get { return _localizeListCurrent; }
        }
        #endregion

        #region ILotusSingleton properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Статус основного экземпляра
        /// </summary>
        public bool IsMainInstance
        {
            get
            {
                return _isMainInstance;
            }
            set
            {
                _isMainInstance = value;
            }
        }

        /// <summary>
        /// Статус удаления игрового объекта
        /// </summary>
        /// <remarks>
        /// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
        /// </remarks>
        public TSingletonDestroyMode DestroyMode
        {
            get
            {
                return _destroyMode;
            }
            set
            {
                _destroyMode = value;
            }
        }

        /// <summary>
        /// Не удалять объект когда загружается новая сцена
        /// </summary>
        public bool IsDontDestroy
        {
            get
            {
                return _isDontDestroy;
            }
            set
            {
                _isDontDestroy = value;
            }
        }
        #endregion

        #region ILotusMessageHandler properties
        /// <summary>
        /// Группа которой принадлежит данный обработчик событий
        /// </summary>
        public string MessageHandlerGroup
        {
            get
            {
                return (_groupMessage);
            }
        }
        #endregion

        #region Unity events
        
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        
        public void Reset()
        {
            if (_languages == null)
            {
                _languages = new List<CLanguageInfo>
                {
                    new CLanguageInfo(CLanguageInfo.LanguageNames[0], CLanguageInfo.LanguageAbbrs[0]),
                    new CLanguageInfo(CLanguageInfo.LanguageNames[1], CLanguageInfo.LanguageAbbrs[1])
                };
            }

            if (_localizeListDefault == null)
            {
                // Создаем список
                _localizeListDefault = new List<string>();

                // Создаем словарь
                _localizeDictionaryDefault = new Dictionary<long, string>();

                // Загружаем данные
                if (DefaultLanguage.FileData == null)
                {
                    Debug.LogErrorFormat("Default Language <{0}> not data", DefaultLanguage.Name);
                    return;
                }

                // Загружаем данные
                List<KeyValuePair<string, string>> data = LoadLocalizeDataFromFile(DefaultLanguage.FileData);

                // Загружаем данные в список и словарь
                LoadData(data, ref _localizeDictionaryDefault, ref _localizeListDefault);
            }
        }

        
        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        
        public void Awake()
        {
            if (_languages == null)
            {
                _languages = new List<CLanguageInfo>
                {
                    new CLanguageInfo(CLanguageInfo.LanguageNames[0], CLanguageInfo.LanguageAbbrs[0]),
                    new CLanguageInfo(CLanguageInfo.LanguageNames[1], CLanguageInfo.LanguageAbbrs[1])
                };
            }

            if (_localizeListDefault == null)
            {
                // Создаем список
                _localizeListDefault = new List<string>();

                // Создаем словарь
                _localizeDictionaryDefault = new Dictionary<long, string>();

                // Загружаем данные
                if (DefaultLanguage.FileData == null)
                {
                    Debug.LogErrorFormat("Default Language <{0}> not data", DefaultLanguage.Name);
                    return;
                }

                // Загружаем данные
                List<KeyValuePair<string, string>> data = LoadLocalizeDataFromFile(DefaultLanguage.FileData);

                // Загружаем данные в список и словарь
                LoadData(data, ref _localizeDictionaryDefault, ref _localizeListDefault);
            }

            // Инициализация данных
            if (!CheckDuplicate())
            {

            }

            if (_isDontDestroy)
            {
                GameObject.DontDestroyOnLoad(this.gameObject);
            }
        }

        
        /// <summary>
        /// Включение компонента
        /// </summary>
        
        public void OnEnable()
        {

        }

        
        /// <summary>
        /// Старт скрипта
        /// </summary>
        
        public void Start()
        {

        }

        
        /// <summary>
        /// Рисование UnityGUI
        /// </summary>
        
        public void OnGUI()
        {

        }

        
        /// <summary>
        /// Отключение компонента
        /// </summary>
        
        public void OnDisable()
        {
        }

        
        /// <summary>
        /// Игровой объект уничтожился
        /// </summary>
        
        public void OnDestroy()
        {
        }
        #endregion

        #region Serialization methods
        
        /// <summary>
        /// Процесс перед сериализацией объекта
        /// </summary>
        
        public void OnBeforeSerialize()
        {

        }

        
        /// <summary>
        /// Процесс после сериализацией объекта
        /// </summary>
        
        public void OnAfterDeserialize()
        {

        }
        #endregion

        #region Message handler methods
        
        /// <summary>
        /// Основной метод для обработки сообщения
        /// </summary>
        /// <param name="args">Аргументы сообщения</param>
        /// <returns>Статус успешности обработки сообщений</returns>
        
        public int OnMessageHandler(CMessageArgs args)
        {
            return (10);
        }
        #endregion

        #region Main methods
        
        /// <summary>
        /// Получение текущего текстового ресурса по текстовому ресурсу по умолчанию
        /// </summary>
        /// <param name="text">Текстовый ресурс по умолчанию</param>
        /// <returns>Текущий текстовый ресурс</returns>
        
        public string GetLocalizeResource(string text)
        {
            if (_localizeListCurrent != null && _localizeListDefault != null)
            {
                // Ищем совпадение в предыдущем списке
                for (var i = 0; i < _localizeListDefault.Count; i++)
                {
                    var textDefault = _localizeListDefault[i];

                    // Символ перевода каретки убираем
                    if (text.IndexOf(XCharHelper.CarriageReturn) > -1)
                    {
                        text = text.Replace("\r", "");
                    }

                    if (string.CompareOrdinal(textDefault, text) == 0)
                    {
                        return _localizeListCurrent[i];
                    }
                }
            }

            return text;
        }

        
        /// <summary>
        /// Смена языка
        /// </summary>
        /// <param name="languageName">Имя языка</param>
        
        public void ChangedLang(string languageName)
        {
            // Смена языка идет первый раз
            if (_localizeListCurrent == null)
            {
                _localizeListCurrent = new List<string>(_localizeListDefault.Count);
            }
            else
            {
                // Копируем с предыдущего
                _localizeListDefault.Clear();
                _localizeListDefault.AddRange(_localizeListCurrent);
                _localizeListCurrent.Clear();
            }

            if (_localizeDictionaryCurrent == null)
            {
                _localizeDictionaryCurrent = new Dictionary<long, string>();
            }
            else
            {
                // Копируем с предыдущего
                _localizeDictionaryDefault.Clear();

                foreach (var key in _localizeDictionaryCurrent.Keys)
                {
                    _localizeDictionaryDefault.Add(key, _localizeDictionaryCurrent[key]);
                }

                _localizeDictionaryCurrent.Clear();
            }

            // Ищем язык
            CLanguageInfo lang = FindLang(languageName);
            if (lang == null)
            {
                Debug.LogErrorFormat("Language <{0}> not found", languageName);
                return;
            }

            // Индекс языка
            _currentIndexLanguage = _languages.IndexOf(lang);

            // Загружаем данные
            if (lang.FileData == null)
            {
                Debug.LogErrorFormat("Language <{0}> not data", languageName);
                return;
            }

            List<KeyValuePair<string, string>> data = LoadLocalizeDataFromFile(lang.FileData);

            if (data.Count != _localizeListDefault.Count + _localizeDictionaryDefault.Count)
            {
                Debug.LogErrorFormat("Language <{0}> data do not converge", languageName);
                return;
            }

            LoadData(data, ref _localizeDictionaryCurrent, ref _localizeListCurrent);
        }

        
        /// <summary>
        /// Поиск языка для локализации
        /// </summary>
        /// <param name="languageName">Имя языка</param>
        /// <returns>Найденный язык или null</returns>
        
        public CLanguageInfo FindLang(string languageName)
        {
            for (var i = 0; i < _languages.Count; i++)
            {
                if (_languages[i].Name == languageName)
                {
                    return _languages[i];
                }
            }

            return null;
        }

        
        /// <summary>
        /// Добавление языка для локализации
        /// </summary>
        /// <param name="languageName">Имя языка</param>
        
        public void AddLang(string languageName)
        {
            _languages.Add(new CLanguageInfo(languageName, languageName.Substring(0, 2).ToLower()));
        }

        
        /// <summary>
        /// Очистка списка языков
        /// </summary>
        
        public void ClearLangs()
        {
            CLanguageInfo langDefault = DefaultLanguage;
            _languages.Clear();
            _languages.Add(langDefault);
        }
        #endregion

        #region Data methods
        
        /// <summary>
        /// Получение всех данных полей которые поддерживают локализацию
        /// </summary>
        /// <param name="fields">Массив полей</param>
        /// <param name="obj">Экземпляр типа</param>
        
        internal void GetAllLocalizeDataFromFields(System.Reflection.FieldInfo[] fields, object obj)
        {
            for (var i = 0; i < fields.Length; i++)
            {
                // Если наш тип строковый и поддерживает локализацию
                if (fields[i].FieldType == typeof(string) && fields[i].HasAttribute<LotusLocalizationAttribute>())
                {
                    //1) Получаем текст
                    var value = (string)fields[i].GetValue(obj);

                    //2) Добавляем в базу
                    _localizeListDefault.Add(value);
                }

                // Если наш тип прямо поддерживает локализацию
                if (fields[i].FieldType == typeof(TLocalizableText))
                {
                    //1) Получаем данные
                    var strLoc = (TLocalizableText)fields[i].GetValue(obj);

                    //2) Получаем ключ по хэш коду
                    var key = strLoc.IDKeyLocalize;
                    if (key != 0 && key != -1)
                    {
                        //3) Добавляем в базу
                        if (!_localizeDictionaryDefault.ContainsKey(key))
                        {
                            _localizeDictionaryDefault.Add(key, strLoc.Text);
                        }
                    }
                    else
                    {
                        // Только если объект участвует в локализации
                        if (key != -1)
                        {
                            // Добавляем в список
                            _localizeListDefault.Add(strLoc.Text);
                        }
                    }
                }
            }
        }

        
        /// <summary>
        /// Получение всех данных свойств которые поддерживают локализацию
        /// </summary>
        /// <param name="properties">Массив полей</param>
        /// <param name="obj">Экземпляр типа</param>
        
        internal void GetAllLocalizeDataFromProperties(System.Reflection.PropertyInfo[] properties, object obj)
        {
            for (var i = 0; i < properties.Length; i++)
            {
                // Если наш тип строковый и поддерживает локализацию
                if (properties[i].PropertyType == typeof(string) && properties[i].HasAttribute<LotusLocalizationAttribute>())
                {
                    //1) Получаем текст
                    var value = (string)properties[i].GetValue(obj, null);

                    //2) Добавляем в базу
                    _localizeListDefault.Add(value);
                }

                // Если наш тип прямо поддерживает локализацию
                if (properties[i].PropertyType == typeof(TLocalizableText))
                {
                    //1) Получаем данные
                    var strLoc = (TLocalizableText)properties[i].GetValue(obj, null);

                    //2) Получаем ключ по хэш коду
                    var key = strLoc.IDKeyLocalize;
                    if (key != 0 && key != -1)
                    {
                        //3) Добавляем в базу
                        if (!_localizeDictionaryDefault.ContainsKey(key))
                        {
                            _localizeDictionaryDefault.Add(key, strLoc.Text);
                        }
                    }
                    else
                    {
                        // Только если объект участвует в локализации
                        if (key != -1)
                        {
                            // Добавляем в список
                            _localizeListDefault.Add(strLoc.Text);
                        }
                    }
                }
            }
        }

        
        /// <summary>
        /// Получение всех данных которые поддерживают локализацию
        /// </summary>
        /// <remarks>
        /// Метод берет список всех компонентов производных от MonoBehaviour и:
        /// 1) Если компонент реализует интерфейс ILotusLocalizable то добавляется в базу локализации
        /// 2) Если поле/свойство имеет строковый тип и атрибут LotusLocalizationAttribute то добавляется в базу локализации
        /// 3) Если поле/свойство имеет тип TStringLocalizable то добавляется в базу локализации
        /// 4) Если поле/свойство имеет тип класса или структуры и его поля/свойства поддерживают п.2 или 3 то добавляется в базу локализации
        /// </remarks>
        [Obsolete("XReflection отключён: fields/properties = null — метод падает с NRE. Требуется новая реализация обхода сериализуемых членов")]
        internal void GetAllLocalizeData()
        {
#if UNITY_EDITOR
            // Подготавливаем базу
            if (_localizeListDefault == null)
            {
                _localizeListDefault = new List<string>();
            }
            if (_localizeListDefault.Count > 0)
            {
                _localizeListDefault.Clear();
            }
            if (_localizeDictionaryDefault == null)
            {
                _localizeDictionaryDefault = new Dictionary<long, string>();
            }
            if (_localizeDictionaryDefault.Count > 0)
            {
                _localizeDictionaryDefault.Clear();
            }

            // Добавляем все элементы (в том числе и неактивные)
            MonoBehaviour[] elements = Resources.FindObjectsOfTypeAll<MonoBehaviour>();
            for (var i = 0; i < elements.Length; i++)
            {
                if (elements[i] != null)
                {
                    // Отдельно обрабатываем интерфейс
                    var localizable = elements[i] as ILotusLocalizable;
                    if (localizable != null)
                    {
                        //1) Получаем текст
                        var value = localizable.TextLocalize;

                        //2) Получаем ключ по хэш коду
                        var key = localizable.IDKeyLocalize;
                        if (key != 0 && key != -1)
                        {
                            //3) Добавляем в базу
                            if (!_localizeDictionaryDefault.ContainsKey(key))
                            {
                                _localizeDictionaryDefault.Add(key, value);
                            }
                        }
                        else
                        {
                            // Только если объект участвует в локализации
                            if (key != -1)
                            {
                                // Добавляем в список
                                _localizeListDefault.Add(value);
                            }
                        }
                    }

                    // Обрабатываем поля первого уровня
                    System.Reflection.FieldInfo[] fields = null;// XReflection.GetSerializeData(elements[i].GetType()).Fields;
                    GetAllLocalizeDataFromFields(fields, elements[i]);

                    // Обрабатываем поля второго уровня
                    for (var f = 0; f < fields.Length; f++)
                    {
                        try
                        {
                            // Ссылки не обрабатываем
                            if (fields[f].FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
                                continue;

                            // Если наш тип сложный
                            if ((fields[f].FieldType.IsClass || fields[f].FieldType.IsValueType)
                                && fields[f].FieldType.IsPrimitive == false
                                && fields[f].FieldType.IsEnum == false)
                            {
                                // 1) Объект родитель
                                var objParent = fields[f].GetValue(elements[i]);

                                if (objParent != null)
                                {
                                    System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;

                                    // 2) Получаем поля
                                    System.Reflection.FieldInfo[] fieldsTwo = fields[f].FieldType.GetFields(flags);

                                    // 3) Обрабатываем поля
                                    GetAllLocalizeDataFromFields(fieldsTwo, objParent);

                                    // 4) Получаем свойства
                                    System.Reflection.PropertyInfo[] propertiesTwo = fields[f].FieldType.GetProperties(flags);

                                    // 5) Обрабатываем свойства
                                    GetAllLocalizeDataFromProperties(propertiesTwo, objParent);
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            Debug.LogException(exc);
                        }
                    }

                    // Обрабатываем свойства первого уровня
                    System.Reflection.PropertyInfo[] properties = null;// XReflection.GetSerializeData(elements[i].GetType()).Properties;
                    GetAllLocalizeDataFromProperties(properties, elements[i]);

                    // Обрабатываем свойства второго уровня
                    for (var p = 0; p < properties.Length; p++)
                    {
                        try
                        {
                            // Ссылки не обрабатываем
                            if (properties[p].PropertyType.IsSubclassOf(typeof(UnityEngine.Object)))
                                continue;

                            // Если наш тип сложный
                            if ((properties[p].PropertyType.IsClass || properties[p].PropertyType.IsValueType)
                                && properties[p].PropertyType.IsPrimitive == false
                                && properties[p].PropertyType.IsEnum == false)
                            {
                                // 1) Объект родитель
                                var objParent = properties[p].GetValue(elements[i], null);

                                if (objParent != null)
                                {
                                    System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;

                                    // 2) Получаем поля
                                    System.Reflection.FieldInfo[] fieldsTwo = properties[p].PropertyType.GetFields(flags);

                                    // 3) Обрабатываем поля
                                    GetAllLocalizeDataFromFields(fieldsTwo, objParent);

                                    // 4) Получаем свойства
                                    System.Reflection.PropertyInfo[] propertiesTwo = properties[p].PropertyType.GetProperties(flags);

                                    // 5) Обрабатываем свойства
                                    GetAllLocalizeDataFromProperties(propertiesTwo, objParent);
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            Debug.LogException(exc);
                        }
                    }
                }
            }

            // Заполняем данные для оперативного перевода
            if (_listTranslate == null) _listTranslate = new List<CTextTranslate>();
            _listTranslate.Clear();

            if (_localizeDictionaryDefault != null)
            {
                // Отображаем данные сначала с ключами
                foreach (var item in _localizeDictionaryDefault.Keys)
                {
                    var tt = new CTextTranslate();
                    tt.IDKeyLocalize = (int)item;
                    tt.Default = _localizeDictionaryDefault[item];
                    _listTranslate.Add(tt);
                }
            }

            if (_localizeListDefault != null)
            {
                // Отображаем просто данные
                for (var i = 0; i < _localizeListDefault.Count; i++)
                {
                    var tt = new CTextTranslate();
                    tt.Default = _localizeListDefault[i];
                    _listTranslate.Add(tt);
                }
            }
#endif
        }

        
        /// <summary>
        /// Создание файла локализации по умолчанию
        /// </summary>
        /// <param name="fileName">Имя файла локализации по умолчанию</param>
        
        internal void CreateLocalizeFileDefault(string fileName)
        {
#if UNITY_EDITOR
            // Подготавливаем базу
            GetAllLocalizeData();

            // Создаем файл локализации по умолчанию
            var writer = new StreamWriter(fileName);

            // 1) Сначала записываем текстовые данные с ключом локализации в формате ##[key]
            writer.WriteLine("///////////////////////////////////");
            writer.WriteLine("// Data from key");
            writer.WriteLine("///////////////////////////////////");
            foreach (var item in _localizeDictionaryDefault)
            {
                var text = item.Value;

                // Записываем данные через разделитель
                writer.Write(XStringHelper.SeparatorFileData);
                writer.Write(SYMBOL_KEY_START);
                writer.Write(item.Key.ToString());
                writer.WriteLine(SYMBOL_KEY_END);

                // Сами данные
                writer.WriteLine(text);
            }

            // 2) Потом записываем данные просто данные
            writer.WriteLine("///////////////////////////////////");
            writer.WriteLine("// Data no key");
            writer.WriteLine("///////////////////////////////////");
            for (var i = 0; i < _localizeListDefault.Count; i++)
            {
                var text = _localizeListDefault[i];

                // Записываем данные через разделитель в формате
                writer.WriteLine(XStringHelper.SeparatorFileData);

                // Сами данные
                writer.WriteLine(text);
            }

            writer.Close();

            // Обновляем в редакторе
            UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
            UnityEditor.EditorUtility.DisplayDialog(XFileDialog.FILE_SAVE_SUCCESSFULLY, "Path\n" + fileName, "OK");

            DefaultLanguage.FileData = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(fileName);
#endif
        }

        
        /// <summary>
        /// Загрузка данных локализации в указанный словарь и список
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="dic">Словарь</param>
        /// <param name="list">Список</param>
        
        internal void LoadData(List<KeyValuePair<string, string>> data, ref Dictionary<long, string> dic, ref List<string> list)
        {
            // Загружаем данные
            for (var i = 0; i < data.Count; i++)
            {
                // Если это ключ - т.е. обрамлен символами []
                if (data[i].Key.IndexOf(SYMBOL_KEY_START) >= 0 && data[i].Key.IndexOf(SYMBOL_KEY_END) > 1)
                {
                    // Читаем ключ
                    var keyText = data[i].Key.Trim(SYMBOL_KEY_START, SYMBOL_KEY_END, SYMBOL_KEY_DELIMITER,
                        XCharHelper.NewLine, XCharHelper.CarriageReturn, XCharHelper.Space);
                    var key = long.Parse(keyText);

                    // Добавляем в словарь
                    dic.Add(key, data[i].Value);
                }
                else
                {
                    list.Add(data[i].Value);
                }
            }
        }

        
        /// <summary>
        /// Загрузка данных локализации
        /// </summary>
        /// <param name="textAsset">Текстовый файл локализации</param>
        /// <returns>Данные локализации</returns>
        
        internal List<KeyValuePair<string, string>> LoadLocalizeDataFromFile(TextAsset textAsset)
        {
            // Разбиваем на токены
            var lines = textAsset.text.Split(XCharHelper.NewLine);

            // Разбиваем на токены
            List<KeyValuePair<string, string>> data = XStringHelper.ConvertLinesToGroupLinesIgnoringComments(lines,
                XStringHelper.SeparatorFileData);

            return data;
        }

        
        /// <summary>
        /// Сохранение переведённых данных в файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        [Obsolete("Метод пустой — сохранение перевода не реализовано")]
        internal void SaveTranslationData(string fileName)
        {
#if UNITY_EDITOR

#endif
        }
        #endregion
    }
    
    /**@}*/
}
#endif
