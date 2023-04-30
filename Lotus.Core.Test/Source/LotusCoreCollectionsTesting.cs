//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема коллекций
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCoreCollectionsTesting.cs
*		Тестирование подсистемы коллекций модуля базового ядра.
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
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для тестирования подсистемы коллекций модуля базового ядра
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XCoreCollectionsTesting
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="ListArray{TItem}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestListArray()
			{
				ListArray<Int32> sample = new ListArray<Int32>(20);

				// Добавление
				sample.Add(2000);
				Assert.AreEqual(sample.Count, 1);
				Assert.AreEqual(sample[0], 2000);

				// Вставка
				sample.Insert(0, 1000);
				Assert.AreEqual(sample.Count, 2);
				Assert.AreEqual(sample[0], 1000);
				Assert.AreEqual(sample[1], 2000);

				// Вставка
				sample.Insert(1, 500);
				Assert.AreEqual(sample.Count, 3);
				Assert.AreEqual(sample[0], 1000);
				Assert.AreEqual(sample[1], 500);
				Assert.AreEqual(sample[2], 2000);

				// Удаление
				sample.Remove(500);
				Assert.AreEqual(sample.Count, 2);
				Assert.AreEqual(sample[0], 1000);
				Assert.AreEqual(sample[1], 2000);

				// Очистка
				sample.Clear();
				Assert.AreEqual(sample.Count, 0);
				Assert.AreEqual(sample.MaxCount, 20);

				// Добавление списка элементов
				sample.AddItems(23, 568, 788, 4587);
				Assert.AreEqual(sample.Count, 4);

				// Вставка списка элементов
				sample.InsertItems(2, 87, 987);
				Assert.AreEqual(sample.Count, 6);
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 568);
				Assert.AreEqual(sample[2], 87);
				Assert.AreEqual(sample[3], 987);
				Assert.AreEqual(sample[4], 788);
				Assert.AreEqual(sample[5], 4587);

				// Удаление диапазона
				sample.RemoveRange(3, 2);
				Assert.AreEqual(sample.Count, 4);
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 568);
				Assert.AreEqual(sample[2], 87);
				Assert.AreEqual(sample[3], 4587);

				// Добавление списка элементов
				sample.AddItems(444, 545, 999, 842);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 568); // Будет удалено в следующем тесте
				Assert.AreEqual(sample[2], 87);
				Assert.AreEqual(sample[3], 4587);
				Assert.AreEqual(sample[4], 444); // Будет удалено в следующем тесте
				Assert.AreEqual(sample[5], 545);
				Assert.AreEqual(sample[6], 999);
				Assert.AreEqual(sample[7], 842); // Будет удалено в следующем тесте

				// Удаление по условию
				Int32 count = sample.RemoveAll((Int32 x) =>
				{
					return (x % 2 == 0);
				});
				Assert.AreEqual(sample.Count, 5);
				Assert.AreEqual(count, 3);
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 87);
				Assert.AreEqual(sample[2], 4587);
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 999);

				// Добавление списка элементов
				sample.AddItems(23, 568, 788, 4587);
				Assert.AreEqual(sample.Count, 9);
				Assert.AreEqual(sample[0], 23); // Duplicate 23
				Assert.AreEqual(sample[1], 87);
				Assert.AreEqual(sample[2], 4587); // Duplicate 4587
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 23); // Duplicate 23
				Assert.AreEqual(sample[6], 568);
				Assert.AreEqual(sample[7], 788);
				Assert.AreEqual(sample[8], 4587); // Duplicate 4587

				// Удаление дубликатов
				Int32 count_dublicate = sample.RemoveDuplicates();
				Assert.AreEqual(count_dublicate, 2);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 87);
				Assert.AreEqual(sample[2], 4587);
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 568);
				Assert.AreEqual(sample[6], 788);

				// Сортировка по возрастанию
				sample.SortAscending();
				Assert.AreEqual(sample[0], 23);
				Assert.AreEqual(sample[1], 87);
				Assert.AreEqual(sample[2], 545);
				Assert.AreEqual(sample[3], 568);
				Assert.AreEqual(sample[4], 788);
				Assert.AreEqual(sample[5], 999);
				Assert.AreEqual(sample[6], 4587);

				// Сортировка по убыванию
				sample.SortDescending();
				Assert.AreEqual(sample[0], 4587);
				Assert.AreEqual(sample[1], 999);
				Assert.AreEqual(sample[2], 788);
				Assert.AreEqual(sample[3], 568);
				Assert.AreEqual(sample[4], 545);
				Assert.AreEqual(sample[5], 87);
				Assert.AreEqual(sample[6], 23);

				// Удаление первого элемента
				sample.RemoveFirst();
				Assert.AreEqual(sample.Count, 6);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 788);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 87);
				Assert.AreEqual(sample[5], 23);

				// Удаление последнего элемента
				sample.RemoveLast();
				Assert.AreEqual(sample.Count, 5);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 788);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 87);

				// Емкость
				sample.TrimExcess();
				Assert.AreEqual(sample.Count, 5);
				Assert.AreEqual(sample.MaxCount, 5);

				// Перемещаем элемент с индексом 2 вниз
				sample.MoveDown(2);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 788);
				Assert.AreEqual(sample[2], 545);
				Assert.AreEqual(sample[3], 568);
				Assert.AreEqual(sample[4], 87);

				// Циклическое смещение элементов списка вниз
				sample.ShiftDown();
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 999);
				Assert.AreEqual(sample[2], 788);
				Assert.AreEqual(sample[3], 545);
				Assert.AreEqual(sample[4], 568);

				sample.SortAscending();
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);

				// Поиск ближайшего индекса
				Assert.AreEqual(sample.GetClosestIndex(50), 0);
				Assert.AreEqual(sample.GetClosestIndex(87), 0);
				Assert.AreEqual(sample.GetClosestIndex(90), 0);
				Assert.AreEqual(sample.GetClosestIndex(545), 1);
				Assert.AreEqual(sample.GetClosestIndex(550), 1);
				Assert.AreEqual(sample.GetClosestIndex(998), 3);
				Assert.AreEqual(sample.GetClosestIndex(999), sample.LastIndex);
				Assert.AreEqual(sample.GetClosestIndex(1000), sample.LastIndex);

				// Обрезка
				sample.Add(5566);
				sample.Add(9874);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				ListArray<Int32> save = sample.GetItemsDuplicate();

				//
				// Обрезка списка сначала
				//
				Assert.AreEqual(sample.TrimStart(87, false), 0);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimStart(87), 1);
				Assert.AreEqual(sample[0], 545);
				Assert.AreEqual(sample[1], 568);
				Assert.AreEqual(sample[2], 788);
				Assert.AreEqual(sample[3], 999);
				Assert.AreEqual(sample[4], 5566);
				Assert.AreEqual(sample[5], 9874);

				Assert.AreEqual(sample.TrimStart(999, false), 3);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 5566);
				Assert.AreEqual(sample[2], 9874);

				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimStart(999), 5);
				Assert.AreEqual(sample[0], 5566);
				Assert.AreEqual(sample[1], 9874);

				Assert.AreEqual(sample.TrimStart(999), -1); // Ничего не нашли
				
				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimStart(9874, false), 6);
				Assert.AreEqual(sample.Count, 1);
				Assert.AreEqual(sample[0], 9874);

				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimStart(9874), 7);
				Assert.AreEqual(sample.Count, 0);

				//
				// Поиск ближайшего индекса
				//
				sample.AssignItems(87, 545, 568, 788, 999);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);

				
				Assert.AreEqual(sample.GetClosestIndex(50), 0);
				Assert.AreEqual(sample.GetClosestIndex(87), 0);
				Assert.AreEqual(sample.GetClosestIndex(90), 0);
				Assert.AreEqual(sample.GetClosestIndex(545), 1);
				Assert.AreEqual(sample.GetClosestIndex(550), 1);
				Assert.AreEqual(sample.GetClosestIndex(568), 2);
				Assert.AreEqual(sample.GetClosestIndex(788), 3);
				Assert.AreEqual(sample.GetClosestIndex(998), 3);
				Assert.AreEqual(sample.GetClosestIndex(999), sample.LastIndex);
				Assert.AreEqual(sample.GetClosestIndex(1000), sample.LastIndex);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestStart(50), 0);
				Assert.AreEqual(sample.TrimClosestStart(87, false), 0);
				Assert.AreEqual(sample.TrimClosestStart(87), 1);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 545);
				Assert.AreEqual(sample[1], 568);
				Assert.AreEqual(sample[2], 788);
				Assert.AreEqual(sample[3], 999);
				Assert.AreEqual(sample[4], 1203);
				Assert.AreEqual(sample[5], 5684);
				Assert.AreEqual(sample[6], 5987);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestStart(800), 4);
				Assert.AreEqual(sample.Count, 4);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 1203);
				Assert.AreEqual(sample[2], 5684);
				Assert.AreEqual(sample[3], 5987);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestStart(788), 4);
				Assert.AreEqual(sample.Count, 4);
				Assert.AreEqual(sample[0], 999);
				Assert.AreEqual(sample[1], 1203);
				Assert.AreEqual(sample[2], 5684);
				Assert.AreEqual(sample[3], 5987);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestStart(788, false), 3);
				Assert.AreEqual(sample.Count, 5);
				Assert.AreEqual(sample[0], 788);
				Assert.AreEqual(sample[1], 999);
				Assert.AreEqual(sample[2], 1203);
				Assert.AreEqual(sample[3], 5684);
				Assert.AreEqual(sample[4], 5987);

				Assert.AreEqual(sample.TrimClosestStart(5987, false), 4);
				Assert.AreEqual(sample.Count, 1);
				Assert.AreEqual(sample[0], 5987);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestStart(5987), 8);
				Assert.AreEqual(sample.Count, 0);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestEnd(10000, false), 0);
				Assert.AreEqual(sample.TrimClosestEnd(10000), 0);
				Assert.AreEqual(sample.TrimClosestEnd(5987, false), 0);
				Assert.AreEqual(sample.TrimClosestEnd(5987), 1);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);

				Assert.AreEqual(sample.TrimClosestEnd(1000), 2);
				Assert.AreEqual(sample.Count, 5);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);

				Assert.AreEqual(sample.TrimClosestEnd(545), 4);
				Assert.AreEqual(sample.Count, 1);
				Assert.AreEqual(sample[0], 87);

				sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
				Assert.AreEqual(sample.Count, 8);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 1203);
				Assert.AreEqual(sample[6], 5684);
				Assert.AreEqual(sample[7], 5987);

				Assert.AreEqual(sample.TrimClosestEnd(545, false), 6);
				Assert.AreEqual(sample.Count, 2);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);

				Assert.AreEqual(sample.TrimClosestEnd(40, false), 2);
				Assert.AreEqual(sample.Count, 0);

				//
				// Обрезка сконца
				//
				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimEnd(9874, false), 0);
				Assert.AreEqual(sample.TrimEnd(9874), 1);
				Assert.AreEqual(sample.Count, 6);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);

				Assert.AreEqual(sample.TrimEnd(788, false), 2);
				Assert.AreEqual(sample.Count, 4);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);

				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimEnd(788), 4);
				Assert.AreEqual(sample.Count, 3);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);

				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimEnd(87, false), 6);
				Assert.AreEqual(sample.Count, 1);
				Assert.AreEqual(sample[0], 87);

				sample.AssignItems(save);
				Assert.AreEqual(sample.Count, 7);
				Assert.AreEqual(sample[0], 87);
				Assert.AreEqual(sample[1], 545);
				Assert.AreEqual(sample[2], 568);
				Assert.AreEqual(sample[3], 788);
				Assert.AreEqual(sample[4], 999);
				Assert.AreEqual(sample[5], 5566);
				Assert.AreEqual(sample[6], 9874);

				Assert.AreEqual(sample.TrimEnd(87), 7);
				Assert.AreEqual(sample.Count, 0);

				//
				// Еще один пример
				//
				ListArray<String> list = new ListArray<String>();
				list.Add("00");
				list.Add("01"); //
				list.Add("02"); //
				list.Add("03"); //
				list.Add("04"); //
				list.Add("05");
				list.Add("06");
				list.Add("07");
				list.Add("08");
				list.Add("09");
				list.Add("10");
				list.Add("11");

				list.RemoveRange(1, 4);

				Assert.AreEqual(list.Count, 8);
				Assert.AreEqual(list[0], "00");
				Assert.AreEqual(list[1], "05");
				Assert.AreEqual(list[2], "06");
				Assert.AreEqual(list[3], "07");
				Assert.AreEqual(list[4], "08");
				Assert.AreEqual(list[5], "09");
				Assert.AreEqual(list[6], "10");
				Assert.AreEqual(list[7], "11");

				list.Clear();

				list.Add("00");
				list.Add("01"); //
				list.Add("02"); //
				list.Add("03"); //
				list.Add("04"); //
				list.Add("05");
				list.Add("06");
				list.Add("07");
				list.Add("08");
				list.Add("09");
				list.Add("10");
				list.Add("11");

				list.RemoveRange(list.LastIndex, 1);

				Assert.AreEqual(list.Count, 11);
				Assert.AreEqual(list[0], "00");
				Assert.AreEqual(list[1], "01");
				Assert.AreEqual(list[2], "02");
				Assert.AreEqual(list[3], "03");
				Assert.AreEqual(list[4], "04");
				Assert.AreEqual(list[5], "05");
				Assert.AreEqual(list[6], "06");
				Assert.AreEqual(list[7], "07");
				Assert.AreEqual(list[8], "08");
				Assert.AreEqual(list[9], "09");
				Assert.AreEqual(list[10], "10");

				list.RemoveItemsEnd(8);

				Assert.AreEqual(list.Count, 8);
				Assert.AreEqual(list[0], "00");
				Assert.AreEqual(list[1], "01");
				Assert.AreEqual(list[2], "02");
				Assert.AreEqual(list[3], "03");
				Assert.AreEqual(list[4], "04");
				Assert.AreEqual(list[5], "05");
				Assert.AreEqual(list[6], "06");
				Assert.AreEqual(list[7], "07");

				list.RemoveItemsEnd(7);

				Assert.AreEqual(list.Count, 7);
				Assert.AreEqual(list[0], "00");
				Assert.AreEqual(list[1], "01");
				Assert.AreEqual(list[2], "02");
				Assert.AreEqual(list[3], "03");
				Assert.AreEqual(list[4], "04");
				Assert.AreEqual(list[5], "05");
				Assert.AreEqual(list[6], "06");

				//
				// Операции множеств
				//
				ListArray<Int32> deferencs_source = new ListArray<int>();
				deferencs_source.AddItems(0, 2, 4, 7, 5);

				ListArray<Int32> deferencs = deferencs_source.DifferenceItems(0, 4, 7, 12, 15, 7);
				Assert.AreEqual(deferencs.Count, 2);
				Assert.AreEqual(deferencs[0], 2);
				Assert.AreEqual(deferencs[1], 5);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="QueueArray{TItem}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestQueueArray()
			{
				QueueArray<Int32> queue = new QueueArray<Int32>();
				queue.Enqueue(100);
				queue.Enqueue(90);
				queue.Enqueue(80);
				queue.Enqueue(70);
				queue.Enqueue(60);
				queue.Enqueue(50);
				queue.Enqueue(30);

				Assert.AreEqual(queue.Dequeue(), 100);
				Assert.AreEqual(queue.Dequeue(), 90);
				Assert.AreEqual(queue.Dequeue(), 80);
				Assert.AreEqual(queue.Dequeue(), 70);
				Assert.AreEqual(queue.Dequeue(), 60);
				Assert.AreEqual(queue.Dequeue(), 50);
				Assert.AreEqual(queue.Dequeue(), 30);
				Assert.AreEqual(queue.Count, 0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="DequeArray{TItem}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestDequeArray()
			{
				DequeArray<Int32> deque = new DequeArray<int>();

				deque.AddFront(4);
				deque.AddFront(3);
				deque.AddFront(2);
				deque.AddFront(1);
				deque.AddBack(5);
				deque.AddBack(6);
				deque.AddBack(7);
				deque.AddBack(8);

				Assert.AreEqual(deque.GetElement(0), 1);
				Assert.AreEqual(deque.GetElement(1), 2);
				Assert.AreEqual(deque.GetElement(2), 3);
				Assert.AreEqual(deque.GetElement(3), 4);
				Assert.AreEqual(deque.GetElement(4), 5);
				Assert.AreEqual(deque.GetElement(5), 6);
				Assert.AreEqual(deque.GetElement(6), 7);
				Assert.AreEqual(deque.GetElement(7), 8);
				Assert.AreEqual(deque.Count, 8);


				deque.AddFront(100);
				deque.AddFront(200);
				deque.AddBack(10000);

				Assert.AreEqual(deque.GetElement(0), 200);
				Assert.AreEqual(deque.GetElement(1), 100);
				Assert.AreEqual(deque.GetElement(2), 1);
				Assert.AreEqual(deque.GetElement(3), 2);
				Assert.AreEqual(deque.GetElement(4), 3);
				Assert.AreEqual(deque.GetElement(5), 4);
				Assert.AreEqual(deque.GetElement(6), 5);
				Assert.AreEqual(deque.GetElement(7), 6);
				Assert.AreEqual(deque.GetElement(8), 7);
				Assert.AreEqual(deque.GetElement(9), 8);
				Assert.AreEqual(deque.GetElement(10), 10000);
				Assert.AreEqual(deque.Count, 11);


				deque.RemoveFront();
				deque.RemoveFront();
				deque.RemoveFront();
				deque.RemoveFront();

				Assert.AreEqual(deque.GetElement(0), 3);
				Assert.AreEqual(deque.GetElement(1), 4);
				Assert.AreEqual(deque.GetElement(2), 5);
				Assert.AreEqual(deque.GetElement(3), 6);
				Assert.AreEqual(deque.GetElement(4), 7);
				Assert.AreEqual(deque.GetElement(5), 8);
				Assert.AreEqual(deque.GetElement(6), 10000);
				Assert.AreEqual(deque.Count, 7);


				deque.RemoveBack();
				deque.RemoveBack();

				Assert.AreEqual(deque.GetElement(0), 3);
				Assert.AreEqual(deque.GetElement(1), 4);
				Assert.AreEqual(deque.GetElement(2), 5);
				Assert.AreEqual(deque.GetElement(3), 6);
				Assert.AreEqual(deque.GetElement(4), 7);
				Assert.AreEqual(deque.Count, 5);


				deque.AddFront(100);
				deque.AddFront(200);
				deque.AddBack(10000);

				Assert.AreEqual(deque.GetElement(0), 200);
				Assert.AreEqual(deque.GetElement(1), 100);
				Assert.AreEqual(deque.GetElement(2), 3);
				Assert.AreEqual(deque.GetElement(3), 4);
				Assert.AreEqual(deque.GetElement(4), 5);
				Assert.AreEqual(deque.GetElement(5), 6);
				Assert.AreEqual(deque.GetElement(6), 7);
				Assert.AreEqual(deque.GetElement(7), 10000);
				Assert.AreEqual(deque.Count, 8);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="PriorityQueue{TItem}"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestPriorityQueue()
			{
				PriorityQueue<Int32> priority_queue = new PriorityQueue<int>();

				priority_queue.Push(3);
				priority_queue.Push(2);
				priority_queue.Push(1);
				priority_queue.Push(5);
				priority_queue.Push(6);
				priority_queue.Push(7);
				priority_queue.Push(8);
				priority_queue.Push(4);

				Assert.AreEqual(priority_queue.Pop(), 1);
				Assert.AreEqual(priority_queue.Pop(), 2);
				Assert.AreEqual(priority_queue.Pop(), 3);
				Assert.AreEqual(priority_queue.Pop(), 4);
				Assert.AreEqual(priority_queue.Pop(), 5);
				Assert.AreEqual(priority_queue.Pop(), 6);

				priority_queue.Push(60);
				priority_queue.Push(70);
				priority_queue.Push(80);
				priority_queue.Push(40);

				Assert.AreEqual(priority_queue.Pop(), 7);
				Assert.AreEqual(priority_queue.Pop(), 8);
				Assert.AreEqual(priority_queue.Pop(), 40);
				Assert.AreEqual(priority_queue.Pop(), 60);
				Assert.AreEqual(priority_queue.Pop(), 70);
				Assert.AreEqual(priority_queue.Pop(), 80);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тестирование методов <see cref="SparseSet"/>
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Test]
			public static void TestSparseSet()
			{
				SparseSet sparse_set = new SparseSet(1024);
				sparse_set.Add(15); // 0
				sparse_set.Add(20); // 1
				sparse_set.Add(25); // 2
				sparse_set.Add(30); // 3

				Assert.AreEqual(sparse_set.Contains(15), true);
				Assert.AreEqual(sparse_set.Contains(20), true);
				Assert.AreEqual(sparse_set.Contains(25), true);
				Assert.AreEqual(sparse_set.Contains(30), true);
				Assert.AreEqual(sparse_set.Count, 4);
				Assert.AreEqual(sparse_set.MaxCount, 1024);

				sparse_set.Remove(20);

				Assert.AreEqual(sparse_set.Contains(20), false);
				Assert.AreEqual(sparse_set[0], 15);
				Assert.AreEqual(sparse_set[1], 30);
				Assert.AreEqual(sparse_set[2], 25);

				Assert.AreEqual(sparse_set.Count, 3);
				Assert.AreEqual(sparse_set.MaxCount, 1024);

				sparse_set.Add(1024);
				Assert.AreEqual(sparse_set.Contains(1024), true);
				Assert.AreEqual(sparse_set.Count, 4);
				Assert.AreEqual(sparse_set.MaxCount, 2048);

				sparse_set.AddValues(233, 3666, 15, 5555, 66, 777);
				Assert.AreEqual(sparse_set.Contains(233), true);
				Assert.AreEqual(sparse_set.Contains(3666), true);
				Assert.AreEqual(sparse_set.Contains(5555), true);
				Assert.AreEqual(sparse_set.Contains(15), true);
				Assert.AreEqual(sparse_set.Contains(66), true);
				Assert.AreEqual(sparse_set.Contains(777), true);
				Assert.AreEqual(sparse_set.Count, 9);
				Assert.AreEqual(sparse_set.MaxCount, 8192);

				sparse_set.RemoveValues(233, 3666, 5555, 15, 66, 777);
				Assert.AreEqual(sparse_set.Count, 3);
				Assert.AreEqual(sparse_set.Contains(1024), true);
				Assert.AreEqual(sparse_set.Contains(25), true);
				Assert.AreEqual(sparse_set.Contains(30), true);
				Assert.AreEqual(sparse_set.MaxCount, 8192);
			}
		}
	}
}
//=====================================================================================================================