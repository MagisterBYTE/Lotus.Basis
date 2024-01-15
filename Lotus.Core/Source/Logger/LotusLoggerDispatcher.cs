//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема логирования
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLoggerDispatcher.cs
*		Центральный диспетчер для трассировки, диагностики, отладки и логирования процесса работы приложения.
*		Центральный диспетчер применяется для трассировки, диагностики, отладки и логирования процесса работы приложения
*	и обеспечивает хранение и регистрацию как всех системных/межплатформенных сообщений, так и всех сообщений от бизнес-логики
*	приложения.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.IO;
using System.Runtime.CompilerServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreLogger
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Центральный диспетчер для трассировки, диагностики, отладки и логирования процесса работы приложения.
		/// </summary>
		/// <remarks>
		/// Центральный диспетчер применяется для трассировки, диагностики, отладки и логирования процесса работы
		/// приложения и обеспечивает хранение и регистрацию как всех системных/межплатформенных сообщений, так и всех
		/// сообщений от бизнес-логики приложения
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XLogger
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal static ILotusLoggerView _logger;
			internal static ListArray<TLogMessage> _messages;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Текущий логгер для визуального отображения
			/// </summary>
			public static ILotusLoggerView Logger
			{
				get { return _logger; }
				set { _logger = value; }
			}

			/// <summary>
			/// Все сообщения
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

			#region ======================================= ОБЩИЕ МЕТОДЫ ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение
			/// </summary>
			/// <param name="message">Сообщение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Log(TLogMessage message)
			{
				Messages.Add(message);
				if (_logger != null)
				{
					_logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохраннее сообщений текстовый файл
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SaveToText(String fileName)
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

			#region ======================================= МЕТОДЫ Info ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации
			/// </summary>
			/// <param name="info">Объект информации</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfo(System.Object info,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (info != null)
				{
					var text = info.ToString() ?? String.Empty;

					var message = new TLogMessage(text, TLogType.Info);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);
					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="info">Объект информации</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfoModule(String moduleName, System.Object info,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (info != null)
				{
					var text = info.ToString() ?? String.Empty;

					var message = new TLogMessage(moduleName, text, TLogType.Info);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);

					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации с форматированием данных
			/// </summary>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfoFormat(String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(text, TLogType.Info);
				Messages.Add(message);

				if (_logger != null) _logger.Log(text, TLogType.Info);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации с форматированием данных
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfoFormatModule(String moduleName, String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(moduleName, text, TLogType.Info);
				Messages.Add(message);

				if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Info);
			}
			#endregion

			#region ======================================= МЕТОДЫ Warning ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении
			/// </summary>
			/// <param name="warning">Объект предупреждения</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarning(System.Object warning,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (warning != null)
				{
					var text = warning.ToString() ?? String.Empty;

					var message = new TLogMessage(text, TLogType.Warning);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);

					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="warning">Объект предупреждения</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarningModule(String moduleName, System.Object warning,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (warning != null)
				{
					var text = warning.ToString() ?? String.Empty;

					var message = new TLogMessage(moduleName, text, TLogType.Warning);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);

					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении с форматированием данных
			/// </summary>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarningFormat(String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(text, TLogType.Warning);
				Messages.Add(message);

				if (_logger != null) _logger.Log(text, TLogType.Warning);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении с форматированием данных
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarningFormatModule(String moduleName, String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(moduleName, text, TLogType.Warning);
				Messages.Add(message);

				if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Warning);
			}
			#endregion

			#region ======================================= МЕТОДЫ Error ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке
			/// </summary>
			/// <param name="error">Объект ошибки</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogError(System.Object error,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (error != null)
				{
					var text = error.ToString() ?? String.Empty;

					var message = new TLogMessage(text, TLogType.Error);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);

					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="error">Объект ошибки</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogErrorModule(String moduleName, System.Object error,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				if (error != null)
				{
					var text = error.ToString() ?? String.Empty;

					var message = new TLogMessage(moduleName, text, TLogType.Error);
					message.MemberName = memberName;
					message.FilePath = filePath;
					message.LineNumber = lineNumber;

					Messages.Add(message);

					if (_logger != null) _logger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке с форматированием данных
			/// </summary>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogErrorFormat(String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(text, TLogType.Error);
				Messages.Add(message);

				if (_logger != null) _logger.Log(text, TLogType.Error);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке с форматированием данных
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogErrorFormatModule(String moduleName, String format, params Object[] args)
			{
				var text = String.Format(format, args);

				var message = new TLogMessage(moduleName, text, TLogType.Error);
				Messages.Add(message);

				if (_logger != null) _logger.LogModule(moduleName, text, TLogType.Error);
			}
			#endregion

			#region ======================================= МЕТОДЫ Exception ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об исключении
			/// </summary>
			/// <param name="exc">Исключение</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogException(Exception exc,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
			{
				var message = new TLogMessage(exc.Message, TLogType.Exception);
				message.MemberName = memberName;
				message.FilePath = filePath;
				message.LineNumber = lineNumber;

				Messages.Add(message);

				if (_logger != null) _logger.Log(message);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об исключении
			/// </summary>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="exc">Исключение</param>
			/// <param name="memberName">Имя метода (заполняется автоматически)</param>
			/// <param name="filePath">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="lineNumber">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogExceptionModule(String moduleName, Exception exc,
				[CallerMemberName] String memberName = "",
				[CallerFilePath] String filePath = "",
				[CallerLineNumber] Int32 lineNumber = 0)
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
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================