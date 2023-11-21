﻿//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ViewModel формата JSON
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewModelJson.cs
*		Определение элемента ViewModel данных формата JSON.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
//---------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreViewModel
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс реализующий минимальный элемент ViewModel данных формата JSON
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CViewModelJson : ViewModelHierarchy<JObject>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="model">Модель</param>
			/// <param name="parentItem">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewModelJson(JObject model, ILotusViewModelHierarchy parentItem)
				: base(model, parentItem)
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusViewModelHierarchy ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание конкретной ViewModel для указанной модели
			/// </summary>
			/// <param name="model">Модель</param>
			/// <returns>ViewModel</returns>
			//---------------------------------------------------------------------------------------------------------
			public override ILotusViewModelHierarchy CreateViewModelHierarchy(System.Object model)
			{
				if(model is JObject jobject)
				{
					return new CViewModelJson(jobject, this.IParent);
				}

				return null;
			}	

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void BuildFromModel()
			{
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для элементов отображения которые поддерживают концепцию просмотра и управления с полноценной 
		/// поддержкой всех операций
		/// </summary>
		/// <remarks>
		/// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
		/// позволяет выбирать данные и производить над ними операции
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CCollectionViewJson : CollectionViewModelHierarchy<CViewModelJson, JObject>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewJson()
				: base(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewJson(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			/// <param name="source">Источник данных</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewJson(String name, JObject source)
				: base(name, source)
			{
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================