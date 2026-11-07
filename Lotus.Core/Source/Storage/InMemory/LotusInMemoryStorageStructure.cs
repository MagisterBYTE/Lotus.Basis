using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lotus.Core
{
    /** \addtogroup CoreStorage
    *@{*/
    /// <summary>
    /// Интерфейс структуры in-memory хранилища на базе списков сущностей.
    /// </summary>
    public interface ILotusInMemoryStorageStructure : ILotusStorageStructure
    {
        /// <summary>
        /// Получить список сущностей указанного типа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Список сущностей.</returns>
        List<TEntity> GetEntitiesList<TEntity>() where TEntity : class;

        /// <summary>
        /// Зарегистрировать список сущностей указанного типа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="list">Список сущностей.</param>
        void Register<TEntity>(List<TEntity> list) where TEntity : class;
    }

    /// <summary>
    /// Структура in-memory хранилища с реестром списков сущностей.
    /// </summary>
    /// <remarks>
    /// Типизированные наследники могут объявить public-свойства <see cref="List{T}"/> —
    /// они будут зарегистрированы через <see cref="RegisterFromProperties"/>.
    /// </remarks>
    public class StorageStructureInMemory : ILotusInMemoryStorageStructure
    {
        #region Fields
        private readonly Dictionary<Type, object> _lists = new();
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public StorageStructureInMemory()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="registerFromProperties">
        /// Автоматически зарегистрировать public-свойства типа <see cref="List{T}"/>.
        /// </param>
        public StorageStructureInMemory(bool registerFromProperties)
        {
            if (registerFromProperties)
            {
                RegisterFromProperties();
            }
        }
        #endregion

        #region ILotusStorageStructure methods
        /// <inheritdoc/>
        public IQueryable<TEntity>? GetEntities<TEntity>() where TEntity : class, new()
        {
            if (_lists.TryGetValue(typeof(TEntity), out var boxed) && boxed is List<TEntity> list)
            {
                return list.AsQueryable();
            }

            return null;
        }
        #endregion

        #region ILotusInMemoryStorageStructure methods
        /// <inheritdoc/>
        public List<TEntity> GetEntitiesList<TEntity>() where TEntity : class
        {
            return GetOrCreateList<TEntity>();
        }

        /// <inheritdoc/>
        public void Register<TEntity>(List<TEntity> list) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(list, nameof(list));
            _lists[typeof(TEntity)] = list;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получить существующий список или создать новый и зарегистрировать его.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Список сущностей.</returns>
        public List<TEntity> GetOrCreateList<TEntity>() where TEntity : class
        {
            if (_lists.TryGetValue(typeof(TEntity), out var boxed) && boxed is List<TEntity> list)
            {
                return list;
            }

            list = [];
            _lists[typeof(TEntity)] = list;
            return list;
        }

        /// <summary>
        /// Проверить, зарегистрирован ли список для указанного типа.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <returns>Статус регистрации.</returns>
        public bool IsRegistered<TEntity>() where TEntity : class
        {
            return _lists.ContainsKey(typeof(TEntity));
        }

        /// <summary>
        /// Получить зарегистрированный список по типу сущности или null.
        /// </summary>
        /// <param name="entityType">Тип сущности.</param>
        /// <returns>Список или null.</returns>
        public IList? GetList(Type entityType)
        {
            XArgumentNull.ThrowIfNull(entityType, nameof(entityType));
            return _lists.TryGetValue(entityType, out var boxed) ? boxed as IList : null;
        }

        /// <summary>
        /// Получить зарегистрированный список по типу или создать пустой.
        /// </summary>
        /// <param name="entityType">Тип сущности.</param>
        /// <returns>Список.</returns>
        public IList GetOrCreateList(Type entityType)
        {
            XArgumentNull.ThrowIfNull(entityType, nameof(entityType));
            if (_lists.TryGetValue(entityType, out var boxed) && boxed is IList existing)
            {
                return existing;
            }

            var listType = typeof(List<>).MakeGenericType(entityType);
            var list = (IList)Activator.CreateInstance(listType)!;
            _lists[entityType] = list;
            return list;
        }

        /// <summary>
        /// Заменить содержимое зарегистрированного списка данными из другого списка.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="source">Источник данных.</param>
        public void ReplaceListContent<TEntity>(IEnumerable<TEntity> source) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(source, nameof(source));
            var list = GetOrCreateList<TEntity>();
            list.Clear();
            list.AddRange(source);
        }

        /// <summary>
        /// Зарегистрировать все public-свойства текущего экземпляра типа <see cref="List{T}"/>.
        /// </summary>
        public void RegisterFromProperties()
        {
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (!property.CanRead)
                {
                    continue;
                }

                var propertyType = property.PropertyType;
                if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(List<>))
                {
                    continue;
                }

                var entityType = propertyType.GetGenericArguments()[0];
                var value = property.GetValue(this);
                if (value is null)
                {
                    value = Activator.CreateInstance(propertyType)!;
                    if (property.CanWrite)
                    {
                        property.SetValue(this, value);
                    }
                }

                _lists[entityType] = value;
            }
        }

        /// <summary>
        /// Скопировать содержимое всех зарегистрированных списков из другой структуры того же типа.
        /// </summary>
        /// <param name="source">Источник.</param>
        public void CopyListsFrom(StorageStructureInMemory source)
        {
            XArgumentNull.ThrowIfNull(source, nameof(source));

            foreach (var pair in source._lists)
            {
                if (!_lists.TryGetValue(pair.Key, out var targetBoxed) || targetBoxed is not IList targetList)
                {
                    // Создаём/регистрируем список того же generic-типа
                    var listType = typeof(List<>).MakeGenericType(pair.Key);
                    targetList = (IList)Activator.CreateInstance(listType)!;
                    _lists[pair.Key] = targetList;
                }
                else
                {
                    targetList.Clear();
                }

                if (pair.Value is IList sourceList)
                {
                    foreach (var item in sourceList)
                    {
                        targetList.Add(item);
                    }
                }
            }
        }
        #endregion
    }
    /**@}*/
}
