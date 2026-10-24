#if UNITY_EDITOR
using System;
using System.IO;

using Lotus.Core;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор основного сервиса для сериализации данных в редакторе Unity
    /// </summary>
    public class LotusSerializationServiceWindow : EditorWindow
    {
        #region Static methods
        /// <summary>
        /// Показать окно сервиса сериализации
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathSerialization + "Service", false, XCoreSettingsEditor.MenuOrderSerialization + 1)]
        public static void ShowServiceWindow()
        {
            var window = GetWindow<LotusSerializationServiceWindow>();
            window.titleContent.text = "Serialization Service";
            window.Show();
        }

        /// <summary>
        /// Сохранение выбранного игрового объекта в файл
        /// </summary>
        [Obsolete("XSerializationDispatcherUnity удалён — SaveToXml не подключён. Используйте XSerializationDispatcher")]
        [MenuItem(XEditorSettings.MenuPlace + "Save/SaveSelectedGameObject", false, XEditorSettings.MenuOrderLast + 2)]
        public static void SaveSelectedGameObject()
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }

            if (!Directory.Exists(XEditorSettings.AutoSavePath))
            {
                Directory.CreateDirectory(XEditorSettings.AutoSavePath);
            }

            var fileName = Selection.activeGameObject.name + "_" + XDateTime.GetStrDateTimeShort();
            _ = Path.Combine(XEditorSettings.AutoSavePath, fileName);
        }

        /// <summary>
        /// Сохранение всей сцены в файл
        /// </summary>
        [Obsolete("XSerializationDispatcherUnity удалён — SaveToXml не подключён. Используйте XSerializationDispatcher")]
        [MenuItem(XEditorSettings.MenuPlace + "Save/SaveScene", false, XEditorSettings.MenuOrderLast + 3)]
        public static void SaveScene()
        {
            if (!Directory.Exists(XEditorSettings.AutoSavePath))
            {
                Directory.CreateDirectory(XEditorSettings.AutoSavePath);
            }

            var currentScene = SceneManager.GetActiveScene();
            var fileName = (currentScene.name.IsExists() ? currentScene.name : "Noname") + "_" +
                XDateTime.GetStrDateTimeShort();
            _ = Path.Combine(XEditorSettings.AutoSavePath, fileName);
        }

        /// <summary>
        /// Загрузка игровых объектов из файла
        /// </summary>
        [Obsolete("XSerializationDispatcherUnity удалён — LoadFromXml не подключён. Используйте XSerializationDispatcher")]
        [MenuItem(XEditorSettings.MenuPlace + "Load/LoadFromFile", false, XEditorSettings.MenuOrderLast + 4)]
        public static void LoadFromFile()
        {
        }
        #endregion

        #region Fields
        private readonly string _descriptionService = "Основной сервис для сериализации данных";
        private Vector2 _scrollPos;
        private string _fileNameDesignXml;
        private TextAsset _fileAssetDesignXml;
        private TSerializationEditorPolicy _editorPolicy;
        private TSerializationEditorVolume _editorVolume;
        private bool _isSerializableChild;

        [Obsolete("Не используется в текущем UI окна")]
        private string _fileNameGameXml;

        [Obsolete("Не используется в текущем UI окна")]
        private TextAsset _fileAssetGameXml;

        [Obsolete("Не используется в текущем UI окна")]
        private bool _expandedEditorSave;

        [Obsolete("Не используется в текущем UI окна")]
        private bool _expandedGameSave;
        #endregion

        #region Unity events
        /// <summary>
        /// Рисование UnityGUI
        /// </summary>
        public void OnGUI()
        {
            GUILayout.Space(4.0f);
            EditorGUILayout.HelpBox(_descriptionService, MessageType.Info);

            GUILayout.Space(4.0f);
            _editorPolicy = XEditorInspector.PropertyEnum("Policy", _editorPolicy);

            GUILayout.Space(4.0f);
            _editorVolume = XEditorInspector.PropertyEnum("Volume", _editorVolume);

            if (_editorVolume == TSerializationEditorVolume.Game)
            {
                GUILayout.Space(2.0f);
                XEditorInspector.DrawGroup("Not supported in this release", XEditorStyles.ColorRed, TextAnchor.MiddleCenter);
            }

            if (_editorVolume == TSerializationEditorVolume.Selected)
            {
                GUILayout.Space(2.0f);
                _isSerializableChild = XEditorInspector.PropertyBoolean("IsSerializableChild", _isSerializableChild);

                GUILayout.Space(2.0f);
                GUILayout.Label("Selected Object:", EditorStyles.boldLabel);

                var height = Selection.gameObjects.Length * XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                if (height < XInspectorViewParams.CONTROL_HEIGHT_SPACE)
                {
                    height = XInspectorViewParams.CONTROL_HEIGHT_SPACE * 2;
                }
                if (height > XInspectorViewParams.CONTROL_HEIGHT_SPACE * 4)
                {
                    height = XInspectorViewParams.CONTROL_HEIGHT_SPACE * 4;
                }

                _scrollPos = GUILayout.BeginScrollView(_scrollPos, GUILayout.Height(height));
                {
                    for (var i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        GUILayout.Label(Selection.gameObjects[i].name, EditorStyles.whiteLabel);
                    }
                }
                GUILayout.EndScrollView();
            }

            GUILayout.Space(4.0f);
            EditorGUILayout.BeginHorizontal();
            {
                _fileNameDesignXml = XEditorInspector.PropertyString("Name XML", _fileNameDesignXml);
                if (GUILayout.Button("Save"))
                {
                    Debug.LogWarning(
                        "SaveToXml недоступен: XSerializationDispatcherUnity удалён. Используйте XSerializationDispatcher.");
                }
                if (GUILayout.Button("Auto Save"))
                {
#pragma warning disable CS0618
                    SaveScene();
#pragma warning restore CS0618
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(4.0f);
            EditorGUILayout.BeginHorizontal();
            {
                _fileAssetDesignXml = XEditorInspector.PropertyResource("Asset XML", _fileAssetDesignXml);
                if (GUILayout.Button("Load"))
                {
                    Debug.LogWarning(
                        "LoadFromXml недоступен: XSerializationDispatcherUnity удалён. Используйте XSerializationDispatcher.");
                }
                if (GUILayout.Button("Auto Load", XEditorStyles.BUTTON_DROP_DOWN_1))
                {
                    if (!Directory.Exists(XEditorSettings.AutoSavePath))
                    {
                        Directory.CreateDirectory(XEditorSettings.AutoSavePath);
                    }

                    var files = Directory.GetFiles(XEditorSettings.AutoSavePath);
                    var menu = new GenericMenu();
                    for (var i = 0; i < files.Length; i++)
                    {
                        var filePath = files[i];
                        menu.AddItem(new GUIContent(Path.GetFileNameWithoutExtension(filePath)), false, (object selectedFile) =>
                        {
                            Debug.LogWarning(
                                "LoadFromXml недоступен: XSerializationDispatcherUnity удалён. File: " + selectedFile);
                        }, filePath);
                    }

                    menu.ShowAsContext();
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.0f);
        }
        #endregion
    }
    /**@}*/
}
#endif
