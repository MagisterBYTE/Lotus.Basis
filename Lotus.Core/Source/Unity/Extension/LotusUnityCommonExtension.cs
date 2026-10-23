#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;

using UnityEngine;

namespace Lotus.Core
{
    
    /** 
     * \defgroup CoreUnityExtension Методы расширений Unity
     * \ingroup Core
     * \brief Методы расширения для базовых типов Unity.
     * @{
     */
    
    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Color"/>
    /// </summary>
    
    public static class XUnityColorExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация цветового значения в строку
        /// </summary>
        /// <param name="this">Цветовое значение</param>
        /// <returns>Строка</returns>
        
        public static string SerializeToString(this Color @this)
        {
            return string.Format("{0},{1},{2},{3}", (int)(@this.r * 255.0f), (int)(@this.g * 255.0f),
                (int)(@this.b * 255.0f), (int)(@this.a * 255.0f));
        }

        /// <summary>
        /// Преобразование к цвету в формате RGBA
        /// </summary>
        /// <param name="this">Цветовое значение</param>
        /// <returns>Цвет в формате RGBA</returns>
        
        public static int ToRGBA(this Color @this)
        {
            var red = @this.r >= 1.0f ? 0xff :
                           @this.r <= 0.0f ? 0x00 : (int)((@this.r * 255.0f) + 0.5f);
            var green = @this.g >= 1.0f ? 0xff :
                           @this.g <= 0.0f ? 0x00 : (int)((@this.g * 255.0f) + 0.5f);
            var blue = @this.b >= 1.0f ? 0xff :
                           @this.b <= 0.0f ? 0x00 : (int)((@this.b * 255.0f) + 0.5f);
            var alpha = @this.a >= 1.0f ? 0xff :
                           @this.a <= 0.0f ? 0x00 : (int)((@this.a * 255.0f) + 0.5f);

            return (red << 24) | (green << 16) | (blue << 8) | alpha;
        }

        /// <summary>
        /// Преобразование к 32-битному цветовому значению
        /// </summary>
        /// <param name="this">Цветовое значение</param>
        /// <returns>32-битное цветовое значение</returns>
        
        public static Color32 ToColor32(this Color @this)
        {
            const float factor = 255.0f;
            var color = new Color32();
            color.r = (byte)(@this.r * factor);
            color.g = (byte)(@this.g * factor);
            color.b = (byte)(@this.b * factor);
            color.a = (byte)(@this.a * factor);

            return color;
        }

        /// <summary>
        /// Преобразование к 32-битному цветовому значению
        /// </summary>
        /// <param name="this">Цветовое значение</param>
        /// <returns>32-битное цветовое значение</returns>
        
        public static TColor ToTColor(this Color @this)
        {
            const float factor = 255.0f;
            var color = new TColor();
            color.R = (byte)(@this.r * factor);
            color.G = (byte)(@this.g * factor);
            color.B = (byte)(@this.b * factor);
            color.A = (byte)(@this.a * factor);

            return color;
        }

        /// <summary>
        /// Аппроксимация равенства цветовых значений
        /// </summary>
        /// <param name="this">Первое значение</param>
        /// <param name="color">Второе значение</param>
        /// <param name="epsilon">Погрешность</param>
        /// <returns>Статус равенства значений</returns>
        
        public static bool Approximately(this Color @this, Color color, float epsilon)
        {
            if (Mathf.Abs(@this.r - color.r) < epsilon &&
                Mathf.Abs(@this.g - color.g) < epsilon &&
                Mathf.Abs(@this.b - color.b) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Аппроксимация равенства цветовых значений с учетом альфа канала
        /// </summary>
        /// <param name="this">Первое значение</param>
        /// <param name="color">Второе значение</param>
        /// <param name="epsilon">Погрешность</param>
        /// <returns>Статус равенства значений</returns>
        
        public static bool ApproximatelyWithAlpha(this Color @this, Color color, float epsilon)
        {
            if (Mathf.Abs(@this.r - color.r) < epsilon &&
                Mathf.Abs(@this.g - color.g) < epsilon &&
                Mathf.Abs(@this.b - color.b) < epsilon &&
                Mathf.Abs(@this.a - color.a) < epsilon)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Получение инвертированного цвета без учета альфа канала
        /// </summary>
        /// <param name="this">Цвет</param>
        /// <returns>Инвертированный цвет</returns>
        
        public static Color Inverted(this Color @this)
        {
            var result = Color.white - @this;
            result.a = @this.a;
            return result;
        }

        /// <summary>
        /// Получение цвети с измененной компонентой красного канала
        /// </summary>
        /// <param name="this">Цвет</param>
        /// <param name="red">Новое значение красной компоненты</param>
        /// <returns>Модифицированный цвет</returns>
        
        public static Color WithRed(this Color @this, float red)
        {
            return new Color(red, @this.g, @this.b, @this.a);
        }

        /// <summary>
        /// Получение цвети с измененной компонентой зеленого канала
        /// </summary>
        /// <param name="this">Цвет</param>
        /// <param name="green">Новое значение зеленой компоненты</param>
        /// <returns>Модифицированный цвет</returns>
        
        public static Color WithGreen(this Color @this, float green)
        {
            return new Color(@this.r, green, @this.b, @this.a);
        }

        /// <summary>
        /// Получение цвети с измененной компонентой синего канала
        /// </summary>
        /// <param name="this">Цвет</param>
        /// <param name="blue">Новое значение синий компоненты</param>
        /// <returns>Модифицированный цвет</returns>
        
        public static Color WithBlue(this Color @this, float blue)
        {
            return new Color(@this.r, @this.g, blue, @this.a);
        }

        /// <summary>
        /// Получение цвети с измененной компонентой альфа канала
        /// </summary>
        /// <param name="this">Цвет</param>
        /// <param name="alpha">Новое значение альфа компоненты</param>
        /// <returns>Модифицированный цвет</returns>
        
        public static Color WithAlpha(this Color @this, float alpha)
        {
            return new Color(@this.r, @this.g, @this.b, alpha);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Color32"/>
    /// </summary>
    
    public static class XUnityColor32Extension
    {
        #region Main methods
        
        /// <summary>
        /// Преобразование к цвету в формате RGBA
        /// </summary>
        /// <param name="this">32-битное цветовое значение</param>
        /// <returns>Цвет в формате RGBA</returns>
        
        public static int ToRGBA(this Color32 @this)
        {
            return (@this.r << 24) | (@this.g << 16) | (@this.b << 8) | @this.a;
        }

        /// <summary>
        /// Преобразование к цветовому значению
        /// </summary>
        /// <param name="this">32-битное цветовое значение</param>
        /// <returns>Цветовое значение</returns>
        
        public static Color ToColor(this Color32 @this)
        {
            const float factor = 1.0f / 255.0f;
            var color = new Color();
            color.r = @this.r * factor;
            color.g = @this.g * factor;
            color.b = @this.b * factor;
            color.a = @this.a * factor;

            return color;
        }

        /// <summary>
        /// Сериализация цветового значения в строку
        /// </summary>
        /// <param name="this">Цветовое значение</param>
        /// <returns>Строка</returns>
        
        public static string SerializeToString(this Color32 @this)
        {
            return string.Format("{0},{1},{2},{3}", @this.r, @this.g, @this.b, @this.a);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Rect"/>
    /// </summary>
    
    public static class XUnityRectExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация прямоугольника в строку
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <returns>Строка данных</returns>
        
        public static string SerializeToString(this Rect @this)
        {
            return string.Format("{0};{1};{2};{3}", @this.x, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника полученного в результате пересечения
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rectOther">Прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате пересечения</returns>
        
        public static Rect Intersect(this Rect @this, Rect rectOther)
        {
            return XUnityRect.IntersectRect(rectOther, @this);
        }

        /// <summary>
        /// Получение прямоугольника полученного в результате объединения
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rectOther">Прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате объединения</returns>
        
        public static Rect Union(this Rect @this, Rect rectOther)
        {
            return XUnityRect.UnionRect(rectOther, @this);
        }

        /// <summary>
        /// Увеличение/уменьшение размеров прямоугольника из центра на указанные величины
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <returns>Прямоугольник полученный в результате увеличения/уменьшения</returns>
        
        public static Rect Inflate(this Rect @this, float width, float height)
        {
            return new Rect(@this.x - width, @this.y - height, @this.width + (2 * width), @this.height + (2 * height));
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов и возвращение прямоугольника столбца по индексу
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="columnCount">Количество столбцов</param>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <returns>Прямоугольник столбца по индексу</returns>
        
        public static Rect GetColumnFromIndex(this Rect @this, int columnCount, int columnIndex)
        {
            var rect = new Rect(@this.x + (columnIndex * @this.width / columnCount),
                @this.y, @this.width / columnCount, @this.height);

            return rect;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumns(this Rect @this, float percentOne, ref Rect rectOne,
            float percentTwo, ref Rect rectTwo, float offsetFromLeft, float offsetFromRight)
        {
            var width = @this.width;

            rectOne.x = @this.x;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumnsWithOffset(this Rect @this, float percentOne, ref Rect rectOne,
            float percentTwo, ref Rect rectTwo, float offsetFromLeft, float offsetFromRight)
        {
            var width = @this.width - (offsetFromLeft + offsetFromRight);

            rectOne.x = @this.x + offsetFromLeft;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением справа
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumnsWithOffsetRight(this Rect @this, float percentOne, ref Rect rectOne,
            float percentTwo, ref Rect rectTwo, float offsetFromRight)
        {
            var width = @this.width - (offsetFromRight);

            rectOne.x = @this.x;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением слева
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        
        public static void GetColumnsWithOffsetLeft(this Rect @this, float percentOne, ref Rect rectOne,
            float percentTwo, ref Rect rectTwo, float offsetFromLeft)
        {
            var width = @this.width - (offsetFromLeft);

            rectOne.x = @this.x + offsetFromLeft;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество строк и возвращение прямоугольника строки по индексу
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rowCount">Количество строк</param>
        /// <param name="rowIndex">Индекс строки</param>
        /// <returns>Прямоугольник строки по индексу</returns>
        
        public static Rect GetRowFromIndex(this Rect @this, int rowCount, int rowIndex)
        {
            var rect = new Rect(@this.x, @this.y + (rowIndex * @this.height / rowCount), 
                @this.width, @this.height / rowCount);

            return rect;
        }

        /// <summary>
        /// Получение прямоугольника смещенного по вертикали и горизонтали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetX">Смещение по горизонтали</param>
        /// <param name="offsetY">Смещение по вертикали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static Rect GetFromOffset(this Rect @this, float offsetX, float offsetY)
        {
            return new Rect(@this.x + offsetX, @this.y + offsetY, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника смещенного по вертикали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetY">Смещение по вертикали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static Rect GetFromOffsetY(this Rect @this, float offsetY)
        {
            return new Rect(@this.x, @this.y + offsetY, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника смещенного по горизонтали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetX">Смещение по горизонтали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static Rect GetFromOffsetX(this Rect @this, float offsetX)
        {
            return new Rect(@this.x + offsetX, @this.y, @this.width, @this.height);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.RectInt"/>
    /// </summary>
    
    public static class XUnityRectIntExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация прямоугольника в строку
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <returns>Строка данных</returns>
        
        public static string SerializeToString(this RectInt @this)
        {
            return string.Format("{0};{1};{2};{3}", @this.x, @this.y, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника полученного в результате пересечения
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rectOther">Прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате пересечения</returns>
        
        public static RectInt Intersect(this RectInt @this, RectInt rectOther)
        {
            return XUnityRectInt.IntersectRect(rectOther, @this);
        }

        /// <summary>
        /// Получение прямоугольника полученного в результате объединения
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rectOther">Прямоугольник</param>
        /// <returns>Прямоугольник полученный в результате объединения</returns>
        
        public static RectInt Union(this RectInt @this, RectInt rectOther)
        {
            return XUnityRectInt.UnionRect(rectOther, @this);
        }

        /// <summary>
        /// Увеличение/уменьшение размеров прямоугольника из центра на указанные величины
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <returns>Прямоугольник полученный в результате увеличения/уменьшения</returns>
        
        public static RectInt Inflate(this RectInt @this, int width, int height)
        {
            return new RectInt(@this.x - width, @this.y - height, @this.width + (2 * width), @this.height + (2 * height));
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов и возвращение прямоугольника столбца по индексу
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="columnCount">Количество столбцов</param>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <returns>Прямоугольник столбца по индексу</returns>
        
        public static RectInt GetColumnFromIndex(this RectInt @this, int columnCount, int columnIndex)
        {
            var rect = new RectInt(@this.x + (columnIndex * @this.width / columnCount),
                @this.y, @this.width / columnCount, @this.height);

            return rect;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumns(this RectInt @this, int percentOne, ref RectInt rectOne,
            int percentTwo, ref RectInt rectTwo, int offsetFromLeft, int offsetFromRight)
        {
            var width = @this.width;

            rectOne.x = @this.x;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumnsWithOffset(this RectInt @this, int percentOne, ref RectInt rectOne,
            int percentTwo, ref RectInt rectTwo, int offsetFromLeft, int offsetFromRight)
        {
            var width = @this.width - (offsetFromLeft + offsetFromRight);

            rectOne.x = @this.x + offsetFromLeft;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением справа
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromRight">Дополнительное абсолютное смещение справа</param>
        
        public static void GetColumnsWithOffsetRight(this RectInt @this, int percentOne, ref RectInt rectOne,
            int percentTwo, ref RectInt rectTwo, int offsetFromRight)
        {
            var width = @this.width - (offsetFromRight);

            rectOne.x = @this.x;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество столбцов со смещением слева
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="percentOne">Процент ширины первого прямоугольника</param>
        /// <param name="rectOne">Прямоугольник для сохранения результата</param>
        /// <param name="percentTwo">Процент ширины второго прямоугольника</param>
        /// <param name="rectTwo">Прямоугольник для сохранения результата</param>
        /// <param name="offsetFromLeft">Дополнительное абсолютное смещение слева</param>
        
        public static void GetColumnsWithOffsetLeft(this RectInt @this, int percentOne, ref RectInt rectOne,
            int percentTwo, ref RectInt rectTwo, int offsetFromLeft)
        {
            var width = @this.width - (offsetFromLeft);

            rectOne.x = @this.x + offsetFromLeft;
            rectOne.y = @this.y;
            rectOne.width = width * percentOne;
            rectOne.height = @this.height;

            rectTwo.x = rectOne.xMax;
            rectTwo.y = @this.y;
            rectTwo.width = width * percentTwo;
            rectTwo.height = @this.height;
        }

        /// <summary>
        /// Деление прямоугольника на указанное количество строк и возвращение прямоугольника строки по индексу
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="rowCount">Количество строк</param>
        /// <param name="rowIndex">Индекс строки</param>
        /// <returns>Прямоугольник строки по индексу</returns>
        
        public static RectInt GetRowFromIndex(this RectInt @this, int rowCount, int rowIndex)
        {
            var rect = new RectInt(@this.x, @this.y + (rowIndex * @this.height / rowCount),
                @this.width, @this.height / rowCount);

            return rect;
        }

        /// <summary>
        /// Получение прямоугольника смещенного по вертикали и горизонтали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetX">Смещение по горизонтали</param>
        /// <param name="offsetY">Смещение по вертикали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static RectInt GetFromOffset(this RectInt @this, int offsetX, int offsetY)
        {
            return new RectInt(@this.x + offsetX, @this.y + offsetY, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника смещенного по вертикали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetY">Смещение по вертикали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static RectInt GetFromOffsetY(this RectInt @this, int offsetY)
        {
            return new RectInt(@this.x, @this.y + offsetY, @this.width, @this.height);
        }

        /// <summary>
        /// Получение прямоугольника смещенного по горизонтали
        /// </summary>
        /// <param name="this">Прямоугольник</param>
        /// <param name="offsetX">Смещение по горизонтали</param>
        /// <returns>Прямоугольник полученный в результате смещения</returns>
        
        public static RectInt GetFromOffsetX(this RectInt @this, int offsetX)
        {
            return new RectInt(@this.x + offsetX, @this.y, @this.width, @this.height);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.RectOffset"/>
    /// </summary>
    
    public static class XUnityRectOffsetExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация прямоугольника смещения в строку
        /// </summary>
        /// <param name="this">Прямоугольник смещения</param>
        /// <returns>Строка данных</returns>
        
        public static string SerializeToString(this RectOffset @this)
        {
            return string.Format("{0};{1};{2};{3}", @this.left, @this.top, @this.right, @this.bottom);
        }

        /// <summary>
        /// Установка границ по границам спрайта
        /// </summary>
        /// <param name="this">Прямоугольник смещения</param>
        /// <param name="border">Границы спрайта</param>
        
        public static void SetFromBorderSprite(this RectOffset @this, Vector4 border)
        {
            @this.left = (int)border.x;
            @this.top = (int)border.w;
            @this.right = (int)border.z;
            @this.bottom = (int)border.y;
        }

        /// <summary>
        /// Конвертация в стандартную форму границ спрайта
        /// </summary>
        /// <param name="this">Прямоугольник смещения</param>
        /// <returns>Границы спрайта</returns>
        
        public static Vector4 ToBorderSprite(this RectOffset @this)
        {
            var border = new Vector4(@this.left, @this.bottom, @this.right, @this.top);
            return (border);
        }

        /// <summary>
        /// Конвертация в стандартную форму границ
        /// </summary>
        /// <param name="this">Прямоугольник смещения</param>
        /// <returns>Границы</returns>
        
        public static Vector4 ToBorderDefault(this RectOffset @this)
        {
            var border = new Vector4(@this.left, @this.top, @this.right, @this.bottom);
            return (border);
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.Bounds"/>
    /// </summary>
    
    public static class XUnityBoundsExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация прямоугольника AABB в строку
        /// </summary>
        /// <param name="this">Прямоугольник AABB</param>
        /// <returns>Строка данных</returns>
        
        public static string SerializeToString(this Bounds @this)
        {
            return string.Format("{0};{1};{2};{3};{4};{5}", @this.center.x, @this.center.y, @this.center.z,
                @this.extents.x, @this.extents.y, @this.extents.z);
        }

        /// <summary>
        /// Получение максимальной длины стороны ограничивающего объема
        /// </summary>
        /// <param name="this">Ограничивающий объем</param>
        /// <returns>Максимальная длина стороны</returns>
        
        public static float GetMaxExtents(this Bounds @this)
        {
            if (@this.extents.x >= @this.extents.y && @this.extents.x >= @this.extents.z)
            {
                return @this.extents.x;
            }
            else
            {
                if (@this.extents.y >= @this.extents.x && @this.extents.y >= @this.extents.z)
                {
                    return @this.extents.y;
                }
                else
                {
                    return @this.extents.z;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.BoundsInt"/>
    /// </summary>
    
    public static class XUnityBoundsIntExtension
    {
        #region Main methods
        
        /// <summary>
        /// Сериализация прямоугольника AABB в строку
        /// </summary>
        /// <param name="this">Прямоугольник AABB</param>
        /// <returns>Строка данных</returns>
        
        public static string SerializeToString(this BoundsInt @this)
        {
            return string.Format("{0};{1};{2};{3};{4};{5}", @this.x, @this.y, @this.z,
                @this.size.x, @this.size.y, @this.size.z);
        }

        /// <summary>
        /// Получение максимальной длины стороны ограничивающего объема
        /// </summary>
        /// <param name="this">Ограничивающий объем</param>
        /// <returns>Максимальная длина стороны</returns>
        
        public static float GetMaxExtents(this BoundsInt @this)
        {
            if (@this.max.x >= @this.max.y && @this.max.x >= @this.max.z)
            {
                return @this.max.x;
            }
            else
            {
                if (@this.max.y >= @this.max.x && @this.max.y >= @this.max.z)
                {
                    return @this.max.y;
                }
                else
                {
                    return @this.max.z;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для типа <see cref="UnityEngine.LayerMask"/>
    /// </summary>
    
    public static class XLayerMaskExtension
    {
        #region Main methods
        
        /// <summary>
        /// Обращение маски слоев
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask Inverse(this LayerMask @this)
        {
            return ~@this;
        }

        /// <summary>
        /// Добавление к маски слоев указанный слоев
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <param name="layerNames">Список имен слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask AddToMask(this LayerMask @this, string[] layerNames)
        {
            return @this | XUnityLayerMask.NamesToMask(layerNames);
        }

        /// <summary>
        /// Удаление из маски слоев указанный слоев
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <param name="layerNames">Список имен слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask RemoveFromMask(this LayerMask @this, string[] layerNames)
        {
            LayerMask invertedOriginal = ~@this;
            return ~(invertedOriginal | XUnityLayerMask.NamesToMask(layerNames));
        }

        /// <summary>
        /// Удаление из маски слоев указанный слоев
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <param name="layers">Список индексов слоев</param>
        /// <returns>Маска слоев</returns>
        
        public static LayerMask RemoveFromMask(this LayerMask @this, int[] layers)
        {
            var len = layers.Length;
            var names = new string[len];
            for (var i = 0; i < len; i++)
            {
                names[i] = LayerMask.LayerToName(layers[i]);
            }

            return RemoveFromMask(@this, names);
        }

        /// <summary>
        /// Проверка на вхождение указанного слоя
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <param name="other">Маска слоев</param>
        /// <returns>Статус вхождения</returns>
        [Obsolete("Некорректно трактует other.value как индекс слоя (1 << other.value). Используйте (@this.value & other.value) != 0")]
        public static bool Contains(this LayerMask @this, LayerMask other)
        {
            // Convert the object's layer to a bitfield for comparison
            var bitMask = 1 << other.value;
            return (@this.value & bitMask) > 0;
        }

        /// <summary>
        /// Преобразование маски слоев к массиву имен слоев
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <returns>Список имен слоев</returns>
        
        public static string[] MaskToNames(this LayerMask @this)
        {
            var output = new List<string>();

            for (var i = 0; i < 32; ++i)
            {
                var shifted = 1 << i;
                if ((@this & shifted) == shifted)
                {
                    var layerName = LayerMask.LayerToName(i);
                    if (layerName.IsExists())
                    {
                        output.Add(layerName);
                    }
                }
            }
            return output.ToArray();
        }

        /// <summary>
        /// Преобразование маски слоев к текстовому представлению
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <returns>Строка с именами слоев</returns>
        
        public static string MaskToString(this LayerMask @this)
        {
            return MaskToString(@this, ", ");
        }

        /// <summary>
        /// Преобразование маски слоев к текстовому представлению
        /// </summary>
        /// <param name="this">Маска слоев</param>
        /// <param name="delimiter">Разделитель</param>
        /// <returns>Строка с именами слоев</returns>
        
        public static string MaskToString(this LayerMask @this, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(@this));
        }
        #endregion
    }

    /**@}*/
}
#endif
