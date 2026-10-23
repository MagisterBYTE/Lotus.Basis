using System;
#if !UNITY_2017_1_OR_NEWER
using System.ComponentModel.DataAnnotations;
#endif

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
#if !UNITY_2017_1_OR_NEWER
        [Key]
        [Required]
#endif
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