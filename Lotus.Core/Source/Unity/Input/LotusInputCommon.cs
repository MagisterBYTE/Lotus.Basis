#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityInput Подсистема ввода Unity
     * \ingroup Core
     * \brief Подсистема ввода поддерживает абстрактный ввод не зависимо от источника данных и платформы
        (пользовательский ввод от мыши и клавиатуры или от интерфейса пользователя для мобильных платформ).
        Также подсистема ввод поддерживает событийное информировании о событий ввода через интерфейсы оповещения.
     * \details
        //## Описание
        Подсистема позволяет получать, например, состояние некой кнопки независимо что представляет собой эту
        кнопку - клавиатура, мышь или элемент интерфейса. Также подсистема информирует о событиях ввода посредством
        вызова соответствующего подписчика.

        //## Использование
        1. Воспользуйтесь элементами GUI (UI) и зарегистрирует их с помощью методов \ref Lotus.Core.XInputDispatcher
        как соответствующие кнопки (или джойстики).
        Также можно зарегистрировать джойстик как основные оси - тогда они будут эмулировать ввод мыши и клавиатуры (W, S, A, D)
        2. Реализовать соответствующие интерфейсы \ref Lotus.Core.ILotusPointerDown.
        3. В событии Start/OnEnable скрипта подписаться на обработку событий по каждому реализованному интерфейсу.
        4. В событии OnDestroy/OnDisable скрипта отписаться от обработки событий по каждому реализованному интерфейсу.
        5. Диспетчер пользовательского ввода можно использовать в ручную(непосредственно вызывать его методы в нужных местах) или посредством \ref Lotus.Core.LotusSystemDispatcher
     * @{
     */
    /// <summary>
    /// Перечисление кнопок мыши для управления
    /// </summary>
    public enum TMouseButtonInput
    {
        /// <summary>
        /// Нет данных
        /// </summary>
        None = -1,

        /// <summary>
        /// Левая кнопка мыши
        /// </summary>
        Left = 0,

        /// <summary>
        /// Правая кнопка мыши
        /// </summary>
        Right = 1,

        /// <summary>
        /// Средняя кнопка мыши
        /// </summary>
        Middle = 2
    }


    /// <summary>
    /// Интерфейс для эмуляции клавиши или кнопки
    /// </summary>
    /// <remarks>
    /// Указанный интерфейс должен реализовать элемент пользовательского интерфейса для того что иметь возможность
    /// эмулировать ввод данных с клавиатуры или кнопок мыши
    /// </remarks>
    public interface ILotusVirtualButton
    {
        #region Properties
        /// <summary>
        /// Имя виртуальной кнопки
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Статус удержания нажатой кнопки
        /// </summary>
        bool IsButtonPressed { get; }

        /// <summary>
        /// Статус нажатия кнопки
        /// </summary>
        bool IsButtonDown { get; }

        /// <summary>
        /// Статус отпускания кнопки
        /// </summary>
        bool IsButtonUp { get; }
        #endregion
    }


    /// <summary>
    /// Интерфейс для эмуляции смещения/перемещения по одномерной оси
    /// </summary>
    /// <remarks>
    /// Указанный интерфейс должен реализовать элемент пользовательского интерфейса для того что иметь возможность
    /// эмулировать ввод данных с одномерных линейных перемещений(скролл мыши)
    /// </remarks>
    public interface ILotusVirtualAxis
    {
        #region Properties
        /// <summary>
        /// Имя виртуальной оси
        /// </summary>
        string Name { get; }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение смещения/перемещения по оси
        /// </summary>
        /// <remarks>
        /// Перемещение/смещение всегда указывается в относительных единицах в диапазоне от [-1; 1]
        /// </remarks>
        /// <returns>Смещение/перемещение по оси</returns>
        float GetAxis();
        #endregion
    }


    /// <summary>
    /// Интерфейс для эмуляции смещения/перемещения по двумерной оси
    /// </summary>
    /// <remarks>
    /// Указанный интерфейс должен реализовать элемент пользовательского интерфейса для того что иметь возможность
    /// эмулировать ввод данных с двухмерных линейных перемещений (курсор мыши, гироскоп)
    /// </remarks>
    public interface ILotusVirtualJoystick
    {
        #region Properties
        /// <summary>
        /// Имя виртуального джойстика
        /// </summary>
        string Name { get; }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение смещения/перемещения по осям
        /// </summary>
        /// <remarks>
        /// Перемещение/смещение всегда указывается в относительных единицах в диапазоне от [-1; 1]
        /// </remarks>
        /// <returns>Смещение/перемещение по осям</returns>
        Vector2 GetAxis();

        /// <summary>
        /// Получение смещения/перемещения по горизонтальной оси
        /// </summary>
        /// <returns>Смещение/перемещение по горизонтальной оси</returns>
        float GetAxisHorizontal();

        /// <summary>
        /// Получение смещения/перемещения по вертикальной оси
        /// </summary>
        /// <returns>Смещение/перемещение по вертикальной оси</returns>
        float GetAxisVertical();
        #endregion
    }


    /// <summary>
    /// Интерфейс для оповещения о нажатии указателя
    /// </summary>
    public interface ILotusPointerDown
    {
        /// <summary>
        /// Событие нажатия
        /// </summary>
        /// <param name="pointerId">Идентификатор указателя</param>
        void PointerDown(int pointerId);
    }


    /// <summary>
    /// Интерфейс для оповещения о перемещении указателя
    /// </summary>
    public interface ILotusPointerMove
    {
        /// <summary>
        /// Событие перемещения
        /// </summary>
        /// <param name="pointerId">Идентификатор указателя</param>
        void PointerMove(int pointerId);
    }


    /// <summary>
    /// Интерфейс для оповещения удерживания указателя
    /// </summary>
    public interface ILotusPointerPress
    {
        /// <summary>
        /// Событие удерживания
        /// </summary>
        /// <param name="pointerId">Идентификатор указателя</param>
        void PointerPress(int pointerId);
    }


    /// <summary>
    /// Интерфейс для оповещения отпускания указателя
    /// </summary>
    public interface ILotusPointerUp
    {
        /// <summary>
        /// Событие отпускания
        /// </summary>
        /// <param name="pointerId">Идентификатор указателя</param>
        void PointerUp(int pointerId);
    }

    /**@}*/
}
#endif
