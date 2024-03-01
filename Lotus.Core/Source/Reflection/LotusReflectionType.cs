using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreReflection
*@{*/
    /// <summary>
    /// Класс содержащий тип и его кэшированные данные рефлексии.
    /// </summary>
    /// <remarks>
    /// Реализация кэширование всех членов данных типа, с возможность быстрого поиска членов, проверкой на поддержку
    /// интерфейса и другими функциями.
    /// </remarks>
    public class CReflectedType : IDisposable
    {
        #region Const
        /// <summary>
        /// Флаги метаданных используемые при поиске полей.
        /// </summary>
        public const BindingFlags BINDING_FIELDS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Флаги метаданных используемые при поиске свойств.
        /// </summary>
        public const BindingFlags BINDING_PROPERTIES = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        /// <summary>
        /// Флаги метаданных используемые при поиске методов.
        /// </summary>
        public const BindingFlags BINDING_METHODS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        #endregion

        #region СТАТИСТИЧЕСКИЕ ДАННЫЕ 
        /// <summary>
        /// Массив списка аргументов для одного аргумента.
        /// </summary>
        public static readonly object?[] ArgList1 = new object[1];

        /// <summary>
        /// Массив списка аргументов для двух аргументов.
        /// </summary>
        public static readonly object?[] ArgList2 = new object[2];

        /// <summary>
        /// Массив списка аргументов для трех аргументов.
        /// </summary>
        public static readonly object?[] ArgList3 = new object[3];

        /// <summary>
        /// Массив списка аргументов для четырех аргументов.
        /// </summary>
        public static readonly object?[] ArgList4 = new object[4];
        #endregion

        #region Fields
        //Основные параметры
        protected internal Type _cachedType;
        protected internal Dictionary<string, FieldInfo> _fields;
        protected internal Dictionary<string, PropertyInfo> _properties;
        protected internal Dictionary<string, MethodInfo> _methods;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип - данные которого кэшированные.
        /// </summary>
        public Type CachedType
        {
            get { return _cachedType; }
        }

        /// <summary>
        /// Словарь метаданных полей по имени поля.
        /// </summary>
        public Dictionary<string, FieldInfo> Fields
        {
            get { return _fields; }
        }

        /// <summary>
        /// Словарь метаданных свойств по имени свойства.
        /// </summary>
        public Dictionary<string, PropertyInfo> Properties
        {
            get { return _properties; }
        }

        /// <summary>
        /// Словарь метаданных методов по имени метода.
        /// </summary>
        public Dictionary<string, MethodInfo> Methods
        {
            get { return _methods; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект указанным типом.
        /// </summary>
        /// <param name="cachedType">Тип данные которого будут сохранены.</param>
        public CReflectedType(Type cachedType)
        {
            _cachedType = cachedType;

            GetFields();
            GetProperties();
            GetMethods();
        }

        /// <summary>
        /// Конструктор инициализирует объект указанным типом.
        /// </summary>
        /// <param name="cachedType">Тип данные которого будут сохранены.</param>
        /// <param name="extractMembers">Объем извлекаемых данных для кэширования.</param>
        public CReflectedType(Type cachedType, TExtractMembers extractMembers)
        {
            _cachedType = cachedType;
            if (extractMembers.IsFlagSet(TExtractMembers.Fields))
            {
                GetFields();
            }
            if (extractMembers.IsFlagSet(TExtractMembers.Properties))
            {
                GetProperties();
            }
            if (extractMembers.IsFlagSet(TExtractMembers.Methods))
            {
                GetMethods();
            }
        }
        #endregion

        #region IDisposable methods
        /// <summary>
        /// Освобождение управляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение управляемых ресурсов.
        /// </summary>
        /// <param name="disposing">Статус освобождения.</param>
        private void Dispose(bool disposing)
        {
            // Освобождаем только управляемые ресурсы
            if (disposing)
            {
                if (_fields != null)
                {
                    _fields.Clear();
                }

                if (_properties != null)
                {
                    _properties.Clear();
                }

                if (_methods != null)
                {
                    _methods.Clear();
                }
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        #region Fields methods 
        /// <summary>
        /// Получение метаданных полей.
        /// </summary>
        private void GetFields()
        {
            if (_fields == null)
            {
                _fields = new Dictionary<string, FieldInfo>();

                var fields = _cachedType.GetFields(BINDING_FIELDS);

                foreach (var field in fields)
                {
                    _fields.Add(field.Name, field);
                }
            }
        }

        /// <summary>
        /// Проверка на существование поля с указанным именем.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Статус проверки.</returns>
        public bool ContainsField(string fieldName)
        {
            GetFields();
            return _fields!.ContainsKey(fieldName);
        }

        /// <summary>
        /// Получение метаданных поля по имени.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Метаданные поля или null если поля с таким именем не оказалось.</returns>
        public FieldInfo? GetField(string fieldName)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                return field_info;
            }

            return null;
        }

        /// <summary>
        /// Получение типа поля по имени.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Тип поля или null если поля с таким именем не оказалось.</returns>
        public Type? GetFieldType(string fieldName)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                return field_info.FieldType;
            }

            return null;
        }

        /// <summary>
        /// Получение имени типа поля по имени.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Имя типа поля или пустая строка если поля с таким именем не оказалось.</returns>
        public string GetFieldTypeName(string fieldName)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                return field_info.FieldType.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Получение списка метаданных полей имеющих указанный тип.
        /// </summary>
        /// <typeparam name="TType">Тип поля.</typeparam>
        /// <returns>Список метаданных полей.</returns>
        public List<FieldInfo> GetFieldsFromType<TType>()
        {
            var fields = new List<FieldInfo>();

            GetFields();
            foreach (var field_info in _fields.Values)
            {
                if (field_info.FieldType == typeof(TType))
                {
                    fields.Add(field_info);
                }
            }

            return fields;
        }

        /// <summary>
        /// Получение списка метаданных полей имеющих указанный атрибут.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <returns>Список метаданных полей.</returns>
        public List<FieldInfo> GetFieldsHasAttribute<TAttribute>() where TAttribute : Attribute
        {
            var fields = new List<FieldInfo>();

            GetFields();
            foreach (var field_info in _fields.Values)
            {
                if (Attribute.IsDefined(field_info, typeof(TAttribute)))
                {
                    fields.Add(field_info);
                }
            }

            return fields;
        }

        /// <summary>
        /// Получение атрибута указанного поля.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Атрибут поля или null.</returns>
        public TAttribute? GetAttributeFromField<TAttribute>(string fieldName) where TAttribute : System.Attribute
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                if (Attribute.IsDefined(field_info, typeof(TAttribute)))
                {
                    return Attribute.GetCustomAttribute(field_info, typeof(TAttribute)) as TAttribute;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <returns>Значение поля.</returns>
        public object? GetFieldValue(string fieldName, object? instance)
        {
            GetFields();
            if (_fields!.TryGetValue(fieldName, out var field_info))
            {
                if (field_info.IsStatic)
                {
                    return field_info.GetValue(null);
                }
                else
                {
                    return field_info.GetValue(instance);
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="fieldInfoResult">Метаданные поля.</param>
        /// <returns>Значение поля.</returns>
        public object? GetFieldValue(string fieldName, object instance, out FieldInfo? fieldInfoResult)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                fieldInfoResult = field_info;
                if (field_info.IsStatic)
                {
                    return field_info.GetValue(null);
                }
                else
                {
                    return field_info.GetValue(instance);
                }
            }

            fieldInfoResult = null;
            return null;
        }

        /// <summary>
        /// Получение значения указанного поля которое представляет собой коллекцию.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Значение поля.</returns>
        public object? GetFieldValue(string fieldName, object instance, int index)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                object? collection = null;
                if (field_info.IsStatic)
                {
                    collection = field_info.GetValue(null);
                }
                else
                {
                    collection = field_info.GetValue(instance);
                }

                if (collection != null)
                {
                    if (collection is IList list)
                    {
                        return list[index];
                    }
                    else
                    {
                        if (collection is IEnumerable enumerable)
                        {
                            var enumeration = enumerable.GetEnumerator();

                            for (var i = 0; i <= index; i++)
                            {
                                if (!enumeration.MoveNext())
                                {
                                    return null;
                                }
                            }
                            return enumeration.Current;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения указанного поля которое представляет собой коллекцию.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <param name="fieldInfoResult">Метаданные поля.</param>
        /// <returns>Значение поля.</returns>
        public object? GetFieldValue(string fieldName, object instance, int index, out FieldInfo? fieldInfoResult)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                fieldInfoResult = field_info;
                object? collection = null;
                if (field_info.IsStatic)
                {
                    collection = field_info.GetValue(null);
                }
                else
                {
                    collection = field_info.GetValue(instance);
                }

                if (collection != null)
                {
                    if (collection is IList list)
                    {
                        return list[index];
                    }
                    else
                    {
                        if (collection is IEnumerable enumerable)
                        {
                            var enumeration = enumerable.GetEnumerator();

                            for (var i = 0; i <= index; i++)
                            {
                                if (!enumeration.MoveNext())
                                {
                                    return null;
                                }
                            }
                            return enumeration.Current;
                        }
                    }
                }
            }

            fieldInfoResult = null;
            return null;
        }

        /// <summary>
        /// Установка значения указанного поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="value">Значение поля.</param>
        /// <returns> Статус успешности установки поля.</returns>
        public bool SetFieldValue(string fieldName, object? instance, object value)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                if (field_info.IsStatic)
                {
                    field_info.SetValue(null, value);
                }
                else
                {
                    field_info.SetValue(instance, value);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Установка значения указанного поля которое представляет собой коллекцию.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="value">Значение поля.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <returns> Статус успешности установки поля.</returns>
        public bool SetFieldValue(string fieldName, object? instance, object value, int index)
        {
            GetFields();
            if (_fields.TryGetValue(fieldName, out var field_info))
            {
                IList? list = null;
                if (field_info.IsStatic)
                {
                    list = field_info.GetValue(null) as IList;
                }
                else
                {
                    list = field_info.GetValue(instance) as IList;
                }

                if (list != null)
                {
                    list[index] = value;
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Properties methods
        /// <summary>
        /// Получение метаданных свойств.
        /// </summary>
        private void GetProperties()
        {
            if (_properties == null)
            {
                _properties = new Dictionary<string, PropertyInfo>();

                var properties = _cachedType.GetProperties(BINDING_PROPERTIES);

                foreach (var property in properties)
                {
                    _properties.Add(property.Name, property);
                }
            }
        }

        /// <summary>
        /// Проверка на существование свойства с указанным именем.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Статус проверки.</returns>
        public bool ContainsProperty(string propertyName)
        {
            GetProperties();
            return _properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// Получение метаданных свойства по имени.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Метаданные свойства или null если свойства с таким именем не оказалось.</returns>
        public PropertyInfo? GetProperty(string propertyName)
        {
            GetProperties();

            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                return property_info;
            }

            return null;
        }

        /// <summary>
        /// Получение типа свойства по имени.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Тип свойства или null если свойства с таким именем не оказалось.</returns>
        public Type? GetPropertyType(string propertyName)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                return property_info.PropertyType;
            }

            return null;
        }

        /// <summary>
        /// Получение имени типа свойства по имени.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Имя типа свойства или пустая строка если свойства с таким именем не оказалось.</returns>
        public string GetPropertyTypeName(string propertyName)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                return property_info.PropertyType.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Получение списка метаданных свойств имеющих указанный тип.
        /// </summary>
        /// <typeparam name="TType">Тип свойства.</typeparam>
        /// <returns>Список метаданных свойств.</returns>
        public List<PropertyInfo> GetPropertiesFromType<TType>()
        {
            var properties = new List<PropertyInfo>();
            GetProperties();
            foreach (var property_info in _properties.Values)
            {
                if (property_info.PropertyType == typeof(TType))
                {
                    properties.Add(property_info);
                }
            }

            return properties;
        }

        /// <summary>
        /// Получение списка метаданных свойств имеющих указанный атрибут.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <returns>Список метаданных свойств.</returns>
        public List<PropertyInfo> GetPropertiesHasAttribute<TAttribute>() where TAttribute : Attribute
        {
            var properties = new List<PropertyInfo>();

            GetProperties();
            foreach (var property_info in _properties.Values)
            {
                if (Attribute.IsDefined(property_info, typeof(TAttribute)))
                {
                    properties.Add(property_info);
                }
            }

            return properties;
        }

        /// <summary>
        /// Получение атрибута указанного свойства.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns>Атрибут поля или null.</returns>
        public TAttribute? GetAttributeFromProperty<TAttribute>(string propertyName) where TAttribute : System.Attribute
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                if (Attribute.IsDefined(property_info, typeof(TAttribute)))
                {
                    return Attribute.GetCustomAttribute(property_info, typeof(TAttribute)) as TAttribute;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение значения указанного свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <returns>Значение свойства.</returns>
        public object? GetPropertyValue(string propertyName, object? instance)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                return property_info.GetValue(instance);
            }

            return null;
        }

        /// <summary>
        /// Получение значения указанного свойства которое представляет собой коллекцию.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <returns>Значение свойства.</returns>
        public object? GetPropertyValue(string propertyName, object instance, int index)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                var collection = property_info.GetValue(instance);

                if (collection != null)
                {
                    if (collection is IList list)
                    {
                        return list[index];
                    }
                    else
                    {
                        if (collection is IEnumerable enumerable)
                        {
                            var enumeration = enumerable.GetEnumerator();

                            for (var i = 0; i <= index; i++)
                            {
                                if (!enumeration.MoveNext())
                                {
                                    return null;
                                }
                            }
                            return enumeration.Current;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Установка значения указанного свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="value">Значение свойства.</param>
        /// <returns> Статус успешности установки свойства.</returns>
        public bool SetPropertyValue(string propertyName, object instance, object value)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                property_info.SetValue(instance, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Установка значения указанного свойства которое представляет собой коллекцию.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="index">Индекс элемента.</param>
        /// <returns> Статус успешности установки свойства.</returns>
        public bool SetPropertyValue(string propertyName, object instance, object value, int index)
        {
            GetProperties();
            if (_properties.TryGetValue(propertyName, out var property_info))
            {
                var list = property_info.GetValue(instance) as IList;
                if (list != null)
                {
                    list[index] = value;
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Methods methods
        /// <summary>
        /// Получение метаданных метод.
        /// </summary>
        private void GetMethods()
        {
            if (_methods == null)
            {
                _methods = new Dictionary<string, MethodInfo>();

                foreach (var method in _cachedType.GetMethods(BINDING_METHODS))
                {
                    if (_methods.ContainsKey(method.Name) == false)
                    {
                        _methods.Add(method.Name, method);
                    }
                }
            }
        }

        /// <summary>
        /// Проверка на существование метода с указанным именем.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <returns>Статус проверки.</returns>
        public bool ContainsMethod(string methodName)
        {
            GetMethods();
            return _methods.ContainsKey(methodName);
        }

        /// <summary>
        /// Получение метаданных метода по имени.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <returns>Метаданные метода или null если метода с таким именем не оказалось.</returns>
        public MethodInfo? GetMethod(string methodName)
        {
            GetMethods();
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                return method_info;
            }

            return null;
        }

        /// <summary>
        /// Получение типа возвращаемого значения метода по имени.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <returns>Тип возвращаемого значения метода или null если метода с таким именем не оказалось.</returns>
        public Type? GetMethodReturnType(string methodName)
        {
            GetMethods();
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                return method_info.ReturnType;
            }

            return null;
        }

        /// <summary>
        /// Получение имени типа возвращаемого значения метода по имени.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <returns>Имя типа возвращаемого значения метода или пустая строка если метода с таким именем не оказалось.</returns>
        public string GetMethodReturnTypeName(string methodName)
        {
            GetMethods();
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                return method_info.ReturnType.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Получение списка метаданных методов имеющих указанный атрибут.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <returns>Список метаданных методов.</returns>
        public List<MethodInfo> GetMethodsHasAttribute<TAttribute>() where TAttribute : Attribute
        {
            var methods = new List<MethodInfo>();

            GetMethods();
            foreach (var method_info in _methods.Values)
            {
                if (Attribute.IsDefined(method_info, typeof(TAttribute)))
                {
                    methods.Add(method_info);
                }
            }

            return methods;
        }

        /// <summary>
        /// Получение атрибута указанного метода.
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
        /// <param name="methodName">Имя метода.</param>
        /// <returns>Атрибут метода или null.</returns>
        public TAttribute? GetAttributeFromMethod<TAttribute>(string methodName) where TAttribute : System.Attribute
        {
            GetMethods();
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                if (Attribute.IsDefined(method_info, typeof(TAttribute)))
                {
                    return Attribute.GetCustomAttribute(method_info, typeof(TAttribute)) as TAttribute;
                }
            }

            return null;
        }

        /// <summary>
        /// Вызов указанного метода.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <returns>Значение метода.</returns>
        public object? InvokeMethod(string methodName, object? instance)
        {
            GetMethods();
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                if (method_info.IsStatic)
                {
                    return method_info.Invoke(null, null);
                }
                else
                {
                    return method_info.Invoke(instance, null);
                }
            }

            return null;
        }

        /// <summary>
        /// Вызов указанного метода с одним аргументом.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="arg">Аргумент метода.</param>
        /// <returns>Значение метода.</returns>
        public object? InvokeMethod(string methodName, object? instance, object? arg)
        {
            GetMethods();
            ArgList1[0] = arg;
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                if (method_info.IsStatic)
                {
                    return method_info.Invoke(null, ArgList1);
                }
                else
                {
                    return method_info.Invoke(instance, ArgList1);
                }
            }

            return null;
        }

        /// <summary>
        /// Вызов указанного метода с двумя аргументами.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="arg1">Первый аргумент метода.</param>
        /// <param name="arg2">Второй аргумент метода.</param>
        /// <returns>Значение метода.</returns>
        public object? InvokeMethod(string methodName, object? instance, object? arg1,
                object? arg2)
        {
            GetMethods();
            ArgList2[0] = arg1;
            ArgList2[1] = arg2;
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                if (method_info.IsStatic)
                {
                    return method_info.Invoke(null, ArgList2);
                }
                else
                {
                    return method_info.Invoke(instance, ArgList2);
                }
            }

            return null;
        }

        /// <summary>
        /// Вызов указанного метода с тремя аргументами.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        /// <param name="instance">Экземпляр объекта.</param>
        /// <param name="arg1">Первый аргумент метода.</param>
        /// <param name="arg2">Второй аргумент метода.</param>
        /// <param name="arg3">Третий аргумент метода.</param>
        /// <returns>Значение метода.</returns>
        public object? InvokeMethod(string methodName, object? instance, object? arg1,
                object? arg2, object? arg3)
        {
            GetMethods();
            ArgList3[0] = arg1;
            ArgList3[1] = arg2;
            ArgList3[3] = arg3;
            if (_methods.TryGetValue(methodName, out var method_info))
            {
                if (method_info.IsStatic)
                {
                    return method_info.Invoke(null, ArgList3);
                }
                else
                {
                    return method_info.Invoke(instance, ArgList3);
                }
            }

            return null;
        }
        #endregion
    }
    /**@}*/
}