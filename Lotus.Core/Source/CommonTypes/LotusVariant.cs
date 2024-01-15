//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема общих типов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusVariant.cs
*		Универсальный тип данных.
*		Реализация универсального типа данных с поддержкой сериализации на уровне Unity. Данный тип хранит один из возможных
*	предопределённых типов данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Numerics;
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
		/// Определение допустимых типов значений
		/// </summary>
		/// <remarks>
		/// Определение стандартных типов данных значения в контексте использования универсального типа.
		/// Указанные типы данных имеют прямую поддержку в подсистеме сообщений.
		/// Также они охватываю более 90% функциональности которая требуется в проектах с поддержкой универсального типа.
		/// Для Unity определенна поддержка игровых объектов и наиболее распространённых ресурсов
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TValueType
		{
			/// <summary>
			/// Отсутствие определенного значения
			/// </summary>
			Void,

			/// <summary>
			/// Логический тип
			/// </summary>
			Boolean,

			/// <summary>
			/// Целый тип
			/// </summary>
			Integer,

			/// <summary>
			/// Перечисление
			/// </summary>
			Enum,

			/// <summary>
			/// Вещественный тип
			/// </summary>
			Float,

			/// <summary>
			/// Тип даты-времени
			/// </summary>
			DateTime,

			/// <summary>
			/// Строковый тип
			/// </summary>
			String,

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
			Vector4D,

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Игровой объект
			/// </summary>
			GameObject,

			/// <summary>
			/// Ресурс Unity - двухмерная текстура
			/// </summary>
			Texture2D,

			/// <summary>
			/// Ресурс Unity - спрайт
			/// </summary>
			Sprite,

			/// <summary>
			/// Ресурс Unity - трехмерная модель
			/// </summary>
			Model,

			/// <summary>
			/// Ресурс Unity - текстовые или бинарные данные
			/// </summary>
			TextAsset,
#endif
			/// <summary>
			/// Базовый объект
			/// </summary>
			SysObject
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Универсальный тип данных
		/// </summary>
		/// <remarks>
		/// Реализация универсального типа данных с поддержкой сериализации на уровне Unity.
		/// Данный тип хранит один из возможных предопределённых типов данных.
		/// Для однозначной идентификации объектов которые не могут быть сведены к стандартному типу в поле <see cref="CVariant.StringValue"/>
		/// храниться контекстная информация об объекте (его тип, ссылка и т.д.).
		/// При установке владельца интерфейса для уведомлений, объект уведомляет об изменении своих свойств.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CVariant : IComparable<CVariant>, ILotusOwnedObject, ICloneable
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация универсального типа данных из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Универсальный тип данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CVariant DeserializeFromString(String data)
			{
				//
				// TODO
				// 
				var variant = new CVariant();
				return variant;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TValueType _valueType;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal String _stringData;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
			protected internal UnityEngine.Vector4 _numberData;
#else
			internal Vector4 _numberData;
#endif
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal System.Object _referenceData;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
			protected internal UnityEngine.Object _unityData;
#endif

			[NonSerialized]
			protected internal ILotusOwnerObject _owner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Тип данных значения
			/// </summary>
			public TValueType ValueType
			{
				get { return _valueType; }
				set
				{
					_valueType = value;
					if (_owner != null) _owner.OnNotifyUpdated(this, _valueType, nameof(ValueType));
				}
			}

			/// <summary>
			/// Логический тип
			/// </summary>
			public Boolean BooleanValue
			{
#if UNITY_2017_1_OR_NEWER
				get { return _numberData.x == 1; }
				set
				{
					if (value)
					{
						_numberData.x = 1;
					}
					else
					{
						_numberData.x = 0;
					}

					_valueType = TValueType.Boolean;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData.x == 1, nameof(BooleanValue));
				}
#else
				get { return _numberData.X == 1; }
				set
				{
					if (value)
					{
						_numberData.X = 1;
					}
					else
					{
						_numberData.X = 0;
					}

					_valueType = TValueType.Boolean;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData.X == 1, nameof(BooleanValue));
				}
#endif
			}

			/// <summary>
			/// Целый тип
			/// </summary>
			public Int32 IntegerValue
			{
#if UNITY_2017_1_OR_NEWER
				get { return (Int32)_numberData.x; }
				set
				{
					_numberData.x = value;
					_valueType = TValueType.Integer;
					if (_owner != null) _owner.OnNotifyUpdated(this, (Int32)_numberData.x, nameof(IntegerValue));
				}
#else
				get { return (Int32)_numberData.X; }
				set
				{
					_numberData.X = value;
					_valueType = TValueType.Integer;
					if (_owner != null) _owner.OnNotifyUpdated(this, (Int32)_numberData.X, nameof(IntegerValue));
				}
#endif
			}

			/// <summary>
			/// Перечисление
			/// </summary>
			/// <remarks>
			/// Имя реального типа сохраняется в поле <see cref="_stringData"/>
			/// </remarks>
			public Enum? EnumValue
			{
				get
				{
					if (_referenceData != null && _referenceData.GetType().IsEnum)
					{
						return (Enum)_referenceData;
					}
					else
					{
						return null;
					}
				}
				set
				{
					_referenceData = value;
					_valueType = TValueType.Enum;
					if (_referenceData != null)
					{
						_stringData = _referenceData.GetType().Name;
					}
					if (_owner != null) _owner.OnNotifyUpdated(this, EnumValue, nameof(EnumValue));
				}
			}

			/// <summary>
			/// Вещественный тип
			/// </summary>
			public Single FloatValue
			{
#if UNITY_2017_1_OR_NEWER
				get { return _numberData.x; }
				set
				{
					_numberData.x = value;
					_valueType = TValueType.Float;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData.x, nameof(FloatValue));
				}
#else
				get { return (Single)_numberData.X; }
				set
				{
					_numberData.X = value;
					_valueType = TValueType.Float;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData.X, nameof(FloatValue));
				}
#endif
			}

			/// <summary>
			/// Тип даты-времени
			/// </summary>
			public DateTime DateTimeValue
			{
				get { return DateTime.Parse(_stringData); }
				set
				{
					_stringData = value.ToString();
					_valueType = TValueType.DateTime;
					if (_owner != null) _owner.OnNotifyUpdated(this, DateTime.Parse(_stringData), nameof(DateTimeValue));
				}
			}

			/// <summary>
			/// Строковый тип
			/// </summary>
			public String StringValue
			{
				get { return _stringData; }
				set
				{
					_stringData = value;
					_valueType = TValueType.String;
					if (_owner != null) _owner.OnNotifyUpdated(this, _stringData, nameof(StringValue));
				}
			}

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Двухмерный объект данных
			/// </summary>
			public UnityEngine.Vector2 Vector2DValue
			{
				get { return _numberData; }
				set
				{
					_numberData = value;
					_valueType = TValueType.Vector2D;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData, nameof(Vector2DValue));
				}
			}

			/// <summary>
			/// Трехмерный объект данных
			/// </summary>
			public UnityEngine.Vector3 Vector3DValue
			{
				get { return _numberData; }
				set
				{
					_numberData = value;
					_valueType = TValueType.Vector3D;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData, nameof(Vector3DValue));
				}
			}

			/// <summary>
			/// Четырехмерный объект данных
			/// </summary>
			public UnityEngine.Vector4 Vector4DValue
			{
				get { return _numberData; }
				set
				{
					_numberData = value;
					_valueType = TValueType.Vector4D;
					if (_owner != null) _owner.OnNotifyUpdated(this, _numberData, nameof(Vector4DValue));
				}
			}

			/// <summary>
			/// Цвет
			/// </summary>
			public UnityEngine.Color ColorValue
			{
				get { return new UnityEngine.Color(_numberData.x, _numberData.y, _numberData.z, _numberData.w); }
				set
				{
					_numberData.x = value.r;
					_numberData.y = value.g;
					_numberData.z = value.b;
					_numberData.w = value.a;
					_valueType = TValueType.Color;
					if (_owner != null) _owner.OnNotifyUpdated(this, ColorValue, nameof(ColorValue));
				}
			}
#else
			/// <summary>
			/// Двухмерный объект данных
			/// </summary>
			public Vector2 Vector2DValue
			{
				get { return new Vector2(_numberData.X, _numberData.Y); }
				set
				{
					_numberData.X = value.X;
					_numberData.Y = value.Y;
					_valueType = TValueType.Vector2D;
					if (_owner != null) _owner.OnNotifyUpdated(this, Vector2DValue, nameof(Vector2DValue));
				}
			}

			/// <summary>
			/// Трехмерный объект данных
			/// </summary>
			public Vector3 Vector3DValue
			{
				get { return new Vector3(_numberData.X, _numberData.Y, _numberData.Z); }
				set
				{
					_numberData.X = value.X;
					_numberData.Y = value.Y;
					_numberData.Z = value.Z;
					_valueType = TValueType.Vector3D;
					if (_owner != null) _owner.OnNotifyUpdated(this, Vector2DValue, nameof(Vector3DValue));
				}
			}

			/// <summary>
			/// Четырехмерный объект данных
			/// </summary>
			public Vector4 Vector4DValue
			{
				get { return _numberData; }
				set
				{
					_numberData = value;
					_valueType = TValueType.Vector4D;
					if (_owner != null) _owner.OnNotifyUpdated(this, Vector2DValue, nameof(Vector4DValue));
				}
			}

			/// <summary>
			/// Цвет
			/// </summary>
			public TColor ColorValue
			{
				get { return new TColor((Byte)_numberData.X, (Byte)_numberData.Y, (Byte)_numberData.Z, (Byte)_numberData.W); }
				set
				{
					_numberData.X = value.R;
					_numberData.Y = value.G;
					_numberData.Z = value.B;
					_numberData.W = value.A;
					_valueType = TValueType.Color;
					if (_owner != null) _owner.OnNotifyUpdated(this, ColorValue, nameof(ColorValue));
				}
			}
#endif
			/// <summary>
			/// Базовый объект
			/// </summary>
			/// <remarks>
			/// Имя реального типа объекта сохраняется в поле <see cref="_stringData"/>
			/// </remarks>
			public System.Object SysObject
			{
				get { return _referenceData; }
				set
				{
					_referenceData = value;
					_valueType = TValueType.SysObject;
					if (_referenceData != null)
					{
						_stringData = _referenceData.GetType().Name;
					}

					if (_owner != null) _owner.OnNotifyUpdated(this, _referenceData, nameof(SysObject));
				}
			}

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Игровой объект
			/// </summary>
			public UnityEngine.GameObject GameObjectValue
			{
				get 
				{ 
					return _unityData as UnityEngine.GameObject; 
				}
				set
				{
					_unityData = value;
					_valueType = TValueType.GameObject;
					if (_owner != null) _owner.OnNotifyUpdated(this, _unityData as UnityEngine.GameObject, nameof(GameObjectValue));
				}
			}

			/// <summary>
			/// Ресурс Unity - двухмерная текстура
			/// </summary>
			public UnityEngine.Texture2D Texture2DValue
			{
				get
				{
					return _unityData as UnityEngine.Texture2D;
				}
				set
				{
					_unityData = value;
					_valueType = TValueType.Texture2D;
					if (_owner != null) _owner.OnNotifyUpdated(this, _unityData as UnityEngine.Texture2D, nameof(Texture2DValue));
				}
			}

			/// <summary>
			/// Ресурс Unity - спрайт
			/// </summary>
			public UnityEngine.Sprite SpriteValue
			{
				get
				{
					return _unityData as UnityEngine.Sprite;
				}
				set
				{
					_unityData = value;
					_valueType = TValueType.Sprite;
					if (_owner != null) _owner.OnNotifyUpdated(this, _unityData as UnityEngine.Sprite, nameof(SpriteValue));
				}
			}

			/// <summary>
			/// Ресурс Unity - трехмерная модель
			/// </summary>
			public UnityEngine.GameObject ModelValue
			{
				get
				{
					return _unityData as UnityEngine.GameObject;
				}
				set
				{
					_unityData = value;
					_valueType = TValueType.Model;
					if (_owner != null) _owner.OnNotifyUpdated(this, _unityData as UnityEngine.GameObject, nameof(ModelValue));
				}
			}

			/// <summary>
			/// Ресурс Unity - текстовые или бинарные данные
			/// </summary>
			public UnityEngine.TextAsset TextAssetValue
			{
				get
				{
					return _unityData as UnityEngine.TextAsset;
				}
				set
				{
					_unityData = value;
					_valueType = TValueType.TextAsset;
					if (_owner != null) _owner.OnNotifyUpdated(this, _unityData as UnityEngine.TextAsset, nameof(TextAssetValue));
				}
			}
#endif
			/// <summary>
			/// Владелец
			/// </summary>
			public ILotusOwnerObject? IOwner
			{
				get { return _owner; }
				set
				{
					_owner = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные универсального типа нулевыми значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CVariant()
			{
				_valueType = TValueType.Void;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные универсального типа указанным объектом
			/// </summary>
			/// <remarks>
			/// Тип объекта выводится автоматически
			/// </remarks>
			/// <param name="obj">Объект</param>
			//---------------------------------------------------------------------------------------------------------
			public CVariant(System.Object obj)
			{
				Set(obj);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение универсальных типов данных для упорядочивания
			/// </summary>
			/// <param name="other">Объект универсального типа данных</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CVariant? other)
			{
				if (other == null) return 0;

				var result = 0;
				switch (_valueType)
				{
					case TValueType.Void:
						break;
					case TValueType.Boolean:
						break;
					case TValueType.Integer:
						{
							result = Comparer<Int32>.Default.Compare(IntegerValue, other.IntegerValue);
						}
						break;
					case TValueType.Enum:
						break;
					case TValueType.Float:
						{
							result = Comparer<Single>.Default.Compare(FloatValue, other.FloatValue);
						}
						break;
					case TValueType.DateTime:
						{
							result = Comparer<DateTime>.Default.Compare(DateTimeValue, other.DateTimeValue);
						}
						break;
					case TValueType.String:
						{
							result = String.CompareOrdinal(StringValue, other.StringValue);
						}
						break;
					case TValueType.Color:
						{
#if UNITY_2017_1_OR_NEWER
							result = Comparer<UnityEngine.Color>.Default.Compare(ColorValue, other.ColorValue);
#else
							result = Comparer<TColor>.Default.Compare(ColorValue, other.ColorValue);
#endif
						}
						break;
					case TValueType.Vector2D:
						break;
					case TValueType.Vector3D:
						break;
					case TValueType.Vector4D:
						break;
#if UNITY_2017_1_OR_NEWER
					case TValueType.GameObject:
					case TValueType.Texture2D:
					case TValueType.Sprite:
					case TValueType.Model:
					case TValueType.TextAsset:
						{
							// Сравниваем по имени
							UnityEngine.Object unity_object_this = _unityData;
							UnityEngine.Object unity_object_other = other._unityData;
							if(unity_object_this != null && unity_object_other != null)
							{
								result = String.CompareOrdinal(unity_object_this.name, unity_object_other.name);
							}
						}
						break;
#endif
					case TValueType.SysObject:
						break;

					default:
						break;
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода объекта универсального типа данных
			/// </summary>
			/// <returns>Хеш-код</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return this.GetHashCode() ^ _numberData.GetHashCode() ^ _stringData.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование объекта универсального типа данных
			/// </summary>
			/// <returns>Копия объекта универсального типа данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String? ToString()
			{
				var result = "";
				switch (_valueType)
				{
					case TValueType.Void:
						{
							result = nameof(TValueType.Void);
						}
						break;
					case TValueType.Boolean:
						{
							result = BooleanValue.ToString();
						}
						break;
					case TValueType.Integer:
						{
							result = IntegerValue.ToString();
						}
						break;
					case TValueType.Enum:
						break;
					case TValueType.Float:
						{
							result = FloatValue.ToString();
						}
						break;
					case TValueType.DateTime:
						break;
					case TValueType.String:
						{
							result = StringValue;
						}
						break;
					case TValueType.Color:
						{
							result = ColorValue.ToString();
						}
						break;
					case TValueType.Vector2D:
						{
							result = Vector2DValue.ToString();
						}
						break;
					case TValueType.Vector3D:
						{
							result = Vector3DValue.ToString();
						}
						break;
					case TValueType.Vector4D:
						{
							result = Vector4DValue.ToString();
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case TValueType.GameObject:
					case TValueType.Texture2D:
					case TValueType.Sprite:
					case TValueType.Model:
					case TValueType.TextAsset:
						{
							result = _unityData?.ToString();
						}
						break;
#endif
					case TValueType.SysObject:
						{
							result = _referenceData.ToString();
						}
						break;

					default:
						break;
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <param name="format">Формат отображения</param>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public String? ToString(String format)
			{
				var result = "";
				switch (_valueType)
				{
					case TValueType.Void:
						{
							result = nameof(TValueType.Void);
						}
						break;
					case TValueType.Boolean:
						{
							result = BooleanValue.ToString();
						}
						break;
					case TValueType.Integer:
						{
							result = IntegerValue.ToString(format);
						}
						break;
					case TValueType.Enum:
						break;
					case TValueType.Float:
						{
							result = FloatValue.ToString(format);
						}
						break;
					case TValueType.DateTime:
						break;
					case TValueType.String:
						{
							result = StringValue;
						}
						break;
					case TValueType.Color:
						{
							result = ColorValue.ToString();
						}
						break;
					case TValueType.Vector2D:
						{
							result = Vector2DValue.ToString(format);
						}
						break;
					case TValueType.Vector3D:
						{
							result = Vector3DValue.ToString(format);
						}
						break;
					case TValueType.Vector4D:
						{
							result = Vector4DValue.ToString(format);
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case TValueType.GameObject:
						{
							result = GameObjectValue.ToString();
						}
						break;
					case TValueType.Texture2D:
						{
							result = Texture2DValue.ToString();
						}
						break;
					case TValueType.Sprite:
						{
							result = SpriteValue.ToString();
						}
						break;
					case TValueType.Model:
						{
							result = ModelValue.ToString();
						}
						break;
					case TValueType.TextAsset:
						{
							result = TextAssetValue.ToString();
						}
						break;
#endif
					case TValueType.SysObject:
						{
							result = _referenceData.ToString();
						}
						break;
					default:
						break;
				}

				return result;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения универсального типа данных
			/// </summary>
			/// <param name="value">Значение</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(System.Object value)
			{
				if (value == null)
				{
					_valueType = TValueType.Void;
					return;
				}

				// Получаем тип
				Type type = value.GetType();
				switch (type.Name)
				{
					case nameof(Boolean):
						{
							var v = (Boolean)value;
#if UNITY_2017_1_OR_NEWER
							if (v)
							{
								_numberData.x = 1;
							}
							else
							{
								_numberData.x = 0;
							}
#else

							if (v)
							{
								_numberData.X = 1;
							}
							else
							{
								_numberData.X = 0;
							}
#endif
							_valueType = TValueType.Boolean;
							if (_owner != null) _owner.OnNotifyUpdated(this, BooleanValue, nameof(BooleanValue));
						}
						break;
					case nameof(Int32):
						{
#if UNITY_2017_1_OR_NEWER
							_numberData.x = (Int32)value;
#else
							_numberData.X = (Int32)value;
#endif
							_valueType = TValueType.Integer;
							if (_owner != null) _owner.OnNotifyUpdated(this, IntegerValue, nameof(IntegerValue));
						}
						break;
					case nameof(Single):
						{
#if UNITY_2017_1_OR_NEWER
							_numberData.x = (Single)value;
#else
							_numberData.X = (Single)value;
#endif
							_valueType = TValueType.Float;
							if (_owner != null) _owner.OnNotifyUpdated(this, FloatValue, nameof(FloatValue));
						}
						break;
					case nameof(DateTime):
						{
							var v = (DateTime)value;
							_stringData = v.ToString();
							_valueType = TValueType.DateTime;
							if (_owner != null) _owner.OnNotifyUpdated(this, DateTimeValue, nameof(DateTimeValue));
						}
						break;
					case nameof(String):
						{
							_stringData = value.ToString();
							_valueType = TValueType.String;
							if (_owner != null) _owner.OnNotifyUpdated(this, StringValue, nameof(StringValue));
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case nameof(UnityEngine.Color):
						{
							var v = (UnityEngine.Color)value;
							_numberData.x = v.r;
							_numberData.y = v.g;
							_numberData.z = v.b;
							_numberData.w = v.a;
							_valueType = TValueType.Color;
							if (_owner != null) _owner.OnNotifyUpdated(this, ColorValue, nameof(ColorValue));
						}
						break;
					case nameof(UnityEngine.Vector2):
						{
							var v = (UnityEngine.Vector2)value;
							_numberData = v;
							_valueType = TValueType.Vector2D;
							if (_owner != null) _owner.OnNotifyUpdated(this, Vector2DValue, nameof(Vector2DValue));
						}
						break;
					case nameof(UnityEngine.Vector3):
						{
							var v = (UnityEngine.Vector3)value;
							_numberData = v;
							_valueType = TValueType.Vector3D;
							if (_owner != null) _owner.OnNotifyUpdated(this, Vector3DValue, nameof(Vector3DValue));
						}
						break;
					case nameof(UnityEngine.Vector4):
						{
							var v = (UnityEngine.Vector4)value;
							_numberData = v;
							_valueType = TValueType.Vector4D;
							if (_owner != null) _owner.OnNotifyUpdated(this, Vector4DValue, nameof(Vector4DValue));
						}
						break;
#endif
					default:
						{
							// Проверка на перечисление
							if (type.IsEnum)
							{
								_referenceData = value;
								_stringData = type.Name;
								if (_owner != null) _owner.OnNotifyUpdated(this, EnumValue, nameof(EnumValue));
								break;
							}

#if UNITY_2017_1_OR_NEWER
							// Проверка на тип Unity
							if (type == typeof(UnityEngine.GameObject))
							{
								var game_object = value as UnityEngine.GameObject;
								_unityData = value as UnityEngine.GameObject;
								if (game_object.scene.name == null)
								{
									_valueType = TValueType.Model;
									if (_owner != null) _owner.OnNotifyUpdated(this, ModelValue, nameof(ModelValue));
								}
								else
								{
									_valueType = TValueType.GameObject;
									if (_owner != null) _owner.OnNotifyUpdated(this, GameObjectValue, nameof(GameObjectValue));
								}
								break;
							}

							// Проверка на тип Texture2D
							if (type == typeof(UnityEngine.Texture2D))
							{
								_unityData = value as UnityEngine.Texture2D;
								_valueType = TValueType.Texture2D;
								if (_owner != null) _owner.OnNotifyUpdated(this, Texture2DValue, nameof(Texture2DValue));
								break;
							}

							// Проверка на тип Sprite
							if (type == typeof(UnityEngine.Sprite))
							{
								_unityData = value as UnityEngine.Sprite;
								_valueType = TValueType.Sprite;
								if (_owner != null) _owner.OnNotifyUpdated(this, SpriteValue, nameof(SpriteValue));
								break;
							}

							// Проверка на тип TextAsset
							if (type == typeof(UnityEngine.TextAsset))
							{
								_unityData = value as UnityEngine.TextAsset;
								_valueType = TValueType.TextAsset;
								if (_owner != null) _owner.OnNotifyUpdated(this, TextAssetValue, nameof(TextAssetValue));
								break;
							}
#endif
							_referenceData = value;
							_valueType = TValueType.SysObject;
							_stringData = type.Name;
							if (_owner != null) _owner.OnNotifyUpdated(this, SysObject, nameof(SysObject));

						}
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка типа универсального типа данных
			/// </summary>
			/// <param name="type">Тип</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(Type type)
			{
				switch (type.Name)
				{
					case nameof(Boolean):
						{
							_valueType = TValueType.Boolean;
						}
						break;
					case nameof(Int32):
						{
							_valueType = TValueType.Integer;
						}
						break;
					case nameof(Single):
						{
							_valueType = TValueType.Float;
						}
						break;
					case nameof(DateTime):
						{
							_valueType = TValueType.DateTime;
						}
						break;
					case nameof(String):
						{
							_valueType = TValueType.String;
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case nameof(UnityEngine.Color):
						{
							_valueType = TValueType.Color;
						}
						break;
					case nameof(UnityEngine.Vector2):
						{
							_valueType = TValueType.Vector2D;
						}
						break;
					case nameof(UnityEngine.Vector3):
						{
							_valueType = TValueType.Vector3D;
						}
						break;
					case nameof(UnityEngine.Vector4):
						{
							_valueType = TValueType.Vector4D;
						}
						break;
#endif
					default:
						{
							// Проверка на перечисление
							if (type.IsEnum)
							{
								_valueType = TValueType.Enum;
								break;
							}

#if UNITY_2017_1_OR_NEWER

							// Проверка на тип Unity
							if (type == typeof(UnityEngine.GameObject))
							{
								_valueType = TValueType.GameObject;
								break;
							}

							// Проверка на тип Texture2D
							if (type == typeof(UnityEngine.Texture2D))
							{
								_valueType = TValueType.Texture2D;
								break;
							}

							// Проверка на тип Sprite
							if (type == typeof(UnityEngine.Sprite))
							{
								_valueType = TValueType.Sprite;
								break;
							}

							// Проверка на тип TextAsset
							if (type == typeof(UnityEngine.TextAsset))
							{
								_valueType = TValueType.TextAsset;
								break;
							}
#endif
							_valueType = TValueType.SysObject;
							_stringData = type.Name;

						}
						break;
				}

				if (_owner != null) _owner.OnNotifyUpdated(this, ValueType, nameof(ValueType));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значение соответствующего типа
			/// </summary>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object? Get()
			{
				switch (_valueType)
				{
					case TValueType.Void:
						{
							return null;
						}
					case TValueType.Boolean:
						{
#if UNITY_2017_1_OR_NEWER
							return _numberData.x == 1;
#else
							return _numberData.X == 1;
#endif
						}
					case TValueType.Integer:
						{
#if UNITY_2017_1_OR_NEWER
							return (Int32)_numberData.x;
#else
							return (Int32)_numberData.X;
#endif
						}
					case TValueType.Enum:
						{
							return _referenceData;
						}
					case TValueType.Float:
						{
#if UNITY_2017_1_OR_NEWER
							return _numberData.x;
#else
							return _numberData.X;
#endif
						}
					case TValueType.DateTime:
						{
							return DateTime.Parse(_stringData);
						}
					case TValueType.String:
						{
							return _stringData;
						}
#if UNITY_2017_1_OR_NEWER
					case TValueType.Color:
						{
							return new UnityEngine.Color(_numberData.x, _numberData.y, _numberData.z, _numberData.w);
						}
					case TValueType.Vector2D:
						{
							return new UnityEngine.Vector2(_numberData.x, _numberData.y);
						}
					case TValueType.Vector3D:
						{
							return new UnityEngine.Vector3(_numberData.x, _numberData.y, _numberData.z);
						}
					case TValueType.Vector4D:
						{
							return new UnityEngine.Vector4(_numberData.x, _numberData.y, _numberData.z, _numberData.w);
						}
					case TValueType.GameObject:
						{
							return _unityData;
						}
					case TValueType.Texture2D:
						{
							return _unityData;
						}
					case TValueType.Sprite:
						{
							return _unityData;
						}
					case TValueType.Model:
						{
							return _unityData;
						}
					case TValueType.TextAsset:
						{
							return _unityData;
						}
#endif
					case TValueType.SysObject:
						break;
					default:
						{
						}
						break;
				}

				return _referenceData;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значение как строкового типа
			/// </summary>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public String? GetAsString()
			{
				switch (_valueType)
				{
					case TValueType.Void:
						{
							return nameof(TValueType.Void);
						}
					case TValueType.Boolean:
						{
#if UNITY_2017_1_OR_NEWER
							return(_numberData.x == 1).ToString();
#else
							return (_numberData.X == 1).ToString();
#endif
						}
					case TValueType.Integer:
						{
#if UNITY_2017_1_OR_NEWER
							return ((Int32)_numberData.x).ToString();
#else
							return ((Int32)_numberData.X).ToString();
#endif
						}
					case TValueType.Enum:
						{
							if (_referenceData == null)
							{
								return "Null";
							}
							else
							{
								return _referenceData.ToString();
							}
						}
					case TValueType.Float:
						{
#if UNITY_2017_1_OR_NEWER
							return _numberData.x.ToString();
#else
							return _numberData.X.ToString();
#endif
						}
					case TValueType.DateTime:
						{
							return _stringData;
						}
					case TValueType.String:
						{
							return _stringData;
						}
#if UNITY_2017_1_OR_NEWER
					case TValueType.Color:
						{
							return new UnityEngine.Color(_numberData.x, _numberData.y, _numberData.z, _numberData.w).ToString();
						}
					case TValueType.Vector2D:
						{
							return new UnityEngine.Vector2(_numberData.x, _numberData.y).ToString();
						}
					case TValueType.Vector3D:
						{
							return new UnityEngine.Vector3(_numberData.x, _numberData.y, _numberData.z).ToString();
						}
					case TValueType.Vector4D:
						{
							return new UnityEngine.Vector4(_numberData.x, _numberData.y, _numberData.z, _numberData.w).ToString();
						}
					case TValueType.GameObject:
						{
							return ((UnityEngine.GameObject)_unityData)?.name;
						}
					case TValueType.Texture2D:
						{
							return ((UnityEngine.Texture2D)_unityData)?.name;
						}
					case TValueType.Sprite:
						{
							return ((UnityEngine.Sprite)_unityData)?.name;
						}
					case TValueType.Model:
						{
							return ((UnityEngine.GameObject)_unityData)?.name;
						}
					case TValueType.TextAsset:
						{
							return ((UnityEngine.TextAsset)_unityData)?.name;
						}
#endif
					case TValueType.SysObject:
						break;

					default:
						{
						}
						break;
				}

				if (_referenceData == null)
				{
					return "Null";
				}
				else
				{
					return _referenceData.ToString();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				_valueType = TValueType.Void;
				if (_owner != null) _owner.OnNotifyUpdated(this, ValueType, nameof(ValueType));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация типа данных в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual String SerializeToString()
			{
				return String.Format("[{0}]", _valueType);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================