using System;
using System.ComponentModel.DataAnnotations;

using Lotus.Core;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Класс для описания и представления отдельного файла.
    /// </summary>
    public class ResourceFile : BaseEntityDb<Guid>, IComparable<ResourceFile>
    {
        #region Const
        /// <summary>
        /// Имя таблицы.
        /// </summary>
        public const string TABLE_NAME = "ResourceFile";
        #endregion

        #region Models methods
        /// <summary>
        /// Конфигурирование модели для типа <see cref="ResourceFile"/>.
        /// </summary>
        /// <param name="modelBuilder">Интерфейс для построения моделей.</param>
        /// <param name="schemeName">Схема куда будет помещена таблица.</param>
        public static void ModelCreating(ModelBuilder modelBuilder, string schemeName)
        {
            // Определение для таблицы
            var model = modelBuilder.Entity<ResourceFile>();
            model.ToTable(TABLE_NAME, schemeName);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Наименование файла.
        /// </summary>
        [MaxLength(20)]
        public string? Name { get; set; }

        /// <summary>
        /// Место хранение файла.
        /// </summary>
        public TResourceFileStorage StorageType { get; set; }

        /// <summary>
        /// Формат хранения файла в базе данных.
        /// </summary>
        public TResourceFileSaveFormat SaveFormat { get; set; }

        /// <summary>
        /// Uri для загрузки файла если он храниться локально или на сервере.
        /// </summary>
        [MaxLength(256)]
        public string? LoadPath { get; set; }

        /// <summary>
        /// Размер изображения в байтах.
        /// </summary>
        public int? SizeInBytes { get; set; }

        /// <summary>
        /// Данные файла.
        /// </summary>
        public byte[]? Data { get; set; }

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
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(ResourceFile? other)
        {
            if (other == null) return 0;
            if (Name != null)
            {
                return (Name.CompareTo(other.Name));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Имя объекта.</returns>
        public override string ToString()
        {
            return (Name ?? string.Empty);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Преобразование в объект типа <see cref="FileDto"/>.
        /// </summary>
        /// <returns>Объект <see cref="FileDto"/>.</returns>
        public FileDto ToFileDto()
        {
            var dto = new FileDto
            {
                Name = Name,
                Id = Id,
                SizeInBytes = SizeInBytes
            };
            return dto;
        }

        /// <summary>
        /// Преобразование в объект типа <see cref="FileBase64Dto"/>.
        /// </summary>
        /// <returns>Объект <see cref="FileBase64Dto"/>.</returns>
        public FileBase64Dto ToFileBase64Dto()
        {
            var dto = new FileBase64Dto
            {
                Name = Name,
                Id = Id,
                SizeInBytes = SizeInBytes
            };

            if (StorageType == TResourceFileStorage.Database)
            {
                if (SaveFormat == TResourceFileSaveFormat.Base64)
                {
                    dto.Data = LoadPath!;
                }
                else
                {
                    dto.Data = Convert.ToBase64String(Data!);
                }
            }

            return dto;
        }
        #endregion
    }
    /**@}*/
}