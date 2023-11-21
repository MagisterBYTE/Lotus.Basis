﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorDecorationIndent.cs
*		Атрибут для определения уровня смещения отображения элемента инспектора свойств.
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
		/// Атрибут для определения уровня смещения отображения элемента инспектора свойств
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusIndentLevelAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusIndentLevelAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly Int32 _indentLevel;
			internal readonly Boolean _isAbsolute = false;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Уровень смещения
			/// </summary>
			public Int32 IndentLevel
			{
				get { return _indentLevel; }
			}

			/// <summary>
			/// Статус абсолютного смещения
			/// </summary>
			public Boolean IsAbsolute
			{
				get { return _isAbsolute; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="indentLevel">Уровень смещения</param>
			/// <param name="isAbsolute">Статус абсолютного смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusIndentLevelAttribute(Int32 indentLevel, Boolean isAbsolute = true)
			{
				_indentLevel = indentLevel;
				_isAbsolute = isAbsolute;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================