#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnity
    *@{*/
    /// <summary>
    /// Основной системной диспетчер
    /// </summary>
    /// <remarks>
    /// Основной системной диспетчер обеспечивает централизованное управление (инициализацию и обновления) и
    /// механизм исполнения остальных подсистем.
    /// Он является связующим звеном между центральными диспетчерами различных подсистем и платформой Unity
    /// </remarks>
    [Serializable]
    [AddComponentMenu("Lotus/Core/System Dispatcher")]
    [LotusExecutionOrder(5)]
    public class LotusSystemDispatcher : LotusSystemSingleton<LotusSystemDispatcher>, ISerializationCallbackReceiver,
        ILotusSingleton, ILotusMessageHandler
    {
        #region Constants
        /// <summary>
        /// Имя сообщения для показа/скрытия FPS
        /// </summary>
        public const string MESSAGE_FPS = "FPS";

        /// <summary>
        /// Имя сообщения для показа/скрытия дополнительной информации
        /// </summary>
        public const string MESSAGE_INFO = "Info";
        #endregion

        #region Fields
        // Основные параметры
        [SerializeField]
        internal bool _isMainInstance;
        [SerializeField]
        internal TSingletonDestroyMode _destroyMode;
        [SerializeField]
        internal bool _isDontDestroy;
        [SerializeField]
        internal string _groupMessage = "System";

        // Параметры разработки IMGUI
        [SerializeField]
        internal bool _useIMGUI;
        [SerializeField]
        internal GUISkin _currentSkin;

        // Управление консолью
        [SerializeField]
        internal bool _consoleVisible;
        [SerializeField]
        internal float _consoleHeight = 300f;

        // Данные по FPS
        [SerializeField]
        internal bool _isFpsShow = true;
        [SerializeField]
        internal Rect _fpsPosition = new Rect(5, 5, 100, 25);
        [SerializeField]
        internal float _fpsUpdateInterval = 0.5f;
        [NonSerialized]
        internal GUIStyle _fpsGUIStyle;
        [NonSerialized]
        internal float _fpsMin = 200f;
        [NonSerialized]
        internal float _fpsAccum;
        [NonSerialized]
        internal int _fpsFrames;
        [NonSerialized]
        internal float _fpsTimeStart;
        [NonSerialized]
        internal string _fpsText = string.Empty;
        [NonSerialized]
        internal Action _onUpdateFps;

        // Дополнительная информация
        [SerializeField]
        internal bool _isInfoShow;
        [SerializeField]
        internal Rect _infoPosition;
        [NonSerialized]
        internal string _infoText;

        // Данные в режиме редактора
#if UNITY_EDITOR
        [SerializeField]
        internal int _selectedTab;
#endif
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
            get { return _isMainInstance; }
            set { _isMainInstance = value; }
        }

        /// <summary>
        /// Статус удаления игрового объекта
        /// </summary>
        /// <remarks>
        /// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
        /// </remarks>
        public TSingletonDestroyMode DestroyMode
        {
            get { return _destroyMode; }
            set { _destroyMode = value; }
        }

        /// <summary>
        /// Не удалять объект когда загружается новая сцена
        /// </summary>
        public bool IsDontDestroy
        {
            get { return _isDontDestroy; }
            set { _isDontDestroy = value; }
        }
        #endregion

        #region Properties
        //
        // ПАРАМЕТРЫ РАЗРАБОТКИ IMGUI
        //
        /// <summary>
        /// Использовать графику технологии IMGUI
        /// </summary>
        public bool UseIMGUI
        {
            get { return _useIMGUI; }
            set { _useIMGUI = value; }
        }

        /// <summary>
        /// Текущий визуальный скин для рисования
        /// </summary>
        public GUISkin CurrentSkin
        {
            get { return _currentSkin; }
            set
            {
                _currentSkin = value;
                if (_currentSkin != null && XGUISkin.IsDefaultSkinNull)
                {
                    XGUISkin.DefaultSkin = _currentSkin;
                }
            }
        }

        //
        // КОНСОЛЬ
        //
        /// <summary>
        /// Отображение консоли
        /// </summary>
        public bool ConsoleVisible
        {
            get { return _consoleVisible; }
            set { _consoleVisible = value; }
        }

        /// <summary>
        /// Высота консоли
        /// </summary>
        public float ConsoleHeight
        {
            get { return _consoleHeight; }
            set { _consoleHeight = value; }
        }

        //
        // ДАННЫЕ ПО FPS
        //
        /// <summary>
        /// Количества кадров в секунду
        /// </summary>
        public float FPS
        {
            get { return _fpsMin; }
        }

        /// <summary>
        /// Отображение количества кадров в секунду
        /// </summary>
        public bool IsFpsShow
        {
            get { return _isFpsShow; }
            set { _isFpsShow = value; }
        }

        /// <summary>
        /// Обновление интервала FPS
        /// </summary>
        public float FpsUpdateInterval
        {
            get { return _fpsUpdateInterval; }
            set { _fpsUpdateInterval = value; }
        }

        /// <summary>
        /// Позиция для вывода количества кадров в секунду
        /// </summary>
        public Rect FpsPosition
        {
            get { return _fpsPosition; }
            set { _fpsPosition = value; }
        }

        /// <summary>
        /// Обработчик события обновления вывода количества кадров в секунду
        /// </summary>
        public Action OnUpdateFps
        {
            get { return _onUpdateFps; }
            set { _onUpdateFps = value; }
        }

        //
        // ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ
        //
        /// <summary>
        /// Отображение дополнительной информации (размер экрана и масштаб)
        /// </summary>
        public bool IsInfoShow
        {
            get { return _isInfoShow; }
            set { _isInfoShow = value; }
        }

        /// <summary>
        /// Позиция блока дополнительной информации
        /// </summary>
        public Rect InfoPosition
        {
            get { return _infoPosition; }
            set { _infoPosition = value; }
        }
        #endregion

        #region ILotusMessageHandler properties
        /// <summary>
        /// Группа которой принадлежит данный обработчик событий
        /// </summary>
        public string MessageHandlerGroup
        {
            get { return _groupMessage; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
        /// </summary>
        public void Reset()
        {
            //
            // БАЗОВОЕ ЯДРО
            //

            // Лог (для дальнейшего логирования)
            XConsoleDispatcher.OnResetEditor();

            // Подсистема ввода
            XInputDispatcher.OnResetEditor();

            // Инициализация центрального диспетчера игровых объектов
            XGameObjectDispatcher.OnResetEditor();

            // Инициализация центрального диспетчера компонентов
            XComponentDispatcher.OnResetEditor();

            // Инициализация центрального диспетчера ресурсов
            XResourcesDispatcher.OnResetEditor();
        }

        /// <summary>
        /// Псевдоконструктор скрипта
        /// </summary>
        public void Awake()
        {
            // FPS
            InitFPS();

            // Подсистема IMGUI
            if (_useIMGUI)
            {
                InitIMGUI();
            }

            // Инициализация данных
            if (!CheckDuplicate())
            {
                //
                // БАЗОВОЕ ЯДРО
                //
                // Лог (для дальнейшего логирования)
                XConsoleDispatcher.OnInit();

                // Подсистема ввода
                XInputDispatcher.OnInit();

                // Инициализация центрального диспетчера игровых объектов
                XGameObjectDispatcher.OnInit();

                // Инициализация центрального диспетчера компонентов
                XComponentDispatcher.OnInit();

                // Инициализация центрального диспетчера ресурсов
                XResourcesDispatcher.OnInit();
            }

            if (_isDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
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
            if (!CheckDuplicate())
            {
                //
                // БАЗОВЫЙ МОДУЛЬ UNITY
                //
                XInputDispatcher.OnStart();
            }

            _infoPosition = new Rect(5, 5, 400, 80);
            _infoPosition.x = LotusSystemSettingsService.Instance.ScreenWidth - _infoPosition.width - _infoPosition.x;
            UpdateInfoText();
        }

        /// <summary>
        /// Обновление скрипта каждый кадр
        /// </summary>
        public void Update()
        {
            UpdateInfoText();

            // Обновляем FPS
            UpdateFPS();

            // Обработка событий ввода
            XInputDispatcher.OnUpdate();
        }

        /// <summary>
        /// Обновление скрипта каждый кадр после (после Update)
        /// </summary>
        public void LateUpdate()
        {
            XInputDispatcher.OnLateUpdate();
        }

        /// <summary>
        /// Рисование UnityGUI
        /// </summary>
        public void OnGUI()
        {
            if (_useIMGUI)
            {
                UpdateIMGUI();
            }

            if (_consoleVisible)
            {
                XConsoleDispatcher.OnGUI(_consoleHeight);
            }

            if (_isFpsShow && _fpsGUIStyle != null)
            {
                GUI.backgroundColor = Color.black;
                GUI.contentColor = Color.yellow;
                GUI.Box(_fpsPosition, _fpsText, _fpsGUIStyle);
            }

            if (_isInfoShow && _fpsGUIStyle != null)
            {
                GUI.backgroundColor = Color.black;
                GUI.contentColor = Color.yellow;
                GUI.Box(_infoPosition, _infoText, _fpsGUIStyle);
            }
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

        /// <summary>
        /// Завершение работы приложения
        /// </summary>
        public void OnApplicationQuit()
        {
            XConsoleDispatcher.OnShutdown();
        }
        #endregion

        #region ISerializationCallbackReceiver methods
        /// <summary>
        /// Процесс перед сериализацией объекта
        /// </summary>
        public void OnBeforeSerialize()
        {
            if (_currentSkin != null && XGUISkin.IsDefaultSkinNull)
            {
                XGUISkin.DefaultSkin = _currentSkin;
            }
        }

        /// <summary>
        /// Процесс после сериализацией объекта
        /// </summary>
        public void OnAfterDeserialize()
        {
            if (_currentSkin != null && XGUISkin.IsDefaultSkinNull)
            {
                XGUISkin.DefaultSkin = _currentSkin;
            }
        }
        #endregion

        #region ILotusMessageHandler methods
        /// <summary>
        /// Основной метод для обработки сообщения
        /// </summary>
        /// <param name="args">Аргументы сообщения</param>
        /// <returns>Код обработки собщения</returns>
        public int OnMessageHandler(CMessageArgs args)
        {
            var status = false;
            if (args is CMessageConsole argsConsole)
            {
                if (MESSAGE_FPS.EqualIgnoreCase(argsConsole.Name))
                {
                    _isFpsShow = XBooleanConverter.Parse(argsConsole.Argument);
                    status = true;
                }
                if (MESSAGE_INFO.EqualIgnoreCase(argsConsole.Name))
                {
                    _isInfoShow = XBooleanConverter.Parse(argsConsole.Argument);
                    status = true;
                }
            }
            else
            {
                switch (args.Name)
                {
                    case MESSAGE_FPS:
                        {
                            //_isFpsShow = args.BooleanValue;
                            status = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return 10;
        }
        #endregion

        #region FPS methods
        /// <summary>
        /// Инициализация подсистемы FPS
        /// </summary>
        private void InitFPS()
        {
            if (_fpsGUIStyle == null)
            {
                _fpsGUIStyle = new GUIStyle(GUIStyle.none);
                _fpsGUIStyle.fontSize = 18;
                _fpsGUIStyle.alignment = TextAnchor.MiddleCenter;
                _fpsGUIStyle.normal.background = XTexture2D.BlackAlpha75;
                _fpsGUIStyle.normal.textColor = Color.white;
            }
        }

        /// <summary>
        /// Обновление данных о текущем FPS
        /// </summary>
        private void UpdateFPS()
        {
            var unscaledDeltaTime = Time.unscaledDeltaTime;
            if (unscaledDeltaTime <= 0f)
            {
                return;
            }

            _fpsTimeStart -= unscaledDeltaTime;
            _fpsAccum += 1.0f / unscaledDeltaTime;
            ++_fpsFrames;

            // Interval ended - update GUI text and start new interval
            if (_fpsTimeStart <= 0.0f)
            {
                var fps = _fpsAccum / _fpsFrames;
                _fpsTimeStart = _fpsUpdateInterval;
                _fpsAccum = 0.0f;
                _fpsFrames = 0;
                if (_fpsMin > fps)
                {
                    _fpsMin = fps;
                }

                _fpsText = "FPS = " + fps.ToString("F2");
                _onUpdateFps?.Invoke();
            }
        }
        #endregion

        #region IMGUI methods
        /// <summary>
        /// Инициализация подсистемы IMGUI
        /// </summary>
        private void InitIMGUI()
        {
        }

        /// <summary>
        /// Обновление подсистемы IMGUI
        /// </summary>
        private void UpdateIMGUI()
        {
            GUI.skin = _currentSkin;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Обновление текста дополнительной информации о экране
        /// </summary>
        private void UpdateInfoText()
        {
            var settings = LotusSystemSettingsService.Instance;
            _infoText = "Screen Width = " + Screen.width.ToString() + "; Screen Height = " + Screen.height.ToString();
            var scaledScreenX = settings.ScaledScreenX;
            var scaledScreenY = settings.ScaledScreenY;
            _infoText += "\n" + "ScaledScreenX = " + scaledScreenX.ToString("F2") + "; ScaledScreenY = " + scaledScreenY.ToString("F2");
        }
        #endregion
    }
    /**@}*/
}
#endif
