using System;

namespace Lotus.Core
{
    /** \addtogroup CoreHelpers
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы применяемые в целом для всех объектов платформы NET.
    /// </summary>
    public static class XObject
    {
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
        /// Универсальный и безопасный метод сравнение двух объектов.
        /// </summary>
        /// <remarks>
        /// Применяется когда неизвестен тип объекта (ссылочный или значение).
        /// </remarks>
        /// <typeparam name="TType">Тип объекта.</typeparam>
        /// <param name="left">Объект.</param>
        /// <param name="right">Объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public static bool GenericEquals<TType>(in TType? left, in TType? right)
        {
            if (typeof(TType).IsValueType)
            {
                return left!.Equals(right);
            }
            else
            {
                if (left != null)
                {
                    return left.Equals(right);
                }
                else
                {
                    if (right != null)
                    {
                        return right.Equals(left);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Универсальный и безопасный метод сравнение двух объектов ссылочного типа.
        /// </summary>
        /// <param name="left">Объект.</param>
        /// <param name="right">Объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public static bool ObjectEquals(object? left, object? right)
        {
            if (left != null)
            {
                return left.Equals(right);
            }
            else
            {
                if (right != null)
                {
                    return right.Equals(left);
                }
            }

            return true;
        }

        /// <summary>
        /// Сравнение объектов по возрастанию.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус сравнения.</returns>
        public static int ComprareOfAscending<TType>(in TType left, in TType right) where TType : class
        {
            if (left == null)
            {
                if (right == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (right == null)
                {
                    return -1;
                }
                else
                {
                    if (left is IComparable<TType> left_comparable)
                    {
                        return left_comparable.CompareTo(right);
                    }
                    else
                    {
                        if (left is IComparable left_comparable_generic)
                        {
                            return left_comparable_generic.CompareTo(right);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сравнение объектов по убыванию.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус сравнения.</returns>
        public static int ComprareOfDescending<TType>(in TType left, in TType right) where TType : class
        {
            var result = ComprareOfAscending(left, right);
            if (result == 1)
            {
                return -1;
            }
            else
            {
                if (result == -1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
    /**@}*/
}