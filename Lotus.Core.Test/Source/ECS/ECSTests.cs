#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace Lotus.Core.ECS
{
    /// <summary>
    /// Статический класс для тестирования методов подсистемы ECS базового ядра.
    /// </summary>
    public static class ECSTests
    {
        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public struct TWeapon
        {
            public int Fire;
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public struct THealth
        {
            public int Live;
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public struct TPlayer
        {
            public int Id;
        }

        /// <summary>
        /// Служебный класс для тестирования.
        /// </summary>
        public struct TDeadStatus
        {
            public bool IsDead;
        }

        /// <summary>
        /// Тестирование методов подсистемы ECS.
        /// </summary>
        [Fact]
        public static void TestECS()
        {
            var world = new CEcsWorld();

            var pety = world.NewEntity();
            var sany = world.NewEntity();
            var igor = world.NewEntity();

            world.AddComponent<TWeapon>(pety.Id);
            world.AddComponent<THealth>(pety.Id);
            world.AddComponent<TPlayer>(pety.Id);

            world.AddComponent<TWeapon>(sany.Id);
            world.AddComponent<THealth>(sany.Id);

            world.AddComponent<THealth>(igor.Id);
            world.AddComponent<TPlayer>(igor.Id);

            var filterHealth = world.CreateFilterComponent();
            filterHealth.Include<THealth>().Include<TPlayer>();

            var filterEntities = filterHealth.GetEntities();
            Assert.Equal(filterHealth.CountEntities, 2);
            for (var i = 0; i < filterHealth.CountEntities; i++)
            {
                ref var health = ref world.GetComponent<THealth>(filterEntities[i]);
                health.Live = 325;

                ref var player = ref world.GetComponent<TPlayer>(filterEntities[i]);
                player.Id = 17;
            }

            filterHealth.Include<TDeadStatus>();
            filterEntities = filterHealth.GetEntities();
            Assert.Equal(filterHealth.CountEntities, 0);
            for (var i = 0; i < filterHealth.CountEntities; i++)
            {
                ref var health = ref world.GetComponent<THealth>(filterEntities[i]);
                health.Live = 325;

                ref var player = ref world.GetComponent<TPlayer>(filterEntities[i]);
                player.Id = 17;
            }

            Assert.Equal(world.HasComponent<TWeapon>(pety.Id), true);
            Assert.Equal(world.HasComponent<THealth>(pety.Id), true);
            Assert.Equal(world.HasComponent<TPlayer>(pety.Id), true);

            Assert.Equal(world.HasComponent<TWeapon>(sany.Id), true);
            Assert.Equal(world.HasComponent<THealth>(sany.Id), true);

            Assert.Equal(world.HasComponent<THealth>(igor.Id), true);
            Assert.Equal(world.HasComponent<TPlayer>(igor.Id), true);
        }
    }
}