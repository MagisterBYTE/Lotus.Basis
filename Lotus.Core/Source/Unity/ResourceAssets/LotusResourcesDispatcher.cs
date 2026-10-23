#if UNITY_2017_1_OR_NEWER
using System;
using System.IO;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Центральный диспетчер ресурсов
    /// </summary>
    /// <remarks>
    /// <para>
    /// Центральный диспетчер ресурсов унифицирует работу со всеми доступными ресурсами в Unity. Он обеспечивает
    /// единую технологию для поиска, доступа, загрузки, динамического связывания и сохранения ресурса.
    /// </para>
    /// <para>
    /// Управляется центральным диспетчером <see cref="LotusSystemDispatcher"/>.
    /// Если центральный диспетчер не используется, то методы нужно вызывать вручную в соответствующих местах
    /// </para>
    /// </remarks>
    public static class XResourcesDispatcher
    {
        #region Dispatcher methods
        /// <summary>
        /// Перезапуск данных центрального диспетчера ресурсов в режиме редактора
        /// </summary>
        public static void OnResetEditor()
        {
        }

        /// <summary>
        /// Первичная инициализация данных центрального диспетчера ресурсов
        /// </summary>
        public static void OnInit()
        {
        }

        /// <summary>
        /// Выгрузка сцены - здесь происходит выгрузка неиспользуемых ресурсов
        /// </summary>
        /// <param name="sceneName">Имя сцены</param>
        public static void OnSceneUnload(string sceneName)
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Поиск ресурса по идентификатору ID
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="id">Идентификатор ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static TResource Find<TResource>(EntityId id) where TResource : UnityEngine.Object
        {
            return Find(id, typeof(TResource)) as TResource;
        }

        /// <summary>
        /// Поиск ресурса по идентификатору ID
        /// </summary>
        /// <param name="id">Идентификатор ресурса</param>
        /// <param name="resourceType">Тип ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static UnityEngine.Object Find(EntityId id, Type resourceType)
        {
#if UNITY_EDITOR
            var obj = EditorUtility.EntityIdToObject(id);
            if (obj != null)
            {
                return obj;
            }
#endif

            var resources = Resources.FindObjectsOfTypeAll(resourceType);
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].GetEntityId() == id)
                {
                    return resources[i];
                }
            }

#if UNITY_EDITOR
            var assetPath = AssetDatabase.GetAssetPath(id);
            if (!string.IsNullOrEmpty(assetPath))
            {
                var resource = AssetDatabase.LoadAssetAtPath(assetPath, resourceType);
                if (resource != null)
                {
                    Debug.LogFormat("Resource <{0}> find by ID - load from assets", resource.name);
                    return resource;
                }
            }
#endif
            return null;
        }

        /// <summary>
        /// Поиск ресурса по идентификатору ID и имени
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="id">Идентификатор ресурса</param>
        /// <param name="name">Имя ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static TResource Find<TResource>(EntityId id, string name) where TResource : UnityEngine.Object
        {
            return Find(id, name, typeof(TResource)) as TResource;
        }

        /// <summary>
        /// Поиск ресурса по идентификатору ID и имени
        /// </summary>
        /// <param name="id">Идентификатор ресурса</param>
        /// <param name="name">Имя ресурса</param>
        /// <param name="resourceType">Тип ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static UnityEngine.Object Find(EntityId id, string name, Type resourceType)
        {
#if UNITY_EDITOR
            var obj = EditorUtility.EntityIdToObject(id);
            if (obj != null && obj.name == name)
            {
                return obj;
            }
#endif

            var resources = Resources.FindObjectsOfTypeAll(resourceType);
            UnityEngine.Object findFromName = null;
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].name == name)
                {
                    findFromName = resources[i];
                    if (resources[i].GetEntityId() == id)
                    {
                        return resources[i];
                    }
                }
            }

            // Если нашли ресурс определённого типа по имени — считаем это совпадением
            if (findFromName != null)
            {
                return findFromName;
            }

#if UNITY_EDITOR
            var assetPath = AssetDatabase.GetAssetPath(id);
            if (!string.IsNullOrEmpty(assetPath))
            {
                var resource = AssetDatabase.LoadAssetAtPath(assetPath, resourceType);
                if (resource != null && resource.name == name)
                {
                    Debug.LogFormat("Resource <{0}> find by NAME - load from assets", resource.name);
                    return resource;
                }
            }
#endif
            return null;
        }

        /// <summary>
        /// Поиск ресурса по имени
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="name">Имя ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static TResource Find<TResource>(string name) where TResource : UnityEngine.Object
        {
            return Find(name, typeof(TResource)) as TResource;
        }

        /// <summary>
        /// Поиск ресурса по имени
        /// </summary>
        /// <param name="name">Имя ресурса</param>
        /// <param name="resourceType">Тип ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static UnityEngine.Object Find(string name, Type resourceType)
        {
            var resources = Resources.FindObjectsOfTypeAll(resourceType);
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].name == name)
                {
                    return resources[i];
                }
            }

#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{resourceType.Name}");
            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var resourceName = Path.GetFileNameWithoutExtension(path);
                if (resourceName != name)
                {
                    continue;
                }

                var resource = AssetDatabase.LoadAssetAtPath(path, resourceType);
                if (resource != null)
                {
                    Debug.LogFormat("Resource <{0}> find by NAME - load from assets", resource.name);
                    return resource;
                }
            }
#endif
            return null;
        }

        /// <summary>
        /// Поиск ресурса по вхождению имени
        /// </summary>
        /// <typeparam name="TResource">Тип ресурса</typeparam>
        /// <param name="name">Имя ресурса</param>
        /// <returns>Найденный ресурс или null</returns>
        public static TResource FindMatch<TResource>(string name) where TResource : UnityEngine.Object
        {
            var resources = Resources.FindObjectsOfTypeAll<TResource>();
            for (var i = 0; i < resources.Length; i++)
            {
                if (resources[i].name.Contains(name, StringComparison.Ordinal))
                {
                    return resources[i];
                }
            }

#if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{typeof(TResource).Name}");
            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var resourceName = Path.GetFileNameWithoutExtension(path);
                if (!resourceName.Contains(name, StringComparison.Ordinal))
                {
                    continue;
                }

                var resource = AssetDatabase.LoadAssetAtPath<TResource>(path);
                if (resource != null)
                {
                    Debug.LogFormat("Resource <{0}> find match by NAME - load from assets", resource.name);
                    return resource;
                }
            }
#endif
            return null;
        }
        #endregion
    }

    /**@}*/
}
#endif
