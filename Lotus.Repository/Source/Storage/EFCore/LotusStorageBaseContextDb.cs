using System;
using System.Threading;
using System.Threading.Tasks;

using Lotus.Core;

namespace Lotus.Repository
{
    /**
     * \defgroup RepositoryEFCore Подсистема EF Core
     * \ingroup Repository
     * \brief Подсистема EF Core для работы с базами данных.
     * @{
     */
    /// <summary>
    /// Класс представляющий собой хранилища данных для EF Core.
    /// </summary>
    public class StorageBaseContextDb<TContext> : PropertyChangedBase, ILotusStorage where TContext : ContextDbStorageStructure
    {
        #region Fields
        protected internal string _connectingString;
        protected internal bool _needSaved;
        protected internal TContext _context;
        protected internal object[] _ids = new object[1];
        #endregion

        #region Properties
        /// <summary>
        /// Данные связвания хранилища с источником.
        /// </summary>
        public string ConnectingData
        {
            get { return _connectingString; }
            set
            {
                _connectingString = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статус указывающий на то что хранилище необходимо сохранить.
        /// </summary>
        public bool NeedSaved
        {
            get { return _needSaved; }
            set
            {
                _needSaved = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Структура базы данных.
        /// </summary>
        public ILotusStorageStructure IStructure
        {
            get { return _context; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public StorageBaseContextDb()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public StorageBaseContextDb(TContext context)
        {
            _context = context;
        }
        #endregion

        #region ILotusStorage methods
        /// <inheritdoc/>
        public virtual ILotusRepository<TEntity, TKey>? GetRepository<TEntity, TKey>()
            where TEntity : class, ILotusIdentifierId<TKey>, new()
            where TKey : notnull, IEquatable<TKey>
        {
            var list = _context.Set<TEntity>();
            if (list != null)
            {
                return new RepositoryDbSet<TEntity, TKey>(_context);
            }

            return null;
        }

        /// <inheritdoc/>
        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual async ValueTask<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await _context.SaveChangesAsync(token);
        }
        #endregion
    }
    /**@}*/
}