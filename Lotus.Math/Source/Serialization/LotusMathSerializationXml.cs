//=====================================================================================================================
// Проект: Модуль математической системы
// Раздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMathSerializationXml.cs
*		Методы расширения для сериализации математических типов в XML формат.
*		Реализация методов расширений потоков чтения и записи XML данных, а также методов работы с объектной моделью 
*	документа XML для математических типов в XML формат.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
//=====================================================================================================================
namespace Lotus
{
	namespace Maths
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup MathSerialization Подсистема сериализации
         * \ingroup Math
         * \brief Подсистема сериализации обеспечивает сериализацию математических типов в распространены форматы данных.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширения потоков чтения и записи XML данных для сериализации 
		/// математических типов в XML формат
		/// </summary>
		/// <remarks>
		/// Для обеспечения большей гибкости и универсальности сериализация базовых классов и данных
		/// математической подсистемы в формате XML предусмотрена только в формате атрибутов элементов XML
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XMathSerializationXml
		{
			#region ======================================= ЗАПИСЬ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных прямоугольника в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="rect">Прямоугольник</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteRect2DToAttribute(this XmlWriter xml_writer, String name, Rect2Df rect)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(rect.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных прямоугольника в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="rect">Прямоугольник</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteRect2DToAttribute(this XmlWriter xml_writer, String name, Rect2D rect)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(rect.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных двухмерного вектора в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="vector">Двухмерный вектор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteVector2DToAttribute(this XmlWriter xml_writer, String name, Vector2D vector)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(vector.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных двухмерного вектора в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="vector">Двухмерный вектор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteVector2DToAttribute(this XmlWriter xml_writer, String name, Vector2Df vector)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(vector.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных двухмерного вектора в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="vector">Двухмерный вектор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteVector2DToAttribute(this XmlWriter xml_writer, String name, Vector2Di vector)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(vector.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных трехмерного вектора в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="vector">Двухмерный вектор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteVector3DToAttribute(this XmlWriter xml_writer, String name, Vector3D vector)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(vector.SerializeToString());
				xml_writer.WriteEndAttribute();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных трехмерного вектора в формат атрибутов
			/// </summary>
			/// <param name="xml_writer">Средство записи данных в формат XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="vector">Двухмерный вектор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteVector3DToAttribute(this XmlWriter xml_writer, String name, Vector3Df vector)
			{
				xml_writer.WriteStartAttribute(name);
				xml_writer.WriteValue(vector.SerializeToString());
				xml_writer.WriteEndAttribute();
			}
			#endregion

			#region ======================================= ЧТЕНИЕ ДАННЫХ =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных прямоугольника из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Rect2D ReadMathRect2DFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Rect2D.DeserializeFromString(value);
				}
				return Rect2D.Empty;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных прямоугольника из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Rect2D ReadMathRect2DFromAttribute(this XmlReader xml_reader, String name, Rect2D default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Rect2D.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных прямоугольника из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Rect2Df ReadMathRect2DfFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Rect2Df.DeserializeFromString(value);
				}
				return Rect2Df.Empty;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных прямоугольника из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Прямоугольник</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Rect2Df ReadMathRect2DfFromAttribute(this XmlReader xml_reader, String name, Rect2Df default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Rect2Df.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2D ReadMathVector2DFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2D.DeserializeFromString(value);
				}
				return Vector2D.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2D ReadMathVector2DFromAttribute(this XmlReader xml_reader, String name, Vector2D default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2D.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df ReadMathVector2DfFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2Df.DeserializeFromString(value);
				}
				return Vector2Df.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Df ReadMathVector2DfFromAttribute(this XmlReader xml_reader, String name, Vector2Df default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2Df.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Di ReadMathVector2DiFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2Di.DeserializeFromString(value);
				}
				return Vector2Di.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных двухмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Двухмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector2Di ReadMathVector2DiFromAttribute(this XmlReader xml_reader, String name, Vector2Di default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector2Di.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных трехмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Трехмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3D ReadMathVector3DFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector3D.DeserializeFromString(value);
				}
				return Vector3D.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных трехмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Трехмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3D ReadMathVector3DFromAttribute(this XmlReader xml_reader, String name, Vector3D default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector3D.DeserializeFromString(value);
				}
				return default_value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных трехмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <returns>Трехмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df ReadMathVector3DfFromAttribute(this XmlReader xml_reader, String name)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector3Df.DeserializeFromString(value);
				}
				return Vector3Df.Zero;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных трехмерного вектора из формата атрибутов
			/// </summary>
			/// <param name="xml_reader">Средство чтения данных формата XML</param>
			/// <param name="name">Имя атрибута</param>
			/// <param name="default_value">Значение по умолчанию в случает отсутствия атрибута</param>
			/// <returns>Трехмерный вектор</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Vector3Df ReadMathVector3DfFromAttribute(this XmlReader xml_reader, String name, Vector3Df default_value)
			{
				String value;
				if ((value = xml_reader.GetAttribute(name)) != null)
				{
					return Vector3Df.DeserializeFromString(value);
				}
				return Vector3Df.Zero;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================