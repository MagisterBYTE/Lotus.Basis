using System;
using System.Collections.Generic;
using System.Linq;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Lotus.Core.Storage.InMemory
{
    /// <summary>
    /// Тесты для <see cref="DataStorageInMemory"/>.
    /// </summary>
    public class DataStorageInMemoryTests
    {
        private class TestEntity : ILotusIdentifierId<Guid>, ILotusNameable
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private class TestEntityDto
        {
            public Guid Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
        }

        private class TestStructure : StorageStructureInMemory
        {
            public List<TestEntity> Entities { get; set; } = [];

            public TestStructure()
                : base(registerFromProperties: true)
            {
            }
        }

        private class MappingStorage : DataStorageInMemory
        {
            public MappingStorage(StorageStructureInMemory structure)
                : base(structure)
            {
            }

            protected override TResponse MapToResponse<TEntity, TResponse>(TEntity entity)
            {
                if (entity is TestEntity testEntity && typeof(TResponse) == typeof(TestEntityDto))
                {
                    return (TResponse)(object)new TestEntityDto
                    {
                        Id = testEntity.Id,
                        DisplayName = testEntity.Name,
                    };
                }

                return base.MapToResponse<TEntity, TResponse>(entity);
            }
        }

        private static DataStorageInMemory CreateStorage()
        {
            return new DataStorageInMemory(new TestStructure());
        }

        [Fact]
        public void RegisterFromProperties_RegistersListProperty()
        {
            var structure = new TestStructure();

            Assert.True(structure.IsRegistered<TestEntity>());
            Assert.Same(structure.Entities, structure.GetEntitiesList<TestEntity>());
        }

        [Fact]
        public void Add_And_Query_Work()
        {
            var storage = CreateStorage();
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "A" };

            storage.Add(entity);

            Assert.True(storage.NeedSaved);
            Assert.Same(entity, storage.Query<TestEntity>().Single());
        }

        [Fact]
        public void GetById_And_GetByName_Work()
        {
            var storage = CreateStorage();
            var id = Guid.NewGuid();
            storage.Add(new TestEntity { Id = id, Name = "Named" });

            Assert.Equal("Named", storage.GetById<TestEntity, Guid>(id)!.Name);
            Assert.Equal(id, storage.GetByName<TestEntity>("Named")!.Id);
            Assert.Null(storage.GetById<TestEntity, Guid>(Guid.NewGuid()));
        }

        [Fact]
        public void GetByIds_ReturnsMatchingEntities()
        {
            var storage = CreateStorage();
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            storage.Add(new TestEntity { Id = id1, Name = "A" });
            storage.Add(new TestEntity { Id = id2, Name = "B" });

            var result = storage.GetByIds<TestEntity, Guid>([id2, Guid.NewGuid()]);

            Assert.Single(result);
            Assert.Equal(id2, result[0]!.Id);
        }

        [Fact]
        public void GetOrAdd_CreatesWhenMissing()
        {
            var storage = CreateStorage();
            var id = Guid.NewGuid();

            var created = storage.GetOrAdd<TestEntity, Guid>(id);
            var existing = storage.GetOrAdd<TestEntity, Guid>(id);

            Assert.Equal(id, created.Id);
            Assert.Same(created, existing);
            Assert.Single(storage.Query<TestEntity>());
        }

        [Fact]
        public void AddOrUpdate_ReplacesExisting()
        {
            var storage = CreateStorage();
            var id = Guid.NewGuid();
            storage.Add(new TestEntity { Id = id, Name = "Old" });

            var updated = storage.AddOrUpdate<TestEntity, Guid>(new TestEntity { Id = id, Name = "New" });

            Assert.Equal("New", updated.Name);
            Assert.Equal("New", storage.Query<TestEntity>().Single().Name);
        }

        [Fact]
        public void Remove_RemovesEntity()
        {
            var storage = CreateStorage();
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "A" };
            storage.Add(entity);

            storage.Remove(entity);

            Assert.Empty(storage.Query<TestEntity>());
        }

        [Fact]
        public void SyncRelatedCollections_AddsUpdatesAndRemoves()
        {
            var storage = CreateStorage();
            var keepId = Guid.NewGuid();
            var removeId = Guid.NewGuid();
            var addId = Guid.NewGuid();
            storage.Add(new TestEntity { Id = keepId, Name = "KeepOld" });
            storage.Add(new TestEntity { Id = removeId, Name = "Remove" });

            storage.SyncRelatedCollections<TestEntity, Guid>(
            [
                new TestEntity { Id = keepId, Name = "KeepNew" },
                new TestEntity { Id = addId, Name = "Add" },
            ]);

            var items = storage.Query<TestEntity>().OrderBy(x => x.Name).ToArray();
            Assert.Equal(2, items.Length);
            Assert.Equal("Add", items[0].Name);
            Assert.Equal("KeepNew", items[1].Name);
        }

        [Fact]
        public void GetRepository_SharesSameList()
        {
            var storage = CreateStorage();
            var repository = storage.GetRepository<TestEntity, Guid>();
            Assert.NotNull(repository);

            repository!.Add(new TestEntity { Id = Guid.NewGuid(), Name = "ViaRepo" });

            Assert.Single(storage.Query<TestEntity>());
        }

        [Fact]
        public void SaveChanges_ClearsNeedSaved()
        {
            var storage = CreateStorage();
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "A" });

            var count = storage.SaveChanges();

            Assert.True(count >= 1);
            Assert.False(storage.NeedSaved);
        }

        [Fact]
        public async ThreadingTask PageAsync_WithoutProjection_ReturnsPage()
        {
            var storage = CreateStorage();
            for (var i = 0; i < 5; i++)
            {
                storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = $"Item{i}" });
            }

            var page = await storage.PageAsync(
                storage.Query<TestEntity>().OrderBy(x => x.Name),
                page: 1,
                pageSize: 2);

            Assert.Equal(5, page.PageInfo.TotalCount);
            Assert.Equal(2, page.Payload.Count);
            Assert.Equal(1, page.PageInfo.PageNumber);
        }

        [Fact]
        public async ThreadingTask PageAsync_WithProjection_UsesMapToResponse()
        {
            var storage = new MappingStorage(new TestStructure());
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Alpha" });
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Beta" });

            var page = await storage.PageAsync<TestEntity, TestEntityDto>(
                storage.Query<TestEntity>().OrderBy(x => x.Name));

            Assert.Equal(2, page.Payload.Count);
            Assert.Contains(page.Payload, x => x.DisplayName == "Alpha");
            Assert.Contains(page.Payload, x => x.DisplayName == "Beta");
        }

        [Fact]
        public async ThreadingTask PageAsync_WithProjection_ThrowsWithoutOverride()
        {
            var storage = CreateStorage();
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "A" });

            await Assert.ThrowsAsync<NotSupportedException>(async () =>
                await storage.PageAsync<TestEntity, TestEntityDto>(
                    storage.Query<TestEntity>().OrderBy(x => x.Name)));
        }

        [Fact]
        public async ThreadingTask FirstOrDefaultAsync_Works()
        {
            var storage = CreateStorage();
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "A" });
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "B" });

            var result = await storage.FirstOrDefaultAsync<TestEntity>(x => x.Name == "B");

            Assert.Equal("B", result!.Name);
        }
    }
}
