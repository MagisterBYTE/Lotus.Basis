using System;
using System.ComponentModel.DataAnnotations;

namespace Lotus.Core
{
    /**
     * \defgroup CoreEntityDb Подсистема сущностей
     * \ingroup Core
     * \brief Подсистема сущностей поддерживающих идентификатор через первичный ключ.
     * @{
     */
    /// <summary>
    /// Базовый шаблонный класс для сущностей поддерживающих идентификатор через первичный ключ.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа.</typeparam>
    public class BaseEntityDb<TKey> : ILotusIdentifierId<TKey>
        where TKey : notnull, IEquatable<TKey>
    {
        #region Properties
        /// <summary>
        /// Ключ сущности.
        /// </summary>
        [Key]
        [Required]
        public TKey Id { get; set; }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Наименование объекта.</returns>
        public override string? ToString()
        {
            return Id.ToString();
        }
        #endregion
    }
    /**@}*/
}