#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    /** \addtogroup CoreUnityInspector
    *@{*/
    /// <summary>
    /// Статический класс реализующий визуальные стили для оформления элементов инспектора свойств
    /// </summary>
    public static class XEditorStyles
    {
        #region Icon names
        //
        // РЕСУРСЫ
        //
        /// <summary>
        /// Имя иконки директории
        /// </summary>
        public const string ICON_FOLDER = "Folder Icon";

        /// <summary>
        /// Имя иконки
        /// </summary>
        public const string ICON_DEFAULT_ASSET = "DefaultAsset Icon";

        /// <summary>
        /// Имя иконки пользовательского ресурса
        /// </summary>
        public const string ICON_SCRIPTABLE_OBJECT = "ScriptableObject Icon";

        /// <summary>
        /// Имя иконки материала
        /// </summary>
        public const string ICON_MATERIAL = "Material Icon";

        /// <summary>
        /// Имя иконки текстуры
        /// </summary>
        public const string ICON_TEXTURE_2D = "Texture2D Icon";

        /// <summary>
        /// Имя иконки шрифта
        /// </summary>
        public const string ICON_FONT = "Font Icon";

        /// <summary>
        /// Имя иконки звукового ресурса
        /// </summary>
        public const string ICON_AUDIO_SOURCE = "AudioSource Icon";

        /// <summary>
        /// Имя иконки анимационного клипа
        /// </summary>
        public const string ICON_ANIMATION_CLIP = "AnimationClip Icon";

        /// <summary>
        /// Имя иконки префаба
        /// </summary>
        public const string ICON_PREFAB = "Prefab Icon";

        /// <summary>
        /// Имя иконки префаба модели
        /// </summary>
        public const string ICON_PREFAB_MODEL = "PrefabModel Icon";

        /// <summary>
        /// Имя иконки меша
        /// </summary>
        public const string ICON_MESH = "Mesh Icon";

        /// <summary>
        /// Имя иконки шейдера
        /// </summary>
        public const string ICON_SHADER = "Shader Icon";

        /// <summary>
        /// Имя иконки скина GUI
        /// </summary>
        public const string ICON_GUISKIN = "GUISkin Icon";

        /// <summary>
        /// Имя иконки текстового файла
        /// </summary>
        public const string ICON_TEXT = "TextAsset Icon";

        /// <summary>
        /// Имя иконки XML/UXML файла
        /// </summary>
        public const string ICON_XML = "UxmlScript Icon";

        /// <summary>
        /// Имя иконки cs файла
        /// </summary>
        public const string ICON_SCRIPT = "cs Script Icon";

        /// <summary>
        /// Имя иконки мета файла
        /// </summary>
        public const string ICON_META = "MetaFile Icon";

        /// <summary>
        /// Имя иконки сцены
        /// </summary>
        public const string ICON_SCENE = "SceneAsset Icon";

        /// <summary>
        /// Имя иконки видеофайла
        /// </summary>
        public const string ICON_VIDEO_CLIP = "VideoClip Icon";

        /// <summary>
        /// Имя иконки физического материала (тип PhysicsMaterial в Unity 6; built-in icon — PhysicMaterial Icon)
        /// </summary>
        public const string ICON_PHYSICS_MATERIAL = "PhysicMaterial Icon";

        /// <summary>
        /// Имя иконки физического материала
        /// </summary>
        [Obsolete("Устаревшее имя константы. Используйте ICON_PHYSICS_MATERIAL")]
        public const string ICON_PHYSIC_MATERIAL = ICON_PHYSICS_MATERIAL;

        /// <summary>
        /// Имя иконки настроек
        /// </summary>
        public const string ICON_GAME_MANAGER = "GameManager Icon";

        /// <summary>
        /// Имя иконки сигнала
        /// </summary>
        public const string ICON_SIGNAL = "animationdopesheetkeyframe";
        #endregion

        #region Button style names
        /// <summary>
        /// Имя стиля обычной мини кнопки
        /// </summary>
        public const string MINI_BUTTON = "miniButton";

        /// <summary>
        /// Имя стиля мини кнопки расположенной слева
        /// </summary>
        public const string MINI_BUTTON_L = "miniButtonLeft";

        /// <summary>
        /// Имя стиля мини кнопки расположенной посередине
        /// </summary>
        public const string MINI_BUTTON_M = "miniButtonMid";

        /// <summary>
        /// Имя стиля мини кнопки расположенной справа
        /// </summary>
        public const string MINI_BUTTON_R = "miniButtonRight";

        /// <summary>
        /// Имя стиля обычной кнопки DropDown
        /// </summary>
        public const string BUTTON_DROP_DOWN = "DropDown";

        /// <summary>
        /// Имя стиля обычной кнопки DropDownButton
        /// </summary>
        public const string BUTTON_DROP_DOWN_1 = "DropDownButton";
        #endregion

        #region Box style names
        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_SHURIKEN = "ShurikenEffectBg";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FLOW_OVERLAY = "flow overlay box";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FLOW_NODE_0 = "flow node 0";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FLOW_NODE_0_ON = "flow node 0 on";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FLOW_NODE_1 = "flow node 1";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FLOW_NODE_1_ON = "flow node 1 on";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_BLOCK = "CN Box";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_CURVE = "CurveEditorBackground";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_FRAME = "Dopesheetkeyframe";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_SHADOW = "IN ThumbnailShadow";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_OBJECT = "ObjectFieldThumb";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_SELECTION = "SelectionRect";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_NODE = "TE NodeBox";

        /// <summary>
        /// Имя стиля бокса
        /// </summary>
        public const string BOX_NODE_S = "TE NodeBoxSelected";
        #endregion

        #region Search style names
        /// <summary>
        /// Имя стиля текстового поля для строки поиска (Unity 6 / EditorStyles.toolbarSearchField)
        /// </summary>
        public const string SEARCH_FIELD = "ToolbarSearchTextField";

        /// <summary>
        /// Имя стиля кнопки выпадающего меню для строки поиска
        /// </summary>
        public const string SEARCH_FIELD_POPUP = "ToolbarSearchTextFieldPopup";

        /// <summary>
        /// Имя стиля кнопки очистки для строки поиска
        /// </summary>
        public const string SEARCH_FIELD_CANCEL = "ToolbarSearchCancelButton";
        #endregion

        #region Fields
        // Основные параметры
        internal static GUIStyle _dirtyStyle;

        // Стили кнопок
        internal static GUIStyle _buttonMiniDefaultGreenRightStyle;
        internal static GUIStyle _buttonMiniDefaultRedRightStyle;
        internal static GUIStyle _buttonMiniHeaderRedRightStyle;
        internal static GUIStyle _buttonMiniHeaderStyle;
        internal static GUIStyle _buttonMiniHeaderMiddleStyle;
        internal static GUIStyle _buttonPreviewStyle;

        // Стили переключателей
        internal static GUIStyle _toggleLeftStyle;

        // Стили текстового поля
        internal static GUIStyle _textHeaderHeightStyle;

        // Стили сворачиваемой панели
        internal static GUIStyle _foldoutBoldStyle;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Рабочий стиль
        /// </summary>
        public static GUIStyle DirtyStyle
        {
            get
            {
                if (_dirtyStyle == null)
                {
                    _dirtyStyle = new GUIStyle(GUIStyle.none);
                }

                return _dirtyStyle;
            }
        }

        //
        // СТИЛИ КНОПОК
        //
        /// <summary>
        /// Стиль для стандартной малой кнопки расположенной справа
        /// </summary>
        public static GUIStyle ButtonMiniDefaultGreenRightStyle
        {
            get
            {
                if (_buttonMiniDefaultGreenRightStyle == null)
                {
#if UNITY_EDITOR
                    _buttonMiniDefaultGreenRightStyle = new GUIStyle(UnityEditor.EditorStyles.miniButtonRight);
                    _buttonMiniDefaultGreenRightStyle.normal.textColor = ColorGreen;

                    if (UnityEditor.EditorGUIUtility.isProSkin == false)
                    {
                        _buttonMiniDefaultGreenRightStyle.fixedHeight = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    }
#endif
                }
                return _buttonMiniDefaultGreenRightStyle;
            }
        }

        /// <summary>
        /// Стиль для стандартной малой кнопки расположенной справа
        /// </summary>
        public static GUIStyle ButtonMiniDefaultRedRightStyle
        {
            get
            {
                if (_buttonMiniDefaultRedRightStyle == null)
                {
#if UNITY_EDITOR
                    _buttonMiniDefaultRedRightStyle = new GUIStyle(UnityEditor.EditorStyles.miniButtonRight);
                    _buttonMiniDefaultRedRightStyle.normal.textColor = ColorRed;

                    if (UnityEditor.EditorGUIUtility.isProSkin == false)
                    {
                        _buttonMiniDefaultRedRightStyle.fixedHeight = XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    }
#endif
                }
                return _buttonMiniDefaultRedRightStyle;
            }
        }

        /// <summary>
        /// Стиль для стандартной малой(высотой в заголовок) кнопки расположенной справа
        /// </summary>
        public static GUIStyle ButtonMiniHeaderRedRightStyle
        {
            get
            {
                if (_buttonMiniHeaderRedRightStyle == null)
                {
#if UNITY_EDITOR
                    _buttonMiniHeaderRedRightStyle = new GUIStyle(UnityEditor.EditorStyles.miniButtonRight);
                    _buttonMiniHeaderRedRightStyle.alignment = TextAnchor.MiddleCenter;
                    _buttonMiniHeaderRedRightStyle.fixedHeight = XInspectorViewParams.HEADER_HEIGHT;
                    _buttonMiniHeaderRedRightStyle.normal.textColor = ColorRed;
                    _buttonMiniHeaderRedRightStyle.border.top = 3;
                    _buttonMiniHeaderRedRightStyle.border.left = 3;
                    _buttonMiniHeaderRedRightStyle.border.right = 3;
                    _buttonMiniHeaderRedRightStyle.border.bottom = 3;
#endif
                }
                return _buttonMiniHeaderRedRightStyle;
            }
        }

        /// <summary>
        /// Стиль для стандартной малой(высотой в заголовок) кнопки
        /// </summary>
        public static GUIStyle ButtonMiniHeaderStyle
        {
            get
            {
                if (_buttonMiniHeaderStyle == null)
                {
#if UNITY_EDITOR
                    _buttonMiniHeaderStyle = new GUIStyle(UnityEditor.EditorStyles.miniButton);
                    _buttonMiniHeaderStyle.alignment = TextAnchor.MiddleCenter;
                    _buttonMiniHeaderStyle.fixedHeight = XInspectorViewParams.HEADER_HEIGHT;
                    _buttonMiniHeaderStyle.border = new RectOffset(3, 3, 3, 3);
#endif
                }
                return _buttonMiniHeaderStyle;
            }
        }

        /// <summary>
        /// Стиль для стандартной малой(высотой в заголовок) кнопки расположенной посередине
        /// </summary>
        public static GUIStyle ButtonMiniHeaderMiddleStyle
        {
            get
            {
                if (_buttonMiniHeaderMiddleStyle == null)
                {
#if UNITY_EDITOR
                    _buttonMiniHeaderMiddleStyle = new GUIStyle(UnityEditor.EditorStyles.miniButtonMid);
                    _buttonMiniHeaderMiddleStyle.alignment = TextAnchor.MiddleCenter;
                    _buttonMiniHeaderMiddleStyle.fixedHeight = XInspectorViewParams.HEADER_HEIGHT;
                    _buttonMiniHeaderMiddleStyle.border = new RectOffset(3, 3, 3, 3);
#endif
                }
                return _buttonMiniHeaderMiddleStyle;
            }
        }

        /// <summary>
        /// Стиль для кнопки предпросмотра
        /// </summary>
        public static GUIStyle ButtonPreviewStyle
        {
            get
            {
                if (_buttonPreviewStyle == null)
                {
#if UNITY_EDITOR
                    _buttonPreviewStyle = new GUIStyle(UnityEditor.EditorStyles.largeLabel);
                    _buttonPreviewStyle.alignment = TextAnchor.MiddleCenter;
                    _buttonPreviewStyle.fontSize = 10;
#endif
                }
                return _buttonPreviewStyle;
            }
        }

        //
        // СТИЛИ ПЕРЕКЛЮЧАТЕЛЕЙ
        //
        /// <summary>
        /// Стиль для переключателя расположенного слева
        /// </summary>
        public static GUIStyle ToggleLeftStyle
        {
            get
            {
                if (_toggleLeftStyle == null)
                {
#if UNITY_EDITOR
                    _toggleLeftStyle = new GUIStyle(UnityEditor.EditorStyles.toggle);
                    _toggleLeftStyle.alignment = TextAnchor.MiddleCenter;
                    _toggleLeftStyle.fixedWidth = 0;
#endif
                }
                return _toggleLeftStyle;
            }
        }

        /// <summary>
        /// Стиль для переключателя расположенного слева
        /// </summary>
        [Obsolete("Опечатка в имени. Используйте ToggleLeftStyle")]
        public static GUIStyle ToogleLeftStyle
        {
            get { return ToggleLeftStyle; }
        }

        //
        // СТИЛИ ТЕКСТОВОГО ПОЛЯ
        //
        /// <summary>
        /// Стиль для текстового поля высотой в заголовок
        /// </summary>
        public static GUIStyle TextHeaderHeightStyle
        {
            get
            {
                if (_textHeaderHeightStyle == null)
                {
#if UNITY_EDITOR
                    _textHeaderHeightStyle = new GUIStyle(UnityEditor.EditorStyles.textField);
                    _textHeaderHeightStyle.alignment = TextAnchor.MiddleLeft;
                    _textHeaderHeightStyle.fixedHeight = XInspectorViewParams.HEADER_HEIGHT;
#endif
                }
                return _textHeaderHeightStyle;
            }
        }

        //
        // СТИЛИ СВОРАЧИВАЕМОЙ ПАНЕЛИ
        //
        /// <summary>
        /// Жирный текст
        /// </summary>
        public static GUIStyle FoldoutBoldStyle
        {
            get
            {
                if (_foldoutBoldStyle == null)
                {
#if UNITY_EDITOR
                    _foldoutBoldStyle = new GUIStyle(UnityEditor.EditorStyles.foldout);
                    _foldoutBoldStyle.fontStyle = FontStyle.Bold;
#endif
                }
                return _foldoutBoldStyle;
            }
        }

        //
        // ЦВЕТА ИНФОРМАЦИОННЫХ ПАНЕЛЕЙ
        //
        /// <summary>
        /// Цвет заголовка секции
        /// </summary>
#if UNITY_EDITOR
        public static readonly Color ColorHeaderSection = UnityEditor.EditorGUIUtility.isProSkin
            ? new Color(0.2f, 0.7f, 0.2f)
            : new Color(0.1f, 0.4f, 0.1f);
#else
        public static readonly Color ColorHeaderSection = new Color(0.2f, 0.7f, 0.2f);
#endif

        /// <summary>
        /// Цвет заголовка группы
        /// </summary>
#if UNITY_EDITOR
        public static readonly Color ColorHeaderGroup = UnityEditor.EditorGUIUtility.isProSkin
            ? new Color(0.65f, 0.85f, 0.1f)
            : new Color(0.1f, 0.1f, 0.6f);
#else
        public static readonly Color ColorHeaderGroup = new Color(0.65f, 0.85f, 0.1f);
#endif

        /// <summary>
        /// Зеленый цвет
        /// </summary>
#if UNITY_EDITOR
        public static readonly Color ColorGreen = UnityEditor.EditorGUIUtility.isProSkin
            ? new Color(0f, 0.85f, 0f)
            : new Color(0.2f, 0.8f, 0.2f);
#else
        public static readonly Color ColorGreen = Color.green;
#endif

        /// <summary>
        /// Желтый цвет
        /// </summary>
#if UNITY_EDITOR
        public static readonly Color ColorYellow = UnityEditor.EditorGUIUtility.isProSkin
            ? new Color(0.8f, 0.8f, 0f)
            : new Color(0.5f, 0.5f, 0.0f);
#else
        public static readonly Color ColorYellow = Color.yellow;
#endif

        /// <summary>
        /// Красный цвет
        /// </summary>
#if UNITY_EDITOR
        public static readonly Color ColorRed = UnityEditor.EditorGUIUtility.isProSkin
            ? new Color(0.85f, 0.0f, 0f)
            : new Color(0.8f, 0.2f, 0.2f);
#else
        public static readonly Color ColorRed = Color.red;
#endif
        #endregion

        #region Main methods
        /// <summary>
        /// Инициализация визуальных стилей
        /// </summary>
        public static void InitEditorStyle()
        {
            _dirtyStyle = new GUIStyle(GUIStyle.none);
        }

        /// <summary>
        /// Поиск стиля по имени
        /// </summary>
        /// <param name="styleName">Имя стиля</param>
        /// <returns>Найденный стиль или null</returns>
        public static GUIStyle FindStyle(string styleName)
        {
            GUIStyle style = null;
#if UNITY_EDITOR
            if (styleName.IsExists())
            {
                switch (styleName)
                {
                    case nameof(UnityEditor.EditorStyles.miniButton):
                        return UnityEditor.EditorStyles.miniButton;
                    case nameof(UnityEditor.EditorStyles.miniButtonLeft):
                        return UnityEditor.EditorStyles.miniButtonLeft;
                    case nameof(UnityEditor.EditorStyles.miniButtonMid):
                        return UnityEditor.EditorStyles.miniButtonMid;
                    case nameof(UnityEditor.EditorStyles.miniButtonRight):
                        return UnityEditor.EditorStyles.miniButtonRight;
                    default:
                        break;
                }

                GUISkin skin = UnityEditor.EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Scene);
                if (skin != null)
                {
                    style = skin.FindStyle(styleName);
                }

                if (style == null)
                {
                    skin = UnityEditor.EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Inspector);
                    if (skin != null)
                    {
                        style = skin.FindStyle(styleName);
                    }
                }
            }
#endif
            return style;
        }
        #endregion
    }

    /**@}*/
}
#endif
