using System.Collections.Generic;
using System.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreCollections
	*@{*/
    /// <summary>
    /// Словарь содержащий в качестве значений список определенного типа.
    /// </summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения списка словаря.</typeparam>
    public class DictionaryList<TKey, TValue> : Dictionary<TKey, ListArray<TValue>> where TKey : notnull
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует данные словаря предустановленными данными.
        /// </summary>
        public DictionaryList()
            : base()
        {
        }

        /// <summary>
        /// Конструктор инициализирует данные словаря указанными данными.
        /// </summary>
        /// <param name="capacity">Емкость.</param>
        public DictionaryList(int capacity)
            : base(capacity)
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление пары ключ - значение.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="item">Элемент.</param>
        public void Add(in TKey key, in TValue item)
        {
            if (ContainsKey(key))
            {
                var list = this[key];
                list.Add(item);
            }
            else
            {
                var list = new ListArray<TValue>
                    {
                        item
                    };
                Add(key, list);
            }
        }

        /// <summary>
        /// Проверка на наличие элемента в списке значение.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус наличия.</returns>
        public bool ContainsValue(in TValue item)
        {
            var result = false;

            foreach (var list in Values)
            {
                if (list.Contains(item))
                {
                    return true;
                }
            }

            return result;
        }

        /// <summary>
        /// Удаление элемента в списке значение.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус успешности удаления.</returns>
        public bool RemoveValue(in TValue item)
        {
            var result = false;

            foreach (var list in Values)
            {
                var index = list.IndexOf(item);
                if (index > -1)
                {
                    list.RemoveAt(index);

                    // Если это последний элемент то удаляем сам список и ключ
                    if (list.Count == 0)
                    {
                        var key = this.FirstOrDefault(x => x.Value == list).Key;
                        Remove(key);
                    }

                    return true;
                }
            }

            return result;
        }

        /// <summary>
        /// Удаление элемента в списке значение.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="item">Элемент.</param>
        /// <returns>Статус успешности удаления.</returns>
        public bool RemoveValue(in TKey key, in TValue item)
        {
            var result = false;

            foreach (var list in Values)
            {
                var index = list.IndexOf(item);
                if (index > -1)
                {
                    list.RemoveAt(index);

                    // Если это последний элемент то удаляем сам список и ключ
                    if (list.Count == 0)
                    {
                        Remove(key);
                    }

                    return true;
                }
            }

            return result;
        }
        #endregion
    }
    /**@}*/
}