using System;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core
{
    /// <summary>
    /// Статический класс для тестирования подсистемы ViewModel модуля базового ядра.
    /// </summary>
    public static class XCoreViewModelTesting
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public class CActivity : COwnedObject, IComparable<CActivity>, ILotusIdentifierLong
        {
            public string Vilage { get; set; }
            public string Programm { get; set; }
            public int Price { get; set; }
            public int Total { get; set; }

            public long Id
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

            public override string ToString()
            {
                return Vilage + "=" + Programm + "=" + Price.ToString();
            }

            public int CompareTo(CActivity other)
            {
                return Price.CompareTo(other.Price);
            }
        }

        /// <summary>
        /// ViewModel для тестирования.
        /// </summary>
        public class ViewModelActivity : ViewModel<CActivity>
        {
            public ViewModelActivity(CActivity model, string name = null)
                : base(model, name)
            {
            }
        }

        /// <summary>
        /// CollectionViewModel для тестирования.
        /// </summary>
        public class CollectionViewModelActivity : CollectionViewModel<ViewModelActivity, CActivity>
        {
            public override ILotusViewModel CreateViewModel(object model)
            {
                if (model is CActivity activity)
                {
                    return new ViewModelActivity(activity);
                }

                return null;
            }
        }

        /// <summary>
        /// Тестирование методов.
        /// </summary>
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

        /// <summary>
        /// Тестирование методов коллекции.
        /// </summary>
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