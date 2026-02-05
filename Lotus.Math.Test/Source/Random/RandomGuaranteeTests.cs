using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Maths
{
    [TestFixture]
    public class RandomGuaranteeTests
    {
        private ILotusRandom _rng;
        private RandomGuarantee _guarantee;

        [SetUp]
        public void Setup()
        {
            // Используем исправленный RandomShift для тестов
            _rng = new RandomShift(12345); // Фиксированный сид для воспроизводимости
            _guarantee = new RandomGuarantee(_rng, 100);
        }

        /// <summary>
        /// DistributionPrecision: Подтверждает вашу главную цель — гарантированные 25/50/25 выпадений на 100 попыток.
        /// </summary>
        [Test]
        public void Test_DistributionPrecision_100Percent()
        {
            // Настраиваем: 25% - тип 0, 50% - тип 1, 25% - тип 2
            _guarantee.AddProbabilityList(25, 50, 25);

            var results = new Dictionary<int, int>();

            // Делаем ровно 100 вызовов (один полный цикл Capacity)
            for (var i = 0; i < 100; i++)
            {
                var val = _guarantee.NextProbability();
                if (!results.ContainsKey(val)) results[val] = 0;
                results[val]++;
            }

            // Проверяем идеальную точность
            ClassicAssert.AreEqual(25, results[0], "Индекс 0 должен выпасть ровно 25 раз");
            ClassicAssert.AreEqual(50, results[1], "Индекс 1 должен выпасть ровно 50 раз");
            ClassicAssert.AreEqual(25, results[2], "Индекс 2 должен выпасть ровно 25 раз");
        }

        /// <summary>
        /// CheckProbability: Доказывает, что метод «заглядывает» вперед и срабатывает только тогда, 
        /// когда в очереди действительно нужный объект, не ломая общую статистику.
        /// </summary>
        [Test]
        public void Test_CheckProbability_DoesNotWasteItems()
        {
            _guarantee.ClearProbability();
            _guarantee.AddProbability(777, 10); // 10% шанс на успех
                                                // Остальные 90% заполнятся -1 автоматически при Reset

            var successCount = 0;
            var totalAttempts = 0;

            // Имитируем цикл, где мы ПРОВЕРЯЕМ на 777
            for (var i = 0; i < 100; i++)
            {
                if (_guarantee.CheckProbability(777))
                {
                    successCount++;
                }
                else
                {
                    // Если не 777, сдвигаем очередь вручную
                    _guarantee.NextProbability();
                }
                totalAttempts++;
            }

            ClassicAssert.AreEqual(10, successCount, "CheckProbability должен подтвердить успех ровно 10 раз");
            ClassicAssert.AreEqual(100, totalAttempts);
        }

        /// <summary>
        /// ResetCapacity: Проверяет, как математика подстраивается под разные размеры буфера 
        /// (например, если вы решите делать гарантию не на 100, а на 10 или 1000 вызовов)
        /// </summary>
        [Test]
        public void Test_ResetCapacity_WorksCorrectly()
        {
            // Уменьшаем емкость до 10
            _guarantee = new RandomGuarantee(_rng, 10);
            _guarantee.AddProbability(1, 20); // 20% от 10 элементов = 2 элемента

            var count = 0;
            for (var i = 0; i < 10; i++)
            {
                if (_guarantee.NextProbability() == 1) count++;
            }

            ClassicAssert.AreEqual(2, count, "При Capacity 10 и 20% шанс, должно выпасть 2 элемента");
        }

        /// <summary>
        /// NextProbabilityAndReset: Проверяет «бесконечный» цикл. Когда игрок доходит до конца списка, 
        /// генератор бесшовно перемешивается и начинает заново.
        /// </summary>
        [Test]
        public void Test_NextProbabilityAndReset_CyclesCorrectly()
        {
            _guarantee.AddProbability(5, 100); // 100% шанс на пятерку

            // Проходим 100 элементов первого цикла
            for (var i = 0; i < 100; i++)
            {
                _guarantee.NextProbabilityAndReset();
            }

            // В конце цикла должен был произойти автоматический Reset.
            // Проверяем, что на 101-м элементе мы снова получаем данные, а не ошибку
            ClassicAssert.DoesNotThrow(() =>
            {
                var val = _guarantee.NextProbabilityAndReset();
                ClassicAssert.AreEqual(5, val);
            });

            ClassicAssert.AreEqual(0, _guarantee.CurrentIndex, "После сброса индекс должен стать 0");
        }

        [Test]
        public void Test_ProbabilityProperty_SimulateOldList()
        {
            _guarantee.ClearProbability();
            _guarantee.AddProbability(10, 5); // Значение 10 с шансом 5%

            // Используем реализацию Варианта 2 из предыдущего ответа
            var probList = new List<int>();
            foreach (var rule in _guarantee.Rules)
            {
                var count = (int)System.Math.Round((double)rule.Percent * _guarantee.Capacity / 100.0);
                for (var i = 0; i < count; i++) probList.Add(rule.Index);
            }

            ClassicAssert.AreEqual(5, probList.Count);
            ClassicAssert.IsTrue(probList.All(x => x == 10));
        }
    }
}