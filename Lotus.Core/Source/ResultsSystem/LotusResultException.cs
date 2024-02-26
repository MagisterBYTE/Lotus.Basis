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
            ILotusResult result = Result.Failed;

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
                        result = XResult.Failed(exc.HResult, exc.Message, exc.Source);
                    }
                    break;
                case ArgumentOutOfRangeException exc:
                    {
                        result = XResult.Failed(exc.HResult, exc.Message, exc.ParamName);
                    }
                    break;
                case ArgumentNullException exc:
                    {
                        result = XResult.Failed(exc.HResult, exc.Message, exc.ParamName);
                    }
                    break;
                case ArgumentException exc:
                    {
                        result = XResult.Failed(exc.HResult, exc.Message, exc.ParamName);
                    }
                    break;
                default:
                    {
                        result = XResult.Failed(exception.HResult, exception.Message, exception.Source);
                    }
                    break;
            }

            return result;
        }
    }
    /**@}*/
}