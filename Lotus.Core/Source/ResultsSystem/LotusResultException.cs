using System;

namespace Lotus.Core
{
    /** \addtogroup CoreResultsSystem
	*@{*/
    /// <summary>
    /// Определение интерфейса для поддержки концепции результата/ответа операции для исключений.
    /// </summary>
    public interface ILotusResultException
    {
        /// <summary>
        /// Получение результата/ответ операции.
        /// </summary>
        /// <returns>Результат/ответ операции.</returns>
        ILotusResult GetResult();
    }

    /// <summary>
    /// Статический класс для реализации методов расширения стандартные исключений.
    /// </summary>
    public static class XResultExceptionExtension
    {
        /// <summary>
        /// Получение по умолчанию данных исключения в виде результата.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        /// <returns>Результат/ответ операции.</returns>
        public static ILotusResult GetResultDefault(this Exception exception)
        {
            ILotusResult result = Result.BadRequest;

            if (exception == null)
            {
                return result;
            }

            if (exception is ILotusResultException resultException)
            {
                return resultException.GetResult();
            }
            switch (exception)
            {
                case NullReferenceException exc:
                    {
                        result = Result.Failed(exc.Message, exc.HResult, exc.Source ?? string.Empty);
                    }
                    break;
                case ArgumentOutOfRangeException exc:
                    {
                        result = Result.Failed(exc.Message, exc.HResult, exc.ParamName!);
                    }
                    break;
                case ArgumentNullException exc:
                    {
                        result = Result.Failed(exc.Message, exc.HResult, exc.ParamName!);
                    }
                    break;
                case ArgumentException exc:
                    {
                        result = Result.Failed(exc.Message, exc.HResult, exc.ParamName!);
                    }
                    break;
                default:
                    {
                        result = Result.Failed(exception.Message, exception.HResult, exception.Source ?? string.Empty);
                    }
                    break;
            }

            return result;
        }
    }
    /**@}*/
}