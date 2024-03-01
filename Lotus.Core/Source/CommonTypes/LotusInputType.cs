using System;

namespace Lotus.Core
{
    /** \addtogroup CoreCommonTypes
	*@{*/
    /// <summary>
    /// Тип кнопки у мыши.
    /// </summary>
    public enum TMouseButton
    {
        /// <summary>
        /// Левая кнопка мыши.
        /// </summary>
        Left = 1,

        /// <summary>
        /// Правая кнопка мыши.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Средняя кнопка мыши.
        /// </summary>
        Middle = 4
    }

    /// <summary>
    /// Выбор типа кнопки у мыши.
    /// </summary>
    public enum TMouseButtonSelect
    {
        /// <summary>
        /// Выбор отсутствует.
        /// </summary>
        None = 0,

        /// <summary>
        /// Левая кнопка мыши.
        /// </summary>
        Left = 1,

        /// <summary>
        /// Правая кнопка мыши.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Средняя кнопка мыши.
        /// </summary>
        Middle = 4
    }

    /// <summary>
    /// Набор кнопок у мыши.
    /// </summary>
    [Flags]
    public enum TMouseButtonSet
    {
        /// <summary>
        /// Выбор отсутствует.
        /// </summary>
        None = 0,

        /// <summary>
        /// Левая кнопка мыши.
        /// </summary>
        Left = 1,

        /// <summary>
        /// Правая кнопка мыши.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Средняя кнопка мыши.
        /// </summary>
        Middle = 4
    }

    /// <summary>
    /// Курсор.
    /// </summary>
    public enum TCursor
    {
        /// <summary>
        /// Обычная стрелка.
        /// </summary>
        Arrow,

        /// <summary>
        /// Крестик.
        /// </summary>
        Cross,

        /// <summary>
        /// Рука.
        /// </summary>
        Hand,

        /// <summary>
        /// Помощь.
        /// </summary>
        Help,

        /// <summary>
        /// Курсор который указывает на недоступность определенной области для данной операции.
        /// </summary>
        No,

        /// <summary>
        /// Специальный невидимый курсор.
        /// </summary>
        None,

        /// <summary>
        /// Курсор в виде ручки.
        /// </summary>
        Pen,

        /// <summary>
        /// Курсор изменения размера, состоящий из четырех соединенных стрелок, указывающих вверх, вниз, влево и вправо.
        /// </summary>
        SizeAll,

        /// <summary>
        /// Курсор двунаправленный (северо-восток — юго-запад) изменения размера.
        /// </summary>
        SizeNESW,

        /// <summary>
        /// Курсор двунаправленный (вверх-вниз) изменения размера.
        /// </summary>
        SizeNS,

        /// <summary>
        /// Курсор двунаправленный (северо-запад — юго-восток) изменения размера.
        /// </summary>
        SizeNWSE,

        /// <summary>
        /// Курсор двунаправленный (влево-вправо) изменения размера.
        /// </summary>
        SizeWE
    }

    /// <summary>
    /// Клавиша клавиатуры.
    /// </summary>
    public enum TKey
    {
        /// <summary>
        /// Клавиша M.
        /// </summary>
        M,

        /// <summary>
        /// Клавиша Z.
        /// </summary>
        Z,

        /// <summary>
        /// Функциональная клавиша F1.
        /// </summary>
        F1,

        /// <summary>
        /// Функциональная клавиша F2.
        /// </summary>
        F2,

        /// <summary>
        /// Функциональная клавиша F3.
        /// </summary>
        F3,

        /// <summary>
        /// Функциональная клавиша F4.
        /// </summary>
        F4,

        /// <summary>
        /// Функциональная клавиша F5.
        /// </summary>
        F5,

        /// <summary>
        /// Функциональная клавиша F6.
        /// </summary>
        F6,

        /// <summary>
        /// Функциональная клавиша F7.
        /// </summary>
        F7,

        /// <summary>
        /// Функциональная клавиша F8.
        /// </summary>
        F8,

        /// <summary>
        /// Функциональная клавиша F9.
        /// </summary>
        F9,

        /// <summary>
        /// Функциональная клавиша F10.
        /// </summary>
        F10,

        /// <summary>
        /// Клавиша Escape.
        /// </summary>
        Escape,

        /// <summary>
        /// Клавиша ввод.
        /// </summary>
        Enter,

        /// <summary>
        /// Пробел.
        /// </summary>
        Space,

        /// <summary>
        /// Клавиша Delete.
        /// </summary>
        Delete,

        /// <summary>
        /// Левый контрол.
        /// </summary>
        LeftControl,

        /// <summary>
        /// Правый контрол.
        /// </summary>
        RightControl,

        /// <summary>
        /// Левый Shift.
        /// </summary>
        LeftShift,

        /// <summary>
        /// Правый Shift.
        /// </summary>
        RightShift,

        /// <summary>
        /// Стрелка влево.
        /// </summary>
        LeftArrow,

        /// <summary>
        /// Стрелка вправо.
        /// </summary>
        RightArrow,

        /// <summary>
        /// Стрелка вверх.
        /// </summary>
        UpArrow,

        /// <summary>
        /// Стрелка вниз.
        /// </summary>
        DownArrow
    }
    /**@}*/
}