//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема атрибутов
// Подраздел: Атрибуты для управления и оформления числовых свойств/полей
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAttributeNumberStep.cs
*		Атрибут для определения шага приращения значения.
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
        /// Атрибут для определения шага приращения значения
        /// </summary>
        /// <remarks>
        /// В зависимости от способа задания значение распространяется либо на весь тип, либо к каждому экземпляру
        /// </remarks>
        //-------------------------------------------------------------------------------------------------------------
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		public sealed class LotusStepValueAttribute : Attribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly System.Object mStepValue;
			internal readonly String mMemberName;
			internal readonly TInspectorMemberType mMemberType;
			internal String mStyleButtonLeftName;
			internal String mStyleButtonRightName;
#if UNITY_EDITOR
			internal UnityEngine.GUIStyle mStyleButtonLeft;
			internal UnityEngine.GUIStyle mStyleButtonRight;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Шаг приращения
			/// </summary>
			public System.Object StepValue
			{
				get { return mStepValue; }
			}

			/// <summary>
			/// Имя члена объекта представляющий шаг приращения значения
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

			/// <summary>
			/// Имя визуального стиля для кнопки слева
			/// </summary>
			public String StyleButtonLeftName
			{
				get { return mStyleButtonLeftName; }
				set { mStyleButtonLeftName = value; }
			}

			/// <summary>
			/// Имя визуального стиля для кнопки справа
			/// </summary>
			public String StyleButtonRightName
			{
				get { return mStyleButtonRightName; }
				set { mStyleButtonRightName = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="step_value">Шаг приращения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(System.Object step_value)
			{
				mStepValue = step_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="member_name">Имя члена объекта представляющий шаг приращения значения</param>
			/// <param name="member_type">Тип члена объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(String member_name, TInspectorMemberType member_type = TInspectorMemberType.Method)
			{
				mMemberName = member_name;
				mMemberType = member_type;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="type">Тип представляющий шаг приращения значения</param>
			/// <param name="member_name">Имя члена типа представляющий шаг приращения значения</param>
			/// <param name="member_type">Тип члена типа</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(Type type, String member_name, TInspectorMemberType member_type = TInspectorMemberType.Method)
			{
				mStepValue = type;
				mMemberName = member_name;
				mMemberType = member_type;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================