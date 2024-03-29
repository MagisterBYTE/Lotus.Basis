using System;

namespace Lotus.Core
{
    /** \addtogroup CoreTreeNode
    *@{*/
    /// <summary>
    /// Базовый класс для формирования константного типа на основании перечисления с поддержкой имени.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисление</typeparam>
    public abstract class TypedefObjectName<TEnum> : TypedefObject<TEnum>, ILotusNameable
        where TEnum : Enum
    {
        #region Properties
        /// <summary>
        /// Имя объекта.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Constructors
        protected TypedefObjectName()
        {
        }
        #endregion
    }
    /**@}*/
}
