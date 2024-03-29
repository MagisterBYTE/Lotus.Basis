using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lotus.Core
{
    /**
     * \defgroup CoreTypedef Подсистема константных типов
     * \ingroup Core
     * \brief Подсистема константных типов реализует тип, объекты которого создаются один раз и сравниваются по значению.
     * @{
     */
    /// <summary>
    /// Базовый класс для формирования константного типа на основании перечисления.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления.</typeparam>
    public abstract class TypedefObject<TEnum> : PropertyChangedBase,
        IEquatable<TypedefObject<TEnum>>,
        IEquatable<TEnum>,
        IEqualityComparer<TypedefObject<TEnum>>
        where TEnum : Enum
    {
        #region Properties
        /// <summary>
        /// Перечисление соответствующего типа.
        /// </summary>
        public TEnum Type { get; init; } = default!;
        #endregion

        #region System methods
        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is TypedefObject<TEnum> other)
            {
                return Equals(other);
            }

            return false;
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public bool Equals(TypedefObject<TEnum>? other)
        {
            return other != null && EqualityComparer<TEnum>.Default.Equals(Type, other.Type);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public bool Equals(TEnum? other)
        {
            return EqualityComparer<TEnum>.Default.Equals(Type, other);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="x">Первый объект.</param>
        /// <param name="y">Второй объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public bool Equals(TypedefObject<TEnum>? x, TypedefObject<TEnum>? y)
        {
            if (x is not null)
            {
                return x.Equals(y);
            }

            return true;
        }

        /// <summary>
        /// Получение хеш-кода объекта.
        /// </summary>
        /// <returns>Хеш-код объекта.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Type);
        }

        /// <summary>
        /// Получение хеш-кода указанного объекта.
        /// </summary>
        /// <param name="obj">Объект.</param>
        /// <returns>Хеш-код объекта.</returns>
        public int GetHashCode([DisallowNull] TypedefObject<TEnum> obj)
        {
            if (obj is not null)
            {
                return obj.GetHashCode();
            }

            return 0;
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление.</returns>
        public override string ToString()
        {
            return $"Type: {Type}";
        }
        #endregion
    }
    /**@}*/
}
