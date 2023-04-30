//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема логирования
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusLoggerCommon.cs
*		Определение общих типов и структур данных для подсистемы логирования.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup CoreLogger Подсистема логирования
         * \ingroup Core
         * \brief Подсистема логирования представляет собой подсистему, предназначенную для трассировки, диагностики, отладки 
			и логирования процесса работы приложения.
		 * \details
			## Описание
		    Центральный диспетчер подсистемы логирования \ref Lotus.Core.XLogger применяется для трассировки, диагностики, 
			отладки и логирования процесса работы приложения и обеспечивает хранение и регистрацию как всех 
			системных/межплатформенных сообщений, так и всех сообщений от бизнес-логики приложения.

			## Использование
			1. Реализовать интерфейс(логгер) \ref Lotus.Core.ILoggerView для вывода и отображения сообщений
			2. Присоединить объект-логгер к свойству \ref Lotus.Core.XLogger.Logger
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тип сообщения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TLogType
		{
			/// <summary>
			/// Информационное сообщение
			/// </summary>
			Info,

			/// <summary>
			/// Предупреждающие сообщение
			/// </summary>
			Warning,

			/// <summary>
			/// Сообщение об ошибке
			/// </summary>
			Error,

			/// <summary>
			/// Исключение
			/// </summary>
			Exception,

			/// <summary>
			/// Информация об удачном выполнении задачи
			/// </summary>
			Succeed,

			/// <summary>
			/// Информация об неудачном выполнении задачи
			/// </summary>
			Failed
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Структура сообщения лога
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public struct TLogMessage
		{
			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Имя модуля/подсистемы
			/// </summary>
			public String Module { get; set; }

			/// <summary>
			/// Текст сообщения
			/// </summary>
			public String Text { get; set; }

			/// <summary>
			/// Имя метода в котором произошел вызов
			/// </summary>
			public String MemberName { get; set; }

			/// <summary>
			/// Полный путь к файлу где произошел вызов
			/// </summary>
			public String FilePath { get; set; }

			/// <summary>
			/// Номер строки в файле где произошел вызов
			/// </summary>
			public Int32 LineNumber { get; set; }

			/// <summary>
			/// Время сообщения
			/// </summary>
			public Single Time { get; set; }

			/// <summary>
			/// Тип сообщения
			/// </summary>
			public TLogType Type { get; set; }
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Краткая трассировка сообщения с указанием файла, строки и метода
			/// </summary>
			public String TraceShort 
			{
				get { return (MemberName + " [" + Path.GetFileNameWithoutExtension(FilePath) + ":" + LineNumber.ToString() + "]"); } 
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="text">Имя сообщения</param>
			/// <param name="type">Тип сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public TLogMessage(String text, TLogType type)
			{
				Module = "";
				Text = text;
				MemberName = "";
				FilePath = "";
				LineNumber = 0;
				Time = 0;
				Type = type;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="module">Имя модуля/подсистемы</param>
			/// <param name="text">Имя сообщения</param>
			/// <param name="type">Тип сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public TLogMessage(String module, String text, TLogType type)
			{
				Module = module;
				Text = text;
				MemberName = "";
				FilePath = "";
				LineNumber = 0;
				Time = 0;
				Type = type;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разборка строки трассировки на компоненты
			/// </summary>
			/// <param name="trace">Строка трассировки</param>
			//---------------------------------------------------------------------------------------------------------
			public void ParseTrace(String trace)
			{
				var items = trace.Split(XChar.SeparatorNewCarriageLine);
				if(items.Length > 0)
				{
					Text = items[0];

					if(items.Length > 1)
					{
						var data = items[1];

						// Находим имя файла
						var index_file = data.IndexOf("(at");
						if(index_file > -1)
						{
							// Формируем имя метода
							MemberName = data.Remove(index_file);

							// Находим последнюю точку
							var index_dot = MemberName.LastIndexOf('.');
							if(index_dot > -1)
							{
								// Удаляем названия пространства имен
								MemberName = MemberName.Remove(0, index_dot);
							}

							MemberName = MemberName.Trim();

							// Формируем имя файла
							FilePath = data.Remove(0, index_file);

							// Удаляем префикс "(at"
							FilePath = FilePath.Remove(0, 3);

							// Находим двоеточие и удаляем до него
							var index_colon = FilePath.LastIndexOf(':');
							if(index_colon > -1)
							{
								FilePath = FilePath.Remove(index_colon);
								FilePath = Path.GetFileNameWithoutExtension(FilePath);
							}

							FilePath = FilePath.Trim();
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разборка строки трассировки на компоненты
			/// </summary>
			/// <param name="trace">Строка трассировки</param>
			//---------------------------------------------------------------------------------------------------------
			public void ParseStackTrace(String trace)
			{
				// Получаем список строк трассировки
				var items = trace.Split(XChar.SeparatorNewCarriageLine, StringSplitOptions.RemoveEmptyEntries);

				if (items.Length > 1)
				{
					// Делаем обратный порядок чтобы получить правильную последовательность
					Array.Reverse(items);

					for (var i = 0; i < items.Length - 1; i++)
					{
						var line_trace = items[i];

						if (i == 0)
						{
							MemberName = ExtractMemberName(line_trace);
							FilePath = ExtractFileName(line_trace);
						}
						else
						{
							MemberName += XString.HierarchySpaces[i] + ExtractMemberName(line_trace);
							FilePath += "\n" + ExtractFileName(line_trace);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Извлечь имя метода из строки трассировки
			/// </summary>
			/// <param name="line_trace">Строка трассировки</param>
			/// <returns>Имя метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public String ExtractMemberName(String line_trace)
			{
				// Находим имя файла
				var index_file = line_trace.LastIndexOf('(');
				if (index_file > -1)
				{
					// Формируем имя метода
					var member_name = line_trace.Remove(index_file);

					// Находим последнюю точку
					var index_dot = member_name.LastIndexOf('.');
					if (index_dot > -1)
					{
						// Удаляем названия пространства имен
						member_name = member_name.Remove(0, index_dot + 1);
					}

					// Удаляем все аргументы
					var start = member_name.IndexOf('(');
					var end = member_name.IndexOf(')');
					if(end - start > 1)
					{
						member_name = member_name.Remove(start + 1, (end - start));
					}

					return (member_name.Trim('\n', ' '));
				}

				return ("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Извлечь имя метода из строки трассировки
			/// </summary>
			/// <param name="line_trace">Строка трассировки</param>
			/// <returns>Имя метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public String ExtractFileName(String line_trace)
			{
				// Находим имя файла
				var index_file = line_trace.LastIndexOf('(');
				if (index_file > -1)
				{
					// Формируем имя файла
					var file_path = line_trace.Remove(0, index_file);

					// Находим двоеточие и удаляем до него
					var index_colon = file_path.LastIndexOf(':');
					if (index_colon > -1)
					{
						file_path = file_path.Remove(index_colon);
						file_path = Path.GetFileName(file_path);
					}

					file_path = file_path.Trim('\n', ' ', '(', ')');

					return (file_path);
				}

				return ("");
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================