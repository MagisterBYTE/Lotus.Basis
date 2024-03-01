namespace Lotus.Repository
{
    // Примеры тестов для класса RepositoryDbSet<TEntity, TKey>
    public class RepositoryDbSetTests : IClassFixture<DomainContextFixture>
    {
        private RepositoryDbSet<Person, int> _repository;

        public DomainContextFixture Fixture { get; }

        public RepositoryDbSetTests(DomainContextFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void Testing_Repository()
        {
            var context = Fixture.CreateContext();
            _repository = new RepositoryDbSet<Person, int>(context);

            // Arrange

            // Act
            var result = _repository.Query();

            // Assert
            Assert.NotNull(result);
            Assert.True(result is IQueryable<Person>);

            //
            // Add
            //

            // Arrange
            _repository.Add(new Person { Id = 1, Name = "Entity 1" });
            _repository.Flush();

            // Act
            var result1 = _repository.FirstOrDefault(null);

            // Assert
            Assert.NotNull(result1);
            Assert.Equal(1, result1.Id);

            //
            // Add
            //
            // Act
            _repository.Add(new Person { Id = 2, Name = "Entity 2" });
            _repository.Flush();

            // Assert
            Assert.Equal(2, _repository.Query().Count());
            Assert.True(_repository.Query().Any(e => e.Id == 2));

            //
            // Remove
            //
            // Arrange
            var entity = new Person { Id = 1, Name = "Entity 1" };

            // Act
            _repository.Remove(entity);
            _repository.Flush();

            // Assert
            Assert.Equal(1, _repository.Query().Count());

            //
            // Update
            //
            // Arrange
            var entityUpdate = new Person { Id = 2, Name = "Entity 23333" };

            // Act
            _repository.Update(entityUpdate);
            _repository.Flush();

            // Assert
            Assert.Equal(1, _repository.Query().Count());
            Assert.True(_repository.FirstOrDefault(null)!.Name == "Entity 23333");
        }
    }
}