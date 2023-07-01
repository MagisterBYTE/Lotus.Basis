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
		public class CIntCalculated : PropertyChangedBase, ICloneable, ILotusOwnedObject, ILotusNotCalculation
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация объекта из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CIntCalculated DeserializeFromString(String data)
			{
				var int_value = XNumbers.ParseInt(data, 0);
				return new CIntCalculated(int_value);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 mValue;
			protected internal Int32 mSupplement;
			protected internal Boolean mNotCalculation;
			protected internal ILotusOwnerObject mOwner;
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
					return mValue;
				}
				set
				{
					mValue = value;
					if(mOwner != null)
					{
						mOwner.OnNotifyUpdated(this, mValue, nameof(Value));
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
					return mSupplement;
				}
				set
				{
					mSupplement = value;
					if (mOwner != null)
					{
						mOwner.OnNotifyUpdated(this, mSupplement, nameof(Supplement));
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
					if (mNotCalculation)
					{
						return 0;
					}
					else
					{
						return mValue + mSupplement;
					}
				}
				set
				{
					if (mNotCalculation != true)
					{
						mValue = value - mSupplement;
						if (mOwner != null)
						{
							mOwner.OnNotifyUpdated(this, mValue + mSupplement, nameof(CalculatedValue));
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
			public ILotusOwnerObject IOwner
			{
				get { return mOwner; }
				set { mOwner = value; }
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusNotCalculation =============================
			/// <summary>
			/// Не учитывать параметр в расчетах
			/// </summary>
			[XmlAttribute]
			public Boolean NotCalculation
			{
				get { return mNotCalculation; }
				set
				{
					mNotCalculation = value;

					if (mOwner != null)
					{
						mOwner.OnNotifyUpdated(this, mNotCalculation, nameof(NotCalculation));
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
			public CIntCalculated(Int32 value)
			{
				mValue = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="ownerObject">Владелец значения</param>
			//---------------------------------------------------------------------------------------------------------
			public CIntCalculated(Int32 value, ILotusOwnerObject ownerObject)
			{
				mValue = value;
				mOwner = ownerObject;
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