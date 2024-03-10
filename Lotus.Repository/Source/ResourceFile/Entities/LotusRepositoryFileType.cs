using System;
using System.ComponentModel.DataAnnotations;

using Lotus.Core;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Класс для типа файла.
    /// </summary>
    public class ResourceFileType : BaseEntityDb<int>, IComparable<ResourceFileType>
    {
        #region Const
        /// <summary>
        /// Имя таблицы.
        /// </summary>
        public const string TABLE_NAME = "ResourceFileType";
        #endregion

        #region Models methods
        /// <summary>
        /// Конфигурирование модели для типа <see cref="ResourceFileType"/>.
        /// </summary>
        /// <param name="modelBuilder">Интерфейс для построения моделей.</param>
        /// <param name="schemeName">Схема куда будет помещена таблица.</param>
        public static void ModelCreating(ModelBuilder modelBuilder, string schemeName)
        {
            // Определение для таблицы
            var model = modelBuilder.Entity<ResourceFileType>();
            model.ToTable(TABLE_NAME, schemeName);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Наименование типа файла.
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Отображаемое наименование типа файла.
        /// </summary>
        [MaxLength(40)]
        public string? DisplayName { get; set; }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(ResourceFileType? other)
        {
            if (other == null) return 0;
            return (Name.CompareTo(other.Name));
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Имя объекта.</returns>
        public override string ToString()
        {
            return (DisplayName ?? Name);
        }
        #endregion
    }
    /**@}*/
}