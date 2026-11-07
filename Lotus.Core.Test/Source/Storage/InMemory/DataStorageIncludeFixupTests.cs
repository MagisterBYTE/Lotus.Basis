using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using ThreadingTask = System.Threading.Tasks.Task;

namespace Lotus.Core.Storage.InMemory
{
    /// <summary>
    /// Тесты Include и Fixup для in-memory / file хранилища.
    /// </summary>
    public class DataStorageIncludeFixupTests
    {
        private class RoleEntity : ILotusIdentifierId<int>
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;

            [JsonIgnore]
            public List<UserEntity> Users { get; set; } = [];
        }

        private class UserEntity : ILotusIdentifierId<int>
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int RoleId { get; set; }

            [JsonIgnore]
            public RoleEntity? Role { get; set; }
        }

        private class DomainStructure : StorageStructureInMemory
        {
            public List<UserEntity> Users { get; set; } = [];
            public List<RoleEntity> Roles { get; set; } = [];

            public DomainStructure()
                : base(registerFromProperties: true)
            {
            }
        }

        private class DomainFileStorage : DataStorageFile
        {
            public DomainFileStorage(DomainStructure structure, string? path = null)
                : base(structure, path)
            {
            }

            protected override void FixupRelationships()
            {
                // Явный fixup после Load: заполняем User.Role по RoleId.
                HydrateNavigation<UserEntity>(typeof(UserEntity).GetProperty(nameof(UserEntity.Role))!);
            }
        }

        private static void Seed(DataStorageInMemory storage)
        {
            storage.Add(new RoleEntity { Id = 1, Name = "Admin" });
            storage.Add(new RoleEntity { Id = 2, Name = "User" });
            storage.Add(new UserEntity { Id = 10, Name = "Alice", RoleId = 1 });
            storage.Add(new UserEntity { Id = 11, Name = "Bob", RoleId = 2 });
            storage.Add(new UserEntity { Id = 12, Name = "Eve", RoleId = 1 });
        }

        [Fact]
        public void Query_Include_HydratesReferenceNavigation()
        {
            var storage = new DataStorageInMemory(new DomainStructure());
            Seed(storage);

            Assert.Null(storage.Query<UserEntity>().First(x => x.Id == 10).Role);

            var users = storage.Query<UserEntity>(q => q.Include(u => u.Role)).ToArray();

            Assert.Equal("Admin", users.Single(x => x.Id == 10).Role!.Name);
            Assert.Equal("User", users.Single(x => x.Id == 11).Role!.Name);
            Assert.Same(
                users.Single(x => x.Id == 10).Role,
                users.Single(x => x.Id == 12).Role);
        }

        [Fact]
        public void Query_Include_HydratesCollectionNavigation()
        {
            var storage = new DataStorageInMemory(new DomainStructure());
            Seed(storage);

            var roles = storage.Query<RoleEntity>(q => q.Include(r => r.Users)).ToArray();

            var admin = roles.Single(x => x.Id == 1);
            Assert.Equal(2, admin.Users.Count);
            Assert.Contains(admin.Users, u => u.Name == "Alice");
            Assert.Contains(admin.Users, u => u.Name == "Eve");

            var userRole = roles.Single(x => x.Id == 2);
            Assert.Single(userRole.Users);
            Assert.Equal("Bob", userRole.Users[0].Name);
        }

        [Fact]
        public void Fixup_AfterLoad_FillsNavigations()
        {
            var path = Path.Combine(Path.GetTempPath(), $"lotus-include-{Guid.NewGuid():N}.json");
            try
            {
                var writer = new DomainFileStorage(new DomainStructure(), path);
                Seed(writer);
                writer.SaveChanges();

                var loaded = new DomainFileStorage(new DomainStructure(), path);
                loaded.Load(); // внутри вызовет FixupRelationships

                var alice = loaded.Query<UserEntity>().Single(x => x.Name == "Alice");
                Assert.NotNull(alice.Role);
                Assert.Equal("Admin", alice.Role!.Name);
                Assert.Equal(1, alice.RoleId);
            }
            finally
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        [Fact]
        public void Fixup_PublicMethod_CanBeCalledManually()
        {
            var storage = new DomainFileStorage(new DomainStructure());
            Seed(storage);

            Assert.Null(storage.Query<UserEntity>().First().Role);

            storage.Fixup();

            Assert.All(storage.Query<UserEntity>(), u => Assert.NotNull(u.Role));
            Assert.Equal("User", storage.Query<UserEntity>().Single(x => x.Name == "Bob").Role!.Name);
        }

        [Fact]
        public async ThreadingTask LoadAsync_CallsFixup()
        {
            var path = Path.Combine(Path.GetTempPath(), $"lotus-include-{Guid.NewGuid():N}.json");
            try
            {
                var writer = new DomainFileStorage(new DomainStructure(), path);
                Seed(writer);
                await writer.SaveChangesAsync();

                var loaded = new DomainFileStorage(new DomainStructure(), path);
                await loaded.LoadAsync();

                Assert.Equal("Admin", loaded.Query<UserEntity>().Single(x => x.Id == 12).Role!.Name);
            }
            finally
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
