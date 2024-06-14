using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения потоков чтения и записи XML данных для сериализации
    /// базовых классов платформы NET в XML формат.
    /// </summary>
    public static class XXmlStreamExtension
    {
        #region Write methods  
        /// <summary>
        /// Запись строкового значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Строковое значение.</param>
        public static void WriteStringToAttribute(this XmlWriter xmlWriter, string name, string value)
        {
            xmlWriter.WriteAttributeString(name, value);
        }

        /// <summary>
        /// Запись логического значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Логическое значение.</param>
        public static void WriteBooleanToAttribute(this XmlWriter xmlWriter, string name, bool value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись целочисленного значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Целочисленное значение.</param>
        public static void WriteIntegerToAttribute(this XmlWriter xmlWriter, string name, int value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись целочисленного значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Целочисленное значение.</param>
        public static void WriteLongToAttribute(this XmlWriter xmlWriter, string name, long value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись вещественного значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Вещественное значение.</param>
        public static void WriteSingleToAttribute(this XmlWriter xmlWriter, string name, float value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись вещественного значения в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Вещественное значение.</param>
        public static void WriteDoubleToAttribute(this XmlWriter xmlWriter, string name, double value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись списка целых значений в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="integers">Список целых значений.</param>
        /// <param name="lengthString">Длина строки значений.</param>
        public static void WriteIntegerListToAttribute(this XmlWriter xmlWriter, string name, IList<int> integers, int lengthString = 10)
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

        /// <summary>
        /// Запись списка вещественных значений в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="floats">Список вещественных значений.</param>
        /// <param name="lengthString">Длина строки значений.</param>
        public static void WriteSingleListToAttribute(this XmlWriter xmlWriter, string name, IList<float> floats, int lengthString = 10)
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

        /// <summary>
        /// Запись списка вещественных значений в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="doubles">Список вещественных значений.</param>
        /// <param name="lengthString">Длина строки значений.</param>
        public static void WriteDoubleListToAttribute(this XmlWriter xmlWriter, string name, IList<double> doubles, int lengthString = 10)
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

        /// <summary>
        /// Запись значение перечисления в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Перечисление.</param>
        public static void WriteEnumToAttribute(this XmlWriter xmlWriter, string name, Enum value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }

        /// <summary>
        /// Запись даты-времени в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="value">Дата-время.</param>
        public static void WriteDateTimeAttribute(this XmlWriter xmlWriter, string name, DateTime value)
        {
            xmlWriter.WriteAttributeString(name, value.ToString());
        }
        #endregion

        #region Read methods 
        /// <summary>
        /// Проверка и при необходимости перемещение к следующему элементу.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
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

        /// <summary>
        /// Проверка и при необходимости перемещение к указанному элементу.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="elementName">Имя элемента.</param>
        /// <returns>Статус перемещения к элементу.</returns>
        public static bool MoveToElement(this XmlReader xmlReader, string elementName)
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

        /// <summary>
        /// Чтение строкового значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Целочисленное значение.</returns>
        public static string ReadStringFromAttribute(this XmlReader xmlReader, string name, string defaultValue = "")
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение логического значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Логическое значение.</returns>
        public static bool ReadBooleanFromAttribute(this XmlReader xmlReader, string name, bool defaultValue = false)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XBooleanHelper.Parse(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение целочисленного значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Целочисленное значение.</returns>
        public static int ReadIntegerFromAttribute(this XmlReader xmlReader, string name, int defaultValue = 0)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XNumberHelper.ParseInt(value, defaultValue);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение целочисленного значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Целочисленное значение.</returns>
        public static long ReadLongFromAttribute(this XmlReader xmlReader, string name, long defaultValue = -1)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XNumberHelper.ParseLong(value, defaultValue);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение вещественного значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Вещественное значение.</returns>
        public static float ReadSingleFromAttribute(this XmlReader xmlReader, string name, float defaultValue = 0)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XNumberHelper.ParseSingle(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение вещественного значения из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Вещественное значение.</returns>
        public static double ReadDoubleFromAttribute(this XmlReader xmlReader, string name, double defaultValue = 0)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XNumberHelper.ParseDouble(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение массива целых значений из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Массив целых значений, или null если данные пустые.</returns>
        public static int[]? ReadIntegersFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                var values = value.Split(XCharHelper.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length > 0)
                {
                    var massive = new int[values.Length];

                    for (var i = 0; i < values.Length; i++)
                    {
                        massive[i] = XNumberHelper.ParseInt(values[i]);
                    }

                    return massive;
                }
            }
            return null;
        }

        /// <summary>
        /// Чтение массива вещественных значений из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Массив вещественных значений, или null если данные пустые.</returns>
        public static float[]? ReadSinglesFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                var values = value.Split(XCharHelper.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length > 0)
                {
                    var massive = new float[values.Length];

                    for (var i = 0; i < values.Length; i++)
                    {
                        massive[i] = XNumberHelper.ParseSingle(values[i]);
                    }

                    return massive;
                }
            }
            return null;
        }

        /// <summary>
        /// Чтение массива вещественных значений из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Массив вещественных значений, или null если данные пустые.</returns>
        public static double[]? ReadDoublesFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                var values = value.Split(XCharHelper.SeparatorComma, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length > 0)
                {
                    var massive = new double[values.Length];

                    for (var i = 0; i < values.Length; i++)
                    {
                        massive[i] = XNumberHelper.ParseDouble(values[i]);
                    }

                    return massive;
                }
            }
            return null;
        }

        /// <summary>
        /// Чтение данных перечисления из формата атрибутов.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Перечисление.</returns>
        public static TEnum ReadEnumFromAttribute<TEnum>(this XmlReader xmlReader, string name, TEnum? defaultValue = default(TEnum))
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            return defaultValue!;
        }

        /// <summary>
        /// Чтение данных даты-времени из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Дата-время.</returns>
        public static DateTime ReadDateTimeFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XDateTimeHelper.Parse(value);
            }

            return DateTime.Now;
        }

        /// <summary>
        /// Чтение данных даты-времени из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Дата-время.</returns>
        public static DateTime ReadDateTimeFromAttribute(this XmlReader xmlReader, string name, DateTime defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XDateTimeHelper.Parse(value);
            }

            return defaultValue;
        }

        /// <summary>
        /// Чтение данных типа версии из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Версия.</returns>
        public static Version ReadVersionFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return new Version(value);
            }

            return new Version();
        }

        /// <summary>
        /// Чтение данных типа версии из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Версия.</returns>
        public static Version ReadVersionFromAttribute(this XmlReader xmlReader, string name, Version defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return new Version(value);
            }

            return defaultValue;
        }

        /// <summary>
        /// Чтение данных универсального идентификатора ресурса из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Универсальный идентификатора ресурса.</returns>
        public static Uri ReadUriFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return new Uri(value);
            }

            return new Uri("");
        }

        /// <summary>
        /// Чтение данных универсального идентификатора ресурса из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Универсальный идентификатора ресурса.</returns>
        public static Uri ReadUriFromAttribute(this XmlReader xmlReader, string name, Uri defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return new Uri(value);
            }

            return defaultValue;
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения объектной модели XML для сериализации
    /// базовых классов платформы NET в XML формат.
    /// </summary>
    public static class XXmlDocumentExtension
    {
        #region Attribute methods
        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static string GetAttributeValueFromName(this XmlNode @this, string attributeName, string defaultValue = "")
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

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static bool GetAttributeValueFromNameAsBoolean(this XmlNode @this, string attributeName, bool defaultValue = false)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XBooleanHelper.Parse(value);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XBooleanHelper.Parse(value);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static int GetAttributeValueFromNameAsInteger(this XmlNode @this, string attributeName, int defaultValue = 0)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XNumberHelper.ParseInt(value, defaultValue);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XNumberHelper.ParseInt(value, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static long GetAttributeValueFromNameAsLong(this XmlNode @this, string attributeName, long defaultValue = 0)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XNumberHelper.ParseLong(value, defaultValue);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XNumberHelper.ParseLong(value, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static float GetAttributeValueFromNameAsSingle(this XmlNode @this, string attributeName, float defaultValue = 0)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XNumberHelper.ParseSingle(value, defaultValue);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XNumberHelper.ParseSingle(value, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static double GetAttributeValueFromNameAsDouble(this XmlNode @this, string attributeName, float defaultValue = 0)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XNumberHelper.ParseDouble(value, defaultValue);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XNumberHelper.ParseDouble(value, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static decimal GetAttributeValueFromNameAsDecimal(this XmlNode @this, string attributeName, decimal defaultValue = 0)
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XNumberHelper.ParseDecimal(value, defaultValue);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XNumberHelper.ParseDecimal(value, defaultValue);
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static TEnum GetAttributeValueFromNameAsEnum<TEnum>(this XmlNode @this, string attributeName, TEnum? defaultValue = default(TEnum))
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

        /// <summary>
        /// Получение значения атрибута по имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static DateTime GetAttributeValueFromNameAsDateTime(this XmlNode @this, string attributeName,
            DateTime defaultValue = default(DateTime))
        {
            if (@this.Attributes == null) return defaultValue;

            if (@this.Attributes[attributeName] != null)
            {
                var value = @this.Attributes[attributeName]!.Value;
                return XDateTimeHelper.Parse(value);
            }
            else
            {
                var upper_name = attributeName.ToUpper();
                if (@this.Attributes[upper_name] != null)
                {
                    var value = @this.Attributes[upper_name]!.Value;
                    return XDateTimeHelper.Parse(value);
                }
                else
                {
                    return defaultValue;
                }
            }
        }
        #endregion

        #region Attribute methods 
        /// <summary>
        /// Получение значения атрибута по зависимому имени.
        /// </summary>
        /// <param name="this">Узел документа XML.</param>
        /// <param name="attributeName">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Значение атрибута.</returns>
        public static string GetAttributeValueFromDependentName(this XmlNode @this, string attributeName,
                string defaultValue = "")
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
    /**@}*/
}