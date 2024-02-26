using System;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Атрибут для информирование о том что тип сериализуется как примитивный.
    /// </summary>
    /// <remarks>
    /// В контексте текстового потока данных это означает что он может быт записан в одну строку (в формат атрибута XML)
    /// Тип помеченный данным атрибутом должен обязательно реализовать статический метод 
    /// с именем <see cref="LotusSerializeAsPrimitiveAttribute.DESERIALIZE_FROM_STRING"/> и обычный метод с именем
    /// <see cref="LotusSerializeAsPrimitiveAttribute.SERIALIZE_TO_STRING"/>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class LotusSerializeAsPrimitiveAttribute : Attribute
    {
        #region Const
        /// <summary>
        /// Имя статического метода типа который создает объект из переданной строки.
        /// </summary>
        public const string DESERIALIZE_FROM_STRING = "DeserializeFromString";

        /// <summary>
        /// Имя метода типа который сериализует объект в строку и возвращает её.
        /// </summary>
        public const string SERIALIZE_TO_STRING = "SerializeToString";
        #endregion
    }
    /**@}*/
}