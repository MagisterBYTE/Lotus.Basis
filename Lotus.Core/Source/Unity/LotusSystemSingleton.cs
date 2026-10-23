#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnity
    *@{*/
    /// <summary>
    /// Режим поведения при дублировании экземпляров объектов
    /// </summary>
    public enum TSingletonDestroyMode
    {
        /// <summary>
        /// Ничего не делать
        /// </summary>
        None,

        /// <summary>
        /// Удалить полностью игровой объект на котором находиться компонент экземпляра
        /// </summary>
        GameObject,

        /// <summary>
        /// Удалить только компонент экземпляра
        /// </summary>
        Component
    }


    /// <summary>
    /// Определение интерфейса паттерна Singleton
    /// </summary>
    public interface ILotusSingleton
    {
        /// <summary>
        /// Статус основного экземпляра
        /// </summary>
        /// <remarks>
        /// <para>
        /// Статус имеет значение только если используется несколько объектов.
        /// Например, в основной и двух дополнительный сценах присутствуют объекты.
        /// Когда ведется работа в режиме редактора и тестируется, например, любая сцена то объект присутствует в единственном числе - это обеспечивается конструкций языка и платформой.
        /// </para>
        /// <para>
        /// Однако если мы используем концепцию при которой одна сцена всегда загружена, а остальные загружаются и выгружаются
        /// по мере необходимости, то неизбежно в приложении возникнуть два объекта.
        /// Указанной свойство как раз и позволяет учесть такой вариант и показать какой объект основной, а какой можно удалить (доступна операция зависит от свойства)
        /// </para>
        /// </remarks>
        bool IsMainInstance { get; set; }

        /// <summary>
        /// Статус удаления игрового объекта
        /// </summary>
        /// <remarks>
        /// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
        /// </remarks>
        TSingletonDestroyMode DestroyMode { get; set; }
    }


    /// <summary>
    /// Реализация паттерна Singleton
    /// </summary>
    /// <remarks>
    /// Указанная реализация обеспечивает единство объекта на уровне приложения.
    /// В Unity 2020+/6 для поиска экземпляров на сцене используется <see cref="Object.FindObjectsByType{T}"/>
    /// с <see cref="FindObjectsInactive"/>; <see cref="Object.FindObjectsOfType{T}"/> устарел.
    /// </remarks>
    [Serializable]
    public class LotusSystemSingleton<TMonoBehaviour> : MonoBehaviour where TMonoBehaviour : MonoBehaviour, ILotusSingleton
    {
        #region Static fields
        // Глобальный экземпляр
        internal static TMonoBehaviour _instance;
        #endregion

        #region Static properties
        /// <summary>
        /// Глобальный экземпляр
        /// </summary>
        public static TMonoBehaviour Instance
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Статус создания глобального экземпляра
        /// </summary>
        public static bool IsCreatedInstance
        {
            get { return _instance != null; }
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Установка глобального экземпляра
        /// </summary>
        /// <param name="instances">Массив экземпляров</param>
        protected static void SetInstance(TMonoBehaviour[] instances)
        {
            for (var i = 0; i < instances.Length; i++)
            {
                // Если это не префаб
                if (!instances[i].gameObject.IsPrefab())
                {
                    // Если это не основной глобальный экземпляр до мы его удаляем
                    if (instances[i].IsMainInstance)
                    {
                        _instance = instances[i];
                    }
                    else
                    {
                        var monoName = typeof(TMonoBehaviour).Name;
                        switch (instances[i].DestroyMode)
                        {
                            case TSingletonDestroyMode.None:
                                {
                                    Debug.LogWarningFormat("Instance: {0} > 1, object: {1}", monoName,
                                        instances[i].gameObject.name);
                                }
                                break;
                            case TSingletonDestroyMode.GameObject:
                                {
                                    Debug.LogWarningFormat("Instance: {0} > 1, delete the object: {1}", monoName,
                                        instances[i].gameObject.name);

                                    Destroy(instances[i].gameObject);
                                }
                                break;
                            case TSingletonDestroyMode.Component:
                                {
                                    Debug.LogWarningFormat("Instance: {0} > 1, delete the component: {1}", monoName,
                                        instances[i].gameObject.name);

                                    Destroy(instances[i]);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Создание глобального экземпляра
        /// </summary>
        protected static void CreateInstance()
        {
            // Если глобальный экземпляр пустой
            if (_instance == null)
            {
                // Получаем все компоненты данного типа (активные объекты сцены)
                var instances = FindSceneInstances(false);
                if (instances.Length == 1)
                {
                    // Все хорошо
                    _instance = instances[0];
                    return;
                }

                // Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
                // Такой случай может быть при загрузки другой сцены
                if (instances.Length > 1)
                {
                    SetInstance(instances);
                }
            }

            // Глобальный экземпляр мы до сих пор не нашли
            // Рассмотрим вариант когда игровой объект выключен
            if (_instance == null)
            {
                var instances = FindSceneInstances(true);
                if (instances.Length == 1)
                {
                    // Все хорошо
                    _instance = instances[0];
                    return;
                }

                // Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
                // Такой случай может быть при загрузки другой сцены
                if (instances.Length > 1)
                {
                    SetInstance(instances);
                }
            }

            // Ну значит его точно нет
            if (_instance == null)
            {
                var typeSingleton = typeof(TMonoBehaviour);
                var go = new GameObject(typeSingleton.Name.Replace("Lotus", ""), typeSingleton);
                _instance = go.GetComponent<TMonoBehaviour>();
            }
        }

        /// <summary>
        /// Проверка и удаление дубликатов глобального экземпляра
        /// </summary>
        /// <remarks>
        /// Во время разработки глобальные экземпляры являются таковыми только в пределах активной сцены.
        /// Однако если использовать методику при которой есть главной сцена и второстепенные сцены загружаемые/выгружаемые по мере
        /// необходимости может возникнуть ситуация, когда в приложение работают сразу несколько экземпляров.
        /// Это неправильно. Для этого данный метод удаляет все дубликаты и оставляет только один - основной экземпляр
        /// свойство которого <see cref="ILotusSingleton.IsMainInstance"/> равно True
        /// </remarks>
        /// <returns>Статус удаления дубликата</returns>
        protected static bool CheckDuplicate()
        {
            // Получаем все компоненты данного типа
            var instances = FindSceneInstances(false);
            if (instances.Length == 1)
            {
                // Все хорошо
                _instance = instances[0];
                return false;
            }

            // Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
            // Такой случай может быть при загрузки другой сцены
            if (instances.Length > 1)
            {
                SetInstance(instances);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка и удаление дубликатов глобального экземпляра
        /// </summary>
        /// <returns>Статус удаления дубликата</returns>
        [Obsolete("Опечатка в имени. Используйте CheckDuplicate")]
        protected static bool CheckDublicate()
        {
            return CheckDuplicate();
        }

        /// <summary>
        /// Поиск экземпляров компонента на сцене
        /// </summary>
        /// <param name="includeInactive">Включать ли неактивные объекты</param>
        /// <returns>Массив найденных экземпляров</returns>
        private static TMonoBehaviour[] FindSceneInstances(bool includeInactive)
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<TMonoBehaviour>(
                includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude);
#elif UNITY_2020_1_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<TMonoBehaviour>(
                includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                FindObjectsSortMode.None);
#else
            if (includeInactive)
            {
                return Resources.FindObjectsOfTypeAll<TMonoBehaviour>();
            }

            return UnityEngine.Object.FindObjectsOfType<TMonoBehaviour>();
#endif
        }
        #endregion
    }
    /**@}*/
}
#endif
