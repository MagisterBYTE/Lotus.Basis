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
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        public PoolManagerUnityBase(int max_instance)
            : base(max_instance)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула</param>
        public PoolManagerUnityBase(int max_instance, Func<TPoolObject> constructor)
            : base(max_instance, constructor)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        public PoolManagerUnityBase(int max_instance, TPoolObject prefab)
        {
            _maxInstances = max_instance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject pool_object = UnityEngine.Object.Instantiate(prefab);
                pool_object.gameObject.SetActive(false);
                _poolObjects.Push(pool_object);
            }
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        /// <param name="parent">Родительский компонент трансформации</param>
        public PoolManagerUnityBase(int max_instance, TPoolObject prefab, UnityEngine.Transform parent)
        {
            _maxInstances = max_instance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

#if UNITY_EDITOR
            _parent = parent;
#endif

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject pool_object = UnityEngine.Object.Instantiate(prefab);
                pool_object.gameObject.SetActive(false);
                _poolObjects.Push(pool_object);
#if UNITY_EDITOR
                pool_object.transform.SetParent(parent, false);
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
                    TPoolObject pool_obj = UnityEngine.Object.Instantiate(_prefab);
                    pool_obj.gameObject.SetActive(false);
                    _poolObjects.Push(pool_obj);
#if UNITY_EDITOR
                    if (_parent != null)
                    {
                        pool_obj.transform.SetParent(_parent, false);
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
                        TPoolObject pool_obj = _constructor();
                        pool_obj.gameObject.SetActive(false);
                        _poolObjects.Push(pool_obj);
#if UNITY_EDITOR
                        if (_parent != null)
                        {
                            pool_obj.transform.SetParent(_parent, false);
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

            TPoolObject pool_object = _poolObjects.Pop();
            pool_object.gameObject.SetActive(true);

            return pool_object;
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
            TPoolObject pool_object = Take();
            pool_object.transform.SetParent(parent, false);
            pool_object.transform.position = position;
            pool_object.transform.rotation = rotation;
            return pool_object;
        }


        /// <summary>
        /// Освободить объект и положить его назад в пул
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен
        /// </remarks>
        /// <param name="pool_object">Объект</param>

        public override void Release(TPoolObject pool_object)
        {
            pool_object.gameObject.SetActive(false);
            _poolObjects.Push(pool_object);

#if UNITY_EDITOR
            if (_parent != null)
            {
                pool_object.transform.SetParent(_parent, false);
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
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        public PoolManagerUnity(int max_instance)
            : base(max_instance)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="constructor">Конструктор для создания начального количества объектов пула</param>
        public PoolManagerUnity(int max_instance, Func<TPoolObject> constructor)
            : base(max_instance, constructor)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        public PoolManagerUnity(int max_instance, TPoolObject prefab)
        {
            _maxInstances = max_instance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject pool_object = UnityEngine.Object.Instantiate(prefab);
                pool_object.OnPoolRelease();
                _poolObjects.Push(pool_object);
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="max_instance">Максимальное количество объектов для пула</param>
        /// <param name="prefab">Префаб для заполнения пула</param>
        /// <param name="parent">Родительский компонент трансформации</param>
        public PoolManagerUnity(int max_instance, TPoolObject prefab, Transform parent)
        {
            _maxInstances = max_instance;
            _poolObjects = new StackArray<TPoolObject>(_maxInstances);
            _prefab = prefab;

#if UNITY_EDITOR
            _parent = parent;
#endif

            for (var i = 0; i < _maxInstances; i++)
            {
                TPoolObject pool_object = UnityEngine.Object.Instantiate(prefab);
                pool_object.OnPoolRelease();
                _poolObjects.Push(pool_object);
#if UNITY_EDITOR
                pool_object.transform.SetParent(parent, false);
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
                    TPoolObject pool_obj = UnityEngine.Object.Instantiate(_prefab);
                    pool_obj.OnPoolRelease();
                    _poolObjects.Push(pool_obj);
#if UNITY_EDITOR
                    if (_parent != null)
                    {
                        pool_obj.transform.SetParent(_parent, false);
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
                        TPoolObject pool_obj = _constructor();
                        pool_obj.OnPoolRelease();
                        _poolObjects.Push(pool_obj);
#if UNITY_EDITOR
                        if (_parent != null)
                        {
                            pool_obj.transform.SetParent(_parent, false);
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

            TPoolObject pool_object = _poolObjects.Pop();
            pool_object.OnPoolTake();

            return pool_object;
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
            TPoolObject pool_object = Take();
            pool_object.OnPoolTake();
            pool_object.transform.SetParent(parent, false);
            pool_object.transform.position = position;
            pool_object.transform.rotation = rotation;
            return pool_object;
        }


        /// <summary>
        /// Освободить объект и положить его назад в пул
        /// </summary>
        /// <remarks>
        /// Применяется когда объект не нужен
        /// </remarks>
        /// <param name="pool_object">Объект</param>
        public override void Release(TPoolObject pool_object)
        {
            pool_object.OnPoolRelease();
            _poolObjects.Push(pool_object);

#if UNITY_EDITOR
            if (_parent != null)
            {
                pool_object.transform.SetParent(_parent, false);
            }
#endif
        }
        #endregion
    }

    /**@}*/
}
#endif