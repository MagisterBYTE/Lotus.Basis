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
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreServiceOS Подсистема сервисов OS
		//! Подсистема вспомогательных сервисов обеспечивает дополнительную рабочую функциональность, связанную с 
		//! платформо-зависимыми реализациями и особенностями отдельных системных элементов.
		//! Сюда входит работа с диалоговыми окнами открытия/закрытия файла, объекта реализующего выполнения задачи 
		//! в отельном потоке и работа с реестром Windows.
		//! \ingroup Core
		/*@{*/
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
			internal static BackgroundWorker mDefault;
			internal static Action mOnCompute;
			internal static Action<Int32, Object> mOnProgress;
			internal static Action<Object> mOnCompleted;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Диспетчер для выполнения задачи в отдельном потоке
			/// </summary>
			public static BackgroundWorker Default
			{
				get
				{
					if (mDefault == null)
					{
						mDefault = new BackgroundWorker();
						mDefault.WorkerReportsProgress = true;
						mDefault.DoWork += OnBackgroundWorkerDoWork;
						mDefault.ProgressChanged += OnBackgroundWorkerProgressWork;
						mDefault.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
					}
					return (mDefault);
				}
			}

			/// <summary>
			/// Основной делегат для выполнения задачи
			/// </summary>
			public static Action OnCompute
			{
				get { return (mOnCompute); }
				set { mOnCompute = value; }
			}

			/// <summary>
			/// Делегат для информирования ходе выполнения задачи. Аргумент – процент выполнения и объект состояния 
			/// </summary>
			public static Action<Int32, Object> OnProgress
			{
				get { return (mOnProgress); }
				set { mOnProgress = value; }
			}

			/// <summary>
			/// Делегат для информирования окончания задачи. Аргумент – результата выполнения задачи
			/// </summary>
			public static Action<Object> OnCompleted
			{
				get { return (mOnCompleted); }
				set { mOnCompleted = value; }
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="on_compute">Основной делегат для выполнения задачи</param>
			/// <param name="on_completed">Делегат для информирования окончания задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Run(Action on_compute, Action<Object> on_completed)
			{
				mOnCompute = on_compute;
				mOnCompleted = on_completed;
				mOnProgress = null;
				Default.RunWorkerAsync();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск выполнения задачи
			/// </summary>
			/// <param name="on_compute">Основной делегат для выполнения задачи</param>
			/// <param name="on_progress">Делегат для информирования ходе выполнения задачи</param>
			/// <param name="on_completed">Делегат для информирования окончания задачи</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Run(Action on_compute, Action<Int32, Object> on_progress, Action<Object> on_completed)
			{
				mOnCompute = on_compute;
				mOnCompleted = on_completed;
				mOnProgress = on_progress;
				Default.RunWorkerAsync();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование о ходе выполнения задачи
			/// </summary>
			/// <param name="percent">Процент выполнения</param>
			/// <param name="user_state">Объект состояния</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgress(Int32 percent, Object user_state)
			{
				if (Default.WorkerReportsProgress)
				{
					Default.ReportProgress(percent, user_state);
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
			private static void OnBackgroundWorkerDoWork(Object sender, DoWorkEventArgs args)
			{
				mOnCompute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование о процессе выполнения задачи
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnBackgroundWorkerProgressWork(Object sender, ProgressChangedEventArgs args)
			{
				if (mOnProgress != null)
				{
					mOnProgress(args.ProgressPercentage, args.UserState);
				}
				else
				{
					if (args.UserState is TLogMessage)
					{
						TLogMessage message = (TLogMessage)args.UserState;
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
			private static void OnBackgroundWorkerRunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs args)
			{
				if (args.Error != null)
				{
					XLogger.LogException(args.Error);
				}
				else
				{
					mOnCompleted(args.Result);
				}
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения <see cref="BackgroundWorker"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionBackgroundWorker
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации из отдельного потока
			/// </summary>
			/// <param name="background_worker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="info">Объект информации</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogInfo(this BackgroundWorker background_worker, Int32 percent, System.Object info)
			{
				if(background_worker != null && background_worker.WorkerReportsProgress)
				{
					background_worker.ReportProgress(percent, new TLogMessage(info.ToString(), TLogType.Info));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение о простой информации из отдельного потока
			/// </summary>
			/// <param name="background_worker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="info">Объект информации</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogInfo(this BackgroundWorker background_worker, Int32 percent, 
				String module_name, System.Object info)
			{
				if (background_worker != null && background_worker.WorkerReportsProgress)
				{
					background_worker.ReportProgress(percent, new TLogMessage(module_name, info.ToString(), TLogType.Info));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке из отдельного потока
			/// </summary>
			/// <param name="background_worker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="error">Объект ошибки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogError(this BackgroundWorker background_worker, Int32 percent, System.Object error)
			{
				if (background_worker != null && background_worker.WorkerReportsProgress)
				{
					background_worker.ReportProgress(percent, new TLogMessage(error.ToString(), TLogType.Error));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Оповещение об ошибке из отдельного потока
			/// </summary>
			/// <param name="background_worker">Диспетчер отдельного потока</param>
			/// <param name="percent">Процент выполнения </param>
			/// <param name="module_name">Имя модуля/подсистемы</param>
			/// <param name="error">Объект ошибки</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReportProgressLogError(this BackgroundWorker background_worker, Int32 percent,
				String module_name, System.Object error)
			{
				if (background_worker != null && background_worker.WorkerReportsProgress)
				{
					background_worker.ReportProgress(percent, new TLogMessage(module_name, error.ToString(), TLogType.Error));
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================