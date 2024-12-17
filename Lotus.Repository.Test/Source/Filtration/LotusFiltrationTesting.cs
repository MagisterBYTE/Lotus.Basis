namespace Lotus.Repository
{
    /// <summary>
    /// Класс для тестирования методов фильтрации.
    /// </summary>
    public class FiltrationTesting : IClassFixture<DomainContextFixture>
    {
        public DomainContextFixture Fixture { get; }

        public FiltrationTesting(DomainContextFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void TestFiltrationString()
        {
            var context = Fixture.CreateContext();

            var propertyName = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Name),

                //
                // TFilterFunction.Equals
                //

                Value = "canEditUser",
                Function = TFilterFunction.Equals
            };

            var result = context.Permissions.Filter(propertyName).ToArray();

            Assert.Single(result);
            Assert.Equal("canEditUser", result[0].Name);


            //
            // TFilterFunction.NotEqual
            //
            propertyName.Value = "canEditUser";
            propertyName.Function = TFilterFunction.NotEqual;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(8, result.Length);

            //
            // TFilterFunction.Contains
            //
            propertyName.Value = "can";
            propertyName.Function = TFilterFunction.Contains;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(4, result.Length);

            //
            // TFilterFunction.StartsWith
            //
            propertyName.Value = "can";
            propertyName.Function = TFilterFunction.StartsWith;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(4, result.Length);

            //
            // TFilterFunction.EndsWith
            //
            propertyName.Value = "User";
            propertyName.Function = TFilterFunction.EndsWith;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(2, result.Length);

            //
            // TFilterFunction.NotEmpty
            //
            propertyName.Value = "";
            propertyName.Function = TFilterFunction.NotEmpty;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(7, result.Length);
        }

        [Fact]
        public void TestFiltrationArray()
        {
            var context = Fixture.CreateContext();

            var propertyName = new FilterByProperty
            {
                PropertyPath = "Permissions",

                //
                // TFilterFunction.IncludeAny
                //

                Values = ["5"],
                Function = TFilterFunction.IncludeAny
            };

            var result = context.Roles.Filter(propertyName).OrderBy(x => x.Id).ToArray();

            Assert.Equal(4, result.Length);
            Assert.Equal("admin", result[0].Name);
            Assert.Equal("editor", result[1].Name);
            Assert.Equal("user", result[2].Name);
            Assert.Equal("guest", result[3].Name);

            propertyName.Values = ["4", "2"];
            propertyName.Function = TFilterFunction.IncludeAny;

            result = [.. context.Roles.Filter(propertyName).OrderBy(x => x.Id)];

            Assert.Equal(3, result.Length);
            Assert.Equal("admin", result[0].Name);
            Assert.Equal("editor", result[1].Name);
            Assert.Equal("guest", result[2].Name);

            //
            // TFilterFunction.IncludeEquals
            //

            propertyName.Values = ["4", "5"];
            propertyName.Function = TFilterFunction.IncludeEquals;

            result = [.. context.Roles.Filter(propertyName).OrderBy(x => x.Id)];

            Assert.Single(result);

            propertyName.Values = ["4"];
            propertyName.Function = TFilterFunction.IncludeEquals;

            result = [.. context.Roles.Filter(propertyName).OrderBy(x => x.Id)];

            Assert.Empty(result);

            //
            // TFilterFunction.IncludeNone
            //

            propertyName.Values = ["4"];
            propertyName.Function = TFilterFunction.IncludeNone;

            result = [.. context.Roles.Filter(propertyName).OrderBy(x => x.Id)];

            Assert.Equal(2, result.Length);
            Assert.Equal("editor", result[0].Name);
            Assert.Equal("user", result[1].Name);

            propertyName.Values = ["4", "2"];
            propertyName.Function = TFilterFunction.IncludeNone;

            result = [.. context.Roles.Filter(propertyName).OrderBy(x => x.Id)];

            Assert.Single(result);
            Assert.Equal("user", result[0].Name);
        }

        [Fact]
        public void TestFiltrationNumber()
        {
            var context = Fixture.CreateContext();

            var propertyName = new FilterByProperty
            {
                PropertyPath = nameof(Permission.Id),

                //
                // TFilterFunction.Equals
                //

                Value = "1",
                Function = TFilterFunction.Equals
            };

            var result = context.Permissions.Filter(propertyName).ToArray();

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);


            //
            // TFilterFunction.NotEqual
            //
            propertyName.Value = "2";
            propertyName.Function = TFilterFunction.NotEqual;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(8, result.Length);

            //
            // TFilterFunction.LessThan
            //
            propertyName.Value = "3";
            propertyName.Function = TFilterFunction.LessThan;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(2, result.Length);

            //
            // TFilterFunction.Between
            //
            propertyName.Values = ["4", "8"];
            propertyName.Function = TFilterFunction.Between;

            result = [.. context.Permissions.Filter(propertyName)];

            Assert.Equal(3, result.Length);
        }
    }
}