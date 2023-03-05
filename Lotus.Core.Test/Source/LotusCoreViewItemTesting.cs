//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreViewItemTesting.cs
*		Тестирование подсистемы отображения данных модуля базового ядра.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
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
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования подсистемы отображения данных модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreViewItemTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Служебный класс для тестирования
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public class CActivity : COwnedObject, IComparable<CActivity>, ILotusIdentifierId
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
					return (Vilage + "=" + Programm + "=" + Price.ToString());
				}

				public Int32 CompareTo(CActivity other)
				{
					return (Price.CompareTo(other.Price));
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
				ViewItem<CActivity> view_item = new ViewItem<CActivity>(new CActivity()
				{
					Vilage = "Бреды",
					Programm = "Академия",
					Price = 2000
				});
				view_item.IsEnabled = true;
				view_item.IsSelected = true;
				view_item.IsPresented = true;

				Assert.AreEqual(ViewItem<CActivity>.IsSupportIdentifierId, true);
				Assert.AreEqual(ViewItem<CActivity>.IsSupportNameable, false);
				Assert.AreEqual(ViewItem<CActivity>.IsSupportViewEnabled, false);
				Assert.AreEqual(ViewItem<CActivity>.IsSupportViewSelected, false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestCollectionView()
			{
				CollectionView<ViewItem<CActivity>, CActivity> viewItems = new CollectionView<ViewItem<CActivity>, CActivity>();
				viewItems.Source = CActivity.Activities;
				
				// Общие данные
				Assert.AreEqual(viewItems.Count, 10);
				Assert.AreEqual(CollectionView<ViewItem<CActivity>, CActivity>.IsNullable, true);
				Assert.AreEqual(viewItems.IsReadOnly, false);
				Assert.AreEqual(viewItems.IsFixedSize, true);

				// Проверка имени 
				for (Int32 i = 0; i < CActivity.Activities.Length; i++)
				{
					Assert.AreEqual(viewItems[i].ToString(), CActivity.Activities[i].ToString());
				}

				// Сортировка
				viewItems.SortAscending();
				for (Int32 i = 0; i < CActivity.Activities.Length; i++)
				{
					Assert.AreEqual(viewItems[i].DataContext.Price, CActivity.Activities[i].Price);
				}

				viewItems.SortDescending();
				for (Int32 i = 0; i < CActivity.Activities.Length; i++)
				{
					Assert.AreEqual(viewItems[i].DataContext.Price, CActivity.Activities[9 - i].Price);
				}

				// Фильтрация
				viewItems.IsFiltered = true;
				viewItems.Filter = (CActivity activity) =>
				{
					return (activity.Price > 5);
				};
				Assert.AreEqual(viewItems.Count, 5);
				viewItems.SortDescending();
				Assert.AreEqual(viewItems[0].DataContext.Price, 10);
				Assert.AreEqual(viewItems[1].DataContext.Price, 9);
				Assert.AreEqual(viewItems[2].DataContext.Price, 8);
				Assert.AreEqual(viewItems[3].DataContext.Price, 7);
				Assert.AreEqual(viewItems[4].DataContext.Price, 6);

				// Удаление
				//viewItems.RemoveAll()
			}
		}
	}
}
//=====================================================================================================================