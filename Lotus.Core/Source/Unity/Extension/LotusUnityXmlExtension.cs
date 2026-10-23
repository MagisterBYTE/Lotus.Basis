#if UNITY_2017_1_OR_NEWER
using System;
using System.Xml;

using UnityEngine;

namespace Lotus.Core
{
    
    /** \addtogroup CoreUnityExtension
    *@{*/
    
    /// <summary>
    /// Статический класс реализующий методы расширения потоков чтения и записи XML данных для сериализации базовых классов и структурных типов Unity в XML формат
    /// </summary>
    /// <remarks>
    /// Реализация методов расширений потоков чтения и записи XML данных, а также методов работы с объектной моделью 
    /// документа XML для сериализации базовых классов и структурных типов Unity в XML формат
    /// </remarks>
    
    public static class XXmlStreamUnityExtension
    {
        #region Fields
        /// <summary>
        /// Имя идентификатора игрового объекта
        /// </summary>
        public const string ID = "ID";

        /// <summary>
        /// Имя игрового объекта
        /// </summary>
        public const string NAME = "Name";

        /// <summary>
        /// Полный путь игрового объекта
        /// </summary>
        public const string PATH = "Path";

        /// <summary>
        /// Имя тэга игрового объекта
        /// </summary>
        public const string TAG = "Tag";

        /// <summary>
        /// Имя типа компонента или ресурса
        /// </summary>
        public const string TYPE = "Type";
        #endregion

        #region Write POD methods
        
        /// <summary>
        /// Запись данных двухмерного вектора в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="vector">Двухмерный вектор</param>
        
        public static void WriteVector2DToAttribute(this XmlWriter xmlWriter, string name, Vector2 vector)
        {
            xmlWriter.WriteAttributeString(name, vector.SerializeToString());
        }

        
        /// <summary>
        /// Запись данных трехмерного вектора в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="vector">Трехмерный вектор</param>
        
        public static void WriteVector3DToAttribute(this XmlWriter xmlWriter, string name, Vector3 vector)
        {
            xmlWriter.WriteAttributeString(name, vector.SerializeToString());
        }

        
        /// <summary>
        /// Запись данных четырехмерного вектора в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="vector">Четырехмерный вектор</param>
        
        public static void WriteVector4DToAttribute(this XmlWriter xmlWriter, string name, Vector4 vector)
        {
            xmlWriter.WriteAttributeString(name, vector.SerializeToString());
        }

        
        /// <summary>
        /// Запись данных кватерниона в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="quaternion">Кватернион</param>
        
        public static void WriteQuaternionToAttribute(this XmlWriter xmlWriter, string name, Quaternion quaternion)
        {
            xmlWriter.WriteAttributeString(name, quaternion.SerializeToString());
        }

        
        /// <summary>
        /// Запись данных цветового значения в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="color">Цветовое значение</param>
        
        public static void WriteColorToAttribute(this XmlWriter xmlWriter, string name, Color color)
        {
            xmlWriter.WriteAttributeString(name, color.SerializeToString());
        }

        
        /// <summary>
        /// Запись данных прямоугольника в формат атрибутов
        /// </summary>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="rect">Прямоугольник</param>
        
        public static void WriteRectToAttribute(this XmlWriter xmlWriter, string name, Rect rect)
        {
            xmlWriter.WriteAttributeString(name, rect.SerializeToString());
        }
        #endregion

        #region Write reference methods
        
        /// <summary>
        /// Запись данных ссылки на компонент в формат атрибутов
        /// </summary>
        /// <remarks>
        /// Внимание!!!
        /// Запишется 5 атрибутов: идентификатор, имя, путь тэг и тип компонента
        /// </remarks>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="component">Компонент</param>
        
        public static void WriteComponentToAttribute(this XmlWriter xmlWriter, string name, UnityEngine.Component component)
        {
            if (component != null)
            {
                xmlWriter.WriteAttributeString(name + ID, component.gameObject.GetEntityId().ToString());
                xmlWriter.WriteAttributeString(name + NAME, component.gameObject.name);
                //xmlWriter.WriteAttributeString(name + PATH, XSerializatorUnity.GetPathScene(component));
                xmlWriter.WriteAttributeString(name + TAG, component.gameObject.tag);
                xmlWriter.WriteAttributeString(name + TYPE, component.GetType().Name);
            }
            else
            {
                xmlWriter.WriteAttributeString(name + ID, "-1");
            }
        }

        
        /// <summary>
        /// Запись данных ссылки на игровой объект в формат атрибутов
        /// </summary>
        /// <remarks>
        /// Внимание!!!
        /// Запишется 4 атрибута: идентификатор, имя, путь и тэг
        /// </remarks>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="gameObject">Компонент</param>
        
        public static void WriteGameObjectToAttribute(this XmlWriter xmlWriter, string name, GameObject gameObject)
        {
            if (gameObject != null)
            {
                xmlWriter.WriteAttributeString(name + ID, gameObject.GetEntityId().ToString());
                xmlWriter.WriteAttributeString(name + NAME, gameObject.name);
                //xmlWriter.WriteAttributeString(name + PATH, XSerializatorUnity.GetPathScene(gameObject));
                xmlWriter.WriteAttributeString(name + TAG, gameObject.tag);
            }
            else
            {
                xmlWriter.WriteAttributeString(name + ID, "-1");
            }
        }

        
        /// <summary>
        /// Запись данных ссылки на ресурс в формат атрибутов
        /// </summary>
        /// <remarks>
        /// Внимание!!!
        /// Запишется 3 атрибута: идентификатор, имя и тип ресурса
        /// </remarks>
        /// <param name="xmlWriter">Средство записи данных в формат XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="resource">Ресурс</param>
        
        public static void WriteResourceToAttribute(this XmlWriter xmlWriter, string name, UnityEngine.Object resource)
        {
            if (resource != null)
            {
                xmlWriter.WriteAttributeString(name + ID, resource.GetEntityId().ToString());
                xmlWriter.WriteAttributeString(name + NAME, resource.name);
                xmlWriter.WriteAttributeString(name + TYPE, resource.GetType().Name);
            }
            else
            {
                xmlWriter.WriteAttributeString(name + ID, "-1");
            }
        }
        #endregion

        #region Read POD methods
        
        /// <summary>
        /// Чтение данных двухмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Двухмерный вектор</returns>
        
        public static Vector2 ReadUnityVector2DFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector2.DeserializeFromString(value);
            }
            return Vector2.zero;
        }

        
        /// <summary>
        /// Чтение данных двухмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Двухмерный вектор</returns>
        
        public static Vector2 ReadUnityVector2DFromAttribute(this XmlReader xmlReader, string name, Vector2 defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector2.DeserializeFromString(value);
            }
            return defaultValue;
        }

        
        /// <summary>
        /// Чтение данных трехмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Трехмерный вектор</returns>
        
        public static Vector3 ReadUnityVector3DFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector3.DeserializeFromString(value);
            }
            return Vector3.zero;
        }

        
        /// <summary>
        /// Чтение данных трехмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Трехмерный вектор</returns>
        
        public static Vector3 ReadUnityVector3DFromAttribute(this XmlReader xmlReader, string name, Vector3 defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector3.DeserializeFromString(value);
            }
            return defaultValue;
        }

        
        /// <summary>
        /// Чтение данных четырехмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Четырехмерный вектор</returns>
        
        public static Vector4 ReadUnityVector4DFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector4.DeserializeFromString(value);
            }
            return Vector4.zero;
        }

        
        /// <summary>
        /// Чтение данных четырехмерного вектора в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Четырехмерный вектор</returns>
        
        public static Vector4 ReadUnityVector4DFromAttribute(this XmlReader xmlReader, string name, Vector4 defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityVector4.DeserializeFromString(value);
            }
            return defaultValue;
        }

        
        /// <summary>
        /// Чтение данных кватерниона в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Кватернион</returns>
        
        public static Quaternion ReadUnityQuaternionFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityQuaternion.DeserializeFromString(value);
            }
            return Quaternion.identity;
        }

        
        /// <summary>
        /// Чтение данных кватерниона в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Кватернион</returns>
        
        public static Quaternion ReadUnityQuaternionFromAttribute(this XmlReader xmlReader, string name, Quaternion defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityQuaternion.DeserializeFromString(value);
            }
            return defaultValue;
        }

        
        /// <summary>
        /// Чтение данных цветового значения в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color ReadUnityColorFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityColor.DeserializeFromString(value);
            }
            return Color.black;
        }

        
        /// <summary>
        /// Чтение данных цветового значения в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color ReadUnityColorFromAttribute(this XmlReader xmlReader, string name, Color defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityColor.DeserializeFromString(value);
            }
            return defaultValue;
        }

        
        /// <summary>
        /// Чтение данных прямоугольника в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <returns>Прямоугольник</returns>
        
        public static Rect ReadUnityRectFromAttribute(this XmlReader xmlReader, string name)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityRect.DeserializeFromString(value);
            }
            return Rect.zero;
        }

        
        /// <summary>
        /// Чтение данных прямоугольника в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Прямоугольник</returns>
        
        public static Rect ReadUnityRectFromAttribute(this XmlReader xmlReader, string name, Rect defaultValue)
        {
            string value;
            if ((value = xmlReader.GetAttribute(name)) != null)
            {
                return XUnityRect.DeserializeFromString(value);
            }
            return defaultValue;
        }
        #endregion

        #region Read reference methods
        
        /// <summary>
        /// Чтение данных ссылки на компонент в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Компонент Unity</returns>
        [Obsolete("Поиск через XSerializatorUnity отключён — метод фактически всегда возвращает defaultValue. Требуется новая реализация резолва ссылок")]
        public static TComponent ReadUnityComponentFromAttribute<TComponent>(this XmlReader xmlReader, string name, 
            TComponent defaultValue) where TComponent : UnityEngine.Component
        {
            TComponent component = null;
            GameObject gameObject = null;

            // Должно быть записано идентификатор, имя, путь тэг и тип компонента
            var valueId = xmlReader.GetAttribute(name + ID);
            var valueName = xmlReader.GetAttribute(name + NAME);
            var valuePath = xmlReader.GetAttribute(name + PATH);
            var valueTag = xmlReader.GetAttribute(name + TAG);
            var valueType = xmlReader.GetAttribute(name + TYPE);

            // Если имени нет значит однозначно выходим
            if (string.IsNullOrEmpty(valueName))
            {
                // Пробуем такой вариант
                valueName = xmlReader.GetAttribute(name);
                if (string.IsNullOrEmpty(valueName))
                {
                    return defaultValue;
                }
            }

            // Проверка на значение по умолчанию
            if (defaultValue != null && defaultValue.name == valueName) return defaultValue;

            // Получаем идентификатор
            var id = -1;
            if (valueId.IsExists())
            {
                int.TryParse(valueId, out id);
            }

            // Объекта нет возвращаем значение по умолчанию
            if (id == -1)
            {
                return (defaultValue);
            }

            // Ищем
            //gameObject = XSerializatorUnity.FindGameObject(id, valuePath, valueName, valueTag);

            if (gameObject != null)
            {
                //component = XSerializatorUnity.EnsureComponent(gameObject, typeof(TComponent)) as TComponent;
                return (component);
            }
            else
            {
                return (defaultValue);
            }
        }

        
        /// <summary>
        /// Чтение данных ссылки на игровой объект в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Игровой объект Unity</returns>
        [Obsolete("Поиск через XSerializatorUnity отключён — метод фактически всегда возвращает defaultValue. Требуется новая реализация резолва ссылок")]
        public static GameObject ReadUnityGameObjectFromAttribute(this XmlReader xmlReader, string name, GameObject defaultValue)
        {
            GameObject gameObject = null;

            // Должно быть записано идентификатор, имя, путь тэг и тип компонента
            var valueId = xmlReader.GetAttribute(name + ID);
            var valueName = xmlReader.GetAttribute(name + NAME);
            var valuePath = xmlReader.GetAttribute(name + PATH);
            var valueTag = xmlReader.GetAttribute(name + TAG);

            // Если имени нет значит однозначно выходим
            if (string.IsNullOrEmpty(valueName))
            {
                // Пробуем такой вариант
                valueName = xmlReader.GetAttribute(name);
                if (string.IsNullOrEmpty(valueName))
                {
                    return defaultValue;
                }
            }

            // Проверка на значение по умолчанию
            if (defaultValue != null && defaultValue.name == valueName) return defaultValue;

            // Получаем идентификатор
            var id = -1;
            if (valueId.IsExists())
            {
                int.TryParse(valueId, out id);
            }

            // Объекта нет возвращаем значение по умолчанию
            if (id == -1)
            {
                return (defaultValue);
            }

            // Ищем
            //gameObject = XSerializatorUnity.FindGameObject(id, valuePath, valueName, valueTag);

            if (gameObject != null)
            {
                return (gameObject);
            }
            else
            {
                return (defaultValue);
            }
        }

        
        /// <summary>
        /// Чтение данных ссылки на ресурс в формате атрибутов
        /// </summary>
        /// <param name="xmlReader">Средство чтения данных формата XML</param>
        /// <param name="name">Имя атрибута</param>
        /// <param name="defaultValue">Значение по умолчанию в случает отсутствия атрибута</param>
        /// <returns>Ресурс Unity</returns>
        [Obsolete("Поиск через XSerializatorUnity отключён — метод фактически всегда возвращает defaultValue. Требуется новая реализация резолва ссылок")]
        public static TResource ReadUnityResourceFromAttribute<TResource>(this XmlReader xmlReader, string name,
            TResource defaultValue) where TResource : UnityEngine.Object
        {
            TResource resource = null;

            // Должно быть записано идентификатор, имя и тип ресурса
            var valueId = xmlReader.GetAttribute(name + ID);
            var valueName = xmlReader.GetAttribute(name + NAME);
            var valueType = xmlReader.GetAttribute(name + TYPE);

            // Если имени нет значит однозначно выходим
            if (string.IsNullOrEmpty(valueName))
            {
                // Пробуем такой вариант
                valueName = xmlReader.GetAttribute(name);
                if (string.IsNullOrEmpty(valueName))
                {
                    return defaultValue;
                }
            }

            // Проверка на значение по умолчанию
            if(defaultValue != null && defaultValue.name == valueName) return defaultValue;

            // Получаем идентификатор
            var id = -1;
            if (valueId.IsExists())
            {
                int.TryParse(valueId, out id);
            }

            // Объекта нет возвращаем значение по умолчанию
            if (id == -1)
            {
                return (defaultValue);
            }

            // Ищем
            //resource = XSerializatorUnity.FindResource(id, valueName, typeof(TResource))as TResource;
            if(resource != null)
            {
                return (resource);
            }
            else
            {
                return (defaultValue);
            }
        }
        #endregion
    }
    
    /**@}*/
}
#endif
