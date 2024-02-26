using System;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Атрибут для исключения автоматической сериализации объекта.
    /// </summary>
    /// <remarks>
    /// Атрибут применяется для типов которые не нужно автоматические сериализовывать, то есть не надо брать данные
    /// для сериализации в процесс анализа типов.
    /// Как правило такие типы должны сериализовать специальный образом
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class LotusSerializeDisableAttribute : Attribute
    {
    }
    /**@}*/
}