using System;
namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для описания типа.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class LotusDescriptionTypeAttribute : Attribute
    {
        #region Fields
        internal readonly string _description;
        #endregion

        #region Properties
        /// <summary>
        /// Описание типа.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="description">Описание типа.</param>
        public LotusDescriptionTypeAttribute(string description)
        {
            _description = description;
        }
        #endregion
    }
    /**@}*/
}