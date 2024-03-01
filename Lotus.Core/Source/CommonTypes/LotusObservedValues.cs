using System;

namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Логический тип который информирует об изменении своего значения.
    /// </summary>
    [Serializable]
    public struct BoolObserved : IEquatable<BoolObserved>, IComparable<BoolObserved>, ICloneable
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static BoolObserved DeserializeFromString(string data)
        {
            var value = new BoolObserved();
            value.SetValue(XBoolean.Parse(data));
            return value;
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        internal bool _value;
        [NonSerialized]
        internal Action<bool>? _onChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Значение.
        /// </summary>
        public bool Value
        {
            readonly get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (_onChanged != null) _onChanged(_value);
                }
            }
        }

        /// <summary>
        /// Событие для нотификации об изменении значения.
        /// </summary>
        public Action<bool>? OnChanged
        {
            readonly get { return _onChanged; }
            set { _onChanged = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public BoolObserved(bool value)
        {
            _value = value;
            _onChanged = null;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="onChanged">Обработчик события изменения значения.</param>
        public BoolObserved(bool value, Action<bool> onChanged)
        {
            _value = value;
            _onChanged = onChanged;
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
            if (obj != null)
            {
                if (obj is BoolObserved value)
                {
                    return Equals(value);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public readonly bool Equals(BoolObserved other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(BoolObserved other)
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
        public static bool operator ==(BoolObserved left, BoolObserved right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(BoolObserved left, BoolObserved right)
        {
            return !(left == right);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="bool"/>.</returns>
        public static implicit operator bool(BoolObserved value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="BoolObserved"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="BoolObserved"/>.</returns>
        public static implicit operator BoolObserved(bool value)
        {
            return new BoolObserved(value);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Непосредственная установка значения без информирования.
        /// </summary>
        /// <param name="value">Значение.</param>
        public void SetValue(bool value)
        {
            _value = value;
        }

        /// <summary>
        /// Информирование об изменение значения.
        /// </summary>
        public readonly void ChangedValue()
        {
            if (_onChanged != null) _onChanged(_value);
        }

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

    /// <summary>
    /// Целочисленный тип который информирует об изменении своего значения.
    /// </summary>
    [Serializable]
    public struct IntObserved : IEquatable<IntObserved>, IComparable<IntObserved>, ICloneable
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static IntObserved DeserializeFromString(string data)
        {
            var value = new IntObserved();
            value.SetValue(int.Parse(data));
            return value;
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        internal int _value;
        [NonSerialized]
        internal Action<int>? _onChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Значение.
        /// </summary>
        public int Value
        {
            readonly get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (_onChanged != null) _onChanged(_value);
                }
            }
        }

        /// <summary>
        /// Событие для нотификации об изменении значения.
        /// </summary>
        public Action<int>? OnChanged
        {
            readonly get { return _onChanged; }
            set { _onChanged = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public IntObserved(int value)
        {
            _value = value;
            _onChanged = null;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="onChanged">Обработчик события изменения значения.</param>
        public IntObserved(int value, Action<int> onChanged)
        {
            _value = value;
            _onChanged = onChanged;
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
            if (obj != null)
            {
                if (obj is IntObserved value)
                {
                    return Equals(value);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public readonly bool Equals(IntObserved other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(IntObserved other)
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
        public static bool operator ==(IntObserved left, IntObserved right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(IntObserved left, IntObserved right)
        {
            return !(left == right);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="int"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="int"/>.</returns>
        public static implicit operator int(IntObserved value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="IntObserved"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="IntObserved"/>.</returns>
        public static implicit operator IntObserved(int value)
        {
            return new IntObserved(value);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Непосредственная установка значения без информирования.
        /// </summary>
        /// <param name="value">Значение.</param>
        public void SetValue(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Информирование об изменение значения.
        /// </summary>
        public readonly void ChangedValue()
        {
            if (_onChanged != null) _onChanged(_value);
        }

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

    /// <summary>
    /// Вещественный тип который информирует об изменении своего значения.
    /// </summary>
    [Serializable]
    public struct SingleObserved : IEquatable<SingleObserved>, IComparable<SingleObserved>, ICloneable
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static SingleObserved DeserializeFromString(string data)
        {
            var value = new SingleObserved();
            value.SetValue(XNumbers.ParseSingle(data));
            return value;
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        internal float _value;
        [NonSerialized]
        internal Action<float>? _onChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Значение.
        /// </summary>
        public float Value
        {
            readonly get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (_onChanged != null) _onChanged(_value);
                }
            }
        }

        /// <summary>
        /// Событие для нотификации об изменении значения.
        /// </summary>
        public Action<float>? OnChanged
        {
            readonly get { return _onChanged; }
            set { _onChanged = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public SingleObserved(float value)
        {
            _value = value;
            _onChanged = null;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="onChanged">Обработчик события изменения значения.</param>
        public SingleObserved(float value, Action<float> onChanged)
        {
            _value = value;
            _onChanged = onChanged;
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
            if (obj != null)
            {
                if (obj is SingleObserved value)
                {
                    return Equals(value);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public readonly bool Equals(SingleObserved other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(SingleObserved other)
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
        public static bool operator ==(SingleObserved left, SingleObserved right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(SingleObserved left, SingleObserved right)
        {
            return !(left == right);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="float"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="float"/>.</returns>
        public static implicit operator float(SingleObserved value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="SingleObserved"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="SingleObserved"/>.</returns>
        public static implicit operator SingleObserved(float value)
        {
            return new SingleObserved(value);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Непосредственная установка значения без информирования.
        /// </summary>
        /// <param name="value">Значение.</param>
        public void SetValue(float value)
        {
            _value = value;
        }

        /// <summary>
        /// Информирование об изменение значения.
        /// </summary>
        public readonly void ChangedValue()
        {
            if (_onChanged != null) _onChanged(_value);
        }

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

    /// <summary>
    /// Строковый тип который информирует об изменении своего значения.
    /// </summary>
    [Serializable]
    public struct StringObserved : IEquatable<StringObserved>, IComparable<StringObserved>, ICloneable
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static StringObserved DeserializeFromString(string data)
        {
            var value = new StringObserved(data);
            return value;
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        internal string _value;
        [NonSerialized]
        internal Action<string>? _onChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Значение.
        /// </summary>
        public string Value
        {
            readonly get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (_onChanged != null) _onChanged(_value);
                }
            }
        }

        /// <summary>
        /// Событие для нотификации об изменении значения.
        /// </summary>
        public Action<string>? OnChanged
        {
            readonly get { return _onChanged; }
            set { _onChanged = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public StringObserved(string value)
        {
            _value = value;
            _onChanged = null;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="onChanged">Обработчик события изменения значения.</param>
        public StringObserved(string value, Action<string> onChanged)
        {
            _value = value;
            _onChanged = onChanged;
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
            if (obj != null)
            {
                if (obj is StringObserved value)
                {
                    return Equals(value);
                }
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public readonly bool Equals(StringObserved other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public readonly int CompareTo(StringObserved other)
        {
            return string.CompareOrdinal(_value, other._value);
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
        public static bool operator ==(StringObserved left, StringObserved right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Сравнение объектов на неравенство.
        /// </summary>
        /// <param name="left">Первый объект.</param>
        /// <param name="right">Второй объект.</param>
        /// <returns>Статус неравенство.</returns>
        public static bool operator !=(StringObserved left, StringObserved right)
        {
            return !(left == right);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа <see cref="string"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="string"/>.</returns>
        public static implicit operator string(StringObserved value)
        {
            return value._value;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="StringObserved"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="StringObserved"/>.</returns>
        public static implicit operator StringObserved(string value)
        {
            return new StringObserved(value);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Непосредственная установка значения без информирования.
        /// </summary>
        /// <param name="value">Значение.</param>
        public void SetValue(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Информирование об изменение значения.
        /// </summary>
        public readonly void ChangedValue()
        {
            if (_onChanged != null) _onChanged(_value);
        }

        /// <summary>
        /// Сериализация объекта в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public readonly string SerializeToString()
        {
            return _value;
        }
        #endregion
    }
    /**@}*/
}