using System;

namespace Lotus.Core.EntityDb
{
    /// <summary>
    /// Тесты для критических участков EntityDb системы.
    /// </summary>
    public class EntityDbCriticalTests
    {
        /// <summary>
        /// Тест BaseEntityDb - корректная работа с ключом int.
        /// </summary>
        [Fact]
        public void BaseEntityDb_WithIntKey_WorksCorrectly()
        {
            var entity = new BaseEntityDb<int>
            {
                Id = 123
            };

            Assert.Equal(123, entity.Id);
            Assert.Equal("123", entity.ToString());
        }

        /// <summary>
        /// Тест BaseEntityDb - корректная работа с ключом Guid.
        /// </summary>
        [Fact]
        public void BaseEntityDb_WithGuidKey_WorksCorrectly()
        {
            var guid = Guid.NewGuid();
            var entity = new BaseEntityDb<Guid>
            {
                Id = guid
            };

            Assert.Equal(guid, entity.Id);
            Assert.Equal(guid.ToString(), entity.ToString());
        }

        /// <summary>
        /// Тест EntityDb - корректная работа с датами создания и модификации.
        /// </summary>
        [Fact]
        public void EntityDb_WithDates_WorksCorrectly()
        {
            var created = DateTime.UtcNow.AddDays(-10);
            var modified = DateTime.UtcNow;

            var entity = new EntityDb<int>
            {
                Id = 456,
                Created = created,
                Modified = modified
            };

            Assert.Equal(456, entity.Id);
            Assert.Equal(created, entity.Created);
            Assert.Equal(modified, entity.Modified);
        }

        /// <summary>
        /// Тест EntityDbSoftDeletable - корректная работа с мягким удалением.
        /// </summary>
        [Fact]
        public void EntityDbSoftDeletable_WithSoftDelete_WorksCorrectly()
        {
            var deleted = DateTime.UtcNow;

            var entity = new EntityDbSoftDeletable<int>
            {
                Id = 789,
                Created = DateTime.UtcNow.AddDays(-5),
                Modified = DateTime.UtcNow.AddDays(-1),
                Deleted = deleted
            };

            Assert.Equal(789, entity.Id);
            Assert.NotNull(entity.Deleted);
            Assert.Equal(deleted, entity.Deleted);
        }

        /// <summary>
        /// Тест EntityDbSoftDeletable - корректная работа без удаления.
        /// </summary>
        [Fact]
        public void EntityDbSoftDeletable_WithoutDelete_WorksCorrectly()
        {
            var entity = new EntityDbSoftDeletable<int>
            {
                Id = 999,
                Created = DateTime.UtcNow.AddDays(-5),
                Modified = DateTime.UtcNow.AddDays(-1),
                Deleted = null
            };

            Assert.Equal(999, entity.Id);
            Assert.Null(entity.Deleted);
        }

        /// <summary>
        /// Тест EntityDb - корректная реализация интерфейса ILotusEntityDb.
        /// </summary>
        [Fact]
        public void EntityDb_ImplementsILotusEntityDb()
        {
            var entity = new EntityDb<int>
            {
                Id = 111,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            ILotusEntityDb<int>? interfaceEntity = entity;
            Assert.NotNull(interfaceEntity);
            Assert.Equal(111, interfaceEntity.Id);
            Assert.True(interfaceEntity.Created > DateTime.MinValue);
            Assert.True(interfaceEntity.Modified > DateTime.MinValue);
        }
    }
}
