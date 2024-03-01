using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Статический класс для определения ошибок подсистемы файловых ресурсов.
    /// </summary>
    public static class XResourceFileErrors
    {
        #region Fields
        /// <summary>
        /// Файл не найден.
        /// </summary>
        public static readonly Result NotFound = new()
        {
            Code = 1001,
            Message = "Файл не найден",
            Succeeded = false,
        };

        /// <summary>
        /// Нельзя удалить константный файл.
        /// </summary>
        public static readonly Result NotDeleteConst = new()
        {
            Code = 1002,
            Message = "Нельзя удалить константный файл",
            Succeeded = false,
        };
        #endregion
    }
    /**@}*/
}