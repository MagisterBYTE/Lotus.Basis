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
// Последнее изменение от 27.03.2022
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
		//! \addtogroup CoreDataBinding
		/*@{*/
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
			protected internal MemberInfo mModelMember;
			protected internal MemberInfo mViewMember;
			protected internal Func<System.Object, System.Object> mOnConvertToModel;
			protected internal Func<System.Object, System.Object> mOnConvertToView;
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
				get { return mModelMember; }
			}

			/// <summary>
			/// Член данных для связывания со стороны объекта представления
			/// </summary>
			public MemberInfo ViewMember
			{
				get { return mViewMember; }
			}

			/// <summary>
			/// Делегат для преобразования объекта представления в объект модели
			/// </summary>
			public Func<System.Object, System.Object> OnConvertToModel
			{
				get { return mOnConvertToModel; }
				set { mOnConvertToModel = value; }
			}

			/// <summary>
			/// Делегат для преобразования объекта модели в объект представления
			/// </summary>
			public Func<System.Object, System.Object> OnConvertToView
			{
				get { return mOnConvertToView; }
				set { mOnConvertToView = value; }
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
			/// <param name="model_instance">Экземпляр объекта модели</param>
			/// <param name="model_member_name">Имя члена объекта модели</param>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			/// <param name="view_member_name">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public CBindingReflection(System.Object model_instance, String model_member_name, System.Object view_instance, 
				String view_member_name)
			{
				SetModel(model_instance, model_member_name);
				SetView(view_instance, view_member_name);
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
			/// <param name="model_instance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object model_instance)
			{
				ResetModel(model_instance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <param name="model_instance">Экземпляр объекта модели</param>
			/// <param name="member_name">Имя члена объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object model_instance, String member_name)
			{
				ResetModel(model_instance);

				mModelMember = SetMemberType(model_instance, member_name, ref mModelMemberType);
				if (mModelMember != null)
				{
					mModelMemberName = member_name;
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
				return mModelMember.GetMemberValue(mModelInstance);
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
				if (mIsEnabled)
				{
					if (mModelMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (mModelPropertyChanged != null)
						{
							// Получаем актуальное значение
							System.Object value = GetModelValue();

							// Если есть конвертер используем его
							if (mOnConvertToView != null)
							{
								mViewMember.SetMemberValue(mViewInstance, mOnConvertToView(value));
							}
							else
							{
								if (mIsStringView)
								{
									mViewMember.SetMemberValue(mViewInstance, value.ToString());
								}
								else
								{
									mViewMember.SetMemberValue(mViewInstance, value);
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
			/// <param name="view_instance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object view_instance)
			{
				ResetView(view_instance);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			/// <param name="member_name">Имя члена типа объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object view_instance, String member_name)
			{
				ResetView(view_instance);
				mViewMember = SetMemberType(view_instance, member_name, ref mViewMemberType);
				if (mViewMember != null)
				{
					mViewMemberName = member_name;
				}
				if(mViewMember.GetMemberType() == typeof(String))
				{
					mIsStringView = true;
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
				return mViewMember.GetMemberValue(mViewInstance);
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
				if (mIsEnabled)
				{
					if (mViewMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (mViewPropertyChanged != null)
						{
							// Получаем актуальное значение
							System.Object value = GetModelValue();

							if (mOnConvertToModel != null)
							{
								mModelMember.SetMemberValue(mModelInstance, mOnConvertToModel(value));
							}
							else
							{
								mModelMember.SetMemberValue(mModelInstance, value);
							}
						}
					}
				}
			}
			#endregion

		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================