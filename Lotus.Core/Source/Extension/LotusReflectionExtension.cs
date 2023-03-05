//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusReflectionExtension.cs
*		Методы расширения подсистемы рефлексии.
*		Реализация максимально обобщенных расширений направленных на работу с отражением типов и метаданных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreExtension
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для типа <see cref="Type"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflectionTypeExtension
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			//
			// ИМЯ ТИПОВ ШАБЛОННЫХ КОНТЙНЕРОВ
			//
			public const String COLLECTION_1 = "Collection`1";
			public const String LIST_1 = "List`1";
			public const String LIST_ARRAY_1 = "ListArray`1";
			public const String OBSERVABLE_COLLECTION_1 = "ObservableCollection`1";

			/// <summary>
			/// Префикс имени для модулей и сборок платформы Lotus
			/// </summary>
			public const String LOTUS_PREFIX = "Lotus";

#if (UNITY_2017_1_OR_NEWER)
			/// <summary>
			/// Префикс имени для модулей и сборок Unity
			/// </summary>
			public const String UNITY_PREFIX = "UnityEngine";
#endif
			#endregion

			#region ======================================= ХАРАКТЕРИСТИКИ ТИПА =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на примитивный тип
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsPrimitiveType(this Type @this)
			{
				return (@this.IsPrimitive || @this.IsEnum || @this == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на числовой тип
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsNumericType(this Type @this)
			{
				switch (Type.GetTypeCode(@this))
				{
					case TypeCode.Byte:
					case TypeCode.SByte:
					case TypeCode.UInt16:
					case TypeCode.UInt32:
					case TypeCode.UInt64:
					case TypeCode.Int16:
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Decimal:
					case TypeCode.Double:
					case TypeCode.Single:
					case TypeCode.Boolean:
						return true;
					default:
						return false;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип структуры
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsStructType(this Type @this)
			{
				return (@this.IsValueType && !IsPrimitiveType(@this) && !@this.IsGenericType);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на статический тип класса
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsStaticType(this Type @this)
			{
				return (@this.IsClass && @this.IsAbstract && @this.IsSealed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип класса
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsClassType(this Type @this)
			{
#if (UNITY_2017_1_OR_NEWER)
				return @this.IsClass && !@this.IsArray && !@this.IsGenericType &&
					   @this.Name != nameof(UnityEngine.Object) && !@this.IsSubclassOf(typeof(UnityEngine.Object));
#else
				return (@this.IsClass && !@this.IsArray && !@this.IsGenericType && @this.Name != nameof(System.Object));
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на принадлежность типа к платформе Lotus
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsLotusPlatformType(this Type @this)
			{
				return (@this.FullName.Contains(LOTUS_PREFIX));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка типа на поддержу интерфейса
			/// </summary>
			/// <typeparam name="TInterface">Тип интерфейса</typeparam>
			/// <param name="this">Тип</param>
			/// <returns>Статус поддержи интерфейса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsSupportInterface<TInterface>(this Type @this) where TInterface : class
			{
				return (typeof(TInterface).IsAssignableFrom(@this));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на классический обобщенный тип коллекции
			/// </summary>
			/// <param name="type">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Boolean IsGenericCollectionType(Type type)
			{
				return (type.IsGenericType && (type.Name == LIST_1 || type.Name == LIST_ARRAY_1 || type.Name == COLLECTION_1));
			}
			#endregion

			#region ======================================= ТИПЫ КОЛЛЕКЦИИ ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип коллекции
			/// </summary>
			/// <remarks>
			/// Под типом коллекции понимается коллекция производная от <see cref="Collection{T}"/>, не более 4 уровня
			/// </remarks>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsCollectionType(this Type @this)
			{
				Boolean status = (@this.IsGenericType && @this.Name == COLLECTION_1);
				if (status == false)
				{
					// Пробуем проверить базовый тип
					Type base_type = @this.BaseType;
					if (base_type != null)
					{
						status = (base_type.IsGenericType && base_type.Name == COLLECTION_1);
					}

					// И еще последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == COLLECTION_1);
						}
					}

					// И еще один последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == COLLECTION_1);
						}
					}

				}
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип списка
			/// </summary>
			/// <remarks>
			/// Под типом списка понимается список производный от <see cref="List{T}"/>, не более 4 уровня
			/// </remarks>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsListType(this Type @this)
			{
				Boolean status = (@this.IsGenericType && @this.Name == LIST_1);
				if (status == false)
				{
					// Пробуем проверить базовый тип
					Type base_type = @this.BaseType;
					if (base_type != null)
					{
						status = (base_type.IsGenericType && base_type.Name == LIST_1);
					}

					// И еще последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == LIST_1);
						}
					}

					// И еще один последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == LIST_1);
						}
					}

				}
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип списка
			/// </summary>
			/// <remarks>
			/// Под типом списка понимается список производный от <see cref="ListArray{TItem}"/>, не более 4 уровня
			/// </remarks>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsListLotusType(this Type @this)
			{
				Boolean status = (@this.IsGenericType && @this.Name == LIST_ARRAY_1);
				if (status == false)
				{
					// Пробуем проверить базовый тип
					Type base_type = @this.BaseType;
					if (base_type != null)
					{
						status = (base_type.IsGenericType && base_type.Name == LIST_ARRAY_1);
					}

					// И еще последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == LIST_ARRAY_1);
						}
					}

					// И еще один последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == LIST_ARRAY_1);
						}
					}

				}
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на классический тип коллекции
			/// </summary>
			/// <remarks>
			/// Под классическим типом коллекции понимается массив, коллекции производны от <see cref="Collection{T}"/>,
			/// коллекции производны от <see cref="List{T}"/>, а также производные от <see cref="ListArray{TItem}"/> 
			/// </remarks>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsClassicCollectionType(this Type @this)
			{
				Boolean status = @this.IsArray || IsGenericCollectionType(@this);
				if (status == false)
				{
					// Пробуем проверить базовый тип
					Type base_type = @this.BaseType;
					if (base_type != null)
					{
						status = IsGenericCollectionType(base_type);
					}

					// И еще последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType;
						if (base_type != null)
						{
							status = IsGenericCollectionType(base_type);
						}
					}

					// И еще один последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType.BaseType;
						if (base_type != null)
						{
							status = IsGenericCollectionType(base_type);
						}
					}

				}
				return status;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на специальный тип наблюдаемой коллекции
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsObservableCollectionType(this Type @this)
			{
				Boolean status = (@this.IsGenericType && @this.Name == OBSERVABLE_COLLECTION_1);
				if (status == false)
				{
					// Пробуем проверить базовый тип
					Type base_type = @this.BaseType;
					if (base_type != null)
					{
						status = (base_type.IsGenericType && base_type.Name == OBSERVABLE_COLLECTION_1);
					}

					// И еще последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == OBSERVABLE_COLLECTION_1);
						}
					}

					// И еще один последний уровень
					if (status == false && base_type != null)
					{
						base_type = @this.BaseType.BaseType.BaseType;
						if (base_type != null)
						{
							status = (base_type.IsGenericType && base_type.Name == OBSERVABLE_COLLECTION_1);
						}
					}

				}
				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип словаря
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsDictionaryType(this Type @this)
			{
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа элемента классической коллекции
			/// </summary>
			/// <remarks>
			/// Под классическим типом коллекции понимается массив, коллекции производны от <see cref="Collection{T}"/>,
			/// коллекции производны от <see cref="List{T}"/>, а также производные от <see cref="ListArray{TItem}"/> 
			/// </remarks>
			/// <param name="this">Тип</param>
			/// <returns>Тип элемента коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetClassicCollectionItemType(this Type @this)
			{
				if (@this.IsArray)
				{
					return @this.GetElementType();
				}
				else
				{
					if (IsGenericCollectionType(@this))
					{
						return @this.GetGenericArguments()[0];
					}
					else
					{
						// Пробуем проверить базовый тип
						Type base_type = @this.BaseType;
						if (base_type != null)
						{
							if (IsGenericCollectionType(base_type))
							{
								return base_type.GetGenericArguments()[0];
							}
							else
							{
								// И еще один уровень
								base_type = @this.BaseType.BaseType;
								if (base_type != null)
								{
									if (IsGenericCollectionType(base_type))
									{
										return base_type.GetGenericArguments()[0];
									}
									else
									{
										// И еще один последний уровень
										base_type = @this.BaseType.BaseType.BaseType;
										if (base_type != null)
										{
											if (IsGenericCollectionType(base_type))
											{
												return base_type.GetGenericArguments()[0];
											}
										}
									}
								}
							}
						}
					}
				}

				return (null);
			}
			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ РАБОТЫ С UNITY =================================
#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на принадлежность типа к модулю Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityModule(this Type @this)
			{
				return (@this.FullName.Contains(UNITY_PREFIX));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип структуры Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityStructType(this Type @this)
			{
				if (@this.FullName.Contains(UNITY_PREFIX))
				{
					return (@this.IsValueType && !@this.IsPrimitive && !@this.IsEnum && !@this.IsGenericType);
				}
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на тип класса Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityClassType(this Type @this)
			{
				if (@this.FullName.Contains(UNITY_PREFIX))
				{
					return @this.IsClass && !@this.IsArray && !@this.IsGenericType &&
						@this.Name != nameof(UnityEngine.Object) && !@this.IsSubclassOf(typeof(UnityEngine.Object));
				}
				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на базовый тип Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityObjectType(this Type @this)
			{
				return @this.IsSubclassOf(typeof(UnityEngine.Object));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на принадлежность типа к типу игрового объекта Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус принадлежности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityGameObjectType(this Type @this)
			{
				return (@this == typeof(UnityEngine.GameObject));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на принадлежность типа к типу компонента Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус принадлежности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityComponentType(this Type @this)
			{
				return (@this.IsSubclassOf(typeof(UnityEngine.Component)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на принадлежность типа к типу ресурса Unity
			/// </summary>
			/// <param name="this">Тип</param>
			/// <returns>Статус принадлежности</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsUnityResourceType(this Type @this)
			{
				if (@this.IsSubclassOf(typeof(UnityEngine.Object)) && 
					@this != typeof(UnityEngine.GameObject) && !@this.IsSubclassOf(typeof(UnityEngine.Component)))
				{
					return (true);
				}

				return (false);
			}
#endif
			#endregion

			#region ======================================= РАБОТА СО СТАТИЧЕСКИМИ ДАННЫМИ ============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных публичного статического поля по указанному имени
			/// </summary>
			/// <param name="this">Тип</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Метаданные поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static FieldInfo GetStaticField(this Type @this, String field_name)
			{
				return (@this.GetField(field_name, BindingFlags.Public | BindingFlags.Static));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения публичного статического поля по указанному имени
			/// </summary>
			/// <typeparam name="TValue">Тип значения поля</typeparam>
			/// <param name="this">Тип</param>
			/// <param name="field_name">Имя поля</param>
			/// <returns>Значение поля</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValue GetStaticFieldValue<TValue>(this Type @this, String field_name)
			{
				FieldInfo field_info = @this.GetField(field_name, BindingFlags.Public | BindingFlags.Static);
				if(field_info != null)
				{
					return((TValue)field_info.GetValue(null));
				}

				return (default(TValue));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение метаданных публичного статического свойства по указанному имени
			/// </summary>
			/// <param name="this">Тип</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Метаданные свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static PropertyInfo GetStaticProperty(this Type @this, String property_name)
			{
				return (@this.GetProperty(property_name, BindingFlags.Public | BindingFlags.Static));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения публичного статического свойства по указанному имени
			/// </summary>
			/// <typeparam name="TValue">Тип значения свойства</typeparam>
			/// <param name="this">Тип</param>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Значение свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TValue GetStaticPropertyValue<TValue>(this Type @this, String property_name)
			{
				PropertyInfo property_info = @this.GetProperty(property_name, BindingFlags.Public | BindingFlags.Static);
				if (property_info != null)
				{
					return ((TValue)property_info.GetValue(null, null));
				}

				return (default(TValue));
			}
			#endregion

			#region ======================================= РАБОТА С АТРИБУТАМИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение статуса наличия атрибута указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Тип</param>
			/// <returns>Статус наличия атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean HasAttribute<TAttribute>(this Type @this) where TAttribute : Attribute
			{
				return (Attribute.IsDefined(@this, typeof(TAttribute)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Тип</param>
			/// <returns>Экземпляр атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute GetAttribute<TAttribute>(this Type @this) where TAttribute : Attribute
			{
				if(Attribute.IsDefined(@this, typeof(TAttribute)))
				{
					return (Attribute.GetCustomAttribute(@this, typeof(TAttribute)) as TAttribute);
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение массива атрибутов указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Тип</param>
			/// <returns>Экземпляр атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute[] GetAttributes<TAttribute>(this Type @this) where TAttribute : Attribute
			{
				if (Attribute.IsDefined(@this, typeof(TAttribute), true))
				{
					return (Attribute.GetCustomAttributes(@this, typeof(TAttribute), true) as TAttribute[]);
				}
				return (null);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для перечисления <see cref="TypeCode"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflectionTypeCodeExtension
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в обычный тип
			/// </summary>
			/// <param name="this">Код типа объекта</param>
			/// <returns>Тип</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type ToType(this TypeCode @this)
			{
				switch (@this)
				{
					case TypeCode.Boolean:
						return typeof(Boolean);

					case TypeCode.Byte:
						return typeof(Byte);

					case TypeCode.Char:
						return typeof(Char);

					case TypeCode.DateTime:
						return typeof(DateTime);

					case TypeCode.DBNull:
						return typeof(DBNull);

					case TypeCode.Decimal:
						return typeof(Decimal);

					case TypeCode.Double:
						return typeof(Double);

					case TypeCode.Empty:
						return null;

					case TypeCode.Int16:
						return typeof(Int16);

					case TypeCode.Int32:
						return typeof(Int32);

					case TypeCode.Int64:
						return typeof(Int64);

					case TypeCode.Object:
						return typeof(System.Object);

					case TypeCode.SByte:
						return typeof(SByte);

					case TypeCode.Single:
						return typeof(Single);

					case TypeCode.String:
						return typeof(String);

					case TypeCode.UInt16:
						return typeof(UInt16);

					case TypeCode.UInt32:
						return typeof(UInt32);

					case TypeCode.UInt64:
						return typeof(UInt64);
				}

				return null;
			}
			#endregion
		}


		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для типа <see cref="MemberInfo"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflectionMemberExtension
		{
			#region ======================================= ХАРАКТЕРИСТИКИ ТИПА =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка метаданных члена объекта на статус устаревшего
			/// </summary>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Статус устаревшего метаданных члена объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsObsolete(this MemberInfo @this)
			{
				return (Attribute.IsDefined(@this, typeof(ObsoleteAttribute)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка метаданных члена объекта на тип делегата
			/// </summary>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Статус типа делегата</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsDelegateType(this MemberInfo @this)
			{
				Type result = null;

				switch (@this.MemberType)
				{
					case MemberTypes.All:
						break;
					case MemberTypes.Constructor:
						break;
					case MemberTypes.Custom:
						break;
					case MemberTypes.Event:
						break;
					case MemberTypes.Field:
						{
							result = ((FieldInfo)@this).FieldType;
						}
						break;
					case MemberTypes.Method:
						{
							result = ((MethodInfo)@this).ReturnType;
						}
						break;
					case MemberTypes.NestedType:
						{
							result = (Type)@this;
						}
						break;
					case MemberTypes.Property:
						{
							result = ((PropertyInfo)@this).PropertyType;
						}
						break;
					case MemberTypes.TypeInfo:
						{
							result = (Type)@this;
						}
						break;
					default:
						{

						}
						break;
				}

				if (result != null)
				{
					// Проверка на делегат
					if (result.IsSubclassOf(typeof(Delegate)) || 
						result.IsSubclassOf(typeof(MulticastDelegate)) || 
						result == typeof(Delegate))
					{
						return (true);
					}
				}

				return (false);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение пути метаданных члена объекта
			/// </summary>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Путь метаданных члена объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetMemberPath(this MemberInfo @this)
			{
				String base_path = @this.DeclaringType.Name + '.' + @this.Name;
				if (@this.DeclaringType.DeclaringType != null)
				{
					if (@this.DeclaringType.DeclaringType.DeclaringType != null)
					{
						return (@this.DeclaringType.DeclaringType.DeclaringType.Name + '.' +
							@this.DeclaringType.DeclaringType.Name + '.' + base_path);
					}
					else
					{
						return (@this.DeclaringType.DeclaringType.Name + '.' + base_path);
					}
				}
				else
				{
					return (base_path);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа метаданных члена объекта(в контексте получения данных)
			/// </summary>
			/// <remarks>
			/// Для методов возвращается тип возвращаемого значения
			/// </remarks>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Соответствующий тип метаданных члена объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetMemberType(this MemberInfo @this)
			{
				Type result = null;

				switch (@this.MemberType)
				{
					case MemberTypes.All:
						break;
					case MemberTypes.Constructor:
						break;
					case MemberTypes.Custom:
						break;
					case MemberTypes.Event:
						break;
					case MemberTypes.Field:
						{
							result = ((FieldInfo)@this).FieldType;
						}
						break;
					case MemberTypes.Method:
						{
							result = ((MethodInfo)@this).ReturnType;
						}
						break;
					case MemberTypes.NestedType:
						{
							result = (Type)@this;
						}
						break;
					case MemberTypes.Property:
						{
							result = ((PropertyInfo)@this).PropertyType;
						}
						break;
					case MemberTypes.TypeInfo:
						{
							result = (Type)@this;
						}
						break;
					default:
						{

						}
						break;
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение типа параметра метаданных члена объекта (в контексте получения данных)
			/// </summary>
			/// <remarks>
			/// Для методов возвращается тип первого аргумента
			/// </remarks>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Соответствующий тип параметра метаданных члена объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Type GetParamaterType(this MemberInfo @this)
			{
				Type result = null;

				switch (@this.MemberType)
				{
					case MemberTypes.All:
						break;
					case MemberTypes.Constructor:
						break;
					case MemberTypes.Custom:
						break;
					case MemberTypes.Event:
						break;
					case MemberTypes.Field:
						{
							result = ((FieldInfo)@this).FieldType;
						}
						break;
					case MemberTypes.Method:
						{
							var param = ((MethodInfo)@this).GetParameters().FirstOrDefault();
							if (param == null)
							{
								result = null;
							}
							else
							{
								result = param.ParameterType;
							}
						}
						break;
					case MemberTypes.NestedType:
						break;
					case MemberTypes.Property:
						{
							result = ((PropertyInfo)@this).PropertyType;
						}
						break;
					case MemberTypes.TypeInfo:
						break;
					default:
						{

						}
						break;
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значение метаданных члена объекта (в контексте получения значение от метода или свойства/поля)
			/// </summary>
			/// <param name="this">Метаданные члена объекта</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetMemberValue(this MemberInfo @this, System.Object instance)
			{
				System.Object result = null;

				switch (@this.MemberType)
				{
					case MemberTypes.All:
						break;
					case MemberTypes.Constructor:
						break;
					case MemberTypes.Custom:
						break;
					case MemberTypes.Event:
						break;
					case MemberTypes.Field:
						{
							result = ((FieldInfo)@this).GetValue(instance);
						}
						break;
					case MemberTypes.Method:
						{
							result = ((MethodInfo)@this).Invoke(instance, null);
						}
						break;
					case MemberTypes.NestedType:
						break;
					case MemberTypes.Property:
						{
							result = ((PropertyInfo)@this).GetValue(instance, null);
						}
						break;
					case MemberTypes.TypeInfo:
						break;
					default:
						{

						}
						break;
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значение метаданных члена объекта (в контексте получения значение от метода или свойства/поля)
			/// </summary>
			/// <typeparam name="TType">Тип метаданных</typeparam>
			/// <param name="this">Метаданные члена объекта</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Значение указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TType GetMemberValue<TType>(this MemberInfo @this, System.Object instance)
			{
				return (TType)GetMemberValue(@this, instance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка значения метаданных члена объекта (в контексте установки значения)
			/// </summary>
			/// <param name="this">Метаданные члена объекта</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="value">Значение</param>
			/// <param name="index">Индекс при применении индексированных свойств</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetMemberValue(this MemberInfo @this, System.Object instance, System.Object value,
				Int32 index = -1)
			{
				if (instance == null)
				{
					String error = String.Format("Instance = NULL, Member name: [{0}] = Value: [{1}]", @this.Name, value.ToString());
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogError(error);
#else
					XLogger.LogError(error);
#endif
					return;
				}

				switch (@this.MemberType)
				{
					case MemberTypes.All:
						break;
					case MemberTypes.Constructor:
						break;
					case MemberTypes.Custom:
						break;
					case MemberTypes.Event:
						break;
					case MemberTypes.Field:
						{
							((FieldInfo)@this).SetValue(instance, value);
						}
						break;
					case MemberTypes.Method:
						{
							var method = (MethodInfo)@this;

							if (method.GetParameters().Any())
							{
								CReflectedType.ArgList1[0] = value;
								method.Invoke(instance, CReflectedType.ArgList1);
							}
							else
							{
								method.Invoke(instance, null);
							}
						}
						break;
					case MemberTypes.NestedType:
						break;
					case MemberTypes.Property:
						{
							if (index == -1)
							{
								((PropertyInfo)@this).SetValue(instance, value, null);
							}
							else
							{
								CReflectedType.ArgList1[0] = value;
								((PropertyInfo)@this).SetValue(instance, value, CReflectedType.ArgList1);
							}
						}
						break;
					case MemberTypes.TypeInfo:
						break;
					default:
						{

						}
						break;
				}
			}
			#endregion

			#region ======================================= РАБОТА С АТРИБУТАМИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение статуса наличия атрибута указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Статус наличия атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean HasAttribute<TAttribute>(this MemberInfo @this) where TAttribute : Attribute
			{
				return (Attribute.IsDefined(@this, typeof(TAttribute)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение атрибута указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Экземпляр атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute GetAttribute<TAttribute>(this MemberInfo @this) where TAttribute : Attribute
			{
				if (Attribute.IsDefined(@this, typeof(TAttribute)))
				{
					return (Attribute.GetCustomAttribute(@this, typeof(TAttribute)) as TAttribute);
				}
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение массива атрибутов указанного типа
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="this">Метаданные члена объекта</param>
			/// <returns>Экземпляр атрибута указанного типа</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TAttribute[] GetAttributes<TAttribute>(this MemberInfo @this) where TAttribute : Attribute
			{
				if (Attribute.IsDefined(@this, typeof(TAttribute), true))
				{
					return (Attribute.GetCustomAttributes(@this, typeof(TAttribute), true) as TAttribute[]);
				}
				return (null);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для типа <see cref="PropertyInfo"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflectionPropertyExtension
		{
			#region ======================================= ХАРАКТЕРИСТИКИ ТИПА =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка метаданных свойства объекта на статику
			/// </summary>
			/// <param name="this">Метаданные свойства объекта</param>
			/// <returns>Статус статического свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsStatic(this PropertyInfo @this)
			{
				return (@this.GetGetMethod().IsStatic || @this.GetSetMethod().IsStatic);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка метаданных свойства объекта на индексатор
			/// </summary>
			/// <param name="this">Метаданные свойства объекта</param>
			/// <returns>Статус индексатора</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsIndexer(this PropertyInfo @this)
			{
				ParameterInfo[] pia = @this.GetIndexParameters();
				if (pia != null && pia.Length > 0)
				{
					return (true);
				}

				return (false);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений для типа <see cref="MethodInfo"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XReflectionMethodExtension
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание функции(делегата) из метаданных метода
			/// </summary>
			/// <remarks>
			/// Параметр this не передается в метод
			/// </remarks>
			/// <typeparam name="TFunction">Тип делегата</typeparam>
			/// <param name="this">Метаданные метода</param>
			/// <returns>Делегат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TFunction MakeFunction<TFunction>(this MethodInfo @this) where TFunction : class
			{
				return (Delegate.CreateDelegate(typeof(TFunction), @this) as TFunction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание функции(делегата) из метаданных метода
			/// </summary>
			/// <remarks>
			/// Параметр this не передается в метод
			/// </remarks>
			/// <typeparam name="TFunction">Тип делегата</typeparam>
			/// <param name="this">Метаданные метода</param>
			/// <returns>Делегат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TFunction MakeStaticFunction<TFunction>(this MethodInfo @this) where TFunction : class
			{
				return (Delegate.CreateDelegate(typeof(TFunction), null, @this) as TFunction);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание функции(делегата) из метаданных метода
			/// </summary>
			/// <typeparam name="TFunction">Тип делегата</typeparam>
			/// <param name="this">Метаданные метода</param>
			/// <returns>Делегат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TFunction MakeFunctionGenericThis<TFunction>(this MethodInfo @this) where TFunction : class
			{
				var obj = Expression.Parameter(typeof(System.Object), "obj");
				var item = Expression.Convert(obj, @this.DeclaringType);
				var call = Expression.Call(item, @this);
				var lambda = Expression.Lambda<TFunction>(call, obj);
				return lambda.Compile();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание функции(делегата) из метаданных метода
			/// </summary>
			/// <typeparam name="TFunction">Тип делегата</typeparam>
			/// <param name="this">Метаданные метода</param>
			/// <returns>Делегат</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TFunction MakeFunctionGenericInput<TFunction>(this MethodInfo @this) where TFunction : class
			{
				var obj = Expression.Parameter(typeof(System.Object), "obj");
				var item = Expression.Convert(obj, @this.DeclaringType);
				var obj2 = Expression.Parameter(typeof(System.Object), "input");
				var item2 = Expression.Convert(obj2, @this.GetParameters()[0].ParameterType);
				var call = Expression.Call(item, @this, item2);
				var lambda = Expression.Lambda<TFunction>(call, obj, obj2);
				return lambda.Compile();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================