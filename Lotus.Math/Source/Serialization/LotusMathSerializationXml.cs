using System.Xml;

namespace Lotus.Maths
{
    /**
     * \defgroup MathSerialization Подсистема сериализации
     * \ingroup Math
     * \brief Подсистема сериализации обеспечивает сериализацию математических типов в распространены форматы данных.
     * @{
     */
    /// <summary>
    /// Статический класс реализующий методы расширения потоков чтения и записи XML данных для сериализации.
    /// математических типов в XML формат.
    /// </summary>
    /// <remarks>
    /// Для обеспечения большей гибкости и универсальности сериализация базовых классов и данных
    /// математической подсистемы в формате XML предусмотрена только в формате атрибутов элементов XML.
    /// </remarks>
    public static class XMathSerializationXml
    {
        #region Write methods
        /// <summary>
        /// Запись данных прямоугольника в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="rect">Прямоугольник.</param>
        public static void WriteRect2DToAttribute(this XmlWriter xmlWriter, string name, Rect2Df rect)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(rect.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных прямоугольника в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="rect">Прямоугольник.</param>
        public static void WriteRect2DToAttribute(this XmlWriter xmlWriter, string name, Rect2D rect)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(rect.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных двухмерного вектора в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="vector">Двухмерный вектор.</param>
        public static void WriteVector2DToAttribute(this XmlWriter xmlWriter, string name, Vector2D vector)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(vector.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных двухмерного вектора в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="vector">Двухмерный вектор.</param>
        public static void WriteVector2DToAttribute(this XmlWriter xmlWriter, string name, Vector2Df vector)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(vector.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных двухмерного вектора в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="vector">Двухмерный вектор.</param>
        public static void WriteVector2DToAttribute(this XmlWriter xmlWriter, string name, Vector2Di vector)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(vector.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных трехмерного вектора в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="vector">Двухмерный вектор.</param>
        public static void WriteVector3DToAttribute(this XmlWriter xmlWriter, string name, Vector3D vector)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(vector.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }

        /// <summary>
        /// Запись данных трехмерного вектора в формат атрибутов.
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="vector">Двухмерный вектор.</param>
        public static void WriteVector3DToAttribute(this XmlWriter xmlWriter, string name, Vector3Df vector)
        {
            xmlWriter.WriteStartAttribute(name);
            xmlWriter.WriteValue(vector.SerializeToString());
            xmlWriter.WriteEndAttribute();
        }
        #endregion

        #region Read methods 
        /// <summary>
        /// Чтение данных прямоугольника из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Прямоугольник.</returns>
        public static Rect2D ReadMathRect2DFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Rect2D.DeserializeFromString(value);
            }
            return Rect2D.Empty;
        }

        /// <summary>
        /// Чтение данных прямоугольника из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Прямоугольник.</returns>
        public static Rect2D ReadMathRect2DFromAttribute(this XmlReader xmlReader, string name, Rect2D defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Rect2D.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных прямоугольника из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Прямоугольник.</returns>
        public static Rect2Df ReadMathRect2DfFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Rect2Df.DeserializeFromString(value);
            }
            return Rect2Df.Empty;
        }

        /// <summary>
        /// Чтение данных прямоугольника из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Прямоугольник.</returns>
        public static Rect2Df ReadMathRect2DfFromAttribute(this XmlReader xmlReader, string name, Rect2Df defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Rect2Df.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2D ReadMathVector2DFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2D.DeserializeFromString(value);
            }
            return Vector2D.Zero;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2D ReadMathVector2DFromAttribute(this XmlReader xmlReader, string name, Vector2D defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2D.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Df ReadMathVector2DfFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2Df.DeserializeFromString(value);
            }
            return Vector2Df.Zero;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Df ReadMathVector2DfFromAttribute(this XmlReader xmlReader, string name, Vector2Df defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2Df.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Di ReadMathVector2DiFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2Di.DeserializeFromString(value);
            }
            return Vector2Di.Zero;
        }

        /// <summary>
        /// Чтение данных двухмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Двухмерный вектор.</returns>
        public static Vector2Di ReadMathVector2DiFromAttribute(this XmlReader xmlReader, string name, Vector2Di defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector2Di.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных трехмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Трехмерный вектор.</returns>
        public static Vector3D ReadMathVector3DFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector3D.DeserializeFromString(value);
            }
            return Vector3D.Zero;
        }

        /// <summary>
        /// Чтение данных трехмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Трехмерный вектор.</returns>
        public static Vector3D ReadMathVector3DFromAttribute(this XmlReader xmlReader, string name, Vector3D defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector3D.DeserializeFromString(value);
            }
            return defaultValue;
        }

        /// <summary>
        /// Чтение данных трехмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <returns>Трехмерный вектор.</returns>
        public static Vector3Df ReadMathVector3DfFromAttribute(this XmlReader xmlReader, string name)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector3Df.DeserializeFromString(value);
            }
            return Vector3Df.Zero;
        }

        /// <summary>
        /// Чтение данных трехмерного вектора из формата атрибутов.
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML.</param>
        /// <param name="name">Имя атрибута.</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута.</param>
        /// <returns>Трехмерный вектор.</returns>
        public static Vector3Df ReadMathVector3DfFromAttribute(this XmlReader xmlReader, string name, Vector3Df defaultValue)
        {
            string? value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return Vector3Df.DeserializeFromString(value);
            }
            return Vector3Df.Zero;
        }
        #endregion
    }
    /**@}*/
}