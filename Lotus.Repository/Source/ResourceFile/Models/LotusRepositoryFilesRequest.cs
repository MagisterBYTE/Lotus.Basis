using System;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
        *@{*/
    /// <summary>
    /// Класс для получения списка изображений с учетом фильтрации и сортировки.
    /// </summary>
    public class FilesRequest : Request
    {
        /// <summary>
        /// Идентификатор автора файла.
        /// </summary>
        public Guid? AuthorId { get; set; }

        /// <summary>
        /// Идентификатор типа файла.
        /// </summary>
        public int? FileTypeId { get; set; }

        /// <summary>
        /// Идентификатор группы файла.
        /// </summary>
        public int? GroupId { get; set; }
    }
    /**@}*/
}