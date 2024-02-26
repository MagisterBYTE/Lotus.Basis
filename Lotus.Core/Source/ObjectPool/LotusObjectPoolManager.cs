using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreObjectPool Подсистема объектного пула
     * \ingroup Core
     * \brief Объектный пул - порождающий шаблон проектирования, набор инициализированных и готовых к использованию объектов.
	 * \details
		Когда системе требуется объект, он не создается, а берется из пула. Когда объект больше не нужен, он не
		уничтожается, а возвращается в пул. Представлена отдельно общая реализация и реализация в контексте использования игровых объектов Unity
		
		## Возможности/особенности
		1. Простая работа с пулом готовых объектов
		2. Общая и специальная реализация для Unity объектов
		
		## Описание
		Подсистема пула предназначена для более эффективной работы с объектами путем их повторного использования вместо 
		создания/уничтожения объектов при необходимости. Подсистема в первую очередь направлена на эффектность и скорость
		работы, а не удобство работы. Объекты, готовые к использованию хранятся в стеке, работа ведется через основной 
		менеджер \ref Lotus.Core.PoolManager. Основные операции являются: взять объект из пула для его использования
		(метод \ref Lotus.Core.PoolManager.Take) и положить объект который больше не нужен в пул (метод \ref Lotus.Core.PoolManager.Release)
		В реализации для Unity объектов, дополнительно, в методах Take - игровой объект активируется, в 
		методе Release - игровой объект деактивируется.

		## Использование
		1. Для обычных объектов реализовать интерфейс \ref Lotus.Core.ILotusPoolObject
     * @{
     */
    /// <summary>
    /// Интерфейс менеджера для управления пулом объектов.
    /// </summary>
    public interface ILotusPoolManager : ILotusNameable
    {
        #region Properties
        /// <summary>
        /// Максимальное количество объектов для пула.
        /// </summary>
        /// <remarks>
        /// В случае, если по запросу объектов в пуле не будет, то это значение увеличится вдвое и создаться указанное количество объектов.
        /// </remarks>
        int MaxInstances { get; }

        /// <summary>
        /// Количество объектов в пуле.
        /// </summary>
        int InstanceCount { get; }
        #endregion

        #region Methods 
        /// <summary>
        /// Взять готовый объект из пула.
        /// </summary>
        /// <remarks>
        /// Это максимально общая и универсальная реализация.
        /// </remarks>
        /// <returns>Объект.</returns>
        object TakeObjectFromPool();

        /// <summary>
        /// Освободить объект и положить его назад в пул.
        /// </summary>
        /// <remarks>
        /// Это максимально общая и универсальная реализация.
        /// </remarks>
        /// <param name="poolObject">Объект.</param>
        void ReleaseObjectToPool(object poolObject);
        #endregion
    }

    /// <summary>
    /// Базовый менеджер для управления пулом объектов.
    /// </summary>
    /// <typeparam name="TPoolObject">Тип объекта пула.</typeparam>
    public class PoolManagerBase<TPoolObject> : ILotusPoolManager
    {
        #region Fields
        protected internal string _name = "";
        protected internal int _maxInstances = 20;
        protected internal StackArray<TPoolObject> _poolObjects;
        protected internal Func<TPoolObject> _constructor;
        #endregion

        #region Properties
        /// <summary>
        /// Наименование менеджера.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Максимальное количество объектов для пула.
        /// </summary>
        public int MaxInstances
        {
            get { return _maxInstances; }
        }

        /// <summary>
        /// Количество объектов в пуле.
        /// </summary>
        public int InstanceCount
        {
            get { return _poolObjects.Count; }
        }

        /// <summary>
        /// Конструктор для создания объектов пула.
        /// </summary>
        public Func<TPoolObject> Constructor
        {
            get { return _constructor; }
            set { _constructor = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public PoolManagerBase()
        {
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула.</param>
        public PoolManagerBase(int maxInstance)
        {
            _maxInstances = maxInstance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула.</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула .</param>
        public PoolManagerBase(int maxInstance, Func<TPoolObject> constructor)
        {
            _maxInstances = maxInstance;
            _constructor = constructor;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);

            for (var i = 0; i < _maxInstances; i++)
            {
                _poolObjects.Push(constructor());
            }
        }
        #endregion

        #region ILotusPoolManager methods
        /// <summary>
        /// Взять готовый объект из пула.
        /// </summary>
        /// <returns>Объект.</returns>
        public object TakeObjectFromPool()
        {
            return Take()!;
        }

        /// <summary>
        /// Вставка объекта в пул.
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен.
        /// </remarks>
        /// <param name="poolObject">Объект.</param>
        public void ReleaseObjectToPool(object poolObject)
        {
            Release((TPoolObject)poolObject);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Увеличение размера пула.
        /// </summary>
        protected void ResizePool()
        {
            _maxInstances = _maxInstances * 2;
            for (var i = 0; i < _maxInstances; i++)
            {
                _poolObjects.Push(_constructor());
            }
        }

        /// <summary>
        /// Взять готовый объект из пула.
        /// </summary>
        /// <returns>Объект.</returns>
        public virtual TPoolObject Take()
        {
            if (_poolObjects.Count == 0 && _constructor != null)
            {
                ResizePool();
            }

            TPoolObject pool_object = _poolObjects.Pop()!;
            return pool_object;
        }

        /// <summary>
        /// Освободить объект и положить его назад в пул.
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен.
        /// </remarks>
        /// <param name="poolObject">Объект.</param>
        public virtual void Release(TPoolObject poolObject)
        {
            _poolObjects.Push(poolObject);
        }

        /// <summary>
        /// Очистка всего пула.
        /// </summary>
        public void Clear()
        {
            _poolObjects.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Менеджер для управления пулом объектов с поддержкой пула.
    /// </summary>
    /// <typeparam name="TPoolObject">Тип объекта пула.</typeparam>
    public class PoolManager<TPoolObject> : PoolManagerBase<TPoolObject> where TPoolObject : ILotusPoolObject
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public PoolManager()
            : base()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула.</param>
        public PoolManager(int maxInstance)
            : base(maxInstance)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула.</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула .</param>
        public PoolManager(int maxInstance, Func<TPoolObject> constructor)
            : base(maxInstance, constructor)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Взять готовый объект из пула.
        /// </summary>
        /// <returns>Объект.</returns>
        public override TPoolObject Take()
        {
            if (_poolObjects.Count == 0 && _constructor != null)
            {
                ResizePool();
            }

            TPoolObject pool_object = _poolObjects.Pop()!;
            pool_object.OnPoolTake();
            return pool_object;
        }

        /// <summary>
        /// Освободить объект и положить его назад в пул.
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен.
        /// </remarks>
        /// <param name="poolObject">Объект.</param>
        public override void Release(TPoolObject poolObject)
        {
            poolObject.OnPoolRelease();
            _poolObjects.Push(poolObject);
        }
        #endregion
    }
    /**@}*/
}