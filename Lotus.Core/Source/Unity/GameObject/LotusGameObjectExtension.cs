#if UNITY_2017_1_OR_NEWER
using System;
using System.Text;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityGameObject
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений игрового объекта Unity
    /// </summary>
    public static class XGameObjectExtension
    {
        #region Main methods
        /// <summary>
        /// Проверка на статус префаба игрового объекта
        /// </summary>
        /// <param name="this">Игровой объект</param>
        /// <returns>Статус префаба игрового объекта</returns>
        public static bool IsPrefab(this GameObject @this)
        {
            return @this.scene.name == null;
        }


        /// <summary>
        /// Получение полного пути игрового объект в иерархии игровых объектов
        /// </summary>
        /// <param name="this">Игровой объект</param>
        /// <returns>Полный путь</returns>
        public static string GetPathScene(this GameObject @this)
        {
            var path = new StringBuilder(40);
            while (@this.transform.parent != null)
            {
                @this = @this.transform.parent.gameObject;
                if (@this.transform.parent != null)
                {
                    path.Insert(0, @this.name);
                    path.Insert(0, "/");
                }
                else
                {
                    path.Insert(0, @this.name);
                }
            }

            return path.ToString();
        }


        /// <summary>
        /// Гарантирование обеспечение компонента (который может быть только один на игровом объекте)
        /// </summary>
        /// <param name="this">Игровой объект</param>
        /// <param name="typeComponent">Тип компонента</param>
        /// <returns>Добавленный или существующий компонент</returns>
        public static Component EnsureComponent(this GameObject @this, Type typeComponent)
        {
#if UNITY_EDITOR
            if (@this == null)
            {
                return null;
            }
#endif
            Component component = @this.GetComponent(typeComponent);
            if (component == null)
            {
                component = @this.AddComponent(typeComponent);
            }

            return component;
        }
        #endregion

        #region Search methods
        /// <summary>
        /// Поиск игрового среди дочерних объектов по указанному имени
        /// </summary>
        /// <param name="this">Игровой объект</param>
        /// <param name="childName">Имя дочернего игрового объекта</param>
        /// <param name="inChildren">Дополнительный поиск среди дочерних объектов второго уровня</param>
        /// <returns>Найденный игровой объект или null</returns>
        public static GameObject FindInChildren(this GameObject @this, string childName, bool inChildren = false)
        {
            // Поиск среди дочерних объектов
            Transform thisTransform = @this.transform;
            for (var i = 0; i < thisTransform.childCount; i++)
            {
                Transform tran = thisTransform.GetChild(i);
                if (tran != null && tran.name == childName)
                {
                    return tran.gameObject;
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
                            Transform tran = transformChild.GetChild(c);
                            if (tran != null && tran.name == childName)
                            {
                                return tran.gameObject;
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
