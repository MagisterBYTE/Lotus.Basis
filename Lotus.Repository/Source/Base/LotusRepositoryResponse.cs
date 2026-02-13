using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryBase
	*@{*/
    /// <summary>
    /// Базовый интерфейс получения данных.
    /// </summary>
    public interface ILotusResponse
    {
        /// <summary>
        /// Результат получения данных.
        /// </summary>
        ILotusResult? Result { get; set; }
    }

    /// <summary>
    /// Интерфейс получения данных.
    /// </summary>
    /// <typeparam name="TPayload">Тип данных.</typeparam>
    public interface ILotusResponse<TPayload> : ILotusResponse
    {
        /// <summary>
        /// Данные.
        /// </summary>
        TPayload Payload { get; set; }
    }

    /// <summary>
    /// Класс для получения данных.
    /// </summary>
    public class Response : ILotusResponse
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения.
        /// </summary>
        public static readonly Response Ok = new();
        #endregion

        #region Failed methods 
        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Данные.</returns>
        public static Response Failed(Result result)
        {
            return new Response(result);
        }

        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public static Response Failed(int? code = null, object? value = null)
        {
            return new Response(false, code, value);
        }

        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public static Response Failed(string message, int? code = null, object? value = null)
        {
            return new Response(false, message, code, value);
        }
        #endregion

        #region Succeed methods 
        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Данные.</returns>
        public static Response Succeed(Result result)
        {
            return new Response(result);
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public static Response Succeed(int? code = null, object? value = null)
        {
            return new Response(true, code, value);
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public static Response Succeed(string message, int? code = null, object? value = null)
        {
            return new Response(true, message, code, value);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Результат получения данных.
        /// </summary>
        public ILotusResult? Result { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="result">Данные.</param>
        public Response(ILotusResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Response(bool status, int? code = null, object? value = null)
        {
            Result = new Result(status, code, value);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Response(bool status, string message, int? code = null, object? value = null)
        {
            Result = new Result(status, message, code, value);
        }
        #endregion
    }

    /// <summary>
    /// Класс для получения данных.
    /// </summary>
    /// <typeparam name="TPayload">Тип данных.</typeparam>
    public class Response<TPayload> : Response, ILotusResponse<TPayload>
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения.
        /// </summary>
        public new static readonly Response<TPayload> Ok = new();
        #endregion

        #region Failed methods 
        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Failed(Result result)
        {
            return new Response<TPayload>(result);
        }

        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Failed(int? code = null, object? value = null)
        {
            return new Response<TPayload>(false, code, value);
        }

        /// <summary>
        /// Формирование данных в случае неуспешности выполнения метода/операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Failed(string message, int? code = null, object? value = null)
        {
            return new Response<TPayload>(false, message, code, value);
        }
        #endregion

        #region Succeed methods 
        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="payload">Полезные данные.</param>
        /// <returns>Данные.</returns>
        public static Response<TPayload> Succeed(TPayload payload)
        {
            return new Response<TPayload>(payload);
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="result">Результат/ответ.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Succeed(Result result)
        {
            return new Response<TPayload>(result);
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Succeed(int? code = null, object? value = null)
        {
            return new Response<TPayload>(true, code, value);
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Данные.</returns>
        public new Response<TPayload> Succeed(string message, int? code = null, object? value = null)
        {
            return new Response<TPayload>(true, message, code, value);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Данные.
        /// </summary>
        public TPayload Payload { get; set; } = default!;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные поверхности предустановленными значениями.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="payload">Данные.</param>
        public Response(TPayload payload)
        {
            Payload = payload;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="result">Ответ/результат операции.</param>
        public Response(ILotusResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Response(bool status, int? code = null, object? value = null)
        {
            Result = new Result(status, code, value);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="status">Статус выполнения операции.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        public Response(bool status, string message, int? code = null, object? value = null)
        {
            Result = new Result(status, message, code, value);
        }
        #endregion
    }
    /**@}*/
}