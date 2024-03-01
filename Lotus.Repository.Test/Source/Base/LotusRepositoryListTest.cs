using Lotus.Core;

namespace Lotus.Repository
{
    public class RepositoryListTests
    {
        private class MyEntity : ILotusIdentifierId<Guid>, ILotusNameable
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void Constructor_SetsList()
        {
            // Arrange
            var list = new List<MyEntity>();

            // Act
            var repository = new RepositoryList<MyEntity, Guid>(list);

            // Assert
            Assert.Same(list, repository._list);
        }

        [Fact]
        public void SetList_SetsNewList()
        {
            // Arrange
            var repository = new RepositoryList<MyEntity, Guid>(new List<MyEntity>());
            var newList = new List<MyEntity>();

            // Act
            repository.SetList(newList);

            // Assert
            Assert.Same(newList, repository._list);
        }

        [Fact]
        public void Query_ReturnsQueryableList()
        {
            // Arrange
            var list = new List<MyEntity>();
            var repository = new RepositoryList<MyEntity, Guid>(list);

            // Act
            var queryable = repository.Query();

            // Assert
            Assert.Equal(list, queryable.ToList());
        }


        [Fact]
        public void FirstOrDefault_ReturnsFirstEntityWhenPredicateIsNullAndListNotEmpty()
        {
            // Arrange
            var entity1 = new MyEntity();
            var entity2 = new MyEntity();
            var repository = new RepositoryList<MyEntity, Guid>(new List<MyEntity> { entity1, entity2 });

            // Act
            var result = repository.FirstOrDefault(null);

            // Assert
            Assert.Same(entity1, result);
        }

        [Fact]
        public void FirstOrDefault_ReturnsNullWhenPredicateIsNullAndListIsEmpty()
        {
            // Arrange
            var repository = new RepositoryList<MyEntity, Guid>(new List<MyEntity>());

            // Act
            var result = repository.FirstOrDefault(null);

            // Assert
            Assert.Null(result);
        }
    }
}