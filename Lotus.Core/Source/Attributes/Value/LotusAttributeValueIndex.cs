using System;

namespace Lotus.Core
{
	/** \addtogroup CoreAttribute
	*@{*/
	/// <summary>
	/// Атрибут для определения строки через индекс значение.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusIndexToStringAttribute : UnityEngine.PropertyAttribute
#else
	public sealed class LotusIndexToStringAttribute : Attribute
#endif
	{
		#region Fields
		internal readonly Type mSourceType;
		internal readonly string _memberName;
		internal readonly TInspectorMemberType _memberType;
		#endregion

		#region Properties
		/// <summary>
		/// Тип объекта.
		/// </summary>
		public Type SourceType
		{
			get { return mSourceType; }
		}

		/// <summary>
		/// Имя члена объекта осуществляющего конвертацию из строки в числовое значение.
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
		/// <param name="memberName">Имя члена объекта осуществляющего конвертацию из строки в числовое значение.</param>
		/// <param name="memberType">Тип члена объекта.</param>
		public LotusIndexToStringAttribute(string memberName, TInspectorMemberType memberType)
		{
			_memberName = memberName;
			_memberType = memberType;
		}

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="type">Тип объекта.</param>
		/// <param name="memberName">Имя члена объекта осуществляющего конвертацию из строки в числовое значение.</param>
		/// <param name="memberType">Тип члена объекта.</param>
		public LotusIndexToStringAttribute(Type type, string memberName, TInspectorMemberType memberType)
		{
			mSourceType = type;
			_memberName = memberName;
			_memberType = memberType;
		}
		#endregion
	}
	/**@}*/
}