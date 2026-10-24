#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор для пользовательского скрипта
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class LotusInspectorMonoBehaviourEditor : LotusInspectorBaseEditor
    {
    }
    /**@}*/
}
#endif
