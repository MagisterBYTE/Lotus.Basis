//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingBase.cs
*		Параметры для связывания данных.
*		Реализация класса для связывания данных одного члена объекта представления с одним членом данных объекта модели.
*	Связываться должны только одинаковые типы. Только к строковому типу представления можно связать любой тип объекта
*	модели так как происходит преобразования в текстовые данные.
*		Определение объекта представления (он же целевой объект) и объекта модели (он же объект источник) зависят только
*	от контекста использования и режима связывания. Это разделение условно, предназначено в первую очередь для того чтобы
*	просто идентифицировать данные в определении класса.
*		Для связывания данных используется стандартная технология рефлексии данных что не очень быстро и эффективно, 
*	но зато универсально, и технология Delegate.CreateDelegate котороя обеспечивает более быстрое обновление свойств и методов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Reflection;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreDataBinding
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс реализующий привязку данных между свойством/методом объекта модели и объекта представления
		/// </summary>
		/// <remarks>
		/// Реализация базового класса для связывания данных одного члена объекта представления с одним членом данных 
		/// объекта модели.
		/// Связываться должны только одинаковые типы. 
		/// Только к строковому типу представления можно связать любой тип объекта модели так как происходит преобразования в текстовые данные.
		/// Определение объекта представления (он же целевой объект) и объекта модели(он же объект источник) зависят только от
		/// контекста использования и режима связывания.
		/// Это разделение условно, предназначено в первую очередь для того что бы просто идентифицировать данные в 
		/// определении класса
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBindingBase : IComparable<CBindingBase>, IDisposable
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal String _name;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal Boolean _isEnabled = true;

#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TBindingMode _mode;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TBindingModeChanged _modeChanged;

			// Объект модели
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal String _modelMemberName;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TBindingMemberType _modelMemberType;
			protected internal System.Object _modelInstance;
			protected internal INotifyPropertyChanged _modelPropertyChanged;

			// Объект представления
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal String _viewMemberName;
#if UNITY_2017_1_OR_NEWER
			[UnityEngine.SerializeField]
#endif
			protected internal TBindingMemberType _viewMemberType;
			protected internal Boolean _isStringView;
			protected internal System.Object _viewInstance;
			protected internal INotifyPropertyChanged _viewPropertyChanged;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Имя привязки данных
			/// </summary>
			/// <remarks>
			/// Используется для идентификации привязки
			/// </remarks>
			public String Name
			{
				get { return _name; }
				set { _name = value; }
			}

			/// <summary>
			/// Статус включенности привязки данных
			/// </summary>
			/// <remarks>
			/// Применяется для временного отключения/включения связывания данных без удаления самой привязки
			/// </remarks>
			public Boolean IsEnabled
			{
				get { return _isEnabled; }
				set { _isEnabled = value; }
			}

			/// <summary>
			/// Режим связывания данных
			/// </summary>
			public TBindingMode Mode
			{
				get { return _mode; }
				set { _mode = value; }
			}

			/// <summary>
			/// Режим изменения свойства объекта представления
			/// </summary>
			/// <remarks>
			/// Запланировано для будущих реализаций
			/// </remarks>
			public TBindingModeChanged ModeChanged
			{
				get { return _modeChanged; }
				set { _modeChanged = value; }
			}

			//
			// ОБЪЕКТ МОДЕЛИ
			//
			/// <summary>
			/// Имя члена объекта привязки со стороны модели
			/// </summary>
			public String ModelMemberName
			{
				get { return _modelMemberName; }
				set { _modelMemberName = value; }
			}

			/// <summary>
			/// Тип члена объекта привязки со стороны модели
			/// </summary>
			public TBindingMemberType ModelMemberType
			{
				get { return _modelMemberType; }
				set { _modelMemberType = value; }
			}

			/// <summary>
			/// Экземпляр модели
			/// </summary>
			/// <remarks>
			/// Экземпляр модели это собственно объект модели, он не обязательно должен поддерживать интерфейс <see cref="ILotusNotifyPropertyChanged"/>
			/// например если его данными только управляют TBindingMode.DataManager
			/// </remarks>
			public System.Object ModelInstance
			{
				get { return _modelInstance; }
				set { _modelInstance = value; }
			}

			/// <summary>
			/// Интерфейс объекта модели для нотификации об изменении данных
			/// </summary>
			public INotifyPropertyChanged ModelPropertyChanged
			{
				get { return _modelPropertyChanged; }
			}

			//
			// ОБЪЕКТ ПРЕДСТАВЛЕНИЯ
			//
			/// <summary>
			/// Имя члена объекта привязки со стороны представления
			/// </summary>
			public String ViewMemberName
			{
				get { return _viewMemberName; }
				set { _viewMemberName = value; }
			}

			/// <summary>
			/// Тип члена объекта привязки со стороны представления
			/// </summary>
			public TBindingMemberType ViewMemberType
			{
				get { return _viewMemberType; }
				set { _viewMemberType = value; }
			}

			/// <summary>
			/// Статус строкового отображения 
			/// </summary>
			/// <remarks>
			/// Истинное значение означает что объект представления, как правило, лишь отображает 
			/// данные модели и представление данные модели возможно через стандартный метод <see cref="System.Object.ToString"/>
			/// </remarks>
			public Boolean IsStringView
			{
				get { return _isStringView; }
			}

			/// <summary>
			/// Экземпляр представления
			/// </summary>
			/// <remarks>
			/// Экземпляр представления это собственно объект представления, он не обязательно должен поддерживать интерфейс <see cref="ILotusNotifyPropertyChanged"/>
			/// например если только отображает данные TBindingMode.ViewData
			/// </remarks>
			public System.Object ViewInstance
			{
				get { return _viewInstance; }
				set { _viewInstance = value; }
			}

			/// <summary>
			/// Интерфейс объекта представления для нотификации об изменении данных
			/// </summary>
			public INotifyPropertyChanged ViewPropertyChanged
			{
				get { return _viewPropertyChanged; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBindingBase()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CBindingBase other)
			{
				return String.CompareOrdinal(Name, other.Name);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public String ToStringShort()
			{
				var mode = "";
				switch (_mode)
				{
					case TBindingMode.ViewData:
						mode = " <= ";
						break;
					case TBindingMode.DataManager:
						mode = " => ";
						break;
					case TBindingMode.TwoWay:
						mode = " <=> ";
						break;
					default:
						break;
				}

				return ViewMemberName + mode + ModelMemberName;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				var mode = "";
				switch (_mode)
				{
					case TBindingMode.ViewData:
						mode = "<=";
						break;
					case TBindingMode.DataManager:
						mode = "=>";
						break;
					case TBindingMode.TwoWay:
						mode = "<=>";
						break;
					default:
						break;
				}

				return "View (" + ViewMemberName + ") " + mode + " Model (" + ModelMemberName + ")";
			}
			#endregion

			#region ======================================= МЕТОДЫ IDisposable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Освобождение управляемых ресурсов
			/// </summary>
			/// <param name="disposing">Статус освобождения</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void Dispose(Boolean disposing)
			{
				// Освобождаем только управляемые ресурсы
				if (disposing)
				{
					if (_modelPropertyChanged != null)
					{
						_modelPropertyChanged.PropertyChanged -= UpdateModelProperty;
						_modelPropertyChanged = null;
					}
					if (_viewPropertyChanged != null)
					{
						_viewPropertyChanged.PropertyChanged -= UpdateViewProperty;
						_viewPropertyChanged = null;
					}
				}

				// Освобождаем неуправляемые ресурсы
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка типа члена объекта
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="memberName">Имя члена объекта</param>
			/// <param name="memberType">Тип члена объекта</param>
			/// <returns>Член данных</returns>
			//---------------------------------------------------------------------------------------------------------
			protected MemberInfo SetMemberType(System.Object instance, String memberName, ref TBindingMemberType memberType)
			{
				MemberInfo member = instance.GetType().GetMember(memberName)[0];
				if (member != null)
				{
					if (member.MemberType == MemberTypes.Property)
					{
						memberType = TBindingMemberType.Property;
					}
					else
					{
						if (member.MemberType == MemberTypes.Field)
						{
							memberType = TBindingMemberType.Field;
						}
						else
						{
							memberType = TBindingMemberType.Method;
						}
					}
				}

				return member;
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ МОДЕЛИ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка объекта модели
			/// </summary>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			protected void ResetModel(System.Object modelInstance)
			{
				if (_modelPropertyChanged != null)
				{
					_modelPropertyChanged.PropertyChanged -= UpdateModelProperty;
				}

				_modelInstance = modelInstance;
				_modelPropertyChanged = modelInstance as INotifyPropertyChanged;
				if (_modelPropertyChanged != null)
				{
					_modelPropertyChanged.PropertyChanged += UpdateModelProperty;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта модели
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void UpdateModelProperty(Object sender, PropertyChangedEventArgs args)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта модели уже корректно настроены
			/// </remarks>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetModel(System.Object modelInstance)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			/// <param name="memberName">Имя члена объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetModel(System.Object modelInstance, String memberName)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта модели
			/// </summary>
			/// <remarks>
			/// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
			/// его запросить, например, во время присоединения
			/// </remarks>
			/// <returns>Значение привязанного свойства/метода объекта модели</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object GetModelValue()
			{
				return null;
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ ПРЕДСТАВЛЕНИЯ ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Переустановка объекта представления
			/// </summary>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			protected void ResetView(System.Object viewInstance)
			{
				if (_viewPropertyChanged != null)
				{
					_viewPropertyChanged.PropertyChanged -= UpdateViewProperty;
				}

				_viewInstance = viewInstance;
				_viewPropertyChanged = viewInstance as INotifyPropertyChanged;
				if (_viewPropertyChanged != null)
				{
					_viewPropertyChanged.PropertyChanged += UpdateViewProperty;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта представления
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void UpdateViewProperty(Object sender, PropertyChangedEventArgs args)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта представления уже корректно настроены
			/// </remarks>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetView(System.Object viewInstance)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			/// <param name="memberName">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SetView(System.Object viewInstance, String memberName)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта представления
			/// </summary>
			/// <remarks>
			/// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
			/// его запросить, например, во время присоединения
			/// </remarks>
			/// <returns>Значение привязанного свойства/метода объекта представления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object GetViewValue()
			{
				return null;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================