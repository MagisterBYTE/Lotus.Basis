using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Lotus.Core
{
    /** \addtogroup CoreLogger
	*@{*/
    /// <summary>
    /// Центральный диспетчер для трассировки, диагностики, отладки и логирования процесса работы приложения.
    /// </summary>
    /// <remarks>
    /// Центральный диспетчер применяется для трассировки, диагностики, отладки и логирования процесса работы
    /// приложения и обеспечивает хранение и регистрацию как всех системных/межплатформенных сообщений, так и всех
    /// сообщений от бизнес-логики приложения.
    /// </remarks>
    public static class XLogger
    {
        #region Fields
        internal static ILotusLoggerView _logger;
        internal static ListArray<TLogMessage> _messages;
        #endregion

        #region Properties
        /// <summary>
        /// Текущий логгер для визуального отображения.
        /// </summary>
        public static ILotusLoggerView Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        /// <summary>
        /// Все сообщения.
        /// </summary>
        public static ListArray<TLogMessage> Messages
        {
            get
            {
                if (_messages == null)
                {
                    _messages = new ListArray<TLogMessage>();
                    _messages.IsNotify = true;
                }
                return _messages;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Оповещение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public static void Log(TLogMessage message)
        {
            Messages.Add(message);
            if (_logger != null)
            {
                _logger.Log(message);
            }
        }

        /// <summary>
        /// Сохраннее сообщений текстовый файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public static void SaveToText(string fileName)
        {
            if (_messages != null)
            {
                var file_stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                var stream_writer = new StreamWriter(file_stream);

                for (var i = 0; i < _messages.Count; i++)
                {
                    stream_writer.WriteLine(_messages[i].Text);
                }

                stream_writer.Close();
                file_stream.Close();
            }
        }
        #endregion

        #region Info methods
        /// <summary>
        /// Оповещение о простой информации.
        /// </summary>
        /// <param name="info">Объект информации.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogInfo(object info,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (info != null)
            {
                var text = info.ToString() ?? string.Empty;

                var message = new TLogMessage(text, TLogType.Info);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);
                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение о простой информации.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="info">Объект информации.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogInfoModule(string moduleName, object info,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (info != null)
            {
                var text = info.ToString() ?? string.Empty;

                var message = new TLogMessage(moduleName, text, TLogType.Info);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);

                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение о простой информации с форматированием данных.
        /// </summary>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogInfoFormat(string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(text, TLogType.Info);
            Messages.Add(message);

            if (_logger != null) _logger.Log(text, TLogType.Info);
        }

        /// <summary>
        /// Оповещение о простой информации с форматированием данных.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogInfoFormatModule(string moduleName, string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(moduleName, text, TLogType.Info);
            Messages.Add(message);

            if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Info);
        }
        #endregion

        #region Warning methods
        /// <summary>
        /// Оповещение о предупреждении.
        /// </summary>
        /// <param name="warning">Объект предупреждения.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogWarning(object warning,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (warning != null)
            {
                var text = warning.ToString() ?? string.Empty;

                var message = new TLogMessage(text, TLogType.Warning);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);

                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение о предупреждении.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="warning">Объект предупреждения.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogWarningModule(string moduleName, object warning,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (warning != null)
            {
                var text = warning.ToString() ?? string.Empty;

                var message = new TLogMessage(moduleName, text, TLogType.Warning);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);

                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение о предупреждении с форматированием данных.
        /// </summary>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogWarningFormat(string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(text, TLogType.Warning);
            Messages.Add(message);

            if (_logger != null) _logger.Log(text, TLogType.Warning);
        }

        /// <summary>
        /// Оповещение о предупреждении с форматированием данных.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogWarningFormatModule(string moduleName, string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(moduleName, text, TLogType.Warning);
            Messages.Add(message);

            if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Warning);
        }
        #endregion

        #region Error methods
        /// <summary>
        /// Оповещение об ошибке.
        /// </summary>
        /// <param name="error">Объект ошибки.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogError(object error,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (error != null)
            {
                var text = error.ToString() ?? string.Empty;

                var message = new TLogMessage(text, TLogType.Error);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);

                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение об ошибке.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="error">Объект ошибки.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogErrorModule(string moduleName, object error,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (error != null)
            {
                var text = error.ToString() ?? string.Empty;

                var message = new TLogMessage(moduleName, text, TLogType.Error);
                message.MemberName = memberName;
                message.FilePath = filePath;
                message.LineNumber = lineNumber;

                Messages.Add(message);

                if (_logger != null) _logger.Log(message);
            }
        }

        /// <summary>
        /// Оповещение об ошибке с форматированием данных.
        /// </summary>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogErrorFormat(string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(text, TLogType.Error);
            Messages.Add(message);

            if (_logger != null) _logger.Log(text, TLogType.Error);
        }

        /// <summary>
        /// Оповещение об ошибке с форматированием данных.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="format">Строка с форматом данных.</param>
        /// <param name="args">Список аргументов.</param>
        public static void LogErrorFormatModule(string moduleName, string format, params object[] args)
        {
            var text = string.Format(format, args);

            var message = new TLogMessage(moduleName, text, TLogType.Error);
            Messages.Add(message);

            if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Error);
        }
        #endregion

        #region Exception methods
        /// <summary>
        /// Оповещение об исключении.
        /// </summary>
        /// <param name="exc">Исключение.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogException(Exception exc,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var message = new TLogMessage(exc.Message, TLogType.Exception);
            message.MemberName = memberName;
            message.FilePath = filePath;
            message.LineNumber = lineNumber;

            Messages.Add(message);

            if (_logger != null) _logger.Log(message);
        }

        /// <summary>
        /// Оповещение об исключении.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="exc">Исключение.</param>
        /// <param name="memberName">Имя метода (заполняется автоматически).</param>
        /// <param name="filePath">Полный путь к файлу (заполняется автоматически).</param>
        /// <param name="lineNumber">Номер строки в файле (заполняется автоматически).</param>
        public static void LogExceptionModule(string moduleName, Exception exc,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var message = new TLogMessage(moduleName, exc.Message, TLogType.Exception);
            message.MemberName = memberName;
            message.FilePath = filePath;
            message.LineNumber = lineNumber;

            Messages.Add(message);

            if (_logger != null) _logger.Log(message);
        }
        #endregion
    }
    /**@}*/
}