using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Lotus.Core.Storage.File
{
    /// <summary>
    /// Тесты для <see cref="DataStorageFile"/>: CRUD, поиск, фильтрация, сортировка, пагинация и persistence.
    /// </summary>
    public class DataStorageFileTests : IDisposable
    {
        private readonly string _tempPath;
        private readonly List<string> _tempFiles = [];

        private class TestEntity : ILotusIdentifierId<Guid>, ILotusNameable
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Rank { get; set; }
        }

        private class TestEntityDto
        {
            public Guid Id { get; set; }
            public string DisplayName { get; set; } = string.Empty;
            public int Rank { get; set; }
        }

        private class TestStructure : StorageStructureInMemory
        {
            public List<TestEntity> Entities { get; set; } = [];

            public TestStructure()
                : base(registerFromProperties: true)
            {
            }
        }

        private class MappingFileStorage : DataStorageFile
        {
            public MappingFileStorage(StorageStructureInMemory structure, string? filePath = null)
                : base(structure, filePath)
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
                        Rank = testEntity.Rank,
                    };
                }

                return base.MapToResponse<TEntity, TResponse>(entity);
            }
        }

        public DataStorageFileTests()
        {
            _tempPath = Path.Combine(Path.GetTempPath(), $"lotus-file-storage-{Guid.NewGuid():N}");
            Directory.CreateDirectory(_tempPath);
        }

        public void Dispose()
        {
            foreach (var file in _tempFiles)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }

            if (Directory.Exists(_tempPath))
            {
                Directory.Delete(_tempPath, recursive: true);
            }
        }

        private string CreateTempFile(string extension = ".json")
        {
            var path = Path.Combine(_tempPath, $"{Guid.NewGuid():N}{extension}");
            _tempFiles.Add(path);
            return path;
        }

        private static DataStorageFile CreateStorage(string? path = null)
        {
            return new DataStorageFile(new TestStructure(), path);
        }

        private static (Guid alpha, Guid beta, Guid gamma, Guid delta) SeedSampleData(DataStorageFile storage)
        {
            var alpha = Guid.NewGuid();
            var beta = Guid.NewGuid();
            var gamma = Guid.NewGuid();
            var delta = Guid.NewGuid();

            storage.AddRange(
            [
                new TestEntity { Id = alpha, Name = "Alpha", Rank = 3 },
                new TestEntity { Id = beta, Name = "Beta", Rank = 1 },
                new TestEntity { Id = gamma, Name = "Gamma", Rank = 2 },
                new TestEntity { Id = delta, Name = "canEditUser", Rank = 4 },
            ]);

            return (alpha, beta, gamma, delta);
        }

        #region Persistence
        [Fact]
        public void SaveChanges_And_Load_JsonRoundtrip()
        {
            var path = CreateTempFile();
            var id = Guid.NewGuid();
            var storage = CreateStorage(path);
            storage.Add(new TestEntity { Id = id, Name = "Persisted", Rank = 7 });
            Assert.True(storage.NeedSaved);

            var saved = storage.SaveChanges();
            Assert.True(saved >= 1);
            Assert.False(storage.NeedSaved);
            Assert.True(System.IO.File.Exists(path));

            var loaded = CreateStorage(path);
            loaded.Load();

            var entity = loaded.Query<TestEntity>().Single();
            Assert.Equal(id, entity.Id);
            Assert.Equal("Persisted", entity.Name);
            Assert.Equal(7, entity.Rank);
            Assert.False(loaded.NeedSaved);
        }

        [Fact]
        public async ThreadingTask SaveChangesAsync_And_LoadAsync_JsonRoundtrip()
        {
            var path = CreateTempFile();
            var storage = CreateStorage(path);
            storage.AddRange(
            [
                new TestEntity { Id = Guid.NewGuid(), Name = "One", Rank = 1 },
                new TestEntity { Id = Guid.NewGuid(), Name = "Two", Rank = 2 },
            ]);

            await storage.SaveChangesAsync();

            var loaded = CreateStorage(path);
            await loaded.LoadAsync();

            Assert.Equal(2, loaded.Query<TestEntity>().Count());
            Assert.Contains(loaded.Query<TestEntity>(), x => x.Name == "One");
            Assert.Contains(loaded.Query<TestEntity>(), x => x.Name == "Two");
        }

        [Fact]
        public void Load_OverwritesExistingListContent()
        {
            var path = CreateTempFile();
            var id = Guid.NewGuid();
            var writer = CreateStorage(path);
            writer.Add(new TestEntity { Id = id, Name = "FromFile", Rank = 1 });
            writer.SaveChanges();

            var structure = new TestStructure();
            structure.Entities.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Stale", Rank = 99 });
            var reader = new DataStorageFile(structure, path);
            reader.Load();

            Assert.Single(structure.Entities);
            Assert.Equal("FromFile", structure.Entities[0].Name);
            Assert.Equal(id, structure.Entities[0].Id);
        }

        [Fact]
        public void SaveChanges_WithoutPath_OnlyClearsNeedSaved()
        {
            var storage = CreateStorage();
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "A", Rank = 1 });

            var count = storage.SaveChanges();

            Assert.True(count >= 1);
            Assert.False(storage.NeedSaved);
        }

        [Fact]
        public void Mutations_Persist_AfterReload()
        {
            var path = CreateTempFile();
            var storage = CreateStorage(path);
            var (alpha, beta, gamma, _) = SeedSampleData(storage);

            storage.Remove(storage.GetById<TestEntity, Guid>(gamma)!);
            storage.AddOrUpdate<TestEntity, Guid>(new TestEntity { Id = beta, Name = "BetaUpdated", Rank = 10 });
            storage.Add(new TestEntity { Id = Guid.NewGuid(), Name = "Epsilon", Rank = 5 });
            storage.SaveChanges();

            var loaded = CreateStorage(path);
            loaded.Load();

            var items = loaded.Query<TestEntity>().OrderBy(x => x.Name).ToArray();
            Assert.Equal(4, items.Length);
            Assert.DoesNotContain(items, x => x.Id == gamma);
            Assert.Equal("BetaUpdated", loaded.GetById<TestEntity, Guid>(beta)!.Name);
            Assert.Equal(10, loaded.GetById<TestEntity, Guid>(beta)!.Rank);
            Assert.NotNull(loaded.GetById<TestEntity, Guid>(alpha));
            Assert.Contains(items, x => x.Name == "Epsilon");
        }
        #endregion

        #region Search
        [Fact]
        public void GetById_GetByName_GetByIds_Work()
        {
            var storage = CreateStorage();
            var (alpha, beta, gamma, _) = SeedSampleData(storage);

            Assert.Equal("Alpha", storage.GetById<TestEntity, Guid>(alpha)!.Name);
            Assert.Equal(beta, storage.GetByName<TestEntity>("Beta")!.Id);
            Assert.Null(storage.GetById<TestEntity, Guid>(Guid.NewGuid()));
            Assert.Null(storage.GetByName<TestEntity>("Missing"));

            var byIds = storage.GetByIds<TestEntity, Guid>([gamma, Guid.NewGuid(), alpha]);
            Assert.Equal(2, byIds.Count);
            Assert.Contains(byIds, x => x!.Id == alpha);
            Assert.Contains(byIds, x => x!.Id == gamma);
        }

        [Fact]
        public async ThreadingTask GetByIdAsync_GetByNameAsync_FirstOrDefaultAsync_Work()
        {
            var storage = CreateStorage();
            var (alpha, _, _, _) = SeedSampleData(storage);

            Assert.Equal(alpha, (await storage.GetByIdAsync<TestEntity, Guid>(alpha))!.Id);
            Assert.Equal("Alpha", (await storage.GetByNameAsync<TestEntity>("Alpha"))!.Name);
            Assert.Equal("Gamma", (await storage.FirstOrDefaultAsync<TestEntity>(x => x.Name == "Gamma"))!.Name);
            Assert.Null(await storage.FirstOrDefaultAsync<TestEntity>(x => x.Name == "Missing"));
        }

        [Fact]
        public void GetOrAdd_ReturnsExistingOrCreates()
        {
            var storage = CreateStorage();
            var id = Guid.NewGuid();

            var created = storage.GetOrAdd<TestEntity, Guid>(id);
            created.Name = "Created";
            var existing = storage.GetOrAdd<TestEntity, Guid>(id);

            Assert.Same(created, existing);
            Assert.Equal("Created", existing.Name);
            Assert.Single(storage.Query<TestEntity>());
        }
        #endregion

        #region Delete and mass update
        [Fact]
        public void Remove_And_RemoveRange_Work()
        {
            var storage = CreateStorage();
            var (alpha, beta, gamma, delta) = SeedSampleData(storage);

            storage.Remove(storage.GetById<TestEntity, Guid>(alpha)!);
            Assert.Null(storage.GetById<TestEntity, Guid>(alpha));

            storage.RemoveRange(
            [
                storage.GetById<TestEntity, Guid>(beta)!,
                storage.GetById<TestEntity, Guid>(gamma)!,
            ]);

            Assert.Single(storage.Query<TestEntity>());
            Assert.Equal(delta, storage.Query<TestEntity>().Single().Id);
            Assert.True(storage.NeedSaved);
        }

        [Fact]
        public void AddOrUpdate_And_UpdateRange_Work()
        {
            var storage = CreateStorage();
            var (alpha, beta, _, _) = SeedSampleData(storage);

            var updated = storage.AddOrUpdate<TestEntity, Guid>(new TestEntity
            {
                Id = alpha,
                Name = "AlphaNew",
                Rank = 100,
            });
            Assert.Equal("AlphaNew", updated.Name);
            Assert.Equal(100, storage.GetById<TestEntity, Guid>(alpha)!.Rank);

            var created = storage.AddOrUpdate<TestEntity, Guid>(new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "NewItem",
                Rank = 9,
            });
            Assert.Equal("NewItem", storage.GetById<TestEntity, Guid>(created.Id)!.Name);

            var betaEntity = storage.GetById<TestEntity, Guid>(beta)!;
            betaEntity.Name = "BetaTouched";
            storage.UpdateRange([betaEntity]);
            Assert.Equal("BetaTouched", storage.GetById<TestEntity, Guid>(beta)!.Name);
            Assert.Equal(5, storage.Query<TestEntity>().Count());
        }

        [Fact]
        public void SyncRelatedCollections_ReplacesSet()
        {
            var storage = CreateStorage();
            var (alpha, beta, gamma, _) = SeedSampleData(storage);
            var keepUpdated = new TestEntity { Id = alpha, Name = "AlphaSynced", Rank = 1 };
            var added = new TestEntity { Id = Guid.NewGuid(), Name = "Added", Rank = 2 };

            storage.SyncRelatedCollections<TestEntity, Guid>([keepUpdated, added]);

            var items = storage.Query<TestEntity>().OrderBy(x => x.Name).ToArray();
            Assert.Equal(2, items.Length);
            Assert.Equal("Added", items[0].Name);
            Assert.Equal("AlphaSynced", items[1].Name);
            Assert.Null(storage.GetById<TestEntity, Guid>(beta));
            Assert.Null(storage.GetById<TestEntity, Guid>(gamma));
        }
        #endregion

        #region Filter / Sort / Page
        [Fact]
        public void Filter_ByNameAndRank_Works()
        {
            var storage = CreateStorage();
            SeedSampleData(storage);

            var equals = storage.Query<TestEntity>().Filter(new FilterByProperty
            {
                PropertyPath = nameof(TestEntity.Name),
                Value = "Beta",
                Function = TFilterFunction.Equals,
            }).ToArray();
            Assert.Single(equals);
            Assert.Equal("Beta", equals[0].Name);

            var contains = storage.Query<TestEntity>().Filter(new FilterByProperty
            {
                PropertyPath = nameof(TestEntity.Name),
                Value = "can",
                Function = TFilterFunction.Contains,
            }).ToArray();
            Assert.Single(contains);
            Assert.Equal("canEditUser", contains[0].Name);

            var lessThan = storage.Query<TestEntity>().Filter(new FilterByProperty
            {
                PropertyPath = nameof(TestEntity.Rank),
                Value = "3",
                Function = TFilterFunction.LessThan,
            }).ToArray();
            Assert.Equal(2, lessThan.Length);
        }

        [Fact]
        public void Sort_ByNameAndRank_Works()
        {
            var storage = CreateStorage();
            SeedSampleData(storage);

            var byNameDesc = storage.Query<TestEntity>()
                .OrderByProperty(nameof(TestEntity.Name), isDesc: true)
                .Select(x => x.Name)
                .ToArray();
            // Ordinal/culture: 'G' < 'c', поэтому Gamma выше canEditUser при desc.
            Assert.Equal(["Gamma", "canEditUser", "Beta", "Alpha"], byNameDesc);

            var multi = storage.Query<TestEntity>().Sort(
                new SortByProperty { PropertyPath = nameof(TestEntity.Rank), IsDesc = false },
                new SortByProperty { PropertyPath = nameof(TestEntity.Name), IsDesc = false })
                .Select(x => x.Name)
                .ToArray();
            Assert.Equal(["Beta", "Gamma", "Alpha", "canEditUser"], multi);
        }

        [Fact]
        public async ThreadingTask PageAsync_WithoutProjection_Works()
        {
            var storage = CreateStorage();
            SeedSampleData(storage);

            var ordered = storage.Query<TestEntity>().OrderBy(x => x.Rank).ThenBy(x => x.Name);
            var page = await storage.PageAsync(ordered, page: 1, pageSize: 2);

            Assert.Equal(4, page.PageInfo.TotalCount);
            Assert.Equal(1, page.PageInfo.PageNumber);
            Assert.Equal(2, page.PageInfo.PageSize);
            Assert.Equal(2, page.Payload.Count);
            // Rank order: Beta(1), Gamma(2), Alpha(3), canEditUser(4) -> page1 = Alpha, canEditUser
            Assert.Equal(["Alpha", "canEditUser"], page.Payload.Select(x => x.Name).ToArray());
        }

        [Fact]
        public async ThreadingTask PageAsync_WithFilterSortAndProjection_Works()
        {
            var path = CreateTempFile();
            var storage = new MappingFileStorage(new TestStructure(), path);
            SeedSampleData(storage);
            storage.SaveChanges();

            var loaded = new MappingFileStorage(new TestStructure(), path);
            loaded.Load();

            var query = loaded.Query<TestEntity>()
                .Filter(new FilterByProperty
                {
                    PropertyPath = nameof(TestEntity.Rank),
                    Values = ["0", "4"],
                    Function = TFilterFunction.Between,
                })
                .OrderByProperty(nameof(TestEntity.Name), isDesc: false);

            var page = await loaded.PageAsync<TestEntity, TestEntityDto>(query, page: 0, pageSize: 2);

            // Between uses > first and < second, so ranks 1..3 remain: Alpha(3), Beta(1), Gamma(2)
            Assert.Equal(3, page.PageInfo.TotalCount);
            Assert.Equal(2, page.Payload.Count);
            Assert.Equal("Alpha", page.Payload.ElementAt(0).DisplayName);
            Assert.Equal("Beta", page.Payload.ElementAt(1).DisplayName);
        }

        [Fact]
        public async ThreadingTask PageAsync_WithRequest_UsesPageInfo()
        {
            var storage = CreateStorage();
            SeedSampleData(storage);

            var request = new Request
            {
                PageInfo = new PageInfoRequest { PageNumber = 0, PageSize = 3 },
            };

            var page = await storage.PageAsync(
                storage.Query<TestEntity>().OrderByDescending(x => x.Rank),
                request);

            Assert.Equal(4, page.PageInfo.TotalCount);
            Assert.Equal(3, page.Payload.Count);
            Assert.Equal(4, page.Payload.First().Rank);
        }
        #endregion

        #region Repository bridge
        [Fact]
        public void GetRepository_SharesList_AndPersists()
        {
            var path = CreateTempFile();
            var storage = CreateStorage(path);
            var repository = storage.GetRepository<TestEntity, Guid>();
            Assert.NotNull(repository);

            var id = Guid.NewGuid();
            repository!.Add(new TestEntity { Id = id, Name = "ViaRepo", Rank = 1 });
            // RepositoryList itself does not mark NeedSaved; mark via storage op or SaveChanges after direct list mutation.
            storage.Update(repository.GetById(id)!);
            storage.SaveChanges();

            var loaded = CreateStorage(path);
            loaded.Load();
            Assert.Equal("ViaRepo", loaded.GetById<TestEntity, Guid>(id)!.Name);
        }
        #endregion
    }
}
