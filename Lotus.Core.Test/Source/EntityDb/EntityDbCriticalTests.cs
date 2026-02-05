using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.EntityDb
{
    /// <summary>
    /// Тесты для критических участков EntityDb системы.
    /// </summary>
    [TestFixture]
    public class EntityDbCriticalTests
    {
        /// <summary>
        /// Тест BaseEntityDb - корректная работа с ключом int.
        /// </summary>
        [Test]
        public void BaseEntityDb_WithIntKey_WorksCorrectly()
        {
            var entity = new BaseEntityDb<int>
            {
                Id = 123
            };

            ClassicAssert.AreEqual(123, entity.Id);
            ClassicAssert.AreEqual("123", entity.ToString());
        }

        /// <summary>
        /// Тест BaseEntityDb - корректная работа с ключом Guid.
        /// </summary>
        [Test]
        public void BaseEntityDb_WithGuidKey_WorksCorrectly()
        {
            var guid = Guid.NewGuid();
            var entity = new BaseEntityDb<Guid>
            {
                Id = guid
            };

            ClassicAssert.AreEqual(guid, entity.Id);
            ClassicAssert.AreEqual(guid.ToString(), entity.ToString());
        }

        /// <summary>
        /// Тест EntityDb - корректная работа с датами создания и модификации.
        /// </summary>
        [Test]
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

            ClassicAssert.AreEqual(456, entity.Id);
            ClassicAssert.AreEqual(created, entity.Created);
            ClassicAssert.AreEqual(modified, entity.Modified);
        }

        /// <summary>
        /// Тест EntityDbSoftDeletable - корректная работа с мягким удалением.
        /// </summary>
        [Test]
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

            ClassicAssert.AreEqual(789, entity.Id);
            ClassicAssert.IsNotNull(entity.Deleted);
            ClassicAssert.AreEqual(deleted, entity.Deleted);
        }

        /// <summary>
        /// Тест EntityDbSoftDeletable - корректная работа без удаления.
        /// </summary>
        [Test]
        public void EntityDbSoftDeletable_WithoutDelete_WorksCorrectly()
        {
            var entity = new EntityDbSoftDeletable<int>
            {
                Id = 999,
                Created = DateTime.UtcNow.AddDays(-5),
                Modified = DateTime.UtcNow.AddDays(-1),
                Deleted = null
            };

            ClassicAssert.AreEqual(999, entity.Id);
            ClassicAssert.IsNull(entity.Deleted);
        }

        /// <summary>
        /// Тест EntityDb - корректная реализация интерфейса ILotusEntityDb.
        /// </summary>
        [Test]
        public void EntityDb_ImplementsILotusEntityDb()
        {
            var entity = new EntityDb<int>
            {
                Id = 111,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            ILotusEntityDb<int>? interfaceEntity = entity;
            ClassicAssert.IsNotNull(interfaceEntity);
            ClassicAssert.AreEqual(111, interfaceEntity.Id);
            ClassicAssert.IsTrue(interfaceEntity.Created > DateTime.MinValue);
            ClassicAssert.IsTrue(interfaceEntity.Modified > DateTime.MinValue);
        }
    }
}
