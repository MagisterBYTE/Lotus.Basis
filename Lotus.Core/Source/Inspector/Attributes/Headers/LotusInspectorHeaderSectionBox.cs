﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorHeaderSectionBox.cs
*		Атрибут декоративной отрисовки заголовка секции в боксе.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreInspectorAttribute
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут декоративной отрисовки заголовка секции в боксе
		/// </summary>
		/// <remarks>
		/// Реализация атрибута декоративной отрисовки заголовка секции в боксе c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusHeaderSectionBoxAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusHeaderSectionBoxAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mName;
			internal TColor mTextColor;
			internal String mTextAlignment = "MiddleCenter";
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public TColor TextColor
			{
				get { return mTextColor; }
				set { mTextColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public String TextAlignment
			{
				get { return mTextAlignment; }
				set { mTextAlignment = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute()
			{
				mName = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="colorBGRA">Цвет текста заголовка</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name, UInt32 colorBGRA, String text_alignment = "MiddleLeft")
			{
				mName = name;
				mTextColor = TColor.FromBGRA(colorBGRA);
				mTextAlignment = text_alignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="colorBGRA">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name, UInt32 colorBGRA, Int32 ord, String text_alignment = "MiddleLeft")
			{
				mName = name;
				mTextColor = TColor.FromBGRA(colorBGRA);
				mTextAlignment = text_alignment;
#if UNITY_2017_1_OR_NEWER
				order = ord;
#endif
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================