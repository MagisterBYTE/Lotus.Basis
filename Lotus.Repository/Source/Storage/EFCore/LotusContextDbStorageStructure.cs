using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryEFCore
    *@{*/
    /// <summary>
    /// Базовый класс контекста базы данных для структуры базы данных хранилища
    /// </summary>
    public class ContextDbStorageStructure : DbContext, ILotusStorageStructure
    {
        #region Fields
        protected internal string _connectingString;
        #endregion

        #region Properties
        /// <summary>
        /// Данные связвания хранилища с источником.
        /// </summary>
        public string ConnectingData
        {
            get { return _connectingString; }
            set { _connectingString = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public ContextDbStorageStructure()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="connectingString">Строка для подключения к БД.</param>
        public ContextDbStorageStructure(string connectingString)
        {
            _connectingString = connectingString;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="options">Опции</param>
        public ContextDbStorageStructure(DbContextOptions options)
            : base(options)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="options">Опции</param>
        public ContextDbStorageStructure(DbContextOptions<ContextDbStorageStructure> options)
            : base(options)
        {

        }
        #endregion

        #region ILotusStorageStructure methods
        /// <inheritdoc/>
        public IQueryable<TEntity>? GetEntities<TEntity>() where TEntity : class, new()
        {
            var list = this.Set<TEntity>();
            return list;
        }
        #endregion
    }
    /**@}*/
}