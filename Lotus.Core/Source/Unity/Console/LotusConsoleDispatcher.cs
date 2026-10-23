#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityConsole
    *@{*/
    /// <summary>
    /// Центральный диспетчер консоли
    /// </summary>
    /// <remarks>
    /// <para>
    /// Реализация центрального диспетчера консоли обеспечивающего вывод отладочных сообщений, исполнения команд
    /// и посылку сообщений
    /// </para>
    /// <para>
    /// Управляется центральным диспетчером <see cref="LotusSystemDispatcher"/>.
    /// Если центральный диспетчер не используется, то методы нужно вызывать вручную в соответствующих местах
    /// </para>
    /// </remarks>
    public static class XConsoleDispatcher
    {
        #region Fields
        /// <summary>
        /// Все команды по имени
        /// </summary>
        public static readonly Dictionary<string, Action<string>> Commands = new Dictionary<string, Action<string>>();

        // Параметры отображения
        internal static Rect _viewRect;
        internal static Vector2 _scroll;
        internal static bool _isCollapsed;
        internal static bool _isOpened;
#if UNITY_EDITOR || UNITY_STANDALONE
        internal static float _heightCaption = 30;
        internal static float _widthButtonMode = 120;
#else
        internal static float _heightCaption = 40;
        internal static float _widthButtonMode = 160;
#endif

        // Режим ввода команды или сообщения
        internal static int _modeCommand;
        internal static string _inputString = "";

        // Стили для отображения
        internal static GUIStyle _columnHeaderStyle;
        internal static GUIStyle _itemStyle;
        internal static GUIStyle _panelStyle;

        // Служебные данные
        internal static string _titleConsole;
        internal static Action _onDrawMessageArea;
        #endregion

        #region Properties
        /// <summary>
        /// Свернуть окно консоли
        /// </summary>
        public static bool IsCollapsed
        {
            get { return _isCollapsed; }
            set { _isCollapsed = value; }
        }

        /// <summary>
        /// Открыть/закрыть окно консоли
        /// </summary>
        public static bool IsOpened
        {
            get { return _isOpened; }
            set { _isOpened = value; }
        }
        #endregion

        #region Dispatcher methods
        /// <summary>
        /// Перезапуск данных центрального диспетчера в режиме редактора
        /// </summary>
        public static void OnResetEditor()
        {
            OnInit();
        }


        /// <summary>
        /// Первичная инициализация данных центрального диспетчера
        /// </summary>
        public static void OnInit()
        {
            Application.logMessageReceived += MessageHandler;
            _inputString = "";

            if (_itemStyle == null)
            {
                _itemStyle = new GUIStyle(GUIStyle.none);
#if UNITY_EDITOR || UNITY_STANDALONE
                _itemStyle.fontSize = 14;
#else
                _itemStyle.fontSize = 18;
#endif
                _itemStyle.alignment = TextAnchor.MiddleLeft;
                _itemStyle.wordWrap = true;
                _itemStyle.normal.textColor = Color.white;
            }

            if (_panelStyle == null)
            {
                _panelStyle = new GUIStyle(GUIStyle.none);
                _panelStyle.fontSize = 12;
                _panelStyle.alignment = TextAnchor.UpperLeft;
                _panelStyle.normal.background = XTexture2D.BlackAlpha75;
                _panelStyle.normal.textColor = Color.white;
            }

            if (_columnHeaderStyle == null)
            {
                _columnHeaderStyle = new GUIStyle(GUIStyle.none);
#if UNITY_EDITOR || UNITY_STANDALONE
                _columnHeaderStyle.fontSize = 14;
#else
                _columnHeaderStyle.fontSize = 18;
#endif
                _columnHeaderStyle.fontStyle = FontStyle.Bold;
                _columnHeaderStyle.alignment = TextAnchor.MiddleLeft;
                _columnHeaderStyle.normal.background = XTexture2D.BlackAlpha75;
                _columnHeaderStyle.normal.textColor = Color.white;
            }

            if (Application.GetStackTraceLogType(LogType.Log) == StackTraceLogType.None)
            {
                _onDrawMessageArea = DrawMessageAreaNone;
            }
            else
            {
                _onDrawMessageArea = DrawMessageAreaTrace;
            }
        }


        /// <summary>
        /// Рисование сообщений лога
        /// </summary>
        /// <param name="heightPanel">Высота панели консоли</param>
        public static void OnGUI(float heightPanel)
        {
            if (_isCollapsed == false)
            {
                _viewRect.x = 0;
                _viewRect.y = _isOpened ? Screen.height - heightPanel : Screen.height - _heightCaption;
                _viewRect.height = _isOpened ? heightPanel : _heightCaption;
                _viewRect.width = Screen.width;

                if (_isOpened)
                {
                    // Выводим поверх всего
                    GUI.depth = 1;
                    GUILayout.BeginArea(_viewRect);
                    {
                        DrawHeaderArea();
                        _onDrawMessageArea();
                    }
                    GUILayout.EndArea();

                    var boxConsole = new Rect(0, Screen.height - heightPanel, Screen.width, _heightCaption);
                    if (Event.current.type == EventType.MouseDown)
                    {
                        if (boxConsole.Contains(Event.current.mousePosition))
                        {
                            _isOpened = !_isOpened;
                        }
                    }
                }
                else
                {
                    _viewRect.x = 0;
                    _viewRect.y = Screen.height - _heightCaption;
                    _viewRect.height = _heightCaption;
                    _viewRect.width = Screen.width;

                    GUILayout.BeginArea(_viewRect);
                    DrawHeaderArea();
                    GUILayout.EndArea();

                    if (Event.current.type == EventType.MouseDown)
                    {
                        if (_viewRect.Contains(Event.current.mousePosition))
                        {
                            _isOpened = !_isOpened;
                        }
                    }
                }
            }
            else
            {
                float widthButton = 50;
                _viewRect.x = Screen.width - widthButton;
                _viewRect.y = Screen.height - _heightCaption;
                _viewRect.height = _heightCaption;
                _viewRect.width = widthButton;

                GUILayout.BeginArea(_viewRect);
                {
                    DrawButtonCollapsed(widthButton, _heightCaption);
                }
                GUILayout.EndArea();
            }
        }


        /// <summary>
        /// Завершение работы центрального диспетчера
        /// </summary>
        public static void OnShutdown()
        {
            Application.logMessageReceived -= MessageHandler;
        }


        /// <summary>
        /// Завершение работы центрального диспетчера
        /// </summary>
        [Obsolete("Опечатка в имени. Используйте OnShutdown")]
        public static void OnShudown()
        {
            OnShutdown();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Обработчик событий лога
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        /// <param name="stackTrace">Трассировка сообщения</param>
        /// <param name="type">Тип сообщения</param>
        public static void MessageHandler(string text, string stackTrace, LogType type)
        {
            var log = new LogMessage(text, ResolveLogType(type));

            if (stackTrace.IsExists())
            {
                log.ParseStackTrace(stackTrace);
            }

            log.Time = Time.time;

            XLogger.Messages.Add(log);

            ComputeTitleConsole();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static TLogType ResolveLogType(LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    return TLogType.Error;
                case LogType.Assert:
                    return TLogType.Error;
                case LogType.Warning:
                    return TLogType.Warning;
                case LogType.Log:
                    return TLogType.Info;
                case LogType.Exception:
                    return TLogType.Exception;
                default:
                    return TLogType.Info;
            }
        }


        /// <summary>
        /// Запуск на исполнение команды или посылка сообщения
        /// </summary>
        private static void SetExecutedCommandOrMessage()
        {
            // Получаем токены разделенные пробелом
            var tokens = _inputString.Split(XCharHelper.SeparatorSpaces, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length > 0)
            {
                // Имя команды/сообщения
                var name = tokens[0];

                // Аргумент команды/сообщения
                var arg = "";
                if (tokens.Length > 1)
                {
                    arg = tokens[1];
                }

                if (_modeCommand == 1)
                {
                    // Если такая команда есть то вызываем
                    if (Commands.ContainsKey(name))
                    {
                        Commands[name](arg);

                        // Информируем что команда исполнена
                        XLogger.Messages.Add(new LogMessage("Command: <" + name + "> executed", TLogType.Succeed));
                    }
                    else
                    {
                        // Системная команда
                        if (name == "ft1" || name == "ft0")
                        {
                            if (name == "ft0")
                            {
                                _onDrawMessageArea = DrawMessageAreaNone;
                            }
                            else
                            {
                                _onDrawMessageArea = DrawMessageAreaTrace;
                            }

                            // Информируем что команда неисполнена
                            XLogger.Messages.Add(new LogMessage("System command: <" + name + "> executed", TLogType.Failed));
                        }
                        else
                        {
                            // Информируем что команда неисполнена
                            XLogger.Messages.Add(new LogMessage("Command: <" + name + "> not found", TLogType.Failed));
                        }
                    }
                }
                else
                {
                    // Формируем сообщение
                    var message = new CMessageConsole(name, arg);

                    // Посылаем сообщение
                    //XMessageDispatcher.SendMessage(message);

                    // Информируем что сообщение послано
                    XLogger.Messages.Add(new LogMessage("Message: Name <" + name + ">  Arg <" + arg + ">", TLogType.Info));
                }

                ComputeTitleConsole();
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование заголовочной области
        /// </summary>
        private static void DrawHeaderArea()
        {
            float widthButton = 100;

            GUILayout.BeginHorizontal();
            {
                // Кнопка очистить все сообщения
                DrawButtonClear(widthButton, _heightCaption);

                // Если есть режим команды
                if (_modeCommand != 0)
                {
                    // Область информирования
                    if (Screen.width > 1600)
                    {
                        GUILayout.Box(_titleConsole, GUILayout.Height(_heightCaption), GUILayout.MaxWidth(Screen.width * 0.5f));
                    }
                    else
                    {
                        GUILayout.Box(_titleConsole, GUILayout.Height(_heightCaption), GUILayout.MaxWidth(Screen.width * 0.3f));
                    }

#if UNITY_EDITOR || UNITY_STANDALONE
                    DrawInputCommand(widthButton * 2, _heightCaption);
#else
                    DrawInputCommand(widthButton * 3, _heightCaption);
#endif
                    DrawButtonSend(widthButton, _heightCaption);
                }
                else
                {
                    // Область информирования
                    GUILayout.Box(_titleConsole, GUILayout.Height(_heightCaption));
                }

                // Кнопка переключение режима
                DrawButtonMode(widthButton, _heightCaption);

                // Кнопка сворачивания/разворачивания
                DrawButtonCollapsed(widthButton / 2, _heightCaption);
            }
            GUILayout.EndHorizontal();
        }


        /// <summary>
        /// Рисование области и всех сообщений
        /// </summary>
        private static void DrawMessageAreaNone()
        {
            // Считаем размеры для вывода каждой области
            var wNumber = Mathf.Ceil(Screen.width * 0.05f);
            var wText = Mathf.Ceil(Screen.width * 0.94f);

            _scroll = GUILayout.BeginScrollView(_scroll);
            {
                // Заголовок
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(4);
                    GUILayout.Label("N", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wNumber));
                    GUILayout.Label("Text", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wText));
                }
                GUILayout.EndHorizontal();

                // Сообщения
                for (var i = 0; i < XLogger.Messages.Count; i++)
                {
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(4);

                        // Номер
                        GUI.color = Color.white;
                        GUILayout.Label(i.ToString(), _itemStyle, GUILayout.Width(wNumber));

                        // Сообщение
                        switch (XLogger.Messages[i].Type)
                        {
                            case TLogType.Info: { GUI.color = XUnityColor.LawnGreen; } break;
                            case TLogType.Warning: { GUI.color = Color.yellow; } break;
                            case TLogType.Error: { GUI.color = Color.red; } break;
                            case TLogType.Exception: { GUI.color = Color.gray; } break;
                            default:
                                break;
                        }

                        GUILayout.Label(XLogger.Messages[i].Text, _itemStyle, GUILayout.Width(wText));
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
        }


        /// <summary>
        /// Рисование области и всех сообщений для полной отладки
        /// </summary>
        private static void DrawMessageAreaTrace()
        {
            // Считаем размеры для вывода каждой области
            var wNumber = Mathf.Ceil(Screen.width * 0.05f);
            var wText = Mathf.Ceil(Screen.width * 0.32f);
            var wMember = Mathf.Ceil(Screen.width * 0.35f);
            var wFile = Mathf.Ceil(Screen.width * 0.27f);

            _scroll = GUILayout.BeginScrollView(_scroll);
            {
                // Заголовок
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(4);
                    GUILayout.Label("N", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wNumber));
                    GUILayout.Label("Text", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wText));
                    GUILayout.Label("MemberName", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wMember));
                    GUILayout.Label("FilePath", _columnHeaderStyle, GUILayout.Height(30), GUILayout.Width(wFile));
                }
                GUILayout.EndHorizontal();

                // Сообщения
                for (var i = 0; i < XLogger.Messages.Count; i++)
                {
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(4);

                        // Номер
                        GUI.color = Color.white;
                        GUILayout.Label(i.ToString(), _itemStyle, GUILayout.Width(wNumber));

                        // Сообщение
                        switch (XLogger.Messages[i].Type)
                        {
                            case TLogType.Info: { GUI.color = XUnityColor.LawnGreen; } break;
                            case TLogType.Warning: { GUI.color = Color.yellow; } break;
                            case TLogType.Error: { GUI.color = Color.red; } break;
                            case TLogType.Exception: { GUI.color = Color.gray; } break;
                            default:
                                break;
                        }
                        GUILayout.Label(XLogger.Messages[i].Text, _itemStyle, GUILayout.Width(wText));

                        // Метод
                        GUI.color = Color.white;
                        GUILayout.Label(XLogger.Messages[i].MemberName, _itemStyle, GUILayout.Width(wMember));

                        // Файл
                        GUILayout.Label(XLogger.Messages[i].FilePath, _itemStyle, GUILayout.Width(wFile));
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
        }


        /// <summary>
        /// Рисование области вывода команды/сообщения
        /// </summary>
        /// <param name="width">Ширина области</param>
        /// <param name="height">Высота области</param>
        private static void DrawInputCommand(float width, float height)
        {
            if (_modeCommand == 1)
            {
                GUILayout.Label("Input command: ");
            }
            if (_modeCommand == 2)
            {
                GUILayout.Label("Input message: ");
            }
            _inputString = GUILayout.TextField(_inputString, GUILayout.Width(width), GUILayout.Height(height));

            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.character == XCharHelper.NewLine)
                {
                    Event.current.Use();
                    SetExecutedCommandOrMessage();
                    _inputString = "";
                }
            }
        }


        /// <summary>
        /// Рисование кнопки сворачивания/разворачивания
        /// </summary>
        /// <param name="width">Ширина кнопки</param>
        /// <param name="height">Высота кнопки</param>
        private static void DrawButtonCollapsed(float width, float height)
        {
            if (GUILayout.Button(_isCollapsed ? XStringHelper.TriangleLeft : XStringHelper.TriangleDown,
                GUILayout.Width(width), GUILayout.Height(height)))
            {
                _isCollapsed = !_isCollapsed;
            }
        }


        /// <summary>
        /// Рисование кнопки переключения режима
        /// </summary>
        /// <param name="width">Ширина кнопки</param>
        /// <param name="height">Высота кнопки</param>
        private static void DrawButtonMode(float width, float height)
        {
            switch (_modeCommand)
            {
                case 0:
                    {
                        if (GUILayout.Button("Mode [None]",
                            Screen.width > 1200 ? GUILayout.Width(160) : GUILayout.Width(_widthButtonMode),
                            GUILayout.Height(height)))
                        {
                            _modeCommand++;

                            if (_modeCommand == 3)
                            {
                                _modeCommand = 0;
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        if (GUILayout.Button("Mode [Command]", GUILayout.Height(height)))
                        {
                            _modeCommand++;

                            if (_modeCommand == 3)
                            {
                                _modeCommand = 0;
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (GUILayout.Button("Mode [Message]", GUILayout.Height(height)))
                        {
                            _modeCommand++;

                            if (_modeCommand == 3)
                            {
                                _modeCommand = 0;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Рисование кнопки для отправки сообщения
        /// </summary>
        /// <param name="width">Ширина кнопки</param>
        /// <param name="height">Высота кнопки</param>
        private static void DrawButtonSend(float width, float height)
        {
            if (GUILayout.Button("Send", GUILayout.Width(width), GUILayout.Height(height)))
            {
                SetExecutedCommandOrMessage();

                _inputString = "";
            }
        }


        /// <summary>
        /// Рисование кнопки для очистки всех сообщений
        /// </summary>
        /// <param name="width">Ширина кнопки</param>
        /// <param name="height">Высота кнопки</param>
        private static void DrawButtonClear(float width, float height)
        {
            if (GUILayout.Button("Clear", GUILayout.Width(width), GUILayout.Height(height)))
            {
                if (XLogger.Messages.Count > 0)
                {
                    XLogger.Messages.Clear();
                    ComputeTitleConsole();
                }
            }
        }


        /// <summary>
        /// Просчитать заголовок консоли
        /// </summary>
        private static void ComputeTitleConsole()
        {
            if (XLogger.Messages.Count == 0)
            {
                _titleConsole = "Console [messages: <color=yellow>0</color>]";
            }
            else
            {
                _titleConsole = "Console [messages: <color=yellow>" + XLogger.Messages.Count.ToString() + "</color>]"
                    + "  [last: <color=yellow>" +
                    XLogger.Messages[XLogger.Messages.Count - 1].Text + "</color>]";
            }
        }
        #endregion

        #region Command methods
        /// <summary>
        /// Проверка на существование команды с указанным именем
        /// </summary>
        /// <param name="name">Имя команды</param>
        /// <returns>Статус существования команды</returns>
        public static bool ExistsCommand(string name)
        {
            return Commands.ContainsKey(name);
        }


        /// <summary>
        /// Добавление команды с указанным именем
        /// </summary>
        /// <param name="name">Имя команды</param>
        /// <param name="onCommand">Обработчик команды</param>
        /// <returns>Статус успешности добавления</returns>
        public static bool AddCommand(string name, Action<string> onCommand)
        {
            if (!Commands.ContainsKey(name))
            {
                Commands.Add(name, onCommand);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Удаление команды с указанным именем
        /// </summary>
        /// <param name="name">Имя команды</param>
        /// <returns>Статус успешности удаления</returns>
        public static bool RemoveCommand(string name)
        {
            if (Commands.ContainsKey(name))
            {
                Commands.Remove(name);
                return true;
            }

            return false;
        }
        #endregion
    }
    /**@}*/
}
#endif
