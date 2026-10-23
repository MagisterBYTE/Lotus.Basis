#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityLocalization
    *@{*/
    /// <summary>
    /// Атрибут для определения возможности локализации свойства/поля
    /// </summary>
    /// <remarks>
    /// Данный атрибут соответственно определяет возможность локализации на уровне типа объекта.
    /// Атрибут применяется в части автоматизации получение/установки значения центральным сервисом
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class LotusLocalizationAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public LotusLocalizationAttribute()
        {
        }
        #endregion
    }

    /**@}*/
}
#endif
