using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Collections
{
    /// <summary>
    /// Тесты для <see cref="DictionaryList{TKey, TValue}"/>.
    /// </summary>
    [TestFixture]
    public class DictionaryListTests
    {
        /// <summary>
        /// Тест метода RemoveValue - оптимизированный поиск.
        /// </summary>
        [Test]
        public void RemoveValue_RemovesItemFromAnyList()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 20);
            dictionary.Add("key1", 10);
            dictionary.Add("key2", 20);
            dictionary.Add("key2", 30);

            var result = dictionary.RemoveValueAll(20);

            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(dictionary.ContainsKey("key1"));
            ClassicAssert.IsTrue(dictionary.ContainsKey("key2"));
            ClassicAssert.AreEqual(1, dictionary["key1"].Count);
            ClassicAssert.AreEqual(1, dictionary["key2"].Count);
        }

        /// <summary>
        /// Тест метода RemoveValue - удаляет ключ если список становится пустым.
        /// </summary>
        [Test]
        public void RemoveValue_RemovesKeyWhenListBecomesEmpty()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 10);

            var result = dictionary.RemoveValueAll(10);

            ClassicAssert.IsTrue(result);
            ClassicAssert.IsFalse(dictionary.ContainsKey("key1"));
        }

        /// <summary>
        /// Тест метода RemoveValue - возвращает false если элемент не найден.
        /// </summary>
        [Test]
        public void RemoveValue_WithNonExistentItem_ReturnsFalse()
        {
            var dictionary = new DictionaryList<string, int>();
            dictionary.Add("key1", 10);

            var result = dictionary.RemoveValueFirst(99);

            ClassicAssert.IsFalse(result);
            ClassicAssert.IsTrue(dictionary.ContainsKey("key1"));
        }

        /// <summary>
        /// Тест метода RemoveValue - работает с пустым словарем.
        /// </summary>
        [Test]
        public void RemoveValue_WithEmptyDictionary_ReturnsFalse()
        {
            var dictionary = new DictionaryList<string, int>();

            var result = dictionary.RemoveValueAll(10);

            ClassicAssert.IsFalse(result);
        }
    }
}
