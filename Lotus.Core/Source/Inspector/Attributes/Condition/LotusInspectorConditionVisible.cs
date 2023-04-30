﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorConditionVisible.cs
*		Атрибут видимости(отображения поля/свойства)в зависимости от логического условия равенства.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreInspectorAttribute
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут видимости(отображения поля/свойства)в зависимости от логического условия равенства
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusVisibleEqualityAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusVisibleEqualityAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mManagingMemberName;
			internal TInspectorMemberType mMemberType;
			internal Boolean mValue;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя члена объекта от которого определяется видимость
			/// </summary>
			public String ManagingMemberName
			{
				get { return mManagingMemberName; }
				set { mManagingMemberName = value; }
			}

			/// <summary>
			/// Тип члена объекта
			/// </summary>
			public TInspectorMemberType MemberType
			{
				get { return mMemberType; }
				set { mMemberType = value; }
			}

			/// <summary>
			/// Целевое значение поля/свойства при котором существует видимость
			/// </summary>
			public Boolean Value
			{
				get { return mValue; }
				set { mValue = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="managing_member_name">Имя члена объекта от которого определяется видимость</param>
			/// <param name="member_type">Тип члена объекта</param>
			/// <param name="value">Целевое значение поля/свойства при котором существует видимость</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusVisibleEqualityAttribute(String managing_member_name, TInspectorMemberType member_type, Boolean value)
			{
				mManagingMemberName = managing_member_name;
				mMemberType = member_type;
				mValue = value;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================