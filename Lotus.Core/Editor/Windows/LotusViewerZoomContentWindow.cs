#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /// <summary>
    /// Редактор для просмотра, масштабирования и панорамирование внутреннего контента
    /// </summary>
    public class LotusViewerZoomContentWindow : LotusUtilityBaseWindow
    {
        #region Static methods
        /// <summary>
        /// Показать окно для просмотра, масштабирования и панорамирование внутреннего контента
        /// </summary>
        [MenuItem(XCoreSettingsEditor.MenuPathUtility + "Viewer Zoom Content", false, XCoreSettingsEditor.MenuOrderUtility + 15)]
        public static void ShowViewerZoomContent()
        {
            var window = GetWindow<LotusViewerZoomContentWindow>();
            window.titleContent.text = "Viewer Zoom Content";

            window.OnConstruct();
            window.Show();
        }
        #endregion

        #region Fields
        // Основные данные
        #endregion

        #region Main methods
        /// <summary>
        /// Конструирование окна
        /// </summary>
        public void OnConstruct()
        {
            _isManager = false;
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование основной панели
        /// </summary>
        public override void DrawContent()
        {
            GUILayout.Box("RectContent", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        }
        #endregion
    }
}
#endif
