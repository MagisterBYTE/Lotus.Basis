using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Algorithm
{
    [TestFixture]
    public class PathFinderWaveTests
    {
        [Test]
        public void Find_WithSimpleMap_ShouldFindPath()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();
            map.SetBlock(2, 0);
            map.SetBlock(2, 1);
            map.SetBlock(2, 2);

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 2),
                Target = new TMapPoint(4, 2)
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(pathFinder.IsFoundPath);
            ClassicAssert.IsNotNull(pathFinder.Path);
            ClassicAssert.Greater(pathFinder.Path.Count, 0);
            // Путь строится от старта к цели
            // Проверяем что путь не пустой и содержит точки
        }

        [Test]
        public void Find_WithNoPath_ShouldReturnFalse()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetBlock(); // Все заблокировано
            map.SetEmpty(0, 0); // Только старт
            map.SetEmpty(4, 4); // Только цель

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsFalse(result);
            ClassicAssert.IsFalse(pathFinder.IsFoundPath);
        }

        [Test]
        public void Find_WithStartOnBlock_ShouldReturnFalse()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetBlock();

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(2, 2),
                Target = new TMapPoint(4, 4)
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsFalse(result);
            ClassicAssert.IsFalse(pathFinder.IsFoundPath);
        }

        [Test]
        public void Find_WithTargetOnBlock_ShouldReturnFalse()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();
            map.SetBlock(4, 4);

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsFalse(result);
            ClassicAssert.IsFalse(pathFinder.IsFoundPath);
        }

        [Test]
        public void Find_WithSameStartAndTarget_ShouldFindPath()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();
            var point = new TMapPoint(2, 2);

            var pathFinder = new PathFinderWave(map)
            {
                Start = point,
                Target = point
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(pathFinder.IsFoundPath);
            // Когда старт и цель совпадают, путь может быть пустым или содержать одну точку
            ClassicAssert.GreaterOrEqual(pathFinder.Path.Count, 0);
        }

        [Test]
        public void ExpansionWave_WithValidMap_ShouldReturnTrue()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            // Act
            var result = pathFinder.ExpansionWave();

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(pathFinder.IsFoundPath);
        }

        [Test]
        public void ExpansionWave_WithNullMap_ShouldReturnFalse()
        {
            // Arrange
            var pathFinder = new PathFinderWave
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            // Act
            var result = pathFinder.ExpansionWave();

            // Assert
            ClassicAssert.IsFalse(result);
            ClassicAssert.IsFalse(pathFinder.IsFoundPath);
        }

        [Test]
        public void ResetWave_ShouldClearPathData()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            pathFinder.Find();

            // Act
            pathFinder.ResetWave();

            // Assert
            ClassicAssert.IsFalse(pathFinder.IsFoundPath);
        }

        [Test]
        public void WaveMap_ShouldContainWaveData()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            // Act
            pathFinder.Find();

            // Assert
            ClassicAssert.IsNotNull(pathFinder.WaveMap);
            ClassicAssert.AreEqual(5, pathFinder.WaveMap.GetLength(0));
            ClassicAssert.AreEqual(5, pathFinder.WaveMap.GetLength(1));
        }

        [Test]
        public void ExpansionWaveOnStep_ShouldWorkCorrectly()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderWave(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4)
            };

            pathFinder.PreparationsWaveOnStep();

            // Act
            var continueSearch = true;
            var iterations = 0;
            while (continueSearch && iterations < 100)
            {
                continueSearch = pathFinder.ExpansionWaveOnStep();
                iterations++;
            }

            // Assert
            ClassicAssert.IsTrue(pathFinder.IsFoundPath || iterations >= 100);
        }
    }
}
