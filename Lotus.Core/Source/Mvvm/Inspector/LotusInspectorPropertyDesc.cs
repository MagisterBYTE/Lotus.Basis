using System;
using System.Reflection;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspector
	*@{*/
    /// <summary>
    /// Описание свойства объекта для его актуального описания, отображения и редактирования пользователем.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Рассмотрим подробнее для чего нужна отдельная структура описания свойства/поля объекта
    /// </para>
    /// <para>
    /// При определении полей и свойств типов для них определяются соответствующие атрибуты описания, 
    /// если этот объект предусматривает редактирование со стороны пользователя.
    /// </para>
    /// <para>
    /// При последующем наследовании от этих типов изменяется соответственно и функциональное(предметное) назначение
    /// производного типа и очень часто требуется уточнить описание соответствующего поля/свойства, либо скрыть его,
    /// однако повторно переопределить атрибуты уже невозможно.
    /// </para>
    /// <para>
    /// Исходя из этого данная структура позволяет задать описании поля/свойства для каждого конкретного типа
    /// в иерархии и даже частично менять механизм их редактирования.
    /// </para>
    /// </remarks>
    public class CPropertyDesc : IComparable<CPropertyDesc>
    {
        #region Static methods
        /// <summary>
        /// Создание/переопределение отображаемого имя свойства и его описания.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="displayName">Отображаемое имя свойства.</param>
        /// <param name="description">Описание свойства.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideDisplayNameAndDescription<TType>(string propertyName, string displayName,
                string? description = null)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.DisplayName = displayName;
            property_desc.Description = description ?? string.Empty;

            return property_desc;
        }

        /// <summary>
        /// Создание/переопределение категории свойства.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="category">Категория.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideCategory<TType>(string propertyName, string category)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.Category = category;

            return property_desc;
        }

        /// <summary>
        /// Создание/переопределение скрытия свойства.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideHide<TType>(string propertyName)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.IsHideInspector = true;

            return property_desc;
        }

        /// <summary>
        /// Создание/переопределение порядка отображения свойства.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="order">Порядковый номер отображения свойства в группе.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideOrder<TType>(string propertyName, int order)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.PropertyOrder = order;

            return property_desc;
        }

        /// <summary>
        /// Создание/переопределение значения по умолчанию свойства.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="defaultValue">Значение свойства по умолчанию.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideDefaultValue<TType>(string propertyName, object defaultValue)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.DefaultValue = defaultValue;

            return property_desc;
        }

        /// <summary>
        /// Создание/переопределение значения списка допустимых значений свойства.
        /// </summary>
        /// <typeparam name="TType">Тип.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="listValues">Список допустимых значений свойства.</param>
        /// <returns>Описатель свойств.</returns>
        public static CPropertyDesc OverrideListValues<TType>(string propertyName, object listValues)
        {
            var property_desc = new CPropertyDesc();

            // Проверяем
            if (typeof(TType).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public) == null)
            {
                XLogger.LogErrorFormat("Не найдено свойство <{0}> в типе <{1}>", propertyName, typeof(TType).Name);
            }

            property_desc.PropertyName = propertyName;
            property_desc.ListValues = listValues;

            return property_desc;
        }
        #endregion

        #region ПОЛУЧЕНИЯ methods
        /// <summary>
        /// Получение значения из декларированного значения в различных формах.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Различные значения которые используются при дополнительном управлении свойствами и полями объекта в инспекторе 
        /// свойств можно задать различными способами.
        /// </para>
        /// <para>
        /// В атрибутах можно задать только константные выражения, в описание свойства можно задать статические выражения, 
        /// поэтому декларированное значении должно интерпретироваться различным способом в зависимости от формы его задания.
        /// </para>
        /// <para>
        /// Данный метод анализирует все доступные формы задания декларированное значении и получает конкретное значение.
        /// </para>
        /// </remarks>
        /// <param name="declareValue">Декларированное значение.</param>
        /// <param name="memberName">Имя члена объекта/типа.</param>
        /// <param name="memberType">Тип члена объект/типа.</param>
        /// <param name="instance"></param>
        /// <returns>Экземпляр объекта.</returns>
        public static object? GetValue(object declareValue, string memberName, TInspectorMemberType memberType,
                object? instance = null)
        {
            object? result = null;

            // Проверяем непосредственного значение
            if (declareValue != null)
            {
                // 1) Задан как статические данные
                if (declareValue is Type type && memberName.IsExists())
                {
                    // Получаем тип
                    switch (memberType)
                    {
                        case TInspectorMemberType.Field:
                            {
                                result = type.GetStaticFieldValue<object>(memberName);
                                return result;
                            }
                        case TInspectorMemberType.Property:
                            {
                                result = type.GetStaticPropertyValue<object>(memberName);
                                return result;
                            }
                        case TInspectorMemberType.Method:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // 2) Это может быть метаданные поля 
                    if (declareValue is FieldInfo fieldInfo)
                    {
                        if (fieldInfo.IsStatic)
                        {
                            result = fieldInfo.GetValue(null);
                        }
                        else
                        {
                            result = fieldInfo.GetValue(instance);
                        }

                        return result;
                    }

                    // 3) Это может быть метаданные свойства
                    if (declareValue is PropertyInfo propertyInfo)
                    {
                        if (propertyInfo.IsStatic())
                        {
                            result = propertyInfo.GetValue(null, null);
                        }
                        else
                        {
                            result = propertyInfo.GetValue(instance, null);
                        }

                        return result;
                    }

                    // 4) Декларированное значение и есть значение
                    result = declareValue;
                    return result;
                }
            }
            else
            {
                // У нас есть только строковые данные
                // Если задана в строки имя типа и его член данных
                if (memberName.IndexOf(XChar.Dot) > -1)
                {
                    result = XReflection.GetStaticDataFromType(memberName);
                    return result;
                }
                else
                {
                    // Задано только имя члена данных, используем экземпляр объекта
                    if (instance == null) return result;

                    switch (memberType)
                    {
                        case TInspectorMemberType.Field:
                            {
                                result = XReflection.GetFieldValue(instance, memberName);
                            }
                            break;
                        case TInspectorMemberType.Property:
                            {
                                result = XReflection.GetPropertyValue(instance, memberName);
                            }
                            break;
                        case TInspectorMemberType.Method:
                            {
                                result = XReflection.InvokeMethod(instance, memberName);
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            return result;
        }
        #endregion

        #region Fields
        // Основные параметры
        protected internal string _propertyName;

        // Параметры описания
        protected internal string _displayName;
        protected internal string _description;
        protected internal int _propertyOrder = -1;
        protected internal string _category;
        protected internal int _categoryOrder = -1;

        // Параметры управления
        protected internal bool _isHideInspector;
        protected internal bool _isReadOnly;
        protected internal object _defaultValue;
        protected internal object _listValues;
        #endregion

        #region Properties
        //
        // ПАРАМЕТРЫ ОПИСАНИЯ
        //
        /// <summary>
        /// Имя свойства с которым связано данное описание.
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
            }
        }

        //
        // ПАРАМЕТРЫ ОПИСАНИЯ
        //
        /// <summary>
        /// Отображаемое имя свойства.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        /// <summary>
        /// Описание свойства.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Порядковый номер отображения свойства внутри категории.
        /// </summary>
        public int PropertyOrder
        {
            get { return _propertyOrder; }
            set
            {
                _propertyOrder = value;
            }
        }

        /// <summary>
        /// Категория свойства.
        /// </summary>
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
            }
        }

        /// <summary>
        /// Порядковый номер отображения категории.
        /// </summary>
        public int CategoryOrder
        {
            get { return _categoryOrder; }
            set
            {
                _categoryOrder = value;
            }
        }

        //
        // ПАРАМЕТРЫ УПРАВЛЕНИЯ
        //
        /// <summary>
        /// Свойство скрыто для отображения в инспекторе свойств.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
            }
        }

        /// <summary>
        /// Свойство только для чтения.
        /// </summary>
        public bool IsHideInspector
        {
            get { return _isHideInspector; }
            set
            {
                _isHideInspector = value;
            }
        }

        /// <summary>
        /// Значение свойства по умолчанию.
        /// </summary>
        public object DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
            }
        }

        /// <summary>
        /// Список допустимых значений свойств.
        /// </summary>
        public object ListValues
        {
            get { return _listValues; }
            set
            {
                _listValues = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CPropertyDesc()
        {
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение объектов для упорядочивания.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус сравнения объектов.</returns>
        public int CompareTo(CPropertyDesc? other)
        {
            if (other == null) return 0;

            return DisplayName.CompareTo(other.DisplayName);
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Отображаемое имя свойства.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(_category))
            {
                return DisplayName;
            }
            else
            {
                return _category + "=" + DisplayName;
            }
        }
        #endregion
    }
    /**@}*/
}