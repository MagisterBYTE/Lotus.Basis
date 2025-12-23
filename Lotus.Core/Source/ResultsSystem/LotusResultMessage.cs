namespace Lotus.Core
{
    /** \addtogroup CoreResultsSystem
	*@{*/
    /// <summary>
    /// Определение интерфейса для отдельного текста ответа/результата операции.
    /// </summary>
    public interface ILotusResultMessage
    {
        /// <summary>
        /// Условный уровень текста.
        /// </summary>
        /// <remarks>
        /// В зависимости от контекста, уровень текста может по-разному интерпретироваться или его вообще не может быть.
        /// </remarks>
        int? Level { get; }

        /// <summary>
        /// Произвольный текст.
        /// </summary>
        string Text { get; }
    }

    /// <summary>
    /// Произвольный текст ответа/результата операции.
    /// </summary>
    public class ResultMessage : ILotusResultMessage
    {
        /// <inheritdoc/>
        public int? Level { get; set; }

        /// <inheritdoc/>
        public string Text { get; set; }
    }
    /**@}*/
}