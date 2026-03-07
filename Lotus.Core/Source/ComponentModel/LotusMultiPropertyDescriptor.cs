using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreComponentModel
	*@{*/
    /// <summary>
    /// Дескриптор свойства, предназначенный для одновременного управления значением одного и того же свойства в группе объектов.
    /// </summary>
    public class MultiPropertyDescriptor : PropertyDescriptor
    {
        #region Fields
        /// <summary>
        /// Базовый дескриптор свойства первого объекта.
        /// </summary>
        private readonly PropertyDescriptor _baseProp;

        /// <summary>
        /// Список целевых объектов, к которым применяется данное свойство.
        /// </summary>
        private readonly List<object> _targets;
        #endregion

        #region Override properties
        /// <summary>
        /// Возвращает тип компонента, которому принадлежит это свойство. 
        /// Используется <see cref="MultiObject"/> для корректной работы с PropertyGrid.
        /// </summary>
        public override Type ComponentType => typeof(MultiObject);

        /// <summary>
        /// Указывает, является ли свойство доступным только для чтения.
        /// </summary>
        public override bool IsReadOnly => _baseProp.IsReadOnly;

        /// <summary>
        /// Возвращает тип данных свойства.
        /// </summary>
        public override Type PropertyType => _baseProp.PropertyType;

        /// <summary>
        /// Возвращает преобразователь типов. Если свойство является Enum, 
        /// возвращает кастомный <see cref="EnumDescriptionConverter"/>.
        /// </summary>
        public override TypeConverter Converter
        {
            get
            {
                if (_baseProp.PropertyType.IsEnum)
                {
                    return new EnumDescriptionConverter(_baseProp.PropertyType);
                }
                return base.Converter;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MultiPropertyDescriptor"/>.
        /// </summary>
        /// <param name="baseProp">Базовый дескриптор свойства для копирования атрибутов и метаданных.</param>
        /// <param name="targets">Список объектов, значения которых будут синхронизироваться.</param>
        public MultiPropertyDescriptor(PropertyDescriptor baseProp, List<object> targets)
            : base(baseProp.Name, baseProp.Attributes.Cast<Attribute>().ToArray())
        {
            _baseProp = baseProp;
            _targets = targets;
        }
        #endregion

        #region Override methods
        /// <summary>
        /// Возвращает значение свойства. Если у всех объектов значения одинаковы — возвращает это значение, иначе возвращает null.
        /// </summary>
        /// <param name="component">Игнорируется, так как используются внутренние цели <see cref="_targets"/>.</param>
        /// <returns>Общее значение или null.</returns>
        public override object? GetValue(object? component)
        {
            var firstValue = _baseProp.GetValue(_targets[0]);
            foreach (var target in _targets.Skip(1))
            {
                var properties = TypeDescriptor.GetProperties(target)!;
                var currentVal = properties[_baseProp.Name]?.GetValue(target);

                if (!Equals(firstValue, currentVal))
                {
                    return null;
                }
            }
            return firstValue;
        }

        /// <summary>
        /// Устанавливает новое значение свойства для всех целевых объектов.
        /// </summary>
        /// <param name="component">Компонент, инициирующий изменение.</param>
        /// <param name="value">Новое значение для установки.</param>
        public override void SetValue(object? component, object? value)
        {
            foreach (var target in _targets)
            {
                var properties = TypeDescriptor.GetProperties(target)!;
                var property = properties[_baseProp.Name];
                if (property is not null && !property.IsReadOnly)
                {
                    property.SetValue(target, value);
                }
            }
            OnValueChanged(component, EventArgs.Empty);
        }

        /// <summary>
        /// Определяет, можно ли сбросить значение свойства к состоянию по умолчанию.
        /// </summary>
        /// <param name="component">Объект для проверки.</param>
        public override bool CanResetValue(object component)
        {
            return _targets.Any() && _baseProp.CanResetValue(_targets[0]);
        }

        /// <summary>
        /// Сбрасывает значение свойства ко всем целевым объектам.
        /// </summary>
        /// <param name="component">Объект, на котором производится сброс.</param>
        public override void ResetValue(object component)
        {
            _targets.ForEach(t => _baseProp.ResetValue(t));
        }

        /// <summary>
        /// Определяет, должно ли значение свойства сериализоваться.
        /// </summary>
        /// <param name="component">Проверяемый объект.</param>
        /// <returns>Всегда false для исключения дублирования данных.</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        #endregion
    }
    /**@}*/
}