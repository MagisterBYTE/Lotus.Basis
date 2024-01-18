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
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreParameters
		*@{*/
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
				_value = new ListArray<IParameterItem>();
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(String parameterName)
				: base(parameterName)
			{
				_value = new ListArray<IParameterItem>();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameters">Список параметров</param>
			//---------------------------------------------------------------------------------------------------------
			public CParameters(String parameterName, params IParameterItem[] parameters)
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

			#region ======================================= МЕТОДЫ ILotusOwnerObject ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присоединение указанного зависимого объекта
			/// </summary>
			/// <param name="ownedObject">Объект</param>
			/// <param name="add">Статус добавления в коллекцию</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AttachOwnedObject(ILotusOwnedObject ownedObject, Boolean add)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отсоединение указанного зависимого объекта
			/// </summary>
			/// <param name="ownedObject">Объект</param>
			/// <param name="remove">Статус удаления из коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void DetachOwnedObject(ILotusOwnedObject ownedObject, Boolean remove)
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
			/// <param name="ownedObject">Зависимый объект</param>
			/// <param name="data">Объект, данные которого будут меняться</param>
			/// <param name="dataName">Имя данных</param>
			/// <returns>Статус разрешения/согласования изменения данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean OnNotifyUpdating(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
			{
				return true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Информирование данного объекта об окончании изменении данных указанного объекта
			/// </summary>
			/// <param name="ownedObject">Зависимый объект</param>
			/// <param name="data">Объект, данные которого изменились</param>
			/// <param name="dataName">Имя данных</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void OnNotifyUpdated(ILotusOwnedObject ownedObject, System.Object? data, String dataName)
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись в строковый поток в формате Json
			/// </summary>
			/// <param name="streamWriter">Строковый поток</param>
			/// <param name="depth">Текущая глубина вложенности</param>
			/// <param name="isArray">Статус массива</param>
			//---------------------------------------------------------------------------------------------------------
			public override void WriteToJson(StreamWriter streamWriter, Int32 depth, Boolean isArray)
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
					if(item != null) 
					{
						item.WriteToJson(streamWriter, depth + 1, false);
					}
				}

				streamWriter.Write("\n");
				streamWriter.Write(XString.Depths[depth]);
				streamWriter.Write("}");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение первого параметра имеющего указанный тип или значение по умолчанию
			/// </summary>
			/// <typeparam name="TType">Тип значения</typeparam>
			/// <param name="defaultValue">Значение по умолчанию если элемент не найден</param>
			/// <returns>Первый найденный параметрам с указанным типов или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
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

			#region ======================================= МЕТОДЫ ДОБАВЛЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить логический параметр
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddBool(String parameterName, Boolean parameterValue)
			{
				_value!.Add(new CParameterBool(parameterName, parameterValue));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить целочисленный параметр
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddInteger(String parameterName, Int32 parameterValue)
			{
				_value!.Add(new CParameterInteger(parameterName, parameterValue));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить вещественный параметр
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddReal(String parameterName, Double parameterValue)
			{
				_value!.Add(new CParameterReal(parameterName, parameterValue));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить строковый параметр
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddString(String parameterName, String parameterValue)
			{
				_value!.Add(new CParameterString(parameterName, parameterValue));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр перечисление
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddEnum<TEnum>(String parameterName, TEnum parameterValue) where TEnum : Enum
			{
				_value!.Add(new CParameterEnum<TEnum>(parameterName, parameterValue));
				return this;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр имеющий тип значания базового объекта
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValue">Значение параметра</param>
			/// <param name="allowDuplicates">Разрешить дубликаты объектов</param>
			/// <returns>Текущий список параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameters AddObject(String parameterName, System.Object parameterValue, Boolean allowDuplicates)
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр имеющий тип списка параметра
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterList<IParameterItem> AddListParameter(String parameterName)
			{
				var parameterList = new CParameterList<IParameterItem>(parameterName);

				_value!.Add(parameterList);

				return parameterList;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавить параметр имеющий тип шаблонного списка
			/// </summary>
			/// <typeparam name="TItem">Тип элемента списка</typeparam>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Список</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterList<TItem> AddListTemplate<TItem>(String parameterName)
			{
				var parameterList = new CParameterList<TItem>(parameterName);

				_value!.Add(parameterList);

				return parameterList;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЯ ДАННЫХ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение логического параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterBool? GetBool(String parameterName)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
					{
						return parameter;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения логического параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValueDefault">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean GetBoolValue(String parameterName, Boolean parameterValueDefault = false)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
					{
						return parameter.Value;
					}
				}

				return parameterValueDefault;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterInteger? GetInteger(String parameterName)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
					{
						return parameter;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValueDefault">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetIntegerValue(String parameterName, Int32 parameterValueDefault = -1)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
					{
						return parameter.Value;
					}
				}

				return parameterValueDefault;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterReal? GetReal(String parameterName)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
					{
						return parameter;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValueDefault">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Double GetRealValue(String parameterName, Double parameterValueDefault = -1)
			{

                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
					{
						return parameter.Value;
					}
				}

				return parameterValueDefault;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <returns>Параметр</returns>
			//---------------------------------------------------------------------------------------------------------
			public CParameterString? GetString(String parameterName)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
					{
						return parameter;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="parameterValueDefault">Значение параметра по умолчанию</param>
			/// <returns>Значение параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public String? GetStringValue(String parameterName, String parameterValueDefault = "")
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
					{
						return parameter.Value;
					}
				}

				return parameterValueDefault;
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБНОВЛЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения логического параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="newValue">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateBoolValue(String parameterName, Boolean newValue)
			{
				for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterBool parameter)
					{
						parameter.Value = newValue;
						return true;
					}
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения целочисленного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="newValue">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateIntegerValue(String parameterName, Int32 newValue)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterInteger parameter)
					{
						parameter.Value = newValue;
						return true;
					}
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения вещественного параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="newValue">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateRealValue(String parameterName, Double newValue)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterReal parameter)
					{
						parameter.Value = newValue;
						return true;
					}
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление значения строкового параметра с указанным именем
			/// </summary>
			/// <param name="parameterName">Имя параметра</param>
			/// <param name="newValue">Новое значение параметра</param>
			/// <returns>Статус обновления значения параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean UpdateStringValue(String parameterName, String newValue)
			{
                for (var i = 0; i < _value!.Count; i++)
				{
					if (String.Compare(parameterName, _value[i].Name) == 0 && _value[i] is CParameterString parameter)
					{
						parameter.Value = newValue;
						return true;
					}
				}

				return false;
			}
			#endregion

			#region ======================================= МЕТОДЫ ЗАГРУЗКИ ДАННЫХ ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка параметров из файла
			/// </summary>
			/// <param name="fileName">Полное имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public void Load(String fileName)
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

			#region ======================================= МЕТОДЫ СОХРАНЕНИЯ ДАННЫХ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения параметров в файл в формате Json
			/// </summary>
			/// <param name="fileName">Полное имя файла</param>
			/// <param name="human">Статус читаемого вида</param>
			//---------------------------------------------------------------------------------------------------------
			public void SaveToJson(String fileName, Boolean human)
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
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================