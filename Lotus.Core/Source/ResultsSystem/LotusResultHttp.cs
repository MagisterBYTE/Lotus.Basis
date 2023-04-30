//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема результата операции
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusResultHttp.cs
*		Определение класса для представления ответа/результата выполнения операции при работе с Http.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Net;
using System.Threading.Tasks;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreResultsSystem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления ответа/результата выполнения операции при работе с Http
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusResultHttp : ILotusResult
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Код статуса Http
			/// </summary>
			public HttpStatusCode HttpCode { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для формирования ответа/результата выполнения операции
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XResultHttp
		{
			#region ======================================= НЕУСПЕШЕЫЙ РЕЗУЛЬТАТ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции
			/// </summary>
			/// <param name="result">Результат/ответ</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp> FailedAsync(Result result)
			{
				return ValueTask.FromResult(new ResultHttp(result.Code, result.Message, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp> FailedAsync(Int32 code, String message)
			{
				return ValueTask.FromResult(new ResultHttp(code, message, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="result">Результат/ответ</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedAsync<TData>(Result result)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(result.Code, result.Message, default, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedAsync<TData>(Int32 code, String message)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(code, message, default, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Данные</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedAsync<TData>(Int32 code, String message, TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(code, message, data, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.BadRequest"/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Данные</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedBadRequestAsync<TData>(Int32 code, String message, TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.BadRequest, code, message, data, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Unauthorized "/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Данные</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedUnauthorizedAsync<TData>(Int32 code, String message, TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.Unauthorized, code, message, data, false));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Forbidden "/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Данные</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> FailedForbiddenAsync<TData>(Int32 code, String message, TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.Forbidden, code, message, data, false));
			}
			#endregion

			#region ======================================= УСПЕШЕЫЙ РЕЗУЛЬТАТ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.OK"/>
			/// </summary>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp> SucceedOkAsync()
			{
				return ValueTask.FromResult(new ResultHttp(HttpStatusCode.OK, 0, null, true));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.NoContent"/>
			/// </summary>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp> SucceedNoContentAsync()
			{
				return ValueTask.FromResult(new ResultHttp(HttpStatusCode.NoContent, 0, null, true));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.OK"/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="data">Объект</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> SucceedOkAsync<TData>(TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.OK, 0, null, data, true));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.Created"/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="data">Объект</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> SucceedCreatedAsync<TData>(TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.Created, 0, null, data, true));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.NoContent"/>
			/// </summary>
			/// <typeparam name="TData">Тип объекта</typeparam>
			/// <param name="data">Объект</param>
			/// <returns>Результат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static ValueTask<ResultHttp<TData>> SucceedNoContentAsync<TData>(TData data)
			{
				return ValueTask.FromResult(new ResultHttp<TData>(HttpStatusCode.NoContent, 0, null, data, true));
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий некий результат/ответ выполнения операции при работе с Http
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class ResultHttp : Result, ILotusResultHttp
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Код статуса Http
			/// </summary>
			public HttpStatusCode HttpCode { get; set; }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp()
			{
				HttpCode = HttpStatusCode.OK;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(Int32 code, Boolean status)
				:base(code, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(String message, Boolean status)
				: base(message, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(Int32 code, String message, Boolean status)
				: base(code, message, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="httpCode">Код статуса Http</param>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(HttpStatusCode httpCode, Int32 code, String message, Boolean status)
				: base(code, message, status)
			{
				HttpCode = httpCode;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return $"OK: {Succeeded} | Message: {Message}";
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий некий результат/ответ работы метода при работе с Http
		/// </summary>
		/// <typeparam name="TData">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class ResultHttp<TData> : Result<TData>, ILotusResultHttp
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Код статуса Http
			/// </summary>
			public HttpStatusCode HttpCode { get; set; }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp()
			{
				HttpCode = HttpStatusCode.OK;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(Int32 code, Boolean status)
				: base(code, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="data">Дополнительные данные</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(Int32 code, TData data, Boolean status)
				: base(code, data, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data">Дополнительные данные</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(TData data, Boolean status)
				: base(data, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Дополнительные данные</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(String message, TData data, Boolean status)
				: base(message, data, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Дополнительные данные</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(Int32 code, String message, TData data, Boolean status)
				:base(code, message, data, status)
			{
				HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="httpCode">Код статуса Http</param>
			/// <param name="code">Код</param>
			/// <param name="message">Сообщение о результате выполнения операции</param>
			/// <param name="data">Дополнительные данные</param>
			/// <param name="status">Статус выполнения операции</param>
			//---------------------------------------------------------------------------------------------------------
			public ResultHttp(HttpStatusCode httpCode, Int32 code, String message, TData data, Boolean status)
				: base(code, message, data, status)
			{
				HttpCode = httpCode;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================