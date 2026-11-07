#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif


namespace Lotus.Core.Collections
{
    public class ListArrayTests
    {
        /// <summary>
        /// Тестирование методов <see cref="ListArray{TItem}"/>.
        /// </summary>
        [Fact]
        public void AllMethods()
        {
            var sample = new ListArray<int>(20);

            // Добавление
            sample.Add(2000);
            Assert.Equal(sample.Count, 1);
            Assert.Equal(sample[0], 2000);

            // Вставка
            sample.Insert(0, 1000);
            Assert.Equal(sample.Count, 2);
            Assert.Equal(sample[0], 1000);
            Assert.Equal(sample[1], 2000);

            // Вставка
            sample.Insert(1, 500);
            Assert.Equal(sample.Count, 3);
            Assert.Equal(sample[0], 1000);
            Assert.Equal(sample[1], 500);
            Assert.Equal(sample[2], 2000);

            // Удаление
            sample.Remove(500);
            Assert.Equal(sample.Count, 2);
            Assert.Equal(sample[0], 1000);
            Assert.Equal(sample[1], 2000);

            // Очистка
            sample.Clear();
            Assert.Equal(sample.Count, 0);
            Assert.Equal(sample.MaxCount, 20);

            // Добавление списка элементов
            sample.AddItems(23, 568, 788, 4587);
            Assert.Equal(sample.Count, 4);

            // Вставка списка элементов
            sample.InsertItems(2, 87, 987);
            Assert.Equal(sample.Count, 6);
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 568);
            Assert.Equal(sample[2], 87);
            Assert.Equal(sample[3], 987);
            Assert.Equal(sample[4], 788);
            Assert.Equal(sample[5], 4587);

            // Удаление диапазона
            sample.RemoveRange(3, 2);
            Assert.Equal(sample.Count, 4);
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 568);
            Assert.Equal(sample[2], 87);
            Assert.Equal(sample[3], 4587);

            // Добавление списка элементов
            sample.AddItems(444, 545, 999, 842);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 568); // Будет удалено в следующем тесте
            Assert.Equal(sample[2], 87);
            Assert.Equal(sample[3], 4587);
            Assert.Equal(sample[4], 444); // Будет удалено в следующем тесте
            Assert.Equal(sample[5], 545);
            Assert.Equal(sample[6], 999);
            Assert.Equal(sample[7], 842); // Будет удалено в следующем тесте

            // Удаление по условию
            var count = sample.RemoveAll((int x) =>
            {
                return x % 2 == 0;
            });
            Assert.Equal(sample.Count, 5);
            Assert.Equal(count, 3);
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 87);
            Assert.Equal(sample[2], 4587);
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 999);

            // Добавление списка элементов
            sample.AddItems(23, 568, 788, 4587);
            Assert.Equal(sample.Count, 9);
            Assert.Equal(sample[0], 23); // Duplicate 23
            Assert.Equal(sample[1], 87);
            Assert.Equal(sample[2], 4587); // Duplicate 4587
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 23); // Duplicate 23
            Assert.Equal(sample[6], 568);
            Assert.Equal(sample[7], 788);
            Assert.Equal(sample[8], 4587); // Duplicate 4587

            // Удаление дубликатов
            var countDublicate = sample.RemoveDuplicates();
            Assert.Equal(countDublicate, 2);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 87);
            Assert.Equal(sample[2], 4587);
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 568);
            Assert.Equal(sample[6], 788);

            // Сортировка по возрастанию
            sample.SortAscending();
            Assert.Equal(sample[0], 23);
            Assert.Equal(sample[1], 87);
            Assert.Equal(sample[2], 545);
            Assert.Equal(sample[3], 568);
            Assert.Equal(sample[4], 788);
            Assert.Equal(sample[5], 999);
            Assert.Equal(sample[6], 4587);

            // Сортировка по убыванию
            sample.SortDescending();
            Assert.Equal(sample[0], 4587);
            Assert.Equal(sample[1], 999);
            Assert.Equal(sample[2], 788);
            Assert.Equal(sample[3], 568);
            Assert.Equal(sample[4], 545);
            Assert.Equal(sample[5], 87);
            Assert.Equal(sample[6], 23);

            // Удаление первого элемента
            sample.RemoveFirst();
            Assert.Equal(sample.Count, 6);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 788);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 87);
            Assert.Equal(sample[5], 23);

            // Удаление последнего элемента
            sample.RemoveLast();
            Assert.Equal(sample.Count, 5);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 788);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 87);

            // Емкость
            sample.TrimExcess();
            Assert.Equal(sample.Count, 5);
            Assert.Equal(sample.MaxCount, 5);

            // Перемещаем элемент с индексом 2 вниз
            sample.MoveDown(2);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 788);
            Assert.Equal(sample[2], 545);
            Assert.Equal(sample[3], 568);
            Assert.Equal(sample[4], 87);

            // Циклическое смещение элементов списка вниз
            sample.ShiftDown();
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 999);
            Assert.Equal(sample[2], 788);
            Assert.Equal(sample[3], 545);
            Assert.Equal(sample[4], 568);

            sample.SortAscending();
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);

            // Поиск ближайшего индекса
            Assert.Equal(sample.GetClosestIndex(50), 0);
            Assert.Equal(sample.GetClosestIndex(87), 0);
            Assert.Equal(sample.GetClosestIndex(90), 0);
            Assert.Equal(sample.GetClosestIndex(545), 1);
            Assert.Equal(sample.GetClosestIndex(550), 1);
            Assert.Equal(sample.GetClosestIndex(998), 3);
            Assert.Equal(sample.GetClosestIndex(999), sample.LastIndex);
            Assert.Equal(sample.GetClosestIndex(1000), sample.LastIndex);

            // Обрезка
            sample.Add(5566);
            sample.Add(9874);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            var save = sample.GetItemsDuplicate();

            //
            // Обрезка списка сначала
            //
            Assert.Equal(sample.TrimStart(87, false), 0);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimStart(87), 1);
            Assert.Equal(sample[0], 545);
            Assert.Equal(sample[1], 568);
            Assert.Equal(sample[2], 788);
            Assert.Equal(sample[3], 999);
            Assert.Equal(sample[4], 5566);
            Assert.Equal(sample[5], 9874);

            Assert.Equal(sample.TrimStart(999, false), 3);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 5566);
            Assert.Equal(sample[2], 9874);

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimStart(999), 5);
            Assert.Equal(sample[0], 5566);
            Assert.Equal(sample[1], 9874);

            Assert.Equal(sample.TrimStart(999), -1); // Ничего не нашли

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimStart(9874, false), 6);
            Assert.Equal(sample.Count, 1);
            Assert.Equal(sample[0], 9874);

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimStart(9874), 7);
            Assert.Equal(sample.Count, 0);

            //
            // Поиск ближайшего индекса
            //
            sample.AssignItems(87, 545, 568, 788, 999);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);


            Assert.Equal(sample.GetClosestIndex(50), 0);
            Assert.Equal(sample.GetClosestIndex(87), 0);
            Assert.Equal(sample.GetClosestIndex(90), 0);
            Assert.Equal(sample.GetClosestIndex(545), 1);
            Assert.Equal(sample.GetClosestIndex(550), 1);
            Assert.Equal(sample.GetClosestIndex(568), 2);
            Assert.Equal(sample.GetClosestIndex(788), 3);
            Assert.Equal(sample.GetClosestIndex(998), 3);
            Assert.Equal(sample.GetClosestIndex(999), sample.LastIndex);
            Assert.Equal(sample.GetClosestIndex(1000), sample.LastIndex);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestStart(50), 0);
            Assert.Equal(sample.TrimClosestStart(87, false), 0);
            Assert.Equal(sample.TrimClosestStart(87), 1);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 545);
            Assert.Equal(sample[1], 568);
            Assert.Equal(sample[2], 788);
            Assert.Equal(sample[3], 999);
            Assert.Equal(sample[4], 1203);
            Assert.Equal(sample[5], 5684);
            Assert.Equal(sample[6], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestStart(800), 4);
            Assert.Equal(sample.Count, 4);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 1203);
            Assert.Equal(sample[2], 5684);
            Assert.Equal(sample[3], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestStart(788), 4);
            Assert.Equal(sample.Count, 4);
            Assert.Equal(sample[0], 999);
            Assert.Equal(sample[1], 1203);
            Assert.Equal(sample[2], 5684);
            Assert.Equal(sample[3], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestStart(788, false), 3);
            Assert.Equal(sample.Count, 5);
            Assert.Equal(sample[0], 788);
            Assert.Equal(sample[1], 999);
            Assert.Equal(sample[2], 1203);
            Assert.Equal(sample[3], 5684);
            Assert.Equal(sample[4], 5987);

            Assert.Equal(sample.TrimClosestStart(5987, false), 4);
            Assert.Equal(sample.Count, 1);
            Assert.Equal(sample[0], 5987);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestStart(5987), 8);
            Assert.Equal(sample.Count, 0);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestEnd(10000, false), 0);
            Assert.Equal(sample.TrimClosestEnd(10000), 0);
            Assert.Equal(sample.TrimClosestEnd(5987, false), 0);
            Assert.Equal(sample.TrimClosestEnd(5987), 1);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);

            Assert.Equal(sample.TrimClosestEnd(1000), 2);
            Assert.Equal(sample.Count, 5);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);

            Assert.Equal(sample.TrimClosestEnd(545), 4);
            Assert.Equal(sample.Count, 1);
            Assert.Equal(sample[0], 87);

            sample.AssignItems(87, 545, 568, 788, 999, 1203, 5684, 5987);
            Assert.Equal(sample.Count, 8);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 1203);
            Assert.Equal(sample[6], 5684);
            Assert.Equal(sample[7], 5987);

            Assert.Equal(sample.TrimClosestEnd(545, false), 6);
            Assert.Equal(sample.Count, 2);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);

            Assert.Equal(sample.TrimClosestEnd(40, false), 2);
            Assert.Equal(sample.Count, 0);

            //
            // Обрезка сконца
            //
            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimEnd(9874, false), 0);
            Assert.Equal(sample.TrimEnd(9874), 1);
            Assert.Equal(sample.Count, 6);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);

            Assert.Equal(sample.TrimEnd(788, false), 2);
            Assert.Equal(sample.Count, 4);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimEnd(788), 4);
            Assert.Equal(sample.Count, 3);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimEnd(87, false), 6);
            Assert.Equal(sample.Count, 1);
            Assert.Equal(sample[0], 87);

            sample.AssignItems(save);
            Assert.Equal(sample.Count, 7);
            Assert.Equal(sample[0], 87);
            Assert.Equal(sample[1], 545);
            Assert.Equal(sample[2], 568);
            Assert.Equal(sample[3], 788);
            Assert.Equal(sample[4], 999);
            Assert.Equal(sample[5], 5566);
            Assert.Equal(sample[6], 9874);

            Assert.Equal(sample.TrimEnd(87), 7);
            Assert.Equal(sample.Count, 0);

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

            Assert.Equal(list.Count, 8);
            Assert.Equal(list[0], "00");
            Assert.Equal(list[1], "05");
            Assert.Equal(list[2], "06");
            Assert.Equal(list[3], "07");
            Assert.Equal(list[4], "08");
            Assert.Equal(list[5], "09");
            Assert.Equal(list[6], "10");
            Assert.Equal(list[7], "11");

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

            Assert.Equal(list.Count, 11);
            Assert.Equal(list[0], "00");
            Assert.Equal(list[1], "01");
            Assert.Equal(list[2], "02");
            Assert.Equal(list[3], "03");
            Assert.Equal(list[4], "04");
            Assert.Equal(list[5], "05");
            Assert.Equal(list[6], "06");
            Assert.Equal(list[7], "07");
            Assert.Equal(list[8], "08");
            Assert.Equal(list[9], "09");
            Assert.Equal(list[10], "10");

            list.RemoveItemsEnd(8);

            Assert.Equal(list.Count, 8);
            Assert.Equal(list[0], "00");
            Assert.Equal(list[1], "01");
            Assert.Equal(list[2], "02");
            Assert.Equal(list[3], "03");
            Assert.Equal(list[4], "04");
            Assert.Equal(list[5], "05");
            Assert.Equal(list[6], "06");
            Assert.Equal(list[7], "07");

            list.RemoveItemsEnd(7);

            Assert.Equal(list.Count, 7);
            Assert.Equal(list[0], "00");
            Assert.Equal(list[1], "01");
            Assert.Equal(list[2], "02");
            Assert.Equal(list[3], "03");
            Assert.Equal(list[4], "04");
            Assert.Equal(list[5], "05");
            Assert.Equal(list[6], "06");

            //
            // Операции множеств
            //
            var deferencsSource = new ListArray<int>();
            deferencsSource.AddItems(0, 2, 4, 7, 5);

            var deferencs = deferencsSource.DifferenceItems(0, 4, 7, 12, 15, 7);
            Assert.Equal(deferencs.Count, 2);
            Assert.Equal(deferencs[0], 2);
            Assert.Equal(deferencs[1], 5);
        }
    }
}
