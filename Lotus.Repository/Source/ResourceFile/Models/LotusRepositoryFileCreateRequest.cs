using System;
using System.IO;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
    *@{*/
    /// <summary>
    /// Класс для создания нового файла.
    /// </summary>
    public abstract class FileCreateRequest
    {
        /// <summary>
        /// Наименование файла.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Место назначение файла.
        /// </summary>
        public TResourceFileStorage Target { get; set; }

        /// <summary>
        /// Формат хранения файла.
        /// </summary>
        public TResourceFileSaveFormat SaveFormat { get; set; }

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

    /// <summary>
    /// Класс для создания ссылки на локальный файл.
    /// </summary>
    public class FileCreateLocalRequest : FileCreateRequest
    {
        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Класс для создания нового файла из потока.
    /// </summary>
    public class FileCreateStreamRequest : FileCreateRequest
    {
        /// <summary>
        /// Поток для чтения файла.
        /// </summary>
        public Stream ReadStream { get; set; } = default!;

        public FileCreateStreamRequest()
        {
            SaveFormat = TResourceFileSaveFormat.Raw;
            Target = TResourceFileStorage.Database;
        }
    }

    /// <summary>
    /// Класс для создания нового файла из байтового массива.
    /// </summary>
    public class FileCreateRawRequest : FileCreateRequest
    {
        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[] Data { get; set; } = default!;

        public FileCreateRawRequest()
        {
            SaveFormat = TResourceFileSaveFormat.Raw;
            Target = TResourceFileStorage.Database;
        }
    }

    /// <summary>
    /// Класс для создания нового файла из строки base64.
    /// </summary>
    public class FileCreateBase64Request : FileCreateRequest
    {
        /// <summary>
        /// Данные файла в формате строки base64.
        /// </summary>
        public string Data { get; set; } = default!;

        public FileCreateBase64Request()
        {
            SaveFormat = TResourceFileSaveFormat.Base64;
            Target = TResourceFileStorage.Database;
        }
    }
    /**@}*/
}