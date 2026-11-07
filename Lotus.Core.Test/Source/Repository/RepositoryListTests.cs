using System;
using System.Collections.Generic;
using System.Linq;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Lotus.Core.Repository
{
    /// <summary>
    /// Тесты для <see cref="RepositoryList{TEntity, TKey}"/>.
    /// </summary>
    public class RepositoryListTests
    {
        private class MyEntity : ILotusIdentifierId<Guid>, ILotusNameable
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void Constructor_SetsList()
        {
            var list = new List<MyEntity>();

            var repository = new RepositoryList<MyEntity, Guid>(list);

            Assert.Same(list, repository._list);
        }

        [Fact]
        public void Constructor_ThrowsWhenListIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryList<MyEntity, Guid>(null!));
        }

        [Fact]
        public void SetList_SetsNewList()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);
            var newList = new List<MyEntity>();

            repository.SetList(newList);

            Assert.Same(newList, repository._list);
        }

        [Fact]
        public void SetList_ThrowsWhenListIsNull()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);

            Assert.Throws<ArgumentNullException>(() => repository.SetList(null!));
        }

        [Fact]
        public void Query_ReturnsQueryableList()
        {
            var list = new List<MyEntity>
            {
                new() { Id = Guid.NewGuid(), Name = "A" },
                new() { Id = Guid.NewGuid(), Name = "B" },
            };
            var repository = new RepositoryList<MyEntity, Guid>(list);

            var queryable = repository.Query();

            Assert.Equal(list, queryable.ToList());
        }

        [Fact]
        public void FirstOrDefault_ReturnsFirstEntityWhenPredicateIsNullAndListNotEmpty()
        {
            var entity1 = new MyEntity { Id = Guid.NewGuid(), Name = "First" };
            var entity2 = new MyEntity { Id = Guid.NewGuid(), Name = "Second" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2]);

            var result = repository.FirstOrDefault(null);

            Assert.Same(entity1, result);
        }

        [Fact]
        public void FirstOrDefault_ReturnsNullWhenPredicateIsNullAndListIsEmpty()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);

            var result = repository.FirstOrDefault(null);

            Assert.Null(result);
        }

        [Fact]
        public void FirstOrDefault_ReturnsMatchingEntity()
        {
            var entity1 = new MyEntity { Id = Guid.NewGuid(), Name = "Alpha" };
            var entity2 = new MyEntity { Id = Guid.NewGuid(), Name = "Beta" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2]);

            var result = repository.FirstOrDefault(x => x!.Name == "Beta");

            Assert.Same(entity2, result);
        }

        [Fact]
        public void GetById_ReturnsEntity()
        {
            var id = Guid.NewGuid();
            var entity = new MyEntity { Id = id, Name = "Item" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = repository.GetById(id);

            Assert.Same(entity, result);
        }

        [Fact]
        public void GetById_ReturnsNullWhenNotFound()
        {
            var repository = new RepositoryList<MyEntity, Guid>(
            [
                new MyEntity { Id = Guid.NewGuid(), Name = "Item" }
            ]);

            var result = repository.GetById(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async ThreadingTask GetByIdAsync_ReturnsEntity()
        {
            var id = Guid.NewGuid();
            var entity = new MyEntity { Id = id, Name = "Item" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = await repository.GetByIdAsync(id);

            Assert.Same(entity, result);
        }

        [Fact]
        public void GetByIds_ReturnsMatchingEntities()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var entity1 = new MyEntity { Id = id1, Name = "A" };
            var entity2 = new MyEntity { Id = id2, Name = "B" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2]);

            var result = repository.GetByIds([id2, Guid.NewGuid(), id1]);

            Assert.Equal(2, result.Count);
            Assert.Same(entity2, result[0]);
            Assert.Same(entity1, result[1]);
        }

        [Fact]
        public async ThreadingTask GetByIdsAsync_ReturnsMatchingEntities()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var entity1 = new MyEntity { Id = id1, Name = "A" };
            var entity2 = new MyEntity { Id = id2, Name = "B" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2]);

            var result = await repository.GetByIdsAsync([id1, id2]);

            Assert.Equal(2, result.Count);
            Assert.Same(entity1, result[0]);
            Assert.Same(entity2, result[1]);
        }

        [Fact]
        public void GetByName_ReturnsEntity()
        {
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "Named" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = repository.GetByName("Named");

            Assert.Same(entity, result);
        }

        [Fact]
        public async ThreadingTask GetByNameAsync_ReturnsEntity()
        {
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "Named" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = await repository.GetByNameAsync("Named");

            Assert.Same(entity, result);
        }

        [Fact]
        public void GetOrAdd_ReturnsExistingEntity()
        {
            var id = Guid.NewGuid();
            var entity = new MyEntity { Id = id, Name = "Existing" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = repository.GetOrAdd(id);

            Assert.Same(entity, result);
            Assert.Single(repository._list);
        }

        [Fact]
        public void GetOrAdd_CreatesEntityWhenMissing()
        {
            var id = Guid.NewGuid();
            var repository = new RepositoryList<MyEntity, Guid>([]);

            var result = repository.GetOrAdd(id);

            Assert.Equal(id, result.Id);
            Assert.Single(repository._list);
        }

        [Fact]
        public async ThreadingTask GetOrAddAsync_CreatesEntityWhenMissing()
        {
            var id = Guid.NewGuid();
            var repository = new RepositoryList<MyEntity, Guid>([]);

            var result = await repository.GetOrAddAsync(id);

            Assert.Equal(id, result.Id);
            Assert.Single(repository._list);
        }

        [Fact]
        public void Add_AddsEntity()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "New" };

            var result = repository.Add(entity);

            Assert.Same(entity, result);
            Assert.Same(entity, repository._list[0]);
        }

        [Fact]
        public async ThreadingTask AddAsync_AddsEntity()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "New" };

            var result = await repository.AddAsync(entity);

            Assert.Same(entity, result);
            Assert.Single(repository._list);
        }

        [Fact]
        public void AddRange_AddsEntities()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);
            var entities = new[]
            {
                new MyEntity { Id = Guid.NewGuid(), Name = "A" },
                new MyEntity { Id = Guid.NewGuid(), Name = "B" },
            };

            repository.AddRange(entities);

            Assert.Equal(2, repository._list.Count);
        }

        [Fact]
        public async ThreadingTask AddRangeAsync_AddsEntities()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);
            var entities = new[]
            {
                new MyEntity { Id = Guid.NewGuid(), Name = "A" },
                new MyEntity { Id = Guid.NewGuid(), Name = "B" },
            };

            await repository.AddRangeAsync(entities);

            Assert.Equal(2, repository._list.Count);
        }

        [Fact]
        public void Remove_RemovesEntity()
        {
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "A" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            repository.Remove(entity);

            Assert.Empty(repository._list);
        }

        [Fact]
        public void RemoveRange_RemovesEntities()
        {
            var entity1 = new MyEntity { Id = Guid.NewGuid(), Name = "A" };
            var entity2 = new MyEntity { Id = Guid.NewGuid(), Name = "B" };
            var entity3 = new MyEntity { Id = Guid.NewGuid(), Name = "C" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2, entity3]);

            repository.RemoveRange([entity1, entity3]);

            Assert.Single(repository._list);
            Assert.Same(entity2, repository._list[0]);
        }

        [Fact]
        public void RemoveId_RemovesEntity()
        {
            var id = Guid.NewGuid();
            var entity = new MyEntity { Id = id, Name = "A" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            repository.RemoveId(id);

            Assert.Empty(repository._list);
        }

        [Fact]
        public void RemoveIdsRange_RemovesEntities()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var entity1 = new MyEntity { Id = id1, Name = "A" };
            var entity2 = new MyEntity { Id = id2, Name = "B" };
            var entity3 = new MyEntity { Id = Guid.NewGuid(), Name = "C" };
            var repository = new RepositoryList<MyEntity, Guid>([entity1, entity2, entity3]);

            repository.RemoveIdsRange([id1, id2]);

            Assert.Single(repository._list);
            Assert.Same(entity3, repository._list[0]);
        }

        [Fact]
        public void Update_ReturnsSameEntity()
        {
            var entity = new MyEntity { Id = Guid.NewGuid(), Name = "A" };
            var repository = new RepositoryList<MyEntity, Guid>([entity]);

            var result = repository.Update(entity);

            Assert.Same(entity, result);
        }

        [Fact]
        public async ThreadingTask FlushAsync_Completes()
        {
            var repository = new RepositoryList<MyEntity, Guid>([]);

            await repository.FlushAsync();
            repository.Flush();
        }
    }
}
