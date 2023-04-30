//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема общих типов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusColor.cs
*		Класс для представления цвета.
*		Реализация типа для универсального представления цвета. Применяется для кроссплатформенной реализации концепции
*	цвета.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Runtime.InteropServices;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
        //-------------------------------------------------------------------------------------------------------------
        /** 
         * \defgroup CoreCommonTypes Подсистема общих типов
         * \ingroup Core
         * \brief Подсистема общих типов реализует общие кроссплатформенные типы данных.
         * @{
         */
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Класс для представления цвета
        /// </summary>
        /// <remarks>
        /// Применяется для кроссплатформенной реализации концепции цвета
        /// </remarks>
        //-------------------------------------------------------------------------------------------------------------
        [Serializable]
		[StructLayout(LayoutKind.Sequential, Size = 4)]
		public struct TColor : IComparable<TColor>, IEquatable<TColor>, ICloneable
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Текстовый формат отображения компонентов цвета
			/// </summary>
			private const String ToStringFormat = "A:{0} R:{1} G:{2} B:{3}";

			/// <summary>
			/// Нулевой цвет
			/// </summary>
			public static readonly TColor Zero = TColor.FromBGRA(0x00000000);

			/// <summary>
			/// Прозрачный цвет
			/// </summary>
			public static readonly TColor Transparent = TColor.FromBGRA(0x00000000);

			/// <summary>
			/// Цвет - AliceBlue
			/// </summary>
			public static readonly TColor AliceBlue = TColor.FromBGRA(0xFFF0F8FF);

			/// <summary>
			/// Цвет - AntiqueWhite
			/// </summary>
			public static readonly TColor AntiqueWhite = TColor.FromBGRA(0xFFFAEBD7);

			/// <summary>
			/// Цвет - Aqua
			/// </summary>
			public static readonly TColor Aqua = TColor.FromBGRA(0xFF00FFFF);

			/// <summary>
			/// Цвет - Aquamarine
			/// </summary>
			public static readonly TColor Aquamarine = TColor.FromBGRA(0xFF7FFFD4);

			/// <summary>
			/// Цвет - Azure
			/// </summary>
			public static readonly TColor Azure = TColor.FromBGRA(0xFFF0FFFF);

			/// <summary>
			/// Цвет - Beige
			/// </summary>
			public static readonly TColor Beige = TColor.FromBGRA(0xFFF5F5DC);

			/// <summary>
			/// Цвет - Bisque
			/// </summary>
			public static readonly TColor Bisque = TColor.FromBGRA(0xFFFFE4C4);

			/// <summary>
			/// Цвет - Black
			/// </summary>
			public static readonly TColor Black = TColor.FromBGRA(0xFF000000);

			/// <summary>
			/// Цвет - BlanchedAlmond
			/// </summary>
			public static readonly TColor BlanchedAlmond = TColor.FromBGRA(0xFFFFEBCD);

			/// <summary>
			/// Цвет - Blue
			/// </summary>
			public static readonly TColor Blue = TColor.FromBGRA(0xFF0000FF);

			/// <summary>
			/// Цвет - BlueViolet
			/// </summary>
			public static readonly TColor BlueViolet = TColor.FromBGRA(0xFF8A2BE2);

			/// <summary>
			/// Цвет - Brown
			/// </summary>
			public static readonly TColor Brown = TColor.FromBGRA(0xFFA52A2A);

			/// <summary>
			/// Цвет - BurlyWood
			/// </summary>
			public static readonly TColor BurlyWood = TColor.FromBGRA(0xFFDEB887);

			/// <summary>
			/// Цвет - CadetBlue
			/// </summary>
			public static readonly TColor CadetBlue = TColor.FromBGRA(0xFF5F9EA0);

			/// <summary>
			/// Цвет - Chartreuse
			/// </summary>
			public static readonly TColor Chartreuse = TColor.FromBGRA(0xFF7FFF00);

			/// <summary>
			/// Цвет - Chocolate
			/// </summary>
			public static readonly TColor Chocolate = TColor.FromBGRA(0xFFD2691E);

			/// <summary>
			/// Цвет - Coral
			/// </summary>
			public static readonly TColor Coral = TColor.FromBGRA(0xFFFF7F50);

			/// <summary>
			/// Цвет - CornflowerBlue
			/// </summary>
			public static readonly TColor CornflowerBlue = TColor.FromBGRA(0xFF6495ED);

			/// <summary>
			/// Цвет - Cornsilk
			/// </summary>
			public static readonly TColor Cornsilk = TColor.FromBGRA(0xFFFFF8DC);

			/// <summary>
			/// Цвет - Crimson
			/// </summary>
			public static readonly TColor Crimson = TColor.FromBGRA(0xFFDC143C);

			/// <summary>
			/// Цвет - Cyan
			/// </summary>
			public static readonly TColor Cyan = TColor.FromBGRA(0xFF00FFFF);

			/// <summary>
			/// Цвет - DarkBlue
			/// </summary>
			public static readonly TColor DarkBlue = TColor.FromBGRA(0xFF00008B);

			/// <summary>
			/// Цвет - DarkCyan
			/// </summary>
			public static readonly TColor DarkCyan = TColor.FromBGRA(0xFF008B8B);

			/// <summary>
			/// Цвет - DarkGoldenrod
			/// </summary>
			public static readonly TColor DarkGoldenrod = TColor.FromBGRA(0xFFB8860B);

			/// <summary>
			/// Цвет - DarkGray
			/// </summary>
			public static readonly TColor DarkGray = TColor.FromBGRA(0xFFA9A9A9);

			/// <summary>
			/// Цвет - DarkGreen
			/// </summary>
			public static readonly TColor DarkGreen = TColor.FromBGRA(0xFF006400);

			/// <summary>
			/// Цвет - DarkKhaki
			/// </summary>
			public static readonly TColor DarkKhaki = TColor.FromBGRA(0xFFBDB76B);

			/// <summary>
			/// Цвет - DarkMagenta
			/// </summary>
			public static readonly TColor DarkMagenta = TColor.FromBGRA(0xFF8B008B);

			/// <summary>
			/// Цвет - DarkOliveGreen
			/// </summary>
			public static readonly TColor DarkOliveGreen = TColor.FromBGRA(0xFF556B2F);

			/// <summary>
			/// Цвет - DarkOrange
			/// </summary>
			public static readonly TColor DarkOrange = TColor.FromBGRA(0xFFFF8C00);

			/// <summary>
			/// Цвет - DarkOrchid
			/// </summary>
			public static readonly TColor DarkOrchid = TColor.FromBGRA(0xFF9932CC);

			/// <summary>
			/// Цвет - DarkRed
			/// </summary>
			public static readonly TColor DarkRed = TColor.FromBGRA(0xFF8B0000);

			/// <summary>
			/// Цвет - DarkSalmon
			/// </summary>
			public static readonly TColor DarkSalmon = TColor.FromBGRA(0xFFE9967A);

			/// <summary>
			/// Цвет - DarkSeaGreen
			/// </summary>
			public static readonly TColor DarkSeaGreen = TColor.FromBGRA(0xFF8FBC8B);

			/// <summary>
			/// Цвет - DarkSlateBlue
			/// </summary>
			public static readonly TColor DarkSlateBlue = TColor.FromBGRA(0xFF483D8B);

			/// <summary>
			/// Цвет - DarkSlateGray
			/// </summary>
			public static readonly TColor DarkSlateGray = TColor.FromBGRA(0xFF2F4F4F);

			/// <summary>
			/// Цвет - DarkTurquoise
			/// </summary>
			public static readonly TColor DarkTurquoise = TColor.FromBGRA(0xFF00CED1);

			/// <summary>
			/// Цвет - DarkViolet
			/// </summary>
			public static readonly TColor DarkViolet = TColor.FromBGRA(0xFF9400D3);

			/// <summary>
			/// Цвет - DeepPink
			/// </summary>
			public static readonly TColor DeepPink = TColor.FromBGRA(0xFFFF1493);

			/// <summary>
			/// Цвет - DeepSkyBlue
			/// </summary>
			public static readonly TColor DeepSkyBlue = TColor.FromBGRA(0xFF00BFFF);

			/// <summary>
			/// Цвет - DimGray
			/// </summary>
			public static readonly TColor DimGray = TColor.FromBGRA(0xFF696969);

			/// <summary>
			/// Цвет - DodgerBlue
			/// </summary>
			public static readonly TColor DodgerBlue = TColor.FromBGRA(0xFF1E90FF);

			/// <summary>
			/// Цвет - Firebrick
			/// </summary>
			public static readonly TColor Firebrick = TColor.FromBGRA(0xFFB22222);

			/// <summary>
			/// Цвет - FloralWhite
			/// </summary>
			public static readonly TColor FloralWhite = TColor.FromBGRA(0xFFFFFAF0);

			/// <summary>
			/// Цвет - ForestGreen
			/// </summary>
			public static readonly TColor ForestGreen = TColor.FromBGRA(0xFF228B22);

			/// <summary>
			/// Цвет - Fuchsia
			/// </summary>
			public static readonly TColor Fuchsia = TColor.FromBGRA(0xFFFF00FF);

			/// <summary>
			/// Цвет - Gainsboro
			/// </summary>
			public static readonly TColor Gainsboro = TColor.FromBGRA(0xFFDCDCDC);

			/// <summary>
			/// Цвет - GhostWhite
			/// </summary>
			public static readonly TColor GhostWhite = TColor.FromBGRA(0xFFF8F8FF);

			/// <summary>
			/// Цвет - Gold
			/// </summary>
			public static readonly TColor Gold = TColor.FromBGRA(0xFFFFD700);

			/// <summary>
			/// Цвет - Goldenrod
			/// </summary>
			public static readonly TColor Goldenrod = TColor.FromBGRA(0xFFDAA520);

			/// <summary>
			/// Цвет - Gray
			/// </summary>
			public static readonly TColor Gray = TColor.FromBGRA(0xFF808080);

			/// <summary>
			/// Цвет - Green
			/// </summary>
			public static readonly TColor Green = TColor.FromBGRA(0xFF008000);

			/// <summary>
			/// Цвет - GreenYellow
			/// </summary>
			public static readonly TColor GreenYellow = TColor.FromBGRA(0xFFADFF2F);

			/// <summary>
			/// Цвет - Honeydew
			/// </summary>
			public static readonly TColor Honeydew = TColor.FromBGRA(0xFFF0FFF0);

			/// <summary>
			/// Цвет - HotPink
			/// </summary>
			public static readonly TColor HotPink = TColor.FromBGRA(0xFFFF69B4);

			/// <summary>
			/// Цвет - IndianRed
			/// </summary>
			public static readonly TColor IndianRed = TColor.FromBGRA(0xFFCD5C5C);

			/// <summary>
			/// Цвет - Indigo
			/// </summary>
			public static readonly TColor Indigo = TColor.FromBGRA(0xFF4B0082);

			/// <summary>
			/// Цвет - Ivory
			/// </summary>
			public static readonly TColor Ivory = TColor.FromBGRA(0xFFFFFFF0);

			/// <summary>
			/// Цвет - Khaki
			/// </summary>
			public static readonly TColor Khaki = TColor.FromBGRA(0xFFF0E68C);

			/// <summary>
			/// Цвет - Lavender
			/// </summary>
			public static readonly TColor Lavender = TColor.FromBGRA(0xFFE6E6FA);

			/// <summary>
			/// Цвет - LavenderBlush
			/// </summary>
			public static readonly TColor LavenderBlush = TColor.FromBGRA(0xFFFFF0F5);

			/// <summary>
			/// Цвет - LawnGreen
			/// </summary>
			public static readonly TColor LawnGreen = TColor.FromBGRA(0xFF7CFC00);

			/// <summary>
			/// Цвет - LemonChiffon
			/// </summary>
			public static readonly TColor LemonChiffon = TColor.FromBGRA(0xFFFFFACD);

			/// <summary>
			/// Цвет - LightBlue
			/// </summary>
			public static readonly TColor LightBlue = TColor.FromBGRA(0xFFADD8E6);

			/// <summary>
			/// Цвет - LightCoral
			/// </summary>
			public static readonly TColor LightCoral = TColor.FromBGRA(0xFFF08080);

			/// <summary>
			/// Цвет - LightCyan
			/// </summary>
			public static readonly TColor LightCyan = TColor.FromBGRA(0xFFE0FFFF);

			/// <summary>
			/// Цвет - LightGoldenrodYellow
			/// </summary>
			public static readonly TColor LightGoldenrodYellow = TColor.FromBGRA(0xFFFAFAD2);

			/// <summary>
			/// Цвет - LightGray
			/// </summary>
			public static readonly TColor LightGray = TColor.FromBGRA(0xFFD3D3D3);

			/// <summary>
			/// Цвет - LightGreen
			/// </summary>
			public static readonly TColor LightGreen = TColor.FromBGRA(0xFF90EE90);

			/// <summary>
			/// Цвет - LightPink
			/// </summary>
			public static readonly TColor LightPink = TColor.FromBGRA(0xFFFFB6C1);

			/// <summary>
			/// Цвет - LightSalmon
			/// </summary>
			public static readonly TColor LightSalmon = TColor.FromBGRA(0xFFFFA07A);

			/// <summary>
			/// Цвет - LightSeaGreen
			/// </summary>
			public static readonly TColor LightSeaGreen = TColor.FromBGRA(0xFF20B2AA);

			/// <summary>
			/// Цвет - LightSkyBlue
			/// </summary>
			public static readonly TColor LightSkyBlue = TColor.FromBGRA(0xFF87CEFA);

			/// <summary>
			/// Цвет - LightSlateGray
			/// </summary>
			public static readonly TColor LightSlateGray = TColor.FromBGRA(0xFF778899);

			/// <summary>
			/// Цвет - LightSteelBlue
			/// </summary>
			public static readonly TColor LightSteelBlue = TColor.FromBGRA(0xFFB0C4DE);

			/// <summary>
			/// Цвет - LightYellow
			/// </summary>
			public static readonly TColor LightYellow = TColor.FromBGRA(0xFFFFFFE0);

			/// <summary>
			/// Цвет - Lime
			/// </summary>
			public static readonly TColor Lime = TColor.FromBGRA(0xFF00FF00);

			/// <summary>
			/// Цвет - LimeGreen
			/// </summary>
			public static readonly TColor LimeGreen = TColor.FromBGRA(0xFF32CD32);

			/// <summary>
			/// Цвет - Linen
			/// </summary>
			public static readonly TColor Linen = TColor.FromBGRA(0xFFFAF0E6);

			/// <summary>
			/// Цвет - Magenta
			/// </summary>
			public static readonly TColor Magenta = TColor.FromBGRA(0xFFFF00FF);

			/// <summary>
			/// Цвет - Maroon
			/// </summary>
			public static readonly TColor Maroon = TColor.FromBGRA(0xFF800000);

			/// <summary>
			/// Цвет - MediumAquamarine
			/// </summary>
			public static readonly TColor MediumAquamarine = TColor.FromBGRA(0xFF66CDAA);

			/// <summary>
			/// Цвет - MediumBlue
			/// </summary>
			public static readonly TColor MediumBlue = TColor.FromBGRA(0xFF0000CD);

			/// <summary>
			/// Цвет - MediumOrchid
			/// </summary>
			public static readonly TColor MediumOrchid = TColor.FromBGRA(0xFFBA55D3);

			/// <summary>
			/// Цвет - MediumPurple
			/// </summary>
			public static readonly TColor MediumPurple = TColor.FromBGRA(0xFF9370DB);

			/// <summary>
			/// Цвет - MediumSeaGreen
			/// </summary>
			public static readonly TColor MediumSeaGreen = TColor.FromBGRA(0xFF3CB371);

			/// <summary>
			/// Цвет - MediumSlateBlue
			/// </summary>
			public static readonly TColor MediumSlateBlue = TColor.FromBGRA(0xFF7B68EE);

			/// <summary>
			/// Цвет - MediumSpringGreen
			/// </summary>
			public static readonly TColor MediumSpringGreen = TColor.FromBGRA(0xFF00FA9A);

			/// <summary>
			/// Цвет - MediumTurquoise
			/// </summary>
			public static readonly TColor MediumTurquoise = TColor.FromBGRA(0xFF48D1CC);

			/// <summary>
			/// Цвет - MediumVioletRed
			/// </summary>
			public static readonly TColor MediumVioletRed = TColor.FromBGRA(0xFFC71585);

			/// <summary>
			/// Цвет - MidnightBlue
			/// </summary>
			public static readonly TColor MidnightBlue = TColor.FromBGRA(0xFF191970);

			/// <summary>
			/// Цвет - MintCream
			/// </summary>
			public static readonly TColor MintCream = TColor.FromBGRA(0xFFF5FFFA);

			/// <summary>
			/// Цвет - MistyRose
			/// </summary>
			public static readonly TColor MistyRose = TColor.FromBGRA(0xFFFFE4E1);

			/// <summary>
			/// Цвет - Moccasin
			/// </summary>
			public static readonly TColor Moccasin = TColor.FromBGRA(0xFFFFE4B5);

			/// <summary>
			/// Цвет - NavajoWhite
			/// </summary>
			public static readonly TColor NavajoWhite = TColor.FromBGRA(0xFFFFDEAD);

			/// <summary>
			/// Цвет - Navy
			/// </summary>
			public static readonly TColor Navy = TColor.FromBGRA(0xFF000080);

			/// <summary>
			/// Цвет - OldLace
			/// </summary>
			public static readonly TColor OldLace = TColor.FromBGRA(0xFFFDF5E6);

			/// <summary>
			/// Цвет - Olive
			/// </summary>
			public static readonly TColor Olive = TColor.FromBGRA(0xFF808000);

			/// <summary>
			/// Цвет - OliveDrab
			/// </summary>
			public static readonly TColor OliveDrab = TColor.FromBGRA(0xFF6B8E23);

			/// <summary>
			/// Цвет - Orange
			/// </summary>
			public static readonly TColor Orange = TColor.FromBGRA(0xFFFFA500);

			/// <summary>
			/// Цвет - OrangeRed
			/// </summary>
			public static readonly TColor OrangeRed = TColor.FromBGRA(0xFFFF4500);

			/// <summary>
			/// Цвет - Orchid
			/// </summary>
			public static readonly TColor Orchid = TColor.FromBGRA(0xFFDA70D6);

			/// <summary>
			/// Цвет - PaleGoldenrod
			/// </summary>
			public static readonly TColor PaleGoldenrod = TColor.FromBGRA(0xFFEEE8AA);

			/// <summary>
			/// Цвет - PaleGreen
			/// </summary>
			public static readonly TColor PaleGreen = TColor.FromBGRA(0xFF98FB98);

			/// <summary>
			/// Цвет - PaleTurquoise
			/// </summary>
			public static readonly TColor PaleTurquoise = TColor.FromBGRA(0xFFAFEEEE);

			/// <summary>
			/// Цвет - PaleVioletRed
			/// </summary>
			public static readonly TColor PaleVioletRed = TColor.FromBGRA(0xFFDB7093);

			/// <summary>
			/// Цвет - PapayaWhip
			/// </summary>
			public static readonly TColor PapayaWhip = TColor.FromBGRA(0xFFFFEFD5);

			/// <summary>
			/// Цвет - PeachPuff
			/// </summary>
			public static readonly TColor PeachPuff = TColor.FromBGRA(0xFFFFDAB9);

			/// <summary>
			/// Цвет - Peru
			/// </summary>
			public static readonly TColor Peru = TColor.FromBGRA(0xFFCD853F);

			/// <summary>
			/// Цвет - Pink
			/// </summary>
			public static readonly TColor Pink = TColor.FromBGRA(0xFFFFC0CB);

			/// <summary>
			/// Цвет - Plum
			/// </summary>
			public static readonly TColor Plum = TColor.FromBGRA(0xFFDDA0DD);

			/// <summary>
			/// Цвет - PowderBlue
			/// </summary>
			public static readonly TColor PowderBlue = TColor.FromBGRA(0xFFB0E0E6);

			/// <summary>
			/// Цвет - Purple
			/// </summary>
			public static readonly TColor Purple = TColor.FromBGRA(0xFF800080);

			/// <summary>
			/// Цвет - Red
			/// </summary>
			public static readonly TColor Red = TColor.FromBGRA(0xFFFF0000);

			/// <summary>
			/// Цвет - RosyBrown
			/// </summary>
			public static readonly TColor RosyBrown = TColor.FromBGRA(0xFFBC8F8F);

			/// <summary>
			/// Цвет - RoyalBlue
			/// </summary>
			public static readonly TColor RoyalBlue = TColor.FromBGRA(0xFF4169E1);

			/// <summary>
			/// Цвет - SaddleBrown
			/// </summary>
			public static readonly TColor SaddleBrown = TColor.FromBGRA(0xFF8B4513);

			/// <summary>
			/// Цвет - Salmon
			/// </summary>
			public static readonly TColor Salmon = TColor.FromBGRA(0xFFFA8072);

			/// <summary>
			/// Цвет - SandyBrown
			/// </summary>
			public static readonly TColor SandyBrown = TColor.FromBGRA(0xFFF4A460);

			/// <summary>
			/// Цвет - SeaGreen
			/// </summary>
			public static readonly TColor SeaGreen = TColor.FromBGRA(0xFF2E8B57);

			/// <summary>
			/// Цвет - SeaShell
			/// </summary>
			public static readonly TColor SeaShell = TColor.FromBGRA(0xFFFFF5EE);

			/// <summary>
			/// Цвет - Sienna
			/// </summary>
			public static readonly TColor Sienna = TColor.FromBGRA(0xFFA0522D);

			/// <summary>
			/// Цвет - Silver
			/// </summary>
			public static readonly TColor Silver = TColor.FromBGRA(0xFFC0C0C0);

			/// <summary>
			/// Цвет - SkyBlue
			/// </summary>
			public static readonly TColor SkyBlue = TColor.FromBGRA(0xFF87CEEB);

			/// <summary>
			/// Цвет - SlateBlue
			/// </summary>
			public static readonly TColor SlateBlue = TColor.FromBGRA(0xFF6A5ACD);

			/// <summary>
			/// Цвет - SlateGray
			/// </summary>
			public static readonly TColor SlateGray = TColor.FromBGRA(0xFF708090);

			/// <summary>
			/// Цвет - Snow
			/// </summary>
			public static readonly TColor Snow = TColor.FromBGRA(0xFFFFFAFA);

			/// <summary>
			/// Цвет - SpringGreen
			/// </summary>
			public static readonly TColor SpringGreen = TColor.FromBGRA(0xFF00FF7F);

			/// <summary>
			/// Цвет - SteelBlue
			/// </summary>
			public static readonly TColor SteelBlue = TColor.FromBGRA(0xFF4682B4);

			/// <summary>
			/// Цвет - Tan
			/// </summary>
			public static readonly TColor Tan = TColor.FromBGRA(0xFFD2B48C);

			/// <summary>
			/// Цвет - Teal
			/// </summary>
			public static readonly TColor Teal = TColor.FromBGRA(0xFF008080);

			/// <summary>
			/// Цвет - Thistle
			/// </summary>
			public static readonly TColor Thistle = TColor.FromBGRA(0xFFD8BFD8);

			/// <summary>
			/// Цвет - Tomato
			/// </summary>
			public static readonly TColor Tomato = TColor.FromBGRA(0xFFFF6347);

			/// <summary>
			/// Цвет - Turquoise
			/// </summary>
			public static readonly TColor Turquoise = TColor.FromBGRA(0xFF40E0D0);

			/// <summary>
			/// Цвет - Violet
			/// </summary>
			public static readonly TColor Violet = TColor.FromBGRA(0xFFEE82EE);

			/// <summary>
			/// Цвет - Wheat
			/// </summary>
			public static readonly TColor Wheat = TColor.FromBGRA(0xFFF5DEB3);

			/// <summary>
			/// Цвет - White
			/// </summary>
			public static readonly TColor White = TColor.FromBGRA(0xFFFFFFFF);

			/// <summary>
			/// Цвет - WhiteSmoke
			/// </summary>
			public static readonly TColor WhiteSmoke = TColor.FromBGRA(0xFFF5F5F5);

			/// <summary>
			/// Цвет - Yellow
			/// </summary>
			public static readonly TColor Yellow = TColor.FromBGRA(0xFFFFFF00);

			/// <summary>
			/// Цвет - YellowGreen
			/// </summary>
			public static readonly TColor YellowGreen = TColor.FromBGRA(0xFF9ACD32);
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к допустимому значению компоненты цвета
			/// </summary>
			/// <param name="component">Значение</param>
			/// <returns>Допустимое значение компоненты цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Byte ToByte(Double component)
			{
				var value = (Int32)(component * 255.0);
				return ToByte(value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к допустимому значению компоненты цвета
			/// </summary>
			/// <param name="component">Значение</param>
			/// <returns>Допустимое значение компоненты цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			private static Byte ToByte(Single component)
			{
				var value = (Int32)(component * 255.0f);
				return ToByte(value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к допустимому значению компоненты цвета
			/// </summary>
			/// <param name="value">Значение</param>
			/// <returns>Допустимое значение компоненты цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Byte ToByte(Int32 value)
			{
				return (Byte)(value < 0 ? 0 : value > 255 ? 255 : value);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Add(ref TColor a, ref TColor b, out TColor result)
			{
				result.A = (Byte)(a.A + b.A);
				result.R = (Byte)(a.R + b.R);
				result.G = (Byte)(a.G + b.G);
				result.B = (Byte)(a.B + b.B);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor Add(TColor a, TColor b)
			{
				return new TColor(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Subtract(ref TColor a, ref TColor b, out TColor result)
			{
				result.A = (Byte)(a.A - b.A);
				result.R = (Byte)(a.R - b.R);
				result.G = (Byte)(a.G - b.G);
				result.B = (Byte)(a.B - b.B);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычитание цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor Subtract(TColor a, TColor b)
			{
				return new TColor(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Модуляция цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Modulate(ref TColor a, ref TColor b, out TColor result)
			{
				result.A = (Byte)(a.A * b.A / 255.0f);
				result.R = (Byte)(a.R * b.R / 255.0f);
				result.G = (Byte)(a.G * b.G / 255.0f);
				result.B = (Byte)(a.B * b.B / 255.0f);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Модуляция цвета
			/// </summary>
			/// <param name="a">Первый цвет</param>
			/// <param name="b">Второй цвет</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor Modulate(TColor a, TColor b)
			{
				return new TColor(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование компонентов цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="scale">Коэффициент масштаба</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Scale(ref TColor value, Single scale, out TColor result)
			{
				result.A = (Byte)(value.A * scale);
				result.R = (Byte)(value.R * scale);
				result.G = (Byte)(value.G * scale);
				result.B = (Byte)(value.B * scale);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Масштабирование компонентов цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="scale">Коэффициент масштаба</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor Scale(TColor value, Single scale)
			{
				return new TColor((Byte)(value.R * scale), (Byte)(value.G * scale), (Byte)(value.B * scale), (Byte)(value.A * scale));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инвертированный цвет
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Negate(ref TColor value, out TColor result)
			{
				result.A = (Byte)(255 - value.A);
				result.R = (Byte)(255 - value.R);
				result.G = (Byte)(255 - value.G);
				result.B = (Byte)(255 - value.B);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инвертированный цвет
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor Negate(TColor value)
			{
				return new TColor(255 - value.R, 255 - value.G, 255 - value.B, 255 - value.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Ограничение цвета в пределах указанного диапазона
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="min">Минимальное значение</param>
			/// <param name="max">Максимальное значение</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void Clamp(ref TColor value, ref TColor min, ref TColor max, out TColor result)
			{
				var alpha = value.A;
				alpha = alpha > max.A ? max.A : alpha;
				alpha = alpha < min.A ? min.A : alpha;

				var red = value.R;
				red = red > max.R ? max.R : red;
				red = red < min.R ? min.R : red;

				var green = value.G;
				green = green > max.G ? max.G : green;
				green = green < min.G ? min.G : green;

				var blue = value.B;
				blue = blue > max.B ? max.B : blue;
				blue = blue < min.B ? min.B : blue;

				result = new TColor(red, green, blue, alpha);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование цвет из упакованного формата BGRA целого числа
			/// </summary>
			/// <param name="color">Значение цвета в BGRA формате целого числа</param>
			/// <returns>Цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor FromBGRA(Int32 color)
			{
				return new TColor((Byte)((color >> 16) & 255), (Byte)((color >> 8) & 255), (Byte)(color & 255), (Byte)((color >> 24) & 255));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование цвет из упакованного формата BGRA целого числа
			/// </summary>
			/// <param name="color">Значение цвета в BGRA формате целого числа</param>
			/// <returns>Цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor FromBGRA(UInt32 color)
			{
				return new TColor((Byte)((color >> 16) & 255), (Byte)((color >> 8) & 255), (Byte)(color & 255), (Byte)((color >> 24) & 255));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование цвет из упакованного формата ABGR целого числа
			/// </summary>
			/// <param name="color">Значение цвета в ABGR формате целого числа</param>
			/// <returns>Цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor FromABGR(Int32 color)
			{
				return new TColor((Byte)(color >> 24), (Byte)(color >> 16), (Byte)(color >> 8), (Byte)color);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Формирование цвет из упакованного формата RGBA целого числа
			/// </summary>
			/// <param name="color">Значение цвета в RGBA формате целого числа</param>
			/// <returns>Цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor FromRGBA(Int32 color)
			{
				return new TColor(color);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регулировка контрастности цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="contrast">Коэффициент контраста</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void AdjustContrast(ref TColor value, Single contrast, out TColor result)
			{
				result.A = value.A;
				result.R = ToByte(0.5f + (contrast * ((value.R / 255.0f) - 0.5f)));
				result.G = ToByte(0.5f + (contrast * ((value.G / 255.0f) - 0.5f)));
				result.B = ToByte(0.5f + (contrast * ((value.B / 255.0f) - 0.5f)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регулировка контрастности цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="contrast">Коэффициент контраста</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor AdjustContrast(TColor value, Single contrast)
			{
				return new TColor(
					ToByte(0.5f + (contrast * ((value.R / 255.0f) - 0.5f))),
					ToByte(0.5f + (contrast * ((value.G / 255.0f) - 0.5f))),
					ToByte(0.5f + (contrast * ((value.B / 255.0f) - 0.5f))),
					value.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регулировка насыщенности цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="saturation">Коэффициент насыщенности</param>
			/// <param name="result">Результирующий цвет</param>
			//---------------------------------------------------------------------------------------------------------
			public static void AdjustSaturation(ref TColor value, Single saturation, out TColor result)
			{
				var grey = (value.R / 255.0f * 0.2125f) + (value.G / 255.0f * 0.7154f) + (value.B / 255.0f * 0.0721f);

				result.A = value.A;
				result.R = ToByte(grey + (saturation * ((value.R / 255.0f) - grey)));
				result.G = ToByte(grey + (saturation * ((value.G / 255.0f) - grey)));
				result.B = ToByte(grey + (saturation * ((value.B / 255.0f) - grey)));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регулировка насыщенности цвета
			/// </summary>
			/// <param name="value">Цвет</param>
			/// <param name="saturation">Коэффициент насыщенности</param>
			/// <returns>Результирующий цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor AdjustSaturation(TColor value, Single saturation)
			{
				var grey = (value.R / 255.0f * 0.2125f) + (value.G / 255.0f * 0.7154f) + (value.B / 255.0f * 0.0721f);

				return new TColor(
					ToByte(grey + (saturation * ((value.R / 255.0f) - grey))),
					ToByte(grey + (saturation * ((value.G / 255.0f) - grey))),
					ToByte(grey + (saturation * ((value.B / 255.0f) - grey))),
					value.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Аппроксимация равенства значений цвета
			/// </summary>
			/// <param name="a">Первое значение</param>
			/// <param name="b">Второе значение</param>
			/// <param name="epsilon">Погрешность</param>
			/// <returns>Статус равенства значений цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean Approximately(in TColor a, in TColor b, Int32 epsilon = 1)
			{
				return Math.Abs(a.R - b.G) <= epsilon &&
					   Math.Abs(a.G - b.G) <= epsilon &&
					   Math.Abs(a.B - b.B) <= epsilon;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Десереализация цвета из строки
			/// </summary>
			/// <param name="data">Строка данных</param>
			/// <returns>Цвет</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TColor DeserializeFromString(String data)
			{
				var color = new TColor();
				var color_data = data.Split(',');
				color.R = Byte.Parse(color_data[0]);
				color.G = Byte.Parse(color_data[1]);
				color.B = Byte.Parse(color_data[2]);
				color.A = Byte.Parse(color_data[3]);
				return color;
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Красная компонента цвета
			/// </summary>
			public Byte R;

			/// <summary>
			/// Зеленая компонента цвета
			/// </summary>
			public Byte G;

			/// <summary>
			/// Синяя компонента цвета
			/// </summary>
			public Byte B;

			/// <summary>
			/// Альфа компонента цвета
			/// </summary>
			public Byte A;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Красная компонента цвета
			/// </summary>
			public Single RedComponent
			{
				get { return R / 255.0f; }
				set { R = ToByte(value); }
			}

			/// <summary>
			/// Зеленая компонента цвета
			/// </summary>
			public Single GreenComponent
			{
				get { return G / 255.0f; }
				set { G = ToByte(value); }
			}

			/// <summary>
			/// Синяя компонента цвета
			/// </summary>
			public Single BlueComponent
			{
				get { return B / 255.0f; }
				set { B = ToByte(value); }
			}

			/// <summary>
			/// Альфа компонента цвета
			/// </summary>
			public Single AlphaComponent
			{
				get { return A / 255.0f; }
				set { A = ToByte(value); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="value">Компонент цвета</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(Byte value)
			{
				A = R = G = B = value;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="red">Красная компонента цвета</param>
			/// <param name="green">Зеленая компонента цвета</param>
			/// <param name="blue">Синяя компонента цвета</param>
			/// <param name="alpha">Альфа компонента цвета</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(Byte red, Byte green, Byte blue, Byte alpha = 255)
			{
				R = red;
				G = green;
				B = blue;
				A = alpha;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="red">Красная компонента цвета</param>
			/// <param name="green">Зеленая компонента цвета</param>
			/// <param name="blue">Синяя компонента цвета</param>
			/// <param name="alpha">Альфа компонента цвета</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(Single red, Single green, Single blue, Single alpha = 1.0f)
			{
				R = ToByte(red);
				G = ToByte(green);
				B = ToByte(blue);
				A = ToByte(alpha);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="rgba">Значение цвета в RGBA формате целого числа</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(Int32 rgba)
			{
				A = (Byte)((rgba >> 24) & 255);
				B = (Byte)((rgba >> 16) & 255);
				G = (Byte)((rgba >> 8) & 255);
				R = (Byte)(rgba & 255);
			}
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="color">Цвет UnityEngine</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(UnityEngine.Color color)
			{
				R = (Byte)(color.r * 255);
				G = (Byte)(color.g * 255);
				B = (Byte)(color.b * 255);
				A = (Byte)(color.a * 255);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="color">Цвет UnityEngine</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(UnityEngine.Color32 color)
			{
				R = color.r;
				G = color.g;
				B = color.b;
				A = color.a;
			}
#endif

#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="color">Цвет WPF</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(System.Windows.Media.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
#endif
#if USE_GDI
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="color">Цвет WPF</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(System.Drawing.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
#endif
#if USE_SHARPDX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="color">Цвет SharpDX</param>
			//---------------------------------------------------------------------------------------------------------
			public TColor(SharpDX.Color color)
			{
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
#endif
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (typeof(TColor) == obj.GetType())
					{
						var color = (TColor)obj;
						return Equals(color);
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства цветов по значению
			/// </summary>
			/// <param name="other">Сравниваемый цвет</param>
			/// <returns>Статус равенства цветов</returns>
			//---------------------------------------------------------------------------------------------------------
#if NET45 || UNITY_2017_1_OR_NEWER
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
			public Boolean Equals(TColor other)
			{
				return R == other.R && G == other.G && B == other.B && A == other.A;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение цветов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый цвет</param>
			/// <returns>Статус сравнения цветов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(TColor other)
			{
				if (R > other.R)
				{
					return 1;
				}
				else
				{
					if (R == other.R && G > other.G)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода цвета
			/// </summary>
			/// <returns>Хеш-код цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				unchecked
				{
					var hash_code = R.GetHashCode();
					hash_code = (hash_code * 397) ^ G.GetHashCode();
					hash_code = (hash_code * 397) ^ B.GetHashCode();
					hash_code = (hash_code * 397) ^ A.GetHashCode();
					return hash_code;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование цвета
			/// </summary>
			/// <returns>Копия цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return MemberwiseClone();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление цвета с указанием значений компонентов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return String.Format(ToStringFormat, A, R, G, B);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на равенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус равенства</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(TColor left, TColor right)
			{
				return left.Equals(right);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов на неравенство
			/// </summary>
			/// <param name="left">Первый объект</param>
			/// <param name="right">Второй объект</param>
			/// <returns>Статус неравенство</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(TColor left, TColor right)
			{
				return !(left == right);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="UnityEngine.Color32">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="UnityEngine.Color32"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator UnityEngine.Color32(TColor color)
			{
				return new UnityEngine.Color32(color.R, color.G, color.B, color.A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="UnityEngine.Color">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="UnityEngine.Color"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator UnityEngine.Color(TColor color)
			{
				return new UnityEngine.Color((color.R / 255.0f), (color.G / 255.0f), 
					(color.B / 255.0f), (color.A / 255.0f));
			}
#endif
#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Windows.Media.Color">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="System.Windows.Media.Color"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator System.Windows.Media.Color(TColor color)
			{
				return (System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
			}
#endif
#if USE_GDI
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="System.Drawing.Color">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="System.Drawing.Color"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator System.Drawing.Color(TColor color)
			{
				return (System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
			}
#endif
#if USE_SHARPDX
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.Color">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="SharpDX.Color"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe static implicit operator SharpDX.Color(TColor color)
			{
				return (*(SharpDX.Color*)&color);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.Color">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="SharpDX.Color"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator SharpDX.Color4(TColor color)
			{
				return (new SharpDX.Color4(color.RedComponent, color.GreenComponent, color.BlueComponent, color.AlphaComponent));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.RawColorBGRA">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="SharpDX.RawColorBGRA"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator global::SharpDX.Mathematics.Interop.RawColorBGRA(TColor value)
			{
				return (new SharpDX.Mathematics.Interop.RawColorBGRA(value.B, value.G, value.R, value.A));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="SharpDX.RawColor4">
			/// </summary>
			/// <param name="color">Цвет</param>
			/// <returns>Объект <see cref="SharpDX.RawColor4"></returns>
			//---------------------------------------------------------------------------------------------------------
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static implicit operator SharpDX.Mathematics.Interop.RawColor4(TColor color)
			{
				return (new SharpDX.Mathematics.Interop.RawColor4(color.RedComponent, color.GreenComponent,
					color.BlueComponent, color.AlphaComponent));
			}
#endif
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация компонентов цвета на основе индекса
			/// </summary>
			/// <param name="index">Индекс компонента</param>
			/// <returns>Компонента цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public Byte this[Int32 index]
			{
				get
				{
					switch (index)
					{
						case 0: return R;
						case 1: return G;
						case 2: return B;
						default: return A;
					}
				}

				set
				{
					switch (index)
					{
						case 0: R = value; break;
						case 1: G = value; break;
						case 2: B = value; break;
						default: A = value; break;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение яркости цвета в модели(HSB)
			/// </summary>
			/// <returns>Яркость цвета в модели(HSB)</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetBrightness()
			{
				var r = R / 255.0f;
				var g = G / 255.0f;
				var b = B / 255.0f;

				Single max, min;

				max = r; min = r;

				if (g > max) max = g;
				if (b > max) max = b;

				if (g < min) min = g;
				if (b < min) min = b;

				return (max + min) / 2;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение оттенка цвета в модели(HSB)
			/// </summary>
			/// <returns>Оттенок цвета в модели(HSB)</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetHue()
			{
				if (R == G && G == B)
				{
					return 0; // 0 makes as good an UNDEFINED value as any
				}

				var r = R / 255.0f;
				var g = G / 255.0f;
				var b = B / 255.0f;

				Single max, min;
				Single delta;
				var hue = 0.0f;

				max = r; min = r;

				if (g > max) max = g;
				if (b > max) max = b;

				if (g < min) min = g;
				if (b < min) min = b;

				delta = max - min;

				if (r == max)
				{
					hue = (g - b) / delta;
				}
				else if (g == max)
				{
					hue = 2 + ((b - r) / delta);
				}
				else if (b == max)
				{
					hue = 4 + ((r - g) / delta);
				}
				hue *= 60;

				if (hue < 0.0f)
				{
					hue += 360.0f;
				}

				return hue;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение насыщенности цвета в модели(HSB)
			/// </summary>
			/// <returns>Насыщенность цвета в модели(HSB)</returns>
			//---------------------------------------------------------------------------------------------------------
			public Single GetSaturation()
			{
				var r = R / 255.0f;
				var g = G / 255.0f;
				var b = B / 255.0f;

				Single max, min;
				Single l, s = 0;

				max = r; min = r;

				if (g > max) max = g;
				if (b > max) max = b;

				if (g < min) min = g;
				if (b < min) min = b;

				// if max == min, then there is no color and
				// the saturation is zero.
				//
				if (max != min)
				{
					l = (max + min) / 2;

					if (l <= .5)
					{
						s = (max - min) / (max + min);
					}
					else
					{
						s = (max - min) / (2 - max - min);
					}
				}
				return s;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сериализация цвета в строку
			/// </summary>
			/// <returns>Строка данных</returns>
			//---------------------------------------------------------------------------------------------------------
			public String SerializeToString()
			{
				return String.Format("{0},{1},{2},{3}", R, G, B, A);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к цвету в формате RGBA
			/// </summary>
			/// <returns>Цвет в формате RGBA</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 ToRGBA()
			{
				return (R << 24) | (G << 16) | (B << 8) | A;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению в шестнадцатеричном формате в порядке RGBA
			/// </summary>
			/// <returns>Текстовое представление цвета</returns>
			//---------------------------------------------------------------------------------------------------------
			public String ToStringHEX()
			{
				return String.Format("{0:x2}{1:x2}{2:x2}{3:x2}", R, G, B, A);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий константы цвета в формате BGRA
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XColorBGRA
		{
			/// <summary>
			/// Нулевой цвет
			/// </summary>
			public const UInt32 Zero = 0x00000000;

			/// <summary>
			/// Прозрачный цвет
			/// </summary>
			public const UInt32 Transparent = 0x00000000;

			/// <summary>
			/// Цвет - AliceBlue
			/// </summary>
			public const UInt32 AliceBlue = 0xFFF0F8FF;

			/// <summary>
			/// Цвет - AntiqueWhite
			/// </summary>
			public const UInt32 AntiqueWhite = 0xFFFAEBD7;

			/// <summary>
			/// Цвет - Aqua
			/// </summary>
			public const UInt32 Aqua = 0xFF00FFFF;

			/// <summary>
			/// Цвет - Aquamarine
			/// </summary>
			public const UInt32 Aquamarine = 0xFF7FFFD4;

			/// <summary>
			/// Цвет - Azure
			/// </summary>
			public const UInt32 Azure = 0xFFF0FFFF;

			/// <summary>
			/// Цвет - Beige
			/// </summary>
			public const UInt32 Beige = 0xFFF5F5DC;

			/// <summary>
			/// Цвет - Bisque
			/// </summary>
			public const UInt32 Bisque = 0xFFFFE4C4;

			/// <summary>
			/// Цвет - Black
			/// </summary>
			public const UInt32 Black = 0xFF000000;

			/// <summary>
			/// Цвет - BlanchedAlmond
			/// </summary>
			public const UInt32 BlanchedAlmond = 0xFFFFEBCD;

			/// <summary>
			/// Цвет - Blue
			/// </summary>
			public const UInt32 Blue = 0xFF0000FF;

			/// <summary>
			/// Цвет - BlueViolet
			/// </summary>
			public const UInt32 BlueViolet = 0xFF8A2BE2;

			/// <summary>
			/// Цвет - Brown
			/// </summary>
			public const UInt32 Brown = 0xFFA52A2A;

			/// <summary>
			/// Цвет - BurlyWood
			/// </summary>
			public const UInt32 BurlyWood = 0xFFDEB887;

			/// <summary>
			/// Цвет - CadetBlue
			/// </summary>
			public const UInt32 CadetBlue = 0xFF5F9EA0;

			/// <summary>
			/// Цвет - Chartreuse
			/// </summary>
			public const UInt32 Chartreuse = 0xFF7FFF00;

			/// <summary>
			/// Цвет - Chocolate
			/// </summary>
			public const UInt32 Chocolate = 0xFFD2691E;

			/// <summary>
			/// Цвет - Coral
			/// </summary>
			public const UInt32 Coral = 0xFFFF7F50;

			/// <summary>
			/// Цвет - CornflowerBlue
			/// </summary>
			public const UInt32 CornflowerBlue = 0xFF6495ED;

			/// <summary>
			/// Цвет - Cornsilk
			/// </summary>
			public const UInt32 Cornsilk = 0xFFFFF8DC;

			/// <summary>
			/// Цвет - Crimson
			/// </summary>
			public const UInt32 Crimson = 0xFFDC143C;

			/// <summary>
			/// Цвет - Cyan
			/// </summary>
			public const UInt32 Cyan = 0xFF00FFFF;

			/// <summary>
			/// Цвет - DarkBlue
			/// </summary>
			public const UInt32 DarkBlue = 0xFF00008B;

			/// <summary>
			/// Цвет - DarkCyan
			/// </summary>
			public const UInt32 DarkCyan = 0xFF008B8B;

			/// <summary>
			/// Цвет - DarkGoldenrod
			/// </summary>
			public const UInt32 DarkGoldenrod = 0xFFB8860B;

			/// <summary>
			/// Цвет - DarkGray
			/// </summary>
			public const UInt32 DarkGray = 0xFFA9A9A9;

			/// <summary>
			/// Цвет - DarkGreen
			/// </summary>
			public const UInt32 DarkGreen = 0xFF006400;

			/// <summary>
			/// Цвет - DarkKhaki
			/// </summary>
			public const UInt32 DarkKhaki = 0xFFBDB76B;

			/// <summary>
			/// Цвет - DarkMagenta
			/// </summary>
			public const UInt32 DarkMagenta = 0xFF8B008B;

			/// <summary>
			/// Цвет - DarkOliveGreen
			/// </summary>
			public const UInt32 DarkOliveGreen = 0xFF556B2F;

			/// <summary>
			/// Цвет - DarkOrange
			/// </summary>
			public const UInt32 DarkOrange = 0xFFFF8C00;

			/// <summary>
			/// Цвет - DarkOrchid
			/// </summary>
			public const UInt32 DarkOrchid = 0xFF9932CC;

			/// <summary>
			/// Цвет - DarkRed
			/// </summary>
			public const UInt32 DarkRed = 0xFF8B0000;

			/// <summary>
			/// Цвет - DarkSalmon
			/// </summary>
			public const UInt32 DarkSalmon = 0xFFE9967A;

			/// <summary>
			/// Цвет - DarkSeaGreen
			/// </summary>
			public const UInt32 DarkSeaGreen = 0xFF8FBC8B;

			/// <summary>
			/// Цвет - DarkSlateBlue
			/// </summary>
			public const UInt32 DarkSlateBlue = 0xFF483D8B;

			/// <summary>
			/// Цвет - DarkSlateGray
			/// </summary>
			public const UInt32 DarkSlateGray = 0xFF2F4F4F;

			/// <summary>
			/// Цвет - DarkTurquoise
			/// </summary>
			public const UInt32 DarkTurquoise = 0xFF00CED1;

			/// <summary>
			/// Цвет - DarkViolet
			/// </summary>
			public const UInt32 DarkViolet = 0xFF9400D3;

			/// <summary>
			/// Цвет - DeepPink
			/// </summary>
			public const UInt32 DeepPink = 0xFFFF1493;

			/// <summary>
			/// Цвет - DeepSkyBlue
			/// </summary>
			public const UInt32 DeepSkyBlue = 0xFF00BFFF;

			/// <summary>
			/// Цвет - DimGray
			/// </summary>
			public const UInt32 DimGray = 0xFF696969;

			/// <summary>
			/// Цвет - DodgerBlue
			/// </summary>
			public const UInt32 DodgerBlue = 0xFF1E90FF;

			/// <summary>
			/// Цвет - Firebrick
			/// </summary>
			public const UInt32 Firebrick = 0xFFB22222;

			/// <summary>
			/// Цвет - FloralWhite
			/// </summary>
			public const UInt32 FloralWhite = 0xFFFFFAF0;

			/// <summary>
			/// Цвет - ForestGreen
			/// </summary>
			public const UInt32 ForestGreen = 0xFF228B22;

			/// <summary>
			/// Цвет - Fuchsia
			/// </summary>
			public const UInt32 Fuchsia = 0xFFFF00FF;

			/// <summary>
			/// Цвет - Gainsboro
			/// </summary>
			public const UInt32 Gainsboro = 0xFFDCDCDC;

			/// <summary>
			/// Цвет - GhostWhite
			/// </summary>
			public const UInt32 GhostWhite = 0xFFF8F8FF;

			/// <summary>
			/// Цвет - Gold
			/// </summary>
			public const UInt32 Gold = 0xFFFFD700;

			/// <summary>
			/// Цвет - Goldenrod
			/// </summary>
			public const UInt32 Goldenrod = 0xFFDAA520;

			/// <summary>
			/// Цвет - Gray
			/// </summary>
			public const UInt32 Gray = 0xFF808080;

			/// <summary>
			/// Цвет - Green
			/// </summary>
			public const UInt32 Green = 0xFF008000;

			/// <summary>
			/// Цвет - GreenYellow
			/// </summary>
			public const UInt32 GreenYellow = 0xFFADFF2F;

			/// <summary>
			/// Цвет - Honeydew
			/// </summary>
			public const UInt32 Honeydew = 0xFFF0FFF0;

			/// <summary>
			/// Цвет - HotPink
			/// </summary>
			public const UInt32 HotPink = 0xFFFF69B4;

			/// <summary>
			/// Цвет - IndianRed
			/// </summary>
			public const UInt32 IndianRed = 0xFFCD5C5C;

			/// <summary>
			/// Цвет - Indigo
			/// </summary>
			public const UInt32 Indigo = 0xFF4B0082;

			/// <summary>
			/// Цвет - Ivory
			/// </summary>
			public const UInt32 Ivory = 0xFFFFFFF0;

			/// <summary>
			/// Цвет - Khaki
			/// </summary>
			public const UInt32 Khaki = 0xFFF0E68C;

			/// <summary>
			/// Цвет - Lavender
			/// </summary>
			public const UInt32 Lavender = 0xFFE6E6FA;

			/// <summary>
			/// Цвет - LavenderBlush
			/// </summary>
			public const UInt32 LavenderBlush = 0xFFFFF0F5;

			/// <summary>
			/// Цвет - LawnGreen
			/// </summary>
			public const UInt32 LawnGreen = 0xFF7CFC00;

			/// <summary>
			/// Цвет - LemonChiffon
			/// </summary>
			public const UInt32 LemonChiffon = 0xFFFFFACD;

			/// <summary>
			/// Цвет - LightBlue
			/// </summary>
			public const UInt32 LightBlue = 0xFFADD8E6;

			/// <summary>
			/// Цвет - LightCoral
			/// </summary>
			public const UInt32 LightCoral = 0xFFF08080;

			/// <summary>
			/// Цвет - LightCyan
			/// </summary>
			public const UInt32 LightCyan = 0xFFE0FFFF;

			/// <summary>
			/// Цвет - LightGoldenrodYellow
			/// </summary>
			public const UInt32 LightGoldenrodYellow = 0xFFFAFAD2;

			/// <summary>
			/// Цвет - LightGray
			/// </summary>
			public const UInt32 LightGray = 0xFFD3D3D3;

			/// <summary>
			/// Цвет - LightGreen
			/// </summary>
			public const UInt32 LightGreen = 0xFF90EE90;

			/// <summary>
			/// Цвет - LightPink
			/// </summary>
			public const UInt32 LightPink = 0xFFFFB6C1;

			/// <summary>
			/// Цвет - LightSalmon
			/// </summary>
			public const UInt32 LightSalmon = 0xFFFFA07A;

			/// <summary>
			/// Цвет - LightSeaGreen
			/// </summary>
			public const UInt32 LightSeaGreen = 0xFF20B2AA;

			/// <summary>
			/// Цвет - LightSkyBlue
			/// </summary>
			public const UInt32 LightSkyBlue = 0xFF87CEFA;

			/// <summary>
			/// Цвет - LightSlateGray
			/// </summary>
			public const UInt32 LightSlateGray = 0xFF778899;

			/// <summary>
			/// Цвет - LightSteelBlue
			/// </summary>
			public const UInt32 LightSteelBlue = 0xFFB0C4DE;

			/// <summary>
			/// Цвет - LightYellow
			/// </summary>
			public const UInt32 LightYellow = 0xFFFFFFE0;

			/// <summary>
			/// Цвет - Lime
			/// </summary>
			public const UInt32 Lime = 0xFF00FF00;

			/// <summary>
			/// Цвет - LimeGreen
			/// </summary>
			public const UInt32 LimeGreen = 0xFF32CD32;

			/// <summary>
			/// Цвет - Linen
			/// </summary>
			public const UInt32 Linen = 0xFFFAF0E6;

			/// <summary>
			/// Цвет - Magenta
			/// </summary>
			public const UInt32 Magenta = 0xFFFF00FF;

			/// <summary>
			/// Цвет - Maroon
			/// </summary>
			public const UInt32 Maroon = 0xFF800000;

			/// <summary>
			/// Цвет - MediumAquamarine
			/// </summary>
			public const UInt32 MediumAquamarine = 0xFF66CDAA;

			/// <summary>
			/// Цвет - MediumBlue
			/// </summary>
			public const UInt32 MediumBlue = 0xFF0000CD;

			/// <summary>
			/// Цвет - MediumOrchid
			/// </summary>
			public const UInt32 MediumOrchid = 0xFFBA55D3;

			/// <summary>
			/// Цвет - MediumPurple
			/// </summary>
			public const UInt32 MediumPurple = 0xFF9370DB;

			/// <summary>
			/// Цвет - MediumSeaGreen
			/// </summary>
			public const UInt32 MediumSeaGreen = 0xFF3CB371;

			/// <summary>
			/// Цвет - MediumSlateBlue
			/// </summary>
			public const UInt32 MediumSlateBlue = 0xFF7B68EE;

			/// <summary>
			/// Цвет - MediumSpringGreen
			/// </summary>
			public const UInt32 MediumSpringGreen = 0xFF00FA9A;

			/// <summary>
			/// Цвет - MediumTurquoise
			/// </summary>
			public const UInt32 MediumTurquoise = 0xFF48D1CC;

			/// <summary>
			/// Цвет - MediumVioletRed
			/// </summary>
			public const UInt32 MediumVioletRed = 0xFFC71585;

			/// <summary>
			/// Цвет - MidnightBlue
			/// </summary>
			public const UInt32 MidnightBlue = 0xFF191970;

			/// <summary>
			/// Цвет - MintCream
			/// </summary>
			public const UInt32 MintCream = 0xFFF5FFFA;

			/// <summary>
			/// Цвет - MistyRose
			/// </summary>
			public const UInt32 MistyRose = 0xFFFFE4E1;

			/// <summary>
			/// Цвет - Moccasin
			/// </summary>
			public const UInt32 Moccasin = 0xFFFFE4B5;

			/// <summary>
			/// Цвет - NavajoWhite
			/// </summary>
			public const UInt32 NavajoWhite = 0xFFFFDEAD;

			/// <summary>
			/// Цвет - Navy
			/// </summary>
			public const UInt32 Navy = 0xFF000080;

			/// <summary>
			/// Цвет - OldLace
			/// </summary>
			public const UInt32 OldLace = 0xFFFDF5E6;

			/// <summary>
			/// Цвет - Olive
			/// </summary>
			public const UInt32 Olive = 0xFF808000;

			/// <summary>
			/// Цвет - OliveDrab
			/// </summary>
			public const UInt32 OliveDrab = 0xFF6B8E23;

			/// <summary>
			/// Цвет - Orange
			/// </summary>
			public const UInt32 Orange = 0xFFFFA500;

			/// <summary>
			/// Цвет - OrangeRed
			/// </summary>
			public const UInt32 OrangeRed = 0xFFFF4500;

			/// <summary>
			/// Цвет - Orchid
			/// </summary>
			public const UInt32 Orchid = 0xFFDA70D6;

			/// <summary>
			/// Цвет - PaleGoldenrod
			/// </summary>
			public const UInt32 PaleGoldenrod = 0xFFEEE8AA;

			/// <summary>
			/// Цвет - PaleGreen
			/// </summary>
			public const UInt32 PaleGreen = 0xFF98FB98;

			/// <summary>
			/// Цвет - PaleTurquoise
			/// </summary>
			public const UInt32 PaleTurquoise = 0xFFAFEEEE;

			/// <summary>
			/// Цвет - PaleVioletRed
			/// </summary>
			public const UInt32 PaleVioletRed = 0xFFDB7093;

			/// <summary>
			/// Цвет - PapayaWhip
			/// </summary>
			public const UInt32 PapayaWhip = 0xFFFFEFD5;

			/// <summary>
			/// Цвет - PeachPuff
			/// </summary>
			public const UInt32 PeachPuff = 0xFFFFDAB9;

			/// <summary>
			/// Цвет - Peru
			/// </summary>
			public const UInt32 Peru = 0xFFCD853F;

			/// <summary>
			/// Цвет - Pink
			/// </summary>
			public const UInt32 Pink = 0xFFFFC0CB;

			/// <summary>
			/// Цвет - Plum
			/// </summary>
			public const UInt32 Plum = 0xFFDDA0DD;

			/// <summary>
			/// Цвет - PowderBlue
			/// </summary>
			public const UInt32 PowderBlue = 0xFFB0E0E6;

			/// <summary>
			/// Цвет - Purple
			/// </summary>
			public const UInt32 Purple = 0xFF800080;

			/// <summary>
			/// Цвет - Red
			/// </summary>
			public const UInt32 Red = 0xFFFF0000;

			/// <summary>
			/// Цвет - RosyBrown
			/// </summary>
			public const UInt32 RosyBrown = 0xFFBC8F8F;

			/// <summary>
			/// Цвет - RoyalBlue
			/// </summary>
			public const UInt32 RoyalBlue = 0xFF4169E1;

			/// <summary>
			/// Цвет - SaddleBrown
			/// </summary>
			public const UInt32 SaddleBrown = 0xFF8B4513;

			/// <summary>
			/// Цвет - Salmon
			/// </summary>
			public const UInt32 Salmon = 0xFFFA8072;

			/// <summary>
			/// Цвет - SandyBrown
			/// </summary>
			public const UInt32 SandyBrown = 0xFFF4A460;

			/// <summary>
			/// Цвет - SeaGreen
			/// </summary>
			public const UInt32 SeaGreen = 0xFF2E8B57;

			/// <summary>
			/// Цвет - SeaShell
			/// </summary>
			public const UInt32 SeaShell = 0xFFFFF5EE;

			/// <summary>
			/// Цвет - Sienna
			/// </summary>
			public const UInt32 Sienna = 0xFFA0522D;

			/// <summary>
			/// Цвет - Silver
			/// </summary>
			public const UInt32 Silver = 0xFFC0C0C0;

			/// <summary>
			/// Цвет - SkyBlue
			/// </summary>
			public const UInt32 SkyBlue = 0xFF87CEEB;

			/// <summary>
			/// Цвет - SlateBlue
			/// </summary>
			public const UInt32 SlateBlue = 0xFF6A5ACD;

			/// <summary>
			/// Цвет - SlateGray
			/// </summary>
			public const UInt32 SlateGray = 0xFF708090;

			/// <summary>
			/// Цвет - Snow
			/// </summary>
			public const UInt32 Snow = 0xFFFFFAFA;

			/// <summary>
			/// Цвет - SpringGreen
			/// </summary>
			public const UInt32 SpringGreen = 0xFF00FF7F;

			/// <summary>
			/// Цвет - SteelBlue
			/// </summary>
			public const UInt32 SteelBlue = 0xFF4682B4;

			/// <summary>
			/// Цвет - Tan
			/// </summary>
			public const UInt32 Tan = 0xFFD2B48C;

			/// <summary>
			/// Цвет - Teal
			/// </summary>
			public const UInt32 Teal = 0xFF008080;

			/// <summary>
			/// Цвет - Thistle
			/// </summary>
			public const UInt32 Thistle = 0xFFD8BFD8;

			/// <summary>
			/// Цвет - Tomato
			/// </summary>
			public const UInt32 Tomato = 0xFFFF6347;

			/// <summary>
			/// Цвет - Turquoise
			/// </summary>
			public const UInt32 Turquoise = 0xFF40E0D0;

			/// <summary>
			/// Цвет - Violet
			/// </summary>
			public const UInt32 Violet = 0xFFEE82EE;

			/// <summary>
			/// Цвет - Wheat
			/// </summary>
			public const UInt32 Wheat = 0xFFF5DEB3;

			/// <summary>
			/// Цвет - White
			/// </summary>
			public const UInt32 White = 0xFFFFFFFF;

			/// <summary>
			/// Цвет - WhiteSmoke
			/// </summary>
			public const UInt32 WhiteSmoke = 0xFFF5F5F5;

			/// <summary>
			/// Цвет - Yellow
			/// </summary>
			public const UInt32 Yellow = 0xFFFFFF00;

			/// <summary>
			/// Цвет - YellowGreen
			/// </summary>
			public const UInt32 YellowGreen = 0xFF9ACD32;
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================