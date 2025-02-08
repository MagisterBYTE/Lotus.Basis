#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    [TestFixture]
    public class ListArrayTests
    {
        /// <summary>
        /// Тестирование методов <see cref="ListArray{TItem}"/>.
        /// </summary>
        [Test]
        public void AllMethods()
        {
            var sample = new ListArray<int>(20);

            // Добавление
            sample.Add(2000);
            ClassicAssert.AreEqual(sample.Count, 1);
            ClassicAssert.AreEqual(sample[0], 2000);

            // Вставка
            sample.Insert(0, 1000);
            ClassicAssert.AreEqual(sample.Count, 2);
            ClassicAssert.AreEqual(sample[0], 1000);
            ClassicAssert.AreEqual(sample[1], 2000);

            // Вставка
            sample.Insert(1, 500);
            ClassicAssert.AreEqual(sample.Count, 3);
            ClassicAssert.AreEqual(sample[0], 1000);
            ClassicAssert.AreEqual(sample[1], 500);
            ClassicAssert.AreEqual(sample[2], 2000);

            // Удаление
            sample.Remove(500);
            ClassicAssert.AreEqual(sample.Count, 2);
            ClassicAssert.AreEqual(sample[0], 1000);
            ClassicAssert.AreEqual(sample[1], 2000);

            // Очистка
            sample.Clear();
            ClassicAssert.AreEqual(sample.Count, 0);
            ClassicAssert.AreEqual(sample.MaxCount, 20);

            // Добавление списка элементов
            sample.AddItems(23, 568, 788, 4587);
            ClassicAssert.AreEqual(sample.Count, 4);

            // Вставка списка элементов
            sample.InsertItems(2, 87, 987);
            ClassicAssert.AreEqual(sample.Count, 6);
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 568);
            ClassicAssert.AreEqual(sample[2], 87);
            ClassicAssert.AreEqual(sample[3], 987);
            ClassicAssert.AreEqual(sample[4], 788);
            ClassicAssert.AreEqual(sample[5], 4587);

            // Удаление диапазона
            sample.RemoveRange(3, 2);
            ClassicAssert.AreEqual(sample.Count, 4);
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 568);
            ClassicAssert.AreEqual(sample[2], 87);
            ClassicAssert.AreEqual(sample[3], 4587);

            // Добавление списка элементов
            sample.AddItems(444, 545, 999, 842);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 568); // Будет удалено в следующем тесте
            ClassicAssert.AreEqual(sample[2], 87);
            ClassicAssert.AreEqual(sample[3], 4587);
            ClassicAssert.AreEqual(sample[4], 444); // Будет удалено в следующем тесте
            ClassicAssert.AreEqual(sample[5], 545);
            ClassicAssert.AreEqual(sample[6], 999);
            ClassicAssert.AreEqual(sample[7], 842); // Будет удалено в следующем тесте

            // Удаление по условию
            var count = sample.RemoveAll((int x) =>
            {
                return x % 2 == 0;
            });
            ClassicAssert.AreEqual(sample.Count, 5);
            ClassicAssert.AreEqual(count, 3);
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 87);
            ClassicAssert.AreEqual(sample[2], 4587);
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 999);

            // Добавление списка элементов
            sample.AddItems(23, 568, 788, 4587);
            ClassicAssert.AreEqual(sample.Count, 9);
            ClassicAssert.AreEqual(sample[0], 23); // Duplicate 23
            ClassicAssert.AreEqual(sample[1], 87);
            ClassicAssert.AreEqual(sample[2], 4587); // Duplicate 4587
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 23); // Duplicate 23
            ClassicAssert.AreEqual(sample[6], 568);
            ClassicAssert.AreEqual(sample[7], 788);
            ClassicAssert.AreEqual(sample[8], 4587); // Duplicate 4587

            // Удаление дубликатов
            var count_dublicate = sample.RemoveDuplicates();
            ClassicAssert.AreEqual(count_dublicate, 2);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 87);
            ClassicAssert.AreEqual(sample[2], 4587);
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 568);
            ClassicAssert.AreEqual(sample[6], 788);

            // Сортировка по возрастанию
            sample.SortAscending();
            ClassicAssert.AreEqual(sample[0], 23);
            ClassicAssert.AreEqual(sample[1], 87);
            ClassicAssert.AreEqual(sample[2], 545);
            ClassicAssert.AreEqual(sample[3], 568);
            ClassicAssert.AreEqual(sample[4], 788);
            ClassicAssert.AreEqual(sample[5], 999);
            ClassicAssert.AreEqual(sample[6], 4587);

            // Сортировка по убыванию
            sample.SortDescending();
            ClassicAssert.AreEqual(sample[0], 4587);
            ClassicAssert.AreEqual(sample[1], 999);
            ClassicAssert.AreEqual(sample[2], 788);
            ClassicAssert.AreEqual(sample[3], 568);
            ClassicAssert.AreEqual(sample[4], 545);
            ClassicAssert.AreEqual(sample[5], 87);
            ClassicAssert.AreEqual(sample[6], 23);

            // Удаление первого элемента
            sample.RemoveFirst();
            ClassicAssert.AreEqual(sample.Count, 6);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 788);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 87);
            ClassicAssert.AreEqual(sample[5], 23);

            // Удаление последнего элемента
            sample.RemoveLast();
            ClassicAssert.AreEqual(sample.Count, 5);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 788);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 87);

            // Емкость
            sample.TrimExcess();
            ClassicAssert.AreEqual(sample.Count, 5);
            ClassicAssert.AreEqual(sample.MaxCount, 5);

            // Перемещаем элемент с индексом 2 вниз
            sample.MoveDown(2);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 788);
            ClassicAssert.AreEqual(sample[2], 545);
            ClassicAssert.AreEqual(sample[3], 568);
            ClassicAssert.AreEqual(sample[4], 87);

            // Циклическое смещение элементов списка вниз
            sample.ShiftDown();
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 999);
            ClassicAssert.AreEqual(sample[2], 788);
            ClassicAssert.AreEqual(sample[3], 545);
            ClassicAssert.AreEqual(sample[4], 568);

            sample.SortAscending();
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);

            // Поиск ближайшего индекса
            ClassicAssert.AreEqual(sample.GetClosestIndex(50), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(87), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(90), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(545), 1);
            ClassicAssert.AreEqual(sample.GetClosestIndex(550), 1);
            ClassicAssert.AreEqual(sample.GetClosestIndex(998), 3);
            ClassicAssert.AreEqual(sample.GetClosestIndex(999), sample.LastIndex);
            ClassicAssert.AreEqual(sample.GetClosestIndex(1000), sample.LastIndex);

            // Обрезка
            sample.Add(5566);
            sample.Add(9874);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            var save = sample.GetItemsDuplicate();

            //
            // Обрезка списка сначала
            //
            ClassicAssert.AreEqual(sample.TrimStart(87, false), 0);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(87), 1);
            ClassicAssert.AreEqual(sample[0], 545);
            ClassicAssert.AreEqual(sample[1], 568);
            ClassicAssert.AreEqual(sample[2], 788);
            ClassicAssert.AreEqual(sample[3], 999);
            ClassicAssert.AreEqual(sample[4], 5566);
            ClassicAssert.AreEqual(sample[5], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(999, false), 3);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 5566);
            ClassicAssert.AreEqual(sample[2], 9874);

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(999), 5);
            ClassicAssert.AreEqual(sample[0], 5566);
            ClassicAssert.AreEqual(sample[1], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(999), -1); // Ничего не нашли

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(9874, false), 6);
            ClassicAssert.AreEqual(sample.Count, 1);
            ClassicAssert.AreEqual(sample[0], 9874);

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimStart(9874), 7);
            ClassicAssert.AreEqual(sample.Count, 0);

            //
            // Поиск ближайшего индекса
            //
            sample.AssignItems(87, 545, 568, 788, 999);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);


            ClassicAssert.AreEqual(sample.GetClosestIndex(50), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(87), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(90), 0);
            ClassicAssert.AreEqual(sample.GetClosestIndex(545), 1);
            ClassicAssert.AreEqual(sample.GetClosestIndex(550), 1);
            ClassicAssert.AreEqual(sample.GetClosestIndex(568), 2);
            ClassicAssert.AreEqual(sample.GetClosestIndex(788), 3);
            ClassicAssert.AreEqual(sample.GetClosestIndex(998), 3);
            ClassicAssert.AreEqual(sample.GetClosestIndex(999), sample.LastIndex);
            ClassicAssert.AreEqual(sample.GetClosestIndex(1000), sample.LastIndex);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(50), 0);
            ClassicAssert.AreEqual(sample.TrimClosestStart(87, false), 0);
            ClassicAssert.AreEqual(sample.TrimClosestStart(87), 1);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 545);
            ClassicAssert.AreEqual(sample[1], 568);
            ClassicAssert.AreEqual(sample[2], 788);
            ClassicAssert.AreEqual(sample[3], 999);
            ClassicAssert.AreEqual(sample[4], 1203);
            ClassicAssert.AreEqual(sample[5], 5684);
            ClassicAssert.AreEqual(sample[6], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(800), 4);
            ClassicAssert.AreEqual(sample.Count, 4);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 1203);
            ClassicAssert.AreEqual(sample[2], 5684);
            ClassicAssert.AreEqual(sample[3], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(788), 4);
            ClassicAssert.AreEqual(sample.Count, 4);
            ClassicAssert.AreEqual(sample[0], 999);
            ClassicAssert.AreEqual(sample[1], 1203);
            ClassicAssert.AreEqual(sample[2], 5684);
            ClassicAssert.AreEqual(sample[3], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(788, false), 3);
            ClassicAssert.AreEqual(sample.Count, 5);
            ClassicAssert.AreEqual(sample[0], 788);
            ClassicAssert.AreEqual(sample[1], 999);
            ClassicAssert.AreEqual(sample[2], 1203);
            ClassicAssert.AreEqual(sample[3], 5684);
            ClassicAssert.AreEqual(sample[4], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(5987, false), 4);
            ClassicAssert.AreEqual(sample.Count, 1);
            ClassicAssert.AreEqual(sample[0], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestStart(5987), 8);
            ClassicAssert.AreEqual(sample.Count, 0);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestEnd(10000, false), 0);
            ClassicAssert.AreEqual(sample.TrimClosestEnd(10000), 0);
            ClassicAssert.AreEqual(sample.TrimClosestEnd(5987, false), 0);
            ClassicAssert.AreEqual(sample.TrimClosestEnd(5987), 1);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);

            ClassicAssert.AreEqual(sample.TrimClosestEnd(1000), 2);
            ClassicAssert.AreEqual(sample.Count, 5);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);

            ClassicAssert.AreEqual(sample.TrimClosestEnd(545), 4);
            ClassicAssert.AreEqual(sample.Count, 1);
            ClassicAssert.AreEqual(sample[0], 87);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            ClassicAssert.AreEqual(sample.Count, 8);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 1203);
            ClassicAssert.AreEqual(sample[6], 5684);
            ClassicAssert.AreEqual(sample[7], 5987);

            ClassicAssert.AreEqual(sample.TrimClosestEnd(545, false), 6);
            ClassicAssert.AreEqual(sample.Count, 2);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);

            ClassicAssert.AreEqual(sample.TrimClosestEnd(40, false), 2);
            ClassicAssert.AreEqual(sample.Count, 0);

            //
            // Обрезка сконца
            //
            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimEnd(9874, false), 0);
            ClassicAssert.AreEqual(sample.TrimEnd(9874), 1);
            ClassicAssert.AreEqual(sample.Count, 6);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);

            ClassicAssert.AreEqual(sample.TrimEnd(788, false), 2);
            ClassicAssert.AreEqual(sample.Count, 4);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimEnd(788), 4);
            ClassicAssert.AreEqual(sample.Count, 3);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimEnd(87, false), 6);
            ClassicAssert.AreEqual(sample.Count, 1);
            ClassicAssert.AreEqual(sample[0], 87);

            sample.AssignItems(save);
            ClassicAssert.AreEqual(sample.Count, 7);
            ClassicAssert.AreEqual(sample[0], 87);
            ClassicAssert.AreEqual(sample[1], 545);
            ClassicAssert.AreEqual(sample[2], 568);
            ClassicAssert.AreEqual(sample[3], 788);
            ClassicAssert.AreEqual(sample[4], 999);
            ClassicAssert.AreEqual(sample[5], 5566);
            ClassicAssert.AreEqual(sample[6], 9874);

            ClassicAssert.AreEqual(sample.TrimEnd(87), 7);
            ClassicAssert.AreEqual(sample.Count, 0);

            //
            // Еще один пример
            //
            var list = new ListArray<string>();
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

            ClassicAssert.AreEqual(list.Count, 8);
            ClassicAssert.AreEqual(list[0], "00");
            ClassicAssert.AreEqual(list[1], "05");
            ClassicAssert.AreEqual(list[2], "06");
            ClassicAssert.AreEqual(list[3], "07");
            ClassicAssert.AreEqual(list[4], "08");
            ClassicAssert.AreEqual(list[5], "09");
            ClassicAssert.AreEqual(list[6], "10");
            ClassicAssert.AreEqual(list[7], "11");

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

            ClassicAssert.AreEqual(list.Count, 11);
            ClassicAssert.AreEqual(list[0], "00");
            ClassicAssert.AreEqual(list[1], "01");
            ClassicAssert.AreEqual(list[2], "02");
            ClassicAssert.AreEqual(list[3], "03");
            ClassicAssert.AreEqual(list[4], "04");
            ClassicAssert.AreEqual(list[5], "05");
            ClassicAssert.AreEqual(list[6], "06");
            ClassicAssert.AreEqual(list[7], "07");
            ClassicAssert.AreEqual(list[8], "08");
            ClassicAssert.AreEqual(list[9], "09");
            ClassicAssert.AreEqual(list[10], "10");

            list.RemoveItemsEnd(8);

            ClassicAssert.AreEqual(list.Count, 8);
            ClassicAssert.AreEqual(list[0], "00");
            ClassicAssert.AreEqual(list[1], "01");
            ClassicAssert.AreEqual(list[2], "02");
            ClassicAssert.AreEqual(list[3], "03");
            ClassicAssert.AreEqual(list[4], "04");
            ClassicAssert.AreEqual(list[5], "05");
            ClassicAssert.AreEqual(list[6], "06");
            ClassicAssert.AreEqual(list[7], "07");

            list.RemoveItemsEnd(7);

            ClassicAssert.AreEqual(list.Count, 7);
            ClassicAssert.AreEqual(list[0], "00");
            ClassicAssert.AreEqual(list[1], "01");
            ClassicAssert.AreEqual(list[2], "02");
            ClassicAssert.AreEqual(list[3], "03");
            ClassicAssert.AreEqual(list[4], "04");
            ClassicAssert.AreEqual(list[5], "05");
            ClassicAssert.AreEqual(list[6], "06");

            //
            // Операции множеств
            //
            var deferencs_source = new ListArray<int>();
            deferencs_source.AddItems(0, 2, 4, 7, 5);

            var deferencs = deferencs_source.DifferenceItems(0, 4, 7, 12, 15, 7);
            ClassicAssert.AreEqual(deferencs.Count, 2);
            ClassicAssert.AreEqual(deferencs[0], 2);
            ClassicAssert.AreEqual(deferencs[1], 5);
        }
    }
}