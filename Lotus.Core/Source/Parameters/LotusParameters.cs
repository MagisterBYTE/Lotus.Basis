using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreParameters
	*@{*/
    /// <summary>
    /// Класс для представления параметра значения которого представляет список параметров.
    /// </summary>
    [Serializable]
    public class CParameters : ParameterItem<ListArray<IParameterItem>>, ILotusOwnerObject
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.Parameters; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameters()
        {
            _value = new ListArray<IParameterItem>();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameters">Список параметров.</param>
        public CParameters(params IParameterItem[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        parameters[i].IOwner = this;
                    }
                }

                _value = new ListArray<IParameterItem>(parameters);
            }
            else
            {
                _value = new ListArray<IParameterItem>();
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        public CParameters(string parameterName)
            : base(parameterName)
        {
            _value = new ListArray<IParameterItem>();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameters">Список параметров.</param>
        public CParameters(string parameterName, params IParameterItem[] parameters)
            : base(parameterName)
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        parameters[i].IOwner = this;
                    }
                }

                _value = new ListArray<IParameterItem>(parameters);
            }
            else
            {
                _value = new ListArray<IParameterItem>();
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="parameters">Список параметров.</param>
        public CParameters(int id, params IParameterItem[] parameters)
            : base(id)
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        parameters[i].IOwner = this;
                    }
                }

                _value = new ListArray<IParameterItem>(parameters);
            }
            else
            {
                _value = new ListArray<IParameterItem>();
            }
        }
        #endregion

        #region ILotusOwnerObject methods
        /// <summary>
        /// Присоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="add">Статус добавления в коллекцию.</param>
        public virtual void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add)
        {

        }

        /// <summary>
        /// Отсоединение указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Объект.</param>
        /// <param name="remove">Статус удаления из коллекции.</param>
        public virtual void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove)
        {

        }

        /// <summary>
        /// Обновление связей для зависимых объектов.
        /// </summary>
        public virtual void UpdateOwnedObjects()
        {

        }

        /// <summary>
        /// Информирование данного объекта о начале изменения данных указанного зависимого объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого будут меняться.</param>
        /// <param name="dataName">Имя данных.</param>
        /// <returns>Статус разрешения/согласования изменения данных.</returns>
        public virtual bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object? data, string dataName)
        {
            return true;
        }

        /// <summary>
        /// Информирование данного объекта об окончании изменении данных указанного объекта.
        /// </summary>
        /// <param name="ownedObject">Зависимый объект.</param>
        /// <param name="data">Объект, данные которого изменились.</param>
        /// <param name="dataName">Имя данных.</param>
        public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, object? data, string dataName)
        {

        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XChar.NewLine);
            streamWriter.Write(XString.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XChar.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XChar.DoubleQuotes);

                streamWriter.Write(":");
                streamWriter.Write("\n");
                streamWriter.Write(XString.Depths[depth]);
            }

            streamWriter.Write("{");

            foreach (var item in _value!)
            {
                if (item != null)
                {
                    item.WriteToJson(streamWriter, depth + 1, false);
                }
            }

            streamWriter.Write("\n");
            streamWriter.Write(XString.Depths[depth]);
            streamWriter.Write("}");
        }

        /// <summary>
        /// Получение первого параметра имеющего указанный тип или значение по умолчанию.
        /// </summary>
        /// <typeparam name="TType">Тип значения.</typeparam>
        /// <param name="defaultValue">Значение по умолчанию если элемент не найден.</param>
        /// <returns>Первый найденный параметрам с указанным типов или значение по умолчанию.</returns>
        public TType GetValueOfType<TType>(TType? defaultValue = default)
        {
            if (Value == default) return defaultValue!;

            for (var i = 0; i < Value.Count; i++)
            {
                if (Value[i].Value is TType result)
                {
                    return result;
                }
            }

            return defaultValue!;
        }
        #endregion

        #region Add methods
        /// <summary>
        /// Добавить логический параметр.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddBool(string parameterName, bool parameterValue)
        {
            _value!.Add(new CParameterBool(parameterName, parameterValue));
            return this;
        }

        /// <summary>
        /// Добавить целочисленный параметр.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddInteger(string parameterName, int parameterValue)
        {
            _value!.Add(new CParameterInteger(parameterName, parameterValue));
            return this;
        }

        /// <summary>
        /// Добавить вещественный параметр.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddReal(string parameterName, double parameterValue)
        {
            _value!.Add(new CParameterReal(parameterName, parameterValue));
            return this;
        }

        /// <summary>
        /// Добавить строковый параметр.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddString(string parameterName, string parameterValue)
        {
            _value!.Add(new CParameterString(parameterName, parameterValue));
            return this;
        }

        /// <summary>
        /// Добавить параметр перечисление.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddEnum<TEnum>(string parameterName, TEnum parameterValue) where TEnum : Enum
        {
            _value!.Add(new CParameterEnum<TEnum>(parameterName, parameterValue));
            return this;
        }

        /// <summary>
        /// Добавить параметр имеющий тип значания базового объекта.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValue">Значение параметра.</param>
        /// <param name="allowDuplicates">Разрешить дубликаты объектов.</param>
        /// <returns>Текущий список параметров.</returns>
        public CParameters AddObject(string parameterName, object parameterValue, bool allowDuplicates)
        {
            if (allowDuplicates)
            {
                _value!.Add(new CParameterObject(parameterName, parameterValue));
            }
            else
            {
                // Смотрим есть ли объект с таким значением и именем
                for (var i = 0; i < _value!.Count; i++)
                {
                    if (_value[i].ValueType == TParameterValueType.Object &&
                        _value[i].Value == parameterValue &&
                        _value[i].Name == parameterName)
                    {
                        return this;
                    }
                }

                // Если нет то добавялем
                _value.Add(new CParameterObject(parameterName, parameterValue));
            }
            return this;
        }

        /// <summary>
        /// Добавить параметр имеющий тип списка параметра.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Список.</returns>
        public CParameterList<IParameterItem> AddListParameter(string parameterName)
        {
            var parameterList = new CParameterList<IParameterItem>(parameterName);

            _value!.Add(parameterList);

            return parameterList;
        }

        /// <summary>
        /// Добавить параметр имеющий тип шаблонного списка.
        /// </summary>
        /// <typeparam name="TItem">Тип элемента списка.</typeparam>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Список.</returns>
        public CParameterList<TItem> AddListTemplate<TItem>(string parameterName)
        {
            var parameterList = new CParameterList<TItem>(parameterName);

            _value!.Add(parameterList);

            return parameterList;
        }
        #endregion

        #region Get methods
        /// <summary>
        /// Получение логического параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Параметр.</returns>
        public CParameterBool? GetBool(string parameterName)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения логического параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValueDefault">Значение параметра по умолчанию.</param>
        /// <returns>Значение параметра.</returns>
        public bool GetBoolValue(string parameterName, bool parameterValueDefault = false)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
                {
                    return parameter.Value;
                }
            }

            return parameterValueDefault;
        }

        /// <summary>
        /// Получение целочисленного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Параметр.</returns>
        public CParameterInteger? GetInteger(string parameterName)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения целочисленного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValueDefault">Значение параметра по умолчанию.</param>
        /// <returns>Значение параметра.</returns>
        public int GetIntegerValue(string parameterName, int parameterValueDefault = -1)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
                {
                    return parameter.Value;
                }
            }

            return parameterValueDefault;
        }

        /// <summary>
        /// Получение вещественного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Параметр.</returns>
        public CParameterReal? GetReal(string parameterName)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения вещественного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValueDefault">Значение параметра по умолчанию.</param>
        /// <returns>Значение параметра.</returns>
        public double GetRealValue(string parameterName, double parameterValueDefault = -1)
        {

            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
                {
                    return parameter.Value;
                }
            }

            return parameterValueDefault;
        }

        /// <summary>
        /// Получение строкового параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <returns>Параметр.</returns>
        public CParameterString? GetString(string parameterName)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
                {
                    return parameter;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения строкового параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="parameterValueDefault">Значение параметра по умолчанию.</param>
        /// <returns>Значение параметра.</returns>
        public string? GetStringValue(string parameterName, string parameterValueDefault = "")
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
                {
                    return parameter.Value;
                }
            }

            return parameterValueDefault;
        }
        #endregion

        #region Update methods
        /// <summary>
        /// Обновление значения логического параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="newValue">Новое значение параметра.</param>
        /// <returns>Статус обновления значения параметра.</returns>
        public bool UpdateBoolValue(string parameterName, bool newValue)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
                {
                    parameter.Value = newValue;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Обновление значения целочисленного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="newValue">Новое значение параметра.</param>
        /// <returns>Статус обновления значения параметра.</returns>
        public bool UpdateIntegerValue(string parameterName, int newValue)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
                {
                    parameter.Value = newValue;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Обновление значения вещественного параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="newValue">Новое значение параметра.</param>
        /// <returns>Статус обновления значения параметра.</returns>
        public bool UpdateRealValue(string parameterName, double newValue)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
                {
                    parameter.Value = newValue;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Обновление значения строкового параметра с указанным именем.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="newValue">Новое значение параметра.</param>
        /// <returns>Статус обновления значения параметра.</returns>
        public bool UpdateStringValue(string parameterName, string newValue)
        {
            for (var i = 0; i < _value!.Count; i++)
            {
                if (string.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
                {
                    parameter.Value = newValue;
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Load methods
        /// <summary>
        /// Загрузка параметров из файла.
        /// </summary>
        /// <param name="fileName">Полное имя файла.</param>
        public void Load(string fileName)
        {
            //FileStream file_stream = new FileStream(file_name, FileMode.Open);
            //JsonDocument json_doc = JsonDocument.Parse(file_stream);
            //JsonElement root_element = json_doc.RootElement;

            //foreach (JsonProperty item in root_element.EnumerateObject())
            //{
            //	switch (item.Value.ValueKind)
            //	{
            //		case JsonValueKind.Undefined:
            //			break;
            //		case JsonValueKind.Object:
            //			break;
            //		case JsonValueKind.Array:
            //			break;
            //		case JsonValueKind.String:
            //			{
            //				AddString(item.Name, item.Value.GetString());
            //			}
            //			break;
            //		case JsonValueKind.Number:
            //			{
            //				String number = item.Value.GetString();
            //				if (number.IsDotOrCommaSymbols())
            //				{
            //					Double value = XNumbers.ParseDouble(number);
            //					AddReal(item.Name, value);
            //				}
            //				else
            //				{
            //					AddInteger(item.Name, item.Value.GetInt32());
            //				}
            //			}
            //			break;
            //		case JsonValueKind.True:
            //			{
            //				AddBool(item.Name, item.Value.GetBoolean());
            //			}
            //			break;
            //		case JsonValueKind.False:
            //			{
            //				AddBool(item.Name, item.Value.GetBoolean());
            //			}
            //			break;
            //		case JsonValueKind.Null:
            //			break;
            //		default:
            //			break;
            //	}
            //}

            //file_stream.Close();
        }
        #endregion

        #region Save methods
        /// <summary>
        /// Сохранения параметров в файл в формате Json.
        /// </summary>
        /// <param name="fileName">Полное имя файла.</param>
        /// <param name="human">Статус читаемого вида.</param>
        public void SaveToJson(string fileName, bool human)
        {
            if (_value == default) return;

            var file_stream = new FileStream(fileName, FileMode.Create);
            var stream_writer = new StreamWriter(file_stream, System.Text.Encoding.UTF8);

            stream_writer.Write("{");

            WriteToJson(stream_writer, 1, false);

            stream_writer.Write("\n}");

            stream_writer.Close();
            file_stream.Close();
        }
        #endregion
    }
    /**@}*/
}