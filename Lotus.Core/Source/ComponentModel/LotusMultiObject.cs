using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Lotus.Core
{
    /** 
     * \defgroup CoreComponentModel Подсистема компонентной модели
     * \ingroup Core
     * \brief Подсистема компонентной модели реализует типы и структуры данных для адаптации к пользовательскому интерфейсу.
     * @{
     */
    /// <summary>
    /// Представляет агрегатор для нескольких объектов, позволяющий редактировать их общие свойства одновременно и 
    /// собирать ошибки валидации.
    /// </summary>
    public class MultiObject : CustomTypeDescriptor, IDataErrorInfo
    {
        #region Fields
        /// <summary>
        /// Список целевых объектов, которыми управляет данный экземпляр.
        /// </summary>
        private readonly List<object> _targets;

        /// <summary>
        /// Реализация IDataErrorInfo. Возвращает общую ошибку по объекту. 
        /// В данной реализации всегда null.
        /// </summary>
        public string Error => string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Инициализирует новый экземпляр MultiObject на основе перечисления объектов.
        /// </summary>
        public MultiObject(IEnumerable<object> targets)
        {
            _targets = targets?.ToList() ?? [];
        }

        /// <summary>
        /// Инициализирует новый экземпляр MultiObject на основе списка (удобно для WinForms/WPF коллекций).
        /// </summary>
        public MultiObject(IList targets)
        {
            _targets = targets?.Cast<object>().ToList() ?? [];
        }
        #endregion

        /// <summary>
        /// Возвращает агрегированную строку ошибок для указанного свойства со всех целевых объектов.
        /// </summary>
        /// <param name="columnName">Имя проверяемого свойства.</param>
        public string this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();
                foreach (var target in _targets)
                {
                    if (target is IDataErrorInfo info)
                    {
                        var error = info[columnName];
                        if (!string.IsNullOrEmpty(error))
                            errors.AppendLine(error);
                    }
                }
                return errors.Length > 0 ? errors.ToString().Trim() : string.Empty;
            }
        }

        /// <summary>
        /// Динамически формирует список свойств, которые являются общими для всех объектов в списке.
        /// Свойства считаются идентичными, если совпадают их имена и типы данных.
        /// </summary>
        public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
        {
            if (_targets is null || !_targets.Any()) return PropertyDescriptorCollection.Empty;

            var first = _targets[0];
            var baseProps = TypeDescriptor.GetProperties(first, attributes).OfType<PropertyDescriptor>();

            // Фильтрация: оставляем только те свойства, которые присутствуют у каждого объекта в наборе
            var commonProps = baseProps.Where(p =>
                _targets.Skip(1).All(t =>
                    TypeDescriptor.GetProperties(t).Cast<PropertyDescriptor>()
                    .Any(tp => tp.Name == p.Name && tp.PropertyType == p.PropertyType)
                )
            );

            // Обертка свойств в MultiPropertyDescriptor для синхронного изменения всех объектов
            var result = commonProps.Select(p => new MultiPropertyDescriptor(p, _targets)).ToArray();
            return new PropertyDescriptorCollection(result);
        }

        /// <summary>
        /// Динамически формирует список свойств, которые являются общими для всех объектов в списке.
        /// Свойства считаются идентичными, если совпадают их имена и типы данных.
        /// </summary>
        public override PropertyDescriptorCollection GetProperties() => GetProperties(null);

        /// <summary>
        /// Указывает, что владельцем свойств является текущий объект-дескриптор.
        /// </summary>
        public override object GetPropertyOwner(PropertyDescriptor? pd) => this;
    }
    /**@}*/
}