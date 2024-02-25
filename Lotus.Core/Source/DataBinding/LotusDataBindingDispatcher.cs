using System;
using System.Collections.Generic;

namespace Lotus.Core
{
    /** \addtogroup CoreDataBinding
    *@{*/
    /// <summary>
    /// Диспетчер привязок данных для хранения и управления всем привязками данных.
    /// </summary>
    /// <remarks>
    /// Реализация диспетчера привязок данных который обеспечивает централизованное управление всеми привязками
    /// данных, их создание и удаление
    /// </remarks>
    public static class XBindingDispatcher
    {
        #region Fields
        private static List<BindingBase> mBindings;
        #endregion

        #region Properties
        /// <summary>
        /// Все привязки данных.
        /// </summary>
        public static List<BindingBase> Bindings
        {
            get
            {
                if (mBindings == null)
                {
                    mBindings = new List<BindingBase>();
                }

                return mBindings;
            }
        }
        #endregion

        #region Creator methods 
        /// <summary>
        /// Создание привязки данных через рефлексию.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingReflection CreateReflection(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode = TBindingMode.ViewData)
        {
            var binding = new BindingReflection(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            Bindings.Add(binding);
            return binding;
        }

        /// <summary>
        /// Создание привязки данных через рефлексию.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <param name="onConvertToView">Делегат для преобразования объекта модели в объект представления.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingReflection CreateReflection(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode, Func<object, object> onConvertToView)
        {
            var binding = new BindingReflection(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            binding.OnConvertToView = onConvertToView;
            Bindings.Add(binding);
            return binding;
        }

        /// <summary>
        /// Создание привязки данных через рефлексию.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <param name="onConvertToView">Делегат для преобразования объекта модели в объект представления.</param>
        /// <param name="onConvertToModel">Делегат для преобразования объекта представления в объект модели.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingReflection CreateReflection(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode, Func<object, object> onConvertToView,
            Func<object, object> onConvertToModel)
        {
            var binding = new BindingReflection(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            binding.OnConvertToView = onConvertToView;
            binding.OnConvertToModel = onConvertToModel;
            Bindings.Add(binding);
            return binding;
        }

        /// <summary>
        /// Создание привязки данных через делегат.
        /// </summary>
        /// <typeparam name="TTypeModel">Тип члена объекта модели.</typeparam>
        /// <typeparam name="TTypeView">Тип члена объекта представления.</typeparam>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode = TBindingMode.ViewData)
        {
            var binding = new BindingDelegate<TTypeModel, TTypeView>(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            Bindings.Add(binding);
            return binding;
        }

        /// <summary>
        /// Создание привязки данных через делегат.
        /// </summary>
        /// <typeparam name="TTypeModel">Тип члена объекта модели.</typeparam>
        /// <typeparam name="TTypeView">Тип члена объекта представления.</typeparam>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <param name="onConvertToView">Делегат для преобразования объекта модели в объект представления.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode, Func<TTypeModel, TTypeView> onConvertToView)
        {
            var binding = new BindingDelegate<TTypeModel, TTypeView>(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            binding.OnConvertToView = onConvertToView;
            Bindings.Add(binding);
            return binding;
        }

        /// <summary>
        /// Создание привязки данных через делегат.
        /// </summary>
        /// <typeparam name="TTypeModel">Тип члена объекта модели.</typeparam>
        /// <typeparam name="TTypeView">Тип члена объекта представления.</typeparam>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="model">Объект модели.</param>
        /// <param name="modelName">Имя члена объекта модели.</param>
        /// <param name="view">Объект представления.</param>
        /// <param name="viewName">Имя члена объекта представления.</param>
        /// <param name="mode">Режим связывания данных между объектом модели и объектом представления.</param>
        /// <param name="onConvertToView">Делегат для преобразования объекта модели в объект представления.</param>
        /// <param name="onConvertToModel">Делегат для преобразования объекта представления в объект модели.</param>
        /// <returns>Экземпляр связывания данных.</returns>
        public static BindingDelegate<TTypeModel, TTypeView> CreateDelegate<TTypeModel, TTypeView>(string name, object model, string modelName, object view,
            string viewName, TBindingMode mode, Func<TTypeModel, TTypeView> onConvertToView,
            Func<TTypeView, TTypeModel> onConvertToModel)
        {
            var binding = new BindingDelegate<TTypeModel, TTypeView>(model, modelName, view, viewName);
            binding.Name = name;
            binding.Mode = mode;
            binding.OnConvertToView = onConvertToView;
            binding.OnConvertToModel = onConvertToModel;
            Bindings.Add(binding);
            return binding;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение привязки данных по имени.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <returns>Найденная привязка данных или null.</returns>
        public static BindingBase? GetBinding(string name)
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

        /// <summary>
        /// Удаление привязки данных по имени.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        public static void RemoveBinding(string name)
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

        /// <summary>
        /// Удаление привязки данных.
        /// </summary>
        /// <param name="element">Привязка данных.</param>
        public static void RemoveBinding(BindingBase element)
        {
            Bindings.Remove(element);
        }

        /// <summary>
        /// Удаление всех привязок данных.
        /// </summary>
        public static void ClearBindings()
        {
            Bindings.Clear();
        }

        /// <summary>
        /// Включение/отключение привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="isEnabled">Статус включения/отключения.</param>
        public static void SetBindingEnabled(string name, bool isEnabled)
        {
            for (var i = 0; i < Bindings.Count; i++)
            {
                if (Bindings[i].Name == name)
                {
                    Bindings[i].IsEnabled = isEnabled;
                    break;
                }
            }
        }

        /// <summary>
        /// Установка объекта модели привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="modelInstance">Экземпляр объекта модели.</param>
        public static void SetBindingModel(string name, object modelInstance)
        {
            for (var i = 0; i < Bindings.Count; i++)
            {
                if (Bindings[i].Name == name)
                {
                    Bindings[i].SetModel(modelInstance);
                    break;
                }
            }
        }

        /// <summary>
        /// Установка объекта представления привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <param name="viewInstance">Экземпляр объекта представления.</param>
        public static void SetBindingView(string name, object viewInstance)
        {
            for (var i = 0; i < Bindings.Count; i++)
            {
                if (Bindings[i].Name == name)
                {
                    Bindings[i].SetView(viewInstance);
                    break;
                }
            }
        }

        /// <summary>
        /// Получение объекта модели привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <returns>Экземпляр объекта модели.</returns>
        public static object? GetBindingModel(string name)
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

        /// <summary>
        /// Получение объекта представления привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <returns>Экземпляр объекта представления.</returns>
        public static object? GetBindingView(string name)
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

        /// <summary>
        /// Получение значения привязанного свойства/метода объекта модели привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <returns>Значение привязанного свойства/метода объекта модели привязки данных.</returns>
        public static object? GetBindingModelValue(string name)
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

        /// <summary>
        /// Получение значения привязанного свойства/метода объекта представления привязки данных.
        /// </summary>
        /// <param name="name">Имя привязки данных.</param>
        /// <returns>Значение привязанного свойства/метода объекта представления привязки данных.</returns>
        public static object? GetBindingViewValue(string name)
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
    /**@}*/
}