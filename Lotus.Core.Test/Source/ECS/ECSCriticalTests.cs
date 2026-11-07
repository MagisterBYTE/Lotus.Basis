using System;

namespace Lotus.Core.ECS
{
    /// <summary>
    /// Тесты для критических участков ECS системы.
    /// </summary>
    public class ECSCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public struct TTestComponent
        {
            public int Value;
        }

        /// <summary>
        /// Тест GetEntity - возвращает dummy entity для несуществующего id.
        /// </summary>
        [Fact]
        public void GetEntity_WithNonExistentId_ReturnsDummyEntity()
        {
            var world = new CEcsWorld();
            var entity = world.NewEntity();

            var nonExistentId = entity.Id + 1000;
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                ref var result = ref world.GetEntity(nonExistentId);
            });
        }

        /// <summary>
        /// Тест GetEntity - возвращает правильную сущность для существующего id.
        /// </summary>
        [Fact]
        public void GetEntity_WithExistentId_ReturnsCorrectEntity()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var result = ref world.GetEntity(entity.Id);

            Assert.Equal(entity.Id, result.Id);
        }

        /// <summary>
        /// Тест RemoveEntity - корректно удаляет сущность и обновляет индексы.
        /// </summary>
        [Fact]
        public void RemoveEntity_RemovesEntityAndUpdatesIndices()
        {
            var world = new CEcsWorld();
            ref var entity1 = ref world.NewEntity();
            ref var entity2 = ref world.NewEntity();
            ref var entity3 = ref world.NewEntity();

            var id1 = entity1.Id;
            var id2 = entity2.Id;
            var id3 = entity3.Id;

            Assert.Equal(3, world.CountEntity);
            Assert.True(world.ContainsEntity(id2));

            world.RemoveEntity(id2);

            Assert.Equal(2, world.CountEntity);
            Assert.False(world.ContainsEntity(id2));
            Assert.True(world.ContainsEntity(id1));
            Assert.True(world.ContainsEntity(id3));
        }

        /// <summary>
        /// Тест AddComponent - выбрасывает исключение при попытке добавить существующий компонент.
        /// </summary>
        [Fact]
        public void AddComponent_WithExistingComponent_ThrowsException()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            ref var component = ref world.GetComponent<TTestComponent>(entity.Id);
            component.Value = 100;
            var entityId = entity.Id;

            Assert.Throws<Exception>(() =>
            {
                world.AddComponent<TTestComponent>(entityId);
            });
        }

        /// <summary>
        /// Тест GetComponent - выбрасывает исключение для несуществующего компонента.
        /// </summary>
        [Fact]
        public void GetComponent_WithNonExistentComponent_ThrowsException()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();
            var entityId = entity.Id;

            Assert.Throws<Exception>(() =>
            {
                ref var component = ref world.GetComponent<TTestComponent>(entityId);
            });
        }

        /// <summary>
        /// Тест GetOrAddComponent - возвращает существующий компонент.
        /// </summary>
        [Fact]
        public void GetOrAddComponent_WithExistingComponent_ReturnsExisting()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var component1 = ref world.GetOrAddComponent<TTestComponent>(entity.Id);
            component1.Value = 100;

            ref var component2 = ref world.GetOrAddComponent<TTestComponent>(entity.Id);

            Assert.Equal(100, component2.Value);
            Assert.Equal(1, world.GetComponentData<TTestComponent>()!.Count);
        }

        /// <summary>
        /// Тест GetOrAddComponent - создает новый компонент если не существует.
        /// </summary>
        [Fact]
        public void GetOrAddComponent_WithNonExistentComponent_CreatesNew()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var component = ref world.GetOrAddComponent<TTestComponent>(entity.Id);
            component.Value = 200;

            Assert.Equal(200, component.Value);
            Assert.True(world.HasComponent<TTestComponent>(entity.Id));
        }

        /// <summary>
        /// Тест RemoveComponent - корректно удаляет компонент и обновляет счетчик.
        /// </summary>
        [Fact]
        public void RemoveComponent_RemovesComponentAndUpdatesCount()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            Assert.True(world.HasComponent<TTestComponent>(entity.Id));
            Assert.Equal(1, entity.ComponentCount);

            world.RemoveComponent<TTestComponent>(entity.Id);
            Assert.False(world.HasComponent<TTestComponent>(entity.Id));
            Assert.Equal(0, entity.ComponentCount);
        }

        /// <summary>
        /// Тест RemoveEntity - корректно обрабатывает удаление сущности с компонентами.
        /// </summary>
        [Fact]
        public void RemoveEntity_WithComponents_HandlesCorrectly()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            Assert.True(world.HasComponent<TTestComponent>(entity.Id));

            world.RemoveEntity(entity.Id);
            Assert.False(world.ContainsEntity(entity.Id));
        }

        /// <summary>
        /// Тест ContainsEntity - корректно проверяет существование сущности.
        /// </summary>
        [Fact]
        public void ContainsEntity_ChecksExistenceCorrectly()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            Assert.True(world.ContainsEntity(entity.Id));
        }
    }
}
