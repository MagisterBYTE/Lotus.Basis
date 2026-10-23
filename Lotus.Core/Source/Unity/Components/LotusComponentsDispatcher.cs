#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityComponent Подсистема компонентов Unity
     * \ingroup Core
     * \brief Подсистема компонентов содержит функциональность для работы с компонентами.
     * \details Подсистема компонентов направлена на достижение более эффективной работы с компонентами, в особенности
        эффективного поиска и получение компонентов по различным критериям, кэшированием определенных типов компонент,
        упрощения типовых действий с компонентами, а также получение интерфейсов.

        Также подсистема содержит методы расширения для стандартных и пользовательских компонентов.
     * @{
     */
    /// <summary>
    /// Центральный диспетчер компонентов
    /// </summary>
    /// <remarks>
    /// <para>
    /// Центральный диспетчер компонентов обеспечивает более удобный и расширенный функционал для работы с компонентами
    /// включая эффективный поиск компонентов по различным критериям, кэширования определенных компонентов, получение
    /// интерфейсов.
    /// </para>
    /// <para>
    /// Управляется центральным диспетчером <see cref="LotusSystemDispatcher"/>.
    /// Если центральный диспетчер не используется, то методы нужно вызывать вручную в соответствующих местах
    /// </para>
    /// </remarks>
    public static class XComponentDispatcher
    {
        #region Fields
        public static Dictionary<Type, List<Component>> _cachedComponents;
        #endregion

        #region Properties
        /// <summary>
        /// Список кэшированных компонентов сгруппированных по типу
        /// </summary>
        public static Dictionary<Type, List<Component>> CachedComponents
        {
            get
            {
                if (_cachedComponents == null)
                {
                    _cachedComponents = new Dictionary<Type, List<Component>>(100);
                }
                return _cachedComponents;
            }
        }
        #endregion

        #region Dispatcher methods
        /// <summary>
        /// Перезапуск данных центрального диспетчера компонентов в режиме редактора
        /// </summary>
        public static void OnResetEditor()
        {
        }


        /// <summary>
        /// Первичная инициализация данных центрального диспетчера компонентов
        /// </summary>
        public static void OnInit()
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Удаление компонента
        /// </summary>
        /// <remarks>
        /// Обеспечивается корректное удаление компонента в режиме редактора и в режиме игры
        /// </remarks>
        /// <param name="behaviour">Компонент</param>
        public static void Destroy(Behaviour behaviour)
        {
#if UNITY_EDITOR
            if (behaviour != null)
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    // Удаляем компонент
                    behaviour.enabled = false;
                    GameObject.Destroy(behaviour);
                }
                else
                {
                    // Удаляем компонент
                    GameObject.DestroyImmediate(behaviour);
                }
            }
#else
            // Удаляем объект
            GameObject.Destroy(behaviour);
#endif
        }


        /// <summary>
        /// Кэширование компонентов
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        public static void SetCachedComponents<TComponent>() where TComponent : Component
        {
            TComponent[] scripts = Resources.FindObjectsOfTypeAll<TComponent>();
            List<Component> listComponents = null;
            if (CachedComponents.ContainsKey(typeof(TComponent)))
            {
                listComponents = CachedComponents[typeof(TComponent)];
            }
            else
            {
                listComponents = new List<Component>();
                CachedComponents.Add(typeof(TComponent), listComponents);
            }

            for (var i = 0; i < scripts.Length; i++)
            {
                if (scripts[i].gameObject.IsPrefab() == false)
                {
                    listComponents.Add(scripts[i]);
                }
            }
        }


        /// <summary>
        /// Получение списка всех компонент на сцене (включая не активные)
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <returns>Список всех компонентов на сцене</returns>
        public static List<TComponent> GetAll<TComponent>() where TComponent : Component
        {
            var list = new List<TComponent>();
            TComponent[] scripts = Resources.FindObjectsOfTypeAll<TComponent>();

            for (var i = 0; i < scripts.Length; i++)
            {
                if (scripts[i].gameObject.IsPrefab() == false)
                {
                    list.Add(scripts[i]);
                }
            }

            return list;
        }


        /// <summary>
        /// Получение списка всех компонент на сцене (включая не активные) с реализацией указанного интерфейса
        /// </summary>
        /// <typeparam name="IComponent">Тип интерфейса</typeparam>
        /// <returns>Список всех компонентов на сцене</returns>
        public static List<IComponent> GetAllOfInterface<IComponent>()
        {
            var list = new List<IComponent>();
            var rootObjs = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in rootObjs)
            {
                // Pass in "true" to include inactive and disabled children
                list.AddRange(root.GetComponentsInChildren<IComponent>(true));
            }
            return list;
        }


        /// <summary>
        /// Получение списка всех компонент на сцене удовлетворяющих условию предиката (включая не активные)
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="match">Предикат</param>
        /// <returns>Список всех компонентов на сцене</returns>
        public static List<TComponent> GetAll<TComponent>(Predicate<TComponent> match) where TComponent : Component
        {
            var list = new List<TComponent>();
            TComponent[] scripts = Resources.FindObjectsOfTypeAll<TComponent>();

            for (var i = 0; i < scripts.Length; i++)
            {
                if (scripts[i].gameObject.IsPrefab() == false && match(scripts[i]))
                {
                    list.Add(scripts[i]);
                }
            }

            return list;
        }


        /// <summary>
        /// Получение массива всех компонент на сцене (включая не активные)
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <returns>Массив всех компонентов на сцене</returns>
        [Obsolete("Используйте GetAll<TComponent>().ToArray() — дублирующий API")]
        public static TComponent[] GetAllOfArray<TComponent>() where TComponent : Component
        {
            return GetAll<TComponent>().ToArray();
        }


        /// <summary>
        /// Получение массива всех компонент на сцене удовлетворяющих условию предиката (включая не активные)
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="match">Предикат</param>
        /// <returns>Массив всех компонентов на сцене</returns>
        [Obsolete("Используйте GetAll<TComponent>(match).ToArray() — дублирующий API")]
        public static TComponent[] GetAllOfArray<TComponent>(Predicate<TComponent> match) where TComponent : Component
        {
            return GetAll(match).ToArray();
        }


        /// <summary>
        /// Получение списка всех компонент в указанном списке
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="list">Список компонент или игровых объектов</param>
        /// <returns>Список всех компонентов в указанном списке</returns>
        public static List<TComponent> GetAll<TComponent>(IList list) where TComponent : Component
        {
            var listResult = new List<TComponent>();

            for (var i = 0; i < list.Count; i++)
            {
                var comp = list[i] as TComponent;
                if (comp != null)
                {
                    if (listResult.Contains(comp) == false)
                    {
                        listResult.Add(comp);
                    }
                }
                else
                {
                    var go = list[i] as GameObject;
                    if (go != null)
                    {
                        comp = go.GetComponent<TComponent>();
                        if (comp != null)
                        {
                            if (listResult.Contains(comp) == false)
                            {
                                listResult.Add(comp);
                            }
                        }
                    }
                }
            }

            return listResult;
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Поиск компонента который может быть только один на сцене
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent Find<TComponent>() where TComponent : Component
        {
            TComponent[] scripts = Resources.FindObjectsOfTypeAll<TComponent>();

            if (scripts != null && scripts.Length > 0)
            {
                return scripts[0];
            }

            return null;
        }


        /// <summary>
        /// Поиск компонента по идентификатору ID игрового объекта
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent Find<TComponent>(EntityId id) where TComponent : Component
        {
            GameObject goElement = XGameObjectDispatcher.Find(id);
            if (goElement == null)
            {
                return null;
            }
            else
            {
                return goElement.GetComponent<TComponent>();
            }
        }


        /// <summary>
        /// Поиск компонента по имени игрового объекта
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="name">Имя игрового объекта</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent Find<TComponent>(string name) where TComponent : Component
        {
            GameObject goElement = XGameObjectDispatcher.Find(name);
            if (goElement == null)
            {
                return null;
            }
            else
            {
                return goElement.GetComponent<TComponent>();
            }
        }


        /// <summary>
        /// Поиск компонента по вхождению имени игрового объекта
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="name">Имя игрового объекта</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent FindMatch<TComponent>(string name) where TComponent : Component
        {
            GameObject goElement = XGameObjectDispatcher.FindMatch(name);
            if (goElement == null)
            {
                return null;
            }
            else
            {
                return goElement.GetComponent<TComponent>();
            }
        }


        /// <summary>
        /// Поиск компонента по имени игрового объекта в указанном списке
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="name">Имя игрового объекта</param>
        /// <param name="list">Список компонент или игровых объектов</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent FindInList<TComponent>(string name, IList list) where TComponent : Component
        {
            for (var i = 0; i < list.Count; i++)
            {
                var comp = list[i] as Component;
                if (comp != null)
                {
                    if (comp.gameObject.name == name)
                    {
                        return comp.GetComponent<TComponent>();
                    }
                }
                else
                {
                    var go = list[i] as GameObject;
                    if (go != null && go.name == name)
                    {
                        return go.GetComponent<TComponent>();
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Поиск компонента по идентификатору ID игрового объекта в указанном списке
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <param name="list">Список компонент или игровых объектов</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent FindInList<TComponent>(EntityId id, IList list) where TComponent : Component
        {
            for (var i = 0; i < list.Count; i++)
            {
                var comp = list[i] as Component;
                if (comp != null)
                {
                    if (comp.gameObject.GetEntityId() == id)
                    {
                        return comp.GetComponent<TComponent>();
                    }
                }
                else
                {
                    var go = list[i] as GameObject;
                    if (go != null && go.GetEntityId() == id)
                    {
                        return go.GetComponent<TComponent>();
                    }
                }
            }

            return null;
        }
        #endregion
    }
    /**@}*/
}
#endif
