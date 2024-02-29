using System;

#nullable disable

namespace Lotus.UnitMeasurement
{
    /** \addtogroup UnitMeasurement
	*@{*/
    /// <summary>
    /// Класс определяющий величину и позволяющий динамически менять тип единицы измерения.
    /// </summary>
    [Serializable]
    public struct TMeasurementValue : IEquatable<TMeasurementValue>, IComparable<TMeasurementValue>
    {
        #region Static fields
        /// <summary>
        /// Пустое значение.
        /// </summary>
        public static readonly TMeasurementValue Empty = new();
        #endregion

        #region Static methods
        /// <summary>
        /// Создание величины для измерения вещей.
        /// </summary>
        /// <param name="value">Количество.</param>
        /// <param name="unitThing">Единица измерения вещей.</param>
        /// <returns>Величина для измерения вещей.</returns>
        public static TMeasurementValue CreateThing(int value, TUnitThing unitThing = TUnitThing.Thing)
        {
            return new TMeasurementValue(value, TMeasurementType.Thing, unitThing);
        }

        /// <summary>
        /// Создание величины для измерения длины.
        /// </summary>
        /// <param name="value">Количество.</param>
        /// <param name="unitLength">Единица измерения длины.</param>
        /// <returns>Величина для измерения длины.</returns>
        public static TMeasurementValue CreateLength(double value, TUnitLength unitLength = TUnitLength.Meter)
        {
            return new TMeasurementValue(value, TMeasurementType.Length, unitLength);
        }

        /// <summary>
        /// Создание величины для измерения площади.
        /// </summary>
        /// <param name="value">Количество.</param>
        /// <param name="unitArea">Единица измерения площади.</param>
        /// <returns>Величина для измерения площади.</returns>
        public static TMeasurementValue CreateArea(double value, TUnitArea unitArea = TUnitArea.SquareMeter)
        {
            return new TMeasurementValue(value, TMeasurementType.Area, unitArea);
        }

        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static TMeasurementValue DeserializeFromString(string data)
        {
            //String number = data.Substring(0, data.IndexOf('{'));

            //String measurement_str = data.ExtractString("{", "}");
            //TMeasurementType measurement = XExtensionMeasurementType.Parse(measurement_str);

            //String unit_str = data.ExtractString("[", "]");
            //Enum unit = measurement.GetUnitValueFromString(unit_str);

            //TMeasurementValue value = new TMeasurementValue(XNumbers.ParseDouble(number), measurement, unit);
            var value = new TMeasurementValue();
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
        internal TMeasurementType _measurementType;

#if UNITY_2017_1_OR_NEWER
		[UnityEngine.SerializeField]
#endif
        internal Enum _unitType;
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
        /// Тип измерения.
        /// </summary>
        public readonly TMeasurementType QuantityType
        {
            get { return _measurementType; }
        }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        public readonly Enum UnitType
        {
            get { return _unitType; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="measurementType">Тип измерения.</param>
        public TMeasurementValue(double value, TMeasurementType measurementType)
        {
            _value = value;
            _measurementType = measurementType;
            _unitType = _measurementType.GetUnitValueDefault();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="measurementType">Тип измерения.</param>
        /// <param name="unitType">Единица измерения.</param>
        public TMeasurementValue(double value, TMeasurementType measurementType, Enum unitType)
        {
            _value = value;
            _measurementType = measurementType;
            _unitType = unitType;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="unitType">Единица измерения.</param>
        public TMeasurementValue(double value, Enum unitType)
        {
            _value = value;
            _unitType = unitType;
            _measurementType = XUnitType.GetMeasurementType(unitType);
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override readonly bool Equals(object obj)
        {
            if (obj is TMeasurementValue value)
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
        public readonly bool Equals(TMeasurementValue other)
        {
            return _value == other._value && _unitType == other._unitType;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(TMeasurementValue other)
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
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override readonly string ToString()
        {
            return _value.ToString() + " " + GetAbbreviationUnit();
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнение объектов на равенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус равенства.</returns>
        public static bool operator ==(TMeasurementValue left, TMeasurementValue right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(TMeasurementValue left, TMeasurementValue right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Сравнение объектов по операции меньше.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator <(TMeasurementValue left, TMeasurementValue right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Сравнение объектов по операции меньше или равно.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator <=(TMeasurementValue left, TMeasurementValue right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Сравнение объектов по операции больше.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator >(TMeasurementValue left, TMeasurementValue right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Сравнение объектов по операции больше или равно.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус операции.</returns>
        public static bool operator >=(TMeasurementValue left, TMeasurementValue right)
        {
            return left.CompareTo(right) >= 0;
        }
        #endregion

        #region Operators conversion
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="double"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="double"/>.</returns>
        public static implicit operator double(TMeasurementValue value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="double"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="TMeasurementValue"/>.</returns>
        public static implicit operator TMeasurementValue(double value)
        {
            return new TMeasurementValue(value, TMeasurementType.Undefined);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение копии объекта с новыми параметрами.
        /// </summary>
        /// <param name="value">Новое значение.</param>
        /// <returns>Объект.</returns>
        public readonly TMeasurementValue Clone(double value)
        {
            return new TMeasurementValue(value, _measurementType, _unitType);
        }

        /// <summary>
        /// Получение копии объекта с новыми параметрами.
        /// </summary>
        /// <param name="unitType">Новая единица измерения.</param>
        /// <returns>Объект.</returns>
        public readonly TMeasurementValue Clone(Enum unitType)
        {
            return new TMeasurementValue(_value, unitType);
        }

        /// <summary>
        /// Установка типа измерения и единицы измерения.
        /// </summary>
        /// <param name="measurementType">Тип измерения.</param>
        /// <param name="unitType">Единица измерения.</param>
        public void SetQuantityAndUnit(TMeasurementType measurementType, Enum unitType)
        {
            _measurementType = measurementType;
            _unitType = unitType ?? measurementType.GetUnitValueDefault();
        }

        /// <summary>
        /// Получение аббревиатуры единицы измерения.
        /// </summary>
        /// <returns>Аббревиатура единицы измерения .</returns>
        public readonly string GetAbbreviationUnit()
        {
            if (_unitType != null)
            {
                //return (_unitType.GetAbbreviationOrName());
                return _unitType.ToString();
            }
            else
            {
                return "нп";
            }
        }

        /// <summary>
        /// Сериализация объекта в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            if (_unitType == null)
            {
                return _value.ToString() + "{" + _measurementType.ToString() + "}[TUnitThing.Undefined]";
            }
            else
            {
                return _value.ToString() + "{" + _measurementType.ToString() + "}[" + _unitType.ToString() + "]";
            }
        }
        #endregion
    }
    /**@}*/
}