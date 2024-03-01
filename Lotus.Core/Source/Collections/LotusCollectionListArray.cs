using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreCollections
	*@{*/
    /// <summary>
    /// Список на основе массива.
    /// </summary>
    /// <remarks>
    /// Среди основных преимуществ использования собственного типа списка можно выделить:
    /// - Обеспечивается более высокая скорость работы за счет отказа проверок границ списка.
    /// - Можно получить доступ непосредственно к родному массиву.
    /// - Также поддерживается сериализации на уровни Unity, с учетом собственного редактора.
    /// - Поддержка уведомлений о смене состояний коллекции.
    /// - Поддержка обобщенного интерфейса IList.
    /// </remarks>
    /// <typeparam name="TItem">Тип элемента списка.</typeparam>
    [Serializable]
    public class ListArray<TItem> : IList<TItem>, IList, ILotusCheckOne<TItem>,
            ILotusCheckAll<TItem>, ILotusVisit<TItem>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region Inner types
        /// <summary>
        /// Тип реализующий перечислителя по списку.
        /// </summary>
        public struct ListArrayEnumerator : IEnumerator<TItem>
        {
            #region  ДАННЫЕ
            private readonly ListArray<TItem> _list;
            private int _index;
            private TItem? _current;
            #endregion

            #region  СВОЙСТВА
            /// <summary>
            /// Текущий элемент.
            /// </summary>
            public readonly TItem Current
            {
                get
                {
                    return _current!;
                }
            }

            /// <summary>
            /// Текущий элемент.
            /// </summary>
            readonly object IEnumerator.Current
            {
                get
                {
                    return _current!;
                }
            }
            #endregion

            #region  КОНСТРУКТОРЫ
            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Конструктор инициализирует данные перечислителя указанным списком.
            /// </summary>
            /// <param name="list">Список.</param>
            //-----------------------------------------------------------------------------------------------------
            internal ListArrayEnumerator(ListArray<TItem> list)
            {
                _list = list;
                _index = 0;
                _current = _list[0];
            }
            #endregion

            #region  ОБЩИЕ МЕТОДЫ
            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Освобождение управляемых ресурсов.
            /// </summary>
            //-----------------------------------------------------------------------------------------------------
            public readonly void Dispose()
            {
            }

            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Переход к следующему элементу списка.
            /// </summary>
            /// <returns>Возможность перехода к следующему элементу списка.</returns>
            //-----------------------------------------------------------------------------------------------------
            public bool MoveNext()
            {
                if (_index < _list.Count)
                {
                    _current = _list._arrayOfItems[_index];
                    _index++;
                    return true;
                }
                else
                {
                    _index = _list.Count + 1;
                    _current = default;
                    return false;
                }
            }

            //-----------------------------------------------------------------------------------------------------
            /// <summary>
            /// Перестановка позиции на первый элемент списка.
            /// </summary>
            //-----------------------------------------------------------------------------------------------------
            void IEnumerator.Reset()
            {
                _index = 0;
                _current = _list[0];
            }
            #endregion
        }

        /// <summary>
        /// Вспомогательный функтор для сравнения элемента.
        /// </summary>
        internal sealed class FunctorComparer : IComparer<TItem>
        {
            readonly Comparison<TItem> Comparison;

            public FunctorComparer(Comparison<TItem> comparison)
            {
                Comparison = comparison;
            }

            public int Compare(TItem? x, TItem? y)
            {
                return Comparison(x!, y!);
            }
        }
        #endregion

        #region Const
        /// <summary>
        /// Максимальное количество элементов на начальном этапе.
        /// </summary>
        public const int INIT_MAX_COUNT = 8;

        /// <summary>
        /// Статус ссылочного типа элемента коллекции.
        /// </summary>
        public static readonly bool IsNullable = !typeof(TItem).IsValueType || Nullable.GetUnderlyingType(typeof(TItem)) != null;

        /// <summary>
        /// Статус поддержки типом элемента интерфейса <see cref="ILotusIndexable"/>.
        /// </summary>
        public static readonly bool IsIndexable = typeof(TItem).IsSupportInterface<ILotusIndexable>();

        /// <summary>
        /// Статус поддержки типом элемента интерфейса ILotusDuplicate.
        /// </summary>
        public static readonly bool IsDuplicatable = typeof(TItem).IsSupportInterface<ILotusDuplicate<TItem>>();

        /// <summary>
        /// Компаратор поддержки операций сравнения объектов в отношении равенства.
        /// </summary>
        public static readonly EqualityComparer<TItem> EqualityComparerDefault = EqualityComparer<TItem>.Default;

        /// <summary>
        /// Компаратор поддержки операций сравнения объектов при упорядочении.
        /// </summary>
        public static readonly Comparer<TItem> ComparerDefault = Comparer<TItem>.Default;
        #endregion

        #region Static fields
        protected static readonly PropertyChangedEventArgs PropertyArgsCount = new PropertyChangedEventArgs(nameof(Count));
        protected static readonly PropertyChangedEventArgs PropertyArgsIndexer = new PropertyChangedEventArgs("Item[]");
        protected static readonly PropertyChangedEventArgs PropertyArgsIsEmpty = new PropertyChangedEventArgs(nameof(IsEmpty));
        protected static readonly PropertyChangedEventArgs PropertyArgsIsFill = new PropertyChangedEventArgs(nameof(IsFill));
        protected static readonly NotifyCollectionChangedEventArgs CollectionArgsReset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        #endregion

        #region Fields
        // Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        protected internal TItem?[] _arrayOfItems;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
        protected internal int _count;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
        protected internal int _maxCount;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
        protected internal bool _isNotify;
        protected internal bool _isReadOnly;
        protected internal bool _isFixedSize;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Максимальное количество элементов.
        /// </summary>
        /// <remarks>
        /// Максимальное количество элементов на данном этапе, если текущее количество элементов будет равно максимальному,
        /// то при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество
        /// элементов увеличится в двое.
        /// Можно заранее увеличить максимальное количество элементов вызвав метод <see cref="Resize(int)"/>
        /// </remarks>
        public int MaxCount
        {
            get { return _maxCount; }
        }

        /// <summary>
        /// Статус пустой коллекции.
        /// </summary>
        public bool IsEmpty
        {
            get { return _count == 0; }
        }

        /// <summary>
        /// Статус заполненной коллекции.
        /// </summary>
        /// <remarks>
        /// Статус заполненной коллекции означает то на текущий момент текущее количество элементов равно максимальному 
        /// и при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество 
        /// элементов увеличится в двое
        /// </remarks>
        public bool IsFill
        {
            get { return _count == _maxCount; }
        }

        /// <summary>
        /// Статус включения уведомлений коллекции о своих изменениях.
        /// </summary>
        public bool IsNotify
        {
            get { return _isNotify; }
            set
            {
                _isNotify = value;
            }
        }

        /// <summary>
        /// Индекс последнего элемента.
        /// </summary>
        public int LastIndex
        {
            get { return _count - 1; }
        }

        /// <summary>
        /// Данные массива для сериализации.
        /// </summary>
        public TItem?[] SerializeItems
        {
            get
            {
                TrimExcess();
                return _arrayOfItems;
            }
            set
            {
                SetData(value!, value.Length);
            }
        }

        //
        // ДОСТУП К ЭЛЕМЕНТАМ
        //
        /// <summary>
        /// Первый элемент.
        /// </summary>
        public TItem? ItemFirst
        {
            get
            {
                return _arrayOfItems[0];
            }
            set
            {
                _arrayOfItems[0] = value;
            }
        }

        /// <summary>
        /// Второй элемент.
        /// </summary>
        public TItem? ItemSecond
        {
            get
            {
                return _arrayOfItems[0];
            }
            set
            {
                _arrayOfItems[0] = value;
            }
        }

        /// <summary>
        /// Предпоследний элемент.
        /// </summary>
        public TItem? ItemPenultimate
        {
            get
            {
                return _arrayOfItems[_count - 2];
            }
            set
            {
                _arrayOfItems[_count - 2] = value;
            }
        }

        /// <summary>
        /// Последний элемент.
        /// </summary>
        public TItem? ItemLast
        {
            get
            {
                return _arrayOfItems[_count - 1];
            }
            set
            {
                _arrayOfItems[_count - 1] = value;
            }
        }
        #endregion

        #region Properties IList
        /// <summary>
        /// Количество элементов.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Статус коллекции только для чтения.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }

        /// <summary>
        /// Статус фиксированной коллекции.
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return _isFixedSize;
            }
            set
            {
                _isFixedSize = value;
            }
        }

        /// <summary>
        /// Статус синхронизации коллекции.
        /// </summary>
        public bool IsSynchronized
        {
            get { return _arrayOfItems.IsSynchronized; }
        }

        /// <summary>
        /// Объект синхронизации.
        /// </summary>
        public object SyncRoot
        {
            get { return _arrayOfItems.SyncRoot; }
        }

        /// <summary>
        /// Индексация списка.
        /// </summary>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Элемент.</returns>
        object? IList.this[int index]
        {
            get
            {
                return _arrayOfItems[index];
            }
            set
            {
                try
                {
                    _arrayOfItems[index] = value == null ? default : (TItem)value;

                    if (_isNotify)
                    {

                    }
                }
                catch (Exception exc)
                {
#if UNITY_2017_1_OR_NEWER
						UnityEngine.Debug.LogException(exc);
#else
                    XLogger.LogException(exc);
#endif
                }

            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные списка предустановленными данными.
        /// </summary>
        public ListArray()
            : this(INIT_MAX_COUNT)
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public ListArray(int capacity)
        {
            _maxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
            _count = 0;
            _arrayOfItems = new TItem[_maxCount];
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="items">Список элементов.</param>
        public ListArray(IList<TItem?> items)
        {
            if (items != null && items.Count > 0)
            {
                _maxCount = items.Count;
                _count = items.Count;
                _arrayOfItems = new TItem[_maxCount];

                for (var i = 0; i < items.Count; i++)
                {
                    _arrayOfItems[i] = items[i];
                }
            }
            else
            {
                _maxCount = INIT_MAX_COUNT;
                _count = 0;
                _arrayOfItems = new TItem[_maxCount];
            }
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация списка.
        /// </summary>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Элемент.</returns>
        public TItem this[int index]
        {
            get { return _arrayOfItems[index]!; }
            set
            {
                if (_isNotify)
                {
                    var original_item = _arrayOfItems[index];
                    _arrayOfItems[index] = value;

                    NotifyPropertyChanged(PropertyArgsIndexer);
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, original_item, value, index);
                }
                else
                {
                    _arrayOfItems[index] = value;
                }
            }
        }
        #endregion

        #region IEnumerable methods
        /// <summary>
        /// Получение перечислителя.
        /// </summary>
        /// <returns>Перечислитель.</returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return new ListArrayEnumerator(this);
        }

        /// <summary>
        /// Получение перечислителя.
        /// </summary>
        /// <returns>Перечислитель.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListArrayEnumerator(this);
        }
        #endregion

        #region IList methods
        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="value">Элемент.</param>
        /// <returns>Количество элементов.</returns>
        public int Add(object? value)
        {
            try
            {
                Add(value == null ? default : (TItem)value);
                return _count;
            }
            catch (Exception exc)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogException(exc);
#else
                XLogger.LogException(exc);
#endif
            }

            return _count;
        }

        /// <summary>
        /// Проверка на наличие элемента в списке.
        /// </summary>
        /// <param name="value">Элемент.</param>
        /// <returns>Статус наличия элемента в списке.</returns>
        public bool Contains(object? value)
        {
            return Array.IndexOf(_arrayOfItems, value, 0, _count) > -1;
        }

        /// <summary>
        /// Получение индекса элемента в списке.
        /// </summary>
        /// <param name="value">Элемент.</param>
        /// <returns>Индекс элемента в списке.</returns>
        public int IndexOf(object? value)
        {
            return Array.IndexOf(_arrayOfItems, value, 0, _count);
        }

        /// <summary>
        /// Вставка элемента в указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="value">Элемент.</param>
        public void Insert(int index, object? value)
        {
            if (value is TItem item_type)
            {
                Insert(index, item_type);
            }
        }

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="value">Элемент.</param>
        public void Remove(object? value)
        {
            if (value is TItem item_type)
            {
                Remove(item_type);
            }
        }

        /// <summary>
        /// Копирование элементов в указанный массив.
        /// </summary>
        /// <param name="array">Целевой массив.</param>
        /// <param name="index">Индекс с которого начинается копирование.</param>
        public void CopyTo(Array array, int index)
        {
            _arrayOfItems.CopyTo(array, index);
        }
        #endregion

        #region IList<TItem> methods
        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        public void Add(TItem? item)
        {
            Add(in item);
        }

        /// <summary>
        /// Проверка на наличие элемента в списке.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус наличия элемента в списке.</returns>
        public bool Contains(TItem? item)
        {
            return Contains(in item);
        }

        /// <summary>
        /// Получение индекса элемента в списке.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Индекс элемента в списке.</returns>
        public int IndexOf(TItem? item)
        {
            return Array.IndexOf(_arrayOfItems, item, 0, _count);
        }

        /// <summary>
        /// Вставка элемента в указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="item">Элемент.</param>
        public void Insert(int index, TItem? item)
        {
            Insert(index, in item);
        }

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус успешности удаления.</returns>
        public bool Remove(TItem? item)
        {
            return Remove(in item);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости.
        /// </summary>
        /// <param name="index">Индекс элемента списка.</param>
        /// <param name="element">Элемент списка.</param>
        public void SetAt(int index, in TItem element)
        {
            if (index >= _count)
            {
                Add(element);
            }
            else
            {
                _arrayOfItems[index] = element;
            }
        }

        /// <summary>
        /// Получение элемента списка по индексу.
        /// </summary>
        /// <remarks>
        /// В случае если индекс выходит за границы списка, то возвращается последний элемент.
        /// </remarks>
        /// <param name="index">Индекс элемента списка.</param>
        /// <returns>Элемент.</returns>
        public TItem? GetAt(int index)
        {
            if (index >= _count)
            {
                if (_count == 0)
                {
                    // Создаем объект по умолчанию
                    var item = Activator.CreateInstance<TItem>();
                    Add(in item);
                    return _arrayOfItems[0];
                }
                else
                {
                    return _arrayOfItems[LastIndex];
                }
            }
            else
            {
                return _arrayOfItems[index];
            }
        }

        /// <summary>
        /// Резервирование места на определённое количество элементов с учетом существующих.
        /// </summary>
        /// <param name="count">Количество элементов.</param>
        public void Reserve(int count)
        {
            if (count <= 0) return;
            var new_count = _count + count;

            if (new_count > _maxCount)
            {
                while (_maxCount < new_count)
                {
                    _maxCount <<= 1;
                }

                var items = new TItem[_maxCount];
                Array.Copy(_arrayOfItems, items, _count);
                _arrayOfItems = items;

                // Проходим по всем объектам и если надо создаем объект
                if (IsNullable)
                {
                    for (var i = _count; i < new_count; i++)
                    {
                        if (_arrayOfItems[i] == null)
                        {
                            _arrayOfItems[i] = Activator.CreateInstance<TItem>();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Изменение максимального количества элементов которая может вместить коллекция.
        /// </summary>
        /// <param name="newMaxCount">Новое максимальное количество элементов.</param>
        public void Resize(int newMaxCount)
        {
            // Если мы увеличиваем емкость массива
            if (newMaxCount > _maxCount)
            {
                _maxCount = newMaxCount;
                var items = new TItem[_maxCount];
                Array.Copy(_arrayOfItems, items, _count);
                _arrayOfItems = items;
            }
            else
            {
                // Максимальное количество элементов меньше текущего
                // Все ссылочные элементы в данном случае нам надо удалить
                if (newMaxCount < _count)
                {
                    if (IsNullable)
                    {
                        for (var i = newMaxCount; i < _count; i++)
                        {
                            _arrayOfItems[i] = default;
                        }
                    }

                    _count = newMaxCount;
                    _maxCount = newMaxCount;
                    var items = new TItem[_maxCount];
                    Array.Copy(_arrayOfItems, items, _count);
                    _arrayOfItems = items;
                }
                else
                {
                    // Простое уменьшение размера массива
                    _maxCount = newMaxCount;
                    var items = new TItem[_maxCount];
                    Array.Copy(_arrayOfItems, items, _count);
                    _arrayOfItems = items;
                }
            }
        }

        /// <summary>
        /// Определение емкости, равную фактическому числу элементов в списке, если это число меньше порогового значения.
        /// </summary>
        public void TrimExcess()
        {
            var items = new TItem[_count];
            Array.Copy(_arrayOfItems, items, _count);
            _arrayOfItems = items;
            _maxCount = _count;
        }

        /// <summary>
        /// Копирование элементов в указанный массив.
        /// </summary>
        /// <param name="array">Целевой массив.</param>
        /// <param name="arrayIndex">Позиция начала копирования.</param>
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            Array.Copy(_arrayOfItems, 0, array, arrayIndex, _count);
        }

        /// <summary>
        /// Установка данных на прямую.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="count">Количество данных.</param>
        public void SetData(TItem[] data, int count)
        {
            _arrayOfItems = data;
            _count = count >= 0 ? count : 0;
            _maxCount = _arrayOfItems.Length;
        }

        /// <summary>
        /// Получение непосредственно данных.
        /// </summary>
        /// <returns>Данные.</returns>
        public TItem?[] GetData()
        {
            return _arrayOfItems;
        }

        /// <summary>
        /// Установка индекса для элементов.
        /// </summary>
        /// <remarks>
        /// Индексы присваиваются согласно порядковому номеру элемента.
        /// </remarks>
        public void SetIndexElement()
        {
            if (IsIndexable)
            {
                for (var i = 0; i < _count; i++)
                {
                    ((ILotusIndexable)_arrayOfItems[i]!).Index = i;
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод для нотификации о переустановке коллекции.
        /// </summary>
        public void NotifyCollectionReset()
        {
            PropertyChanged?.Invoke(this, PropertyArgsCount);
            PropertyChanged?.Invoke(this, PropertyArgsIndexer);
            CollectionChanged?.Invoke(this, CollectionArgsReset);
        }

        /// <summary>
        /// Вспомогательный метод для нотификации об очистки коллекции.
        /// </summary>
        public void NotifyCollectionClear()
        {
            PropertyChanged?.Invoke(this, PropertyArgsCount);
            PropertyChanged?.Invoke(this, PropertyArgsIndexer);
            CollectionChanged?.Invoke(this, CollectionArgsReset);
        }
        #endregion

        #region Add methods
        /// <summary>
        /// Добавление элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        public void Add(in TItem? item)
        {
            // Если текущие количество элементов равно максимально возможному
            if (_count == _maxCount)
            {
                _maxCount *= 2;
                var items = new TItem[_maxCount];
                Array.Copy(_arrayOfItems, items, _count);
                _arrayOfItems = items;
            }

            _arrayOfItems[_count] = item;
            _count++;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, _count - 1);
            }
        }

        /// <summary>
        /// Дублирование последнего элемента и его добавление.
        /// </summary>
        public void AddDuplicateLastItem()
        {
            if (_count > 0)
            {
                // Если текущие количество элементов равно максимально возможному
                if (_count == _maxCount)
                {
                    _maxCount *= 2;
                    var items = new TItem[_maxCount];
                    Array.Copy(_arrayOfItems, items, _count);
                    _arrayOfItems = items;
                }

                var item = default(TItem);
                if (IsDuplicatable)
                {
                    item = (ItemLast as ILotusDuplicate<TItem>)!.Duplicate();
                }
                else
                {
                    if (ItemLast is ICloneable)
                    {
                        item = (TItem)(ItemLast as ICloneable)!.Clone();
                    }
                    else
                    {
                        if (IsNullable)
                        {

                        }
                        else
                        {
                            item = ItemLast;
                        }
                    }
                }

                _arrayOfItems[_count] = item;
                _count++;

                if (_isNotify)
                {
                    PropertyChanged?.Invoke(this, PropertyArgsCount);
                    PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, _count - 1);
                }
            }
        }

        /// <summary>
        /// Добавление элементов.
        /// </summary>
        /// <param name="items">Элементы.</param>
        public void AddItems(params TItem[] items)
        {
            Reserve(items.Length);
            Array.Copy(items, 0, _arrayOfItems, _count, items.Length);
            _count += items.Length;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                var original_count = _count - items.Length;
                for (var i = 0; i < items.Length; i++)
                {
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
                }
            }
        }

        /// <summary>
        /// Добавление элементов.
        /// </summary>
        /// <param name="items">Элементы.</param>
        public void AddItems(IList<TItem> items)
        {
            Reserve(items.Count);
            for (var i = 0; i < items.Count; i++)
            {
                _arrayOfItems[i + _count] = items[i];
            }
            _count += items.Count;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                var original_count = _count - items.Count;
                for (var i = 0; i < items.Count; i++)
                {
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
                }
            }
        }
        #endregion

        #region Insert methods
        /// <summary>
        /// Вставка элемента в указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="item">Элемент.</param>
        public void Insert(int index, in TItem? item)
        {
            if (index >= _count)
            {
                Add(item);
                return;
            }

            // Если текущие количество элементов равно максимально возможному
            if (_count == _maxCount)
            {
                _maxCount *= 2;
                var items = new TItem[_maxCount];
                Array.Copy(_arrayOfItems, items, _count);
                _arrayOfItems = items;
            }

            Array.Copy(_arrayOfItems, index, _arrayOfItems, index + 1, _count - index);
            _arrayOfItems[index] = item;
            _count++;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            }
        }

        /// <summary>
        /// Вставка элемента после указанного элемента.
        /// </summary>
        /// <param name="original">Элемент после которого будет произведена вставка.</param>
        /// <param name="item">Элемент.</param>
        public void InsertAfter(in TItem original, in TItem item)
        {
            var index = Array.IndexOf(_arrayOfItems, original);
            Insert(index + 1, item);
        }

        /// <summary>
        /// Вставка элемента перед указанным элементом.
        /// </summary>
        /// <param name="original">Элемент перед которым будет произведена вставка.</param>
        /// <param name="item">Элемент.</param>
        public void InsertBefore(in TItem original, in TItem item)
        {
            var index = Array.IndexOf(_arrayOfItems, original);
            Insert(index, item);
        }

        /// <summary>
        /// Вставка элементов в указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="items">Элементы.</param>
        public void InsertItems(int index, params TItem[] items)
        {
            if (index >= _count)
            {
                AddItems(items);
                return;
            }

            Reserve(items.Length);
            Array.Copy(_arrayOfItems, index, _arrayOfItems, index + items.Length, _count - index);
            Array.Copy(items, 0, _arrayOfItems, index, items.Length);
            _count += items.Length;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                for (var i = 0; i < items.Length; i++)
                {
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
                }
            }
        }

        /// <summary>
        /// Вставка элементов в указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки.</param>
        /// <param name="items">Элементы.</param>
        public void InsertItems(int index, IList<TItem> items)
        {
            if (index >= _count)
            {
                AddItems(items);
                return;
            }

            Reserve(items.Count);
            Array.Copy(_arrayOfItems, index, _arrayOfItems, index + items.Count, _count - index);
            for (var i = 0; i < items.Count; i++)
            {
                _arrayOfItems[i + index] = items[i];
            }
            _count += items.Count;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                for (var i = 0; i < items.Count; i++)
                {
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
                }
            }
        }
        #endregion

        #region Remove methods
        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус успешности удаления.</returns>
        public bool Remove(in TItem? item)
        {
            var index = Array.IndexOf(_arrayOfItems, item, 0, _count);
            if (index != -1)
            {
                _count--;
                Array.Copy(_arrayOfItems, index + 1, _arrayOfItems, index, _count - index);
                _arrayOfItems[_count] = default;

                if (_isNotify)
                {
                    PropertyChanged?.Invoke(this, PropertyArgsCount);
                    PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                    NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
                }


                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаление элементов.
        /// </summary>
        /// <param name="items">Элементы.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int RemoveItems(params TItem[] items)
        {
            var count = 0;
            for (var i = 0; i < items.Length; i++)
            {
                var index = Array.IndexOf(_arrayOfItems, items[i], 0, _count);
                if (index != -1)
                {
                    _count--;
                    Array.Copy(_arrayOfItems, index + 1, _arrayOfItems, index, _count - index);
                    _arrayOfItems[_count] = default;
                    count++;

                    if (_isNotify)
                    {
                        PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                        NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Удаление элементов.
        /// </summary>
        /// <param name="items">Элементы.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int RemoveItems(IList<TItem> items)
        {
            var count = 0;
            for (var i = 0; i < items.Count; i++)
            {
                var index = Array.IndexOf(_arrayOfItems, items[i], 0, _count);
                if (index != -1)
                {
                    _count--;
                    Array.Copy(_arrayOfItems, index + 1, _arrayOfItems, index, _count - index);
                    _arrayOfItems[_count] = default;
                    count++;

                    if (_isNotify)
                    {
                        PropertyChanged?.Invoke(this, PropertyArgsCount);
                        PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                        NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Удаление элементов с указанного индекса и до конца коллекции.
        /// </summary>
        /// <param name="index">Индекс начала удаления элементов.</param>
        public void RemoveItemsEnd(int index)
        {
            var count = _count - index;
            RemoveRange(index, count);
        }

        /// <summary>
        /// Удаление элементов с определенного диапазона.
        /// </summary>
        /// <param name="index">Индекс начала удаления элементов.</param>
        /// <param name="count">Количество удаляемых элементов.</param>
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					index = 0;
#else
                XLogger.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
                index = 0;
#endif
            }

            if (count < 1)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Count is less than one: <{0}> (Return)", count);
					count = 1;
#else
                XLogger.LogErrorFormat("Count is less than one: <{0}> (Return)", count);
                count = 1;
#endif
            }

            if (_count - index < count)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("The index <{0}> + count <{1}> is greater than the number of elements: <2> (Return)", index, count, _count);
					return;
#else
                XLogger.LogErrorFormat("The index <{0}> + count <{1}> is greater than the number of elements: <2> (Return)", index, count, _count);
                return;
#endif
            }

            if (count > 0)
            {
                var i = _count;
                _count -= count;

                if (index < _count)
                {
                    Array.Copy(_arrayOfItems, index + count, _arrayOfItems, index, _count - index);
                }

                Array.Clear(_arrayOfItems, _count, count);

                if (_isNotify)
                {
                    PropertyChanged?.Invoke(this, PropertyArgsCount);
                    PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                    NotifyCollectionReset();
                }
            }
        }

        /// <summary>
        /// Удаление элемента.
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента.</param>
        public void RemoveAt(int index)
        {
            if (index < 0)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					return;
#else
                XLogger.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
                return;
#endif
            }

            if (index > LastIndex)
            {
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("The index is greater than the number of elements: <{0}> (Return)", index);
					return;
#else
                XLogger.LogErrorFormat("The index is greater than the number of elements: <{0}> (Return)", index);
                return;
#endif
            }

            if (_isNotify)
            {
                var temp = _arrayOfItems[index];

                _count--;
                Array.Copy(_arrayOfItems, index + 1, _arrayOfItems, index, _count - index);
                _arrayOfItems[_count] = default;

                PropertyChanged?.Invoke(this, PropertyArgsCount);
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, temp, index);
            }
            else
            {
                _count--;
                Array.Copy(_arrayOfItems, index + 1, _arrayOfItems, index, _count - index);
                _arrayOfItems[_count] = default;
            }
        }

        /// <summary>
        /// Удаление первого элемента.
        /// </summary>
        public void RemoveFirst()
        {
            RemoveAt(0);
        }

        /// <summary>
        /// Удаление последнего элемента.
        /// </summary>
        public void RemoveLast()
        {
            if (_count > 0)
            {
                RemoveAt(_count - 1);
            }
        }

        /// <summary>
        /// Удаление дубликатов элементов.
        /// </summary>
        /// <returns>Количество дубликатов элементов.</returns>
        public int RemoveDuplicates()
        {
            var unique = new TItem?[_count];
            var count = 0;
            for (var i = 0; i < _count; i++)
            {
                var item = _arrayOfItems[i];
                var index = Array.IndexOf(unique, item);
                if (index == -1)
                {
                    unique[count] = item;
                    count++;
                }
            }

            // У нас есть дубликаты
            if (count < _count)
            {
                Array.Copy(unique, 0, _arrayOfItems, 0, count);

                for (var i = count; i < _count; i++)
                {
                    _arrayOfItems[i] = default;
                }

                var delta = _count - count;
                _count = count;

                if (_isNotify)
                {
                    NotifyCollectionReset();
                }

                return delta;
            }

            return 0;
        }

        /// <summary>
        /// Удаление всех элементов, удовлетворяющих условиям указанного предиката.
        /// </summary>
        /// <param name="match">Предикат.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int RemoveAll(Predicate<TItem?> match)
        {
            var free_index = 0;   // the first free slot in items array

            // Find the first item which needs to be removed.
            while (free_index < _count && !match(_arrayOfItems[free_index])) free_index++;
            if (free_index >= _count) return 0;

            var current = free_index + 1;
            while (current < _count)
            {
                // Find the first item which needs to be kept.
                while (current < _count && match(_arrayOfItems[current])) current++;

                if (current < _count)
                {
                    // copy item to the free slot.
                    _arrayOfItems[free_index++] = _arrayOfItems[current++];
                }
            }

            Array.Clear(_arrayOfItems, free_index, _count - free_index);
            var result = _count - free_index;
            _count = free_index;

            if (_isNotify)
            {
                NotifyCollectionReset();
            }

            return result;
        }

        /// <summary>
        /// Обрезать список сначала до указанного элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <param name="included">Включать указанный элемент в удаление.</param>
        /// <returns>Количество удаленных элементов, -1 если элемент не найден.</returns>
        public int TrimStart(in TItem item, bool included = true)
        {
            var index = Array.IndexOf(_arrayOfItems, item);
            if (index > -1)
            {
                if (index == 0)
                {
                    if (included)
                    {
                        // Удаляем первый элемент
                        RemoveFirst();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (index == LastIndex)
                    {
                        // Удаляем все элементы
                        if (included)
                        {
                            var count = _count;
                            Clear();
                            return count;
                        }
                        else
                        {
                            // Удаляем элементы до последнего
                            var count = _count - 1;
                            RemoveRange(0, count);
                            return count;
                        }
                    }
                    else
                    {
                        if (included)
                        {
                            RemoveRange(0, index + 1);
                            return index + 1;
                        }
                        else
                        {
                            RemoveRange(0, index);
                            return index;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Обрезать список с конца до указанного элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <param name="included">Включать указанный элемент в удаление.</param>
        /// <returns>Количество удаленных элементов, -1 если элемент не найден.</returns>
        public int TrimEnd(in TItem item, bool included = true)
        {
            var index = Array.LastIndexOf(_arrayOfItems, item);
            if (index > -1)
            {
                if (index == 0)
                {
                    // Удаляем все элементы
                    if (included)
                    {
                        var count = _count;
                        Clear();
                        return count;
                    }
                    else
                    {
                        // Удаляем элементы до первого
                        var count = _count - 1;
                        RemoveRange(1, count);
                        return count;
                    }
                }
                else
                {
                    if (index == LastIndex)
                    {
                        // Удаляем последний элемент
                        if (included)
                        {
                            RemoveLast();
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        if (included)
                        {
                            var count = _count - index;
                            RemoveRange(index, count);
                            return count;
                        }
                        else
                        {
                            var count = _count - index - 1;
                            RemoveRange(index + 1, count);
                            return count;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Очистка списка.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_arrayOfItems, 0, _count);
            _count = 0;

            if (_isNotify)
            {
                NotifyCollectionClear();
            }
        }
        #endregion

        #region Sets methods 
        /// <summary>
        /// Присвоение новых элементов списку.
        /// </summary>
        /// <param name="items">Элементы.</param>
        public void AssignItems(params TItem[] items)
        {
            Reserve(items.Length - _count);
            Array.Copy(items, 0, _arrayOfItems, 0, items.Length);
            _count = items.Length;

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Присвоение новых элементов списку.
        /// </summary>
        /// <param name="items">Элементы.</param>
        public void AssignItems(IList<TItem> items)
        {
            Reserve(items.Count - _count);
            for (var i = 0; i < items.Count; i++)
            {
                _arrayOfItems[i] = items[i];
            }
            _count = items.Count;

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Объединение элементов и существующих элементов списка.
        /// </summary>
        /// <remarks>
        /// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке.
        /// </remarks>
        /// <param name="items">Элементы.</param>
        public void UnionItems(params TItem[] items)
        {
            Reserve(items.Length);
            for (var i = 0; i < items.Length; i++)
            {
                if (Array.IndexOf(_arrayOfItems, items[i]) == -1)
                {
                    Add(items[i]);
                }
            }
            _count = items.Length;
        }

        /// <summary>
        /// Объединение элементов и существующих элементов списка.
        /// </summary>
        /// <remarks>
        /// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке.
        /// </remarks>
        /// <param name="items">Элементы.</param>
        public void UnionItems(IList<TItem> items)
        {
            Reserve(items.Count);
            for (var i = 0; i < items.Count; i++)
            {
                if (Array.IndexOf(_arrayOfItems, items[i]) == -1)
                {
                    Add(items[i]);
                }
            }
            _count = items.Count;
        }

        /// <summary>
        /// Разница между элементами списка и элементами указанного списка.
        /// </summary>
        /// <remarks>
        /// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
        /// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
        /// переданному списку
        /// </remarks>
        /// <param name="items">Элементы.</param>
        /// <returns>Список элементов списка.</returns>
        public ListArray<TItem> DifferenceItems(IList<TItem?> items)
        {
            var difference = new ListArray<TItem>();

            for (var i = 0; i < _count; i++)
            {
                if (items.Contains(_arrayOfItems[i]) == false)
                {
                    difference.Add(_arrayOfItems[i]);
                }
            }

            return difference;
        }

        /// <summary>
        /// Разница между элементами списка и элементами указанного списка.
        /// </summary>
        /// <remarks>
        /// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
        /// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
        /// переданному списку
        /// </remarks>
        /// <example>
        /// Исходный список
        /// [0, 2, 4, 7, 5]
        /// Переданный список
        /// [0, 4, 7, 12, 15, 7]
        /// Результат
        /// [2, 5]
        /// </example>
        /// <param name="items">Элементы.</param>
        /// <returns>Список элементов списка.</returns>
        public ListArray<TItem> DifferenceItems(params TItem?[] items)
        {
            var difference = new ListArray<TItem>();

            for (var i = 0; i < _count; i++)
            {
                if (items.ContainsElement(_arrayOfItems[i]) == false)
                {
                    difference.Add(_arrayOfItems[i]);
                }
            }

            return difference;
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Проверка на наличие элемента в списке.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус наличия.</returns>
        public bool Contains(in TItem? item)
        {
            return Array.IndexOf(_arrayOfItems, item, 0, _count) != -1;
        }

        /// <summary>
        /// Поиск с начала списка индекса указанного элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Индекс элемента.</returns>
        public int IndexOf(in TItem? item)
        {
            return Array.IndexOf(_arrayOfItems, item, 0, _count);
        }

        /// <summary>
        /// Поиск с конца списка индекса указанного элемента.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Индекс элемента.</returns>
        public int LastIndexOf(in TItem? item)
        {
            return Array.LastIndexOf(_arrayOfItems, item, 0, _count);
        }

        /// <summary>
        /// Поиск с начала списка индекса элемента удовлетворяющему указанному предикату.
        /// </summary>
        /// <param name="match">Предикат.</param>
        /// <returns>Индекс элемента.</returns>
        public int Find(Predicate<TItem?> match)
        {
            for (var i = 0; i < _count; i++)
            {
                if (match(_arrayOfItems[i])) { return i; }
            }

            return -1;
        }

        /// <summary>
        /// Поиск с конца списка индекса элемента удовлетворяющему указанному предикату.
        /// </summary>
        /// <param name="match">Предикат.</param>
        /// <returns>Индекс элемента.</returns>
        public int FindLast(Predicate<TItem?> match)
        {
            for (var i = LastIndex; i >= 0; i--)
            {
                if (match(_arrayOfItems[i])) { return i; }
            }

            return -1;
        }

        /// <summary>
        /// Поиск с начала списка элемента удовлетворяющему указанному предикату.
        /// </summary>
        /// <param name="match">Предикат.</param>
        /// <returns>Элемент или значение по умолчанию.</returns>
        public TItem? Search(Predicate<TItem?> match)
        {
            for (var i = 0; i < _count; i++)
            {
                if (match(_arrayOfItems[i])) { return _arrayOfItems[i]; }
            }

            return default;
        }

        /// <summary>
        /// Поиск с конца списка элемента удовлетворяющему указанному предикату.
        /// </summary>
        /// <param name="match">Предикат.</param>
        /// <returns>Элемент или значение по умолчанию.</returns>
        public TItem? SearchLast(Predicate<TItem?> match)
        {
            for (var i = LastIndex; i >= 0; i--)
            {
                if (match(_arrayOfItems[i])) { return _arrayOfItems[i]; }
            }

            return default;
        }
        #endregion

        #region Operation methods
        /// <summary>
        /// Проверка элементов списка на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Список удовлетворяет условию предиката если каждый его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool CheckAll(Predicate<TItem?> match)
        {
            var result = true;
            for (var i = 0; i < _count; i++)
            {
                if (match(_arrayOfItems[i]) == false)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Проверка элементов списка на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Список удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        public virtual bool CheckOne(Predicate<TItem?> match)
        {
            var result = false;
            for (var i = 0; i < _count; i++)
            {
                if (match(_arrayOfItems[i]))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Посещение элементов списка указанным посетителем.
        /// </summary>
        /// <param name="onVisitor">Делегат посетителя.</param>
        public virtual void Visit(Action<TItem?> onVisitor)
        {
            for (var i = 0; i < _count; i++)
            {
                onVisitor(_arrayOfItems[i]);
            }
        }
        #endregion

        #region Modify methods
        /// <summary>
        /// Обмен местами элементов списка.
        /// </summary>
        /// <param name="oldIndex">Старая позиция.</param>
        /// <param name="newIndex">Новая позиция.</param>
        public void Swap(int oldIndex, int newIndex)
        {
            var temp = _arrayOfItems[oldIndex];
            _arrayOfItems[oldIndex] = _arrayOfItems[newIndex];
            _arrayOfItems[newIndex] = temp;

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, newIndex, oldIndex);
            }
        }

        /// <summary>
        /// Перемещение элемента в новую позицию.
        /// </summary>
        /// <param name="oldIndex">Старая позиция.</param>
        /// <param name="newIndex">Новая позиция.</param>
        public void Move(int oldIndex, int newIndex)
        {
            var temp = _arrayOfItems[oldIndex];
            RemoveAt(oldIndex);
            Insert(newIndex, temp);

            if (_isNotify)
            {
                PropertyChanged?.Invoke(this, PropertyArgsIndexer);
                NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, newIndex, oldIndex);
            }
        }

        /// <summary>
        /// Перемещение элемента списка вниз.
        /// </summary>
        /// <remarks>
        /// При перемещении вниз индекс элемент увеличивается.
        /// </remarks>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public void MoveDown(int elementIndex)
        {
            var next = (elementIndex + 1) % _count;
            Swap(elementIndex, next);
        }

        /// <summary>
        /// Перемещение элемента списка вверх.
        /// </summary>
        /// <remarks>
        /// При перемещении вниз индекс элемент уменьшается.
        /// </remarks>
        /// <param name="elementIndex">Индекс перемещаемого элемента.</param>
        public void MoveUp(int elementIndex)
        {
            var previous = elementIndex - 1;
            if (previous < 0) previous = LastIndex;
            Swap(elementIndex, previous);
        }

        /// <summary>
        /// Циклическое смещение элементов списка вниз.
        /// </summary>
        public void ShiftDown()
        {
            XCollectionsExtension.Shift(this, true);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Циклическое смещение элементов списка вверх.
        /// </summary>
        public void ShiftUp()
        {
            XCollectionsExtension.Shift(this, false);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Перетасовка элементов списка.
        /// </summary>
        public void Shuffle()
        {
            XCollectionsExtension.Shuffle(this);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Сортировка списка по возрастанию.
        /// </summary>
        public virtual void SortAscending()
        {
            Array.Sort(_arrayOfItems, 0, _count);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Сортировка списка по убыванию.
        /// </summary>
        public virtual void SortDescending()
        {
            Array.Sort(_arrayOfItems, 0, _count);
            Array.Reverse(_arrayOfItems, 0, _count);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Сортировка элементов списка посредством делегата.
        /// </summary>
        /// <param name="comparer">Делегат сравнивающий два объекта одного типа.</param>
        public virtual void Sort(IComparer<TItem> comparer)
        {
            Array.Sort(_arrayOfItems!, 0, _count, comparer);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }

        /// <summary>
        /// Сортировка элементов списка посредством делегата.
        /// </summary>
        /// <remarks>
        /// Внимание, методом надо пользоваться с осторожностью так он учитывает реальный размер массива.
        /// </remarks>
        /// <param name="comparison">Делегат сравнивающий два объекта одного типа.</param>
        public virtual void Sort(Comparison<TItem> comparison)
        {
            IComparer<TItem> comparer = new FunctorComparer(comparison);

            Array.Sort(_arrayOfItems!, 0, _count, comparer);

            if (_isNotify)
            {
                NotifyCollectionReset();
            }
        }
        #endregion

        #region Closest methods
        /// <summary>
        /// Поиск ближайшего индекса элемента по его значению, не больше указанного значения.
        /// </summary>
        /// <remarks>
        /// Применяется только для списков, отсортированных по возрастанию.
        /// </remarks>
        /// <example>
        /// [2, 4, 10, 15] -> ищем элемент со значением 1, вернет индекс 0
        /// [2, 4, 10, 15] -> ищем элемент со значением 2, вернет индекс 0
        /// [2, 4, 10, 15] -> ищем элемент со значением 3, вернет индекс 0
        /// [2, 4, 10, 15] -> ищем элемент со значением 4, вернет индекс 1
        /// [2, 4, 10, 15] -> ищем элемент со значением 9, вернет индекс 1
        /// [2, 4, 10, 15] -> ищем элемент со значением 20, вернет индекс 3
        /// </example>
        /// <param name="item">Элемент.</param>
        /// <returns>Ближайший индекс элемента.</returns>
        public int GetClosestIndex(in TItem item)
        {
            // Если элемент равен или меньше первого возвращаем нулевой индекс
            if (ComparerDefault.Compare(item, ItemFirst) <= 0)
            {
                return 0;
            }

            // Если элемент равен или больше последнего возвращаем последний индекс
            if (ComparerDefault.Compare(item, ItemLast) >= 0)
            {
                return LastIndex;
            }

            for (var i = 1; i < _count; i++)
            {
                // Получаем статус сравнения
                var status = ComparerDefault.Compare(item, _arrayOfItems[i]);

                // Если элемент равен возвращаем данный индекс
                if (status == 0)
                {
                    return i;
                }
                else
                {
                    // Если он меньше предыдущего то возвращаем предыдущий индекс
                    if (status == -1)
                    {
                        return i - 1;
                    }
                }
            }

            return LastIndex;
        }

        /// <summary>
        /// Обрезать список сначала до ближайшего указанного элемента.
        /// </summary>
        /// <remarks>
        /// Применяется только для списков, отсортированных по возрастанию.
        /// Включаемый элемент будет удален если он совпадает
        /// </remarks>
        /// <param name="item">Элемент.</param>
        /// <param name="included">Включать указанный элемент в удаление.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int TrimClosestStart(in TItem item, bool included = true)
        {
            var comprare_first = ComparerDefault.Compare(item, ItemFirst);
            var comprare_last = ComparerDefault.Compare(item, ItemLast);

            // Элемент находиться за пределами списка - в начале
            if (comprare_first <= 0)
            {
                // Удаляем первый элемент если он включен и равен
                if (comprare_first == 0 && included)
                {
                    RemoveFirst();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            // Элемент находиться за пределами списка - в конце 
            if (comprare_last >= 0)
            {
                // Если он совпал последним элементом и при этом его не надо удалять
                if (comprare_last == 0 && included == false)
                {
                    // Удаляем элементы до последнего
                    var count = Count - 1;
                    RemoveRange(0, count);
                    return count;
                }
                else
                {
                    // Удаляем все элементы
                    var count = _count;
                    Clear();
                    return count;
                }
            }

            // Элемент находиться в пределах списка
            var max_count = Count - 1;
            for (var i = 1; i < max_count; i++)
            {
                var status = ComparerDefault.Compare(item, _arrayOfItems[i]);

                // Элемент полностью совпал
                if (status == 0)
                {
                    if (included)
                    {
                        RemoveRange(0, i + 1);
                        return i + 1;
                    }
                    else
                    {
                        RemoveRange(0, i);
                        return i;
                    }
                }
                else
                {
                    // Только если он меньше
                    if (status == -1)
                    {
                        RemoveRange(0, i);
                        return i;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Обрезать список с конца до ближайшего указанного элемента.
        /// </summary>
        /// <remarks>
        /// Применяется только для списков, отсортированных по возрастанию.
        /// Включаемый элемент будет удален если он совпадает
        /// </remarks>
        /// <param name="item">Элемент.</param>
        /// <param name="included">Включать указанный элемент в удаление.</param>
        /// <returns>Количество удаленных элементов.</returns>
        public int TrimClosestEnd(in TItem item, bool included = true)
        {
            var comprare_first = ComparerDefault.Compare(item, ItemFirst);
            var comprare_last = ComparerDefault.Compare(item, ItemLast);

            // Элемент находиться за пределами списка - в начале
            if (comprare_first <= 0)
            {
                // Удаляем до первого элементы если он выключен и совпадает
                if (comprare_first == 0 && included == false)
                {
                    var count = Count - 1;
                    RemoveRange(1, count);
                    return count;
                }
                else
                {
                    // Удаляем все элементы
                    var count = Count;
                    Clear();
                    return count;
                }
            }

            // Элемент находиться за пределами списка - в конце 
            if (comprare_last >= 0)
            {
                // Если он совпал последним элементом и при этом его надо удалять
                if (comprare_last == 0 && included)
                {
                    // Удаляем последний элемент
                    RemoveLast();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            // Элемент находиться в пределах списка
            for (var i = Count - 1; i >= 1; i--)
            {
                var status = ComparerDefault.Compare(item, _arrayOfItems[i]);

                // Элемент полностью совпал
                if (status == 0)
                {
                    if (included)
                    {
                        var count = _count;
                        RemoveItemsEnd(i);
                        return count - i;
                    }
                    else
                    {
                        var count = _count;
                        RemoveItemsEnd(i + 1);
                        return count - i - 1;
                    }
                }
                else
                {
                    // Только если он больше
                    if (status == 1)
                    {
                        var count = _count;
                        RemoveItemsEnd(i + 1);
                        return count - i - 1;
                    }
                }
            }

            return 0;
        }
        #endregion

        #region Grouping methods
        /// <summary>
        /// Получение списка групп элементов сгруппированных по указанному свойству.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Список групп элементов.</returns>
        public virtual ListArray<ListArray<TItem>> GetGroupedByProperty(string propertyName)
        {
            // Список групп
            var groups = new ListArray<ListArray<TItem>>();

            // Cписок уникальных значений
            var unique_list = new ListArray<object>();

            if (IsNullable)
            {
                // Будем получать свойство для каждого элемента так как возможно наследование
                for (var i = 0; i < _count; i++)
                {
                    // Получем свойство
                    var property_info = _arrayOfItems[i]?.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                    if (property_info != null)
                    {
                        // Ищем совпадение в существующем списке
                        var find_index = unique_list.Find((object? value) =>
                        {
                            var findValue = property_info.GetValue(_arrayOfItems[i], null);
                            return Object.ReferenceEquals(value, findValue);
                        });

                        // Совпадение не нашли значит это уникальный элемент
                        if (find_index == -1)
                        {
                            // Добавляем в уникальные значений
                            unique_list.Add(property_info.GetValue(_arrayOfItems[i], null));

                            // Создаем группу
                            var group = new ListArray<TItem>();

                            // Добавляем туда данный элемент
                            group.Add(_arrayOfItems[i]);

                            // Добавляем саму группу
                            groups.Add(group);
                        }
                        else
                        {
                            // Это не уникальный элемент, а значит группа такая уже есть
                            // Получаем группу
                            var group = groups[find_index];

                            // Добавляем туда данный элемент
                            group.Add(_arrayOfItems[i]);
                        }
                    }
                }
            }
            else
            {
                // Получаем свойство 1 раз
                var property_info = typeof(TItem).GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property_info != null)
                {
                    for (var i = 0; i < _count; i++)
                    {
                        // Ищем совпадение в существующем списке
                        var find_index = unique_list.Find((object? value) =>
                        {
                            var findValue = property_info.GetValue(_arrayOfItems[i], null);
                            return value!.Equals(findValue);
                        });

                        // Совпадение не нашли значит это уникальный элемент
                        if (find_index == -1)
                        {
                            // Создаем группу
                            var group = new ListArray<TItem>();

                            // Добавляем туда данный элемент
                            group.Add(_arrayOfItems[i]);

                            // Добавляем саму группу
                            groups.Add(group);
                        }
                        else
                        {
                            // Это не уникальный элемент, а значит группа такая уже есть
                            // Получаем группу
                            var group = groups[find_index];

                            // Добавляем туда данный элемент
                            group.Add(_arrayOfItems[i]);
                        }

                    }
                }
            }

            return groups;
        }
        #endregion

        #region Get collection methods
        /// <summary>
        /// Получение дубликата коллекции.
        /// </summary>
        /// <returns>Дубликат коллекции.</returns>
        public ListArray<TItem> GetItemsDuplicate()
        {
            var items = new ListArray<TItem>(MaxCount);
            Array.Copy(_arrayOfItems, 0, items._arrayOfItems, 0, _count);
            items._count = Count;
            return items;
        }

        /// <summary>
        /// Получение различающихся (уникальных) элементов списка.
        /// </summary>
        /// <returns>Список различающихся(уникальных) элементов списка.</returns>
        public ListArray<TItem> GetItemsDistinct()
        {
            var unique_list = new ListArray<TItem>();

            if (IsNullable)
            {
                for (var i = 0; i < _count; i++)
                {
                    // Ищем совпадение в существующем списке
                    var find_index = unique_list.Find((TItem? value) =>
                    {
                        return Object.ReferenceEquals(value, _arrayOfItems[i]);
                    });

                    // Совпадение не нашли значит это уникальный элемент
                    if (find_index == -1)
                    {
                        // Добавляем в уникальные значений
                        unique_list.Add(_arrayOfItems[i]);
                    }
                }
            }
            else
            {
                for (var i = 0; i < _count; i++)
                {
                    // Ищем совпадение в существующем списке
                    var find_index = unique_list.Find((TItem? value) =>
                    {
                        return value!.Equals(_arrayOfItems[i]);
                    });

                    // Совпадение не нашли значит это уникальный элемент
                    if (find_index == -1)
                    {
                        // Добавляем в уникальные значений
                        unique_list.Add(_arrayOfItems[i]);
                    }
                }
            }

            return unique_list;
        }

        /// <summary>
        /// Получение списка уникальных значений указанного свойства.
        /// </summary>
        /// <remarks>
        /// Используется рефлексия.
        /// </remarks>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Список уникальных свойств.</returns>
        public ListArray<object> GetDistinctValueFromPropertyName(string propertyName)
        {
            var unique_list = new ListArray<object>();

            if (IsNullable)
            {
                // Будем получать свойство и каждого элемента так как возможно наследование
                for (var i = 0; i < _count; i++)
                {
                    // Получем свойство
                    var property_info = _arrayOfItems[i]?.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                    if (property_info != null)
                    {
                        // Ищем совпадение в существующем списке
                        var find_index = unique_list.Find((object? value) =>
                        {
                            var findValue = property_info.GetValue(_arrayOfItems[i], null);
                            return Object.ReferenceEquals(value, findValue);
                        });

                        // Совпадение не нашли значит это уникальный элемент
                        if (find_index == -1)
                        {
                            // Добавляем в уникальные значений
                            unique_list.Add(property_info.GetValue(_arrayOfItems[i], null));
                        }
                    }
                }
            }
            else
            {
                // Получаем свойство 1 раз
                var property_info = typeof(TItem).GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property_info != null)
                {
                    for (var i = 0; i < _count; i++)
                    {
                        // Ищем совпадение в существующем списке
                        var find_index = unique_list.Find((object? value) =>
                        {
                            var findValue = property_info.GetValue(_arrayOfItems[i], null);
                            return value!.Equals(findValue);
                        });

                        // Совпадение не нашли значит это уникальный элемент
                        if (find_index == -1)
                        {
                            // Добавляем в уникальные значений
                            unique_list.Add(property_info.GetValue(_arrayOfItems[i], null));
                        }

                    }
                }
            }

            return unique_list;
        }

        /// <summary>
        /// Получение списка элементов по указанному предикату.
        /// </summary>
        /// <param name="match">Предикатор.</param>
        /// <returns>Список элементов.</returns>
        public ListArray<TItem> GetItemsWhere(Predicate<TItem?> match)
        {
            var result = new ListArray<TItem>(_maxCount);

            for (var i = 0; i < _count; i++)
            {
                if (match(_arrayOfItems[i]))
                {
                    result.Add(_arrayOfItems[i]);
                }
            }

            return result;
        }
        #endregion

        #region Interface INotifyPropertyChanged
        /// <summary>
        /// Событие срабатывает ПОСЛЕ изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений свойства.
        /// </summary>
        /// <param name="args">Аргументы события.</param>
        public void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion

        #region Interface INotifyCollectionChanged
        /// <summary>
        /// Событие для информирование событий коллекций.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Вспомогательный метод для нотификации изменений коллекции.
        /// </summary>
        /// <param name="action">Действие с коллекцией.</param>
        /// <param name="item">Элемент коллекции.</param>
        /// <param name="index">Индекс элемента.</param>
        protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, object? item, int index)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index));
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений коллекции.
        /// </summary>
        /// <param name="action">Действие с коллекцией.</param>
        /// <param name="item">Элемент коллекции.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <param name="oldIndex">Предыдущий индекс элемента.</param>
        protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, object? item, int index, int oldIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        /// <summary>
        /// Вспомогательный метод для нотификации изменений коллекции.
        /// </summary>
        /// <param name="action">Действие с коллекцией.</param>
        /// <param name="oldItem">Элемент коллекции.</param>
        /// <param name="newItem">Элемент коллекции.</param>
        /// <param name="index">Индекс элемента.</param>
        protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, object? oldItem, object? newItem, int index)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }
        #endregion
    }
    /**@}*/
}