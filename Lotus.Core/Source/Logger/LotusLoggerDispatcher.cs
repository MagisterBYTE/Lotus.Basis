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
// Последнее изменение от 27.03.2022
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
		//! \addtogroup CoreLogger
		/*@{*/
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
			internal static ILoggerView mLogger;
			internal static ListArray<TLogMessage> mMessages;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Текущий логгер для визуального отображения
			/// </summary>
			public static ILoggerView Logger
			{
				get { return (mLogger); }
				set { mLogger = value; }
			}

			/// <summary>
			/// Все сообщения
			/// </summary>
			public static ListArray<TLogMessage> Messages
			{
				get
				{
					if (mMessages == null)
					{
						mMessages = new ListArray<TLogMessage>();
						mMessages.IsNotify = true;
					}
					return (mMessages);
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
				if (mLogger != null)
				{
					mLogger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохраннее сообщений текстовый файл
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SaveToText(String file_name)
			{
				if (mMessages != null)
				{
					FileStream file_stream = new FileStream(file_name, FileMode.Create, FileAccess.Write);
					StreamWriter stream_writer = new StreamWriter(file_stream);

					for (Int32 i = 0; i < mMessages.Count; i++)
					{
						stream_writer.WriteLine(mMessages[i].Text);
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
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfo(System.Object info,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (info != null)
				{
					String text = info.ToString();

					TLogMessage message = new TLogMessage(text, TLogType.Info);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);
					if (mLogger != null) mLogger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="info">Объект информации</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfoModule(String module_name, System.Object info,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (info != null)
				{
					String text = info.ToString();

					TLogMessage message = new TLogMessage(module_name, text, TLogType.Info);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);

					if (mLogger != null) mLogger.Log(message);
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
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(text, TLogType.Info);
				Messages.Add(message);

				if (mLogger != null) mLogger.Log(text, TLogType.Info);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации с форматированием данных
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogInfoFormatModule(String module_name, String format, params Object[] args)
			{
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(module_name, text, TLogType.Info);
				Messages.Add(message);

				if (mLogger != null) mLogger.LogModule(module_name, text, TLogType.Info);
			}
			#endregion

			#region ======================================= МЕТОДЫ Warning ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении
			/// </summary>
			/// <param name="warning">Объект предупреждения</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarning(System.Object warning,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (warning != null)
				{
					String text = warning.ToString();

					TLogMessage message = new TLogMessage(text, TLogType.Warning);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);

					if (mLogger != null) mLogger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="warning">Объект предупреждения</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarningModule(String module_name, System.Object warning,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (warning != null)
				{
					String text = warning.ToString();

					TLogMessage message = new TLogMessage(module_name, text, TLogType.Warning);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);

					if (mLogger != null) mLogger.Log(message);
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
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(text, TLogType.Warning);
				Messages.Add(message);

				if (mLogger != null) mLogger.Log(text, TLogType.Warning);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о предупреждении с форматированием данных
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogWarningFormatModule(String module_name, String format, params Object[] args)
			{
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(module_name, text, TLogType.Warning);
				Messages.Add(message);

				if (mLogger != null) mLogger.LogModule(module_name, text, TLogType.Warning);
			}
			#endregion

			#region ======================================= МЕТОДЫ Error ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке
			/// </summary>
			/// <param name="error">Объект ошибки</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogError(System.Object error,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (error != null)
				{
					String text = error.ToString();

					TLogMessage message = new TLogMessage(text, TLogType.Error);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);

					if (mLogger != null) mLogger.Log(message);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="error">Объект ошибки</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogErrorModule(String module_name, System.Object error,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				if (error != null)
				{
					String text = error.ToString();

					TLogMessage message = new TLogMessage(module_name, text, TLogType.Error);
					message.MemberName = member_name;
					message.FilePath = file_path;
					message.LineNumber = line_number;

					Messages.Add(message);

					if (mLogger != null) mLogger.Log(message);
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
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(text, TLogType.Error);
				Messages.Add(message);

				if (mLogger != null) mLogger.Log(text, TLogType.Error);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке с форматированием данных
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="format">Строка с форматом данных</param>
			/// <param name="args">Список аргументов</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogErrorFormatModule(String module_name, String format, params Object[] args)
			{
				String text = String.Format(format, args);

				TLogMessage message = new TLogMessage(module_name, text, TLogType.Error);
				Messages.Add(message);

				if (mLogger != null) mLogger.LogModule(module_name, text, TLogType.Error);
			}
			#endregion

			#region ======================================= МЕТОДЫ Exception ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об исключении
			/// </summary>
			/// <param name="exc">Исключение</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogException(Exception exc,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				TLogMessage message = new TLogMessage(exc.Message, TLogType.Exception);
				message.MemberName = member_name;
				message.FilePath = file_path;
				message.LineNumber = line_number;

				Messages.Add(message);

				if (mLogger != null) mLogger.Log(message);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об исключении
			/// </summary>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="exc">Исключение</param>
			/// <param name="member_name">Имя метода (заполняется автоматически)</param>
			/// <param name="file_path">Полный путь к файлу (заполняется автоматически)</param>
			/// <param name="line_number">Номер строки в файле (заполняется автоматически)</param>
			//---------------------------------------------------------------------------------------------------------
			public static void LogExceptionModule(String module_name, Exception exc,
				[CallerMemberName] String member_name = "",
				[CallerFilePath] String file_path = "",
				[CallerLineNumber] Int32 line_number = 0)
			{
				TLogMessage message = new TLogMessage(module_name, exc.Message, TLogType.Exception);
				message.MemberName = member_name;
				message.FilePath = file_path;
				message.LineNumber = line_number;

				Messages.Add(message);

				if (mLogger != null) mLogger.Log(message);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================