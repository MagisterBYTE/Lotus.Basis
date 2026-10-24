#if UNITY_EDITOR
using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор основного системного диспетчера Lotus
    /// </summary>
    [CustomEditor(typeof(LotusSystemDispatcher))]
    public class LotusSystemDispatcherEditor : UnityEditor.Editor
    {
        #region Static methods
        /// <summary>
        /// Добавление системного диспетчера в сцену
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPath + "Create SystemDispatcher", false, XCoreSettingsEditor.MenuOrderLast + 1)]
        public static void CreateSystemDispatcher()
        {
            var dispatcher = LotusSystemDispatcher.Instance;
            dispatcher.gameObject.layer = XLayer.DefaultId;
        }
        #endregion

        #region Fields
        private LotusSystemDispatcher _dispatcher;
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _dispatcher = target as LotusSystemDispatcher;
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

                GUILayout.Space(4.0f);
                XEditorInspector.DrawGroup("Settings 2D Core");
                {
                    GUILayout.Space(2.0f);
                    _dispatcher.UseIMGUI = XEditorInspector.PropertyBoolean(
                        nameof(_dispatcher.UseIMGUI), _dispatcher.UseIMGUI);

                    if (_dispatcher.UseIMGUI)
                    {
                        GUILayout.Space(2.0f);
                        _dispatcher.CurrentSkin = XEditorInspector.PropertyResource(
                            "CurrentSkin", _dispatcher.CurrentSkin);
                    }
                }

                XEditorInspector.DrawGroup("Parameters console");
                {
                    GUILayout.Space(2.0f);
                    _dispatcher.ConsoleVisible = XEditorInspector.PropertyBoolean(
                        nameof(_dispatcher.ConsoleVisible), _dispatcher.ConsoleVisible);

                    GUILayout.Space(2.0f);
                    _dispatcher.ConsoleHeight = XEditorInspector.PropertyFloat(
                        nameof(_dispatcher.ConsoleHeight), _dispatcher.ConsoleHeight);
                }

                XEditorInspector.DrawGroup("Parameters fps");
                {
                    GUILayout.Space(2.0f);
                    _dispatcher.IsFpsShow = XEditorInspector.PropertyBoolean(
                        nameof(_dispatcher.IsFpsShow), _dispatcher.IsFpsShow);

                    GUILayout.Space(2.0f);
                    _dispatcher.FpsUpdateInterval = XEditorInspector.PropertyFloatSlider(
                        nameof(_dispatcher.FpsUpdateInterval), _dispatcher.FpsUpdateInterval, 0.1f, 2.0f);

                    GUILayout.Space(2.0f);
                    _dispatcher.FpsPosition = XEditorInspector.PropertyRect(
                        nameof(_dispatcher.FpsPosition), _dispatcher.FpsPosition);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.Save();
            }

            GUILayout.Space(2.0f);
        }
        #endregion
    }
    /**@}*/
}
#endif
