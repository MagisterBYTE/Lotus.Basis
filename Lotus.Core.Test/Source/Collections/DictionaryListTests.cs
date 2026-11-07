using System.Collections.Generic;

namespace Lotus.Core.Collections
{
    /// <summary>
    /// Тесты для <see cref="DictionaryList{TKey, TValue}"/>.
    /// </summary>
    public class DictionaryListTests
    {
        /// <summary>
        /// Тест метода RemoveValue - оптимизированный поиск.
        /// </summary>
        [Fact]
        public void RemoveValue_RemovesItemFromAnyList()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 20);
            dictionary.Add("key1", 10);
            dictionary.Add("key2", 20);
            dictionary.Add("key2", 30);

            var result = dictionary.RemoveValueAll(20);

            Assert.True(result);
            Assert.True(dictionary.ContainsKey("key1"));
            Assert.True(dictionary.ContainsKey("key2"));
            Assert.Equal(1, dictionary["key1"].Count);
            Assert.Equal(1, dictionary["key2"].Count);
        }

        /// <summary>
        /// Тест метода RemoveValue - удаляет ключ если список становится пустым.
        /// </summary>
        [Fact]
        public void RemoveValue_RemovesKeyWhenListBecomesEmpty()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 10);

            var result = dictionary.RemoveValueAll(10);

            Assert.True(result);
            Assert.False(dictionary.ContainsKey("key1"));
        }

        /// <summary>
        /// Тест метода RemoveValue - возвращает false если элемент не найден.
        /// </summary>
        [Fact]
        public void RemoveValue_WithNonExistentItem_ReturnsFalse()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 10);

            var result = dictionary.RemoveValueFirst(99);

            Assert.False(result);
            Assert.True(dictionary.ContainsKey("key1"));
        }

        /// <summary>
        /// Тест метода RemoveValue - работает с пустым словарем.
        /// </summary>
        [Fact]
        public void RemoveValue_WithEmptyDictionary_ReturnsFalse()
        {
            var dictionary = new DictionaryList<string, int>();

            var result = dictionary.RemoveValueAll(10);

            Assert.False(result);
        }
    }
}
