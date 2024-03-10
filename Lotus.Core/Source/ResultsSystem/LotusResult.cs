using System;

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
        public static readonly Result Ok = new Result(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public static readonly Result Bad = new Result(-1, "Error", null, false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Failed(int code)
        {
            return new Result(code, "Error", default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Failed(int code, string message)
        {
            return new Result(code, message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Данные.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Failed(int code, string message, object? data)
        {
            return new Result(code, message, data, false);
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Succeed(object data)
        {
            return new Result(0, data, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Succeed(int code, object? data)
        {
            return new Result(code, data, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result Succeed(int code, string message, object? data)
        {
            return new Result(code, message, data, true);
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
        public int Code { get; set; }

        /// <summary>
        /// Сообщение о результате выполнения операции.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дополнительные данные.
        /// </summary>
        public object? Data { get; set; }
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
        /// <param name="code">Код.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, bool status)
        {
            Code = code;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(string message, bool status)
        {
            Message = message;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, object? data, bool status)
        {
            Code = code;
            Data = data;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(string message, object? data, bool status)
        {
            Message = message;
            Data = data;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, string message, object? data, bool status)
        {
            Code = code;
            Message = message;
            Data = data;
            Succeeded = status;
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
            return $"OK: {Succeeded} | Message: {Message}";
        }
        #endregion
    }

    /// <summary>
    /// Класс определяющий некий результат/ответ операции с типизированными дополнительным данными.
    /// </summary>
    /// <typeparam name="TData">Тип данных.</typeparam>
    public class Result<TData> : Result, ILotusResult<TData>
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public new static readonly Result<TData> Ok = new Result<TData>(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public new static readonly Result<TData> Bad = new Result<TData>(-1, "Error", default, false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат/ответ операции.</returns>
        public new static Result<TData> Failed(int code)
        {
            return new Result<TData>(code, "Error", default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат/ответ операции.</returns>
        public new static Result<TData> Failed(int code, string message)
        {
            return new Result<TData>(code, message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Данные.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Failed(int code, string message, TData data)
        {
            return new Result<TData>(code, message, data, false);
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Succeed(TData data)
        {
            return new Result<TData>(0, data, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Succeed(int code, TData? data)
        {
            return new Result<TData>(code, data, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Succeed(int code, string message, TData? data)
        {
            return new Result<TData>(code, message, data, true);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Данные.
        /// </summary>
        new public TData? Data { get; set; }
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
        /// <param name="code">Код.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, bool status)
            : base(code, status)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, TData? data, bool status)
            : base(code, data, status)
        {
            Data = data;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(TData data, bool status)
        {
            base.Data = data;
            Data = data;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(string message, TData? data, bool status)
            : base(message, data, status)
        {
            Data = data;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Дополнительные данные.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, string message, TData? data, bool status)
            : base(code, message, data, status)
        {
            Data = data;
        }
        #endregion
    }
    /**@}*/
}