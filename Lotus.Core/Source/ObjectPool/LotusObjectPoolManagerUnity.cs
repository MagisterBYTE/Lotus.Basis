#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreObjectPool
    *@{*/
    /// <summary>
    /// Базовый менеджер для управления пулом игровых объектов Unity
    /// </summary>
    /// <remarks>
    /// Это простой базовый диспетчер для управления пулом игровых объектов Unity когда игровому объекту не требуется
    /// никакой дополнительной логики при взятие из пулу и возвращение его назад. 
    /// При взятие из пула игровой объект активируется, при возвращении в пул - деактивируется
    /// </remarks>
    /// <typeparam name="TPoolObject">Тип объекта пула</typeparam>
    public class PoolManagerUnityBase<TPoolObject> : PoolManagerBase<TPoolObject> where TPoolObject : MonoBehaviour
    {
        #region Fields
        protected TPoolObject _prefab;
#if UNITY_EDITOR
        protected UnityEngine.Transform _parent;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Префаб для заполнения пула
        /// </summary>
        public TPoolObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Родительский компонент трансформации
        /// </summary>
        /// <remarks>
        /// Применяется в режиме редактора для наведения порядка в инспекторе сцены
        /// </remarks>
        public Transform Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
#endif
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public PoolManagerUnityBase()
            : base()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        public PoolManagerUnityBase(int maxInstance)
            : base(maxInstance)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула</param>
        public PoolManagerUnityBase(int maxInstance, Func<TPoolObject> constructor)
            : base(maxInstance, constructor)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        public PoolManagerUnityBase(int maxInstance, TPoolObject prefab)
        {
            _maxInstances = maxInstance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject poolObject = UnityEngine.Object.Instantiate(prefab);
                poolObject.gameObject.SetActive(false);
                _poolObjects.Push(poolObject);
            }
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        /// <param name="parent">Родительский компонент трансформации</param>
        public PoolManagerUnityBase(int maxInstance, TPoolObject prefab, UnityEngine.Transform parent)
        {
            _maxInstances = maxInstance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

#if UNITY_EDITOR
            _parent = parent;
#endif

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject poolObject = UnityEngine.Object.Instantiate(prefab);
                poolObject.gameObject.SetActive(false);
                _poolObjects.Push(poolObject);
#if UNITY_EDITOR
                poolObject.transform.SetParent(parent, false);
#endif
            }
        }
        #endregion

        #region Main methods

        /// <summary>
        /// Увеличение размера пула
        /// </summary>

        protected void ResizePoolUnity()
        {
            // Отдаем предпочтение префабу
            if (_prefab != null)
            {
                _maxInstances = _maxInstances * 2;
                for (var i = 0; i < _maxInstances; i++)
                {
                    TPoolObject poolObj = UnityEngine.Object.Instantiate(_prefab);
                    poolObj.gameObject.SetActive(false);
                    _poolObjects.Push(poolObj);
#if UNITY_EDITOR
                    if (_parent != null)
                    {
                        poolObj.transform.SetParent(_parent, false);
                    }
#endif
                }
            }
            else
            {
                if (_constructor != null)
                {
                    _maxInstances = _maxInstances * 2;
                    for (var i = 0; i < _maxInstances; i++)
                    {
                        TPoolObject poolObj = _constructor();
                        poolObj.gameObject.SetActive(false);
                        _poolObjects.Push(poolObj);
#if UNITY_EDITOR
                        if (_parent != null)
                        {
                            poolObj.transform.SetParent(_parent, false);
                        }
#endif
                    }
                }
            }
        }


        /// <summary>
        /// Взять готовый объект из пула
        /// </summary>
        /// <returns>Объект</returns>
        public override TPoolObject Take()
        {
            if (_poolObjects.Count == 0)
            {
                ResizePoolUnity();
            }

            TPoolObject poolObject = _poolObjects.Pop();
            poolObject.gameObject.SetActive(true);

            return poolObject;
        }

        /// <summary>
        /// Взять готовый объект из пула и присвоить указанные параметры
        /// </summary>
        /// <param name="parent">Родительский компонент трансформации</param>
        /// <param name="position">Позиция объекта</param>
        /// <param name="rotation">Вращение объекта</param>
        /// <returns>Объект</returns>
        public TPoolObject Take(Transform parent, Vector3 position, Quaternion rotation)
        {
            TPoolObject poolObject = Take();
            poolObject.transform.SetParent(parent, false);
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            return poolObject;
        }


        /// <summary>
        /// Освободить объект и положить его назад в пул
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен
        /// </remarks>
        /// <param name="poolObject">Объект</param>

        public override void Release(TPoolObject poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _poolObjects.Push(poolObject);

#if UNITY_EDITOR
            if (_parent != null)
            {
                poolObject.transform.SetParent(_parent, false);
            }
#endif
        }
        #endregion
    }


    /// <summary>
    /// Базовый менеджер для управления пулом игровых объектов Unity
    /// </summary>
    /// <remarks>
    /// Это простой базовый диспетчер для управления пулом игровых объектов Unity когда игровому объекту не требуется
    /// никакой дополнительной логики при взятие из пулу и возвращение его назад. 
    /// При взятие из пула игровой объект активируется, при возвращении в пул - деактивируется
    /// </remarks>
    /// <typeparam name="TPoolObject">Тип объекта пула</typeparam>

    public class PoolManagerUnity<TPoolObject> : PoolManager<TPoolObject>
        where TPoolObject : UnityEngine.MonoBehaviour, ILotusPoolObject
    {
        #region Fields
        protected TPoolObject _prefab;
#if UNITY_EDITOR
        protected Transform _parent;
#endif
        #endregion

        #region Properties
        /// <summary>
        /// Префаб для заполнения пула
        /// </summary>
        public TPoolObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Родительский компонент трансформации
        /// </summary>
        /// <remarks>
        /// Применяется в режиме редактора для наведения порядка в инспекторе сцены
        /// </remarks>
        public Transform Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
#endif
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public PoolManagerUnity()
            : base()
        {
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        public PoolManagerUnity(int maxInstance)
            : base(maxInstance)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула</param>
        public PoolManagerUnity(int maxInstance, Func<TPoolObject> constructor)
            : base(maxInstance, constructor)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        public PoolManagerUnity(int maxInstance, TPoolObject prefab)
        {
            _maxInstances = maxInstance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject poolObject = UnityEngine.Object.Instantiate(prefab);
                poolObject.OnPoolRelease();
                _poolObjects.Push(poolObject);
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="maxInstance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        /// <param name="parent">Родительский компонент трансформации</param>
        public PoolManagerUnity(int maxInstance, TPoolObject prefab, Transform parent)
        {
            _maxInstances = maxInstance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

#if UNITY_EDITOR
            _parent = parent;
#endif

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject poolObject = UnityEngine.Object.Instantiate(prefab);
                poolObject.OnPoolRelease();
                _poolObjects.Push(poolObject);
#if UNITY_EDITOR
                poolObject.transform.SetParent(parent, false);
#endif
            }
        }
        #endregion

        #region Main methods

        /// <summary>
        /// Увеличение размера пула
        /// </summary>
        protected void ResizePoolUnity()
        {
            // Отдаем предпочтение префабу
            if (_prefab != null)
            {
                _maxInstances = _maxInstances * 2;
                for (var i = 0; i < _maxInstances; i++)
                {
                    TPoolObject poolObj = UnityEngine.Object.Instantiate(_prefab);
                    poolObj.OnPoolRelease();
                    _poolObjects.Push(poolObj);
#if UNITY_EDITOR
                    if (_parent != null)
                    {
                        poolObj.transform.SetParent(_parent, false);
                    }
#endif
                }
            }
            else
            {
                if (_constructor != null)
                {
                    _maxInstances = _maxInstances * 2;
                    for (var i = 0; i < _maxInstances; i++)
                    {
                        TPoolObject poolObj = _constructor();
                        poolObj.OnPoolRelease();
                        _poolObjects.Push(poolObj);
#if UNITY_EDITOR
                        if (_parent != null)
                        {
                            poolObj.transform.SetParent(_parent, false);
                        }
#endif
                    }
                }
            }
        }


        /// <summary>
        /// Взять готовый объект из пула
        /// </summary>
        /// <returns>Объект</returns>
        public override TPoolObject Take()
        {
            if (_poolObjects.Count == 0)
            {
                ResizePoolUnity();
            }

            TPoolObject poolObject = _poolObjects.Pop();
            poolObject.OnPoolTake();

            return poolObject;
        }


        /// <summary>
        /// Взять готовый объект из пула и присвоить указанные параметры
        /// </summary>
        /// <param name="parent">Родительский компонент трансформации</param>
        /// <param name="position">Позиция объекта</param>
        /// <param name="rotation">Вращение объекта</param>
        /// <returns>Объект</returns>
        public TPoolObject Take(Transform parent, Vector3 position, Quaternion rotation)
        {
            TPoolObject poolObject = Take();
            poolObject.OnPoolTake();
            poolObject.transform.SetParent(parent, false);
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            return poolObject;
        }


        /// <summary>
        /// Освободить объект и положить его назад в пул
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен
        /// </remarks>
        /// <param name="poolObject">Объект</param>
        public override void Release(TPoolObject poolObject)
        {
            poolObject.OnPoolRelease();
            _poolObjects.Push(poolObject);

#if UNITY_EDITOR
            if (_parent != null)
            {
                poolObject.transform.SetParent(_parent, false);
            }
#endif
        }
        #endregion
    }

    /**@}*/
}
#endif