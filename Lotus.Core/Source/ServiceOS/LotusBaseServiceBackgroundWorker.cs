//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сервисов OS
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseServiceBackgroundWorker.cs
*		Диспетчер для выполнения задачи/метода в отдельном потоке.
*		Реализация диспетчера который обеспечивает удобное выполнения задачи/метода в отдельном потоке на основе 
*	системного объекта BackgroundWorker.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup CoreServiceOS Подсистема сервисов OS
         * \ingroup Core
         * \brief Подсистема вспомогательных сервисов обеспечивает дополнительную рабочую функциональность, связанную с 
			платформо-зависимыми реализациями и особенностями отдельных системных элементов.
		 * \details Сюда входит работа с диалоговыми окнами открытия/закрытия файла, объекта реализующего выполнения задачи 
			в отельном потоке и работа с реестром Windows.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Диспетчер для выполнения задачи/метода в отдельном потоке
		/// </summary>
		/// <remarks>
		/// Реализация диспетчера который обеспечивает удобное выполнения задачи/метода в отдельном потоке на основе 
		/// системного объекта <see cref="BackgroundWorker"/> 
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XBackgroundManager
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal static BackgroundWorker _default;
			internal static Action _onCompute;
			internal static Action<Int32, Object?>? _onProgress;
			internal static Action<Object?> _onCompleted;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Диспетчер для выполнения задачи в отдельном потоке
			/// </summary>
			public static BackgroundWorker Default
			{
				get
				{
					if (_default == null)
					{
						_default = new BackgroundWorker();
						_default.WorkerReportsProgress = true;
						_default.DoWork += OnBackgroundWorkerDoWork;
						_default.ProgressChanged += OnBackgroundWorkerProgressWork;
						_default.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
					}
					return _default;
				}
			}

			/// <summary>
			/// Основной делегат для выполнения задачи
			/// </summary>
			public static Action OnCompute
			{
				get { return _onCompute; }
				set { _onCompute = value; }
			}

			/// <summary>
			/// Делегат для информирования ходе выполнения задачи. Аргумент – процент выполнения и объект состояния 
			/// </summary>
			public static Action<Int32, Object?>? OnProgress
			{
				get { return _onProgress; }
				set { _onProgress = value; }
			}

			/// <summary>
			/// Делегат для информирования окончания задачи. Аргумент – результата выполнения задачи
			/// </summary>
			public static Action<Object?> OnCompleted
			{
				get { return _onCompleted; }
				set { _onCompleted = value; }
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="onCompute">Основной делегат для выполнения задачи</param>
			/// <param name="onCompleted">Делегат для информирования окончания задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Run(Action onCompute, Action<Object?> onCompleted)
			{
				_onCompute = onCompute;
				_onCompleted = onCompleted;
				_onProgress = null;
				Default.RunWorkerAsync();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="onCompute">Основной делегат для выполнения задачи</param>
			/// <param name="onProgress">Делегат для информирования ходе выполнения задачи</param>
			/// <param name="onCompleted">Делегат для информирования окончания задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Run(Action onCompute, Action<Int32, Object?> onProgress, Action<Object?> onCompleted)
			{
				_onCompute = onCompute;
				_onCompleted = onCompleted;
				_onProgress = onProgress;
				Default.RunWorkerAsync();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование о ходе выполнения задачи
			/// </summary>
			/// <param name="percent">Процент выполнения</param>
			/// <param name="userState">Объект состояния</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgress(Int32 percent, Object userState)
			{
				if (Default.WorkerReportsProgress)
				{
					Default.ReportProgress(percent, userState);
				}
			}
			#endregion

			#region ======================================= ОБРАБОТЧИКИ СОБЫТИЙ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Основной метод для выполнения задачи в фоновом потоке
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnBackgroundWorkerDoWork(Object? sender, DoWorkEventArgs args)
			{
				_onCompute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование о процессе выполнения задачи
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnBackgroundWorkerProgressWork(Object? sender, ProgressChangedEventArgs args)
			{
				if (_onProgress != null)
				{
					_onProgress(args.ProgressPercentage, args.UserState);
				}
				else
				{
					if (args.UserState is TLogMessage message)
					{
						XLogger.Log(message);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Окончание выполнения задачи
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnBackgroundWorkerRunWorkerCompleted(Object? sender, RunWorkerCompletedEventArgs args)
			{
				if (args.Error != null)
				{
					XLogger.LogException(args.Error);
				}
				else
				{
					_onCompleted(args.Result);
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения <see cref="BackgroundWorker"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XBackgroundWorkerExtension
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации из отдельного потока
			/// </summary>
			/// <param name="backgroundWorker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="info">Объект информации</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogInfo(this BackgroundWorker backgroundWorker, Int32 percent, System.Object? info)
			{
				if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
				{
					var text = info != null ? info.ToString()! : $"Persent: {percent}";
					backgroundWorker.ReportProgress(percent, new TLogMessage(text, TLogType.Info));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации из отдельного потока
			/// </summary>
			/// <param name="backgroundWorker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="info">Объект информации</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogInfo(this BackgroundWorker backgroundWorker, Int32 percent,
				String moduleName, System.Object? info)
			{
				if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
				{
					var text = info != null ? info.ToString()! : $"Persent: {percent}";
					backgroundWorker.ReportProgress(percent, new TLogMessage(moduleName, text, TLogType.Info));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке из отдельного потока
			/// </summary>
			/// <param name="backgroundWorker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="error">Объект ошибки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogError(this BackgroundWorker backgroundWorker, Int32 percent, System.Object? error)
			{
				if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
				{
					var text = error != null ? error.ToString()! : $"Persent: {percent}";
					backgroundWorker.ReportProgress(percent, new TLogMessage(text, TLogType.Error));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке из отдельного потока
			/// </summary>
			/// <param name="backgroundWorker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="moduleName">Имя модуля/подсистемы</param>
			/// <param name="error">Объект ошибки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogError(this BackgroundWorker backgroundWorker, Int32 percent,
				String moduleName, System.Object? error)
			{
				if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
				{
					var text = error != null ? error.ToString()! : $"Persent: {percent}";
					backgroundWorker.ReportProgress(percent, new TLogMessage(moduleName, text, TLogType.Error));
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