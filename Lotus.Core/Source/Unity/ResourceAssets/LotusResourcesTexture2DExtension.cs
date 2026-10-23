#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lotus.Core
{
    /** \addtogroup CoreUnityResource
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений ресурса Texture2D
    /// </summary>
    public static class XTexture2DExtension
    {
        #region Main methods
        /// <summary>
        /// Получение основного спрайта текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <returns>Спрайт</returns>
        public static Sprite GetSpriteInDesign(this Texture2D @this)
        {
#if UNITY_EDITOR
            var allSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(@this));
            if (allSprites.Length > 0 && allSprites[0] is Sprite sprite)
            {
                return sprite;
            }

            return null;
#else
            return null;
#endif
        }

        /// <summary>
        /// Получение массива спрайтов текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <returns>Массив спрайтов</returns>
        public static Sprite[] GetSpritesInDesign(this Texture2D @this)
        {
#if UNITY_EDITOR
            var allSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(@this));
            if (allSprites.Length == 0)
            {
                return null;
            }

            var sprites = new Sprite[allSprites.Length];
            for (var i = 0; i < allSprites.Length; i++)
            {
                sprites[i] = allSprites[i] as Sprite;
            }

            return sprites;
#else
            return null;
#endif
        }

        /// <summary>
        /// Получение бордюров основного спрайта текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <returns>Бордюры</returns>
        public static Vector4 GetSpriteBorderInDesign(this Texture2D @this)
        {
#if UNITY_EDITOR
            var allSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(@this));
            if (allSprites.Length > 0 && allSprites[0] is Sprite sprite)
            {
                return sprite.border;
            }

            return Vector4.zero;
#else
            return Vector4.zero;
#endif
        }

        /// <summary>
        /// Изменение контраста текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <param name="value">Коэффициент изменения контраста от 0 до 1</param>
        public static void ChangeContrastInDesign(this Texture2D @this, float value)
        {
            var pixels = @this.GetPixels();

            // Средняя яркость
            double brightness = 0;
            for (var y = 0; y < @this.height; y++)
            {
                for (var x = 0; x < @this.width; x++)
                {
                    var pixel = pixels[x + (y * @this.width)];
                    brightness += (pixel.r * 0.299) + (pixel.g * 0.587) + (pixel.b * 0.114);
                }
            }

            var averageBrightness = (float)(brightness / (@this.height * @this.width));

            for (var y = 0; y < @this.height; y++)
            {
                for (var x = 0; x < @this.width; x++)
                {
                    var index = x + (y * @this.width);
                    var pixel = pixels[index];
                    pixel.r += (pixel.r - averageBrightness) * value;
                    pixel.g += (pixel.g - averageBrightness) * value;
                    pixel.b += (pixel.b - averageBrightness) * value;
                    pixels[index] = pixel;
                }
            }

            @this.SetPixels(pixels);
            @this.Apply();
        }

        /// <summary>
        /// Изменение яркости текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <param name="value">Коэффициент изменения яркости от 0 до 1</param>
        public static void ChangeBrightnessInDesign(this Texture2D @this, float value)
        {
            var pixels = @this.GetPixels();

            for (var y = 0; y < @this.height; y++)
            {
                for (var x = 0; x < @this.width; x++)
                {
                    var index = x + (y * @this.width);
                    var pixel = pixels[index];
                    Color.RGBToHSV(pixel, out var hue, out var saturation, out var brightness);
                    brightness += value;
                    pixels[index] = Color.HSVToRGB(hue, saturation, brightness);
                }
            }

            @this.SetPixels(pixels);
            @this.Apply();
        }

        /// <summary>
        /// Изменение насыщенности текстуры в режиме редактора
        /// </summary>
        /// <param name="this">Текстура</param>
        /// <param name="value">Коэффициент насыщенности яркости от 0 до 1</param>
        public static void ChangeSaturationInDesign(this Texture2D @this, float value)
        {
            var pixels = @this.GetPixels();

            for (var y = 0; y < @this.height; y++)
            {
                for (var x = 0; x < @this.width; x++)
                {
                    var index = x + (y * @this.width);
                    var pixel = pixels[index];
                    Color.RGBToHSV(pixel, out var hue, out var saturation, out var brightness);
                    saturation += value;
                    pixels[index] = Color.HSVToRGB(hue, saturation, brightness);
                }
            }

            @this.SetPixels(pixels);
            @this.Apply();
        }
        #endregion
    }

    /**@}*/
}
#endif
