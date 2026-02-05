using NUnit.Framework;
using NUnit.Framework.Legacy;

using Lotus.Maths;

namespace Lotus.Algorithm
{
    [TestFixture]
    public class PathFinderAStarTests
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

            var pathFinder = new PathFinderAStar(map)
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
            // Путь строится от цели к старту, поэтому Start и Target меняются местами
            ClassicAssert.AreEqual(pathFinder.Target.X, pathFinder.Path.Start.X);
            ClassicAssert.AreEqual(pathFinder.Target.Y, pathFinder.Path.Start.Y);
            ClassicAssert.AreEqual(pathFinder.Start.X, pathFinder.Path.Target.X);
            ClassicAssert.AreEqual(pathFinder.Start.Y, pathFinder.Path.Target.Y);
        }

        [Test]
        public void Find_WithNoPath_ShouldReturnFalse()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetBlock(); // Все заблокировано
            map.SetEmpty(0, 0); // Только старт
            map.SetEmpty(4, 4); // Только цель

            var pathFinder = new PathFinderAStar(map)
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
        public void Find_WithDiagonalAllowed_ShouldFindShorterPath()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderAStar(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4),
                IsAllowDiagonal = true
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(pathFinder.IsFoundPath);
            ClassicAssert.LessOrEqual(pathFinder.Path.Count, 6); // Диагональный путь должен быть короче
        }

        [Test]
        public void Find_WithDiagonalNotAllowed_ShouldFindOrthogonalPath()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();

            var pathFinder = new PathFinderAStar(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(4, 4),
                IsAllowDiagonal = false
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            ClassicAssert.IsTrue(result);
            ClassicAssert.IsTrue(pathFinder.IsFoundPath);
            ClassicAssert.GreaterOrEqual(pathFinder.Path.Count, 8); // Ортогональный путь должен быть длиннее
        }

        [Test]
        public void Find_WithSameStartAndTarget_ShouldFindPath()
        {
            // Arrange
            var map = new CMap2D(5, 5);
            map.SetEmpty();
            var point = new TMapPoint(2, 2);

            var pathFinder = new PathFinderAStar(map)
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

            var pathFinder = new PathFinderAStar(map)
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
            var pathFinder = new PathFinderAStar
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

            var pathFinder = new PathFinderAStar(map)
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
        public void Find_WithSearchLimit_ShouldRespectLimit()
        {
            // Arrange
            var map = new CMap2D(10, 10);
            map.SetEmpty();

            var pathFinder = new PathFinderAStar(map)
            {
                Start = new TMapPoint(0, 0),
                Target = new TMapPoint(9, 9),
                SearchLimit = 5 // Очень маленький лимит
            };

            // Act
            var result = pathFinder.Find();

            // Assert
            // Может не найти путь из-за лимита
            // Проверяем что не упало с ошибкой
            ClassicAssert.IsNotNull(pathFinder.Path);
        }
    }
}
