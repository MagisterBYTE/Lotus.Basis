namespace Lotus.Core
{
    /** \addtogroup CoreLogger
	*@{*/
    /// <summary>
    /// Интерфейс регистратора(логгера) для визуального отображения оповещений.
    /// </summary>
    /// <remarks>
    /// Основная задача интерфейса представить механизм для визуального отображения оповещений.
    /// </remarks>
    public interface ILotusLoggerView
    {
        #region Main methods 
        /// <summary>
        /// Общее оповещение.
        /// </summary>
        /// <param name="text">Имя сообщения.</param>
        /// <param name="type">Тип сообщения.</param>
        void Log(string text, TLogType type);

        /// <summary>
        /// Общее оповещение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Log(TLogMessage message);

        /// <summary>
        /// Оповещение определённого модуля/подсистемы.
        /// </summary>
        /// <param name="moduleName">Имя модуля/подсистемы.</param>
        /// <param name="text">Имя сообщения.</param>
        /// <param name="type">Тип сообщения.</param>
        void LogModule(string moduleName, string text, TLogType type);
        #endregion
    }
    /**@}*/
}