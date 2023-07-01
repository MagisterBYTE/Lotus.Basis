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
		public interface IParameterItem : ICloneable, ILotusNameable, ILotusIdentifierId, ILotusOwnedObject
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
			System.Object Value { get; set; }

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
			new TValue Value { get; set; }
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
			protected internal String mName;
			protected internal TValue mValue;
			protected internal Int64 mId;
			protected internal Boolean mIsActive;
			protected internal Byte mUserTag;
			protected internal Byte mUserData;

			// Владелец
			protected internal ILotusOwnerObject mOwner;
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
				get { return mName; }
				set 
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mName, nameof(Name));
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
			System.Object IParameterItem.Value
			{
				get { return mValue; }
				set 
				{
					mValue = (TValue)value;
					NotifyPropertyChanged(PropertyArgsIValue);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mValue, nameof(Value));
				}
			}

			/// <summary>
			/// Значение параметра
			/// </summary>
			[XmlElement]
			public TValue Value
			{
				get { return mValue; }
				set
				{
					mValue = value;
					NotifyPropertyChanged(PropertyArgsValue);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mValue, nameof(Value));
				}
			}

			/// <summary>
			/// Уникальный идентификатор параметра
			/// </summary>
			[XmlAttribute]
			public Int64 Id
			{
				get { return mId; }
				set
				{
					mId = value;
					NotifyPropertyChanged(PropertyArgsId);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, Id, nameof(Id));
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
				get { return mIsActive; }
				set 
				{
					mIsActive = value;
					NotifyPropertyChanged(PropertyArgsIsActive);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, IsActive, nameof(IsActive));
				}
			}

			/// <summary>
			/// Пользовательский тэг данных
			/// </summary>
			[XmlAttribute]
			public Byte UserTag
			{
				get { return mUserTag; }
				set 
				{
					mUserTag = value;
					NotifyPropertyChanged(PropertyArgsUserTag);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, UserTag, nameof(UserTag));
				}
			}

			/// <summary>
			/// Пользовательский тип данных
			/// </summary>
			[XmlAttribute]
			public Byte UserData
			{
				get { return mUserData; }
				set 
				{
					mUserData = value;
					NotifyPropertyChanged(PropertyArgsUserData);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, UserData, nameof(UserData));
				}
			}

			/// <summary>
			/// Владелец параметра
			/// </summary>
			[XmlIgnore]
			public ILotusOwnerObject IOwner
			{
				get { return mOwner; }
				set { mOwner = value; }
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
				mName = "";
				mId = XGenerateId.Generate(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(String parameterName)
			{
				mName = parameterName;
				mId = XGenerateId.Generate(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(Int64 id)
			{
				mName = "";
				mId = id;
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
			public Int32 CompareTo(ParameterItem<TValue> other)
			{
				return String.CompareOrdinal(Name, other.Name);
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
			public ParameterItem<TValue> Duplicate(CParameters parameters = null)
			{
				return MemberwiseClone() as ParameterItem<TValue>;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				var result = String.Format("{0} = {1}", mName, base.ToString());
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