//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема фильтрации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryFilterProperty.cs
*		Определение основных типов для фильтрации по свойству/полю объекта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using Lotus.Core;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryFilter
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый интерфейс для фильтрации по свойству/полю объекта
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusFilterProperty
		{
			/// <summary>
			/// Свойство/поле по которому идет фильтрация 
			/// </summary>
			String PropertyName { get; set; }

			/// <summary>
			/// Функция для фильтрации
			/// </summary>
			TFilterFunction Function { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для фильтрации по свойству/полю объекта поддерживающего интерфейс <see cref="IComparable"/> 
		/// </summary>
		/// <typeparam name="TPropertyType">Тип свойства</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class Filter<TPropertyType> : ILotusFilterProperty where TPropertyType : IComparable<TPropertyType> 
		{
			/// <summary>
			/// Свойство/поле по которому идет фильтрация 
			/// </summary>
			public String PropertyName { get; set; } = default!;

			/// <summary>
			/// Функция для фильтрации
			/// </summary>
			public TFilterFunction Function { get; set; }

			/// <summary>
			/// Значение для фильтрации
			/// </summary>
			public TPropertyType Value { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для фильтрации по свойству/полю объекта строкового типа
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CFilterString : Filter<String>
		{
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Универсальный класс для фильтрации по свойству/полю объекта 
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CFilterProperty : ILotusFilterProperty
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Свойство/поле по которому идет фильтрация 
			/// </summary>
			public String PropertyName { get; set; } = default!;

			/// <summary>
			/// Функция для фильтрации
			/// </summary>
			public TFilterFunction Function { get; set; }

			/// <summary>
			/// Тип свойства
			/// </summary>
			public TEntityPropertyType PropertyType { get; set; }

			/// <summary>
			/// Статус типа свойства массив
			/// </summary>
			public Boolean? IsArray { get; set; }

			/// <summary>
			/// Учитывать регистр при фильтрации строк
			/// </summary>
			public Boolean? IsSensativeCase { get; set; }

			/// <summary>
			/// Значение
			/// </summary>
			public String? Value { get; set; }

			/// <summary>
			/// Массив значений
			/// </summary>
			public String[]? Values { get; set; }
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CFilterProperty()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return base.ToString();
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить информацию о свойстве фильтрации
			/// </summary>
			/// <typeparam name="TItem">Тпи объекта</typeparam>
			/// <returns>Информация о свойстве</returns>
			//---------------------------------------------------------------------------------------------------------
			public PropertyInfo GetPropertyInfo<TItem>()
			{
				var propertyInfo = typeof(TItem).GetProperties().First(property => property.Name == PropertyName);
				return propertyInfo;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получить константу дерева выражения для искомого значения
			/// </summary>
			/// <param name="index">Индекс искомого значения. -1 значения по умолчанию</param>
			/// <returns>Константа</returns>
			//---------------------------------------------------------------------------------------------------------
			public ConstantExpression GetConstantExpression(Int32 index = -1)
			{
				string value = index == -1 ? Value! : Values![index];

				ConstantExpression constantExpression = null;
				switch (PropertyType) 
				{
					case TEntityPropertyType.Boolean:
						{
							constantExpression = Expression.Constant(XBoolean.Parse(value));
						}
						break;
					case TEntityPropertyType.Integer:
						{
							constantExpression = Expression.Constant(XNumbers.ParseInt(value));
						}
						break;
					case TEntityPropertyType.Enum:
						{
							constantExpression = Expression.Constant(XNumbers.ParseInt(value));
						}
						break;
					case TEntityPropertyType.Float:
						{
							constantExpression = Expression.Constant(XNumbers.ParseSingle(value));
						}
						break;
					case TEntityPropertyType.DateTime:
						{
							constantExpression = Expression.Constant(DateTime.Parse(value));
						}
						break;
					case TEntityPropertyType.String:
						{
							constantExpression = Expression.Constant(value);
						}
						break;
				}

				return constantExpression;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================