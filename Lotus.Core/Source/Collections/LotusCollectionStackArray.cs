using System;

namespace Lotus.Core
{
    /** \addtogroup CoreCollections
	*@{*/
    /// <summary>
    /// Стек на основе массива.
    /// </summary>
    /// <remarks>
    /// Реализация стека на основе массива, с полной поддержкой функциональности <see cref="ListArray{TItem}"/> 
    /// с учетом особенности реализации стека
    /// </remarks>
    /// <typeparam name="TItem">Тип элемента стека.</typeparam>
    [Serializable]
    public class StackArray<TItem> : ListArray<TItem>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные стека предустановленными данными.
        /// </summary>
        public StackArray()
            : base(INIT_MAX_COUNT)
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные стека указанными данными.
        /// </summary>
        /// <param name="maxCount">Максимальное количество элементов.</param>
        public StackArray(int maxCount)
            : base(maxCount)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Вставка элемента в вершину стека.
        /// </summary>
        /// <param name="item">Элемент.</param>
        public void Push(in TItem item)
        {
            Add(item);
        }

        /// <summary>
        /// Вытолкнуть(удалить) элемент из вершины стека.
        /// </summary>
        /// <returns>Элемент.</returns>
        public TItem? Pop()
        {
            if (_count > 0)
            {
                var item = _arrayOfItems[_count - 1];
                _count--;
                _arrayOfItems[_count] = default;

                return item;
            }
            else
            {
#if UNITY_2017_1_OR_NEWER
				UnityEngine.Debug.LogError("Not element in stack!!!");
#else
                XLogger.LogError("Not element in stack!!!");
#endif
                return default;
            }
        }

        /// <summary>
        /// Взять элемент из вершины стека, но не выталкивать его(не удалять).
        /// </summary>
        /// <returns>Элемент.</returns>
        public TItem? Peek()
        {
            if (_count > 0)
            {
                return _arrayOfItems[_count - 1];
            }
            else
            {
#if UNITY_2017_1_OR_NEWER
				UnityEngine.Debug.LogError("Not element in stack!!!");
#else
                XLogger.LogError("Not element in stack!!!");
#endif
                return default;
            }
        }
        #endregion
    }
    /**@}*/
}