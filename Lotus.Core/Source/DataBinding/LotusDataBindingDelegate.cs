//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingDelegate.cs
*		Реализация класса для связывания данных на основе делегата.
*		Для связывания параметров используется технология Delegate.CreateDelegate что обеспечивает более быстрое 
*	обновление свойств и полей.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Reflection;
using System.Collections.Generic;
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
		/// Класс реализующий привязку данных между свойством/методом объекта модели и объекта представления
		/// </summary>
		/// <remarks>
		/// Реализация класса для связывания данных.
		/// Для связывания параметров используется технология <see cref="Delegate.CreateDelegate(Type, Object, String)"/> 
		/// что обеспечивает более быстрое обновление свойств и полей
		/// </remarks>
		/// <typeparam name="TTypeModel">Тип члена объекта модели</typeparam>
		/// <typeparam name="TTypeView">Тип члена объекта представления</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class BindingDelegate<TTypeModel, TTypeView> : CBindingBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Action<TTypeModel> mActionModel;
			protected internal Action<TTypeView> mActionView;
			protected internal Func<TTypeView, TTypeModel> _onConvertToModel;
			protected internal Func<TTypeModel, TTypeView> _onConvertToView;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Делегат для установки значений объекту модели
			/// </summary>
			public Action<TTypeModel> ActionModel
			{
				get { return mActionModel; }
			}

			/// <summary>
			/// Делегат для установки значений объекту представления
			/// </summary>
			public Action<TTypeView> ActionView
			{
				get { return mActionView; }
			}

			/// <summary>
			/// Делегат для преобразования объекта представления в объект модели
			/// </summary>
			public Func<TTypeView, TTypeModel> OnConvertToModel
			{
				get { return _onConvertToModel; }
				set { _onConvertToModel = value; }
			}

			/// <summary>
			/// Делегат для преобразования объекта модели в объект представления
			/// </summary>
			public Func<TTypeModel, TTypeView> OnConvertToView
			{
				get { return _onConvertToView; }
				set { _onConvertToView = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public BindingDelegate()
			{
				_isStringView = typeof(TTypeView) == typeof(String);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			/// <param name="modelMemberName">Имя члена объекта модели</param>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			/// <param name="viewMemberName">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public BindingDelegate(System.Object modelInstance, String modelMemberName, System.Object viewInstance, 
				String viewMemberName)
			{
				SetModel(modelInstance, modelMemberName);
				SetView(viewInstance, viewMemberName);
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ МОДЕЛИ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта модели уже корректно настроены
			/// </remarks>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object modelInstance)
			{
				ResetModel(modelInstance);
				if (_mode != TBindingMode.ViewData)
				{
					var member_name_model = _modelMemberName;
					if (_modelMemberType == TBindingMemberType.Property)
					{
						member_name_model = "set_" + _modelMemberName;
					}
					try
					{
						mActionModel = (Action<TTypeModel>)Delegate.CreateDelegate(typeof(Action<TTypeModel>), modelInstance, member_name_model);
					}
					catch (Exception exc)
					{
#if UNITY_2017_1_OR_NEWER
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <param name="modelInstance">Экземпляр объекта модели</param>
			/// <param name="memberName">Имя члена объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object modelInstance, String memberName)
			{
				ResetModel(modelInstance);

				if (SetMemberType(modelInstance, memberName, ref _modelMemberType) != null)
				{
					_modelMemberName = memberName;
					if (_mode != TBindingMode.ViewData)
					{
						var member_name_model = _modelMemberName;
						if (_modelMemberType == TBindingMemberType.Property)
						{
							member_name_model = "set_" + _modelMemberName;
						}
						try
						{
							mActionModel = (Action<TTypeModel>)Delegate.CreateDelegate(typeof(Action<TTypeModel>), modelInstance, member_name_model);
						}
						catch (Exception exc)
						{
#if UNITY_2017_1_OR_NEWER
							UnityEngine.Debug.LogException(exc);
#else
							XLogger.LogException(exc);
#endif
						}
					}
				}
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
			public override System.Object GetModelValue()
			{
				// Проверяем сначала свойство 
				if (XReflection.ContainsProperty(_modelInstance, _modelMemberName))
				{
					return XReflection.GetPropertyValue(_modelInstance, _modelMemberName);
				}
				else
				{
					// Теперь поле
					return XReflection.GetFieldValue(_modelInstance, _modelMemberName);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта модели
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void UpdateModelProperty(Object sender, PropertyChangedEventArgs args)
			{
				if (_isEnabled)
				{
					if (_modelMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (_modelPropertyChanged != null)
						{
							// Получаем актуальное значение
							var value = GetModelValue();
							if (_onConvertToView != null)
							{
								mActionView(_onConvertToView((TTypeModel)value));
							}
							else
							{
								if (_isStringView)
								{
									mActionView((TTypeView)(System.Object)value.ToString());
								}
								else
								{
									mActionView((TTypeView)value);
								}
							}
						}
					}
				}
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ ПРЕДСТАВЛЕНИЯ ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта представления уже корректно настроены
			/// </remarks>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object viewInstance)
			{
				ResetView(viewInstance);
				if (_mode != TBindingMode.DataManager)
				{
					var member_name_view = _viewMemberName;
					if (_viewMemberType == TBindingMemberType.Property)
					{
						member_name_view = "set_" + _viewMemberName;
					}
					try
					{
						mActionView = (Action<TTypeView>)Delegate.CreateDelegate(typeof(Action<TTypeView>), viewInstance, member_name_view);
					}
					catch (Exception exc)
					{
#if UNITY_2017_1_OR_NEWER
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			/// <param name="memberName">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object viewInstance, String memberName)
			{
				_isStringView = typeof(TTypeView) == typeof(String);
				ResetView(viewInstance);
				if (SetMemberType(viewInstance, memberName, ref _viewMemberType) != null)
				{
					_viewMemberName = memberName;
					if (_mode != TBindingMode.DataManager)
					{
						var member_name_view = _viewMemberName;
						if (_viewMemberType == TBindingMemberType.Property)
						{
							member_name_view = "set_" + _viewMemberName;
						}
						try
						{
							mActionView = (Action<TTypeView>)Delegate.CreateDelegate(typeof(Action<TTypeView>), viewInstance, member_name_view);
						}
						catch (Exception exc)
						{
#if UNITY_2017_1_OR_NEWER
							UnityEngine.Debug.LogException(exc);
#else
							XLogger.LogException(exc);
#endif
						}
					}
				}
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
			public override System.Object GetViewValue()
			{
				// Проверяем сначала свойство 
				if (XReflection.ContainsProperty(_viewInstance, _viewMemberName))
				{
					return XReflection.GetPropertyValue(_viewInstance, _viewMemberName);
				}
				else
				{
					// Теперь поле
					return XReflection.GetFieldValue(_viewInstance, _viewMemberName);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта представления
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void UpdateViewProperty(Object sender, PropertyChangedEventArgs args)
			{
				if (_isEnabled)
				{
					if (_viewMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (_viewPropertyChanged != null)
						{
							// Получаем актуальное значение
							var value = GetModelValue();

							if (_onConvertToModel != null)
							{
								mActionModel(_onConvertToModel((TTypeView)value));
							}
							else
							{
								mActionModel((TTypeModel)value);
							}
						}
					}
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================