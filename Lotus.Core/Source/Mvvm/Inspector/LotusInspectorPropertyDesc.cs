//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема поддержки инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorPropertyDesc.cs
*		Определение класса для дополнительного описание свойства объекта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreInspector
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Описание свойства объекта для его актуального описания, отображения и редактирования пользователем
		/// </summary>
		/// <remarks>
		/// <para>
		/// Рассмотрим подробнее для чего нужна отдельная структура описания свойства/поля объекта
		/// </para>
		/// <para>
		/// При определении полей и свойств типов для них определяются соответствующие атрибуты описания, 
		/// если этот объект предусматривает редактирование со стороны пользователя.
		/// </para>
		/// <para>
		/// При последующем наследовании от этих типов изменяется соответственно и функциональное(предметное) назначение
		/// производного типа и очень часто требуется уточнить описание соответствующего поля/свойства, либо скрыть его,
		/// однако повторно переопределить атрибуты уже невозможно.
		/// </para>
		/// <para>
		/// Исходя из этого данная структура позволяет задать описании поля/свойства для каждого конкретного типа
		/// в иерархии и даже частично менять механизм их редактирования.
		/// </para>
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CPropertyDesc : IComparable<CPropertyDesc>
		{
			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение отображаемого имя свойства и его описания
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="displayName">Отображаемое имя свойства</param>
			/// <param name="description">Описание свойства</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideDisplayNameAndDescription<TType>(String propertyName, String displayName, String description = null)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.DisplayName = displayName;
				property_desc.Description = description;

				return property_desc;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение категории свойства
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="category">Категория</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideCategory<TType>(String propertyName, String category)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.Category = category;

				return property_desc;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение скрытия свойства
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideHide<TType>(String propertyName)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.IsHideInspector = true;

				return property_desc;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение порядка отображения свойства
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="order">Порядковый номер отображения свойства в группе</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideOrder<TType>(String propertyName, Int32 order)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.PropertyOrder = order;

				return property_desc;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение значения по умолчанию свойства 
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="defaultValue">Значение свойства по умолчанию</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideDefaultValue<TType>(String propertyName, System.Object defaultValue)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.DefaultValue = defaultValue;

				return property_desc;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание/переопределение значения списка допустимых значений свойства 
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <param name="propertyName">Имя свойства</param>
			/// <param name="listValues">Список допустимых значений свойства</param>
			/// <returns>Описатель свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CPropertyDesc OverrideListValues<TType>(String propertyName, System.Object listValues)
			{
				var property_desc = new CPropertyDesc();

				// Проверяем
				if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
				{
					XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
				}

				property_desc.PropertyName = propertyName;
				property_desc.ListValues = listValues;

				return property_desc;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЯ ВЕЛИЧИНЫ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения из декларированного значения в различных формах
			/// </summary>
			/// <remarks>
			/// <para>
			/// Различные значения которые используются при дополнительном управлении свойствами и полями объекта в инспекторе 
			/// свойств можно задать различными способами
			/// </para>
			/// <para>
			/// В атрибутах можно задать только константные выражения, в описание свойства можно задать статические выражения, 
			/// поэтому декларированное значении должно интерпретироваться различным способом в зависимости от формы его задания
			/// </para>
			/// <para>
			/// Данный метод анализирует все доступные формы задания декларированное значении и получает конкретное значение
			/// </para>
			/// </remarks>
			/// <param name="declareValue">Декларированное значение</param>
			/// <param name="memberName">Имя члена объекта/типа</param>
			/// <param name="memberType">Тип члена объект/типа</param>
			/// <param name="instance"></param>
			/// <returns>Экземпляр объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetValue(System.Object declareValue, String memberName, TInspectorMemberType memberType, 
				System.Object instance = null)
			{
				System.Object result = null;

				// Проверяем непосредственного значение
				if (declareValue != null)
				{
					// 1) Задан как статические данные
					if(declareValue is Type && memberName.IsExists())
					{
						// Получаем тип
						var type = declareValue as Type;
						switch (memberType)
						{
							case TInspectorMemberType.Field:
								{
									result = type.GetStaticFieldValue<System.Object>(memberName);
									return result;
								}
							case TInspectorMemberType.Property:
								{
									result = type.GetStaticPropertyValue<System.Object>(memberName);
									return result;
								}
							case TInspectorMemberType.Method:
								break;
							default:
								break;
						}
					}
					else
					{
						// 2) Это может быть метаданные поля 
						if (declareValue is FieldInfo)
						{
							var field_info = declareValue as FieldInfo;
							if(field_info.IsStatic)
							{
								result = field_info.GetValue(null);
							}
							else
							{
								result = field_info.GetValue(instance);
							}

							return result;
						}

						// 3) Это может быть метаданные свойства
						if (declareValue is PropertyInfo)
						{
							var property_info = declareValue as PropertyInfo;
							if (property_info.IsStatic())
							{
								result = property_info.GetValue(null, null);
							}
							else
							{
								result = property_info.GetValue(instance, null);
							}

							return result;
						}

						// 4) Декларированное значение и есть значение
						result = declareValue;
						return result;
					}
				}
				else
				{
					// У нас есть только строковые данные
					// Если задана в строки имя типа и его член данных
					if (memberName.IndexOf(XChar.Dot) > -1)
					{
						result = XReflection.GetStaticDataFromType(memberName);
						return result;
					}
					else
					{
						// Задано только имя члена данных, используем экземпляр объекта
						switch (memberType)
						{
							case TInspectorMemberType.Field:
								{
									result = XReflection.GetFieldValue(instance, memberName);
								}
								break;
							case TInspectorMemberType.Property:
								{
									result = XReflection.GetPropertyValue(instance, memberName);
								}
								break;
							case TInspectorMemberType.Method:
								{
									result = XReflection.InvokeMethod(instance, memberName);
								}
								break;
							default:
								break;
						}
					}

				}
				return result;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mPropertyName;

			// Параметры описания
			protected internal String _displayName;
			protected internal String mDescription;
			protected internal Int32 mPropertyOrder = -1;
			protected internal String _category;
			protected internal Int32 _categoryOrder = -1;

			// Параметры управления
			protected internal Boolean _isHideInspector;
			protected internal Boolean _isReadOnly;
			protected internal Object mDefaultValue;
			protected internal Object _listValues;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ПАРАМЕТРЫ ОПИСАНИЯ
			//
			/// <summary>
			/// Имя свойства с которым связано данное описание
			/// </summary>
			public String PropertyName
			{
				get
				{
					return mPropertyName;
				}
				set
				{
					mPropertyName = value;
				}
			}

			//
			// ПАРАМЕТРЫ ОПИСАНИЯ
			//
			/// <summary>
			/// Отображаемое имя свойства
			/// </summary>
			public String DisplayName
			{
				get
				{
					return _displayName;
				}
				set
				{
					_displayName = value;
				}
			}

			/// <summary>
			/// Описание свойства
			/// </summary>
			public String Description
			{
				get { return mDescription; }
				set
				{
					mDescription = value;
				}
			}

			/// <summary>
			/// Порядковый номер отображения свойства внутри категории
			/// </summary>
			public Int32 PropertyOrder
			{
				get { return mPropertyOrder; }
				set
				{
					mPropertyOrder = value;
				}
			}

			/// <summary>
			/// Категория свойства
			/// </summary>
			public String Category
			{
				get { return _category; }
				set
				{
					_category = value;
				}
			}

			/// <summary>
			/// Порядковый номер отображения категории
			/// </summary>
			public Int32 CategoryOrder
			{
				get { return _categoryOrder; }
				set
				{
					_categoryOrder = value;
				}
			}

			//
			// ПАРАМЕТРЫ УПРАВЛЕНИЯ
			//
			/// <summary>
			/// Свойство скрыто для отображения в инспекторе свойств
			/// </summary>
			public Boolean IsReadOnly
			{
				get { return _isReadOnly; }
				set
				{
					_isReadOnly = value;
				}
			}

			/// <summary>
			/// Свойство только для чтения
			/// </summary>
			public Boolean IsHideInspector
			{
				get { return _isHideInspector; }
				set
				{
					_isHideInspector = value;
				}
			}

			/// <summary>
			/// Значение свойства по умолчанию
			/// </summary>
			public Object DefaultValue
			{
				get { return mDefaultValue; }
				set
				{
					mDefaultValue = value;
				}
			}

			/// <summary>
			/// Список допустимых значений свойств
			/// </summary>
			public Object ListValues
			{
				get { return _listValues; }
				set
				{
					_listValues = value;
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPropertyDesc()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CPropertyDesc other)
			{
				return DisplayName.CompareTo(other.DisplayName);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Отображаемое имя свойства</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				if (String.IsNullOrEmpty(_category))
				{
					return DisplayName;
				}
				else
				{
					return _category + "=" + DisplayName;
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