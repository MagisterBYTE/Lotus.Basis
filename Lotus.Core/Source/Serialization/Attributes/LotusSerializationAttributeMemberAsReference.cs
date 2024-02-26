using System;

namespace Lotus.Core.Serialization
{
	/** \addtogroup CoreSerialization
	*@{*/
	/// <summary>
	/// Атрибут для определения сериализации свойства/поля как ссылки.
	/// </summary>
	/// <remarks>
	/// Если поле/свойство имеет тип класса, т.е ссылочного типа иногда требуется не сохранять его данные, а лишь
	/// сохранить ссылку на этот объект.
	/// При этом объем сохраняемых данные должен быть достаточным чтобы после загрузки всех данных 
	/// мы смогли их восстановить и связать.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class LotusSerializeMemberAsReferenceAttribute : Attribute
	{
	}
	/**@}*/
}