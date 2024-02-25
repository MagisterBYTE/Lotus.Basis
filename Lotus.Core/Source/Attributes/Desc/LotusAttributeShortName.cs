using System;

namespace Lotus.Core
{
	/** \addtogroup CoreAttribute
	*@{*/
	/// <summary>
	/// Атрибут для определения дополнительного короткого имени.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusShortNameAttribute : Attribute
	{
		#region Fields
		internal readonly string _name;
		#endregion

		#region Properties
		/// <summary>
		/// Имя.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="name">Имя.</param>
		public LotusShortNameAttribute(string name)
		{
			_name = name;
		}
		#endregion
	}
	/**@}*/
}