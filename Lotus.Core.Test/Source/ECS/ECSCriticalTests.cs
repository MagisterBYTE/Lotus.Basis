using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.ECS
{
    /// <summary>
    /// Тесты для критических участков ECS системы.
    /// </summary>
    [TestFixture]
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
        [Test]
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
        [Test]
        public void GetEntity_WithExistentId_ReturnsCorrectEntity()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var result = ref world.GetEntity(entity.Id);

            ClassicAssert.AreEqual(entity.Id, result.Id);
        }

        /// <summary>
        /// Тест RemoveEntity - корректно удаляет сущность и обновляет индексы.
        /// </summary>
        [Test]
        public void RemoveEntity_RemovesEntityAndUpdatesIndices()
        {
            var world = new CEcsWorld();
            ref var entity1 = ref world.NewEntity();
            ref var entity2 = ref world.NewEntity();
            ref var entity3 = ref world.NewEntity();

            var id1 = entity1.Id;
            var id2 = entity2.Id;
            var id3 = entity3.Id;

            ClassicAssert.AreEqual(3, world.CountEntity);
            ClassicAssert.IsTrue(world.ContainsEntity(id2));

            world.RemoveEntity(id2);

            ClassicAssert.AreEqual(2, world.CountEntity);
            ClassicAssert.IsFalse(world.ContainsEntity(id2));
            ClassicAssert.IsTrue(world.ContainsEntity(id1));
            ClassicAssert.IsTrue(world.ContainsEntity(id3));
        }

        /// <summary>
        /// Тест AddComponent - выбрасывает исключение при попытке добавить существующий компонент.
        /// </summary>
        [Test]
        public void AddComponent_WithExistingComponent_ThrowsException()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            ref var component = ref world.GetComponent<TTestComponent>(entity.Id);
            component.Value = 100;
            var entityId = entity.Id;

            ClassicAssert.Throws<Exception>(() =>
            {
                world.AddComponent<TTestComponent>(entityId);
            });
        }

        /// <summary>
        /// Тест GetComponent - выбрасывает исключение для несуществующего компонента.
        /// </summary>
        [Test]
        public void GetComponent_WithNonExistentComponent_ThrowsException()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();
            var entityId = entity.Id;

            ClassicAssert.Throws<Exception>(() =>
            {
                ref var component = ref world.GetComponent<TTestComponent>(entityId);
            });
        }

        /// <summary>
        /// Тест GetOrAddComponent - возвращает существующий компонент.
        /// </summary>
        [Test]
        public void GetOrAddComponent_WithExistingComponent_ReturnsExisting()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var component1 = ref world.GetOrAddComponent<TTestComponent>(entity.Id);
            component1.Value = 100;

            ref var component2 = ref world.GetOrAddComponent<TTestComponent>(entity.Id);

            ClassicAssert.AreEqual(100, component2.Value);
            ClassicAssert.AreEqual(1, world.GetComponentData<TTestComponent>()!.Count);
        }

        /// <summary>
        /// Тест GetOrAddComponent - создает новый компонент если не существует.
        /// </summary>
        [Test]
        public void GetOrAddComponent_WithNonExistentComponent_CreatesNew()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ref var component = ref world.GetOrAddComponent<TTestComponent>(entity.Id);
            component.Value = 200;

            ClassicAssert.AreEqual(200, component.Value);
            ClassicAssert.IsTrue(world.HasComponent<TTestComponent>(entity.Id));
        }

        /// <summary>
        /// Тест RemoveComponent - корректно удаляет компонент и обновляет счетчик.
        /// </summary>
        [Test]
        public void RemoveComponent_RemovesComponentAndUpdatesCount()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            ClassicAssert.IsTrue(world.HasComponent<TTestComponent>(entity.Id));
            ClassicAssert.AreEqual(1, entity.ComponentCount);

            world.RemoveComponent<TTestComponent>(entity.Id);
            ClassicAssert.IsFalse(world.HasComponent<TTestComponent>(entity.Id));
            ClassicAssert.AreEqual(0, entity.ComponentCount);
        }

        /// <summary>
        /// Тест RemoveEntity - корректно обрабатывает удаление сущности с компонентами.
        /// </summary>
        [Test]
        public void RemoveEntity_WithComponents_HandlesCorrectly()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            world.AddComponent<TTestComponent>(entity.Id);
            ClassicAssert.IsTrue(world.HasComponent<TTestComponent>(entity.Id));

            world.RemoveEntity(entity.Id);
            ClassicAssert.IsFalse(world.ContainsEntity(entity.Id));
        }

        /// <summary>
        /// Тест ContainsEntity - корректно проверяет существование сущности.
        /// </summary>
        [Test]
        public void ContainsEntity_ChecksExistenceCorrectly()
        {
            var world = new CEcsWorld();
            ref var entity = ref world.NewEntity();

            ClassicAssert.IsTrue(world.ContainsEntity(entity.Id));
        }
    }
}
