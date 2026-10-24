#if UNITY_EDITOR
using System;
using System.IO;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор центрального диспетчера для локализации ресурсов
    /// </summary>
    [CustomEditor(typeof(LotusLocalizationDispatcher))]
    public class LotusLocalizationDispatcherEditor : UnityEditor.Editor
    {
        #region Constants
        /// <summary>
        /// Контент кнопки удаления
        /// </summary>
        private static readonly GUIContent ContentRemove = new GUIContent("X");

        /// <summary>
        /// Контент кнопки добавления
        /// </summary>
        private static readonly GUIContent ContentAdd = new GUIContent("+");
        #endregion

        #region Static methods
        /// <summary>
        /// Создание основного сервиса для локализации текстовых данных
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathLocalization + "Create Localization Dispatcher", false,
            XCoreSettingsEditor.MenuOrderLocalization)]
        public static void Create()
        {
            _ = LotusLocalizationDispatcher.Instance;
        }
        #endregion

        #region Fields
        // Основные параметры
        private LotusLocalizationDispatcher _dispatcher;
        private int _selectedLangName;
        private string _pathFileName = "Assets";

        [Obsolete("Используется только в отключённой секции DrawTranslation")]
        private Vector2 _scrollAll;

#pragma warning disable CS0618
        [Obsolete("Yandex Translate API v1.5 отключён. Используйте новый провайдер перевода вместо CTranslatorYandex")]
        private CTranslatorYandex _translator;
#pragma warning restore CS0618
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _dispatcher = target as LotusLocalizationDispatcher;
#pragma warning disable CS0618
            _translator = new CTranslatorYandex();
#pragma warning restore CS0618
        }

        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup("Singleton settings");
                {
                    GUILayout.Space(2.0f);
                    _dispatcher.IsMainInstance = XEditorInspector.PropertyBoolean(
                        nameof(_dispatcher.IsMainInstance), _dispatcher.IsMainInstance);

                    GUILayout.Space(2.0f);
                    EditorGUI.BeginDisabledGroup(_dispatcher.IsMainInstance);
                    {
                        _dispatcher.DestroyMode = XEditorInspector.PropertyEnum(
                            nameof(_dispatcher.DestroyMode), _dispatcher.DestroyMode);
                    }
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Space(2.0f);
                    _dispatcher.IsDontDestroy = XEditorInspector.PropertyBoolean(
                        nameof(_dispatcher.IsDontDestroy), _dispatcher.IsDontDestroy);
                }

                // Рисование языков
                DrawLanguages();

                // Рисование тестирования переводчика
                DrawTranslator();
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }

            GUILayout.Space(2.0f);
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Перевод всех данных
        /// </summary>
        [Obsolete("Зависит от устаревшего CTranslatorYandex — Yandex Translate API v1.5 отключён")]
        private void TranslateAllData()
        {
#pragma warning disable CS0618
            _translator.AbbrsFrom = _dispatcher.DefaultLanguage.Abbreviation;
            _translator.AbbrsTo = CLanguageInfo.LanguageAbbrs[_selectedLangName];

            for (var j = 0; j < _dispatcher._listTranslate.Count; j++)
            {
                _translator.OriginalText = _dispatcher._listTranslate[j].Default;

                _translator.Translate();

                _dispatcher._listTranslate[j].Translate = _translator.TranslateText;

                EditorUtility.DisplayCancelableProgressBar("Translate", _translator.OriginalText,
                    (j + 1) / (float)_dispatcher._listTranslate.Count);
            }
#pragma warning restore CS0618

            EditorUtility.ClearProgressBar();
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование языков
        /// </summary>
        public void DrawLanguages()
        {
            GUILayout.Space(4.0f);
            _dispatcher._expandedLanguages = XEditorInspector.DrawSectionFoldout("Languages",
                _dispatcher._expandedLanguages);
            if (_dispatcher._expandedLanguages)
            {
                // Перебираем список
                XEditorInspector.DrawGroup("Items:");

                for (var i = 0; i < _dispatcher.Languages.Count; i++)
                {
                    GUILayout.Space(4.0f);
                    var lang = _dispatcher.Languages[i];
                    {
                        // Заголовок
                        if (i == 0)
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField("Name");
                                EditorGUILayout.LabelField("File");
                            }
                            EditorGUILayout.EndHorizontal();
                            GUILayout.Space(4.0f);
                        }

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.BeginHorizontal();
                        {
                            lang.Name = XEditorInspector.SelectorValue(lang.Name, CLanguageInfo.LanguageNames);
                            lang.FileData = (TextAsset)EditorGUILayout.ObjectField(lang.FileData, typeof(TextAsset), false);

                            if (GUILayout.Button(ContentRemove, XEditorStyles.ButtonMiniDefaultRedRightStyle))
                            {
                                if (i > 0)
                                {
                                    _dispatcher.Languages.RemoveAt(i);
                                    EditorGUILayout.EndHorizontal();

                                    _dispatcher.SaveInEditor();
                                    break;
                                }

                                _dispatcher.DefaultLanguage.Name = "";
                                _dispatcher.DefaultLanguage.FileData = null;
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        if (EditorGUI.EndChangeCheck())
                        {
                            _dispatcher.SaveInEditor();
                        }
                    }
                }

                GUILayout.Space(2.0f);
                if (GUILayout.Button(ContentAdd, XEditorStyles.ButtonMiniHeaderMiddleStyle,
                    GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                {
                    _dispatcher.AddLang("Default");
                }

                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup("Operations:");

                // Генерация основного файла
                GUILayout.Space(4.0f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Generate base"))
                    {
                        // Путь к сохранению
                        _pathFileName = EditorUtility.OpenFolderPanel("Set path localization data", _pathFileName, "Lang");
                        var langName = CLanguageInfo.LanguageNames[_selectedLangName];
                        var path = Path.Combine(_pathFileName, langName) + ".bytes";

                        // Корректируем путь
                        var assetsIndex = path.IndexOf("Assets", StringComparison.Ordinal);
                        if (assetsIndex >= 0)
                        {
                            path = path.Remove(0, assetsIndex);
                            path = path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                            // Создаем файл
                            _dispatcher.CreateLocalizeFileDefault(path);

                            // Делаем его по умолчанию
                            _dispatcher.DefaultLanguage.Name = langName;
                            _dispatcher.DefaultLanguage.Abbreviation = CLanguageInfo.LanguageAbbrs[_selectedLangName];
                            _dispatcher.DefaultLanguage.FileData = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                        }
                    }

                    _selectedLangName = XEditorInspector.SelectorIndex(_selectedLangName, CLanguageInfo.LanguageNames);
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(2.0f);
                if (GUILayout.Button("Clear"))
                {
                    _dispatcher.ClearLangs();
                }
            }
        }

        /// <summary>
        /// Рисование всех текстовых данных и автоматический перевод
        /// </summary>
        [Obsolete("Секция Translation отключена — UI не реализован. Ранее использовал CTranslatorYandex")]
        public void DrawTranslation()
        {
        }

        /// <summary>
        /// Рисование тестирования переводчика
        /// </summary>
        public void DrawTranslator()
        {
            GUILayout.Space(4.0f);
            _dispatcher._expandedTestTranslator = XEditorInspector.DrawSectionFoldout("Test Translator",
                _dispatcher._expandedTestTranslator);
            if (_dispatcher._expandedTestTranslator)
            {
                EditorGUILayout.HelpBox(
                    "CTranslatorYandex устарел: Yandex Translate API v1.5 отключён. Требуется новый провайдер перевода.",
                    MessageType.Warning);

#pragma warning disable CS0618
                GUILayout.Space(2.0f);
                _translator.AbbrsFrom = XEditorInspector.PropertyString("Abbrs From", _translator.AbbrsFrom);

                GUILayout.Space(2.0f);
                _translator.AbbrsTo = XEditorInspector.PropertyString("Abbrs To", _translator.AbbrsTo);

                GUILayout.Space(2.0f);
                _translator.OriginalText = XEditorInspector.PropertyString("OriginalText", _translator.OriginalText);

                GUILayout.Space(2.0f);
                _translator.TranslateText = XEditorInspector.PropertyString("TranslateText", _translator.TranslateText);

                GUILayout.Space(2.0f);
                if (GUILayout.Button("Translate"))
                {
                    _translator.Translate();
                }
#pragma warning restore CS0618
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
