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
// Последнее изменение от 30.04.2023
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
		/** \addtogroup UnitMeasurement
		*@{*/
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
			/// <param name="unitThing">Единица измерения вещей</param>
			/// <returns>Величина для измерения вещей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateThing(Int32 value, TUnitThing unitThing = TUnitThing.Thing)
			{
				return new TMeasurementValue(value, TMeasurementType.Thing, unitThing);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание величины для измерения длины
			/// </summary>
			/// <param name="value">Количество</param>
			/// <param name="unitLength">Единица измерения длины</param>
			/// <returns>Величина для измерения длины</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateLength(Double value, TUnitLength unitLength = TUnitLength.Meter)
			{
				return new TMeasurementValue(value, TMeasurementType.Length, unitLength);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание величины для измерения площади
			/// </summary>
			/// <param name="value">Количество</param>
			/// <param name="unitArea">Единица измерения площади</param>
			/// <returns>Величина для измерения площади</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TMeasurementValue CreateArea(Double value, TUnitArea unitArea = TUnitArea.SquareMeter)
			{
				return new TMeasurementValue(value, TMeasurementType.Area, unitArea);
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
				var value = new TMeasurementValue();
				return value;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			internal Double mValue;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			internal TMeasurementType mMeasurementType;

#if UNITY_2017_1_OR_NEWER
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
				readonly get { return mValue; }
				set
				{
					mValue = value;
				}
			}

			/// <summary>
			/// Тип измерения
			/// </summary>
			public readonly TMeasurementType QuantityType
			{
				get { return mMeasurementType; }
			}

			/// <summary>
			/// Единица измерения
			/// </summary>
			public readonly Enum UnitType
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
			/// <param name="measurementType">Тип измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, TMeasurementType measurementType)
			{
				mValue = value;
				mMeasurementType = measurementType;
				mUnitType = mMeasurementType.GetUnitValueDefault();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="measurementType">Тип измерения</param>
			/// <param name="unitType">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, TMeasurementType measurementType, Enum unitType)
			{
				mValue = value;
				mMeasurementType = measurementType;
				mUnitType = unitType;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Значение</param>
			/// <param name="unitType">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public TMeasurementValue(Double value, Enum unitType)
			{
				mValue = value;
				mUnitType = unitType;
				mMeasurementType = XUnitType.GetMeasurementType(unitType);
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
			public override readonly Boolean Equals(System.Object obj)
			{
				if (obj != null)
				{
					if (obj is TMeasurementValue)
					{
						var value = (TMeasurementValue)obj;
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
			public readonly Boolean Equals(TMeasurementValue other)
			{
				return mValue == other.mValue && mUnitType == other.mUnitType;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly Int32 CompareTo(TMeasurementValue other)
			{
				return mValue.CompareTo(other.mValue);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода объекта
			/// </summary>
			/// <returns>Хеш-код объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly Int32 GetHashCode()
			{
				return mValue.GetHashCode() ^ base.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта
			/// </summary>
			/// <returns>Копия объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly System.Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override readonly String ToString()
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
			public readonly TMeasurementValue Clone(Double value)
			{
				return new TMeasurementValue(value, mMeasurementType, mUnitType);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение копии объекта с новыми параметрами
			/// </summary>
			/// <param name="unitType">Новая единица измерения</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly TMeasurementValue Clone(Enum unitType)
			{
				return new TMeasurementValue(mValue, unitType);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка типа измерения и единицы измерения
			/// </summary>
			/// <param name="measurementType">Тип измерения</param>
			/// <param name="unitType">Единица измерения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetQuantityAndUnit(TMeasurementType measurementType, Enum unitType)
			{
				mMeasurementType = measurementType;
				if(unitType != null)
				{
					mUnitType = unitType;
				}
				else
				{
					mUnitType = measurementType.GetUnitValueDefault();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение аббревиатуры единицы измерения
			/// </summary>
			/// <returns>Аббревиатура единицы измерения </returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly String GetAbbreviationUnit()
			{
				if(mUnitType != null)
				{
					//return (mUnitType.GetAbbreviationOrName());
					return mUnitType.ToString();
				}
				else
				{
					return "нп";
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация объекта в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public readonly String SerializeToString()
			{
				if (mUnitType == null)
				{
					return mValue.ToString() + "{" + mMeasurementType.ToString() + "}[TUnitThing.Undefined]";
				}
				else
				{
					return mValue.ToString() + "{" + mMeasurementType.ToString() + "}[" + mUnitType.ToString() + "]";
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================