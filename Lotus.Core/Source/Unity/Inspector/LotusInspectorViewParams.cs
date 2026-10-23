#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityInspector Подсистема поддержки инспектора свойств Unity
     * \ingroup Core
     * \brief Подсистема поддержки инспектора свойств Unity.
     * @{
     */
    /// <summary>
    /// Статический класс определяющий размеры и разметку для отображении элементов редактирования в инспекторе свойств.
    /// </summary>
    public static class XInspectorViewParams
    {
        #region Constants
        /// <summary>
        /// Стандартный отступ между элементами инспектора свойств
        /// </summary>
        public const float SPACE = 2; // UnityEditor.EditorGUIUtility.standardVerticalSpacing

        /// <summary>
        /// Стандартная высота одного элемента инспектора свойств
        /// </summary>
        public const float CONTROL_HEIGHT = 18; // UnityEditor.EditorGUIUtility.singleLineHeight

        /// <summary>
        /// Высота одного элемента инспектора свойств с отступом
        /// </summary>
        public const float CONTROL_HEIGHT_SPACE = CONTROL_HEIGHT + SPACE;

        /// <summary>
        /// Высота заголовка
        /// </summary>
        public const float HEADER_HEIGHT = CONTROL_HEIGHT + (SPACE * 2);

        /// <summary>
        /// Ширина одной мини кнопки
        /// </summary>
        public const float BUTTON_MINI_WIDTH = 20;

        /// <summary>
        /// Высота одной мини кнопки
        /// </summary>
        public const float BUTTON_MINI_HEIGHT = CONTROL_HEIGHT;

        /// <summary>
        /// Ширина иконки-кнопки для предпросмотра
        /// </summary>
        public const float BUTTON_PREVIEW_WIDTH = 16;

        /// <summary>
        /// Ширина полосы прокрутки инспектора свойств
        /// </summary>
        public const float SCROLL_WIDTH = 16;

        /// <summary>
        /// Отступ в пикселях на 1 уровень вложенности(глубины) элемента
        /// </summary>
        public const int OFFSET_INDENT = 12;
        #endregion
    }

    /**@}*/
}
#endif
