#if UNITY_2017_1_OR_NEWER
using System;

using UnityEngine;

namespace Lotus.Core
{
    
    /** \addtogroup CoreUnityResource
    *@{*/
    
    /// <summary>
    /// Статический класс реализующий дополнительные методы и константные данные при работе с GUISkin
    /// </summary>
    
    public static class XGUISkin
    {
        #region Constants
        /// <summary>
        /// Имя основного скина GUI
        /// </summary>
        public const string DEFAULT_SKIN_NAME = "DarkKit";

        //
        // ТИПОВЫЕ ИМЕНА СТИЛЕЙ
        //
        //
        // ПРОСТОЙ ТЕКСТ
        //
        /// <summary>
        /// Имя визуального стиля для рисования простого текста
        /// </summary>
        public const string TEXT_STYLE_NAME = "Text";

        /// <summary>
        /// Имя визуального стиля для рисования моноширинного текста
        /// </summary>
        public const string TEXT_MONO_STYLE_NAME = "TextMono";

        /// <summary>
        /// Имя визуального стиля для рисования текста заголовка
        /// </summary>
        public const string TEXT_HEADER_STYLE_NAME = "TextHeader";

        /// <summary>
        /// Имя визуального стиля для рисования текста значения
        /// </summary>
        public const string TEXT_VALUE_STYLE_NAME = "TextValue";

        /// <summary>
        /// Имя визуального стиля для рисования текста столбца
        /// </summary>
        public const string TEXT_COLUMN_STYLE_NAME = "TextColumn";

        /// <summary>
        /// Имя визуального стиля для рисования текста меньшим шрифтом
        /// </summary>
        public const string TEXT_SMALL_STYLE_NAME = "TextSmall";

        //
        // НАДПИСИ
        //
        /// <summary>
        /// Имя визуального стиля для рисования общей надписи
        /// </summary>
        public const string LABEL_STYLE_NAME = "label";

        /// <summary>
        /// Имя визуального стиля для рисования моноширинной надписи
        /// </summary>
        public const string LABEL_MONO_STYLE_NAME = "LabelMono";

        /// <summary>
        /// Имя визуального стиля для рисования надписи заголовка
        /// </summary>
        public const string LABEL_HEADER_STYLE_NAME = "LabelHeader";

        /// <summary>
        /// Имя визуального стиля для рисования надписи значения
        /// </summary>
        public const string LABEL_VALUE_STYLE_NAME = "LabelValue";

        /// <summary>
        /// Имя визуального стиля для рисования надписи столбца
        /// </summary>
        public const string LABEL_COLUMN_STYLE_NAME = "LabelColumn";

        /// <summary>
        /// Имя визуального стиля для рисования надписи меньшим шрифтом
        /// </summary>
        public const string LABEL_SMALL_STYLE_NAME = "LabelSmall";

        //
        // ПАНЕЛИ
        //
        /// <summary>
        /// Имя визуального стиля для рисования общей панели
        /// </summary>
        public const string PANEL_STYLE_NAME = "box";

        /// <summary>
        /// Имя визуального стиля для рисования панели группирования
        /// </summary>
        public const string PANEL_GROUP_STYLE_NAME = "PanelGroup";

        /// <summary>
        /// Имя визуального стиля для рисования панели с заголовком
        /// </summary>
        public const string PANEL_HEADER_STYLE_NAME = "PanelHeader";

        /// <summary>
        /// Имя визуального стиля для рисования панели значения
        /// </summary>
        public const string PANEL_VALUE_STYLE_NAME = "PanelValue";

        /// <summary>
        /// Имя визуального стиля для рисования панели столбца
        /// </summary>
        public const string PANEL_COLUMN_STYLE_NAME = "PanelColumn";

        //
        // КНОПКИ
        //
        /// <summary>
        /// Имя визуального стиля для рисования некоторых управляющих кнопок
        /// </summary>
        public const string BUTTON_MINI_STYLE_NAME = "ButtonMini";
        #endregion

        #region Fields
        internal static GUISkin _defaultSkin;
        internal static GUIStyle _textStyle;
        internal static GUIStyle _textMonoStyle;
        internal static GUIStyle _textHeaderStyle;
        internal static GUIStyle _textValueStyle;
        internal static GUIStyle _textColumnStyle;
        internal static GUIStyle _textSmallStyle;

        internal static GUIStyle _labelStyle;
        internal static GUIStyle _labelMonoStyle;
        internal static GUIStyle _labelHeaderStyle;
        internal static GUIStyle _labelValueStyle;
        internal static GUIStyle _labelColumnStyle;
        internal static GUIStyle _labelSmallStyle;
    
        internal static GUIStyle _panelStyle;
        internal static GUIStyle _panelGroupStyle;
        internal static GUIStyle _panelHeaderStyle;
        internal static GUIStyle _panelValueStyle;
        internal static GUIStyle _panelColumnStyle;

        internal static GUIStyle _buttonMiniStyle;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Проверка на существование визуального скина по умолчанию
        /// </summary>
        public static bool IsDefaultSkinNull
        {
            get
            {
                return (_defaultSkin == null);
            }
        }

        /// <summary>
        /// Текущий визуальный скин для рисования
        /// </summary>
        public static GUISkin DefaultSkin
        {
            get
            {
                if (_defaultSkin == null)
                {
                    LoadDefault();
                }
                return _defaultSkin;
            }
            set
            {
                _defaultSkin = value;
            }
        }

        //
        // ПРОСТОЙ ТЕКСТ
        //
        /// <summary>
        /// Визуальный стиль для рисования простого текста
        /// </summary>
        public static GUIStyle TextStyle
        {
            get
            {
                if (_textStyle == null)
                {
                    LoadDefault();
                }

                return (_textStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования для рисования моноширинного текста
        /// </summary>
        public static GUIStyle TextMonoStyle
        {
            get
            {
                if (_textMonoStyle == null)
                {
                    LoadDefault();
                }

                return (_textMonoStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования текста заголовка
        /// </summary>
        public static GUIStyle TextHeaderStyle
        {
            get
            {
                if (_textHeaderStyle == null)
                {
                    LoadDefault();
                }

                return (_textHeaderStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования текста значения
        /// </summary>
        public static GUIStyle TextValueStyle
        {
            get
            {
                if (_textValueStyle == null)
                {
                    LoadDefault();
                }

                return (_textValueStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования текста столбца
        /// </summary>
        public static GUIStyle TextColumnStyle
        {
            get
            {
                if (_textColumnStyle == null)
                {
                    LoadDefault();
                }

                return (_textColumnStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования текста меньшим шрифтом
        /// </summary>
        public static GUIStyle TextSmallStyle
        {
            get
            {
                if (_textSmallStyle == null)
                {
                    LoadDefault();
                }

                return (_textSmallStyle);
            }
        }

        //
        // НАДПИСИ
        //
        /// <summary>
        /// Визуальный стиль для рисования простой надписи
        /// </summary>
        public static GUIStyle LabelStyle
        {
            get
            {
                if (_labelStyle == null)
                {
                    LoadDefault();
                }

                return (_labelStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования для рисования моноширинной надписи
        /// </summary>
        public static GUIStyle LabelMonoStyle
        {
            get
            {
                if (_labelMonoStyle == null)
                {
                    LoadDefault();
                }

                return (_labelMonoStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования надписи заголовка
        /// </summary>
        public static GUIStyle LabelHeaderStyle
        {
            get
            {
                if (_labelHeaderStyle == null)
                {
                    LoadDefault();
                }

                return (_labelHeaderStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования надписи значения
        /// </summary>
        public static GUIStyle LabelValueStyle
        {
            get
            {
                if (_labelValueStyle == null)
                {
                    LoadDefault();
                }

                return (_labelValueStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования надписи столбца
        /// </summary>
        public static GUIStyle LabelColumnStyle
        {
            get
            {
                if (_labelColumnStyle == null)
                {
                    LoadDefault();
                }

                return (_labelColumnStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования надписи меньшим шрифтом
        /// </summary>
        public static GUIStyle LabelSmallStyle
        {
            get
            {
                if (_labelSmallStyle == null)
                {
                    LoadDefault();
                }

                return (_labelSmallStyle);
            }
        }

        //
        // ПАНЕЛИ
        //
        /// <summary>
        /// Визуальный стиль для рисования общей панели
        /// </summary>
        public static GUIStyle PanelStyle
        {
            get
            {
                if (_panelStyle == null)
                {
                    LoadDefault();
                }

                return (_panelStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования панели группирования
        /// </summary>
        public static GUIStyle PanelGroupStyle
        {
            get
            {
                if (_panelGroupStyle == null)
                {
                    LoadDefault();
                }

                return (_panelGroupStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования панели с заголовком
        /// </summary>
        public static GUIStyle PanelHeaderStyle
        {
            get
            {
                if (_panelHeaderStyle == null)
                {
                    LoadDefault();
                }

                return (_panelHeaderStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования панели значения
        /// </summary>
        public static GUIStyle PanelValueStyle
        {
            get
            {
                if (_panelValueStyle == null)
                {
                    LoadDefault();
                }

                return (_panelValueStyle);
            }
        }

        /// <summary>
        /// Визуальный стиль для рисования панели столбца
        /// </summary>
        public static GUIStyle PanelColumnStyle
        {
            get
            {
                if (_panelColumnStyle == null)
                {
                    LoadDefault();
                }

                return (_panelColumnStyle);
            }
        }

        //
        // КНОПКИ
        //
        /// <summary>
        /// Визуальный стиль для рисования некоторых управляющих кнопок
        /// </summary>
        public static GUIStyle ButtonMiniStyle
        {
            get
            {
                if (_buttonMiniStyle == null)
                {
                    LoadDefault();
                }

                return _buttonMiniStyle;
            }
        }
        #endregion

        #region Main methods
        
        /// <summary>
        /// Загрузка визуального скина по умолчанию
        /// </summary>
        
        internal static void LoadDefault()
        {
            if (_defaultSkin == null)
            {
                GUISkin[] skins = Resources.FindObjectsOfTypeAll<GUISkin>();

                for (var i = 0; i < skins.Length; i++)
                {
                    if (DEFAULT_SKIN_NAME.Equal(skins[i].name))
                    {
                        _defaultSkin = skins[i];
                        break;
                    }
                }

                if (_defaultSkin == null && skins.Length > 0)
                {
                    _defaultSkin = skins[0];
                }
            }

            if (_defaultSkin != null)
            {
                LoadDefaultStyles(_defaultSkin);
            }
        }

        
        /// <summary>
        /// Загрузка стандартных визуальных стилей 
        /// </summary>
        /// <param name="skin">Визуальный скин</param>
        
        private static void LoadDefaultStyles(GUISkin skin)
        {
            //
            // ПРОСТОЙ ТЕКСТ
            //
            if (_textStyle == null) _textStyle = skin.FindStyle(TEXT_STYLE_NAME);
            if (_textStyle == null)
            {
                _textStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_STYLE_NAME, "label");
            }

            if (_textMonoStyle == null) _textMonoStyle = skin.FindStyle(TEXT_MONO_STYLE_NAME);
            if (_textMonoStyle == null)
            {
                _textMonoStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_MONO_STYLE_NAME, "label");
            }

            if (_textHeaderStyle == null) _textHeaderStyle = skin.FindStyle(TEXT_HEADER_STYLE_NAME);
            if (_textHeaderStyle == null)
            {
                _textHeaderStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_HEADER_STYLE_NAME, "label");
            }

            if (_textValueStyle == null) _textValueStyle = skin.FindStyle(TEXT_VALUE_STYLE_NAME);
            if (_textValueStyle == null)
            {
                _textValueStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_VALUE_STYLE_NAME, "label");
            }

            if (_textColumnStyle == null) _textColumnStyle = skin.FindStyle(TEXT_COLUMN_STYLE_NAME);
            if (_textColumnStyle == null)
            {
                _textColumnStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_COLUMN_STYLE_NAME, "label");
            }

            if (_textSmallStyle == null) _textSmallStyle = skin.FindStyle(TEXT_SMALL_STYLE_NAME);
            if (_textSmallStyle == null)
            {
                _textSmallStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", TEXT_SMALL_STYLE_NAME, "label");
            }

            //
            // НАДПИСИ
            //
            if (_labelStyle == null) _labelStyle = skin.label;

            if (_labelMonoStyle == null) _labelMonoStyle = skin.FindStyle(LABEL_MONO_STYLE_NAME);
            if (_labelMonoStyle == null)
            {
                _labelMonoStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", LABEL_MONO_STYLE_NAME, "label");
            }

            if (_labelHeaderStyle == null) _labelHeaderStyle = skin.FindStyle(LABEL_HEADER_STYLE_NAME);
            if (_labelHeaderStyle == null)
            {
                _labelHeaderStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", LABEL_HEADER_STYLE_NAME, "label");
            }

            if (_labelValueStyle == null) _labelValueStyle = skin.FindStyle(LABEL_VALUE_STYLE_NAME);
            if (_labelValueStyle == null)
            {
                _labelValueStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", LABEL_VALUE_STYLE_NAME, "label");
            }

            if (_labelColumnStyle == null) _labelColumnStyle = skin.FindStyle(LABEL_COLUMN_STYLE_NAME);
            if (_labelColumnStyle == null)
            {
                _labelColumnStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", LABEL_COLUMN_STYLE_NAME, "label");
            }

            if (_labelSmallStyle == null) _labelSmallStyle = skin.FindStyle(LABEL_SMALL_STYLE_NAME);
            if (_labelSmallStyle == null)
            {
                _labelSmallStyle = skin.label;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", LABEL_SMALL_STYLE_NAME, "label");
            }

            //
            // ПАНЕЛИ
            //
            if (_panelStyle == null) _panelStyle = skin.box;

            if (_panelGroupStyle == null) _panelGroupStyle = skin.FindStyle(PANEL_GROUP_STYLE_NAME);
            if (_panelGroupStyle == null)
            {
                _panelGroupStyle = skin.box;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", PANEL_GROUP_STYLE_NAME, "box");
            }

            if (_panelHeaderStyle == null) _panelHeaderStyle = skin.FindStyle(PANEL_HEADER_STYLE_NAME);
            if (_panelHeaderStyle == null)
            {
                _panelHeaderStyle = skin.box;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", PANEL_HEADER_STYLE_NAME, "box");
            }

            if (_panelValueStyle == null) _panelValueStyle = skin.FindStyle(PANEL_VALUE_STYLE_NAME);
            if (_panelValueStyle == null)
            {
                _panelValueStyle = skin.box;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", PANEL_VALUE_STYLE_NAME, "box");
            }

            if (_panelColumnStyle == null) _panelColumnStyle = skin.FindStyle(PANEL_COLUMN_STYLE_NAME);
            if (_panelColumnStyle == null)
            {
                _panelColumnStyle = skin.box;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", PANEL_COLUMN_STYLE_NAME, "box");
            }

            if (_buttonMiniStyle == null) _buttonMiniStyle = skin.FindStyle(BUTTON_MINI_STYLE_NAME);
            if (_buttonMiniStyle == null)
            {
                _buttonMiniStyle = new GUIStyle(skin.button);
                _buttonMiniStyle.border.left = 1;
                _buttonMiniStyle.border.right = 1;
                _buttonMiniStyle.border.top = 1;
                _buttonMiniStyle.border.bottom = 1;
                _buttonMiniStyle.fontSize = 9;
                Debug.LogWarningFormat("Visual style <{0}> not found, default style loaded <{1}>", BUTTON_MINI_STYLE_NAME, "button");
            }

        }
        #endregion
    }
    
    /**@}*/
    
}
#endif
