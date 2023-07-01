﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Подраздел: Атрибуты для инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorControlButton.cs
*		Атрибут реализующий отображение кнопки рядом с полем/свойством для вызова метода.
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
		/// Атрибут реализующий отображение кнопки рядом с полем/свойством для вызова метода
		/// </summary>
		/// <remarks>
		/// Если метод принимает аргумент то он должен быть того же типа как и тип поля/свойства.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
		public sealed class LotusButtonAttribute : UnityEngine.PropertyAttribute
#else
		public sealed class LotusButtonAttribute : Attribute
#endif
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal readonly String mMethodName;
			internal String mLabel;
			internal Boolean mInputArgument;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя метода
			/// </summary>
			public String MethodName
			{
				get { return mMethodName; }
			}

			/// <summary>
			/// Надпись на кнопке
			/// </summary>
			public String Label
			{
				get { return mLabel; }
				set { mLabel = value; }
			}

			/// <summary>
			/// Статус получения аргумента
			/// </summary>
			public Boolean InputArgument
			{
				get { return mInputArgument; }
				set { mInputArgument = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <param name="label">Надпись на кнопке</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusButtonAttribute(String methodName, String label = "D")
			{
				mMethodName = methodName;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================