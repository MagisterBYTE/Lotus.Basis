#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности базового компонента Unity
    /// </summary>
    public static class XComponentExtension
    {
        #region Main methods
        /// <summary>
        /// Гарантированное получение компонента (который может быть только один на игровом объекте)
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент</param>
        /// <returns>Добавленный или существующий компонент</returns>
        public static TComponent EnsureComponent<TComponent>(this Component @this) where TComponent : Component
        {
            TComponent existComponent = @this.gameObject.GetComponent<TComponent>();
            if (existComponent == null)
            {
                existComponent = @this.gameObject.AddComponent<TComponent>();
            }

            return existComponent;
        }


        /// <summary>
        /// Удаление компонента
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент</param>
        public static void RemoveComponent<TComponent>(this Component @this) where TComponent : Component
        {
            TComponent deletingComponent = @this.GetComponent<TComponent>();
            if (deletingComponent != null)
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    // Удаляем компонент
                    GameObject.Destroy(deletingComponent);
                }
                else
                {
                    // Удаляем компонент
                    GameObject.DestroyImmediate(deletingComponent);
                }
#else
                // Удаляем компонент
                GameObject.Destroy(deletingComponent);
#endif
            }
        }
        #endregion

        #region Child methods
        /// <summary>
        /// Добавление компонента к дочернему объекту
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент</param>
        /// <param name="childName">Имя дочернего игрового объекта</param>
        /// <returns>Добавленный или существующий компонент</returns>
        public static TComponent AddChildComponent<TComponent>(this Component @this, string childName) where TComponent : Component
        {
            TComponent addingComponent = null;
            for (var i = 0; i < @this.transform.childCount; i++)
            {
                Transform transform = @this.transform.GetChild(i);
                if (transform.name == childName)
                {
                    addingComponent = transform.GetComponent<TComponent>();
                    if (addingComponent == null)
                    {
                        addingComponent = transform.gameObject.AddComponent<TComponent>();
                    }

                    return addingComponent;
                }
            }

            var goChild = new GameObject(childName, typeof(TComponent));
            goChild.transform.SetParent(@this.transform, false);
            goChild.layer = @this.gameObject.layer;
            addingComponent = goChild.GetComponent<TComponent>();
            return addingComponent;
        }


        /// <summary>
        /// Поиск компонента определенного типа среди дочерних объектов по имени
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент</param>
        /// <param name="childName">Имя дочернего игрового объекта</param>
        /// <param name="inChildren">Дополнительный поиск среди дочерних объектов второго уровня</param>
        /// <returns>Найденный компонент или null</returns>
        public static TComponent FindComponentInChildren<TComponent>(this Component @this, string childName, bool inChildren = false) where TComponent : Component
        {
            // Поиск среди дочерних объектов
            Transform thisTransform = @this.transform;
            for (var i = 0; i < thisTransform.childCount; i++)
            {
                TComponent findComponent = thisTransform.GetChild(i).GetComponent<TComponent>();
                if (findComponent != null && findComponent.name == childName)
                {
                    return findComponent;
                }
            }

            if (inChildren)
            {
                for (var ic = 0; ic < thisTransform.childCount; ic++)
                {
                    Transform transformChild = thisTransform.GetChild(ic).transform;
                    if (transformChild != null && transformChild.childCount > 0)
                    {
                        for (var c = 0; c < transformChild.childCount; c++)
                        {
                            TComponent findComponent = transformChild.GetChild(c).GetComponent<TComponent>();
                            if (findComponent != null && findComponent.name == childName)
                            {
                                return findComponent;
                            }
                        }
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
