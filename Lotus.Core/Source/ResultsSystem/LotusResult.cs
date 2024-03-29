using System;
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
        public static readonly Result Ok = new(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public static readonly Result Bad = new(-1, "Error", null, false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(int code)
        {
            return new Result(code, "Error", default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(int code, string message)
        {
            return new Result(code, message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Failed(int code, string message, object value)
        {
            return new Result(code, message, value, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(int code)
        {
            return ValueTask.FromResult(new Result(code, "Error", default, false));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(int code, string message)
        {
            return ValueTask.FromResult(new Result(code, message, default, false));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> FailedAsync(int code, string message, object value)
        {
            return ValueTask.FromResult(new Result(code, message, value, false));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(object value)
        {
            return new Result(0, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(int code, object? value)
        {
            return new Result(code, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result Succeed(int code, string message, object? value)
        {
            return new Result(code, message, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(object value)
        {
            return ValueTask.FromResult(new Result(0, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(int code, object? value)
        {
            return ValueTask.FromResult(new Result(code, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result> SucceedAsync(int code, string message, object? value)
        {
            return ValueTask.FromResult(new Result(code, message, value, true));
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
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, object? value, bool status)
        {
            Code = code;
            Value = value;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(string message, object? value, bool status)
        {
            Message = message;
            Value = value;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, string message, object? value, bool status)
        {
            Code = code;
            Message = message;
            Value = value;
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
    /// Класс определяющий некий результат/ответ операции с типизированным значением результата выполнения операции.
    /// </summary>
    /// <typeparam name="TValue">Тип значения результата операции.</typeparam>
    public class Result<TValue> : Result, ILotusResult<TValue>
    {
        #region Const
        /// <summary>
        /// Результат успешного выполнения операции.
        /// </summary>
        public new static readonly Result<TValue> Ok = new(0, true);

        /// <summary>
        /// Результат неуспешного выполнения операции.
        /// </summary>
        public new static readonly Result<TValue> Bad = new(-1, "Error", default, false);
        #endregion

        #region Failed result methods
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static Result<TValue> Failed(int code)
        {
            return new Result<TValue>(code, "Error", default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static Result<TValue> Failed(int code, string message)
        {
            return new Result<TValue>(code, message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Failed(int code, string message, TValue value)
        {
            return new Result<TValue>(code, message, value, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static ValueTask<Result<TValue>> FailedAsync(int code)
        {
            return ValueTask.FromResult(new Result<TValue>(code, "Error", default, false));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public new static ValueTask<Result<TValue>> FailedAsync(int code, string message)
        {
            return ValueTask.FromResult(new Result<TValue>(code, message, default, false));
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> FailedAsync(int code, string message, TValue value)
        {
            return ValueTask.FromResult(new Result<TValue>(code, message, value, false));
        }
        #endregion

        #region Succeed result methods
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(TValue value)
        {
            return new Result<TValue>(0, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(int code, TValue? value)
        {
            return new Result<TValue>(code, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static Result<TValue> Succeed(int code, string message, TValue? value)
        {
            return new Result<TValue>(code, message, value, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(TValue value)
        {
            return ValueTask.FromResult(new Result<TValue>(0, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(int code, TValue? value)
        {
            return ValueTask.FromResult(new Result<TValue>(code, value, true));
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <returns>Результат выполнения операции.</returns>
        public static ValueTask<Result<TValue>> SucceedAsync(int code, string message, TValue? value)
        {
            return ValueTask.FromResult(new Result<TValue>(code, message, value, true));
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
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, TValue? value, bool status)
            : base(code, value, status)
        {
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(TValue value, bool status)
        {
            base.Value = value;
            Value = value;
            Succeeded = status;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(string message, TValue? value, bool status)
            : base(message, value, status)
        {
            Value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="value">Значение результата выполнения операции.</param>
        /// <param name="status">Статус выполнения операции.</param>
        public Result(int code, string message, TValue? value, bool status)
            : base(code, message, value, status)
        {
            Value = value;
        }
        #endregion
    }
    /**@}*/
}