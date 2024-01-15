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
			internal readonly String _memberName;
			internal readonly TInspectorMemberType _memberType;
			internal String _styleButtonLeftName;
			internal String _styleButtonRightName;
#if UNITY_EDITOR
			internal UnityEngine.GUIStyle _styleButtonLeft;
			internal UnityEngine.GUIStyle _styleButtonRight;
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
				get { return _memberName; }
			}

			/// <summary>
			/// Тип члена объекта
			/// </summary>
			public TInspectorMemberType MemberType
			{
				get { return _memberType; }
			}

			/// <summary>
			/// Имя визуального стиля для кнопки слева
			/// </summary>
			public String StyleButtonLeftName
			{
				get { return _styleButtonLeftName; }
				set { _styleButtonLeftName = value; }
			}

			/// <summary>
			/// Имя визуального стиля для кнопки справа
			/// </summary>
			public String StyleButtonRightName
			{
				get { return _styleButtonRightName; }
				set { _styleButtonRightName = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="stepValue">Шаг приращения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(System.Object stepValue)
			{
				mStepValue = stepValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="memberName">Имя члена объекта представляющий шаг приращения значения</param>
			/// <param name="memberType">Тип члена объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(String memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
			{
				_memberName = memberName;
				_memberType = memberType;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="type">Тип представляющий шаг приращения значения</param>
			/// <param name="memberName">Имя члена типа представляющий шаг приращения значения</param>
			/// <param name="memberType">Тип члена типа</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusStepValueAttribute(Type type, String memberName, TInspectorMemberType memberType = TInspectorMemberType.Method)
			{
				mStepValue = type;
				_memberName = memberName;
				_memberType = memberType;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================