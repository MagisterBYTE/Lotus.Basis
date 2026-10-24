#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения базового компонента логики MonoBehaviour
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public static class XMonoBehaviourEditorExtension
    {
        #region Main methods
        /// <summary>
        /// Сохранение данных компонента в режиме редактора
        /// </summary>
        /// <param name="monoBehaviour">Базовый компонент логики</param>
        public static void SaveInEditor(this MonoBehaviour monoBehaviour)
        {
            EditorUtility.SetDirty(monoBehaviour);
            if (!EditorApplication.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(monoBehaviour.gameObject.scene);
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
