#if UNITY_EDITOR
using System.Collections.Generic;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /// <summary>
    /// Редактор для просмотра стилей редактора Unity
    /// </summary>
    /// <remarks>
    /// За основу взята реализация:
    /// https://forum.unity.com/threads/advanced-editor-styles-viewer-free.284148/
    /// </remarks>
    public class LotusViewerEditorStylesWindow : EditorWindow
    {
        #region Nested types
        /// <summary>
        /// Параметры отображения стиля
        /// </summary>
        public class StylePref
        {
            public int Height;
            public int Width;
            public bool NpActive;
            public bool PActive;
            public bool Enabled = true;
            public Color BgColor = Color.white;
            public Color ContentColor = Color.white;
            public bool DisplayText;
            public string Text = "ABC";
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Показать для просмотра стилей редактора Unity
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathUtility + "Viewer Editor Styles", false, XCoreSettingsEditor.MenuOrderUtility + 20)]
        public static void ShowViewerEditorStyles()
        {
            var window = GetWindow<LotusViewerEditorStylesWindow>();
            window.titleContent.text = "Viewer Editor Styles";

            window.minSize = new Vector2(335, 400);
            window.Show();
        }
        #endregion

        #region Fields
        private Vector2 _scrollPos;
        private int _countSize;
        private int _initialCount;
        private readonly List<GUIStyle> _editorStyles = new List<GUIStyle>();
        private int _selSearchParam = 2;
        private int _selOption;
        private int _selSkin = 1;

        private GenericMenu _optionsMenu = new GenericMenu();
        private readonly List<StylePref> _stylePrefs = new List<StylePref>();
        private int _totalSizeOfView;
        private readonly List<GUISkin> _projectSkins = new List<GUISkin>();
        private readonly List<GUISkin> _usableSkins = new List<GUISkin>();
        private readonly List<string> _guiSkins = new List<string>();

        private readonly string[] _searchParams = new string[]
        {
            "StartsWith",
            "EndsWith",
            "Contains"
        };

        private int _totalSizeOfStyles;
        private string _lastCopy = string.Empty;
        private string _searchText = string.Empty;

        [System.Obsolete("Не используется в текущем UI окна")]
        public EditorSkin ESkin;
        #endregion

        #region Unity events
        /// <summary>
        /// Активация окна
        /// </summary>
        private void OnEnable()
        {
            _totalSizeOfStyles = 0;

            _usableSkins.Clear();
            _projectSkins.Clear();
            _guiSkins.Clear();

            _projectSkins.AddRange(XEditorAssetDatabase.GetAssetsFromFolder<GUISkin>(XEditorSettings.ASSETS_PATH));
            _usableSkins.Add(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game));
            _usableSkins.Add(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector));

            foreach (GUISkin skin in _projectSkins)
            {
                _usableSkins.Add(skin);
            }

            foreach (GUISkin skin in _usableSkins)
            {
                _guiSkins.Add(skin.name);
            }

            _guiSkins[1] = "InspectorSkin";

            LoadSkin(_usableSkins[_selSkin]);
        }

        /// <summary>
        /// Рисование UnityGUI
        /// </summary>
        private void OnGUI()
        {
            wantsMouseMove = true;
            GUI.Box(new Rect(0, 0, position.width, 17), GUIContent.none, EditorStyles.toolbar);
            GUI.Label(new Rect(5, 0, 500, 14), "Count : " + _countSize);

            _selSkin = EditorGUI.Popup(new Rect(position.width - 220, 0, 90, 14), string.Empty, _selSkin,
                _guiSkins.ToArray(), EditorStyles.toolbarDropDown);
            if (GUI.changed)
            {
                LoadSkin(_usableSkins[_selSkin]);
            }

            GUI.BeginGroup(new Rect(position.width - 102, 2, 86, 14));
            _searchText = GUI.TextField(new Rect(-16, 0, 102, 14), _searchText, EditorStyles.toolbarSearchField);
            GUI.EndGroup();

            _selSearchParam = EditorGUI.Popup(new Rect(position.width - 118, 2, 16, 16), string.Empty, _selSearchParam,
                _searchParams, XEditorStyles.SEARCH_FIELD_POPUP);

            if (GUI.Button(new Rect(position.width - 16, 2, 16, 14), GUIContent.none, XEditorStyles.SEARCH_FIELD_CANCEL))
            {
                _searchText = string.Empty;
                Repaint();
            }

            if (GUI.changed)
            {
                UpdateListSize();
            }

            GUI.Box(new Rect(0, position.height - 17, position.width, 17), GUIContent.none, "OL Title");
            float lastY = 0;
            var count = 0;

            _scrollPos = GUI.BeginScrollView(new Rect(0, 18, position.width, position.height - 35), _scrollPos,
                new Rect(0, 0, position.width - 16, _totalSizeOfView + 10));
            for (var i = 0; i < _initialCount; i++)
            {
                GUIStyle style = _editorStyles[i];
                if (CanShow(style.name))
                {
                    StylePref stylePref = _stylePrefs[i];
                    float areaHeight = stylePref.Height;
                    var newY = lastY + (5 * count);
                    GUI.Box(new Rect(5, newY + 5, position.width - 26, areaHeight + 54), GUIContent.none, EditorStyles.toolbar);
                    var area = new Rect(5, newY + 5, position.width - 26, areaHeight + 50);
                    GUI.BeginGroup(area);
                    GUI.Box(new Rect(0, 0, area.width - 1, 17), GUIContent.none, "CN Box");
                    GUI.Box(new Rect(0, 18, area.width - 1, 17), GUIContent.none, "OL Title");

                    GUI.TextField(new Rect(0, 2, area.width - 10, 16),
                        i + " : " + style.name);

                    if (GUI.Button(new Rect(0, 18, 60, 14), "Options", "OL Title"))
                    {
                        _selOption = i;
                        _optionsMenu = new GenericMenu();
                        _optionsMenu.AddItem(new GUIContent("Copy"), false, CopyItem);
                        _optionsMenu.AddItem(new GUIContent("Display Sample Text"), _stylePrefs[_selOption].DisplayText, ShowSampleText);
                        _optionsMenu.AddItem(new GUIContent("Enable or Disable"), _stylePrefs[_selOption].Enabled, ToggleDisabled);
                        _optionsMenu.DropDown(new Rect(0, 22, 30, 14));
                    }

                    GUI.color = Color.gray;
                    GUI.Box(new Rect(4, 38, area.width - 8, area.height - 38), GUIContent.none, "GroupBox");
                    GUI.color = Color.white;

                    if (_stylePrefs[i].DisplayText)
                    {
                        _stylePrefs[i].Text = GUI.TextField(new Rect(62, 20, 40, 14), _stylePrefs[i].Text);
                        var colorGroup = new Rect(new Rect(104, 20, position.width - 125, 14));
                        GUI.BeginGroup(colorGroup);
                        _stylePrefs[i].BgColor = EditorGUI.ColorField(new Rect(0, 0, (colorGroup.width / 2) - 5, 14),
                            _stylePrefs[i].BgColor);
                        GUI.color = _stylePrefs[i].BgColor.Inverted();
                        GUI.Label(new Rect(0, 0, 120, 14), "BG Color");
                        GUI.color = Color.white;

                        _stylePrefs[i].ContentColor =
                            EditorGUI.ColorField(new Rect(colorGroup.width / 2, 0, (colorGroup.width / 2) - 8, 14),
                                _stylePrefs[i].ContentColor);

                        GUI.color = _stylePrefs[i].ContentColor.Inverted();
                        GUI.Label(new Rect(colorGroup.width / 2, 0, 120, 14), "Text Color");
                        GUI.color = Color.white;

                        GUI.EndGroup();
                        GUI.enabled = _stylePrefs[i].Enabled;
                        GUI.backgroundColor = _stylePrefs[i].BgColor;
                        GUI.contentColor = _stylePrefs[i].ContentColor;
                        Vector2 size = style.CalcSize(new GUIContent(_stylePrefs[i].Text));
                        _stylePrefs[i].NpActive = GUI.Toggle(new Rect(10, 41, (stylePref.Width / 2) + size.x, size.y),
                            _stylePrefs[i].NpActive, _stylePrefs[i].Text, style);

                        if (EditorGUIUtility.isProSkin && _selSkin == 1)
                        {
                            _stylePrefs[i].PActive = GUI.Toggle(
                                new Rect(30 + (stylePref.Width / 2) + size.x, 41, (stylePref.Width / 2) + size.x, size.y),
                                _stylePrefs[i].PActive, _stylePrefs[i].Text, style.name);
                        }
                        GUI.contentColor = Color.white;
                        GUI.backgroundColor = Color.white;
                    }
                    else
                    {
                        GUI.enabled = _stylePrefs[i].Enabled;
                        _stylePrefs[i].NpActive = GUI.Toggle(new Rect(10, 41, stylePref.Width, stylePref.Height),
                            _stylePrefs[i].NpActive, string.Empty, style);

                        if (EditorGUIUtility.isProSkin && _selSkin == 1)
                        {
                            _stylePrefs[i].PActive = GUI.Toggle(new Rect(30 + stylePref.Width, 41, stylePref.Width, stylePref.Height),
                                _stylePrefs[i].PActive, string.Empty, style.name);
                        }
                    }
                    GUI.enabled = true;
                    GUI.EndGroup();
                    lastY = newY - (5 * count) + (areaHeight + 54);
                    count++;
                }
            }
            GUI.EndScrollView();
            GUI.Label(new Rect(5, position.height - 16, position.width, 16), "Last style copied : " + _lastCopy);
            Repaint();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Загрузка скина
        /// </summary>
        /// <param name="skin">Скин</param>
        private void LoadSkin(GUISkin skin)
        {
            _stylePrefs.Clear();
            _editorStyles.Clear();
            foreach (GUIStyle style in skin)
            {
                _stylePrefs.Add(new StylePref());
                _editorStyles.Add(style);
            }
            _initialCount = _stylePrefs.Count;
            UpdateListSize();
        }

        /// <summary>
        /// Показать текст
        /// </summary>
        public void ShowSampleText()
        {
            _stylePrefs[_selOption].DisplayText = !_stylePrefs[_selOption].DisplayText;
        }

        /// <summary>
        /// Скопировать в буфер
        /// </summary>
        public void CopyItem()
        {
            GUIStyle style = _editorStyles[_selOption];
            EditorGUIUtility.systemCopyBuffer = '"' + style.name + '"';
            _lastCopy = '"' + style.name + '"';
            Repaint();
        }

        /// <summary>
        /// Установить статус недоступности визуального стиля
        /// </summary>
        public void ToggleDisabled()
        {
            _stylePrefs[_selOption].Enabled = !_stylePrefs[_selOption].Enabled;
            Repaint();
        }

        /// <summary>
        /// Обновление списка
        /// </summary>
        public void UpdateListSize()
        {
            var lastY = 0;
            var newY = 0;
            var count = 0;
            for (var i = 0; i < _initialCount; i++)
            {
                GUIStyle style = _editorStyles[i];

                if (CanShow(style.name))
                {
                    _stylePrefs[i] = new StylePref();
                    var height = 16;
                    var width = 16;
                    newY = lastY + (5 * count);
                    if (style.active.background != null)
                    {
                        height = style.active.background.height;
                        width = style.active.background.width;
                    }

                    if (style.hover.background != null)
                    {
                        height = style.hover.background.height;
                        width = style.hover.background.width;
                    }

                    if (style.normal.background != null)
                    {
                        height = style.normal.background.height;
                        width = style.normal.background.width;
                    }

                    if (height < 8 || width < 8)
                    {
                        height = 8;
                        width = 8;
                    }

                    _totalSizeOfStyles += height;
                    _stylePrefs[i].Height = height;
                    _stylePrefs[i].Width = width;
                    lastY = newY - (5 * count) + (_stylePrefs[i].Height + 54);
                    count++;
                }
            }
            _totalSizeOfView = lastY + ((count - 1) * 5);
            _countSize = count;
        }

        /// <summary>
        /// Проверка на видимость объект по вхождению строки поиска
        /// </summary>
        /// <param name="searchString">Строка поиска</param>
        /// <returns>Статус видимости объекта</returns>
        private bool CanShow(string searchString)
        {
            var canShow = false;

            switch (_selSearchParam)
            {
                case 0:
                    if (searchString.ToLower().StartsWith(_searchText.ToLower()))
                        canShow = true;
                    break;

                case 1:
                    if (searchString.ToLower().EndsWith(_searchText.ToLower()))
                        canShow = true;
                    break;

                case 2:
                    if (searchString.ToLower().Contains(_searchText.ToLower()))
                        canShow = true;
                    break;
            }

            return canShow;
        }
        #endregion
    }
}
#endif
