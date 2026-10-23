#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityGameObject
    *@{*/
    /// <summary>
    /// Центральный диспетчер игровых объектов
    /// </summary>
    /// <remarks>
    /// <para>
    /// Центральный диспетчер игровых объектов обеспечивает унифицированный поиск и предоставление всех доступных
    /// игровых объектов, включая неактивные объекты которые находятся на сцене.
    /// </para>
    /// <para>
    /// Управляется центральным диспетчером <see cref="LotusSystemDispatcher"/>.
    /// Если центральный диспетчер не используется, то методы нужно вызывать вручную в соответствующих местах
    /// </para>
    /// <para>
    /// В Unity 2020+/6 для обхода сцены предпочтителен <see cref="Object.FindObjectsByType{T}"/>
    /// с <see cref="FindObjectsInactive.Include"/> (не возвращает prefab-assets).
    /// <see cref="Resources.FindObjectsOfTypeAll{T}"/> оставлен как fallback для старых версий.
    /// </para>
    /// </remarks>
    public static class XGameObjectDispatcher
    {
        #region Fields
        public static Dictionary<string, List<GameObject>> _cachedGameObjects;
        #endregion

        #region Properties
        /// <summary>
        /// Список кэшированных игровых объектов на сцене сгруппированных по тегу
        /// </summary>
        public static Dictionary<string, List<GameObject>> CachedGameObjects
        {
            get
            {
                if (_cachedGameObjects == null)
                {
                    _cachedGameObjects = new Dictionary<string, List<GameObject>>(100);
                }
                return _cachedGameObjects;
            }
        }
        #endregion

        #region Dispatcher methods
        /// <summary>
        /// Перезапуск данных центрального диспетчера игровых объектов в режиме редактора
        /// </summary>
        public static void OnResetEditor()
        {
        }


        /// <summary>
        /// Первичная инициализация центрального диспетчера игровых объектов
        /// </summary>
        public static void OnInit()
        {
            UpdateCached();
        }


        /// <summary>
        /// Добавление игрового объекта в текущую сцену
        /// </summary>
        /// <param name="go">Игровой объект</param>
        public static void OnGameObjectAdded(GameObject go)
        {
        }


        /// <summary>
        /// Удаление игрового объекта из текущей сцены
        /// </summary>
        /// <param name="go">Игровой объект</param>
        public static void OnGameObjectRemoved(GameObject go)
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Уничтожение игрового объекта
        /// </summary>
        /// <remarks>
        /// Обеспечивается корректное удаление игрового объекта в режиме редактора и в режиме игры
        /// </remarks>
        /// <param name="go">Игровой объект</param>
        public static void Destroy(GameObject go)
        {
#if UNITY_EDITOR
            if (go != null)
            {
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    // Удаляем объект
                    GameObject.Destroy(go);
                }
                else
                {
                    // Удаляем объект
                    GameObject.DestroyImmediate(go);
                }
            }
#else
            // Удаляем объект
            GameObject.Destroy(go);
#endif
        }


        /// <summary>
        /// Построение иерархии объектов по указанному списку данных для хранения иерархической связи
        /// </summary>
        /// <param name="hierarchy">Список объектов</param>
        public static void BuildHierarchy(List<TGameObjectHierarchy> hierarchy)
        {
            if (hierarchy == null || hierarchy.Count == 0)
            {
                return;
            }

            var byId = new Dictionary<int, int>(hierarchy.Count);
            for (var i = 0; i < hierarchy.Count; i++)
            {
                byId[hierarchy[i].Id] = i;
            }

            for (var i = 0; i < hierarchy.Count; i++)
            {
                var parentId = hierarchy[i].ParentId;
                if (parentId == -1)
                {
                    continue;
                }

                if (!byId.TryGetValue(parentId, out var parentIndex))
                {
                    continue;
                }

                var child = hierarchy[i].Object;
                var parent = hierarchy[parentIndex].Object;
                if (child != null && parent != null)
                {
                    child.SetParent(parent, false);
                }
            }
        }


        /// <summary>
        /// Обновление данных о кэшированных игровых объектов на сцене
        /// </summary>
        public static void UpdateCached()
        {
            CachedGameObjects.Clear();
            var gameObjects = GetSceneGameObjects();
            for (var i = 0; i < gameObjects.Length; i++)
            {
                var go = gameObjects[i];
                if (!IsValidSceneObject(go))
                {
                    continue;
                }

                if (CachedGameObjects.TryGetValue(go.tag, out var list))
                {
                    list.Add(go);
                }
                else
                {
                    CachedGameObjects.Add(go.tag, new List<GameObject> { go });
                }
            }
        }
        #endregion

        #region Search methods
#if UNITY_EDITOR
        /// <summary>
        /// Поиск игрового объекта по идентификатору ID
        /// </summary>
        /// <remarks>
        /// Только в режиме редактора
        /// </remarks>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        [Obsolete("Используйте Find(int id) — дублирует editor-путь поиска")]
        internal static GameObject FindFromID(int id)
        {
            // В режиме редактора используем его возможности
            UnityEngine.Object obj = UnityEditor.EditorUtility.InstanceIDToObject(id);
            return obj as GameObject;
        }
#endif


        /// <summary>
        /// Поиск игрового объекта по идентификатору ID
        /// </summary>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(EntityId id)
        {
#if UNITY_EDITOR
            // В режиме редактора используем его возможности
            UnityEngine.Object obj = UnityEditor.EditorUtility.EntityIdToObject(id);
            var gameObject = obj as GameObject;
            if (gameObject != null)
            {
                return gameObject;
            }
#endif
            // Ищем в кэше
            var found = FindInCache(go => go.GetEntityId() == id);
            if (found != null)
            {
                return found;
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            found = FindInSceneFallback(go => go.GetEntityId() == id, "ID");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по идентификатору ID и его имени
        /// </summary>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <param name="name">Имя игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(EntityId id, string name)
        {
#if UNITY_EDITOR
            // В режиме редактора используем его возможности
            UnityEngine.Object obj = UnityEditor.EditorUtility.EntityIdToObject(id);
            var gameObject = obj as GameObject;

            // Только если совпадает имя
            if (gameObject != null && gameObject.name == name)
            {
                return gameObject;
            }
#endif
            // Ищем в кэше
            var found = FindInCache(go => go.GetEntityId() == id && go.name == name);
            if (found != null)
            {
                return found;
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            found = FindInSceneFallback(go => go.GetEntityId() == id && go.name == name, "ID and NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по имени
        /// </summary>
        /// <remarks>
        /// Сначала вызывается встроенный <see cref="GameObject.Find(string)"/> (только активные объекты),
        /// затем кэш (включая неактивные). Для иерархического пути можно передавать path вида "/Root/Child".
        /// </remarks>
        /// <param name="name">Имя игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(string name)
        {
            // Ищем стандартным путем (только активные)
            var gameObject = GameObject.Find(name);
            if (gameObject != null)
            {
                return gameObject;
            }

            // Ищем в кэше (включая неактивные)
            var found = FindInCache(go => go.name == name);
            if (found != null)
            {
                return found;
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            found = FindInSceneFallback(go => go.name == name, "NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по имени и тегу
        /// </summary>
        /// <param name="name">Имя игрового объекта</param>
        /// <param name="tag">Тэг игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(string name, string tag)
        {
            // Ищем в кэше по тегу
            if (CachedGameObjects.TryGetValue(tag, out var listTag))
            {
                for (var i = 0; i < listTag.Count; i++)
                {
                    var go = listTag[i];
                    if (!IsValidSceneObject(go))
                    {
                        continue;
                    }

                    if (go.name == name)
                    {
                        return go;
                    }
                }
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            var found = FindInSceneFallback(go => go.name == name && go.CompareTag(tag), "TAG and NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по пути, имени и тегу
        /// </summary>
        /// <param name="path">Путь игрового объекта</param>
        /// <param name="name">Имя игрового объекта</param>
        /// <param name="tag">Тэг игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(string path, string name, string tag)
        {
            // Ищем в кэше по тегу
            if (CachedGameObjects.TryGetValue(tag, out var listTag))
            {
                for (var i = 0; i < listTag.Count; i++)
                {
                    var go = listTag[i];
                    if (!IsValidSceneObject(go))
                    {
                        continue;
                    }

                    if (go.name == name && go.GetPathScene() == path)
                    {
                        return go;
                    }
                }
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            var found = FindInSceneFallback(
                go => go.name == name && go.CompareTag(tag) && go.GetPathScene() == path,
                "PATH and NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по идентификатору ID, пути, имени и тегу
        /// </summary>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <param name="path">Путь игрового объекта</param>
        /// <param name="name">Имя игрового объекта</param>
        /// <param name="tag">Тэг игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject Find(EntityId id, string path, string name, string tag)
        {
#if UNITY_EDITOR
            // В режиме редактора используем его возможности
            UnityEngine.Object obj = UnityEditor.EditorUtility.EntityIdToObject(id);
            var gameObject = obj as GameObject;

            // Только если совпадает имя
            if (gameObject != null && gameObject.name == name)
            {
                return gameObject;
            }
#endif
            // Ищем в кэше по тегу
            if (CachedGameObjects.TryGetValue(tag, out var listTag))
            {
                for (var i = 0; i < listTag.Count; i++)
                {
                    var go = listTag[i];
                    if (!IsValidSceneObject(go))
                    {
                        continue;
                    }

                    if (go.name != name)
                    {
                        continue;
                    }

                    // Проверяем по идентификатору
                    if (id != EntityId.None && id == go.GetEntityId())
                    {
                        return go;
                    }

                    if (go.GetPathScene() == path)
                    {
                        return go;
                    }
                }
            }

#if UNITY_EDITOR
            // В режиме редактора ищем дополнительно и сообщаем об этом
            var found = FindInSceneFallback(go =>
            {
                if (go.name != name || !go.CompareTag(tag))
                {
                    return false;
                }

                if (id != EntityId.None && id == go.GetEntityId())
                {
                    return true;
                }

                return go.GetPathScene() == path;
            }, "ID/PATH/TAG/NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по вхождению имени
        /// </summary>
        /// <param name="name">Часть имени игрового объекта</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject FindMatch(string name)
        {
            var found = FindInCache(go => go.name.IndexOf(name) > -1);
            if (found != null)
            {
                return found;
            }

#if UNITY_EDITOR
            found = FindInSceneFallback(go => go.name.IndexOf(name) > -1, "match by NAME");
            if (found != null)
            {
                return found;
            }
#endif

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по идентификатору ID в указанном списке
        /// </summary>
        /// <param name="id">Идентификатор игрового объекта</param>
        /// <param name="list">Список игровых объектов</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject FindInList(EntityId id, IList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var go = list[i] as GameObject;
                if (go != null && go.GetEntityId() == id)
                {
                    return go;
                }
            }

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по имени в указанном списке
        /// </summary>
        /// <param name="name">Имя игрового объекта</param>
        /// <param name="list">Список игровых объектов</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject FindInList(string name, IList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var go = list[i] as GameObject;
                if (go != null && go.name == name)
                {
                    return go;
                }
            }

            return null;
        }


        /// <summary>
        /// Поиск игрового объекта по вхождению имени в указанном списке
        /// </summary>
        /// <param name="name">Часть имени игрового объекта</param>
        /// <param name="list">Список игровых объектов</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject FindInListMatch(string name, IList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var go = list[i] as GameObject;
                if (go != null && go.name.IndexOf(name) > -1)
                {
                    return go;
                }
            }

            return null;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Получение всех игровых объектов сцены (включая неактивные), без prefab-assets
        /// </summary>
        /// <returns>Массив игровых объектов</returns>
        private static GameObject[] GetSceneGameObjects()
        {
#if UNITY_6000_0_OR_NEWER
            // Unity 6: без SortMode (устарел); не возвращает assets/prefabs
            return UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include);
#elif UNITY_2020_1_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#else
            return Resources.FindObjectsOfTypeAll<GameObject>();
#endif
        }


        /// <summary>
        /// Проверка, что объект является валидным объектом сцены (не hideFlags / не prefab-asset)
        /// </summary>
        /// <param name="go">Игровой объект</param>
        /// <returns>Статус валидности</returns>
        private static bool IsValidSceneObject(GameObject go)
        {
            if (go == null)
            {
                return false;
            }

            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
            {
                return false;
            }

            if (go.IsPrefab())
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Поиск в кэше по предикату
        /// </summary>
        /// <param name="match">Предикат</param>
        /// <returns>Найденный объект или null</returns>
        private static GameObject FindInCache(Predicate<GameObject> match)
        {
            foreach (var item in CachedGameObjects.Values)
            {
                for (var i = 0; i < item.Count; i++)
                {
                    var go = item[i];
                    if (!IsValidSceneObject(go))
                    {
                        continue;
                    }

                    if (match(go))
                    {
                        return go;
                    }
                }
            }

            return null;
        }


#if UNITY_EDITOR
        /// <summary>
        /// Fallback-поиск по всей сцене в редакторе
        /// </summary>
        /// <param name="match">Предикат</param>
        /// <param name="criteria">Описание критерия для лога</param>
        /// <returns>Найденный объект или null</returns>
        private static GameObject FindInSceneFallback(Predicate<GameObject> match, string criteria)
        {
            var gameObjects = GetSceneGameObjects();
            for (var i = 0; i < gameObjects.Length; i++)
            {
                var go = gameObjects[i];
                if (!IsValidSceneObject(go))
                {
                    continue;
                }

                if (match(go))
                {
                    Debug.LogFormat("GameObject <{0}> find by {1} in editor mode", go.name, criteria);
                    return go;
                }
            }

            return null;
        }
#endif
        #endregion
    }
    /**@}*/
}
#endif
