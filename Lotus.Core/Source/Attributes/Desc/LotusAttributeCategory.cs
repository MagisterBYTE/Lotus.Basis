using System;

namespace Lotus.Core
{
	/** \addtogroup CoreAttribute
	*@{*/
	/// <summary>
	/// Атрибут для определения группы свойств/полей.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusCategoryAttribute : Attribute
	{
		#region Fields
		internal readonly string _name;
		internal readonly int _order;
		#endregion

		#region Properties
		/// <summary>
		/// Имя группы.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Порядок отображения группы свойств.
		/// </summary>
		public int Order
		{
			get { return _order; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="name">Имя группы.</param>
		/// <param name="order">Порядок отображения группы свойств.</param>
		public LotusCategoryAttribute(string name, int order)
		{
			_name = name;
			_order = order;
		}
		#endregion
	}
	/**@}*/
}