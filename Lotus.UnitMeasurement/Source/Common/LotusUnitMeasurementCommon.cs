using System;

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Интерфейс для определения величины поддерживающую определённую единицу измерения.
    /// </summary>
    public interface ILotusUnitValue
    {
        /// <summary>
        /// Значение величины.
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        Enum UnitType { get; }
    }

    /// <summary>
    /// Интерфейс для определения величины поддерживающую определённую единицу измерения.
    /// </summary>
    /// <typeparam name="TUnit">Тип единицы измерения.</typeparam>
    public interface ILotusUnitValue<out TUnit> : ILotusUnitValue where TUnit : Enum
    {
        /// <summary>
        /// Единица измерения.
        /// </summary>
        new TUnit UnitType { get; }
    }

    /// <summary>
    /// Класс определяющий величину и соответствующую определённую единицу измерения.
    /// </summary>
    /// <typeparam name="TUnit">Тип единицы измерения.</typeparam>
    [Serializable]
    public struct TUnitValue<TUnit> : ILotusUnitValue<TUnit>, IEquatable<TUnitValue<TUnit>>,
        IComparable<TUnitValue<TUnit>>, ICloneable where TUnit : Enum
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static TUnitValue<TUnit> DeserializeFromString(string data)
        {
            if (data.Contains(','))
            {
                data = data.Replace(',', '.');
            }

            if (double.TryParse(data, out var result))
            {
            }

            var value = new TUnitValue<TUnit>(result);
            return value;
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
#endif
        internal double _value;
#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
#endif
        internal TUnit _unitType;
        #endregion

        #region Properties
        /// <summary>
        /// Значение.
        /// </summary>
        public double Value
        {
            readonly get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        public readonly TUnit UnitType
        {
            get { return _unitType; }
        }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        readonly Enum ILotusUnitValue.UnitType
        {
            get
            {
                return _unitType;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public TUnitValue(double value)
        {
            _value = value;
            _unitType = (TUnit)(object)1;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="unitType">Единица измерения.</param>
        public TUnitValue(double value, TUnit unitType)
        {
            _value = value;
            _unitType = unitType;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is TUnitValue<TUnit> value)
            {
                return Equals(value);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public readonly bool Equals(TUnitValue<TUnit> other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(TUnitValue<TUnit> other)
        {
            return _value.CompareTo(other._value);
        }

        /// <summary>
        /// Получение хеш-кода объекта.
        /// </summary>
        /// <returns>Хеш-код объекта.</returns>
        public override readonly int GetHashCode()
        {
            return _value.GetHashCode() ^ base.GetHashCode();
        }

        /// <summary>
        /// Полное копирование объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override readonly string ToString()
        {
            return _value.ToString();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение объектов на равенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус равенства.</returns>
        public static bool operator ==(TUnitValue<TUnit> left, TUnitValue<TUnit> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(TUnitValue<TUnit> left, TUnitValue<TUnit> right)
        {
            return !(left == right);
        }
        #endregion

        #region Operators conversion
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="double"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="double"/>.</returns>
        public static implicit operator double(TUnitValue<TUnit> value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="TUnitValue{TUnit}"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="TUnitValue{TUnit}"/>.</returns>
        public static implicit operator TUnitValue<TUnit>(double value)
        {
            return new TUnitValue<TUnit>(value);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Сериализация объекта в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return _value.ToString();
        }
        #endregion
    }
    /**@}*/
}