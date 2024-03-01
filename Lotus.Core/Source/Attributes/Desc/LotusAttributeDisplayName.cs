using System;
namespace Lotus.Core
{
    /** \addtogroup CoreAttribute
	*@{*/
    /// <summary>
    /// Атрибут для отображения удобочитаемого имя свойства или поля.
    /// </summary>
    /// <remarks>
    /// Может применяться для отображения имени на соответствующем языке
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusDisplayNameAttribute : Attribute
    {
        #region Fields
        internal readonly string _name;
        internal readonly int _indent;
        #endregion

        #region Properties
        /// <summary>
        /// Отображаемое имя свойства.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Уровень смещения.
        /// </summary>
        public int Indent
        {
            get { return _indent; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Отображаемое имя.</param>
        public LotusDisplayNameAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Отображаемое имя.</param>
        /// <param name="indent">Уровень смещения.</param>
        public LotusDisplayNameAttribute(string name, int indent)
        {
            _name = name;
            _indent = indent;
        }
        #endregion
    }
    /**@}*/
}