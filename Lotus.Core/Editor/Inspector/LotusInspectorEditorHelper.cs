#if UNITY_EDITOR
using System;
using System.Reflection;

using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Статический класс для определения дополнительных параметров и методов для рисования свойств/полей
    /// </summary>
    public static class XEditorInspector
    {
        #region Fields
        /// <summary>
        /// Рабочий контент GUI
        /// </summary>
        public static readonly GUIContent Content = new GUIContent();

        /// <summary>
        /// Рабочий контент GUI для надписей
        /// </summary>
        public static readonly GUIContent ContentLabel = new GUIContent();

        /// <summary>
        /// Рабочий контент GUI для заголовков
        /// </summary>
        public static readonly GUIContent ContentHeader = new GUIContent();

        /// <summary>
        /// Актуальная ширина панели инспектора свойств
        /// </summary>
        public static readonly PropertyInfo PropertyContextWidth = typeof(EditorGUIUtility).GetProperty("contextWidth", BindingFlags.NonPublic | BindingFlags.Static);
        #endregion

        #region Main methods
        /// <summary>
        /// Получение текстуры иконки связанной с расширением файла
        /// </summary>
        /// <remarks>
        /// https://answers.unity.com/questions/792118/what-are-the-editor-resources-by-name-for-editorgu.html
        /// </remarks>
        /// <param name="ext">Расширение файла</param>
        /// <returns>Текстура иконки файла или текстура общей иконки</returns>
        public static Texture2D GetIconForFile(string ext)
        {
            var fileIcon = ext;
            var dot = ext.IndexOf('.');
            if (dot > -1)
            {
                fileIcon = ext.Substring(dot + 1);
            }

            fileIcon = fileIcon.ToLower();

            switch (fileIcon)
            {
                case "guiskin":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_GUISKIN).image as Texture2D;
                    }
                case "mat":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_MATERIAL).image as Texture2D;
                    }
                case "prefab":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_PREFAB).image as Texture2D;
                    }
                case "shader":
                case "cginc":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_SHADER).image as Texture2D;
                    }
                case "txt":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_TEXT).image as Texture2D;
                    }
                case "xml":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_XML).image as Texture2D;
                    }
                case "cs":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_SCRIPT).image as Texture2D;
                    }
                case "boo":
                    {
                        return EditorGUIUtility.FindTexture("boo Script Icon");
                    }
                case "js":
                    {
                        return EditorGUIUtility.FindTexture("Js Script Icon");
                    }
                case "meta":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_META).image as Texture2D;
                    }
                case "unity":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_SCENE).image as Texture2D;
                    }
                case "asset":
                case "prefs":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_GAME_MANAGER).image as Texture2D;
                    }
                case "anim":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_ANIMATION_CLIP).image as Texture2D;
                    }
                case "signal":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_SIGNAL).image as Texture2D;
                    }
                case "physicmaterial":
                case "physicsmaterial":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_PHYSICS_MATERIAL).image as Texture2D;
                    }
                case "ttf":
                case "otf":
                case "fon":
                case "fnt":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_FONT).image as Texture2D;
                    }
                case "aac":
                case "aif":
                case "aiff":
                case "au":
                case "mid":
                case "midi":
                case "mp3":
                case "mpa":
                case "ra":
                case "ram":
                case "wma":
                case "wav":
                case "wave":
                case "ogg":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_AUDIO_SOURCE).image as Texture2D;
                    }
                case "ai":
                case "apng":
                case "png":
                case "bmp":
                case "cdr":
                case "dib":
                case "eps":
                case "exif":
                case "gif":
                case "ico":
                case "icon":
                case "j":
                case "j2c":
                case "j2k":
                case "jas":
                case "jiff":
                case "jng":
                case "jp2":
                case "jpc":
                case "jpe":
                case "jpeg":
                case "jpf":
                case "jpg":
                case "jpw":
                case "jpx":
                case "jtf":
                case "mac":
                case "omf":
                case "qif":
                case "qti":
                case "qtif":
                case "tex":
                case "tfw":
                case "tga":
                case "tif":
                case "tiff":
                case "wmf":
                case "psd":
                case "exr":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_TEXTURE_2D).image as Texture2D;
                    }
                case "3df":
                case "3dm":
                case "3dmf":
                case "3ds":
                case "3dv":
                case "3dx":
                case "blend":
                case "c4d":
                case "lwo":
                case "lws":
                case "ma":
                case "max":
                case "mb":
                case "mesh":
                case "obj":
                case "vrl":
                case "wrl":
                case "wrz":
                case "fbx":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_PREFAB_MODEL).image as Texture2D;
                    }
                case "asf":
                case "asx":
                case "avi":
                case "dat":
                case "divx":
                case "dvx":
                case "mlv":
                case "m2l":
                case "m2t":
                case "m2ts":
                case "m2v":
                case "m4e":
                case "m4v":
                case "mjp":
                case "mov":
                case "movie":
                case "mp21":
                case "mp4":
                case "mpe":
                case "mpeg":
                case "mpg":
                case "mpv2":
                case "ogm":
                case "qt":
                case "rm":
                case "rmvb":
                case "wmw":
                case "xvid":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_VIDEO_CLIP).image as Texture2D;
                    }
                case "colors":
                case "gradients":
                case "curves":
                case "curvesnormalized":
                case "particlecurves":
                case "particlecurvessigned":
                case "particledoublecurves":
                case "particledoublecurvessigned":
                    {
                        return EditorGUIUtility.IconContent(XEditorStyles.ICON_SCRIPTABLE_OBJECT).image as Texture2D;
                    }
            }
            return EditorGUIUtility.IconContent(XEditorStyles.ICON_DEFAULT_ASSET).image as Texture2D;
        }

        /// <summary>
        /// Получение текстуры иконки связанной с указанным типом
        /// </summary>
        /// <param name="type">Тип объекта</param>
        /// <returns>Текстура иконки файла или текстура общей иконки</returns>
        public static Texture2D GetIconForFile(Type type)
        {
            var typeEditor = typeof(EditorGUIUtility);
            var findTextureByTypeMethod = typeEditor.GetMethod("FindTextureByType", BindingFlags.NonPublic | BindingFlags.Static);

            if (findTextureByTypeMethod != null)
            {
                var texture = findTextureByTypeMethod.Invoke(null, new object[] { type });
                if (texture is Texture2D texture2D)
                {
                    return texture2D;
                }
            }

            return EditorGUIUtility.FindTexture("DefaultAsset Icon");
        }
        #endregion

        #region Content methods
        /// <summary>
        /// Подготовка контента GUI для рисования свойства на основе доступных атрибутов
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        public static void PrepareContent(SerializedProperty property)
        {
            if (property != null)
            {
                // Есть атрибут имени
                LotusDisplayNameAttribute name = property.GetAttribute<LotusDisplayNameAttribute>();
                if (name != null)
                {
                    Content.text = name.Name;
                    EditorGUI.indentLevel = name.Indent;
                }
                else
                {
                    Content.text = property.displayName;
                }

                // Есть атрибут подсказки
                TooltipAttribute tooltip = property.GetAttribute<TooltipAttribute>();
                if (tooltip != null)
                {
                    Content.tooltip = tooltip.tooltip;
                }
                else
                {
                    Content.tooltip = string.Empty;
                }
            }
        }
        #endregion

        #region Section draw methods
        /// <summary>
        /// Рисование секции
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSection(string text, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            ContentHeader.text = text;
            DrawSection(ContentHeader, XEditorStyles.ColorHeaderSection, textAnchor);
        }

        /// <summary>
        /// Рисование секции
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSection(string text, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            ContentHeader.text = text;
            DrawSection(ContentHeader, colorText, textAnchor);
        }

        /// <summary>
        /// Рисование секции
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSection(GUIContent content, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            DrawSection(content, XEditorStyles.ColorHeaderSection, textAnchor);
        }

        /// <summary>
        /// Рисование секции
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSection(GUIContent content, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            GUIStyle style = EditorStyles.boldLabel;

            // Старые параметры
            Color oldColor = style.normal.textColor;
            TextAnchor oldAnchor = style.alignment;
            style.fontSize++;

            // Новые параметры
            style.normal.textColor = colorText;
            style.alignment = textAnchor;

            // Рисуем
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            GUILayout.Label(content, style);

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
            style.fontSize--;
        }

        /// <summary>
        /// Рисование секции в области
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSectionBox(string text, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            ContentHeader.text = text;
            DrawSectionBox(ContentHeader, XEditorStyles.ColorHeaderSection, textAnchor);
        }

        /// <summary>
        /// Рисование секции в области
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSectionBox(string text, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            ContentHeader.text = text;
            DrawSectionBox(ContentHeader, colorText, textAnchor);
        }

        /// <summary>
        /// Рисование секции в области
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSectionBox(GUIContent content, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            DrawSectionBox(content, XEditorStyles.ColorHeaderSection, textAnchor);
        }

        /// <summary>
        /// Рисование секции в области
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawSectionBox(GUIContent content, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            GUIStyle style = EditorStyles.boldLabel;

            // Старые параметры
            Color oldColor = style.normal.textColor;
            TextAnchor oldAnchor = style.alignment;
            style.fontSize++;

            // Новые параметры
            style.normal.textColor = colorText;
            style.alignment = textAnchor;

            // Рисуем
            Rect position = EditorGUILayout.BeginHorizontal();
            {
                //GUILayout.Space(-OffsetFoldoutOne);
                GUILayout.Box(GUIContent.none, EditorStyles.helpBox, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT + EditorGUIUtility.standardVerticalSpacing));

                position.height -= EditorGUIUtility.standardVerticalSpacing;
                if (textAnchor == TextAnchor.UpperLeft ||
                    textAnchor == TextAnchor.MiddleLeft ||
                    textAnchor == TextAnchor.LowerLeft)
                {
                    position.x += EditorGUIUtility.standardVerticalSpacing * 2;
                }

                GUI.Label(position, content, style);
            }
            EditorGUILayout.EndHorizontal();

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
            style.fontSize--;
        }

        /// <summary>
        /// Рисование секции в области по центру инспектора свойств с раскрытием
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawSectionFoldout(string text, bool opened, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            var result = string.Empty;
            if (opened == false)
            {
                result = XStringHelper.TriangleRight + " " + text;
            }
            else
            {
                result = XStringHelper.TriangleDown + " " + text;
            }

            ContentHeader.text = result;
            DrawSectionBox(ContentHeader, textAnchor);

            if (Event.current.type == EventType.MouseDown)
            {
                Rect mouseRect = GUILayoutUtility.GetLastRect();
                if (mouseRect.Contains(Event.current.mousePosition))
                {
                    opened = !opened;
                    Event.current.Use();
                }
            }

            return opened;
        }

        /// <summary>
        /// Рисование секции в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="onDraw">Делегат для рисования данных при раскрытии панели</param>
        /// <param name="onMoveUp">Делегат для действий при перемещении вверх</param>
        /// <param name="onMoveDown">Делегат для действий при перемещении вниз</param>
        /// <param name="onDelete">Делегат для действий при удалении</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawSectionFoldout(string text, ref bool opened, Action onDraw,
            Action onMoveUp, Action onMoveDown, Action onDelete,
            TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            var result = string.Empty;
            if (opened == false)
            {
                result = XStringHelper.TriangleRight + " " + text;
            }
            else
            {
                result = XStringHelper.TriangleDown + " " + text;
            }

            ContentHeader.text = result;
            DrawSectionBox(ContentHeader, textAnchor);

            Rect mouseRect = GUILayoutUtility.GetLastRect();

            if (opened)
            {
                onDraw();
            }

            // Управляющие кнопки
            var rectButtonClose = new Rect(mouseRect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouseRect.y + 2,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
            if (GUI.Button(rectButtonClose, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
            {
                onDelete();
            }

            var rectButtonMoveDown = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH *2) - 2, mouseRect.y + 2,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
            if (GUI.Button(rectButtonMoveDown, XStringHelper.TriangleDown, EditorStyles.miniButtonMid))
            {
                onMoveDown();
            }

            var rectButtonMoveUp = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouseRect.y + 2,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.HEADER_HEIGHT - 1);
            if (GUI.Button(rectButtonMoveUp, XStringHelper.TriangleUp, EditorStyles.miniButtonLeft))
            {
                onMoveUp();
            }

            if (Event.current.type == EventType.MouseDown)
            {
                if (mouseRect.Contains(Event.current.mousePosition) &&
                    rectButtonClose.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveDown.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveUp.Contains(Event.current.mousePosition) == false)
                {
                    opened = !opened;
                    Event.current.Use();
                }
            }

            return opened;
        }
        #endregion

        #region Group draw methods
        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(string text, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            ContentHeader.text = text;
            DrawGroup(ContentHeader, XEditorStyles.ColorHeaderGroup, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(string text, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            ContentHeader.text = text;
            DrawGroup(ContentHeader, colorText, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="indent">Уровень смещения</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(string text, Color colorText, int indent, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            ContentHeader.text = text;
            DrawGroup(ContentHeader, colorText, indent, textAnchor);
        }

        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(GUIContent content, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            DrawGroup(content, XEditorStyles.ColorHeaderGroup, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(GUIContent content, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            DrawGroup(content, colorText, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы
        /// </summary>
        /// <param name="content">Контент заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="indent">Уровень смещения</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroup(GUIContent content, Color colorText, int indent, TextAnchor textAnchor = TextAnchor.MiddleLeft)
        {
            GUIStyle style = EditorStyles.boldLabel;

            // Старые параметры
            Color oldColor = style.normal.textColor;
            TextAnchor oldAnchor = style.alignment;

            // Новые параметры
            style.normal.textColor = colorText;
            style.alignment = textAnchor;

            // Рисуем
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            if (indent > 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(XInspectorViewParams.OFFSET_INDENT * indent);
                GUILayout.Label(content, style);
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label(content, style);
            }

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
        }

        /// <summary>
        /// Рисование группы в области
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroupBox(string text, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            DrawGroupBox(text, XEditorStyles.ColorHeaderGroup, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы в области
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroupBox(string text, Color colorText, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            DrawGroupBox(text, colorText, 0, textAnchor);
        }

        /// <summary>
        /// Рисование группы в области
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="colorText">Цвет текста заголовка</param>
        /// <param name="indent">Уровень смещения</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        public static void DrawGroupBox(string text, Color colorText, int indent, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            GUIStyle style = EditorStyles.boldLabel;

            // Старые параметры
            Color oldColor = style.normal.textColor;
            TextAnchor oldAnchor = style.alignment;

            // Новые параметры
            style.normal.textColor = colorText;
            style.alignment = textAnchor;

            // Рисуем
            Rect position = EditorGUILayout.BeginHorizontal();
            {
                if (indent > 0)
                {
                    GUILayout.Space(XInspectorViewParams.OFFSET_INDENT * indent);
                }
                GUILayout.Box(GUIContent.none, EditorStyles.helpBox, GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

                position.height -= EditorGUIUtility.standardVerticalSpacing;
                if (textAnchor == TextAnchor.UpperLeft ||
                    textAnchor == TextAnchor.MiddleLeft ||
                    textAnchor == TextAnchor.LowerLeft)
                {
                    position.x += EditorGUIUtility.standardVerticalSpacing * 2;
                }

                GUI.Label(position, text, style);
            }
            EditorGUILayout.EndHorizontal();

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.alignment = oldAnchor;
        }

        /// <summary>
        /// Рисование группы по центру инспектора свойств с раскрытием и смещением с левой стороны
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawGroupFoldout(string text, bool opened, TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            var result = string.Empty;
            if (opened == false)
            {
                result = XStringHelper.TriangleRight + " " + text;
            }
            else
            {
                result = XStringHelper.TriangleDown + " " + text;
            }

            DrawGroupBox(result, textAnchor);

            if (Event.current.type == EventType.MouseDown)
            {
                Rect mouseRect = GUILayoutUtility.GetLastRect();
                if (mouseRect.Contains(Event.current.mousePosition))
                {
                    opened = !opened;
                    Event.current.Use();
                }
            }

            return opened;
        }

        /// <summary>
        /// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="onDraw">Делегат для рисования данных при раскрытии панели</param>
        /// <param name="onMoveUp">Делегат для действий при перемещении вверх</param>
        /// <param name="onMoveDown">Делегат для действий при перемещении вниз</param>
        /// <param name="onDelete">Делегат для действий при удалении</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawGroupFoldout(string text, ref bool opened, Action onDraw,
            Action onMoveUp, Action onMoveDown, Action onDelete,
            TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            var result = string.Empty;
            if (opened == false)
            {
                result = XStringHelper.TriangleRight + " " + text;
            }
            else
            {
                result = XStringHelper.TriangleDown + " " + text;
            }

            DrawGroupBox(result, textAnchor);

            Rect mouseRect = GUILayoutUtility.GetLastRect();

            if (opened)
            {
                onDraw();
            }

            // Управляющие кнопки
            var rectButtonDelete = new Rect(mouseRect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonDelete, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
            {
                onDelete();
            }

            var rectButtonMoveDown = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 2) - 2, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonMoveDown, XStringHelper.TriangleDown, EditorStyles.miniButtonMid))
            {
                onMoveDown();
            }

            var rectButtonMoveUp = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonMoveUp, XStringHelper.TriangleUp, EditorStyles.miniButtonLeft))
            {
                onMoveUp();
            }

            if (Event.current.type == EventType.MouseDown)
            {
                if (mouseRect.Contains(Event.current.mousePosition) &&
                    rectButtonDelete.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveDown.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveUp.Contains(Event.current.mousePosition) == false)
                {
                    opened = !opened;
                    Event.current.Use();
                }
            }

            return opened;
        }

        /// <summary>
        /// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="onDraw">Делегат для рисования данных при раскрытии панели</param>
        /// <param name="onMoveUp">Делегат для действий при перемещении вверх</param>
        /// <param name="onMoveDown">Делегат для действий при перемещении вниз</param>
        /// <param name="onDuplicate">Делегат для действий при дублировании элемента</param>
        /// <param name="onDelete">Делегат для действий при удалении</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawGroupFoldout(string text, ref bool opened, Action onDraw,
            Action onMoveUp, Action onMoveDown, Action onDuplicate, Action onDelete,
            TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            return DrawGroupFoldout(0, text, ref opened, onDraw, onMoveUp, onMoveDown, onDuplicate,
                onDelete, textAnchor);
        }

        /// <summary>
        /// Рисование группы в области по центру инспектора свойств с раскрытием и делегатами рисования и управления
        /// </summary>
        /// <param name="indent">Уровень вложения заголовка</param>
        /// <param name="text">Текст заголовка</param>
        /// <param name="opened">Статус открывания</param>
        /// <param name="onDraw">Делегат для рисования данных при раскрытии панели</param>
        /// <param name="onMoveUp">Делегат для действий при перемещении вверх</param>
        /// <param name="onMoveDown">Делегат для действий при перемещении вниз</param>
        /// <param name="onDuplicate">Делегат для действий при дублировании элемента</param>
        /// <param name="onDelete">Делегат для действий при удалении</param>
        /// <param name="textAnchor">Выравнивание текста</param>
        /// <returns>Статус открытости</returns>
        public static bool DrawGroupFoldout(int indent, string text, ref bool opened, Action onDraw,
            Action onMoveUp, Action onMoveDown, Action onDuplicate, Action onDelete,
            TextAnchor textAnchor = TextAnchor.MiddleCenter)
        {
            var result = string.Empty;
            if (opened == false)
            {
                result = XStringHelper.TriangleRight + " " + text;
            }
            else
            {
                result = XStringHelper.TriangleDown + " " + text;
            }

            DrawGroupBox(result, XEditorStyles.ColorHeaderGroup, indent, textAnchor);

            Rect mouseRect = GUILayoutUtility.GetLastRect();

            if (opened)
            {
                onDraw();
            }

            // Управляющие кнопки
            var rectButtonDelete = new Rect(mouseRect.xMax - XInspectorViewParams.BUTTON_MINI_WIDTH - 1, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonDelete, "X", XEditorStyles.ButtonMiniDefaultRedRightStyle))
            {
                onDelete();
            }

            var rectButtonDuplicate = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 2) - 2, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonDuplicate, "D", EditorStyles.miniButtonMid))
            {
                onDuplicate();
            }

            var rectButtonMoveDown = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 3) - 3, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonMoveDown, XStringHelper.TriangleDown, EditorStyles.miniButtonMid))
            {
                onMoveDown();
            }

            var rectButtonMoveUp = new Rect(mouseRect.xMax - (XInspectorViewParams.BUTTON_MINI_WIDTH * 4) - 4, mouseRect.y + 1,
                XInspectorViewParams.BUTTON_MINI_WIDTH, XInspectorViewParams.CONTROL_HEIGHT_SPACE - 1);
            if (GUI.Button(rectButtonMoveUp, XStringHelper.TriangleUp, EditorStyles.miniButtonLeft))
            {
                onMoveUp();
            }

            if (Event.current.type == EventType.MouseDown)
            {
                if (mouseRect.Contains(Event.current.mousePosition) &&
                    rectButtonDelete.Contains(Event.current.mousePosition) == false &&
                    rectButtonDuplicate.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveDown.Contains(Event.current.mousePosition) == false &&
                    rectButtonMoveUp.Contains(Event.current.mousePosition) == false)
                {
                    opened = !opened;
                    Event.current.Use();
                }
            }

            return opened;
        }
        #endregion

        #region Caption methods
        /// <summary>
        /// Рисование заголовков
        /// </summary>
        /// <param name="text">Текст заголовка</param>
        public static void DrawCaption(string text)
        {
            var previousColor = GUI.color;
            GUI.color = Color.yellow;
            EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
            GUI.color = previousColor;
        }
        #endregion

        #region Layout methods
        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="offsetRight">Дополнительное смещение от правой стороны</param>
        public static void ComputeRects(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField, float coeff2,
            float offsetRight = 0)
        {
            // Получаем актуальную ширину инспектора свойств
            var contextWidth = (float)PropertyContextWidth.GetValue(null, null);

            // Рабочая ширина
            var workWidth = contextWidth - (offsetRight + XInspectorViewParams.SCROLL_WIDTH);
            rectLabel = rect;

            // Если ноль то ширина для надписи равна текущей
            if (coeff1 == 0)
            {
                rectLabel.width = EditorGUIUtility.labelWidth;

                rectField = rect;
                rectField.x = rectLabel.xMax;
                rectField.width = Mathf.Min(workWidth, rect.width) - EditorGUIUtility.labelWidth;
            }
            else
            {
                rectLabel.width = workWidth * coeff1;

                rectField = rect;
                rectField.x = rectLabel.xMax;
                rectField.width = Mathf.Min(workWidth, rect.width) * coeff2;
            }

            if (rectField.xMax > workWidth)
            {
                var delta = rectField.xMax - workWidth - XInspectorViewParams.SCROLL_WIDTH;
                rectField.width -= delta + 6;
            }
        }

        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField1">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="rectField2">Третий вычисляемый прямоугольник</param>
        /// <param name="coeff3">Коэффициент ширины третьего прямоугольника</param>
        /// <param name="offsetRight">Дополнительное смещение от правой стороны</param>
        public static void ComputeRects(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField1, float coeff2,
            out Rect rectField2, float coeff3, float offsetRight = 0)
        {
            // Получаем актуальную ширину инспектора свойств
            var contextWidth = (float)PropertyContextWidth.GetValue(null, null);

            // Рабочая ширина
            var workWidth = contextWidth - (offsetRight + XInspectorViewParams.SCROLL_WIDTH);
            rectLabel = rect;

            // Если ноль то ширина для надписи равна текущей
            if (coeff1 == 0)
            {
                rectLabel.width = EditorGUIUtility.labelWidth;
            }
            else
            {
                rectLabel.width = workWidth * coeff1;
            }

            rectField1 = rect;
            rectField1.x = rectLabel.xMax;
            rectField1.width = Mathf.Min(workWidth, rect.width) * coeff2;

            rectField2 = rect;
            rectField2.x = rectField1.xMax;
            rectField2.width = Mathf.Min(workWidth, rect.width) * coeff3;

            if (rectField2.xMax > workWidth)
            {
                var delta = rectField2.xMax - workWidth - XInspectorViewParams.SCROLL_WIDTH;
                rectField2.width -= delta + 6;
            }
        }

        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="rectButtonRight">Прямоугольник для кнопки справа</param>
        public static void ComputeRectsWithButton(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField,
            float coeff2, out Rect rectButtonRight)
        {
            ComputeRects(rect, out rectLabel, coeff1, out rectField, coeff2, 0);
            rectField.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight = rect;
            rectButtonRight.x = rectField.xMax;
            rectButtonRight.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
        }

        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField1">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="rectField2">Третий вычисляемый прямоугольник</param>
        /// <param name="coeff3">Коэффициент ширины третьего прямоугольника</param>
        /// <param name="rectButtonRight">Прямоугольник для кнопки справа</param>
        public static void ComputeRectsWithButtons(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField1,
            float coeff2, out Rect rectField2, float coeff3, out Rect rectButtonRight)
        {
            ComputeRects(rect, out rectLabel, coeff1, out rectField1, coeff2, out rectField2, coeff3, 0);
            rectField2.width -= XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight = rect;
            rectButtonRight.x = rectField2.xMax;
            rectButtonRight.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
        }

        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="rectButtonLeft">Прямоугольник для кнопки слева</param>
        /// <param name="rectButtonRight">Прямоугольник для кнопки справа</param>
        public static void ComputeRectsWithButtons(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField, float coeff2,
            out Rect rectButtonLeft, out Rect rectButtonRight)
        {
            ComputeRects(rect, out rectLabel, coeff1, out rectField, coeff2, 0);
            rectField.width -= XInspectorViewParams.BUTTON_MINI_WIDTH * 2;

            rectButtonLeft = rect;
            rectButtonLeft.x = rectField.xMax;
            rectButtonLeft.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonLeft.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

            rectButtonRight = rectButtonLeft;
            rectButtonRight.x = rectButtonLeft.xMax;
            rectButtonRight.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
        }

        /// <summary>
        /// Вычисление прямоугольников для отображения элементов с учетом отступов
        /// </summary>
        /// <param name="rect">Базовый прямоугольник</param>
        /// <param name="rectLabel">Первый вычисляемый прямоугольник</param>
        /// <param name="coeff1">Коэффициент ширины первого прямоугольника</param>
        /// <param name="rectField">Второй вычисляемый прямоугольник</param>
        /// <param name="coeff2">Коэффициент ширины второго прямоугольника</param>
        /// <param name="rectButtonLeft">Прямоугольник для кнопки слева</param>
        /// <param name="rectButtonMiddle">Прямоугольник для кнопки по центру</param>
        /// <param name="rectButtonRight">Прямоугольник для кнопки справа</param>
        public static void ComputeRectsWithButtons(Rect rect, out Rect rectLabel, float coeff1, out Rect rectField, float coeff2,
            out Rect rectButtonLeft, out Rect rectButtonMiddle, out Rect rectButtonRight)
        {
            ComputeRects(rect, out rectLabel, coeff1, out rectField, coeff2, 0);
            rectField.width -= XInspectorViewParams.BUTTON_MINI_WIDTH * 3;

            rectButtonLeft = rect;
            rectButtonLeft.x = rectField.xMax;
            rectButtonLeft.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonLeft.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

            rectButtonMiddle = rectButtonLeft;
            rectButtonMiddle.x = rectButtonLeft.xMax;
            rectButtonMiddle.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonMiddle.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;

            rectButtonRight = rectButtonMiddle;
            rectButtonRight.x = rectButtonMiddle.xMax;
            rectButtonRight.width = XInspectorViewParams.BUTTON_MINI_WIDTH;
            rectButtonRight.height = XInspectorViewParams.BUTTON_MINI_HEIGHT;
        }
        #endregion

        #region Property editors
        /// <summary>
        /// Редактор выбора индекса из массива значений строкового типа
        /// </summary>
        /// <param name="selected">Выбранный индекс</param>
        /// <param name="contents">Массив значений</param>
        /// <returns>Выбранный индекс</returns>
        public static int SelectorIndex(int selected, string[] contents)
        {
            selected = EditorGUILayout.Popup(selected, contents, EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

            return selected;
        }

        /// <summary>
        /// Редактор выбора индекса из массива значений строкового типа
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="selected">Выбранный индекс</param>
        /// <param name="contents">Массив значений</param>
        /// <returns>Выбранный индекс</returns>
        public static int SelectorIndex(string name, int selected, string[] contents)
        {
            selected = EditorGUILayout.Popup(name, selected, contents, EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

            return selected;
        }

        /// <summary>
        /// Редактор выбора значения из массива значений строкового типа
        /// </summary>
        /// <param name="selected">Выбранное значение</param>
        /// <param name="contents">Массив значений</param>
        /// <returns>Выбранное значение</returns>
        public static string SelectorValue(string selected, string[] contents)
        {
            var index = -1;

            for (var i = 0; i < contents.Length; i++)
            {
                if (contents[i].EqualIgnoreCase(selected))
                {
                    index = i;
                    break;
                }
            }

            index = EditorGUILayout.Popup(index, contents, EditorStyles.toolbarPopup, GUILayout.ExpandWidth(true));

            if (index == -1) return contents[0];
            return contents[index];
        }

        /// <summary>
        /// Редактор свойства типа bool
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static bool PropertyBoolean(string name, bool value)
        {
            Content.text = name;
            value = EditorGUILayout.Toggle(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа bool с отображением как группы
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static bool PropertyBooleanAsGroup(string name, bool value)
        {
            GUIStyle style = EditorStyles.label;

            // Старые параметры
            Color oldColor = style.normal.textColor;
            Font oldFont = style.font;

            // Новые параметры
            style.normal.textColor = XEditorStyles.ColorHeaderGroup;
            style.font = EditorStyles.boldFont;

            // Рисуем
            Content.text = name;
            value = EditorGUILayout.Toggle(Content, value);

            // Восстанавливаем старые параметры
            style.normal.textColor = oldColor;
            style.font = oldFont;

            return value;
        }

        /// <summary>
        /// Редактор свойства типа int
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static int PropertyInt(string name, int value)
        {
            Content.text = name;
            value = EditorGUILayout.IntField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа int в указанном диапазоне
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Измененное значение свойства</returns>
        public static int PropertyIntSlider(string name, int value, int min, int max)
        {
            Content.text = name;
            value = EditorGUILayout.IntSlider(Content, value, min, max);
            return value;
        }

        /// <summary>
        /// Редактор свойства для слоя
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static int PropertyLayer(string name, int value)
        {
            Content.text = name;
            value = EditorGUILayout.LayerField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа float
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static float PropertyFloat(string name, float value)
        {
            Content.text = name;
            value = EditorGUILayout.FloatField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа float в указанном диапазоне
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Измененное значение свойства</returns>
        public static float PropertyFloatSlider(string name, float value, float min, float max)
        {
            Content.text = name;
            value = EditorGUILayout.Slider(Content, value, min, max);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа float в указанном диапазоне
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <param name="step">Шаг приращения</param>
        /// <returns>Измененное значение свойства</returns>
        public static float PropertyFloatManager(string name, float value, float min, float max, float step)
        {
            Content.text = name;
            EditorGUILayout.BeginHorizontal();
            {
                value = EditorGUILayout.Slider(Content, value, min, max);

                if (GUILayout.Button(XStringHelper.TriangleDown, EditorStyles.miniButtonLeft,
                    GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                {
                    value -= step;
                    if (value < min) value = min;
                }

                if (GUILayout.Button(XStringHelper.TriangleUp, EditorStyles.miniButtonMid,
                    GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                {
                    value += step;
                    if (value > max) value = max;
                }

                if (GUILayout.Button("Rand", EditorStyles.miniButtonRight))
                {
                    value = UnityEngine.Random.Range(min, max);
                    if (value < min) value = min;
                    if (value > max) value = max;
                }
            }
            EditorGUILayout.EndHorizontal();
            return value;
        }

        /// <summary>
        /// Рисовании строкового свойства
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        public static void PropertyLabel(string name, string value)
        {
            Content.text = name;
            ContentLabel.text = value;
            EditorGUILayout.LabelField(Content, ContentLabel, GUILayout.ExpandWidth(true));
        }

        /// <summary>
        /// Редактор свойства типа string
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static string PropertyString(string name, string value)
        {
            if (value == null)
            {
                value = "";
            }

            Content.text = name;
            value = EditorGUILayout.TextField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Enum
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static TEnum PropertyEnum<TEnum>(string name, TEnum value) where TEnum : Enum
        {
            Content.text = name;
            value = (TEnum)EditorGUILayout.EnumPopup(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Enum (для установки флагов)
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Enum PropertyFlags(string name, Enum value)
        {
            Content.text = name;
            value = EditorGUILayout.EnumFlagsField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Vector2
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Vector2 PropertyVector2D(string name, Vector2 value)
        {
            Content.text = name;
            value = EditorGUILayout.Vector2Field(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Vector3
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Vector3 PropertyVector3D(string name, Vector3 value)
        {
            Content.text = name;
            value = EditorGUILayout.Vector3Field(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Vector4
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Vector4 PropertyVector4D(string name, Vector4 value)
        {
            Content.text = name;
            value = EditorGUILayout.Vector4Field(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Vector4 представляемого как бордюры в формате данных спрайта
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="data">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Vector4 PropertyBorderSprite(string name, Vector4 data)
        {
            Content.text = name;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel(Content, EditorStyles.label);
                data.x = EditorGUILayout.FloatField(data.x, GUILayout.MaxWidth(60));
                data.w = EditorGUILayout.FloatField(data.w, GUILayout.MaxWidth(60));
                data.z = EditorGUILayout.FloatField(data.z, GUILayout.MaxWidth(60));
                data.y = EditorGUILayout.FloatField(data.y, GUILayout.MaxWidth(60));
            }
            EditorGUILayout.EndHorizontal();

            return data;
        }

        /// <summary>
        /// Редактор свойства типа Vector4 представляемого как бордюры в стандартном формате
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="data">Значение свойства</param>
        /// <returns>Отредактированное значение</returns>
        public static Vector4 PropertyBorderNormal(string name, Vector4 data)
        {
            Content.text = name;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel(Content, EditorStyles.label);
                data.x = EditorGUILayout.FloatField(data.x, GUILayout.MaxWidth(60));
                data.y = EditorGUILayout.FloatField(data.y, GUILayout.MaxWidth(60));
                data.z = EditorGUILayout.FloatField(data.z, GUILayout.MaxWidth(60));
                data.w = EditorGUILayout.FloatField(data.w, GUILayout.MaxWidth(60));
            }
            EditorGUILayout.EndHorizontal();

            return data;
        }

        /// <summary>
        /// Редактор свойства типа Color
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Color PropertyColor(string name, Color value)
        {
            Content.text = name;
            value = EditorGUILayout.ColorField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Rect
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static Rect PropertyRect(string name, Rect value)
        {
            Content.text = name;
            value = EditorGUILayout.RectField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа RectOffset
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static RectOffset PropertyRectOffset(string name, RectOffset value)
        {
            Content.text = name;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel(Content, EditorStyles.label);
                value.left = EditorGUILayout.IntField(value.left, GUILayout.MaxWidth(60));
                value.top = EditorGUILayout.IntField(value.top, GUILayout.MaxWidth(60));
                value.right = EditorGUILayout.IntField(value.right, GUILayout.MaxWidth(60));
                value.bottom = EditorGUILayout.IntField(value.bottom, GUILayout.MaxWidth(60));
            }
            EditorGUILayout.EndHorizontal();

            return value;
        }

        /// <summary>
        /// Редактор свойства типа AnimationCurve
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static AnimationCurve PropertyCurve(string name, AnimationCurve value)
        {
            Content.text = name;
            value = EditorGUILayout.CurveField(Content, value);
            return value;
        }

        /// <summary>
        /// Редактор свойства типа Component
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static TComponent PropertyComponent<TComponent>(string name, TComponent value) where TComponent : UnityEngine.Component
        {
            Content.text = name;
            var result = EditorGUILayout.ObjectField(Content, value, typeof(TComponent), true) as TComponent;
            return result;
        }

        /// <summary>
        /// Редактор свойства типа GameObject
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static GameObject PropertyGameObject(string name, GameObject value)
        {
            Content.text = name;
            var result = EditorGUILayout.ObjectField(Content, value, typeof(GameObject), true) as GameObject;
            return result;
        }

        /// <summary>
        /// Редактор свойства типа UnityEngine.Object (для ресурсов)
        /// </summary>
        /// <param name="name">Имя свойства</param>
        /// <param name="value">Значение свойства</param>
        /// <returns>Измененное значение свойства</returns>
        public static TResource PropertyResource<TResource>(string name, TResource value) where TResource : UnityEngine.Object
        {
            Content.text = name;
            var result = EditorGUILayout.ObjectField(Content, value, typeof(TResource), false,
                GUILayout.Height(EditorGUIUtility.singleLineHeight)) as TResource;
            return result;
        }
        #endregion

        #region Decoration methods
        /// <summary>
        /// Получение и отображение атрибутов декорации
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        public static void DrawDecorateAttributes(SerializedProperty property)
        {
            CCachedMember cachedData = XEditorCachedData.GetMember(property);
            DrawDecorateAttributes(cachedData);
        }

        /// <summary>
        /// Получение и отображение атрибутов декорации
        /// </summary>
        /// <param name="memberInfo">Метаданные члена данных</param>
        public static void DrawDecorateAttributes(MemberInfo memberInfo)
        {
            CCachedMember cachedData = XEditorCachedData.GetMember(memberInfo);
            DrawDecorateAttributes(cachedData);
        }

        /// <summary>
        /// Получение и отображение атрибутов декорации
        /// </summary>
        /// <param name="cachedData">Кэшированные данные</param>
        public static void DrawDecorateAttributes(CCachedMember cachedData)
        {
            // Space
            SpaceAttribute spaceAttribute = cachedData.GetAttribute<SpaceAttribute>();
            if (spaceAttribute != null)
            {
                GUILayout.Space(spaceAttribute.height);
            }

            // Header
            HeaderAttribute headerAttribute = cachedData.GetAttribute<HeaderAttribute>();
            if (headerAttribute != null)
            {
                GUILayout.Label(headerAttribute.header, EditorStyles.boldLabel);
            }

            // HeaderSection
            LotusHeaderSectionAttribute headerSectionAttribute = cachedData.GetAttribute<LotusHeaderSectionAttribute>();
            if (headerSectionAttribute != null)
            {
                XEditorInspector.DrawSection(headerSectionAttribute.Name, headerSectionAttribute.TextColor,
                    XEnumConverter.ToEnum<TextAnchor>(headerSectionAttribute.TextAlignment));
            }

            // HeaderSectionBox
            LotusHeaderSectionBoxAttribute headerSectionBoxAttribute = cachedData.GetAttribute<LotusHeaderSectionBoxAttribute>();
            if (headerSectionBoxAttribute != null)
            {
                XEditorInspector.DrawSectionBox(headerSectionBoxAttribute.Name, headerSectionBoxAttribute.TextColor,
                    XEnumConverter.ToEnum<TextAnchor>(headerSectionBoxAttribute.TextAlignment));
            }

            // HeaderGroup
            LotusHeaderGroupAttribute headerGroupAttribute = cachedData.GetAttribute<LotusHeaderGroupAttribute>();
            if (headerGroupAttribute != null)
            {
                if (headerGroupAttribute.TextColor == XColors.Zero)
                {
                    headerGroupAttribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
                }

                XEditorInspector.DrawGroup(headerGroupAttribute.Name,
                    headerGroupAttribute.TextColor,
                    headerGroupAttribute.Indent,
                    XEnumConverter.ToEnum<TextAnchor>(headerGroupAttribute.TextAlignment));
            }

            // HeaderGroup
            LotusHeaderGroupBoxAttribute headerGroupBoxAttribute = cachedData.GetAttribute<LotusHeaderGroupBoxAttribute>();
            if (headerGroupBoxAttribute != null)
            {
                if (headerGroupBoxAttribute.TextColor == XColors.Zero)
                {
                    headerGroupBoxAttribute.TextColor = new TColor(XEditorStyles.ColorHeaderGroup);
                }

                XEditorInspector.DrawGroupBox(headerGroupBoxAttribute.Name,
                    headerGroupBoxAttribute.TextColor,
                    headerGroupBoxAttribute.Indent,
                    XEnumConverter.ToEnum<TextAnchor>(headerGroupBoxAttribute.TextAlignment));
            }
        }
        #endregion

    }
    /**@}*/
}
#endif
