#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;

using Lotus.Core;

using UnityEditor;

using UnityEngine;

namespace Lotus.Editor
{
    /// <summary>
    /// Базовая панель(окно) утилиты для представления панели редактирования платформы Lotus
    /// </summary>
    /// <remarks>
    /// Используется в качестве базовой панели(окна) для остальных панелей(окон).
    /// Поддерживает различные режимы и структуру отображения.
    /// Автоматически может загружать описание структурной части панели из текстового файла
    /// </remarks>
    public class LotusUtilityBaseWindow : EditorWindow
    {
        #region Static methods
        /// <summary>
        /// Показать окно базового редактора Lotus
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathUtility + "Base Window Utility", false, XCoreSettingsEditor.MenuOrderUtility + 1)]
        public static void ShowUtilityBase()
        {
            var window = GetWindow<LotusUtilityBaseWindow>();
            window.titleContent.text = "Base Window Utility";
            window.Show();
        }
        #endregion

        #region Fields
        // Служебные данные для рисования
        protected LotusEditorSplitView _splitViewMain = new LotusEditorSplitView(true, 0.25f);
        protected LotusEditorSplitView _splitViewExplore = new LotusEditorSplitView(false, 0.8f);
        protected LotusEditorSplitView _splitViewContent = new LotusEditorSplitView(false, 0.9f);

        // Статус отображения отдельных панелей
        protected bool _isToolbar = true;
        protected bool _isOutput = true;
        protected bool _isExplore = true;
        protected bool _isManager = true;

        // Выбранная вкладка
        protected int _selectedTab;
        protected string[] _tabs = new string[1];
        protected string[] _desc = new string[1];
        #endregion

        #region Properties
        /// <summary>
        /// Статус отображения панели инструментов
        /// </summary>
        public bool IsToolbar
        {
            get { return _isToolbar; }
            set { _isToolbar = value; }
        }

        /// <summary>
        /// Статус отображения панели вывода
        /// </summary>
        public bool IsOutput
        {
            get { return _isOutput; }
            set { _isOutput = value; }
        }

        /// <summary>
        /// Статус отображения панели обозревателя
        /// </summary>
        public bool IsExplore
        {
            get { return _isExplore; }
            set { _isExplore = value; }
        }

        /// <summary>
        /// Статус отображения панели управления
        /// </summary>
        public bool IsManager
        {
            get { return _isManager; }
            set { _isManager = value; }
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Рисование UnityGUI
        /// </summary>
        public virtual void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
                {
                    if (_isToolbar)
                    {
                        if (_tabs != null && _tabs.Length > 1)
                        {
                            GUILayout.Space(2.0f);
                            _selectedTab = GUILayout.Toolbar(_selectedTab, _tabs, GUILayout.ExpandWidth(true),
                                GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
                        }
                    }

                    GUILayout.FlexibleSpace();

                    GUILayout.Space(2.0f);
                    _isToolbar = GUILayout.Toggle(_isToolbar, "Toolbar", EditorStyles.miniButtonLeft,
                        GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

                    _isOutput = GUILayout.Toggle(_isOutput, "Output", EditorStyles.miniButtonMid,
                        GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

                    _isExplore = GUILayout.Toggle(_isExplore, "Explore", EditorStyles.miniButtonMid,
                        GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));

                    _isManager = GUILayout.Toggle(_isManager, "Manager", EditorStyles.miniButtonRight,
                        GUILayout.Height(XInspectorViewParams.HEADER_HEIGHT));
                }
                EditorGUILayout.EndHorizontal();

                if (_isExplore)
                {
                    _splitViewMain.BeginSplitView();
                    {
                        if (_isManager)
                        {
                            _splitViewExplore.BeginSplitView();
                            {
                                DrawExplore();
                                _splitViewExplore.Split(0, 0);
                                DrawManager();
                            }
                            _splitViewExplore.EndSplitView();
                        }
                        else
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(4.0f);
                            EditorGUILayout.BeginVertical();
                            DrawExplore();
                            EditorGUILayout.EndVertical();
                            GUILayout.Space(6.0f);
                            EditorGUILayout.EndHorizontal();
                        }

                        _splitViewMain.Split(-2, 0);
                        if (_isOutput)
                        {
                            _splitViewContent.BeginSplitView();
                            {
                                DrawContent();
                                _splitViewContent.Split(0, 22);
                                DrawOutput();
                            }
                            _splitViewContent.EndSplitView();
                        }
                        else
                        {
                            GUILayout.BeginVertical();
                            DrawContent();
                            GUILayout.EndVertical();
                        }
                    }
                    _splitViewMain.EndSplitView();
                }
                else
                {
                    if (_isOutput)
                    {
                        _splitViewContent.BeginSplitView();
                        {
                            DrawContent();
                            _splitViewContent.Split(0, 22);
                            DrawOutput();
                        }
                        _splitViewContent.EndSplitView();
                    }
                    else
                    {
                        DrawContent();
                    }
                }
            }
            EditorGUILayout.EndVertical();
            Repaint();

            BeginWindows();
            {
                DrawWindows();
            }
            EndWindows();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Добавление вкладок
        /// </summary>
        /// <param name="tabs">Набор вкладок</param>
        public void AddTabs(params string[] tabs)
        {
            if (tabs != null && tabs.Length > 0)
            {
                _tabs = new string[tabs.Length];
                for (var i = 0; i < tabs.Length; i++)
                {
                    _tabs[i] = tabs[i];
                }
            }
        }

        /// <summary>
        /// Загрузка информации по вкладкам
        /// </summary>
        public void LoadTabAndDesc()
        {
            var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
            path = path.RemoveExtension();
            path += "Desc.txt";
            List<KeyValuePair<string, string>> data = XStringHelper.ConvertLinesToGroupLines(File.ReadAllLines(path), XStringHelper.SeparatorFileData);

            if (data != null && data.Count > 0)
            {
                _tabs = new string[data.Count];
                _desc = new string[data.Count];
                for (var i = 0; i < data.Count; i++)
                {
                    _tabs[i] = data[i].Key.Trim('#');
                    _desc[i] = data[i].Value;
                }
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование основной панели
        /// </summary>
        public virtual void DrawContent()
        {
            GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        }

        /// <summary>
        /// Рисование панели обозревателя
        /// </summary>
        public virtual void DrawExplore()
        {
            GUILayout.Box("RectExplore", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        }

        /// <summary>
        /// Рисование панели управления
        /// </summary>
        public virtual void DrawManager()
        {
            GUILayout.Box("RectManager", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        }

        /// <summary>
        /// Рисование панели вывода информации
        /// </summary>
        public virtual void DrawOutput()
        {
            if (_desc.Length > 1 && _selectedTab != -1 && _selectedTab < _desc.Length)
            {
                if (_desc[_selectedTab].IsExists())
                {
                    var fontSize = EditorStyles.helpBox.fontSize;
                    EditorStyles.helpBox.fontSize = 12;
                    EditorGUILayout.HelpBox(_desc[_selectedTab], MessageType.Info);
                    EditorStyles.helpBox.fontSize = fontSize;
                }
            }
            else
            {
                GUILayout.Box("RectOutput", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            }
        }

        /// <summary>
        /// Рисование окон
        /// </summary>
        public virtual void DrawWindows()
        {
        }
        #endregion
    }
}
#endif
