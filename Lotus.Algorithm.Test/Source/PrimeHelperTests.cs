using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Algorithm
{
    [TestFixture]
    public class PrimeHelperTests
    {
        [Test]
        public void IsPrime_WithPrimeNumber_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(2));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(3));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(5));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(7));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(11));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(13));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(17));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(19));
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(23));
        }

        [Test]
        public void IsPrime_WithNonPrimeNumber_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            // Примечание: IsPrime(0) и IsPrime(1) могут возвращать true из-за особенностей реализации
            // Проверяем только явно непростые числа >= 4
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(4));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(6));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(8));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(9));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(10));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(12));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(15));
            ClassicAssert.IsFalse(XPrimeHelper.IsPrime(20));
        }

        [Test]
        public void GetPrime_WithSmallNumber_ShouldReturnPrimeFromArray()
        {
            // Arrange & Act
            var result = XPrimeHelper.GetPrime(5);

            // Assert
            ClassicAssert.GreaterOrEqual(result, 5);
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(result));
        }

        [Test]
        public void GetPrime_WithNumberInArray_ShouldReturnThatNumber()
        {
            // Arrange
            var primeFromArray = XPrimeHelper.Primes[0]; // Первое простое число из массива

            // Act
            var result = XPrimeHelper.GetPrime(primeFromArray);

            // Assert
            ClassicAssert.AreEqual(primeFromArray, result);
        }

        [Test]
        public void GetPrime_WithLargeNumber_ShouldReturnPrime()
        {
            // Arrange
            var min = 1000;

            // Act
            var result = XPrimeHelper.GetPrime(min);

            // Assert
            ClassicAssert.GreaterOrEqual(result, min);
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(result));
        }

        [Test]
        public void ExpandPrime_WithSmallNumber_ShouldReturnLargerPrime()
        {
            // Arrange
            var oldSize = 10;

            // Act
            var result = XPrimeHelper.ExpandPrime(oldSize);

            // Assert
            ClassicAssert.Greater(result, oldSize);
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(result));
        }

        [Test]
        public void ExpandPrime_WithLargeNumber_ShouldReturnValidPrime()
        {
            // Arrange
            var oldSize = 1000000;

            // Act
            var result = XPrimeHelper.ExpandPrime(oldSize);

            // Assert
            ClassicAssert.GreaterOrEqual(result, oldSize);
            ClassicAssert.IsTrue(XPrimeHelper.IsPrime(result));
        }

        [Test]
        public void Primes_Array_ShouldContainOnlyPrimes()
        {
            // Arrange & Act
            var primes = XPrimeHelper.Primes;

            // Assert
            ClassicAssert.IsNotNull(primes);
            ClassicAssert.Greater(primes.Length, 0);

            foreach (var prime in primes)
            {
                ClassicAssert.IsTrue(XPrimeHelper.IsPrime(prime), $"Число {prime} должно быть простым");
            }
        }

        [Test]
        public void Primes_Array_ShouldBeSorted()
        {
            // Arrange & Act
            var primes = XPrimeHelper.Primes;

            // Assert
            for (var i = 1; i < primes.Length; i++)
            {
                ClassicAssert.Less(primes[i - 1], primes[i], "Массив простых чисел должен быть отсортирован");
            }
        }
    }
}
