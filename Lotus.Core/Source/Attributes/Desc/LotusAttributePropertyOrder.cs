using System;
using System.Runtime.CompilerServices;

namespace Lotus.Core
{
	/** \addtogroup CoreAttribute
	*@{*/
	/// <summary>
	/// Атрибут для определения порядка отображения свойств/полей в инспекторе свойств.
	/// </summary>
	/// <remarks>
	/// Атрибут является эмуляцией одноименного атрибута компонента WPF Toolkit
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusPropertyOrderAttribute : Attribute
	{
		#region Fields
		internal readonly int _order;
		#endregion

		#region Properties
		/// <summary>
		/// Порядок отображения свойства.
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
		/// <param name="order">Порядок отображения свойства.</param>
		public LotusPropertyOrderAttribute(int order)
		{
			_order = order;
		}
		#endregion
	}

	/// <summary>
	/// Атрибут для автоматического порядка отображения свойств/полей в инспекторе свойств согласно их декларации.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusAutoOrderAttribute : Attribute
	{
		#region Fields
		internal readonly int _order;
		#endregion

		#region Properties
		/// <summary>
		/// Порядок отображения свойства.
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
		/// <param name="order">Порядок отображения свойства.</param>
		public LotusAutoOrderAttribute([CallerLineNumber] int order = 0)
		{
			_order = order;
		}
		#endregion
	}
	/**@}*/
}