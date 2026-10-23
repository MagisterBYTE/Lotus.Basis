#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы функциональности компонента Behaviour
    /// </summary>
    public static class XBehaviourExtension
    {
        #region Main methods
        /// <summary>
        /// Включение компонента
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент логики</param>
        /// <param name="enabled">Статус включения компонента</param>
        public static void EnabledComponent<TComponent>(this Behaviour @this, bool enabled) where TComponent : Behaviour
        {
            TComponent enablingComponent = @this.GetComponent<TComponent>();
            if (enablingComponent != null)
            {
                enablingComponent.enabled = enabled;
            }
        }


        /// <summary>
        /// Автоматическое добавление/удаление компонента
        /// </summary>
        /// <typeparam name="TComponent">Тип компонента</typeparam>
        /// <param name="this">Базовый компонент логики</param>
        /// <param name="enabled">Статус добавления/удаление компонента</param>
        public static void AutoComponent<TComponent>(this Behaviour @this, bool enabled) where TComponent : Behaviour
        {
            TComponent component = @this.GetComponent<TComponent>();
            if (component != null)
            {
                if (enabled == false)
                {
#if UNITY_EDITOR
                    component.enabled = false;
                    if (UnityEditor.EditorApplication.isPlaying)
                    {
                        // Удаляем компонент
                        GameObject.Destroy(component);
                    }
                    else
                    {
                        // Удаляем компонент
                        GameObject.DestroyImmediate(component);
                    }
#else
                    // Удаляем компонент
                    GameObject.Destroy(component);
#endif
                }
            }
            else
            {
                if (enabled)
                {
                    component = @this.gameObject.AddComponent<TComponent>();
                }
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
