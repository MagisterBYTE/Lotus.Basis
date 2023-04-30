//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема рефлексии
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusReflectionDispatcher.cs
*		Центральный диспетчер для работы с рефлексией данных, проверки и анализа типов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		/// Центральный диспетчер реализующий работу с рефлексией данных, проверку и анализ типов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflection
		{
			#region ======================================= ДАННЫЕ ====================================================
			public static Dictionary<String, CReflectedType> mCached;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Словарь кэшированных данных рефлексии по полному имени типа
			/// </summary>
			public static Dictionary<String, CReflectedType> Cached
			{
				get
				{
					if(mCached == null)
					{
						mCached = new Dictionary<String, CReflectedType>(400);
					}
					return (mCached);
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление типа в кэш
			/// </summary>
			/// <param name="cached_type">Тип данные которого будут сохранены</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean AddCachedType(Type cached_type)
			{
				if (Cached.ContainsKey(cached_type.FullName))
				{
					return (false);
				}
				else
				{
					var reflected_type = new CReflectedType(cached_type);
					Cached.Add(cached_type.FullName, reflected_type);
					return (true);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление типа в кэш
			/// </summary>
			/// <param name="cached_type">Тип данные которого будут сохранены</param>
			/// <param name="extract_members">Объем извлекаемых данных для кэширования</param>
			/// <returns>Статус успешности добавления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean AddCachedType(Type cached_type, TExtractMembers extract_members)
			{
				if (Cached.ContainsKey(cached_type.FullName))
				{
					return (false);
				}
				else
				{
					var reflected_type = new CReflectedType(cached_type, extract_members);
					Cached.Add(cached_type.FullName, reflected_type);
					return (true);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения статических данных от поля или свойства по строки данных 
			/// </summary>
			/// <remarks>
			/// Строка данных представляет собой строку в формате: полное имя типа.статический член данных
			/// </remarks>
			/// <param name="full_type_name_member_name">Строка данных</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetStaticDataFromType(String full_type_name_member_name)
			{
				var last_dot = full_type_name_member_name.LastIndexOf(XChar.Dot);
				if(last_dot > -1)
				{
					var full_type_name = full_type_name_member_name.Substring(0, last_dot);
					var member_name = full_type_name_member_name.Substring(last_dot + 1);

					// Проверяем наличие типа
					if (Cached.ContainsKey(full_type_name))
					{
						// Проверяем наличие статического поля
						if (Cached[full_type_name].ContainsField(member_name))
						{
							return (Cached[full_type_name].GetFieldValue(member_name, null));
						}
						else
						{
							// Проверяем наличие статического свойства
							if (Cached[full_type_name].ContainsProperty(member_name))
							{
								return (Cached[full_type_name].GetPropertyValue(member_name, null));
							}
							else
							{
								return (null);
							}
						}
					}
					else
					{
						return (null);
					}
				}
				else
				{
					return (null);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Фильтрация сборок для анализа
			/// </summary>
			/// <remarks>
			/// Для многих методов требуется получить все загруженные в домен сборки и их проанализировать, 
			/// для того чтобы избавиться от точно ненужных сборок их нужно отфильтровать
			/// </remarks>
			/// <param name="assembly">Сборка</param>
			/// <returns>Статус фильтрации</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Boolean FilterAssembly(Assembly assembly)
			{
				if (assembly.FullName.Contains("Mono")) return (false);
				if (assembly.FullName.Contains("mscorlib")) return (false);
				if (assembly.FullName.Contains("System")) return (false);
				if (assembly.FullName.Contains("Editor")) return (false);
				if (assembly.FullName.Contains("Test")) return (false);
				return (true);
			}
			#endregion

			#region ======================================= МЕТОДЫ СОЗДАНИЯ ОБЪЕКТА ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создания экземпляра объекта указанного типа
			/// </summary>
			/// <typeparam name="TType">Тип объекта</typeparam>
			/// <returns>Созданный объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TType CreateInstance<TType>()
			{
				return (TType)CreateInstance(typeof(TType));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создания экземпляра объекта указанного типа
			/// </summary>
			/// <param name="type">Тип объекта</param>
			/// <returns>Созданный объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object CreateInstance(Type type)
			{
				return CreateInstance(type, null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создания экземпляра объекта указанного типа
			/// </summary>
			/// <typeparam name="TType">Тип объекта</typeparam>
			/// <param name="args">Аргументы для конструктора</param>
			/// <returns>Созданный объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TType CreateInstance<TType>(params System.Object[] args)
			{
				return (TType)CreateInstance(typeof(TType), args);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создания экземпляра объекта указанного типа
			/// </summary>
			/// <remarks>
			/// Этот метод является оболочкой для вызова методов типа <see cref="System.Activator"/> только с дополнительными
			/// проверками и частными случаями
			/// </remarks>
			/// <param name="type">Тип объекта</param>
			/// <param name="args">Аргументы для конструктора</param>
			/// <returns>Созданный объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object CreateInstance(Type type, params System.Object[] args)
			{
				// Проверяем тип на принадлежность к системе Unity
#if (UNITY_2017_1_OR_NEWER)
				if(type == typeof(UnityEngine.GameObject))
				{
					// Создаем игровой объект
					UnityEngine.GameObject game_object = new UnityEngine.GameObject("create_from_instance");
					return game_object;
				}
				else
				{
					// Создать экземпляр компонента мы не можем
					if (type.IsSubclassOf(typeof(UnityEngine.Component)))
					{
						UnityEngine.Debug.LogErrorFormat("You cannot create components: <{0}>", type.Name);
						return null;
					}
					else
					{
						// Получается это у нас ресурс, как правило ресурсы не создаются в игре, они должны только загружаться
						// Сделаем исключение только для материала и меша
						if(type == typeof(UnityEngine.Material))
						{
							// Возвращаем новый стандартный материал
							return new UnityEngine.Material(UnityEngine.Shader.Find("Standard"));
						}
						else
						{
							if (type == typeof(UnityEngine.Mesh))
							{
								// Возвращаем новый пустой меш
								// Используется при процедурной генерации
								return new UnityEngine.Mesh();
							}
						}
					}
				}
#endif
				// Системные типы у которых нет конструкторов по умолчанию, но они будут использоваться и поэтому их надо создавать
				if (type == typeof(String))
				{
					return "";
				}
				if (type == typeof(Uri))
				{
					return "http://www.contoso.com/";
				}
				else
				{
					// Мы не рассматриваем случай когда у пользовательских типов нет конструктора по умолчанию
					if (args == null)
					{
						return Activator.CreateInstance(type);
					}
					else
					{
						return Activator.CreateInstance(type, args);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание объектов типов производных от указанного типа
			/// </summary>
			/// <remarks>
			/// Метод проходит по всем сборка находит производные типы и создает объекты конструктором по умолчанию
			/// </remarks>
			/// <param name="base_type">Базовый тип</param>
			/// <returns>Список объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<Object> CreateObjectsFromBaseType(Type base_type)
			{
				var list = new List<Object>();

				// Получаем все загруженные сборки в домене
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();

				// Проходим по всем сборкам
				for (var ia = 0; ia < assemblies.Length; ia++)
				{
					// Сборка
					var assemble = assemblies[ia];

					if (FilterAssembly(assemble))
					{
						// Получаем все типы в сборке
						var types = assemble.GetTypes();

						// Проходим по всем типам
						for (var it = 0; it < types.Length; it++)
						{
							// Получаем тип
							var type = types[it];

							// Если он производный и не абстрактный
							if (type.IsSubclassOf(base_type) && !type.IsAbstract)
							{
								try
								{
									var instance = Activator.CreateInstance(type, true);
									if (instance != null)
									{
										list.Add(instance);
										continue;
									}
								}
								catch (Exception)
								{
								}
							}
						}
					}
				}

				return (list);
			}
			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ РАБОТЫ С UNITY =================================
#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение типа объекта в Unity
			/// </summary>
			/// <param name="obj">Проверяемый объект</param>
			/// <returns>Тип объекта в системе Unity</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TUnityObjectType GetUnityObjectType(System.Object obj)
			{
				// Получаем тип
				Type type = obj.GetType();
				return GetUnityObjectType(type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение типа объекта в Unity на основе информации типа
			/// </summary>
			/// <param name="type">Информация о типе</param>
			/// <returns>Тип объекта в системе Unity</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TUnityObjectType GetUnityObjectType(Type type)
			{
				// Игровой объект
				if (type == typeof(UnityEngine.GameObject))
				{
					return TUnityObjectType.GameObject;
				}

				// Компоненты
				if (type.IsSubclassOf(typeof(UnityEngine.Component)))
				{
					if(type.IsUnityModule())
					{
						return TUnityObjectType.Component;
					}
					else
					{
						return TUnityObjectType.UserComponent;
					}
				}

				// Пользовательские ресурсы
				if (type.IsSubclassOf(typeof(UnityEngine.ScriptableObject)))
				{
					if (type.IsUnityModule())
					{
						return TUnityObjectType.Resource;
					}
					else
					{
						return TUnityObjectType.UserResource;
					}
				}

				// Остаются или ресурсы Unity (простые структурные типы Unity не рассматриваются)
				if (type.IsSubclassOf(typeof(UnityEngine.Object)))
				{
					return TUnityObjectType.Resource;
				}

				return TUnityObjectType.Resource;
			}
#endif
			#endregion

			#region ======================================= РАБОТА С ПОЛЯМИ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование поля с указанным именем
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ContainsField(System.Object instance, String field_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].ContainsField(field_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.ContainsField(field_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных поля по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Метаданные поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static FieldInfo GetField(System.Object instance, String field_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetField(field_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetField(field_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа поля по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Тип поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetFieldType(System.Object instance, String field_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldType(field_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldType(field_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа поля по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Имя типа поля или null если поля с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetFieldTypeName(System.Object instance, String field_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldTypeName(field_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldTypeName(field_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных полей имеющих указанный тип
			/// </summary>
			/// <typeparam name="TType">Тип поля</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Список метаданных полей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<FieldInfo> GetFieldsFromType<TType>(System.Object instance)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldsFromType<TType>());
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldsFromType<TType>());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных полей имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Список метаданных полей</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<FieldInfo> GetFieldsHasAttribute<TAttribute>(System.Object instance) where TAttribute : Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldsHasAttribute<TAttribute>());
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldsHasAttribute<TAttribute>());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного поля
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Атрибут поля или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute GetAttributeFromField<TAttribute>(System.Object instance, String field_name) where TAttribute : System.Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetAttributeFromField<TAttribute>(field_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetAttributeFromField<TAttribute>(field_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetFieldValue(System.Object instance, String field_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldValue(field_name, instance));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldValue(field_name, instance));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="field_info_result">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetFieldValue(System.Object instance, String field_name, out FieldInfo field_info_result)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldValue(field_name, instance, out field_info_result));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldValue(field_name, instance, out field_info_result));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetFieldValue(System.Object instance, String field_name, Int32 index)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldValue(field_name, instance, index));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldValue(field_name, instance, index));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="index">Индекс элемента</param>
			/// <param name="field_info_result">Метаданные поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetFieldValue(System.Object instance, String field_name, Int32 index, out FieldInfo field_info_result)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetFieldValue(field_name, instance, index, out field_info_result));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetFieldValue(field_name, instance, index, out field_info_result));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="value">Значение поля</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetFieldValue(System.Object instance, String field_name, System.Object value)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].SetFieldValue(field_name, instance, value));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.SetFieldValue(field_name, instance, value));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного поля которое представляет собой коллекцию
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="field_name">Имя поля</param>
			/// <param name="value">Значение поля</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetFieldValue(System.Object instance, String field_name, System.Object value, Int32 index)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].SetFieldValue(field_name, instance, value, index));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Fields);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.SetFieldValue(field_name, instance, value, index));
				}
			}
			#endregion

			#region ======================================= РАБОТА СО СВОЙСТВАМИ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование свойства с указанным именем
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ContainsProperty(System.Object instance, String property_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].ContainsProperty(property_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.ContainsProperty(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных свойства по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Метаданные свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static PropertyInfo GetProperty(System.Object instance, String property_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetProperty(property_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetProperty(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа свойства по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Тип свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetPropertyType(System.Object instance, String property_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertyType(property_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertyType(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа свойства по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Имя типа свойства или null если свойства с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetPropertyTypeName(System.Object instance, String property_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertyTypeName(property_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertyTypeName(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных свойств имеющих указанный тип
			/// </summary>
			/// <typeparam name="TType">Тип свойства</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Список метаданных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<PropertyInfo> GetPropertiesFromType<TType>(System.Object instance)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertiesFromType<TType>());
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertiesFromType<TType>());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных свойств имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Список метаданных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<PropertyInfo> GetPropertiesHasAttribute<TAttribute>(System.Object instance) where TAttribute : Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertiesHasAttribute<TAttribute>());
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertiesHasAttribute<TAttribute>());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного свойства
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Атрибут свойства или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute GetAttributeFromProperty<TAttribute>(System.Object instance, String property_name) where TAttribute : System.Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetAttributeFromProperty<TAttribute>(property_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetAttributeFromProperty<TAttribute>(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetPropertyValue(System.Object instance, String property_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertyValue(property_name, instance));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertyValue(property_name, instance));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetPropertyValue(System.Object instance, String property_name, Int32 index)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetPropertyValue(property_name, instance, index));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetPropertyValue(property_name, instance, index));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetPropertyValue(System.Object instance, String property_name, System.Object value)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].SetPropertyValue(property_name, instance, value));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.SetPropertyValue(property_name, instance, value));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения указанного свойства которое представляет собой коллекцию
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="property_name">Имя свойства</param>
			/// <param name="value">Значение свойства</param>
			/// <param name="index">Индекс элемента</param>
			/// <returns> Статус успешности установки свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean SetPropertyValue(System.Object instance, String property_name, System.Object value, Int32 index)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].SetPropertyValue(property_name, instance, value, index));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Properties);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.SetPropertyValue(property_name, instance, value, index));
				}
			}
			#endregion

			#region ======================================= РАБОТА С МЕТОДАМИ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование метода с указанным именем
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ContainsMethod(System.Object instance, String method_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].ContainsMethod(method_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.ContainsMethod(method_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных метода по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Метаданные метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static MethodInfo GetMethod(System.Object instance, String method_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetMethod(method_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetMethod(method_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Тип возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetMethodReturnType(System.Object instance, String method_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetMethodReturnType(method_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetMethodReturnType(method_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени типа возвращаемого значения метода по имени
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Имя типа возвращаемого значения метода или null если метода с таким именем не оказалось</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetMethodReturnTypeName(System.Object instance, String method_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetMethodReturnTypeName(method_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetMethodReturnTypeName(method_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка метаданных методов имеющих указанный атрибут
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Список метаданных методов</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<MethodInfo> GetMethodsHasAttribute<TAttribute>(System.Object instance) where TAttribute : Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetMethodsHasAttribute<TAttribute>());
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetMethodsHasAttribute<TAttribute>());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного метода
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Атрибут метода или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute GetAttributeFromMethod<TAttribute>(System.Object instance, String method_name) where TAttribute : System.Attribute
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].GetAttributeFromMethod<TAttribute>(method_name));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.GetAttributeFromMethod<TAttribute>(method_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object InvokeMethod(System.Object instance, String method_name)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].InvokeMethod(method_name, instance));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.InvokeMethod(method_name, instance));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с одним аргументом
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <param name="arg">Аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object InvokeMethod(System.Object instance, String method_name, System.Object arg)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].InvokeMethod(method_name, instance, arg));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.InvokeMethod(method_name, instance, arg));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с двумя аргументами
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object InvokeMethod(System.Object instance, String method_name, System.Object arg1,
				System.Object arg2)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].InvokeMethod(method_name, instance, arg1, arg2));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.InvokeMethod(method_name, instance, arg1, arg2));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вызов указанного метода с тремя аргументами
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="method_name">Имя метода</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <param name="arg3">Третий аргумент метода</param>
			/// <returns>Значение метода</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object InvokeMethod(System.Object instance, String method_name, System.Object arg1,
				System.Object arg2, System.Object arg3)
			{
				Type type = instance.GetType();
				if (Cached.ContainsKey(type.FullName))
				{
					return (Cached[type.FullName].InvokeMethod(method_name, instance, arg1, arg2, arg3));
				}
				else
				{
					var reflected_type = new CReflectedType(type, TExtractMembers.Methods);
					Cached.Add(type.FullName, reflected_type);
					return (reflected_type.InvokeMethod(method_name, instance, arg1, arg2, arg3));
				}
			}
			#endregion

			#region ======================================= РАБОТА СО СТАТИЧЕСКИМИ ПОЛЯМИ =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование статического поля с указанным именем
			/// </summary>
			/// <param name="full_type_name">Полное имя типа</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ContainsStaticField(String full_type_name, String field_name)
			{
				if (Cached.ContainsKey(full_type_name))
				{
					return (Cached[full_type_name].ContainsField(field_name));
				}
				else
				{
					return (false);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного статического  поля
			/// </summary>
			/// <param name="full_type_name">Полное имя типа</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetStaticFieldValue(String full_type_name, String field_name)
			{
				if (Cached.ContainsKey(full_type_name))
				{
					return (Cached[full_type_name].GetFieldValue(field_name, null));
				}
				else
				{
					return (null);
				}
			}
			#endregion

			#region ======================================= РАБОТА СО СТАТИЧЕСКИМИ СВОЙСТВАМИ =========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существование статического свойства с указанным именем
			/// </summary>
			/// <param name="full_type_name">Полное имя типа</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ContainsStaticProperty(String full_type_name, String property_name)
			{
				if (Cached.ContainsKey(full_type_name))
				{
					return (Cached[full_type_name].ContainsProperty(property_name));
				}
				else
				{
					return (false);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения указанного свойства
			/// </summary>
			/// <param name="full_type_name">Полное имя типа</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetStaticPropertyValue(String full_type_name, String property_name)
			{ 
				if (Cached.ContainsKey(full_type_name))
				{
					return (Cached[full_type_name].GetPropertyValue(property_name, null));
				}
				else
				{
					return (null);
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