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
// Последнее изменение от 27.03.2022
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
		//! \addtogroup CoreReflection
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс содержащий тип и его кэшированные данные рефлексии
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public sealed class CReflectedType : IDisposable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Флаги метаданных используемые при поиске полей
			/// </summary>
			public const BindingFlags BINDING_FIELDS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

			/// <summary>
			/// Флаги метаданных используемые при поиске свойств
			/// </summary>
			public const BindingFlags BINDING_PROPERTIES = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

			/// <summary>
			/// Флаги метаданных используемые при поиске методов
			/// </summary>
			public const BindingFlags BINDING_METHODS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
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
			internal Type mCachedType;
			internal Dictionary<String, FieldInfo> mFields;
			internal Dictionary<String, PropertyInfo> mProperties;
			internal Dictionary<String, MethodInfo> mMethods;
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
				get { return mCachedType; }
			}

			/// <summary>
			/// Словарь метаданных полей по имени поля
			/// </summary>
			public Dictionary<String, FieldInfo> Fields
			{
				get { return (mFields); }
			}

			/// <summary>
			/// Словарь метаданных свойств по имени свойства
			/// </summary>
			public Dictionary<String, PropertyInfo> Properties
			{
				get { return (mProperties); }
			}

			/// <summary>
			/// Словарь метаданных методов по имени метода
			/// </summary>
			public Dictionary<String, MethodInfo> Methods
			{
				get { return (mMethods); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект указанным типом
			/// </summary>
			/// <param name="cached_type">Тип данные которого будут сохранены</param>
			//---------------------------------------------------------------------------------------------------------
			public CReflectedType(Type cached_type)
			{
				mCachedType = cached_type;
				
				GetFields();
				GetProperties();
				GetMethods();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект указанным типом
			/// </summary>
			/// <param name="cached_type">Тип данные которого будут сохранены</param>
			/// <param name="extract_members">Объем извлекаемых данных для кэширования</param>
			//---------------------------------------------------------------------------------------------------------
			public CReflectedType(Type cached_type, TExtractMembers extract_members)
			{
				mCachedType = cached_type;
				if (extract_members.IsFlagSet(TExtractMembers.Fields))
				{
					GetFields();
				}
				if (extract_members.IsFlagSet(TExtractMembers.Properties))
				{
					GetProperties();
				}
				if (extract_members.IsFlagSet(TExtractMembers.Methods))
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
					mCachedType = null;
					if (mFields != null)
					{
						mFields.Clear();
						mFields = null;
					}

					if (mProperties != null)
					{
						mProperties.Clear();
						mProperties = null;
					}

					if (mMethods != null)
					{
						mMethods.Clear();
						mMethods = null;
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
				if (mFields == null) mFields = new Dictionary<String, FieldInfo>();

				FieldInfo[] fields = mCachedType.GetFields(BINDING_FIELDS);

				foreach (FieldInfo field in fields)
				{
					mFields.Add(field.Name, field);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование поля с указанным именем
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsField(String field_name)
			{
				if (mFields == null) GetFields();
				return (mFields.ContainsKey(field_name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных поля по имени
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Метаданные поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public FieldInfo GetField(String field_name)
			{
				if (mFields == null || mFields.Count == 0)
				{
					GetFields();
				}
				FieldInfo field_info;
				if(mFields.TryGetValue(field_name, out field_info))
				{
					return (field_info);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа поля по имени
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Тип поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetFieldType(String field_name)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					return (field_info.FieldType);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа поля по имени
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Имя типа поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetFieldTypeName(String field_name)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					return (field_info.FieldType.Name);
				}

				return ("");
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
				List<FieldInfo> fields = new List<FieldInfo>();

				if (mFields == null) GetFields();
				foreach (var field_info in mFields.Values)
				{
					if (field_info.FieldType == typeof(TType))
					{
						fields.Add(field_info);
					}
				}

				return (fields);
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
				List<FieldInfo> fields = new List<FieldInfo>();

				if (mFields == null) GetFields();
				foreach (var field_info in mFields.Values)
				{
					if (Attribute.IsDefined(field_info, typeof(TAttribute)))
					{
						fields.Add(field_info);
					}
				}

				return (fields);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного поля
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Атрибут поля или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromField<TAttribute>(String field_name) where TAttribute : System.Attribute
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					if (Attribute.IsDefined(field_info, typeof(TAttribute)))
					{
						return (Attribute.GetCustomAttribute(field_info, typeof(TAttribute)) as TAttribute);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String field_name, System.Object instance)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					if (field_info.IsStatic)
					{
						return (field_info.GetValue(null));
					}
					else
					{
						return (field_info.GetValue(instance));
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_info_result">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String field_name, System.Object instance, out FieldInfo field_info_result)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					field_info_result = field_info;
					if (field_info.IsStatic)
					{
						return (field_info.GetValue(null));
					}
					else
					{
						return (field_info.GetValue(instance));
					}
				}

				field_info_result = null;
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String field_name, System.Object instance, Int32 index)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
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
							IList list = collection as IList;
							return (list[index]);
						}
						else
						{
							if (collection is IEnumerable)
							{
								IEnumerable enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (Int32 i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return (null);
									}
								}
								return (enumeration.Current);
							}
						}
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <param name="field_info_result">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetFieldValue(String field_name, System.Object instance, Int32 index, out FieldInfo field_info_result)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					field_info_result = field_info;
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
							IList list = collection as IList;
							return (list[index]);
						}
						else
						{
							if (collection is IEnumerable)
							{
								IEnumerable enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (Int32 i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return (null);
									}
								}
								return (enumeration.Current);
							}
						}
					}
				}

				field_info_result = null;
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение поля</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetFieldValue(String field_name, System.Object instance, System.Object value)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
				{
					if (field_info.IsStatic)
					{
						field_info.SetValue(null, value);
					}
					else
					{
						field_info.SetValue(instance, value);
					}

					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="field_name">Имя поля</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение поля</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetFieldValue(String field_name, System.Object instance, System.Object value, Int32 index)
			{
				if (mFields == null) GetFields();
				FieldInfo field_info;
				if (mFields.TryGetValue(field_name, out field_info))
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

				return (false);
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
				if (mProperties == null) mProperties = new Dictionary<String, PropertyInfo>();

				PropertyInfo[] properties = mCachedType.GetProperties(BINDING_PROPERTIES);

				foreach (PropertyInfo property in properties)
				{
					mProperties.Add(property.Name, property);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование свойства с указанным именем
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsProperty(String property_name)
			{
				if (mProperties == null) GetProperties();
				return (mProperties.ContainsKey(property_name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных свойства по имени
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Метаданные свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public PropertyInfo GetProperty(String property_name)
			{
				if (mProperties == null) GetProperties();

				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					return (property_info);
				}


				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа свойства по имени
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Тип свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetPropertyType(String property_name)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					return (property_info.PropertyType);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа свойства по имени
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Имя типа свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetPropertyTypeName(String property_name)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					return (property_info.PropertyType.Name);
				}

				return ("");
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
				List<PropertyInfo> properties = new List<PropertyInfo>();
				if (mProperties == null) GetProperties();

				foreach (var property_info in mProperties.Values)
				{
					if (property_info.PropertyType == typeof(TType))
					{
						properties.Add(property_info);
					}
				}


				return (properties);
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
				List<PropertyInfo> properties = new List<PropertyInfo>();

				if (mProperties == null) GetProperties();
				foreach (var property_info in mProperties.Values)
				{
					if (Attribute.IsDefined(property_info, typeof(TAttribute)))
					{
						properties.Add(property_info);
					}
				}

				return (properties);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного свойства
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Атрибут поля или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromProperty<TAttribute>(String property_name) where TAttribute : System.Attribute
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					if (Attribute.IsDefined(property_info, typeof(TAttribute)))
					{
						return (Attribute.GetCustomAttribute(property_info, typeof(TAttribute)) as TAttribute);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetPropertyValue(String property_name, System.Object instance)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					return (property_info.GetValue(instance));
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object GetPropertyValue(String property_name, System.Object instance, Int32 index)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					System.Object collection = property_info.GetValue(instance);

					if (collection != null)
					{
						if (collection is IList)
						{
							IList list = collection as IList;
							return (list[index]);
						}
						else
						{
							if (collection is IEnumerable)
							{
								IEnumerable enumerable = collection as IEnumerable;

								var enumeration = enumerable.GetEnumerator();

								for (Int32 i = 0; i <= index; i++)
								{
									if (!enumeration.MoveNext())
									{
										return (null);
									}
								}
								return (enumeration.Current);
							}
						}
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение свойства</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetPropertyValue(String property_name, System.Object instance, System.Object value)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					property_info.SetValue(instance, value);

					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean SetPropertyValue(String property_name, System.Object instance, System.Object value, Int32 index)
			{
				if (mProperties == null) GetProperties();
				PropertyInfo property_info;
				if (mProperties.TryGetValue(property_name, out property_info))
				{
					IList list = property_info.GetValue(instance) as IList;
					if (list != null)
					{
						list[index] = value;
						return true;
					}
				}

				return (false);
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
				if (mMethods == null) mMethods = new Dictionary<String, MethodInfo>();

				foreach (MethodInfo method in mCachedType.GetMethods(BINDING_METHODS))
				{
					if (mMethods.ContainsKey(method.Name) == false)
					{
						mMethods.Add(method.Name, method);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование метода с указанным именем
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean ContainsMethod(String method_name)
			{
				if (mMethods == null) GetMethods();
				return (mMethods.ContainsKey(method_name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных метода по имени
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Метаданные метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public MethodInfo GetMethod(String method_name)
			{
				if (mMethods == null) GetMethods();
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					return (method_info);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Тип возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public Type GetMethodReturnType(String method_name)
			{
				if (mMethods == null) GetMethods();
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					return (method_info.ReturnType);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Имя типа возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetMethodReturnTypeName(String method_name)
			{
				if (mMethods == null) GetMethods();
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					return (method_info.ReturnType.Name);
				}

				return ("");
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
				List<MethodInfo> methods = new List<MethodInfo>();

				if (mMethods == null) GetMethods();
				foreach (var method_info in mMethods.Values)
				{
					if (Attribute.IsDefined(method_info, typeof(TAttribute)))
					{
						methods.Add(method_info);
					}
				}

				return (methods);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного метода
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Атрибут метода или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public TAttribute GetAttributeFromMethod<TAttribute>(String method_name) where TAttribute : System.Attribute
			{
				if (mMethods == null) GetMethods();
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					if (Attribute.IsDefined(method_info, typeof(TAttribute)))
					{
						return (Attribute.GetCustomAttribute(method_info, typeof(TAttribute)) as TAttribute);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String method_name, System.Object instance)
			{
				if (mMethods == null) GetMethods();
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					if (method_info.IsStatic)
					{
						return (method_info.Invoke(null, null));
					}
					else
					{
						return (method_info.Invoke(instance, null));
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с одним аргументом
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg">Аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String method_name, System.Object instance, System.Object arg)
			{
				if (mMethods == null) GetMethods();
				ArgList1[0] = arg;
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					if (method_info.IsStatic)
					{
						return (method_info.Invoke(null, ArgList1));
					}
					else
					{
						return (method_info.Invoke(instance, ArgList1));
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с двумя аргументами
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String method_name, System.Object instance, System.Object arg1,
				System.Object arg2)
			{
				if (mMethods == null) GetMethods();
				ArgList2[0] = arg1;
				ArgList2[1] = arg2;
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					if (method_info.IsStatic)
					{
						return (method_info.Invoke(null, ArgList2));
					}
					else
					{
						return (method_info.Invoke(instance, ArgList2));
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с тремя аргументами
			/// </summary>
			/// <param name="method_name">Имя метода</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <param name="arg3">Третий аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object InvokeMethod(String method_name, System.Object instance, System.Object arg1,
				System.Object arg2, System.Object arg3)
			{
				if (mMethods == null) GetMethods();
				ArgList3[0] = arg1;
				ArgList3[1] = arg2;
				ArgList3[3] = arg3;
				MethodInfo method_info;
				if (mMethods.TryGetValue(method_name, out method_info))
				{
					if (method_info.IsStatic)
					{
						return (method_info.Invoke(null, ArgList3));
					}
					else
					{
						return (method_info.Invoke(instance, ArgList3));
					}
				}

				return (null);
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================