using System;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Атрибут для определения псевдонима имени типа.
    /// </summary>
    /// <remarks>
    /// По умолчанию сериализуется собственное имя типа, данным атрибутов это можно поменять например для более
    /// удобочитаемого имени типа.
    /// Также следует позаботится об уникальности псевдонима в рамках проекта.
    /// Так как псевдоним образует имя элемента в формате XML то он должен подчинятся правилам использования 
    /// допустимых символов
    /// </remarks>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class LotusSerializeAliasTypeAttribute : Attribute
    {
        #region Fields
        internal string _name;
        #endregion

        #region Properties
        /// <summary>
        /// Имя типа для сериализации.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя типа для сериализации.</param>
        public LotusSerializeAliasTypeAttribute(string name)
        {
            _name = name;
        }
        #endregion
    }
    /**@}*/
}