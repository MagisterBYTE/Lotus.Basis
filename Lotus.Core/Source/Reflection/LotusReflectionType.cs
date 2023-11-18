//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема рефлексии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusReflectionType.cs
*		Класс содержащий тип и его кэшированные данные рефлексии.
*		Реализация кэширование всех членов данных типа, с возможность быстрого поиска членов, проверкой на поддержку
*	интерфейса и другими функциями.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreReflection
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс содержащий тип и его кэшированные данные рефлексии
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CReflectedType : IDisposable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Флаги метаданных используемые при поиске полей
			/// </summary>
			public const BindingFlags BINDINGFIELDS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

			/// <summary>
			/// Флаги метаданных используемые при поиске свойств
			/// </summary>
			public const BindingFlags BINDINGPROPERTIES = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

			/// <summary>
			/// Флаги метаданных используемые при поиске методов
			/// </summary>
			public const BindingFlags BINDINGMETHODS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			#endregion

			#region ======================================= СТАТИСТИЧЕСКИЕ ДАННЫЕ =====================================
			/// <summary>
			/// Массив списка аргументов для одного аргумента
			/// </summary>
			public static readonly System.Object[] ArgList1 = new System.Object[1];

			/// <summary>
			/// Массив списка аргументов для двух аргументов
			/// </summary>
			public static readonly System.Object[] ArgList2 = new System.Object[2];

			/// <summary>
			/// Массив списка аргументов для трех аргументов
			/// </summary>
			public static readonly System.Object[] ArgList3 = new System.Object[3];

			/// <summary>
			/// Массив списка аргументов для четырех аргументов
			/// </summary>
			public static readonly System.Object[] ArgList4 = new System.Object[4];
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			//Основные параметры
			protected internal Type _cachedType;
			protected internal Dictionary<String, FieldInfo> _fields;
			protected internal Dictionary<String, PropertyInfo> _properties;
			protected internal Dictionary<String, MethodInfo> _methods;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип - данные которого кэшированные
			/// </summary>
			public Type CachedType
			{
				get { return _cachedType; }
			}

			/// <summary>
			/// Словарь метаданных полей по имени поля
			/// </summary>
			public Dictionary<String, FieldInfo> Fields
			{
				get { return _fields; }
			}

			/// <summary>
			/// Словарь метаданных свойств по имени свойства
			/// </summary>
			public Dictionary<String, PropertyInfo> Properties
			{
				get { return _properties; }
			}

			/// <summary>
			/// Словарь метаданных методов по имени метода
			/// </summary>
			public Dictionary<String, MethodInfo> Methods
			{
				get { return _methods; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект указанным типом
			/// </summary>
			/// <param name="cachedType">Тип данные которого будут сохранены</param>
			//---------------------------------------------------------------------------------------------------------
			public CReflectedType(Type cachedType)
			{
				_cachedType = cachedType;
				
				GetFields();
				GetProperties();
				GetMethods();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект указанным типом
			/// </summary>
			/// <param name="cachedType">Тип данные которого будут сохранены</param>
			/// <param name="extractMembers">Объем извлекаемых данных для кэширования</param>
			//---------------------------------------------------------------------------------------------------------
			public CReflectedType(Type cachedType, TExtractMembers extractMembers)
			{
				_cachedType = cachedType;
				if (extractMembers.IsFlagSet(TExtractMembers.Fields))
				{
					GetFields();
				}
				if (extractMembers.IsFlagSet(TExtractMembers.Properties))
				{
					GetProperties();
				}
				if (extractMembers.IsFlagSet(TExtractMembers.Methods))
				{
					GetMethods();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ IDisposable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			/// <param name="disposing">Статус освобождения</param>
			//---------------------------------------------------------------------------------------------------------
			private void Dispose(Boolean disposing)
			{
				// Освобождаем только управляемые ресурсы
				if (disposing)
				{
					_cachedType = null;
					if (_fields != null)
					{
						_fields.Clear();
						_fields = null;
					}

					if (_properties != null)
					{
						_properties.Clear();
						_properties = null;
					}

					if (_methods != null)
					{
						_methods.Clear();
						_methods = null;
					}
				}

				// Освобождаем неуправляемые ресурсы
			}
			#endregion

			#region ======================================= РАБОТА С ПОЛЯМИ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных полей
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void GetFields()
			{
				if (_fields == null) _fields = new Dictionary<String, FieldInfo>();

				FieldInfo[] fields = _cachedType.GetFields(BINDINGFIELDS);

				foreach (FieldInfo field in fields)
				{
					_fields.Add(field.Name, field);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование поля с указанным именем
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsField(String fieldName)
			{
				if (_fields == null) GetFields();
				return _fields.ContainsKey(fieldName);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных поля по имени
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <returns>Метаданные поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public FieldInfo GetField(String fieldName)
			{
				if (_fields == null || _fields.Count == 0)
				{
					GetFields();
				}
				FieldInfo field_info;
				if(_fields.TryGetValue(fieldName, out field_info))
				{
					return field_info;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа поля по имени
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <returns>Тип поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetFieldType(String fieldName)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					return field_info.FieldType;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа поля по имени
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <returns>Имя типа поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetFieldTypeName(String fieldName)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					return field_info.FieldType.Name;
				}

				return "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных полей имеющих указанный тип
			/// </summary>
			/// <typeparam name="TType">Тип поля</typeparam>
			/// <returns>Список метаданных полей</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<FieldInfo> GetFieldsFromType<TType>()
			{
				var fields = new List<FieldInfo>();

				if (_fields == null) GetFields();
				foreach (var field_info in _fields.Values)
				{
					if (field_info.FieldType == typeof(TType))
					{
						fields.Add(field_info);
					}
				}

				return fields;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных полей имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <returns>Список метаданных полей</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<FieldInfo> GetFieldsHasAttribute<TAttribute>() where TAttribute : Attribute
			{
				var fields = new List<FieldInfo>();

				if (_fields == null) GetFields();
				foreach (var field_info in _fields.Values)
				{
					if (Attribute.IsDefined(field_info, typeof(TAttribute)))
					{
						fields.Add(field_info);
					}
				}

				return fields;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного поля
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="fieldName">Имя поля</param>
			/// <returns>Атрибут поля или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromField<TAttribute>(String fieldName) where TAttribute : System.Attribute
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					if (Attribute.IsDefined(field_info, typeof(TAttribute)))
					{
						return Attribute.GetCustomAttribute(field_info, typeof(TAttribute)) as TAttribute;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String fieldName, System.Object instance)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					if (field_info.IsStatic)
					{
						return field_info.GetValue(null);
					}
					else
					{
						return field_info.GetValue(instance);
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="fieldInfoResult">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String fieldName, System.Object instance, out FieldInfo fieldInfoResult)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					fieldInfoResult = field_info;
					if (field_info.IsStatic)
					{
						return field_info.GetValue(null);
					}
					else
					{
						return field_info.GetValue(instance);
					}
				}

				fieldInfoResult = null;
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String fieldName, System.Object instance, Int32 index)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					System.Object collection = null;
					if (field_info.IsStatic)
					{
						collection = field_info.GetValue(null);
					}
					else
					{
						collection = field_info.GetValue(instance);
					}

					if (collection != null)
					{
						if (collection is IList)
						{
							var list = collection as IList;
							return list[index];
						}
						else
						{
							if (collection is IEnumerable)
							{
								var enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (var i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return null;
									}
								}
								return enumeration.Current;
							}
						}
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <param name="fieldInfoResult">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String fieldName, System.Object instance, Int32 index, out FieldInfo fieldInfoResult)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					fieldInfoResult = field_info;
					System.Object collection = null;
					if (field_info.IsStatic)
					{
						collection = field_info.GetValue(null);
					}
					else
					{
						collection = field_info.GetValue(instance);
					}

					if (collection != null)
					{
						if (collection is IList)
						{
							var list = collection as IList;
							return list[index];
						}
						else
						{
							if (collection is IEnumerable)
							{
								var enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (var i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return null;
									}
								}
								return enumeration.Current;
							}
						}
					}
				}

				fieldInfoResult = null;
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение поля</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetFieldValue(String fieldName, System.Object instance, System.Object value)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					if (field_info.IsStatic)
					{
						field_info.SetValue(null, value);
					}
					else
					{
						field_info.SetValue(instance, value);
					}

					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="fieldName">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение поля</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetFieldValue(String fieldName, System.Object instance, System.Object value, Int32 index)
			{
				if (_fields == null) GetFields();
				FieldInfo field_info;
				if (_fields.TryGetValue(fieldName, out field_info))
				{
					IList list = null;
					if (field_info.IsStatic)
					{
						list = field_info.GetValue(null) as IList;
					}
					else
					{
						list = field_info.GetValue(instance) as IList;
					}

					if (list != null)
					{
						list[index] = value;
						return true;
					}
				}

				return false;
			}
			#endregion

			#region ======================================= РАБОТА СО СВОЙСТВАМИ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных свойств
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void GetProperties()
			{
				if (_properties == null) _properties = new Dictionary<String, PropertyInfo>();

				PropertyInfo[] properties = _cachedType.GetProperties(BINDINGPROPERTIES);

				foreach (PropertyInfo property in properties)
				{
					_properties.Add(property.Name, property);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование свойства с указанным именем
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsProperty(String propertyName)
			{
				if (_properties == null) GetProperties();
				return _properties.ContainsKey(propertyName);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных свойства по имени
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Метаданные свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public PropertyInfo GetProperty(String propertyName)
			{
				if (_properties == null) GetProperties();

				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					return property_info;
				}


				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа свойства по имени
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Тип свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetPropertyType(String propertyName)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					return property_info.PropertyType;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа свойства по имени
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Имя типа свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetPropertyTypeName(String propertyName)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					return property_info.PropertyType.Name;
				}

				return "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных свойств имеющих указанный тип
			/// </summary>
			/// <typeparam name="TType">Тип свойства</typeparam>
			/// <returns>Список метаданных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<PropertyInfo> GetPropertiesFromType<TType>()
			{
				var properties = new List<PropertyInfo>();
				if (_properties == null) GetProperties();

				foreach (var property_info in _properties.Values)
				{
					if (property_info.PropertyType == typeof(TType))
					{
						properties.Add(property_info);
					}
				}


				return properties;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных свойств имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <returns>Список метаданных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<PropertyInfo> GetPropertiesHasAttribute<TAttribute>() where TAttribute : Attribute
			{
				var properties = new List<PropertyInfo>();

				if (_properties == null) GetProperties();
				foreach (var property_info in _properties.Values)
				{
					if (Attribute.IsDefined(property_info, typeof(TAttribute)))
					{
						properties.Add(property_info);
					}
				}

				return properties;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного свойства
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Атрибут поля или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromProperty<TAttribute>(String propertyName) where TAttribute : System.Attribute
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					if (Attribute.IsDefined(property_info, typeof(TAttribute)))
					{
						return Attribute.GetCustomAttribute(property_info, typeof(TAttribute)) as TAttribute;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetPropertyValue(String propertyName, System.Object? instance)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					return property_info.GetValue(instance);
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetPropertyValue(String propertyName, System.Object instance, Int32 index)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					var collection = property_info.GetValue(instance);

					if (collection != null)
					{
						if (collection is IList)
						{
							var list = collection as IList;
							return list[index];
						}
						else
						{
							if (collection is IEnumerable)
							{
								var enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (var i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return null;
									}
								}
								return enumeration.Current;
							}
						}
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение свойства</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetPropertyValue(String propertyName, System.Object instance, System.Object value)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					property_info.SetValue(instance, value);

					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetPropertyValue(String propertyName, System.Object instance, System.Object value, Int32 index)
			{
				if (_properties == null) GetProperties();
				PropertyInfo property_info;
				if (_properties.TryGetValue(propertyName, out property_info))
				{
					var list = property_info.GetValue(instance) as IList;
					if (list != null)
					{
						list[index] = value;
						return true;
					}
				}

				return false;
			}
			#endregion

			#region ======================================= РАБОТА С МЕТОДАМИ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных метод
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void GetMethods()
			{
				if (_methods == null) _methods = new Dictionary<String, MethodInfo>();

				foreach (MethodInfo method in _cachedType.GetMethods(BINDINGMETHODS))
				{
					if (_methods.ContainsKey(method.Name) == false)
					{
						_methods.Add(method.Name, method);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование метода с указанным именем
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsMethod(String methodName)
			{
				if (_methods == null) GetMethods();
				return _methods.ContainsKey(methodName);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных метода по имени
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <returns>Метаданные метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public MethodInfo GetMethod(String methodName)
			{
				if (_methods == null) GetMethods();
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					return method_info;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <returns>Тип возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetMethodReturnType(String methodName)
			{
				if (_methods == null) GetMethods();
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					return method_info.ReturnType;
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <returns>Имя типа возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetMethodReturnTypeName(String methodName)
			{
				if (_methods == null) GetMethods();
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					return method_info.ReturnType.Name;
				}

				return "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных методов имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <returns>Список метаданных методов</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<MethodInfo> GetMethodsHasAttribute<TAttribute>() where TAttribute : Attribute
			{
				var methods = new List<MethodInfo>();

				if (_methods == null) GetMethods();
				foreach (var method_info in _methods.Values)
				{
					if (Attribute.IsDefined(method_info, typeof(TAttribute)))
					{
						methods.Add(method_info);
					}
				}

				return methods;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного метода
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="methodName">Имя метода</param>
			/// <returns>Атрибут метода или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromMethod<TAttribute>(String methodName) where TAttribute : System.Attribute
			{
				if (_methods == null) GetMethods();
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					if (Attribute.IsDefined(method_info, typeof(TAttribute)))
					{
						return Attribute.GetCustomAttribute(method_info, typeof(TAttribute)) as TAttribute;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String methodName, System.Object instance)
			{
				if (_methods == null) GetMethods();
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					if (method_info.IsStatic)
					{
						return method_info.Invoke(null, null);
					}
					else
					{
						return method_info.Invoke(instance, null);
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с одним аргументом
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg">Аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String methodName, System.Object instance, System.Object arg)
			{
				if (_methods == null) GetMethods();
				ArgList1[0] = arg;
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					if (method_info.IsStatic)
					{
						return method_info.Invoke(null, ArgList1);
					}
					else
					{
						return method_info.Invoke(instance, ArgList1);
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с двумя аргументами
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String methodName, System.Object instance, System.Object arg1,
				System.Object arg2)
			{
				if (_methods == null) GetMethods();
				ArgList2[0] = arg1;
				ArgList2[1] = arg2;
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					if (method_info.IsStatic)
					{
						return method_info.Invoke(null, ArgList2);
					}
					else
					{
						return method_info.Invoke(instance, ArgList2);
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с тремя аргументами
			/// </summary>
			/// <param name="methodName">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <param name="arg3">Третий аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String methodName, System.Object instance, System.Object arg1,
				System.Object arg2, System.Object arg3)
			{
				if (_methods == null) GetMethods();
				ArgList3[0] = arg1;
				ArgList3[1] = arg2;
				ArgList3[3] = arg3;
				MethodInfo method_info;
				if (_methods.TryGetValue(methodName, out method_info))
				{
					if (method_info.IsStatic)
					{
						return method_info.Invoke(null, ArgList3);
					}
					else
					{
						return method_info.Invoke(instance, ArgList3);
					}
				}

				return null;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================