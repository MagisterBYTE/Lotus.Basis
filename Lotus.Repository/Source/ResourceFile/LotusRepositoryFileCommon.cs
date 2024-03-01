namespace Lotus.Repository
{
    /**
     * \defgroup RepositoryResourceFile Подсистема файловых ресурсов
     * \ingroup Repository
     * \brief Подсистема файловых ресурсов предназначения для работы с отдельной сущностью – файла - независимо 
		от места и способа его хранения.
     * @{
     */
    /// <summary>
    /// Место хранение файла.
    /// </summary>
    public enum TResourceFileStorage
    {
        /// <summary>
        /// Файл храниться локально.
        /// </summary>
        /// <remarks>
        /// В данном случае просто сохраняется информация об его идентификаторе.
        /// </remarks>
        Local,

        /// <summary>
        /// Файл храниться на сервере.
        /// </summary>
        Server,

        /// <summary>
        /// Файл храниться в базе данных.
        /// </summary>
        Database,
    }

    /// <summary>
    /// Формат хранения файла в базе данных.
    /// </summary>
    public enum TResourceFileSaveFormat
    {
        /// <summary>
        /// Данные файла в формате строки base64.
        /// </summary>
        Base64,

        /// <summary>
        /// Данные файла в формате байтового массива.
        /// </summary>
        Raw
    }
    /**@}*/
}