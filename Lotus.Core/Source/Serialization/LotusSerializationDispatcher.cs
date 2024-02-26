using System.IO;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Диспетчер подсистемы сериализации данных для сохранения/загрузки объектов в различных форматах.
    /// </summary>
    /// <remarks>
    /// Диспетчер обеспечивает хранение и представление всех доступных сериализаторов, а также 
    /// при сохранение/загрузки данных выбирает нужный тип сериализатора.
    /// </remarks>
    public static class XSerializationDispatcher
    {
        #region Properties
        /// <summary>
        /// Текущий сериализатор в формат Xml.
        /// </summary>
        public static CSerializerXml SerializerXml { get; set; }

        /// <summary>
        /// Текущий сериализатор в формат Json.
        /// </summary>
        public static CSerializerJson SerializerJson { get; set; }
        #endregion

        #region Save methods
        /// <summary>
        /// Сохранения объекта в файл.
        /// </summary>
        /// <remarks>
        /// Формат записи определяется исходя из расширения файла.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        public static void SaveTo(string fileName, object instance)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case XFileExtension.XML_D:
                    {
                        if (SerializerXml == null)
                        {
                            SerializerXml = new CSerializerXml();
                        }

                        SerializerXml.SaveTo(fileName, instance);
                    }
                    break;
                case XFileExtension.JSON_D:
                    {
                        if (SerializerJson == null)
                        {
                            SerializerJson = new CSerializerJson();
                        }

                        SerializerJson.SaveTo(fileName, instance);
                    }
                    break;
                case XFileExtension.BIN_D:
                case XFileExtension.BYTES_D:
                    {
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Load methods
        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Формат чтения определяется исходя из расширения файла.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Объект.</returns>
        public static object? LoadFrom(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            object? result = null;
            switch (ext)
            {
                case XFileExtension.XML_D:
                    {
                        if (SerializerXml == null)
                        {
                            SerializerXml = new CSerializerXml();
                        }

                        result = SerializerXml.LoadFrom(fileName);
                    }
                    break;
                case XFileExtension.JSON_D:
                    {
                        if (SerializerJson == null)
                        {
                            SerializerJson = new CSerializerJson();
                        }

                        result = SerializerJson.LoadFrom(fileName);
                    }
                    break;
                case XFileExtension.BIN_D:
                case XFileExtension.BYTES_D:
                    {
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Формат чтения определяется исходя из расширения файла.
        /// </remarks>
        /// <typeparam name="TResultType">Тип объекта.</typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Объект.</returns>
        public static TResultType? LoadFrom<TResultType>(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            TResultType? result = default;
            switch (ext)
            {
                case XFileExtension.XML_D:
                    {
                        if (SerializerXml == null)
                        {
                            SerializerXml = new CSerializerXml();
                        }

                        result = SerializerXml.LoadFrom<TResultType>(fileName);
                    }
                    break;
                case XFileExtension.JSON_D:
                    {
                        if (SerializerJson == null)
                        {
                            SerializerJson = new CSerializerJson();
                        }

                        result = SerializerJson.LoadFrom<TResultType>(fileName);
                    }
                    break;
                case XFileExtension.BIN_D:
                case XFileExtension.BYTES_D:
                    {
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion

        #region Update methods
        /// <summary>
        /// Обновление объекта из файла.
        /// </summary>
        /// <remarks>
        /// Формат чтения определяется исходя из расширения файла.
        /// </remarks>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fileName">Имя файла.</param>
        public static void UpdateFrom(object instance, string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case XFileExtension.XML_D:
                    {
                        if (SerializerXml == null)
                        {
                            SerializerXml = new CSerializerXml();
                        }

                        SerializerXml.UpdateFrom(instance, fileName);
                    }
                    break;
                case XFileExtension.JSON_D:
                    {
                        if (SerializerJson == null)
                        {
                            SerializerJson = new CSerializerJson();
                        }

                        SerializerJson.UpdateFrom(instance, fileName);
                    }
                    break;
                case XFileExtension.BIN_D:
                case XFileExtension.BYTES_D:
                    {
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
    /**@}*/
}