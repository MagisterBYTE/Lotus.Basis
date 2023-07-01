//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сериализации
// Подраздел: Подсистема сериализаторов данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializerBase.cs
*		Определение концепции сериализатора данных.
*		Сериализатор данных непосредственно осуществляет процесс сохранения/загрузки данных из определённого формата данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreSerialization
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Интерфейс сериализатор данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusSerializer : ILotusNameable
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="fileName">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="parameters">Параметры сохранения</param>
			//---------------------------------------------------------------------------------------------------------
			void SaveTo(String fileName, System.Object instance, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="fileName">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object LoadFrom(String fileName, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="fileName">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			TResultType LoadFrom<TResultType>(String fileName, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="fileName">Имя файла</param>
			/// <param name="parameters">Параметры обновления</param>
			//---------------------------------------------------------------------------------------------------------
			void UpdateFrom(System.Object instance, String fileName, CParameters parameters = null);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый сериализатор
		/// </summary>
		/// <remarks>
		/// Базовый сериализатор реализует только механизм получения данных для сериализации
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CBaseSerializer : ILotusSerializer
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal String mName;
			protected internal Func<String, System.Object> mConstructor;
			protected internal Dictionary<Int64, ILotusSerializableObject> mSerializableObjects;

#if UNITY_2017_1_OR_NEWER
			protected internal List<CSerializeReferenceUnity> mSerializeReferences;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя сериализатора
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Конструктор для создания объекта по имени типа
			/// </summary>
			public virtual Func<String, System.Object> Constructor
			{
				get
				{
					return mConstructor;
				}
				set
				{
					mConstructor = value;
				}
			}

			/// <summary>
			/// Словарь всех объектов поддерживающих интерфейс сериализации объекта
			/// </summary>
			public virtual Dictionary<Int64, ILotusSerializableObject> SerializableObjects
			{
				get
				{
					return mSerializableObjects;
				}
			}

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Список объект для связывания данных
			/// </summary>
			public virtual List<CSerializeReferenceUnity> SerializeReferences
			{
				get
				{
					return (mSerializeReferences);
				}
			}
#endif
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBaseSerializer()
				: this("")
			{
				
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сериализатора</param>
			//---------------------------------------------------------------------------------------------------------
			public CBaseSerializer(String name)
			{
				mName = name;
				mSerializableObjects = new Dictionary<Int64, ILotusSerializableObject>(100);
#if UNITY_2017_1_OR_NEWER
				mSerializeReferences = new List<CSerializeReferenceUnity>();
#endif
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSerializer ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="parameters">Параметры сохранения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SaveTo(String file_name, System.Object instance, CParameters parameters = null)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object LoadFrom(String file_name, CParameters parameters = null)
			{
				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResultType LoadFrom<TResultType>(String file_name, CParameters parameters = null)
			{
				return default;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры обновления</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateFrom(System.Object instance, String file_name, CParameters parameters = null)
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБЪЕКТОВ СЕРИАЛИЗАЦИИ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации перед сохранением
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateSerializableBeforeSave()
			{
				if (SerializableObjects.Count > 0)
				{
					var parameters = new CParameters( new CParameterObject(this.Name, this));
					foreach (var item in SerializableObjects.Values)
					{
						if (item is ILotusBeforeSave before_save)
						{
							before_save.OnBeforeSave(parameters);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации после сохранения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации перед загрузкой
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации после полной загрузки
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
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

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка словаря объектов поддерживающих интерфейс сериализации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClearSerializableObjects()
			{
				SerializableObjects.Clear();
			}
			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ СВЯЗЫВАНИЯ ДАННЫХ ==============================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Связывание ссылочных данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void LinkSerializeReferences()
			{
				for (Int32 i = 0; i < mSerializeReferences.Count; i++)
				{
					mSerializeReferences[i].Link();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка ссылочных данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearSerializeReferences()
			{
				mSerializeReferences.Clear();
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================