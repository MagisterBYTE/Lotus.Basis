#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор для пользовательского ресурса
    /// </summary>
    [CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class LotusInspectorScriptableObjectEditor : LotusInspectorBaseEditor
    {
    }
    /**@}*/
}
#endif
