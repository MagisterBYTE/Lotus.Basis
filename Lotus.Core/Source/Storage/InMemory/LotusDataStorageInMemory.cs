using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Core
{
    /** \addtogroup CoreStorage
    *@{*/
    /// <summary>
    /// In-memory реализация <see cref="ILotusDataStorage"/> на базе списков сущностей.
    /// </summary>
    public class DataStorageInMemory : PropertyChangedBase, ILotusDataStorage
    {
        #region Fields
        protected internal StorageStructureInMemory _structure;
        protected internal string _connectingData = string.Empty;
        protected internal bool _needSaved;
        protected internal int _pendingChanges;
        #endregion

        #region Properties
        /// <inheritdoc/>
        public string ConnectingData
        {
            get => _connectingData;
            set
            {
                _connectingData = value ?? string.Empty;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public bool NeedSaved
        {
            get => _needSaved;
            set
            {
                _needSaved = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public ILotusStorageStructure IStructure => _structure;

        /// <summary>
        /// Типизированная структура in-memory хранилища.
        /// </summary>
        public StorageStructureInMemory Structure => _structure;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="structure">Структура хранилища.</param>
        public DataStorageInMemory(StorageStructureInMemory structure)
        {
            _structure = structure ?? throw new ArgumentNullException(nameof(structure));
        }

        /// <summary>
        /// Конструктор по умолчанию создаёт пустую структуру.
        /// </summary>
        public DataStorageInMemory()
            : this(new StorageStructureInMemory())
        {
        }
        #endregion

        #region ILotusStorage methods
        /// <inheritdoc/>
        public virtual ILotusRepository<TEntity, TKey>? GetRepository<TEntity, TKey>()
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
        {
            var list = _structure.GetOrCreateList<TEntity>();
            return new RepositoryList<TEntity, TKey>(list);
        }

        /// <inheritdoc/>
        public virtual int SaveChanges()
        {
            var count = _pendingChanges;
            _pendingChanges = 0;
            NeedSaved = false;
            return count;
        }

        /// <inheritdoc/>
        public virtual ValueTask<int> SaveChangesAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(SaveChanges());
        }
        #endregion

        #region Mapping
        /// <summary>
        /// Преобразование сущности в тип ответа.
        /// </summary>
        /// <remarks>
        /// По умолчанию поддерживается только случай <typeparamref name="TEntity"/> == <typeparamref name="TResponse"/>.
        /// Конкретные реализации должны переопределить метод для маппинга в DTO.
        /// </remarks>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TResponse">Тип ответа.</typeparam>
        /// <param name="entity">Сущность.</param>
        /// <returns>Объект ответа.</returns>
        protected virtual TResponse MapToResponse<TEntity, TResponse>(TEntity entity)
            where TEntity : class
            where TResponse : class
        {
            if (entity is TResponse same)
            {
                return same;
            }

            throw new NotSupportedException(
                $"Override {nameof(MapToResponse)} to map {typeof(TEntity).Name} -> {typeof(TResponse).Name}.");
        }
        #endregion

        #region Relationships
        /// <summary>
        /// Восстановить навигационные связи по внешним ключам.
        /// </summary>
        /// <remarks>
        /// Вызывается после <c>Load</c> у файлового хранилища.
        /// Переопределите в наследнике, чтобы явно заполнить navigation (User.Role и т.п.).
        /// Базовая реализация пустая — связи не угадываются автоматически для всего графа.
        /// </remarks>
        protected virtual void FixupRelationships()
        {
        }

        /// <summary>
        /// Публичный вызов <see cref="FixupRelationships"/>.
        /// </summary>
        public void Fixup()
        {
            FixupRelationships();
        }

        /// <summary>
        /// Гидратация одной навигации для всех сущностей типа <typeparamref name="TEntity"/> в памяти.
        /// </summary>
        /// <remarks>
        /// Конвенции:
        /// <list type="bullet">
        /// <item>Ссылка: свойство <c>Role</c> + FK <c>RoleId</c> на текущей сущности.</item>
        /// <item>Коллекция: свойство <c>Users</c> типа <c>List&lt;User&gt;</c> + FK <c>{EntityName}Id</c> на связанной сущности (например <c>RoleId</c>).</item>
        /// </list>
        /// </remarks>
        protected virtual void HydrateNavigation<TEntity>(PropertyInfo navigationProperty)
            where TEntity : class
        {
            XArgumentNull.ThrowIfNull(navigationProperty, nameof(navigationProperty));

            var collectionItemType = navigationProperty.PropertyType.GetClassicCollectionItemType();
            if (collectionItemType is not null)
            {
                HydrateCollectionNavigation<TEntity>(navigationProperty, collectionItemType);
                return;
            }

            HydrateReferenceNavigation<TEntity>(navigationProperty);
        }

        /// <summary>
        /// Гидратация ссылочной навигации (N:1 / 1:1), например <c>User.Role</c>.
        /// </summary>
        protected virtual void HydrateReferenceNavigation<TEntity>(PropertyInfo navigationProperty)
            where TEntity : class
        {
            var relatedType = navigationProperty.PropertyType;
            if (!relatedType.IsClass || relatedType == typeof(string))
            {
                return;
            }

            var fkProperty = typeof(TEntity).GetProperty(navigationProperty.Name + "Id",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            var relatedIdProperty = relatedType.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (fkProperty is null || relatedIdProperty is null)
            {
                return;
            }

            var relatedList = _structure.GetList(relatedType);
            if (relatedList is null || relatedList.Count == 0)
            {
                foreach (var entity in _structure.GetOrCreateList<TEntity>())
                {
                    navigationProperty.SetValue(entity, null);
                }

                return;
            }

            var relatedById = new Dictionary<object, object>();
            foreach (var related in relatedList)
            {
                if (related is null)
                {
                    continue;
                }

                var id = relatedIdProperty.GetValue(related);
                if (id is not null)
                {
                    relatedById[id] = related;
                }
            }

            foreach (var entity in _structure.GetOrCreateList<TEntity>())
            {
                var fkValue = fkProperty.GetValue(entity);
                if (fkValue is null)
                {
                    navigationProperty.SetValue(entity, null);
                    continue;
                }

                relatedById.TryGetValue(fkValue, out var related);
                navigationProperty.SetValue(entity, related);
            }
        }

        /// <summary>
        /// Гидратация коллекционной навигации (1:N), например <c>Role.Users</c>.
        /// </summary>
        protected virtual void HydrateCollectionNavigation<TEntity>(PropertyInfo navigationProperty, Type relatedType)
            where TEntity : class
        {
            var entityIdProperty = typeof(TEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            var fkProperty = FindForeignKeyProperty(relatedType, typeof(TEntity));
            if (entityIdProperty is null || fkProperty is null)
            {
                return;
            }

            var relatedList = _structure.GetList(relatedType);
            if (relatedList is null)
            {
                relatedList = Array.Empty<object>();
            }

            foreach (var entity in _structure.GetOrCreateList<TEntity>())
            {
                var entityId = entityIdProperty.GetValue(entity);
                var children = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(relatedType))!;

                if (entityId is not null)
                {
                    foreach (var related in relatedList)
                    {
                        if (related is null)
                        {
                            continue;
                        }

                        var fkValue = fkProperty.GetValue(related);
                        if (fkValue is not null && fkValue.Equals(entityId))
                        {
                            children.Add(related);
                        }
                    }
                }

                navigationProperty.SetValue(entity, children);
            }
        }

        /// <summary>
        /// Найти FK-свойство на <paramref name="dependentType"/>, указывающее на <paramref name="principalType"/>.
        /// </summary>
        /// <remarks>
        /// Пробует <c>{PrincipalName}Id</c> и вариант без суффикса <c>Entity</c>
        /// (например <c>RoleEntity</c> → <c>RoleId</c>).
        /// </remarks>
        protected static PropertyInfo? FindForeignKeyProperty(Type dependentType, Type principalType)
        {
            var candidates = new List<string> { principalType.Name + "Id" };
            const string entitySuffix = "Entity";
            if (principalType.Name.EndsWith(entitySuffix, StringComparison.Ordinal)
                && principalType.Name.Length > entitySuffix.Length)
            {
                candidates.Add(principalType.Name[..^entitySuffix.Length] + "Id");
            }

            foreach (var name in candidates)
            {
                var property = dependentType.GetProperty(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property is not null)
                {
                    return property;
                }
            }

            return null;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Пометить хранилище как изменённое.
        /// </summary>
        /// <param name="changeCount">Количество изменений.</param>
        protected void MarkChanged(int changeCount = 1)
        {
            if (changeCount > 0)
            {
                _pendingChanges += changeCount;
            }

            NeedSaved = true;
        }

        /// <summary>
        /// Найти индекс сущности по идентификатору.
        /// </summary>
        protected static int FindIndexById<TEntity, TKey>(List<TEntity> list, TKey id)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] is ILotusIdentifierId<TKey> identifiable && identifiable.Id.Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Найти сущность по идентификатору.
        /// </summary>
        protected static TEntity? FindById<TEntity, TKey>(List<TEntity> list, TKey id)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>
        {
            var index = FindIndexById<TEntity, TKey>(list, id);
            return index >= 0 ? list[index] : null;
        }
        #endregion

        #region ILotusDataStorage methods
        /// <inheritdoc/>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _structure.GetOrCreateList<TEntity>().AsQueryable();
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> Query<TEntity>(Action<ILotusIncludeBuilder<TEntity>> include)
            where TEntity : class
        {
            XArgumentNull.ThrowIfNull(include, nameof(include));
            var builder = new InMemoryIncludeBuilder<TEntity>(this);
            include(builder);
            builder.Apply();
            return Query<TEntity>();
        }

        /// <summary>
        /// In-memory построитель Include: гидратирует навигации в списках структуры.
        /// </summary>
        private sealed class InMemoryIncludeBuilder<TEntity> : ILotusIncludeBuilder<TEntity>
            where TEntity : class
        {
            private readonly DataStorageInMemory _storage;
            private readonly List<PropertyInfo> _navigations = [];

            public InMemoryIncludeBuilder(DataStorageInMemory storage)
            {
                _storage = storage;
            }

            public ILotusIncludeBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigation)
            {
                XArgumentNull.ThrowIfNull(navigation, nameof(navigation));
                var property = GetNavigationProperty(navigation);
                if (property is not null)
                {
                    _navigations.Add(property);
                }

                return this;
            }

            public void Apply()
            {
                foreach (var navigation in _navigations)
                {
                    _storage.HydrateNavigation<TEntity>(navigation);
                }
            }

            private static PropertyInfo? GetNavigationProperty<TProperty>(Expression<Func<TEntity, TProperty>> navigation)
            {
                Expression body = navigation.Body;
                if (body is UnaryExpression unary && unary.NodeType == ExpressionType.Convert)
                {
                    body = unary.Operand;
                }

                if (body is MemberExpression member && member.Member is PropertyInfo property)
                {
                    return property;
                }

                throw new ArgumentException(
                    $"Expression '{navigation}' must be a simple property access, e.g. x => x.Role.",
                    nameof(navigation));
            }
        }

        /// <inheritdoc/>
        public TEntity? GetById<TEntity, TKey>(TKey id)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>
        {
            return FindById<TEntity, TKey>(_structure.GetOrCreateList<TEntity>(), id);
        }

        /// <inheritdoc/>
        public ValueTask<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class
            where TKey : struct, IEquatable<TKey>
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(GetById<TEntity, TKey>(id));
        }

        /// <inheritdoc/>
        public IList<TEntity?> GetByIds<TEntity, TKey>(IEnumerable<TKey> ids)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>
        {
            XArgumentNull.ThrowIfNull(ids, nameof(ids));
            var idSet = ids as ISet<TKey> ?? ids.ToHashSet();
            var list = _structure.GetOrCreateList<TEntity>();
            var result = new List<TEntity?>();
            foreach (var entity in list)
            {
                if (idSet.Contains(entity.Id))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public ValueTask<IList<TEntity?>> GetByIdsAsync<TEntity, TKey>(IEnumerable<TKey> ids, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>
            where TKey : struct, IEquatable<TKey>
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(GetByIds<TEntity, TKey>(ids));
        }

        /// <inheritdoc/>
        public TEntity? GetByName<TEntity>(string name) where TEntity : class, ILotusNameable
        {
            return _structure.GetOrCreateList<TEntity>().Find(x => x.Name == name);
        }

        /// <inheritdoc/>
        public ValueTask<TEntity?> GetByNameAsync<TEntity>(string name, CancellationToken token = default)
            where TEntity : class, ILotusNameable
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(GetByName<TEntity>(name));
        }

        /// <inheritdoc/>
        public TEntity GetOrAdd<TEntity, TKey>(TKey id)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            var list = _structure.GetOrCreateList<TEntity>();
            var existing = FindById<TEntity, TKey>(list, id);
            if (existing is not null)
            {
                return existing;
            }

            var entity = new TEntity { Id = id };
            list.Add(entity);
            MarkChanged();
            return entity;
        }

        /// <inheritdoc/>
        public ValueTask<TEntity> GetOrAddAsync<TEntity, TKey>(TKey id, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(GetOrAdd<TEntity, TKey>(id));
        }

        /// <inheritdoc/>
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entity, nameof(entity));
            _structure.GetOrCreateList<TEntity>().Add(entity);
            MarkChanged();
            return entity;
        }

        /// <inheritdoc/>
        public ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(Add(entity));
        }

        /// <inheritdoc/>
        public TEntity AddOrUpdate<TEntity, TKey>(TEntity entity)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            XArgumentNull.ThrowIfNull(entity, nameof(entity));
            var list = _structure.GetOrCreateList<TEntity>();
            var index = FindIndexById<TEntity, TKey>(list, entity.Id);
            if (index < 0)
            {
                list.Add(entity);
                MarkChanged();
                return entity;
            }

            list[index] = entity;
            MarkChanged();
            return entity;
        }

        /// <inheritdoc/>
        public ValueTask<TEntity> AddOrUpdateAsync<TEntity, TKey>(TEntity entity, CancellationToken token = default)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            token.ThrowIfCancellationRequested();
            return XValueTask.FromResult(AddOrUpdate<TEntity, TKey>(entity));
        }

        /// <inheritdoc/>
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entities, nameof(entities));
            var items = entities as ICollection<TEntity> ?? entities.ToArray();
            _structure.GetOrCreateList<TEntity>().AddRange(items);
            MarkChanged(items.Count);
        }

        /// <inheritdoc/>
        public Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class
        {
            token.ThrowIfCancellationRequested();
            AddRange(entities);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entity, nameof(entity));
            MarkChanged();
            return entity;
        }

        /// <inheritdoc/>
        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entities, nameof(entities));
#if UNITY_2017_1_OR_NEWER
            var count = entities is ICollection<TEntity> collection
                ? collection.Count
                : entities.Count();
#else
            var count = entities.TryGetNonEnumeratedCount(out var nonEnumeratedCount)
                ? nonEnumeratedCount
                : entities.Count();
#endif
            MarkChanged(Math.Max(count, 1));
        }

        /// <inheritdoc/>
        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entity, nameof(entity));
            if (_structure.GetOrCreateList<TEntity>().Remove(entity))
            {
                MarkChanged();
            }
        }

        /// <inheritdoc/>
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            XArgumentNull.ThrowIfNull(entities, nameof(entities));
            var list = _structure.GetOrCreateList<TEntity>();
            var removed = 0;
            foreach (var entity in entities)
            {
                if (list.Remove(entity))
                {
                    removed++;
                }
            }

            if (removed > 0)
            {
                MarkChanged(removed);
            }
        }

        /// <inheritdoc/>
        public void SyncRelatedCollections<TEntity, TKey>(IEnumerable<TEntity> entities)
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : struct, IEquatable<TKey>
        {
            XArgumentNull.ThrowIfNull(entities, nameof(entities));
            var newEntities = entities.ToList();
            var newIds = newEntities.Select(x => x.Id).ToHashSet();
            var list = _structure.GetOrCreateList<TEntity>();
            var changes = 0;

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (!newIds.Contains(list[i].Id))
                {
                    list.RemoveAt(i);
                    changes++;
                }
            }

            foreach (var newItem in newEntities)
            {
                var index = FindIndexById<TEntity, TKey>(list, newItem.Id);
                if (index < 0)
                {
                    list.Add(newItem);
                    changes++;
                }
                else if (!ReferenceEquals(list[index], newItem))
                {
                    list[index] = newItem;
                    changes++;
                }
            }

            if (changes > 0)
            {
                MarkChanged(changes);
            }
        }
        #endregion

        #region IQueryable terminals
        /// <inheritdoc/>
        public Task<TEntity?> SingleOrDefaultAsync<TEntity>(CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Query<TEntity>().SingleOrDefault());
        }

        /// <inheritdoc/>
        public Task<TEntity?> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Query<TEntity>().SingleOrDefault(predicate));
        }

        /// <inheritdoc/>
        public Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(query.SingleOrDefault());
        }

        /// <inheritdoc/>
        public Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(query.SingleOrDefault(predicate));
        }

        /// <inheritdoc/>
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Query<TEntity>().FirstOrDefault());
        }

        /// <inheritdoc/>
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(Query<TEntity>().FirstOrDefault(predicate));
        }

        /// <inheritdoc/>
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(query.FirstOrDefault());
        }

        /// <inheritdoc/>
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(query.FirstOrDefault(predicate));
        }
        #endregion

        #region Paging
        /// <inheritdoc/>
        public Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
            where TEntity : class
            where TResponse : class
        {
            if (request.PageInfo is null)
            {
                return PageAsync<TEntity, TResponse>(query, token);
            }

            return PageAsync<TEntity, TResponse>(query, request.PageInfo.PageNumber, request.PageInfo.PageSize, token);
        }

        /// <inheritdoc/>
        public Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, Request request, CancellationToken token = default)
            where TEntity : class
        {
            if (request.PageInfo is null)
            {
                return PageAsync(query, token);
            }

            return PageAsync(query, request.PageInfo.PageNumber, request.PageInfo.PageSize, token);
        }

        /// <inheritdoc/>
        public Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
            where TEntity : class
            where TResponse : class
        {
            token.ThrowIfCancellationRequested();
            var totalCount = query.Count();
            var entities = query.Skip(page * pageSize).Take(pageSize).ToArray();
            var data = entities.Select(MapToResponse<TEntity, TResponse>).ToArray();

            return Task.FromResult(new ResponsePage<TResponse>
            {
                Payload = data,
                PageInfo = new PageInfoResponse
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    CurrentPageSize = data.Length,
                    TotalCount = totalCount,
                },
            });
        }

        /// <inheritdoc/>
        public Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, int page, int pageSize, CancellationToken token = default)
            where TEntity : class
        {
            token.ThrowIfCancellationRequested();
            var totalCount = query.Count();
            var data = query.Skip(page * pageSize).Take(pageSize).ToArray();

            return Task.FromResult(new ResponsePage<TEntity>
            {
                Payload = data,
                PageInfo = new PageInfoResponse
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    CurrentPageSize = data.Length,
                    TotalCount = totalCount,
                },
            });
        }

        /// <inheritdoc/>
        public Task<ResponsePage<TResponse>> PageAsync<TEntity, TResponse>(IOrderedQueryable<TEntity> query, CancellationToken token = default)
            where TEntity : class
            where TResponse : class
        {
            token.ThrowIfCancellationRequested();
            var entities = query.ToArray();
            var data = entities.Select(MapToResponse<TEntity, TResponse>).ToArray();
            var totalCount = data.Length;

            return Task.FromResult(new ResponsePage<TResponse>
            {
                Payload = data,
                PageInfo = new PageInfoResponse
                {
                    PageNumber = 0,
                    PageSize = totalCount,
                    CurrentPageSize = data.Length,
                    TotalCount = totalCount,
                },
            });
        }

        /// <inheritdoc/>
        public Task<ResponsePage<TEntity>> PageAsync<TEntity>(IOrderedQueryable<TEntity> query, CancellationToken token = default)
            where TEntity : class
        {
            token.ThrowIfCancellationRequested();
            var data = query.ToArray();
            var totalCount = data.Length;

            return Task.FromResult(new ResponsePage<TEntity>
            {
                Payload = data,
                PageInfo = new PageInfoResponse
                {
                    PageNumber = 0,
                    PageSize = totalCount,
                    CurrentPageSize = data.Length,
                    TotalCount = totalCount,
                },
            });
        }
        #endregion
    }
    /**@}*/
}
