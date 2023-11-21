﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorHeaderGroupBox.cs
*		Атрибут декоративной отрисовки заголовка группы в боксе.
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
		/// Атрибут декоративной отрисовки заголовка группы в боксе
		/// </summary>
		/// <remarks>
		/// Реализация атрибута декоративной отрисовки заголовка группы в боксе c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusHeaderGroupBoxAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusHeaderGroupBoxAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String _name;
			internal TColor _textColor;
			internal String _textAlignment = "MiddleLeft";
			internal Int32 _indent;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return _name; }
				set { _name = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public TColor TextColor
			{
				get { return _textColor; }
				set { _textColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public String TextAlignment
			{
				get { return _textAlignment; }
				set { _textAlignment = value; }
			}

			/// <summary>
			/// Уровень смещения
			/// </summary>
			public Int32 Indent
			{
				get { return _indent; }
				set { _indent = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute()
			{
				_name = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name)
			{
				_name = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, Int32 indent)
			{
				_name = name;
				_indent = indent;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="colorBGRA">Цвет текста заголовка</param>
			/// <param name="textAlignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, UInt32 colorBGRA, String textAlignment = "MiddleLeft")
			{
				_name = name;
				_textColor = TColor.FromBGRA(colorBGRA);
				_textAlignment = textAlignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="colorBGRA">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="textAlignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, UInt32 colorBGRA, Int32 ord, String textAlignment = "MiddleLeft")
			{
				_name = name;
				_textColor = TColor.FromBGRA(colorBGRA);
				_textAlignment = textAlignment;
#if UNITY_2017_1_OR_NEWER
				order = ord;
#endif
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================