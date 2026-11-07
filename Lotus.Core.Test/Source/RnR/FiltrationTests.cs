using System.Collections.Generic;
using System.Linq;

namespace Lotus.Core.RnR
{
    /// <summary>
    /// Тесты фильтрации <see cref="XFilterQueryableExtension"/> без зависимости от EF Core.
    /// </summary>
    public class FiltrationTests
    {
        private class Permission : ILotusIdentifierId<int>
        {
            public int Id { get; set; }
            public string? Name { get; set; }

            public Permission()
            {
            }

            public Permission(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        private class Role : ILotusIdentifierId<int>
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public List<Permission> Permissions { get; set; } = [];

            public Role()
            {
            }

            public Role(int id, string name, params Permission[] permissions)
            {
                Id = id;
                Name = name;
                Permissions.AddRange(permissions);
            }
        }

        private static List<Permission> CreatePermissions()
        {
            return
            [
                new Permission(1, "admin"),
                new Permission(2, "editor"),
                new Permission(3, "user"),
                new Permission(4, "canCreateUser"),
                new Permission(5, "canEditUser"),
                new Permission(6, "canWorldCreate"),
                new Permission(7, "canWorldCreate"),
                new Permission(8, ""),
                new Permission(9, ""),
            ];
        }

        private static List<Role> CreateRoles(List<Permission> permissions)
        {
            return
            [
                new Role(1, "admin", permissions[0], permissions[3], permissions[4]),
                new Role(2, "editor", permissions[1], permissions[4]),
                new Role(3, "user", permissions[2], permissions[4], permissions[5], permissions[6]),
                new Role(4, "guest", permissions[3], permissions[4]),
            ];
        }

        [Fact]
        public void Filter_StringFunctions()
        {
            var permissions = CreatePermissions().AsQueryable();

            var propertyName = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Name),
                Value = "canEditUser",
                Function = TFilterFunction.Equals,
            };

            var result = permissions.Filter(propertyName).ToArray();

            Assert.Single(result);
            Assert.Equal("canEditUser", result[0].Name);

            propertyName.Value = "canEditUser";
            propertyName.Function = TFilterFunction.NotEqual;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(8, result.Length);

            propertyName.Value = "can";
            propertyName.Function = TFilterFunction.Contains;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(4, result.Length);

            propertyName.Value = "can";
            propertyName.Function = TFilterFunction.StartsWith;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(4, result.Length);

            propertyName.Value = "User";
            propertyName.Function = TFilterFunction.EndsWith;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(2, result.Length);

            propertyName.Value = "";
            propertyName.Function = TFilterFunction.NotEmpty;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(7, result.Length);
        }

        [Fact]
        public void Filter_CollectionFunctions()
        {
            var permissions = CreatePermissions();
            var roles = CreateRoles(permissions).AsQueryable();

            var propertyName = new FilterByProperty
            {
                PropertyPath = "Permissions",
                Values = ["5"],
                Function = TFilterFunction.IncludeAny,
            };

            var result = roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

            Assert.Equal(4, result.Length);
            Assert.Equal("admin", result[0].Name);
            Assert.Equal("editor", result[1].Name);
            Assert.Equal("user", result[2].Name);
            Assert.Equal("guest", result[3].Name);

            propertyName.Values = ["4", "2"];
            propertyName.Function = TFilterFunction.IncludeAny;
            result = [.. roles.Filter(propertyName).OrderBy(x => x.Id)];
            Assert.Equal(3, result.Length);
            Assert.Equal("admin", result[0].Name);
            Assert.Equal("editor", result[1].Name);
            Assert.Equal("guest", result[2].Name);

            propertyName.Values = ["4", "5"];
            propertyName.Function = TFilterFunction.IncludeEquals;
            result = [.. roles.Filter(propertyName).OrderBy(x => x.Id)];
            Assert.Single(result);

            propertyName.Values = ["4"];
            propertyName.Function = TFilterFunction.IncludeEquals;
            result = [.. roles.Filter(propertyName).OrderBy(x => x.Id)];
            Assert.Empty(result);

            propertyName.Values = ["4"];
            propertyName.Function = TFilterFunction.IncludeNone;
            result = [.. roles.Filter(propertyName).OrderBy(x => x.Id)];
            Assert.Equal(2, result.Length);
            Assert.Equal("editor", result[0].Name);
            Assert.Equal("user", result[1].Name);

            propertyName.Values = ["4", "2"];
            propertyName.Function = TFilterFunction.IncludeNone;
            result = [.. roles.Filter(propertyName).OrderBy(x => x.Id)];
            Assert.Single(result);
            Assert.Equal("user", result[0].Name);
        }

        [Fact]
        public void Filter_NumberFunctions()
        {
            var permissions = CreatePermissions().AsQueryable();

            var propertyName = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Id),
                Value = "1",
                Function = TFilterFunction.Equals,
            };

            var result = permissions.Filter(propertyName).ToArray();

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);

            propertyName.Value = "2";
            propertyName.Function = TFilterFunction.NotEqual;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(8, result.Length);

            propertyName.Value = "3";
            propertyName.Function = TFilterFunction.LessThan;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(2, result.Length);

            propertyName.Values = ["4", "8"];
            propertyName.Function = TFilterFunction.Between;
            result = [.. permissions.Filter(propertyName)];
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void Filter_ReturnsOriginalQueryWhenFiltersAreEmpty()
        {
            var permissions = CreatePermissions().AsQueryable();

            var result = permissions.Filter().ToArray();

            Assert.Equal(9, result.Length);
        }

        [Fact]
        public void FilterByProperty_IsValid_RequiresValuesForIncludeFunctions()
        {
            var filter = new FilterByProperty
            {
                PropertyPath = "Permissions",
                Function = TFilterFunction.IncludeAny,
            };

            Assert.False(filter.IsValid());

            filter.Values = ["1"];
            Assert.True(filter.IsValid());
        }

        [Fact]
        public void FilterByProperty_IsValid_AllowsEmptyAndNotEmptyWithoutValue()
        {
            var empty = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Name),
                Function = TFilterFunction.Empty,
            };
            var notEmpty = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Name),
                Function = TFilterFunction.NotEmpty,
            };

            Assert.True(empty.IsValid());
            Assert.True(notEmpty.IsValid());
        }
    }
}
