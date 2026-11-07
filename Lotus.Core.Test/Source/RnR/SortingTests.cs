using System.Linq;

namespace Lotus.Core.RnR
{
    /// <summary>
    /// Тесты сортировки <see cref="XSortingQueryableExtension"/>.
    /// </summary>
    public class SortingTests
    {
        private class Item
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Rank { get; set; }
        }

        private static IQueryable<Item> CreateItems()
        {
            return new[]
            {
                new Item { Id = 1, Name = "Charlie", Rank = 2 },
                new Item { Id = 2, Name = "Alpha", Rank = 3 },
                new Item { Id = 3, Name = "Bravo", Rank = 1 },
                new Item { Id = 4, Name = "Alpha", Rank = 1 },
            }.AsQueryable();
        }

        [Fact]
        public void OrderByProperty_Ascending()
        {
            var result = CreateItems().OrderByProperty(nameof(Item.Name), false).ToArray();

            Assert.Equal("Alpha", result[0].Name);
            Assert.Equal("Alpha", result[1].Name);
            Assert.Equal("Bravo", result[2].Name);
            Assert.Equal("Charlie", result[3].Name);
        }

        [Fact]
        public void OrderByProperty_Descending()
        {
            var result = CreateItems().OrderByProperty(nameof(Item.Id), true).ToArray();

            Assert.Equal(4, result[0].Id);
            Assert.Equal(3, result[1].Id);
            Assert.Equal(2, result[2].Id);
            Assert.Equal(1, result[3].Id);
        }

        [Fact]
        public void Sort_ByMultipleProperties()
        {
            var result = CreateItems().Sort(
                new SortByProperty { PropertyPath = nameof(Item.Name), IsDesc = false },
                new SortByProperty { PropertyPath = nameof(Item.Rank), IsDesc = true }).ToArray();

            Assert.Equal(2, result[0].Id);
            Assert.Equal(4, result[1].Id);
            Assert.Equal(3, result[2].Id);
            Assert.Equal(1, result[3].Id);
        }

        [Fact]
        public void Sort_ReturnsOriginalQueryWhenPropertiesAreEmpty()
        {
            var items = CreateItems();

            var result = items.Sort().ToArray();

            Assert.Equal(4, result.Length);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void Sort_WithDefaultKey_UsesKeyWhenPropertiesAreEmpty()
        {
            var result = CreateItems().Sort(null, x => x.Rank).ToArray();

            Assert.Equal(3, result[0].Id);
            Assert.Equal(4, result[1].Id);
            Assert.Equal(1, result[2].Id);
            Assert.Equal(2, result[3].Id);
        }
    }
}
