using System;
namespace Lotus.Core
{
	/** \addtogroup CoreAttribute
	*@{*/
	/// <summary>
	/// Атрибут для определения минимального значения величины.
	/// </summary>
	/// <remarks>
	/// В зависимости от способа задания значение распространяется либо на весь тип, либо к каждому экземпляру.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class LotusMinValueAttribute : Attribute
	{
		#region Fields
		internal readonly object _minValue;
		internal readonly string _memberName;
		internal readonly TInspectorMemberType _memberType;
		#endregion

		#region Properties
		/// <summary>
		/// Минимальное значение величины.
		/// </summary>
		public object MinValue
		{
			get { return _minValue; }
		}

		/// <summary>
		/// Имя члена объекта содержащие минимальное значение.
		/// </summary>
		public string MemberName
		{
			get { return _memberName; }
		}

		/// <summary>
		/// Тип члена объекта.
		/// </summary>
		public TInspectorMemberType MemberType
		{
			get { return _memberType; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="minValue">Минимальное значение величины.</param>
		public LotusMinValueAttribute(object minValue)
		{
			_minValue = minValue;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="memberName">Имя члена объекта содержащие минимальное значение.</param>
		/// <param name="memberType">Тип члена объекта.</param>
		public LotusMinValueAttribute(string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
		{
			_memberName = memberName;
			_memberType = memberType;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="type">Тип содержащие минимальное значение.</param>
		/// <param name="memberName">Имя члена типа содержащие минимальное значение.</param>
		/// <param name="memberType">Тип члена типа.</param>
		public LotusMinValueAttribute(Type type, string memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
		{
			_minValue = type;
			_memberName = memberName;
			_memberType = memberType;
		}
		#endregion
	}
	/**@}*/
}