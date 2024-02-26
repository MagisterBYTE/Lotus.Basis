using System;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Атрибут для определения сериализации свойства/поля.
    /// </summary>
    /// <remarks>
    /// Реализация атрибута для определения возможности сериализации свойства/поля.
    /// Соответственно определяет возможность сериализации на уровне типа объекта.
    /// Также атрибут позволяет определить под каким именем будет записано поле или свойство объекта в поток данных.
    /// Так как имя образует имя элемента/атрибута в формате XML то он должен подчинятся правилам использования 
    /// допустимых символов.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LotusSerializeMemberAttribute : Attribute
    {
        #region Fields
        internal string _name;
        #endregion

        #region Properties
        /// <summary>
        /// Имя для сериализации члена типа.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusSerializeMemberAttribute()
        {
            _name = "";
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя для сериализации члена типа.</param>
        public LotusSerializeMemberAttribute(string name)
        {
            _name = name;
        }
        #endregion
    }
    /**@}*/
}