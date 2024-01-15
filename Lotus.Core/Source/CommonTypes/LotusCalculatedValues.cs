//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема общих типов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCalculatedValues.cs
*		Калькулированное значение.
*		Класс содержащий значение и дополнительные свойства для его управления.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreCommonTypes
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс содержащий целое значение и дополнительные свойства для его управления
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class IntCalculated : PropertyChangedBase, ICloneable, ILotusOwnedObject, ILotusNotCalculation
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация объекта из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IntCalculated DeserializeFromString(String data)
			{
				var int_value = XNumbers.ParseInt(data, 0);
				return new IntCalculated(int_value);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 _value;
			protected internal Int32 _supplement;
			protected internal Boolean _notCalculation;
			protected internal ILotusOwnerObject _owner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Базовое значение
			/// </summary>
			[XmlAttribute]
			public Int32 Value
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

					NotifyPropertyChanged(nameof(Value));
					NotifyPropertyChanged(nameof(CalculatedValue));
				}
			}

			/// <summary>
			/// Смещение базового значения
			/// </summary>
			[XmlAttribute]
			public Int32 Supplement
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

					NotifyPropertyChanged(nameof(Supplement));
					NotifyPropertyChanged(nameof(CalculatedValue));
				}
			}

			/// <summary>
			/// Вычисленное значение
			/// </summary>
			[XmlIgnore]
			public Int32 CalculatedValue
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

						NotifyPropertyChanged(nameof(Value));
						NotifyPropertyChanged(nameof(CalculatedValue));
					}
				}
			}

			/// <summary>
			/// Владелец объекта
			/// </summary>
			[XmlIgnore]
			public ILotusOwnerObject? IOwner
			{
				get { return _owner; }
				set { _owner = value; }
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusNotCalculation =============================
			/// <summary>
			/// Не учитывать параметр в расчетах
			/// </summary>
			[XmlAttribute]
			public Boolean NotCalculation
			{
				get { return _notCalculation; }
				set
				{
					_notCalculation = value;

					if (_owner != null)
					{
						_owner.OnNotifyUpdated(this, _notCalculation, nameof(NotCalculation));
					}

					NotifyPropertyChanged(nameof(CalculatedValue));
					NotifyPropertyChanged(nameof(NotCalculation));
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			public IntCalculated(Int32 value)
			{
				_value = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="ownerObject">Владелец значения</param>
			//---------------------------------------------------------------------------------------------------------
			public IntCalculated(Int32 value, ILotusOwnerObject ownerObject)
			{
				_value = value;
				_owner = ownerObject;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return CalculatedValue.ToString();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================