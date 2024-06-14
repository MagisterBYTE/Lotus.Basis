using System;
using System.Globalization;
using System.Text;

namespace Lotus.Maths
{
    /** \addtogroup MathCommon
    *@{*/
    /// <summary>
    /// Статический класс реализующий математические методы и функции.
    /// </summary>
    public static class XMath
    {
        #region Const
        /// <summary>
        /// Значение, для которого все абсолютные значения меньше, чем считаются равными нулю.
        /// </summary>
        public const double ZeroTolerance_D = 0.000000001;

        /// <summary>
        /// Значение, для которого все абсолютные значения меньше, чем считаются равными нулю.
        /// </summary>
        public const float ZeroTolerance_F = 0.000001f;

        /// <summary>
        /// Точность вещественного числа.
        /// </summary>
        public const double Eplsilon_D = 0.00000001;

        /// <summary>
        /// Точность вещественного числа.
        /// </summary>
        public const float Eplsilon_F = 0.0000001f;

        /// <summary>
        /// Точность вещественного числа.
        /// </summary>
        public const double Eplsilon3_D = 0.001;

        /// <summary>
        /// Точность вещественного числа.
        /// </summary>
        public const float Eplsilon3_F = 0.001f;

        /// <summary>
        /// Коэффициент для преобразования радианов в градусы.
        /// </summary>
        public const double RadianToDegree_D = 57.29577951;

        /// <summary>
        /// Коэффициент для преобразования радианов в градусы.
        /// </summary>
        public const float RadianToDegree_F = 57.29577951f;

        /// <summary>
        /// Коэффициент для преобразования градусов в радианы.
        /// </summary>
        public const double DegreeToRadian_D = 0.01745329;

        /// <summary>
        /// Коэффициент для преобразования градусов в радианы.
        /// </summary>
        public const float DegreeToRadian_F = 0.01745329f;

        /// <summary>
        /// Экспонента.
        /// </summary>
        public const double Exponent_D = 2.71828182;

        /// <summary>
        /// Экспонента.
        /// </summary>
        public const float Exponent_F = 2.71828182f;

        /// <summary>
        /// Log2(e).
        /// </summary>
        public const double Log2E_D = 1.44269504;

        /// <summary>
        /// Log2(e).
        /// </summary>
        public const float Log2E_F = 1.44269504f;

        /// <summary>
        /// Log10(e).
        /// </summary>
        public const double Log10E_D = 0.43429448;

        /// <summary>
        /// Log10(e).
        /// </summary>
        public const float Log10E_F = 0.43429448f;

        /// <summary>
        /// Ln(2).
        /// </summary>
        public const double Ln2_D = 0.69314718;

        /// <summary>
        /// Ln(2).
        /// </summary>
        public const float Ln2_F = 0.69314718f;

        /// <summary>
        /// Ln(10).
        /// </summary>
        public const double Ln10_D = 2.30258509;

        /// <summary>
        /// Ln(10).
        /// </summary>
        public const float Ln10_F = 2.30258509f;

        /// <summary>
        /// Число Pi * 2.
        /// </summary>
        public const double PI2_D = 6.283185306;

        /// <summary>
        /// Число Pi * 2.
        /// </summary>
        public const float PI2_F = 6.283185306f;

        /// <summary>
        /// Число Pi.
        /// </summary>
        public const double PI_D = 3.141592653;

        /// <summary>
        /// Число Pi.
        /// </summary>
        public const float PI_F = 3.141592653f;

        /// <summary>
        /// Число Pi/2.
        /// </summary>
        public const double PI_2_D = 1.570796326;

        /// <summary>
        /// Число Pi/2.
        /// </summary>
        public const float PI_2_F = 1.570796326f;

        /// <summary>
        /// Число Pi/3.
        /// </summary>
        public const double PI_3_D = 1.047197551;

        /// <summary>
        /// Число Pi/3.
        /// </summary>
        public const float PI_3_F = 1.047197551f;

        /// <summary>
        /// Число Pi/4.
        /// </summary>
        public const double PI_4_D = 0.785398163;

        /// <summary>
        /// Число Pi/4.
        /// </summary>
        public const float PI_4_F = 0.785398163f;

        /// <summary>
        /// Число Pi/6.
        /// </summary>
        public const double PI_6_D = 0.523598775598;

        /// <summary>
        /// Число Pi/6.
        /// </summary>
        public const float PI_6_F = 0.523598775598f;
        #endregion

        #region Main methods
        /// <summary>
        /// Обмен значениями двух объектов.
        /// </summary>
        /// <typeparam name="TType">Тип объекта.</typeparam>
        /// <param name="left">Первое значение.</param>
        /// <param name="right">Второе значение.</param>
        public static void Swap<TType>(ref TType left, ref TType right)
        {
            var temp = left;
            left = right;
            right = temp;
        }

        /// <summary>
        /// Проверка на нулевое значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsZero(double value)
        {
            return Math.Abs(value) < ZeroTolerance_D;
        }

        /// <summary>
        /// Проверка на нулевое значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsZero(float value)
        {
            return Math.Abs(value) < ZeroTolerance_F;
        }

        /// <summary>
        /// Проверка на единичное значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsOne(double value)
        {
            return IsZero(value - 1.0);
        }

        /// <summary>
        /// Проверка на единичное значение.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Статус проверки.</returns>
        public static bool IsOne(float value)
        {
            return IsZero(value - 1.0);
        }

        /// <summary>
        /// Ограничение значения в пределах от 0 до 1.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение.</returns>
        public static double Clamp01(double value)
        {
            if (value < 0) value = 0;
            if (value > 1) value = 1;

            return value;
        }

        /// <summary>
        /// Ограничение значения в пределах от 0 до 1.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение.</returns>
        public static float Clamp01(float value)
        {
            if (value < 0) value = 0;
            if (value > 1) value = 1;

            return value;
        }

        /// <summary>
        /// Ограничение значения в указанных пределах.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="min">Минимальное значение.</param>
        /// <param name="max">Максимальное значение.</param>
        /// <returns>Значение.</returns>
        public static double Clamp(double value, double min, double max)
        {
            if (value < min) value = min;
            if (value > max) value = max;

            return value;
        }

        /// <summary>
        /// Ограничение значения в указанных пределах.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="min">Минимальное значение.</param>
        /// <param name="max">Максимальное значение.</param>
        /// <returns>Значение.</returns>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;

            return value;
        }

        /// <summary>
        /// Ограничение значения в указанных пределах.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="min">Минимальное значение.</param>
        /// <param name="max">Максимальное значение.</param>
        /// <returns>Значение.</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;

            return value;
        }

        /// <summary>
        /// Аппроксимация равенства значений.
        /// </summary>
        /// <param name="a">Первое значение.</param>
        /// <param name="b">Второе значение.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(double a, double b)
        {
            if (Math.Abs(a - b) < Eplsilon3_D)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Аппроксимация равенства значений.
        /// </summary>
        /// <param name="a">Первое значение.</param>
        /// <param name="b">Второе значение.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(float a, float b)
        {
            if (Math.Abs(a - b) < Eplsilon3_F)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Аппроксимация равенства значений.
        /// </summary>
        /// <param name="a">Первое значение.</param>
        /// <param name="b">Второе значение.</param>
        /// <param name="epsilon">Погрешность.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(double a, double b, double epsilon = 0.0001)
        {
            if (Math.Abs(a - b) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Аппроксимация равенства значений.
        /// </summary>
        /// <param name="a">Первое значение.</param>
        /// <param name="b">Второе значение.</param>
        /// <param name="epsilon">Погрешность.</param>
        /// <returns>Статус равенства значений.</returns>
        public static bool Approximately(float a, float b, float epsilon = 0.001f)
        {
            if (Math.Abs(a - b) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Вычисление квадратного корня.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Квадратный корень.</returns>
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }

        /// <summary>
        /// Вычисление обратного квадратного корня.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение обратного квадратного корня.</returns>
        public static double InvSqrt(double value)
        {
            var result = Math.Sqrt(value);

            if (result > ZeroTolerance_D)
            {
                return 1.0 / result;
            }

            return 1.0;
        }

        /// <summary>
        /// Вычисление обратного квадратного корня.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Значение обратного квадратного корня.</returns>
        public static float InvSqrt(float value)
        {
            var result = (float)Math.Sqrt(value);

            if (result > ZeroTolerance_F)
            {
                return 1.0f / result;
            }

            return 1.0f;
        }

        /// <summary>
        /// Вычисление синуса.
        /// </summary>
        /// <param name="radians">Угол в радианах.</param>
        /// <returns>Значение синуса.</returns>
        public static float Sin(float radians)
        {
            return (float)Math.Sin(radians);
        }

        /// <summary>
        /// Вычисление косинуса.
        /// </summary>
        /// <param name="radians">Угол в радианах.</param>
        /// <returns>Значение косинуса.</returns>
        public static float Cos(float radians)
        {
            return (float)Math.Cos(radians);
        }

        /// <summary>
        /// Преобразование интервала одного к другому.
        /// </summary>
        /// <remarks>
        /// Под другому это можно интерпретировать как пересечение по Y вертикальной линии отрезка проходящего через точки:
        /// - x1 = dest_start
        /// - x2 = dest_end
        /// - y1 = source_start
        /// - y2 = source_end
        /// - px = value
        /// </remarks>
        /// <param name="destStart">Начала целевого интервала.</param>
        /// <param name="destEnd">Конец целевого интервала.</param>
        /// <param name="sourceStart">Начало исходного интервала.</param>
        /// <param name="sourceEnd">Конец исходного интервала.</param>
        /// <param name="value">Исходное значение.</param>
        /// <returns>Целевое значение.</returns>
        public static double ConvertInterval(double destStart, double destEnd, double sourceStart,
                double sourceEnd, double value)
        {
            var x1 = destStart;
            var x2 = destEnd;
            var y1 = sourceStart;
            var y2 = sourceEnd;

            var k = (y2 - y1) / (x2 - x1);
            var b = y1 - (k * x1);

            var result = (k * value) + b;

            return result;
        }

        /// <summary>
        /// Преобразование интервала одного к другому.
        /// </summary>
        /// <remarks>
        /// Под другому это можно интерпретировать как пересечение по Y вертикальной линии отрезка проходящего через точки:
        /// - x1 = dest_start
        /// - x2 = dest_end
        /// - y1 = source_start
        /// - y2 = source_end
        /// - px = value
        /// </remarks>
        /// <param name="destStart">Начала целевого интервала.</param>
        /// <param name="destEnd">Конец целевого интервала.</param>
        /// <param name="sourceStart">Начало исходного интервала.</param>
        /// <param name="sourceEnd">Конец исходного интервала.</param>
        /// <param name="value">Исходное значение.</param>
        /// <returns>Целевое значение.</returns>
        public static float ConvertInterval(float destStart, float destEnd, float sourceStart,
                float sourceEnd, float value)
        {
            var x1 = destStart;
            var x2 = destEnd;
            var y1 = sourceStart;
            var y2 = sourceEnd;

            var k = (y2 - y1) / (x2 - x1);
            var b = y1 - (k * x1);

            var result = (k * value) + b;

            return result;
        }

        /// <summary>
        /// Преобразование процента в часть.
        /// </summary>
        /// <param name="percent">Процент от 0 до 100.</param>
        /// <returns>Часть.</returns>
        public static float ToPartFromPercent(int percent)
        {
            float p = percent;
            if (percent <= 0)
            {
                p = 0;
            }
            if (percent >= 100)
            {
                p = 100;
            }

            return p / 100.0f;
        }

        /// <summary>
        /// Округление до нужного целого.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="round">Степень округления.</param>
        /// <returns>Округленное значение.</returns>
        public static double RoundToNearest(double value, int round)
        {
            if (value >= 0)
            {
                var result = Math.Floor((value + ((double)round / 2)) / round) * round;
                return result;
            }
            else
            {
                var result = Math.Ceiling((value - ((double)round / 2)) / round) * round;
                return result;
            }
        }

        /// <summary>
        /// Округление до нужного целого.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="round">Степень округления.</param>
        /// <returns>Округленное значение.</returns>
        public static float RoundToNearest(float value, int round)
        {
            if (value >= 0)
            {
                return (float)(Math.Floor((value + ((float)round / 2)) / round) * round);
            }
            else
            {
                return (float)(Math.Ceiling((value - ((float)round / 2)) / round) * round);
            }
        }


        /// <summary>
        /// Округление до нужного.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="round">Степень округления.</param>
        /// <returns>Округленное значение.</returns>
        public static float RoundToSingle(float value, float round)
        {
            if (value >= 0)
            {
                return (float)(Math.Floor((value + (round / 2)) / round) * round);
            }
            else
            {
                return (float)(Math.Ceiling((value - (round / 2)) / round) * round);
            }
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с массивом.
    /// </summary>
    /// <remarks>
    /// Обратите внимание массив(исходный) переданный в качестве аргумента в методы всегда изменяется.
    /// Все методы возвращают результирующий массив.
    /// </remarks>
    internal static class XArrayHelper
    {
        /// <summary>
        /// Добавление элемента в конец массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="item">Элемент.</param>
        /// <returns>Массив.</returns>
        public static TType[] Add<TType>(TType[] array, in TType item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
            return array;
        }

        /// <summary>
        /// Добавление элемента в конец массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="current_count">Текущие количество элементов.</param>
        /// <param name="item">Элемент.</param>
        /// <returns>Массив.</returns>
        public static TType[] Add<TType>(TType[] array, ref int current_count, in TType item)
        {
            if (current_count == array.Length)
            {
                Array.Resize(ref array, current_count << 1);
            }

            array[current_count] = item;
            current_count++;
            return array;
        }

        /// <summary>
        /// Добавление элемента в конец массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="current_count">Текущие количество элементов.</param>
        /// <param name="items">Список элементов.</param>
        /// <returns>Массив.</returns>
        public static TType[] AddRange<TType>(TType[] array, ref int current_count, params TType[] items)
        {
            if (array.Length < current_count + items.Length)
            {
                var max_size = current_count + items.Length;
                var new_arary = new TType[max_size];
                Array.Copy(array, new_arary, current_count);
                array = items;
            }

            Array.Copy(items, 0, array, current_count, items.Length);
            current_count += items.Length;
            return array;
        }

        /// <summary>
        /// Вставка элемента в указанную позицию массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="item">Элемент.</param>
        /// <param name="index">Позиция(индекс) вставки элемента.</param>
        /// <returns>Массив.</returns>
        public static TType[] InsertAt<TType>(TType[] array, in TType item, int index)
        {
            var temp = array;
            array = new TType[array.Length + 1];
            Array.Copy(temp, 0, array, 0, index);
            array[index] = item;
            Array.Copy(temp, index, array, index + 1, temp.Length - index);

            return array;
        }

        /// <summary>
        /// Вставка элементов в указанную позицию массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="items">Набор элементов.</param>
        /// <param name="index">Позиция(индекс) вставки элементов.</param>
        /// <returns>Массив.</returns>
        public static TType[] InsertAt<TType>(TType[] array, int index, params TType[] items)
        {
            var temp = array;
            array = new TType[array.Length + items.Length];
            Array.Copy(temp, 0, array, 0, index);
            Array.Copy(items, 0, array, index, items.Length);
            Array.Copy(temp, index, array, index + items.Length, temp.Length - index);

            return array;
        }

        /// <summary>
        /// Вставка элемента в начало массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="item">Элемент.</param>
        /// <returns>Массив.</returns>
        public static TType[] Push<TType>(TType[] array, in TType item)
        {
            return InsertAt(array, item, 0);
        }

        /// <summary>
        /// Удаление элементов массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="start">Начальная позиция удаления.</param>
        /// <param name="count">Количество элементов для удаления.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveAt<TType>(TType[] array, int start, int count)
        {
            var temp = array;
            array = new TType[array.Length - count >= 0 ? array.Length - count : 0];
            Array.Copy(temp, array, start);
            var index = start + count;
            if (index < temp.Length)
            {
                Array.Copy(temp, index, array, start, temp.Length - index);
            }

            return array;
        }

        /// <summary>
        /// Удаление элемента массива по индексу.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="index">Индекс удаляемого элемента.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveAt<TType>(TType[] array, int index)
        {
            return RemoveAt(array, index, 1);
        }

        /// <summary>
        /// Удаление диапазона массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="start">Начало удаляемого диапазона.</param>
        /// <param name="end">Конец удаляемого диапазона.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveRange<TType>(TType[] array, int start, int end)
        {
            return RemoveAt(array, start, end - start + 1);
        }

        /// <summary>
        /// Удаление первого элемента массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveFirst<TType>(TType[] array)
        {
            return RemoveAt(array, 0, 1);
        }

        /// <summary>
        /// Удаление элементов с начала массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="count">Количество удаляемых элементов.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveFirst<TType>(TType[] array, int count)
        {
            return RemoveAt(array, 0, count);
        }

        /// <summary>
        /// Удаление последнего элемента массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveLast<TType>(TType[] array)
        {
            return RemoveAt(array, array.Length - 1, 1);
        }

        /// <summary>
        /// Удаление последних элементов массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="count">Количество удаляемых элементов.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveLast<TType>(TType[] array, int count)
        {
            return RemoveAt(array, array.Length - count, count);
        }

        /// <summary>
        /// Поиск и удаление первого вхождения элемента.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="item">Удаляемый элемент.</param>
        /// <returns>Массив.</returns>
        public static TType[] Remove<TType>(TType[] array, in TType item)
        {
            var index = Array.IndexOf(array, item);
            if (index >= 0)
            {
                return RemoveAt(array, index);
            }

            return array;
        }

        /// <summary>
        /// Удаление всех вхождений элемента.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="item">Удаляемый элемент.</param>
        /// <returns>Массив.</returns>
        public static TType[] RemoveAll<TType>(TType[] array, in TType item)
        {
            int index;
            do
            {
                index = Array.IndexOf(array, item);
                if (index >= 0)
                {
                    array = RemoveAt(array, index);
                }
            }
            while (index >= 0 && array.Length > 0);
            return array;
        }

        /// <summary>
        /// Смещение элементов массива.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="start_index">Индекс элемент с которого начитается смещение.</param>
        /// <param name="offset">Количество смещения.</param>
        /// <param name="count">Количество смещаемых элементов.</param>
        /// <returns>Массив.</returns>
        public static TType[] Shift<TType>(TType[] array, int start_index, int offset, int count)
        {
            var result = (TType[])array.Clone();

            if (start_index >= result.Length)
            {
                start_index = start_index < 0 ? 0 : (result.Length - 1);
            }
            else
            {
                start_index = start_index < 0 ? 0 : (start_index);
            }
            if (start_index + count >= result.Length)
            {
                count = count < 0 ? 0 : (result.Length - start_index - 1);
            }
            else
            {
                count = count < 0 ? 0 : (count);
            }
            if (start_index + count + offset >= result.Length)
            {
                offset = start_index + offset < 0 ? -start_index : (result.Length - start_index - count);
            }
            else
            {
                offset = start_index + offset < 0 ? -start_index : (offset);
            }

            var abs_offset = Math.Abs(offset);
            var items = new TType[count]; // What we want to move
            var dec = new TType[abs_offset]; // What is going to replace the thing we move
            Array.Copy(array, start_index, items, 0, count);
            Array.Copy(array, start_index + (offset >= 0 ? count : offset), dec, 0, abs_offset);
            Array.Copy(dec, 0, result, start_index + (offset >= 0 ? 0 : offset + count), abs_offset);
            Array.Copy(items, 0, result, start_index + offset, count);

            return result;
        }

        /// <summary>
        /// Смещение элементов массива вправо.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="start_index">Индекс элемент с которого начитается смещение.</param>
        /// <returns>Массив.</returns>
        public static TType[] ShiftRight<TType>(TType[] array, int start_index)
        {
            return Shift(array, start_index, 1, 1);
        }

        /// <summary>
        /// Смещение элементов массива влево.
        /// </summary>
        /// <typeparam name="TType">Тип элемента массива.</typeparam>
        /// <param name="array">Массив.</param>
        /// <param name="start_index">Индекс элемент с которого начитается смещение.</param>
        /// <returns>Массив.</returns>
        public static TType[] ShiftLeft<TType>(TType[] array, int start_index)
        {
            return Shift(array, start_index, -1, 1);
        }
    }

    /// <summary>
    /// Статический класс реализующий дополнительные методы для работы с числовыми типами.
    /// </summary>
    internal static class XNumberHelper
    {
        #region Format numbers
        /// <summary>
        /// Денежный формат.
        /// </summary>
        public const string Monetary = "{0:c}";
        #endregion

        #region Int32 
        /// <summary>
        /// Проверка на установленный флаг.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flag">Проверяемый флаг.</param>
        /// <returns>Статус установки флага.</returns>
        public static bool IsFlagSet(int value, int flag)
        {
            return (value & flag) != 0;
        }

        /// <summary>
        /// Установка флага.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Новое значение.</returns>
        public static int SetFlag(int value, int flags)
        {
            value |= flags;
            return value;
        }

        /// <summary>
        /// Очистка флага.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="flags">Флаг.</param>
        /// <returns>Новое значение.</returns>
        public static int ClearFlag(int value, int flags)
        {
            value &= ~flags;
            return value;
        }

        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextInt(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            const int max = 11;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }

                if (number.Length > max)
                {
                    break;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static int ParseInt(string text, int defaultValue = 0)
        {
            text = ParseableTextLong(text);

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;

            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseInt(string text, out int result)
        {
            text = ParseableTextInt(text);

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Int64
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в целый тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextLong(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            const int max = 19;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }

                if (number.Length > max)
                {
                    break;
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static long ParseLong(string text, long defaultValue = 0)
        {
            text = ParseableTextLong(text);

            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseLong(string text, out long result)
        {
            text = ParseableTextLong(text);

            if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Single 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextSingle(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static float ParseSingle(string text, float defaultValue = 0)
        {
            text = ParseableTextSingle(text);

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseSingle(string text, out float result)
        {
            text = ParseableTextSingle(text);

            if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Double 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextDouble(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static double ParseDouble(string text, double defaultValue = 0)
        {
            text = ParseableTextDouble(text);

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="result">Значение.</param>
        /// <returns>Статус успешности преобразования.</returns>
        public static bool TryParseDouble(string text, out double result)
        {
            text = ParseableTextDouble(text);

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Decimal 
        /// <summary>
        /// Преобразование в текст который можно сконвертировать в вещественный тип.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <returns>Текст.</returns>
        public static string ParseableTextDeciminal(string text)
        {
            var number = new StringBuilder(text.Length);

            var add_minus = false;
            var add_dot = false;
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '-' && (i != text.Length - 1) && add_minus == false)
                {
                    number.Append(c);
                    add_minus = true;
                    continue;
                }

                if ((c == ',' || c == '.') && (i != text.Length - 1) && add_dot == false)
                {
                    number.Append('.');
                    add_dot = true;
                    continue;
                }

                if (c is >= '0' and <= '9')
                {
                    number.Append(c);
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Преобразование текста в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ParseDecimal(string text, decimal defaultValue = 0)
        {
            text = ParseableTextDeciminal(text);

            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Преобразование текста, представленного как отображение валюты, в число.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <param name="defaultValue">Значение по умолчанию если преобразовать не удалось.</param>
        /// <returns>Значение.</returns>
        public static decimal ParseCurrency(string text, decimal defaultValue = 0)
        {
            text = ParseableTextDeciminal(text);

            if (decimal.TryParse(text, NumberStyles.Currency, CultureInfo.InvariantCulture, out var resultValue))
            {
                return resultValue;
            }

            return defaultValue;
        }
        #endregion
    }
    /**@}*/
}