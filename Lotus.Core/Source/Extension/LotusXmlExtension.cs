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
// Последнее изменение от 27.03.2022
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
		//! \addtogroup CoreExtension
		/*@{*/
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
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Строковое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteStringToAttribute(this XmlWriter xml_writer, String name, String value)
			{
				xml_writer.WriteAttributeString(name, value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись логического значения в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Логическое значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteBooleanToAttribute(this XmlWriter xml_writer, String name, Boolean value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись целочисленного значения в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Целочисленное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteIntegerToAttribute(this XmlWriter xml_writer, String name, Int32 value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись целочисленного значения в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Целочисленное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteLongToAttribute(this XmlWriter xml_writer, String name, Int64 value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись вещественного значения в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Вещественное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteSingleToAttribute(this XmlWriter xml_writer, String name, Single value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись вещественного значения в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Вещественное значение</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDoubleToAttribute(this XmlWriter xml_writer, String name, Double value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка целых значений в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="integers">Список целых значений</param>
			/// <param name="length_string">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteIntegerListToAttribute(this XmlWriter xml_writer, String name, IList<Int32> integers, Int32 length_string = 10)
			{
				if (integers != null && integers.Count > 0)
				{
					xml_writer.WriteStartAttribute(name);
					StringBuilder sb = new StringBuilder(integers.Count * 4);

					// Записываем данные по порядку
					for (Int32 i = 0; i < integers.Count; i++)
					{
						// Для лучшей читаемости
						if (length_string > 0)
						{
							if (i % length_string == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(integers[i]);
					}

					xml_writer.WriteValue(sb.ToString());
					xml_writer.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="floats">Список вещественных значений</param>
			/// <param name="length_string">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteSingleListToAttribute(this XmlWriter xml_writer, String name, IList<Single> floats, Int32 length_string = 10)
			{
				if (floats != null && floats.Count > 0)
				{
					xml_writer.WriteStartAttribute(name);
					StringBuilder sb = new StringBuilder(floats.Count * 4);

					// Записываем данные по порядку
					for (Int32 i = 0; i < floats.Count; i++)
					{
						// Для лучшей читаемости
						if (length_string > 0)
						{
							if (i % length_string == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(floats[i]);
					}

					xml_writer.WriteValue(sb.ToString());
					xml_writer.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись списка вещественных значений в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="doubles">Список вещественных значений</param>
			/// <param name="length_string">Длина строки значений</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDoubleListToAttribute(this XmlWriter xml_writer, String name, IList<Double> doubles, Int32 length_string = 10)
			{
				if (doubles != null && doubles.Count > 0)
				{
					xml_writer.WriteStartAttribute(name);
					StringBuilder sb = new StringBuilder(doubles.Count * 4);

					// Записываем данные по порядку
					for (Int32 i = 0; i < doubles.Count; i++)
					{
						// Для лучшей читаемости
						if (length_string > 0)
						{
							if (i % length_string == 0)
							{
								sb.Append("\n");
							}
						}

						sb.Append(doubles[i]);
					}

					xml_writer.WriteValue(sb.ToString());
					xml_writer.WriteEndAttribute();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись значение перечисления в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Перечисление</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteEnumToAttribute(this XmlWriter xml_writer, String name, Enum value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись даты-времени в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="value">Дата-время</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDateTimeAttribute(this XmlWriter xml_writer, String name, DateTime value)
			{
				xml_writer.WriteAttributeString(name, value.ToString());
			}
			#endregion

			#region ======================================= ЧТЕНИЕ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и при необходимости перемещение к следующему элементу
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveToNextElement(this XmlReader xml_reader)
			{
				if (xml_reader.NodeType == XmlNodeType.Element) return;

				// Перемещаемся к следующему элементу
				while (xml_reader.NodeType != XmlNodeType.Element)
				{
					xml_reader.Read();

					if (xml_reader.EOF) break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и при необходимости перемещение к указанному элементу
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="element_name">Имя элемента</param>
			/// <returns>Статус перемещения к элементу</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean MoveToElement(this XmlReader xml_reader, String element_name)
			{
				if (xml_reader.NodeType == XmlNodeType.Element && xml_reader.Name == element_name)
				{
					if (xml_reader.IsEmptyElement && xml_reader.AttributeCount == 0)
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
					xml_reader.ReadToFollowing(element_name);
					if (xml_reader.IsEmptyElement && xml_reader.AttributeCount == 0)
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
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String ReadStringFromAttribute(this XmlReader xml_reader, String name, String default_value = "")
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return value;
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение логического значения из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Логическое значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean ReadBooleanFromAttribute(this XmlReader xml_reader, String name, Boolean default_value = false)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return (XBoolean.Parse(value));
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение целочисленного значения из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 ReadIntegerFromAttribute(this XmlReader xml_reader, String name, Int32 default_value = 0)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Int32.Parse(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение целочисленного значения из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Целочисленное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 ReadLongFromAttribute(this XmlReader xml_reader, String name, Int64 default_value = -1)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Int64.Parse(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение вещественного значения из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Вещественное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single ReadSingleFromAttribute(this XmlReader xml_reader, String name, Single default_value = 0)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return XNumbers.ParseSingle(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение вещественного значения из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Вещественное значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double ReadDoubleFromAttribute(this XmlReader xml_reader, String name, Double default_value = 0)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return XNumbers.ParseDouble(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение массива целых значений из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив целых значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32[] ReadIntegersFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					String[] values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						Int32[] massive = new Int32[values.Length];

						for (Int32 i = 0; i < values.Length; i++)
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
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив вещественных значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single[] ReadSinglesFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					String[] values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						Single[] massive = new Single[values.Length];

						for (Int32 i = 0; i < values.Length; i++)
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
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Массив вещественных значений, или null если данные пустые</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double[] ReadDoublesFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					String[] values = value.Split(XChar.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length > 0)
					{
						Double[] massive = new Double[values.Length];

						for (Int32 i = 0; i < values.Length; i++)
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
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Перечисление</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum ReadEnumFromAttribute<TEnum>(this XmlReader xml_reader, String name, TEnum default_value = default(TEnum))
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return (TEnum)Enum.Parse(typeof(TEnum), value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных даты-времени из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Дата-время</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime ReadDateTimeFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return DateTime.Parse(value);
				}

				return DateTime.Now;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных даты-времени из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Дата-время</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime ReadDateTimeFromAttribute(this XmlReader xml_reader, String name, DateTime default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return DateTime.Parse(value);
				}

				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных типа версии из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Версия</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Version ReadVersionFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return new Version(value);
				}

				return new Version();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных типа версии из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Версия</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Version ReadVersionFromAttribute(this XmlReader xml_reader, String name, Version default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return new Version(value);
				}

				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных универсального идентификатора ресурса из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Универсальный идентификатора ресурса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Uri ReadUriFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return new Uri(value);
				}

				return new Uri("");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных универсального идентификатора ресурса из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Универсальный идентификатора ресурса</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Uri ReadUriFromAttribute(this XmlReader xml_reader, String name, Uri default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return new Uri(value);
				}

				return default_value;
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
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAttributeValueFromName(this XmlNode @this, String attribute_name, String default_value = "")
			{
				if (@this.Attributes[attribute_name] != null)
				{
					return (@this.Attributes[attribute_name].Value);
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						return (@this.Attributes[upper_name].Value);
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean GetAttributeValueFromNameAsBoolean(this XmlNode @this, String attribute_name, Boolean default_value = false)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (XBoolean.Parse(value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (XBoolean.Parse(value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 GetAttributeValueFromNameAsInteger(this XmlNode @this, String attribute_name, Int32 default_value = 0)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (Int32.Parse(value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (Int32.Parse(value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int64 GetAttributeValueFromNameAsLong(this XmlNode @this, String attribute_name, Int64 default_value = 0)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (Int64.Parse(value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (Int64.Parse(value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Single GetAttributeValueFromNameAsSingle(this XmlNode @this, String attribute_name, Single default_value = 0)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (XNumbers.ParseSingle(value, default_value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (XNumbers.ParseSingle(value, default_value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Double GetAttributeValueFromNameAsDouble(this XmlNode @this, String attribute_name, Single default_value = 0)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (XNumbers.ParseDouble(value, default_value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (XNumbers.ParseDouble(value, default_value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Decimal GetAttributeValueFromNameAsDecimal(this XmlNode @this, String attribute_name, Decimal default_value = 0)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (XNumbers.ParseDecimal(value, default_value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (XNumbers.ParseDecimal(value, default_value));
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TEnum GetAttributeValueFromNameAsEnum<TEnum>(this XmlNode @this, String attribute_name, TEnum default_value = default(TEnum))
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (TEnum)Enum.Parse(typeof(TEnum), value);
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (TEnum)Enum.Parse(typeof(TEnum), value);
					}
					else
					{
						return (default_value);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения атрибута по имени
			/// </summary>
			/// <param name="this">Узел документа XML</param>
			/// <param name="attribute_name">Имя атрибута</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static DateTime GetAttributeValueFromNameAsDateTime(this XmlNode @this, String attribute_name)
			{
				if (@this.Attributes[attribute_name] != null)
				{
					String value = @this.Attributes[attribute_name].Value;
					return (DateTime.Parse(value));
				}
				else
				{
					String upper_name = attribute_name.ToUpper();
					if (@this.Attributes[upper_name] != null)
					{
						String value = @this.Attributes[upper_name].Value;
						return (DateTime.Parse(value));
					}
					else
					{
						return (DateTime.Now);
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
			/// <param name="attribute_name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение атрибута</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetAttributeValueFromDependentName(this XmlNode @this, String attribute_name, 
				String default_value = "")
			{
				if (@this.Attributes["name"] != null)
				{
					if (@this.Attributes["name"].Value == attribute_name)
					{
						return (@this.Attributes["value"].Value);
					}
					else
					{
						return (default_value);
					}
				}
				else
				{
					return (default_value);
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================