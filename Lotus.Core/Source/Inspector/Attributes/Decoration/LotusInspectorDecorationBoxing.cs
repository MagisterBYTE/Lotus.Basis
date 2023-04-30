//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorDecorationBoxing.cs
*		Атрибут для определения фонового бокса элемента инспектора свойств.
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
		/// Атрибут для определения фонового бокса элемента инспектора свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusBoxingAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusBoxingAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly Int32 mOffsetLeft;
			internal readonly Int32 mOffsetTop;
			internal readonly Int32 mOffsetRight;
			internal readonly Int32 mOffsetBottom;
			internal String mBackgroundStyleName;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Смещение слева
			/// </summary>
			public Int32 OffsetLeft
			{
				get { return mOffsetLeft; }
			}

			/// <summary>
			/// Смещение сверху
			/// </summary>
			public Int32 OffsetTop
			{
				get { return mOffsetTop; }
			}

			/// <summary>
			/// Смещение справа
			/// </summary>
			public Int32 OffsetRight
			{
				get { return mOffsetRight; }
			}

			/// <summary>
			/// Смещение снизу
			/// </summary>
			public Int32 OffsetBottom
			{
				get { return mOffsetBottom; }
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
			/// <param name="offset_left">Красная компонента цвета</param>
			/// <param name="offset_top">Зеленая компонента цвета</param>
			/// <param name="offset_right">Синяя компонента цвета</param>
			/// <param name="offset_bottom">Альфа компонента цвета</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusBoxingAttribute(Int32 offset_left, Int32 offset_top = 0, Int32 offset_right = 0, 
				Int32 offset_bottom = 0)
			{
				mOffsetLeft = offset_left;
				mOffsetTop = offset_top;
				mOffsetRight = offset_right;
				mOffsetBottom = offset_bottom;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================