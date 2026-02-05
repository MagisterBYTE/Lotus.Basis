using System;
using System.Collections.Generic;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Lotus.Maths;

namespace Lotus.Algorithm
{
    [TestFixture]
    public class FloodVisitTests
    {
        [Test]
        public void FloodVisit4_WithValidInput_ShouldVisitAllConnectedCells()
        {
            // Arrange
            var massive = new int[,]
            {
                { 1, 1, 0, 2 },
                { 1, 1, 0, 2 },
                { 0, 0, 0, 2 },
                { 3, 3, 3, 2 }
            };
            var visited = new List<(int x, int y)>();
            var start = new Vector2Di(0, 0);

            // Act
            massive.FloodVisit4(start, (x, y) => visited.Add((x, y)));

            // Assert
            ClassicAssert.AreEqual(4, visited.Count); // Должно посетить 4 ячейки со значением 1
            ClassicAssert.Contains((0, 0), visited);
            ClassicAssert.Contains((1, 0), visited);
            ClassicAssert.Contains((0, 1), visited);
            ClassicAssert.Contains((1, 1), visited);
        }

        [Test]
        public void FloodVisit4_WithDifferentStart_ShouldVisitCorrectRegion()
        {
            // Arrange
            var massive = new int[,]
            {
                { 1, 1, 0, 2 },
                { 1, 1, 0, 2 },
                { 0, 0, 0, 2 },
                { 3, 3, 3, 2 }
            };
            var visited = new List<(int x, int y)>();
            var start = new Vector2Di(3, 3); // Стартовая точка со значением 2

            // Act
            massive.FloodVisit4(start, (x, y) => visited.Add((x, y)));

            // Assert
            ClassicAssert.GreaterOrEqual(visited.Count, 3); // Должно посетить ячейки со значением 2
            ClassicAssert.Contains((3, 3), visited);
            // Проверяем что все посещенные ячейки имеют значение 2
            var startValue = massive[start.X, start.Y];
            foreach (var (x, y) in visited)
            {
                ClassicAssert.AreEqual(startValue, massive[x, y], $"Ячейка ({x}, {y}) должна иметь значение {startValue}");
            }
        }

        [Test]
        public void FloodVisit8_WithValidInput_ShouldVisitAllConnectedCellsIncludingDiagonals()
        {
            // Arrange
            var massive = new int[,]
            {
                { 1, 0, 1 },
                { 0, 1, 0 },
                { 1, 0, 1 }
            };
            var visited = new List<(int x, int y)>();
            var start = new Vector2Di(0, 0);

            // Act
            massive.FloodVisit8(start, (x, y) => visited.Add((x, y)));

            // Assert
            ClassicAssert.GreaterOrEqual(visited.Count, 5); // Должно посетить все ячейки со значением 1 (включая диагонали)
            ClassicAssert.Contains((0, 0), visited);
            ClassicAssert.Contains((1, 1), visited);
            ClassicAssert.Contains((2, 2), visited);
        }

        [Test]
        public void FloodVisit8_ShouldNotVisitDifferentValues()
        {
            // Arrange
            var massive = new int[,]
            {
                { 1, 2, 1 },
                { 2, 1, 2 },
                { 1, 2, 1 }
            };
            var visited = new List<(int x, int y)>();
            var start = new Vector2Di(0, 0);

            // Act
            massive.FloodVisit8(start, (x, y) => visited.Add((x, y)));

            // Assert
            // Должно посетить только ячейки со значением 1
            foreach (var (x, y) in visited)
            {
                ClassicAssert.AreEqual(1, massive[x, y], $"Ячейка ({x}, {y}) должна иметь значение 1");
            }
        }

        [Test]
        public void FloodVisit8_WithCustomComparer_ShouldWorkCorrectly()
        {
            // Arrange
            var massive = new string[,]
            {
                { "A", "B", "A" },
                { "B", "A", "B" },
                { "A", "B", "A" }
            };
            var visited = new List<(int x, int y)>();
            var start = new Vector2Di(0, 0);
            var comparer = StringComparer.OrdinalIgnoreCase;

            // Act
            massive.FloodVisit8(start, (x, y) => visited.Add((x, y)), comparer);

            // Assert
            ClassicAssert.GreaterOrEqual(visited.Count, 5); // Должно посетить все ячейки со значением "A" или "a"
            ClassicAssert.Contains((0, 0), visited);
        }

        [Test]
        public void FloodVisit4_WithOutOfBoundsStart_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var massive = new int[,] { { 1, 2 }, { 3, 4 } };
            var start = new Vector2Di(10, 10);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                massive.FloodVisit4(start, (x, y) => { });
            });
        }

        [Test]
        public void FloodVisit8_WithOutOfBoundsStart_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var massive = new int[,] { { 1, 2 }, { 3, 4 } };
            var start = new Vector2Di(-1, -1);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                massive.FloodVisit8(start, (x, y) => { });
            });
        }

        [Test]
        public void FloodVisit4_WithNullMassive_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[,]? massive = null;
            var start = new Vector2Di(0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                massive!.FloodVisit4(start, (x, y) => { });
            });
        }

        [Test]
        public void FloodVisit8_WithNullMassive_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[,]? massive = null;
            var start = new Vector2Di(0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                massive!.FloodVisit8(start, (x, y) => { });
            });
        }

        [Test]
        public void FloodVisit4_WithNullDelegate_ShouldThrowArgumentNullException()
        {
            // Arrange
            var massive = new int[,] { { 1, 2 }, { 3, 4 } };
            var start = new Vector2Di(0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                massive.FloodVisit4(start, null!);
            });
        }

        [Test]
        public void FloodVisit8_WithNullDelegate_ShouldThrowArgumentNullException()
        {
            // Arrange
            var massive = new int[,] { { 1, 2 }, { 3, 4 } };
            var start = new Vector2Di(0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                massive.FloodVisit8(start, null!);
            });
        }
    }
}
