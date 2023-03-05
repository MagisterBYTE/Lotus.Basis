﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема идентификаторов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusIdentifierId.cs
*		Определение шаблонного и базового класса для сущностей поддерживающих идентификатор.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreIdentifiers
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Шаблонный класс для сущностей поддерживающих идентификатор
		/// </summary>
		/// <typeparam name="TKey">Тип идентификатор</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class IdentifierId<TKey> : PropertyChangedBase, ILotusIdentifierId<TKey> where TKey : IEquatable<TKey>
		{
			#region ======================================= ДАННЫЕ ====================================================
#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.SerializeField]
			[UnityEngine.HideInInspector]
#endif
			protected internal TKey mId;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ИДЕНТИФИКАЦИЯ
			//
			/// <summary>
			/// Уникальный идентификатор объекта
			/// </summary>
			[Browsable(false)]
			public virtual TKey Id
			{
				get { return (mId); }
				set
				{
					mId = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public IdentifierId()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public IdentifierId(TKey id)
			{
				mId = id;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Значение идентификатора</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (Id.ToString());
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс реализующий идентификацию через уникальный числовой идентификатор
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CIdentifierId : IdentifierId<Int64>, ILotusIdentifierId
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CIdentifierId()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Уникальный числовой идентификатор</param>
			//---------------------------------------------------------------------------------------------------------
			public CIdentifierId(Int64 id)
				:base(id)
			{
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================