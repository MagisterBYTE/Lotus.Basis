using System;

namespace Lotus.Core.ObjectPool
{
    /// <summary>
    /// Тесты для критических участков ObjectPool (исправление имени переменной poolObject).
    /// </summary>
    public class ObjectPoolCriticalTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        private class TestPoolObject : ILotusPoolObject
        {
            public int Value { get; set; }
            public bool OnPoolTakeCalled { get; private set; }
            public bool OnPoolReleaseCalled { get; private set; }

            public bool IsPoolObject => throw new NotImplementedException();

            public void OnPoolTake()
            {
                OnPoolTakeCalled = true;
            }

            public void OnPoolRelease()
            {
                OnPoolReleaseCalled = true;
            }
        }

        /// <summary>
        /// Тест Take - корректная работа с исправленным именем переменной (poolObject вместо poolObject).
        /// </summary>
        [Fact]
        public void Take_WithPoolObjects_ReturnsCorrectObject()
        {
            // Arrange
            var manager = new PoolManager<TestPoolObject>(5, () => new TestPoolObject { Value = 100 });

            // Act - используем метод с исправленным именем переменной
            var poolObject = manager.Take();

            // Assert - проверяем что объект корректно взят из пула
            Assert.NotNull(poolObject);
            Assert.Equal(100, poolObject.Value);
            Assert.True(poolObject.OnPoolTakeCalled, "OnPoolTake should be called");
            Assert.Equal(4, manager.InstanceCount);
        }

        /// <summary>
        /// Тест Take - автоматическое расширение пула при пустом пуле.
        /// </summary>
        [Fact]
        public void Take_WithEmptyPool_ResizesPool()
        {
            // Arrange
            var initialMaxInstances = 2;
            var manager = new PoolManager<TestPoolObject>(initialMaxInstances, () => new TestPoolObject());

            // Act - берем все объекты из пула и еще один (должно произойти расширение)
            var obj1 = manager.Take();
            var obj2 = manager.Take();
            var obj3 = manager.Take(); // Должно вызвать ResizePool

            // Assert
            Assert.NotNull(obj3);
            Assert.True(manager.MaxInstances > initialMaxInstances, "Pool should be resized");
        }

        /// <summary>
        /// Тест Release - корректная работа с исправленным именем переменной.
        /// </summary>
        [Fact]
        public void Release_WithPoolObject_ReturnsToPool()
        {
            // Arrange
            var manager = new PoolManager<TestPoolObject>(5, () => new TestPoolObject { Value = 100 });
            var poolObject = manager.Take();
            var initialCount = manager.InstanceCount;

            // Act - используем метод с исправленным именем переменной
            manager.Release(poolObject);

            // Assert
            Assert.True(poolObject.OnPoolReleaseCalled, "OnPoolRelease should be called");
            Assert.Equal(initialCount + 1, manager.InstanceCount);
        }

        /// <summary>
        /// Тест Take и Release - циклическое использование объектов.
        /// </summary>
        [Fact]
        public void TakeAndRelease_CyclicUsage_WorksCorrectly()
        {
            // Arrange
            var manager = new PoolManager<TestPoolObject>(3, () => new TestPoolObject { Value = 50 });

            // Act - берем и возвращаем объекты несколько раз
            var obj1 = manager.Take();
            obj1.Value = 200;
            manager.Release(obj1);

            var obj2 = manager.Take();
            obj2.Value = 300;
            manager.Release(obj2);

            var obj3 = manager.Take();

            // Assert - проверяем что объекты корректно переиспользуются
            Assert.Equal(2, manager.InstanceCount);
        }

        /// <summary>
        /// Тест PoolManagerBase - базовый функционал без OnPoolTake/OnPoolRelease.
        /// </summary>
        [Fact]
        public void PoolManagerBase_Take_WorksWithoutPoolObjectInterface()
        {
            // Arrange
            var manager = new PoolManagerBase<TestPoolObject>(3, () => new TestPoolObject { Value = 100 });

            // Act
            var poolObject = manager.Take();

            // Assert
            Assert.NotNull(poolObject);
            Assert.False(poolObject.OnPoolTakeCalled, "OnPoolTake should not be called in base class");
        }
    }
}
