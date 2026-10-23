#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента Renderer
    /// </summary>
    public static class XRendererExtension
    {
        #region Main methods
        /// <summary>
        /// Проверка на видимость объекта от указанной камера
        /// </summary>
        /// <remarks>
        /// Credits: http://wiki.unity3d.com/index.php?title=IsVisibleFrom
        /// </remarks>
        /// <param name="this">Проверяемый объект</param>
        /// <param name="camera">Камера</param>
        /// <returns>Статус видимости</returns>
        public static bool IsVisibleFrom(this Renderer @this, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, @this.bounds);
        }


        /// <summary>
        /// Получение альфы компонента цвета рендера спрайтов
        /// </summary>
        /// <param name="this">Рендер спрайтов</param>
        /// <returns>Альфа компонента цвета</returns>
        public static float GetAlphaColor(this SpriteRenderer @this)
        {
            return @this.color.a;
        }


        /// <summary>
        /// Установка альфы компонента цвета рендера спрайтов
        /// </summary>
        /// <param name="this">Рендер спрайтов</param>
        /// <param name="alpha">Альфа компонента цвета</param>
        public static void SetAlphaColor(this SpriteRenderer @this, float alpha)
        {
            @this.color = new Color(@this.color.r, @this.color.g, @this.color.b, alpha);
        }
        #endregion
    }
    /**@}*/
}
#endif
