#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    
    /** 
     * \defgroup CoreUnityHelpers Вспомогательная подсистема Unity
     * \ingroup Core
     * \brief Вспомогательная подсистема содержит хелперы расширяющие возможности базовых типов Unity.
     * @{
     */
    
    /// <summary>
    /// Статический класс для расширения функциональности типа Color
    /// </summary>
    
    public static class XUnityColor
    {
        #region Constants
        /// <summary>
        /// Нулевой цвет
        /// </summary>
        [Obsolete("Дублирует Transparent / Color.clear. Используйте Transparent или Color.clear")]
        public static readonly Color Zero = XUnityColor.FromBGRA(0x00000000);

        /// <summary>
        /// Прозрачный цвет
        /// </summary>
        public static readonly Color Transparent = XUnityColor.FromBGRA(0x00000000);

        /// <summary>
        /// Цвет - AliceBlue
        /// </summary>
        public static readonly Color AliceBlue = XUnityColor.FromBGRA(0xFFF0F8FF);

        /// <summary>
        /// Цвет - AntiqueWhite
        /// </summary>
        public static readonly Color AntiqueWhite = XUnityColor.FromBGRA(0xFFFAEBD7);

        /// <summary>
        /// Цвет - Aqua
        /// </summary>
        public static readonly Color Aqua = XUnityColor.FromBGRA(0xFF00FFFF);

        /// <summary>
        /// Цвет - Aquamarine
        /// </summary>
        public static readonly Color Aquamarine = XUnityColor.FromBGRA(0xFF7FFFD4);

        /// <summary>
        /// Цвет - Azure
        /// </summary>
        public static readonly Color Azure = XUnityColor.FromBGRA(0xFFF0FFFF);

        /// <summary>
        /// Цвет - Beige
        /// </summary>
        public static readonly Color Beige = XUnityColor.FromBGRA(0xFFF5F5DC);

        /// <summary>
        /// Цвет - Bisque
        /// </summary>
        public static readonly Color Bisque = XUnityColor.FromBGRA(0xFFFFE4C4);

        /// <summary>
        /// Цвет - Black
        /// </summary>
        public static readonly Color Black = XUnityColor.FromBGRA(0xFF000000);

        /// <summary>
        /// Цвет - BlanchedAlmond
        /// </summary>
        public static readonly Color BlanchedAlmond = XUnityColor.FromBGRA(0xFFFFEBCD);

        /// <summary>
        /// Цвет - Blue
        /// </summary>
        public static readonly Color Blue = XUnityColor.FromBGRA(0xFF0000FF);

        /// <summary>
        /// Цвет - BlueViolet
        /// </summary>
        public static readonly Color BlueViolet = XUnityColor.FromBGRA(0xFF8A2BE2);

        /// <summary>
        /// Цвет - Brown
        /// </summary>
        public static readonly Color Brown = XUnityColor.FromBGRA(0xFFA52A2A);

        /// <summary>
        /// Цвет - BurlyWood
        /// </summary>
        public static readonly Color BurlyWood = XUnityColor.FromBGRA(0xFFDEB887);

        /// <summary>
        /// Цвет - CadetBlue
        /// </summary>
        public static readonly Color CadetBlue = XUnityColor.FromBGRA(0xFF5F9EA0);

        /// <summary>
        /// Цвет - Chartreuse
        /// </summary>
        public static readonly Color Chartreuse = XUnityColor.FromBGRA(0xFF7FFF00);

        /// <summary>
        /// Цвет - Chocolate
        /// </summary>
        public static readonly Color Chocolate = XUnityColor.FromBGRA(0xFFD2691E);

        /// <summary>
        /// Цвет - Coral
        /// </summary>
        public static readonly Color Coral = XUnityColor.FromBGRA(0xFFFF7F50);

        /// <summary>
        /// Цвет - CornflowerBlue
        /// </summary>
        public static readonly Color CornflowerBlue = XUnityColor.FromBGRA(0xFF6495ED);

        /// <summary>
        /// Цвет - Cornsilk
        /// </summary>
        public static readonly Color Cornsilk = XUnityColor.FromBGRA(0xFFFFF8DC);

        /// <summary>
        /// Цвет - Crimson
        /// </summary>
        public static readonly Color Crimson = XUnityColor.FromBGRA(0xFFDC143C);

        /// <summary>
        /// Цвет - Cyan
        /// </summary>
        public static readonly Color Cyan = XUnityColor.FromBGRA(0xFF00FFFF);

        /// <summary>
        /// Цвет - DarkBlue
        /// </summary>
        public static readonly Color DarkBlue = XUnityColor.FromBGRA(0xFF00008B);

        /// <summary>
        /// Цвет - DarkCyan
        /// </summary>
        public static readonly Color DarkCyan = XUnityColor.FromBGRA(0xFF008B8B);

        /// <summary>
        /// Цвет - DarkGoldenrod
        /// </summary>
        public static readonly Color DarkGoldenrod = XUnityColor.FromBGRA(0xFFB8860B);

        /// <summary>
        /// Цвет - DarkGray
        /// </summary>
        public static readonly Color DarkGray = XUnityColor.FromBGRA(0xFFA9A9A9);

        /// <summary>
        /// Цвет - DarkGreen
        /// </summary>
        public static readonly Color DarkGreen = XUnityColor.FromBGRA(0xFF006400);

        /// <summary>
        /// Цвет - DarkKhaki
        /// </summary>
        public static readonly Color DarkKhaki = XUnityColor.FromBGRA(0xFFBDB76B);

        /// <summary>
        /// Цвет - DarkMagenta
        /// </summary>
        public static readonly Color DarkMagenta = XUnityColor.FromBGRA(0xFF8B008B);

        /// <summary>
        /// Цвет - DarkOliveGreen
        /// </summary>
        public static readonly Color DarkOliveGreen = XUnityColor.FromBGRA(0xFF556B2F);

        /// <summary>
        /// Цвет - DarkOrange
        /// </summary>
        public static readonly Color DarkOrange = XUnityColor.FromBGRA(0xFFFF8C00);

        /// <summary>
        /// Цвет - DarkOrchid
        /// </summary>
        public static readonly Color DarkOrchid = XUnityColor.FromBGRA(0xFF9932CC);

        /// <summary>
        /// Цвет - DarkRed
        /// </summary>
        public static readonly Color DarkRed = XUnityColor.FromBGRA(0xFF8B0000);

        /// <summary>
        /// Цвет - DarkSalmon
        /// </summary>
        public static readonly Color DarkSalmon = XUnityColor.FromBGRA(0xFFE9967A);

        /// <summary>
        /// Цвет - DarkSeaGreen
        /// </summary>
        public static readonly Color DarkSeaGreen = XUnityColor.FromBGRA(0xFF8FBC8B);

        /// <summary>
        /// Цвет - DarkSlateBlue
        /// </summary>
        public static readonly Color DarkSlateBlue = XUnityColor.FromBGRA(0xFF483D8B);

        /// <summary>
        /// Цвет - DarkSlateGray
        /// </summary>
        public static readonly Color DarkSlateGray = XUnityColor.FromBGRA(0xFF2F4F4F);

        /// <summary>
        /// Цвет - DarkTurquoise
        /// </summary>
        public static readonly Color DarkTurquoise = XUnityColor.FromBGRA(0xFF00CED1);

        /// <summary>
        /// Цвет - DarkViolet
        /// </summary>
        public static readonly Color DarkViolet = XUnityColor.FromBGRA(0xFF9400D3);

        /// <summary>
        /// Цвет - DeepPink
        /// </summary>
        public static readonly Color DeepPink = XUnityColor.FromBGRA(0xFFFF1493);

        /// <summary>
        /// Цвет - DeepSkyBlue
        /// </summary>
        public static readonly Color DeepSkyBlue = XUnityColor.FromBGRA(0xFF00BFFF);

        /// <summary>
        /// Цвет - DimGray
        /// </summary>
        public static readonly Color DimGray = XUnityColor.FromBGRA(0xFF696969);

        /// <summary>
        /// Цвет - DodgerBlue
        /// </summary>
        public static readonly Color DodgerBlue = XUnityColor.FromBGRA(0xFF1E90FF);

        /// <summary>
        /// Цвет - Firebrick
        /// </summary>
        public static readonly Color Firebrick = XUnityColor.FromBGRA(0xFFB22222);

        /// <summary>
        /// Цвет - FloralWhite
        /// </summary>
        public static readonly Color FloralWhite = XUnityColor.FromBGRA(0xFFFFFAF0);

        /// <summary>
        /// Цвет - ForestGreen
        /// </summary>
        public static readonly Color ForestGreen = XUnityColor.FromBGRA(0xFF228B22);

        /// <summary>
        /// Цвет - Fuchsia
        /// </summary>
        public static readonly Color Fuchsia = XUnityColor.FromBGRA(0xFFFF00FF);

        /// <summary>
        /// Цвет - Gainsboro
        /// </summary>
        public static readonly Color Gainsboro = XUnityColor.FromBGRA(0xFFDCDCDC);

        /// <summary>
        /// Цвет - GhostWhite
        /// </summary>
        public static readonly Color GhostWhite = XUnityColor.FromBGRA(0xFFF8F8FF);

        /// <summary>
        /// Цвет - Gold
        /// </summary>
        public static readonly Color Gold = XUnityColor.FromBGRA(0xFFFFD700);

        /// <summary>
        /// Цвет - Goldenrod
        /// </summary>
        public static readonly Color Goldenrod = XUnityColor.FromBGRA(0xFFDAA520);

        /// <summary>
        /// Цвет - Gray
        /// </summary>
        public static readonly Color Gray = XUnityColor.FromBGRA(0xFF808080);

        /// <summary>
        /// Цвет - Green
        /// </summary>
        public static readonly Color Green = XUnityColor.FromBGRA(0xFF008000);

        /// <summary>
        /// Цвет - GreenYellow
        /// </summary>
        public static readonly Color GreenYellow = XUnityColor.FromBGRA(0xFFADFF2F);

        /// <summary>
        /// Цвет - Honeydew
        /// </summary>
        public static readonly Color Honeydew = XUnityColor.FromBGRA(0xFFF0FFF0);

        /// <summary>
        /// Цвет - HotPink
        /// </summary>
        public static readonly Color HotPink = XUnityColor.FromBGRA(0xFFFF69B4);

        /// <summary>
        /// Цвет - IndianRed
        /// </summary>
        public static readonly Color IndianRed = XUnityColor.FromBGRA(0xFFCD5C5C);

        /// <summary>
        /// Цвет - Indigo
        /// </summary>
        public static readonly Color Indigo = XUnityColor.FromBGRA(0xFF4B0082);

        /// <summary>
        /// Цвет - Ivory
        /// </summary>
        public static readonly Color Ivory = XUnityColor.FromBGRA(0xFFFFFFF0);

        /// <summary>
        /// Цвет - Khaki
        /// </summary>
        public static readonly Color Khaki = XUnityColor.FromBGRA(0xFFF0E68C);

        /// <summary>
        /// Цвет - Lavender
        /// </summary>
        public static readonly Color Lavender = XUnityColor.FromBGRA(0xFFE6E6FA);

        /// <summary>
        /// Цвет - LavenderBlush
        /// </summary>
        public static readonly Color LavenderBlush = XUnityColor.FromBGRA(0xFFFFF0F5);

        /// <summary>
        /// Цвет - LawnGreen
        /// </summary>
        public static readonly Color LawnGreen = XUnityColor.FromBGRA(0xFF7CFC00);

        /// <summary>
        /// Цвет - LemonChiffon
        /// </summary>
        public static readonly Color LemonChiffon = XUnityColor.FromBGRA(0xFFFFFACD);

        /// <summary>
        /// Цвет - LightBlue
        /// </summary>
        public static readonly Color LightBlue = XUnityColor.FromBGRA(0xFFADD8E6);

        /// <summary>
        /// Цвет - LightCoral
        /// </summary>
        public static readonly Color LightCoral = XUnityColor.FromBGRA(0xFFF08080);

        /// <summary>
        /// Цвет - LightCyan
        /// </summary>
        public static readonly Color LightCyan = XUnityColor.FromBGRA(0xFFE0FFFF);

        /// <summary>
        /// Цвет - LightGoldenrodYellow
        /// </summary>
        public static readonly Color LightGoldenrodYellow = XUnityColor.FromBGRA(0xFFFAFAD2);

        /// <summary>
        /// Цвет - LightGray
        /// </summary>
        public static readonly Color LightGray = XUnityColor.FromBGRA(0xFFD3D3D3);

        /// <summary>
        /// Цвет - LightGreen
        /// </summary>
        public static readonly Color LightGreen = XUnityColor.FromBGRA(0xFF90EE90);

        /// <summary>
        /// Цвет - LightPink
        /// </summary>
        public static readonly Color LightPink = XUnityColor.FromBGRA(0xFFFFB6C1);

        /// <summary>
        /// Цвет - LightSalmon
        /// </summary>
        public static readonly Color LightSalmon = XUnityColor.FromBGRA(0xFFFFA07A);

        /// <summary>
        /// Цвет - LightSeaGreen
        /// </summary>
        public static readonly Color LightSeaGreen = XUnityColor.FromBGRA(0xFF20B2AA);

        /// <summary>
        /// Цвет - LightSkyBlue
        /// </summary>
        public static readonly Color LightSkyBlue = XUnityColor.FromBGRA(0xFF87CEFA);

        /// <summary>
        /// Цвет - LightSlateGray
        /// </summary>
        public static readonly Color LightSlateGray = XUnityColor.FromBGRA(0xFF778899);

        /// <summary>
        /// Цвет - LightSteelBlue
        /// </summary>
        public static readonly Color LightSteelBlue = XUnityColor.FromBGRA(0xFFB0C4DE);

        /// <summary>
        /// Цвет - LightYellow
        /// </summary>
        public static readonly Color LightYellow = XUnityColor.FromBGRA(0xFFFFFFE0);

        /// <summary>
        /// Цвет - Lime
        /// </summary>
        public static readonly Color Lime = XUnityColor.FromBGRA(0xFF00FF00);

        /// <summary>
        /// Цвет - LimeGreen
        /// </summary>
        public static readonly Color LimeGreen = XUnityColor.FromBGRA(0xFF32CD32);

        /// <summary>
        /// Цвет - Linen
        /// </summary>
        public static readonly Color Linen = XUnityColor.FromBGRA(0xFFFAF0E6);

        /// <summary>
        /// Цвет - Magenta
        /// </summary>
        public static readonly Color Magenta = XUnityColor.FromBGRA(0xFFFF00FF);

        /// <summary>
        /// Цвет - Maroon
        /// </summary>
        public static readonly Color Maroon = XUnityColor.FromBGRA(0xFF800000);

        /// <summary>
        /// Цвет - MediumAquamarine
        /// </summary>
        public static readonly Color MediumAquamarine = XUnityColor.FromBGRA(0xFF66CDAA);

        /// <summary>
        /// Цвет - MediumBlue
        /// </summary>
        public static readonly Color MediumBlue = XUnityColor.FromBGRA(0xFF0000CD);

        /// <summary>
        /// Цвет - MediumOrchid
        /// </summary>
        public static readonly Color MediumOrchid = XUnityColor.FromBGRA(0xFFBA55D3);

        /// <summary>
        /// Цвет - MediumPurple
        /// </summary>
        public static readonly Color MediumPurple = XUnityColor.FromBGRA(0xFF9370DB);

        /// <summary>
        /// Цвет - MediumSeaGreen
        /// </summary>
        public static readonly Color MediumSeaGreen = XUnityColor.FromBGRA(0xFF3CB371);

        /// <summary>
        /// Цвет - MediumSlateBlue
        /// </summary>
        public static readonly Color MediumSlateBlue = XUnityColor.FromBGRA(0xFF7B68EE);

        /// <summary>
        /// Цвет - MediumSpringGreen
        /// </summary>
        public static readonly Color MediumSpringGreen = XUnityColor.FromBGRA(0xFF00FA9A);

        /// <summary>
        /// Цвет - MediumTurquoise
        /// </summary>
        public static readonly Color MediumTurquoise = XUnityColor.FromBGRA(0xFF48D1CC);

        /// <summary>
        /// Цвет - MediumVioletRed
        /// </summary>
        public static readonly Color MediumVioletRed = XUnityColor.FromBGRA(0xFFC71585);

        /// <summary>
        /// Цвет - MidnightBlue
        /// </summary>
        public static readonly Color MidnightBlue = XUnityColor.FromBGRA(0xFF191970);

        /// <summary>
        /// Цвет - MintCream
        /// </summary>
        public static readonly Color MintCream = XUnityColor.FromBGRA(0xFFF5FFFA);

        /// <summary>
        /// Цвет - MistyRose
        /// </summary>
        public static readonly Color MistyRose = XUnityColor.FromBGRA(0xFFFFE4E1);

        /// <summary>
        /// Цвет - Moccasin
        /// </summary>
        public static readonly Color Moccasin = XUnityColor.FromBGRA(0xFFFFE4B5);

        /// <summary>
        /// Цвет - NavajoWhite
        /// </summary>
        public static readonly Color NavajoWhite = XUnityColor.FromBGRA(0xFFFFDEAD);

        /// <summary>
        /// Цвет - Navy
        /// </summary>
        public static readonly Color Navy = XUnityColor.FromBGRA(0xFF000080);

        /// <summary>
        /// Цвет - OldLace
        /// </summary>
        public static readonly Color OldLace = XUnityColor.FromBGRA(0xFFFDF5E6);

        /// <summary>
        /// Цвет - Olive
        /// </summary>
        public static readonly Color Olive = XUnityColor.FromBGRA(0xFF808000);

        /// <summary>
        /// Цвет - OliveDrab
        /// </summary>
        public static readonly Color OliveDrab = XUnityColor.FromBGRA(0xFF6B8E23);

        /// <summary>
        /// Цвет - Orange
        /// </summary>
        public static readonly Color Orange = XUnityColor.FromBGRA(0xFFFFA500);

        /// <summary>
        /// Цвет - OrangeRed
        /// </summary>
        public static readonly Color OrangeRed = XUnityColor.FromBGRA(0xFFFF4500);

        /// <summary>
        /// Цвет - Orchid
        /// </summary>
        public static readonly Color Orchid = XUnityColor.FromBGRA(0xFFDA70D6);

        /// <summary>
        /// Цвет - PaleGoldenrod
        /// </summary>
        public static readonly Color PaleGoldenrod = XUnityColor.FromBGRA(0xFFEEE8AA);

        /// <summary>
        /// Цвет - PaleGreen
        /// </summary>
        public static readonly Color PaleGreen = XUnityColor.FromBGRA(0xFF98FB98);

        /// <summary>
        /// Цвет - PaleTurquoise
        /// </summary>
        public static readonly Color PaleTurquoise = XUnityColor.FromBGRA(0xFFAFEEEE);

        /// <summary>
        /// Цвет - PaleVioletRed
        /// </summary>
        public static readonly Color PaleVioletRed = XUnityColor.FromBGRA(0xFFDB7093);

        /// <summary>
        /// Цвет - PapayaWhip
        /// </summary>
        public static readonly Color PapayaWhip = XUnityColor.FromBGRA(0xFFFFEFD5);

        /// <summary>
        /// Цвет - PeachPuff
        /// </summary>
        public static readonly Color PeachPuff = XUnityColor.FromBGRA(0xFFFFDAB9);

        /// <summary>
        /// Цвет - Peru
        /// </summary>
        public static readonly Color Peru = XUnityColor.FromBGRA(0xFFCD853F);

        /// <summary>
        /// Цвет - Pink
        /// </summary>
        public static readonly Color Pink = XUnityColor.FromBGRA(0xFFFFC0CB);

        /// <summary>
        /// Цвет - Plum
        /// </summary>
        public static readonly Color Plum = XUnityColor.FromBGRA(0xFFDDA0DD);

        /// <summary>
        /// Цвет - PowderBlue
        /// </summary>
        public static readonly Color PowderBlue = XUnityColor.FromBGRA(0xFFB0E0E6);

        /// <summary>
        /// Цвет - Purple
        /// </summary>
        public static readonly Color Purple = XUnityColor.FromBGRA(0xFF800080);

        /// <summary>
        /// Цвет - Red
        /// </summary>
        public static readonly Color Red = XUnityColor.FromBGRA(0xFFFF0000);

        /// <summary>
        /// Цвет - RosyBrown
        /// </summary>
        public static readonly Color RosyBrown = XUnityColor.FromBGRA(0xFFBC8F8F);

        /// <summary>
        /// Цвет - RoyalBlue
        /// </summary>
        public static readonly Color RoyalBlue = XUnityColor.FromBGRA(0xFF4169E1);

        /// <summary>
        /// Цвет - SaddleBrown
        /// </summary>
        public static readonly Color SaddleBrown = XUnityColor.FromBGRA(0xFF8B4513);

        /// <summary>
        /// Цвет - Salmon
        /// </summary>
        public static readonly Color Salmon = XUnityColor.FromBGRA(0xFFFA8072);

        /// <summary>
        /// Цвет - SandyBrown
        /// </summary>
        public static readonly Color SandyBrown = XUnityColor.FromBGRA(0xFFF4A460);

        /// <summary>
        /// Цвет - SeaGreen
        /// </summary>
        public static readonly Color SeaGreen = XUnityColor.FromBGRA(0xFF2E8B57);

        /// <summary>
        /// Цвет - SeaShell
        /// </summary>
        public static readonly Color SeaShell = XUnityColor.FromBGRA(0xFFFFF5EE);

        /// <summary>
        /// Цвет - Sienna
        /// </summary>
        public static readonly Color Sienna = XUnityColor.FromBGRA(0xFFA0522D);

        /// <summary>
        /// Цвет - Silver
        /// </summary>
        public static readonly Color Silver = XUnityColor.FromBGRA(0xFFC0C0C0);

        /// <summary>
        /// Цвет - SkyBlue
        /// </summary>
        public static readonly Color SkyBlue = XUnityColor.FromBGRA(0xFF87CEEB);

        /// <summary>
        /// Цвет - SlateBlue
        /// </summary>
        public static readonly Color SlateBlue = XUnityColor.FromBGRA(0xFF6A5ACD);

        /// <summary>
        /// Цвет - SlateGray
        /// </summary>
        public static readonly Color SlateGray = XUnityColor.FromBGRA(0xFF708090);

        /// <summary>
        /// Цвет - Snow
        /// </summary>
        public static readonly Color Snow = XUnityColor.FromBGRA(0xFFFFFAFA);

        /// <summary>
        /// Цвет - SpringGreen
        /// </summary>
        public static readonly Color SpringGreen = XUnityColor.FromBGRA(0xFF00FF7F);

        /// <summary>
        /// Цвет - SteelBlue
        /// </summary>
        public static readonly Color SteelBlue = XUnityColor.FromBGRA(0xFF4682B4);

        /// <summary>
        /// Цвет - Tan
        /// </summary>
        public static readonly Color Tan = XUnityColor.FromBGRA(0xFFD2B48C);

        /// <summary>
        /// Цвет - Teal
        /// </summary>
        public static readonly Color Teal = XUnityColor.FromBGRA(0xFF008080);

        /// <summary>
        /// Цвет - Thistle
        /// </summary>
        public static readonly Color Thistle = XUnityColor.FromBGRA(0xFFD8BFD8);

        /// <summary>
        /// Цвет - Tomato
        /// </summary>
        public static readonly Color Tomato = XUnityColor.FromBGRA(0xFFFF6347);

        /// <summary>
        /// Цвет - Turquoise
        /// </summary>
        public static readonly Color Turquoise = XUnityColor.FromBGRA(0xFF40E0D0);

        /// <summary>
        /// Цвет - Violet
        /// </summary>
        public static readonly Color Violet = XUnityColor.FromBGRA(0xFFEE82EE);

        /// <summary>
        /// Цвет - Wheat
        /// </summary>
        public static readonly Color Wheat = XUnityColor.FromBGRA(0xFFF5DEB3);

        /// <summary>
        /// Цвет - White
        /// </summary>
        public static readonly Color White = XUnityColor.FromBGRA(0xFFFFFFFF);

        /// <summary>
        /// Цвет - WhiteSmoke
        /// </summary>
        public static readonly Color WhiteSmoke = XUnityColor.FromBGRA(0xFFF5F5F5);

        /// <summary>
        /// Цвет - Yellow
        /// </summary>
        public static readonly Color Yellow = XUnityColor.FromBGRA(0xFFFFFF00);

        /// <summary>
        /// Цвет - YellowGreen
        /// </summary>
        public static readonly Color YellowGreen = XUnityColor.FromBGRA(0xFF9ACD32);
        #endregion

        #region Main methods
        
        /// <summary>
        /// Формирование цвет из упакованного формата BGRA целого числа
        /// </summary>
        /// <param name="color">Значение цвета в BGRA формате целого числа</param>
        /// <returns>Цвет</returns>
        
        public static Color FromBGRA(uint color)
        {
            var red = (byte)((color >> 16) & 255) / 255.0f;
            var green = (byte)((color >> 8) & 255) / 255.0f;
            var blue = (byte)(color & 255) / 255.0f;
            var alpha = (byte)((color >> 24) & 255) / 255.0f;

            return new Color(red, green, blue, alpha);
        }

        
        /// <summary>
        /// Установка нового значения свойства типа Color
        /// </summary>
        /// <param name="currentValue">Текущие значение</param>
        /// <param name="newValue">Новое значение</param>
        /// <returns>Статус установка нового значения свойства</returns>
        
        public static bool SetColor(ref Color currentValue, Color newValue)
        {
            if (currentValue.r == newValue.r && currentValue.g == newValue.g &&
                currentValue.b == newValue.b && currentValue.a == newValue.a)
            {
                return false;
            }

            currentValue = newValue;
            return true;
        }

        
        /// <summary>
        /// Десереализация цветового значения из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color DeserializeFromString(string data)
        {
            Color color;
            var colorData = data.Split(',');
            color.r = int.Parse(colorData[0]) / 255.0f;
            color.g = int.Parse(colorData[1]) / 255.0f;
            color.b = int.Parse(colorData[2]) / 255.0f;
            color.a = int.Parse(colorData[3]) / 255.0f;
            return color;
        }

        
        /// <summary>
        /// Преобразование в объект цвета из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Цвет</returns>
        
        public static Color ToColor(object value)
        {
            if (value is Color)
            {
                return ((Color)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is uint)
                    {
                        return (FromBGRA((uint)value));
                    }
                }
            }

            return (White);
        }
        #endregion
    }


    /// <summary>
    /// Статический класс для расширения функциональности типа Color32
    /// </summary>
    public static class XUnityColor32
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация цветового значения из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color32 DeserializeFromString(string data)
        {
            var colorData = data.Split(',');
            var r = (byte)int.Parse(colorData[0]);
            var g = (byte)int.Parse(colorData[1]);
            var b = (byte)int.Parse(colorData[2]);
            var a = (byte)int.Parse(colorData[3]);
            return new Color32(r, g, b, a);
        }
        #endregion
    }

    
    /// <summary>
    /// Статический класс для расширения функциональности типа Rect
    /// </summary>
    public static class XUnityRect
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация прямоугольника из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Прямоугольник</returns>
        
        public static Rect DeserializeFromString(string data)
        {
            var rect = new Rect();
            var rectData = data.Split(';');
            rect.x = XNumberConverter.ParseSingle(rectData[0]);
            rect.y = XNumberConverter.ParseSingle(rectData[1]);
            rect.width = XNumberConverter.ParseSingle(rectData[2]);
            rect.height = XNumberConverter.ParseSingle(rectData[3]);
            return rect;
        }

        
        /// <summary>
        /// Получение прямоугольника образованного пересечением двух прямоугольников
        /// </summary>
        /// <param name="a">Первый прямоугольник</param>
        /// <param name="b">Второй прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате пересечения</returns>
        
        public static Rect IntersectRect(Rect a, Rect b)
        {
            var x1 = Mathf.Max(a.x, b.x);
            var x2 = Mathf.Min(a.x + a.width, b.x + b.width);
            var y1 = Mathf.Max(a.y, b.y);
            var y2 = Mathf.Min(a.y + a.height, b.y + b.height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new Rect(x1, y1, x2 - x1, y2 - y1);
            }

            return Rect.zero;
        }

        
        /// <summary>
        /// Получение прямоугольника образованного объединением двух прямоугольников
        /// </summary>
        /// <param name="a">Первый прямоугольник</param>
        /// <param name="b">Второй прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате объединения</returns>
        
        public static Rect UnionRect(Rect a, Rect b)
        {
            var x1 = Mathf.Min(a.x, b.x);
            var x2 = Mathf.Max(a.x + a.width, b.x + b.width);
            var y1 = Mathf.Min(a.y, b.y);
            var y2 = Mathf.Max(a.y + a.height, b.y + b.height);

            return new Rect(x1, y1, x2 - x1, y2 - y1);
        }

        
        /// <summary>
        /// Преобразование в объект прямоугольника из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Прямоугольник</returns>
        
        public static Rect ToRect(object value)
        {
            if (value is Rect)
            {
                return ((Rect)value);
            }
            else
            {
                if (value is string v)
                {
                    return (DeserializeFromString(v));
                }
                else
                {
                    if (value is RectInt rectInt)
                    {
                        return (new Rect(rectInt.x, rectInt.y, rectInt.width, rectInt.height));
                    }
                }
            }

            return (Rect.zero);
        }
        #endregion
    }

    
    /// <summary>
    /// Набор прямоугольников расположенных по горизонтали
    /// </summary>
    /// <remarks>
    /// Структура представляющая собой массив прямоугольников расположенным расположенных по горизонтали относительно
    /// друг друга по определённым правила
    /// </remarks>
    public struct TRectSetH
    {
        #region Static methods
        
        /// <summary>
        /// Получение набора прямоугольников в соответствии с заданной разбивкой
        /// </summary>
        /// <remarks>
        /// Специальный метод для разбивки прямоугольников для отображения полей объекта в одну линию
        /// </remarks>
        /// <param name="totalRect">Общий прямоугольник</param>
        /// <param name="count">Количество элементов списка</param>
        /// <param name="percents">Список процентов ширины для каждого поля</param>
        /// <returns>Набор прямоугольников</returns>
        
        public static TRectSetH CreateForColumnsFromPercent(Rect totalRect, int count, params float[] percents)
        {
            if (count > 0)
            {
                var rects = new TRectSetH(percents.Length + 1);

                // Считать будем от ширины с минусом по единицы на каждую колонку и минус ширина для вывода индекса
                float indexWidth = 12;
                if (count > 9)
                {
                    indexWidth = 16;
                }
                var totalWidth = totalRect.width - percents.Length - indexWidth;

                rects.Rects[0].x = totalRect.x;
                rects.Rects[0].y = totalRect.y;
                rects.Rects[0].width = indexWidth;
                rects.Rects[0].height = totalRect.height;

                for (var i = 1; i < percents.Length + 1; i++)
                {
                    rects.Rects[i].x = rects.Rects[i - 1].xMax + 1;
                    rects.Rects[i].y = totalRect.y;
                    rects.Rects[i].width = Mathf.Ceil(percents[i - 1] / 100 * totalWidth);
                    rects.Rects[i].height = totalRect.height;
                }

                return (rects);

            }
            else
            {
                return (new TRectSetH(totalRect, percents));
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Массив прямоугольников
        /// </summary>
        public readonly Rect[] Rects;
        #endregion

        #region Properties
        /// <summary>
        /// Количество прямоугольников
        /// </summary>
        public int Count
        {
            get { return (Rects.Length); }
        }

        /// <summary>
        /// Значение координаты по X
        /// </summary>
        public float X
        {
            get { return Rects[0].x; }
            set
            {
                Rects[0].x = value;
                for (var i = 1; i < Rects.Length; i++)
                {
                    Rects[i].x = Rects[i - 1].xMax;
                }
            }
        }

        /// <summary>
        /// Значение координаты по Y
        /// </summary>
        public float Y
        {
            get { return (Rects[0].y); }
            set
            {
                for (var i = 0; i < Rects.Length; i++)
                {
                    Rects[i].y = value;
                }
            }
        }

        /// <summary>
        /// Общая ширина прямоугольников
        /// </summary>
        public float Width
        {
            get
            {
                return (Rects[Rects.Length - 1].xMax - Rects[0].x);
            }
            set
            {

            }
        }

        /// <summary>
        /// Высота прямоугольников
        /// </summary>
        public float Height
        {
            get
            {
                return (Rects[0].height);
            }
            set
            {
                for (var i = 0; i < Rects.Length; i++)
                {
                    Rects[i].height = value;
                }
            }
        }
        #endregion

        #region Constructors
        
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="countRect">Количество прямоугольников</param>
        
        public TRectSetH(int countRect)
        {
            Rects = new Rect[countRect];
        }

        
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="totalRect">Общий прямоугольник</param>
        /// <param name="percents">Список процентов ширины для каждого прямоугольника</param>
        
        public TRectSetH(Rect totalRect, params float[] percents)
        {
            Rects = new Rect[percents.Length];

            Rects[0].x = totalRect.x;
            Rects[0].y = totalRect.y;
            Rects[0].width = Mathf.Ceil(percents[0] / 100 * totalRect.width);
            Rects[0].height = totalRect.height;

            for (var i = 1; i < percents.Length; i++)
            {
                Rects[i].x = Rects[i - 1].xMax + 1;
                Rects[i].y = totalRect.y;
                Rects[i].width = Mathf.Ceil(percents[i] / 100 * totalRect.width);
                Rects[i].height = totalRect.height;
            }
        }
        #endregion

        #region System methods
        
        /// <summary>
        /// Преобразование к текстовому представлению
        /// </summary>
        /// <returns>Текстовое представление объекта</returns>
        
        public override string ToString()
        {
            return (string.Format("X:{0},Y:{1},W:{2},H:{3}", X, Y, Width, Height));
        }
        #endregion

        #region Indexer
        
        /// <summary>
        /// Индексация прямоугольников на основе индекса
        /// </summary>
        /// <param name="index">Индекс прямоугольника</param>
        /// <returns>Прямоугольник</returns>
        
        public Rect this[int index]
        {
            get
            {
                return (Rects[index]);
            }
            set
            {
                Rects[index].x = value.x;
                Rects[index].y = value.y;
                Rects[index].width = value.width;
                Rects[index].height = value.height;
            }
        }
        #endregion
    }

    
    /// <summary>
    /// Статический класс для расширения функциональности типа RectInt
    /// </summary>
    
    public static class XUnityRectInt
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация прямоугольника из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Прямоугольник</returns>
        
        public static RectInt DeserializeFromString(string data)
        {
            var rect = new RectInt();
            var rectData = data.Split(';');
            rect.x = XNumberConverter.ParseInt(rectData[0]);
            rect.y = XNumberConverter.ParseInt(rectData[1]);
            rect.width = XNumberConverter.ParseInt(rectData[2]);
            rect.height = XNumberConverter.ParseInt(rectData[3]);
            return rect;
        }

        
        /// <summary>
        /// Получение прямоугольника образованного пересечением двух прямоугольников
        /// </summary>
        /// <param name="a">Первый прямоугольник</param>
        /// <param name="b">Второй прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате пересечения</returns>
        
        public static RectInt IntersectRect(RectInt a, RectInt b)
        {
            var x1 = Mathf.Max(a.x, b.x);
            var x2 = Mathf.Min(a.x + a.width, b.x + b.width);
            var y1 = Mathf.Max(a.y, b.y);
            var y2 = Mathf.Min(a.y + a.height, b.y + b.height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectInt(x1, y1, x2 - x1, y2 - y1);
            }

            return new RectInt();
        }

        
        /// <summary>
        /// Получение прямоугольника образованного объединением двух прямоугольников
        /// </summary>
        /// <param name="a">Первый прямоугольник</param>
        /// <param name="b">Второй прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате объединения</returns>
        
        public static RectInt UnionRect(RectInt a, RectInt b)
        {
            var x1 = Mathf.Min(a.x, b.x);
            var x2 = Mathf.Max(a.x + a.width, b.x + b.width);
            var y1 = Mathf.Min(a.y, b.y);
            var y2 = Mathf.Max(a.y + a.height, b.y + b.height);

            return new RectInt(x1, y1, x2 - x1, y2 - y1);
        }

        
        /// <summary>
        /// Преобразование в объект прямоугольника из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Прямоугольник</returns>
        
        public static RectInt ToRect(object value)
        {
            if (value is RectInt)
            {
                return ((RectInt)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Rect rect)
                    {
                        return (new RectInt((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
                    }
                }
            }

            return (new RectInt(0, 0, 0, 0));
        }
        #endregion
    }

    
    /// <summary>
    /// Статический класс для расширения функциональности типа RectOffset
    /// </summary>
    public static class XUnityRectOffset
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация прямоугольника смещения из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Прямоугольник смещения</returns>
        
        public static RectOffset DeserializeFromString(string data)
        {
            var rect = new RectOffset();
            var rectData = data.Split(';');
            rect.left = int.Parse(rectData[0]);
            rect.top = int.Parse(rectData[1]);
            rect.right = int.Parse(rectData[2]);
            rect.bottom = int.Parse(rectData[3]);
            return rect;
        }
        #endregion
    }

    
    /// <summary>
    /// Статический класс для расширения функциональности типа Bounds
    /// </summary>
    public static class XUnityBounds
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация прямоугольника AABB из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Прямоугольник AABB</returns>
        
        public static Bounds DeserializeFromString(string data)
        {
            var bounds = new Bounds();
            var rectData = data.Split(';');
            Vector3 center, extents;
            center.x = XNumberConverter.ParseSingle(rectData[0]);
            center.y = XNumberConverter.ParseSingle(rectData[1]);
            center.z = XNumberConverter.ParseSingle(rectData[2]);
            extents.x = XNumberConverter.ParseSingle(rectData[3]);
            extents.y = XNumberConverter.ParseSingle(rectData[4]);
            extents.z = XNumberConverter.ParseSingle(rectData[5]);
            bounds.center = center;
            bounds.extents = extents;
            return bounds;
        }

        
        /// <summary>
        /// Преобразование в объект AABB из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>AABB</returns>
        
        public static Bounds ToBounds(object value)
        {
            if (value is Bounds)
            {
                return ((Bounds)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is BoundsInt boundsInt)
                    {
                        return (new Bounds(boundsInt.center, boundsInt.size));
                    }
                }
            }

            return (new Bounds());
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа BoundsInt
    /// </summary>
    public static class XUnityBoundsInt
    {
        #region Main methods
        
        /// <summary>
        /// Десереализация прямоугольника AABB из строки
        /// </summary>
        /// <param name="data">Строка данных</param>
        /// <returns>Прямоугольник AABB</returns>
        
        public static BoundsInt DeserializeFromString(string data)
        {
            var bounds = new BoundsInt();
            var rectData = data.Split(';');
            var size = new Vector3Int();
            bounds.x = XNumberConverter.ParseInt(rectData[0]);
            bounds.y = XNumberConverter.ParseInt(rectData[1]);
            bounds.z = XNumberConverter.ParseInt(rectData[2]);
            size.x = XNumberConverter.ParseInt(rectData[3]);
            size.y = XNumberConverter.ParseInt(rectData[4]);
            size.z = XNumberConverter.ParseInt(rectData[5]);
            bounds.size = size;
            return bounds;
        }

        
        /// <summary>
        /// Преобразование в объект AABB из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>AABB</returns>
        
        public static BoundsInt ToBounds(object value)
        {
            if (value is BoundsInt)
            {
                return ((BoundsInt)value);
            }
            else
            {
                if (value is string)
                {
                    return (DeserializeFromString((string)value));
                }
                else
                {
                    if (value is Bounds bounds)
                    {
                        return (new BoundsInt(bounds.center.ToInt(), bounds.size.ToInt()));
                    }
                }
            }

            return (new BoundsInt());
        }
        #endregion
    }

    /// <summary>
    /// Статический класс для расширения функциональности типа LayerMask
    /// </summary>
    
    public static class XUnityLayerMask
    {
        #region Main methods
        
        /// <summary>
        /// Создание маски слоев по именам
        /// </summary>
        /// <param name="layerNames">Список имен слоев</param>
        /// <returns>Маска слоев</returns>
        [Obsolete("Дублирующий API. Используйте NamesToMask")]
        public static LayerMask Create(string[] layerNames)
        {
            return NamesToMask(layerNames);
        }

        
        /// <summary>
        /// Создание маски слоев по индексам
        /// </summary>
        /// <param name="layerNumbers">Список индексов слоев</param>
        /// <returns>Маска слоев</returns>
        [Obsolete("Дублирующий API. Используйте LayerNumbersToMask")]
        public static LayerMask Create(int[] layerNumbers)
        {
            return LayerNumbersToMask(layerNumbers);
        }

        
        /// <summary>
        /// Создание маски слоев по именам
        /// </summary>
        /// <param name="layerNames">Список имен слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask NamesToMask(string[] layerNames)
        {
            var result = (LayerMask)0;
            for (var i = 0; i < layerNames.Length; i++)
            {
                var name = layerNames[i];
                result |= 1 << LayerMask.NameToLayer(name);
            }
            return result;
        }

        
        /// <summary>
        /// Создание маски слоев по индексам
        /// </summary>
        /// <param name="layerNumbers">Список индексов слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask LayerNumbersToMask(int[] layerNumbers)
        {
            LayerMask result = 0;
            for (var i = 0; i < layerNumbers.Length; i++)
            {
                var layer = layerNumbers[i];
                result |= 1 << layer;
            }
            return result;
        }

        
        /// <summary>
        /// Преобразование в объект маски слоев из базового объекта
        /// </summary>
        /// <remarks>
        /// Метод анализирует реальный тип объекта и пробует соответственным образом выполнить преобразование
        /// </remarks>
        /// <param name="value">Значение</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask ToLayerMask(object value)
        {
            if (value is LayerMask)
            {
                return ((LayerMask)value);
            }
            else
            {
                if (value is string)
                {
                    return (LayerMask.NameToLayer((string)value));
                }
                else
                {
                    if (value is int)
                    {
                        return (Convert.ToInt32(value));
                    }
                }
            }

            return (0);
        }
        #endregion
    }
    
    /**@}*/
    
}
#endif
