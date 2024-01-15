//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема ViewModel
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreViewItemTesting.cs
*		Тестирование подсистемы ViewModel модуля базового ядра.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования подсистемы ViewModel модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreViewModelTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class CActivity : COwnedObject, IComparable<CActivity>, ILotusIdentifierLong
			{
				public String Vilage { get; set; }
				public String Programm { get; set; }
				public Int32 Price { get; set; }
				public Int32 Total { get; set; }

				public Int64 Id
				{
					get;

					set;
				}

				public readonly static CActivity[] Activities = new CActivity[]
				{
					new CActivity()
					{
						Vilage = "Андреевское",
						Programm = "Ремонт",
						Price = 1
					},
					new CActivity()
					{
						Vilage = "Андреевское",
						Programm = "Ремонт",
						Price = 2
					},
					new CActivity()
					{
						Vilage = "Андреевское",
						Programm = "Ремонт",
						Price = 3
					},
					new CActivity()
					{
						Vilage = "Андреевское",
						Programm = "Безопасность",
						Price = 4
					},
					new CActivity()
					{
						Vilage = "Андреевское",
						Programm = "Безопасность",
						Price = 5
					},
					new CActivity()
					{
						Vilage = "Калиниское",
						Programm = "Ремонт",
						Price = 6
					},
					new CActivity()
					{
						Vilage = "Калиниское",
						Programm = "Ремонт",
						Price = 7
					},
					new CActivity()
					{
						Vilage = "Калиниское",
						Programm = "Ремонт",
						Price = 8
					},
					new CActivity()
					{
						Vilage = "Калиниское",
						Programm = "Безопасность",
						Price = 9
					},
					new CActivity()
					{
						Vilage = "Калиниское",
						Programm = "Безопасность",
						Price = 10
					},
				};

				public override String ToString()
				{
					return Vilage + "=" + Programm + "=" + Price.ToString();
				}

				public Int32 CompareTo(CActivity other)
				{
					return Price.CompareTo(other.Price);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// ViewModel для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class ViewModelActivity : ViewModel<CActivity>
			{
				public ViewModelActivity(CActivity model, String name = null) 
					: base(model, name)
				{
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// CollectionViewModel для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class CollectionViewModelActivity : CollectionViewModel<ViewModelActivity, CActivity>
			{
				public override ILotusViewModel CreateViewModel(System.Object model)
				{
					if(model is CActivity activity)
					{
						return new ViewModelActivity(activity);
					}

					return null;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestView()
			{
				var view_item = new ViewModelActivity(new CActivity()
				{
					Vilage = "Бреды",
					Programm = "Академия",
					Price = 2000
				});
				view_item.IsEnabled = true;
				view_item.IsSelected = true;
				view_item.IsPresented = true;

				ClassicAssert.AreEqual(true, ViewModelActivity.IsSupportIdentifierLong);
				ClassicAssert.AreEqual(false, ViewModelActivity.IsSupportNameable);
				ClassicAssert.AreEqual(false, ViewModelActivity.IsSupportModelEnabled);
				ClassicAssert.AreEqual(false, ViewModelActivity.IsSupportModelSelected);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestCollectionView()
			{
				var viewItems = new CollectionViewModelActivity();
				viewItems.Source = CActivity.Activities;
				
				// Общие данные
				ClassicAssert.AreEqual(10, viewItems.Count);
				ClassicAssert.AreEqual(true, CollectionViewModelActivity.IsNullable);
				ClassicAssert.AreEqual(false, viewItems.IsReadOnly);
				ClassicAssert.AreEqual(true, viewItems.IsFixedSize);

				// Проверка имени 
				for (var i = 0; i < CActivity.Activities.Length; i++)
				{
					ClassicAssert.AreEqual(viewItems[i].ToString(), CActivity.Activities[i].ToString());
				}

				// Сортировка
				viewItems.SortAscending();
				for (var i = 0; i < CActivity.Activities.Length; i++)
				{
					ClassicAssert.AreEqual(CActivity.Activities[i].Price, viewItems[i].Model.Price);
				}

				viewItems.SortDescending();
				for (var i = 0; i < CActivity.Activities.Length; i++)
				{
					ClassicAssert.AreEqual(CActivity.Activities[9 - i].Price, viewItems[i].Model.Price);
				}

				// Фильтрация
				viewItems.IsFiltered = true;
				viewItems.Filter = (CActivity activity) =>
				{
					return activity.Price > 5;
				};
				ClassicAssert.AreEqual(5, viewItems.Count);
				viewItems.SortDescending();
				ClassicAssert.AreEqual(10, viewItems[0].Model.Price);
				ClassicAssert.AreEqual(9, viewItems[1].Model.Price);
				ClassicAssert.AreEqual(8, viewItems[2].Model.Price);
				ClassicAssert.AreEqual(7, viewItems[3].Model.Price);
				ClassicAssert.AreEqual(6, viewItems[4].Model.Price);

				// Удаление
				//viewItems.RemoveAll()
			}
		}
	}
}
//=====================================================================================================================