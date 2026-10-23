#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections;
using System.Collections.Specialized;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityTypes
    *@{*/
    /// <summary>
    /// Контейнер для создания, хранение и управление списком дочерних элементов
    /// </summary>
    /// <remarks>
    /// <para>
    /// Контейнер обеспечивает базовую функциональность для создания, хранения и управления списком дочерних элементов
    /// применительно к игровым объектам Unity
    /// </para>
    /// <para>
    /// Он обеспечивает синхронизацию между позицией элемента в списке и позицией компонента трасформации применительно
    /// к игровому объекту
    /// </para>
    /// </remarks>
    /// <typeparam name="TItem">Тип элемента</typeparam>
    [Serializable]
    public class ContainerComponent<TItem> : ListArray<TItem> where TItem : UnityEngine.Component
    {
        #region Fields
        // Основные параметры
        [SerializeField]
        internal Transform _parent;

        // Механизм создания элементов
        [NonSerialized]
        internal IList _itemsSource;
        [NonSerialized]
        internal Func<object, TItem> _itemConstructor;
        [SerializeField]
        internal TItem _itemPrefab;
        #endregion

        #region Properties
        /// <summary>
        /// Родительский компонент трансформации
        /// </summary>
        public Transform Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Источник данных
        /// </summary>
        /// <remarks>
        /// При установке контейнер пересоздаёт дочерние элементы.
        /// Если источник реализует <see cref="INotifyCollectionChanged"/>, изменения отслеживаются автоматически.
        /// </remarks>
        public IList ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                if (ReferenceEquals(_itemsSource, value))
                {
                    return;
                }

                UnsubscribeItemsSource();
                _itemsSource = value;
                SubscribeItemsSource();
                RebuildFromItemsSource();
            }
        }

        /// <summary>
        /// Внешний конструктор для создания элемента из объекта источника данных
        /// </summary>
        public Func<object, TItem> ItemConstructor
        {
            get { return _itemConstructor; }
            set
            {
                _itemConstructor = value;
                if (_itemsSource != null)
                {
                    RebuildFromItemsSource();
                }
            }
        }

        /// <summary>
        /// Префаб для создания элемента
        /// </summary>
        public TItem ItemPrefab
        {
            get { return _itemPrefab; }
            set { _itemPrefab = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные контейнера предустановленными данными
        /// </summary>
        public ContainerComponent()
            : base(INIT_MAX_COUNT)
        {
        }


        /// <summary>
        /// Конструктор инициализирует данные контейнера указанными данными
        /// </summary>
        /// <param name="maxCount">Максимальное количество элементов</param>
        public ContainerComponent(int maxCount)
            : base(maxCount)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Обновление индексов компонентов трансформации
        /// </summary>
        /// <remarks>
        /// Подразумевается что положение элементов в списке изменилось, например в результате сортировки или
        /// перемещения элемента и теперь необходимо синхронизировать индексы компонента трансформации
        /// </remarks>
        public virtual void UpdateIndexTransformSibling()
        {
            for (var i = 0; i < Count; i++)
            {
                _arrayOfItems[i].transform.SetSiblingIndex(i);
            }
        }


        /// <summary>
        /// Обновление индексов элементов
        /// </summary>
        /// <remarks>
        /// Подразумевается что индексы компонентов трансформации изменились теперь необходимо синхронизировать
        /// с положение данных элементов в списке
        /// </remarks>
        public virtual void UpdateIndexItems()
        {
            if (Count < 2)
            {
                return;
            }

            Sort((left, right) =>
            {
                if (left == null && right == null) return 0;
                if (left == null) return 1;
                if (right == null) return -1;
                return left.transform.GetSiblingIndex().CompareTo(right.transform.GetSiblingIndex());
            });
        }


        /// <summary>
        /// Пересоздание элементов контейнера из источника данных
        /// </summary>
        public virtual void RebuildFromItemsSource()
        {
            DeleteItems();

            if (_itemsSource == null || _itemsSource.Count == 0)
            {
                return;
            }

            for (var i = 0; i < _itemsSource.Count; i++)
            {
                var data = _itemsSource[i];
                var item = CreateItemFromData(data);
                if (item != null)
                {
                    AddExistsItem(item);
                }
            }
        }
        #endregion

        #region Add methods
        /// <summary>
        /// Добавление существующего элемента в контейнер
        /// </summary>
        /// <param name="item">Элемент</param>
        public virtual void AddExistsItem(TItem item)
        {
            // Присваиваем в иерархию
            if (_parent != null)
            {
                item.transform.SetParent(_parent, false);
            }

            item.name = "Item_" + Count.ToString();

            // Добавляем в список
            Add(item);
        }


        /// <summary>
        /// Создание из префаба и добавление элемента в контейнер
        /// </summary>
        /// <returns>Созданный элемент</returns>
        public virtual TItem AddNewItemFromPrefab()
        {
            if (_itemPrefab == null)
            {
                Debug.LogError("ItemPrefab == NULL");
                return default(TItem);
            }

            // Создаем элемент и добавляем
            TItem item = GameObject.Instantiate(_itemPrefab);
            item.gameObject.SetActive(true);
            AddExistsItem(item);
            return item;
        }


        /// <summary>
        /// Создание элемента из указанного префаба и добавление элемента в контейнер
        /// </summary>
        /// <param name="itemPrefab">Префаб элемента</param>
        /// <returns>Созданный элемент</returns>
        public virtual TItem AddNewItemFromPrefab(TItem itemPrefab)
        {
            if (itemPrefab == null)
            {
                Debug.LogError("itemPrefab == NULL");
                return default(TItem);
            }

            // Создаем элемент и добавляем
            TItem item = GameObject.Instantiate(itemPrefab);
            item.gameObject.SetActive(true);
            AddExistsItem(item);
            return item;
        }


        /// <summary>
        /// Дублирование элемента и добавление элемента в контейнер
        /// </summary>
        /// <param name="index">Индекс дублируемого элемента</param>
        public virtual void DuplicateItem(int index)
        {
            // Создаем элемент
            TItem item = GameObject.Instantiate(this[index]);
            AddExistsItem(item);
        }
        #endregion

        #region Insert methods
        /// <summary>
        /// Вставка элемента в указанную позицию
        /// </summary>
        /// <param name="index">Позиция вставки</param>
        /// <param name="item">Элемент</param>
        public virtual void InsertItem(int index, TItem item)
        {
            // Присваиваем в иерархию
            if (_parent != null)
            {
                item.transform.SetParent(_parent, false);
                item.transform.SetSiblingIndex(index);
            }

            item.name = "Item_" + Count.ToString();

            // Вставляем в список
            Insert(index, item);
        }


        /// <summary>
        /// Вставка элемента после указанного элемента
        /// </summary>
        /// <param name="original">Элемент после которого будет произведена вставка</param>
        /// <param name="item">Элемент</param>
        public virtual void InsertAfterItem(TItem original, TItem item)
        {
            if (item == null) return;

            var index = IndexOf(original);
            InsertItem(index + 1, item);
        }


        /// <summary>
        /// Вставка элемента перед указанным элементом
        /// </summary>
        /// <param name="original">Элемент перед которым будет произведена вставка</param>
        /// <param name="item">Элемент</param>
        public virtual void InsertBeforeItem(TItem original, TItem item)
        {
            if (item == null) return;

            var index = IndexOf(original);
            InsertItem(index, item);
        }
        #endregion

        #region Remove methods
        /// <summary>
        /// Удаление элемента из контейнера по указанному имени игрового объекта
        /// </summary>
        /// <param name="gameObjectName">Имя игрового объекта</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool RemoveItem(string gameObjectName)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_arrayOfItems[i].gameObject.name == gameObjectName)
                {
                    return RemoveItem(_arrayOfItems[i]);
                }
            }

            return false;
        }


        /// <summary>
        /// Удаление элемента из контейнера по указанному имени игрового объекта вместе с игровым объектом
        /// </summary>
        /// <param name="gameObjectName">Имя игрового объекта</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool DeleteItem(string gameObjectName)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_arrayOfItems[i].gameObject.name == gameObjectName)
                {
                    return DeleteItem(i);
                }
            }

            return false;
        }


        /// <summary>
        /// Удаление элемента из контейнера по индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool RemoveItem(int index)
        {
            // Удаляем элемент
            RemoveAt(index);
            return true;
        }


        /// <summary>
        /// Удаление элемента из контейнера по индексу вместе с игровым объектом
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool DeleteItem(int index)
        {
            var gameObject = _arrayOfItems[index].gameObject;
            RemoveAt(index);
            XGameObjectDispatcher.Destroy(gameObject);
            return true;
        }


        /// <summary>
        /// Удаление элемента из контейнера
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool RemoveItem(TItem item)
        {
            if (item == null) return false;

            return Remove(item);
        }


        /// <summary>
        /// Удаление элемента из контейнера вместе с игровым объектом
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <returns>Статус успешности удаления</returns>
        public virtual bool DeleteItem(TItem item)
        {
            if (item == null) return false;

            var gameObject = item.gameObject;
            if (Remove(item))
            {
                XGameObjectDispatcher.Destroy(gameObject);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Удаление всех элементов из контейнера
        /// </summary>
        public void ClearItems()
        {
            Clear();
        }


        /// <summary>
        /// Удаление всех элементов из контейнера вместе с игровыми объектами
        /// </summary>
        public void DeleteItems()
        {
            for (var i = 0; i < Count; i++)
            {
                XGameObjectDispatcher.Destroy(_arrayOfItems[i].gameObject);
            }

            Clear();
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Проверка на существовании элемента в контейнере
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <returns>Статус существования элемента</returns>
        public virtual bool ExistsItem(TItem item)
        {
            if (item == null) return false;

            for (var i = 0; i < Count; i++)
            {
                if (_arrayOfItems[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Поиск индекса указанного элемента
        /// </summary>
        /// <param name="item">Элемент</param>
        /// <returns>Индекс элемента или -1 если модель не найдена</returns>
        public virtual int IndexOfItem(TItem item)
        {
            return IndexOf(item);
        }


        /// <summary>
        /// Поиск элемента по указанному имени игрового объекта
        /// </summary>
        /// <param name="gameObjectName">Имя игрового объекта</param>
        /// <returns>Найденный модель или null</returns>
        public virtual TItem GetItemFromName(string gameObjectName)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_arrayOfItems[i].gameObject.name == gameObjectName)
                {
                    return _arrayOfItems[i];
                }
            }

            return default(TItem);
        }
        #endregion

        #region Move methods
        /// <summary>
        /// Перемещение элемента в новую позицию
        /// </summary>
        /// <param name="oldIndex">Старая позиция</param>
        /// <param name="newIndex">Новая позиция</param>
        public virtual void MoveItem(int oldIndex, int newIndex)
        {
            Move(oldIndex, newIndex);
            UpdateIndexTransformSibling();
        }


        /// <summary>
        /// Перемещение элемента вверх по списку
        /// </summary>
        /// <param name="item">Элемент</param>
        public virtual void MoveUpItem(TItem item)
        {
            if (item == null) return;

            var index = IndexOf(item);
            if (index > 0)
            {
                MoveUp(index);
                UpdateIndexTransformSibling();
            }
        }


        /// <summary>
        /// Перемещение элемента вниз по списку
        /// </summary>
        /// <param name="item">Элемент</param>
        public virtual void MoveDownItem(TItem item)
        {
            if (item == null) return;

            var index = IndexOf(item);
            if (index > -1 && index < Count - 1)
            {
                MoveDown(index);
                UpdateIndexTransformSibling();
            }
        }


        /// <summary>
        /// Сортировка элементов по возрастанию
        /// </summary>
        public void SortItemsAscending()
        {
            SortAscending();
            UpdateIndexTransformSibling();
        }


        /// <summary>
        /// Сортировка элементов по убыванию
        /// </summary>
        public void SortItemsDescending()
        {
            SortDescending();
            UpdateIndexTransformSibling();
        }


        /// <summary>
        /// Сортировка элементов посредством делегата
        /// </summary>
        /// <param name="comparison">Делегат сравнивающий два объекта одного типа</param>
        public void SortItems(Comparison<TItem> comparison)
        {
            Sort(comparison);
            UpdateIndexTransformSibling();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Создание элемента из объекта источника данных
        /// </summary>
        /// <param name="data">Объект данных</param>
        /// <returns>Созданный элемент или null</returns>
        private TItem CreateItemFromData(object data)
        {
            if (_itemConstructor != null)
            {
                return _itemConstructor(data);
            }

            if (_itemPrefab == null)
            {
                Debug.LogError("ItemConstructor == NULL and ItemPrefab == NULL");
                return default(TItem);
            }

            TItem item = GameObject.Instantiate(_itemPrefab);
            item.gameObject.SetActive(true);
            return item;
        }


        /// <summary>
        /// Подписка на изменения источника данных
        /// </summary>
        private void SubscribeItemsSource()
        {
            if (_itemsSource is INotifyCollectionChanged collectionChanged)
            {
                collectionChanged.CollectionChanged += OnItemsSourceCollectionChanged;
            }
        }


        /// <summary>
        /// Отписка от изменений источника данных
        /// </summary>
        private void UnsubscribeItemsSource()
        {
            if (_itemsSource is INotifyCollectionChanged collectionChanged)
            {
                collectionChanged.CollectionChanged -= OnItemsSourceCollectionChanged;
            }
        }


        /// <summary>
        /// Обработка изменения источника данных
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="args">Аргументы события</param>
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (args.NewItems == null)
                        {
                            break;
                        }

                        var insertIndex = args.NewStartingIndex;
                        for (var i = 0; i < args.NewItems.Count; i++)
                        {
                            var item = CreateItemFromData(args.NewItems[i]);
                            if (item == null)
                            {
                                continue;
                            }

                            if (insertIndex >= 0)
                            {
                                InsertItem(insertIndex + i, item);
                            }
                            else
                            {
                                AddExistsItem(item);
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (args.OldStartingIndex >= 0 && args.OldItems != null)
                        {
                            for (var i = 0; i < args.OldItems.Count; i++)
                            {
                                DeleteItem(args.OldStartingIndex);
                            }
                        }
                        else
                        {
                            RebuildFromItemsSource();
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (args.NewItems == null || args.NewStartingIndex < 0)
                        {
                            RebuildFromItemsSource();
                            break;
                        }

                        for (var i = 0; i < args.NewItems.Count; i++)
                        {
                            var index = args.NewStartingIndex + i;
                            if (index < 0 || index >= Count)
                            {
                                continue;
                            }

                            DeleteItem(index);
                            var item = CreateItemFromData(args.NewItems[i]);
                            if (item != null)
                            {
                                InsertItem(index, item);
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        if (args.OldStartingIndex >= 0 && args.NewStartingIndex >= 0)
                        {
                            MoveItem(args.OldStartingIndex, args.NewStartingIndex);
                        }
                        else
                        {
                            RebuildFromItemsSource();
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                default:
                    RebuildFromItemsSource();
                    break;
            }
        }
        #endregion
    }


    /// <summary>
    /// Контейнер для компонента <see cref="RectTransform"/>
    /// </summary>
    [Serializable]
    public class ContainerRectTransform : ContainerComponent<RectTransform>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные контейнера предустановленными данными
        /// </summary>
        public ContainerRectTransform()
            : base(INIT_MAX_COUNT)
        {
        }


        /// <summary>
        /// Конструктор инициализирует данные контейнера указанными данными
        /// </summary>
        /// <param name="maxCount">Максимальное количество элементов</param>
        public ContainerRectTransform(int maxCount)
            : base(maxCount)
        {
        }
        #endregion
    }
    /**@}*/
}
#endif
