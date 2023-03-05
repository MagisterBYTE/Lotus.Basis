//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема параметрических объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusParameters.cs
*		Определение класса для представления параметра значения которого представляет список параметров.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreParameters
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс для представления параметра значения которого представляет список параметров
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CParameters : ParameterItem<ListArray<IParameterItem>>, ILotusOwnerObject
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public override TParameterValueType ValueType
			{
				get { return TParameterValueType.Parameters; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CParameters()
			{
				mValue = new ListArray<IParameterItem>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameters">Список параметров</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(params IParameterItem[] parameters)
			{
				if (parameters != null && parameters.Length > 0)
				{
					for (Int32 i = 0; i < parameters.Length; i++)
					{
						if (parameters[i] != null)
						{
							parameters[i].IOwner = this;
						}
					}
				}

				mValue = new ListArray<IParameterItem>(parameters);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(String parameter_name)
				: base(parameter_name)
			{
				mValue = new ListArray<IParameterItem>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameters">Список параметров</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(String parameter_name, params IParameterItem[] parameters)
				: base(parameter_name)
			{
				if(parameters != null && parameters.Length > 0)
				{
					for (Int32 i = 0; i < parameters.Length; i++)
					{
						if(parameters[i] != null)
						{
							parameters[i].IOwner = this;
						}
					}
				}

				mValue = new ListArray<IParameterItem>(parameters);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			/// <param name="parameters">Список параметров</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(Int32 id, params IParameterItem[] parameters)
				: base(id)
			{
				if (parameters != null && parameters.Length > 0)
				{
					for (Int32 i = 0; i < parameters.Length; i++)
					{
						if (parameters[i] != null)
						{
							parameters[i].IOwner = this;
						}
					}
				}

				mValue = new ListArray<IParameterItem>(parameters);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="add">Статус добавления в коллекцию</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AttachOwnedObject(ILotusOwnedObject owned_object, Boolean add)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отсоединение указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Объект</param>
			/// <param name="remove">Статус удаления из коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void DetachOwnedObject(ILotusOwnedObject owned_object, Boolean remove)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление связей для зависимых объектов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateOwnedObjects()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта о начале изменения данных указанного зависимого объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект, данные которого будут меняться</param>
			/// <param name="data_name">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean OnNotifyUpdating(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта об окончании изменении данных указанного объекта
			/// </summary>
			/// <param name="owned_object">Зависимый объект</param>
			/// <param name="data">Объект, данные которого изменились</param>
			/// <param name="data_name">Имя данных</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OnNotifyUpdated(ILotusOwnedObject owned_object, System.Object data, String data_name)
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение первого параметра имеющего указанный тип или значение по умолчанию
			/// </summary>
			/// <typeparam name="TType">Тип значения</typeparam>
			/// <param name="default_value">Значение по умолчанию если элемент не найден</param>
			/// <returns>Первый найденный параметрам с указанным типов или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
			public TType GetValueOfType<TType>(TType default_value = default)
			{
				for (Int32 i = 0; i < Value.Count; i++)
				{
					if (Value[i].Value is TType result)
					{
						return (result);
					}
				}

				return (default_value);
			}
			#endregion

			#region ======================================= МЕТОДЫ ДОБАВЛЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить логический параметр
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddBool(String parameter_name, Boolean parameter_value)
			{
				mValue.Add(new CParameterBool(parameter_name, parameter_value));
				return (this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить целочисленный параметр
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddInteger(String parameter_name, Int32 parameter_value)
			{
				mValue.Add(new CParameterInteger(parameter_name, parameter_value));
				return (this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить вещественный параметр
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddReal(String parameter_name, Double parameter_value)
			{
				mValue.Add(new CParameterReal(parameter_name, parameter_value));
				return (this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить строковый параметр
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddString(String parameter_name, String parameter_value)
			{
				mValue.Add(new CParameterString(parameter_name, parameter_value));
				return (this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр перечисление
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddEnum<TEnum>(String parameter_name, TEnum parameter_value) where TEnum : Enum
			{
				mValue.Add(new CParameterEnum<TEnum>(parameter_name, parameter_value));
				return (this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр имеющий тип значания базового объекта
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value">Значение параметра</param>
			/// <param name="allow_duplicates">Разрешить дубликаты объектов</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddObject(String parameter_name, System.Object parameter_value, Boolean allow_duplicates)
			{
				if (allow_duplicates)
				{
					mValue.Add(new CParameterObject(parameter_name, parameter_value));
				}
				else
				{
					// Смотрим есть ли объект с таким значением и именем
					for (Int32 i = 0; i < Value.Count; i++)
					{
						if(Value[i].ValueType == TParameterValueType.Object &&
							Value[i].Value == parameter_value &&
							Value[i].Name == parameter_name)
						{
							return (this);
						}
					}

					// Если нет то добавялем
					mValue.Add(new CParameterObject(parameter_name, parameter_value));
				}
				return (this);
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЯ ДАННЫХ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение логического параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterBool GetBool(String parameter_name)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if(String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterBool parameter)
					{
						return (parameter);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения логического параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value_default">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean GetBoolValue(String parameter_name, Boolean parameter_value_default = false)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterBool parameter)
					{
						return (parameter.Value);
					}
				}

				return (parameter_value_default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterInteger GetInteger(String parameter_name)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterInteger parameter)
					{
						return (parameter);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value_default">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetIntegerValue(String parameter_name, Int32 parameter_value_default = -1)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterInteger parameter)
					{
						return (parameter.Value);
					}
				}

				return (parameter_value_default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterReal GetReal(String parameter_name)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterReal parameter)
					{
						return (parameter);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value_default">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetRealValue(String parameter_name, Double parameter_value_default = -1)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterReal parameter)
					{
						return (parameter.Value);
					}
				}

				return (parameter_value_default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterString GetString(String parameter_name)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterString parameter)
					{
						return (parameter);
					}
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="parameter_value_default">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public String GetStringValue(String parameter_name, String parameter_value_default = "")
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterString parameter)
					{
						return (parameter.Value);
					}
				}

				return (parameter_value_default);
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБНОВЛЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения логического параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="new_value">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateBoolValue(String parameter_name, Boolean new_value)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterBool parameter)
					{
						parameter.Value = new_value;
						return (true);
					}
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="new_value">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateIntegerValue(String parameter_name, Int32 new_value)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterInteger parameter)
					{
						parameter.Value = new_value;
						return (true);
					}
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="new_value">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateRealValue(String parameter_name, Double new_value)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterReal parameter)
					{
						parameter.Value = new_value;
						return (true);
					}
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			/// <param name="new_value">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateStringValue(String parameter_name, String new_value)
			{
				for (Int32 i = 0; i < mValue.Count; i++)
				{
					if (String.Compare(parameter_name, mValue[i].Name) == 0 && mValue[i] is CParameterString parameter)
					{
						parameter.Value = new_value;
						return (true);
					}
				}

				return (false);
			}
			#endregion

			#region ======================================= МЕТОДЫ ЗАГРУЗКИ ДАННЫХ ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка параметров из файла
			/// </summary>
			/// <param name="file_name">Полное имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void Load(String file_name)
			{
				//FileStream file_stream = new FileStream(file_name, FileMode.Open);
    //            JsonDocument json_doc = JsonDocument.Parse(file_stream);
    //            JsonElement root_element = json_doc.RootElement;

    //            foreach (JsonProperty item in root_element.EnumerateObject())
    //            {
    //                switch (item.Value.ValueKind)
    //                {
    //                    case JsonValueKind.Undefined:
    //                        break;
    //                    case JsonValueKind.Object:
    //                        break;
    //                    case JsonValueKind.Array:
    //                        break;
    //                    case JsonValueKind.String:
    //                        {
    //                            AddString(item.Name, item.Value.GetString());
    //                        }
    //                        break;
    //                    case JsonValueKind.Number:
    //                        {
    //                            String number = item.Value.GetString();
    //                            if (number.IsDotOrCommaSymbols())
    //                            {
    //                                Double value = XNumbers.ParseDouble(number);
    //                                AddReal(item.Name, value);
    //                            }
    //                            else
    //                            {
    //                                AddInteger(item.Name, item.Value.GetInt32());
    //                            }
    //                        }
    //                        break;
    //                    case JsonValueKind.True:
    //                        {
    //                            AddBool(item.Name, item.Value.GetBoolean());
    //                        }
    //                        break;
    //                    case JsonValueKind.False:
    //                        {
    //                            AddBool(item.Name, item.Value.GetBoolean());
    //                        }
    //                        break;
    //                    case JsonValueKind.Null:
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }

    //            file_stream.Close();
			}
			#endregion

			#region ======================================= МЕТОДЫ СОХРАНЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения параметров в файл в формате Json
			/// </summary>
			/// <param name="file_name">Полное имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void SaveToJson(String file_name)
			{
				FileStream file_stream = new FileStream(file_name, FileMode.Create);
				StreamWriter stream_writer = new StreamWriter(file_stream, System.Text.Encoding.UTF8);
				stream_writer.Write('{');

				for (Int32 i = 0; i < Value.Count; i++)
				{
					IParameterItem parameter = Value[i];

					switch (parameter.ValueType)
					{
						case TParameterValueType.Null:
							break;
						case TParameterValueType.Boolean:
							break;
						case TParameterValueType.Integer:
							{
								stream_writer.Write(XChar.DoubleQuotes);
								stream_writer.Write(parameter.Name);
								stream_writer.Write(XChar.DoubleQuotes);

								stream_writer.Write(": ");

								stream_writer.Write(parameter.Value.ToString());
							}
							break;
						case TParameterValueType.Real:
							{
								stream_writer.Write(XChar.DoubleQuotes);
								stream_writer.Write(parameter.Name);
								stream_writer.Write(XChar.DoubleQuotes);

								stream_writer.Write(": ");

								stream_writer.Write(parameter.Value.ToString());
							}
							break;
						case TParameterValueType.DateTime:
							break;
						case TParameterValueType.String:
							{
								stream_writer.Write(XChar.DoubleQuotes);
								stream_writer.Write(parameter.Name);
								stream_writer.Write(XChar.DoubleQuotes);

								stream_writer.Write(": ");

								stream_writer.Write(XChar.DoubleQuotes);
								stream_writer.Write(parameter.Value);
								stream_writer.Write(XChar.DoubleQuotes);
							}
							break;
						case TParameterValueType.Enum:
							break;
						case TParameterValueType.List:
							break;
						case TParameterValueType.Object:
							break;
						case TParameterValueType.Parameters:
							break;
						case TParameterValueType.Color:
							break;
						case TParameterValueType.Vector2D:
							break;
						case TParameterValueType.Vector3D:
							break;
						case TParameterValueType.Vector4D:
							break;
						default:
							break;
					}

					if(i != Value.Count - 1)
					{
						stream_writer.Write(XChar.Comma);
					}
				}

				stream_writer.Write('}');
				stream_writer.Close();
				file_stream.Close();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================