using System;

using Lotus.Core;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Класс для представления данных файла.
    /// </summary>
    public class FileDto : IdentifierDtoId<Guid>
    {
        /// <summary>
        /// Наименование файла.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Размер изображения в байтах.
        /// </summary>
        public int? SizeInBytes { get; set; }
    }

    /// <summary>
    /// Класс для представления данных файла в формате строки base64.
    /// </summary>
    public class FileBase64Dto : FileDto
    {
        /// <summary>
        /// Данные файла в формате строки base64.
        /// </summary>
        public string Data { get; set; } = default!;
    }

    /// <summary>
    /// Класс для представления данных файла в формате байтового массива.
    /// </summary>
    public class FileRawDto : FileDto
    {
        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[] Data { get; set; } = default!;
    }
    /**@}*/
}