//=====================================================================================================================
// Проект: Модуль единиц измерения
// Раздел: Типы измерения
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnitMeasurementType.cs
*		Определение основных типов измерения.
*		Тип измерения определяет для какой конкретно физической величины или математической абстракции происходит измерения. 
*	В зависимости от типа измерения определяется доступный набор единиц измерения.
*		Типы измерения бывают базовые, производные и комплексные.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace UnitMeasurement
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup UnitMeasurement
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс определяющий величину и позволяющий динамически менять тип единицы измерения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public struct TMeasurementValue : IEquatable<TMeasurementValue>, IComparable<TMeasurementValue>, ICloneable
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			/// <summary>
			/// Пустое значение
			/// </summary>
			public static readonly TMeasurementValue Empty = new TMeasurementValue();
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание величины для измерения вещей
			/// </summary>
			/// <param name="value">Количество</param>
			/// <param name="unit_thing">Единица измерения вещей</param>
			/// <returns>Величина для измерения вещей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateThing(Int32 value, TUnitThing unit_thing = TUnitThing.Thing)
			{
				return (new TMeasurementValue(value, TMeasurementType.Thing, unit_thing));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание величины для измерения длины
			/// </summary>
			/// <param name="value">Количество</param>
			/// <param name="unit_length">Единица измерения длины</param>
			/// <returns>Величина для измерения длины</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateLength(Double value, TUnitLength unit_length = TUnitLength.Meter)
			{
				return (new TMeasurementValue(value, TMeasurementType.Length, unit_length));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание величины для измерения площади
			/// </summary>
			/// <param name="value">Количество</param>
			/// <param name="unit_area">Единица измерения площади</param>
			/// <returns>Величина для измерения площади</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateArea(Double value, TUnitArea unit_area = TUnitArea.SquareMeter)
			{
				return (new TMeasurementValue(value, TMeasurementType.Area, unit_area));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация объекта из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue DeserializeFromString(String data)
			{
				//String number = data.Substring(0, data.IndexOf('{'));

				//String measurement_str = data.ExtractString("{", "}");
				//TMeasurementType measurement = XExtensionMeasurementType.Parse(measurement_str);

				//String unit_str = data.ExtractString("[", "]");
				//Enum unit = measurement.GetUnitValueFromString(unit_str);

				//TMeasurementValue value = new TMeasurementValue(XNumbers.ParseDouble(number), measurement, unit);
				TMeasurementValue value = new TMeasurementValue();
				return value;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.SerializeField]
#endif
			internal Double mValue;

#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.SerializeField]
#endif
			internal TMeasurementType mMeasurementType;

#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.SerializeField]
#endif
			internal Enum mUnitType;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Значение
			/// </summary>
			public Double Value
			{
				get { return (mValue); }
				set
				{
					mValue = value;
				}
			}

			/// <summary>
			/// Тип измерения
			/// </summary>
			public TMeasurementType QuantityType
			{
				get { return mMeasurementType; }
			}

			/// <summary>
			/// Единица измерения
			/// </summary>
			public Enum UnitType
			{
				get { return mUnitType; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="measurement_type">Тип измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, TMeasurementType measurement_type)
			{
				mValue = value;
				mMeasurementType = measurement_type;
				mUnitType = mMeasurementType.GetUnitValueDefault();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="measurement_type">Тип измерения</param>
			/// <param name="unit_type">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, TMeasurementType measurement_type, Enum unit_type)
			{
				mValue = value;
				mMeasurementType = measurement_type;
				mUnitType = unit_type;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="unit_type">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, Enum unit_type)
			{
				mValue = value;
				mUnitType = unit_type;
				mMeasurementType = XUnitType.GetMeasurementType(unit_type);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object obj)
			{
				if (obj != null)
				{
					if (typeof(TMeasurementValue) == obj.GetType())
					{
						TMeasurementValue value = (TMeasurementValue)obj;
						return Equals(value);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(TMeasurementValue other)
			{
				return (mValue == other.mValue && mUnitType == other.mUnitType);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(TMeasurementValue other)
			{
				return mValue.CompareTo(other.mValue);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода объекта
			/// </summary>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return mValue.GetHashCode() ^ base.GetHashCode();
			}

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
				return mValue.ToString() + " " + GetAbbreviationUnit();
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на равенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус равенства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(TMeasurementValue left, TMeasurementValue right)
			{
				return left.Equals(right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на неравенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус неравенство</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(TMeasurementValue left, TMeasurementValue right)
			{
				return !(left == right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов по операции меньше
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус операции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator <(TMeasurementValue left, TMeasurementValue right)
			{
				return left.CompareTo(right) < 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов по операции меньше или равно
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус операции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator <=(TMeasurementValue left, TMeasurementValue right)
			{
				return left.CompareTo(right) <= 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов по операции больше
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус операции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator >(TMeasurementValue left, TMeasurementValue right)
			{
				return left.CompareTo(right) > 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов по операции больше или равно
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус операции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator >=(TMeasurementValue left, TMeasurementValue right)
			{
				return left.CompareTo(right) >= 0;
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Double"/>
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Объект <see cref="System.Double"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator Double(TMeasurementValue value)
			{
				return value.mValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Double"/>
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Объект <see cref="TMeasurementValue"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator TMeasurementValue(Double value)
			{
				return new TMeasurementValue(value, TMeasurementType.Undefined);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта с новыми параметрами
			/// </summary>
			/// <param name="value">Новое значение</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue Clone(Double value)
			{
				return (new TMeasurementValue(value, mMeasurementType, mUnitType));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта с новыми параметрами
			/// </summary>
			/// <param name="unit_type">Новая единица измерения</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue Clone(Enum unit_type)
			{
				return (new TMeasurementValue(mValue, unit_type));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка типа измерения и единицы измерения
			/// </summary>
			/// <param name="measurement_type">Тип измерения</param>
			/// <param name="unit_type">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetQuantityAndUnit(TMeasurementType measurement_type, Enum unit_type)
			{
				mMeasurementType = measurement_type;
				if(unit_type != null)
				{
					mUnitType = unit_type;
				}
				else
				{
					mUnitType = measurement_type.GetUnitValueDefault();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение аббревиатуры единицы измерения
			/// </summary>
			/// <returns>Аббревиатура единицы измерения </returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetAbbreviationUnit()
			{
				if(mUnitType != null)
				{
					//return (mUnitType.GetAbbreviationOrName());
					return (mUnitType.ToString());
				}
				else
				{
					return ("нп");
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация объекта в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public String SerializeToString()
			{
				if (mUnitType == null)
				{
					return (mValue.ToString() + "{" + mMeasurementType.ToString() + "}[TUnitThing.Undefined]");
				}
				else
				{
					return (mValue.ToString() + "{" + mMeasurementType.ToString() + "}[" + mUnitType.ToString() + "]");
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================