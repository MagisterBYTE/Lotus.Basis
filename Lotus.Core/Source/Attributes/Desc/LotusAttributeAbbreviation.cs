using System;

namespace Lotus.Core
{
	/** 
	 * \defgroup CoreAttribute Подсистема атрибутов
	 * \ingroup Core
	 * \brief Подсистема атрибутов содержит необходимые атрибуты для расширения функциональности и добавления новых
		характеристик к свойствам, методам и классам.
	 * @{
	 */
	/// <summary>
	/// Атрибут для определения аббревиатуры.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusAbbreviationAttribute : Attribute
	{
		#region Fields
		internal readonly string _name;
		#endregion

		#region Properties
		/// <summary>
		/// Аббревиатура.
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
		/// <param name="name">Аббревиатура.</param>
		public LotusAbbreviationAttribute(string name)
		{
			_name = name;
		}
		#endregion
	}
	/**@}*/
}