using System;
using System.ComponentModel.DataAnnotations;

using Lotus.Core;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Класс для группы файла.
    /// </summary>
    public class ResourceFileGroup : EntityDb<int>, IComparable<ResourceFileGroup>
    {
        #region Const
        /// <summary>
        /// Имя таблицы.
        /// </summary>
        public const string TABLE_NAME = "ResourceFileGroup";
        #endregion

        #region Models methods
        /// <summary>
        /// Конфигурирование модели для типа <see cref="ResourceFileGroup"/>.
        /// </summary>
        /// <param name="modelBuilder">Интерфейс для построения моделей.</param>
        /// <param name="schemeName">Схема куда будет помещена таблица.</param>
        public static void ModelCreating(ModelBuilder modelBuilder, string schemeName)
        {
            // Определение для таблицы
            var model = modelBuilder.Entity<ResourceFileGroup>();
            model.ToTable(TABLE_NAME, schemeName);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Наименование группы.
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Отображаемое наименование группы.
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
        public int CompareTo(ResourceFileGroup? other)
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