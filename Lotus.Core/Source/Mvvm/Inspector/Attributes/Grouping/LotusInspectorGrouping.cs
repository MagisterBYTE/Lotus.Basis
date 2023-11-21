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
			internal String _groupName;
			internal TColor _headerColor;
			internal TColor _background;
			internal String _backgroundStyleName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя группы
			/// </summary>
			public String GroupName
			{
				get { return _groupName; }
				set { _groupName = value; }
			}

			/// <summary>
			/// Цвет надписи
			/// </summary>
			public TColor HeaderColor
			{
				get { return _headerColor; }
				set
				{
					_headerColor = value;
				}
			}

			/// <summary>
			/// Фоновый цвет
			/// </summary>
			public TColor Background
			{
				get { return _background; }
				set
				{
					_background = value;
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
				get { return _backgroundStyleName; }
				set { _backgroundStyleName = value; }
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
				_groupName = groupName;
				_background = TColor.White;
				_headerColor = TColor.White;
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
				_groupName = groupName;
				_background = TColor.White;
				_headerColor = TColor.FromBGRA(headerColorBgra);
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
				_groupName = groupName;
				_background = TColor.FromBGRA(backgroundBgra);
				_headerColor = TColor.FromBGRA(headerColorBgra);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================