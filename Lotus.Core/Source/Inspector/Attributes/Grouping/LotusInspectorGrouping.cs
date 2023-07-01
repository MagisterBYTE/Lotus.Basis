//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorGrouping.cs
*		Атрибут для определения логической группы элементов инспектора свойств.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
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
		/// Атрибут для определения логической группы элементов инспектора свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusGroupingAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusGroupingAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mGroupName;
			internal TColor mHeaderColor;
			internal TColor mBackground;
			internal String mBackgroundStyleName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя группы
			/// </summary>
			public String GroupName
			{
				get { return mGroupName; }
				set { mGroupName = value; }
			}

			/// <summary>
			/// Цвет надписи
			/// </summary>
			public TColor HeaderColor
			{
				get { return mHeaderColor; }
				set
				{
					mHeaderColor = value;
				}
			}

			/// <summary>
			/// Фоновый цвет
			/// </summary>
			public TColor Background
			{
				get { return mBackground; }
				set
				{
					mBackground = value;
				}
			}

			/// <summary>
			/// Имя визуального стиля для фонового поля
			/// </summary>
			/// <remarks>
			/// При необходимости может использоваться и для элементов
			/// </remarks>
			public String BackgroundStyleName
			{
				get { return mBackgroundStyleName; }
				set { mBackgroundStyleName = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="groupName">Имя группы</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusGroupingAttribute(String groupName)
			{
				mGroupName = groupName;
				mBackground = TColor.White;
				mHeaderColor = TColor.White;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="groupName">Имя группы</param>
			/// <param name="headerColorBgra">Цвет надписи в формате BGRA</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusGroupingAttribute(String groupName, UInt32 headerColorBgra)
			{
				mGroupName = groupName;
				mBackground = TColor.White;
				mHeaderColor = TColor.FromBGRA(headerColorBgra);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="groupName">Имя группы</param>
			/// <param name="headerColorBgra">Цвет надписи в формате BGRA</param>
			/// <param name="backgroundBgra">Фоновый цвет в формате BGRA</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusGroupingAttribute(String groupName, UInt32 headerColorBgra, UInt32 backgroundBgra)
			{
				mGroupName = groupName;
				mBackground = TColor.FromBGRA(backgroundBgra);
				mHeaderColor = TColor.FromBGRA(headerColorBgra);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================