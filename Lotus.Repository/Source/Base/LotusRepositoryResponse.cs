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
        public static readonly Response Ok = new Response();
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
        /// <param name="message">Сообщение о результате выполнения метода.</param>
        /// <returns>Данные.</returns>
        public static Response Failed(int code, string message)
        {
            return new Response(code, message);
        }
        #endregion

        #region Succeed methods 
        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <returns>Данные</returns>
        public static Response Succeed()
        {
            return Response.Ok;
        }

        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения метода.</param>
        /// <returns>Данные.</returns>
        public static Response Succeed(int code, string message)
        {
            return new Response(code, message, default, true);
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
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения метода.</param>
        public Response(int code, string message)
        {
            Result = new Result(code, message, null, false);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Response(int code, string message, object? value, bool status)
        {
            Result = new Result(code, message, value, status);
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
        public new static readonly Response<TPayload> Ok = new Response<TPayload>();
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
        /// <param name="message">Сообщение о результате выполнения метода.</param>
        /// <returns>Данные.</returns>
        public new static Response<TPayload> Failed(int code, string message)
        {
            return new Response<TPayload>(code, message);
        }
        #endregion

        #region Succeed methods 
        /// <summary>
        /// Формирование данных в случае успешности выполнения метода/операции.
        /// </summary>
        /// <param name="data">Объект данных.</param>
        /// <returns>Данные.</returns>
        public static Response<TPayload> Succeed(TPayload data)
        {
            return new Response<TPayload>(data);
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
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        public Response(int code, string message)
        {
            Result = new Result(code, message, null, false);
        }
        #endregion
    }
    /**@}*/
}