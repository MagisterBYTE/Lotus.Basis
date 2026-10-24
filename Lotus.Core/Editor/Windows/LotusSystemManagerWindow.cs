#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lotus.Editor
{
    /// <summary>
    /// Редактор для управления основными системными параметрами и настройки системы Lotus
    /// </summary>
    public class LotusSystemManagerWindow : LotusUtilityBaseWindow
    {
        #region Static methods
        /// <summary>
        /// Показать окно редактора для управления основными системными параметрами и настройки системы Lotus
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathUtility + "System Parameters", false, XCoreSettingsEditor.MenuOrderUtility + 5)]
        public static void ShowSystemManager()
        {
            var window = GetWindow<LotusSystemManagerWindow>();
            window.titleContent.text = "System Manager";

            window.OnConstruct();
            window.Show();
        }

        /// <summary>
        /// Удалить потерянные компоненты с выбранного игрового объекта
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPath + "Remove Missing GameObject", false, XCoreSettingsEditor.MenuOrderUtility + 1000)]
        private static void FindAndRemoveMissingInSelected()
        {
            var deepSelection = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);
            var compCount = 0;
            var goCount = 0;
            foreach (var o in deepSelection)
            {
                if (o is GameObject go)
                {
                    var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                    if (count > 0)
                    {
                        // Edit: use undo record object, since undo destroy wont work with missing
                        Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                        compCount += count;
                        goCount++;
                    }
                }
            }
            Debug.Log($"Found and removed {compCount} missing scripts from {goCount} GameObjects");
        }

        /// <summary>
        /// Удалить потерянные компоненты со всех объектов сцены
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPath + "Remove Missing All", false, XCoreSettingsEditor.MenuOrderUtility + 1001)]
        private static void FindAndRemoveMissingInAll()
        {
            var deepSelection = EditorUtility.CollectDeepHierarchy(SceneManager.GetActiveScene().GetRootGameObjects());
            var compCount = 0;
            var goCount = 0;
            foreach (var o in deepSelection)
            {
                if (o is GameObject go)
                {
                    var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                    if (count > 0)
                    {
                        // Edit: use undo record object, since undo destroy wont work with missing
                        Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                        compCount += count;
                        goCount++;
                    }
                }
            }

            Debug.Log($"Found and removed {compCount} missing scripts from {goCount} GameObjects");
        }
        #endregion

        #region Fields
        // Установка порядка исполнения скриптов
        private int _executionOrderStart;
        private string _executionOrderInfo;
        #endregion

        #region Unity events
        /// <summary>
        /// Активация окна
        /// </summary>
        public void OnEnable()
        {
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Конструирование окна
        /// </summary>
        public void OnConstruct()
        {
            LoadTabAndDesc();
            IsExplore = false;
        }

        /// <summary>
        /// Установка правильного порядка исполнения скриптов
        /// </summary>
        public void SetExecutionOrder()
        {
            // Iterate through all scripts (Might be a better way to do this?)
            foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
            {
                Type type = monoScript.GetClass();
                if (type != null)
                {
                    LotusExecutionOrderAttribute attribute = type.GetAttribute<LotusExecutionOrderAttribute>();
                    if (attribute != null)
                    {
                        var executionOrder = attribute.ExecutionOrder;
                        MonoImporter.SetExecutionOrder(monoScript, executionOrder + _executionOrderStart);
                    }
                }
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование основной панели
        /// </summary>
        public override void DrawContent()
        {
            switch (_selectedTab)
            {
                case 0:
                    {
                        DrawExecutionOrder();
                    }
                    break;
                case 1:
                    {
                        DrawSettings();
                    }
                    break;
                case 2:
                    {
                        DrawExport();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Рисование параметров порядка исполнения скриптов
        /// </summary>
        public void DrawExecutionOrder()
        {
            GUILayout.Space(4);
            _executionOrderStart = XEditorInspector.PropertyInt("Start Offset", _executionOrderStart);

            GUILayout.Space(4);
            if (GUILayout.Button("SetExecutionOrder"))
            {
                SetExecutionOrder();
            }

            _executionOrderInfo = "Скрипты системы Lotus исполняются в порядке от: " + _executionOrderStart.ToString() +
                " до " + (5000 + _executionOrderStart).ToString();

            GUILayout.Space(4);
            GUILayout.Label(_executionOrderInfo, EditorStyles.boldLabel);
        }

        /// <summary>
        /// Рисование настроек
        /// </summary>
        public void DrawSettings()
        {
            GUILayout.Space(4);
            GUILayout.Label("Все настройки Lotus расположены в файле LotusUnityEditorSettings", EditorStyles.boldLabel);
        }

        /// <summary>
        /// Рисование параметров экспорта
        /// </summary>
        /// <remarks>
        /// Код экспорта пакетов отключён — ранее использовал модульные X*EditorSettings.SourcePath
        /// </remarks>
        public void DrawExport()
        {
        }
        #endregion
    }
}
#endif
