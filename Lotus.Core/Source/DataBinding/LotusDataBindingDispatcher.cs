//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingDispatcher.cs
*		Диспетчер привязок данных для хранения и управления всем привязками данных.
*		Реализация диспетчера привязок данных который обеспечивает централизованное управление всеми привязками
*	данных, их создание и удаление.
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
		/** \addtogroup CoreDataBinding
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Диспетчер привязок данных для хранения и управления всем привязками данных
		/// </summary>
		/// <remarks>
		/// Реализация диспетчера привязок данных который обеспечивает централизованное управление всеми привязками
		/// данных, их создание и удаление
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XBindingDispatcher
		{
			#region ======================================= ДАННЫЕ ====================================================
			private static List<CBindingBase> mBindings;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Все привязки данных
			/// </summary>
			public static List<CBindingBase> Bindings
			{
				get
				{
					if(mBindings == null)
					{
						mBindings = new List<CBindingBase>();
					}

					return (mBindings);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ СОЗДАНИЯ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через рефлексию
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CBindingReflection CreateReflection(String name, System.Object model, String model_name, System.Object view, 
				String view_name, TBindingMode mode = TBindingMode.ViewData)
			{
				var binding = new CBindingReflection(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				Bindings.Add(binding);
				return binding;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через рефлексию
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <param name="on_convert_to_view">Делегат для преобразования объекта модели в объект представления</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CBindingReflection CreateReflection(String name, System.Object model, String model_name, System.Object view,
				String view_name, TBindingMode mode, Func<System.Object, System.Object> on_convert_to_view)
			{
				var binding = new CBindingReflection(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				binding.OnConvertToView = on_convert_to_view;
				Bindings.Add(binding);
				return binding;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через рефлексию
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <param name="on_convert_to_view">Делегат для преобразования объекта модели в объект представления</param>
			/// <param name="on_convert_to_model">Делегат для преобразования объекта представления в объект модели</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CBindingReflection CreateReflection(String name, System.Object model, String model_name, System.Object view,
				String view_name, TBindingMode mode, Func<System.Object, System.Object> on_convert_to_view,
				Func<System.Object, System.Object> on_convert_to_model)
			{
				var binding = new CBindingReflection(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				binding.OnConvertToView = on_convert_to_view;
				binding.OnConvertToModel = on_convert_to_model;
				Bindings.Add(binding);
				return binding;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через делегат
			/// </summary>
			/// <typeparam name="TTypeModel">Тип члена объекта модели</typeparam>
			/// <typeparam name="TTypeView">Тип члена объекта представления</typeparam>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(String name, System.Object model, String model_name, System.Object view,
				String view_name, TBindingMode mode = TBindingMode.ViewData)
			{
				var binding = new BindingDelegate<TTypeModel, TTypeView>(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				Bindings.Add(binding);
				return binding;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через делегат
			/// </summary>
			/// <typeparam name="TTypeModel">Тип члена объекта модели</typeparam>
			/// <typeparam name="TTypeView">Тип члена объекта представления</typeparam>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <param name="on_convert_to_view">Делегат для преобразования объекта модели в объект представления</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(String name, System.Object model, String model_name, System.Object view,
				String view_name, TBindingMode mode, Func<TTypeModel, TTypeView> on_convert_to_view)
			{
				var binding = new BindingDelegate<TTypeModel, TTypeView>(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				binding.OnConvertToView = on_convert_to_view;
				Bindings.Add(binding);
				return binding;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание привязки данных через делегат
			/// </summary>
			/// <typeparam name="TTypeModel">Тип члена объекта модели</typeparam>
			/// <typeparam name="TTypeView">Тип члена объекта представления</typeparam>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model">Объект модели</param>
			/// <param name="model_name">Имя члена объекта модели</param>
			/// <param name="view">Объект представления</param>
			/// <param name="view_name">Имя члена объекта представления</param>
			/// <param name="mode">Режим связывания данных между объектом модели и объектом представления</param>
			/// <param name="on_convert_to_view">Делегат для преобразования объекта модели в объект представления</param>
			/// <param name="on_convert_to_model">Делегат для преобразования объекта представления в объект модели</param>
			/// <returns>Экземпляр связывания данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(String name, System.Object model, String model_name, System.Object view,
				String view_name, TBindingMode mode, Func<TTypeModel, TTypeView> on_convert_to_view, 
				Func<TTypeView, TTypeModel> on_convert_to_model)
			{
				var binding = new BindingDelegate<TTypeModel, TTypeView>(model, model_name, view, view_name);
				binding.Name = name;
				binding.Mode = mode;
				binding.OnConvertToView = on_convert_to_view;
				binding.OnConvertToModel = on_convert_to_model;
				Bindings.Add(binding);
				return binding;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение привязки данных по имени
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <returns>Найденная привязка данных или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CBindingBase GetBinding(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						return Bindings[i];
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление привязки данных по имени
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RemoveBinding(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						Bindings.RemoveAt(i);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление привязки данных
			/// </summary>
			/// <param name="element">Привязка данных</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RemoveBinding(CBindingBase element)
			{
				Bindings.Remove(element);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех привязок данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public static void ClearBindings()
			{
				Bindings.Clear();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включение/отключение привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="is_enabled">Статус включения/отключения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetBindingEnabled(String name, Boolean is_enabled)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						Bindings[i].IsEnabled = is_enabled;
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="model_instance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetBindingModel(String name, System.Object model_instance)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						Bindings[i].SetModel(model_instance);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetBindingView(String name, System.Object view_instance)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						Bindings[i].SetView(view_instance);
						break;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение объекта модели привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <returns>Экземпляр объекта модели</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetBindingModel(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						return Bindings[i].ModelInstance;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение объекта представления привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <returns>Экземпляр объекта представления</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetBindingView(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						return Bindings[i].ViewInstance;
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта модели привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <returns>Значение привязанного свойства/метода объекта модели привязки данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetBindingModelValue(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						return Bindings[i].GetModelValue();
					}
				}

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта представления привязки данных
			/// </summary>
			/// <param name="name">Имя привязки данных</param>
			/// <returns>Значение привязанного свойства/метода объекта представления привязки данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object GetBindingViewValue(String name)
			{
				for (var i = 0; i < Bindings.Count; i++)
				{
					if (Bindings[i].Name == name)
					{
						return Bindings[i].GetViewValue();
					}
				}

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