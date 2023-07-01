﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема атрибутов
// Подраздел: Атрибуты связанные с возможностью непосредственно управлять значением поля/свойства объекта
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAttributeValueIndex.cs
*		Атрибуты для определения данных через индекс значение.
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
        /** \addtogroup CoreAttribute
		*@{*/
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Атрибут для определения строки через индекс значение
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusIndexToStringAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusIndexToStringAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly Type mSourceType;
			internal readonly String mMemberName;
			internal readonly TInspectorMemberType mMemberType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип объекта
			/// </summary>
			public Type SourceType
			{
				get { return mSourceType; }
			}

			/// <summary>
			/// Имя члена объекта осуществляющего конвертацию из строки в числовое значение
			/// </summary>
			public String MemberName
			{
				get { return mMemberName; }
			}

			/// <summary>
			/// Тип члена объекта
			/// </summary>
			public TInspectorMemberType MemberType
			{
				get { return mMemberType; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="memberName">Имя члена объекта осуществляющего конвертацию из строки в числовое значение</param>
			/// <param name="memberType">Тип члена объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusIndexToStringAttribute(String memberName, TInspectorMemberType memberType)
			{
				mMemberName = memberName;
				mMemberType = memberType;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="type">Тип объекта</param>
			/// <param name="memberName">Имя члена объекта осуществляющего конвертацию из строки в числовое значение</param>
			/// <param name="memberType">Тип члена объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusIndexToStringAttribute(Type type, String memberName, TInspectorMemberType memberType)
			{
				mSourceType = type;
				mMemberName = memberName;
				mMemberType = memberType;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================