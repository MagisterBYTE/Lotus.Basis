using System;
using System.Net;
using System.Threading.Tasks;

namespace Lotus.Core
{
    /** \addtogroup CoreResultsSystem
	*@{*/
    /// <summary>
    /// Класс определяющий некий результат/ответ операции.
    /// </summary>
    public class Result : ILotusResult, ICloneable
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public static readonly Result Ok = new(true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public static readonly Result Error = new(false);

        /// <summary>
        /// Результат неуспешного выполнения операции - BadRequest.
        /// </summary>
        public static readonly Result BadRequest = new(HttpStatusCode.BadRequest, false, "BadRequest");

        /// <summary>
        /// Результат неуспешного выполнения операции - Конфликт на сервере.
        /// </summary>
        public static readonly Result Conflict = new(HttpStatusCode.Conflict, false, "Conflict");

        /// <summary>
        /// Результат неуспешного выполнения операции - Нет авторизации.
        /// </summary>
        public static readonly Result Unauthorized = new(HttpStatusCode.Unauthorized, false, "Unauthorized");

        /// <summary>
        /// Результат неуспешного выполнения операции - Доступ к ресурсу запрещен.
        /// </summary>
        public static readonly Result Forbidden = new(HttpStatusCode.Forbidden, false, "Forbidden");
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(int? code = null, object? value = null)
        {
            return new Result(false, "Error", code, value);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(string message, int? code = null, object? value = null)
        {
            return new Result(false, message, code, value);
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом HttpStatusCode
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(HttpStatusCode? httpCode, string message, int? code = null, object? value = null)
        {
            return new Result(httpCode, false, message, code, value);
        }
        #endregion

        #region Failed result async methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(false, "Error", code, value));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(string message, int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(false, message, code, value));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом HttpStatusCode
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(HttpStatusCode? httpCode, string message, int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(httpCode, false, message, code, value));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(int? code = null, object? value = null)
        {
            return new Result(true, code, value);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(string message, int? code = null, object? value = null)
        {
            return new Result(true, message, code, value);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом HttpStatusCode.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(HttpStatusCode? httpCode, string message, int? code = null, object? value = null)
        {
            return new Result(httpCode, true, message, code, value);
        }
        #endregion

        #region Succeed result async methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(true, code, value));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(string message, int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(true, message, code, value));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом HttpStatusCode.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(HttpStatusCode? httpCode, string message, int? code = null, object? value = null)
        {
            return ValueTask.FromResult(new Result(httpCode, true, message, code, value));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Статус успешности выполнения операции.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Код.
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Код статуса Http.
        /// </summary>
        public HttpStatusCode? HttpCode { get; set; }

        /// <summary>
        /// Сообщение о результате выполнения операции.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Результат выполнения операции.
        /// </summary>
        public object? Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(bool status, int? code = null, object? value = null)
        {
            Succeeded = status;
            Code = code;
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(bool status, string message, int? code = null, object? value = null)
        {
            Message = message;
            Succeeded = status;
            Code = code;
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(HttpStatusCode? httpCode, bool status, string message, int? code = null, object? value = null)
        {
            HttpCode = httpCode;
            Message = message;
            Succeeded = status;
            Code = code;
            Value = value;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Полное копирование объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override string ToString()
        {
            return $"OK: {Succeeded} | Message: {Message ?? string.Empty}";
        }
        #endregion
    }

    /// <summary>
    /// Класс определяющий некий результат/ответ операции с типизированным значением результата выполнения операции.
    /// </summary>
    /// <typeparam name="TValue">Тип значения результата операции.</typeparam>
    public class Result<TValue> : Result, ILotusResult<TValue>
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public new static readonly Result<TValue> Ok = new(true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public new static readonly Result<TValue> Error = new(false);

        /// <summary>
        /// Результат неуспешного выполнения операции - BadRequest.
        /// </summary>
        public new static readonly Result<TValue> BadRequest = new(HttpStatusCode.BadRequest, false, "BadRequest");

        /// <summary>
        /// Результат неуспешного выполнения операции - Конфликт на сервере.
        /// </summary>
        public new static readonly Result<TValue> Conflict = new(HttpStatusCode.Conflict, false, "Conflict");

        /// <summary>
        /// Результат неуспешного выполнения операции - Нет авторизации.
        /// </summary>
        public new static readonly Result<TValue> Unauthorized = new(HttpStatusCode.Unauthorized, false, "Unauthorized");

        /// <summary>
        /// Результат неуспешного выполнения операции - Доступ к ресурсу запрещен.
        /// </summary>
        public new static readonly Result<TValue> Forbidden = new(HttpStatusCode.Forbidden, false, "Forbidden");
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Failed(int? code = null, TValue? value = default)
        {
            return new Result<TValue>(false, "Error", code, value);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Failed(string message, int? code = null, TValue? value = default)
        {
            return new Result<TValue>(false, message, code, value);
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом HttpStatusCode
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Failed(HttpStatusCode? httpCode, string message, int? code = null, TValue? value = default)
        {
            return new Result<TValue>(httpCode, false, message, code, value);
        }
        #endregion

        #region Failed result async methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> FailedAsync(int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(false, "Error", code, value));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> FailedAsync(string message, int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(false, message, code, value));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом HttpStatusCode
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> FailedAsync(HttpStatusCode? httpCode, string message, int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(httpCode, false, message, code, value));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(int? code = null, TValue? value = default)
        {
            return new Result<TValue>(true, code, value);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(string message, int? code = null, TValue? value = default)
        {
            return new Result<TValue>(true, message, code, value);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом HttpStatusCode.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(HttpStatusCode? httpCode, string message, int? code = null, TValue? value = default)
        {
            return new Result<TValue>(httpCode, true, message, code, value);
        }
        #endregion

        #region Succeed result async methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(true, code, value));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(string message, int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(true, message, code, value));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом HttpStatusCode.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(HttpStatusCode? httpCode, string message, int? code = null, TValue? value = default)
        {
            return ValueTask.FromResult(new Result<TValue>(httpCode, true, message, code, value));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Значение результата выполнения операции.
        /// </summary>
        new public TValue? Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(bool status, int? code = null, TValue? value = default)
        {
            Succeeded = status;
            Code = code;
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(bool status, string message, int? code = null, TValue? value = default)
        {
            Message = message;
            Succeeded = status;
            Code = code;
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Result(HttpStatusCode? httpCode, bool status, string message, int? code = null, TValue? value = default)
        {
            HttpCode = httpCode;
            Message = message;
            Succeeded = status;
            Code = code;
            Value = value;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Полное копирование объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public new object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override string ToString()
        {
            return $"OK: {Succeeded} | Message: {Message ?? string.Empty}";
        }
        #endregion
    }
    /**@}*/
}