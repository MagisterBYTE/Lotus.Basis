//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingReflection.cs
*		Реализация класса для связывания данных на основе рефлексии.
*		Для связывания параметров используется стандартная рефлексия что является универсальным методом, 
*	но не достаточно эффективным и быстрым.
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
		/// Класс реализующий привязку данных между свойством/методом объекта модели и объекта представления
		/// </summary>
		/// <remarks>
		/// Реализация класса для связывания данных.
		/// Для связывания параметров используется стандартная рефлексия что является универсальным методом, но не 
		/// достаточно эффективным и быстрым
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CBindingReflection : CBindingBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal MemberInfo _modelMember;
			protected internal MemberInfo _viewMember;
			protected internal Func<System.Object, System.Object> _onConvertToModel;
			protected internal Func<System.Object, System.Object> _onConvertToView;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Член данных для связывания со стороны объекта модели
			/// </summary>
			public MemberInfo ModelMember
			{
				get { return _modelMember; }
			}

			/// <summary>
			/// Член данных для связывания со стороны объекта представления
			/// </summary>
			public MemberInfo ViewMember
			{
				get { return _viewMember; }
			}

			/// <summary>
			/// Делегат для преобразования объекта представления в объект модели
			/// </summary>
			public Func<System.Object, System.Object> OnConvertToModel
			{
				get { return _onConvertToModel; }
				set { _onConvertToModel = value; }
			}

			/// <summary>
			/// Делегат для преобразования объекта модели в объект представления
			/// </summary>
			public Func<System.Object, System.Object> OnConvertToView
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
			public CBindingReflection()
			{
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
			public CBindingReflection(System.Object modelInstance, String modelMemberName, System.Object viewInstance, 
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

				_modelMember = SetMemberType(modelInstance, memberName, ref _modelMemberType);
				if (_modelMember != null)
				{
					_modelMemberName = memberName;
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
				return _modelMember.GetMemberValue(_modelInstance);
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

							// Если есть конвертер используем его
							if (_onConvertToView != null)
							{
								_viewMember.SetMemberValue(_viewInstance, _onConvertToView(value));
							}
							else
							{
								if (_isStringView)
								{
									_viewMember.SetMemberValue(_viewInstance, value.ToString());
								}
								else
								{
									_viewMember.SetMemberValue(_viewInstance, value);
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
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <param name="viewInstance">Экземпляр объекта представления</param>
			/// <param name="memberName">Имя члена типа объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object viewInstance, String memberName)
			{
				ResetView(viewInstance);
				_viewMember = SetMemberType(viewInstance, memberName, ref _viewMemberType);
				if (_viewMember != null)
				{
					_viewMemberName = memberName;
				}
				if(_viewMember.GetMemberType() == typeof(String))
				{
					_isStringView = true;
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
				return _viewMember.GetMemberValue(_viewInstance);
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
								_modelMember.SetMemberValue(_modelInstance, _onConvertToModel(value));
							}
							else
							{
								_modelMember.SetMemberValue(_modelInstance, value);
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