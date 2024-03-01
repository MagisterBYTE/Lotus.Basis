using System;
using System.Xml.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Класс содержащий целое значение и дополнительные свойства для его управления.
    /// </summary>
    [Serializable]
    public class IntCalculated : PropertyChangedBase, ICloneable, ILotusOwnedObject, ILotusNotCalculation
    {
        #region Static methods
        /// <summary>
        /// Десереализация объекта из строки.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <returns>Объект.</returns>
        public static IntCalculated DeserializeFromString(string data)
        {
            var int_value = XNumbers.ParseInt(data, 0);
            return new IntCalculated(int_value);
        }
        #endregion

        #region Fields
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
        protected internal int _value;
        protected internal int _supplement;
        protected internal bool _notCalculation;
        protected internal ILotusOwnerObject? _owner;
        #endregion

        #region Properties
        /// <summary>
        /// Базовое значение.
        /// </summary>
        [XmlAttribute]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (_owner != null)
                {
                    _owner.OnNotifyUpdated(this, _value, nameof(Value));
                }

                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(CalculatedValue));
            }
        }

        /// <summary>
        /// Смещение базового значения.
        /// </summary>
        [XmlAttribute]
        public int Supplement
        {
            get
            {
                return _supplement;
            }
            set
            {
                _supplement = value;
                if (_owner != null)
                {
                    _owner.OnNotifyUpdated(this, _supplement, nameof(Supplement));
                }

                OnPropertyChanged(nameof(Supplement));
                OnPropertyChanged(nameof(CalculatedValue));
            }
        }

        /// <summary>
        /// Вычисленное значение.
        /// </summary>
        [XmlIgnore]
        public int CalculatedValue
        {
            get
            {
                if (_notCalculation)
                {
                    return 0;
                }
                else
                {
                    return _value + _supplement;
                }
            }
            set
            {
                if (_notCalculation != true)
                {
                    _value = value - _supplement;
                    if (_owner != null)
                    {
                        _owner.OnNotifyUpdated(this, _value + _supplement, nameof(CalculatedValue));
                    }

                    OnPropertyChanged(nameof(Value));
                    OnPropertyChanged(nameof(CalculatedValue));
                }
            }
        }

        /// <summary>
        /// Владелец объекта.
        /// </summary>
        [XmlIgnore]
        public ILotusOwnerObject? IOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        #endregion

        #region Properties ILotusNotCalculation 
        /// <summary>
        /// Не учитывать параметр в расчетах.
        /// </summary>
        [XmlAttribute]
        public bool NotCalculation
        {
            get { return _notCalculation; }
            set
            {
                _notCalculation = value;

                if (_owner != null)
                {
                    _owner.OnNotifyUpdated(this, _notCalculation, nameof(NotCalculation));
                }

                OnPropertyChanged(nameof(CalculatedValue));
                OnPropertyChanged(nameof(NotCalculation));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        public IntCalculated(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="ownerObject">Владелец значения.</param>
        public IntCalculated(int value, ILotusOwnerObject ownerObject)
        {
            _value = value;
            _owner = ownerObject;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Полное копирование объекта.
        /// </summary>
        /// <returns>Копия объекта.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление объекта.</returns>
        public override string ToString()
        {
            return CalculatedValue.ToString();
        }
        #endregion
    }
    /**@}*/
}