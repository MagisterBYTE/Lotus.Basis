#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityComponent
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений функциональности компонента Camera
    /// </summary>
    public static class XCameraExtension
    {
        #region Main methods
        /// <summary>
        /// Добавление слоя видимости отображаемых камерой
        /// </summary>
        /// <param name="this">Камера</param>
        /// <param name="layer">Флаг слоя</param>
        public static void LayerAdd(this Camera @this, int layer)
        {
            @this.cullingMask |= (1 << layer);
        }


        /// <summary>
        /// Удаления слоя видимости отображаемых камерой
        /// </summary>
        /// <param name="this">Камера</param>
        /// <param name="layer">Флаг слоя</param>
        public static void LayerRemove(this Camera @this, int layer)
        {
            @this.cullingMask &= ~(1 << layer);
        }


        /// <summary>
        /// Установка размера камеры для ортографического режима
        /// </summary>
        /// <param name="this">Камера</param>
        /// <param name="pixelsPerUnit">Количество пикселей на единицу в мировых координатах</param>
        /// <param name="zoom">Увеличение</param>
        public static void SetOrthoSize(this Camera @this, float pixelsPerUnit = 100.0f, float zoom = 1.0f)
        {
            var h = LotusSystemSettingsService.Instance.ScreenHeight;
            @this.orthographicSize = h / 2f / pixelsPerUnit / zoom;
        }


        /// <summary>
        /// Получение высоты ортографической камеры
        /// </summary>
        /// <param name="this">Камера</param>
        /// <returns>Высота ортографической камеры</returns>
        public static float GetOrthoHeight(this Camera @this)
        {
            return @this.orthographicSize;
        }


        /// <summary>
        /// Получение ширины ортографической камеры
        /// </summary>
        /// <param name="this">Камера</param>
        /// <returns>Ширина ортографической камеры</returns>
        public static float GetOrthoWidth(this Camera @this)
        {
            return @this.orthographicSize * @this.aspect;
        }
        #endregion
    }
    /**@}*/
}
#endif
