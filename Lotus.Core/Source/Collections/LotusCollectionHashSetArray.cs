using System;
using System.Collections;
using System.Collections.Generic;

namespace Lotus.Core
{
    /** \addtogroup CoreCollections
	*@{*/
    /// <summary>
    /// HashSetArray на основе массива.
    /// </summary>
    /// <typeparam name="TItem">Тип элемента.</typeparam>
    [Serializable]
    public class HashSetArray<TItem> : ISet<TItem>, IReadOnlyCollection<TItem>
    {
        #region Inner types
        /// <summary>
        /// Тип реализующий перечислителя по списку.
        /// </summary>
        public struct HashSetArrayEnumerator : IEnumerator<TItem>
        {
            #region Fields
            private HashSetArray<TItem> _set;
            private int _index;
            private int _version;
            private TItem? _current;
            #endregion

            #region Properties
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

            #region Constructors
            /// <summary>
            /// Конструктор инициализирует данные перечислителя указанным списком.
            /// </summary>
            /// <param name="set">Список.</param>
            internal HashSetArrayEnumerator(HashSetArray<TItem> set)
            {
                _set = set;
                _index = 0;
                _version = set._version;
                _current = default;
            }
            #endregion

            #region Main methods
            /// <summary>
            /// Освобождение управляемых ресурсов.
            /// </summary>
            public readonly void Dispose()
            {
            }

            /// <summary>
            /// Переход к следующему элементу списка.
            /// </summary>
            /// <returns>Возможность перехода к следующему элементу списка.</returns>
            public bool MoveNext()
            {
                if (_version != _set._version)
                {
                    return false;
                }

                while (_index < _set._lastIndex)
                {
                    if (_set._slots[_index].hashCode >= 0)
                    {
                        _current = _set._slots[_index].value;
                        _index++;
                        return true;
                    }
                    _index++;
                }
                _index = _set._lastIndex + 1;
                _current = default;
                return false;
            }

            /// <summary>
            /// Перестановка позиции на первый элемент списка.
            /// </summary>
            public void Reset()
            {
                if (_version != _set._version)
                {
                    return;
                }

                _index = 0;
                _current = default;
            }
            #endregion
        }

        /// <summary>
        /// Вспомогательный тип.
        /// </summary>
        protected internal struct Slot
        {
            internal int hashCode;      // Lower 31 bits of hash code, -1 if unused
            internal int next;          // Index of next entry, -1 if last
            internal TItem value;
        }
        #endregion

        #region Const
        // store lower 31 bits of hash code
        private const int Lower31BitMask = 0x7FFFFFFF;

        // cutoff poInt32, above which we won't do stackallocs. This corresponds to 100 integers.
        private const int StackAllocThreshold = 100;

        // when constructing a hashset from an existing collection, it may contain duplicates, 
        // so this is used as the max acceptable excess ratio of capacity to count. Note that
        // this is only used on the ctor and not to automatically shrink if the hashset has, e.g,
        // a lot of adds followed by removes. Users must explicitly shrink by calling TrimExcess.
        // This is set to 3 because capacity is acceptable as 2x rounded up to nearest prime.
        private const int ShrinkThreshold = 3;
        #endregion

        #region Fields
        protected internal int[] _buckets;
        protected internal Slot[] _slots;
        protected internal int _count;
        protected internal int _lastIndex;
        protected internal int _freeList;
        protected internal IEqualityComparer<TItem> _comparer;
        protected internal int _version;
        #endregion

        #region Properties
        /// <summary>
        /// Number of elements in this hashset.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Whether this is readonly.
        /// </summary>
        bool ICollection<TItem>.IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные списка предустановленными данными.
        /// </summary>
        public HashSetArray()
            : this(EqualityComparer<TItem>.Default)
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        public HashSetArray(int capacity)
            : this(capacity, EqualityComparer<TItem>.Default)
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="comparer">Компаратор.</param>
        public HashSetArray(IEqualityComparer<TItem> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TItem>.Default;
            }

            _comparer = comparer;
            _lastIndex = 0;
            _count = 0;
            _freeList = -1;
            _version = 0;
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="collection">Список элементов.</param>
        public HashSetArray(IEnumerable<TItem> collection)
            : this(collection, EqualityComparer<TItem>.Default)
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="collection">Список элементов.</param>
        /// <param name="comparer">Компаратор.</param>
        public HashSetArray(IEnumerable<TItem> collection, IEqualityComparer<TItem> comparer)
            : this(comparer)
        {
            var otherAsHashSet = collection as HashSetArray<TItem>;
            if (otherAsHashSet != null && AreEqualityComparersEqual(this, otherAsHashSet))
            {
                CopyFrom(otherAsHashSet);
            }
            else
            {
                // to avoid excess resizes, first set size based on collection's count. Collection
                // may contain duplicates, so call TrimExcess if resulting hashset is larger than
                // threshold
                var coll = collection as ICollection<TItem>;
                var suggestedCapacity = coll == null ? 0 : coll.Count;
                Initialize(suggestedCapacity);

                UnionWith(collection);

                if (_count > 0 && _slots.Length / _count > ShrinkThreshold)
                {
                    TrimExcess();
                }
            }
        }

        /// <summary>
        /// Конструктор инициализирует данные списка указанными данными.
        /// </summary>
        /// <param name="capacity">Начальная максимальная емкость списка.</param>
        /// <param name="comparer">Компаратор.</param>
        public HashSetArray(int capacity, IEqualityComparer<TItem> comparer)
            : this(comparer)
        {
            if (capacity > 0)
            {
                Initialize(capacity);
            }
        }
        #endregion

        #region ICollection methods
        /// <summary>
        /// Add item to this hashset. This is the explicit implementation of the ICollection.
        /// interface. The other Add method returns bool indicating whether item was added.
        /// </summary>
        /// <param name="item">item to add.</param>
        void ICollection<TItem>.Add(TItem item)
        {
            AddIfNotPresent(item);
        }

        /// <summary>
        /// Remove all items from this set. This clears the elements but not the underlying.
        /// buckets and slots array. Follow this call by TrimExcess to release these.
        /// </summary>
        public void Clear()
        {
            if (_lastIndex > 0)
            {
                // clear the elements so that the gc can reclaim the references.
                // clear only up to _lastIndex for _slots 
                Array.Clear(_slots, 0, _lastIndex);
                Array.Clear(_buckets, 0, _buckets.Length);
                _lastIndex = 0;
                _count = 0;
                _freeList = -1;
            }
            _version++;
        }

        /// <summary>
        /// Checks if this hashset contains the item.
        /// </summary>
        /// <param name="item">item to check for containment.</param>
        /// <returns>true if item contained; false if not.</returns>
        public bool Contains(TItem item)
        {
            if (_buckets != null)
            {
                var hashCode = InternalGetHashCode(item);
                // see note at "HashSetArray" level describing why "- 1" appears in for loop
                for (var i = _buckets[hashCode % _buckets.Length] - 1; i >= 0; i = _slots[i].next)
                {
                    if (_slots[i].hashCode == hashCode && _comparer.Equals(_slots[i].value, item))
                    {
                        return true;
                    }
                }
            }
            // either _buckets is null or wasn't found
            return false;
        }

        /// <summary>
        /// Copy items in this hashset to array, starting at arrayIndex.
        /// </summary>
        /// <param name="array">array to add items to.</param>
        /// <param name="arrayIndex">index to start at.</param>
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, _count);
        }

        /// <summary>
        /// Remove item from this hashset.
        /// </summary>
        /// <param name="item">item to remove.</param>
        /// <returns>true if removed; false if not (i.e. if the item wasn't in the HashSetArray).</returns>
        public bool Remove(TItem item)
        {
            if (_buckets != null)
            {
                var hashCode = InternalGetHashCode(item);
                var bucket = hashCode % _buckets.Length;
                var last = -1;
                for (var i = _buckets[bucket] - 1; i >= 0; last = i, i = _slots[i].next)
                {
                    if (_slots[i].hashCode == hashCode && _comparer.Equals(_slots[i].value, item))
                    {
                        if (last < 0)
                        {
                            // first iteration; update buckets
                            _buckets[bucket] = _slots[i].next + 1;
                        }
                        else
                        {
                            // subsequent iterations; update 'next' pointers
                            _slots[last].next = _slots[i].next;
                        }
                        _slots[i].hashCode = -1;
                        _slots[i].value = default!;
                        _slots[i].next = _freeList;

                        _count--;
                        _version++;
                        if (_count == 0)
                        {
                            _lastIndex = 0;
                            _freeList = -1;
                        }
                        else
                        {
                            _freeList = i;
                        }
                        return true;
                    }
                }
            }
            // either _buckets is null or wasn't found
            return false;
        }
        #endregion

        #region IEnumerable methods
        public HashSetArrayEnumerator GetEnumerator()
        {
            return new HashSetArrayEnumerator(this);
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return new HashSetArrayEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashSetArrayEnumerator(this);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Add item to this HashSetArray. Returns bool indicating whether item was added (won't be added if already present).
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if added, false if already present.</returns>
        public bool Add(TItem item)
        {
            return AddIfNotPresent(in item);
        }

        /// <summary>
        /// Add item to this HashSetArray. Returns bool indicating whether item was added (won't be.
        /// added if already present).
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if added, false if already present.</returns>
        public bool Add(in TItem item)
        {
            return AddIfNotPresent(in item);
        }

        /// <summary>
        /// Searches the set for a given value and returns the equal value it finds, if any.
        /// </summary>
        /// <param name="equalValue">The value to search for.</param>
        /// <param name="actualValue">The value from the set that the search found, or the default value of <typeparamref name="TItem"/> when the search yielded no match.</param>
        /// <returns>A value indicating whether the search was successful.</returns>
        /// <remarks>
        /// This can be useful when you want to reuse a previously stored reference instead of 
        /// a newly constructed one (so that more sharing of references can occur) or to look up
        /// a value that has more complete data than the value you currently have, although their
        /// comparer functions indicate they are equal.
        /// </remarks>
        public bool TryGetValue(in TItem equalValue, out TItem? actualValue)
        {
            if (_buckets != null)
            {
                var i = InternalIndexOf(equalValue);
                if (i >= 0)
                {
                    actualValue = _slots[i].value;
                    return true;
                }
            }
            actualValue = default;
            return false;
        }

        /// <summary>
        /// Take the union of this HashSetArray with other. Modifies this set.
        /// 
        /// Implementation note: GetSuggestedCapacity (to increase capacity in advance avoiding 
        /// multiple resizes ended up not being useful in practice; quickly gets to the 
        /// poInt32 where it's a wasteful check.
        /// </summary>
        /// <param name="other">enumerable with items to add.</param>
        public void UnionWith(IEnumerable<TItem> other)
        {
            foreach (var item in other)
            {
                AddIfNotPresent(item);
            }
        }

        /// <summary>
        /// Takes the intersection of this set with other. Modifies this set.
        /// 
        /// Implementation Notes: 
        /// We get better perf if other is a hashset using same equality comparer, because we 
        /// get constant contains check in other. Resulting cost is O(n1) to iterate over this.
        /// 
        /// If we can't go above route, iterate over the other and mark intersection by checking
        /// contains in this. Then loop over and delete any unmarked elements. Total cost is n2+n1. 
        /// 
        /// Attempts to return early based on counts alone, using the property that the 
        /// intersection of anything with the empty set is the empty set.
        /// </summary>
        /// <param name="other">enumerable with items to add .</param>
        public void IntersectWith(IEnumerable<TItem> other)
        {
            // intersection of anything with empty set is empty set, so return if count is 0
            if (_count == 0)
            {
                return;
            }

            // if other is empty, intersection is empty set; remove all elements and we're done
            // can only figure this out if implements ICollection<TItem>. (IEnumerable<TItem> has no count)
            var otherAsCollection = other as ICollection<TItem>;
            if (otherAsCollection != null)
            {
                if (otherAsCollection.Count == 0)
                {
                    Clear();
                    return;
                }

                var otherAsSet = other as HashSetArray<TItem>;
                // faster if other is a hashset using same equality comparer; so check 
                // that other is a hashset using the same equality comparer.
                if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
                {
                    IntersectWithHashSetWithSameEC(otherAsSet);
                    return;
                }
            }

            //IntersectWithEnumerable(other);
        }

        /// <summary>
        /// Remove items in other from this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to remove.</param>
        public void ExceptWith(IEnumerable<TItem> other)
        {
            // this is already the enpty set; return
            if (_count == 0)
            {
                return;
            }

            // special case if other is this; a set minus itself is the empty set
            if (other == this)
            {
                Clear();
                return;
            }

            // remove every element in other from this
            foreach (var element in other)
            {
                Remove(element);
            }
        }

        /// <summary>
        /// Takes symmetric difference (XOR) with other and this set. Modifies this set.
        /// </summary>
        /// <param name="other">enumerable with items to XOR.</param>
        public void SymmetricExceptWith(IEnumerable<TItem> other)
        {
            // if set is empty, then symmetric difference is other
            if (_count == 0)
            {
                UnionWith(other);
                return;
            }

            // special case this; the symmetric difference of a set with itself is the empty set
            if (other == this)
            {
                Clear();
                return;
            }

            var otherAsSet = other as HashSetArray<TItem>;
            // If other is a HashSetArray, it has unique elements according to its equality comparer,
            // but if they're using different equality comparers, then assumption of uniqueness
            // will fail. So first check if other is a hashset using the same equality comparer;
            // symmetric except is a lot faster and avoids bit array allocations if we can assume
            // uniqueness
            if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
            {
                SymmetricExceptWithUniqueHashSet(otherAsSet);
            }
            else
            {
                //SymmetricExceptWithEnumerable(other);
            }
        }

        /// <summary>
        /// Checks if this is a subset of other.
        /// 
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a subset of anything, including the empty set
        /// 2. If other has unique elements according to this equality comparer, and this has more
        /// elements than other, then it can't be a subset.
        /// 
        /// Furthermore, if other is a hashset using the same equality comparer, we can use a 
        /// faster element-wise check.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a subset of other; false if not.</returns>
        public bool IsSubsetOf(IEnumerable<TItem> other)
        {
            // The empty set is a subset of any set
            if (_count == 0)
            {
                return true;
            }

            var otherAsSet = other as HashSetArray<TItem>;
            // faster if other has unique elements according to this equality comparer; so check 
            // that other is a hashset using the same equality comparer.
            if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
            {
                // if this has more elements then it can't be a subset
                if (_count > otherAsSet.Count)
                {
                    return false;
                }

                // already checked that we're using same equality comparer. simply check that 
                // each element in this is contained in other.
                return IsSubsetOfHashSetWithSameEC(otherAsSet);
            }
            else
            {
                //ElementCount result = CheckUniqueAndUnfoundElements(other, false);
                //return (result.uniqueCount == _count && result.unfoundCount >= 0);
                return false;
            }
        }

        /// <summary>
        /// Checks if this is a proper subset of other (i.e. strictly contained in).
        /// 
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it's a proper subset of a set that contains at least
        /// one element, but it's not a proper subset of the empty set.
        /// 2. If other has unique elements according to this equality comparer, and this has >=
        /// the number of elements in other, then this can't be a proper subset.
        /// 
        /// Furthermore, if other is a hashset using the same equality comparer, we can use a 
        /// faster element-wise check.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a proper subset of other; false if not.</returns>
        public bool IsProperSubsetOf(IEnumerable<TItem> other)
        {
            var otherAsCollection = other as ICollection<TItem>;
            if (otherAsCollection != null)
            {
                // the empty set is a proper subset of anything but the empty set
                if (_count == 0)
                {
                    return otherAsCollection.Count > 0;
                }
                var otherAsSet = other as HashSetArray<TItem>;
                // faster if other is a hashset (and we're using same equality comparer)
                if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (_count >= otherAsSet.Count)
                    {
                        return false;
                    }
                    // this has strictly less than number of items in other, so the following
                    // check suffices for proper subset.
                    return IsSubsetOfHashSetWithSameEC(otherAsSet);
                }
            }

            //ElementCount result = CheckUniqueAndUnfoundElements(other, false);
            //return (result.uniqueCount == _count && result.unfoundCount > 0);
            return false;

        }

        /// <summary>
        /// Checks if this is a superset of other.
        /// 
        /// Implementation Notes:
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If other has no elements (it's the empty set), then this is a superset, even if this
        /// is also the empty set.
        /// 2. If other has unique elements according to this equality comparer, and this has less 
        /// than the number of elements in other, then this can't be a superset
        ///.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a superset of other; false if not.</returns>
        public bool IsSupersetOf(IEnumerable<TItem> other)
        {
            // try to fall out early based on counts
            var otherAsCollection = other as ICollection<TItem>;
            if (otherAsCollection != null)
            {
                // if other is the empty set then this is a superset
                if (otherAsCollection.Count == 0)
                {
                    return true;
                }
                var otherAsSet = other as HashSetArray<TItem>;
                // try to compare based on counts alone if other is a hashset with
                // same equality comparer
                if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (otherAsSet.Count > _count)
                    {
                        return false;
                    }
                }
            }

            return ContainsAllElements(other);
        }

        /// <summary>
        /// Checks if this is a proper superset of other (i.e. other strictly contained in this).
        /// 
        /// Implementation Notes: 
        /// This is slightly more complicated than above because we have to keep track if there
        /// was at least one element not contained in other.
        /// 
        /// The following properties are used up-front to avoid element-wise checks:
        /// 1. If this is the empty set, then it can't be a proper superset of any set, even if 
        /// other is the empty set.
        /// 2. If other is an empty set and this contains at least 1 element, then this is a proper
        /// superset.
        /// 3. If other has unique elements according to this equality comparer, and other's count
        /// is greater than or equal to this count, then this can't be a proper superset
        /// 
        /// Furthermore, if other has unique elements according to this equality comparer, we can
        /// use a faster element-wise check.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this is a proper superset of other; false if not.</returns>
        public bool IsProperSupersetOf(IEnumerable<TItem> other)
        {
            // the empty set isn't a proper superset of any set.
            if (_count == 0)
            {
                return false;
            }

            var otherAsCollection = other as ICollection<TItem>;
            if (otherAsCollection != null)
            {
                // if other is the empty set then this is a superset
                if (otherAsCollection.Count == 0)
                {
                    // note that this has at least one element, based on above check
                    return true;
                }
                var otherAsSet = other as HashSetArray<TItem>;
                // faster if other is a hashset with the same equality comparer
                if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
                {
                    if (otherAsSet.Count >= _count)
                    {
                        return false;
                    }
                    // now perform element check
                    return ContainsAllElements(otherAsSet);
                }
            }
            // couldn't fall out in the above cases; do it the long way
            //ElementCount result = CheckUniqueAndUnfoundElements(other, true);
            //return (result.uniqueCount < _count && result.unfoundCount == 0);
            return false;

        }

        /// <summary>
        /// Checks if this set overlaps other (i.e. they share at least one item).
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if these have at least one common element; false if disjoInt32.</returns>
        public bool Overlaps(IEnumerable<TItem> other)
        {
            if (_count == 0)
            {
                return false;
            }

            foreach (var element in other)
            {
                if (Contains(element))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if this and other contain the same elements. This is set equality:.
        /// duplicates and order are ignored.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool SetEquals(IEnumerable<TItem> other)
        {
            var otherAsSet = other as HashSetArray<TItem>;
            // faster if other is a hashset and we're using same equality comparer
            if (otherAsSet != null && AreEqualityComparersEqual(this, otherAsSet))
            {
                // attempt to return early: since both contain unique elements, if they have 
                // different counts, then they can't be equal
                if (_count != otherAsSet.Count)
                {
                    return false;
                }

                // already confirmed that the sets have the same number of distinct elements, so if
                // one is a superset of the other then they must be equal
                return ContainsAllElements(otherAsSet);
            }
            else
            {
                var otherAsCollection = other as ICollection<TItem>;
                if (otherAsCollection != null)
                {
                    // if this count is 0 but other contains at least one element, they can't be equal
                    if (_count == 0 && otherAsCollection.Count > 0)
                    {
                        return false;
                    }
                }
                //ElementCount result = CheckUniqueAndUnfoundElements(other, true);
                //return (result.uniqueCount == _count && result.unfoundCount == 0);
                return false;
            }
        }

        /// <summary>
        /// Копирование элементов массива.
        /// </summary>
        /// <param name="array">Массив.</param>
        public void CopyTo(TItem[] array)
        {
            CopyTo(array, 0, _count);
        }

        /// <summary>
        /// Копирование элементов массива.
        /// </summary>
        /// <param name="array">Массив.</param>
        /// <param name="arrayIndex">Начальный индекс</param>
        /// <param name="count">Количество элементов.</param>
        public void CopyTo(TItem[] array, int arrayIndex, int count)
        {
            // check array index valid index Int32o array
            if (arrayIndex < 0)
            {
                return;
            }

            // also throw if count less than 0
            if (count < 0)
            {
                return;
            }

            // will array, starting at arrayIndex, be able to hold elements? Note: not
            // checking arrayIndex >= array.Length (consistency with list of allowing
            // count of 0; subsequent check takes care of the rest)
            if (arrayIndex > array.Length || count > array.Length - arrayIndex)
            {
                return;
            }

            var numCopied = 0;
            for (var i = 0; i < _lastIndex && numCopied < count; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    array[arrayIndex + numCopied] = _slots[i].value;
                    numCopied++;
                }
            }
        }

        /// <summary>
        /// Remove elements that match specified predicate. Returns the number of elements removed.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveWhere(Predicate<TItem> match)
        {
            var numRemoved = 0;
            for (var i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    // cache value in case delegate removes it
                    var value = _slots[i].value;
                    if (match(value))
                    {
                        // check again that remove actually removed it
                        if (Remove(value))
                        {
                            numRemoved++;
                        }
                    }
                }
            }
            return numRemoved;
        }

        /// <summary>
        /// Gets the IEqualityComparer that is used to determine equality of keys for.
        /// the HashSetArray.
        /// </summary>
        public IEqualityComparer<TItem> Comparer
        {
            get
            {
                return _comparer;
            }
        }

        /// <summary>
        /// Sets the capacity of this list to the size of the list (rounded up to nearest prime),.
        /// unless count is 0, in which case we release references.
        /// 
        /// This method can be used to minimize a list's memory overhead once it is known that no
        /// new elements will be added to the list. To completely clear a list and release all 
        /// memory referenced by the list, execute the following statements:
        /// 
        /// list.Clear();
        /// list.TrimExcess();.
        /// </summary>
        public void TrimExcess()
        {
            if (_count == 0)
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                // if count is zero, clear references
                _buckets = null;
                _slots = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                _version++;
            }
            else
            {
                // similar to IncreaseCapacity but moves down elements in case add/remove/etc
                // caused fragmentation
                var newSize = XHashHelpers.GetPrime(_count);
                var newSlots = new Slot[newSize];
                var newBuckets = new int[newSize];

                // move down slots and rehash at the same time. newIndex keeps track of current 
                // position in newSlots array
                var newIndex = 0;
                for (var i = 0; i < _lastIndex; i++)
                {
                    if (_slots[i].hashCode >= 0)
                    {
                        newSlots[newIndex] = _slots[i];

                        // rehash
                        var bucket = newSlots[newIndex].hashCode % newSize;
                        newSlots[newIndex].next = newBuckets[bucket] - 1;
                        newBuckets[bucket] = newIndex + 1;

                        newIndex++;
                    }
                }

                _lastIndex = newIndex;
                _slots = newSlots;
                _buckets = newBuckets;
                _freeList = -1;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes the HashSetArray from another HashSetArray with the same element type and.
        /// equality comparer.
        /// </summary>
        /// <param name="source"></param>
        private void CopyFrom(HashSetArray<TItem> source)
        {
            var count = source._count;
            if (count == 0)
            {
                // As well as short-circuiting on the rest of the work done,
                // this avoids errors from trying to access otherAsHashSet._buckets
                // or otherAsHashSet._slots when they aren't initialized.
                return;
            }

            var capacity = source._buckets.Length;
            var threshold = XHashHelpers.ExpandPrime(count + 1);

            if (threshold >= capacity)
            {
                _buckets = (int[])source._buckets.Clone();
                _slots = (Slot[])source._slots.Clone();

                _lastIndex = source._lastIndex;
                _freeList = source._freeList;
            }
            else
            {
                var lastIndex = source._lastIndex;
                var slots = source._slots;
                Initialize(count);
                var index = 0;
                for (var i = 0; i < lastIndex; ++i)
                {
                    var hashCode = slots[i].hashCode;
                    if (hashCode >= 0)
                    {
                        AddValue(index, hashCode, slots[i].value);
                        ++index;
                    }
                }
                _lastIndex = index;
            }
            _count = count;
        }

        /// <summary>
        /// Initializes buckets and slots arrays. Uses suggested capacity by finding next prime.
        /// greater than or equal to capacity.
        /// </summary>
        /// <param name="capacity"></param>
        private void Initialize(int capacity)
        {
            var size = XHashHelpers.GetPrime(capacity);

            _buckets = new int[size];
            _slots = new Slot[size];
        }

        /// <summary>
        /// Expand to new capacity. New capacity is next prime greater than or equal to suggested.
        /// size. This is called when the underlying array is filled. This performs no 
        /// defragmentation, allowing faster execution; note that this is reasonable since 
        /// AddIfNotPresent attempts to insert new elements in re-opened spots.
        /// </summary>
        private void IncreaseCapacity()
        {
            var newSize = XHashHelpers.ExpandPrime(_count);
            if (newSize <= _count)
            {

            }

            // Able to increase capacity; copy elements to larger array and rehash
            SetCapacity(newSize, false);
        }

        /// <summary>
        /// Set the underlying buckets array to size newSize and rehash.  Note that newSize.
        /// *must* be a prime.  It is very likely that you want to call IncreaseCapacity()
        /// instead of this method.
        /// </summary>
        private void SetCapacity(int newSize, bool forceNewHashCodes)
        {
            var newSlots = new Slot[newSize];
            if (_slots != null)
            {
                Array.Copy(_slots, 0, newSlots, 0, _lastIndex);
            }

            if (forceNewHashCodes)
            {
                for (var i = 0; i < _lastIndex; i++)
                {
                    if (newSlots[i].hashCode != -1)
                    {
                        newSlots[i].hashCode = InternalGetHashCode(newSlots[i].value);
                    }
                }
            }

            var newBuckets = new int[newSize];
            for (var i = 0; i < _lastIndex; i++)
            {
                var bucket = newSlots[i].hashCode % newSize;
                newSlots[i].next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }
            _slots = newSlots;
            _buckets = newBuckets;
        }

        /// <summary>
        /// Adds value to HashSetArray if not contained already.
        /// Returns true if added and false if already present.
        /// </summary>
        /// <param name="value">value to find.</param>
        /// <returns></returns>
        private bool AddIfNotPresent(in TItem value)
        {
            if (_buckets == null)
            {
                Initialize(0);
            }

            var hashCode = InternalGetHashCode(value);
            var bucket = hashCode % _buckets!.Length;

            for (var i = _buckets[hashCode % _buckets.Length] - 1; i >= 0; i = _slots[i].next)
            {
                if (_slots[i].hashCode == hashCode && _comparer.Equals(_slots[i].value, value))
                {
                    return false;
                }
            }

            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _slots.Length)
                {
                    IncreaseCapacity();
                    // this will change during resize
                    bucket = hashCode % _buckets.Length;
                }
                index = _lastIndex;
                _lastIndex++;
            }
            _slots[index].hashCode = hashCode;
            _slots[index].value = value;
            _slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
            _count++;
            _version++;

            return true;
        }

        /// <summary>
        /// Add value at known index with known hash code. Used only.
        /// when constructing from another HashSetArray.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hashCode"></param>
        /// <param name="value"></param>
        private void AddValue(int index, int hashCode, in TItem value)
        {
            var bucket = hashCode % _buckets.Length;

            _slots[index].hashCode = hashCode;
            _slots[index].value = value;
            _slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
        }

        /// <summary>
        /// Checks if this contains of other's elements. Iterates over other's elements and.
        /// returns false as soon as it finds an element in other that's not in this.
        /// Used by SupersetOf, ProperSupersetOf, and SetEquals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool ContainsAllElements(IEnumerable<TItem> other)
        {
            foreach (var element in other)
            {
                if (!Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Implementation Notes:.
        /// If other is a hashset and is using same equality comparer, then checking subset is 
        /// faster. Simply check that each element in this is in other.
        /// 
        /// Note: if other doesn't use same equality comparer, then Contains check is invalid,
        /// which is why callers must take are of this.
        /// 
        /// If callers are concerned about whether this is a proper subset, they take care of that.
        ///.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool IsSubsetOfHashSetWithSameEC(HashSetArray<TItem> other)
        {

            foreach (var item in this)
            {
                if (!other.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// If other is a hashset that uses same equality comparer, intersect is much faster.
        /// because we can use other's Contains.
        /// </summary>
        /// <param name="other"></param>
        private void IntersectWithHashSetWithSameEC(HashSetArray<TItem> other)
        {
            for (var i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].hashCode >= 0)
                {
                    var item = _slots[i].value;
                    if (!other.Contains(item))
                    {
                        Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// Used internally by set operations which have to rely on bit array marking. This is like.
        /// Contains but returns index in slots array. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private int InternalIndexOf(in TItem item)
        {
            var hashCode = InternalGetHashCode(item);
            for (var i = _buckets[hashCode % _buckets.Length] - 1; i >= 0; i = _slots[i].next)
            {
                if (_slots[i].hashCode == hashCode && _comparer.Equals(_slots[i].value, item))
                {
                    return i;
                }
            }
            // wasn't found
            return -1;
        }

        /// <summary>
        /// if other is a set, we can assume it doesn't have duplicate elements, so use this.
        /// technique: if can't remove, then it wasn't present in this set, so add.
        /// 
        /// As with other methods, callers take care of ensuring that other is a hashset using the
        /// same equality comparer.
        /// </summary>
        /// <param name="other"></param>
        private void SymmetricExceptWithUniqueHashSet(HashSetArray<TItem> other)
        {
            foreach (var item in other)
            {
                if (!Remove(item))
                {
                    AddIfNotPresent(item);
                }
            }
        }

        /// <summary>
        /// Add if not already in hashset. Returns an out param indicating index where added. This.
        /// is used by SymmetricExcept because it needs to know the following things:
        /// - whether the item was already present in the collection or added from other
        /// - where it's located (if already present, it will get marked for removal, otherwise
        /// marked for keeping).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool AddOrGetLocation(in TItem value, out int location)
        {
            var hashCode = InternalGetHashCode(value);
            var bucket = hashCode % _buckets.Length;
            for (var i = _buckets[hashCode % _buckets.Length] - 1; i >= 0; i = _slots[i].next)
            {
                if (_slots[i].hashCode == hashCode && _comparer.Equals(_slots[i].value, value))
                {
                    location = i;
                    return false; //already present
                }
            }
            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _slots.Length)
                {
                    IncreaseCapacity();
                    // this will change during resize
                    bucket = hashCode % _buckets.Length;
                }
                index = _lastIndex;
                _lastIndex++;
            }
            _slots[index].hashCode = hashCode;
            _slots[index].value = value;
            _slots[index].next = _buckets[bucket] - 1;
            _buckets[bucket] = index + 1;
            _count++;
            _version++;
            location = index;
            return true;
        }

        /// <summary>
        /// Copies this to an array. Used for DebugView.
        /// </summary>
        /// <returns></returns>
        internal TItem[] ToArray()
        {
            var newArray = new TItem[Count];
            CopyTo(newArray);
            return newArray;
        }

        /// <summary>
        /// Internal method used for HashSetEqualityComparer. Compares set1 and set2 according.
        /// to specified comparer.
        /// 
        /// Because items are hashed according to a specific equality comparer, we have to resort
        /// to n^2 search if they're using different equality comparers.
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        internal static bool HashSetEquals(HashSetArray<TItem> set1, HashSetArray<TItem> set2, IEqualityComparer<TItem> comparer)
        {
            // handle null cases first
            if (set1 == null)
            {
                return set2 == null;
            }
            else if (set2 == null)
            {
                // set1 != null
                return false;
            }

            // all comparers are the same; this is faster
            if (AreEqualityComparersEqual(set1, set2))
            {
                if (set1.Count != set2.Count)
                {
                    return false;
                }
                // suffices to check subset
                foreach (var item in set2)
                {
                    if (!set1.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {  // n^2 search because items are hashed according to their respective ECs
                foreach (var set2Item in set2)
                {
                    var found = false;
                    foreach (var set1Item in set1)
                    {
                        if (comparer.Equals(set2Item, set1Item))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Checks if equality comparers are equal. This is used for algorithms that can.
        /// speed up if it knows the other item has unique elements. I.e. if they're using 
        /// different equality comparers, then uniqueness assumption between sets break.
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <returns></returns>
        private static bool AreEqualityComparersEqual(HashSetArray<TItem> set1, HashSetArray<TItem> set2)
        {
            return set1.Comparer.Equals(set2.Comparer);
        }

        /// <summary>
        /// Workaround Comparers that throw ArgumentNullException for GetHashCode(null).
        /// </summary>
        /// <param name="item"></param>
        /// <returns>hash code.</returns>
        private int InternalGetHashCode(in TItem item)
        {
            if (item == null)
            {
                return 0;
            }
            return _comparer.GetHashCode(item) & Lower31BitMask;
        }
        #endregion
    }
    /**@}*/
}