using System;

namespace Lotus.Core
{
	/** \addtogroup CoreCollections
	*@{*/
	/// <summary>
	/// Очередь на основе массива.
	/// </summary>
	/// <remarks>
	/// Реализация очереди на основе массива, с полной поддержкой функциональности <see cref="ListArray{TItem}"/>
	/// с учетом особенности реализации очереди
	/// </remarks>
	/// <typeparam name="TItem">Тип элемента очереди.</typeparam>
	[Serializable]
	public class QueueArray<TItem> : ListArray<TItem>
	{
		#region Fields
		// Основные параметры
		protected internal int _head;
		protected internal int _tail;
		#endregion

		#region Properties
		//
		// ОСНОВНЫЕ ПАРАМЕТРЫ
		//
		/// <summary>
		/// Индекс текущего элемента в начале(голове) очереди.
		/// </summary>
		public int Head
		{
			get { return _head; }
		}

		/// <summary>
		/// Индекс текущего добавленного элемента в конец(хвост) очереди.
		/// </summary>
		public int Tail
		{
			get { return _tail; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Конструктор инициализирует данные очереди предустановленными данными.
		/// </summary>
		public QueueArray()
			: base(INIT_MAX_COUNT)
		{
			_head = 0;
			_tail = -1;
		}

		/// <summary>
		/// Конструктор инициализирует данные очереди указанными данными.
		/// </summary>
		/// <param name="maxCount">Максимальное количество элементов.</param>
		public QueueArray(int maxCount)
			: base(maxCount)
		{
			_head = 0;
			_tail = -1;
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Индексация элементов очереди.
		/// </summary>
		/// <param name="index">Индекс элемента.</param>
		/// <returns>Элемент очереди.</returns>
		new public TItem? this[int index]
		{
			get { return _arrayOfItems[(_head + index) % _maxCount]; }
			set
			{
				_arrayOfItems[(_head + index) % _maxCount] = value;
			}
		}
		#endregion

		#region Main methods
		/// <summary>
		/// Получение элемента очереди по индексу.
		/// </summary>
		/// <param name="index">Индекс элемента очереди.</param>
		/// <returns>Элемент очереди.</returns>
		public TItem? GetElement(int index)
		{
			return _arrayOfItems[(_head + index) % _maxCount];
		}

		/// <summary>
		/// Добавление элемента в конец очереди.
		/// </summary>
		/// <param name="item">Элемент.</param>
		public void Enqueue(in TItem? item)
		{
			// Если текущие количество элементов равно максимально возможному
			if (_count == _maxCount)
			{
				_maxCount *= 2;
				var items = new TItem[_maxCount];
				Array.Copy(_arrayOfItems, items, _count);
				_arrayOfItems = items;
			}

			_count++;
			_tail = (_tail + 1) % _maxCount;
			_arrayOfItems[_tail] = item;
		}

		/// <summary>
		/// Взятие и удаление элемента из головы очереди.
		/// </summary>
		/// <returns>Элемент.</returns>
		public TItem? Dequeue()
		{
			if (_count > 0)
			{
				TItem? item = _arrayOfItems[_head];
				_arrayOfItems[_head] = default;
				_head = (_head + 1) % _maxCount;
				_count--;
				return item;
			}
			else
			{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogError("Not element in queue!!!");
#else
				XLogger.LogError("Not element in queue!!!");
#endif
				return default;
			}

		}

		/// <summary>
		/// Взятие элемента из головы очереди (без его удаления).
		/// </summary>
		/// <returns>Элемент.</returns>
		public TItem? Peek()
		{
			if (_count > 0)
			{
				return _arrayOfItems[_head];
			}
			else
			{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogError("Not element in queue!!!");
#else
				XLogger.LogError("Not element in queue!!!");
#endif
				return default;
			}

		}

		/// <summary>
		/// Проверка на наличие элемента в очереди.
		/// </summary>
		/// <param name="item">Элемент.</param>
		/// <returns>Статус наличия.</returns>
		public new bool Contains(in TItem? item)
		{
			var index = _head;
			var count = _count;

			while (count-- > 0)
			{
				if (_arrayOfItems[index]!.Equals(item))
				{
					return true;
				}
				index = (index + 1) % _maxCount;
			}

			return false;
		}

		/// <summary>
		/// Очистка очереди.
		/// </summary>
		public new void Clear()
		{
			if (_head < _tail)
			{
				Array.Clear(_arrayOfItems, _head, _count);
			}
			else
			{
				Array.Clear(_arrayOfItems, _head, _maxCount - _head);
				Array.Clear(_arrayOfItems, 0, _tail);
			}

			_head = 0;
			_tail = -1;
			_count = 0;
		}
		#endregion
	}
	/**@}*/
}