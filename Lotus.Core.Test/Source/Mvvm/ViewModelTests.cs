using System;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace Lotus.Core.Mvvm
{
    /// <summary>
    /// Статический класс для тестирования подсистемы ViewModel модуля базового ядра.
    /// </summary>
    public static class ViewModelTests
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

            public readonly static CActivity[] Activities =
            [
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
            ];

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
        [Fact]
        public static void TestView()
        {
            var viewItem = new ViewModelActivity(new CActivity()
            {
                Vilage = "Бреды",
                Programm = "Академия",
                Price = 2000
            })
            {
                IsEnabled = true,
                IsSelected = true,
                IsPresented = true
            };

            Assert.Equal(true, ViewModelActivity.IsSupportIdentifierLong);
            Assert.Equal(false, ViewModelActivity.IsSupportNameable);
            Assert.Equal(false, ViewModelActivity.IsSupportModelEnabled);
            Assert.Equal(false, ViewModelActivity.IsSupportModelSelected);
        }

        /// <summary>
        /// Тестирование методов коллекции.
        /// </summary>
        [Fact]
        public static void TestCollectionView()
        {
            var viewItems = new CollectionViewModelActivity
            {
                Source = CActivity.Activities
            };

            // Общие данные
            Assert.Equal(10, viewItems.Count);
            Assert.Equal(true, CollectionViewModelActivity.IsNullable);
            Assert.Equal(false, viewItems.IsReadOnly);
            Assert.Equal(true, viewItems.IsFixedSize);

            // Проверка имени 
            for (var i = 0; i < CActivity.Activities.Length; i++)
            {
                Assert.Equal(viewItems[i].ToString(), CActivity.Activities[i].ToString());
            }

            // Сортировка
            viewItems.SortAscending();
            for (var i = 0; i < CActivity.Activities.Length; i++)
            {
                Assert.Equal(CActivity.Activities[i].Price, viewItems[i].Model.Price);
            }

            viewItems.SortDescending();
            for (var i = 0; i < CActivity.Activities.Length; i++)
            {
                Assert.Equal(CActivity.Activities[9 - i].Price, viewItems[i].Model.Price);
            }

            // Фильтрация
            viewItems.IsFiltered = true;
            viewItems.Filter = (CActivity activity) =>
            {
                return activity.Price > 5;
            };
            Assert.Equal(5, viewItems.Count);
            viewItems.SortDescending();
            Assert.Equal(10, viewItems[0].Model.Price);
            Assert.Equal(9, viewItems[1].Model.Price);
            Assert.Equal(8, viewItems[2].Model.Price);
            Assert.Equal(7, viewItems[3].Model.Price);
            Assert.Equal(6, viewItems[4].Model.Price);

            // Удаление
            //viewItems.RemoveAll()
        }
    }
}