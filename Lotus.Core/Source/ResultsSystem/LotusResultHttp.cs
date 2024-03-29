using System.Net;
using System.Threading.Tasks;

namespace Lotus.Core
{
    /** \addtogroup CoreResultsSystem
    *@{*/
    /// <summary>
    /// Определение интерфейса для представления ответа/результата выполнения операции при работе с Http.
    /// </summary>
    public interface ILotusResultHttp : ILotusResult
    {
        /// <summary>
        /// Код статуса Http.
        /// </summary>
        public HttpStatusCode HttpCode { get; set; }
    }

    /// <summary>
    /// Класс определяющий некий результат/ответ выполнения операции при работе с Http.
    /// </summary>
    public class ResultHttp : Result, ILotusResultHttp
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public new static readonly ResultHttp Ok = new(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public new static readonly ResultHttp Bad = new(HttpStatusCode.BadRequest, -1, "Error", false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Конфликт на сервере.
        /// </summary>
        public static readonly ResultHttp Conflict = new(HttpStatusCode.Conflict, -1, "Conflict", false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Нет авторизации.
        /// </summary>
        public static readonly ResultHttp Unauthorized = new(HttpStatusCode.Unauthorized, -1, "Unauthorized", false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Доступ к ресурсу запрещен.
        /// </summary>
        public static readonly ResultHttp Forbidden = new(HttpStatusCode.Forbidden, -1, "Forbidden", false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата о неуспешности выполнения операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> FailedAsync(Result result)
        {
            return ValueTask.FromResult(new ResultHttp(result.Code, result.Message, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static ValueTask<ResultHttp> FailedAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(code, message, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.BadRequest"/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> FailedBadRequestAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.BadRequest, code, message, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Conflict"/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> FailedConflictAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.Conflict, code, message, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Unauthorized "/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> FailedUnauthorizedAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.Unauthorized, code, message, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Forbidden "/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> FailedForbiddenAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.Forbidden, code, message, false));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.OK"/>.
        /// </summary>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> SucceedOkAsync()
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.OK, 0, string.Empty, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.NoContent"/>.
        /// </summary>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> SucceedNoContentAsync()
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.NoContent, 0, string.Empty, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.Created"/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp> SucceedCreatedAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp(HttpStatusCode.Created, code, message, true));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Код статуса Http.
        /// </summary>
        public HttpStatusCode HttpCode { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public ResultHttp()
        {
            HttpCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(int code, bool status)
            : base(code, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(string message, bool status)
            : base(message, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(int code, string message, bool status)
            : base(code, message, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(HttpStatusCode httpCode, int code, string message, bool status)
            : base(code, message, status)
        {
            HttpCode = httpCode;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override string ToString()
        {
            return $"OK: {Succeeded} | Message: {Message}";
        }
        #endregion
    }

    /// <summary>
    /// Класс определяющий некий результат/ответ работы метода при работе с Http 
    /// с типизированным значением результата выполнения операции.
    /// </summary>
    /// <typeparam name="TValue">Тип значения результата операции.</typeparam>
    public class ResultHttp<TValue> : Result<TValue>, ILotusResultHttp
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public new static readonly ResultHttp<TValue> Ok = new(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public new static readonly ResultHttp<TValue> Bad = new(HttpStatusCode.BadRequest, -1, "Error", default, false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Конфликт на сервере.
        /// </summary>
        public static readonly ResultHttp<TValue> Conflict = new(HttpStatusCode.Conflict, -1, "Conflict", default, false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Нет авторизации.
        /// </summary>
        public static readonly ResultHttp<TValue> Unauthorized = new(HttpStatusCode.Unauthorized, -1, "Unauthorized", default, false);

        /// <summary>
        /// Результат неуспешного выполнения операции - Доступ к ресурсу запрещен.
        /// </summary>
        public static readonly ResultHttp<TValue> Forbidden = new(HttpStatusCode.Forbidden, -1, "Forbidden", default, false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата о неуспешности выполнения операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> FailedAsync(Result result)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(result.Code, result.Message, default, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static ValueTask<ResultHttp<TValue>> FailedAsync(int code, string message)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(code, message, default, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static ValueTask<ResultHttp<TValue>> FailedAsync(int code, string message, TValue value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(code, message, value, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.BadRequest"/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> FailedBadRequestAsync(int code, string message, TValue? value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.BadRequest, code, message, value, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Conflict"/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> FailedConflictAsync(int code, string message, TValue? value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.Conflict, code, message, value, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Unauthorized "/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> FailedUnauthorizedAsync(int code, string message, TValue? value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.Unauthorized, code, message, value, false));
        }

        /// <summary>
        /// Формирование результата о неуспешности выполнения операции с кодом <see cref="HttpStatusCode.Forbidden "/>.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> FailedForbiddenAsync(int code, string message, TValue? value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.Forbidden, code, message, value, false));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.OK"/>.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> SucceedOkAsync(TValue value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.OK, 0, string.Empty, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.Created"/>.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> SucceedCreatedAsync(TValue value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.Created, 0, string.Empty, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции с кодом <see cref="HttpStatusCode.NoContent"/>.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<ResultHttp<TValue>> SucceedNoContentAsync(TValue value)
        {
            return ValueTask.FromResult(new ResultHttp<TValue>(HttpStatusCode.NoContent, 0, string.Empty, value, true));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Код статуса Http.
        /// </summary>
        public HttpStatusCode HttpCode { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public ResultHttp()
        {
            HttpCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(int code, bool status)
            : base(code, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(int code, TValue value, bool status)
            : base(code, value, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(TValue value, bool status)
            : base(value, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(string message, TValue value, bool status)
            : base(message, value, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(int code, string message, TValue? value, bool status)
            : base(code, message, value, status)
        {
            HttpCode = status ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="httpCode">Код статуса Http.</param>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public ResultHttp(HttpStatusCode httpCode, int code, string message, TValue? value, bool status)
            : base(code, message, value, status)
        {
            HttpCode = httpCode;
        }
        #endregion
    }
    /**@}*/
}