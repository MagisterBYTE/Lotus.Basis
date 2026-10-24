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
    /// Редактор сервиса для хранения глобальных системных и пользовательских настроек системы Lotus
    /// </summary>
    [CustomEditor(typeof(LotusSystemSettingsService))]
    public class LotusSystemSettingsServiceEditor : UnityEditor.Editor
    {
        #region Constants
        /// <summary>
        /// Описание сервиса
        /// </summary>
        public const string DESCRIPTION_SERVICE =
            "Сервис для хранения глобальных системных и пользовательских настроек системы Lotus";

        /// <summary>
        /// Путь файла сервиса
        /// </summary>
        public const string SYSTEM_SETTINGS_SERVICE_PATH = XEditorSettings.ResourcesDataPath;
        #endregion

        #region Fields
        /// <summary>
        /// Контент кнопки удаления скина
        /// </summary>
        [Obsolete("UI управления скинами удалён из инспектора System Settings Service")]
        protected static readonly GUIContent ContentDeleteSkin = new GUIContent("X", "Delete reference to skin");

        /// <summary>
        /// Контент кнопки установки скина
        /// </summary>
        [Obsolete("UI управления скинами удалён из инспектора System Settings Service")]
        protected static readonly GUIContent ContentSetSkin = new GUIContent("S", "Set skin to current");

        /// <summary>
        /// Контент кнопки добавления скина
        /// </summary>
        [Obsolete("UI управления скинами удалён из инспектора System Settings Service")]
        protected static readonly GUIContent ContentAddSkin = new GUIContent("Add", "Add reference to skin");

        /// <summary>
        /// Контент кнопки поиска ресурсов
        /// </summary>
        [Obsolete("UI поиска ресурсов удалён из инспектора System Settings Service")]
        protected static readonly GUIContent ContentFind = new GUIContent("Find resources", "Find and load resources");

        private LotusSystemSettingsService _systemSettingsService;
        private string _designScreenSizeText;
        #endregion

        #region Static methods
        /// <summary>
        /// Создание сервиса для хранения глобальных системных и пользовательских настроек
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPath + "Create System Settings Service", false,
            XCoreSettingsEditor.MenuOrderLast + 20)]
        public static void Create()
        {
            // Создаем сервис
            var instance = ScriptableObject.CreateInstance<LotusSystemSettingsService>();
            instance.Create();

            // Создаем ресурс для хранения сервиса
            var path = SYSTEM_SETTINGS_SERVICE_PATH + LotusSystemSettingsService.SYSTEM_SETTINGS_SERVICE_NAME + ".asset";
            if (!Directory.Exists(SYSTEM_SETTINGS_SERVICE_PATH))
            {
                Directory.CreateDirectory(SYSTEM_SETTINGS_SERVICE_PATH);
            }
            AssetDatabase.CreateAsset(instance, path);

            // Обновляем в редакторе
            AssetDatabase.Refresh(ImportAssetOptions.Default);
            EditorUtility.DisplayDialog("System settings service successfully created", "Path\n" + path, "OK");
        }
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _systemSettingsService = target as LotusSystemSettingsService;
        }

        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            GUILayout.Space(4.0f);
            EditorGUILayout.HelpBox(DESCRIPTION_SERVICE, MessageType.Info);

            // Параметры разработки
            DrawDesign();

            GUILayout.Space(2.0f);
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование параметров разработки
        /// </summary>
        public void DrawDesign()
        {
            XEditorInspector.DrawCaption("Параметры разработки");

            EditorGUI.BeginChangeCheck();

            GUILayout.Space(2.0f);
            _systemSettingsService.DesignWidth = XEditorInspector.PropertyInt(
                "Design Width", _systemSettingsService.DesignWidth);

            GUILayout.Space(2.0f);
            _systemSettingsService.DesignHeight = XEditorInspector.PropertyInt(
                "Design Height", _systemSettingsService.DesignHeight);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("H:1600х1000"))
                {
                    _systemSettingsService.DesignWidth = 1600;
                    _systemSettingsService.DesignHeight = 1000;
                    _designScreenSizeText = "H:1600х1000";
                }

                if (GUILayout.Button("H:1600х900"))
                {
                    _systemSettingsService.DesignWidth = 1600;
                    _systemSettingsService.DesignHeight = 900;
                    _designScreenSizeText = "H:1600х900";
                }

                if (GUILayout.Button("V:1000х1600"))
                {
                    _systemSettingsService.DesignWidth = 1000;
                    _systemSettingsService.DesignHeight = 1600;
                    _designScreenSizeText = "V:1000х1600";
                }

                if (GUILayout.Button("V:900х1600"))
                {
                    _systemSettingsService.DesignWidth = 900;
                    _systemSettingsService.DesignHeight = 1600;
                    _designScreenSizeText = "V:900х1600";
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Apply to game screen"))
                {
                    XEditorGameView.ApplyCustomSize(TGameViewSizeType.AspectRatio,
                        EditorUserBuildSettings.activeBuildTarget,
                        _systemSettingsService.DesignWidth, _systemSettingsService.DesignHeight,
                        _designScreenSizeText);
                }
            }
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
