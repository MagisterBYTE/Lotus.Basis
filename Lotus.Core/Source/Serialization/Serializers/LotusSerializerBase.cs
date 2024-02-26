using System;
using System.Collections.Generic;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Интерфейс сериализатор данных.
    /// </summary>
    public interface ILotusSerializer : ILotusNameable
    {
        /// <summary>
        /// Сохранения объекта в файл.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        void SaveTo(string fileName, object instance, CParameters? parameters = null);

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public object? LoadFrom(string fileName, CParameters? parameters = null);

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <typeparam name="TResultType">Тип объекта.</typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        TResultType? LoadFrom<TResultType>(string fileName, CParameters? parameters = null);

        /// <summary>
        /// Обновление объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры обновления.</param>
        void UpdateFrom(object instance, string fileName, CParameters? parameters = null);
    }

    /// <summary>
    /// Базовый сериализатор.
    /// </summary>
    /// <remarks>
    /// Базовый сериализатор реализует только механизм получения данных для сериализации.
    /// </remarks>
    public class CBaseSerializer : ILotusSerializer
    {
        #region Fields
        protected internal string _name;
        protected internal Func<string, object> _constructor;
        protected internal Dictionary<long, ILotusSerializableObject> _serializableObjects;

#if UNITY_2017_1_OR_NEWER
		protected internal List<CSerializeReferenceUnity> _serializeReferences;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Имя сериализатора.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Конструктор для создания объекта по имени типа.
        /// </summary>
        public virtual Func<string, object> Constructor
        {
            get
            {
                return _constructor;
            }
            set
            {
                _constructor = value;
            }
        }

        /// <summary>
        /// Словарь всех объектов поддерживающих интерфейс сериализации объекта.
        /// </summary>
        public virtual Dictionary<long, ILotusSerializableObject> SerializableObjects
        {
            get
            {
                return _serializableObjects;
            }
        }

#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Список объект для связывания данных.
		/// </summary>
		public virtual List<CSerializeReferenceUnity> SerializeReferences
		{
			get
			{
				return (_serializeReferences);
			}
		}
#endif
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CBaseSerializer()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя сериализатора.</param>
        public CBaseSerializer(string name)
        {
            _name = name;
            _serializableObjects = new Dictionary<long, ILotusSerializableObject>(100);
#if UNITY_2017_1_OR_NEWER
			_serializeReferences = new List<CSerializeReferenceUnity>();
#endif
        }
        #endregion

        #region ILotusSerializer methods
        /// <summary>
        /// Сохранения объекта в файл.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="parameters">Параметры сохранения.</param>
        public virtual void SaveTo(string fileName, object instance, CParameters? parameters = null)
        {

        }

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public virtual object? LoadFrom(string fileName, CParameters? parameters = null)
        {
            return null;
        }

        /// <summary>
        /// Загрузка объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <typeparam name="TResultType">Тип объекта.</typeparam>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры загрузки.</param>
        /// <returns>Объект.</returns>
        public virtual TResultType? LoadFrom<TResultType>(string fileName, CParameters? parameters = null)
        {
            return default;
        }

        /// <summary>
        /// Обновление объекта из файла.
        /// </summary>
        /// <remarks>
        /// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция.
        /// </remarks>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="parameters">Параметры обновления.</param>
        public virtual void UpdateFrom(object instance, string fileName, CParameters? parameters = null)
        {
        }
        #endregion

        #region Object methods
        /// <summary>
        /// Обновление объектов поддерживающих интерфейс сериализации перед сохранением.
        /// </summary>
        public void UpdateSerializableBeforeSave()
        {
            if (SerializableObjects.Count > 0)
            {
                var parameters = new CParameters(new CParameterObject(this.Name, this));
                foreach (var item in SerializableObjects.Values)
                {
                    if (item is ILotusBeforeSave before_save)
                    {
                        before_save.OnBeforeSave(parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление объектов поддерживающих интерфейс сериализации после сохранения.
        /// </summary>
        public void UpdateSerializableAfterSave()
        {
            if (SerializableObjects.Count > 0)
            {
                var parameters = new CParameters(new CParameterObject(this.Name, this));
                foreach (var item in SerializableObjects.Values)
                {
                    if (item is ILotusAfterSave after_save)
                    {
                        after_save.OnAfterSave(parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление объектов поддерживающих интерфейс сериализации перед загрузкой.
        /// </summary>
        public void UpdateSerializableBeforeLoad()
        {
            if (SerializableObjects.Count > 0)
            {
                var parameters = new CParameters(new CParameterObject(this.Name, this));
                foreach (var item in SerializableObjects.Values)
                {
                    if (item is ILotusBeforeLoad before_load)
                    {
                        before_load.OnBeforeLoad(parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление объектов поддерживающих интерфейс сериализации после полной загрузки.
        /// </summary>
        public void UpdateSerializableAfterLoad()
        {
            if (SerializableObjects.Count > 0)
            {
                var parameters = new CParameters(new CParameterObject(this.Name, this));
                foreach (var item in SerializableObjects.Values)
                {
                    if (item is ILotusAfterLoad after_load)
                    {
                        after_load.OnAfterLoad(parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Очистка словаря объектов поддерживающих интерфейс сериализации.
        /// </summary>
        public void ClearSerializableObjects()
        {
            SerializableObjects.Clear();
        }
        #endregion

        #region Unity methods
#if UNITY_2017_1_OR_NEWER
		/// <summary>
		/// Связывание ссылочных данных.
		/// </summary>
		public virtual void LinkSerializeReferences()
		{
			for (var i = 0; i < _serializeReferences.Count; i++)
			{
				_serializeReferences[i].Link();
			}
		}

		/// <summary>
		/// Очистка ссылочных данных.
		/// </summary>
		public virtual void ClearSerializeReferences()
		{
			_serializeReferences.Clear();
		}
#endif
        #endregion
    }
    /**@}*/
}