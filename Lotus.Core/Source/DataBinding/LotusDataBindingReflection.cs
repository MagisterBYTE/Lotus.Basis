using System;
using System.ComponentModel;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreDataBinding
    *@{*/
    /// <summary>
    /// Класс реализующий привязку данных между свойством/методом объекта модели и объекта представления.
    /// </summary>
    /// <remarks>
    /// Реализация класса для связывания данных.
    /// Для связывания параметров используется стандартная рефлексия что является универсальным методом, но не 
    /// достаточно эффективным и быстрым.
    /// </remarks>
    [Serializable]
    public class BindingReflection : BindingBase
    {
        #region Fields
        // Основные параметры
        protected internal MemberInfo _modelMember;
        protected internal MemberInfo _viewMember;
        protected internal Func<object, object> _onConvertToModel;
        protected internal Func<object, object> _onConvertToView;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Член данных для связывания со стороны объекта модели.
        /// </summary>
        public MemberInfo ModelMember
        {
            get { return _modelMember; }
        }

        /// <summary>
        /// Член данных для связывания со стороны объекта представления.
        /// </summary>
        public MemberInfo ViewMember
        {
            get { return _viewMember; }
        }

        /// <summary>
        /// Делегат для преобразования объекта представления в объект модели.
        /// </summary>
        public Func<object, object> OnConvertToModel
        {
            get { return _onConvertToModel; }
            set { _onConvertToModel = value; }
        }

        /// <summary>
        /// Делегат для преобразования объекта модели в объект представления.
        /// </summary>
        public Func<object, object> OnConvertToView
        {
            get { return _onConvertToView; }
            set { _onConvertToView = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public BindingReflection()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="modelInstance">Экземпляр объекта модели.</param>
        /// <param name="modelMemberName">Имя члена объекта модели.</param>
        /// <param name="viewInstance">Экземпляр объекта представления.</param>
        /// <param name="viewMemberName">Имя члена объекта представления.</param>
        public BindingReflection(object modelInstance, string modelMemberName, object viewInstance,
            string viewMemberName)
        {
            SetModel(modelInstance, modelMemberName);
            SetView(viewInstance, viewMemberName);
        }
        #endregion

        #region Model methods
        /// <summary>
        /// Установка объекта модели.
        /// </summary>
        /// <remarks>
        /// Предполагается что остальные параметры привязки со стороны объекта модели уже корректно настроены.
        /// </remarks>
        /// <param name="modelInstance">Экземпляр объекта модели.</param>
        public override void SetModel(object modelInstance)
        {
            ResetModel(modelInstance);
        }

        /// <summary>
        /// Установка объекта модели.
        /// </summary>
        /// <param name="modelInstance">Экземпляр объекта модели.</param>
        /// <param name="memberName">Имя члена объекта модели.</param>
        public override void SetModel(object modelInstance, string memberName)
        {
            ResetModel(modelInstance);

            _modelMember = SetMemberType(modelInstance, memberName, ref _modelMemberType)!;
            if (_modelMember != null)
            {
                _modelMemberName = memberName;
            }
        }

        /// <summary>
        /// Получение значения привязанного свойства/метода объекта модели.
        /// </summary>
        /// <remarks>
        /// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
        /// его запросить, например, во время присоединения
        /// </remarks>
        /// <returns>Значение привязанного свойства/метода объекта модели.</returns>
        public override object GetModelValue()
        {
            return _modelMember.GetMemberValue(_modelInstance)!;
        }

        /// <summary>
        /// Обновление данных объекта модели.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="args">Аргументы события.</param>
        protected override void UpdateModelProperty(object? sender, PropertyChangedEventArgs args)
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
                                _viewMember.SetMemberValue(_viewInstance, value?.ToString()!);
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

        #region View methods 
        /// <summary>
        /// Установка объекта представления.
        /// </summary>
        /// <remarks>
        /// Предполагается что остальные параметры привязки со стороны объекта представления уже корректно настроены.
        /// </remarks>
        /// <param name="viewInstance">Экземпляр объекта представления.</param>
        public override void SetView(object viewInstance)
        {
            ResetView(viewInstance);
        }

        /// <summary>
        /// Установка объекта представления.
        /// </summary>
        /// <param name="viewInstance">Экземпляр объекта представления.</param>
        /// <param name="memberName">Имя члена типа объекта представления.</param>
        public override void SetView(object viewInstance, string memberName)
        {
            ResetView(viewInstance);
            _viewMember = SetMemberType(viewInstance, memberName, ref _viewMemberType)!;
            if (_viewMember != null)
            {
                _viewMemberName = memberName;

                if (_viewMember.GetMemberType() == typeof(string))
                {
                    _isStringView = true;
                }
            }
        }

        /// <summary>
        /// Получение значения привязанного свойства/метода объекта представления.
        /// </summary>
        /// <remarks>
        /// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
        /// его запросить, например, во время присоединения
        /// </remarks>
        /// <returns>Значение привязанного свойства/метода объекта представления.</returns>
        public override object GetViewValue()
        {
            return _viewMember.GetMemberValue(_viewInstance)!;
        }

        /// <summary>
        /// Обновление данных объекта представления.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="args">Аргументы события.</param>
        protected override void UpdateViewProperty(object? sender, PropertyChangedEventArgs args)
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
    /**@}*/
}