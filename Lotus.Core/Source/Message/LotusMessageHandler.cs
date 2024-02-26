namespace Lotus.Core
{
    /** \addtogroup CoreMessage
	*@{*/
    /// <summary>
    /// Статический класс для определения типовых кодов обработки сообщения.
    /// </summary>
    /// <remarks>
    /// Сообщение может быть обработано с возвратом любого результата за исключением системных.
    /// </remarks>
    public static class XMessageHandlerResultCode
    {
        /// <summary>
        /// Сообщение выполнено с отрицательным результатом.
        /// </summary>
        public const int NEGATIVE_RESULT = -1;

        /// <summary>
        /// Сообщение не обработано.
        /// </summary>
        public const int NOT_PROCESSED = 0;

        /// <summary>
        /// Сообщение обработано.
        /// </summary>
        public const int PROCESSED = 1;
    }

    /// <summary>
    /// Интерфейс для определения возможности обработки сообщения.
    /// </summary>
    public interface ILotusMessageHandler
    {
        /// <summary>
        /// Основной метод для обработки сообщения.
        /// </summary>
        /// <param name="args">Аргументы сообщения.</param>
        /// <returns>Результат обработки сообщения.</returns>
        int OnMessageHandler(CMessageArgs args);
    }
    /**@}*/
}