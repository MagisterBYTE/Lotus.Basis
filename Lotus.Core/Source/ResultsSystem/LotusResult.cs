using System;

namespace Lotus.Core
{
    /** \addtogroup CoreResultsSystem
	*@{*/
    /// <summary>
    /// Статический класс для формирования ответа/результата выполнения операции.
    /// </summary>
    public static class XResult
    {
        #region Failed result
        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <typeparam name="TData">Тип объекта.</typeparam>
        /// <param name="result">Результат/ответ операции.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Failed<TData>(Result result)
        {
            return new Result<TData>(result.Code, result.Message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <typeparam name="TData">Тип объекта.</typeparam>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Failed<TData>(int code, string message)
        {
            return new Result<TData>(code, message, default, false);
        }

        /// <summary>
        /// Формирование результата/ответа о неуспешности выполнения операции.
        /// </summary>
        /// <typeparam name="TData">Тип объекта.</typeparam>
        /// <param name="code">Код.</param>
        /// <param name="message">Сообщение о результате выполнения операции.</param>
        /// <param name="data">Данные.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Failed<TData>(int code, string message, TData data)
        {
            return new Result<TData>(code, message, data, false);
        }
        #endregion

        #region Succeed result
        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <typeparam name="TData">Тип объекта.</typeparam>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Succeed<TData>(TData data)
        {
            return new Result<TData>(data, true);
        }

        /// <summary>
        /// Формирование результата о успешности выполнения операции.
        /// </summary>
        /// <typeparam name="TData">Тип объекта.</typeparam>
        /// <param name="code">Код.</param>
        /// <param name="data">Объект.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static Result<TData> Succeed<TData>(int code, TData data)
        {
            return new Result<TData>(code, data, true);
        }
        #endregion
    }

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
        public static readonly Result Failed = new Result(0, "Error", null, false);
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
        new public static readonly Result<TData> Ok = new Result<TData>(0, true);
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