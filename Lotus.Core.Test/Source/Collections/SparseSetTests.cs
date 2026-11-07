#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif


namespace Lotus.Core.Collections
{
    public class SparseSetTests
    {
        /// <summary>
        /// Тестирование методов <see cref="SparseSet"/>.
        /// </summary>
        [Fact]
        public void AllMethods()
        {
            var sparseSet = new SparseSet(1024);
            sparseSet.Add(15); // 0
            sparseSet.Add(20); // 1
            sparseSet.Add(25); // 2
            sparseSet.Add(30); // 3

            Assert.Equal(sparseSet.Contains(15), true);
            Assert.Equal(sparseSet.Contains(20), true);
            Assert.Equal(sparseSet.Contains(25), true);
            Assert.Equal(sparseSet.Contains(30), true);
            Assert.Equal(sparseSet.Count, 4);
            Assert.Equal(sparseSet.MaxCount, 1024);

            sparseSet.Remove(20);

            Assert.Equal(sparseSet.Contains(20), false);
            Assert.Equal(sparseSet[0], 15);
            Assert.Equal(sparseSet[1], 30);
            Assert.Equal(sparseSet[2], 25);

            Assert.Equal(sparseSet.Count, 3);
            Assert.Equal(sparseSet.MaxCount, 1024);

            sparseSet.Add(1024);
            Assert.Equal(sparseSet.Contains(1024), true);
            Assert.Equal(sparseSet.Count, 4);
            Assert.Equal(sparseSet.MaxCount, 2048);

            sparseSet.AddValues(233, 3666, 15, 5555, 66, 777);
            Assert.Equal(sparseSet.Contains(233), true);
            Assert.Equal(sparseSet.Contains(3666), true);
            Assert.Equal(sparseSet.Contains(5555), true);
            Assert.Equal(sparseSet.Contains(15), true);
            Assert.Equal(sparseSet.Contains(66), true);
            Assert.Equal(sparseSet.Contains(777), true);
            Assert.Equal(sparseSet.Count, 9);
            Assert.Equal(sparseSet.MaxCount, 8192);

            sparseSet.RemoveValues(233, 3666, 5555, 15, 66, 777);
            Assert.Equal(sparseSet.Count, 3);
            Assert.Equal(sparseSet.Contains(1024), true);
            Assert.Equal(sparseSet.Contains(25), true);
            Assert.Equal(sparseSet.Contains(30), true);
            Assert.Equal(sparseSet.MaxCount, 8192);
        }
    }
}