//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Методы расширений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusXmlExtension.cs
*		Методы расширения для сериализации базовых классов платформы .NET в XML формат.
*		Реализация методов расширений потоков чтения и записи XML данных, а также методов работы с объектной моделью
*	документа XML для сериализации базовых классов платформы .NET в XML формат.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreExtension
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения потоков чтения и записи XML данных для сериализации 
		/// базовых классов платформы NET в XML формат
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XXmlStreamExtension
		{
			#region ======================================= ЗАПИСЬ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись строкового значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Строковое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteStringToAttribute(this XmlWriter xmlWriter, String name, String value)
			{
				xmlWriter.WriteAttributeString(name, value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись логического значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Логическое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteBooleanToAttribute(this XmlWriter xmlWriter, String name, Boolean value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись целочисленного значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Целочисленное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteIntegerToAttribute(this XmlWriter xmlWriter, String name, Int32 value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись целочисленного значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Целочисленное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteLongToAttribute(this XmlWriter xmlWriter, String name, Int64 value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись вещественного значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Вещественное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteSingleToAttribute(this XmlWriter xmlWriter, String name, Single value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись вещественного значения в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Вещественное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDoubleToAttribute(this XmlWriter xmlWriter, String name, Double value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка целых значений в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="integers">Список целых значений</param>
			/// <param name="lengthString">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteIntegerListToAttribute(this XmlWriter xmlWriter, String name, IList<Int32> integers, Int32 lengthString = 10)
			{
				if (integers != null && integers.Count > 0)
				{
					xmlWriter.WriteStartAttribute(name);
					var sb = new StringBuilder(integers.Count * 4);

					// Записываем данные по порядку
					for (var i = 0; i < integers.Count; i++)
					{
						// Для лучшей читаемости
						if (lengthString > 0)
						{
							if (i % lengthString == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(integers[i]);
					}

					xmlWriter.WriteValue(sb.ToString());
					xmlWriter.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="floats">Список вещественных значений</param>
			/// <param name="lengthString">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteSingleListToAttribute(this XmlWriter xmlWriter, String name, IList<Single> floats, Int32 lengthString = 10)
			{
				if (floats != null && floats.Count > 0)
				{
					xmlWriter.WriteStartAttribute(name);
					var sb = new StringBuilder(floats.Count * 4);

					// Записываем данные по порядку
					for (var i = 0; i < floats.Count; i++)
					{
						// Для лучшей читаемости
						if (lengthString > 0)
						{
							if (i % lengthString == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(floats[i]);
					}

					xmlWriter.WriteValue(sb.ToString());
					xmlWriter.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="doubles">Список вещественных значений</param>
			/// <param name="lengthString">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDoubleListToAttribute(this XmlWriter xmlWriter, String name, IList<Double> doubles, Int32 lengthString = 10)
			{
				if (doubles != null && doubles.Count > 0)
				{
					xmlWriter.WriteStartAttribute(name);
					var sb = new StringBuilder(doubles.Count * 4);

					// Записываем данные по порядку
					for (var i = 0; i < doubles.Count; i++)
					{
						// Для лучшей читаемости
						if (lengthString > 0)
						{
							if (i % lengthString == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(doubles[i]);
					}

					xmlWriter.WriteValue(sb.ToString());
					xmlWriter.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись значение перечисления в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Перечисление</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteEnumToAttribute(this XmlWriter xmlWriter, String name, Enum value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись даты-времени в формат атрибутов
			/// </summary>
			/// <param name="xmlWriter">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Дата-время</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDateTimeAttribute(this XmlWriter xmlWriter, String name, DateTime value)
			{
				xmlWriter.WriteAttributeString(name, value.ToString());
			}
			#endregion

			#region ======================================= ЧТЕНИЕ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и при необходимости перемещение к следующему элементу
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveToNextElement(this XmlReader xmlReader)
			{
				if (xmlReader.NodeType == XmlNodeType.Element) return;

				// Перемещаемся к следующему элементу
				while (xmlReader.NodeType != XmlNodeType.Element)
				{
					xmlReader.Read();

					if (xmlReader.EOF) break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и при необходимости перемещение к указанному элементу
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="elementName">Имя элемента</param>
			/// <returns>Статус перемещения к элементу</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean MoveToElement(this XmlReader xmlReader, String elementName)
			{
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == elementName)
				{
					if (xmlReader.IsEmptyElement && xmlReader.AttributeCount == 0)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					xmlReader.ReadToFollowing(elementName);
					if (xmlReader.IsEmptyElement && xmlReader.AttributeCount == 0)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение строкового значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ReadStringFromAttribute(this XmlReader xmlReader, String name, String defaultValue = "")
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return value;
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение логического значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Логическое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ReadBooleanFromAttribute(this XmlReader xmlReader, String name, Boolean defaultValue = false)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return XBoolean.Parse(value);
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение целочисленного значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ReadIntegerFromAttribute(this XmlReader xmlReader, String name, Int32 defaultValue = 0)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return Int32.Parse(value);
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение целочисленного значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 ReadLongFromAttribute(this XmlReader xmlReader, String name, Int64 defaultValue = -1)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return Int64.Parse(value);
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение вещественного значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Вещественное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ReadSingleFromAttribute(this XmlReader xmlReader, String name, Single defaultValue = 0)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return XNumbers.ParseSingle(value);
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение вещественного значения из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Вещественное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ReadDoubleFromAttribute(this XmlReader xmlReader, String name, Double defaultValue = 0)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return XNumbers.ParseDouble(value);
				}
				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива целых значений из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив целых значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32[]? ReadIntegersFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					var values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						var massive = new Int32[values.Length];

						for (var i = 0; i < values.Length; i++)
						{
							massive[i] = Int32.Parse(values[i]);
						}

						return massive;
					}
				}
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива вещественных значений из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив вещественных значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single[]? ReadSinglesFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					var values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						var massive = new Single[values.Length];

						for (var i = 0; i < values.Length; i++)
						{
							massive[i] = XNumbers.ParseSingle(values[i]);
						}

						return massive;
					}
				}
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива вещественных значений из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив вещественных значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double[]? ReadDoublesFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					var values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						var massive = new Double[values.Length];

						for (var i = 0; i < values.Length; i++)
						{
							massive[i] = XNumbers.ParseDouble(values[i]);
						}

						return massive;
					}
				}
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных перечисления из формата атрибутов
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Перечисление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ReadEnumFromAttribute<TEnum>(this XmlReader xmlReader, String name, TEnum? defaultValue = default(TEnum))
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return (TEnum)Enum.Parse(typeof(TEnum), value);
				}
				return defaultValue!;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных даты-времени из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Дата-время</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime ReadDateTimeFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return DateTime.Parse(value);
				}

				return DateTime.Now;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных даты-времени из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Дата-время</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime ReadDateTimeFromAttribute(this XmlReader xmlReader, String name, DateTime defaultValue)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return DateTime.Parse(value);
				}

				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных типа версии из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Версия</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Version ReadVersionFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return new Version(value);
				}

				return new Version();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных типа версии из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Версия</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Version ReadVersionFromAttribute(this XmlReader xmlReader, String name, Version defaultValue)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return new Version(value);
				}

				return defaultValue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных универсального идентификатора ресурса из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Универсальный идентификатора ресурса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Uri ReadUriFromAttribute(this XmlReader xmlReader, String name)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return new Uri(value);
				}

				return new Uri("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных универсального идентификатора ресурса из формата атрибутов
			/// </summary>
			/// <param name="xmlReader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Универсальный идентификатора ресурса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Uri ReadUriFromAttribute(this XmlReader xmlReader, String name, Uri defaultValue)
			{
				String? value;
				if ((value = xmlReader.GetAttribute(name)) != null)
				{
					return new Uri(value);
				}

				return defaultValue;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения объектной модели XML для сериализации 
		/// базовых классов платформы NET в XML формат
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XXmlDocumentExtension
		{
			#region ======================================= РАБОТА С АТРИБУТАМИ =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAttributeValueFromName(this XmlNode @this, String attributeName, String defaultValue = "")
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					return @this.Attributes[attributeName]!.Value;
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						return @this.Attributes[upper_name]!.Value;
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean GetAttributeValueFromNameAsBoolean(this XmlNode @this, String attributeName, Boolean defaultValue = false)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return XBoolean.Parse(value);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return XBoolean.Parse(value);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 GetAttributeValueFromNameAsInteger(this XmlNode @this, String attributeName, Int32 defaultValue = 0)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return Int32.Parse(value);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return Int32.Parse(value);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 GetAttributeValueFromNameAsLong(this XmlNode @this, String attributeName, Int64 defaultValue = 0)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return Int64.Parse(value);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return Int64.Parse(value);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single GetAttributeValueFromNameAsSingle(this XmlNode @this, String attributeName, Single defaultValue = 0)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return XNumbers.ParseSingle(value, defaultValue);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return XNumbers.ParseSingle(value, defaultValue);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetAttributeValueFromNameAsDouble(this XmlNode @this, String attributeName, Single defaultValue = 0)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return XNumbers.ParseDouble(value, defaultValue);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return XNumbers.ParseDouble(value, defaultValue);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAttributeValueFromNameAsDecimal(this XmlNode @this, String attributeName, Decimal defaultValue = 0)
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return XNumbers.ParseDecimal(value, defaultValue);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return XNumbers.ParseDecimal(value, defaultValue);
					}
					else
					{
						return defaultValue;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum GetAttributeValueFromNameAsEnum<TEnum>(this XmlNode @this, String attributeName, TEnum? defaultValue = default(TEnum))
			{
				if (@this.Attributes == null) return defaultValue!;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return (TEnum)Enum.Parse(typeof(TEnum), value);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return (TEnum)Enum.Parse(typeof(TEnum), value);
					}
					else
					{
						return defaultValue!;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime GetAttributeValueFromNameAsDateTime(this XmlNode @this, String attributeName,
				DateTime defaultValue = default(DateTime))
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes[attributeName] != null)
				{
					var value = @this.Attributes[attributeName]!.Value;
					return DateTime.Parse(value);
				}
				else
				{
					var upper_name = attributeName.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						var value = @this.Attributes[upper_name]!.Value;
						return DateTime.Parse(value);
					}
					else
					{
						return defaultValue;
					}
				}
			}
			#endregion

			#region ======================================= РАБОТА С ЗАВИСИМЫМИ АТРИБУТАМИ ============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по зависимому имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attributeName">Имя атрибута</param>
			/// <param name="defaultValue">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAttributeValueFromDependentName(this XmlNode @this, String attributeName,
				String defaultValue = "")
			{
				if (@this.Attributes == null) return defaultValue;

				if (@this.Attributes["name"] != null)
				{
					if (@this.Attributes["name"]!.Value == attributeName)
					{
						return @this.Attributes["value"]!.Value;
					}
					else
					{
						return defaultValue;
					}
				}
				else
				{
					return defaultValue;
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