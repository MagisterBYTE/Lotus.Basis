using System;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Cериализатор для сохранения/загрузки объектов в формат Json.
    /// </summary>
    public class CSerializerJson : CBaseSerializer
    {
        #region Const
        //
        // БАЗОВЫЕ ПУТИ
        //
#if UNITY_2017_1_OR_NEWER
#if UNITY_EDITOR
		/// <summary>
		/// Путь по умолчанию для сохранения/загрузки файлов.
		/// </summary>
		public static String DefaultPath = XCoreSettings.AutoSavePath;
#else
		/// <summary>
		/// Путь по умолчанию для сохранения/загрузки  файлов.
		/// </summary>
		public static String DefaultPath = UnityEngine.Application.persistentDataPath;
#endif
#else
        /// <summary>
        /// Путь по умолчанию для сохранения/загрузки файлов.
        /// </summary>
        public static string DefaultPath = Environment.CurrentDirectory;
#endif

        //
        // БАЗОВЫЕ РАСШИРЕНИЯ
        //
        /// <summary>
        /// Расширение файла по умолчанию.
        /// </summary>
        public static string DefaultExt = XFileExtension.JSON_D;
        #endregion

        #region Fields
        protected internal JsonSerializer _serializer;
        #endregion

        #region Properties
        /// <summary>
        /// Сериализатор Json.
        /// </summary>
        public JsonSerializer Serializer
        {
            get
            {
                return _serializer;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CSerializerJson()
        {
            _serializer = JsonSerializer.CreateDefault();
            AddDefaultConverters(false);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public CSerializerJson(bool useTypeName)
        {
            _serializer = JsonSerializer.CreateDefault();
            AddDefaultConverters(useTypeName);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сериализатора.</param>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public CSerializerJson(string name, bool useTypeName = true)
            : base(name)
        {
            _serializer = JsonSerializer.CreateDefault();
            AddDefaultConverters(useTypeName);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление конвертера.
        /// </summary>
        /// <param name="converter">Конвертер.</param>
        public void AddConverter(JsonConverter converter)
        {
            _serializer.Converters.Add(converter);
        }

        /// <summary>
        /// Добавление стандартных конвертеров.
        /// </summary>
        /// <param name="useTypeName">Использовать имя типа.</param>
        public void AddDefaultConverters(bool useTypeName)
        {
            _serializer.Converters.Add(ColorConverter.Instance);
#if UNITY_2017_1_OR_NEWER
			_serializer.Converters.Add(CUnityColor32Converter.Instance);
			_serializer.Converters.Add(CUnityVector2Converter.Instance);
			_serializer.Converters.Add(CUnityVector2IntConverter.Instance);
			_serializer.Converters.Add(CUnityVector3Converter.Instance);
			_serializer.Converters.Add(CUnityVector3IntConverter.Instance);
			_serializer.Converters.Add(CUnityVector4Converter.Instance);
#endif
            _serializer.Formatting = Formatting.Indented;

            if (useTypeName)
            {
                _serializer.TypeNameHandling = TypeNameHandling.Auto;
                _serializer.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            }
        }
        #endregion

        #region Save methods
        /// <summary>
        /// Сохранения объекта в файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        public override void SaveTo(string fileName, object instance, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Создаем поток для записи
            var stream_writer = new StreamWriter(path, false, Encoding.UTF8);
            SaveTo(stream_writer, instance, parameters);
            stream_writer.Close();

#if UNITY_EDITOR
			// Обновляем в редакторе
			UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
			UnityEditor.EditorUtility.DisplayDialog(XFileDialog.FILE_SAVE_SUCCESSFULLY, "Path\n" + path, "OK");
#endif
        }

        /// <summary>
        /// Сохранения объекта в строку в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        /// <returns>Строка в формате Json.</returns>
        public string SaveTo(object instance, CParameters? parameters = null)
        {
            var file_data = new StringBuilder(200);

            // Создаем поток для записи
            var string_writer = new StringWriter(file_data);
            SaveTo(string_writer, instance, parameters);
            string_writer.Close();

            return file_data.ToString();
        }

        /// <summary>
        /// Сохранения объекта в поток данных в формате Json.
        /// </summary>
        /// <param name="textWriter">Средство для записи в поток строковых данных.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        public void SaveTo(TextWriter textWriter, object instance, CParameters? parameters = null)
        {
            // Добавляем себя в качестве параметра
            if (parameters == null) parameters = new CParameters("SerializerJson");
            parameters.AddObject(this.Name, this, false);

            if (instance is ILotusBeforeSave before_save)
            {
                before_save.OnBeforeSave(parameters);
            }

            _serializer.Serialize(textWriter, instance);

            if (instance is ILotusAfterSave after_save)
            {
                after_save.OnAfterSave(parameters);
            }
        }
        #endregion

        #region Load methods
        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public override object? LoadFrom(string fileName, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var string_json = File.ReadAllText(path);

            // Читаем объект
            var result = LoadFromString(string_json, parameters);

            return result;
        }

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <typeparam name="TResultType">Тип объекта.</typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public override TResultType? LoadFrom<TResultType>(string fileName, CParameters? parameters = null) where TResultType : default
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var string_json = File.ReadAllText(path);

            // Открываем поток
            var string_reader = new StringReader(string_json);

            // Читаем объект
            var result = LoadFrom<TResultType>(string_reader, parameters);
            string_reader.Close();

            return result;
        }

        /// <summary>
        /// Загрузка объекта из строки в формате Json.
        /// </summary>
        /// <param name="stringJson">Строка с данными в формате Json.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public object? LoadFromString(string stringJson, CParameters? parameters = null)
        {
            // Читаем объект
            var result = JsonConvert.DeserializeObject(stringJson);

            if (result is ILotusAfterLoad after_load)
            {
                // Добавляем себя в качестве параметра
                if (parameters == null) parameters = new CParameters("SerializerJson");
                parameters.AddObject(this.Name, this, false);

                after_load.OnAfterLoad(parameters);
            }

            return result;
        }

        /// <summary>
        /// Загрузка объекта из потока данных.
        /// </summary>
        /// <param name="textReader">Средство для чтения из потока строковых данных.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public TResultType? LoadFrom<TResultType>(TextReader textReader, CParameters? parameters = null)
        {
            TResultType? result;

            using (JsonReader reader = new JsonTextReader(textReader))
            {
                result = _serializer.Deserialize<TResultType>(reader);
            }

            if (result is ILotusAfterLoad after_load)
            {
                // Добавляем себя в качестве параметра
                if (parameters == null) parameters = new CParameters("SerializerJson");
                parameters.AddObject(this.Name, this, false);

                after_load.OnAfterLoad(parameters);
            }

            return result;
        }
        #endregion

        #region Update methods
        /// <summary>
        /// Обновление объекта из файла.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public override void UpdateFrom(object instance, string fileName, CParameters? parameters = null)
        {
            // Формируем правильный путь
            var path = XFilePath.GetFileName(DefaultPath, fileName, DefaultExt);

            // Читаем данные
            var string_json = File.ReadAllText(path);

            // Обновляем объект
            UpdateFromString(instance, string_json, parameters);
        }

        /// <summary>
        /// Обновление объекта из строки в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="stringJson">Строка с данными в формате Json.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public void UpdateFromString(object instance, string stringJson, CParameters? parameters = null)
        {
            // Добавляем себя в качестве параметра
            if (parameters == null) parameters = new CParameters("SerializerJson");
            parameters.AddObject(this.Name, this, false);

            if (instance is ILotusBeforeLoad before_load)
            {
                before_load.OnBeforeLoad(parameters);
            }

            // Обновляем объект
            JsonConvert.PopulateObject(stringJson, instance);

            if (instance is ILotusAfterLoad after_load)
            {
                after_load.OnAfterLoad(parameters);
            }
        }

        /// <summary>
        /// Обновление объект из потока данных в формате Json.
        /// </summary>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="textReader">Средство для чтения из потока строковых данных.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public void UpdateFrom(object instance, TextReader textReader, CParameters? parameters = null)
        {
            // Method intentionally left empty.
        }
        #endregion
    }
    /**@}*/
}