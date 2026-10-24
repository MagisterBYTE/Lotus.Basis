#if UNITY_EDITOR
using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор центрального диспетчера для хранения и управления ресурсами анимации
    /// </summary>
    [CustomEditor(typeof(LotusTweenDispatcher))]
    public class LotusTweenDispatcherEditor : UnityEditor.Editor
    {
        #region Static methods
        /// <summary>
        /// Создание основного сервиса для хранения и управления ресурсами анимации
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathTween + "Create Tween Dispatcher", false, XCoreSettingsEditor.MenuOrderTween)]
        public static void Create()
        {
            _ = LotusTweenDispatcher.Instance;
        }
        #endregion

        #region Fields
        private LotusTweenDispatcher _dispatcher;
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _dispatcher = target as LotusTweenDispatcher;
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

                XEditorInspector.DrawGroup("Tween settings");
                {
                    GUILayout.Space(2.0f);
                    _dispatcher._curveStorage = XEditorInspector.PropertyResource(
                        nameof(LotusTweenDispatcher.CurveStorage), _dispatcher._curveStorage);

                    GUILayout.Space(2.0f);
                    _dispatcher._spriteStorage = XEditorInspector.PropertyResource(
                        nameof(LotusTweenDispatcher.SpriteStorage), _dispatcher._spriteStorage);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                _dispatcher.SaveInEditor();
            }

            GUILayout.Space(2.0f);
        }
        #endregion
    }
    /**@}*/
}
#endif
