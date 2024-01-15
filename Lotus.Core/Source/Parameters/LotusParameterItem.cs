//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема параметрических объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusParameterItem.cs
*		Класс для представления параметра - объекта который содержит данные в формате имя=значения.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup CoreParameters Подсистема параметрических объектов
         * \ingroup Core
         * \brief Подсистема параметрических объектов обеспечивает представление и хранение информации в документоориентированном 
			стиле.
		 * \details Основной объект подсистемы — это параметрический объект который хранит список записей в формате
			имя=значения. При этом сама запись также может представлена в виде параметрического объекта. Это обеспечивает 
			представления иерархических структур данных.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение допустимых типов значения для параметра
		/// </summary>
		/// <remarks>
		/// Определение стандартных типов данных значения в контексте использования параметра.
		/// Типы значений спроектированы с учетом поддержки и реализации в современных документоориентированных СУБД.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TParameterValueType
		{
			//
			// ОСНОВНЫЕ ТИПЫ ДАННЫХ
			//
			/// <summary>
			/// Отсутствие определенного значения
			/// </summary>
			Null,

			/// <summary>
			/// Логический тип
			/// </summary>
			Boolean,

			/// <summary>
			/// Целый тип
			/// </summary>
			Integer,

			/// <summary>
			/// Вещественный тип
			/// </summary>
			Real,

			/// <summary>
			/// Тип даты-времени
			/// </summary>
			DateTime,

			/// <summary>
			/// Строковый тип
			/// </summary>
			String,

			/// <summary>
			/// Перечисление
			/// </summary>
			Enum,

			/// <summary>
			/// Список объектов определённого типа
			/// </summary>
			List,

			/// <summary>
			/// Базовый объект
			/// </summary>
			Object,

			/// <summary>
			/// Список параметрических объектов
			/// </summary>
			Parameters,

			//
			// ДОПОЛНИТЕЛЬНЫЕ ТИПЫ ДАННЫХ 
			//
			/// <summary>
			/// Цвет
			/// </summary>
			Color,

			/// <summary>
			/// Двухмерный объект данных
			/// </summary>
			Vector2D,

			/// <summary>
			/// Трехмерный объект данных
			/// </summary>
			Vector3D,

			/// <summary>
			/// Четырехмерный объект данных
			/// </summary>
			Vector4D
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления параметра - объекта который содержит данные в формате имя=значения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IParameterItem : ICloneable, ILotusNameable, ILotusIdentifierLong, ILotusOwnedObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			TParameterValueType ValueType { get; }

			/// <summary>
			/// Значение параметра
			/// </summary>
			System.Object? Value { get; set; }

			/// <summary>
			/// Активность параметра
			/// </summary>
			/// <remarks>
			/// Условная активность параметра - на усмотрение пользователя
			/// </remarks>
			Boolean IsActive { get; set; }

			/// <summary>
			/// Пользовательский тэг данных
			/// </summary>
			Byte UserTag { get; set; }

			/// <summary>
			/// Пользовательский тип данных
			/// </summary>
			Byte UserData { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления параметра - объекта который содержит данные в формате имя=значения
		/// с конкретным типом данных
		/// </summary>
		/// <typeparam name="TValue">Тип значения</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface IParameterItem<TValue> : IParameterItem
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Значение параметра
			/// </summary>
			new TValue? Value { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для представления параметра - объекта который содержит данные в формате имя=значения
		/// </summary>
		/// <typeparam name="TValue">Тип значения</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public abstract class ParameterItem<TValue> : PropertyChangedBase, IParameterItem<TValue>,
			IComparable<ParameterItem<TValue>>, ILotusDuplicate<ParameterItem<TValue>>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsId = new PropertyChangedEventArgs(nameof(Id));
			protected static readonly PropertyChangedEventArgs PropertyArgsIValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsActive = new PropertyChangedEventArgs(nameof(IsActive));
			protected static readonly PropertyChangedEventArgs PropertyArgsUserTag = new PropertyChangedEventArgs(nameof(UserTag));
			protected static readonly PropertyChangedEventArgs PropertyArgsUserData = new PropertyChangedEventArgs(nameof(UserData));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String _name;
			protected internal TValue? _value;
			protected internal Int64 _id;
			protected internal Boolean _isActive;
			protected internal Byte _userTag;
			protected internal Byte _userData;

			// Владелец
			protected internal ILotusOwnerObject _owner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Наименование параметра
			/// </summary>
			/// <remarks>
			/// Имя параметра должно быть уникальных в пределах параметрического объекта
			/// </remarks>
			[XmlAttribute]
			public String Name
			{
				get { return _name; }
				set 
				{
					_name = value;
					NotifyPropertyChanged(PropertyArgsName);
					if (_owner != null) _owner.OnNotifyUpdated(this, _name, nameof(Name));
				}
			}

			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public virtual TParameterValueType ValueType
			{
				get { return TParameterValueType.Null; }
			}

			/// <summary>
			/// Значение параметра
			/// </summary>
			[XmlIgnore]
			System.Object? IParameterItem.Value
			{
				get { return _value; }
				set 
				{
					if (value == null)
					{
						_value = default;
					}
					else
					{
                        _value = (TValue)value;
                    }
					NotifyPropertyChanged(PropertyArgsIValue);
					if (_owner != null) _owner.OnNotifyUpdated(this, _value, nameof(Value));
				}
			}

			/// <summary>
			/// Значение параметра
			/// </summary>
			[XmlElement]
			public TValue? Value
			{
				get { return _value; }
				set
				{
					_value = value;
					NotifyPropertyChanged(PropertyArgsValue);
					if (_owner != null) _owner.OnNotifyUpdated(this, _value, nameof(Value));
				}
			}

			/// <summary>
			/// Уникальный идентификатор параметра
			/// </summary>
			[XmlAttribute]
			public Int64 Id
			{
				get { return _id; }
				set
				{
					_id = value;
					NotifyPropertyChanged(PropertyArgsId);
					if (_owner != null) _owner.OnNotifyUpdated(this, Id, nameof(Id));
				}
			}

			/// <summary>
			/// Активность параметра
			/// </summary>
			/// <remarks>
			/// Условная активность параметра - на усмотрение пользователя
			/// </remarks>
			[XmlAttribute]
			public Boolean IsActive
			{
				get { return _isActive; }
				set 
				{
					_isActive = value;
					NotifyPropertyChanged(PropertyArgsIsActive);
					if (_owner != null) _owner.OnNotifyUpdated(this, IsActive, nameof(IsActive));
				}
			}

			/// <summary>
			/// Пользовательский тэг данных
			/// </summary>
			[XmlAttribute]
			public Byte UserTag
			{
				get { return _userTag; }
				set 
				{
					_userTag = value;
					NotifyPropertyChanged(PropertyArgsUserTag);
					if (_owner != null) _owner.OnNotifyUpdated(this, UserTag, nameof(UserTag));
				}
			}

			/// <summary>
			/// Пользовательский тип данных
			/// </summary>
			[XmlAttribute]
			public Byte UserData
			{
				get { return _userData; }
				set 
				{
					_userData = value;
					NotifyPropertyChanged(PropertyArgsUserData);
					if (_owner != null) _owner.OnNotifyUpdated(this, UserData, nameof(UserData));
				}
			}

			/// <summary>
			/// Владелец параметра
			/// </summary>
			[XmlIgnore]
			public ILotusOwnerObject? IOwner
			{
				get { return _owner; }
				set { _owner = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem()
			{
				_name = "";
				_id = XGenerateId.Generate(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(String parameterName)
			{
				_name = parameterName;
				_id = XGenerateId.Generate(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(Int64 id)
			{
				_name = "";
				_id = id;
			}
			#endregion
			
			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение параметров для упорядочивания
			/// </summary>
			/// <param name="other">Параметр</param>
			/// <returns>Статус сравнения параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(ParameterItem<TValue>? other)
			{
				return String.CompareOrdinal(Name, other?.Name);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование параметра
			/// </summary>
			/// <returns>Копия объекта параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <param name="parameters">Параметры дублирования объекта</param>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public ParameterItem<TValue> Duplicate(CParameters? parameters = null)
			{
				return (MemberwiseClone() as ParameterItem<TValue>)!;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				var result = String.Format("{0} = {1}", _name, _value?.ToString());
				return result;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================